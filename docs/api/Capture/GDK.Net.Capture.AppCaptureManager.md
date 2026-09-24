# <a id="GDK_Net_Capture_AppCaptureManager"></a> Class AppCaptureManager

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Application capture and broadcast management. Reached through <code>GameRuntime.Capture</code>.

```csharp
public sealed class AppCaptureManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AppCaptureManager](GDK.Net.Capture.AppCaptureManager.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Both event registrations (<xref href="GDK.Net.Capture.AppCaptureManager.BroadcastingChanged" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.Capture.AppCaptureManager.MetadataPurged" data-throw-if-not-resolved="false"></xref>)
are created lazily on the first subscriber and released on <xref href="GDK.Net.Capture.AppCaptureManager.Dispose" data-throw-if-not-resolved="false"></xref> with
<code>wait: true</code> so no callback can be in flight once the manager is disposed.
</p>
<p>
The following APIs are deliberately absent because <code>xgameruntime.thunks.dll</code> does not
export them (they are among the 49 functions present in <code>xgameruntime.lib</code> but absent
from the redistributable DLL): <code>XAppCaptureStartUserRecord</code>,
<code>XAppCaptureStopUserRecord</code>, and <code>XAppCaptureCancelUserRecord</code>. Binding any of them
would throw <xref href="System.EntryPointNotFoundException" data-throw-if-not-resolved="false"></xref>, which the runtime surfaces as
<code>E_GAMERUNTIME_VERSION_MISMATCH</code>, a misleading environment error rather than a
missing-API error.
</p>

## Methods

### <a id="GDK_Net_Capture_AppCaptureManager_AddMetadataDouble_System_String_System_Double_GDK_Net_Capture_AppCaptureMetadataPriority_"></a> AddMetadataDouble\(string, double, AppCaptureMetadataPriority\)

Appends a one-shot double metadata event (<code>XAppCaptureMetadataAddDoubleEvent</code>).

```csharp
public void AddMetadataDouble(string name, double value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

UTF-8 event name. Must not be null or empty.

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

Double value.

`priority` [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

Storage priority when the buffer is under pressure.

### <a id="GDK_Net_Capture_AppCaptureManager_AddMetadataInt32_System_String_System_Int32_GDK_Net_Capture_AppCaptureMetadataPriority_"></a> AddMetadataInt32\(string, int, AppCaptureMetadataPriority\)

Appends a one-shot int32 metadata event (<code>XAppCaptureMetadataAddInt32Event</code>).

```csharp
public void AddMetadataInt32(string name, int value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

UTF-8 event name. Must not be null or empty.

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Integer value.

`priority` [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

Storage priority when the buffer is under pressure.

### <a id="GDK_Net_Capture_AppCaptureManager_AddMetadataString_System_String_System_String_GDK_Net_Capture_AppCaptureMetadataPriority_"></a> AddMetadataString\(string, string, AppCaptureMetadataPriority\)

Appends a one-shot string metadata event (<code>XAppCaptureMetadataAddStringEvent</code>).

```csharp
public void AddMetadataString(string name, string value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

UTF-8 event name. Must not be null or empty.

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

UTF-8 string value.

`priority` [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

Storage priority when the buffer is under pressure.

### <a id="GDK_Net_Capture_AppCaptureManager_CancelUserRecord_System_String_"></a> CancelUserRecord\(string\)

Abandons a recording started by <xref href="GDK.Net.Capture.AppCaptureManager.StartUserRecord(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref> without saving it
(<code>XAppCaptureCancelUserRecord</code>).

```csharp
public void CancelUserRecord(string localId)
```

#### Parameters

`localId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier returned by <xref href="GDK.Net.Capture.AppCaptureManager.StartUserRecord(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.

#### Remarks

See <xref href="GDK.Net.Capture.AppCaptureManager.StartUserRecord(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Capture_AppCaptureManager_DisableRecord"></a> DisableRecord\(\)

Disables recording and screenshot capture for the current user
(<code>XAppCaptureDisableRecord</code>).

```csharp
public void DisableRecord()
```

### <a id="GDK_Net_Capture_AppCaptureManager_Dispose"></a> Dispose\(\)

Unregisters all native callbacks (with <code>wait: true</code>) and releases resources. No
callback can be in flight once this method returns.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Capture_AppCaptureManager_EnableRecord"></a> EnableRecord\(\)

Enables recording and screenshot capture for the current user
(<code>XAppCaptureEnableRecord</code>).

```csharp
public void EnableRecord()
```

### <a id="GDK_Net_Capture_AppCaptureManager_GetBroadcastStatus_GDK_Net_Users_User_"></a> GetBroadcastStatus\(User\)

Returns the current broadcast status for <code class="paramref">user</code>
(<code>XAppBroadcastGetStatus</code>).

```csharp
public BroadcastStatus GetBroadcastStatus(User user)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

#### Returns

 [BroadcastStatus](GDK.Net.Capture.BroadcastStatus.md)

### <a id="GDK_Net_Capture_AppCaptureManager_GetMetadataRemainingStorageBytes"></a> GetMetadataRemainingStorageBytes\(\)

Returns the number of bytes remaining in the metadata storage buffer
(<code>XAppCaptureMetadataRemainingStorageBytesAvailable</code>).

```csharp
public ulong GetMetadataRemainingStorageBytes()
```

#### Returns

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Capture_AppCaptureManager_GetVideoCaptureSettings"></a> GetVideoCaptureSettings\(\)

Returns the current video capture configuration (<code>XAppCaptureGetVideoCaptureSettings</code>).

```csharp
public VideoCaptureSettings GetVideoCaptureSettings()
```

#### Returns

 [VideoCaptureSettings](GDK.Net.Capture.VideoCaptureSettings.md)

### <a id="GDK_Net_Capture_AppCaptureManager_IsAppBroadcasting"></a> IsAppBroadcasting\(\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when any app in the system is currently broadcasting
(<code>XAppBroadcastIsAppBroadcasting</code>).

```csharp
public bool IsAppBroadcasting()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_AppCaptureManager_OpenScreenshotStream_System_String_GDK_Net_Capture_AppCaptureScreenshotFormatFlag_"></a> OpenScreenshotStream\(string, AppCaptureScreenshotFormatFlag\)

Opens a stream for reading a screenshot identified by <code class="paramref">localId</code>
(<code>XAppCaptureOpenScreenshotStream</code>).

```csharp
public ScreenshotStream OpenScreenshotStream(string localId, AppCaptureScreenshotFormatFlag format)
```

#### Parameters

`localId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The <xref href="GDK.Net.Capture.TakeScreenshotResult.LocalId" data-throw-if-not-resolved="false"></xref> from a previous <xref href="GDK.Net.Capture.AppCaptureManager.TakeScreenshot(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref> call.

`format` [AppCaptureScreenshotFormatFlag](GDK.Net.Capture.AppCaptureScreenshotFormatFlag.md)

Which format variant to open.

#### Returns

 [ScreenshotStream](GDK.Net.Capture.ScreenshotStream.md)

A <xref href="GDK.Net.Capture.ScreenshotStream" data-throw-if-not-resolved="false"></xref> providing random-access reading. Dispose when done.

### <a id="GDK_Net_Capture_AppCaptureManager_RecordDiagnosticClip_System_DateTimeOffset_System_UInt32_System_String_"></a> RecordDiagnosticClip\(DateTimeOffset, uint, string?\)

Records a diagnostic clip starting at <code class="paramref">startTime</code> (UTC)
(<code>XAppCaptureRecordDiagnosticClip</code>).

```csharp
public DiagnosticClipResult RecordDiagnosticClip(DateTimeOffset startTime, uint durationInMs, string? filenamePrefix = null)
```

#### Parameters

`startTime` [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

Clip start time in UTC. Converted to a Unix <code>time_t</code>.

`durationInMs` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Clip duration in milliseconds.

`filenamePrefix` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional filename prefix for the output file.

#### Returns

 [DiagnosticClipResult](GDK.Net.Capture.DiagnosticClipResult.md)

### <a id="GDK_Net_Capture_AppCaptureManager_RecordTimespan_System_UInt64_"></a> RecordTimespan\(ulong\)

Records a clip of the most recent <code class="paramref">durationInMilliseconds</code> milliseconds
(<code>XAppCaptureRecordTimespan</code>, <code>startTimestamp</code> = null).

```csharp
public LocalClipStream RecordTimespan(ulong durationInMilliseconds)
```

#### Parameters

`durationInMilliseconds` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Length of the clip to capture in milliseconds.

#### Returns

 [LocalClipStream](GDK.Net.Capture.LocalClipStream.md)

A <xref href="GDK.Net.Capture.LocalClipStream" data-throw-if-not-resolved="false"></xref> that provides the clip data and metadata. Dispose it when
done to release the native handle.

### <a id="GDK_Net_Capture_AppCaptureManager_RecordTimespan_System_DateTime_System_UInt64_"></a> RecordTimespan\(DateTime, ulong\)

Records a clip starting at <code class="paramref">startTimestamp</code> (UTC) with the given duration
(<code>XAppCaptureRecordTimespan</code>).

```csharp
public LocalClipStream RecordTimespan(DateTime startTimestamp, ulong durationInMilliseconds)
```

#### Parameters

`startTimestamp` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

UTC time at which the clip begins. Converted to a <code>SYSTEMTIME</code> before being passed to
the runtime.

`durationInMilliseconds` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Length of the clip in milliseconds.

#### Returns

 [LocalClipStream](GDK.Net.Capture.LocalClipStream.md)

### <a id="GDK_Net_Capture_AppCaptureManager_ShowBroadcastUi_GDK_Net_Users_User_"></a> ShowBroadcastUi\(User\)

Shows the system broadcasting UI for <code class="paramref">user</code> (<code>XAppBroadcastShowUI</code>).

```csharp
public void ShowBroadcastUi(User user)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

The user requesting the broadcast UI.

### <a id="GDK_Net_Capture_AppCaptureManager_StartMetadataDoubleState_System_String_System_Double_GDK_Net_Capture_AppCaptureMetadataPriority_"></a> StartMetadataDoubleState\(string, double, AppCaptureMetadataPriority\)

Starts a persistent double metadata state (<code>XAppCaptureMetadataStartDoubleState</code>).

```csharp
public void StartMetadataDoubleState(string name, double value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

`priority` [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

### <a id="GDK_Net_Capture_AppCaptureManager_StartMetadataInt32State_System_String_System_Int32_GDK_Net_Capture_AppCaptureMetadataPriority_"></a> StartMetadataInt32State\(string, int, AppCaptureMetadataPriority\)

Starts a persistent int32 metadata state (<code>XAppCaptureMetadataStartInt32State</code>).

```csharp
public void StartMetadataInt32State(string name, int value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`priority` [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

### <a id="GDK_Net_Capture_AppCaptureManager_StartMetadataStringState_System_String_System_String_GDK_Net_Capture_AppCaptureMetadataPriority_"></a> StartMetadataStringState\(string, string, AppCaptureMetadataPriority\)

Starts a persistent string metadata state (<code>XAppCaptureMetadataStartStringState</code>).
Call <xref href="GDK.Net.Capture.AppCaptureManager.StopMetadataState(System.String)" data-throw-if-not-resolved="false"></xref> or <xref href="GDK.Net.Capture.AppCaptureManager.StopAllMetadataStates" data-throw-if-not-resolved="false"></xref> to end it.

```csharp
public void StartMetadataStringState(string name, string value, AppCaptureMetadataPriority priority = AppCaptureMetadataPriority.Informational)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

`priority` [AppCaptureMetadataPriority](GDK.Net.Capture.AppCaptureMetadataPriority.md)

### <a id="GDK_Net_Capture_AppCaptureManager_StartUserRecord_GDK_Net_Users_User_"></a> StartUserRecord\(User\)

Starts an open-ended recording on behalf of <code class="paramref">user</code> and returns its local
identifier (<code>XAppCaptureStartUserRecord</code>).

```csharp
public string StartUserRecord(User user)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

The user the recording is attributed to.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque local identifier for the in-progress recording. Pass it to
<xref href="GDK.Net.Capture.AppCaptureManager.StopUserRecord(System.String)" data-throw-if-not-resolved="false"></xref> or <xref href="GDK.Net.Capture.AppCaptureManager.CancelUserRecord(System.String)" data-throw-if-not-resolved="false"></xref>.

#### Remarks

<p>
Unlike <xref href="GDK.Net.Capture.AppCaptureManager.RecordTimespan(System.UInt64)" data-throw-if-not-resolved="false"></xref>, which captures a fixed window that has already
elapsed, this begins recording now and runs until <xref href="GDK.Net.Capture.AppCaptureManager.StopUserRecord(System.String)" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Capture.AppCaptureManager.CancelUserRecord(System.String)" data-throw-if-not-resolved="false"></xref>. Every started recording must be stopped or cancelled: the
runtime keeps buffering until then.
</p>

### <a id="GDK_Net_Capture_AppCaptureManager_StopAllMetadataStates"></a> StopAllMetadataStates\(\)

Stops all active persistent metadata states (<code>XAppCaptureMetadataStopAllStates</code>).

```csharp
public void StopAllMetadataStates()
```

### <a id="GDK_Net_Capture_AppCaptureManager_StopMetadataState_System_String_"></a> StopMetadataState\(string\)

Stops the named persistent metadata state (<code>XAppCaptureMetadataStopState</code>).

```csharp
public void StopMetadataState(string name)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

The name that was passed to the corresponding <code>StartMetadata*State</code> call.

### <a id="GDK_Net_Capture_AppCaptureManager_StopUserRecord_System_String_"></a> StopUserRecord\(string\)

Stops a recording started by <xref href="GDK.Net.Capture.AppCaptureManager.StartUserRecord(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref> and returns a description of the
finished clip (<code>XAppCaptureStopUserRecord</code>).

```csharp
public UserRecordingResult StopUserRecord(string localId)
```

#### Parameters

`localId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier returned by <xref href="GDK.Net.Capture.AppCaptureManager.StartUserRecord(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [UserRecordingResult](GDK.Net.Capture.UserRecordingResult.md)

#### Remarks

See <xref href="GDK.Net.Capture.AppCaptureManager.StartUserRecord(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Capture_AppCaptureManager_TakeDiagnosticScreenshot_System_Boolean_GDK_Net_Capture_AppCaptureScreenshotFormatFlag_System_String_"></a> TakeDiagnosticScreenshot\(bool, AppCaptureScreenshotFormatFlag, string?\)

Takes a diagnostic screenshot and writes it to disk
(<code>XAppCaptureTakeDiagnosticScreenshot</code>).

```csharp
public DiagnosticScreenshotResult TakeDiagnosticScreenshot(bool gamescreenOnly, AppCaptureScreenshotFormatFlag captureFlags, string? filenamePrefix = null)
```

#### Parameters

`gamescreenOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, captures only the game surface (not system overlays).

`captureFlags` [AppCaptureScreenshotFormatFlag](GDK.Net.Capture.AppCaptureScreenshotFormatFlag.md)

Which format(s) to capture.

`filenamePrefix` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional filename prefix for the output files.

#### Returns

 [DiagnosticScreenshotResult](GDK.Net.Capture.DiagnosticScreenshotResult.md)

### <a id="GDK_Net_Capture_AppCaptureManager_TakeScreenshot_GDK_Net_Users_User_"></a> TakeScreenshot\(User\)

Takes a screenshot on behalf of <code class="paramref">user</code> and returns its local identifier and
available formats (<code>XAppCaptureTakeScreenshot</code>). Pass the returned
<xref href="GDK.Net.Capture.TakeScreenshotResult.LocalId" data-throw-if-not-resolved="false"></xref> to <xref href="GDK.Net.Capture.AppCaptureManager.OpenScreenshotStream(System.String%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag)" data-throw-if-not-resolved="false"></xref> to read the data.

```csharp
public TakeScreenshotResult TakeScreenshot(User user)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

#### Returns

 [TakeScreenshotResult](GDK.Net.Capture.TakeScreenshotResult.md)

### <a id="GDK_Net_Capture_AppCaptureManager_BroadcastingChanged"></a> BroadcastingChanged

Raised when the app's broadcasting state changes
(<code>XAppBroadcastRegisterIsAppBroadcastingChanged</code>).

```csharp
public event EventHandler? BroadcastingChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler)?

#### Remarks

Delivered on the manager's task queue. Subscription lazily registers the native callback;
the registration is released with <code>wait: true</code> on <xref href="GDK.Net.Capture.AppCaptureManager.Dispose" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Capture_AppCaptureManager_MetadataPurged"></a> MetadataPurged

Raised when the capture metadata buffer is purged due to storage pressure
(<code>XAppCaptureRegisterMetadataPurged</code>).

```csharp
public event EventHandler? MetadataPurged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler)?

