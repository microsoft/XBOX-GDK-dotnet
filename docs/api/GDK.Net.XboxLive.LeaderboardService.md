# <a id="GDK_Net_XboxLive_LeaderboardService"></a> Class LeaderboardService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live leaderboard queries. Mirrors <code>leaderboard_c.h</code>.

```csharp
public sealed class LeaderboardService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardService](GDK.Net.XboxLive.LeaderboardService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_XboxLive_LeaderboardService_GetAsync_GDK_Net_XboxLive_LeaderboardQuery_System_Threading_CancellationToken_"></a> GetAsync\(LeaderboardQuery, CancellationToken\)

Gets a leaderboard page (<code>XblLeaderboardGetLeaderboardAsync</code>).

```csharp
public Task<LeaderboardPage> GetAsync(LeaderboardQuery query, CancellationToken cancellationToken = default)
```

#### Parameters

`query` [LeaderboardQuery](GDK.Net.XboxLive.LeaderboardQuery.md)

The leaderboard query to execute.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardPage](GDK.Net.XboxLive.LeaderboardPage.md)\>

A page of results. Dispose it when no more pages are needed; the rows and columns already
read from it remain valid after disposal.

