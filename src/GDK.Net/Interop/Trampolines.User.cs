// Native-to-managed thunks for the XUser callback surface.
//
// UserManager registers three callbacks: the user-change event, device-association changes and
// default-audio-endpoint changes. All follow the dual-shim pattern documented on Trampolines.cs.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Users;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Thunks for <c>XUserChangeEventCallback</c>, <c>XUserDeviceAssociationChangedCallback</c> and
/// <c>XUserDefaultAudioEndpointUtf16ChangedCallback</c>.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr UserChangeEventCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XUserLocalId, XUserChangeEvent, void>)&OnUserChanged;

    internal static IntPtr DeviceAssociationChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XUserDeviceAssociationChange*, void>)&OnDeviceAssociationChanged;

    internal static IntPtr DefaultAudioEndpointChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XUserLocalId, XUserDefaultAudioEndpointKind, char*, void>)&OnDefaultAudioEndpointChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnUserChanged(IntPtr context, XUserLocalId userLocalId, XUserChangeEvent change)
    {
        try
        {
            UserManager.DispatchUserChanged(context, userLocalId.Value, (UserChangeEvent)change);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnDeviceAssociationChanged(IntPtr context, XUserDeviceAssociationChange* change)
    {
        try
        {
            UserManager.DispatchDeviceAssociationChanged(context, change);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnDefaultAudioEndpointChanged(
        IntPtr context,
        XUserLocalId user,
        XUserDefaultAudioEndpointKind kind,
        char* endpointId)
    {
        try
        {
            UserManager.DispatchDefaultAudioEndpointChanged(context, user, kind, endpointId);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void UserChangeEventCallbackDelegate(
        IntPtr context,
        XUserLocalId userLocalId,
        XUserChangeEvent change);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void DeviceAssociationChangedDelegate(
        IntPtr context,
        XUserDeviceAssociationChange* change);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void DefaultAudioEndpointChangedDelegate(
        IntPtr context,
        XUserLocalId user,
        XUserDefaultAudioEndpointKind kind,
        char* endpointId);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly UserChangeEventCallbackDelegate UserChangeEventCallbackKeepAlive = OnUserChanged;

    private static readonly DeviceAssociationChangedDelegate DeviceAssociationChangedKeepAlive =
        OnDeviceAssociationChanged;

    private static readonly DefaultAudioEndpointChangedDelegate DefaultAudioEndpointChangedKeepAlive =
        OnDefaultAudioEndpointChanged;

    internal static IntPtr UserChangeEventCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(UserChangeEventCallbackKeepAlive);

    internal static IntPtr DeviceAssociationChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(DeviceAssociationChangedKeepAlive);

    internal static IntPtr DefaultAudioEndpointChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(DefaultAudioEndpointChangedKeepAlive);

    private static void OnUserChanged(IntPtr context, XUserLocalId userLocalId, XUserChangeEvent change)
    {
        try
        {
            UserManager.DispatchUserChanged(context, userLocalId.Value, (UserChangeEvent)change);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnDeviceAssociationChanged(IntPtr context, XUserDeviceAssociationChange* change)
    {
        try
        {
            UserManager.DispatchDeviceAssociationChanged(context, change);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnDefaultAudioEndpointChanged(
        IntPtr context,
        XUserLocalId user,
        XUserDefaultAudioEndpointKind kind,
        char* endpointId)
    {
        try
        {
            UserManager.DispatchDefaultAudioEndpointChanged(context, user, kind, endpointId);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
