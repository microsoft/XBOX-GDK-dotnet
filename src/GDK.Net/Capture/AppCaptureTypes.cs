// Public types for the XAppCapture / XAppBroadcast idiomatic layer.
// Enums mirror the raw interop enums in NativeTypes.Capture.cs with .NET-idiomatic casing.
// Result classes wrap native structs and hide fixed-buffer / time_t details.
// Stream classes own opaque native handles and expose Read / Dispose.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Interop;

namespace GDK.Net.Capture;

// ── Enums ──────────────────────────────────────────────────────────────────────────────────────

/// <summary>Priority hint for game-capture metadata events and states. Mirrors <c>XAppCaptureMetadataPriority</c>.</summary>
public enum AppCaptureMetadataPriority : byte
{
    /// <summary>Low-priority detail; may be discarded first when storage is constrained.</summary>
    Informational = 0,
    /// <summary>Higher-priority detail; retained longer under storage pressure.</summary>
    Important = 1,
}

/// <summary>Video codec used for a captured clip. Mirrors <c>XAppCaptureVideoEncoding</c>.</summary>
public enum AppCaptureVideoEncoding : byte
{
    /// <summary>H.264 / AVC.</summary>
    H264 = 0,
    /// <summary>H.265 / HEVC.</summary>
    Hevc = 1,
}

/// <summary>Color format of a captured clip. Mirrors <c>XAppCaptureVideoColorFormat</c>.</summary>
public enum AppCaptureVideoColorFormat : byte
{
    /// <summary>Standard dynamic range.</summary>
    Sdr = 0,
    /// <summary>High dynamic range.</summary>
    Hdr = 1,
}

/// <summary>
/// Screenshot format flags. Multiple flags may be set when both SDR and HDR are available.
/// Mirrors <c>XAppCaptureScreenshotFormatFlag</c>.
/// </summary>
[Flags]
public enum AppCaptureScreenshotFormatFlag : ushort
{
    /// <summary>Standard dynamic range screenshot.</summary>
    Sdr = 1,
    /// <summary>High dynamic range screenshot.</summary>
    Hdr = 2,
}

// ── Result / info types ────────────────────────────────────────────────────────────────────────

/// <summary>
/// Current broadcast capability and state of the user. Returned by
/// <see cref="AppCaptureManager.GetBroadcastStatus"/>. Mirrors <c>XAppBroadcastStatus</c>.
/// </summary>
public sealed class BroadcastStatus
{
    internal BroadcastStatus(XAppBroadcastStatus s)
    {
        CanStartBroadcast = s.CanStartBroadcast != 0;
        IsAnyAppBroadcasting = s.IsAnyAppBroadcasting != 0;
        IsCaptureResourceUnavailable = s.IsCaptureResourceUnavailable != 0;
        IsGameStreamInProgress = s.IsGameStreamInProgress != 0;
        IsGpuConstrained = s.IsGpuConstrained != 0;
        IsAppInactive = s.IsAppInactive != 0;
        IsBlockedForApp = s.IsBlockedForApp != 0;
        IsDisabledByUser = s.IsDisabledByUser != 0;
        IsDisabledBySystem = s.IsDisabledBySystem != 0;
    }

    /// <summary>The user can start a broadcast (<c>canStartBroadcast</c>).</summary>
    public bool CanStartBroadcast { get; }
    /// <summary>At least one app is currently broadcasting (<c>isAnyAppBroadcasting</c>).</summary>
    public bool IsAnyAppBroadcasting { get; }
    /// <summary>A capture resource needed for broadcasting is in use (<c>isCaptureResourceUnavailable</c>).</summary>
    public bool IsCaptureResourceUnavailable { get; }
    /// <summary>A game-streaming session is in progress (<c>isGameStreamInProgress</c>).</summary>
    public bool IsGameStreamInProgress { get; }
    /// <summary>The GPU is constrained and cannot support broadcasting (<c>isGpuConstrained</c>).</summary>
    public bool IsGpuConstrained { get; }
    /// <summary>The app is not in the foreground (<c>isAppInactive</c>).</summary>
    public bool IsAppInactive { get; }
    /// <summary>Broadcasting is blocked for this specific application (<c>isBlockedForApp</c>).</summary>
    public bool IsBlockedForApp { get; }
    /// <summary>The user has disabled broadcasting in system settings (<c>isDisabledByUser</c>).</summary>
    public bool IsDisabledByUser { get; }
    /// <summary>The system has disabled broadcasting (<c>isDisabledBySystem</c>).</summary>
    public bool IsDisabledBySystem { get; }
}

