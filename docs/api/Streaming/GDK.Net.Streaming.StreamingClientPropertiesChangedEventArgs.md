# <a id="GDK_Net_Streaming_StreamingClientPropertiesChangedEventArgs"></a> Class StreamingClientPropertiesChangedEventArgs

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Streaming.StreamingManager.ClientPropertiesChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class StreamingClientPropertiesChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[StreamingClientPropertiesChangedEventArgs](GDK.Net.Streaming.StreamingClientPropertiesChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_StreamingClientPropertiesChangedEventArgs_Client"></a> Client

The client whose properties changed.

```csharp
public StreamingClientId Client { get; }
```

#### Property Value

 [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

### <a id="GDK_Net_Streaming_StreamingClientPropertiesChangedEventArgs_UpdatedProperties"></a> UpdatedProperties

Which properties changed.

```csharp
public IReadOnlyList<StreamingClientProperty> UpdatedProperties { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StreamingClientProperty](GDK.Net.Streaming.StreamingClientProperty.md)\>

