# <a id="GDK_Net_PlayFab_TitleDataManagement"></a> Class TitleDataManagement

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab TitleDataManagement service (<code>PFTitleDataManagement.h</code>).

```csharp
public static class TitleDataManagement
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleDataManagement](GDK.Net.PlayFab.TitleDataManagement.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_TitleDataManagement_ClientGetPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetPublisherDataRequest_System_Threading_CancellationToken_"></a> ClientGetPublisherDataAsync\(PlayFabEntity, TitleDataManagementGetPublisherDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementClientGetPublisherDataAsync</code>.

```csharp
public static Task<TitleDataManagementGetPublisherDataResult> ClientGetPublisherDataAsync(PlayFabEntity entity, TitleDataManagementGetPublisherDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetPublisherDataRequest](GDK.Net.PlayFab.TitleDataManagementGetPublisherDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetPublisherDataResult](GDK.Net.PlayFab.TitleDataManagementGetPublisherDataResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ClientGetTimeAsync_GDK_Net_PlayFab_PlayFabEntity_System_Threading_CancellationToken_"></a> ClientGetTimeAsync\(PlayFabEntity, CancellationToken\)

Calls <code>PFTitleDataManagementClientGetTimeAsync</code>.

```csharp
public static Task<TitleDataManagementGetTimeResult> ClientGetTimeAsync(PlayFabEntity entity, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTimeResult](GDK.Net.PlayFab.TitleDataManagementGetTimeResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ClientGetTitleDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetTitleDataRequest_System_Threading_CancellationToken_"></a> ClientGetTitleDataAsync\(PlayFabEntity, TitleDataManagementGetTitleDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementClientGetTitleDataAsync</code>.

```csharp
public static Task<TitleDataManagementGetTitleDataResult> ClientGetTitleDataAsync(PlayFabEntity entity, TitleDataManagementGetTitleDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetTitleDataRequest](GDK.Net.PlayFab.TitleDataManagementGetTitleDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTitleDataResult](GDK.Net.PlayFab.TitleDataManagementGetTitleDataResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ClientGetTitleNewsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetTitleNewsRequest_System_Threading_CancellationToken_"></a> ClientGetTitleNewsAsync\(PlayFabEntity, TitleDataManagementGetTitleNewsRequest, CancellationToken\)

Calls <code>PFTitleDataManagementClientGetTitleNewsAsync</code>.

```csharp
public static Task<TitleDataManagementGetTitleNewsResult> ClientGetTitleNewsAsync(PlayFabEntity entity, TitleDataManagementGetTitleNewsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetTitleNewsRequest](GDK.Net.PlayFab.TitleDataManagementGetTitleNewsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTitleNewsResult](GDK.Net.PlayFab.TitleDataManagementGetTitleNewsResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerGetPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetPublisherDataRequest_System_Threading_CancellationToken_"></a> ServerGetPublisherDataAsync\(PlayFabEntity, TitleDataManagementGetPublisherDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerGetPublisherDataAsync</code>.

```csharp
public static Task<TitleDataManagementGetPublisherDataResult> ServerGetPublisherDataAsync(PlayFabEntity entity, TitleDataManagementGetPublisherDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetPublisherDataRequest](GDK.Net.PlayFab.TitleDataManagementGetPublisherDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetPublisherDataResult](GDK.Net.PlayFab.TitleDataManagementGetPublisherDataResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerGetTimeAsync_GDK_Net_PlayFab_PlayFabEntity_System_Threading_CancellationToken_"></a> ServerGetTimeAsync\(PlayFabEntity, CancellationToken\)

Calls <code>PFTitleDataManagementServerGetTimeAsync</code>.

```csharp
public static Task<TitleDataManagementGetTimeResult> ServerGetTimeAsync(PlayFabEntity entity, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTimeResult](GDK.Net.PlayFab.TitleDataManagementGetTimeResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerGetTitleDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetTitleDataRequest_System_Threading_CancellationToken_"></a> ServerGetTitleDataAsync\(PlayFabEntity, TitleDataManagementGetTitleDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerGetTitleDataAsync</code>.

```csharp
public static Task<TitleDataManagementGetTitleDataResult> ServerGetTitleDataAsync(PlayFabEntity entity, TitleDataManagementGetTitleDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetTitleDataRequest](GDK.Net.PlayFab.TitleDataManagementGetTitleDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTitleDataResult](GDK.Net.PlayFab.TitleDataManagementGetTitleDataResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerGetTitleInternalDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetTitleDataRequest_System_Threading_CancellationToken_"></a> ServerGetTitleInternalDataAsync\(PlayFabEntity, TitleDataManagementGetTitleDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerGetTitleInternalDataAsync</code>.

```csharp
public static Task<TitleDataManagementGetTitleDataResult> ServerGetTitleInternalDataAsync(PlayFabEntity entity, TitleDataManagementGetTitleDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetTitleDataRequest](GDK.Net.PlayFab.TitleDataManagementGetTitleDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTitleDataResult](GDK.Net.PlayFab.TitleDataManagementGetTitleDataResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerGetTitleNewsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementGetTitleNewsRequest_System_Threading_CancellationToken_"></a> ServerGetTitleNewsAsync\(PlayFabEntity, TitleDataManagementGetTitleNewsRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerGetTitleNewsAsync</code>.

```csharp
public static Task<TitleDataManagementGetTitleNewsResult> ServerGetTitleNewsAsync(PlayFabEntity entity, TitleDataManagementGetTitleNewsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementGetTitleNewsRequest](GDK.Net.PlayFab.TitleDataManagementGetTitleNewsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleDataManagementGetTitleNewsResult](GDK.Net.PlayFab.TitleDataManagementGetTitleNewsResult.md)\>

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerSetPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementSetPublisherDataRequest_System_Threading_CancellationToken_"></a> ServerSetPublisherDataAsync\(PlayFabEntity, TitleDataManagementSetPublisherDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerSetPublisherDataAsync</code>.

```csharp
public static Task ServerSetPublisherDataAsync(PlayFabEntity entity, TitleDataManagementSetPublisherDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementSetPublisherDataRequest](GDK.Net.PlayFab.TitleDataManagementSetPublisherDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerSetTitleDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementSetTitleDataRequest_System_Threading_CancellationToken_"></a> ServerSetTitleDataAsync\(PlayFabEntity, TitleDataManagementSetTitleDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerSetTitleDataAsync</code>.

```csharp
public static Task ServerSetTitleDataAsync(PlayFabEntity entity, TitleDataManagementSetTitleDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementSetTitleDataRequest](GDK.Net.PlayFab.TitleDataManagementSetTitleDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_TitleDataManagement_ServerSetTitleInternalDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_TitleDataManagementSetTitleDataRequest_System_Threading_CancellationToken_"></a> ServerSetTitleInternalDataAsync\(PlayFabEntity, TitleDataManagementSetTitleDataRequest, CancellationToken\)

Calls <code>PFTitleDataManagementServerSetTitleInternalDataAsync</code>.

```csharp
public static Task ServerSetTitleInternalDataAsync(PlayFabEntity entity, TitleDataManagementSetTitleDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [TitleDataManagementSetTitleDataRequest](GDK.Net.PlayFab.TitleDataManagementSetTitleDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

