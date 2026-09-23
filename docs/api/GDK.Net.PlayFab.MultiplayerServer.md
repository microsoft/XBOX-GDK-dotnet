# <a id="GDK_Net_PlayFab_MultiplayerServer"></a> Class MultiplayerServer

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab MultiplayerServer service (<code>PFMultiplayerServer.h</code>).

```csharp
public static class MultiplayerServer
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MultiplayerServer](GDK.Net.PlayFab.MultiplayerServer.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_MultiplayerServer_ListBuildAliasesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_MultiplayerServerListBuildAliasesRequest_System_Threading_CancellationToken_"></a> ListBuildAliasesAsync\(PlayFabEntity, MultiplayerServerListBuildAliasesRequest, CancellationToken\)

Calls <code>PFMultiplayerServerListBuildAliasesAsync</code>.

```csharp
public static Task<MultiplayerServerListBuildAliasesResponse> ListBuildAliasesAsync(PlayFabEntity entity, MultiplayerServerListBuildAliasesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [MultiplayerServerListBuildAliasesRequest](GDK.Net.PlayFab.MultiplayerServerListBuildAliasesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[MultiplayerServerListBuildAliasesResponse](GDK.Net.PlayFab.MultiplayerServerListBuildAliasesResponse.md)\>

### <a id="GDK_Net_PlayFab_MultiplayerServer_ListBuildSummariesV2Async_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_MultiplayerServerListBuildSummariesRequest_System_Threading_CancellationToken_"></a> ListBuildSummariesV2Async\(PlayFabEntity, MultiplayerServerListBuildSummariesRequest, CancellationToken\)

Calls <code>PFMultiplayerServerListBuildSummariesV2Async</code>.

```csharp
public static Task<MultiplayerServerListBuildSummariesResponse> ListBuildSummariesV2Async(PlayFabEntity entity, MultiplayerServerListBuildSummariesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [MultiplayerServerListBuildSummariesRequest](GDK.Net.PlayFab.MultiplayerServerListBuildSummariesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[MultiplayerServerListBuildSummariesResponse](GDK.Net.PlayFab.MultiplayerServerListBuildSummariesResponse.md)\>

### <a id="GDK_Net_PlayFab_MultiplayerServer_ListQosServersForTitleAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_MultiplayerServerListQosServersForTitleRequest_System_Threading_CancellationToken_"></a> ListQosServersForTitleAsync\(PlayFabEntity, MultiplayerServerListQosServersForTitleRequest, CancellationToken\)

Calls <code>PFMultiplayerServerListQosServersForTitleAsync</code>.

```csharp
public static Task<MultiplayerServerListQosServersForTitleResponse> ListQosServersForTitleAsync(PlayFabEntity entity, MultiplayerServerListQosServersForTitleRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [MultiplayerServerListQosServersForTitleRequest](GDK.Net.PlayFab.MultiplayerServerListQosServersForTitleRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[MultiplayerServerListQosServersForTitleResponse](GDK.Net.PlayFab.MultiplayerServerListQosServersForTitleResponse.md)\>

### <a id="GDK_Net_PlayFab_MultiplayerServer_RequestMultiplayerServerAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_MultiplayerServerRequestMultiplayerServerRequest_System_Threading_CancellationToken_"></a> RequestMultiplayerServerAsync\(PlayFabEntity, MultiplayerServerRequestMultiplayerServerRequest, CancellationToken\)

Calls <code>PFMultiplayerServerRequestMultiplayerServerAsync</code>.

```csharp
public static Task<MultiplayerServerRequestMultiplayerServerResponse> RequestMultiplayerServerAsync(PlayFabEntity entity, MultiplayerServerRequestMultiplayerServerRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [MultiplayerServerRequestMultiplayerServerRequest](GDK.Net.PlayFab.MultiplayerServerRequestMultiplayerServerRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[MultiplayerServerRequestMultiplayerServerResponse](GDK.Net.PlayFab.MultiplayerServerRequestMultiplayerServerResponse.md)\>

