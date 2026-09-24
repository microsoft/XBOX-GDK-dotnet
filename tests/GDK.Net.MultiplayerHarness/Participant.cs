using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.PlayFab;
using GDK.Net.PlayFab.Multiplayer;
using GDK.Net.PlayFab.Party;

namespace GDK.Net.MultiplayerHarness;

/// <summary>
/// One player. Authenticates against PlayFab, then runs a frame loop that drains commands from the
/// orchestrator, pumps the Lobby and Party state-change queues and answers when the change it is
/// waiting for arrives.
/// </summary>
/// <remarks>
/// <para>
/// This is the shape of a real title, which is the point of testing multiplayer this way at all.
/// Neither Lobby nor Party is <c>Task</c>-based: an operation is started, and its completion turns
/// up later on a queue the title drains once a frame. Any test that awaits a helper instead of
/// pumping is testing the helper.
/// </para>
/// <para>
/// So commands are not executed where they are read. A command either completes immediately:
/// <c>lobby-post-update</c> just starts a native call, or registers a <see cref="Pending"/>
/// predicate that the pump evaluates against every state change until it matches or the deadline
/// passes. Only then is the reply written.
/// </para>
/// </remarks>
internal sealed class Participant : IDisposable
{
    /// <summary>How long a command may wait for the state change that completes it.</summary>
    /// <remarks>
    /// Generous because the budget has to cover PlayFab's own throttling: with several participants
    /// the title's request rate limit is reached during login, and the lobby service keeps applying
    /// backpressure for a while afterwards, so the first lobby operation of a run can take far
    /// longer than the steady-state one that follows it.
    /// </remarks>
    private static readonly TimeSpan CommandTimeout = TimeSpan.FromSeconds(120);

    /// <summary>
    /// How long to wait for Party's quality-of-service probe. Longer than
    /// <see cref="CommandTimeout"/> because it is measured before the pending command is
    /// registered, so it does not compete with that budget.
    /// </summary>
    private static readonly TimeSpan RegionTimeout = TimeSpan.FromSeconds(60);

    /// <summary>One tick of the loop. 60Hz is a game's frame budget, not an arbitrary poll.</summary>
    private static readonly TimeSpan FrameInterval = TimeSpan.FromMilliseconds(16);

    private readonly HarnessOptions _options;
    private readonly ConcurrentQueue<Command> _inbox = new();
    private readonly List<Pending> _pending = new();
    private readonly TextWriter _out;

    private PlayFabServiceConfig? _config;
    private PlayFabEntity? _entity;
    private PlayFabMultiplayer? _multiplayer;
    private PartyManager? _party;
    private PartyLocalUser? _partyUser;
    private PartyNetwork? _network;
    private PartyEndpoint? _localEndpoint;
    private Lobby? _lobby;
    private bool _running = true;
    private DateTime _nextHeartbeat = DateTime.MinValue;

    public Participant(HarnessOptions options)
    {
        _options = options;
        _out = Console.Out;
    }

    /// <summary>The participant's own name, used only to make the log readable.</summary>
    public string Name => _options.ParticipantName;

    public async Task<int> RunAsync()
    {
        // Reading stdin blocks, so it gets its own thread and hands lines to the loop through a
        // queue. The loop must never block: it owns the pumps.
        var reader = new Thread(ReadCommands) { IsBackground = true, Name = "stdin" };
        reader.Start();

        using GameRuntime runtime = GameRuntime.Initialize();
        PlayFabRuntime.Initialize();

        try
        {
            Send(new Message(Message.ReadyKind, Text: Name));

            while (_running)
            {
                DrainInbox();
                Pump();
                ExpirePending();
                Heartbeat();

                await Task.Delay(FrameInterval).ConfigureAwait(false);
            }

            return 0;
        }
        finally
        {
            // Every handle has to go before GameRuntime.Dispose runs PlayFab's shutdown.
            TearDown();
        }
    }

    public void Dispose() => TearDown();

    // ----------------------------------------------------------------------------------------
    // The loop
    // ----------------------------------------------------------------------------------------

    private void ReadCommands()
    {
        string? line;
        while ((line = Console.In.ReadLine()) is not null)
        {
            if (line.Length == 0)
            {
                continue;
            }

            try
            {
                if (Protocol.ParseCommand(line) is Command command)
                {
                    _inbox.Enqueue(command);
                }
            }
            catch (Exception ex)
            {
                Log($"could not parse a command: {ex.Message}");
            }
        }

        // The orchestrator closed the pipe; nothing more is coming.
        _running = false;
    }

