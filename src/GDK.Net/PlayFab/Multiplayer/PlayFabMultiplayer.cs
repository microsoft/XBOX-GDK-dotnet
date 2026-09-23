using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// The PlayFab Multiplayer library (<c>PFMultiplayer.h</c>, <c>PFLobby.h</c>,
/// <c>PFMatchmaking.h</c>): lobbies and matchmaking, driven by a per-frame state-change pump.
/// </summary>
/// <remarks>
/// <para>
/// Unlike the PlayFab Services APIs, PFMP operations are not awaitable. A start call returns
/// synchronously with an <see cref="OperationId"/>, and its completion arrives later as a record
/// from <see cref="ProcessLobbyStateChanges"/> or <see cref="ProcessMatchmakingStateChanges"/>.
/// Both loops must be pumped once per frame from the title's update thread.
/// </para>
/// <para>
/// Native state-change memory is only valid between the library's <c>StartProcessing</c> and
/// <c>FinishProcessing</c> calls, which the enumerator brackets: <c>FinishProcessing</c> runs when
/// the <c>foreach</c> leaves scope. Each record snapshots the values it exposes, so a record may
/// safely outlive the loop; <see cref="Lobby"/> and <see cref="MatchmakingTicket"/> references stay
/// valid until their teardown change arrives.
/// </para>
/// </remarks>
public sealed unsafe class PlayFabMultiplayer : IDisposable
{
    private readonly Dictionary<IntPtr, Lobby> _lobbies = new();
    private readonly Dictionary<IntPtr, MatchmakingTicket> _tickets = new();
    private readonly IntPtr _handle;
    private long _nextOperation;
    private bool _disposed;

    private PlayFabMultiplayer(IntPtr handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Initializes the multiplayer library for a title (<c>PFMultiplayerInitialize</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Third in the fixed startup order (see <see cref="SubsystemOrder"/>), so the Gaming Runtime
    /// must already be up. That requirement is documented on <c>PFMultiplayerInitialize</c> but not
    /// enforced by it: starting early still returns <c>S_OK</c> and a usable-looking handle, but
    /// the library's PubSub worker is never dispatched, so operations never complete and
    /// <see cref="Dispose"/> then parks forever in <c>PubSubSubscriptionManager::Shutdown</c>. This
    /// checks the runtime up front so the mistake is reported here rather than at an unrelated
    /// teardown much later.
    /// </para>
    /// <para>
    /// The Game Core networking stack also has to be up, so this waits for it. Because the stack
    /// usually comes up within a few hundred milliseconds of process start, omitting the wait makes
    /// the same hang a race that only shows up under load or on a cold boot -- the worst possible
    /// shape for a title to debug. Removing this wait reproduces the hang in roughly one run in
    /// three.
    /// </para>
    /// </remarks>
    /// <exception cref="InvalidOperationException">The Gaming Runtime is not initialized.</exception>
    public static PlayFabMultiplayer Initialize(string titleId)
    {
        if (string.IsNullOrEmpty(titleId))
        {
            throw new ArgumentException("A title id is required.", nameof(titleId));
        }

        RuntimeLifetime.RequireGameRuntime(nameof(NativePlayFab.PFMultiplayerInitialize));
        WaitForNetworkStack();

        IntPtr text = Utf8.Allocate(titleId);
        try
        {
            MultiplayerInitializationConfiguration configuration = default;
            configuration.TitleId = (byte*)text;
            configuration.MultiplayerTaskQueue = IntPtr.Zero;

            IntPtr handle;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerInitialize(&configuration, &handle));

            var multiplayer = new PlayFabMultiplayer(handle);
            RuntimeLifetime.Register(SubsystemOrder.PlayFabMultiplayer, multiplayer);
            return multiplayer;
        }
        finally
        {
            Utf8.Free(text);
        }
    }

    /// <summary>
    /// Blocks until <c>XNetworkingGetConnectivityHint</c> reports the network stack initialized.
    /// </summary>
    /// <remarks>
    /// Bounded, and gives up quietly. A title that genuinely has no network still has to be able to
    /// construct the library and get its errors back the normal way, so a machine that never
    /// reports an initialized stack is left to <c>PFMultiplayerInitialize</c> to judge rather than
    /// being refused here.
    /// </remarks>
    private static void WaitForNetworkStack()
    {
        const int BudgetMilliseconds = 10_000;
        const int PollMilliseconds = 50;

        for (int waited = 0; waited < BudgetMilliseconds; waited += PollMilliseconds)
        {
            XNetworkingConnectivityHint hint;
            if (HResult.Failed(Native.XNetworkingGetConnectivityHint(&hint)))
            {
                return;
            }

            if (hint.networkInitialized != 0)
            {
                return;
            }

            Thread.Sleep(PollMilliseconds);
        }
    }

    /// <summary>
    /// The library's description of an error code (<c>PFMultiplayerGetErrorMessage</c>).
    /// </summary>
    /// <exception cref="PlatformNotSupportedException">
    /// Always, on GDK 260404: the header marks this entry point <c>&lt;nyi /&gt;</c>. Use
    /// <see cref="PlayFabErrors.GetName(int)"/> for a symbolic name instead.
    /// </exception>
    public static string? GetErrorMessage(int errorCode)
    {
        Nyi.Throw(nameof(NativePlayFab.PFMultiplayerGetErrorMessage), "PlayFabErrors.GetName");
        return null;
    }

    /// <summary>
    /// Pins one of the library's internal threads to a set of cores
    /// (<c>PFMultiplayerSetThreadAffinityMask</c>).
    /// </summary>
    public static void SetThreadAffinityMask(MultiplayerThreadId threadId, ulong affinityMask) =>
        Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerSetThreadAffinityMask(
            (PFMultiplayerThreadId)threadId, affinityMask));

