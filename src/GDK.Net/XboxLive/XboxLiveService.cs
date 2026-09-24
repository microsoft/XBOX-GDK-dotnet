using System;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.XboxLive;

/// <summary>
/// Options for <see cref="XboxLiveService.Initialize(XboxLiveOptions)"/>. Mirrors
/// <c>XblInitArgs</c> as it is declared for a GDK title.
/// </summary>
public sealed class XboxLiveOptions
{
    /// <summary>
    /// The title's Service Configuration ID, from Partner Center's Game Setup page. Required, and
    /// <b>case sensitive</b>: paste it verbatim rather than normalizing it.
    /// </summary>
    public string Scid { get; set; } = string.Empty;
}

/// <summary>
/// Xbox Live Services (XSAPI). Reached through <see cref="GameRuntime.XboxLive"/>.
/// </summary>
/// <remarks>
/// <para>
/// XSAPI is a <b>second native module</b>: <c>Microsoft.Xbox.Services.C.Thunks.dll</c>, with a hard
/// dependency on <c>libHttpClient.dll</c>. Neither is installed system-wide, so both must be
/// redistributed in the package layout. Loading is lazy: a title that never calls
/// <see cref="Initialize(XboxLiveOptions)"/> never loads them, and a missing module surfaces as a
/// <see cref="GameRuntimeException"/> carrying <c>E_GAMERUNTIME_DLL_NOT_FOUND</c> rather than a raw
/// <see cref="DllNotFoundException"/>.
/// </para>
/// <para>
/// Initialization is synchronous and needs the title's SCID. <b>Teardown is not:</b>
/// <c>XblCleanupAsync</c> has no synchronous form, so <see cref="CleanupAsync"/> must be awaited
/// before the task queue it ran on is disposed. <see cref="GameRuntime.Dispose"/> cannot await, so
/// it falls back to a bounded synchronous wait; a title that cares about clean shutdown should
/// await <see cref="CleanupAsync"/> itself first.
/// </para>
/// <para>
/// XSAPI state is process-wide, so this type refuses a second
/// <see cref="Initialize(XboxLiveOptions)"/> while already initialized.
/// </para>
/// </remarks>
public sealed unsafe class XboxLiveService : IDisposable
{
    /// <summary>How long <see cref="Dispose"/> waits for <c>XblCleanupAsync</c> to finish.</summary>
    private static readonly TimeSpan DisposeCleanupTimeout = TimeSpan.FromSeconds(5);

    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private bool _initialized;
    private bool _cleanedUp;
    private bool _disposed;
    private string? _scid;

    internal XboxLiveService(GameTaskQueue? queue)
    {
        _queue = queue;
        SocialManager = new SocialManager(this);
        AchievementsManager = new AchievementsManager(this);
    }

    /// <summary>
    /// The social manager: a locally cached, event-driven view of the social graph, pumped by
    /// <see cref="XboxLive.SocialManager.DoWork"/> once per frame.
    /// </summary>
    /// <remarks>
    /// Process-global rather than per-context, which is why it hangs off the service instead of an
    /// <see cref="XboxLiveContext"/>: users are added to it individually. Prefer it over
    /// <see cref="XboxLiveContext.Social"/> when a title needs a live friends list rather than a
    /// one-off query.
    /// </remarks>
    public SocialManager SocialManager { get; }

    /// <summary>
    /// The achievements manager: a locally cached view of every added user's achievements, pumped
    /// by <see cref="XboxLive.AchievementsManager.DoWork"/> once per frame.
    /// </summary>
    /// <remarks>
    /// Process-global rather than per-context. Its queries read the warm cache and return
    /// synchronously, unlike the service calls on <see cref="XboxLiveContext.Achievements"/>, which
    /// go to the network every time.
    /// </remarks>
    public AchievementsManager AchievementsManager { get; }

    /// <summary><see langword="true"/> between a successful initialize and a completed cleanup.</summary>
    public bool IsInitialized
    {
        get
        {
            lock (_gate)
            {
                return _initialized && !_cleanedUp;
            }
        }
    }

    /// <summary>
    /// The Service Configuration ID XSAPI is running against (<c>XblGetScid</c>).
    /// </summary>
    /// <exception cref="InvalidOperationException">Xbox Live has not been initialized.</exception>
    public string Scid
    {
        get
        {
            ThrowIfNotInitialized();

            byte* scid;
            Hr.ThrowIfFailed(NativeXbl.XblGetScid(&scid));
            return Utf8.ToString(scid) ?? _scid ?? string.Empty;
        }
    }

