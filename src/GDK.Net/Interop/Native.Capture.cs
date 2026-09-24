// P/Invoke declarations for XAppCapture.h and the broadcast section it embeds (GDK edition 260404).
//
// XAppBroadcast is declared inside XAppCapture.h, not in a separate header (all bindings are in
// this file.
//
// BACKGROUND) the thunks DLL gap:
//   xgameruntime.lib statically links 404 X* symbols; xgameruntime.thunks.dll exports 390 of them.
//   The 14 absent functions cannot be reached via P/Invoke: a binding for any of them would throw
//   EntryPointNotFoundException, which GameRuntime translates to E_GAMERUNTIME_VERSION_MISMATCH.
//   None of the 14 fall in this family: the user-record trio below was added to the export table
//   in edition 260404, which is this projection's minimum.
//
// NOTE: The native unregister function is XAppCaptureUnRegisterMetadataPurged (capital R in
// "UnRegister"). The C# method uses a lowercase 'r' for .NET naming consistency; EntryPoint
// corrects the binding.
//
// See Interop/Native.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XAppCapture.h : Broadcast ---

    /// <summary><c>XAppBroadcastShowUI</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppBroadcastShowUI(IntPtr requestingUser);

    /// <summary><c>XAppBroadcastGetStatus</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppBroadcastGetStatus(
        IntPtr requestingUser,
        XAppBroadcastStatus* appBroadcastStatus);

    /// <summary><c>XAppBroadcastIsAppBroadcasting</c>: returns C++ bool (1 byte); projected as byte.</summary>
    [LibraryImport(LibraryName)]
    internal static partial byte XAppBroadcastIsAppBroadcasting();

    /// <summary><c>XAppBroadcastRegisterIsAppBroadcastingChanged</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppBroadcastRegisterIsAppBroadcastingChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr appBroadcastMonitorCallback,
        XTaskQueueRegistrationToken* token);

    /// <summary><c>XAppBroadcastUnregisterIsAppBroadcastingChanged</c>: wait is C++ bool (1 byte).</summary>
    [LibraryImport(LibraryName)]
    internal static partial byte XAppBroadcastUnregisterIsAppBroadcastingChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XAppCapture.h : Metadata ---

    /// <summary><c>XAppCaptureMetadataAddStringEvent</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataAddStringEvent(
        byte* name,
        byte* value,
        XAppCaptureMetadataPriority priority);

    /// <summary><c>XAppCaptureMetadataAddInt32Event</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataAddInt32Event(
        byte* name,
        int value,
        XAppCaptureMetadataPriority priority);

    /// <summary><c>XAppCaptureMetadataAddDoubleEvent</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataAddDoubleEvent(
        byte* name,
        double value,
        XAppCaptureMetadataPriority priority);

    /// <summary><c>XAppCaptureMetadataStartStringState</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataStartStringState(
        byte* name,
        byte* value,
        XAppCaptureMetadataPriority priority);

    /// <summary><c>XAppCaptureMetadataStartInt32State</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataStartInt32State(
        byte* name,
        int value,
        XAppCaptureMetadataPriority priority);

    /// <summary><c>XAppCaptureMetadataStartDoubleState</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataStartDoubleState(
        byte* name,
        double value,
        XAppCaptureMetadataPriority priority);

    /// <summary><c>XAppCaptureMetadataStopState</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataStopState(byte* name);

    /// <summary><c>XAppCaptureMetadataStopAllStates</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataStopAllStates();

    /// <summary><c>XAppCaptureMetadataRemainingStorageBytesAvailable</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureMetadataRemainingStorageBytesAvailable(ulong* value);

    /// <summary><c>XAppCaptureRegisterMetadataPurged</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureRegisterMetadataPurged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    /// <summary>
    /// <c>XAppCaptureUnRegisterMetadataPurged</c>: native name has a capital R in "UnRegister";
    /// EntryPoint preserves the binding while the C# method uses conventional casing.
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "XAppCaptureUnRegisterMetadataPurged")]
    internal static partial byte XAppCaptureUnregisterMetadataPurged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XAppCapture.h : Diagnostic APIs ---

    /// <summary><c>XAppCaptureTakeDiagnosticScreenshot</c>: gamescreenOnly is C++ bool (1 byte).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureTakeDiagnosticScreenshot(
        byte gamescreenOnly,
        XAppCaptureScreenshotFormatFlag captureFlags,
        byte* filenamePrefix,
        XAppCaptureDiagnosticScreenshotResult* result);

    /// <summary><c>XAppCaptureRecordDiagnosticClip</c>: startTime is time_t (int64_t on Windows).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureRecordDiagnosticClip(
        long startTime,
        uint durationInMs,
        byte* filenamePrefix,
        XAppCaptureRecordClipResult* result);

    // --- XAppCapture.h : Local Capture APIs ---

    /// <summary><c>XAppCaptureGetVideoCaptureSettings</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureGetVideoCaptureSettings(
        XAppCaptureVideoCaptureSettings* userCaptureSettings);

    /// <summary><c>XAppCaptureRecordTimespan</c>: startTimestamp may be null (uses current time).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureRecordTimespan(
        XSystemTime* startTimestamp,
        ulong durationInMilliseconds,
        XAppCaptureLocalResult* result);

    /// <summary><c>XAppCaptureReadLocalStream</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureReadLocalStream(
        IntPtr handle,
        nuint startPosition,
        uint bytesToRead,
        byte* buffer,
        uint* bytesWritten);

    /// <summary><c>XAppCaptureCloseLocalStream</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureCloseLocalStream(IntPtr handle);

    // --- XAppCapture.h : Screenshot APIs ---

    /// <summary><c>XAppCaptureTakeScreenshot</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureTakeScreenshot(
        IntPtr requestingUser,
        XAppCaptureTakeScreenshotResult* result);

    /// <summary><c>XAppCaptureOpenScreenshotStream</c>: totalBytes is optional; pass non-null to retrieve size.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureOpenScreenshotStream(
        byte* localId,
        XAppCaptureScreenshotFormatFlag screenshotFormat,
        IntPtr* handle,
        ulong* totalBytes);

    /// <summary><c>XAppCaptureReadScreenshotStream</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureReadScreenshotStream(
        IntPtr handle,
        ulong startPosition,
        uint bytesToRead,
        byte* buffer,
        uint* bytesWritten);

    /// <summary><c>XAppCaptureCloseScreenshotStream</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureCloseScreenshotStream(IntPtr handle);

    // --- XAppCapture.h : Settings APIs ---

    /// <summary><c>XAppCaptureEnableRecord</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureEnableRecord();

    /// <summary><c>XAppCaptureDisableRecord</c></summary>
    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureDisableRecord();

    // --- XAppCapture.h: user-controlled continuous recording ---

    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureStartUserRecord(
        IntPtr requestingUser,
        uint localIdBufferLength,
        byte* localIdBuffer);

    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureStopUserRecord(
        byte* localId,
        XAppCaptureUserRecordingResult* result);

    [LibraryImport(LibraryName)]
    internal static partial int XAppCaptureCancelUserRecord(byte* localId);
}

