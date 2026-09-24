# <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult"></a> Class CloudScriptExecuteCloudScriptResult

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFCloudScriptExecuteCloudScriptResult</code>.

```csharp
public sealed class CloudScriptExecuteCloudScriptResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CloudScriptExecuteCloudScriptResult](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_APIRequestsIssued"></a> APIRequestsIssued

<code>APIRequestsIssued</code>.

```csharp
public int APIRequestsIssued { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_Error"></a> Error

<code>Error</code>.

```csharp
public CloudScriptScriptExecutionError? Error { get; set; }
```

#### Property Value

 [CloudScriptScriptExecutionError](GDK.Net.PlayFab.CloudScriptScriptExecutionError.md)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_ExecutionTimeSeconds"></a> ExecutionTimeSeconds

<code>ExecutionTimeSeconds</code>.

```csharp
public double ExecutionTimeSeconds { get; set; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_FunctionName"></a> FunctionName

<code>FunctionName</code>.

```csharp
public string? FunctionName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_FunctionResult"></a> FunctionResult

<code>FunctionResult</code>.

```csharp
public string? FunctionResult { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_FunctionResultTooLarge"></a> FunctionResultTooLarge

<code>FunctionResultTooLarge</code>.

```csharp
public bool? FunctionResultTooLarge { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_HttpRequestsIssued"></a> HttpRequestsIssued

<code>HttpRequestsIssued</code>.

```csharp
public int HttpRequestsIssued { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_Logs"></a> Logs

<code>Logs</code>.

```csharp
public IReadOnlyList<CloudScriptLogStatement>? Logs { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[CloudScriptLogStatement](GDK.Net.PlayFab.CloudScriptLogStatement.md)\>?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_LogsTooLarge"></a> LogsTooLarge

<code>LogsTooLarge</code>.

```csharp
public bool? LogsTooLarge { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_MemoryConsumedBytes"></a> MemoryConsumedBytes

<code>MemoryConsumedBytes</code>.

```csharp
public uint MemoryConsumedBytes { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_ProcessorTimeSeconds"></a> ProcessorTimeSeconds

<code>ProcessorTimeSeconds</code>.

```csharp
public double ProcessorTimeSeconds { get; set; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

### <a id="GDK_Net_PlayFab_CloudScriptExecuteCloudScriptResult_Revision"></a> Revision

<code>Revision</code>.

```csharp
public int Revision { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

