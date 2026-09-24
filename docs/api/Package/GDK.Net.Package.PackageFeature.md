# <a id="GDK_Net_Package_PackageFeature"></a> Class PackageFeature

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

A feature entry returned by <xref href="GDK.Net.Package.GamePackage.EnumerateFeatures(System.String)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XPackageFeature</code>.

```csharp
public sealed class PackageFeature
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PackageFeature](GDK.Net.Package.PackageFeature.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Package_PackageFeature_DisplayName"></a> DisplayName

Human-readable display name.

```csharp
public string DisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageFeature_Hidden"></a> Hidden

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the feature should not be shown in UI.

```csharp
public bool Hidden { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Package_PackageFeature_Id"></a> Id

The feature's unique identifier string.

```csharp
public string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_PackageFeature_StoreIds"></a> StoreIds

The store product ids for the content packs that provide this feature.

```csharp
public IReadOnlyList<string> StoreIds { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_Package_PackageFeature_Tags"></a> Tags

Space-separated tag string associated with this feature.

```csharp
public string Tags { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