    /// <summary>
    /// Initializes Xbox Live Services (<c>XblInitialize</c>). This is the call that first loads the
    /// XSAPI native modules.
    /// </summary>
    /// <param name="options">Initialization options; <see cref="XboxLiveOptions.Scid"/> is required.</param>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">The SCID is empty.</exception>
    /// <exception cref="InvalidOperationException">Xbox Live is already initialized.</exception>
    public void Initialize(XboxLiveOptions options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.Scid))
        {
            throw new ArgumentException(
                "A Service Configuration ID is required. Copy it verbatim from Partner Center; it is case sensitive.",
                nameof(options));
        }

        ThrowIfDisposed();

        lock (_gate)
        {
            if (_initialized && !_cleanedUp)
            {
                throw new InvalidOperationException("Xbox Live Services are already initialized.");
            }

            IntPtr scidPtr = Utf8.Allocate(options.Scid);
            try
            {
                var args = new XblInitArgs
                {
                    Queue = IntPtr.Zero,
                    Scid = scidPtr,
                };

                int hr;
                try
                {
                    hr = NativeXbl.XblInitialize(&args);
                }
                catch (DllNotFoundException ex)
                {
                    throw new GameRuntimeException(
                        HResult.EGameRuntimeDllNotFound,
                        "Xbox Live Services could not be loaded. Microsoft.Xbox.Services.C.Thunks.dll and "
                            + "libHttpClient.dll must both sit next to the executable; see eng/packaging/GdkRedist.targets.",
                        ex);
                }
                catch (EntryPointNotFoundException ex)
                {
                    throw new GameRuntimeException(
                        HResult.EGameRuntimeVersionMismatch,
                        HResult.Describe(HResult.EGameRuntimeVersionMismatch),
                        ex);
                }

                Hr.ThrowIfFailed(hr);
            }
            finally
            {
                // XblInitialize copies the SCID, so the buffer is only needed for the call itself.
                Utf8.Free(scidPtr);
            }

            _scid = options.Scid;
            _initialized = true;
            _cleanedUp = false;
        }
    }

    /// <summary>
    /// Creates an Xbox Live context for <paramref name="user"/> (<c>XblContextCreateHandle</c>).
    /// </summary>
    /// <remarks>
    /// The returned context is valid only while <paramref name="user"/> stays signed in. Dispose and
    /// recreate it when <see cref="UserManager.UserChanged"/> reports
    /// <see cref="UserChangeEvent.SignedInAgain"/>, <see cref="UserChangeEvent.SigningOut"/>
    /// or <see cref="UserChangeEvent.SignedOut"/>.
    /// </remarks>
    public XboxLiveContext CreateContext(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();

        IntPtr raw;
        int hr;
        try
        {
            hr = NativeXbl.XblContextCreateHandle(user.Handle, &raw);
        }
        catch (DllNotFoundException ex)
        {
            throw new GameRuntimeException(
                HResult.EGameRuntimeDllNotFound,
                HResult.Describe(HResult.EGameRuntimeDllNotFound),
                ex);
        }

        Hr.ThrowIfFailed(hr);
        return new XboxLiveContext(new XboxLiveContextHandle(raw), _queue);
    }

    /// <summary>
    /// Shuts Xbox Live Services down (<c>XblCleanupAsync</c>).
    /// </summary>
    /// <remarks>
    /// Every <see cref="XboxLiveContext"/> must be disposed first, and the returned task must be
    /// awaited before the task queue is torn down. Calling this when not initialized is a no-op, so
    /// it is safe in a <c>finally</c>.
    /// </remarks>
    public Task CleanupAsync(CancellationToken cancellationToken = default)
    {
        lock (_gate)
        {
            if (!_initialized || _cleanedUp)
            {
                return Task.CompletedTask;
            }

            // Marked cleaned up before the call completes: XSAPI rejects a concurrent cleanup, and a
            // failed one leaves the library in a state no further call can use either.
            _cleanedUp = true;
        }

        return AsyncOperation.RunAsync(
            _queue.RawHandle(),
            block => NativeXbl.XblCleanupAsync((XAsyncBlock*)block),
            static block => HResult.SOk,
            cancellationToken);
    }

    /// <summary>
    /// Suppresses XSAPI's debug asserts for Xbox Live throttling while running in a development
    /// sandbox (<c>XblDisableAssertsForXboxLiveThrottlingInDevSandboxes</c>).
    /// </summary>
    /// <remarks>
    /// A development aid only. Throttling still happens; only the assert is silenced, and retail
    /// sandboxes are unaffected.
    /// </remarks>
    public void DisableThrottlingAssertsInDevSandboxes()
    {
        ThrowIfNotInitialized();
        NativeXbl.XblDisableAssertsForXboxLiveThrottlingInDevSandboxes(
            XblConfigSetting.ThisCodeNeedsToBeRemoved);
    }

    /// <summary>
    /// Overrides the locale XSAPI reports to the service (<c>XblSetOverrideLocale</c>), for example
    /// <c>"en-US"</c>.
    /// </summary>
    public void SetOverrideLocale(string locale)
    {
        if (locale is null)
        {
            throw new ArgumentNullException(nameof(locale));
        }

        ThrowIfNotInitialized();

        IntPtr buffer = Utf8.Allocate(locale);
        try
        {
            Hr.ThrowIfFailed(NativeXbl.XblSetOverrideLocale((byte*)buffer));
        }
        finally
        {
            Utf8.Free(buffer);
        }
    }

    /// <summary>
    /// Runs <c>XblCleanupAsync</c> and waits for it, because the runtime offers no synchronous
    /// teardown. Prefer awaiting <see cref="CleanupAsync"/> before disposal.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        bool needsCleanup;
        lock (_gate)
        {
            needsCleanup = _initialized && !_cleanedUp;
        }

        if (!needsCleanup)
        {
            return;
        }

        try
        {
            // A bounded wait, not an unbounded one: disposal must not hang a title's shutdown just
            // because the service is unreachable. Under the pumped model nothing is draining the
            // queue at this point, so the timeout is the expected path there.
            CleanupAsync().Wait(DisposeCleanupTimeout);
        }
        catch
        {
            // Dispose must not throw; a failed cleanup is already terminal for the library.
        }
    }

    private void ThrowIfNotInitialized()
    {
        ThrowIfDisposed();

        lock (_gate)
        {
            if (!_initialized || _cleanedUp)
            {
                throw new InvalidOperationException(
                    "Xbox Live Services are not initialized. Call GameRuntime.XboxLive.Initialize first.");
            }
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(XboxLiveService));
        }
    }
}
