using System;
using System.Collections.Generic;
using System.Linq;

namespace GDK.Net.LiveHarness;

/// <summary>
/// The whole check list, in run order.
/// </summary>
/// <remarks>
/// <para>
/// Assembled by an explicit list rather than by reflecting over the assembly. Reflection would be
/// shorter, but the harness publishes with NativeAOT to match what a title ships, and an assembly
/// scan is exactly the pattern the trimmer cannot see through. An explicit list also makes the run
/// order (which is a real contract here, not an incidental detail) readable in one place.
/// </para>
/// <para>
/// Order matters twice. Setup runs before the families that depend on its handles, and teardown runs
/// after everything, because closing an <c>XblContextHandle</c> or signing the user out invalidates
/// state the earlier checks were using.
/// </para>
/// </remarks>
internal static class CheckRegistry
{
    /// <summary>
    /// Checks that establish state, a handle, an initialized subsystem, a value later checks read
    /// out of <see cref="CheckContext.State"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Native state does not survive the process, and an access violation in the Gaming Runtime
    /// cannot be caught in .NET, so a crash means a relaunch. Everything in this set runs again on
    /// resume; everything else is carried forward from the previous report. Without it a crash
    /// anywhere early would leave every later check failing for want of a runtime or a user handle,
    /// which is the same loss of coverage the runner exists to prevent.
    /// </para>
    /// <para>
    /// Listed here rather than tagged at each definition so that the set is auditable in one place:
    /// the failure mode of forgetting one is a confusing cascade a long way from the omission.
    /// </para>
    /// </remarks>
    private static readonly HashSet<string> Replayable = new(StringComparer.Ordinal)
    {
        "runtime.initialize",
        "runtime.title-id",
        "users.subscribe-changed",
        "users.add",
        "package.identifier",
        "store.context",
        "store.product-for-current-game",
        "gamesave.provider",
        "gamesave.create-container",
        "xbl.initialize",
        "xbl.context",
        "xbl.profile.own",
        "xbl.achievements.for-title",
        "xbl.social.relationships",
        "xbl.presence.set",
        "xbl.socialmanager.add-user",
        "xbl.socialmanager.create-filter-group",
        "xbl.titlestorage.upload",
        "xbl.achmanager.add",
        "xbl.achmanager.pump-initialized",
        "xbl.achmanager.get-all",
        "playfab.initialize",
        "playfab.service-config",
        "playfab.title-entity",
        "playfab.local-user",
        "playfab.login",
        "playfab.multiplayer.initialize",
        "playfab.party.initialize",
    };

    public static IEnumerable<LiveCheck> All() =>
        Ordered().Select(check => Replayable.Contains(check.Id) ? check.Replayable() : check);

    private static IEnumerable<LiveCheck> Ordered() =>
        RuntimeChecks.All()
            .Concat(PlatformChecks.All())
            .Concat(PackageChecks.All())
            .Concat(StoreChecks.All())
            .Concat(GameSaveChecks.All())
            .Concat(XboxLiveChecks.All())
            .Concat(XblSocialChecks.All())
            .Concat(XblDataChecks.All())
            .Concat(PlayFabChecks.All())
            .Concat(PlayFabChecks.Teardown())
            .Concat(XblDataChecks.Teardown())
            .Concat(XblSocialChecks.Teardown())
            .Concat(GameSaveChecks.Teardown())
            .Concat(StoreChecks.Teardown())
            .Concat(XboxLiveChecks.Teardown())
            .Concat(RuntimeChecks.Teardown());
}
