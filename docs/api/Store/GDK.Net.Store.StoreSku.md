# <a id="GDK_Net_Store_StoreSku"></a> Class StoreSku

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A Stock Keeping Unit (SKU) within a product. Mirrors <code>XStoreSku</code>.

```csharp
public sealed class StoreSku
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreSku](GDK.Net.Store.StoreSku.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreSku_Availabilities"></a> Availabilities

Available purchase windows for this SKU.

```csharp
public IReadOnlyList<StoreAvailability> Availabilities { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreAvailability](GDK.Net.Store.StoreAvailability.md)\>

### <a id="GDK_Net_Store_StoreSku_BundledSkus"></a> BundledSkus

Store IDs of the SKUs bundled into this SKU.

```csharp
public IReadOnlyList<string> BundledSkus { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_Store_StoreSku_CollectionData"></a> CollectionData

Collection (entitlement) data when <xref href="GDK.Net.Store.StoreSku.IsInUserCollection" data-throw-if-not-resolved="false"></xref> is true.

```csharp
public StoreCollectionData CollectionData { get; }
```

#### Property Value

 [StoreCollectionData](GDK.Net.Store.StoreCollectionData.md)

### <a id="GDK_Net_Store_StoreSku_Description"></a> Description

Localized description of the SKU.

```csharp
public string Description { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreSku_Images"></a> Images

Images associated with this SKU.

```csharp
public IReadOnlyList<StoreImage> Images { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreImage](GDK.Net.Store.StoreImage.md)\>

### <a id="GDK_Net_Store_StoreSku_IsInUserCollection"></a> IsInUserCollection

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the user owns this SKU.

```csharp
public bool IsInUserCollection { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreSku_IsSubscription"></a> IsSubscription

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this SKU is a subscription.

```csharp
public bool IsSubscription { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreSku_IsTrial"></a> IsTrial

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this SKU is a trial.

```csharp
public bool IsTrial { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreSku_Language"></a> Language

BCP-47 language tag for the localized strings.

```csharp
public string Language { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreSku_Price"></a> Price

Pricing information.

```csharp
public StorePrice Price { get; }
```

#### Property Value

 [StorePrice](GDK.Net.Store.StorePrice.md)

### <a id="GDK_Net_Store_StoreSku_SkuId"></a> SkuId

The Store SKU identifier.

```csharp
public string SkuId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreSku_SubscriptionInfo"></a> SubscriptionInfo

Subscription billing details when <xref href="GDK.Net.Store.StoreSku.IsSubscription" data-throw-if-not-resolved="false"></xref> is true.

```csharp
public StoreSubscriptionInfo SubscriptionInfo { get; }
```

#### Property Value

 [StoreSubscriptionInfo](GDK.Net.Store.StoreSubscriptionInfo.md)

### <a id="GDK_Net_Store_StoreSku_Title"></a> Title

Localized title of the SKU.

```csharp
public string Title { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreSku_Videos"></a> Videos

Videos associated with this SKU.

```csharp
public IReadOnlyList<StoreVideo> Videos { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreVideo](GDK.Net.Store.StoreVideo.md)\>