/// <summary>
/// Current video capture configuration. Returned by <see cref="AppCaptureManager.GetVideoCaptureSettings"/>.
/// Mirrors <c>XAppCaptureVideoCaptureSettings</c>.
/// </summary>
public sealed class VideoCaptureSettings
{
    internal VideoCaptureSettings(XAppCaptureVideoCaptureSettings s)
    {
        Width = s.Width;
        Height = s.Height;
        MaxRecordTimespanDurationInMs = s.MaxRecordTimespanDurationInMs;
        Encoding = (AppCaptureVideoEncoding)s.Encoding;
        ColorFormat = (AppCaptureVideoColorFormat)s.ColorFormat;
        IsCaptureByGamesAllowed = s.IsCaptureByGamesAllowed != 0;
    }

    /// <summary>Width of the captured video in pixels (<c>width</c>).</summary>
    public uint Width { get; }
    /// <summary>Height of the captured video in pixels (<c>height</c>).</summary>
    public uint Height { get; }
    /// <summary>Maximum clip duration in milliseconds (<c>maxRecordTimespanDurationInMs</c>).</summary>
    public ulong MaxRecordTimespanDurationInMs { get; }
    /// <summary>Video encoding format (<c>encoding</c>).</summary>
    public AppCaptureVideoEncoding Encoding { get; }
    /// <summary>Color format of the captured video (<c>colorFormat</c>).</summary>
    public AppCaptureVideoColorFormat ColorFormat { get; }
    /// <summary>Whether in-game capture triggered by the system is permitted (<c>isCaptureByGamesAllowed</c>).</summary>
    public bool IsCaptureByGamesAllowed { get; }
}

/// <summary>
/// Metadata for one screenshot file in a diagnostic screenshot result.
/// Mirrors <c>XAppCaptureScreenshotFile</c>.
/// </summary>
public sealed class DiagnosticScreenshotFile
{
    internal unsafe DiagnosticScreenshotFile(XAppCaptureScreenshotFile* f)
    {
        Path = Utf8.ToString(f->Path, 260) ?? string.Empty;
        FileSize = (long)f->FileSize;
        Width = f->Width;
        Height = f->Height;
    }

    /// <summary>Absolute path to the screenshot file on disk (<c>path</c>).</summary>
    public string Path { get; }
    /// <summary>File size in bytes (<c>fileSize</c>).</summary>
    public long FileSize { get; }
    /// <summary>Width in pixels (<c>width</c>).</summary>
    public uint Width { get; }
    /// <summary>Height in pixels (<c>height</c>).</summary>
    public uint Height { get; }
}

/// <summary>
/// Result of <see cref="AppCaptureManager.TakeDiagnosticScreenshot"/>. Contains up to
/// <c>APPCAPTURE_MAX_CAPTURE_FILES</c> (10) screenshot file entries.
/// Mirrors <c>XAppCaptureDiagnosticScreenshotResult</c>.
/// </summary>
public sealed class DiagnosticScreenshotResult
{
    /// <summary>Maximum number of screenshot files the runtime can return in a single call (10).</summary>
    public const int MaxFiles = 10;

