# <a id="GDK_Net_PlayFab_Catalog"></a> Class Catalog

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Catalog service (<code>PFCatalog.h</code>).

```csharp
public static class Catalog
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Catalog](GDK.Net.PlayFab.Catalog.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Catalog_CreateDraftItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogCreateDraftItemRequest_System_Threading_CancellationToken_"></a> CreateDraftItemAsync\(PlayFabEntity, CatalogCreateDraftItemRequest, CancellationToken\)

Calls <code>PFCatalogCreateDraftItemAsync</code>.

```csharp
public static Task<CatalogCreateDraftItemResponse> CreateDraftItemAsync(PlayFabEntity entity, CatalogCreateDraftItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogCreateDraftItemRequest](GDK.Net.PlayFab.CatalogCreateDraftItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogCreateDraftItemResponse](GDK.Net.PlayFab.CatalogCreateDraftItemResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_CreateUploadUrlsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogCreateUploadUrlsRequest_System_Threading_CancellationToken_"></a> CreateUploadUrlsAsync\(PlayFabEntity, CatalogCreateUploadUrlsRequest, CancellationToken\)

Calls <code>PFCatalogCreateUploadUrlsAsync</code>.

```csharp
public static Task<CatalogCreateUploadUrlsResponse> CreateUploadUrlsAsync(PlayFabEntity entity, CatalogCreateUploadUrlsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogCreateUploadUrlsRequest](GDK.Net.PlayFab.CatalogCreateUploadUrlsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogCreateUploadUrlsResponse](GDK.Net.PlayFab.CatalogCreateUploadUrlsResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_DeleteEntityItemReviewsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogDeleteEntityItemReviewsRequest_System_Threading_CancellationToken_"></a> DeleteEntityItemReviewsAsync\(PlayFabEntity, CatalogDeleteEntityItemReviewsRequest, CancellationToken\)

Calls <code>PFCatalogDeleteEntityItemReviewsAsync</code>.

```csharp
public static Task DeleteEntityItemReviewsAsync(PlayFabEntity entity, CatalogDeleteEntityItemReviewsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogDeleteEntityItemReviewsRequest](GDK.Net.PlayFab.CatalogDeleteEntityItemReviewsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_DeleteItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogDeleteItemRequest_System_Threading_CancellationToken_"></a> DeleteItemAsync\(PlayFabEntity, CatalogDeleteItemRequest, CancellationToken\)

Calls <code>PFCatalogDeleteItemAsync</code>.

```csharp
public static Task DeleteItemAsync(PlayFabEntity entity, CatalogDeleteItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogDeleteItemRequest](GDK.Net.PlayFab.CatalogDeleteItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_GetCatalogConfigAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetCatalogConfigRequest_System_Threading_CancellationToken_"></a> GetCatalogConfigAsync\(PlayFabEntity, CatalogGetCatalogConfigRequest, CancellationToken\)

Calls <code>PFCatalogGetCatalogConfigAsync</code>.

```csharp
public static Task<CatalogGetCatalogConfigResponse> GetCatalogConfigAsync(PlayFabEntity entity, CatalogGetCatalogConfigRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetCatalogConfigRequest](GDK.Net.PlayFab.CatalogGetCatalogConfigRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetCatalogConfigResponse](GDK.Net.PlayFab.CatalogGetCatalogConfigResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetDraftItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetDraftItemRequest_System_Threading_CancellationToken_"></a> GetDraftItemAsync\(PlayFabEntity, CatalogGetDraftItemRequest, CancellationToken\)

Calls <code>PFCatalogGetDraftItemAsync</code>.

```csharp
public static Task<CatalogGetDraftItemResponse> GetDraftItemAsync(PlayFabEntity entity, CatalogGetDraftItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetDraftItemRequest](GDK.Net.PlayFab.CatalogGetDraftItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetDraftItemResponse](GDK.Net.PlayFab.CatalogGetDraftItemResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetDraftItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetDraftItemsRequest_System_Threading_CancellationToken_"></a> GetDraftItemsAsync\(PlayFabEntity, CatalogGetDraftItemsRequest, CancellationToken\)

Calls <code>PFCatalogGetDraftItemsAsync</code>.

```csharp
public static Task<CatalogGetDraftItemsResponse> GetDraftItemsAsync(PlayFabEntity entity, CatalogGetDraftItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetDraftItemsRequest](GDK.Net.PlayFab.CatalogGetDraftItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetDraftItemsResponse](GDK.Net.PlayFab.CatalogGetDraftItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetEntityDraftItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetEntityDraftItemsRequest_System_Threading_CancellationToken_"></a> GetEntityDraftItemsAsync\(PlayFabEntity, CatalogGetEntityDraftItemsRequest, CancellationToken\)

Calls <code>PFCatalogGetEntityDraftItemsAsync</code>.

```csharp
public static Task<CatalogGetEntityDraftItemsResponse> GetEntityDraftItemsAsync(PlayFabEntity entity, CatalogGetEntityDraftItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetEntityDraftItemsRequest](GDK.Net.PlayFab.CatalogGetEntityDraftItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetEntityDraftItemsResponse](GDK.Net.PlayFab.CatalogGetEntityDraftItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetEntityItemReviewAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetEntityItemReviewRequest_System_Threading_CancellationToken_"></a> GetEntityItemReviewAsync\(PlayFabEntity, CatalogGetEntityItemReviewRequest, CancellationToken\)

Calls <code>PFCatalogGetEntityItemReviewAsync</code>.

```csharp
public static Task<CatalogGetEntityItemReviewResponse> GetEntityItemReviewAsync(PlayFabEntity entity, CatalogGetEntityItemReviewRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetEntityItemReviewRequest](GDK.Net.PlayFab.CatalogGetEntityItemReviewRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetEntityItemReviewResponse](GDK.Net.PlayFab.CatalogGetEntityItemReviewResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemRequest_System_Threading_CancellationToken_"></a> GetItemAsync\(PlayFabEntity, CatalogGetItemRequest, CancellationToken\)

Calls <code>PFCatalogGetItemAsync</code>.

```csharp
public static Task<CatalogGetItemResponse> GetItemAsync(PlayFabEntity entity, CatalogGetItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemRequest](GDK.Net.PlayFab.CatalogGetItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemResponse](GDK.Net.PlayFab.CatalogGetItemResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemContainersAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemContainersRequest_System_Threading_CancellationToken_"></a> GetItemContainersAsync\(PlayFabEntity, CatalogGetItemContainersRequest, CancellationToken\)

Calls <code>PFCatalogGetItemContainersAsync</code>.

```csharp
public static Task<CatalogGetItemContainersResponse> GetItemContainersAsync(PlayFabEntity entity, CatalogGetItemContainersRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemContainersRequest](GDK.Net.PlayFab.CatalogGetItemContainersRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemContainersResponse](GDK.Net.PlayFab.CatalogGetItemContainersResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemModerationStateAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemModerationStateRequest_System_Threading_CancellationToken_"></a> GetItemModerationStateAsync\(PlayFabEntity, CatalogGetItemModerationStateRequest, CancellationToken\)

Calls <code>PFCatalogGetItemModerationStateAsync</code>.

```csharp
public static Task<CatalogGetItemModerationStateResponse> GetItemModerationStateAsync(PlayFabEntity entity, CatalogGetItemModerationStateRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemModerationStateRequest](GDK.Net.PlayFab.CatalogGetItemModerationStateRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemModerationStateResponse](GDK.Net.PlayFab.CatalogGetItemModerationStateResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemPublishStatusAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemPublishStatusRequest_System_Threading_CancellationToken_"></a> GetItemPublishStatusAsync\(PlayFabEntity, CatalogGetItemPublishStatusRequest, CancellationToken\)

Calls <code>PFCatalogGetItemPublishStatusAsync</code>.

```csharp
public static Task<CatalogGetItemPublishStatusResponse> GetItemPublishStatusAsync(PlayFabEntity entity, CatalogGetItemPublishStatusRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemPublishStatusRequest](GDK.Net.PlayFab.CatalogGetItemPublishStatusRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemPublishStatusResponse](GDK.Net.PlayFab.CatalogGetItemPublishStatusResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemReviewSummaryAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemReviewSummaryRequest_System_Threading_CancellationToken_"></a> GetItemReviewSummaryAsync\(PlayFabEntity, CatalogGetItemReviewSummaryRequest, CancellationToken\)

Calls <code>PFCatalogGetItemReviewSummaryAsync</code>.

```csharp
public static Task<CatalogGetItemReviewSummaryResponse> GetItemReviewSummaryAsync(PlayFabEntity entity, CatalogGetItemReviewSummaryRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemReviewSummaryRequest](GDK.Net.PlayFab.CatalogGetItemReviewSummaryRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemReviewSummaryResponse](GDK.Net.PlayFab.CatalogGetItemReviewSummaryResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemReviewsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemReviewsRequest_System_Threading_CancellationToken_"></a> GetItemReviewsAsync\(PlayFabEntity, CatalogGetItemReviewsRequest, CancellationToken\)

Calls <code>PFCatalogGetItemReviewsAsync</code>.

```csharp
public static Task<CatalogGetItemReviewsResponse> GetItemReviewsAsync(PlayFabEntity entity, CatalogGetItemReviewsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemReviewsRequest](GDK.Net.PlayFab.CatalogGetItemReviewsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemReviewsResponse](GDK.Net.PlayFab.CatalogGetItemReviewsResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_GetItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogGetItemsRequest_System_Threading_CancellationToken_"></a> GetItemsAsync\(PlayFabEntity, CatalogGetItemsRequest, CancellationToken\)

Calls <code>PFCatalogGetItemsAsync</code>.

```csharp
public static Task<CatalogGetItemsResponse> GetItemsAsync(PlayFabEntity entity, CatalogGetItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogGetItemsRequest](GDK.Net.PlayFab.CatalogGetItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogGetItemsResponse](GDK.Net.PlayFab.CatalogGetItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_PublishDraftItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogPublishDraftItemRequest_System_Threading_CancellationToken_"></a> PublishDraftItemAsync\(PlayFabEntity, CatalogPublishDraftItemRequest, CancellationToken\)

Calls <code>PFCatalogPublishDraftItemAsync</code>.

```csharp
public static Task PublishDraftItemAsync(PlayFabEntity entity, CatalogPublishDraftItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogPublishDraftItemRequest](GDK.Net.PlayFab.CatalogPublishDraftItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_ReportItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogReportItemRequest_System_Threading_CancellationToken_"></a> ReportItemAsync\(PlayFabEntity, CatalogReportItemRequest, CancellationToken\)

Calls <code>PFCatalogReportItemAsync</code>.

```csharp
public static Task ReportItemAsync(PlayFabEntity entity, CatalogReportItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogReportItemRequest](GDK.Net.PlayFab.CatalogReportItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_ReportItemReviewAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogReportItemReviewRequest_System_Threading_CancellationToken_"></a> ReportItemReviewAsync\(PlayFabEntity, CatalogReportItemReviewRequest, CancellationToken\)

Calls <code>PFCatalogReportItemReviewAsync</code>.

```csharp
public static Task ReportItemReviewAsync(PlayFabEntity entity, CatalogReportItemReviewRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogReportItemReviewRequest](GDK.Net.PlayFab.CatalogReportItemReviewRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_ReviewItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogReviewItemRequest_System_Threading_CancellationToken_"></a> ReviewItemAsync\(PlayFabEntity, CatalogReviewItemRequest, CancellationToken\)

Calls <code>PFCatalogReviewItemAsync</code>.

```csharp
public static Task ReviewItemAsync(PlayFabEntity entity, CatalogReviewItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogReviewItemRequest](GDK.Net.PlayFab.CatalogReviewItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_SearchItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogSearchItemsRequest_System_Threading_CancellationToken_"></a> SearchItemsAsync\(PlayFabEntity, CatalogSearchItemsRequest, CancellationToken\)

Calls <code>PFCatalogSearchItemsAsync</code>.

```csharp
public static Task<CatalogSearchItemsResponse> SearchItemsAsync(PlayFabEntity entity, CatalogSearchItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogSearchItemsRequest](GDK.Net.PlayFab.CatalogSearchItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogSearchItemsResponse](GDK.Net.PlayFab.CatalogSearchItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Catalog_SetItemModerationStateAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogSetItemModerationStateRequest_System_Threading_CancellationToken_"></a> SetItemModerationStateAsync\(PlayFabEntity, CatalogSetItemModerationStateRequest, CancellationToken\)

Calls <code>PFCatalogSetItemModerationStateAsync</code>.

```csharp
public static Task SetItemModerationStateAsync(PlayFabEntity entity, CatalogSetItemModerationStateRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogSetItemModerationStateRequest](GDK.Net.PlayFab.CatalogSetItemModerationStateRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_SubmitItemReviewVoteAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_System_Threading_CancellationToken_"></a> SubmitItemReviewVoteAsync\(PlayFabEntity, CatalogSubmitItemReviewVoteRequest, CancellationToken\)

Calls <code>PFCatalogSubmitItemReviewVoteAsync</code>.

```csharp
public static Task SubmitItemReviewVoteAsync(PlayFabEntity entity, CatalogSubmitItemReviewVoteRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogSubmitItemReviewVoteRequest](GDK.Net.PlayFab.CatalogSubmitItemReviewVoteRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_TakedownItemReviewsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogTakedownItemReviewsRequest_System_Threading_CancellationToken_"></a> TakedownItemReviewsAsync\(PlayFabEntity, CatalogTakedownItemReviewsRequest, CancellationToken\)

Calls <code>PFCatalogTakedownItemReviewsAsync</code>.

```csharp
public static Task TakedownItemReviewsAsync(PlayFabEntity entity, CatalogTakedownItemReviewsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogTakedownItemReviewsRequest](GDK.Net.PlayFab.CatalogTakedownItemReviewsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_UpdateCatalogConfigAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogUpdateCatalogConfigRequest_System_Threading_CancellationToken_"></a> UpdateCatalogConfigAsync\(PlayFabEntity, CatalogUpdateCatalogConfigRequest, CancellationToken\)

Calls <code>PFCatalogUpdateCatalogConfigAsync</code>.

```csharp
public static Task UpdateCatalogConfigAsync(PlayFabEntity entity, CatalogUpdateCatalogConfigRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogUpdateCatalogConfigRequest](GDK.Net.PlayFab.CatalogUpdateCatalogConfigRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Catalog_UpdateDraftItemAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CatalogUpdateDraftItemRequest_System_Threading_CancellationToken_"></a> UpdateDraftItemAsync\(PlayFabEntity, CatalogUpdateDraftItemRequest, CancellationToken\)

Calls <code>PFCatalogUpdateDraftItemAsync</code>.

```csharp
public static Task<CatalogUpdateDraftItemResponse> UpdateDraftItemAsync(PlayFabEntity entity, CatalogUpdateDraftItemRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CatalogUpdateDraftItemRequest](GDK.Net.PlayFab.CatalogUpdateDraftItemRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CatalogUpdateDraftItemResponse](GDK.Net.PlayFab.CatalogUpdateDraftItemResponse.md)\>

