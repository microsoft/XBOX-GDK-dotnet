// Raw interop types for XAppCapture.h (GDK edition 260404).
// XAppBroadcast declarations live in the same header; they are transcribed in the first section.
// All C++ bool fields (1-byte on MSVC) are projected as byte for blittability across all TFMs.
// Sources: %GameDKCoreLatest%windows\include\XAppCapture.h

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

// ── Enums ──────────────────────────────────────────────────────────────────────────────────────

/// <summary>Mirrors <c>XAppCaptureMetadataPriority</c> (uint8_t) from XAppCapture.h.</summary>
internal enum XAppCaptureMetadataPriority : byte
{
    Informational = 0,
    Important = 1,
}

/// <summary>Mirrors <c>XAppCaptureVideoEncoding</c> (uint8_t) from XAppCapture.h.</summary>
internal enum XAppCaptureVideoEncoding : byte
{
    H264 = 0,
    HEVC = 1,
}

/// <summary>Mirrors <c>XAppCaptureVideoColorFormat</c> (uint8_t) from XAppCapture.h.</summary>
internal enum XAppCaptureVideoColorFormat : byte
{
    SDR = 0,
    HDR = 1,
}

/// <summary>
/// Mirrors <c>XAppCaptureScreenshotFormatFlag</c> (uint16_t) from XAppCapture.h.
/// Both <c>SDR</c> and <c>HDR</c> may be set simultaneously.
/// </summary>
[Flags]
internal enum XAppCaptureScreenshotFormatFlag : ushort
{
    SDR = 1,
    HDR = 2,
}

// ── Structs ────────────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Mirrors Windows <c>SYSTEMTIME</c>. Used inside capture result structs where the GDK records an
/// absolute calendar time. All fields are <c>WORD</c> (ushort): 16 bytes total, 2-byte alignment.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XSystemTime
{
    public ushort Year;
    public ushort Month;
    public ushort DayOfWeek;
    public ushort Day;
    public ushort Hour;
    public ushort Minute;
    public ushort Second;
    public ushort Milliseconds;
}

/// <summary>
/// Mirrors <c>struct XAppBroadcastStatus</c> from XAppCapture.h. All nine <c>bool</c> fields are
/// 1-byte C++ bools; projected as <c>byte</c> for blittability.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XAppBroadcastStatus
{
    public byte CanStartBroadcast;
    public byte IsAnyAppBroadcasting;
    public byte IsCaptureResourceUnavailable;
    public byte IsGameStreamInProgress;
    public byte IsGpuConstrained;
    public byte IsAppInactive;
    public byte IsBlockedForApp;
    public byte IsDisabledByUser;
    public byte IsDisabledBySystem;
}

/// <summary>
/// Mirrors <c>struct XAppCaptureVideoCaptureSettings</c> from XAppCapture.h. The
/// <c>isCaptureByGamesAllowed</c> field is a 1-byte C++ bool; projected as <c>byte</c>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XAppCaptureVideoCaptureSettings
{
    public uint Width;
    public uint Height;
    public ulong MaxRecordTimespanDurationInMs;
    public XAppCaptureVideoEncoding Encoding;
    public XAppCaptureVideoColorFormat ColorFormat;
    public byte IsCaptureByGamesAllowed; // C++ bool: 1 byte
}

/// <summary>Mirrors <c>struct XAppCaptureScreenshotFile</c> from XAppCapture.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XAppCaptureScreenshotFile
{
    /// <summary><c>char path[MAX_PATH]</c>: null-terminated UTF-8 absolute file path.</summary>
    public fixed byte Path[260];
    public nuint FileSize;
    public uint Width;
    public uint Height;
}

/// <summary>
/// Mirrors <c>struct XAppCaptureDiagnosticScreenshotResult</c> from XAppCapture.h. The native
/// <c>files[APPCAPTURE_MAX_CAPTURE_FILES]</c> array (10 elements, <c>const uint8_t APPCAPTURE_MAX_CAPTURE_FILES = 10</c>)
/// is inlined as explicit fields because <c>InlineArray</c> is unavailable on <c>netstandard2.0</c>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XAppCaptureDiagnosticScreenshotResult
{
    public nuint FileCount;
    // files[APPCAPTURE_MAX_CAPTURE_FILES]: 10 elements
    public XAppCaptureScreenshotFile File0;
    public XAppCaptureScreenshotFile File1;
    public XAppCaptureScreenshotFile File2;
    public XAppCaptureScreenshotFile File3;
    public XAppCaptureScreenshotFile File4;
    public XAppCaptureScreenshotFile File5;
    public XAppCaptureScreenshotFile File6;
    public XAppCaptureScreenshotFile File7;
    public XAppCaptureScreenshotFile File8;
    public XAppCaptureScreenshotFile File9;
}

/// <summary>Mirrors <c>struct XAppCaptureRecordClipResult</c> from XAppCapture.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XAppCaptureRecordClipResult
{
    /// <summary><c>char path[MAX_PATH]</c>: null-terminated UTF-8 path to the recorded clip file.</summary>
    public fixed byte Path[260];
    public nuint FileSize;
    public long StartTime;          // time_t: Unix seconds since 1970-01-01 UTC
    public uint DurationInMs;
    public uint Width;
    public uint Height;
    public XAppCaptureVideoEncoding Encoding;
    public uint StartTimePreciseOffsetHns; // 100-nanosecond offset from StartTime
}

/// <summary>Mirrors <c>struct XAppCaptureLocalResult</c> from XAppCapture.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XAppCaptureLocalResult
{
    /// <summary>Opaque <c>XAppCaptureLocalStreamHandle</c>; pass to <c>XAppCaptureReadLocalStream</c> / <c>XAppCaptureCloseLocalStream</c>.</summary>
    public IntPtr ClipHandle;
    public nuint FileSizeInBytes;
    public XSystemTime ClipStartTimestamp; // UTC
    public ulong DurationInMilliseconds;
    public uint Width;
    public uint Height;
    public XAppCaptureVideoEncoding Encoding;
    public XAppCaptureVideoColorFormat ColorFormat;
}

/// <summary>Mirrors <c>struct XAppCaptureUserRecordingResult</c> from XAppCapture.h.</summary>
/// <remarks>
/// Identical to <see cref="XAppCaptureLocalResult"/> apart from the leading stream handle: a user
/// recording is written to the user's own capture library rather than handed back as a stream.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct XAppCaptureUserRecordingResult
{
    public nuint FileSizeInBytes;
    public XSystemTime ClipStartTimestamp; // UTC
    public ulong DurationInMilliseconds;
    public uint Width;
    public uint Height;
    public XAppCaptureVideoEncoding Encoding;
    public XAppCaptureVideoColorFormat ColorFormat;
}

/// <summary>Mirrors <c>struct XAppCaptureTakeScreenshotResult</c> from XAppCapture.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XAppCaptureTakeScreenshotResult
{
    /// <summary><c>char localId[APPCAPTURE_MAX_LOCALID_LENGTH]</c> (250 bytes): opaque identifier for the screenshot.</summary>
    public fixed byte LocalId[250];
    public XAppCaptureScreenshotFormatFlag AvailableScreenshotFormats;
}
