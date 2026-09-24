using System;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.SystemInfo;

/// <summary>
/// Device and runtime identity for the current GDK title. All members call directly into the
/// Gaming Runtime; initialize the runtime with <see cref="GameRuntime.Initialize()"/> before use.
/// </summary>
public static unsafe class GameSystem
{
    // ─── XSystem: analytics / identity ───────────────────────────────────────────

    /// <summary>
    /// Returns analytics information for the current device (<c>XSystemGetAnalyticsInfo</c>).
    /// </summary>
    /// <remarks>Does not require a packaged title.</remarks>
    public static SystemAnalyticsInfo AnalyticsInfo
    {
        get
        {
            XSystemAnalyticsInfo native = Native.XSystemGetAnalyticsInfo();
            return UnpackAnalyticsInfo(ref native);
        }
    }

    /// <summary>
    /// Returns the console id string, which is unique per device
    /// (<c>XSystemGetConsoleId</c>).
    /// </summary>
    /// <remarks>Requires a packaged title and an active internet connection.</remarks>
    public static string ConsoleId => GetFixedString(
        XSystemConstants.ConsoleIdBytes,
        (byte* buf, nuint size, nuint* used) => Native.XSystemGetConsoleId(size, buf, used));

    /// <summary>
    /// Returns the Xbox Live sandbox id for the current device
    /// (<c>XSystemGetXboxLiveSandboxId</c>).
    /// </summary>
    public static string XboxLiveSandboxId => GetFixedString(
        XSystemConstants.XboxLiveSandboxIdMaxBytes,
        (byte* buf, nuint size, nuint* used) => Native.XSystemGetXboxLiveSandboxId(size, buf, used));

    /// <summary>
    /// Returns the device type of the current machine (<c>XSystemGetDeviceType</c>).
    /// </summary>
    public static SystemDeviceType DeviceType => (SystemDeviceType)Native.XSystemGetDeviceType();

    /// <summary>
    /// Returns the runtime and available GDK version information (<c>XSystemGetRuntimeInfo</c>).
    /// </summary>
    public static SystemRuntimeInfo RuntimeInfo
    {
        get
        {
            XSystemRuntimeInfo native = Native.XSystemGetRuntimeInfo();
            return new SystemRuntimeInfo(
                new SystemVersion(native.RuntimeVersion),
                new SystemVersion(native.AvailableVersion));
        }
    }

    /// <summary>
    /// Returns an app-specific device id that is stable per (app, device) pair
    /// (<c>XSystemGetAppSpecificDeviceId</c>).
    /// </summary>
    /// <remarks>Requires a packaged title.</remarks>
    public static string GetAppSpecificDeviceId() => GetFixedString(
        XSystemConstants.AppSpecificDeviceIdBytes,
        (byte* buf, nuint size, nuint* used) => Native.XSystemGetAppSpecificDeviceId(size, buf, used));

    /// <summary>
    /// Requests that the title be allowed to use the full available download bandwidth
    /// (<c>XSystemAllowFullDownloadBandwidth</c>). Pass <see langword="false"/> to return to the
    /// throttled default.
    /// </summary>
    /// <remarks>
    /// Use this only while the title is genuinely blocked on a download, for example on a loading
    /// or install-progress screen. Leaving it enabled during gameplay starves other traffic.
    /// </remarks>
    /// <exception cref="GameRuntimeException">The runtime rejected the request.</exception>
    public static void AllowFullDownloadBandwidth(bool allow)
    {
        Hr.ThrowIfFailed(Native.XSystemAllowFullDownloadBandwidth(allow ? (byte)1 : (byte)0));
    }

    // ─── XSystem: handle validity / tracking ─────────────────────────────────────
    /// <summary>
    /// Returns <see langword="true"/> when <paramref name="handle"/> is a live GDK handle
    /// (<c>XSystemIsHandleValid</c>).
    /// </summary>
    public static bool IsHandleValid(IntPtr handle) => Native.XSystemIsHandleValid(handle) != 0;

