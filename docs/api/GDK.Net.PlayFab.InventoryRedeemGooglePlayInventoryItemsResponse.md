# <a id="GDK_Net_PlayFab_InventoryRedeemGooglePlayInventoryItemsResponse"></a> Class InventoryRedeemGooglePlayInventoryItemsResponse

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryRedeemGooglePlayInventoryItemsResponse</code>.

```csharp
public sealed class InventoryRedeemGooglePlayInventoryItemsResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryRedeemGooglePlayInventoryItemsResponse](GDK.Net.PlayFab.InventoryRedeemGooglePlayInventoryItemsResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryRedeemGooglePlayInventoryItemsResponse_Failed"></a> Failed

<code>Failed</code>.

```csharp
public IReadOnlyList<InventoryRedemptionFailure>? Failed { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[InventoryRedemptionFailure](GDK.Net.PlayFab.InventoryRedemptionFailure.md)\>?

### <a id="GDK_Net_PlayFab_InventoryRedeemGooglePlayInventoryItemsResponse_Succeeded"></a> Succeeded

<code>Succeeded</code>.

```csharp
public IReadOnlyList<InventoryRedemptionSuccess>? Succeeded { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[InventoryRedemptionSuccess](GDK.Net.PlayFab.InventoryRedemptionSuccess.md)\>?

### <a id="GDK_Net_PlayFab_InventoryRedeemGooglePlayInventoryItemsResponse_TransactionIds"></a> TransactionIds

<code>TransactionIds</code>.

```csharp
public IReadOnlyList<string>? TransactionIds { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

