# <a id="GDK_Net_Package_PackageInstallationMonitor"></a> Class PackageInstallationMonitor

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Tracks the installation progress of one or more package chunks.

```csharp
public sealed class PackageInstallationMonitor : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PackageInstallationMonitor](GDK.Net.Package.PackageInstallationMonitor.md)

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

<p>
Obtain an instance via <xref href="GDK.Net.Package.GamePackage.InstallChunksAsync(System.String%2cGDK.Net.Package.PackageChunkSelector%5b%5d%2cSystem.UInt32%2cSystem.Boolean%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>,
<xref href="GDK.Net.Package.GamePackage.InstallChunks(System.String%2cGDK.Net.Package.PackageChunkSelector%5b%5d%2cSystem.UInt32%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref>, or
<xref href="GDK.Net.Package.GamePackage.CreateInstallationMonitor(System.String%2cGDK.Net.Package.PackageChunkSelector%5b%5d%2cSystem.UInt32)" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
Subscribe to <xref href="GDK.Net.Package.PackageInstallationMonitor.ProgressChanged" data-throw-if-not-resolved="false"></xref> to receive native callbacks when progress updates.
The registration token is released with <code>wait:true</code> on <xref href="GDK.Net.Package.PackageInstallationMonitor.Dispose" data-throw-if-not-resolved="false"></xref>, so no
callback is in flight once the object is disposed.
</p>

## Methods

### <a id="GDK_Net_Package_PackageInstallationMonitor_Create_System_String_GDK_Net_Package_PackageChunkSelector___System_UInt32_"></a> Create\(string, PackageChunkSelector\[\]?, uint\)

Creates a monitor for all chunks of the specified package without starting an install
(<code>XPackageCreateInstallationMonitor</code>).

```csharp
public static PackageInstallationMonitor Create(string packageIdentifier, PackageChunkSelector[]? selectors = null, uint minimumUpdateIntervalMs = 0)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier.

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]?

Optional chunk selectors to narrow the monitor scope.
Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or an empty array to monitor all chunks.

`minimumUpdateIntervalMs` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Minimum milliseconds between <xref href="GDK.Net.Package.PackageInstallationMonitor.ProgressChanged" data-throw-if-not-resolved="false"></xref> callbacks; 0 for no throttling.

#### Returns

 [PackageInstallationMonitor](GDK.Net.Package.PackageInstallationMonitor.md)

### <a id="GDK_Net_Package_PackageInstallationMonitor_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Package_PackageInstallationMonitor_GetProgress"></a> GetProgress\(\)

Reads a progress snapshot from the monitor's cached state
(<code>XPackageGetInstallationProgress</code>).

```csharp
public PackageInstallationProgress GetProgress()
```

#### Returns

 [PackageInstallationProgress](GDK.Net.Package.PackageInstallationProgress.md)

### <a id="GDK_Net_Package_PackageInstallationMonitor_Update"></a> Update\(\)

Polls the runtime for updated progress and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the
installation is complete (<code>XPackageUpdateInstallationMonitor</code>).

```csharp
public bool Update()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Package_PackageInstallationMonitor_ProgressChanged"></a> ProgressChanged

Raised when installation progress changes
(<code>XPackageRegisterInstallationProgressChanged</code>).

```csharp
public event EventHandler<PackageProgressChangedEventArgs>? ProgressChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[PackageProgressChangedEventArgs](GDK.Net.Package.PackageProgressChangedEventArgs.md)\>?

#### Remarks

Registration is deferred until the first subscriber. Unregistration (with wait) happens on
<xref href="GDK.Net.Package.PackageInstallationMonitor.Dispose" data-throw-if-not-resolved="false"></xref>.

