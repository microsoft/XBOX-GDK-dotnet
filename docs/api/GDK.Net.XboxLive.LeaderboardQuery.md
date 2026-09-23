# <a id="GDK_Net_XboxLive_LeaderboardQuery"></a> Class LeaderboardQuery

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Parameters for an Xbox Live leaderboard query. Managed equivalent of
<code>XblLeaderboardQuery</code>.

```csharp
public sealed class LeaderboardQuery
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardQuery](GDK.Net.XboxLive.LeaderboardQuery.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_LeaderboardQuery__ctor_System_UInt64_System_String_System_String_System_String_GDK_Net_XboxLive_SocialGroupType_System_Collections_Generic_IEnumerable_System_String__GDK_Net_XboxLive_LeaderboardSortOrder_System_UInt32_System_UInt64_System_UInt32_System_String_GDK_Net_XboxLive_LeaderboardQueryType_"></a> LeaderboardQuery\(ulong, string, string?, string?, SocialGroupType, IEnumerable<string\>?, LeaderboardSortOrder, uint, ulong, uint, string?, LeaderboardQueryType\)

Creates a leaderboard query.

```csharp
public LeaderboardQuery(ulong xboxUserId, string serviceConfigurationId, string? leaderboardName = null, string? statisticName = null, SocialGroupType socialGroup = SocialGroupType.None, IEnumerable<string>? additionalColumnLeaderboardNames = null, LeaderboardSortOrder order = LeaderboardSortOrder.Descending, uint maxItems = 0, ulong skipToXboxUserId = 0, uint skipResultToRank = 0, string? continuationToken = null, LeaderboardQueryType queryType = LeaderboardQueryType.UserStatBacked)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Optional Xbox user id of the requesting user. Set to 0 for a global leaderboard.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's service configuration id.

`leaderboardName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional leaderboard name for an event-based user-stat leaderboard.

`statisticName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional statistic name for a social or title-managed-stat-backed leaderboard.

`socialGroup` [SocialGroupType](GDK.Net.XboxLive.SocialGroupType.md)

The social group to request, or <xref href="GDK.Net.XboxLive.SocialGroupType.None" data-throw-if-not-resolved="false"></xref>.

`additionalColumnLeaderboardNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

Optional additional statistic columns to return.

`order` [LeaderboardSortOrder](GDK.Net.XboxLive.LeaderboardSortOrder.md)

The sort order.

`maxItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum rows to return. 0 lets the service choose.

`skipToXboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Xbox user id to start at. 0 disables this skip.

`skipResultToRank` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Rank to start at. 0 disables this skip.

`continuationToken` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional continuation token for a query resumed by the service.

`queryType` [LeaderboardQueryType](GDK.Net.XboxLive.LeaderboardQueryType.md)

The leaderboard backing store to query.

## Properties

### <a id="GDK_Net_XboxLive_LeaderboardQuery_AdditionalColumnLeaderboardNames"></a> AdditionalColumnLeaderboardNames

Optional additional statistic columns to return.

```csharp
public IReadOnlyList<string> AdditionalColumnLeaderboardNames { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_XboxLive_LeaderboardQuery_ContinuationToken"></a> ContinuationToken

Optional continuation token for a query resumed by the service.

```csharp
public string? ContinuationToken { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_LeaderboardQuery_LeaderboardName"></a> LeaderboardName

Optional leaderboard name for an event-based user-stat leaderboard.

```csharp
public string? LeaderboardName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_LeaderboardQuery_MaxItems"></a> MaxItems

Maximum rows to return. 0 lets the service choose.

```csharp
public uint MaxItems { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_Order"></a> Order

The sort order.

```csharp
public LeaderboardSortOrder Order { get; }
```

#### Property Value

 [LeaderboardSortOrder](GDK.Net.XboxLive.LeaderboardSortOrder.md)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_QueryType"></a> QueryType

The leaderboard backing store to query.

```csharp
public LeaderboardQueryType QueryType { get; }
```

#### Property Value

 [LeaderboardQueryType](GDK.Net.XboxLive.LeaderboardQueryType.md)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_ServiceConfigurationId"></a> ServiceConfigurationId

The title's service configuration id.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_SkipResultToRank"></a> SkipResultToRank

Rank to start at. 0 disables this skip.

```csharp
public uint SkipResultToRank { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_SkipToXboxUserId"></a> SkipToXboxUserId

Xbox user id to start at. 0 disables this skip.

```csharp
public ulong SkipToXboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_SocialGroup"></a> SocialGroup

The social group to request, or <xref href="GDK.Net.XboxLive.SocialGroupType.None" data-throw-if-not-resolved="false"></xref>.

```csharp
public SocialGroupType SocialGroup { get; }
```

#### Property Value

 [SocialGroupType](GDK.Net.XboxLive.SocialGroupType.md)

### <a id="GDK_Net_XboxLive_LeaderboardQuery_StatisticName"></a> StatisticName

Optional statistic name for a social or title-managed-stat-backed leaderboard.

```csharp
public string? StatisticName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_LeaderboardQuery_XboxUserId"></a> XboxUserId

Optional Xbox user id of the requesting user.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

