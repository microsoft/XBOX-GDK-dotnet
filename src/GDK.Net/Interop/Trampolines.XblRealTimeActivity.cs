// Native-to-managed callback thunks for the XSAPI real-time activity service.
//
// Same rules as Trampolines.cs: net8.0/net10.0 uses [UnmanagedCallersOnly] function pointers so
// nothing is allocated or has to stay rooted; netstandard2.0 keeps the equivalent delegates in
// static readonly fields for the process lifetime. Exceptions never unwind into native code.
//
// XSAPI notification handlers take no task queue, so callbacks arrive on an XSAPI-internal thread.

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

    internal static IntPtr RealTimeActivityConnectionStateChangeHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XblRealTimeActivityConnectionState, void>)
            &OnRealTimeActivityConnectionStateChanged;

    internal static IntPtr RealTimeActivityResyncHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, void>)&OnRealTimeActivityResync;


    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnRealTimeActivityConnectionStateChanged(
        IntPtr context,
        XblRealTimeActivityConnectionState connectionState)
    {
        try
        {
            RealTimeActivityRegistry.DispatchConnectionStateChanged(context, connectionState);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnRealTimeActivityResync(IntPtr context)
    {
        try
        {
            RealTimeActivityRegistry.DispatchResyncRequired(context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }


#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void RealTimeActivityConnectionStateChangeHandlerDelegate(
        IntPtr context,
        XblRealTimeActivityConnectionState connectionState);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void RealTimeActivityResyncHandlerDelegate(IntPtr context);


    // Rooted for the process lifetime: XSAPI keeps the function pointers indefinitely.
    private static readonly RealTimeActivityConnectionStateChangeHandlerDelegate
        RealTimeActivityConnectionStateChangeKeepAlive = OnRealTimeActivityConnectionStateChanged;

    private static readonly RealTimeActivityResyncHandlerDelegate
        RealTimeActivityResyncKeepAlive = OnRealTimeActivityResync;


    internal static IntPtr RealTimeActivityConnectionStateChangeHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(RealTimeActivityConnectionStateChangeKeepAlive);

    internal static IntPtr RealTimeActivityResyncHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(RealTimeActivityResyncKeepAlive);


    private static void OnRealTimeActivityConnectionStateChanged(
        IntPtr context,
        XblRealTimeActivityConnectionState connectionState)
    {
        try
        {
            RealTimeActivityRegistry.DispatchConnectionStateChanged(context, connectionState);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

    private static void OnRealTimeActivityResync(IntPtr context)
    {
        try
        {
            RealTimeActivityRegistry.DispatchResyncRequired(context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }


#endif
}
