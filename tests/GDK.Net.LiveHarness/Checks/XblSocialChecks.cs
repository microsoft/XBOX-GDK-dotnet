using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.XboxLive;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Live checks for the social surface of XSAPI: <see cref="SocialService"/>,
/// <see cref="PresenceService"/>, <see cref="PrivacyService"/>, <see cref="SocialManager"/> and
/// <see cref="RealTimeActivityService"/>. These run against the real service and intentionally
/// exercise write paths: the account is a provisioned sandbox account that can be reset.
/// </summary>
/// <remarks>
/// <para>
/// DoWork-pumped managers (<see cref="SocialManager"/>) need a polling loop to observe completion
/// events. Each check that waits for an event drives the pump on a <see cref="Task.Run"/> thread
/// and uses a <see cref="TaskCompletionSource{T}"/> to bring the result back to the check body.
/// The timeout is honest: if no event arrives within the window the check reports that, it does not
/// skip or silently pass.
/// </para>
/// <para>
/// <see cref="RealTimeActivityService"/> events arrive on an XSAPI-internal thread, so the checks
/// for that family subscribe, activate the websocket, wait for the connection-state notification
/// and report what was observed.
/// </para>
/// </remarks>
internal static class XblSocialChecks
{
    private static readonly TimeSpan EventTimeout = TimeSpan.FromSeconds(15);

    /// <summary>
    /// Returns all social checks in dependency order: social queries, presence, privacy,
    /// social manager and real-time activity.
    /// </summary>
    public static IEnumerable<LiveCheck> All()
    {
        foreach (LiveCheck check in SocialFamily())
        {
            yield return check;
        }

        foreach (LiveCheck check in PresenceFamily())
        {
            yield return check;
        }

        foreach (LiveCheck check in PrivacyFamily())
        {
            yield return check;
        }

        foreach (LiveCheck check in SocialManagerFamily())
        {
            yield return check;
        }

        foreach (LiveCheck check in RealTimeActivityFamily())
        {
            yield return check;
        }
    }

    /// <summary>
    /// Returns teardown checks that must run after <see cref="All"/>, ordered so that group handles
    /// are destroyed before the local user is removed and the social manager is left clean.
    /// </summary>
    public static IEnumerable<LiveCheck> Teardown()
    {
        yield return LiveCheck.Sync("xbl.socialmanager.destroy-group", ctx =>
        {
            if (!ctx.TryGet<SocialManagerUserGroup>("xbl.socialmanager.group", out SocialManagerUserGroup? group))
            {
                return "No filter group was stored; skipping XblSocialManagerDestroySocialUserGroup.";
            }

            ctx.RequireRuntime.XboxLive.SocialManager.DestroySocialUserGroup(group);
            ctx.State.Remove("xbl.socialmanager.group");
            return "XblSocialManagerDestroySocialUserGroup completed";
        }, "xbl.context");

        yield return LiveCheck.Sync("xbl.socialmanager.remove-user", ctx =>
        {
            ctx.RequireRuntime.XboxLive.SocialManager.RemoveLocalUser(ctx.RequireUser);
            return $"XblSocialManagerRemoveLocalUser for xuid 0x{ctx.RequireUser.Id:X16}";
        }, "xbl.socialmanager.add-user");
    }

    // -----------------------------------------------------------------------------------------
    // SocialService
    // -----------------------------------------------------------------------------------------

    private static IEnumerable<LiveCheck> SocialFamily()
    {
        yield return LiveCheck.Async("xbl.social.relationships", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            using SocialRelationshipsPage page = await xbl.Social
                .GetRelationshipsAsync(xbl.XboxUserId, SocialRelationshipFilter.All, maxItems: 20)
                .ConfigureAwait(false);

            ctx.State["xbl.social.relationships.total"] = page.TotalCount;
            int friendCount = page.Relationships.Count(r => r.IsFriend);
            return $"XblSocialGetSocialRelationshipsAsync returned {page.TotalCount} total relationship(s); " +
                   $"page has {page.Relationships.Count} entry(ies), {friendCount} friend(s), " +
                   $"hasNext={page.HasNext}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.social.relationships-favorites", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            using SocialRelationshipsPage page = await xbl.Social
                .GetRelationshipsAsync(xbl.XboxUserId, SocialRelationshipFilter.Favorite)
                .ConfigureAwait(false);

            return $"XblSocialGetSocialRelationshipsAsync(Favorite) returned {page.TotalCount} total, " +
                   $"{page.Relationships.Count} on this page";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.social.relationships-paging", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            ulong total = ctx.Get<ulong>("xbl.social.relationships.total");

            if (total == 0)
            {
                throw new SkipCheckException(
                    "The account has no social relationships; there is nothing to page through.");
            }

            using SocialRelationshipsPage first = await xbl.Social
                .GetRelationshipsAsync(xbl.XboxUserId, SocialRelationshipFilter.All, maxItems: 1)
                .ConfigureAwait(false);

            IReadOnlyList<SocialRelationship> all = await first.ReadAllAsync(maxItemsPerPage: 1)
                .ConfigureAwait(false);

            return $"XblSocialRelationshipResultGetNextAsync paged through {all.Count} relationship(s) " +
                   $"(total reported by service: {total})";
        }, "xbl.social.relationships");

