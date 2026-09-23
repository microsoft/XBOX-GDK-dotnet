// Native-to-managed callback thunks for the XSAPI achievements service -- the
// XblAchievementsAddAchievementProgressChangeHandler callback from xsapi-c\achievements_c.h.
//
// Same rules as Trampolines.cs: net8.0/net10.0 uses [UnmanagedCallersOnly] function pointers so
// nothing is allocated or has to stay rooted; netstandard2.0 keeps the equivalent delegate in a
// static readonly field for the process lifetime. Exceptions never unwind into native code.
//
// One difference from the Gaming Runtime's callbacks: XSAPI notification handlers take no task
// queue, so they are invoked on an XSAPI-internal thread rather than on the projection's queue.

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

    internal static IntPtr AchievementProgressChangeHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, void>)&OnAchievementProgressChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnAchievementProgressChanged(IntPtr eventArgs, IntPtr context)
    {
        try
        {
            AchievementProgressRegistry.Dispatch(eventArgs, context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void AchievementProgressChangeHandlerDelegate(IntPtr eventArgs, IntPtr context);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly AchievementProgressChangeHandlerDelegate AchievementProgressChangeKeepAlive =
        OnAchievementProgressChanged;

    internal static IntPtr AchievementProgressChangeHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(AchievementProgressChangeKeepAlive);

    private static void OnAchievementProgressChanged(IntPtr eventArgs, IntPtr context)
    {
        try
        {
            AchievementProgressRegistry.Dispatch(eventArgs, context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#endif
}
