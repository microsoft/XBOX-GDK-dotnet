using System;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Package;

/// <summary>
/// Owns an <c>XPackageInstallationMonitorHandle</c>;
/// released with <c>XPackageCloseInstallationMonitorHandle</c>.
/// </summary>
internal sealed class PackageInstallationMonitorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PackageInstallationMonitorSafeHandle()
        : base(ownsHandle: true)
    {
    }

    internal PackageInstallationMonitorSafeHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XPackageCloseInstallationMonitorHandle(handle);
        return true;
    }
}

/// <summary>
/// Tracks the installation progress of one or more package chunks.
/// </summary>
/// <remarks>
/// <para>
/// Obtain an instance via <see cref="GamePackage.InstallChunksAsync"/>,
/// <see cref="GamePackage.InstallChunks"/>, or
/// <see cref="GamePackage.CreateInstallationMonitor"/>.
/// </para>
/// <para>
/// Subscribe to <see cref="ProgressChanged"/> to receive native callbacks when progress updates.
/// The registration token is released with <c>wait:true</c> on <see cref="Dispose"/>, so no
/// callback is in flight once the object is disposed.
/// </para>
/// </remarks>
public sealed unsafe class PackageInstallationMonitor : IDisposable
{
    private readonly PackageInstallationMonitorSafeHandle _handle;
    private readonly object _gate = new object();
    private EventHandler<PackageProgressChangedEventArgs>? _progressChanged;
    private XTaskQueueRegistrationToken _progressToken;
    private GCHandle _selfHandle;
    private bool _registered;
    private bool _disposed;

    internal PackageInstallationMonitor(IntPtr raw)
    {
        _handle = new PackageInstallationMonitorSafeHandle(raw);
    }

    // ---- public API -------------------------------------------------------------

    /// <summary>
    /// Creates a monitor for all chunks of the specified package without starting an install
    /// (<c>XPackageCreateInstallationMonitor</c>).
    /// </summary>
    /// <param name="packageIdentifier">The opaque package identifier.</param>
    /// <param name="selectors">
    /// Optional chunk selectors to narrow the monitor scope.
    /// Pass <see langword="null"/> or an empty array to monitor all chunks.
    /// </param>
    /// <param name="minimumUpdateIntervalMs">
    /// Minimum milliseconds between <see cref="ProgressChanged"/> callbacks; 0 for no throttling.
    /// </param>
    public static PackageInstallationMonitor Create(
        string packageIdentifier,
        PackageChunkSelector[]? selectors = null,
        uint minimumUpdateIntervalMs = 0)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        return GamePackage.CreateInstallationMonitor(packageIdentifier, selectors, minimumUpdateIntervalMs);
    }

    /// <summary>
    /// Reads a progress snapshot from the monitor's cached state
    /// (<c>XPackageGetInstallationProgress</c>).
    /// </summary>
    public PackageInstallationProgress GetProgress()
    {
        ThrowIfDisposed();
        XPackageInstallationProgress native;
        Native.XPackageGetInstallationProgress(_handle.DangerousGetHandle(), &native);
        return new PackageInstallationProgress(native);
    }

    /// <summary>
    /// Polls the runtime for updated progress and returns <see langword="true"/> when the
    /// installation is complete (<c>XPackageUpdateInstallationMonitor</c>).
    /// </summary>
    public bool Update()
    {
        ThrowIfDisposed();
        return Native.XPackageUpdateInstallationMonitor(_handle.DangerousGetHandle()) != 0;
    }

    /// <summary>
    /// Raised when installation progress changes
    /// (<c>XPackageRegisterInstallationProgressChanged</c>).
    /// </summary>
    /// <remarks>
    /// Registration is deferred until the first subscriber. Unregistration (with wait) happens on
    /// <see cref="Dispose"/>.
    /// </remarks>
    public event EventHandler<PackageProgressChangedEventArgs>? ProgressChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _progressChanged += value;
                EnsureProgressRegistered();
            }
        }
        remove
        {
            lock (_gate)
            {
                _progressChanged -= value;
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed) return;
            _disposed = true;
            _progressChanged = null;

            if (_registered)
            {
                _registered = false;
                Native.XPackageUnregisterInstallationProgressChanged(
                    _handle.DangerousGetHandle(), _progressToken, wait: 1);
                _progressToken = default;
            }

            if (_selfHandle.IsAllocated)
            {
                s_monitors.TryRemove(GCHandle.ToIntPtr(_selfHandle), out _);
                _selfHandle.Free();
            }
        }

        _handle.Dispose();
    }

    // ---- internal dispatch ------------------------------------------------------

    internal static void DispatchProgress(IntPtr context, IntPtr monitorHandle)
    {
        if (!s_monitors.TryGetValue(context, out PackageInstallationMonitor? monitor))
        {
            return;
        }
        monitor.RaiseProgressChanged(monitorHandle);
    }

    // ---- private ----------------------------------------------------------------

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, PackageInstallationMonitor>
        s_monitors = new System.Collections.Concurrent.ConcurrentDictionary<IntPtr, PackageInstallationMonitor>();

    private void EnsureProgressRegistered()
    {
        if (_registered) return;

        _selfHandle = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_selfHandle);
        s_monitors[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XPackageRegisterInstallationProgressChanged(
            _handle.DangerousGetHandle(),
            context,
            s_progressChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            s_monitors.TryRemove(context, out _);
            _selfHandle.Free();
            Hr.ThrowIfFailed(hr);
        }

        _progressToken = token;
        _registered = true;
    }

    private void RaiseProgressChanged(IntPtr monitorHandle)
    {
        EventHandler<PackageProgressChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed) return;
            handler = _progressChanged;
        }

        if (handler == null) return;

        XPackageInstallationProgress native;
        Native.XPackageGetInstallationProgress(monitorHandle, &native);
        handler.Invoke(this, new PackageProgressChangedEventArgs(new PackageInstallationProgress(native)));
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(PackageInstallationMonitor));
    }

    // ---- trampolines ------------------------------------------------------------

#if NET5_0_OR_GREATER

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnProgressChanged(IntPtr context, IntPtr monitorHandle)
    {
        try
        {
            DispatchProgress(context, monitorHandle);
        }
        catch { }
    }

    private static readonly IntPtr s_progressChangedCallback =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, void>)&OnProgressChanged;

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ProgressChangedDelegate(IntPtr context, IntPtr monitorHandle);

    private static void OnProgressChanged(IntPtr context, IntPtr monitorHandle)
    {
        try
        {
            DispatchProgress(context, monitorHandle);
        }
        catch { }
    }

    private static readonly ProgressChangedDelegate s_progressChangedDel = OnProgressChanged;
    private static readonly IntPtr s_progressChangedCallback =
        Marshal.GetFunctionPointerForDelegate(s_progressChangedDel);

#endif
}
