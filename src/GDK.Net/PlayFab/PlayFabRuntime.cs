using System;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab;

/// <summary>
/// Lifetime of the PlayFab libraries: <c>PFCore</c> (the authentication and HTTP layer) and
/// <c>PFServices</c> (the generated service APIs).
/// </summary>
/// <remarks>
/// <para>
/// <see cref="Initialize"/> must be called before any other PlayFab API and, per the GDK, after
/// <see cref="GameRuntime"/> is initialized. <see cref="UninitializeAsync"/> waits for the
/// library's background work to drain, so it must not be called from a completion callback.
/// </para>
/// <para>
/// A task queue is never surfaced: the projection passes <c>nullptr</c> so the PlayFab libraries
/// use their own background queue, matching the "never surface a task queue" rule in
/// eng/interop-conventions.md.
/// </para>
/// </remarks>
public static class PlayFabRuntime
{
    /// <summary>
    /// How long the automatic shutdown driven by <see cref="GameRuntime.Dispose"/> waits for
    /// PlayFab's background work to drain. Shared with the other native teardowns so the whole
    /// shutdown has one budget to reason about.
    /// </summary>
    private static readonly TimeSpan ShutdownTimeout = RuntimeLifetime.TeardownTimeout;

    private static int _initialized;

    /// <summary>Whether <see cref="Initialize"/> has completed without a matching uninitialize.</summary>
    public static bool IsInitialized => Volatile.Read(ref _initialized) != 0;

    /// <summary>
    /// Initializes PlayFab Core and Services (<c>PFInitialize</c>, <c>PFServicesInitialize</c>).
    /// </summary>
    /// <remarks>
    /// Second in the fixed startup order (see <see cref="SubsystemOrder"/>), so the Gaming Runtime
    /// must already be up: PlayFab's asynchronous work runs on the process default task queue,
    /// which the Gaming Runtime owns.
    /// </remarks>
    /// <exception cref="InvalidOperationException">The Gaming Runtime is not initialized.</exception>
    public static unsafe void Initialize()
    {
        RuntimeLifetime.RequireGameRuntime(nameof(NativePlayFab.PFInitialize));

        Hr.ThrowIfFailed(NativePlayFab.PFInitialize(IntPtr.Zero));

        int hr = NativePlayFab.PFServicesInitialize(IntPtr.Zero);
        if (HResult.Failed(hr))
        {
            // Leave nothing half-initialized: Core owns a background queue that would otherwise
            // outlive the failed call.
            _ = NativePlayFab.PFUninitializeAsync(null);
            throw Hr.ToException(hr);
        }

        Volatile.Write(ref _initialized, 1);
    }

    /// <summary>
    /// Shuts PlayFab down and waits for its background work to drain
    /// (<c>PFServicesUninitializeAsync</c>, <c>PFUninitializeAsync</c>).
    /// </summary>
    /// <remarks>
    /// Every <see cref="PlayFabEntity"/>, <see cref="PlayFabServiceConfig"/> and
    /// <see cref="PlayFabLocalUser"/> must be disposed first; PlayFab fails the call while handles
    /// are outstanding.
    /// </remarks>
    public static async Task UninitializeAsync(CancellationToken cancellationToken = default)
    {
        await AsyncOperation.RunAsync(
            IntPtr.Zero, StartServicesUninitialize, GetStatus, cancellationToken).ConfigureAwait(false);

        await AsyncOperation.RunAsync(
            IntPtr.Zero, StartCoreUninitialize, GetStatus, cancellationToken).ConfigureAwait(false);

        Volatile.Write(ref _initialized, 0);
    }

