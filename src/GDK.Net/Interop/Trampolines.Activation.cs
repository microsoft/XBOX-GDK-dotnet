using System;
using System.Runtime.InteropServices;
using GDK.Net.Activation;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunk for the unified activation callback
/// (<c>XGameActivationRegisterForEvent</c>).
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr ActivationInfoCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, XGameActivationInfo*, void>)&OnActivationInfo;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnActivationInfo(IntPtr context, XGameActivationInfo* info)
    {
        try
        {
            GameActivationManager.DispatchActivationInfo(context, info);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ActivationInfoCallbackDelegate(IntPtr context, XGameActivationInfo* info);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly ActivationInfoCallbackDelegate ActivationInfoCallbackKeepAlive = OnActivationInfo;

    internal static IntPtr ActivationInfoCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ActivationInfoCallbackKeepAlive);

    private static void OnActivationInfo(IntPtr context, XGameActivationInfo* info)
    {
        try
        {
            GameActivationManager.DispatchActivationInfo(context, info);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
