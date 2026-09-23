# <a id="GDK_Net_PlayFab_DataFinalizeFileUploadsResponse"></a> Class DataFinalizeFileUploadsResponse

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFDataFinalizeFileUploadsResponse</code>.

```csharp
public sealed class DataFinalizeFileUploadsResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DataFinalizeFileUploadsResponse](GDK.Net.PlayFab.DataFinalizeFileUploadsResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_DataFinalizeFileUploadsResponse_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_DataFinalizeFileUploadsResponse_Metadata"></a> Metadata

<code>Metadata</code>.

```csharp
public IReadOnlyDictionary<string, DataGetFileMetadata>? Metadata { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [DataGetFileMetadata](GDK.Net.PlayFab.DataGetFileMetadata.md)\>?

### <a id="GDK_Net_PlayFab_DataFinalizeFileUploadsResponse_ProfileVersion"></a> ProfileVersion

<code>ProfileVersion</code>.

```csharp
public int ProfileVersion { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

