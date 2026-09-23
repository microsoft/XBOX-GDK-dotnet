# <a id="GDK_Net_Streaming_StreamingConnectionStateChangedEventArgs"></a> Class StreamingConnectionStateChangedEventArgs

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Streaming.StreamingManager.ConnectionStateChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class StreamingConnectionStateChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[StreamingConnectionStateChangedEventArgs](GDK.Net.Streaming.StreamingConnectionStateChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_StreamingConnectionStateChangedEventArgs_Client"></a> Client

The client whose connection state changed.

```csharp
public StreamingClientId Client { get; }
```

#### Property Value

 [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

### <a id="GDK_Net_Streaming_StreamingConnectionStateChangedEventArgs_State"></a> State

The new connection state.

```csharp
public StreamingConnectionState State { get; }
```

#### Property Value

 [StreamingConnectionState](GDK.Net.Streaming.StreamingConnectionState.md)

