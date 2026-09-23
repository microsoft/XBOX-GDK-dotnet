# <a id="GDK_Net_Store_StorePrice"></a> Class StorePrice

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

Price information for a product or SKU. Mirrors <code>XStorePrice</code>.

```csharp
public sealed class StorePrice
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StorePrice](GDK.Net.Store.StorePrice.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StorePrice_BasePrice"></a> BasePrice

The base (undiscounted) price.

```csharp
public float BasePrice { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_Store_StorePrice_CurrencyCode"></a> CurrencyCode

ISO 4217 currency code (e.g., "USD").

```csharp
public string CurrencyCode { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StorePrice_FormattedBasePrice"></a> FormattedBasePrice

Formatted base price string.

```csharp
public string FormattedBasePrice { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StorePrice_FormattedPrice"></a> FormattedPrice

Formatted current price string.

```csharp
public string FormattedPrice { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StorePrice_FormattedRecurrencePrice"></a> FormattedRecurrencePrice

Formatted recurring price string for subscriptions.

```csharp
public string FormattedRecurrencePrice { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StorePrice_IsOnSale"></a> IsOnSale

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the product is currently on sale.

```csharp
public bool IsOnSale { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StorePrice_Price"></a> Price

The current price (may be a sale price).

```csharp
public float Price { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_Store_StorePrice_RecurrencePrice"></a> RecurrencePrice

The recurring price for subscriptions.

```csharp
public float RecurrencePrice { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_Store_StorePrice_SaleEndDate"></a> SaleEndDate

When the current sale ends; <xref href="System.DateTimeOffset.MinValue" data-throw-if-not-resolved="false"></xref> if not on sale.

```csharp
public DateTimeOffset SaleEndDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