    private void DrainInbox()
    {
        while (_inbox.TryDequeue(out Command? command))
        {
            try
            {
                Execute(command);
            }
            catch (Exception ex)
            {
                Reply(command.Id, false, $"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Drains both state-change queues and offers every change to the waiting commands. This is the
    /// once-per-frame call a title makes.
    /// </summary>
    private void Pump()
    {
        if (_multiplayer is not null)
        {
            foreach (LobbyStateChange change in _multiplayer.ProcessLobbyStateChanges())
            {
                Trace(change);
                Offer(change);
            }

            foreach (MatchmakingStateChange change in _multiplayer.ProcessMatchmakingStateChanges())
            {
                Trace(change);
                Offer(change);
            }
        }

        if (_party is not null)
        {
            foreach (PartyStateChange change in _party.ProcessStateChanges())
            {
                Trace(change);
                Observe(change);
                Offer(change);
            }
        }

        // A few commands wait on state the pumps mutate rather than on a change of their own --
        // "are there three members yet" reads Lobby.Members, which the pump refreshed above.
        for (int i = _pending.Count - 1; i >= 0; i--)
        {
            if (_pending[i].Poll is { } poll && poll())
            {
                Complete(i, true, "satisfied");
            }
        }
    }

    /// <summary>
    /// Latches the handles that arrive by state change rather than by return value. Party hands the
    /// network and the local endpoint back this way, so they have to be caught in the pump.
    /// </summary>
    private void Observe(PartyStateChange change)
    {
        switch (change)
        {
            case PartyConnectToNetworkCompleted { Succeeded: true, Network: not null } connected:
                _network = connected.Network;
                break;

            case PartyCreateEndpointCompleted { Succeeded: true, Endpoint: not null } created:
                _localEndpoint = created.Endpoint;
                break;
        }
    }

    /// <summary>
    /// Offers a state change to every waiting command.
    /// </summary>
    /// <remarks>
    /// A matcher signals a failed operation by throwing, so the throw has to fail that one command
    /// rather than escape into the frame loop -- an unhandled exception here would kill the
    /// participant, and the orchestrator would report the far less useful "exited before answering"
    /// instead of what actually went wrong.
    /// </remarks>
    private void Offer(object change)
    {
        for (int i = _pending.Count - 1; i >= 0; i--)
        {
            string? description;
            try
            {
                description = _pending[i].Match(change);
            }
            catch (Exception ex)
            {
                Complete(i, false, $"{ex.GetType().Name}: {ex.Message}");
                continue;
            }

            if (description is not null)
            {
                Complete(i, true, description);
            }
        }
    }

    private void ExpirePending()
    {
        DateTime now = DateTime.UtcNow;
        for (int i = _pending.Count - 1; i >= 0; i--)
        {
            if (now > _pending[i].Deadline)
            {
                Complete(i, false, $"timed out after {CommandTimeout.TotalSeconds:F0}s waiting for {_pending[i].Description}");
            }
        }
    }

    private void Complete(int index, bool ok, string text)
    {
        Pending pending = _pending[index];
        _pending.RemoveAt(index);
        Reply(pending.CommandId, ok, text, ok ? pending.ResolveData() : pending.Data);
    }

    private void Wait(
        int commandId,
        string description,
        Func<object, string?> match,
        Func<bool>? poll = null,
        Dictionary<string, string>? data = null) =>
        _pending.Add(new Pending(
            commandId, description, match, poll, data, DateTime.UtcNow + CommandTimeout));

    // ----------------------------------------------------------------------------------------
    // Verbs
    // ----------------------------------------------------------------------------------------

    private void Execute(Command command)
    {
        switch (command.Verb)
        {
            case Verbs.Login: Login(command); break;
            case Verbs.Shutdown: _running = false; Reply(command.Id, true, "shutting down"); break;

            case Verbs.LobbyCreate: LobbyCreate(command); break;
            case Verbs.LobbyJoin: LobbyJoin(command); break;
            case Verbs.LobbyAwaitMembers: LobbyAwaitMembers(command); break;
            case Verbs.LobbyPostUpdate: LobbyPostUpdate(command); break;
            case Verbs.LobbyAwaitProperty: LobbyAwaitProperty(command); break;
            case Verbs.LobbyLeave: LobbyLeave(command); break;

            case Verbs.PartyPrepare: PartyPrepare(command); break;
            case Verbs.PartyCreateNetwork: PartyCreateNetwork(command); break;
            case Verbs.PartyConnect: PartyConnect(command); break;
            case Verbs.PartyCreateEndpoint: PartyCreateEndpoint(command); break;
            case Verbs.PartyAwaitEndpoints: PartyAwaitEndpoints(command); break;
            case Verbs.PartySend: PartySend(command); break;
            case Verbs.PartyAwaitMessage: PartyAwaitMessage(command); break;
            case Verbs.PartyLeave: PartyLeave(command); break;

            default:
                Reply(command.Id, false, $"unknown verb '{command.Verb}'");
                break;
        }
    }

    /// <summary>
    /// Authenticates with a synthetic id. This is why the harness can run N processes on one box:
    /// a custom id needs no signed-in account, so each process is simply a different player.
    /// </summary>
    /// <remarks>
    /// Titles routinely have "allow client to create accounts" turned off, and 10D176 is one of
    /// them -- a bare client <c>LoginWithCustomID</c> with <c>CreateAccount</c> comes back
    /// <c>E_PF_PLAYER_CREATION_DISABLED</c>. So when a developer secret key is available the
    /// account is provisioned first through the server API, which is not subject to that setting,
    /// and the client login then merely signs in to an account that already exists. That is also
    /// how a real title with a backend does it. Without a secret key the harness falls back to
    /// asking the client login to create the account, which works on a permissive title.
    /// </remarks>
    private void Login(Command command)
    {
        _config = new PlayFabServiceConfig(_options.ApiEndpoint, _options.TitleId);

        string customId = command.Arg("customId");
        string? secretKey = HarnessOptions.FindSecretKey();
        bool provisioned = false;

        if (secretKey is not null)
        {
            Retry(() => Authentication.ServerLoginWithCustomIDAsync(
                _config,
                secretKey,
                new AuthenticationServerLoginWithCustomIDRequest
                {
                    CustomId = customId,
                    CreateAccount = true,
                }).GetAwaiter().GetResult());
            provisioned = true;
        }

        // The server login above returns a token, not a handle. Party wants a real PFEntityHandle,
        // so the player still signs in from the client the way a title with a backend would.
        PlayFabLoginResult login = Retry(() => Authentication.LoginWithCustomIDAsync(
            _config,
            new AuthenticationLoginWithCustomIDRequest
            {
                CustomId = customId,
                CreateAccount = !provisioned,
            }).GetAwaiter().GetResult());

        using (login)
        {
            _entity = login.Detach();
        }

        EntityKey key = _entity.Key;

        // PFMP is deliberately not started here; see Multiplayer(). Lobby is driven by entity key
        // plus token rather than by the handle -- see the comment on LobbyCreate for why -- and the
        // token is handed over when the instance is created.
        string how = provisioned ? "provisioned server-side, signed in" : "signed in";

        Reply(command.Id, true, $"{how} as {login.Result.PlayFabId}", new Dictionary<string, string>
        {
            ["entityId"] = key.Id ?? string.Empty,
            ["entityType"] = key.Type ?? string.Empty,
            ["playFabId"] = login.Result.PlayFabId ?? string.Empty,
        });
    }

    /// <summary>
    /// Brings the Party manager up ahead of the scenario that needs it.
    /// </summary>
    /// <remarks>
    /// Party measures region latency in the background from the moment the manager exists, and a
    /// network cannot be created until that probe has reported, so starting it early is worth a
    /// command of its own. It is deliberately <em>not</em> done during login: bringing Party up
    /// permanently forfeits the ability to shut the multiplayer library down again -- once
    /// PartyInitialize has run, PFMultiplayerUninitialize blocks forever -- so the lobby scenario
    /// gets to finish and hand its PFMP instance back first.
    /// </remarks>
    private void PartyPrepare(Command command)
    {
        RetirePlayFabMultiplayer();

        _ = Party();
        Reply(command.Id, true, "party manager started");
    }

    /// <summary>
    /// Shuts the multiplayer library down without letting it hang the loop.
    /// </summary>
    /// <remarks>
    /// The bounded wait that used to live here now lives in <see cref="PlayFabMultiplayer.Dispose"/>,
    /// so this is just an ordinary dispose that clears the participant's references.
    /// </remarks>
    private void RetirePlayFabMultiplayer()
    {
        PlayFabMultiplayer? multiplayer = _multiplayer;
        _multiplayer = null;
        _lobby = null;

        if (multiplayer is null)
        {
            return;
        }

        Try(multiplayer.Dispose);
    }

    /// <summary>
    /// Runs a PlayFab call, retrying the errors that only mean "someone else got there first".
    /// </summary>
    /// <remarks>
    /// The participants log in at the same instant, and creating several players in one title at
    /// once makes PlayFab return <c>E_PF_CONCURRENT_EDIT_ERROR</c> to whichever loser of the race
    /// it picks; enough participants at once also earns the title's per-second request rate limit.
    /// Both are genuinely transient service conditions rather than projection defects, and a title
    /// would retry them, so the harness does too -- otherwise a scenario that has nothing to do
    /// with account creation fails for a reason it cannot control. The backoff carries jitter
    /// because participants that back off in lockstep just collide again.
    /// </remarks>
    private T Retry<T>(Func<T> call)
    {
        const int Attempts = 6;
        for (int attempt = 1; ; attempt++)
        {
            try
            {
                return call();
            }
            catch (PlayFabException ex) when (attempt < Attempts && IsTransient(ex))
            {
                Log($"retrying after {ex.Message} (attempt {attempt} of {Attempts})");
                int backoff = 500 * (1 << attempt);
                Thread.Sleep(TimeSpan.FromMilliseconds(backoff + Random.Shared.Next(backoff / 2)));
            }
        }
    }

    private void Retry(Action call) => Retry<object?>(() => { call(); return null; });

    private static bool IsTransient(PlayFabException exception) =>
        exception.HResult is PlayFabErrors.ConcurrentEditError
                          or PlayFabErrors.InternalServerError
                          or PlayFabErrors.ServiceUnavailable
                          or PlayFabErrors.DownstreamServiceUnavailable
                          or PlayFabErrors.ApiRequestLimitExceeded
                          or PlayFabErrors.ApiClientRequestRateLimitExceeded;

    // ----------------------------------------------------------------------------------------
    // Lobby
    // ----------------------------------------------------------------------------------------

    /// <remarks>
    /// The lobby calls take an <see cref="EntityKey"/> rather than the <see cref="PlayFabEntity"/>
    /// handle even though the projection offers both. The GDK 260404 headers mark every
    /// <c>PFMultiplayer*WithEntityHandle</c> lobby entry point <c>&lt;nyi /&gt;</c>: the export
    /// exists but has no implementation behind it, and calling one faults the process rather than
    /// returning a failure HRESULT. The key-plus-token form -- the token having been handed to PFMP
    /// by <c>SetEntityToken</c> during login -- is the one that actually works.
    /// </remarks>
    private void LobbyCreate(Command command)
    {
        var configuration = new LobbyCreateConfiguration
        {
            MaxMemberCount = (uint)command.IntArg("maxMembers", 8),
            OwnerMigrationPolicy = LobbyOwnerMigrationPolicy.Automatic,

            // Private, because the guests are handed the connection string directly and joining by
            // connection string does not require a public lobby. A public lobby is additionally
            // published to the title's searchable index, and that indexing is what makes creation
            // intermittently take longer than any reasonable frame-loop budget.
            AccessPolicy = LobbyAccessPolicy.Private,
        };

        // Search properties are only set when a caller asks for them. They make a lobby findable,
        // which nothing here needs -- the connection string is handed to the guests directly -- and
        // PlayFab requires the reserved "string_keyN" names rather than arbitrary ones.
        if (command.OptionalArg("tag") is string tag)
        {
            configuration.SearchProperties = new Dictionary<string, string>
            {
                ["string_key1"] = tag,
            };
        }

        (OperationId operation, Lobby lobby) = Multiplayer().CreateAndJoinLobby(Entity().Key, configuration);
        _lobby = lobby;

        Wait(command.Id, "CreateAndJoinLobbyCompleted", change => change switch
        {
            CreateAndJoinLobbyCompleted done when done.Operation == operation =>
                done.Failed
                    ? throw new InvalidOperationException($"CreateAndJoinLobby failed: 0x{done.ResultCode:X8}")
                    : $"created lobby {done.Lobby?.Id}",
            _ => null,
        }, data: new Dictionary<string, string>());

        // The connection string is what the other processes need, and it is readable as soon as the
        // handle exists rather than only on completion.
        Latch(command.Id, () => new Dictionary<string, string>
        {
            ["connectionString"] = _lobby?.ConnectionString ?? string.Empty,
            ["lobbyId"] = _lobby?.Id ?? string.Empty,
        });
    }

    private void LobbyJoin(Command command)
    {
        (OperationId operation, Lobby lobby) = Multiplayer()
            .JoinLobby(Entity().Key, command.Arg("connectionString"));
        _lobby = lobby;

        Wait(command.Id, "JoinLobbyCompleted", change => change switch
        {
            JoinLobbyCompleted done when done.Operation == operation =>
                done.Failed
                    ? throw new InvalidOperationException($"JoinLobby failed: 0x{done.ResultCode:X8}")
                    : $"joined lobby {done.Lobby?.Id}",
            _ => null,
        });
    }

    /// <summary>
    /// Waits until the lobby reports at least N members. This is the assertion that membership
    /// actually propagated between processes rather than each one merely believing it joined.
    /// </summary>
    private void LobbyAwaitMembers(Command command)
    {
        int expected = command.IntArg("count", 2);
        Wait(
            command.Id,
            $"{expected} lobby member(s)",
            _ => null,
            () => Lobby().Members.Count >= expected);

        Latch(command.Id, () => new Dictionary<string, string>
        {
            ["memberCount"] = Lobby().Members.Count.ToString(),
        });
    }

    private void LobbyPostUpdate(Command command)
    {
        OperationId operation = Lobby().PostUpdate(
            Entity().Key,
            new LobbyDataUpdate
            {
                LobbyProperties = new Dictionary<string, string>
                {
                    [command.Arg("key")] = command.Arg("value"),
                },
            });

        Wait(command.Id, "PostUpdateCompleted", change => change switch
        {
            PostUpdateCompleted done when done.Operation == operation =>
                done.Failed
                    ? throw new InvalidOperationException($"PostUpdate failed: 0x{done.ResultCode:X8}")
                    : "posted the update",
            _ => null,
        });
    }

    /// <summary>
    /// Waits for a property another process wrote. The state change only names the keys that
    /// changed, so the value is read back off the lobby -- which is exactly how a title learns that
    /// the host changed the map.
    /// </summary>
    private void LobbyAwaitProperty(Command command)
    {
        string key = command.Arg("key");
        string expected = command.Arg("value");

        Wait(
            command.Id,
            $"lobby property '{key}' = '{expected}'",
            _ => null,
            () => Lobby().Properties.TryGetValue(key, out string? actual) && actual == expected);
    }

    private void LobbyLeave(Command command)
    {
        OperationId operation = Lobby().Leave(Entity().Key);

        Wait(command.Id, "LeaveLobbyCompleted", change => change switch
        {
            LeaveLobbyCompleted done when done.Operation == operation => "left the lobby",
            _ => null,
        });
    }

    // ----------------------------------------------------------------------------------------
    // Party network
    // ----------------------------------------------------------------------------------------

    private void PartyCreateNetwork(Command command)
    {
        IReadOnlyList<PartyRegion> regions = AwaitRegions();
        if (regions.Count == 0)
        {
            Reply(command.Id, false,
                "PartyGetRegions reported no regions after fifteen seconds of pumping; this " +
                "device has no route to the Party quality-of-service endpoints");
            return;
        }

        var configuration = new PartyNetworkConfiguration
        {
            MaxUserCount = (uint)command.IntArg("maxUsers", 8),
            MaxDeviceCount = (uint)command.IntArg("maxDevices", 8),
            MaxUsersPerDeviceCount = 1,
            MaxDevicesPerUserCount = 1,
            MaxEndpointsPerDeviceCount = 1,
            DirectPeerConnectivityOptions = PartyDirectPeerConnectivityOptions.None,
        };

        (PartyOperationId operation, PartyNetworkDescriptor descriptor, string invitation) =
            Party().CreateNewNetwork(PartyUser(), configuration, regions);
        _ = descriptor;

        string? token = null;
        string? region = null;

        Wait(command.Id, "PartyCreateNewNetworkCompleted", change => change switch
        {
            PartyCreateNewNetworkCompleted done when done.Operation == operation =>
                done.Succeeded
                    ? Capture(done)
                    : throw new InvalidOperationException(
                        $"PartyCreateNewNetwork failed: {done.Result} (detail {done.ErrorDetail})"),
            _ => null,
        });

        // The descriptor CreateNewNetwork hands back synchronously is a placeholder -- serializing
        // it throws until the network actually exists -- so the join token is taken from the
        // completion instead, and attached to the reply once it is known.
        Latch(command.Id, () => new Dictionary<string, string>
        {
            ["descriptor"] = token ?? string.Empty,
            ["invitation"] = invitation,
            ["region"] = region ?? string.Empty,
        });

        string Capture(PartyCreateNewNetworkCompleted done)
        {
            token = done.Descriptor.Serialize();
            region = done.Descriptor.RegionName;
            return $"created a network in {region}";
        }
    }

    /// <summary>
    /// Pumps until Party has measured region latency, or gives up after fifteen seconds.
    /// </summary>
    /// <remarks>
    /// Party discovers regions by running a quality-of-service probe against Azure endpoints and
    /// reports the result as a state change, so <c>PartyGetRegions</c> is empty for the first
    /// moments after the manager exists and a network cannot be created until it fills in. Several
    /// participants on one machine probe at once, which makes this markedly slower than the
    /// single-process case, so the budget is generous. It blocks the frame loop, which is fine
    /// here: the participant has been told to create a network and has nothing else to do until it
    /// can.
    /// </remarks>
    private IReadOnlyList<PartyRegion> AwaitRegions()
    {
        PartyManager party = Party();
        var clock = System.Diagnostics.Stopwatch.StartNew();
        IReadOnlyList<PartyRegion> regions = party.GetRegions();

        while (regions.Count == 0 && clock.Elapsed < RegionTimeout)
        {
            foreach (PartyStateChange change in party.ProcessStateChanges())
            {
                Observe(change);
            }

            regions = party.GetRegions();
            if (regions.Count == 0)
            {
                if (clock.ElapsedMilliseconds % 5000 < 100)
                {
                    Log($"still waiting for Party regions after {clock.Elapsed.TotalSeconds:F0}s");
                }

                Thread.Sleep(100);
            }
        }

        if (regions.Count > 0)
        {
            Log($"Party reported {regions.Count} region(s) after {clock.Elapsed.TotalSeconds:F1}s, " +
                $"closest '{regions[0].RegionName}'");
        }

        return regions;
    }

    /// <summary>
    /// Connects to a network and authenticates into it. The creator runs this too, against its own
    /// descriptor: creating a network does not join it.
    /// </summary>
    private void PartyConnect(Command command)
    {
        PartyNetworkDescriptor descriptor =
            PartyNetworkDescriptor.Deserialize(command.Arg("descriptor"));

        (PartyOperationId connect, PartyNetwork network) = Party().ConnectToNetwork(descriptor);
        _network = network;

        string? invitation = command.OptionalArg("invitation");
        PartyOperationId authenticate = network.AuthenticateLocalUser(
            PartyUser(), string.IsNullOrEmpty(invitation) ? null : invitation);

        bool connected = false;
        Wait(command.Id, "PartyAuthenticateLocalUserCompleted", change =>
        {
            switch (change)
            {
                case PartyConnectToNetworkCompleted done when done.Operation == connect:
                    if (!done.Succeeded)
                    {
                        throw new InvalidOperationException(
                            $"PartyConnectToNetwork failed: {done.Result} (detail {done.ErrorDetail})");
                    }

                    connected = true;
                    return null;

                case PartyAuthenticateLocalUserCompleted done when done.Operation == authenticate:
                    if (!done.Succeeded)
                    {
                        throw new InvalidOperationException(
                            $"PartyAuthenticateLocalUser failed: {done.Result} (detail {done.ErrorDetail})");
                    }

                    return connected
                        ? "connected and authenticated"
                        : "authenticated (the connect completion had not arrived yet)";

                default:
                    return null;
            }
        });
    }

    private void PartyCreateEndpoint(Command command)
    {
        PartyOperationId operation = Network().CreateEndpoint(PartyUser());

        Wait(command.Id, "PartyCreateEndpointCompleted", change => change switch
        {
            PartyCreateEndpointCompleted done when done.Operation == operation =>
                done.Succeeded
                    ? $"created endpoint {done.Endpoint?.UniqueIdentifier}"
                    : throw new InvalidOperationException(
                        $"PartyCreateEndpoint failed: {done.Result} (detail {done.ErrorDetail})"),
            _ => null,
        });
    }

    /// <summary>
    /// Waits until N endpoints are visible, which is how a process learns the others have finished
    /// joining and are addressable.
    /// </summary>
    private void PartyAwaitEndpoints(Command command)
    {
        int expected = command.IntArg("count", 2);
        Wait(
            command.Id,
            $"{expected} party endpoint(s)",
            _ => null,
            () => Network().Endpoints.Count >= expected);
    }

    /// <summary>Broadcasts to every remote endpoint.</summary>
    private void PartySend(Command command)
    {
        var targets = new List<PartyEndpoint>();
        foreach (PartyEndpoint endpoint in Network().Endpoints)
        {
            if (!endpoint.IsLocal)
            {
                targets.Add(endpoint);
            }
        }

        if (targets.Count == 0)
        {
            Reply(command.Id, false, "no remote endpoints to send to");
            return;
        }

        byte[] payload = System.Text.Encoding.UTF8.GetBytes(command.Arg("text"));
        LocalEndpoint().SendMessage(targets, payload, PartySendMessageOptions.GuaranteedDelivery);

        Reply(command.Id, true, $"sent {payload.Length} byte(s) to {targets.Count} endpoint(s)");
    }

    private void PartyAwaitMessage(Command command)
    {
        string expected = command.Arg("text");

        Wait(command.Id, $"a party message reading '{expected}'", change => change switch
        {
            PartyEndpointMessageReceived received
                when System.Text.Encoding.UTF8.GetString(received.Message) == expected =>
                $"received '{expected}' from endpoint {received.SenderEndpoint?.UniqueIdentifier}",
            _ => null,
        });
    }

    private void PartyLeave(Command command)
    {
        PartyOperationId operation = Network().Leave();

        Wait(command.Id, "PartyLeaveNetworkCompleted", change => change switch
        {
            PartyLeaveNetworkCompleted done when done.Operation == operation => "left the network",
            _ => null,
        });
    }

    // ----------------------------------------------------------------------------------------
    // Plumbing
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// Attaches data to a reply that is not known until the pending command completes. Used where
    /// the value is readable straight away but the reply is not sent until the completion arrives.
    /// </summary>
    private void Latch(int commandId, Func<Dictionary<string, string>> factory)
    {
        for (int i = 0; i < _pending.Count; i++)
        {
            if (_pending[i].CommandId == commandId)
            {
                _pending[i] = _pending[i] with { DataFactory = factory };
                return;
            }
        }
    }

    private PlayFabEntity Entity() =>
        _entity ?? throw new InvalidOperationException("This participant has not logged in yet.");

    private Lobby Lobby() =>
        _lobby ?? throw new InvalidOperationException("This participant is not in a lobby.");

    private PartyNetwork Network() =>
        _network ?? throw new InvalidOperationException("This participant is not in a party network.");

    private PartyEndpoint LocalEndpoint() =>
        _localEndpoint ?? throw new InvalidOperationException("This participant has no endpoint.");

    /// <summary>
    /// The multiplayer library, started on first use rather than at login.
    /// </summary>
    /// <remarks>
    /// Deferred deliberately. An instance that is initialized and then left idle while the rest of
    /// the run gets going is markedly less reliable than one used straight away -- its first lobby
    /// operation is the one that stalls -- so the participant pays for it at the moment it has work
    /// for it, and hands it the entity token then.
    /// </remarks>
    private PlayFabMultiplayer Multiplayer()
    {
        if (_multiplayer is null)
        {
            _multiplayer = PlayFabMultiplayer.Initialize(_options.TitleId);
            _multiplayer.SetEntityToken(Entity().Key, EntityToken());
        }

        return _multiplayer;
    }

    private string EntityToken() =>
        Entity().GetEntityTokenAsync().GetAwaiter().GetResult().Token
            ?? throw new InvalidOperationException("PlayFab returned an entity with no token.");

    private PartyManager Party()
    {
        if (_party is null)
        {
            // Party binds a fixed UDP port by default, so the second participant on this machine
            // would fail to bind. Port 0 alone is not enough on Game Core -- there it means "the
            // Game Core preferred multiplayer port", which is exactly the fixed port being fought
            // over -- so the exclusion flag goes with it to get a dynamically assigned one.
            PartyManager.SetLocalUdpSocketBindAddress(
                new PartyLocalUdpSocketBindAddressConfiguration
                {
                    Options = PartyLocalUdpSocketBindAddressOptions
                        .ExcludeGameCorePreferredUdpMultiplayerPort,
                    Port = 0,
                });

            _party = PartyManager.Initialize(_options.TitleId);
        }

        return _party;
    }

    /// <summary>
    /// The Party local user, created on first use from the authenticated entity. Party measures
    /// region latency from the moment the manager exists, so touching it early is deliberate.
    /// </summary>
    private PartyLocalUser PartyUser() => _partyUser ??= Party().CreateLocalUser(Entity());

    private void Reply(int id, bool ok, string text, Dictionary<string, string>? data = null) =>
        Send(new Message(Message.ReplyKind, id, ok, text, data));

    private void Log(string text) => Send(new Message(Message.LogKind, Text: text));

    /// <summary>
    /// Reports a state change under <c>--verbose</c>. Both stacks deliver everything -- including
    /// their failures -- as state changes, so when an operation never completes this trace is the
    /// only way to tell "the service said no" apart from "nothing came back at all".
    /// </summary>
    private void Trace(object change)
    {
        if (_options.Verbose)
        {
            Log($"state change {change.GetType().Name}: {change}");
        }
    }

    /// <summary>
    /// Reports under <c>--verbose</c> that the loop is still turning while a command is pending.
    /// This is what distinguishes an operation the service has not answered from a native call that
    /// never returned and took the pump with it -- two failures that look identical from outside.
    /// </summary>
    private void Heartbeat()
    {
        if (!_options.Verbose || _pending.Count == 0 || DateTime.UtcNow < _nextHeartbeat)
        {
            return;
        }

        _nextHeartbeat = DateTime.UtcNow.AddSeconds(5);
        Log($"pump alive, {_pending.Count} command(s) pending: " +
            string.Join(", ", _pending.ConvertAll(p => p.Description)));
    }

    private void Send(Message message)
    {
        lock (_out)
        {
            _out.WriteLine(Protocol.Serialize(message));
            _out.Flush();
        }
    }

    /// <summary>
    /// Releases everything in the order PlayFab requires. Best-effort throughout: a participant is
    /// often torn down after a failure, and a second exception here would hide the first.
    /// </summary>
    private void TearDown()
    {
        Try(() => _network?.Leave());

        // PartyPrepare has usually retired the multiplayer library already; this is the fallback.
        // The order relative to Party does not matter, but an outstanding operation can still make
        // PFMultiplayerUninitialize block, which is why it is retired on an abandonable thread.
        RetirePlayFabMultiplayer();
        Try(() => _party?.Dispose());
        Try(() => _entity?.Dispose());
        Try(() => _config?.Dispose());

        _network = null;
        _localEndpoint = null;
        _partyUser = null;
        _party = null;
        _multiplayer = null;
        _lobby = null;
        _entity = null;
        _config = null;
    }

    private static void Try(Action action)
    {
        try
        {
            action();
        }
        catch
        {
            // Teardown is best-effort by design.
        }
    }

    /// <summary>A command that cannot answer until the pump sees something.</summary>
    /// <param name="CommandId">The command to reply to.</param>
    /// <param name="Description">What is being waited for, used in the timeout message.</param>
    /// <param name="Match">Returns a description when a state change satisfies the wait.</param>
    /// <param name="Poll">Satisfies the wait by reading state instead of matching a change.</param>
    /// <param name="Data">Data to return with the reply.</param>
    /// <param name="Deadline">When to give up.</param>
    private sealed record Pending(
        int CommandId,
        string Description,
        Func<object, string?> Match,
        Func<bool>? Poll,
        Dictionary<string, string>? Data,
        DateTime Deadline)
    {
        public Func<Dictionary<string, string>>? DataFactory { get; init; }

        public Dictionary<string, string>? ResolveData()
        {
            if (DataFactory is null)
            {
                return Data;
            }

            Dictionary<string, string> produced = DataFactory();
            if (Data is null)
            {
                return produced;
            }

            foreach (KeyValuePair<string, string> pair in produced)
            {
                Data[pair.Key] = pair.Value;
            }

            return Data;
        }
    }
}
