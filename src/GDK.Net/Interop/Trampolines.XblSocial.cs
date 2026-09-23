// Native-to-managed callback thunks for the XSAPI social service --
// XblSocialAddSocialRelationshipChangedHandler from xsapi-c\social_c.h.
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

    internal static IntPtr SocialRelationshipChangedHandler { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, void>)&OnSocialRelationshipChanged;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnSocialRelationshipChanged(IntPtr eventArgs, IntPtr context)
    {
        try
        {
            SocialRelationshipRegistry.Dispatch(eventArgs, context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void SocialRelationshipChangedHandlerDelegate(IntPtr eventArgs, IntPtr context);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly SocialRelationshipChangedHandlerDelegate SocialRelationshipChangedKeepAlive =
        OnSocialRelationshipChanged;

    internal static IntPtr SocialRelationshipChangedHandler { get; } =
        Marshal.GetFunctionPointerForDelegate(SocialRelationshipChangedKeepAlive);

    private static void OnSocialRelationshipChanged(IntPtr eventArgs, IntPtr context)
    {
        try
        {
            SocialRelationshipRegistry.Dispatch(eventArgs, context);
        }
        catch
        {
            // Never let a managed exception cross back into XSAPI.
        }
    }

#endif
}
