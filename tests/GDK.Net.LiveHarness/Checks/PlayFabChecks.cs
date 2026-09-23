using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDK.Net.PlayFab;
using GDK.Net.PlayFab.Multiplayer;
using GDK.Net.PlayFab.Party;

namespace GDK.Net.LiveHarness;

/// <summary>
/// PlayFab: Core lifetime, a service config, both ways of getting an entity (title secret key and
/// the signed-in Xbox user), a real service call, and the two state-change pumps.
/// </summary>
/// <remarks>
/// <para>
/// The family runs against PlayFab title <c>10D176</c> by default (<c>--playfab-title-id</c>
/// overrides it). This is a *PlayFab* title id, unrelated to the Xbox title id in
/// <c>MicrosoftGame.config</c>: PlayFab has its own title registry, and the projection has to be
/// told which one to talk to.
/// </para>
/// <para>
/// Two entity paths are exercised because they fail differently. The secret-key path proves the
/// HTTP layer, the service config and the generated Services marshalling without needing an
/// account, so it still reports something useful on a machine with nobody signed in. The
/// <c>PFLocalUser</c> path is the one a shipping title uses, and is the only one that proves the
/// XUser → PlayFab bridge. The secret key is read from the environment
/// (<c>GDKNET_PLAYFAB_SECRET_KEY</c>, <c>PLAYFAB_DEVELOPER_SECRET_KEY</c> or
/// <c>PLAYFAB_SECRET_KEY</c>) and never appears in the report — a title secret is a server
/// credential, so it must not reach a log, and the checks report only its presence.
/// </para>
/// <para>
/// Lobbies, matchmaking and Party do not use <c>XAsyncBlock</c>: work completes on a pump the title
/// drives. The checks here therefore initialize each library and pump it, which is what proves the
/// state-change readers marshal a real native change correctly; they deliberately stop short of
/// creating a lobby or a network, because both need a second participant to mean anything.
/// </para>
/// </remarks>
internal static class PlayFabChecks
{
    private const string ConfigKey = "playfab.service-config";
    private const string TitleEntityKey = "playfab.title-entity";
    private const string LocalUserKey = "playfab.local-user";
    private const string UserEntityKey = "playfab.user-entity";
    private const string MultiplayerKey = "playfab.multiplayer";
    private const string PartyKey = "playfab.party";
    private const string PartyLocalUserKey = "playfab.party.local-user";

