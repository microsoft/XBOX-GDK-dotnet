# <a id="GDK_Net_PlayFab_CloudScript"></a> Class CloudScript

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab CloudScript service (<code>PFCloudScript.h</code>).

```csharp
public static class CloudScript
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CloudScript](GDK.Net.PlayFab.CloudScript.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_CloudScript_ClientExecuteCloudScriptAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CloudScriptExecuteCloudScriptRequest_System_Threading_CancellationToken_"></a> ClientExecuteCloudScriptAsync\(PlayFabEntity, CloudScriptExecuteCloudScriptRequest, CancellationToken\)

Calls <code>PFCloudScriptClientExecuteCloudScriptAsync</code>.

```csharp
public static Task<CloudScriptExecuteCloudScriptResult> ClientExecuteCloudScriptAsync(PlayFabEntity entity, CloudScriptExecuteCloudScriptRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CloudScriptExecuteCloudScriptRequest](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CloudScriptExecuteCloudScriptResult](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptResult.md)\>

### <a id="GDK_Net_PlayFab_CloudScript_ExecuteEntityCloudScriptAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CloudScriptExecuteEntityCloudScriptRequest_System_Threading_CancellationToken_"></a> ExecuteEntityCloudScriptAsync\(PlayFabEntity, CloudScriptExecuteEntityCloudScriptRequest, CancellationToken\)

Calls <code>PFCloudScriptExecuteEntityCloudScriptAsync</code>.

```csharp
public static Task<CloudScriptExecuteCloudScriptResult> ExecuteEntityCloudScriptAsync(PlayFabEntity entity, CloudScriptExecuteEntityCloudScriptRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CloudScriptExecuteEntityCloudScriptRequest](GDK.Net.PlayFab.CloudScriptExecuteEntityCloudScriptRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CloudScriptExecuteCloudScriptResult](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptResult.md)\>

### <a id="GDK_Net_PlayFab_CloudScript_ExecuteFunctionAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CloudScriptExecuteFunctionRequest_System_Threading_CancellationToken_"></a> ExecuteFunctionAsync\(PlayFabEntity, CloudScriptExecuteFunctionRequest, CancellationToken\)

Calls <code>PFCloudScriptExecuteFunctionAsync</code>.

```csharp
public static Task<CloudScriptExecuteFunctionResult> ExecuteFunctionAsync(PlayFabEntity entity, CloudScriptExecuteFunctionRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CloudScriptExecuteFunctionRequest](GDK.Net.PlayFab.CloudScriptExecuteFunctionRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CloudScriptExecuteFunctionResult](GDK.Net.PlayFab.CloudScriptExecuteFunctionResult.md)\>

### <a id="GDK_Net_PlayFab_CloudScript_ServerExecuteCloudScriptAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_CloudScriptExecuteCloudScriptServerRequest_System_Threading_CancellationToken_"></a> ServerExecuteCloudScriptAsync\(PlayFabEntity, CloudScriptExecuteCloudScriptServerRequest, CancellationToken\)

Calls <code>PFCloudScriptServerExecuteCloudScriptAsync</code>.

```csharp
public static Task<CloudScriptExecuteCloudScriptResult> ServerExecuteCloudScriptAsync(PlayFabEntity entity, CloudScriptExecuteCloudScriptServerRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [CloudScriptExecuteCloudScriptServerRequest](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptServerRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[CloudScriptExecuteCloudScriptResult](GDK.Net.PlayFab.CloudScriptExecuteCloudScriptResult.md)\>

