# <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest"></a> Class CloudScriptExecuteCloudScriptRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFCloudScriptExecuteCloudScriptRequest</code>.

```csharp
public sealed class CloudScriptExecuteCloudScriptRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CloudScriptExecuteCloudScriptRequest](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_FunctionName"></a> FunctionName

<code>FunctionName</code>.

```csharp
public string? FunctionName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_FunctionParameter"></a> FunctionParameter

<code>FunctionParameter</code>.

```csharp
public string? FunctionParameter { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_GeneratePlayStreamEvent"></a> GeneratePlayStreamEvent

<code>GeneratePlayStreamEvent</code>.

```csharp
public bool? GeneratePlayStreamEvent { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_RevisionSelection"></a> RevisionSelection

<code>RevisionSelection</code>.

```csharp
public CloudScriptCloudScriptRevisionOption? RevisionSelection { get; set; }
```

#### Property Value

 [CloudScriptCloudScriptRevisionOption](GDK.Net.PlayFab.CloudScriptCloudScriptRevisionOption.md)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_SpecificRevision"></a> SpecificRevision

<code>SpecificRevision</code>.

```csharp
public int? SpecificRevision { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

