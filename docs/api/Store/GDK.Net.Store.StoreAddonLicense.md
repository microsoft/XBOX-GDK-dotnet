# <a id="GDK_Net_Store_StoreAddonLicense"></a> Class StoreAddonLicense

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

An add-on (DLC) licence. Mirrors <code>XStoreAddonLicense</code>.

```csharp
public sealed class StoreAddonLicense
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreAddonLicense](GDK.Net.Store.StoreAddonLicense.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreAddonLicense_ExpirationDate"></a> ExpirationDate

Expiry date of the add-on licence; <xref href="System.DateTimeOffset.MinValue" data-throw-if-not-resolved="false"></xref> for permanent add-ons.

```csharp
public DateTimeOffset ExpirationDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Store_StoreAddonLicense_InAppOfferToken"></a> InAppOfferToken

The in-app offer token associated with this add-on.

```csharp
public string InAppOfferToken { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreAddonLicense_IsActive"></a> IsActive

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the add-on licence is currently active.

```csharp
public bool IsActive { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreAddonLicense_SkuStoreId"></a> SkuStoreId

The Store SKU identifier the add-on licence is for.

```csharp
public string SkuStoreId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