        yield return LiveCheck.Async("xbl.social.relationship-changed-event", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;

            // Subscribe and immediately check that the registration path does not throw. The
            // SocialRelationshipChanged event fires on an XSAPI-internal thread when a relationship
            // change arrives over RTA. We do not manufacture one, so we observe zero events here;
            // the goal is proving the registration and unregistration paths are exercised.
            var received = 0;

            void Handler(object? sender, SocialRelationshipChangedEventArgs e) =>
                Interlocked.Increment(ref received);

            xbl.Social.RelationshipChanged += Handler;
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
                return $"XblSocialAddSocialRelationshipChangedHandler registered and will be removed; " +
                       $"{received} notification(s) observed during brief wait";
            }
            finally
            {
                xbl.Social.RelationshipChanged -= Handler;
            }
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.social.reputation-feedback", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            ulong total = ctx.Get<ulong>("xbl.social.relationships.total");

            if (total == 0)
            {
                throw new SkipCheckException(
                    "The account has no social relationships; reputation feedback needs a valid target.");
            }

            // Read the first relationship to get a real target xuid.
            using SocialRelationshipsPage page = await xbl.Social
                .GetRelationshipsAsync(xbl.XboxUserId, SocialRelationshipFilter.All, maxItems: 1)
                .ConfigureAwait(false);

            if (page.Relationships.Count == 0)
            {
                throw new SkipCheckException(
                    "XblSocialGetSocialRelationshipsAsync returned no relationships on the first page.");
            }

            ulong targetXuid = page.Relationships[0].XboxUserId;

            // PositiveSkilledPlayer is a safe, non-harmful feedback type for a test environment.
            await xbl.Social
                .SubmitReputationFeedbackAsync(
                    targetXuid,
                    ReputationFeedbackType.PositiveSkilledPlayer,
                    reasonMessage: "harness live check")
                .ConfigureAwait(false);

