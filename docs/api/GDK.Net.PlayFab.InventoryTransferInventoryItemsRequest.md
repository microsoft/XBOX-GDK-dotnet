# <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest"></a> Class InventoryTransferInventoryItemsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryTransferInventoryItemsRequest</code>.

```csharp
public sealed class InventoryTransferInventoryItemsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryTransferInventoryItemsRequest](GDK.Net.PlayFab.InventoryTransferInventoryItemsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_Amount"></a> Amount

<code>Amount</code>.

```csharp
public int? Amount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_DeleteEmptyStacks"></a> DeleteEmptyStacks

<code>DeleteEmptyStacks</code>.

```csharp
public bool DeleteEmptyStacks { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_GivingCollectionId"></a> GivingCollectionId

<code>GivingCollectionId</code>.

```csharp
public string? GivingCollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_GivingETag"></a> GivingETag

<code>GivingETag</code>.

```csharp
public string? GivingETag { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_GivingEntity"></a> GivingEntity

<code>GivingEntity</code>.

```csharp
public EntityKey? GivingEntity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_GivingItem"></a> GivingItem

<code>GivingItem</code>.

```csharp
public InventoryInventoryItemReference? GivingItem { get; set; }
```

#### Property Value

 [InventoryInventoryItemReference](GDK.Net.PlayFab.InventoryInventoryItemReference.md)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_IdempotencyId"></a> IdempotencyId

<code>IdempotencyId</code>.

```csharp
public string? IdempotencyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_NewStackValues"></a> NewStackValues

<code>NewStackValues</code>.

```csharp
public InventoryInitialValues? NewStackValues { get; set; }
```

#### Property Value

 [InventoryInitialValues](GDK.Net.PlayFab.InventoryInitialValues.md)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_ReceivingCollectionId"></a> ReceivingCollectionId

<code>ReceivingCollectionId</code>.

```csharp
public string? ReceivingCollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_ReceivingEntity"></a> ReceivingEntity

<code>ReceivingEntity</code>.

```csharp
public EntityKey? ReceivingEntity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventoryTransferInventoryItemsRequest_ReceivingItem"></a> ReceivingItem

<code>ReceivingItem</code>.

```csharp
public InventoryInventoryItemReference? ReceivingItem { get; set; }
```

#### Property Value

 [InventoryInventoryItemReference](GDK.Net.PlayFab.InventoryInventoryItemReference.md)?

