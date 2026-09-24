# <a id="GDK_Net_XboxLive_TitleManagedStatisticsService"></a> Class TitleManagedStatisticsService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Writes title-managed statistics for the signed-in user. Mirrors
<code>title_managed_statistics_c.h</code>.

```csharp
public sealed class TitleManagedStatisticsService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleManagedStatisticsService](GDK.Net.XboxLive.TitleManagedStatisticsService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_XboxLive_TitleManagedStatisticsService_DeleteAsync_System_Collections_Generic_IEnumerable_System_String__System_Threading_CancellationToken_"></a> DeleteAsync\(IEnumerable<string\>, CancellationToken\)

Deletes title-managed statistics for the signed-in user
(<code>XblTitleManagedStatsDeleteStatsAsync</code>).

```csharp
public Task DeleteAsync(IEnumerable<string> statisticNames, CancellationToken cancellationToken = default)
```

#### Parameters

`statisticNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The case-insensitive statistic names to delete.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_XboxLive_TitleManagedStatisticsService_UpdateAsync_System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_TitleManagedStatistic__System_Threading_CancellationToken_"></a> UpdateAsync\(IEnumerable<TitleManagedStatistic\>, CancellationToken\)

Partially updates existing title-managed statistics
(<code>XblTitleManagedStatsUpdateStatsAsync</code>).

```csharp
public Task UpdateAsync(IEnumerable<TitleManagedStatistic> statistics, CancellationToken cancellationToken = default)
```

#### Parameters

`statistics` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[TitleManagedStatistic](GDK.Net.XboxLive.TitleManagedStatistic.md)\>

The statistics to update. Statistics not included are unchanged.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_XboxLive_TitleManagedStatisticsService_WriteAsync_System_UInt64_System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_TitleManagedStatistic__System_Threading_CancellationToken_"></a> WriteAsync\(ulong, IEnumerable<TitleManagedStatistic\>, CancellationToken\)

Completely replaces the user's title-managed statistics
(<code>XblTitleManagedStatsWriteAsync</code>).

```csharp
public Task WriteAsync(ulong xboxUserId, IEnumerable<TitleManagedStatistic> statistics, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The local user whose stats are being replaced.

`statistics` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[TitleManagedStatistic](GDK.Net.XboxLive.TitleManagedStatistic.md)\>

The complete statistic set to submit. Any existing statistic not included is removed.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

