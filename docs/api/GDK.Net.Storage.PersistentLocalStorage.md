# <a id="GDK_Net_Storage_PersistentLocalStorage"></a> Class PersistentLocalStorage

Namespace: [GDK.Net.Storage](GDK.Net.Storage.md)  
Assembly: GDK.Net.dll  

The title's persistent local storage: a per-title directory that survives updates and is not
synchronised to the cloud (<code>XPersistentLocalStorage.h</code>).

```csharp
public static class PersistentLocalStorage
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PersistentLocalStorage](GDK.Net.Storage.PersistentLocalStorage.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_Storage_PersistentLocalStorage_GetPath"></a> GetPath\(\)

Returns the absolute path of the title's persistent local storage directory
(<code>XPersistentLocalStorageGetPathSize</code> then <code>XPersistentLocalStorageGetPath</code>).

```csharp
public static string GetPath()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Storage_PersistentLocalStorage_GetSpaceInfo"></a> GetSpaceInfo\(\)

Reports the title's storage quota and consumption
(<code>XPersistentLocalStorageGetSpaceInfo</code>).

```csharp
public static PersistentLocalStorageSpaceInfo GetSpaceInfo()
```

#### Returns

 [PersistentLocalStorageSpaceInfo](GDK.Net.Storage.PersistentLocalStorageSpaceInfo.md)

### <a id="GDK_Net_Storage_PersistentLocalStorage_MountForPackage_System_String_"></a> MountForPackage\(string\)

Mounts another package's persistent local storage
(<code>XPersistentLocalStorageMountForPackage</code>).

```csharp
public static PackageMount MountForPackage(string packageIdentifier)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier. Obtain from
<xref href="GDK.Net.Package.GamePackage.GetCurrentPackageIdentifier" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Package.GamePackage.EnumeratePackages(GDK.Net.Package.PackageKind%2cGDK.Net.Package.PackageEnumerationScope)" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [PackageMount](GDK.Net.Package.PackageMount.md)

The mounted package. Dispose when the mount is no longer needed.

#### Remarks

<p>
Unlike the rest of this type, which addresses the calling title's own storage, this reaches
a package identified by <code class="paramref">packageIdentifier</code> — typically a related title in
the same publisher family. The mount is synchronous; no download is triggered.
</p>

### <a id="GDK_Net_Storage_PersistentLocalStorage_PromptUserForSpaceAsync_System_UInt64_System_Threading_CancellationToken_"></a> PromptUserForSpaceAsync\(ulong, CancellationToken\)

Shows the system UI that asks the user to free up storage
(<code>XPersistentLocalStoragePromptUserForSpaceAsync</code>).

```csharp
public static Task PromptUserForSpaceAsync(ulong requestedBytes, CancellationToken cancellationToken = default)
```

#### Parameters

`requestedBytes` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

How many bytes the title needs.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

