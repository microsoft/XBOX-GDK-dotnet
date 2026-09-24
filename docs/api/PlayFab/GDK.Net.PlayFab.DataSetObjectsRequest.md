# <a id="GDK_Net_PlayFab_DataSetObjectsRequest"></a> Class DataSetObjectsRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFDataSetObjectsRequest</code>.

```csharp
public sealed class DataSetObjectsRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DataSetObjectsRequest](GDK.Net.PlayFab.DataSetObjectsRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_DataSetObjectsRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_DataSetObjectsRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_DataSetObjectsRequest_ExpectedProfileVersion"></a> ExpectedProfileVersion

<code>ExpectedProfileVersion</code>.

```csharp
public int? ExpectedProfileVersion { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="GDK_Net_PlayFab_DataSetObjectsRequest_Objects"></a> Objects

<code>Objects</code>.

```csharp
public IReadOnlyList<DataSetObject>? Objects { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[DataSetObject](GDK.Net.PlayFab.DataSetObject.md)\>?

