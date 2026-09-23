# <a id="GDK_Net_Capture"></a> Namespace GDK.Net.Capture

### Classes

 [AppCaptureManager](GDK.Net.Capture.AppCaptureManager.md)

Application capture and broadcast management. Reached through <code>GameRuntime.Capture</code>.

 [BroadcastStatus](GDK.Net.Capture.BroadcastStatus.md)

Current broadcast capability and state of the user. Returned by
<xref href="GDK.Net.Capture.AppCaptureManager.GetBroadcastStatus(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XAppBroadcastStatus</code>.

 [DiagnosticClipResult](GDK.Net.Capture.DiagnosticClipResult.md)

Result of <xref href="GDK.Net.Capture.AppCaptureManager.RecordDiagnosticClip(System.DateTimeOffset%2cSystem.UInt32%2cSystem.String)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XAppCaptureRecordClipResult</code>.

 [DiagnosticScreenshotFile](GDK.Net.Capture.DiagnosticScreenshotFile.md)

Metadata for one screenshot file in a diagnostic screenshot result.
Mirrors <code>XAppCaptureScreenshotFile</code>.

 [DiagnosticScreenshotResult](GDK.Net.Capture.DiagnosticScreenshotResult.md)

Result of <xref href="GDK.Net.Capture.AppCaptureManager.TakeDiagnosticScreenshot(System.Boolean%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag%2cSystem.String)" data-throw-if-not-resolved="false"></xref>. Contains up to
<code>APPCAPTURE_MAX_CAPTURE_FILES</code> (10) screenshot file entries.
Mirrors <code>XAppCaptureDiagnosticScreenshotResult</code>.

 [LocalClipStream](GDK.Net.Capture.LocalClipStream.md)

Provides random-access reading of a local video clip returned by
<xref href="GDK.Net.Capture.AppCaptureManager.RecordTimespan(System.UInt64)" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Capture.AppCaptureManager.RecordTimespan(System.DateTime%2cSystem.UInt64)" data-throw-if-not-resolved="false"></xref>. Owns the native
<code>XAppCaptureLocalStreamHandle</code>; dispose to release it via
<code>XAppCaptureCloseLocalStream</code>.

 [ScreenshotStream](GDK.Net.Capture.ScreenshotStream.md)

Provides random-access reading of a screenshot opened via
<xref href="GDK.Net.Capture.AppCaptureManager.OpenScreenshotStream(System.String%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag)" data-throw-if-not-resolved="false"></xref>. Owns the native
<code>XAppCaptureScreenshotStreamHandle</code>; dispose to release it via
<code>XAppCaptureCloseScreenshotStream</code>.

 [TakeScreenshotResult](GDK.Net.Capture.TakeScreenshotResult.md)

The local identifier and available formats returned by <xref href="GDK.Net.Capture.AppCaptureManager.TakeScreenshot(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.
Pass <xref href="GDK.Net.Capture.TakeScreenshotResult.LocalId" data-throw-if-not-resolved="false"></xref> and a format flag to <xref href="GDK.Net.Capture.AppCaptureManager.OpenScreenshotStream(System.String%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XAppCaptureTakeScreenshotResult</code>.

 [UserRecordingResult](GDK.Net.Capture.UserRecordingResult.md)

The finished clip produced by <xref href="GDK.Net.Capture.AppCaptureManager.StopUserRecord(System.String)" data-throw-if-not-resolved="false"></xref>.

 [VideoCaptureSettings](GDK.Net.Capture.VideoCaptureSettings.md)

Current video capture configuration. Returned by <xref href="GDK.Net.Capture.AppCaptureManager.GetVideoCaptureSettings" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XAppCaptureVideoCaptureSettings</code>.

### Enums

 [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

Priority hint for game-capture metadata events and states. Mirrors <code>XAppCaptureMetadataPriority</code>.

 [AppCaptureScreenshotFormatFlag](GDK.Net.Capture.AppCaptureScreenshotFormatFlag.md)

Screenshot format flags. Multiple flags may be set when both SDR and HDR are available.
Mirrors <code>XAppCaptureScreenshotFormatFlag</code>.

 [AppCaptureVideoColorFormat](GDK.Net.Capture.AppCaptureVideoColorFormat.md)

Color format of a captured clip. Mirrors <code>XAppCaptureVideoColorFormat</code>.

 [AppCaptureVideoEncoding](GDK.Net.Capture.AppCaptureVideoEncoding.md)

Video codec used for a captured clip. Mirrors <code>XAppCaptureVideoEncoding</code>.