    /// <summary>
    /// Shuts PlayFab down if it is still initialized, for <see cref="GameRuntime.Dispose"/>.
    /// </summary>
    /// <remarks>
    /// PlayFab's shutdown runs on the process default task queue, so it has to complete before
    /// <c>XGameRuntimeUninitialize</c> tears that queue down: calling it afterwards faults inside
    /// <c>PFServicesUninitializeAsync</c>. Rather than make every title order the two by hand,
    /// <see cref="GameRuntime.Dispose"/> calls this first. A title that wants to observe the
    /// shutdown, or to bound it itself, calls <see cref="UninitializeAsync"/> beforehand; this then
    /// finds nothing to do.
    /// </remarks>
    internal static void ShutdownIfInitialized()
    {
        if (!IsInitialized)
        {
            return;
        }

        try
        {
            // A bounded wait, not an unbounded one: disposal must not hang a title's shutdown just
            // because the service is unreachable.
            UninitializeAsync().Wait(ShutdownTimeout);
        }
        catch
        {
            // Dispose must not throw; a failed shutdown is already terminal for the library.
        }
        finally
        {
            Volatile.Write(ref _initialized, 0);
        }
    }

    private static unsafe int StartServicesUninitialize(IntPtr block) =>
        NativePlayFab.PFServicesUninitializeAsync((XAsyncBlock*)block);

    private static unsafe int StartCoreUninitialize(IntPtr block) =>
        NativePlayFab.PFUninitializeAsync((XAsyncBlock*)block);

    private static unsafe int GetStatus(IntPtr block) =>
        Native.XAsyncGetStatus((XAsyncBlock*)block, 0);
}

/// <summary>
/// Process-wide HTTP behaviour for the PlayFab libraries (<c>PFHttpConfig.h</c>).
/// </summary>
public static unsafe class PlayFabHttpSettings
{
    /// <summary>Whether a failed request may be retried (<c>PFHttpRetrySettings.allowRetry</c>).</summary>
    public static bool AllowRetry
    {
        get => GetRetrySettings().AllowRetry != 0;
        set
        {
            PFHttpRetrySettings settings = GetRetrySettings();
            settings.AllowRetry = value ? (byte)1 : (byte)0;
            SetRetrySettings(settings);
        }
    }

    /// <summary>The shortest delay before a retry, in seconds.</summary>
    public static uint MinimumRetryDelayInSeconds
    {
        get => GetRetrySettings().MinimumRetryDelayInSeconds;
        set
        {
            PFHttpRetrySettings settings = GetRetrySettings();
            settings.MinimumRetryDelayInSeconds = value;
            SetRetrySettings(settings);
        }
    }

    /// <summary>How long a request may keep being retried, in seconds.</summary>
    public static uint TimeoutWindowInSeconds
    {
        get => GetRetrySettings().TimeoutWindowInSeconds;
        set
        {
            PFHttpRetrySettings settings = GetRetrySettings();
            settings.TimeoutWindowInSeconds = value;
            SetRetrySettings(settings);
        }
    }

    /// <summary>Whether request and response bodies are compressed (<c>PFHttpSettings</c>).</summary>
    public static bool RequestResponseCompression
    {
        get
        {
            PFHttpSettings settings;
            Hr.ThrowIfFailed(NativePlayFab.PFGetHttpSettings(&settings));
            return settings.RequestResponseCompression != 0;
        }

        set
        {
            PFHttpSettings settings;
            Hr.ThrowIfFailed(NativePlayFab.PFGetHttpSettings(&settings));
            settings.RequestResponseCompression = value ? (byte)1 : (byte)0;
            Hr.ThrowIfFailed(NativePlayFab.PFSetHttpSettings(&settings));
        }
    }

    private static PFHttpRetrySettings GetRetrySettings()
    {
        PFHttpRetrySettings settings;
        Hr.ThrowIfFailed(NativePlayFab.PFGetHttpRetrySettings(&settings));
        return settings;
    }

    private static void SetRetrySettings(PFHttpRetrySettings settings) =>
        Hr.ThrowIfFailed(NativePlayFab.PFSetHttpRetrySettings(&settings));
}
