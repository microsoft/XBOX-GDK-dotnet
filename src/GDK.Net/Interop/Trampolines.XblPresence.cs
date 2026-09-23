// Native-to-managed callback thunks for the XSAPI presence service -- the
// XblPresenceAddDevicePresenceChangedHandler and XblPresenceAddTitlePresenceChangedHandler
// callbacks from xsapi-c\presence_c.h.
//
// XSAPI invokes these on an internal real-time-activity thread, not on the projection task queue.

using System;
using System.Runtime.InteropServices;
using GDK.Net.XboxLive;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr PresenceDevicePresenceChangedHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ulong, XblPresenceDeviceType, byte, void>)&OnPresenceDevicePresenceChanged;

    internal static IntPtr PresenceTitlePresenceChangedHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ulong, uint, XblPresenceTitleState, void>)&OnPresenceTitlePresenceChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnPresenceDevicePresenceChanged(
        IntPtr context,
        ulong xuid,
        XblPresenceDeviceType deviceType,
        byte isUserLoggedOnDevice)
    {
        try
        {
            PresenceChangeRegistry.DispatchDevice(context, xuid, deviceType, isUserLoggedOnDevice != 0);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnPresenceTitlePresenceChanged(
        IntPtr context,
        ulong xuid,
        uint titleId,
        XblPresenceTitleState titleState)
    {
        try
        {
            PresenceChangeRegistry.DispatchTitle(context, xuid, titleId, titleState);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void PresenceDevicePresenceChangedHandlerDelegate(
        IntPtr context,
        ulong xuid,
        XblPresenceDeviceType deviceType,
        byte isUserLoggedOnDevice);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void PresenceTitlePresenceChangedHandlerDelegate(
        IntPtr context,
        ulong xuid,
        uint titleId,
        XblPresenceTitleState titleState);

    private static readonly PresenceDevicePresenceChangedHandlerDelegate PresenceDevicePresenceChangedKeepAlive =
        OnPresenceDevicePresenceChanged;

    private static readonly PresenceTitlePresenceChangedHandlerDelegate PresenceTitlePresenceChangedKeepAlive =
        OnPresenceTitlePresenceChanged;

    internal static IntPtr PresenceDevicePresenceChangedHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(PresenceDevicePresenceChangedKeepAlive);

    internal static IntPtr PresenceTitlePresenceChangedHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(PresenceTitlePresenceChangedKeepAlive);

    private static void OnPresenceDevicePresenceChanged(
        IntPtr context,
        ulong xuid,
        XblPresenceDeviceType deviceType,
        byte isUserLoggedOnDevice)
    {
        try
        {
            PresenceChangeRegistry.DispatchDevice(context, xuid, deviceType, isUserLoggedOnDevice != 0);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

    private static void OnPresenceTitlePresenceChanged(
        IntPtr context,
        ulong xuid,
        uint titleId,
        XblPresenceTitleState titleState)
    {
        try
        {
            PresenceChangeRegistry.DispatchTitle(context, xuid, titleId, titleState);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#endif
}
