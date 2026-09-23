# <a id="GDK_Net_Store_StoreCanAcquireLicenseResult"></a> Class StoreCanAcquireLicenseResult

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

Result of a can-acquire-licence check. Mirrors <code>XStoreCanAcquireLicenseResult</code>.

```csharp
public sealed class StoreCanAcquireLicenseResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreCanAcquireLicenseResult](GDK.Net.Store.StoreCanAcquireLicenseResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreCanAcquireLicenseResult_LicensableSku"></a> LicensableSku

The SKU identifier that can be licensed (when <xref href="GDK.Net.Store.StoreCanAcquireLicenseResult.Status" data-throw-if-not-resolved="false"></xref> is <xref href="GDK.Net.Store.StoreCanLicenseStatus.Licensable" data-throw-if-not-resolved="false"></xref>).

```csharp
public string LicensableSku { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreCanAcquireLicenseResult_Status"></a> Status

The status of the licence check.

```csharp
public StoreCanLicenseStatus Status { get; }
```

#### Property Value

 [StoreCanLicenseStatus](GDK.Net.Store.StoreCanLicenseStatus.md)

