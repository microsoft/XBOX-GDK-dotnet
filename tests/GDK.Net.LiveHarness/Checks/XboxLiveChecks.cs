using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Users;
using GDK.Net.XboxLive;

namespace GDK.Net.LiveHarness;

/// <summary>
/// The Xbox Live (XSAPI) surface: initialize, build a context for the signed-in user, and exercise
/// the per-user services hanging off it.
/// </summary>
/// <remarks>
/// <para>
/// These need three things beyond what the rest of the harness needs: the two XSAPI redistributables
/// next to the executable (build with <c>GdkNetIncludeXboxLive=true</c>), a SCID for the title
/// (<c>--scid</c> or <c>%GDKNET_SCID%</c>), and an account signed in to the sandbox the SCID belongs
/// to. A missing prerequisite is skipped rather than failed, because the rest of the harness is
/// still useful on a machine that has none of them.
/// </para>
/// <para>
/// The context is created once and reused by every check below it, which is deliberate: rebuilding
/// it per check would hide the failure mode most worth catching, namely an <c>XblContextHandle</c>
/// that stops being valid partway through a long run.
/// </para>
/// </remarks>
internal static class XboxLiveChecks
{
    public static IEnumerable<LiveCheck> All()
    {
        yield return LiveCheck.Sync("xbl.initialize", ctx =>
        {
            try
            {
                ctx.RequireRuntime.XboxLive.Initialize(new XboxLiveOptions { Scid = ctx.RequireScid });
            }
            catch (DllNotFoundException ex)
            {
                throw new SkipCheckException(
                    "Microsoft.Xbox.Services.C.Thunks.dll / libHttpClient.dll are not next to the " +
                    $"executable. Rebuild with GdkNetIncludeXboxLive=true. ({ex.Message})");
            }

            return $"XblInitialize succeeded; XblGetScid = '{ctx.RequireRuntime.XboxLive.Scid}'";
        }, "runtime.title-id");

        yield return LiveCheck.Sync("xbl.context", ctx =>
        {
            ctx.XboxLiveContext = ctx.RequireRuntime.XboxLive.CreateContext(ctx.RequireUser);
            return $"XblContextCreateHandle for xuid 0x{ctx.XboxLiveContext.XboxUserId:X16}";
        }, "xbl.initialize", "users.add");

        yield return LiveCheck.Sync("xbl.settings", ctx =>
        {
            var settings = ctx.RequireXboxLiveContext.Settings;
            return $"LongHttpTimeout={settings.LongHttpTimeout}, " +
                   $"HttpRetryDelay={settings.HttpRetryDelay}, " +
                   $"HttpTimeoutWindow={settings.HttpTimeoutWindow}, " +
                   $"WebsocketTimeoutWindow={settings.WebsocketTimeoutWindow}, " +
                   $"UseCrossPlatformQosServers={settings.UseCrossPlatformQosServers}";
        }, "xbl.context");

        yield return LiveCheck.Sync("xbl.context-identity", ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            User user = ctx.RequireUser;

            using User fromContext = context.GetUser();
            using XboxLiveContext duplicate = context.Duplicate();

            return context.XboxUserId == user.Id && fromContext == user && duplicate.XboxUserId == context.XboxUserId
                ? $"XblContextGetXboxUserId, XblContextGetUser and XblContextDuplicateHandle all agree on 0x{user.Id:X16}"
                : throw new InvalidOperationException(
                    $"Context identity mismatch: context=0x{context.XboxUserId:X16} user=0x{user.Id:X16} " +
                    $"duplicate=0x{duplicate.XboxUserId:X16} handlesEqual={fromContext == user}");
        }, "xbl.context");

        foreach (LiveCheck check in ProfileFamily())
        {
            yield return check;
        }