    internal unsafe DiagnosticScreenshotResult(XAppCaptureDiagnosticScreenshotResult* r)
    {
        int count = (int)r->FileCount;
        var files = new DiagnosticScreenshotFile[count];
        // r points to stack/unmanaged memory — no fixed statement needed.
        XAppCaptureScreenshotFile* first = &r->File0;
        for (int i = 0; i < count; i++)
        {
            files[i] = new DiagnosticScreenshotFile(first + i);
        }
        Files = files;
    }

    /// <summary>Screenshot files captured in this call (up to <see cref="MaxFiles"/>).</summary>
    public DiagnosticScreenshotFile[] Files { get; }
}

/// <summary>
/// Result of <see cref="AppCaptureManager.RecordDiagnosticClip"/>.
/// Mirrors <c>XAppCaptureRecordClipResult</c>.
/// </summary>
public sealed class DiagnosticClipResult
{
    internal unsafe DiagnosticClipResult(XAppCaptureRecordClipResult* r)
    {
        Path = Utf8.ToString(r->Path, 260) ?? string.Empty;
        FileSize = (long)r->FileSize;
        StartTime = DateTimeOffset.FromUnixTimeSeconds(r->StartTime);
        DurationInMs = r->DurationInMs;
        Width = r->Width;
        Height = r->Height;
        Encoding = (AppCaptureVideoEncoding)r->Encoding;
        StartTimePreciseOffsetHns = r->StartTimePreciseOffsetHns;
    }

    /// <summary>Absolute path to the clip file on disk (<c>path</c>).</summary>
    public string Path { get; }
    /// <summary>File size in bytes (<c>fileSize</c>).</summary>
    public long FileSize { get; }
    /// <summary>Clip start time in UTC (<c>startTime</c>, from Unix <c>time_t</c>).</summary>
    public DateTimeOffset StartTime { get; }
    /// <summary>Clip duration in milliseconds (<c>durationInMs</c>).</summary>
    public uint DurationInMs { get; }
    /// <summary>Video width in pixels (<c>width</c>).</summary>
    public uint Width { get; }
    /// <summary>Video height in pixels (<c>height</c>).</summary>
    public uint Height { get; }
    /// <summary>Video encoding format (<c>encoding</c>).</summary>
    public AppCaptureVideoEncoding Encoding { get; }
    /// <summary>Precise offset in 100-nanosecond units from <see cref="StartTime"/> (<c>startTimePreciseOffsetHns</c>).</summary>
    public uint StartTimePreciseOffsetHns { get; }
}

/// <summary>
/// The local identifier and available formats returned by <see cref="AppCaptureManager.TakeScreenshot"/>.
/// Pass <see cref="LocalId"/> and a format flag to <see cref="AppCaptureManager.OpenScreenshotStream"/>.
/// Mirrors <c>XAppCaptureTakeScreenshotResult</c>.
/// </summary>
public sealed class TakeScreenshotResult
{
    internal unsafe TakeScreenshotResult(XAppCaptureTakeScreenshotResult* r)
    {
        LocalId = Utf8.ToString(r->LocalId, 250) ?? string.Empty;
        AvailableFormats = (AppCaptureScreenshotFormatFlag)r->AvailableScreenshotFormats;
    }

    /// <summary>Opaque local identifier for the screenshot (<c>localId</c>).</summary>
    public string LocalId { get; }
    /// <summary>Which HDR/SDR formats are available for this screenshot (<c>availableScreenshotFormats</c>).</summary>
    public AppCaptureScreenshotFormatFlag AvailableFormats { get; }
}

/// <summary>
/// The finished clip produced by <see cref="AppCaptureManager.StopUserRecord"/>.
/// </summary>
/// <remarks>
/// Unlike <see cref="LocalClipStream"/>, a user recording is written straight to the user's own
/// capture library, so there is no stream handle to read or dispose — this type is a plain
/// description of what was recorded.
/// </remarks>
public sealed class UserRecordingResult
{
    internal unsafe UserRecordingResult(XAppCaptureUserRecordingResult* r)
    {
        FileSizeInBytes = (long)r->FileSizeInBytes;
        StartTimestamp = new DateTime(
            r->ClipStartTimestamp.Year,
            r->ClipStartTimestamp.Month,
            r->ClipStartTimestamp.Day,
            r->ClipStartTimestamp.Hour,
            r->ClipStartTimestamp.Minute,
            r->ClipStartTimestamp.Second,
            r->ClipStartTimestamp.Milliseconds,
            DateTimeKind.Utc);
        DurationInMilliseconds = r->DurationInMilliseconds;
        Width = r->Width;
        Height = r->Height;
        Encoding = (AppCaptureVideoEncoding)r->Encoding;
        ColorFormat = (AppCaptureVideoColorFormat)r->ColorFormat;
    }

