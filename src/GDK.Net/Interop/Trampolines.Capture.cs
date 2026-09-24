// Callback thunks for XAppCapture.h event registrations.
//
// Both XAppBroadcastMonitorCallback and XAppCaptureMetadataPurgedCallback share the same
// signature: void(void* context), so a single thunk dispatches both. The context value
// identifies which AppCaptureManager instance fired.
//
// See Trampolines.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Capture;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunks for the XAppCapture / XAppBroadcast callbacks. Both
/// <c>XAppBroadcastMonitorCallback</c> and <c>XAppCaptureMetadataPurgedCallback</c> have the same
/// <c>void(void* context)</c> shape, so one thunk serves both registrations.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr AppCaptureContextCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, void>)&OnAppCaptureContext;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnAppCaptureContext(IntPtr context)
    {
        try
        {
            AppCaptureManager.DispatchContextCallback(context);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void AppCaptureContextCallbackDelegate(IntPtr context);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly AppCaptureContextCallbackDelegate AppCaptureContextCallbackKeepAlive = OnAppCaptureContext;

    internal static IntPtr AppCaptureContextCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(AppCaptureContextCallbackKeepAlive);

    private static void OnAppCaptureContext(IntPtr context)
    {
        try
        {
            AppCaptureManager.DispatchContextCallback(context);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
