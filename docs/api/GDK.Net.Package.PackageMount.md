# <a id="GDK_Net_Package_PackageMount"></a> Class PackageMount

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

A mounted package. Owns an <code>XPackageMountHandle</code> and exposes the mount path.

```csharp
public sealed class PackageMount : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PackageMount](GDK.Net.Package.PackageMount.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Obtain an instance via <xref href="GDK.Net.Package.PackageMount.MountWithUiAsync(System.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>. Dispose when the mount is no longer
needed; the native handle is released with <code>XPackageCloseMountHandle</code>.

## Properties

### <a id="GDK_Net_Package_PackageMount_MountPath"></a> MountPath

The file-system path to the mounted package
(<code>XPackageGetMountPathSize</code> + <code>XPackageGetMountPath</code>).

```csharp
public string MountPath { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_Package_PackageMount_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Package_PackageMount_MountWithUiAsync_System_String_System_Threading_CancellationToken_"></a> MountWithUiAsync\(string, CancellationToken\)

Mounts a package after prompting the user to download any missing content
(<code>XPackageMountWithUiAsync</code> / <code>XPackageMountWithUiResult</code>).

```csharp
public static Task<PackageMount> MountWithUiAsync(string packageIdentifier, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier. Obtain from
<xref href="GDK.Net.Package.GamePackage.GetCurrentPackageIdentifier" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Package.GamePackage.EnumeratePackages(GDK.Net.Package.PackageKind%2cGDK.Net.Package.PackageEnumerationScope)" data-throw-if-not-resolved="false"></xref>.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PackageMount](GDK.Net.Package.PackageMount.md)\>

A <xref href="GDK.Net.Package.PackageMount" data-throw-if-not-resolved="false"></xref> instance. Dispose when done.

#### Remarks

The deprecated synchronous <code>XPackageMount</code> is not available in
<code>xgameruntime.thunks.dll</code> and is therefore not projected; this async
overload is the only supported mount path.

