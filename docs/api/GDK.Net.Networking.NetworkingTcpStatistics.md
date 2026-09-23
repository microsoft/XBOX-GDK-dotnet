# <a id="GDK_Net_Networking_NetworkingTcpStatistics"></a> Struct NetworkingTcpStatistics

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

TCP receive-buffer usage statistics. Mirrors <code>XNetworkingTcpQueuedReceivedBufferUsageStatistics</code>.

```csharp
public readonly struct NetworkingTcpStatistics
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Networking_NetworkingTcpStatistics_NumBytesCurrentlyQueued"></a> NumBytesCurrentlyQueued

Bytes currently sitting in the queued receive buffer.

```csharp
public ulong NumBytesCurrentlyQueued { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Networking_NetworkingTcpStatistics_NumBytesDroppedDueToAnyFailure"></a> NumBytesDroppedDueToAnyFailure

Bytes dropped for any failure reason.

```csharp
public ulong NumBytesDroppedDueToAnyFailure { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Networking_NetworkingTcpStatistics_NumBytesDroppedForExceedingConfiguredMax"></a> NumBytesDroppedForExceedingConfiguredMax

Bytes dropped because the configured maximum was exceeded.

```csharp
public ulong NumBytesDroppedForExceedingConfiguredMax { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Networking_NetworkingTcpStatistics_PeakNumBytesEverQueued"></a> PeakNumBytesEverQueued

Peak number of bytes ever simultaneously queued.

```csharp
public ulong PeakNumBytesEverQueued { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Networking_NetworkingTcpStatistics_TotalNumBytesQueued"></a> TotalNumBytesQueued

Total bytes queued since the runtime started.

```csharp
public ulong TotalNumBytesQueued { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

