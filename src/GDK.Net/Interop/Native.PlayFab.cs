// The PlayFab modules the projection binds. Unlike the Gaming Runtime, whose public surface is a
// statically linked stub set reachable only through xgameruntime.thunks.dll, every PlayFab module
// ships as an ordinary DLL that exports its flat C surface directly, so each family is bound
// against the module that exports it.
//
// All six must be redistributed next to the game executable inside the package layout; they are
// not present in System32. eng/packaging/GdkRedist.targets copies them from the pinned GDK
// edition's windows\bin tree.
//
// Two shims exist:
//   * net8.0 / net10.0    -> [LibraryImport], source-generated and trimming/AOT friendly.
//   * netstandard2.0      -> [DllImport], the only option on the older surface.
// The generated Native.PF*.cs partials carry both, and every parameter is blittable so the two
// generate equivalent stubs.

namespace GDK.Net.Interop;

internal static unsafe partial class NativePlayFab
{
    /// <summary>PlayFab Core: initialization, service config, entities, authentication, events.</summary>
    internal const string PlayFabCoreLibrary = "PlayFabCore.dll";

    /// <summary>PlayFab Services: the generated title service surface (economy, data, groups, ...).</summary>
    internal const string PlayFabServicesLibrary = "PlayFabServices.dll";

    /// <summary>PlayFab Game Save.</summary>
    internal const string PlayFabGameSaveLibrary = "PlayFabGameSave.dll";

    /// <summary>PlayFab Multiplayer (PFMP): lobby and matchmaking.</summary>
    internal const string PlayFabMultiplayerLibrary = "PlayFabMultiplayer.dll";

    /// <summary>PlayFab Party: networking, voice and text chat.</summary>
    internal const string PartyLibrary = "Party.dll";

    /// <summary>PlayFab Party Xbox Live extension.</summary>
    internal const string PartyXboxLiveLibrary = "PartyXboxLive.dll";
}
