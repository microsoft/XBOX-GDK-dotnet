# <a id="GDK_Net_PlayFab_InventoryTransaction"></a> Class InventoryTransaction

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFInventoryTransaction</code>.

```csharp
public sealed class InventoryTransaction
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InventoryTransaction](GDK.Net.PlayFab.InventoryTransaction.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_InventoryTransaction_ApiName"></a> ApiName

<code>ApiName</code>.

```csharp
public string? ApiName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_ClawbackDetails"></a> ClawbackDetails

<code>ClawbackDetails</code>.

```csharp
public InventoryTransactionClawbackDetails? ClawbackDetails { get; set; }
```

#### Property Value

 [InventoryTransactionClawbackDetails](GDK.Net.PlayFab.InventoryTransactionClawbackDetails.md)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_InventoryTransaction_ItemType"></a> ItemType

<code>ItemType</code>.

```csharp
public string? ItemType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_OperationType"></a> OperationType

<code>OperationType</code>.

```csharp
public string? OperationType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_Operations"></a> Operations

<code>Operations</code>.

```csharp
public IReadOnlyList<InventoryTransactionOperation>? Operations { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[InventoryTransactionOperation](GDK.Net.PlayFab.InventoryTransactionOperation.md)\>?

### <a id="GDK_Net_PlayFab_InventoryTransaction_PurchaseDetails"></a> PurchaseDetails

<code>PurchaseDetails</code>.

```csharp
public InventoryTransactionPurchaseDetails? PurchaseDetails { get; set; }
```

#### Property Value

 [InventoryTransactionPurchaseDetails](GDK.Net.PlayFab.InventoryTransactionPurchaseDetails.md)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_RedeemDetails"></a> RedeemDetails

<code>RedeemDetails</code>.

```csharp
public InventoryTransactionRedeemDetails? RedeemDetails { get; set; }
```

#### Property Value

 [InventoryTransactionRedeemDetails](GDK.Net.PlayFab.InventoryTransactionRedeemDetails.md)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_Timestamp"></a> Timestamp

<code>Timestamp</code>.

```csharp
public DateTimeOffset Timestamp { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_PlayFab_InventoryTransaction_TransactionId"></a> TransactionId

<code>TransactionId</code>.

```csharp
public string? TransactionId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_InventoryTransaction_TransferDetails"></a> TransferDetails

<code>TransferDetails</code>.

```csharp
public InventoryTransactionTransferDetails? TransferDetails { get; set; }
```

#### Property Value

 [InventoryTransactionTransferDetails](GDK.Net.PlayFab.InventoryTransactionTransferDetails.md)?

