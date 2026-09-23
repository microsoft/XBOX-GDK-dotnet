using System;
using System.Runtime.InteropServices;
using GDK.Net.Networking;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunks for the XNetworking callbacks.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr NetworkingUdpPortChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ushort, void>)&OnUdpPortChanged;

    internal static IntPtr NetworkingConnectivityHintChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XNetworkingConnectivityHint*, void>)&OnConnectivityHintChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnUdpPortChanged(IntPtr context, ushort port)
    {
        try
        {
            NetworkingManager.DispatchUdpPortChanged(context, port);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnConnectivityHintChanged(IntPtr context, XNetworkingConnectivityHint* hint)
    {
        try
        {
            NetworkingManager.DispatchConnectivityHintChanged(context, hint);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void UdpPortChangedCallbackDelegate(IntPtr context, ushort port);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ConnectivityHintChangedCallbackDelegate(
        IntPtr context,
        XNetworkingConnectivityHint* hint);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly UdpPortChangedCallbackDelegate UdpPortChangedCallbackKeepAlive = OnUdpPortChanged;
    private static readonly ConnectivityHintChangedCallbackDelegate ConnectivityHintChangedCallbackKeepAlive = OnConnectivityHintChanged;

    internal static IntPtr NetworkingUdpPortChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(UdpPortChangedCallbackKeepAlive);

    internal static IntPtr NetworkingConnectivityHintChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ConnectivityHintChangedCallbackKeepAlive);

    private static void OnUdpPortChanged(IntPtr context, ushort port)
    {
        try
        {
            NetworkingManager.DispatchUdpPortChanged(context, port);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnConnectivityHintChanged(IntPtr context, XNetworkingConnectivityHint* hint)
    {
        try
        {
            NetworkingManager.DispatchConnectivityHintChanged(context, hint);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