    public static IEnumerable<LiveCheck> All()
    {
        yield return LiveCheck.Sync("playfab.initialize", ctx =>
        {
            PlayFabRuntime.Initialize();
            return PlayFabRuntime.IsInitialized
                ? "PFInitialize and PFServicesInitialize succeeded"
                : throw new InvalidOperationException(
                    "PFServicesInitialize reported success but IsInitialized is false.");
        }, "runtime.initialize");

        yield return LiveCheck.Sync("playfab.http-settings", ctx =>
        {
            // Round-trips a setting rather than only reading one: the getter alone would pass even
            // if the struct were laid out wrongly, because every field would read as zero.
            uint original = PlayFabHttpSettings.TimeoutWindowInSeconds;
            PlayFabHttpSettings.TimeoutWindowInSeconds = original + 1;
            uint updated = PlayFabHttpSettings.TimeoutWindowInSeconds;
            PlayFabHttpSettings.TimeoutWindowInSeconds = original;

            return updated == original + 1
                ? $"PFGetHttpRetrySettings/PFSetHttpRetrySettings round-tripped the timeout window " +
                  $"({original}s, allowRetry={PlayFabHttpSettings.AllowRetry}, " +
                  $"compression={PlayFabHttpSettings.RequestResponseCompression})"
                : throw new InvalidOperationException(
                    $"Set the retry timeout window to {original + 1} and read back {updated}.");
        }, "playfab.initialize");

        yield return LiveCheck.Sync("playfab.service-config", ctx =>
        {
            var config = new PlayFabServiceConfig(ctx.Options.PlayFabApiEndpoint, ctx.Options.PlayFabTitleId);
            ctx.State[ConfigKey] = config;

            return string.Equals(config.TitleId, ctx.Options.PlayFabTitleId, StringComparison.Ordinal)
                ? $"PFServiceConfigCreateHandle for title {config.TitleId} at {config.ApiEndpoint}"
                : throw new InvalidOperationException(
                    $"PFServiceConfigGetTitleId returned '{config.TitleId}' for a config created " +
                    $"with '{ctx.Options.PlayFabTitleId}'.");
        }, "playfab.initialize");

        yield return LiveCheck.Async("playfab.title-entity", async ctx =>
        {
            string secretKey = ctx.Options.PlayFabSecretKey
                ?? throw new SkipCheckException(
                    "No developer secret key in the environment (GDKNET_PLAYFAB_SECRET_KEY, " +
                    "PLAYFAB_DEVELOPER_SECRET_KEY or PLAYFAB_SECRET_KEY); the title-entity path needs one.");

            PlayFabEntity entity = await Authentication
                .GetEntityWithSecretKeyAsync(Config(ctx), secretKey, new AuthenticationGetEntityRequest())
                .ConfigureAwait(false);

            ctx.State[TitleEntityKey] = entity;
            return $"PFAuthenticationGetEntityWithSecretKeyAsync returned a title entity for {entity.TitleId}";
        }, "playfab.service-config");

        yield return LiveCheck.Async("playfab.title-entity.token", async ctx =>
        {
            EntityToken token = await Entity(ctx, TitleEntityKey).GetEntityTokenAsync().ConfigureAwait(false);

            // The token itself is a credential, so only its shape is reported.
            return string.IsNullOrEmpty(token.Token)
                ? throw new InvalidOperationException("PFEntityGetEntityTokenAsync returned an empty token.")
                : $"PFEntityGetEntityTokenAsync returned a {token.Token!.Length}-character token " +
                  $"expiring {token.Expiration:u}";
        }, "playfab.title-entity");

        yield return LiveCheck.Async("playfab.server-time", async ctx =>
        {
            // The smallest real round trip through the generated Services layer: no title
            // configuration is required for it to answer, so a failure is the projection's.
            TitleDataManagementGetTimeResult result = await TitleDataManagement
                .ServerGetTimeAsync(Entity(ctx, TitleEntityKey))
                .ConfigureAwait(false);

            TimeSpan skew = result.Time - DateTimeOffset.UtcNow;
            return Math.Abs(skew.TotalMinutes) < 30
                ? $"PFTitleDataManagementServerGetTimeAsync returned {result.Time:u} " +
                  $"({skew.TotalSeconds:F1}s from this machine's clock)"
                : throw new InvalidOperationException(
                    $"PFTitleDataManagementServerGetTimeAsync returned {result.Time:u}, which is " +
                    $"{skew.TotalHours:F1}h from this machine's clock — the timestamp conversion is wrong.");
        }, "playfab.title-entity");

        yield return LiveCheck.Async("playfab.title-data", async ctx =>
        {
            TitleDataManagementGetTitleDataResult result = await TitleDataManagement
                .ServerGetTitleDataAsync(Entity(ctx, TitleEntityKey), new TitleDataManagementGetTitleDataRequest())
                .ConfigureAwait(false);

            int count = result.Data?.Count ?? 0;
            string sample = count == 0
                ? "no keys are configured for this title"
                : string.Join(", ", result.Data!.Keys.Take(5));

            return $"PFTitleDataManagementServerGetTitleDataAsync returned {count} key(s): {sample}";
        }, "playfab.title-entity");

        yield return LiveCheck.Sync("playfab.local-user", ctx =>
        {
            PlayFabLocalUser localUser = PlayFabLocalUser.CreateForXboxUser(Config(ctx), ctx.RequireUser);
            ctx.State[LocalUserKey] = localUser;
            return $"PFLocalUserCreateHandleWithXUser produced local user '{localUser.LocalId}'";
        }, "playfab.service-config", "users.add");

        yield return LiveCheck.Async("playfab.login", async ctx =>
        {
            PlayFabLoginResult login = await LocalUser(ctx).LoginAsync().ConfigureAwait(false);
            ctx.State[UserEntityKey] = login.Detach();

            return $"PFLocalUserLoginAsync signed the Xbox user in as PlayFab id " +
                   $"'{login.Result.PlayFabId ?? "(none)"}' (newlyCreated={login.Result.NewlyCreated})";
        }, "playfab.local-user");

        yield return LiveCheck.Sync("playfab.login.entity", ctx =>
        {
            // TryGetEntity borrows from the local user rather than logging in again; it is only
            // non-null after a successful login, which is what makes it worth asserting here.
            using PlayFabEntity? borrowed = LocalUser(ctx).TryGetEntity();
            return borrowed is null
                ? throw new InvalidOperationException(
                    "PFLocalUserGetEntityHandle returned nothing after a successful login.")
                : $"PFLocalUserGetEntityHandle returned an entity for title {borrowed.TitleId}";
        }, "playfab.login");

        yield return LiveCheck.Async("playfab.account-info", async ctx =>
        {
            try
            {
                AccountManagementGetAccountInfoResult result = await AccountManagement
                    .ClientGetAccountInfoAsync(
                        Entity(ctx, UserEntityKey), new AccountManagementGetAccountInfoRequest())
                    .ConfigureAwait(false);

                return $"PFAccountManagementClientGetAccountInfoAsync returned account " +
                       $"'{result.AccountInfo?.PlayFabId ?? "(none)"}' created " +
                       $"{result.AccountInfo?.Created:u}";
            }
            catch (GameRuntimeException ex) when (
                ex.HResultCode == ServiceGate.HttpBadRequest || ex.HResultCode == ServiceGate.HttpNotFound)
            {
                throw ServiceGate.NotConfigured(ex, "The title does not allow this client API");
            }
        }, "playfab.login");

        yield return LiveCheck.Sync("playfab.multiplayer.initialize", ctx =>
        {
            PlayFabMultiplayer multiplayer = PlayFabMultiplayer.Initialize(ctx.Options.PlayFabTitleId);
            ctx.State[MultiplayerKey] = multiplayer;
            return $"PFMultiplayerInitialize for title {ctx.Options.PlayFabTitleId}";
        }, "playfab.initialize");

        yield return LiveCheck.Sync("playfab.multiplayer.pump", ctx =>
        {
            // Nothing is expected to be pending, so this proves the pump brackets
            // PFMultiplayerStartProcessingLobbyStateChanges/FinishProcessing correctly rather than
            // that any particular change arrives.
            int changes = 0;
            for (int i = 0; i < 10; i++)
            {
                foreach (LobbyStateChange change in Multiplayer(ctx).ProcessLobbyStateChanges())
                {
                    _ = change;
                    changes++;
                }

                foreach (MatchmakingStateChange change in Multiplayer(ctx).ProcessMatchmakingStateChanges())
                {
                    _ = change;
                    changes++;
                }

                System.Threading.Thread.Sleep(50);
            }

            return $"PFMultiplayerStartProcessingLobbyStateChanges and " +
                   $"PFMultiplayerStartProcessingMatchmakingStateChanges pumped cleanly 10 times " +
                   $"({changes} change(s))";
        }, "playfab.multiplayer.initialize");

        yield return LiveCheck.Sync("playfab.multiplayer.lobby", ctx =>
        {
            // A whole lobby round trip in one process: create, observe the completion, then leave
            // and observe that completion too. It is the only check that exercises the lobby call
            // path end to end, and it is the single-process control for the multiplayer harness --
            // if this passes while the harness stalls, the difference is genuinely multi-process.
            PlayFabMultiplayer multiplayer = Multiplayer(ctx);
            PlayFabEntity entity = Entity(ctx, UserEntityKey);
            EntityKey key = entity.Key;

            multiplayer.SetEntityToken(
                key,
                entity.GetEntityTokenAsync().GetAwaiter().GetResult().Token
                    ?? throw new InvalidOperationException("PlayFab returned an entity with no token."));

            var configuration = new LobbyCreateConfiguration
            {
                MaxMemberCount = 8,
                OwnerMigrationPolicy = LobbyOwnerMigrationPolicy.Automatic,
                AccessPolicy = LobbyAccessPolicy.Private,
            };

            (OperationId create, Lobby lobby) = multiplayer.CreateAndJoinLobby(key, configuration);
            CreateAndJoinLobbyCompleted created = PumpFor<CreateAndJoinLobbyCompleted>(
                multiplayer,
                done => done.Operation == create,
                "PFLobbyCreateAndJoinLobbyCompletedStateChange");

            if (created.Failed)
            {
                throw new InvalidOperationException(
                    $"CreateAndJoinLobby failed with 0x{created.ResultCode:X8}.");
            }

            string id = lobby.Id ?? "(unknown)";
            int members = lobby.Members.Count;

            OperationId leave = lobby.Leave(key);
            _ = PumpFor<LeaveLobbyCompleted>(
                multiplayer,
                done => done.Operation == leave,
                "PFLobbyLeaveLobbyCompletedStateChange");

            return $"PFMultiplayerCreateAndJoinLobby created lobby {id} with {members} member(s) " +
                   $"and PFLobbyLeave removed the entity again";
        }, "playfab.multiplayer.initialize", "playfab.login");

        yield return LiveCheck.Sync("playfab.party.initialize", ctx =>
        {
            PartyManager party = PartyManager.Initialize(ctx.Options.PlayFabTitleId);
            ctx.State[PartyKey] = party;
            return $"PartyInitialize for title {ctx.Options.PlayFabTitleId} " +
                   $"(audio work mode {PartyManager.GetWorkMode(PartyThreadId.Audio)})";
        }, "playfab.initialize");

        yield return LiveCheck.Sync("playfab.party.local-user", ctx =>
        {
            // The bridge from PlayFab authentication into Party: the entity from PFLocalUserLogin
            // is what Party authenticates a chat/network participant with, so this is the call that
            // proves the two libraries' handles line up.
            PartyLocalUser localUser = Party(ctx).CreateLocalUser(Entity(ctx, UserEntityKey));
            ctx.State[PartyLocalUserKey] = localUser;

            PartyOperationId destroy = Party(ctx).DestroyLocalUser(localUser);
            for (int i = 0; i < 40; i++)
            {
                foreach (PartyStateChange change in Party(ctx).ProcessStateChanges())
                {
                    if (change is PartyDestroyLocalUserCompleted completed && completed.Operation == destroy)
                    {
                        ctx.State.Remove(PartyLocalUserKey);
                        return $"PartyCreateLocalUser and PartyDestroyLocalUser round-tripped; " +
                               $"the completion state change echoed operation {destroy} with result {completed.Result}";
                    }
                }

                System.Threading.Thread.Sleep(50);
            }

            ctx.State.Remove(PartyLocalUserKey);
            throw new InvalidOperationException(
                $"PartyDestroyLocalUser reported operation {destroy} but no " +
                "PartyDestroyLocalUserCompletedStateChange arrived within two seconds of pumping.");
        }, "playfab.party.initialize", "playfab.login");

        yield return LiveCheck.Sync("playfab.party.regions", ctx =>
        {
            // Party measures region latency asynchronously and reports it as a RegionsChanged state
            // change, so the list is empty until the pump has run for a moment.
            IReadOnlyList<PartyRegion> regions = [];
            for (int i = 0; i < 150 && regions.Count == 0; i++)
            {
                foreach (PartyStateChange change in Party(ctx).ProcessStateChanges())
                {
                    _ = change;
                }

                regions = Party(ctx).GetRegions();
                if (regions.Count == 0)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }

            if (regions.Count == 0)
            {
                throw new SkipCheckException(
                    "PartyGetRegions reported no regions after fifteen seconds of pumping; the " +
                    "device has no route to the Party quality-of-service endpoints.");
            }

            PartyRegion best = regions[0];
            return $"PartyGetRegions returned {regions.Count} region(s), closest '{best.RegionName}' " +
                   $"at {best.RoundTripLatency.TotalMilliseconds:F0}ms";
        }, "playfab.party.initialize");
    }