    /// <summary>
    /// Registers a process-wide callback that fires whenever any GDK handle is created or
    /// destroyed (<c>XSystemHandleTrack</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// The GDK provides no unregister function for this callback, so the first non-null call
    /// registers the native trampoline permanently; subsequent calls update only the managed
    /// target. Passing <see langword="null"/> stops managed dispatch but the native trampoline
    /// remains registered.
    /// </para>
    /// <para>
    /// Only available during development; the call is a no-op in retail packages.
    /// Requires the Gaming Runtime to be initialized.
    /// </para>
    /// </remarks>
    public static void SetHandleTrackingCallback(Action<IntPtr, SystemHandleType, SystemHandleCallbackReason>? callback)
    {
        lock (HandleTrackGate)
        {
            HandleTrackCallback = callback;

            if (callback != null && !HandleTrackRegistered)
            {
                int hr = Native.XSystemHandleTrack(HandleTrackFunctionPointer, IntPtr.Zero);
                Hr.ThrowIfFailed(hr);
                HandleTrackRegistered = true;
            }
        }
    }

    // ─── Trampolines ─────────────────────────────────────────────────────────────

    private static Action<IntPtr, SystemHandleType, SystemHandleCallbackReason>? HandleTrackCallback;
    private static bool HandleTrackRegistered;
    private static readonly object HandleTrackGate = new object();

#if NET5_0_OR_GREATER
    private static IntPtr HandleTrackFunctionPointer { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XSystemHandleType, XSystemHandleCallbackReason, IntPtr, void>)
            &OnNativeHandleEvent;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnNativeHandleEvent(
        IntPtr handle,
        XSystemHandleType type,
        XSystemHandleCallbackReason reason,
        IntPtr context)
    {
        try
        {
            HandleTrackCallback?.Invoke(handle, (SystemHandleType)type, (SystemHandleCallbackReason)reason);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }
#else
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void NativeHandleCallbackDelegate(
        IntPtr handle,
        XSystemHandleType type,
        XSystemHandleCallbackReason reason,
        IntPtr context);

    private static readonly NativeHandleCallbackDelegate HandleTrackDelegateKeepAlive = OnNativeHandleEvent;

    private static IntPtr HandleTrackFunctionPointer { get; } =
        Marshal.GetFunctionPointerForDelegate(HandleTrackDelegateKeepAlive);

    private static void OnNativeHandleEvent(
        IntPtr handle,
        XSystemHandleType type,
        XSystemHandleCallbackReason reason,
        IntPtr context)
    {
        try
        {
            HandleTrackCallback?.Invoke(handle, (SystemHandleType)type, (SystemHandleCallbackReason)reason);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }
#endif

    // ─── Helpers ─────────────────────────────────────────────────────────────────

    private delegate int FixedStringGetter(byte* buffer, nuint size, nuint* used);

    private static string GetFixedString(nuint maxBytes, FixedStringGetter getter)
    {
        byte[] buffer = new byte[(int)maxBytes];
        nuint used;
        int hr;

        fixed (byte* pBuffer = buffer)
        {
            hr = getter(pBuffer, maxBytes, &used);
        }

        Hr.ThrowIfFailed(hr);

        // `used` counts the null terminator; subtract it.
        int length = used > 0 ? (int)used - 1 : 0;
        return length <= 0 ? string.Empty : Encoding.UTF8.GetString(buffer, 0, length);
    }

    private static SystemAnalyticsInfo UnpackAnalyticsInfo(ref XSystemAnalyticsInfo native)
    {
        string family, form;

        fixed (XSystemAnalyticsInfo* pNative = &native)
        {
            // Family[64] is at offset 16 inside the 144-byte struct.
            byte* familyPtr = (byte*)pNative + 16;
            int familyLen = 0;
            while (familyLen < 63 && familyPtr[familyLen] != 0)
            {
                familyLen++;
            }

            family = familyLen > 0
                ? Encoding.UTF8.GetString(familyPtr, familyLen)
                : string.Empty;

            // Form[64] is at offset 80.
            byte* formPtr = (byte*)pNative + 80;
            int formLen = 0;
            while (formLen < 63 && formPtr[formLen] != 0)
            {
                formLen++;
            }

            form = formLen > 0
                ? Encoding.UTF8.GetString(formPtr, formLen)
                : string.Empty;
        }

        return new SystemAnalyticsInfo(
            new SystemVersion(native.OsVersion),
            new SystemVersion(native.HostingOsVersion),
            family,
            form);
    }
}
