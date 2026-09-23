# <a id="GDK_Net_Store_StoreProduct"></a> Class StoreProduct

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A Store product. Mirrors <code>XStoreProduct</code>.

```csharp
public sealed class StoreProduct
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreProduct](GDK.Net.Store.StoreProduct.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Returned from product queries. The <code>XStoreProduct</code> struct and all nested arrays are only
valid during the native enumeration callback; this managed type is a complete deep copy.

## Properties

### <a id="GDK_Net_Store_StoreProduct_Description"></a> Description

Localized description.

```csharp
public string Description { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreProduct_HasDigitalDownload"></a> HasDigitalDownload

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this product has a digital download.

```csharp
public bool HasDigitalDownload { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreProduct_Images"></a> Images

Images associated with this product.

```csharp
public IReadOnlyList<StoreImage> Images { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreImage](GDK.Net.Store.StoreImage.md)\>

### <a id="GDK_Net_Store_StoreProduct_InAppOfferToken"></a> InAppOfferToken

In-app offer token (for add-ons listed via in-app offers).

```csharp
public string InAppOfferToken { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreProduct_IsInUserCollection"></a> IsInUserCollection

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the current user owns this product.

```csharp
public bool IsInUserCollection { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreProduct_Keywords"></a> Keywords

Searchable keywords.

```csharp
public IReadOnlyList<string> Keywords { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_Store_StoreProduct_Language"></a> Language

BCP-47 language tag for the localized strings.

```csharp
public string Language { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreProduct_LinkUri"></a> LinkUri

Deep-link URI to the product's Store page.

```csharp
public string LinkUri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreProduct_Price"></a> Price

Pricing information for the default SKU.

```csharp
public StorePrice Price { get; }
```

#### Property Value

 [StorePrice](GDK.Net.Store.StorePrice.md)

### <a id="GDK_Net_Store_StoreProduct_ProductKind"></a> ProductKind

The type of product.

```csharp
public StoreProductKind ProductKind { get; }
```

#### Property Value

 [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

### <a id="GDK_Net_Store_StoreProduct_Skus"></a> Skus

All SKUs for this product.

```csharp
public IReadOnlyList<StoreSku> Skus { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreSku](GDK.Net.Store.StoreSku.md)\>

### <a id="GDK_Net_Store_StoreProduct_StoreId"></a> StoreId

The product's Store ID (e.g., "9WZDNCRFJBMP").

```csharp
public string StoreId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreProduct_Title"></a> Title

Localized display title.

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreProduct_Videos"></a> Videos

Videos associated with this product.

```csharp
public IReadOnlyList<StoreVideo> Videos { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreVideo](GDK.Net.Store.StoreVideo.md)\>

## Methods

### <a id="GDK_Net_Store_StoreProduct_ToString"></a> ToString\(\)

Returns a string for diagnostics.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

