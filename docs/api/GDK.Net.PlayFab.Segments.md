# <a id="GDK_Net_PlayFab_Segments"></a> Class Segments

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Segments service (<code>PFSegments.h</code>).

```csharp
public static class Segments
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Segments](GDK.Net.PlayFab.Segments.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Segments_ClientGetPlayerSegmentsAsync_GDK_Net_PlayFab_PlayFabEntity_System_Threading_CancellationToken_"></a> ClientGetPlayerSegmentsAsync\(PlayFabEntity, CancellationToken\)

Calls <code>PFSegmentsClientGetPlayerSegmentsAsync</code>.

```csharp
public static Task<SegmentsGetPlayerSegmentsResult> ClientGetPlayerSegmentsAsync(PlayFabEntity entity, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SegmentsGetPlayerSegmentsResult](GDK.Net.PlayFab.SegmentsGetPlayerSegmentsResult.md)\>

### <a id="GDK_Net_PlayFab_Segments_ClientGetPlayerTagsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_SegmentsGetPlayerTagsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayerTagsAsync\(PlayFabEntity, SegmentsGetPlayerTagsRequest, CancellationToken\)

Calls <code>PFSegmentsClientGetPlayerTagsAsync</code>.

```csharp
public static Task<SegmentsGetPlayerTagsResult> ClientGetPlayerTagsAsync(PlayFabEntity entity, SegmentsGetPlayerTagsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [SegmentsGetPlayerTagsRequest](GDK.Net.PlayFab.SegmentsGetPlayerTagsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SegmentsGetPlayerTagsResult](GDK.Net.PlayFab.SegmentsGetPlayerTagsResult.md)\>

### <a id="GDK_Net_PlayFab_Segments_ServerAddPlayerTagAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_SegmentsAddPlayerTagRequest_System_Threading_CancellationToken_"></a> ServerAddPlayerTagAsync\(PlayFabEntity, SegmentsAddPlayerTagRequest, CancellationToken\)

Calls <code>PFSegmentsServerAddPlayerTagAsync</code>.

```csharp
public static Task ServerAddPlayerTagAsync(PlayFabEntity entity, SegmentsAddPlayerTagRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [SegmentsAddPlayerTagRequest](GDK.Net.PlayFab.SegmentsAddPlayerTagRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Segments_ServerGetAllSegmentsAsync_GDK_Net_PlayFab_PlayFabEntity_System_Threading_CancellationToken_"></a> ServerGetAllSegmentsAsync\(PlayFabEntity, CancellationToken\)

Calls <code>PFSegmentsServerGetAllSegmentsAsync</code>.

```csharp
public static Task<SegmentsGetAllSegmentsResult> ServerGetAllSegmentsAsync(PlayFabEntity entity, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SegmentsGetAllSegmentsResult](GDK.Net.PlayFab.SegmentsGetAllSegmentsResult.md)\>

### <a id="GDK_Net_PlayFab_Segments_ServerGetPlayerSegmentsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_SegmentsGetPlayersSegmentsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayerSegmentsAsync\(PlayFabEntity, SegmentsGetPlayersSegmentsRequest, CancellationToken\)

Calls <code>PFSegmentsServerGetPlayerSegmentsAsync</code>.

```csharp
public static Task<SegmentsGetPlayerSegmentsResult> ServerGetPlayerSegmentsAsync(PlayFabEntity entity, SegmentsGetPlayersSegmentsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [SegmentsGetPlayersSegmentsRequest](GDK.Net.PlayFab.SegmentsGetPlayersSegmentsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SegmentsGetPlayerSegmentsResult](GDK.Net.PlayFab.SegmentsGetPlayerSegmentsResult.md)\>

### <a id="GDK_Net_PlayFab_Segments_ServerGetPlayerTagsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_SegmentsGetPlayerTagsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayerTagsAsync\(PlayFabEntity, SegmentsGetPlayerTagsRequest, CancellationToken\)

Calls <code>PFSegmentsServerGetPlayerTagsAsync</code>.

```csharp
public static Task<SegmentsGetPlayerTagsResult> ServerGetPlayerTagsAsync(PlayFabEntity entity, SegmentsGetPlayerTagsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [SegmentsGetPlayerTagsRequest](GDK.Net.PlayFab.SegmentsGetPlayerTagsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SegmentsGetPlayerTagsResult](GDK.Net.PlayFab.SegmentsGetPlayerTagsResult.md)\>

### <a id="GDK_Net_PlayFab_Segments_ServerGetPlayersInSegmentAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_SegmentsGetPlayersInSegmentRequest_System_Threading_CancellationToken_"></a> ServerGetPlayersInSegmentAsync\(PlayFabEntity, SegmentsGetPlayersInSegmentRequest, CancellationToken\)

Calls <code>PFSegmentsServerGetPlayersInSegmentAsync</code>.

```csharp
public static Task<SegmentsGetPlayersInSegmentResult> ServerGetPlayersInSegmentAsync(PlayFabEntity entity, SegmentsGetPlayersInSegmentRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [SegmentsGetPlayersInSegmentRequest](GDK.Net.PlayFab.SegmentsGetPlayersInSegmentRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SegmentsGetPlayersInSegmentResult](GDK.Net.PlayFab.SegmentsGetPlayersInSegmentResult.md)\>

### <a id="GDK_Net_PlayFab_Segments_ServerRemovePlayerTagAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_SegmentsRemovePlayerTagRequest_System_Threading_CancellationToken_"></a> ServerRemovePlayerTagAsync\(PlayFabEntity, SegmentsRemovePlayerTagRequest, CancellationToken\)

Calls <code>PFSegmentsServerRemovePlayerTagAsync</code>.

```csharp
public static Task ServerRemovePlayerTagAsync(PlayFabEntity entity, SegmentsRemovePlayerTagRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [SegmentsRemovePlayerTagRequest](GDK.Net.PlayFab.SegmentsRemovePlayerTagRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

