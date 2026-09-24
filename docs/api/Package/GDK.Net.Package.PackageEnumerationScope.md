# <a id="GDK_Net_Package_PackageEnumerationScope"></a> Enum PackageEnumerationScope

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

The scope of a package enumeration. Mirrors <code>XPackageEnumerationScope</code>.

```csharp
public enum PackageEnumerationScope : uint
```

## Fields

`ThisAndRelated = 1` 

Enumerate this package and related packages.



`ThisOnly = 0` 

Enumerate only the current package.



`ThisPublisher = 2` 

Enumerate all packages from the same publisher.



