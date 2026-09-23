# <a id="GDK_Net_Storage_PersistentLocalStorageSpaceInfo"></a> Struct PersistentLocalStorageSpaceInfo

Namespace: [GDK.Net.Storage](GDK.Net.Storage.md)  
Assembly: GDK.Net.dll  

How much of the title's persistent local storage allocation is used and available, in bytes.

```csharp
public readonly struct PersistentLocalStorageSpaceInfo
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Storage_PersistentLocalStorageSpaceInfo_AvailableFreeBytes"></a> AvailableFreeBytes

Bytes that can be written right now.

```csharp
public ulong AvailableFreeBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Storage_PersistentLocalStorageSpaceInfo_TotalBytes"></a> TotalBytes

Maximum bytes the title may store.

```csharp
public ulong TotalBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Storage_PersistentLocalStorageSpaceInfo_TotalFreeBytes"></a> TotalFreeBytes

Bytes left in the allocation. Reaching these may require prompting the user to free space with
<xref href="GDK.Net.Storage.PersistentLocalStorage.PromptUserForSpaceAsync(System.UInt64%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

```csharp
public ulong TotalFreeBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Storage_PersistentLocalStorageSpaceInfo_UsedBytes"></a> UsedBytes

Bytes already used.

```csharp
public ulong UsedBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

