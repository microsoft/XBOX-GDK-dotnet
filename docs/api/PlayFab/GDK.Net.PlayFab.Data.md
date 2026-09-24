# <a id="GDK_Net_PlayFab_Data"></a> Class Data

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Data service (<code>PFData.h</code>).

```csharp
public static class Data
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Data](GDK.Net.PlayFab.Data.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Data_AbortFileUploadsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataAbortFileUploadsRequest_System_Threading_CancellationToken_"></a> AbortFileUploadsAsync\(PlayFabEntity, DataAbortFileUploadsRequest, CancellationToken\)

Calls <code>PFDataAbortFileUploadsAsync</code>.

```csharp
public static Task<DataAbortFileUploadsResponse> AbortFileUploadsAsync(PlayFabEntity entity, DataAbortFileUploadsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataAbortFileUploadsRequest](GDK.Net.PlayFab.DataAbortFileUploadsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataAbortFileUploadsResponse](GDK.Net.PlayFab.DataAbortFileUploadsResponse.md)\>

### <a id="GDK_Net_PlayFab_Data_DeleteFilesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataDeleteFilesRequest_System_Threading_CancellationToken_"></a> DeleteFilesAsync\(PlayFabEntity, DataDeleteFilesRequest, CancellationToken\)

Calls <code>PFDataDeleteFilesAsync</code>.

```csharp
public static Task<DataDeleteFilesResponse> DeleteFilesAsync(PlayFabEntity entity, DataDeleteFilesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataDeleteFilesRequest](GDK.Net.PlayFab.DataDeleteFilesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataDeleteFilesResponse](GDK.Net.PlayFab.DataDeleteFilesResponse.md)\>

### <a id="GDK_Net_PlayFab_Data_FinalizeFileUploadsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataFinalizeFileUploadsRequest_System_Threading_CancellationToken_"></a> FinalizeFileUploadsAsync\(PlayFabEntity, DataFinalizeFileUploadsRequest, CancellationToken\)

Calls <code>PFDataFinalizeFileUploadsAsync</code>.

```csharp
public static Task<DataFinalizeFileUploadsResponse> FinalizeFileUploadsAsync(PlayFabEntity entity, DataFinalizeFileUploadsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataFinalizeFileUploadsRequest](GDK.Net.PlayFab.DataFinalizeFileUploadsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataFinalizeFileUploadsResponse](GDK.Net.PlayFab.DataFinalizeFileUploadsResponse.md)\>

### <a id="GDK_Net_PlayFab_Data_GetFilesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataGetFilesRequest_System_Threading_CancellationToken_"></a> GetFilesAsync\(PlayFabEntity, DataGetFilesRequest, CancellationToken\)

Calls <code>PFDataGetFilesAsync</code>.

```csharp
public static Task<DataGetFilesResponse> GetFilesAsync(PlayFabEntity entity, DataGetFilesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataGetFilesRequest](GDK.Net.PlayFab.DataGetFilesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataGetFilesResponse](GDK.Net.PlayFab.DataGetFilesResponse.md)\>

### <a id="GDK_Net_PlayFab_Data_GetObjectsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataGetObjectsRequest_System_Threading_CancellationToken_"></a> GetObjectsAsync\(PlayFabEntity, DataGetObjectsRequest, CancellationToken\)

Calls <code>PFDataGetObjectsAsync</code>.

```csharp
public static Task<DataGetObjectsResponse> GetObjectsAsync(PlayFabEntity entity, DataGetObjectsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataGetObjectsRequest](GDK.Net.PlayFab.DataGetObjectsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataGetObjectsResponse](GDK.Net.PlayFab.DataGetObjectsResponse.md)\>

### <a id="GDK_Net_PlayFab_Data_InitiateFileUploadsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataInitiateFileUploadsRequest_System_Threading_CancellationToken_"></a> InitiateFileUploadsAsync\(PlayFabEntity, DataInitiateFileUploadsRequest, CancellationToken\)

Calls <code>PFDataInitiateFileUploadsAsync</code>.

```csharp
public static Task<DataInitiateFileUploadsResponse> InitiateFileUploadsAsync(PlayFabEntity entity, DataInitiateFileUploadsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataInitiateFileUploadsRequest](GDK.Net.PlayFab.DataInitiateFileUploadsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataInitiateFileUploadsResponse](GDK.Net.PlayFab.DataInitiateFileUploadsResponse.md)\>

### <a id="GDK_Net_PlayFab_Data_SetObjectsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_DataSetObjectsRequest_System_Threading_CancellationToken_"></a> SetObjectsAsync\(PlayFabEntity, DataSetObjectsRequest, CancellationToken\)

Calls <code>PFDataSetObjectsAsync</code>.

```csharp
public static Task<DataSetObjectsResponse> SetObjectsAsync(PlayFabEntity entity, DataSetObjectsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [DataSetObjectsRequest](GDK.Net.PlayFab.DataSetObjectsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[DataSetObjectsResponse](GDK.Net.PlayFab.DataSetObjectsResponse.md)\>

