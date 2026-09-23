# <a id="GDK_Net_XboxLive_RealTimeActivityConnectionStateChangedEventArgs"></a> Class RealTimeActivityConnectionStateChangedEventArgs

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.XboxLive.RealTimeActivityService.ConnectionStateChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class RealTimeActivityConnectionStateChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[RealTimeActivityConnectionStateChangedEventArgs](GDK.Net.XboxLive.RealTimeActivityConnectionStateChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_RealTimeActivityConnectionStateChangedEventArgs_State"></a> State

The new websocket connection state.

```csharp
public RealTimeActivityConnectionState State { get; }
```

#### Property Value

 [RealTimeActivityConnectionState](GDK.Net.XboxLive.RealTimeActivityConnectionState.md)

