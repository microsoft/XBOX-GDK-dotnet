using System;
using System.Runtime.InteropServices;
using GDK.Net.Accessibility;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunk for <c>XSpeechSynthesizerInstalledVoicesCallback</c>.
/// This is a synchronous one-shot enumeration callback (fires during the call to
/// <c>XSpeechSynthesizerEnumerateInstalledVoices</c>), but it is still modelled with a
/// rooted-delegate fallback on netstandard2.0 because the delegate object must stay alive
/// for the full duration of the synchronous call stack.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr InstalledVoicesCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<XSpeechSynthesizerVoiceInformation*, IntPtr, byte>)
            &OnInstalledVoice;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnInstalledVoice(
        XSpeechSynthesizerVoiceInformation* info,
        IntPtr context)
    {
        try
        {
            SpeechSynthesizer.AddVoiceToList(info, context);
            return 1; // continue enumeration
        }
        catch
        {
            return 0; // stop enumeration on error
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate byte InstalledVoicesCallbackDelegate(
        XSpeechSynthesizerVoiceInformation* info,
        IntPtr context);

    // Rooted for the process lifetime.
    private static readonly InstalledVoicesCallbackDelegate InstalledVoicesKeepAlive =
        OnInstalledVoice;

    internal static IntPtr InstalledVoicesCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(InstalledVoicesKeepAlive);

    private static unsafe byte OnInstalledVoice(
        XSpeechSynthesizerVoiceInformation* info,
        IntPtr context)
    {
        try
        {
            SpeechSynthesizer.AddVoiceToList(info, context);
            return 1;
        }
        catch
        {
            return 0;
        }
    }

#endif
}