    /// <summary>Total clip data size in bytes (<c>fileSizeInBytes</c>).</summary>
    public long FileSizeInBytes { get; }
    /// <summary>When the clip started, in UTC (<c>clipStartTimestamp</c>).</summary>
    public DateTime StartTimestamp { get; }
    /// <summary>Clip duration in milliseconds (<c>durationInMilliseconds</c>).</summary>
    public ulong DurationInMilliseconds { get; }
    /// <summary>Video width in pixels (<c>width</c>).</summary>
    public uint Width { get; }
    /// <summary>Video height in pixels (<c>height</c>).</summary>
    public uint Height { get; }
    /// <summary>Video encoding (<c>encoding</c>).</summary>
    public AppCaptureVideoEncoding Encoding { get; }
    /// <summary>Color format (<c>colorFormat</c>).</summary>
    public AppCaptureVideoColorFormat ColorFormat { get; }
}

// ── Stream types ───────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Provides random-access reading of a local video clip returned by
/// <see cref="AppCaptureManager.RecordTimespan(ulong)"/> or
/// <see cref="AppCaptureManager.RecordTimespan(DateTime, ulong)"/>. Owns the native
/// <c>XAppCaptureLocalStreamHandle</c>; dispose to release it via
/// <c>XAppCaptureCloseLocalStream</c>.
/// </summary>
public sealed unsafe class LocalClipStream : IDisposable
{
    private IntPtr _handle;
    private bool _disposed;

    internal LocalClipStream(XAppCaptureLocalResult* r)
    {
        _handle = r->ClipHandle;
        FileSizeInBytes = (long)r->FileSizeInBytes;
        StartTimestamp = SystemTimeToDateTime(r->ClipStartTimestamp);
        DurationInMilliseconds = r->DurationInMilliseconds;
        Width = r->Width;
        Height = r->Height;
        Encoding = (AppCaptureVideoEncoding)r->Encoding;
        ColorFormat = (AppCaptureVideoColorFormat)r->ColorFormat;
    }

    /// <summary>Total clip data size in bytes (<c>fileSizeInBytes</c>).</summary>
    public long FileSizeInBytes { get; }
    /// <summary>When the clip started, in UTC (<c>clipStartTimestamp</c>).</summary>
    public DateTime StartTimestamp { get; }
    /// <summary>Clip duration in milliseconds (<c>durationInMilliseconds</c>).</summary>
    public ulong DurationInMilliseconds { get; }
    /// <summary>Video width in pixels (<c>width</c>).</summary>
    public uint Width { get; }
    /// <summary>Video height in pixels (<c>height</c>).</summary>
    public uint Height { get; }
    /// <summary>Video encoding (<c>encoding</c>).</summary>
    public AppCaptureVideoEncoding Encoding { get; }
    /// <summary>Color format (<c>colorFormat</c>).</summary>
    public AppCaptureVideoColorFormat ColorFormat { get; }