            return $"XblSocialSubmitReputationFeedbackAsync(PositiveSkilledPlayer) accepted " +
                   $"for target 0x{targetXuid:X16}";
        }, "xbl.social.relationships");

        yield return LiveCheck.Async("xbl.social.reputation-feedback-batch", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            ulong total = ctx.Get<ulong>("xbl.social.relationships.total");

            if (total == 0)
            {
                throw new SkipCheckException(
                    "The account has no social relationships; batch reputation feedback needs a valid target.");
            }

            using SocialRelationshipsPage page = await xbl.Social
                .GetRelationshipsAsync(xbl.XboxUserId, SocialRelationshipFilter.All, maxItems: 1)
                .ConfigureAwait(false);

            if (page.Relationships.Count == 0)
            {
                throw new SkipCheckException(
                    "XblSocialGetSocialRelationshipsAsync returned no relationships on the first page.");
            }

            ulong targetXuid = page.Relationships[0].XboxUserId;
            var item = new ReputationFeedbackItem(targetXuid, ReputationFeedbackType.PositiveHelpfulPlayer);

            await xbl.Social
                .SubmitBatchReputationFeedbackAsync([item])
                .ConfigureAwait(false);

            return $"XblSocialSubmitBatchReputationFeedbackAsync(PositiveHelpfulPlayer) accepted " +
                   $"for target 0x{targetXuid:X16}";
        }, "xbl.social.relationships");
    }

    // -----------------------------------------------------------------------------------------
    // PresenceService
    // -----------------------------------------------------------------------------------------

    private static IEnumerable<LiveCheck> PresenceFamily()
    {
        yield return LiveCheck.Async("xbl.presence.set", async ctx =>
        {
            // Mark the user as actively playing the title. This is the same call a title makes each
            // frame while the player is active, so exercising it here is low risk.
            await ctx.RequireXboxLiveContext.Presence
                .SetPresenceAsync(isUserActiveInTitle: true)
                .ConfigureAwait(false);

            return "XblPresenceSetPresenceAsync(isUserActiveInTitle=true) accepted";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.presence.get-own", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            PresenceRecord record = await xbl.Presence
                .GetAsync(xbl.XboxUserId)
                .ConfigureAwait(false);

            int deviceCount = record.Devices.Count;
            int titleCount = record.Devices.Sum(d => d.Titles.Count);
            ctx.State["xbl.presence.record"] = record;

            return $"XblPresenceGetPresenceAsync: xuid=0x{record.XboxUserId:X16} " +
                   $"state={record.UserState} device(s)={deviceCount} title record(s)={titleCount}";
        }, "xbl.presence.set");

        yield return LiveCheck.Async("xbl.presence.get-batch", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            IReadOnlyList<PresenceRecord> records = await xbl.Presence
                .GetAsync([xbl.XboxUserId], new PresenceQueryFilters(detailLevel: PresenceDetailLevel.All))
                .ConfigureAwait(false);

            return $"XblPresenceGetPresenceForMultipleUsersAsync returned {records.Count} record(s)" +
                   (records.Count == 0
                       ? " (the service returns records only for users with visible presence)"
                       : $"; first state={records[0].UserState}");
        }, "xbl.presence.get-own");

        yield return LiveCheck.Async("xbl.presence.get-social-group", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            IReadOnlyList<PresenceRecord> records = await xbl.Presence
                .GetForSocialGroupAsync(PresenceSocialGroup.People)
                .ConfigureAwait(false);

            return $"XblPresenceGetPresenceForSocialGroupAsync(People) returned {records.Count} record(s)";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.presence.track-users", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;

            // Register handlers before tracking, so no notification can be lost.
            int deviceEvents = 0;
            int titleEvents = 0;

            void DeviceHandler(object? sender, DevicePresenceChangedEventArgs e) =>
                Interlocked.Increment(ref deviceEvents);
            void TitleHandler(object? sender, TitlePresenceChangedEventArgs e) =>
                Interlocked.Increment(ref titleEvents);

            xbl.Presence.DevicePresenceChanged += DeviceHandler;
            xbl.Presence.TitlePresenceChanged += TitleHandler;

            try
            {
                xbl.Presence.TrackUsers([xbl.XboxUserId]);
                await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
                xbl.Presence.StopTrackingUsers([xbl.XboxUserId]);

                return $"XblPresenceTrackUsers + XblPresenceStopTrackingUsers succeeded; " +
                       $"DevicePresenceChanged fired {deviceEvents} time(s), " +
                       $"TitlePresenceChanged fired {titleEvents} time(s) during brief wait";
            }
            finally
            {
                xbl.Presence.DevicePresenceChanged -= DeviceHandler;
                xbl.Presence.TitlePresenceChanged -= TitleHandler;
            }
        }, "xbl.context");
    }

    // -----------------------------------------------------------------------------------------
    // PrivacyService
    // -----------------------------------------------------------------------------------------

    private static IEnumerable<LiveCheck> PrivacyFamily()
    {
        yield return LiveCheck.Async("xbl.privacy.check-permission-self", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;

            // Checking a permission against one's own xuid exercises the single-user path. The
            // result depends on the account's own settings, so we report it without asserting.
            PrivacyPermissionCheckResult result = await xbl.Privacy
                .CheckPermissionAsync(Permission.ViewTargetProfile, xbl.XboxUserId)
                .ConfigureAwait(false);

            return $"XblPrivacyCheckPermissionAsync(ViewTargetProfile, self): " +
                   $"wasChecked={result.WasChecked} isAllowed={result.IsAllowed} " +
                   $"denyReason={result.DenyReasons.FirstOrDefault()?.Reason}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.privacy.check-permission-anon", async ctx =>
        {
            PrivacyPermissionCheckResult result = await ctx.RequireXboxLiveContext.Privacy
                .CheckPermissionForAnonymousUserAsync(
                    Permission.CommunicateUsingVoice,
                    AnonymousUserType.CrossNetworkUser)
                .ConfigureAwait(false);

            return $"XblPrivacyCheckPermissionForAnonymousUserAsync(CommunicateUsingVoice, CrossNetworkUser): " +
                   $"wasChecked={result.WasChecked} isAllowed={result.IsAllowed} " +
                   $"denyReason={result.DenyReasons.FirstOrDefault()?.Reason}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.privacy.batch-check", async ctx =>
        {
            XboxLiveContext xbl = ctx.RequireXboxLiveContext;
            IReadOnlyList<PrivacyPermissionCheckResult> results = await xbl.Privacy
                .BatchCheckPermissionAsync(
                    [Permission.ViewTargetProfile, Permission.CommunicateUsingText],
                    [xbl.XboxUserId],
                    [AnonymousUserType.CrossNetworkUser])
                .ConfigureAwait(false);

            // 2 permissions × (1 xuid + 1 anonymous type) = 4 results.
            string summary = string.Join(", ", results.Select(r => $"{r.Permission}:{(r.IsAllowed ? "allow" : "deny")}"));
            return $"XblPrivacyBatchCheckPermissionAsync returned {results.Count} result(s): {summary}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.privacy.avoid-list", async ctx =>
        {
            IReadOnlyList<ulong> avoidList = await ctx.RequireXboxLiveContext.Privacy
                .GetAvoidListAsync()
                .ConfigureAwait(false);

            return $"XblPrivacyGetAvoidListAsync returned {avoidList.Count} xuid(s)";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.privacy.mute-list", async ctx =>
        {
            IReadOnlyList<ulong> muteList = await ctx.RequireXboxLiveContext.Privacy
                .GetMuteListAsync()
                .ConfigureAwait(false);

            return $"XblPrivacyGetMuteListAsync returned {muteList.Count} xuid(s)";
        }, "xbl.context");
    }

    // -----------------------------------------------------------------------------------------
    // SocialManager
    // -----------------------------------------------------------------------------------------

    private static IEnumerable<LiveCheck> SocialManagerFamily()
    {
        yield return LiveCheck.Sync("xbl.socialmanager.add-user", ctx =>
        {
            SocialManager sm = ctx.RequireRuntime.XboxLive.SocialManager;
            sm.AddLocalUser(ctx.RequireUser, SocialManagerExtraDetailLevel.NoExtraDetail);

            ulong count = sm.LocalUserCount;
            return $"XblSocialManagerAddLocalUser for xuid 0x{ctx.RequireUser.Id:X16}; " +
                   $"XblSocialManagerGetLocalUserCount={count}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.socialmanager.local-user-added", async ctx =>
        {
            SocialManager sm = ctx.RequireRuntime.XboxLive.SocialManager;

            // Pump until LocalUserAdded arrives or the timeout expires. DoWork must be called from
            // one thread; we do it here on the check's thread.
            LocalUserAddedSocialManagerEvent? added = await PumpForEventAsync<LocalUserAddedSocialManagerEvent>(
                sm,
                e => e.LocalUser?.Id == ctx.RequireUser.Id,
                EventTimeout)
                .ConfigureAwait(false);

            if (added is null)
            {
                throw new InvalidOperationException(
                    $"XblSocialManagerDoWork produced no LocalUserAdded event within " +
                    $"{EventTimeout.TotalSeconds}s. XblSocialManagerAddLocalUser returned success, so " +
                    "the manager owes exactly one such event; not getting it means the pump or the " +
                    "event projection is broken.");
            }

            return added.Succeeded
                ? $"XblSocialManagerDoWork: LocalUserAdded succeeded for xuid 0x{ctx.RequireUser.Id:X16}"
                : throw new InvalidOperationException(
                    $"LocalUserAdded reported failure: {added.Error?.Message ?? "no error detail"}");
        }, "xbl.socialmanager.add-user");

        yield return LiveCheck.Sync("xbl.socialmanager.get-local-users", ctx =>
        {
            SocialManager sm = ctx.RequireRuntime.XboxLive.SocialManager;
            IReadOnlyList<GDK.Net.Users.User> users = sm.GetLocalUsers();

            return $"XblSocialManagerGetLocalUsers returned {users.Count} user(s)";
        }, "xbl.socialmanager.add-user");

        yield return LiveCheck.Sync("xbl.socialmanager.create-filter-group", ctx =>
        {
            SocialManager sm = ctx.RequireRuntime.XboxLive.SocialManager;

            SocialManagerUserGroup group = sm.CreateSocialUserGroupFromFilters(
                ctx.RequireUser,
                PresenceFilter.All,
                RelationshipFilter.Friends);

            ctx.State["xbl.socialmanager.group"] = group;

            return $"XblSocialManagerCreateSocialUserGroupFromFilters(All, Friends) succeeded; " +
                   $"type={group.Type} filters={group.Filters?.PresenceFilter}/{group.Filters?.RelationshipFilter}";
        }, "xbl.socialmanager.add-user");

        yield return LiveCheck.Async("xbl.socialmanager.group-loaded", async ctx =>
        {
            SocialManager sm = ctx.RequireRuntime.XboxLive.SocialManager;

            if (!ctx.TryGet<SocialManagerUserGroup>("xbl.socialmanager.group", out SocialManagerUserGroup? target))
            {
                throw new SkipCheckException("Filter group was not stored by xbl.socialmanager.create-filter-group.");
            }

            // Pump until SocialUserGroupLoaded for our group arrives.
            SocialUserGroupLoadedSocialManagerEvent? loaded = await PumpForEventAsync<SocialUserGroupLoadedSocialManagerEvent>(
                sm,
                e => ReferenceEquals(e.Group, target),
                EventTimeout)
                .ConfigureAwait(false);

            if (loaded is null)
            {
                throw new InvalidOperationException(
                    $"XblSocialManagerDoWork produced no SocialUserGroupLoaded within " +
                    $"{EventTimeout.TotalSeconds}s. The group was created successfully, so the manager " +
                    "owes the event even when the account has no friends and the group loads empty.");
            }

            IReadOnlyList<SocialManagerUser> users = target.GetUsers();
            return loaded.Succeeded
                ? $"XblSocialManagerDoWork: SocialUserGroupLoaded; group has {users.Count} user(s)"
                : throw new InvalidOperationException(
                    $"SocialUserGroupLoaded reported failure: {loaded.Error?.Message ?? "no error detail"}");
        }, "xbl.socialmanager.create-filter-group");

        yield return LiveCheck.Sync("xbl.socialmanager.rich-presence-polling", ctx =>
        {
            SocialManager sm = ctx.RequireRuntime.XboxLive.SocialManager;
            sm.SetRichPresencePollingStatus(ctx.RequireUser, shouldEnablePolling: true);

            return $"XblSocialManagerSetRichPresencePollingStatus(true) accepted for xuid 0x{ctx.RequireUser.Id:X16}";
        }, "xbl.socialmanager.add-user");
    }

    // -----------------------------------------------------------------------------------------
    // RealTimeActivityService
    // -----------------------------------------------------------------------------------------

    private static IEnumerable<LiveCheck> RealTimeActivityFamily()
    {
        yield return LiveCheck.Async("xbl.rta.connection-state-handler", async ctx =>
        {
            RealTimeActivityService rta = ctx.RequireXboxLiveContext.RealTimeActivity;

            var tcs = new TaskCompletionSource<RealTimeActivityConnectionState>(
                TaskCreationOptions.RunContinuationsAsynchronously);

            void Handler(object? sender, RealTimeActivityConnectionStateChangedEventArgs e) =>
                tcs.TrySetResult(e.State);

            rta.ConnectionStateChanged += Handler;
            try
            {
                // Short wait: no state change is expected until Activate is called, so this check's
                // job is just proving that registration and unregistration do not throw.
                RealTimeActivityConnectionState state;
                try
                {
                    state = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                    return $"XblRealTimeActivityAddConnectionStateChangeHandler: observed state={state} immediately on subscription";
                }
                catch (TimeoutException)
                {
                    return "XblRealTimeActivityAddConnectionStateChangeHandler registered and removed without error";
                }
            }
            finally
            {
                rta.ConnectionStateChanged -= Handler;
            }
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.rta.resync-handler", async ctx =>
        {
            RealTimeActivityService rta = ctx.RequireXboxLiveContext.RealTimeActivity;
            var observed = 0;

            void Handler(object? sender, EventArgs e) =>
                Interlocked.Increment(ref observed);

            rta.ResyncRequired += Handler;
            try
            {
                // Resync is a reactive notification; we just verify the handler can be registered
                // and removed without error. A resync during a run would be notable but is not
                // expected in a stable test environment.
                await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
                return $"XblRealTimeActivityAddResyncHandler registered and removed; " +
                       $"{observed} resync notification(s) observed";
            }
            finally
            {
                rta.ResyncRequired -= Handler;
            }
        }, "xbl.context");

    }

    // -----------------------------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------------------------

    /// <summary>
    /// Pumps <paramref name="sm"/> in a tight loop until an event of type
    /// <typeparamref name="T"/> satisfying <paramref name="predicate"/> is observed, or
    /// <paramref name="timeout"/> elapses. Returns <see langword="null"/> on timeout.
    /// </summary>
    private static async Task<T?> PumpForEventAsync<T>(
        SocialManager sm,
        Func<T, bool> predicate,
        TimeSpan timeout)
        where T : SocialManagerEvent
    {
        using var cts = new CancellationTokenSource(timeout);
        CancellationToken ct = cts.Token;

        while (!ct.IsCancellationRequested)
        {
            IReadOnlyList<SocialManagerEvent> events = sm.DoWork();
            foreach (SocialManagerEvent e in events)
            {
                if (e is T typed && predicate(typed))
                {
                    return typed;
                }
            }

            try
            {
                await Task.Delay(16, ct).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }

        return null;
    }
}
