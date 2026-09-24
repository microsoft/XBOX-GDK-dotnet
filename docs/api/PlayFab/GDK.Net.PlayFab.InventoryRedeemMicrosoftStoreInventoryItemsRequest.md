# <a id="GDK_Net_PlayFab_InventoryRedeemMicrosoftStoreInventoryItemsRequest"></a> Class InventoryRedeemMicrosoftStoreInventoryItemsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryRedeemMicrosoftStoreInventoryItemsRequest</code>.

```csharp
public sealed class InventoryRedeemMicrosoftStoreInventoryItemsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryRedeemMicrosoftStoreInventoryItemsRequest](GDK.Net.PlayFab.InventoryRedeemMicrosoftStoreInventoryItemsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryRedeemMicrosoftStoreInventoryItemsRequest_CollectionId"></a> CollectionId

<code>CollectionId</code>.

```csharp
public string? CollectionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryRedeemMicrosoftStoreInventoryItemsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventoryRedeemMicrosoftStoreInventoryItemsRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_InventoryRedeemMicrosoftStoreInventoryItemsRequest_User"></a> User

The Xbox user this request is made on behalf of.

```csharp
public User? User { get; set; }
```

#### Property Value

 [User](../Users/GDK.Net.Users.User.md)?

