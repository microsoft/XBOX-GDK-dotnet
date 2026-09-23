# <a id="GDK_Net_Store_StoreCollectionData"></a> Class StoreCollectionData

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

Collection ownership data for a SKU. Mirrors <code>XStoreCollectionData</code>.

```csharp
public sealed class StoreCollectionData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreCollectionData](GDK.Net.Store.StoreCollectionData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreCollectionData_AcquiredDate"></a> AcquiredDate

Date the user acquired this SKU.

```csharp
public DateTimeOffset AcquiredDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Store_StoreCollectionData_CampaignId"></a> CampaignId

Campaign ID used when acquiring this item (may be empty).

```csharp
public string CampaignId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreCollectionData_DeveloperOfferId"></a> DeveloperOfferId

Developer offer ID used when acquiring this item (may be empty).

```csharp
public string DeveloperOfferId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreCollectionData_EndDate"></a> EndDate

End of the entitlement window; <xref href="System.DateTimeOffset.MinValue" data-throw-if-not-resolved="false"></xref> for permanent items.

```csharp
public DateTimeOffset EndDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Store_StoreCollectionData_IsTrial"></a> IsTrial

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this is a trial entitlement.

```csharp
public bool IsTrial { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreCollectionData_Quantity"></a> Quantity

Quantity owned (for consumables).

```csharp
public uint Quantity { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Store_StoreCollectionData_StartDate"></a> StartDate

Start of the entitlement window (for time-limited items).

```csharp
public DateTimeOffset StartDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Store_StoreCollectionData_TrialTimeRemainingInSeconds"></a> TrialTimeRemainingInSeconds

Seconds remaining in the trial.

```csharp
public uint TrialTimeRemainingInSeconds { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

