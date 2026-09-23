# <a id="GDK_Net_PlayFab_Inventory"></a> Class Inventory

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Inventory service (<code>PFInventory.h</code>).

```csharp
public static class Inventory
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Inventory](GDK.Net.PlayFab.Inventory.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Inventory_AddInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_System_Threading_CancellationToken_"></a> AddInventoryItemsAsync\(PlayFabEntity, InventoryAddInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryAddInventoryItemsAsync</code>.

```csharp
public static Task<InventoryAddInventoryItemsResponse> AddInventoryItemsAsync(PlayFabEntity entity, InventoryAddInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryAddInventoryItemsRequest](GDK.Net.PlayFab.InventoryAddInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryAddInventoryItemsResponse](GDK.Net.PlayFab.InventoryAddInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_DeleteInventoryCollectionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryDeleteInventoryCollectionRequest_System_Threading_CancellationToken_"></a> DeleteInventoryCollectionAsync\(PlayFabEntity, InventoryDeleteInventoryCollectionRequest, CancellationToken\)

Calls <code>PFInventoryDeleteInventoryCollectionAsync</code>.

```csharp
public static Task DeleteInventoryCollectionAsync(PlayFabEntity entity, InventoryDeleteInventoryCollectionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryDeleteInventoryCollectionRequest](GDK.Net.PlayFab.InventoryDeleteInventoryCollectionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Inventory_DeleteInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryDeleteInventoryItemsRequest_System_Threading_CancellationToken_"></a> DeleteInventoryItemsAsync\(PlayFabEntity, InventoryDeleteInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryDeleteInventoryItemsAsync</code>.

```csharp
public static Task<InventoryDeleteInventoryItemsResponse> DeleteInventoryItemsAsync(PlayFabEntity entity, InventoryDeleteInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryDeleteInventoryItemsRequest](GDK.Net.PlayFab.InventoryDeleteInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryDeleteInventoryItemsResponse](GDK.Net.PlayFab.InventoryDeleteInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_ExecuteInventoryOperationsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryExecuteInventoryOperationsRequest_System_Threading_CancellationToken_"></a> ExecuteInventoryOperationsAsync\(PlayFabEntity, InventoryExecuteInventoryOperationsRequest, CancellationToken\)

Calls <code>PFInventoryExecuteInventoryOperationsAsync</code>.

```csharp
public static Task<InventoryExecuteInventoryOperationsResponse> ExecuteInventoryOperationsAsync(PlayFabEntity entity, InventoryExecuteInventoryOperationsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryExecuteInventoryOperationsRequest](GDK.Net.PlayFab.InventoryExecuteInventoryOperationsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryExecuteInventoryOperationsResponse](GDK.Net.PlayFab.InventoryExecuteInventoryOperationsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_ExecuteTransferOperationsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_System_Threading_CancellationToken_"></a> ExecuteTransferOperationsAsync\(PlayFabEntity, InventoryExecuteTransferOperationsRequest, CancellationToken\)

Calls <code>PFInventoryExecuteTransferOperationsAsync</code>.

```csharp
public static Task<InventoryExecuteTransferOperationsResponse> ExecuteTransferOperationsAsync(PlayFabEntity entity, InventoryExecuteTransferOperationsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryExecuteTransferOperationsRequest](GDK.Net.PlayFab.InventoryExecuteTransferOperationsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryExecuteTransferOperationsResponse](GDK.Net.PlayFab.InventoryExecuteTransferOperationsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_GetInventoryCollectionIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryGetInventoryCollectionIdsRequest_System_Threading_CancellationToken_"></a> GetInventoryCollectionIdsAsync\(PlayFabEntity, InventoryGetInventoryCollectionIdsRequest, CancellationToken\)

Calls <code>PFInventoryGetInventoryCollectionIdsAsync</code>.

```csharp
public static Task<InventoryGetInventoryCollectionIdsResponse> GetInventoryCollectionIdsAsync(PlayFabEntity entity, InventoryGetInventoryCollectionIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryGetInventoryCollectionIdsRequest](GDK.Net.PlayFab.InventoryGetInventoryCollectionIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryGetInventoryCollectionIdsResponse](GDK.Net.PlayFab.InventoryGetInventoryCollectionIdsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_GetInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryGetInventoryItemsRequest_System_Threading_CancellationToken_"></a> GetInventoryItemsAsync\(PlayFabEntity, InventoryGetInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryGetInventoryItemsAsync</code>.

```csharp
public static Task<InventoryGetInventoryItemsResponse> GetInventoryItemsAsync(PlayFabEntity entity, InventoryGetInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryGetInventoryItemsRequest](GDK.Net.PlayFab.InventoryGetInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryGetInventoryItemsResponse](GDK.Net.PlayFab.InventoryGetInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_GetInventoryOperationStatusAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryGetInventoryOperationStatusRequest_System_Threading_CancellationToken_"></a> GetInventoryOperationStatusAsync\(PlayFabEntity, InventoryGetInventoryOperationStatusRequest, CancellationToken\)

Calls <code>PFInventoryGetInventoryOperationStatusAsync</code>.

```csharp
public static Task<InventoryGetInventoryOperationStatusResponse> GetInventoryOperationStatusAsync(PlayFabEntity entity, InventoryGetInventoryOperationStatusRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryGetInventoryOperationStatusRequest](GDK.Net.PlayFab.InventoryGetInventoryOperationStatusRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryGetInventoryOperationStatusResponse](GDK.Net.PlayFab.InventoryGetInventoryOperationStatusResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_GetTransactionHistoryAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryGetTransactionHistoryRequest_System_Threading_CancellationToken_"></a> GetTransactionHistoryAsync\(PlayFabEntity, InventoryGetTransactionHistoryRequest, CancellationToken\)

Calls <code>PFInventoryGetTransactionHistoryAsync</code>.

```csharp
public static Task<InventoryGetTransactionHistoryResponse> GetTransactionHistoryAsync(PlayFabEntity entity, InventoryGetTransactionHistoryRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryGetTransactionHistoryRequest](GDK.Net.PlayFab.InventoryGetTransactionHistoryRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryGetTransactionHistoryResponse](GDK.Net.PlayFab.InventoryGetTransactionHistoryResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_PurchaseInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryPurchaseInventoryItemsRequest_System_Threading_CancellationToken_"></a> PurchaseInventoryItemsAsync\(PlayFabEntity, InventoryPurchaseInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryPurchaseInventoryItemsAsync</code>.

```csharp
public static Task<InventoryPurchaseInventoryItemsResponse> PurchaseInventoryItemsAsync(PlayFabEntity entity, InventoryPurchaseInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryPurchaseInventoryItemsRequest](GDK.Net.PlayFab.InventoryPurchaseInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryPurchaseInventoryItemsResponse](GDK.Net.PlayFab.InventoryPurchaseInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_RedeemAppleAppStoreInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryRedeemAppleAppStoreInventoryItemsRequest_System_Threading_CancellationToken_"></a> RedeemAppleAppStoreInventoryItemsAsync\(PlayFabEntity, InventoryRedeemAppleAppStoreInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryRedeemAppleAppStoreInventoryItemsAsync</code>.

```csharp
public static Task<InventoryRedeemAppleAppStoreInventoryItemsResponse> RedeemAppleAppStoreInventoryItemsAsync(PlayFabEntity entity, InventoryRedeemAppleAppStoreInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryRedeemAppleAppStoreInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemAppleAppStoreInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryRedeemAppleAppStoreInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemAppleAppStoreInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_RedeemGooglePlayInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryRedeemGooglePlayInventoryItemsRequest_System_Threading_CancellationToken_"></a> RedeemGooglePlayInventoryItemsAsync\(PlayFabEntity, InventoryRedeemGooglePlayInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryRedeemGooglePlayInventoryItemsAsync</code>.

```csharp
public static Task<InventoryRedeemGooglePlayInventoryItemsResponse> RedeemGooglePlayInventoryItemsAsync(PlayFabEntity entity, InventoryRedeemGooglePlayInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryRedeemGooglePlayInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemGooglePlayInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryRedeemGooglePlayInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemGooglePlayInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_RedeemMicrosoftStoreInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryRedeemMicrosoftStoreInventoryItemsRequest_System_Threading_CancellationToken_"></a> RedeemMicrosoftStoreInventoryItemsAsync\(PlayFabEntity, InventoryRedeemMicrosoftStoreInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryRedeemMicrosoftStoreInventoryItemsAsync</code>.

```csharp
public static Task<InventoryRedeemMicrosoftStoreInventoryItemsResponse> RedeemMicrosoftStoreInventoryItemsAsync(PlayFabEntity entity, InventoryRedeemMicrosoftStoreInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryRedeemMicrosoftStoreInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemMicrosoftStoreInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryRedeemMicrosoftStoreInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemMicrosoftStoreInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_RedeemNintendoEShopInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryRedeemNintendoEShopInventoryItemsRequest_System_Threading_CancellationToken_"></a> RedeemNintendoEShopInventoryItemsAsync\(PlayFabEntity, InventoryRedeemNintendoEShopInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryRedeemNintendoEShopInventoryItemsAsync</code>.

```csharp
public static Task<InventoryRedeemNintendoEShopInventoryItemsResponse> RedeemNintendoEShopInventoryItemsAsync(PlayFabEntity entity, InventoryRedeemNintendoEShopInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryRedeemNintendoEShopInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemNintendoEShopInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryRedeemNintendoEShopInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemNintendoEShopInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_RedeemPlayStationStoreInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryRedeemPlayStationStoreInventoryItemsRequest_System_Threading_CancellationToken_"></a> RedeemPlayStationStoreInventoryItemsAsync\(PlayFabEntity, InventoryRedeemPlayStationStoreInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryRedeemPlayStationStoreInventoryItemsAsync</code>.

```csharp
public static Task<InventoryRedeemPlayStationStoreInventoryItemsResponse> RedeemPlayStationStoreInventoryItemsAsync(PlayFabEntity entity, InventoryRedeemPlayStationStoreInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryRedeemPlayStationStoreInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemPlayStationStoreInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryRedeemPlayStationStoreInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemPlayStationStoreInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_RedeemSteamInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryRedeemSteamInventoryItemsRequest_System_Threading_CancellationToken_"></a> RedeemSteamInventoryItemsAsync\(PlayFabEntity, InventoryRedeemSteamInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryRedeemSteamInventoryItemsAsync</code>.

```csharp
public static Task<InventoryRedeemSteamInventoryItemsResponse> RedeemSteamInventoryItemsAsync(PlayFabEntity entity, InventoryRedeemSteamInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryRedeemSteamInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemSteamInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryRedeemSteamInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemSteamInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_SubtractInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_System_Threading_CancellationToken_"></a> SubtractInventoryItemsAsync\(PlayFabEntity, InventorySubtractInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventorySubtractInventoryItemsAsync</code>.

```csharp
public static Task<InventorySubtractInventoryItemsResponse> SubtractInventoryItemsAsync(PlayFabEntity entity, InventorySubtractInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventorySubtractInventoryItemsRequest](GDK.Net.PlayFab.InventorySubtractInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventorySubtractInventoryItemsResponse](GDK.Net.PlayFab.InventorySubtractInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_TransferInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_System_Threading_CancellationToken_"></a> TransferInventoryItemsAsync\(PlayFabEntity, InventoryTransferInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryTransferInventoryItemsAsync</code>.

```csharp
public static Task<InventoryTransferInventoryItemsResponse> TransferInventoryItemsAsync(PlayFabEntity entity, InventoryTransferInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryTransferInventoryItemsRequest](GDK.Net.PlayFab.InventoryTransferInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryTransferInventoryItemsResponse](GDK.Net.PlayFab.InventoryTransferInventoryItemsResponse.md)\>

### <a id="GDK_Net_PlayFab_Inventory_UpdateInventoryItemsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_System_Threading_CancellationToken_"></a> UpdateInventoryItemsAsync\(PlayFabEntity, InventoryUpdateInventoryItemsRequest, CancellationToken\)

Calls <code>PFInventoryUpdateInventoryItemsAsync</code>.

```csharp
public static Task<InventoryUpdateInventoryItemsResponse> UpdateInventoryItemsAsync(PlayFabEntity entity, InventoryUpdateInventoryItemsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [InventoryUpdateInventoryItemsRequest](GDK.Net.PlayFab.InventoryUpdateInventoryItemsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[InventoryUpdateInventoryItemsResponse](GDK.Net.PlayFab.InventoryUpdateInventoryItemsResponse.md)\>