        foreach (LiveCheck check in AchievementFamily())
        {
            yield return check;
        }
    }

    private static IEnumerable<LiveCheck> ProfileFamily()
    {
        yield return LiveCheck.Async("xbl.profile.own", async ctx =>
        {
            UserProfile profile = await ctx.RequireXboxLiveContext.Profiles.GetOwnAsync().ConfigureAwait(false);
            ctx.State["xbl.profile"] = profile;
            return $"XblProfileGetUserProfileAsync: gamertag='{profile.Gamertag}' " +
                   $"modern='{profile.UniqueModernGamertag}' gamerscore={profile.Gamerscore} " +
                   $"pic={profile.GameDisplayPictureUri}";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.profile.batch", async ctx =>
        {
            var profile = ctx.Get<UserProfile>("xbl.profile");
            IReadOnlyList<UserProfile> profiles = await ctx.RequireXboxLiveContext.Profiles
                .GetAsync([profile.XboxUserId]).ConfigureAwait(false);
            return $"XblProfileGetUserProfilesAsync returned {profiles.Count} profile(s): " +
                   string.Join(", ", profiles.Select(p => p.Gamertag));
        }, "xbl.profile.own");

        yield return LiveCheck.Async("xbl.profile.social-group", async ctx =>
        {
            IReadOnlyList<UserProfile> friends = await ctx.RequireXboxLiveContext.Profiles
                .GetForSocialGroupAsync(SocialGroup.People).ConfigureAwait(false);
            return $"XblProfileGetUserProfilesForSocialGroupAsync returned {friends.Count} profile(s)";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.profile.cancellation", async ctx =>
        {
            using var cts = new CancellationTokenSource();
            Task<UserProfile> pending = ctx.RequireXboxLiveContext.Profiles.GetOwnAsync(cts.Token);
            cts.Cancel();

            try
            {
                UserProfile profile = await pending.ConfigureAwait(false);
                return $"Completed before XAsyncCancel took effect ('{profile.Gamertag}'); cancellation is best-effort.";
            }
            catch (OperationCanceledException)
            {
                return "XAsyncCancel produced E_ABORT and surfaced as OperationCanceledException.";
            }
        }, "xbl.context");
    }

    private static IEnumerable<LiveCheck> AchievementFamily()
    {
        yield return LiveCheck.Async("xbl.achievements.for-title", async ctx =>
        {
            XboxLiveContext context = ctx.RequireXboxLiveContext;
            using AchievementsPage page = await context.Achievements
                .GetForTitleAsync(context.XboxUserId, ctx.Get<uint>("xbl.titleId"), maxItems: 10)
                .ConfigureAwait(false);

            // Snapshot before disposal is deliberate: Achievement is a managed copy, so the list
            // stays readable once the result handle is gone.
            IReadOnlyList<Achievement> all = await page.ReadAllAsync(maxItemsPerPage: 10).ConfigureAwait(false);
            ctx.State["xbl.achievements"] = all;

            return all.Count == 0
                ? "XblAchievementsGetAchievementsForTitleIdAsync returned no achievements (expected " +
                  "for a title with none configured in this sandbox)"
                : $"Read {all.Count} achievement(s) across all pages; " +
                  $"{all.Count(a => a.IsUnlocked)} unlocked. First: {all[0]}";
        }, "xbl.context", "runtime.title-id");

        yield return LiveCheck.Async("xbl.achievements.single", async ctx =>
        {
            Achievement first = FirstAchievement(ctx);
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            using AchievementsPage page = await context.Achievements
                .GetAsync(context.XboxUserId, first.ServiceConfigurationId, first.Id)
                .ConfigureAwait(false);

            Achievement single = page.Achievements[0];
            return $"XblAchievementsGetAchievementAsync round-tripped '{single.Id}': " +
                   $"state={single.ProgressState} rewards={single.Rewards.Count} " +
                   $"requirements={single.Progression.Requirements.Count}";
        }, "xbl.achievements.for-title");

        yield return LiveCheck.Async("xbl.achievements.update", async ctx =>
        {
            Achievement first = FirstAchievement(ctx);
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            var observed = 0;
            void Handler(object? sender, AchievementProgressChangedEventArgs e) =>
                Interlocked.Add(ref observed, e.Changes.Count);

            context.Achievements.ProgressChanged += Handler;
            try
            {
                // This writes real progress to the account. That is the point: it is the only way to
                // prove the call marshals correctly, and the service treats a repeated or lower value
                // as a no-op, so a re-run against an already-unlocked achievement is harmless.
                await context.Achievements
                    .UpdateAsync(context.XboxUserId, first.Id, percentComplete: 100)
                    .ConfigureAwait(false);

                // The notification arrives over RTA, which the harness has not subscribed to, so a
                // zero count is expected here rather than a failure.
                await Task.Delay(TimeSpan.FromMilliseconds(250)).ConfigureAwait(false);

                return $"XblAchievementsUpdateAchievementAsync set '{first.Id}' to 100%; " +
                       $"{observed} progress-change notification(s) observed";
            }
            finally
            {
                context.Achievements.ProgressChanged -= Handler;
            }
        }, "xbl.achievements.for-title");

        yield return LiveCheck.Async("xbl.achievements.update-for-title", async ctx =>
        {
            Achievement first = FirstAchievement(ctx);
            XboxLiveContext context = ctx.RequireXboxLiveContext;

            await context.Achievements
                .UpdateForTitleAsync(
                    context.XboxUserId,
                    ctx.Get<uint>("xbl.titleId"),
                    first.ServiceConfigurationId,
                    first.Id,
                    percentComplete: 100)
                .ConfigureAwait(false);

            return $"XblAchievementsUpdateAchievementForTitleIdAsync accepted '{first.Id}'";
        }, "xbl.achievements.update");
    }

    private static Achievement FirstAchievement(CheckContext ctx)
    {
        var all = ctx.Get<IReadOnlyList<Achievement>>("xbl.achievements");
        return all.Count > 0
            ? all[0]
            : throw new SkipCheckException("The title has no achievements configured in this sandbox.");
    }

    /// <summary>
    /// Teardown, ordered so handles die before the library that owns them.
    /// </summary>
    /// <remarks>
    /// Registered as checks rather than run in a <c>finally</c> so that a failure to clean up is
    /// reported rather than swallowed. <c>XblCleanupAsync</c> failing is a real defect, and a
    /// <c>finally</c> block would hide it.
    /// </remarks>
    public static IEnumerable<LiveCheck> Teardown()
    {
        yield return LiveCheck.Sync("xbl.context.dispose", ctx =>
        {
            ctx.RequireXboxLiveContext.Dispose();
            ctx.XboxLiveContext = null;
            return "XblContextCloseHandle";
        }, "xbl.context");

        yield return LiveCheck.Async("xbl.cleanup", async ctx =>
        {
            await ctx.RequireRuntime.XboxLive.CleanupAsync().ConfigureAwait(false);
            return "XblCleanupAsync completed";
        }, "xbl.initialize");
    }
}
