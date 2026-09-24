// StoreLicense: SafeHandle-backed package/durable licence with PackageLicenseLost event.

using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Store;

/// <summary>
/// Owns an <c>XStoreLicenseHandle</c>; released with <c>XStoreCloseLicenseHandle</c>.
/// </summary>
internal sealed class StoreLicenseHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal StoreLicenseHandle()
        : base(ownsHandle: true) { }

    internal StoreLicenseHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XStoreCloseLicenseHandle(handle);
        return true;
    }
}

/// <summary>
/// A package or durable licence, produced by
/// <see cref="StoreContext.AcquireLicenseForPackageAsync"/> or
/// <see cref="StoreContext.AcquireLicenseForDurablesAsync"/>.
/// </summary>
/// <remarks>
/// <para>
/// The licence handle is owned by a <see cref="SafeHandle"/>; disposal releases
/// <c>XStoreCloseLicenseHandle</c>. Any in-flight <see cref="PackageLicenseLost"/> callback is
/// waited for before the handle is released.
/// </para>
/// <para>
/// Subscribe to <see cref="PackageLicenseLost"/> to be notified when the licence is revoked (for
/// example, when the user signs out or the trial expires). The handler is delivered on the task
/// queue supplied to <see cref="StoreContext.Create"/> or
/// <see cref="StoreContext.CreateForUser"/>.
/// </para>
/// </remarks>
public sealed unsafe class StoreLicense : IDisposable
{
    // Maps GCHandle.ToIntPtr → StoreLicense so the static trampoline can locate the instance.
    private static readonly ConcurrentDictionary<IntPtr, StoreLicense> Registrations = new();

    private readonly StoreLicenseHandle _handle;
    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private EventHandler? _licenseLost;
    private XTaskQueueRegistrationToken _token;
    private GCHandle _self;
    private bool _registered;
    private bool _disposed;

    internal StoreLicense(StoreLicenseHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;
    }

    /// <summary>
    /// <see langword="true"/> when the licence is currently valid
    /// (<c>XStoreIsLicenseValid</c>).
    /// </summary>
    /// <remarks>Requires the Gaming Runtime; throws <see cref="GameRuntimeException"/> if not initialized.</remarks>
    public bool IsValid
    {
        get
        {
            ThrowIfDisposed();
            return Native.XStoreIsLicenseValid(_handle.DangerousGetHandle()) != 0;
        }
    }

    /// <summary>
    /// Raised when the licence is revoked (<c>XStoreRegisterPackageLicenseLost</c>).
    /// </summary>
    /// <remarks>
    /// The registration is lazily created on the first subscription and released, with
    /// <c>wait: true</c>, on <see cref="Dispose"/>.
    /// </remarks>
    public event EventHandler? PackageLicenseLost
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _licenseLost += value;
                EnsureRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _licenseLost -= value;
            }
        }
    }

    /// <summary>Entry point called by the native trampoline.</summary>
    internal static void DispatchLicenseLost(IntPtr context)
    {
        if (!Registrations.TryGetValue(context, out StoreLicense? license))
        {
            return;
        }

        license.RaiseLicenseLost();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _licenseLost = null;

            if (_registered)
            {
                _registered = false;
                Native.XStoreUnregisterPackageLicenseLost(
                    _handle.DangerousGetHandle(), _token, wait: 1);
                _token = default;
            }

            if (_self.IsAllocated)
            {
                Registrations.TryRemove(GCHandle.ToIntPtr(_self), out _);
                _self.Free();
            }
        }

        _handle.Dispose();
    }

    private void EnsureRegistered()
    {
        if (_registered)
        {
            return;
        }

        _self = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_self);
        Registrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XStoreRegisterPackageLicenseLost(
            _handle.DangerousGetHandle(),
            _queue.RawHandle(),
            context,
            StoreLicenseCallbacks.PackageLicenseLostCallback,
            &token);

        if (HResult.Failed(hr))
        {
            Registrations.TryRemove(context, out _);
            _self.Free();
            Hr.ThrowIfFailed(hr);
        }

        _token = token;
        _registered = true;
    }

    private void RaiseLicenseLost()
    {
        EventHandler? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _licenseLost;
        }

        handler?.Invoke(this, EventArgs.Empty);
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(StoreLicense));
        }
    }
}

/// <summary>
/// Static trampolines for <c>XStorePackageLicenseLostCallback</c>.
/// </summary>
internal static unsafe class StoreLicenseCallbacks
{
#if NET5_0_OR_GREATER

    internal static IntPtr PackageLicenseLostCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, void>)&OnPackageLicenseLost;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnPackageLicenseLost(IntPtr context)
    {
        try
        {
            StoreLicense.DispatchLicenseLost(context);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void PackageLicenseLostDelegate(IntPtr context);

    private static readonly PackageLicenseLostDelegate KeepAlive = OnPackageLicenseLost;

    internal static IntPtr PackageLicenseLostCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(KeepAlive);

    private static void OnPackageLicenseLost(IntPtr context)
    {
        try
        {
            StoreLicense.DispatchLicenseLost(context);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
