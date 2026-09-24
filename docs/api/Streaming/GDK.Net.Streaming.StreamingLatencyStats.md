# <a id="GDK_Net_Streaming_StreamingLatencyStats"></a> Struct StreamingLatencyStats

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Latency statistics reported by <code>XGameStreamingGetStreamAddedLatency</code>.

```csharp
public readonly struct StreamingLatencyStats
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_StreamingLatencyStats_AverageInputLatencyUs"></a> AverageInputLatencyUs

Average input latency in microseconds.

```csharp
public uint AverageInputLatencyUs { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingLatencyStats_AverageOutputLatencyUs"></a> AverageOutputLatencyUs

Average output latency in microseconds.

```csharp
public uint AverageOutputLatencyUs { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingLatencyStats_StandardDeviationUs"></a> StandardDeviationUs

Standard deviation of latency in microseconds.

```csharp
public uint StandardDeviationUs { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

