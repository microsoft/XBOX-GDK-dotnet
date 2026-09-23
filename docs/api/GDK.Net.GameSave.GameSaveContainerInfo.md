# <a id="GDK_Net_GameSave_GameSaveContainerInfo"></a> Class GameSaveContainerInfo

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

Metadata for a game-save container returned by enumeration or a targeted query.

```csharp
public sealed class GameSaveContainerInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveContainerInfo](GDK.Net.GameSave.GameSaveContainerInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Containers are logical groups of blobs that are written and read atomically.
A provider has a default quota of 256 MB shared across all its containers.

## Properties

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_BlobCount"></a> BlobCount

Number of blobs currently stored in the container.

```csharp
public uint BlobCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_DisplayName"></a> DisplayName

Human-readable display name for the container.

```csharp
public string DisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_LastModified"></a> LastModified

When the container was last modified (UTC).

```csharp
public DateTimeOffset LastModified { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_Name"></a> Name

Unique container name within the provider.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_NeedsSync"></a> NeedsSync

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the container has not yet synced with the cloud.
Any operation on an unsynced container may trigger a network call when using SyncOnDemand.

```csharp
public bool NeedsSync { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_TotalSize"></a> TotalSize

Total size of all blobs in the container, in bytes.

```csharp
public long TotalSize { get; }
```

#### Property Value

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

## Methods

### <a id="GDK_Net_GameSave_GameSaveContainerInfo_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

