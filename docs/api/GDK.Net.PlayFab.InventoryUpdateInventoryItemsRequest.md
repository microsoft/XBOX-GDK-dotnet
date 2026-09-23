# <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest"></a> Class InventoryUpdateInventoryItemsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryUpdateInventoryItemsRequest</code>.

```csharp
public sealed class InventoryUpdateInventoryItemsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryUpdateInventoryItemsRequest](GDK.Net.PlayFab.InventoryUpdateInventoryItemsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_CollectionId"></a> CollectionId

<code>CollectionId</code>.

```csharp
public string? CollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_ETag"></a> ETag

<code>ETag</code>.

```csharp
public string? ETag { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_IdempotencyId"></a> IdempotencyId

<code>IdempotencyId</code>.

```csharp
public string? IdempotencyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryUpdateInventoryItemsRequest_Item"></a> Item

<code>Item</code>.

```csharp
public InventoryInventoryItem? Item { get; set; }
```

#### Property Value

 [InventoryInventoryItem](GDK.Net.PlayFab.InventoryInventoryItem.md)?

