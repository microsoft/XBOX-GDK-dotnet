# <a id="GDK_Net_Package_PackageInfo"></a> Class PackageInfo

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Information about an installed or available package.
Mirrors <code>XPackageDetails</code>.

```csharp
public sealed class PackageInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PackageInfo](GDK.Net.Package.PackageInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<code>PackageIdentifier</code> is a durable opaque string that identifies a package across versions.
It is not human-readable; use <code>DisplayName</code> for UI.

## Properties

### <a id="GDK_Net_Package_PackageInfo_AgeRestricted"></a> AgeRestricted

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the package is age-restricted on this device.

```csharp
public bool AgeRestricted { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Package_PackageInfo_Count"></a> Count

Total count in the current enumeration batch.

```csharp
public uint Count { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Package_PackageInfo_Description"></a> Description

Human-readable description.

```csharp
public string Description { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageInfo_DisplayName"></a> DisplayName

Human-readable display name.

```csharp
public string DisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageInfo_Index"></a> Index

Zero-based index within the current enumeration batch.

```csharp
public uint Index { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Package_PackageInfo_Installing"></a> Installing

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> while the package is being installed.

```csharp
public bool Installing { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Package_PackageInfo_Kind"></a> Kind

Whether this is a game or content package.

```csharp
public PackageKind Kind { get; }
```

#### Property Value

 [PackageKind](GDK.Net.Package.PackageKind.md)

### <a id="GDK_Net_Package_PackageInfo_PackageIdentifier"></a> PackageIdentifier

The opaque package identifier string (<code>XPACKAGE_IDENTIFIER_MAX_LENGTH</code> = 33 characters).

```csharp
public string PackageIdentifier { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageInfo_Publisher"></a> Publisher

Publisher name.

```csharp
public string Publisher { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageInfo_StoreId"></a> StoreId

Store product id, or empty string if not available.

```csharp
public string StoreId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageInfo_TitleId"></a> TitleId

The package title id string, or empty string if not available.

```csharp
public string TitleId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageInfo_Version"></a> Version

The package version.

```csharp
public PackageVersion Version { get; }
```

#### Property Value

 [PackageVersion](GDK.Net.Package.PackageVersion.md)

## Methods

### <a id="GDK_Net_Package_PackageInfo_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

