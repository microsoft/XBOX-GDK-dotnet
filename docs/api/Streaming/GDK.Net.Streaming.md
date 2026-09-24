# <a id="GDK_Net_Streaming"></a> Namespace GDK.Net.Streaming

### Classes

 [StreamingClientPropertiesChangedEventArgs](GDK.Net.Streaming.StreamingClientPropertiesChangedEventArgs.md)

Payload for <xref href="GDK.Net.Streaming.StreamingManager.ClientPropertiesChanged" data-throw-if-not-resolved="false"></xref>.

 [StreamingConnectionStateChangedEventArgs](GDK.Net.Streaming.StreamingConnectionStateChangedEventArgs.md)

Payload for <xref href="GDK.Net.Streaming.StreamingManager.ConnectionStateChanged" data-throw-if-not-resolved="false"></xref>.

 [StreamingManager](GDK.Net.Streaming.StreamingManager.md)

Game streaming features: connection state, touch-controls, client properties and display
details.

 [TouchBundleVersionInfo](GDK.Net.Streaming.TouchBundleVersionInfo.md)

Version and name of the touch-adaptation bundle active on a streaming client.
Returned by <xref href="GDK.Net.Streaming.StreamingManager.GetTouchBundleVersion(GDK.Net.Streaming.StreamingClientId)" data-throw-if-not-resolved="false"></xref>.

 [TouchControlsStateOperation](GDK.Net.Streaming.TouchControlsStateOperation.md)

A single touch-controls state update operation passed to
<xref href="GDK.Net.Streaming.StreamingManager.UpdateTouchControlsState(System.Collections.Generic.IReadOnlyList%7bGDK.Net.Streaming.TouchControlsStateOperation%7d)" data-throw-if-not-resolved="false"></xref> and related methods.
Mirrors <code>XGameStreamingTouchControlsStateOperation</code>.

### Structs

 [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

Identifies a streaming client connected to the game. Wraps the native
<code>XGameStreamingClientId</code> (a <code>uint64_t</code>).

 [StreamingDisplayDetails](GDK.Net.Streaming.StreamingDisplayDetails.md)

Display details for a streaming client, returned by
<xref href="GDK.Net.Streaming.StreamingManager.GetDisplayDetails(GDK.Net.Streaming.StreamingClientId%2cSystem.UInt32%2cSystem.Single%2cSystem.Single)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XGameStreamingDisplayDetails</code>.

 [StreamingLatencyStats](GDK.Net.Streaming.StreamingLatencyStats.md)

Latency statistics reported by <code>XGameStreamingGetStreamAddedLatency</code>.

 [StreamingPhysicalDimensions](GDK.Net.Streaming.StreamingPhysicalDimensions.md)

Physical dimensions of the streaming client's display, in millimetres.

 [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

A typed value for a touch-controls state operation.
Use the static factory methods <xref href="GDK.Net.Streaming.TouchControlsStateValue.FromBoolean(System.Boolean)" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.Streaming.TouchControlsStateValue.FromInteger(System.Int64)" data-throw-if-not-resolved="false"></xref>,
<xref href="GDK.Net.Streaming.TouchControlsStateValue.FromDouble(System.Double)" data-throw-if-not-resolved="false"></xref>, and <xref href="GDK.Net.Streaming.TouchControlsStateValue.FromString(System.String)" data-throw-if-not-resolved="false"></xref> to construct instances.

### Enums

 [StreamingClientProperty](GDK.Net.Streaming.StreamingClientProperty.md)

Which client property changed. Mirrors <code>XGameStreamingClientProperty</code>.

 [StreamingConnectionState](GDK.Net.Streaming.StreamingConnectionState.md)

The connection state of a streaming client. Mirrors <code>XGameStreamingConnectionState</code>.

 [StreamingGamepadPhysicality](GDK.Net.Streaming.StreamingGamepadPhysicality.md)

Reports which gamepad inputs came from physical hardware and which were synthesised by an
on-screen touch layout (<code>XGameStreamingGamepadPhysicality</code>).

 [StreamingVideoFlags](GDK.Net.Streaming.StreamingVideoFlags.md)

Video capabilities reported by a streaming client. Mirrors <code>XGameStreamingVideoFlags</code>.

 [TouchControlsStateOperationKind](GDK.Net.Streaming.TouchControlsStateOperationKind.md)

The kind of touch-controls state operation. Mirrors <code>XGameStreamingTouchControlsStateOperationKind</code>.

 [TouchControlsStateValueKind](GDK.Net.Streaming.TouchControlsStateValueKind.md)

The kind of value carried by a <xref href="GDK.Net.Streaming.TouchControlsStateValue" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XGameStreamingTouchControlsStateValueKind</code>.