    internal IntPtr Handle
    {
        get
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PlayFabMultiplayer));
            }

            return _handle;
        }
    }

    /// <summary>
    /// Supplies or refreshes an entity's PlayFab token (<c>PFMultiplayerSetEntityToken</c>).
    /// </summary>
    public void SetEntityToken(EntityKey entity, string entityToken)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (entityToken is null)
        {
            throw new ArgumentNullException(nameof(entityToken));
        }

        IntPtr handle = Handle;
        var arena = new PlayFabArena();
        try
        {
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerSetEntityToken(
                handle, MultiplayerInterop.Write(arena, entity), arena.String(entityToken)));
        }
        finally
        {
            arena.Dispose();
        }
    }

    // ------------------------------------------------------------------------------------------
    // Lobby operations
    // ------------------------------------------------------------------------------------------

    /// <summary>
    /// Creates a lobby and joins it (<c>PFMultiplayerCreateAndJoinLobby</c>). Completes with
    /// <see cref="CreateAndJoinLobbyCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) CreateAndJoinLobby(
        EntityKey creator,
        LobbyCreateConfiguration createConfiguration,
        LobbyJoinConfiguration? joinConfiguration = null)
    {
        if (creator is null)
        {
            throw new ArgumentNullException(nameof(creator));
        }

        if (createConfiguration is null)
        {
            throw new ArgumentNullException(nameof(createConfiguration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyCreateConfiguration* create = arena.Alloc<PFLobbyCreateConfiguration>(1);
            createConfiguration.WriteTo(create, arena);

            // PFMP does not mark this parameter _In_opt_, so it dereferences the pointer
            // unconditionally and a nullptr faults the process. An omitted configuration is
            // therefore projected as a zeroed one -- which is the "nothing to say" form --
            // rather than as a null.
            PFLobbyJoinConfiguration* join = arena.Alloc<PFLobbyJoinConfiguration>(1);
            *join = default;
            joinConfiguration?.WriteTo(join, arena);

            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateAndJoinLobby(
                handle,
                MultiplayerInterop.Write(arena, creator),
                create,
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Creates a lobby and joins it as an entity the SDK already owns
    /// (<c>PFMultiplayerCreateAndJoinLobbyWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) CreateAndJoinLobby(
        PlayFabEntity creator,
        LobbyCreateConfiguration createConfiguration,
        LobbyJoinConfiguration? joinConfiguration = null)
    {
        if (creator is null)
        {
            throw new ArgumentNullException(nameof(creator));
        }

        if (createConfiguration is null)
        {
            throw new ArgumentNullException(nameof(createConfiguration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyCreateConfiguration* create = arena.Alloc<PFLobbyCreateConfiguration>(1);
            createConfiguration.WriteTo(create, arena);

            // PFMP does not mark this parameter _In_opt_, so it dereferences the pointer
            // unconditionally and a nullptr faults the process. An omitted configuration is
            // therefore projected as a zeroed one -- which is the "nothing to say" form --
            // rather than as a null.
            PFLobbyJoinConfiguration* join = arena.Alloc<PFLobbyJoinConfiguration>(1);
            *join = default;
            joinConfiguration?.WriteTo(join, arena);

            IntPtr lobby;
            Nyi.Throw("PFMultiplayerCreateAndJoinLobbyWithEntityHandle", "the CreateAndJoinLobby overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateAndJoinLobbyWithEntityHandle(
                handle, creator.Handle, create, join, Context(operation), &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins a lobby from a connection string (<c>PFMultiplayerJoinLobby</c>). Completes with
    /// <see cref="JoinLobbyCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) JoinLobby(
        EntityKey newMember, string connectionString, LobbyJoinConfiguration? configuration = null)
    {
        if (newMember is null)
        {
            throw new ArgumentNullException(nameof(newMember));
        }

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            // PFMP does not mark this parameter _In_opt_, so it dereferences the pointer
            // unconditionally and a nullptr faults the process. An omitted configuration is
            // therefore projected as a zeroed one -- which is the "nothing to say" form --
            // rather than as a null.
            PFLobbyJoinConfiguration* join = arena.Alloc<PFLobbyJoinConfiguration>(1);
            *join = default;
            configuration?.WriteTo(join, arena);

            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinLobby(
                handle,
                MultiplayerInterop.Write(arena, newMember),
                arena.String(connectionString),
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins a lobby as an entity the SDK already owns
    /// (<c>PFMultiplayerJoinLobbyWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) JoinLobby(
        PlayFabEntity newMember,
        string connectionString,
        LobbyJoinConfiguration? configuration = null)
    {
        if (newMember is null)
        {
            throw new ArgumentNullException(nameof(newMember));
        }

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            // PFMP does not mark this parameter _In_opt_, so it dereferences the pointer
            // unconditionally and a nullptr faults the process. An omitted configuration is
            // therefore projected as a zeroed one -- which is the "nothing to say" form --
            // rather than as a null.
            PFLobbyJoinConfiguration* join = arena.Alloc<PFLobbyJoinConfiguration>(1);
            *join = default;
            configuration?.WriteTo(join, arena);

            IntPtr lobby;
            Nyi.Throw("PFMultiplayerJoinLobbyWithEntityHandle", "the JoinLobby overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinLobbyWithEntityHandle(
                handle,
                newMember.Handle,
                arena.String(connectionString),
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Reconnects to a lobby the entity is already a member of
    /// (<c>PFMultiplayerConnectToLobby</c>). Completes with
    /// <see cref="ConnectToLobbyCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) ConnectToLobby(EntityKey newMember, string lobbyId)
    {
        if (newMember is null)
        {
            throw new ArgumentNullException(nameof(newMember));
        }

        if (lobbyId is null)
        {
            throw new ArgumentNullException(nameof(lobbyId));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerConnectToLobby(
                handle,
                MultiplayerInterop.Write(arena, newMember),
                arena.String(lobbyId),
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Reconnects to a lobby as an entity the SDK already owns
    /// (<c>PFMultiplayerConnectToLobbyWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) ConnectToLobby(
        PlayFabEntity newMember, string lobbyId)
    {
        if (newMember is null)
        {
            throw new ArgumentNullException(nameof(newMember));
        }

        if (lobbyId is null)
        {
            throw new ArgumentNullException(nameof(lobbyId));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerConnectToLobbyWithEntityHandle(
                handle, newMember.Handle, arena.String(lobbyId), Context(operation), &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins the lobby described by a matchmaking arrangement string
    /// (<c>PFMultiplayerJoinArrangedLobby</c>). Completes with
    /// <see cref="JoinArrangedLobbyCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) JoinArrangedLobby(
        EntityKey newMember,
        string arrangementString,
        LobbyArrangedJoinConfiguration configuration)
    {
        if (newMember is null)
        {
            throw new ArgumentNullException(nameof(newMember));
        }

        if (arrangementString is null)
        {
            throw new ArgumentNullException(nameof(arrangementString));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyArrangedJoinConfiguration* join =
                arena.Alloc<PFLobbyArrangedJoinConfiguration>(1);
            configuration.WriteTo(join, arena);

            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinArrangedLobby(
                handle,
                MultiplayerInterop.Write(arena, newMember),
                arena.String(arrangementString),
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins an arranged lobby as an entity the SDK already owns
    /// (<c>PFMultiplayerJoinArrangedLobbyWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) JoinArrangedLobby(
        PlayFabEntity newMember,
        string arrangementString,
        LobbyArrangedJoinConfiguration configuration)
    {
        if (newMember is null)
        {
            throw new ArgumentNullException(nameof(newMember));
        }

        if (arrangementString is null)
        {
            throw new ArgumentNullException(nameof(arrangementString));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyArrangedJoinConfiguration* join =
                arena.Alloc<PFLobbyArrangedJoinConfiguration>(1);
            configuration.WriteTo(join, arena);

            IntPtr lobby;
            Nyi.Throw("PFMultiplayerJoinArrangedLobbyWithEntityHandle", "the JoinArrangedLobby overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinArrangedLobbyWithEntityHandle(
                handle,
                newMember.Handle,
                arena.String(arrangementString),
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Searches for joinable lobbies (<c>PFMultiplayerFindLobbies</c>). Completes with
    /// <see cref="FindLobbiesCompleted"/>.
    /// </summary>
    public OperationId FindLobbies(
        EntityKey searchingEntity, LobbySearchConfiguration searchConfiguration)
    {
        if (searchingEntity is null)
        {
            throw new ArgumentNullException(nameof(searchingEntity));
        }

        if (searchConfiguration is null)
        {
            throw new ArgumentNullException(nameof(searchConfiguration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbySearchConfiguration* search = arena.Alloc<PFLobbySearchConfiguration>(1);
            searchConfiguration.WriteTo(search, arena);
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerFindLobbies(
                handle, MultiplayerInterop.Write(arena, searchingEntity), search, Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Searches for joinable lobbies as an entity the SDK already owns
    /// (<c>PFMultiplayerFindLobbiesWithEntityHandle</c>).
    /// </summary>
    public OperationId FindLobbies(
        PlayFabEntity searchingEntity, LobbySearchConfiguration searchConfiguration)
    {
        if (searchingEntity is null)
        {
            throw new ArgumentNullException(nameof(searchingEntity));
        }

        if (searchConfiguration is null)
        {
            throw new ArgumentNullException(nameof(searchConfiguration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbySearchConfiguration* search = arena.Alloc<PFLobbySearchConfiguration>(1);
            searchConfiguration.WriteTo(search, arena);
            Nyi.Throw("PFMultiplayerFindLobbiesWithEntityHandle", "the FindLobbies overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerFindLobbiesWithEntityHandle(
                handle, searchingEntity.Handle, search, Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Starts delivering lobby invites for an entity
    /// (<c>PFMultiplayerStartListeningForLobbyInvites</c>).
    /// </summary>
    public void StartListeningForLobbyInvites(EntityKey listeningEntity)
    {
        if (listeningEntity is null)
        {
            throw new ArgumentNullException(nameof(listeningEntity));
        }

        IntPtr handle = Handle;
        var arena = new PlayFabArena();
        try
        {
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerStartListeningForLobbyInvites(
                handle, MultiplayerInterop.Write(arena, listeningEntity)));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Starts delivering lobby invites for an entity the SDK already owns
    /// (<c>PFMultiplayerStartListeningForLobbyInvitesWithEntityHandle</c>).
    /// </summary>
    public void StartListeningForLobbyInvites(PlayFabEntity listeningEntity)
    {
        if (listeningEntity is null)
        {
            throw new ArgumentNullException(nameof(listeningEntity));
        }

        Nyi.Throw("PFMultiplayerStartListeningForLobbyInvitesWithEntityHandle", "the StartListeningForLobbyInvites overload that takes an EntityKey");
        Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerStartListeningForLobbyInvitesWithEntityHandle(
            Handle, listeningEntity.Handle));
    }

    /// <summary>
    /// Stops delivering lobby invites for an entity
    /// (<c>PFMultiplayerStopListeningForLobbyInvites</c>).
    /// </summary>
    public void StopListeningForLobbyInvites(EntityKey listeningEntity)
    {
        if (listeningEntity is null)
        {
            throw new ArgumentNullException(nameof(listeningEntity));
        }

        IntPtr handle = Handle;
        var arena = new PlayFabArena();
        try
        {
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerStopListeningForLobbyInvites(
                handle, MultiplayerInterop.Write(arena, listeningEntity)));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Stops delivering lobby invites for an entity the SDK already owns
    /// (<c>PFMultiplayerStopListeningForLobbyInvitesWithEntityHandle</c>).
    /// </summary>
    public void StopListeningForLobbyInvites(PlayFabEntity listeningEntity)
    {
        if (listeningEntity is null)
        {
            throw new ArgumentNullException(nameof(listeningEntity));
        }

        Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerStopListeningForLobbyInvitesWithEntityHandle(
            Handle, listeningEntity.Handle));
    }

    /// <summary>
    /// The state of an entity's invite listener
    /// (<c>PFMultiplayerGetLobbyInviteListenerStatus</c>).
    /// </summary>
    public LobbyInviteListenerStatus GetLobbyInviteListenerStatus(EntityKey listeningEntity)
    {
        if (listeningEntity is null)
        {
            throw new ArgumentNullException(nameof(listeningEntity));
        }

        IntPtr handle = Handle;
        var arena = new PlayFabArena();
        try
        {
            PFLobbyInviteListenerStatus status;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerGetLobbyInviteListenerStatus(
                handle, MultiplayerInterop.Write(arena, listeningEntity), &status));
            return (LobbyInviteListenerStatus)status;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Creates a lobby owned by a game server (<c>PFMultiplayerCreateAndClaimServerLobby</c>).
    /// Completes with <see cref="CreateAndClaimServerLobbyCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) CreateAndClaimServerLobby(
        EntityKey server, LobbyCreateConfiguration createConfiguration)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (createConfiguration is null)
        {
            throw new ArgumentNullException(nameof(createConfiguration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyCreateConfiguration* create = arena.Alloc<PFLobbyCreateConfiguration>(1);
            createConfiguration.WriteTo(create, arena);

            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateAndClaimServerLobby(
                handle,
                MultiplayerInterop.Write(arena, server),
                create,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Creates a server-owned lobby for an entity the SDK already owns
    /// (<c>PFMultiplayerCreateAndClaimServerLobbyWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) CreateAndClaimServerLobby(
        PlayFabEntity server, LobbyCreateConfiguration createConfiguration)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (createConfiguration is null)
        {
            throw new ArgumentNullException(nameof(createConfiguration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyCreateConfiguration* create = arena.Alloc<PFLobbyCreateConfiguration>(1);
            createConfiguration.WriteTo(create, arena);

            IntPtr lobby;
            Nyi.Throw("PFMultiplayerCreateAndClaimServerLobbyWithEntityHandle", "the CreateAndClaimServerLobby overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateAndClaimServerLobbyWithEntityHandle(
                handle, server.Handle, create, Context(operation), &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Reclaims a server-owned lobby (<c>PFMultiplayerClaimServerLobby</c>). Completes with
    /// <see cref="ClaimServerLobbyCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) ClaimServerLobby(EntityKey server, string lobbyId)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (lobbyId is null)
        {
            throw new ArgumentNullException(nameof(lobbyId));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerClaimServerLobby(
                handle,
                MultiplayerInterop.Write(arena, server),
                arena.String(lobbyId),
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Reclaims a server-owned lobby for an entity the SDK already owns
    /// (<c>PFMultiplayerClaimServerLobbyWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) ClaimServerLobby(
        PlayFabEntity server, string lobbyId)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (lobbyId is null)
        {
            throw new ArgumentNullException(nameof(lobbyId));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            IntPtr lobby;
            Nyi.Throw("PFMultiplayerClaimServerLobbyWithEntityHandle", "the ClaimServerLobby overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerClaimServerLobbyWithEntityHandle(
                handle, server.Handle, arena.String(lobbyId), Context(operation), &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins an existing lobby as its game server (<c>PFMultiplayerJoinLobbyAsServer</c>).
    /// Completes with <see cref="JoinLobbyAsServerCompleted"/>.
    /// </summary>
    public (OperationId Operation, Lobby Lobby) JoinLobbyAsServer(
        EntityKey server,
        string connectionString,
        LobbyServerJoinConfiguration? configuration = null)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            // PFMP does not mark this parameter _In_opt_, so it dereferences the pointer
            // unconditionally and a nullptr faults the process. An omitted configuration is
            // therefore projected as a zeroed one -- which is the "nothing to say" form --
            // rather than as a null.
            PFLobbyServerJoinConfiguration* join = arena.Alloc<PFLobbyServerJoinConfiguration>(1);
            *join = default;
            configuration?.WriteTo(join, arena);

            IntPtr lobby;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinLobbyAsServer(
                handle,
                MultiplayerInterop.Write(arena, server),
                arena.String(connectionString),
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins a lobby as a game server entity the SDK already owns
    /// (<c>PFMultiplayerJoinLobbyAsServerWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, Lobby Lobby) JoinLobbyAsServer(
        PlayFabEntity server,
        string connectionString,
        LobbyServerJoinConfiguration? configuration = null)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            // PFMP does not mark this parameter _In_opt_, so it dereferences the pointer
            // unconditionally and a nullptr faults the process. An omitted configuration is
            // therefore projected as a zeroed one -- which is the "nothing to say" form --
            // rather than as a null.
            PFLobbyServerJoinConfiguration* join = arena.Alloc<PFLobbyServerJoinConfiguration>(1);
            *join = default;
            configuration?.WriteTo(join, arena);

            IntPtr lobby;
            Nyi.Throw("PFMultiplayerJoinLobbyAsServerWithEntityHandle", "the JoinLobbyAsServer overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinLobbyAsServerWithEntityHandle(
                handle,
                server.Handle,
                arena.String(connectionString),
                join,
                Context(operation),
                &lobby));
            return (operation, Track(lobby));
        }
        finally
        {
            arena.Dispose();
        }
    }

    // ------------------------------------------------------------------------------------------
    // Matchmaking operations
    // ------------------------------------------------------------------------------------------

    /// <summary>
    /// Creates a matchmaking ticket (<c>PFMultiplayerCreateMatchmakingTicket</c>). Completes with
    /// <see cref="MatchmakingTicketCompleted"/>.
    /// </summary>
    /// <param name="localUsers">The local entities to match with.</param>
    /// <param name="localUserAttributes">
    /// One JSON attribute document per local user, in the same order.
    /// </param>
    /// <param name="configuration">The queue and timeout to match under.</param>
    public (OperationId Operation, MatchmakingTicket Ticket) CreateMatchmakingTicket(
        IReadOnlyList<EntityKey> localUsers,
        IReadOnlyList<string> localUserAttributes,
        MatchmakingTicketConfiguration configuration)
    {
        ValidateTicketMembers(localUsers, localUserAttributes, configuration);

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFMatchmakingTicketConfiguration* native = arena.Alloc<PFMatchmakingTicketConfiguration>(1);
            configuration.WriteTo(native, arena);

            IntPtr ticket;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateMatchmakingTicket(
                handle,
                (uint)localUsers.Count,
                MultiplayerInterop.WriteArray(arena, localUsers),
                arena.StringArray(AsNullable(localUserAttributes)),
                native,
                Context(operation),
                &ticket));
            return (operation, TrackTicket(ticket));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Creates a matchmaking ticket for entities the SDK already owns
    /// (<c>PFMultiplayerCreateMatchmakingTicketWithEntityHandles</c>).
    /// </summary>
    public (OperationId Operation, MatchmakingTicket Ticket) CreateMatchmakingTicket(
        IReadOnlyList<PlayFabEntity> localUsers,
        IReadOnlyList<string> localUserAttributes,
        MatchmakingTicketConfiguration configuration)
    {
        ValidateTicketMembers(localUsers, localUserAttributes, configuration);

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFMatchmakingTicketConfiguration* native = arena.Alloc<PFMatchmakingTicketConfiguration>(1);
            configuration.WriteTo(native, arena);

            IntPtr* entities = arena.Alloc<IntPtr>(localUsers.Count);
            for (int i = 0; i < localUsers.Count; i++)
            {
                entities[i] = localUsers[i].Handle;
            }

            IntPtr ticket;
            Nyi.Throw("PFMultiplayerCreateMatchmakingTicketWithEntityHandles", "the CreateMatchmakingTicket overload that takes EntityKey values");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateMatchmakingTicketWithEntityHandles(
                handle,
                (uint)localUsers.Count,
                entities,
                arena.StringArray(AsNullable(localUserAttributes)),
                native,
                Context(operation),
                &ticket));
            return (operation, TrackTicket(ticket));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins an existing matchmaking ticket by id
    /// (<c>PFMultiplayerJoinMatchmakingTicketFromId</c>).
    /// </summary>
    public (OperationId Operation, MatchmakingTicket Ticket) JoinMatchmakingTicketFromId(
        IReadOnlyList<EntityKey> localUsers,
        IReadOnlyList<string> localUserAttributes,
        string ticketId,
        string queueName)
    {
        ValidateTicketMembers(localUsers, localUserAttributes, ticketId, queueName);

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            IntPtr ticket;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerJoinMatchmakingTicketFromId(
                handle,
                (uint)localUsers.Count,
                MultiplayerInterop.WriteArray(arena, localUsers),
                arena.StringArray(AsNullable(localUserAttributes)),
                arena.String(ticketId),
                arena.String(queueName),
                Context(operation),
                &ticket));
            return (operation, TrackTicket(ticket));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Joins an existing matchmaking ticket for entities the SDK already owns
    /// (<c>PFMultiplayerJoinMatchmakingTicketFromIdWithEntityHandles</c>).
    /// </summary>
    public (OperationId Operation, MatchmakingTicket Ticket) JoinMatchmakingTicketFromId(
        IReadOnlyList<PlayFabEntity> localUsers,
        IReadOnlyList<string> localUserAttributes,
        string ticketId,
        string queueName)
    {
        ValidateTicketMembers(localUsers, localUserAttributes, ticketId, queueName);

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            IntPtr* entities = arena.Alloc<IntPtr>(localUsers.Count);
            for (int i = 0; i < localUsers.Count; i++)
            {
                entities[i] = localUsers[i].Handle;
            }

            IntPtr ticket;
            Nyi.Throw("PFMultiplayerJoinMatchmakingTicketFromIdWithEntityHandles", "the JoinMatchmakingTicketFromId overload that takes EntityKey values");
            Hr.ThrowIfFailed(
                NativePlayFab.PFMultiplayerJoinMatchmakingTicketFromIdWithEntityHandles(
                    handle,
                    (uint)localUsers.Count,
                    entities,
                    arena.StringArray(AsNullable(localUserAttributes)),
                    arena.String(ticketId),
                    arena.String(queueName),
                    Context(operation),
                    &ticket));
            return (operation, TrackTicket(ticket));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Creates a backfill ticket for a running match
    /// (<c>PFMultiplayerCreateServerBackfillTicket</c>).
    /// </summary>
    public (OperationId Operation, MatchmakingTicket Ticket) CreateServerBackfillTicket(
        EntityKey server, MatchmakingServerBackfillTicketConfiguration configuration)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFMatchmakingServerBackfillTicketConfiguration* native =
                arena.Alloc<PFMatchmakingServerBackfillTicketConfiguration>(1);
            configuration.WriteTo(native, arena);

            IntPtr ticket;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateServerBackfillTicket(
                handle,
                MultiplayerInterop.Write(arena, server),
                native,
                Context(operation),
                &ticket));
            return (operation, TrackTicket(ticket));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Creates a backfill ticket for a server entity the SDK already owns
    /// (<c>PFMultiplayerCreateServerBackfillTicketWithEntityHandle</c>).
    /// </summary>
    public (OperationId Operation, MatchmakingTicket Ticket) CreateServerBackfillTicket(
        PlayFabEntity server, MatchmakingServerBackfillTicketConfiguration configuration)
    {
        if (server is null)
        {
            throw new ArgumentNullException(nameof(server));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        IntPtr handle = Handle;
        OperationId operation = NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFMatchmakingServerBackfillTicketConfiguration* native =
                arena.Alloc<PFMatchmakingServerBackfillTicketConfiguration>(1);
            configuration.WriteTo(native, arena);

            IntPtr ticket;
            Nyi.Throw("PFMultiplayerCreateServerBackfillTicketWithEntityHandle", "the CreateServerBackfillTicket overload that takes an EntityKey");
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerCreateServerBackfillTicketWithEntityHandle(
                handle, server.Handle, native, Context(operation), &ticket));
            return (operation, TrackTicket(ticket));
        }
        finally
        {
            arena.Dispose();
        }
    }

    // ------------------------------------------------------------------------------------------
    // State-change pumps
    // ------------------------------------------------------------------------------------------

    /// <summary>
    /// Drains the lobby state-change queue. Enumerate it once per frame; leaving the
    /// <c>foreach</c> returns the batch to the library
    /// (<c>PFMultiplayerStartProcessingLobbyStateChanges</c> /
    /// <c>PFMultiplayerFinishProcessingLobbyStateChanges</c>).
    /// </summary>
    public LobbyStateChangeCollection ProcessLobbyStateChanges() => new(this);

    /// <summary>
    /// Drains the matchmaking state-change queue. Enumerate it once per frame
    /// (<c>PFMultiplayerStartProcessingMatchmakingStateChanges</c> /
    /// <c>PFMultiplayerFinishProcessingMatchmakingStateChanges</c>).
    /// </summary>
    public MatchmakingStateChangeCollection ProcessMatchmakingStateChanges() => new(this);

    /// <summary>
    /// Shuts the multiplayer library down (<c>PFMultiplayerUninitialize</c>), invalidating every
    /// lobby and ticket it produced.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <c>PFMultiplayerUninitialize</c> blocks until the library's internal PubSub work has
    /// drained, and that drain is a <c>while (pending &gt; 0) Sleep(...)</c> loop with no timeout
    /// and no failure path. If the work was never dispatchable -- which is what starting the
    /// library before the Gaming Runtime causes -- the loop never ends and the calling thread
    /// parks in <c>PubSubSubscriptionManager::Shutdown</c> indefinitely.
    /// </para>
    /// <para>
    /// <see cref="Initialize"/> guards against causing that, but a <c>Dispose</c> that can wedge a
    /// process is not something to leave to a precondition, so the native call is also bounded: it
    /// runs on a background thread and is abandoned after
    /// <see cref="RuntimeLifetime.TeardownTimeout"/>. Either way this object is disposed on
    /// return and the process can exit. Party is not involved: the two libraries may be
    /// initialized and shut down in either order, overlapped or not.
    /// </para>
    /// <para>
    /// Leave any joined lobbies and let the corresponding completion state changes arrive before
    /// disposing, as <c>PFMultiplayerUninitialize</c> documents.
    /// </para>
    /// </remarks>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        RuntimeLifetime.Unregister(this);

        foreach (Lobby lobby in _lobbies.Values)
        {
            lobby.Invalidate();
        }

        foreach (MatchmakingTicket ticket in _tickets.Values)
        {
            ticket.Invalidate();
        }

        _lobbies.Clear();
        _tickets.Clear();

        IntPtr handle = _handle;
        int hr = 0;
        bool completed = RuntimeLifetime.RunBounded(
            () => hr = NativePlayFab.PFMultiplayerUninitialize(handle),
            RuntimeLifetime.TeardownTimeout);

        if (completed)
        {
            Hr.ThrowIfFailed(hr);
        }
    }

    internal static void* Context(OperationId operation) => (void*)(IntPtr)operation.Value;

    internal static OperationId FromContext(void* context) => new((long)(IntPtr)context);

    internal OperationId NextOperationId() =>
        new(Interlocked.Increment(ref _nextOperation));

    internal Lobby Track(IntPtr handle)
    {
        if (!_lobbies.TryGetValue(handle, out Lobby? lobby))
        {
            lobby = new Lobby(this, handle);
            _lobbies[handle] = lobby;
        }

        return lobby;
    }

    internal Lobby? Find(IntPtr handle) =>
        handle == IntPtr.Zero ? null : Track(handle);

    internal MatchmakingTicket TrackTicket(IntPtr handle)
    {
        if (!_tickets.TryGetValue(handle, out MatchmakingTicket? ticket))
        {
            ticket = new MatchmakingTicket(this, handle);
            _tickets[handle] = ticket;
        }

        return ticket;
    }

    internal void Retire(Lobby? lobby)
    {
        if (lobby is null)
        {
            return;
        }

        lobby.Invalidate();
        _lobbies.Remove(lobby.RawHandle);
    }

    internal void DestroyTicket(MatchmakingTicket ticket)
    {
        Hr.ThrowIfFailed(
            NativePlayFab.PFMultiplayerDestroyMatchmakingTicket(Handle, ticket.RawHandle));
        _tickets.Remove(ticket.RawHandle);
        ticket.Invalidate();
    }

    private static IReadOnlyList<string?> AsNullable(IReadOnlyList<string> values)
    {
        var copy = new string?[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            copy[i] = values[i];
        }

        return copy;
    }

    private static void ValidateTicketMembers<T>(
        IReadOnlyList<T> localUsers, IReadOnlyList<string> attributes, object configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        ValidateTicketMembers(localUsers, attributes);
    }

    private static void ValidateTicketMembers<T>(
        IReadOnlyList<T> localUsers,
        IReadOnlyList<string> attributes,
        string ticketId,
        string queueName)
    {
        if (ticketId is null)
        {
            throw new ArgumentNullException(nameof(ticketId));
        }

        if (queueName is null)
        {
            throw new ArgumentNullException(nameof(queueName));
        }

        ValidateTicketMembers(localUsers, attributes);
    }

    private static void ValidateTicketMembers<T>(
        IReadOnlyList<T> localUsers, IReadOnlyList<string> attributes)
    {
        if (localUsers is null)
        {
            throw new ArgumentNullException(nameof(localUsers));
        }

        if (attributes is null)
        {
            throw new ArgumentNullException(nameof(attributes));
        }

        if (localUsers.Count == 0)
        {
            throw new ArgumentException("At least one local user is required.", nameof(localUsers));
        }

        if (localUsers.Count != attributes.Count)
        {
            throw new ArgumentException(
                "One attribute document is required per local user.", nameof(attributes));
        }
    }
}

/// <summary>
/// The lobby state changes produced by one pump. Enumerating starts the batch; leaving the loop
/// returns it to the multiplayer library.
/// </summary>
public readonly struct LobbyStateChangeCollection : IEnumerable<LobbyStateChange>
{
    private readonly PlayFabMultiplayer _owner;

    internal LobbyStateChangeCollection(PlayFabMultiplayer owner)
    {
        _owner = owner;
    }

    /// <summary>Starts a batch of lobby state changes.</summary>
    public Enumerator GetEnumerator() => new(_owner);

    /// <inheritdoc/>
    IEnumerator<LobbyStateChange> IEnumerable<LobbyStateChange>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Walks one batch of lobby state changes and returns it on <see cref="Dispose"/>.</summary>
    public unsafe struct Enumerator : IEnumerator<LobbyStateChange>
    {
        private readonly PlayFabMultiplayer _owner;
        private readonly PFLobbyStateChange** _changes;
        private readonly uint _count;
        private readonly List<Lobby> _retired;
        private uint _index;
        private LobbyStateChange? _current;
        private bool _finished;

        internal Enumerator(PlayFabMultiplayer owner)
        {
            _owner = owner;
            _retired = new List<Lobby>();
            _index = 0;
            _current = null;
            _finished = false;

            uint count;
            PFLobbyStateChange** changes;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerStartProcessingLobbyStateChanges(
                owner.Handle, &count, &changes));
            _count = count;
            _changes = changes;
        }

        /// <inheritdoc/>
        public LobbyStateChange Current => _current!;

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_index >= _count)
            {
                _current = null;
                return false;
            }

            _current = LobbyStateChangeReader.Read(_owner, _changes[_index], _retired);
            _index++;
            return true;
        }

        /// <summary>Returns the batch to the multiplayer library and retires torn-down lobbies.</summary>
        public void Dispose()
        {
            if (_finished)
            {
                return;
            }

            _finished = true;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerFinishProcessingLobbyStateChanges(
                _owner.Handle, _count, _changes));

            foreach (Lobby lobby in _retired)
            {
                _owner.Retire(lobby);
            }
        }

        /// <inheritdoc/>
        void IEnumerator.Reset() => throw new NotSupportedException();
    }
}

/// <summary>
/// The matchmaking state changes produced by one pump.
/// </summary>
public readonly struct MatchmakingStateChangeCollection : IEnumerable<MatchmakingStateChange>
{
    private readonly PlayFabMultiplayer _owner;

    internal MatchmakingStateChangeCollection(PlayFabMultiplayer owner)
    {
        _owner = owner;
    }

    /// <summary>Starts a batch of matchmaking state changes.</summary>
    public Enumerator GetEnumerator() => new(_owner);

    /// <inheritdoc/>
    IEnumerator<MatchmakingStateChange> IEnumerable<MatchmakingStateChange>.GetEnumerator() =>
        GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Walks one batch of matchmaking state changes.</summary>
    public unsafe struct Enumerator : IEnumerator<MatchmakingStateChange>
    {
        private readonly PlayFabMultiplayer _owner;
        private readonly PFMatchmakingStateChange** _changes;
        private readonly uint _count;
        private uint _index;
        private MatchmakingStateChange? _current;
        private bool _finished;

        internal Enumerator(PlayFabMultiplayer owner)
        {
            _owner = owner;
            _index = 0;
            _current = null;
            _finished = false;

            uint count;
            PFMatchmakingStateChange** changes;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerStartProcessingMatchmakingStateChanges(
                owner.Handle, &count, &changes));
            _count = count;
            _changes = changes;
        }

        /// <inheritdoc/>
        public MatchmakingStateChange Current => _current!;

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_index >= _count)
            {
                _current = null;
                return false;
            }

            _current = LobbyStateChangeReader.Read(_owner, _changes[_index]);
            _index++;
            return true;
        }

        /// <summary>Returns the batch to the multiplayer library.</summary>
        public void Dispose()
        {
            if (_finished)
            {
                return;
            }

            _finished = true;
            Hr.ThrowIfFailed(NativePlayFab.PFMultiplayerFinishProcessingMatchmakingStateChanges(
                _owner.Handle, _count, _changes));
        }

        /// <inheritdoc/>
        void IEnumerator.Reset() => throw new NotSupportedException();
    }
}
