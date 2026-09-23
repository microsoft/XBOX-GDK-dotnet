# <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest"></a> Class InventoryAddInventoryItemsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryAddInventoryItemsRequest</code>.

```csharp
public sealed class InventoryAddInventoryItemsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryAddInventoryItemsRequest](GDK.Net.PlayFab.InventoryAddInventoryItemsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_Amount"></a> Amount

<code>Amount</code>.

```csharp
public int? Amount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_CollectionId"></a> CollectionId

<code>CollectionId</code>.

```csharp
public string? CollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_DurationInSeconds"></a> DurationInSeconds

<code>DurationInSeconds</code>.

```csharp
public double? DurationInSeconds { get; set; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_ETag"></a> ETag

<code>ETag</code>.

```csharp
public string? ETag { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_IdempotencyId"></a> IdempotencyId

<code>IdempotencyId</code>.

```csharp
public string? IdempotencyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_Item"></a> Item

<code>Item</code>.

```csharp
public InventoryInventoryItemReference? Item { get; set; }
```

#### Property Value

 [InventoryInventoryItemReference](GDK.Net.PlayFab.InventoryInventoryItemReference.md)?

### <a id="GDK_Net_PlayFab_InventoryAddInventoryItemsRequest_NewStackValues"></a> NewStackValues

<code>NewStackValues</code>.

```csharp
public InventoryInitialValues? NewStackValues { get; set; }
```

#### Property Value

 [InventoryInitialValues](GDK.Net.PlayFab.InventoryInitialValues.md)?

