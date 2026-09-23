using System;
using System.Runtime.InteropServices;
using GDK.Net.Streaming;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunks for <c>XGameStreamingConnectionStateChangedCallback</c> and
/// <c>XGameStreamingClientPropertiesChangedCallback</c>. Both are lifetime callbacks (the native
/// side holds the pointer until <c>XGameStreaming*Unregister</c> returns), so on
/// netstandard2.0 the delegates are rooted in static fields for the process lifetime.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr StreamingConnectionStateChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ulong, XGameStreamingConnectionState, void>)
            &OnConnectionStateChanged;

    internal static IntPtr StreamingClientPropertiesChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ulong, uint, XGameStreamingClientProperty*, void>)
            &OnClientPropertiesChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnConnectionStateChanged(
        IntPtr context,
        ulong client,
        XGameStreamingConnectionState state)
    {
        try
        {
            StreamingManager.DispatchConnectionStateChanged(context, client, state);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnClientPropertiesChanged(
        IntPtr context,
        ulong client,
        uint updatedPropertiesCount,
        XGameStreamingClientProperty* updatedProperties)
    {
        try
        {
            StreamingManager.DispatchClientPropertiesChanged(
                context, client, updatedPropertiesCount, updatedProperties);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ConnectionStateChangedCallbackDelegate(
        IntPtr context,
        ulong client,
        XGameStreamingConnectionState state);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ClientPropertiesChangedCallbackDelegate(
        IntPtr context,
        ulong client,
        uint updatedPropertiesCount,
        XGameStreamingClientProperty* updatedProperties);

    // Rooted for the process lifetime: the native side keeps these function pointers indefinitely.
    private static readonly ConnectionStateChangedCallbackDelegate ConnectionStateChangedKeepAlive =
        OnConnectionStateChanged;
    private static readonly ClientPropertiesChangedCallbackDelegate ClientPropertiesChangedKeepAlive =
        OnClientPropertiesChanged;

    internal static IntPtr StreamingConnectionStateChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ConnectionStateChangedKeepAlive);

    internal static IntPtr StreamingClientPropertiesChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ClientPropertiesChangedKeepAlive);

    private static void OnConnectionStateChanged(
        IntPtr context,
        ulong client,
        XGameStreamingConnectionState state)
    {
        try
        {
            StreamingManager.DispatchConnectionStateChanged(context, client, state);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static unsafe void OnClientPropertiesChanged(
        IntPtr context,
        ulong client,
        uint updatedPropertiesCount,
        XGameStreamingClientProperty* updatedProperties)
    {
        try
        {
            StreamingManager.DispatchClientPropertiesChanged(
                context, client, updatedPropertiesCount, updatedProperties);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
