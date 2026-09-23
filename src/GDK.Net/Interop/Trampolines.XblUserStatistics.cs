// Native-to-managed callback thunk for the XSAPI user statistics service -- the
// XblUserStatisticsAddStatisticChangedHandler callback from xsapi-c\user_statistics_c.h.
//
// XSAPI invokes this on an internal real-time-activity thread, not on the projection task queue.

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

    internal static IntPtr UserStatisticChangedHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<XblStatisticChangeEventArgs, IntPtr, void>)&OnUserStatisticChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnUserStatisticChanged(XblStatisticChangeEventArgs eventArgs, IntPtr context)
    {
        try
        {
            UserStatisticChangeRegistry.Dispatch(eventArgs, context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void UserStatisticChangedHandlerDelegate(XblStatisticChangeEventArgs eventArgs, IntPtr context);

    private static readonly UserStatisticChangedHandlerDelegate UserStatisticChangedKeepAlive =
        OnUserStatisticChanged;

    internal static IntPtr UserStatisticChangedHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(UserStatisticChangedKeepAlive);

    private static void OnUserStatisticChanged(XblStatisticChangeEventArgs eventArgs, IntPtr context)
    {
        try
        {
            UserStatisticChangeRegistry.Dispatch(eventArgs, context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#endif
}