#else

internal static unsafe partial class Native
{
    // --- XAppCapture.h : Broadcast ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppBroadcastShowUI(IntPtr requestingUser);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppBroadcastGetStatus(
        IntPtr requestingUser,
        XAppBroadcastStatus* appBroadcastStatus);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XAppBroadcastIsAppBroadcasting();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppBroadcastRegisterIsAppBroadcastingChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr appBroadcastMonitorCallback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XAppBroadcastUnregisterIsAppBroadcastingChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XAppCapture.h : Metadata ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataAddStringEvent(
        byte* name,
        byte* value,
        XAppCaptureMetadataPriority priority);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataAddInt32Event(
        byte* name,
        int value,
        XAppCaptureMetadataPriority priority);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataAddDoubleEvent(
        byte* name,
        double value,
        XAppCaptureMetadataPriority priority);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataStartStringState(
        byte* name,
        byte* value,
        XAppCaptureMetadataPriority priority);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataStartInt32State(
        byte* name,
        int value,
        XAppCaptureMetadataPriority priority);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataStartDoubleState(
        byte* name,
        double value,
        XAppCaptureMetadataPriority priority);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataStopState(byte* name);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataStopAllStates();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureMetadataRemainingStorageBytesAvailable(ulong* value);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureRegisterMetadataPurged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, EntryPoint = "XAppCaptureUnRegisterMetadataPurged",
        CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XAppCaptureUnregisterMetadataPurged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XAppCapture.h : Diagnostic APIs ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureTakeDiagnosticScreenshot(
        byte gamescreenOnly,
        XAppCaptureScreenshotFormatFlag captureFlags,
        byte* filenamePrefix,
        XAppCaptureDiagnosticScreenshotResult* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureRecordDiagnosticClip(
        long startTime,
        uint durationInMs,
        byte* filenamePrefix,
        XAppCaptureRecordClipResult* result);

    // --- XAppCapture.h : Local Capture APIs ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureGetVideoCaptureSettings(
        XAppCaptureVideoCaptureSettings* userCaptureSettings);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureRecordTimespan(
        XSystemTime* startTimestamp,
        ulong durationInMilliseconds,
        XAppCaptureLocalResult* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureReadLocalStream(
        IntPtr handle,
        nuint startPosition,
        uint bytesToRead,
        byte* buffer,
        uint* bytesWritten);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureCloseLocalStream(IntPtr handle);

    // --- XAppCapture.h : Screenshot APIs ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureTakeScreenshot(
        IntPtr requestingUser,
        XAppCaptureTakeScreenshotResult* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureOpenScreenshotStream(
        byte* localId,
        XAppCaptureScreenshotFormatFlag screenshotFormat,
        IntPtr* handle,
        ulong* totalBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureReadScreenshotStream(
        IntPtr handle,
        ulong startPosition,
        uint bytesToRead,
        byte* buffer,
        uint* bytesWritten);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureCloseScreenshotStream(IntPtr handle);

    // --- XAppCapture.h : Settings APIs ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureEnableRecord();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureDisableRecord();

    // --- XAppCapture.h: user-controlled continuous recording ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureStartUserRecord(
        IntPtr requestingUser,
        uint localIdBufferLength,
        byte* localIdBuffer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureStopUserRecord(
        byte* localId,
        XAppCaptureUserRecordingResult* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAppCaptureCancelUserRecord(byte* localId);
}

#endif