    /// <summary>
    /// Reads up to <paramref name="count"/> bytes of clip data starting at
    /// <paramref name="startPosition"/> into <paramref name="buffer"/>.
    /// Returns the number of bytes written (<c>XAppCaptureReadLocalStream</c>).
    /// </summary>
    /// <param name="startPosition">Byte offset from the beginning of the stream.</param>
    /// <param name="buffer">Destination buffer.</param>
    /// <param name="bufferOffset">Offset within <paramref name="buffer"/> at which to write.</param>
    /// <param name="count">Maximum number of bytes to read.</param>
    public int Read(nuint startPosition, byte[] buffer, int bufferOffset, int count)
    {
        ThrowIfDisposed();
        if (buffer is null) throw new ArgumentNullException(nameof(buffer));
        if (bufferOffset < 0 || bufferOffset > buffer.Length) throw new ArgumentOutOfRangeException(nameof(bufferOffset));
        if (count < 0 || count > buffer.Length - bufferOffset) throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return 0;

        uint bytesWritten;
        fixed (byte* pinned = buffer)
        {
            Hr.ThrowIfFailed(Native.XAppCaptureReadLocalStream(
                _handle, startPosition, (uint)count, pinned + bufferOffset, &bytesWritten));
        }
        return (int)bytesWritten;
    }

    /// <summary>
    /// Closes the native stream handle (<c>XAppCaptureCloseLocalStream</c>). After disposal, calling
    /// any other method throws <see cref="ObjectDisposedException"/>.
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != IntPtr.Zero)
        {
            Native.XAppCaptureCloseLocalStream(_handle);
            _handle = IntPtr.Zero;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(LocalClipStream));
    }

    private static DateTime SystemTimeToDateTime(XSystemTime st) =>
        new DateTime(st.Year, st.Month, st.Day, st.Hour, st.Minute, st.Second,
                     st.Milliseconds, DateTimeKind.Utc);
}

/// <summary>
/// Provides random-access reading of a screenshot opened via
/// <see cref="AppCaptureManager.OpenScreenshotStream"/>. Owns the native
/// <c>XAppCaptureScreenshotStreamHandle</c>; dispose to release it via
/// <c>XAppCaptureCloseScreenshotStream</c>.
/// </summary>
public sealed unsafe class ScreenshotStream : IDisposable
{
    private IntPtr _handle;
    private bool _disposed;

    internal ScreenshotStream(IntPtr handle, ulong totalBytes)
    {
        _handle = handle;
        TotalBytes = totalBytes;
    }

    /// <summary>Total size of the screenshot data in bytes (<c>totalBytes</c>).</summary>
    public ulong TotalBytes { get; }

    /// <summary>
    /// Reads up to <paramref name="count"/> bytes of screenshot data starting at
    /// <paramref name="startPosition"/> into <paramref name="buffer"/>.
    /// Returns the number of bytes written (<c>XAppCaptureReadScreenshotStream</c>).
    /// </summary>
    /// <param name="startPosition">Byte offset from the beginning of the screenshot.</param>
    /// <param name="buffer">Destination buffer.</param>
    /// <param name="bufferOffset">Offset within <paramref name="buffer"/> at which to write.</param>
    /// <param name="count">Maximum number of bytes to read.</param>
    public int Read(ulong startPosition, byte[] buffer, int bufferOffset, int count)
    {
        ThrowIfDisposed();
        if (buffer is null) throw new ArgumentNullException(nameof(buffer));
        if (bufferOffset < 0 || bufferOffset > buffer.Length) throw new ArgumentOutOfRangeException(nameof(bufferOffset));
        if (count < 0 || count > buffer.Length - bufferOffset) throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return 0;

        uint bytesWritten;
        fixed (byte* pinned = buffer)
        {
            Hr.ThrowIfFailed(Native.XAppCaptureReadScreenshotStream(
                _handle, startPosition, (uint)count, pinned + bufferOffset, &bytesWritten));
        }
        return (int)bytesWritten;
    }

    /// <summary>
    /// Closes the native stream handle (<c>XAppCaptureCloseScreenshotStream</c>). After disposal,
    /// calling any other method throws <see cref="ObjectDisposedException"/>.
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != IntPtr.Zero)
        {
            Native.XAppCaptureCloseScreenshotStream(_handle);
            _handle = IntPtr.Zero;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(ScreenshotStream));
    }
}
