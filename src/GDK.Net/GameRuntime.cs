using System;
using GDK.Net.Activation;
using GDK.Net.Capture;
using GDK.Net.GameUI;
using GDK.Net.Networking;
using GDK.Net.Interop;
using GDK.Net.Users;
using GDK.Net.XboxLive;

namespace GDK.Net;

/// <summary>
/// Controls where <c>XGameRuntimeInitializeWithOptions</c> loads the game configuration from.
/// Mirrors <c>XGameRuntimeGameConfigSource</c> from XGameRuntimeInit.h.
/// </summary>
public enum GameRuntimeGameConfigSource
{
    /// <summary>Use the default MicrosoftGame.config from the package layout.</summary>
    Default = 0,

    /// <summary>The <see cref="GameRuntimeOptions.GameConfig"/> property is an inline XML string.</summary>
    Inline = 1,

    /// <summary>The <see cref="GameRuntimeOptions.GameConfig"/> property is a file path.</summary>
    File = 2,
}

/// <summary>
/// Options for <see cref="GameRuntime.Initialize(GameRuntimeOptions)"/>.
/// Mirrors <c>struct XGameRuntimeOptions</c> from XGameRuntimeInit.h.
/// </summary>
public sealed class GameRuntimeOptions
{
    /// <summary>
    /// Where to load the game configuration from. Defaults to
    /// <see cref="GameRuntimeGameConfigSource.Default"/>.
    /// </summary>
    public GameRuntimeGameConfigSource GameConfigSource { get; set; } = GameRuntimeGameConfigSource.Default;

    /// <summary>
    /// Inline XML game-config content (when <see cref="GameConfigSource"/> is
    /// <see cref="GameRuntimeGameConfigSource.Inline"/>) or a file path (when
    /// <see cref="GameRuntimeGameConfigSource.File"/>). Ignored for
    /// <see cref="GameRuntimeGameConfigSource.Default"/>.
    /// </summary>
    public string? GameConfig { get; set; }
}

/// <summary>
/// Gaming Runtime features that can be probed with
/// <see cref="GameRuntime.IsFeatureAvailable(GameRuntimeFeature)"/>. Mirrors <c>XGameRuntimeFeature</c>.
/// </summary>
public enum GameRuntimeFeature : uint
{
    /// <summary>Accessibility APIs.</summary>
    Accessibility = 0,

    /// <summary>App-capture APIs.</summary>
    AppCapture = 1,

    /// <summary>Asynchronous operation APIs.</summary>
    Async = 2,

    /// <summary>Asynchronous provider APIs.</summary>
    AsyncProvider = 3,

    /// <summary>Display APIs.</summary>
    Display = 4,

    /// <summary>Game identity and launcher APIs.</summary>
    Game = 5,

    /// <summary>Game-invite activation APIs.</summary>
    GameInvite = 6,

    /// <summary>Game-save APIs.</summary>
    GameSave = 7,

    /// <summary>System Game UI APIs.</summary>
    GameUI = 8,

    /// <summary>Cross-game launcher APIs.</summary>
    Launcher = 9,

    /// <summary>Networking APIs.</summary>
    Networking = 10,

    /// <summary>Package APIs.</summary>
    Package = 11,

    /// <summary>Persistent local storage APIs.</summary>
    PersistentLocalStorage = 12,

    /// <summary>Speech synthesizer APIs.</summary>
    SpeechSynthesizer = 13,

    /// <summary>Microsoft Store APIs.</summary>
    Store = 14,

    /// <summary>System information APIs.</summary>
    System = 15,

    /// <summary>Task queue APIs.</summary>
    TaskQueue = 16,

    /// <summary>Thread marker APIs.</summary>
    Thread = 17,

    /// <summary>User identity APIs.</summary>
    User = 18,

    /// <summary>Error reporting APIs.</summary>
    Error = 19,

