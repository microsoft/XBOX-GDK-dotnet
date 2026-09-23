# <a id="GDK_Net_PlayFab_Statistics"></a> Class Statistics

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Statistics service (<code>PFStatistics.h</code>).

```csharp
public static class Statistics
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Statistics](GDK.Net.PlayFab.Statistics.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Statistics_CreateStatisticDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_System_Threading_CancellationToken_"></a> CreateStatisticDefinitionAsync\(PlayFabEntity, StatisticsCreateStatisticDefinitionRequest, CancellationToken\)

Calls <code>PFStatisticsCreateStatisticDefinitionAsync</code>.

```csharp
public static Task CreateStatisticDefinitionAsync(PlayFabEntity entity, StatisticsCreateStatisticDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsCreateStatisticDefinitionRequest](GDK.Net.PlayFab.StatisticsCreateStatisticDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Statistics_DeleteStatisticDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsDeleteStatisticDefinitionRequest_System_Threading_CancellationToken_"></a> DeleteStatisticDefinitionAsync\(PlayFabEntity, StatisticsDeleteStatisticDefinitionRequest, CancellationToken\)

Calls <code>PFStatisticsDeleteStatisticDefinitionAsync</code>.

```csharp
public static Task DeleteStatisticDefinitionAsync(PlayFabEntity entity, StatisticsDeleteStatisticDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsDeleteStatisticDefinitionRequest](GDK.Net.PlayFab.StatisticsDeleteStatisticDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Statistics_DeleteStatisticsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsDeleteStatisticsRequest_System_Threading_CancellationToken_"></a> DeleteStatisticsAsync\(PlayFabEntity, StatisticsDeleteStatisticsRequest, CancellationToken\)

Calls <code>PFStatisticsDeleteStatisticsAsync</code>.

```csharp
public static Task<StatisticsDeleteStatisticsResponse> DeleteStatisticsAsync(PlayFabEntity entity, StatisticsDeleteStatisticsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsDeleteStatisticsRequest](GDK.Net.PlayFab.StatisticsDeleteStatisticsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsDeleteStatisticsResponse](GDK.Net.PlayFab.StatisticsDeleteStatisticsResponse.md)\>

### <a id="GDK_Net_PlayFab_Statistics_GetStatisticDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsGetStatisticDefinitionRequest_System_Threading_CancellationToken_"></a> GetStatisticDefinitionAsync\(PlayFabEntity, StatisticsGetStatisticDefinitionRequest, CancellationToken\)

Calls <code>PFStatisticsGetStatisticDefinitionAsync</code>.

```csharp
public static Task<StatisticsGetStatisticDefinitionResponse> GetStatisticDefinitionAsync(PlayFabEntity entity, StatisticsGetStatisticDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsGetStatisticDefinitionRequest](GDK.Net.PlayFab.StatisticsGetStatisticDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsGetStatisticDefinitionResponse](GDK.Net.PlayFab.StatisticsGetStatisticDefinitionResponse.md)\>

### <a id="GDK_Net_PlayFab_Statistics_GetStatisticsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsGetStatisticsRequest_System_Threading_CancellationToken_"></a> GetStatisticsAsync\(PlayFabEntity, StatisticsGetStatisticsRequest, CancellationToken\)

Calls <code>PFStatisticsGetStatisticsAsync</code>.

```csharp
public static Task<StatisticsGetStatisticsResponse> GetStatisticsAsync(PlayFabEntity entity, StatisticsGetStatisticsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsGetStatisticsRequest](GDK.Net.PlayFab.StatisticsGetStatisticsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsGetStatisticsResponse](GDK.Net.PlayFab.StatisticsGetStatisticsResponse.md)\>

### <a id="GDK_Net_PlayFab_Statistics_GetStatisticsForEntitiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesRequest_System_Threading_CancellationToken_"></a> GetStatisticsForEntitiesAsync\(PlayFabEntity, StatisticsGetStatisticsForEntitiesRequest, CancellationToken\)

Calls <code>PFStatisticsGetStatisticsForEntitiesAsync</code>.

```csharp
public static Task<StatisticsGetStatisticsForEntitiesResponse> GetStatisticsForEntitiesAsync(PlayFabEntity entity, StatisticsGetStatisticsForEntitiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsGetStatisticsForEntitiesRequest](GDK.Net.PlayFab.StatisticsGetStatisticsForEntitiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsGetStatisticsForEntitiesResponse](GDK.Net.PlayFab.StatisticsGetStatisticsForEntitiesResponse.md)\>

### <a id="GDK_Net_PlayFab_Statistics_IncrementStatisticVersionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsIncrementStatisticVersionRequest_System_Threading_CancellationToken_"></a> IncrementStatisticVersionAsync\(PlayFabEntity, StatisticsIncrementStatisticVersionRequest, CancellationToken\)

Calls <code>PFStatisticsIncrementStatisticVersionAsync</code>.

```csharp
public static Task<StatisticsIncrementStatisticVersionResponse> IncrementStatisticVersionAsync(PlayFabEntity entity, StatisticsIncrementStatisticVersionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsIncrementStatisticVersionRequest](GDK.Net.PlayFab.StatisticsIncrementStatisticVersionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsIncrementStatisticVersionResponse](GDK.Net.PlayFab.StatisticsIncrementStatisticVersionResponse.md)\>

### <a id="GDK_Net_PlayFab_Statistics_ListStatisticDefinitionsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsListStatisticDefinitionsRequest_System_Threading_CancellationToken_"></a> ListStatisticDefinitionsAsync\(PlayFabEntity, StatisticsListStatisticDefinitionsRequest, CancellationToken\)

Calls <code>PFStatisticsListStatisticDefinitionsAsync</code>.

```csharp
public static Task<StatisticsListStatisticDefinitionsResponse> ListStatisticDefinitionsAsync(PlayFabEntity entity, StatisticsListStatisticDefinitionsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsListStatisticDefinitionsRequest](GDK.Net.PlayFab.StatisticsListStatisticDefinitionsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsListStatisticDefinitionsResponse](GDK.Net.PlayFab.StatisticsListStatisticDefinitionsResponse.md)\>

### <a id="GDK_Net_PlayFab_Statistics_UpdateStatisticDefinitionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsUpdateStatisticDefinitionRequest_System_Threading_CancellationToken_"></a> UpdateStatisticDefinitionAsync\(PlayFabEntity, StatisticsUpdateStatisticDefinitionRequest, CancellationToken\)

Calls <code>PFStatisticsUpdateStatisticDefinitionAsync</code>.

```csharp
public static Task UpdateStatisticDefinitionAsync(PlayFabEntity entity, StatisticsUpdateStatisticDefinitionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsUpdateStatisticDefinitionRequest](GDK.Net.PlayFab.StatisticsUpdateStatisticDefinitionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Statistics_UpdateStatisticsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_StatisticsUpdateStatisticsRequest_System_Threading_CancellationToken_"></a> UpdateStatisticsAsync\(PlayFabEntity, StatisticsUpdateStatisticsRequest, CancellationToken\)

Calls <code>PFStatisticsUpdateStatisticsAsync</code>.

```csharp
public static Task<StatisticsUpdateStatisticsResponse> UpdateStatisticsAsync(PlayFabEntity entity, StatisticsUpdateStatisticsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [StatisticsUpdateStatisticsRequest](GDK.Net.PlayFab.StatisticsUpdateStatisticsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StatisticsUpdateStatisticsResponse](GDK.Net.PlayFab.StatisticsUpdateStatisticsResponse.md)\>

