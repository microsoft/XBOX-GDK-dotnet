# <a id="GDK_Net_XboxLive_TitleStorageBlobDownloadResult"></a> Class TitleStorageBlobDownloadResult

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Blob bytes returned by a title storage download, plus the service metadata.

```csharp
public sealed class TitleStorageBlobDownloadResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleStorageBlobDownloadResult](GDK.Net.XboxLive.TitleStorageBlobDownloadResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_TitleStorageBlobDownloadResult_Data"></a> Data

Downloaded blob bytes. The array is owned by the caller.

```csharp
public byte[] Data { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="GDK_Net_XboxLive_TitleStorageBlobDownloadResult_Metadata"></a> Metadata

Metadata returned by <code>XblTitleStorageDownloadBlobResult</code>.

```csharp
public TitleStorageBlobMetadata Metadata { get; }
```

#### Property Value

 [TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)

