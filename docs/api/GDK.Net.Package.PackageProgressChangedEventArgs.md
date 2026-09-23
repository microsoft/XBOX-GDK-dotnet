# <a id="GDK_Net_Package_PackageProgressChangedEventArgs"></a> Class PackageProgressChangedEventArgs

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Event arguments for <xref href="GDK.Net.Package.PackageInstallationMonitor.ProgressChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class PackageProgressChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[PackageProgressChangedEventArgs](GDK.Net.Package.PackageProgressChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Package_PackageProgressChangedEventArgs_Progress"></a> Progress

The latest installation progress snapshot.

```csharp
public PackageInstallationProgress Progress { get; }
```

#### Property Value

 [PackageInstallationProgress](GDK.Net.Package.PackageInstallationProgress.md)

