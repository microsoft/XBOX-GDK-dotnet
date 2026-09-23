# <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata"></a> Class TitleStorageBlobMetadata

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Managed snapshot of <code>XblTitleStorageBlobMetadata</code>.

```csharp
public sealed class TitleStorageBlobMetadata
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Native metadata returned from enumeration, upload and download belongs to the result handle or
async result that produced it. Instances of this type copy all fixed UTF-8 buffers and scalar
fields so they remain valid after the native owner is released.

## Constructors

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata__ctor_System_String_GDK_Net_XboxLive_TitleStorageBlobType_GDK_Net_XboxLive_TitleStorageType_System_String_System_String_System_String_System_Nullable_System_DateTimeOffset__System_UInt64_System_UInt64_"></a> TitleStorageBlobMetadata\(string, TitleStorageBlobType, TitleStorageType, string, string?, string?, DateTimeOffset?, ulong, ulong\)

Creates metadata for a title storage blob.

```csharp
public TitleStorageBlobMetadata(string blobPath, TitleStorageBlobType blobType, TitleStorageType storageType, string serviceConfigurationId, string? displayName = null, string? eTag = null, DateTimeOffset? clientTimestamp = null, ulong length = 0, ulong xboxUserId = 0)
```

#### Parameters

`blobPath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Unique blob path, for example <code>foo\bar\blob.json</code>.

`blobType` [TitleStorageBlobType](GDK.Net.XboxLive.TitleStorageBlobType.md)

The blob's payload format.

`storageType` [TitleStorageType](GDK.Net.XboxLive.TitleStorageType.md)

The title storage area containing the blob.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's service configuration id.

`displayName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional friendly display name.

`eTag` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional ETag used for optimistic concurrency.

`clientTimestamp` [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

Optional client-supplied timestamp.

`length` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Known payload length in bytes.

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Owner Xbox user id; ignored for global storage.

## Properties

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_BlobPath"></a> BlobPath

Unique blob path, for example <code>foo\bar\blob.json</code>.

```csharp
public string BlobPath { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_BlobType"></a> BlobType

The blob's payload format.

```csharp
public TitleStorageBlobType BlobType { get; }
```

#### Property Value

 [TitleStorageBlobType](GDK.Net.XboxLive.TitleStorageBlobType.md)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_ClientTimestamp"></a> ClientTimestamp

Optional timestamp assigned by the title.

```csharp
public DateTimeOffset? ClientTimestamp { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_DisplayName"></a> DisplayName

Friendly display name supplied by the title, or an empty string.

```csharp
public string DisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_ETag"></a> ETag

Service ETag for this blob. Pass it back with
<xref href="GDK.Net.XboxLive.TitleStorageETagMatchCondition.IfMatch" data-throw-if-not-resolved="false"></xref> to avoid overwriting a newer version.

```csharp
public string ETag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_Length"></a> Length

Blob length in bytes.

```csharp
public ulong Length { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_ServiceConfigurationId"></a> ServiceConfigurationId

The title's service configuration id.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_StorageType"></a> StorageType

The title storage area containing the blob.

```csharp
public TitleStorageType StorageType { get; }
```

#### Property Value

 [TitleStorageType](GDK.Net.XboxLive.TitleStorageType.md)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_XboxUserId"></a> XboxUserId

Owner Xbox user id; 0 for global storage.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadata_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

