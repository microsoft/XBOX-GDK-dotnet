# <a id="GDK_Net_PlayFab_Leaderboards"></a> Class Leaderboards

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Leaderboards service (<code>PFLeaderboards.h</code>).

```csharp
public static class Leaderboards
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Leaderboards](GDK.Net.PlayFab.Leaderboards.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Leaderboards_CreateLeaderboardDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_System_Threading_CancellationToken_"></a> CreateLeaderboardDefinitionAsync\(PlayFabEntity, LeaderboardsCreateLeaderboardDefinitionRequest, CancellationToken\)

Calls <code>PFLeaderboardsCreateLeaderboardDefinitionAsync</code>.

```csharp
public static Task CreateLeaderboardDefinitionAsync(PlayFabEntity entity, LeaderboardsCreateLeaderboardDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsCreateLeaderboardDefinitionRequest](GDK.Net.PlayFab.LeaderboardsCreateLeaderboardDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Leaderboards_DeleteLeaderboardDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsDeleteLeaderboardDefinitionRequest_System_Threading_CancellationToken_"></a> DeleteLeaderboardDefinitionAsync\(PlayFabEntity, LeaderboardsDeleteLeaderboardDefinitionRequest, CancellationToken\)

Calls <code>PFLeaderboardsDeleteLeaderboardDefinitionAsync</code>.

```csharp
public static Task DeleteLeaderboardDefinitionAsync(PlayFabEntity entity, LeaderboardsDeleteLeaderboardDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsDeleteLeaderboardDefinitionRequest](GDK.Net.PlayFab.LeaderboardsDeleteLeaderboardDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Leaderboards_DeleteLeaderboardEntriesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsDeleteLeaderboardEntriesRequest_System_Threading_CancellationToken_"></a> DeleteLeaderboardEntriesAsync\(PlayFabEntity, LeaderboardsDeleteLeaderboardEntriesRequest, CancellationToken\)

Calls <code>PFLeaderboardsDeleteLeaderboardEntriesAsync</code>.

```csharp
public static Task DeleteLeaderboardEntriesAsync(PlayFabEntity entity, LeaderboardsDeleteLeaderboardEntriesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsDeleteLeaderboardEntriesRequest](GDK.Net.PlayFab.LeaderboardsDeleteLeaderboardEntriesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Leaderboards_GetFriendLeaderboardForEntityAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsGetFriendLeaderboardForEntityRequest_System_Threading_CancellationToken_"></a> GetFriendLeaderboardForEntityAsync\(PlayFabEntity, LeaderboardsGetFriendLeaderboardForEntityRequest, CancellationToken\)

Calls <code>PFLeaderboardsGetFriendLeaderboardForEntityAsync</code>.

```csharp
public static Task<LeaderboardsGetEntityLeaderboardResponse> GetFriendLeaderboardForEntityAsync(PlayFabEntity entity, LeaderboardsGetFriendLeaderboardForEntityRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsGetFriendLeaderboardForEntityRequest](GDK.Net.PlayFab.LeaderboardsGetFriendLeaderboardForEntityRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsGetEntityLeaderboardResponse](GDK.Net.PlayFab.LeaderboardsGetEntityLeaderboardResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_GetLeaderboardAroundEntityAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest_System_Threading_CancellationToken_"></a> GetLeaderboardAroundEntityAsync\(PlayFabEntity, LeaderboardsGetLeaderboardAroundEntityRequest, CancellationToken\)

Calls <code>PFLeaderboardsGetLeaderboardAroundEntityAsync</code>.

```csharp
public static Task<LeaderboardsGetEntityLeaderboardResponse> GetLeaderboardAroundEntityAsync(PlayFabEntity entity, LeaderboardsGetLeaderboardAroundEntityRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsGetLeaderboardAroundEntityRequest](GDK.Net.PlayFab.LeaderboardsGetLeaderboardAroundEntityRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsGetEntityLeaderboardResponse](GDK.Net.PlayFab.LeaderboardsGetEntityLeaderboardResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_GetLeaderboardAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardRequest_System_Threading_CancellationToken_"></a> GetLeaderboardAsync\(PlayFabEntity, LeaderboardsGetEntityLeaderboardRequest, CancellationToken\)

Calls <code>PFLeaderboardsGetLeaderboardAsync</code>.

```csharp
public static Task<LeaderboardsGetEntityLeaderboardResponse> GetLeaderboardAsync(PlayFabEntity entity, LeaderboardsGetEntityLeaderboardRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsGetEntityLeaderboardRequest](GDK.Net.PlayFab.LeaderboardsGetEntityLeaderboardRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsGetEntityLeaderboardResponse](GDK.Net.PlayFab.LeaderboardsGetEntityLeaderboardResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_GetLeaderboardDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsGetLeaderboardDefinitionRequest_System_Threading_CancellationToken_"></a> GetLeaderboardDefinitionAsync\(PlayFabEntity, LeaderboardsGetLeaderboardDefinitionRequest, CancellationToken\)

Calls <code>PFLeaderboardsGetLeaderboardDefinitionAsync</code>.

```csharp
public static Task<LeaderboardsGetLeaderboardDefinitionResponse> GetLeaderboardDefinitionAsync(PlayFabEntity entity, LeaderboardsGetLeaderboardDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsGetLeaderboardDefinitionRequest](GDK.Net.PlayFab.LeaderboardsGetLeaderboardDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsGetLeaderboardDefinitionResponse](GDK.Net.PlayFab.LeaderboardsGetLeaderboardDefinitionResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_GetLeaderboardForEntitiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsGetLeaderboardForEntitiesRequest_System_Threading_CancellationToken_"></a> GetLeaderboardForEntitiesAsync\(PlayFabEntity, LeaderboardsGetLeaderboardForEntitiesRequest, CancellationToken\)

Calls <code>PFLeaderboardsGetLeaderboardForEntitiesAsync</code>.

```csharp
public static Task<LeaderboardsGetEntityLeaderboardResponse> GetLeaderboardForEntitiesAsync(PlayFabEntity entity, LeaderboardsGetLeaderboardForEntitiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsGetLeaderboardForEntitiesRequest](GDK.Net.PlayFab.LeaderboardsGetLeaderboardForEntitiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsGetEntityLeaderboardResponse](GDK.Net.PlayFab.LeaderboardsGetEntityLeaderboardResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_IncrementLeaderboardVersionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsIncrementLeaderboardVersionRequest_System_Threading_CancellationToken_"></a> IncrementLeaderboardVersionAsync\(PlayFabEntity, LeaderboardsIncrementLeaderboardVersionRequest, CancellationToken\)

Calls <code>PFLeaderboardsIncrementLeaderboardVersionAsync</code>.

```csharp
public static Task<LeaderboardsIncrementLeaderboardVersionResponse> IncrementLeaderboardVersionAsync(PlayFabEntity entity, LeaderboardsIncrementLeaderboardVersionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsIncrementLeaderboardVersionRequest](GDK.Net.PlayFab.LeaderboardsIncrementLeaderboardVersionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsIncrementLeaderboardVersionResponse](GDK.Net.PlayFab.LeaderboardsIncrementLeaderboardVersionResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_ListLeaderboardDefinitionsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsListLeaderboardDefinitionsRequest_System_Threading_CancellationToken_"></a> ListLeaderboardDefinitionsAsync\(PlayFabEntity, LeaderboardsListLeaderboardDefinitionsRequest, CancellationToken\)

Calls <code>PFLeaderboardsListLeaderboardDefinitionsAsync</code>.

```csharp
public static Task<LeaderboardsListLeaderboardDefinitionsResponse> ListLeaderboardDefinitionsAsync(PlayFabEntity entity, LeaderboardsListLeaderboardDefinitionsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsListLeaderboardDefinitionsRequest](GDK.Net.PlayFab.LeaderboardsListLeaderboardDefinitionsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardsListLeaderboardDefinitionsResponse](GDK.Net.PlayFab.LeaderboardsListLeaderboardDefinitionsResponse.md)\>

### <a id="GDK_Net_PlayFab_Leaderboards_UnlinkLeaderboardFromStatisticAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsUnlinkLeaderboardFromStatisticRequest_System_Threading_CancellationToken_"></a> UnlinkLeaderboardFromStatisticAsync\(PlayFabEntity, LeaderboardsUnlinkLeaderboardFromStatisticRequest, CancellationToken\)

Calls <code>PFLeaderboardsUnlinkLeaderboardFromStatisticAsync</code>.

```csharp
public static Task UnlinkLeaderboardFromStatisticAsync(PlayFabEntity entity, LeaderboardsUnlinkLeaderboardFromStatisticRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsUnlinkLeaderboardFromStatisticRequest](GDK.Net.PlayFab.LeaderboardsUnlinkLeaderboardFromStatisticRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Leaderboards_UpdateLeaderboardDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsUpdateLeaderboardDefinitionRequest_System_Threading_CancellationToken_"></a> UpdateLeaderboardDefinitionAsync\(PlayFabEntity, LeaderboardsUpdateLeaderboardDefinitionRequest, CancellationToken\)

Calls <code>PFLeaderboardsUpdateLeaderboardDefinitionAsync</code>.

```csharp
public static Task UpdateLeaderboardDefinitionAsync(PlayFabEntity entity, LeaderboardsUpdateLeaderboardDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsUpdateLeaderboardDefinitionRequest](GDK.Net.PlayFab.LeaderboardsUpdateLeaderboardDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Leaderboards_UpdateLeaderboardEntriesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LeaderboardsUpdateLeaderboardEntriesRequest_System_Threading_CancellationToken_"></a> UpdateLeaderboardEntriesAsync\(PlayFabEntity, LeaderboardsUpdateLeaderboardEntriesRequest, CancellationToken\)

Calls <code>PFLeaderboardsUpdateLeaderboardEntriesAsync</code>.

```csharp
public static Task UpdateLeaderboardEntriesAsync(PlayFabEntity entity, LeaderboardsUpdateLeaderboardEntriesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LeaderboardsUpdateLeaderboardEntriesRequest](GDK.Net.PlayFab.LeaderboardsUpdateLeaderboardEntriesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

