# <a id="GDK_Net_XboxLive_UserStatisticsService"></a> Class UserStatisticsService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live user statistic queries and real-time statistic change notifications. Mirrors
<code>user_statistics_c.h</code>.

```csharp
public sealed class UserStatisticsService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserStatisticsService](GDK.Net.XboxLive.UserStatisticsService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Real-time statistic tracking and notifications are backed by XSAPI Real-Time Activity. The title
must activate <xref href="GDK.Net.XboxLive.XboxLiveContext.RealTimeActivity" data-throw-if-not-resolved="false"></xref> before expecting statistic-change
notifications; this service does not take a compile-time dependency on the RTA service.

## Methods

### <a id="GDK_Net_XboxLive_UserStatisticsService_GetMultipleUserStatisticsAsync_System_Collections_Generic_IEnumerable_System_UInt64__System_String_System_Collections_Generic_IEnumerable_System_String__System_Threading_CancellationToken_"></a> GetMultipleUserStatisticsAsync\(IEnumerable<ulong\>, string, IEnumerable<string\>, CancellationToken\)

Gets statistics for several users under one service configuration.

```csharp
public Task<IReadOnlyList<UserStatisticsResult>> GetMultipleUserStatisticsAsync(IEnumerable<ulong> xboxUserIds, string serviceConfigurationId, IEnumerable<string> statisticNames, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Xbox user ids whose statistics should be read.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service configuration id (SCID) to query.

`statisticNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Statistic names to query.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[UserStatisticsResult](GDK.Net.XboxLive.UserStatisticsResult.md)\>\>

### <a id="GDK_Net_XboxLive_UserStatisticsService_GetMultipleUserStatisticsForMultipleServiceConfigurationsAsync_System_Collections_Generic_IEnumerable_System_UInt64__System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_RequestedStatistics__System_Threading_CancellationToken_"></a> GetMultipleUserStatisticsForMultipleServiceConfigurationsAsync\(IEnumerable<ulong\>, IEnumerable<RequestedStatistics\>, CancellationToken\)

Gets statistics for several users across several service configurations.

```csharp
public Task<IReadOnlyList<UserStatisticsResult>> GetMultipleUserStatisticsForMultipleServiceConfigurationsAsync(IEnumerable<ulong> xboxUserIds, IEnumerable<RequestedStatistics> requestedStatistics, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Xbox user ids whose statistics should be read.

`requestedStatistics` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[RequestedStatistics](GDK.Net.XboxLive.RequestedStatistics.md)\>

Service configurations and statistic names to query.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[UserStatisticsResult](GDK.Net.XboxLive.UserStatisticsResult.md)\>\>

### <a id="GDK_Net_XboxLive_UserStatisticsService_GetSingleUserStatisticAsync_System_UInt64_System_String_System_String_System_Threading_CancellationToken_"></a> GetSingleUserStatisticAsync\(ulong, string, string, CancellationToken\)

Gets one statistic for one user (<code>XblUserStatisticsGetSingleUserStatisticAsync</code>).

```csharp
public Task<UserStatisticsResult> GetSingleUserStatisticAsync(ulong xboxUserId, string serviceConfigurationId, string statisticName, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id whose statistic should be read.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service configuration id (SCID) to query.

`statisticName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The statistic name to query.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[UserStatisticsResult](GDK.Net.XboxLive.UserStatisticsResult.md)\>

### <a id="GDK_Net_XboxLive_UserStatisticsService_GetSingleUserStatisticsAsync_System_UInt64_System_String_System_Collections_Generic_IEnumerable_System_String__System_Threading_CancellationToken_"></a> GetSingleUserStatisticsAsync\(ulong, string, IEnumerable<string\>, CancellationToken\)

Gets several statistics for one user (<code>XblUserStatisticsGetSingleUserStatisticsAsync</code>).

```csharp
public Task<UserStatisticsResult> GetSingleUserStatisticsAsync(ulong xboxUserId, string serviceConfigurationId, IEnumerable<string> statisticNames, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id whose statistics should be read.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service configuration id (SCID) to query.

`statisticNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Statistic names to query.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[UserStatisticsResult](GDK.Net.XboxLive.UserStatisticsResult.md)\>

### <a id="GDK_Net_XboxLive_UserStatisticsService_StopTrackingStatistics_System_Collections_Generic_IEnumerable_System_UInt64__System_String_System_Collections_Generic_IEnumerable_System_String__"></a> StopTrackingStatistics\(IEnumerable<ulong\>, string, IEnumerable<string\>\)

Stops tracking statistics for real-time change notifications.

```csharp
public void StopTrackingStatistics(IEnumerable<ulong> xboxUserIds, string serviceConfigurationId, IEnumerable<string> statisticNames)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Users to remove for the named statistics.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service configuration id (SCID) being watched.

`statisticNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Statistic names to stop watching.

### <a id="GDK_Net_XboxLive_UserStatisticsService_StopTrackingUsers_System_Collections_Generic_IEnumerable_System_UInt64__"></a> StopTrackingUsers\(IEnumerable<ulong\>\)

Stops tracking all statistics for the provided users.

```csharp
public void StopTrackingUsers(IEnumerable<ulong> xboxUserIds)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Users whose statistic tracking should be cancelled.

### <a id="GDK_Net_XboxLive_UserStatisticsService_TrackStatistics_System_Collections_Generic_IEnumerable_System_UInt64__System_String_System_Collections_Generic_IEnumerable_System_String__"></a> TrackStatistics\(IEnumerable<ulong\>, string, IEnumerable<string\>\)

Tracks statistics for real-time change notifications.

```csharp
public void TrackStatistics(IEnumerable<ulong> xboxUserIds, string serviceConfigurationId, IEnumerable<string> statisticNames)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Users to append to the tracked set.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service configuration id (SCID) to watch.

`statisticNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Statistic names to watch.

#### Remarks

Changes arrive through <xref href="GDK.Net.XboxLive.UserStatisticsService.StatisticChanged" data-throw-if-not-resolved="false"></xref>; XSAPI opens the real-time activity
connection on demand.

### <a id="GDK_Net_XboxLive_UserStatisticsService_StatisticChanged"></a> StatisticChanged

Raised when a tracked statistic changes.

```csharp
public event EventHandler<StatisticChangedEventArgs>? StatisticChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[StatisticChangedEventArgs](GDK.Net.XboxLive.StatisticChangedEventArgs.md)\>?

#### Remarks

Call <xref href="GDK.Net.XboxLive.UserStatisticsService.TrackStatistics(System.Collections.Generic.IEnumerable%7bSystem.UInt64%7d%2cSystem.String%2cSystem.Collections.Generic.IEnumerable%7bSystem.String%7d)" data-throw-if-not-resolved="false"></xref> first for the statistics you want to receive. XSAPI opens
the real-time activity connection on demand, and callbacks arrive on an XSAPI-internal thread
rather than this context's task queue.

