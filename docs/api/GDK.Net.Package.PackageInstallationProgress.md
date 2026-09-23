# <a id="GDK_Net_Package_PackageInstallationProgress"></a> Struct PackageInstallationProgress

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

A snapshot of a package's installation progress.
Mirrors <code>XPackageInstallationProgress</code>.

```csharp
public readonly struct PackageInstallationProgress
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Package_PackageInstallationProgress_Completed"></a> Completed

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when installation is fully complete.

```csharp
public bool Completed { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Package_PackageInstallationProgress_Fraction"></a> Fraction

A value in [0, 1] representing download fraction, or 0 when total is unknown.

```csharp
public double Fraction { get; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

### <a id="GDK_Net_Package_PackageInstallationProgress_InstalledBytes"></a> InstalledBytes

Bytes installed so far.

```csharp
public ulong InstalledBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Package_PackageInstallationProgress_LaunchBytes"></a> LaunchBytes

Bytes required before the title can be launched.

```csharp
public ulong LaunchBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Package_PackageInstallationProgress_Launchable"></a> Launchable

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when enough data is installed for the title to launch.

```csharp
public bool Launchable { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Package_PackageInstallationProgress_TotalBytes"></a> TotalBytes

Total bytes that will be installed.

```csharp
public ulong TotalBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

