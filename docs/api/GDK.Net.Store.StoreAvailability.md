# <a id="GDK_Net_Store_StoreAvailability"></a> Class StoreAvailability

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A purchase availability window for a SKU. Mirrors <code>XStoreAvailability</code>.

```csharp
public sealed class StoreAvailability
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreAvailability](GDK.Net.Store.StoreAvailability.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreAvailability_AvailabilityId"></a> AvailabilityId

Unique identifier for this availability.

```csharp
public string AvailabilityId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreAvailability_EndDate"></a> EndDate

UTC end date of this availability window; zero means no expiry.

```csharp
public DateTimeOffset EndDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Store_StoreAvailability_Price"></a> Price

Price for this availability.

```csharp
public StorePrice Price { get; }
```

#### Property Value

 [StorePrice](GDK.Net.Store.StorePrice.md)

