# <a id="GDK_Net_Package_PackageInstalledEventArgs"></a> Class PackageInstalledEventArgs

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Event arguments for <xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class PackageInstalledEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[PackageInstalledEventArgs](GDK.Net.Package.PackageInstalledEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Package_PackageInstalledEventArgs_Info"></a> Info

Details about the newly installed package.

```csharp
public PackageInfo Info { get; }
```

#### Property Value

 [PackageInfo](GDK.Net.Package.PackageInfo.md)

