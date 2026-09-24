# <a id="GDK_Net_Streaming_StreamingManager"></a> Class StreamingManager

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Game streaming features: connection state, touch-controls, client properties and display
details.

```csharp
public sealed class StreamingManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StreamingManager](GDK.Net.Streaming.StreamingManager.md)

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
Call <xref href="GDK.Net.Streaming.StreamingManager.Initialize" data-throw-if-not-resolved="false"></xref> once after <code>GameRuntime.Initialize()</code> and dispose before
<code>GameRuntime.Dispose()</code>. The manager calls <code>XGameStreamingInitialize</code> on entry and
<code>XGameStreamingUninitialize</code> on dispose.
</p>
<p>
Connection-state and client-property-change events share the same pattern used by
<code>UserManager</code> and <code>GameActivationManager</code>: a global trampoline dispatches to a
static dictionary keyed by a <xref href="System.Runtime.InteropServices.GCHandle" data-throw-if-not-resolved="false"></xref> stored in the native context pointer.
Event registrations are created lazily on the first subscriber and released on
<xref href="GDK.Net.Streaming.StreamingManager.Dispose" data-throw-if-not-resolved="false"></xref> with <code>wait: true</code>, so no callback can be in flight once the
manager is gone.
</p>
<p>
Per-client property-change notifications are opt-in. Call
<xref href="GDK.Net.Streaming.StreamingManager.WatchClientProperties(GDK.Net.Streaming.StreamingClientId)" data-throw-if-not-resolved="false"></xref> for each client whose property changes you want; the
manager registers the native callback for that client and delivers events through
<xref href="GDK.Net.Streaming.StreamingManager.ClientPropertiesChanged" data-throw-if-not-resolved="false"></xref>.
</p>

## Properties

### <a id="GDK_Net_Streaming_StreamingManager_IsStreaming"></a> IsStreaming

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the game is currently being streamed
(<code>XGameStreamingIsStreaming</code>).

```csharp
public bool IsStreaming { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_Streaming_StreamingManager_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Streaming_StreamingManager_GetClientCount"></a> GetClientCount\(\)

Returns the number of currently connected streaming clients
(<code>XGameStreamingGetClientCount</code>).

```csharp
public uint GetClientCount()
```

#### Returns

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingManager_GetClients"></a> GetClients\(\)

Returns the ids of all currently connected clients (<code>XGameStreamingGetClients</code>).

```csharp
public IReadOnlyList<StreamingClientId> GetClients()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)\>

### <a id="GDK_Net_Streaming_StreamingManager_GetConnectionState_GDK_Net_Streaming_StreamingClientId_"></a> GetConnectionState\(StreamingClientId\)

Returns the current connection state of <code class="paramref">client</code>
(<code>XGameStreamingGetConnectionState</code>).

```csharp
public StreamingConnectionState GetConnectionState(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [StreamingConnectionState](GDK.Net.Streaming.StreamingConnectionState.md)

### <a id="GDK_Net_Streaming_StreamingManager_GetDisplayDetails_GDK_Net_Streaming_StreamingClientId_System_UInt32_System_Single_System_Single_"></a> GetDisplayDetails\(StreamingClientId, uint, float, float\)

Returns the display details for a streaming client
(<code>XGameStreamingGetDisplayDetails</code>).

```csharp
public StreamingDisplayDetails GetDisplayDetails(StreamingClientId client, uint maxSupportedPixels, float widestSupportedAspectRatio, float tallestSupportedAspectRatio)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

The streaming client to query.

`maxSupportedPixels` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The maximum pixel count this title can render. Pass <code>0</code> for no limit.

`widestSupportedAspectRatio` [float](https://learn.microsoft.com/dotnet/api/system.single)

Widest aspect ratio the title supports (e.g. 16/9f).

`tallestSupportedAspectRatio` [float](https://learn.microsoft.com/dotnet/api/system.single)

Tallest aspect ratio the title supports (e.g. 9/16f).

#### Returns

 [StreamingDisplayDetails](GDK.Net.Streaming.StreamingDisplayDetails.md)

### <a id="GDK_Net_Streaming_StreamingManager_GetGamepadPhysicality_System_IntPtr_"></a> GetGamepadPhysicality\(nint\)

Reports which inputs in a gamepad reading came from physical hardware and which were
synthesised by an on-screen touch layout (<code>XGameStreamingGetGamepadPhysicality</code>).

```csharp
public StreamingGamepadPhysicality GetGamepadPhysicality(nint gamepadReading)
```

#### Parameters

`gamepadReading` [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

A non-null <code>IGameInputReading*</code> for a gamepad reading.

#### Returns

 [StreamingGamepadPhysicality](GDK.Net.Streaming.StreamingGamepadPhysicality.md)

#### Remarks

<p>
<code class="paramref">gamepadReading</code> is an opaque <code>IGameInputReading*</code>. GameInput is out
of scope for this projection, so the pointer is passed through untouched: obtain it from
your own GameInput binding and keep the reading alive across this call. The runtime only
inspects it; it is never retained.
</p>

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">gamepadReading</code> is <code>IntPtr.Zero</code>.

### <a id="GDK_Net_Streaming_StreamingManager_GetServerLocationName"></a> GetServerLocationName\(\)

Returns the streaming server's location name, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when streaming is
not active (<code>XGameStreamingGetServerLocationName</code>).

```csharp
public string? GetServerLocationName()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_Streaming_StreamingManager_GetSessionId_GDK_Net_Streaming_StreamingClientId_"></a> GetSessionId\(StreamingClientId\)

Returns the session id of a streaming client
(<code>XGameStreamingGetSessionId</code>).

```csharp
public string GetSessionId(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Streaming_StreamingManager_GetStreamAddedLatency_GDK_Net_Streaming_StreamingClientId_"></a> GetStreamAddedLatency\(StreamingClientId\)

Returns the most recent latency measurements for a streaming client
(<code>XGameStreamingGetStreamAddedLatency</code>).

```csharp
public StreamingLatencyStats GetStreamAddedLatency(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [StreamingLatencyStats](GDK.Net.Streaming.StreamingLatencyStats.md)

### <a id="GDK_Net_Streaming_StreamingManager_GetStreamPhysicalDimensions_GDK_Net_Streaming_StreamingClientId_"></a> GetStreamPhysicalDimensions\(StreamingClientId\)

Returns the physical display dimensions (in mm) of a streaming client's device
(<code>XGameStreamingGetStreamPhysicalDimensions</code>).

```csharp
public StreamingPhysicalDimensions GetStreamPhysicalDimensions(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [StreamingPhysicalDimensions](GDK.Net.Streaming.StreamingPhysicalDimensions.md)

### <a id="GDK_Net_Streaming_StreamingManager_GetTouchBundleVersion_GDK_Net_Streaming_StreamingClientId_"></a> GetTouchBundleVersion\(StreamingClientId\)

Returns the version and name of the touch-adaptation bundle active on a streaming client
(<code>XGameStreamingGetTouchBundleVersion</code>), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when no bundle is
active.

```csharp
public TouchBundleVersionInfo? GetTouchBundleVersion(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [TouchBundleVersionInfo](GDK.Net.Streaming.TouchBundleVersionInfo.md)?

### <a id="GDK_Net_Streaming_StreamingManager_HideTouchControls"></a> HideTouchControls\(\)

Hides touch controls on all connected clients (<code>XGameStreamingHideTouchControls</code>).

```csharp
public void HideTouchControls()
```

### <a id="GDK_Net_Streaming_StreamingManager_HideTouchControls_GDK_Net_Streaming_StreamingClientId_"></a> HideTouchControls\(StreamingClientId\)

Hides touch controls on a specific client (<code>XGameStreamingHideTouchControlsOnClient</code>).

```csharp
public void HideTouchControls(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

### <a id="GDK_Net_Streaming_StreamingManager_Initialize"></a> Initialize\(\)

Initializes the XGameStreaming subsystem (<code>XGameStreamingInitialize</code>). Must be called
after <code>GameRuntime.Initialize()</code>.

```csharp
public static StreamingManager Initialize()
```

#### Returns

 [StreamingManager](GDK.Net.Streaming.StreamingManager.md)

#### Remarks

Event registrations name no task queue, so the Gaming Runtime resolves the process default.

### <a id="GDK_Net_Streaming_StreamingManager_IsTouchInputEnabled_GDK_Net_Streaming_StreamingClientId_"></a> IsTouchInputEnabled\(StreamingClientId\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the streaming client will send touch input to the game
(<code>XGameStreamingIsTouchInputEnabled</code>).

```csharp
public bool IsTouchInputEnabled(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Streaming_StreamingManager_SetResolution_System_UInt32_System_UInt32_"></a> SetResolution\(uint, uint\)

Sets the stream resolution (<code>XGameStreamingSetResolution</code>).
Throws <xref href="GDK.Net.GameRuntimeException" data-throw-if-not-resolved="false"></xref> when the resolution is not supported.

```csharp
public void SetResolution(uint width, uint height)
```

#### Parameters

`width` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`height` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingManager_ShowTouchControlLayout_System_String_"></a> ShowTouchControlLayout\(string?\)

Shows the named touch-control layout on all clients
(<code>XGameStreamingShowTouchControlLayout</code>).

```csharp
public void ShowTouchControlLayout(string? layout)
```

#### Parameters

`layout` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Name of the layout defined in the touch-adaptation bundle, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to
show the default layout.

### <a id="GDK_Net_Streaming_StreamingManager_ShowTouchControlLayout_GDK_Net_Streaming_StreamingClientId_System_String_"></a> ShowTouchControlLayout\(StreamingClientId, string?\)

Shows the named touch-control layout on a specific client
(<code>XGameStreamingShowTouchControlLayoutOnClient</code>).

```csharp
public void ShowTouchControlLayout(StreamingClientId client, string? layout)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

`layout` [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_Streaming_StreamingManager_ShowTouchControlsWithStateUpdate_System_String_System_Collections_Generic_IReadOnlyList_GDK_Net_Streaming_TouchControlsStateOperation__"></a> ShowTouchControlsWithStateUpdate\(string?, IReadOnlyList<TouchControlsStateOperation\>?\)

Shows a layout and updates state variables simultaneously on all clients
(<code>XGameStreamingShowTouchControlsWithStateUpdate</code>).

```csharp
public void ShowTouchControlsWithStateUpdate(string? layout, IReadOnlyList<TouchControlsStateOperation>? operations)
```

#### Parameters

`layout` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`operations` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TouchControlsStateOperation](GDK.Net.Streaming.TouchControlsStateOperation.md)\>?

### <a id="GDK_Net_Streaming_StreamingManager_ShowTouchControlsWithStateUpdate_GDK_Net_Streaming_StreamingClientId_System_String_System_Collections_Generic_IReadOnlyList_GDK_Net_Streaming_TouchControlsStateOperation__"></a> ShowTouchControlsWithStateUpdate\(StreamingClientId, string?, IReadOnlyList<TouchControlsStateOperation\>?\)

Shows a layout and updates state variables simultaneously on a specific client
(<code>XGameStreamingShowTouchControlsWithStateUpdateOnClient</code>).

```csharp
public void ShowTouchControlsWithStateUpdate(StreamingClientId client, string? layout, IReadOnlyList<TouchControlsStateOperation>? operations)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

`layout` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`operations` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TouchControlsStateOperation](GDK.Net.Streaming.TouchControlsStateOperation.md)\>?

### <a id="GDK_Net_Streaming_StreamingManager_UnwatchClientProperties_GDK_Net_Streaming_StreamingClientId_"></a> UnwatchClientProperties\(StreamingClientId\)

Unregisters property-change notifications for <code class="paramref">client</code>
(<code>XGameStreamingUnregisterClientPropertiesChanged</code>). Idempotent.

```csharp
public void UnwatchClientProperties(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

### <a id="GDK_Net_Streaming_StreamingManager_UpdateTouchControlsState_System_Collections_Generic_IReadOnlyList_GDK_Net_Streaming_TouchControlsStateOperation__"></a> UpdateTouchControlsState\(IReadOnlyList<TouchControlsStateOperation\>?\)

Updates touch-control state variables on all clients
(<code>XGameStreamingUpdateTouchControlsState</code>).

```csharp
public void UpdateTouchControlsState(IReadOnlyList<TouchControlsStateOperation>? operations)
```

#### Parameters

`operations` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TouchControlsStateOperation](GDK.Net.Streaming.TouchControlsStateOperation.md)\>?

### <a id="GDK_Net_Streaming_StreamingManager_UpdateTouchControlsState_GDK_Net_Streaming_StreamingClientId_System_Collections_Generic_IReadOnlyList_GDK_Net_Streaming_TouchControlsStateOperation__"></a> UpdateTouchControlsState\(StreamingClientId, IReadOnlyList<TouchControlsStateOperation\>?\)

Updates touch-control state variables on a specific client
(<code>XGameStreamingUpdateTouchControlsStateOnClient</code>).

```csharp
public void UpdateTouchControlsState(StreamingClientId client, IReadOnlyList<TouchControlsStateOperation>? operations)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

`operations` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TouchControlsStateOperation](GDK.Net.Streaming.TouchControlsStateOperation.md)\>?

### <a id="GDK_Net_Streaming_StreamingManager_WatchClientProperties_GDK_Net_Streaming_StreamingClientId_"></a> WatchClientProperties\(StreamingClientId\)

Registers for property-change notifications for <code class="paramref">client</code>
(<code>XGameStreamingRegisterClientPropertiesChanged</code>). Events fire through
<xref href="GDK.Net.Streaming.StreamingManager.ClientPropertiesChanged" data-throw-if-not-resolved="false"></xref>. Idempotent: safe to call more than once for the
same client.

```csharp
public void WatchClientProperties(StreamingClientId client)
```

#### Parameters

`client` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

### <a id="GDK_Net_Streaming_StreamingManager_ClientPropertiesChanged"></a> ClientPropertiesChanged

Raised when a watched streaming client's properties change
(<code>XGameStreamingRegisterClientPropertiesChanged</code>).
Call <xref href="GDK.Net.Streaming.StreamingManager.WatchClientProperties(GDK.Net.Streaming.StreamingClientId)" data-throw-if-not-resolved="false"></xref> to opt a client in.
Delivered on the manager's task queue.

```csharp
public event EventHandler<StreamingClientPropertiesChangedEventArgs>? ClientPropertiesChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[StreamingClientPropertiesChangedEventArgs](GDK.Net.Streaming.StreamingClientPropertiesChangedEventArgs.md)\>?

### <a id="GDK_Net_Streaming_StreamingManager_ConnectionStateChanged"></a> ConnectionStateChanged

Raised when a streaming client connects or disconnects
(<code>XGameStreamingRegisterConnectionStateChanged</code>).
Delivered on the manager's task queue.

```csharp
public event EventHandler<StreamingConnectionStateChangedEventArgs>? ConnectionStateChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[StreamingConnectionStateChangedEventArgs](GDK.Net.Streaming.StreamingConnectionStateChangedEventArgs.md)\>?

