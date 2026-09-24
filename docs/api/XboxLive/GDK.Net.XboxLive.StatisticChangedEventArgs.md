# <a id="GDK_Net_XboxLive_StatisticChangedEventArgs"></a> Class StatisticChangedEventArgs

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.XboxLive.UserStatisticsService.StatisticChanged" data-throw-if-not-resolved="false"></xref>. Managed snapshot of
<code>XblStatisticChangeEventArgs</code>.

```csharp
public sealed class StatisticChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[StatisticChangedEventArgs](GDK.Net.XboxLive.StatisticChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_StatisticChangedEventArgs_LatestStatistic"></a> LatestStatistic

The latest statistic value reported by Xbox Live.

```csharp
public Statistic LatestStatistic { get; }
```

#### Property Value

 [Statistic](GDK.Net.XboxLive.Statistic.md)

### <a id="GDK_Net_XboxLive_StatisticChangedEventArgs_ServiceConfigurationId"></a> ServiceConfigurationId

The service configuration id (SCID) the statistic belongs to.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_StatisticChangedEventArgs_XboxUserId"></a> XboxUserId

The Xbox user id whose statistic changed.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

