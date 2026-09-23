// P/Invoke declarations for the XSystem, XThread, XError, XGame, XLauncher, and XDisplay
// families. All entry points are exported from xgameruntime.thunks.dll.
//
// Follow the dual-shim pattern from Native.cs: [LibraryImport] on NET7_0_OR_GREATER,
// [DllImport] on netstandard2.0. Both blocks must stay signature-identical.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XSystem.h ---

    [LibraryImport(LibraryName)]
    internal static partial XSystemAnalyticsInfo XSystemGetAnalyticsInfo();

    [LibraryImport(LibraryName)]
    internal static partial int XSystemGetConsoleId(
        nuint consoleIdSize,
        byte* consoleId,
        nuint* consoleIdUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XSystemGetXboxLiveSandboxId(
        nuint sandboxIdSize,
        byte* sandboxId,
        nuint* sandboxIdUsed);

    [LibraryImport(LibraryName)]
    internal static partial XSystemDeviceType XSystemGetDeviceType();

    [LibraryImport(LibraryName)]
    internal static partial int XSystemGetAppSpecificDeviceId(
        nuint appSpecificDeviceIdSize,
        byte* appSpecificDeviceId,
        nuint* appSpecificDeviceIdUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XSystemHandleTrack(IntPtr callback, IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial byte XSystemIsHandleValid(IntPtr handle);

    [LibraryImport(LibraryName)]
    internal static partial XSystemRuntimeInfo XSystemGetRuntimeInfo();

    // --- XThread.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XThreadSetTimeSensitive(byte isTimeSensitiveThread);

    [LibraryImport(LibraryName)]
    internal static partial byte XThreadIsTimeSensitive();

    [LibraryImport(LibraryName)]
    internal static partial void XThreadAssertNotTimeSensitive();

    // --- XError.h ---

    [LibraryImport(LibraryName)]
    internal static partial void XErrorSetCallback(IntPtr callback, IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial void XErrorSetOptions(
        XErrorOptions optionsDebuggerPresent,
        XErrorOptions optionsDebuggerNotPresent);

    // --- XGame.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameGetXboxTitleId(uint* titleId);

    // --- XGame.h: XLaunchNewGame / XLaunchRestartOnCrash ---

    [LibraryImport(LibraryName)]
    internal static partial void XLaunchNewGame(byte* exePath, byte* args, IntPtr defaultUser);

    [LibraryImport(LibraryName)]
    internal static partial int XLaunchRestartOnCrash(byte* args, uint reserved);

    // --- XLauncher.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XLaunchUri(IntPtr requestingUser, byte* uri);

    // --- XDisplay.h ---

    [LibraryImport(LibraryName)]
    internal static partial XDisplayHdrModeResult XDisplayTryEnableHdrMode(
        XDisplayHdrModePreference displayModePreference,
        XDisplayHdrModeInfo* displayHdrModeInfo);

    [LibraryImport(LibraryName)]
    internal static partial int XDisplayAcquireTimeoutDeferral(IntPtr* handle);

    [LibraryImport(LibraryName)]
    internal static partial void XDisplayCloseTimeoutDeferralHandle(IntPtr handle);

    // --- XSystem.h: download bandwidth ---
    //
    // allow is a C++ `bool` -- one byte. Passed as a byte rather than a marshalled bool so the
    // declaration is identical under both marshalling models.

    [LibraryImport(LibraryName)]
    internal static partial int XSystemAllowFullDownloadBandwidth(byte allow);
}

#else

internal static unsafe partial class Native
{
    // --- XSystem.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern XSystemAnalyticsInfo XSystemGetAnalyticsInfo();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSystemGetConsoleId(
        nuint consoleIdSize,
        byte* consoleId,
        nuint* consoleIdUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSystemGetXboxLiveSandboxId(
        nuint sandboxIdSize,
        byte* sandboxId,
        nuint* sandboxIdUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern XSystemDeviceType XSystemGetDeviceType();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSystemGetAppSpecificDeviceId(
        nuint appSpecificDeviceIdSize,
        byte* appSpecificDeviceId,
        nuint* appSpecificDeviceIdUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSystemHandleTrack(IntPtr callback, IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XSystemIsHandleValid(IntPtr handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern XSystemRuntimeInfo XSystemGetRuntimeInfo();

    // --- XThread.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XThreadSetTimeSensitive(byte isTimeSensitiveThread);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XThreadIsTimeSensitive();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XThreadAssertNotTimeSensitive();

    // --- XError.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XErrorSetCallback(IntPtr callback, IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XErrorSetOptions(
        XErrorOptions optionsDebuggerPresent,
        XErrorOptions optionsDebuggerNotPresent);

    // --- XGame.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameGetXboxTitleId(uint* titleId);

    // --- XGame.h: XLaunchNewGame / XLaunchRestartOnCrash ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XLaunchNewGame(byte* exePath, byte* args, IntPtr defaultUser);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XLaunchRestartOnCrash(byte* args, uint reserved);

    // --- XLauncher.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XLaunchUri(IntPtr requestingUser, byte* uri);

    // --- XDisplay.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern XDisplayHdrModeResult XDisplayTryEnableHdrMode(
        XDisplayHdrModePreference displayModePreference,
        XDisplayHdrModeInfo* displayHdrModeInfo);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XDisplayAcquireTimeoutDeferral(IntPtr* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XDisplayCloseTimeoutDeferralHandle(IntPtr handle);

    // --- XSystem.h: download bandwidth ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XSystemAllowFullDownloadBandwidth(byte allow);
}

#endif