    /// <summary>
    /// Releases every PlayFab handle and shuts the libraries down. Ordering is load-bearing:
    /// <c>PFUninitializeAsync</c> fails while an entity, local user or service config is still
    /// outstanding.
    /// </summary>
    public static IEnumerable<LiveCheck> Teardown()
    {
        yield return LiveCheck.Sync("playfab.party.dispose", ctx =>
        {
            Party(ctx).Dispose();
            ctx.State.Remove(PartyKey);
            return "PartyCleanup";
        }, "playfab.party.initialize");

        yield return LiveCheck.Sync("playfab.multiplayer.dispose", ctx =>
        {
            Multiplayer(ctx).Dispose();
            ctx.State.Remove(MultiplayerKey);
            return "PFMultiplayerUninitialize";
        }, "playfab.multiplayer.initialize");

        yield return LiveCheck.Sync("playfab.handles.dispose", ctx =>
        {
            int disposed = 0;
            foreach (string key in new[] { UserEntityKey, TitleEntityKey })
            {
                if (ctx.TryGet(key, out PlayFabEntity entity))
                {
                    entity.Dispose();
                    ctx.State.Remove(key);
                    disposed++;
                }
            }

            if (ctx.TryGet(LocalUserKey, out PlayFabLocalUser localUser))
            {
                localUser.Dispose();
                ctx.State.Remove(LocalUserKey);
                disposed++;
            }

            if (ctx.TryGet(ConfigKey, out PlayFabServiceConfig config))
            {
                config.Dispose();
                ctx.State.Remove(ConfigKey);
                disposed++;
            }

            return $"Closed {disposed} PlayFab handle(s)";
        }, "playfab.initialize");

        yield return LiveCheck.Async("playfab.uninitialize", async ctx =>
        {
            await PlayFabRuntime.UninitializeAsync().ConfigureAwait(false);
            return PlayFabRuntime.IsInitialized
                ? throw new InvalidOperationException(
                    "PFUninitializeAsync completed but IsInitialized is still true.")
                : "PFServicesUninitializeAsync and PFUninitializeAsync drained cleanly";
        }, "playfab.initialize", "playfab.handles.dispose");
    }

