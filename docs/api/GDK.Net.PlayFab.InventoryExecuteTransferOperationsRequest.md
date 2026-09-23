# <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest"></a> Class InventoryExecuteTransferOperationsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryExecuteTransferOperationsRequest</code>.

```csharp
public sealed class InventoryExecuteTransferOperationsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryExecuteTransferOperationsRequest](GDK.Net.PlayFab.InventoryExecuteTransferOperationsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_GivingCollectionId"></a> GivingCollectionId

<code>GivingCollectionId</code>.

```csharp
public string? GivingCollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_GivingETag"></a> GivingETag

<code>GivingETag</code>.

```csharp
public string? GivingETag { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_GivingEntity"></a> GivingEntity

<code>GivingEntity</code>.

```csharp
public EntityKey? GivingEntity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_IdempotencyId"></a> IdempotencyId

<code>IdempotencyId</code>.

```csharp
public string? IdempotencyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_Operations"></a> Operations

<code>Operations</code>.

```csharp
public IReadOnlyList<InventoryTransferInventoryItemsOperation>? Operations { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[InventoryTransferInventoryItemsOperation](GDK.Net.PlayFab.InventoryTransferInventoryItemsOperation.md)\>?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_ReceivingCollectionId"></a> ReceivingCollectionId

<code>ReceivingCollectionId</code>.

```csharp
public string? ReceivingCollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryExecuteTransferOperationsRequest_ReceivingEntity"></a> ReceivingEntity

<code>ReceivingEntity</code>.

```csharp
public EntityKey? ReceivingEntity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

