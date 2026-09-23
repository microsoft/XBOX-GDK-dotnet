// P/Invoke declarations for the XAccessibility and XSpeechSynthesizer families.
//
// All entry points below are verified present in xgameruntime.thunks.dll
// (Native.LibraryName) via the authoritative lib→DLL diff (xgameruntime.lib 404 symbols
// vs xgameruntime.thunks.dll 355 exports; 49 total gaps across all families).
//
// No APIs from XAccessibility.h or XSpeechSynthesizer.h appear in the gap list —
// every XClosedCaption*, XHighContrast*, XSpeechToText*, and XSpeechSynthesizer* symbol
// that is declared in the public headers is exported by the thunks DLL.
//
// These APIs are available after XGameRuntimeInitialize; no separate subsystem init is required.
//
// See Interop/Native.cs for the dual-shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XAccessibility.h: closed captions ---

    [LibraryImport(LibraryName)]
    internal static partial int XClosedCaptionGetProperties(XClosedCaptionProperties* properties);

    [LibraryImport(LibraryName)]
    internal static partial int XClosedCaptionSetEnabled(byte enabled);

    // --- XAccessibility.h: high contrast ---

    [LibraryImport(LibraryName)]
    internal static partial int XHighContrastGetMode(XHighContrastMode* mode);

    // --- XAccessibility.h: speech-to-text ---

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechToTextSetPositionHint(XSpeechToTextPositionHint position);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechToTextSendString(
        byte* speakerName,
        byte* content,
        XSpeechToTextType type);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechToTextBeginHypothesisString(
        byte* speakerName,
        byte* content,
        XSpeechToTextType type,
        uint* hypothesisId);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechToTextUpdateHypothesisString(
        uint hypothesisId,
        byte* content);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechToTextFinalizeHypothesisString(
        uint hypothesisId,
        byte* content);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechToTextCancelHypothesisString(uint hypothesisId);

    // --- XSpeechSynthesizer.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerEnumerateInstalledVoices(
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerCreate(IntPtr* speechSynthesizer);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerCloseHandle(IntPtr speechSynthesizer);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerSetDefaultVoice(IntPtr speechSynthesizer);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerSetCustomVoice(
        IntPtr speechSynthesizer,
        byte* voiceId);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerCreateStreamFromText(
        IntPtr speechSynthesizer,
        byte* text,
        IntPtr* speechSynthesisStream);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerCreateStreamFromSsml(
        IntPtr speechSynthesizer,
        byte* ssml,
        IntPtr* speechSynthesisStream);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerCloseStreamHandle(IntPtr speechSynthesisStream);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerGetStreamDataSize(
        IntPtr speechSynthesisStream,
        nuint* bufferSize);

    [LibraryImport(LibraryName)]
    internal static partial int XSpeechSynthesizerGetStreamData(
        IntPtr speechSynthesisStream,
        nuint bufferSize,
        byte* buffer,
        nuint* bufferUsed);
}

#else

internal static unsafe partial class Native
{
    // --- XAccessibility.h: closed captions ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XClosedCaptionGetProperties(XClosedCaptionProperties* properties);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XClosedCaptionSetEnabled(byte enabled);

    // --- XAccessibility.h: high contrast ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XHighContrastGetMode(XHighContrastMode* mode);

    // --- XAccessibility.h: speech-to-text ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechToTextSetPositionHint(XSpeechToTextPositionHint position);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechToTextSendString(
        byte* speakerName,
        byte* content,
        XSpeechToTextType type);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechToTextBeginHypothesisString(
        byte* speakerName,
        byte* content,
        XSpeechToTextType type,
        uint* hypothesisId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechToTextUpdateHypothesisString(
        uint hypothesisId,
        byte* content);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechToTextFinalizeHypothesisString(
        uint hypothesisId,
        byte* content);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechToTextCancelHypothesisString(uint hypothesisId);

    // --- XSpeechSynthesizer.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerEnumerateInstalledVoices(
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerCreate(IntPtr* speechSynthesizer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerCloseHandle(IntPtr speechSynthesizer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerSetDefaultVoice(IntPtr speechSynthesizer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerSetCustomVoice(
        IntPtr speechSynthesizer,
        byte* voiceId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerCreateStreamFromText(
        IntPtr speechSynthesizer,
        byte* text,
        IntPtr* speechSynthesisStream);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerCreateStreamFromSsml(
        IntPtr speechSynthesizer,
        byte* ssml,
        IntPtr* speechSynthesisStream);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerCloseStreamHandle(IntPtr speechSynthesisStream);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerGetStreamDataSize(
        IntPtr speechSynthesisStream,
        nuint* bufferSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSpeechSynthesizerGetStreamData(
        IntPtr speechSynthesisStream,
        nuint bufferSize,
        byte* buffer,
        nuint* bufferUsed);
}

#endif
