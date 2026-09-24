using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDK.Net.Users;
using GDK.Net.XboxLive;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Live checks for XSAPI data services: leaderboards, user and title-managed statistics, title
/// storage, string verification, in-game events, multiplayer activity and the achievements manager.
/// </summary>
/// <remarks>
/// <para>
/// Every family exercises both read and write paths to prove the native binding end-to-end. Checks
/// that leave server-side state (title-storage blobs, multiplayer activity and the
/// achievements-manager local user) are cleaned up by the checks in <see cref="Teardown"/>.
/// </para>
/// <para>
/// Several checks reference the statistic name <c>HarnessScore</c> and the event name
/// <c>HarnessCheck</c>. These must be present in the title's Partner Center service configuration.
/// A missing entry causes the associated service call to fail or return empty results; that is a
/// failure, not a skip, because the API binding is what is under test.
/// </para>
/// </remarks>
internal static class XblDataChecks
{
    /// <summary>
    /// Stat name used for title-managed-statistics writes and the stat-backed leaderboard query.
    /// Must be configured as a title-managed stat in Partner Center.
    /// </summary>
    private const string TestStatName = "HarnessScore";

    /// <summary>
    /// Blob path written to Universal title storage and cleaned up by the teardown delete check.
    /// </summary>
    private const string TestBlobPath = "harness/livecheck.json";

    /// <summary>
    /// Event name passed to <see cref="EventsService.WriteInGameEvent"/>. Must appear in the
    /// title's event manifest; the service silently drops unknown events so this check succeeds
    /// regardless of title configuration.
    /// </summary>
    private const string TestEventName = "HarnessCheck";

    /// <summary>
    /// All data-service checks, ordered so writes precede the reads that verify them. Cleanup for
    /// any persistent server-side state is in <see cref="Teardown"/>.
    /// </summary>
    public static IEnumerable<LiveCheck> All()
    {
        foreach (LiveCheck check in TitleManagedStatisticsFamily())
            yield return check;
        foreach (LiveCheck check in LeaderboardFamily())
            yield return check;
        foreach (LiveCheck check in UserStatisticsFamily())
            yield return check;
        foreach (LiveCheck check in TitleStorageFamily())
            yield return check;
        foreach (LiveCheck check in StringVerificationFamily())
            yield return check;
        foreach (LiveCheck check in EventsFamily())
            yield return check;
        foreach (LiveCheck check in MultiplayerActivityFamily())
            yield return check;
        foreach (LiveCheck check in AchievementsManagerFamily())
            yield return check;
    }

    /// <summary>
    /// Cleanup checks that remove server-side state written by <see cref="All"/>: the title-managed
    /// statistic, the uploaded title-storage blob, the multiplayer activity advertisement and the
    /// achievements-manager local user. Must run before <c>xbl.context.dispose</c> and
    /// <c>xbl.cleanup</c>.
    /// </summary>
    /// <remarks>
    /// The statistic delete belongs here rather than beside its write. Title-managed statistics have
    /// no read API: the only way to observe one is a stat-backed leaderboard query, so deleting it
    /// at the end of <see cref="All"/> would leave the next run querying a statistic the previous run
    /// had already removed, and the leaderboard and user-statistic reads would be vacuous forever.
    /// </remarks>
    public static IEnumerable<LiveCheck> Teardown()
    {
        yield return LiveCheck.Async("xbl.titlestats.delete", async ctx =>
        {
            try
            {
                await ctx.RequireXboxLiveContext.TitleManagedStatistics
                    .DeleteAsync(new[] { TestStatName }).ConfigureAwait(false);
            }
            catch (GameRuntimeException ex) when (ex.HResultCode == ServiceGate.HttpBadRequest)
            {
                throw ServiceGate.NotConfigured(
                    ex,
                    $"'{TestStatName}' is not a configured title-managed stat for this title, so the " +
                    "service rejects deleting it. Writes are accepted and discarded, which is why " +
                    "the write and update checks pass. Configure the stat in Partner Center to " +
                    "exercise this.");
            }

            return $"XblTitleManagedStatsDeleteStatsAsync: deleted {TestStatName}";
        }, "xbl.titlestats.update");

        yield return LiveCheck.Async("xbl.titlestorage.delete", async ctx =>
        {
            TitleStorageBlobMetadata meta = ctx.Get<TitleStorageBlobMetadata>("xbl.titlestorage.metadata");
            await ctx.RequireXboxLiveContext.TitleStorage.DeleteBlobAsync(meta).ConfigureAwait(false);
            return $"XblTitleStorageDeleteBlobAsync deleted '{meta.BlobPath}'";
        }, "xbl.titlestorage.upload");

        yield return LiveCheck.Async("xbl.mpactivity.delete", async ctx =>
        {
            await ctx.RequireXboxLiveContext.MultiplayerActivity.DeleteActivityAsync().ConfigureAwait(false);
            return "XblMultiplayerActivityDeleteActivityAsync removed the harness activity";
        }, "xbl.mpactivity.set");

        yield return LiveCheck.Sync("xbl.achmanager.remove", ctx =>
        {
            ulong xuid = ctx.RequireUser.Id;
            ctx.RequireRuntime.XboxLive.AchievementsManager.RemoveLocalUser(ctx.RequireUser);
            return $"XblAchievementsManagerRemoveLocalUser for 0x{xuid:X16}";
        }, "xbl.achmanager.add");
    }

