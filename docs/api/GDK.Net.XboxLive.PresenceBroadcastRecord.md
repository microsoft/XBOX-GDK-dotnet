# <a id="GDK_Net_XboxLive_PresenceBroadcastRecord"></a> Class PresenceBroadcastRecord

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Broadcast details attached to a title presence record. Managed snapshot of <code>XblPresenceBroadcastRecord</code>.

```csharp
public sealed class PresenceBroadcastRecord
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceBroadcastRecord](GDK.Net.XboxLive.PresenceBroadcastRecord.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_PresenceBroadcastRecord_BroadcastId"></a> BroadcastId

The broadcast id assigned by the provider.

```csharp
public string BroadcastId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_PresenceBroadcastRecord_Provider"></a> Provider

The streaming provider.

```csharp
public PresenceBroadcastProvider Provider { get; }
```

#### Property Value

 [PresenceBroadcastProvider](GDK.Net.XboxLive.PresenceBroadcastProvider.md)

### <a id="GDK_Net_XboxLive_PresenceBroadcastRecord_SessionId"></a> SessionId

The GUID string identifying the broadcast session.

```csharp
public string SessionId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_PresenceBroadcastRecord_StartTime"></a> StartTime

When the broadcast started.

```csharp
public DateTimeOffset StartTime { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_XboxLive_PresenceBroadcastRecord_ViewerCount"></a> ViewerCount

Approximate current viewer count.

```csharp
public uint ViewerCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