    /// <summary>Game event APIs.</summary>
    GameEvent = 20,

    /// <summary>Game streaming APIs.</summary>
    GameStreaming = 21,
}

/// <summary>
/// The entry point of the projection: initializes the Gaming Runtime, owns the default task queue,
/// and exposes the feature areas.
/// </summary>
/// <remarks>
/// <para>
/// The Gaming Runtime is only available to a packaged GDK title with a valid
/// <c>MicrosoftGame.config</c>. On any other process the underlying calls fail, and this type
/// surfaces that as a <see cref="GameRuntimeException"/> rather than a raw
/// <see cref="DllNotFoundException"/>.
/// </para>
/// <para>
/// Disposal order matters: all GDK objects must be released before
/// <c>XGameRuntimeUninitialize</c>, otherwise the runtime reports
/// <c>E_GAMERUNTIME_UNINITIALIZE_ACTIVEOBJECTS</c>. <see cref="Dispose"/> therefore tears down the
/// feature areas and the owned queue first. That includes the process-wide PlayFab libraries,
/// which are not owned by this object: they are torn down in the exact reverse of the fixed
/// startup order described on <see cref="SubsystemOrder"/>, so a title never has to sequence the
/// libraries by hand and cannot leave one running into <c>XGameRuntimeUninitialize</c>.
/// </para>
/// </remarks>
public sealed class GameRuntime : IDisposable
{
    private bool _disposed;

    // XGameRuntimeInitialize takes no arguments, so there is no runtime-wide queue to hold: the
    // managers name no queue and each operation resolves the process default at call time. A title
    // that needs a specific queue passes one per call, or constructs the manager it cares about.
    private GameRuntime()
    {
        Users = new UserManager(null);
        Activation = new GameActivationManager(null);
        GameUi = new GameUiManager(null);
        Networking = new NetworkingManager(null);
        Capture = new AppCaptureManager(null);
        XboxLive = new XboxLiveService(null);
    }

    /// <summary>User sign-in, identity and change events.</summary>
    public UserManager Users { get; }

    /// <summary>Protocol and game-invite activation events.</summary>
    public GameActivationManager Activation { get; }

    /// <summary>System-provided UI dialogs: text entry, player picker, profile cards and error dialogs.</summary>
    public GameUiManager GameUi { get; }

    /// <summary>Network connectivity, cost policy and privilege checks.</summary>
    public NetworkingManager Networking { get; }

    /// <summary>Screenshot, clip recording and broadcast integration.</summary>
    public AppCaptureManager Capture { get; }

    /// <summary>
    /// Xbox Live Services (XSAPI): profiles, achievements and the rest of the Xbox Live surface.
    /// </summary>
    /// <remarks>
    /// Inert until <see cref="XboxLiveService.Initialize(XboxLiveOptions)"/> is called with the
    /// title's SCID. Accessing this property loads nothing: XSAPI's native modules are only pulled
    /// in by the first real call, so a title that does not use Xbox Live is unaffected.
    /// </remarks>
    public XboxLiveService XboxLive { get; }

    /// <summary>
    /// Initializes the Gaming Runtime (<c>XGameRuntimeInitialize</c>).
    /// </summary>
    /// <remarks>
    /// Async calls and event registrations name no task queue, so the Gaming Runtime resolves the
    /// process default at call time, a thread-pool queue on both ports unless the host process
    /// replaced it. Continuations and events therefore arrive on the thread pool, and a title that
    /// must touch its renderer marshals to its own thread as it would for any other background
    /// callback.
    /// </remarks>
    public static GameRuntime Initialize()
    {
        int hr = Invoke(static () => Native.XGameRuntimeInitialize());
        Hr.ThrowIfFailed(hr);

        return new GameRuntime();
    }