    private static IEnumerable<LiveCheck> LeaderboardFamily()
    {
        yield return LiveCheck.Async("xbl.leaderboard.global", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            var query = new LeaderboardQuery(
                xboxUserId: 0,
                serviceConfigurationId: scid,
                statisticName: TestStatName,
                maxItems: 10,
                queryType: LeaderboardQueryType.TitleManagedStatBackedGlobal);

            using LeaderboardPage page = await GetOrSkipAsync(context, query).ConfigureAwait(false);

            return page.Rows.Count == 0
                ? $"XblLeaderboardGetLeaderboardAsync: {TestStatName} leaderboard returned no rows " +
                  $"on this page (totalRows={page.TotalRowCount}; a stat written moments earlier may " +
                  "not have propagated to the leaderboard yet)"
                : $"XblLeaderboardGetLeaderboardAsync: {TestStatName} totalRows={page.TotalRowCount} " +
                  $"pageRows={page.Rows.Count} hasNext={page.HasNext} " +
                  $"top={page.Rows[0]}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.leaderboard.skip-to-user", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            var query = new LeaderboardQuery(
                xboxUserId: context.XboxUserId,
                serviceConfigurationId: scid,
                statisticName: TestStatName,
                maxItems: 5,
                skipToXboxUserId: context.XboxUserId,
                queryType: LeaderboardQueryType.TitleManagedStatBackedGlobal);

            using LeaderboardPage page = await GetOrSkipAsync(context, query).ConfigureAwait(false);

            LeaderboardRow? userRow = page.Rows.FirstOrDefault(r => r.XboxUserId == context.XboxUserId);
            string userEntry = userRow is null ? "not on this page" : $"rank {userRow.Rank}";

            return $"XblLeaderboardGetLeaderboardAsync (skip-to-user 0x{context.XboxUserId:X16}): " +
                   $"totalRows={page.TotalRowCount} pageRows={page.Rows.Count} user={userEntry}";
        }, "xbl.leaderboard.global");
    }

    /// <summary>
    /// Runs a leaderboard query, converting the "no such leaderboard" 404 into a skip.
    /// </summary>
    /// <remarks>
    /// A title-managed-stat-backed leaderboard only exists once the stat is configured in Partner
    /// Center. Until then the service has nothing to serve and returns 404: the same root cause
    /// that makes <c>xbl.titlestats.delete</c> and the user-statistics lookups come back empty.
    /// </remarks>
    private static async Task<LeaderboardPage> GetOrSkipAsync(XboxLiveContext context, LeaderboardQuery query)
    {
        try
        {
            return await context.Leaderboards.GetAsync(query).ConfigureAwait(false);
        }
        catch (GameRuntimeException ex) when (ex.HResultCode == ServiceGate.HttpNotFound)
        {
            throw ServiceGate.NotConfigured(
                ex,
                $"no leaderboard exists for '{TestStatName}'; it has to be configured as a " +
                "title-managed stat in Partner Center before the service will serve one.");
        }
    }

    private static IEnumerable<LiveCheck> UserStatisticsFamily()
    {
        yield return LiveCheck.Async("xbl.userstats.single", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            UserStatisticsResult result = await context.UserStatistics
                .GetSingleUserStatisticAsync(context.XboxUserId, scid, TestStatName)
                .ConfigureAwait(false);

            Statistic? stat = result.ServiceConfigurations
                .SelectMany(sc => sc.Statistics)
                .FirstOrDefault(s => string.Equals(s.Name, TestStatName, StringComparison.OrdinalIgnoreCase));

            return stat is not null
                ? $"XblUserStatisticsGetSingleUserStatisticAsync: {stat.Name}={stat.Value} type={stat.Type}"
                : $"XblUserStatisticsGetSingleUserStatisticAsync: {TestStatName} not present " +
                  "(stat not yet written or not configured as an event-backed stat)";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.userstats.multi-stat", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            UserStatisticsResult result = await context.UserStatistics
                .GetSingleUserStatisticsAsync(
                    context.XboxUserId,
                    scid,
                    new[] { TestStatName, "HarnessGames" })
                .ConfigureAwait(false);

            int total = result.ServiceConfigurations.Sum(sc => sc.Statistics.Count);
            return $"XblUserStatisticsGetSingleUserStatisticsAsync: queried 2 stats, " +
                   $"service returned {total} result(s)";
        }, "xbl.userstats.single");

        yield return LiveCheck.Async("xbl.userstats.multi-user", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            IReadOnlyList<UserStatisticsResult> results = await context.UserStatistics
                .GetMultipleUserStatisticsAsync(
                    new[] { context.XboxUserId },
                    scid,
                    new[] { TestStatName })
                .ConfigureAwait(false);

            int total = results.Sum(r => r.ServiceConfigurations.Sum(sc => sc.Statistics.Count));
            return $"XblUserStatisticsGetMultipleUserStatisticsAsync: " +
                   $"{results.Count} result(s) for 1 user, {total} statistic(s) returned";
        }, "xbl.context");
    }

    private static IEnumerable<LiveCheck> TitleManagedStatisticsFamily()
    {
        yield return LiveCheck.Async("xbl.titlestats.write", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            var stats = new[] { new TitleManagedStatistic(TestStatName, 1000.0) };
            await context.TitleManagedStatistics.WriteAsync(context.XboxUserId, stats)
                .ConfigureAwait(false);

            return $"XblTitleManagedStatsWriteAsync: {TestStatName}=1000 (full replace)";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.titlestats.update", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            var stats = new[] { new TitleManagedStatistic(TestStatName, 2000.0) };
            await context.TitleManagedStatistics.UpdateAsync(stats).ConfigureAwait(false);

            return $"XblTitleManagedStatsUpdateStatsAsync: {TestStatName}=2000 (partial update)";
        }, "xbl.titlestats.write");
    }

    private static IEnumerable<LiveCheck> TitleStorageFamily()
    {
        yield return LiveCheck.Async("xbl.titlestorage.quota", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            TitleStorageQuota quota = await context.TitleStorage
                .GetQuotaAsync(scid, TitleStorageType.Universal)
                .ConfigureAwait(false);

            return $"XblTitleStorageGetQuotaAsync: used={quota.UsedBytes} quota={quota.QuotaBytes}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.titlestorage.upload", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            string json = $"{{\"harness\":\"live\",\"xuid\":\"{context.XboxUserId}\"}}";
            byte[] data = Encoding.UTF8.GetBytes(json);

            var meta = new TitleStorageBlobMetadata(
                blobPath: TestBlobPath,
                blobType: TitleStorageBlobType.Json,
                storageType: TitleStorageType.Universal,
                serviceConfigurationId: scid,
                xboxUserId: context.XboxUserId);

            TitleStorageBlobMetadata uploaded = await context.TitleStorage
                .UploadBlobAsync(meta, data)
                .ConfigureAwait(false);

            ctx.State["xbl.titlestorage.metadata"] = uploaded;

            return $"XblTitleStorageUploadBlobAsync wrote {data.Length} bytes to '{TestBlobPath}'; " +
                   $"ETag={uploaded.ETag}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.titlestorage.download", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            TitleStorageBlobMetadata meta = ctx.Get<TitleStorageBlobMetadata>("xbl.titlestorage.metadata");

            TitleStorageBlobDownloadResult result = await context.TitleStorage
                .DownloadBlobAsync(meta)
                .ConfigureAwait(false);

            string text = Encoding.UTF8.GetString(result.Data);
            bool containsXuid = text.Contains(context.XboxUserId.ToString(), StringComparison.Ordinal);

            return $"XblTitleStorageDownloadBlobAsync read {result.Data.Length} bytes from '{meta.BlobPath}'; " +
                   $"XUID present in payload: {containsXuid}";
        }, "xbl.titlestorage.upload");

        yield return LiveCheck.Async("xbl.titlestorage.list", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            string scid = ctx.RequireRuntime.XboxLive.Scid;

            using TitleStorageBlobMetadataPage page = await context.TitleStorage
                .GetBlobMetadataAsync(
                    scid,
                    TitleStorageType.Universal,
                    blobPath: "harness",
                    xboxUserId: context.XboxUserId)
                .ConfigureAwait(false);

            bool containsBlob = page.Items.Any(
                i => string.Equals(i.BlobPath, TestBlobPath, StringComparison.Ordinal));

            return $"XblTitleStorageGetBlobMetadataAsync: {page.Items.Count} item(s) under 'harness'; " +
                   $"test blob present: {containsBlob}";
        }, "xbl.titlestorage.upload");
    }

    private static IEnumerable<LiveCheck> StringVerificationFamily()
    {
        yield return LiveCheck.Async("xbl.stringverify.clean", async ctx =>
        {
            StringVerificationResult result = await ctx.RequireXboxLiveContext.StringVerification
                .VerifyStringAsync("Hello world")
                .ConfigureAwait(false);

            return $"XblStringVerifyStringAsync('Hello world'): " +
                   $"wasVerified={result.WasVerified} code={result.ResultCode} " +
                   $"acceptable={result.IsAcceptable}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.stringverify.flagged", async ctx =>
        {
            // "ass" is a common profanity-filter test word. The service may flag it as Offensive
            // depending on locale and policy; this check pins the non-trivial result path.
            StringVerificationResult result = await ctx.RequireXboxLiveContext.StringVerification
                .VerifyStringAsync("ass")
                .ConfigureAwait(false);

            return $"XblStringVerifyStringAsync('ass'): " +
                   $"wasVerified={result.WasVerified} code={result.ResultCode} " +
                   $"acceptable={result.IsAcceptable} " +
                   $"offender={result.FirstOffendingSubstring ?? "none"}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.stringverify.batch", async ctx =>
        {
            IReadOnlyList<StringVerificationResult> results = await ctx.RequireXboxLiveContext.StringVerification
                .VerifyStringsAsync(new[] { "Hello world", "ass" })
                .ConfigureAwait(false);

            return $"XblStringVerifyStringsAsync: {results.Count} result(s); " +
                   string.Join(", ", results.Select(r => $"'{r.VerifiedString}'→{r.ResultCode}"));
        }, "xbl.stringverify.clean", "xbl.stringverify.flagged");
    }

    private static IEnumerable<LiveCheck> EventsFamily()
    {
        yield return LiveCheck.Sync("xbl.events.write", ctx =>
        {
            ctx.RequireXboxLiveContext.Events.WriteInGameEvent(
                TestEventName,
                dimensionsJson: "{\"Level\":\"1\",\"Mode\":\"harness\"}",
                measurementsJson: "{\"Score\":9999,\"RoundTime\":1.0}");

            return $"XblEventsWriteInGameEvent '{TestEventName}' with dimensions and measurements";
        }, "xbl.context");
    }

    private static IEnumerable<LiveCheck> MultiplayerActivityFamily()
    {
        yield return LiveCheck.Async("xbl.mpactivity.set", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            var activity = new MultiplayerActivityInfo(
                xboxUserId: context.XboxUserId,
                connectionString: "harness://game/1",
                joinRestriction: MultiplayerActivityJoinRestriction.Public,
                maxPlayers: 8,
                currentPlayers: 1,
                groupId: "harness-group");

            await context.MultiplayerActivity.SetActivityAsync(activity).ConfigureAwait(false);

            return $"XblMultiplayerActivitySetActivityAsync for 0x{context.XboxUserId:X16}: " +
                   "connection='harness://game/1' group='harness-group' maxPlayers=8";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.mpactivity.get", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            IReadOnlyList<MultiplayerActivityInfo> activities = await context.MultiplayerActivity
                .GetActivitiesAsync(new[] { context.XboxUserId })
                .ConfigureAwait(false);

            MultiplayerActivityInfo? own = activities.FirstOrDefault(a => a.XboxUserId == context.XboxUserId);

            if (own is null)
            {
                return $"XblMultiplayerActivityGetActivityAsync: no activity for 0x{context.XboxUserId:X16} " +
                       $"({activities.Count} total returned)";
            }

            bool roundTrip =
                string.Equals(own.ConnectionString, "harness://game/1", StringComparison.Ordinal) &&
                string.Equals(own.GroupId, "harness-group", StringComparison.Ordinal);

            return $"XblMultiplayerActivityGetActivityAsync: connection={own.ConnectionString} " +
                   $"group={own.GroupId} max={own.MaxPlayers} current={own.CurrentPlayers}; " +
                   $"roundTrip={roundTrip}";
        }, "xbl.mpactivity.set");

        yield return LiveCheck.Async("xbl.mpactivity.recent-players", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            var updates = new[]
            {
                new MultiplayerActivityRecentPlayerUpdate(
                    context.XboxUserId,
                    MultiplayerActivityEncounterType.Teammate),
            };

            context.MultiplayerActivity.UpdateRecentPlayers(updates);
            await context.MultiplayerActivity.FlushRecentPlayersAsync().ConfigureAwait(false);

            return $"XblMultiplayerActivityUpdateRecentPlayers + XblMultiplayerActivityFlushRecentPlayersAsync: " +
                   $"flushed 1 encounter for 0x{context.XboxUserId:X16}";
        }, "xbl.context");
    }

    private static IEnumerable<LiveCheck> AchievementsManagerFamily()
    {
        yield return LiveCheck.Sync("xbl.achmanager.add", ctx =>
        {
            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            User user = ctx.RequireUser;
            ulong xuid = user.Id;

            manager.AddLocalUser(user);

            // DoWork has not been called yet, so XSAPI has not synced the cache. XSAPI returns
            // E_FAIL here, which the projection must translate to false rather than throwing.
            bool beforePump = manager.IsUserInitialized(xuid);
            ctx.State["xbl.achmanager.preInit"] = beforePump;

            return $"XblAchievementsManagerAddLocalUser for 0x{xuid:X16}; " +
                   $"XblAchievementsManagerIsUserInitialized before any DoWork = {beforePump} " +
                   "(expected false; E_FAIL treated as not-initialized)";
        }, "xbl.initialize", "users.add");

        yield return LiveCheck.Async("xbl.achmanager.pump-initialized", async ctx =>
        {
            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            ulong xuid = ctx.RequireUser.Id;

            bool initialized = false;
            int totalEvents = 0;
            int pumps = 0;
            DateTime deadline = DateTime.UtcNow.AddSeconds(15);

            while (!initialized && DateTime.UtcNow < deadline)
            {
                IReadOnlyList<AchievementsManagerEvent> events = manager.DoWork();
                pumps++;
                totalEvents += events.Count;

                foreach (AchievementsManagerEvent evt in events)
                {
                    if (evt is AchievementsManagerLocalUserInitialStateSyncedEvent syncedEvt &&
                        syncedEvt.XboxUserId == xuid)
                    {
                        initialized = true;
                        break;
                    }
                }

                if (!initialized)
                {
                    await Task.Delay(250).ConfigureAwait(false);
                }
            }

            if (!initialized)
            {
                throw new InvalidOperationException(
                    $"XblAchievementsManagerDoWork did not deliver " +
                    $"AchievementsManagerLocalUserInitialStateSyncedEvent for 0x{xuid:X16} " +
                    $"within 15 s ({pumps} pump(s), {totalEvents} event(s) seen).");
            }

            return $"XblAchievementsManagerDoWork: LocalUserInitialStateSynced for 0x{xuid:X16} " +
                   $"after {pumps} pump(s), {totalEvents} total event(s)";
        }, "xbl.achmanager.add");

        yield return LiveCheck.Sync("xbl.achmanager.is-initialized", ctx =>
        {
            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            ulong xuid = ctx.RequireUser.Id;

            bool initialized = manager.IsUserInitialized(xuid);

            string preNote = ctx.TryGet<bool>("xbl.achmanager.preInit", out bool pre)
                ? $"; before-pump was {pre} ({(pre ? "fast init" : "E_FAIL path confirmed")})"
                : string.Empty;

            return $"XblAchievementsManagerIsUserInitialized after sync = {initialized}{preNote}";
        }, "xbl.achmanager.pump-initialized");

        yield return LiveCheck.Sync("xbl.achmanager.get-all", ctx =>
        {
            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            ulong xuid = ctx.RequireUser.Id;

            using AchievementsManagerResult result = manager.GetAchievements(xuid);

            if (result.Achievements.Count > 0)
            {
                ctx.State["xbl.achmanager.firstId"] = result.Achievements[0].Id;
            }

            return result.Achievements.Count == 0
                ? "XblAchievementsManagerGetAchievements: no achievements (title has none in this sandbox)"
                : $"XblAchievementsManagerGetAchievements: {result.Achievements.Count} achievement(s); " +
                  $"first='{result.Achievements[0].Id}' state={result.Achievements[0].ProgressState}";
        }, "xbl.achmanager.pump-initialized");

        yield return LiveCheck.Sync("xbl.achmanager.get-by-state", ctx =>
        {
            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            ulong xuid = ctx.RequireUser.Id;

            using AchievementsManagerResult achieved =
                manager.GetAchievementsByState(xuid, AchievementProgressState.Achieved);
            using AchievementsManagerResult inProgress =
                manager.GetAchievementsByState(xuid, AchievementProgressState.InProgress);

            return $"XblAchievementsManagerGetAchievementsByState: " +
                   $"Achieved={achieved.Achievements.Count} InProgress={inProgress.Achievements.Count}";
        }, "xbl.achmanager.get-all");

        yield return LiveCheck.Sync("xbl.achmanager.get-single", ctx =>
        {
            if (!ctx.TryGet<string>("xbl.achmanager.firstId", out string firstId))
            {
                throw new SkipCheckException(
                    "No achievements in the manager cache; title has none configured in this sandbox.");
            }

            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            ulong xuid = ctx.RequireUser.Id;

            using AchievementsManagerResult result = manager.GetAchievement(xuid, firstId);

            return result.Achievements.Count == 0
                ? $"XblAchievementsManagerGetAchievement('{firstId}'): not found in cache"
                : $"XblAchievementsManagerGetAchievement('{firstId}'): " +
                  $"state={result.Achievements[0].ProgressState} " +
                  $"rewards={result.Achievements[0].Rewards.Count}";
        }, "xbl.achmanager.get-all");

        yield return LiveCheck.Async("xbl.achmanager.update", async ctx =>
        {
            if (!ctx.TryGet<string>("xbl.achmanager.firstId", out string firstId))
            {
                throw new SkipCheckException(
                    "No achievements in the manager cache; title has none configured in this sandbox.");
            }

            AchievementsManager manager = ctx.RequireRuntime.XboxLive.AchievementsManager;
            ulong xuid = ctx.RequireUser.Id;

            // An achievement that is already unlocked has no progress left to report, and the
            // manager rejects the update with E_UNEXPECTED rather than treating it as a no-op. That
            // is the service being consistent, not the projection being wrong, so read the cached
            // state first and skip instead of driving a call that cannot succeed.
            using (AchievementsManagerResult current = manager.GetAchievement(xuid, firstId))
            {
                if (current.Achievements.Count > 0 &&
                    current.Achievements[0].ProgressState == AchievementProgressState.Achieved)
                {
                    throw new SkipCheckException(
                        $"'{firstId}' is already Achieved for this account; XblAchievementsManagerUpdateAchievement " +
                        "rejects progress on an unlocked achievement. Reset the account's player data to exercise this.");
                }
            }

            manager.UpdateAchievement(xuid, firstId, currentProgress: 100);

            // Pump briefly so that any immediate progress-update or unlock event is captured.
            int progressEvents = 0;
            int unlockEvents = 0;
            DateTime deadline = DateTime.UtcNow.AddSeconds(5);

            while (DateTime.UtcNow < deadline)
            {
                foreach (AchievementsManagerEvent evt in manager.DoWork())
                {
                    if (evt.XboxUserId != xuid)
                        continue;
                    if (evt is AchievementsManagerAchievementProgressUpdatedEvent)
                        progressEvents++;
                    else if (evt is AchievementsManagerAchievementUnlockedEvent)
                        unlockEvents++;
                }

                await Task.Delay(100).ConfigureAwait(false);
            }

            return $"XblAchievementsManagerUpdateAchievement('{firstId}', 100): " +
                   $"progressEvents={progressEvents} unlockEvents={unlockEvents}";
        }, "xbl.achmanager.get-single");
    }
}
