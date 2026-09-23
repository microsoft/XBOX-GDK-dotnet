# <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest"></a> Class InventorySubtractInventoryItemsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventorySubtractInventoryItemsRequest</code>.

```csharp
public sealed class InventorySubtractInventoryItemsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventorySubtractInventoryItemsRequest](GDK.Net.PlayFab.InventorySubtractInventoryItemsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_Amount"></a> Amount

<code>Amount</code>.

```csharp
public int? Amount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_CollectionId"></a> CollectionId

<code>CollectionId</code>.

```csharp
public string? CollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_DeleteEmptyStacks"></a> DeleteEmptyStacks

<code>DeleteEmptyStacks</code>.

```csharp
public bool DeleteEmptyStacks { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_DurationInSeconds"></a> DurationInSeconds

<code>DurationInSeconds</code>.

```csharp
public double? DurationInSeconds { get; set; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_ETag"></a> ETag

<code>ETag</code>.

```csharp
public string? ETag { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_IdempotencyId"></a> IdempotencyId

<code>IdempotencyId</code>.

```csharp
public string? IdempotencyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventorySubtractInventoryItemsRequest_Item"></a> Item

<code>Item</code>.

```csharp
public InventoryInventoryItemReference? Item { get; set; }
```

#### Property Value

 [InventoryInventoryItemReference](GDK.Net.PlayFab.InventoryInventoryItemReference.md)?