    private static PlayFabServiceConfig Config(CheckContext ctx) =>
        ctx.Get<PlayFabServiceConfig>(ConfigKey);

    private static PlayFabLocalUser LocalUser(CheckContext ctx) =>
        ctx.Get<PlayFabLocalUser>(LocalUserKey);

    private static PlayFabEntity Entity(CheckContext ctx, string key) =>
        ctx.Get<PlayFabEntity>(key);

    private static PlayFabMultiplayer Multiplayer(CheckContext ctx) =>
        ctx.Get<PlayFabMultiplayer>(MultiplayerKey);

    private static PartyManager Party(CheckContext ctx) =>
        ctx.Get<PartyManager>(PartyKey);

    /// <summary>
    /// Pumps the lobby queue until the awaited completion arrives, or gives up with a message that
    /// says what never came back.
    /// </summary>
    /// <remarks>
    /// Lobby operations are not <see cref="System.Threading.Tasks.Task"/>-based: the call returns an
    /// operation id synchronously and the answer is delivered later on the state-change queue, so a
    /// check that wants the result has to run the title's frame loop itself.
    /// </remarks>
    private static T PumpFor<T>(PlayFabMultiplayer multiplayer, Func<T, bool> matches, string what)
        where T : LobbyStateChange
    {
        const int BudgetMilliseconds = 45_000;
        const int PollMilliseconds = 25;

        for (int waited = 0; waited < BudgetMilliseconds; waited += PollMilliseconds)
        {
            foreach (LobbyStateChange change in multiplayer.ProcessLobbyStateChanges())
            {
                if (change is T candidate && matches(candidate))
                {
                    return candidate;
                }
            }

            System.Threading.Thread.Sleep(PollMilliseconds);
        }

        throw new InvalidOperationException(
            $"No {what} arrived within {BudgetMilliseconds / 1000} seconds of pumping.");
    }
}
