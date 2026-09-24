# <a id="GDK_Net_XboxLive_TitleStorageService"></a> Class TitleStorageService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live title storage quota, metadata and blob transfer operations.

```csharp
public sealed class TitleStorageService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleStorageService](GDK.Net.XboxLive.TitleStorageService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Binary uploads and downloads may be chunked by XSAPI. Pass a preferred chunk size to influence
transfer granularity, or 0 to let the runtime choose. Upload buffers are pinned for the full
asynchronous operation; callers must still avoid mutating the array until the returned task has
completed.

## Methods

### <a id="GDK_Net_XboxLive_TitleStorageService_DeleteBlobAsync_GDK_Net_XboxLive_TitleStorageBlobMetadata_System_Boolean_System_Threading_CancellationToken_"></a> DeleteBlobAsync\(TitleStorageBlobMetadata, bool, CancellationToken\)

Deletes a blob from title storage.

```csharp
public Task DeleteBlobAsync(TitleStorageBlobMetadata blobMetadata, bool deleteOnlyIfETagMatches = false, CancellationToken cancellationToken = default)
```

#### Parameters

`blobMetadata` [TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)

Metadata identifying the blob to delete.

`deleteOnlyIfETagMatches` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, delete only if <code class="paramref">blobMetadata</code>'s ETag matches
the service value.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_XboxLive_TitleStorageService_DownloadBlobAsync_GDK_Net_XboxLive_TitleStorageBlobMetadata_GDK_Net_XboxLive_TitleStorageETagMatchCondition_System_String_System_UInt64_System_Threading_CancellationToken_"></a> DownloadBlobAsync\(TitleStorageBlobMetadata, TitleStorageETagMatchCondition, string?, ulong, CancellationToken\)

Downloads a blob into a managed byte array.

```csharp
public Task<TitleStorageBlobDownloadResult> DownloadBlobAsync(TitleStorageBlobMetadata blobMetadata, TitleStorageETagMatchCondition eTagMatchCondition = TitleStorageETagMatchCondition.NotUsed, string? selectQuery = null, ulong preferredDownloadBlockSize = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`blobMetadata` [TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)

Metadata identifying the blob to download.

`eTagMatchCondition` [TitleStorageETagMatchCondition](GDK.Net.XboxLive.TitleStorageETagMatchCondition.md)

Optional ETag condition for optimistic concurrency.

`selectQuery` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional config filter or JSON property selector.

`preferredDownloadBlockSize` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Preferred binary download chunk size in bytes; 0 uses the default.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleStorageBlobDownloadResult](GDK.Net.XboxLive.TitleStorageBlobDownloadResult.md)\>

#### Remarks

The array is sized from <xref href="GDK.Net.XboxLive.TitleStorageBlobMetadata.Length" data-throw-if-not-resolved="false"></xref> and pinned until the
async operation completes. For binary blobs, <code class="paramref">preferredDownloadBlockSize</code>
controls the chunk size XSAPI asks the service to use; pass 0 for the default. The ETag
condition can skip the transfer when the supplied metadata is stale or already current.

### <a id="GDK_Net_XboxLive_TitleStorageService_GetBlobMetadataAsync_System_String_GDK_Net_XboxLive_TitleStorageType_System_String_System_UInt64_System_UInt32_System_UInt32_System_Threading_CancellationToken_"></a> GetBlobMetadataAsync\(string, TitleStorageType, string, ulong, uint, uint, CancellationToken\)

Gets a page of blob metadata under a path.

```csharp
public Task<TitleStorageBlobMetadataPage> GetBlobMetadataAsync(string serviceConfigurationId, TitleStorageType storageType, string blobPath = "", ulong xboxUserId = 0, uint skipItems = 0, uint maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's service configuration id.

`storageType` [TitleStorageType](GDK.Net.XboxLive.TitleStorageType.md)

The title storage area to enumerate.

`blobPath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Root path to enumerate. Empty enumerates from the storage root.

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Owner Xbox user id; ignored for global storage.

`skipItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Number of items to skip before returning results.

`maxItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum items to return. 0 attempts to retrieve all items.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleStorageBlobMetadataPage](GDK.Net.XboxLive.TitleStorageBlobMetadataPage.md)\>

### <a id="GDK_Net_XboxLive_TitleStorageService_GetQuotaAsync_System_String_GDK_Net_XboxLive_TitleStorageType_System_Threading_CancellationToken_"></a> GetQuotaAsync\(string, TitleStorageType, CancellationToken\)

Gets title storage usage and quota for a service configuration.

```csharp
public Task<TitleStorageQuota> GetQuotaAsync(string serviceConfigurationId, TitleStorageType storageType, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's case-sensitive service configuration id.

`storageType` [TitleStorageType](GDK.Net.XboxLive.TitleStorageType.md)

The title storage area to query.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)\>

### <a id="GDK_Net_XboxLive_TitleStorageService_UploadBlobAsync_GDK_Net_XboxLive_TitleStorageBlobMetadata_System_Byte___GDK_Net_XboxLive_TitleStorageETagMatchCondition_System_UInt64_System_Threading_CancellationToken_"></a> UploadBlobAsync\(TitleStorageBlobMetadata, byte\[\], TitleStorageETagMatchCondition, ulong, CancellationToken\)

Uploads a managed byte array to title storage.

```csharp
public Task<TitleStorageBlobMetadata> UploadBlobAsync(TitleStorageBlobMetadata blobMetadata, byte[] data, TitleStorageETagMatchCondition eTagMatchCondition = TitleStorageETagMatchCondition.NotUsed, ulong preferredUploadBlockSize = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`blobMetadata` [TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)

Metadata identifying the blob to upload.

`data` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Blob bytes to upload. The array is pinned until completion.

`eTagMatchCondition` [TitleStorageETagMatchCondition](GDK.Net.XboxLive.TitleStorageETagMatchCondition.md)

Optional ETag condition for optimistic concurrency.

`preferredUploadBlockSize` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Preferred binary upload chunk size in bytes; 0 uses the default.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)\>

#### Remarks

The buffer is pinned for the whole async operation because XSAPI reads it after the native
start call returns. Do not mutate <code class="paramref">data</code> until the task completes. For
binary blobs, <code class="paramref">preferredUploadBlockSize</code> controls chunking. XSAPI defaults
out-of-range values; the GDK 260404 range is 1 KiB to 4 MiB, with a 256 KiB default. Use an
ETag condition to avoid overwriting a newer blob version.

