# <a id="GDK_Net_Store_StorePackageUpdate"></a> Class StorePackageUpdate

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A pending package update. Mirrors <code>XStorePackageUpdate</code>.

```csharp
public sealed class StorePackageUpdate
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StorePackageUpdate](GDK.Net.Store.StorePackageUpdate.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StorePackageUpdate_IsMandatory"></a> IsMandatory

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the update is mandatory.

```csharp
public bool IsMandatory { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StorePackageUpdate_PackageIdentifier"></a> PackageIdentifier

Package identifier of the package with a pending update.

```csharp
public string PackageIdentifier { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

