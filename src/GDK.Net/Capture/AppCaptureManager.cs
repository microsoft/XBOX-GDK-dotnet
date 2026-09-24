// App capture and broadcast management.
//
// Reached through GameRuntime.Capture (note: see the porting report: GameRuntime.cs needs a
// Capture property added before end-users can access this through the standard entry point).
//
// XAppBroadcast is declared inside XAppCapture.h, so both families are projected here.
//
// Event registrations are created lazily on the first subscriber and released on Dispose with
// wait: true, so no callback can be in flight once the manager is disposed.

using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.Capture;

/// <summary>
/// Application capture and broadcast management. Reached through <c>GameRuntime.Capture</c>.
/// </summary>
/// <remarks>
/// <para>
/// Both event registrations (<see cref="BroadcastingChanged"/> and <see cref="MetadataPurged"/>)
/// are created lazily on the first subscriber and released on <see cref="Dispose"/> with
/// <c>wait: true</c> so no callback can be in flight once the manager is disposed.
/// </para>
/// <para>
/// The following APIs are deliberately absent because <c>xgameruntime.thunks.dll</c> does not
/// export them (they are among the 49 functions present in <c>xgameruntime.lib</c> but absent
/// from the redistributable DLL): <c>XAppCaptureStartUserRecord</c>,
/// <c>XAppCaptureStopUserRecord</c>, and <c>XAppCaptureCancelUserRecord</c>. Binding any of them
/// would throw <see cref="System.EntryPointNotFoundException"/>, which the runtime surfaces as
/// <c>E_GAMERUNTIME_VERSION_MISMATCH</c>, a misleading environment error rather than a
/// missing-API error.
/// </para>
/// </remarks>
public sealed unsafe class AppCaptureManager : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, CaptureRegistration> Registrations = new();

    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private EventHandler? _broadcastingChanged;
    private EventHandler? _metadataPurged;

    private CaptureRegistration? _broadcastingRegistration;
    private CaptureRegistration? _metadataRegistration;
    private bool _disposed;

    /// <summary>
    /// Creates a manager whose event callbacks name no task queue, so the Gaming Runtime resolves
    /// the process default.
    /// </summary>
    internal AppCaptureManager(GameTaskQueue? queue)
    {
        _queue = queue;
    }

    // ── Events ─────────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Raised when the app's broadcasting state changes
    /// (<c>XAppBroadcastRegisterIsAppBroadcastingChanged</c>).
    /// </summary>
    /// <remarks>
    /// Delivered on the manager's task queue. Subscription lazily registers the native callback;
    /// the registration is released with <c>wait: true</c> on <see cref="Dispose"/>.
    /// </remarks>
    public event EventHandler? BroadcastingChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _broadcastingChanged += value;
                _broadcastingRegistration ??= Register(CaptureRegistrationKind.Broadcasting);
            }
        }
        remove
        {
            lock (_gate) { _broadcastingChanged -= value; }
        }
    }

    /// <summary>
    /// Raised when the capture metadata buffer is purged due to storage pressure
    /// (<c>XAppCaptureRegisterMetadataPurged</c>).
    /// </summary>
    public event EventHandler? MetadataPurged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _metadataPurged += value;
                _metadataRegistration ??= Register(CaptureRegistrationKind.Metadata);
            }
        }
        remove
        {
            lock (_gate) { _metadataPurged -= value; }
        }
    }

    // ── Broadcast ──────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Shows the system broadcasting UI for <paramref name="user"/> (<c>XAppBroadcastShowUI</c>).
    /// </summary>
    /// <param name="user">The user requesting the broadcast UI.</param>
    public void ShowBroadcastUi(User user)
    {
        ThrowIfDisposed();
        if (user is null) throw new ArgumentNullException(nameof(user));
        Hr.ThrowIfFailed(Native.XAppBroadcastShowUI(user.Handle));
    }

    /// <summary>
    /// Returns the current broadcast status for <paramref name="user"/>
    /// (<c>XAppBroadcastGetStatus</c>).
    /// </summary>
    public BroadcastStatus GetBroadcastStatus(User user)
    {
        ThrowIfDisposed();
        if (user is null) throw new ArgumentNullException(nameof(user));
        XAppBroadcastStatus status;
        Hr.ThrowIfFailed(Native.XAppBroadcastGetStatus(user.Handle, &status));
        return new BroadcastStatus(status);
    }

    /// <summary>
    /// Returns <see langword="true"/> when any app in the system is currently broadcasting
    /// (<c>XAppBroadcastIsAppBroadcasting</c>).
    /// </summary>
    public bool IsAppBroadcasting()
    {
        ThrowIfDisposed();
        return Native.XAppBroadcastIsAppBroadcasting() != 0;
    }

    // ── Metadata ───────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Appends a one-shot string metadata event (<c>XAppCaptureMetadataAddStringEvent</c>).
    /// </summary>
    /// <param name="name">UTF-8 event name. Must not be null or empty.</param>
    /// <param name="value">UTF-8 string value.</param>
    /// <param name="priority">Storage priority when the buffer is under pressure.</param>
    public void AddMetadataString(string name, string value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));
        if (value is null) throw new ArgumentNullException(nameof(value));

        IntPtr pName = Utf8.Allocate(name);
        IntPtr pValue = Utf8.Allocate(value);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataAddStringEvent(
                (byte*)pName, (byte*)pValue, (XAppCaptureMetadataPriority)priority));
        }
        finally
        {
            Utf8.Free(pName);
            Utf8.Free(pValue);
        }
    }

    /// <summary>
    /// Appends a one-shot int32 metadata event (<c>XAppCaptureMetadataAddInt32Event</c>).
    /// </summary>
    /// <param name="name">UTF-8 event name. Must not be null or empty.</param>
    /// <param name="value">Integer value.</param>
    /// <param name="priority">Storage priority when the buffer is under pressure.</param>
    public void AddMetadataInt32(string name, int value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));

        IntPtr pName = Utf8.Allocate(name);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataAddInt32Event(
                (byte*)pName, value, (XAppCaptureMetadataPriority)priority));
        }
        finally
        {
            Utf8.Free(pName);
        }
    }

    /// <summary>
    /// Appends a one-shot double metadata event (<c>XAppCaptureMetadataAddDoubleEvent</c>).
    /// </summary>
    /// <param name="name">UTF-8 event name. Must not be null or empty.</param>
    /// <param name="value">Double value.</param>
    /// <param name="priority">Storage priority when the buffer is under pressure.</param>
    public void AddMetadataDouble(string name, double value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));

        IntPtr pName = Utf8.Allocate(name);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataAddDoubleEvent(
                (byte*)pName, value, (XAppCaptureMetadataPriority)priority));
        }
        finally
        {
            Utf8.Free(pName);
        }
    }

    /// <summary>
    /// Starts a persistent string metadata state (<c>XAppCaptureMetadataStartStringState</c>).
    /// Call <see cref="StopMetadataState"/> or <see cref="StopAllMetadataStates"/> to end it.
    /// </summary>
    public void StartMetadataStringState(string name, string value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));
        if (value is null) throw new ArgumentNullException(nameof(value));

        IntPtr pName = Utf8.Allocate(name);
        IntPtr pValue = Utf8.Allocate(value);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataStartStringState(
                (byte*)pName, (byte*)pValue, (XAppCaptureMetadataPriority)priority));
        }
        finally
        {
            Utf8.Free(pName);
            Utf8.Free(pValue);
        }
    }

    /// <summary>
    /// Starts a persistent int32 metadata state (<c>XAppCaptureMetadataStartInt32State</c>).
    /// </summary>
    public void StartMetadataInt32State(string name, int value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));

        IntPtr pName = Utf8.Allocate(name);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataStartInt32State(
                (byte*)pName, value, (XAppCaptureMetadataPriority)priority));
        }
        finally
        {
            Utf8.Free(pName);
        }
    }

    /// <summary>
    /// Starts a persistent double metadata state (<c>XAppCaptureMetadataStartDoubleState</c>).
    /// </summary>
    public void StartMetadataDoubleState(string name, double value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));

        IntPtr pName = Utf8.Allocate(name);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataStartDoubleState(
                (byte*)pName, value, (XAppCaptureMetadataPriority)priority));
        }
        finally
        {
            Utf8.Free(pName);
        }
    }

    /// <summary>
    /// Stops the named persistent metadata state (<c>XAppCaptureMetadataStopState</c>).
    /// </summary>
    /// <param name="name">The name that was passed to the corresponding <c>StartMetadata*State</c> call.</param>
    public void StopMetadataState(string name)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Metadata name must not be null or empty.", nameof(name));

        IntPtr pName = Utf8.Allocate(name);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureMetadataStopState((byte*)pName));
        }
        finally
        {
            Utf8.Free(pName);
        }
    }

    /// <summary>
    /// Stops all active persistent metadata states (<c>XAppCaptureMetadataStopAllStates</c>).
    /// </summary>
    public void StopAllMetadataStates()
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(Native.XAppCaptureMetadataStopAllStates());
    }

    /// <summary>
    /// Returns the number of bytes remaining in the metadata storage buffer
    /// (<c>XAppCaptureMetadataRemainingStorageBytesAvailable</c>).
    /// </summary>
    public ulong GetMetadataRemainingStorageBytes()
    {
        ThrowIfDisposed();
        ulong value;
        Hr.ThrowIfFailed(Native.XAppCaptureMetadataRemainingStorageBytesAvailable(&value));
        return value;
    }

    // ── Diagnostic ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Takes a diagnostic screenshot and writes it to disk
    /// (<c>XAppCaptureTakeDiagnosticScreenshot</c>).
    /// </summary>
    /// <param name="gamescreenOnly">
    /// When <see langword="true"/>, captures only the game surface (not system overlays).
    /// </param>
    /// <param name="captureFlags">Which format(s) to capture.</param>
    /// <param name="filenamePrefix">Optional filename prefix for the output files.</param>
    public DiagnosticScreenshotResult TakeDiagnosticScreenshot(
        bool gamescreenOnly,
        AppCaptureScreenshotFormatFlag captureFlags,
        string? filenamePrefix = null)
    {
        ThrowIfDisposed();

        IntPtr pPrefix = Utf8.Allocate(filenamePrefix);
        try
        {
            XAppCaptureDiagnosticScreenshotResult result;
            Hr.ThrowIfFailed(Native.XAppCaptureTakeDiagnosticScreenshot(
                gamescreenOnly ? (byte)1 : (byte)0,
                (XAppCaptureScreenshotFormatFlag)captureFlags,
                (byte*)pPrefix,
                &result));
            return new DiagnosticScreenshotResult(&result);
        }
        finally
        {
            Utf8.Free(pPrefix);
        }
    }

    /// <summary>
    /// Records a diagnostic clip starting at <paramref name="startTime"/> (UTC)
    /// (<c>XAppCaptureRecordDiagnosticClip</c>).
    /// </summary>
    /// <param name="startTime">Clip start time in UTC. Converted to a Unix <c>time_t</c>.</param>
    /// <param name="durationInMs">Clip duration in milliseconds.</param>
    /// <param name="filenamePrefix">Optional filename prefix for the output file.</param>
    public DiagnosticClipResult RecordDiagnosticClip(
        DateTimeOffset startTime,
        uint durationInMs,
        string? filenamePrefix = null)
    {
        ThrowIfDisposed();

        IntPtr pPrefix = Utf8.Allocate(filenamePrefix);
        try
        {
            XAppCaptureRecordClipResult result;
            Hr.ThrowIfFailed(Native.XAppCaptureRecordDiagnosticClip(
                startTime.ToUnixTimeSeconds(),
                durationInMs,
                (byte*)pPrefix,
                &result));
            return new DiagnosticClipResult(&result);
        }
        finally
        {
            Utf8.Free(pPrefix);
        }
    }

    // ── Local Capture ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the current video capture configuration (<c>XAppCaptureGetVideoCaptureSettings</c>).
    /// </summary>
    public VideoCaptureSettings GetVideoCaptureSettings()
    {
        ThrowIfDisposed();
        XAppCaptureVideoCaptureSettings settings;
        Hr.ThrowIfFailed(Native.XAppCaptureGetVideoCaptureSettings(&settings));
        return new VideoCaptureSettings(settings);
    }

    /// <summary>
    /// Records a clip of the most recent <paramref name="durationInMilliseconds"/> milliseconds
    /// (<c>XAppCaptureRecordTimespan</c>, <c>startTimestamp</c> = null).
    /// </summary>
    /// <param name="durationInMilliseconds">Length of the clip to capture in milliseconds.</param>
    /// <returns>
    /// A <see cref="LocalClipStream"/> that provides the clip data and metadata. Dispose it when
    /// done to release the native handle.
    /// </returns>
    public LocalClipStream RecordTimespan(ulong durationInMilliseconds)
    {
        ThrowIfDisposed();
        XAppCaptureLocalResult result;
        Hr.ThrowIfFailed(Native.XAppCaptureRecordTimespan(null, durationInMilliseconds, &result));
        return new LocalClipStream(&result);
    }

    /// <summary>
    /// Records a clip starting at <paramref name="startTimestamp"/> (UTC) with the given duration
    /// (<c>XAppCaptureRecordTimespan</c>).
    /// </summary>
    /// <param name="startTimestamp">
    /// UTC time at which the clip begins. Converted to a <c>SYSTEMTIME</c> before being passed to
    /// the runtime.
    /// </param>
    /// <param name="durationInMilliseconds">Length of the clip in milliseconds.</param>
    public LocalClipStream RecordTimespan(DateTime startTimestamp, ulong durationInMilliseconds)
    {
        ThrowIfDisposed();

        XSystemTime st = DateTimeToSystemTime(startTimestamp.ToUniversalTime());
        XAppCaptureLocalResult result;
        Hr.ThrowIfFailed(Native.XAppCaptureRecordTimespan(&st, durationInMilliseconds, &result));
        return new LocalClipStream(&result);
    }

    // ── Screenshot ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Takes a screenshot on behalf of <paramref name="user"/> and returns its local identifier and
    /// available formats (<c>XAppCaptureTakeScreenshot</c>). Pass the returned
    /// <see cref="TakeScreenshotResult.LocalId"/> to <see cref="OpenScreenshotStream"/> to read the data.
    /// </summary>
    public TakeScreenshotResult TakeScreenshot(User user)
    {
        ThrowIfDisposed();
        if (user is null) throw new ArgumentNullException(nameof(user));

        XAppCaptureTakeScreenshotResult result;
        Hr.ThrowIfFailed(Native.XAppCaptureTakeScreenshot(user.Handle, &result));
        return new TakeScreenshotResult(&result);
    }

    /// <summary>
    /// Opens a stream for reading a screenshot identified by <paramref name="localId"/>
    /// (<c>XAppCaptureOpenScreenshotStream</c>).
    /// </summary>
    /// <param name="localId">The <see cref="TakeScreenshotResult.LocalId"/> from a previous <see cref="TakeScreenshot"/> call.</param>
    /// <param name="format">Which format variant to open.</param>
    /// <returns>
    /// A <see cref="ScreenshotStream"/> providing random-access reading. Dispose when done.
    /// </returns>
    public ScreenshotStream OpenScreenshotStream(string localId, AppCaptureScreenshotFormatFlag format)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(localId)) throw new ArgumentException("localId must not be null or empty.", nameof(localId));

        IntPtr pId = Utf8.Allocate(localId);
        try
        {
            IntPtr handle;
            ulong totalBytes;
            Hr.ThrowIfFailed(Native.XAppCaptureOpenScreenshotStream(
                (byte*)pId,
                (XAppCaptureScreenshotFormatFlag)format,
                &handle,
                &totalBytes));
            return new ScreenshotStream(handle, totalBytes);
        }
        finally
        {
            Utf8.Free(pId);
        }
    }

    // ── User-controlled recording ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Starts an open-ended recording on behalf of <paramref name="user"/> and returns its local
    /// identifier (<c>XAppCaptureStartUserRecord</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Unlike <see cref="RecordTimespan(ulong)"/>, which captures a fixed window that has already
    /// elapsed, this begins recording now and runs until <see cref="StopUserRecord"/> or
    /// <see cref="CancelUserRecord"/>. Every started recording must be stopped or cancelled: the
    /// runtime keeps buffering until then.
    /// </para>
    /// </remarks>
    /// <param name="user">The user the recording is attributed to.</param>
    /// <returns>
    /// The opaque local identifier for the in-progress recording. Pass it to
    /// <see cref="StopUserRecord"/> or <see cref="CancelUserRecord"/>.
    /// </returns>
    public string StartUserRecord(User user)
    {
        ThrowIfDisposed();
        if (user is null) throw new ArgumentNullException(nameof(user));

        // APPCAPTURE_MAX_LOCALID_LENGTH from XAppCapture.h; the buffer is written null-terminated.
        const int MaxLocalIdLength = 250;
        byte[] buffer = new byte[MaxLocalIdLength];

        fixed (byte* pinned = buffer)
        {
            Hr.ThrowIfFailed(Native.XAppCaptureStartUserRecord(
                user.Handle, MaxLocalIdLength, pinned));
            return Utf8.ToString(pinned, MaxLocalIdLength) ?? string.Empty;
        }
    }

    /// <summary>
    /// Stops a recording started by <see cref="StartUserRecord"/> and returns a description of the
    /// finished clip (<c>XAppCaptureStopUserRecord</c>).
    /// </summary>
    /// <remarks>
    /// See <see cref="StartUserRecord"/>.
    /// </remarks>
    /// <param name="localId">The identifier returned by <see cref="StartUserRecord"/>.</param>
    public UserRecordingResult StopUserRecord(string localId)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(localId)) throw new ArgumentException("localId must not be null or empty.", nameof(localId));

        IntPtr pId = Utf8.Allocate(localId);
        try
        {
            XAppCaptureUserRecordingResult result;
            Hr.ThrowIfFailed(Native.XAppCaptureStopUserRecord((byte*)pId, &result));
            return new UserRecordingResult(&result);
        }
        finally
        {
            Utf8.Free(pId);
        }
    }

    /// <summary>
    /// Abandons a recording started by <see cref="StartUserRecord"/> without saving it
    /// (<c>XAppCaptureCancelUserRecord</c>).
    /// </summary>
    /// <remarks>
    /// See <see cref="StartUserRecord"/>.
    /// </remarks>
    /// <param name="localId">The identifier returned by <see cref="StartUserRecord"/>.</param>
    public void CancelUserRecord(string localId)
    {
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(localId)) throw new ArgumentException("localId must not be null or empty.", nameof(localId));

        IntPtr pId = Utf8.Allocate(localId);
        try
        {
            Hr.ThrowIfFailed(Native.XAppCaptureCancelUserRecord((byte*)pId));
        }
        finally
        {
            Utf8.Free(pId);
        }
    }

    // ── Settings ───────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Enables recording and screenshot capture for the current user
    /// (<c>XAppCaptureEnableRecord</c>).
    /// </summary>
    public void EnableRecord()
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(Native.XAppCaptureEnableRecord());
    }

    /// <summary>
    /// Disables recording and screenshot capture for the current user
    /// (<c>XAppCaptureDisableRecord</c>).
    /// </summary>
    public void DisableRecord()
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(Native.XAppCaptureDisableRecord());
    }

    // ── IDisposable ────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Unregisters all native callbacks (with <c>wait: true</c>) and releases resources. No
    /// callback can be in flight once this method returns.
    /// </summary>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed) return;
            _disposed = true;
            _broadcastingChanged = null;
            _metadataPurged = null;
            Release(ref _broadcastingRegistration);
            Release(ref _metadataRegistration);
        }
    }

    // ── Internal dispatch ──────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Entry point for <see cref="GDK.Net.Interop.Trampolines.AppCaptureContextCallback"/>. Dispatches
    /// both <c>XAppBroadcastMonitorCallback</c> and <c>XAppCaptureMetadataPurgedCallback</c> to the
    /// appropriate manager and event.
    /// </summary>
    internal static void DispatchContextCallback(IntPtr context)
    {
        if (!Registrations.TryGetValue(context, out CaptureRegistration? registration)) return;
        registration.Owner.Raise(registration.Kind);
    }

    // ── Private helpers ────────────────────────────────────────────────────────────────────────

    private CaptureRegistration Register(CaptureRegistrationKind kind)
    {
        var registration = new CaptureRegistration(this, kind);
        registration.Handle = GCHandle.Alloc(registration, GCHandleType.Normal);
        IntPtr context = GCHandle.ToIntPtr(registration.Handle);
        Registrations[context] = registration;

        XTaskQueueRegistrationToken token;
        int hr = kind == CaptureRegistrationKind.Broadcasting
            ? Native.XAppBroadcastRegisterIsAppBroadcastingChanged(
                _queue.RawHandle(), context, Trampolines.AppCaptureContextCallback, &token)
            : Native.XAppCaptureRegisterMetadataPurged(
                _queue.RawHandle(), context, Trampolines.AppCaptureContextCallback, &token);

        if (HResult.Failed(hr))
        {
            Registrations.TryRemove(context, out _);
            registration.Handle.Free();
            Hr.ThrowIfFailed(hr);
        }

        registration.Token = token;
        return registration;
    }

    private static void Release(ref CaptureRegistration? registration)
    {
        if (registration is null) return;

        // wait: true: returns only once no in-flight callback is executing.
        if (registration.Kind == CaptureRegistrationKind.Broadcasting)
        {
            Native.XAppBroadcastUnregisterIsAppBroadcastingChanged(registration.Token, wait: 1);
        }
        else
        {
            Native.XAppCaptureUnregisterMetadataPurged(registration.Token, wait: 1);
        }

        Registrations.TryRemove(GCHandle.ToIntPtr(registration.Handle), out _);
        registration.Handle.Free();
        registration = null;
    }

    private void Raise(CaptureRegistrationKind kind)
    {
        EventHandler? handler;
        lock (_gate)
        {
            if (_disposed) return;
            handler = kind == CaptureRegistrationKind.Broadcasting ? _broadcastingChanged : _metadataPurged;
        }
        handler?.Invoke(this, EventArgs.Empty);
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(AppCaptureManager));
    }

    private static XSystemTime DateTimeToSystemTime(DateTime utc) => new XSystemTime
    {
        Year = (ushort)utc.Year,
        Month = (ushort)utc.Month,
        DayOfWeek = (ushort)utc.DayOfWeek,
        Day = (ushort)utc.Day,
        Hour = (ushort)utc.Hour,
        Minute = (ushort)utc.Minute,
        Second = (ushort)utc.Second,
        Milliseconds = (ushort)utc.Millisecond,
    };

    // ── Inner types ────────────────────────────────────────────────────────────────────────────

    private enum CaptureRegistrationKind { Broadcasting, Metadata }

    private sealed class CaptureRegistration
    {
        internal CaptureRegistration(AppCaptureManager owner, CaptureRegistrationKind kind)
        {
            Owner = owner;
            Kind = kind;
        }
        internal AppCaptureManager Owner { get; }
        internal CaptureRegistrationKind Kind { get; }
        internal GCHandle Handle { get; set; }
        internal XTaskQueueRegistrationToken Token { get; set; }
    }
}