    /// <summary>
    /// Initializes the Gaming Runtime with custom options (<c>XGameRuntimeInitializeWithOptions</c>).
    /// </summary>
    /// <param name="options">
    /// Initialization options. When <see langword="null"/> this overload behaves identically to
    /// <see cref="Initialize()"/>.
    /// </param>
    public static unsafe GameRuntime Initialize(GameRuntimeOptions? options)
    {
        int hr;
        if (options is null)
        {
            hr = Invoke(static () => Native.XGameRuntimeInitialize());
        }
        else
        {
            IntPtr gameConfigPtr = Utf8.Allocate(options.GameConfig);
            try
            {
                var nativeOptions = new XGameRuntimeOptions
                {
                    GameConfigSource = (XGameRuntimeGameConfigSource)options.GameConfigSource,
                    GameConfig = gameConfigPtr,
                };
                // Cannot use Invoke() here because a lambda cannot capture the address of a local.
                try
                {
                    hr = Native.XGameRuntimeInitializeWithOptions(&nativeOptions);
                }
                catch (DllNotFoundException ex)
                {
                    throw new GameRuntimeException(
                        HResult.EGameRuntimeDllNotFound,
                        HResult.Describe(HResult.EGameRuntimeDllNotFound),
                        ex);
                }
                catch (EntryPointNotFoundException ex)
                {
                    throw new GameRuntimeException(
                        HResult.EGameRuntimeVersionMismatch,
                        HResult.Describe(HResult.EGameRuntimeVersionMismatch),
                        ex);
                }
            }
            finally
            {
                Utf8.Free(gameConfigPtr);
            }
        }

        Hr.ThrowIfFailed(hr);

        return new GameRuntime();
    }

    /// <summary>
    /// Reports whether the installed Gaming Runtime provides <paramref name="feature"/>
    /// (<c>XGameRuntimeIsFeatureAvailable</c>).
    /// </summary>
    public bool IsFeatureAvailable(GameRuntimeFeature feature)
    {
        ThrowIfDisposed();
        return Native.XGameRuntimeIsFeatureAvailable((XGameRuntimeFeature)feature) != 0;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        // Reverse of the fixed initialization order (see SubsystemOrder): anything the title left
        // running comes down first: Party, then multiplayer, so neither can fault at process
        // exit or block XGameRuntimeUninitialize.
        RuntimeLifetime.DisposeAll();

        // PlayFab core next, and before Xbox Live: its shutdown is asynchronous and runs on the
        // process default task queue, so it must complete while that queue is still alive.
        // Skipped entirely when the title never initialized PlayFab, or already shut it down.
        PlayFab.PlayFabRuntime.ShutdownIfInitialized();

        // Xbox Live next: XblCleanupAsync has no synchronous form and must likewise complete while
        // the task queue is still alive.
        XboxLive.Dispose();
        Capture.Dispose();
        Networking.Dispose();
        GameUi.Dispose();
        Activation.Dispose();
        Users.Dispose();

        // The task queue, when there is one, belongs to the caller who supplied it. This overload
        // never creates one, so there is nothing here to dispose.
        Native.XGameRuntimeUninitialize();
    }

    /// <summary>
    /// Runs the first native call of a sequence, translating a missing Gaming Runtime into the GDK's
    /// own <c>E_GAMERUNTIME_DLL_NOT_FOUND</c> so callers only ever see
    /// <see cref="GameRuntimeException"/>.
    /// </summary>
    private static int Invoke(Func<int> call)
    {
        try
        {
            return call();
        }
        catch (DllNotFoundException ex)
        {
            throw new GameRuntimeException(
                HResult.EGameRuntimeDllNotFound,
                HResult.Describe(HResult.EGameRuntimeDllNotFound),
                ex);
        }
        catch (EntryPointNotFoundException ex)
        {
            throw new GameRuntimeException(
                HResult.EGameRuntimeVersionMismatch,
                HResult.Describe(HResult.EGameRuntimeVersionMismatch),
                ex);
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameRuntime));
        }
    }
}
