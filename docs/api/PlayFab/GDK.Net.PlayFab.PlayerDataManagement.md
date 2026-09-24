# <a id="GDK_Net_PlayFab_PlayerDataManagement"></a> Class PlayerDataManagement

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab PlayerDataManagement service (<code>PFPlayerDataManagement.h</code>).

```csharp
public static class PlayerDataManagement
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayerDataManagement](GDK.Net.PlayFab.PlayerDataManagement.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientDeletePlayerCustomPropertiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementClientDeletePlayerCustomPropertiesRequest_System_Threading_CancellationToken_"></a> ClientDeletePlayerCustomPropertiesAsync\(PlayFabEntity, PlayerDataManagementClientDeletePlayerCustomPropertiesRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientDeletePlayerCustomPropertiesAsync</code>.

```csharp
public static Task<PlayerDataManagementClientDeletePlayerCustomPropertiesResult> ClientDeletePlayerCustomPropertiesAsync(PlayFabEntity entity, PlayerDataManagementClientDeletePlayerCustomPropertiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementClientDeletePlayerCustomPropertiesRequest](GDK.Net.PlayFab.PlayerDataManagementClientDeletePlayerCustomPropertiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientDeletePlayerCustomPropertiesResult](GDK.Net.PlayFab.PlayerDataManagementClientDeletePlayerCustomPropertiesResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientGetPlayerCustomPropertyAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementClientGetPlayerCustomPropertyRequest_System_Threading_CancellationToken_"></a> ClientGetPlayerCustomPropertyAsync\(PlayFabEntity, PlayerDataManagementClientGetPlayerCustomPropertyRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientGetPlayerCustomPropertyAsync</code>.

```csharp
public static Task<PlayerDataManagementClientGetPlayerCustomPropertyResult> ClientGetPlayerCustomPropertyAsync(PlayFabEntity entity, PlayerDataManagementClientGetPlayerCustomPropertyRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementClientGetPlayerCustomPropertyRequest](GDK.Net.PlayFab.PlayerDataManagementClientGetPlayerCustomPropertyRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientGetPlayerCustomPropertyResult](GDK.Net.PlayFab.PlayerDataManagementClientGetPlayerCustomPropertyResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientGetUserDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ClientGetUserDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientGetUserDataAsync</code>.

```csharp
public static Task<PlayerDataManagementClientGetUserDataResult> ClientGetUserDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementClientGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientGetUserPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ClientGetUserPublisherDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientGetUserPublisherDataAsync</code>.

```csharp
public static Task<PlayerDataManagementClientGetUserDataResult> ClientGetUserPublisherDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementClientGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientGetUserPublisherReadOnlyDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ClientGetUserPublisherReadOnlyDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientGetUserPublisherReadOnlyDataAsync</code>.

```csharp
public static Task<PlayerDataManagementClientGetUserDataResult> ClientGetUserPublisherReadOnlyDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementClientGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientGetUserReadOnlyDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ClientGetUserReadOnlyDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientGetUserReadOnlyDataAsync</code>.

```csharp
public static Task<PlayerDataManagementClientGetUserDataResult> ClientGetUserReadOnlyDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementClientGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientListPlayerCustomPropertiesAsync_GDK_Net_PlayFab_PlayFabEntity_System_Threading_CancellationToken_"></a> ClientListPlayerCustomPropertiesAsync\(PlayFabEntity, CancellationToken\)

Calls <code>PFPlayerDataManagementClientListPlayerCustomPropertiesAsync</code>.

```csharp
public static Task<PlayerDataManagementClientListPlayerCustomPropertiesResult> ClientListPlayerCustomPropertiesAsync(PlayFabEntity entity, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientListPlayerCustomPropertiesResult](GDK.Net.PlayFab.PlayerDataManagementClientListPlayerCustomPropertiesResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientUpdatePlayerCustomPropertiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest_System_Threading_CancellationToken_"></a> ClientUpdatePlayerCustomPropertiesAsync\(PlayFabEntity, PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientUpdatePlayerCustomPropertiesAsync</code>.

```csharp
public static Task<PlayerDataManagementClientUpdatePlayerCustomPropertiesResult> ClientUpdatePlayerCustomPropertiesAsync(PlayFabEntity entity, PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest](GDK.Net.PlayFab.PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementClientUpdatePlayerCustomPropertiesResult](GDK.Net.PlayFab.PlayerDataManagementClientUpdatePlayerCustomPropertiesResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientUpdateUserDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest_System_Threading_CancellationToken_"></a> ClientUpdateUserDataAsync\(PlayFabEntity, PlayerDataManagementClientUpdateUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientUpdateUserDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ClientUpdateUserDataAsync(PlayFabEntity entity, PlayerDataManagementClientUpdateUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementClientUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementClientUpdateUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ClientUpdateUserPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest_System_Threading_CancellationToken_"></a> ClientUpdateUserPublisherDataAsync\(PlayFabEntity, PlayerDataManagementClientUpdateUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementClientUpdateUserPublisherDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ClientUpdateUserPublisherDataAsync(PlayFabEntity entity, PlayerDataManagementClientUpdateUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementClientUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementClientUpdateUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerDeletePlayerCustomPropertiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerDeletePlayerCustomPropertiesRequest_System_Threading_CancellationToken_"></a> ServerDeletePlayerCustomPropertiesAsync\(PlayFabEntity, PlayerDataManagementServerDeletePlayerCustomPropertiesRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerDeletePlayerCustomPropertiesAsync</code>.

```csharp
public static Task<PlayerDataManagementServerDeletePlayerCustomPropertiesResult> ServerDeletePlayerCustomPropertiesAsync(PlayFabEntity entity, PlayerDataManagementServerDeletePlayerCustomPropertiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerDeletePlayerCustomPropertiesRequest](GDK.Net.PlayFab.PlayerDataManagementServerDeletePlayerCustomPropertiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerDeletePlayerCustomPropertiesResult](GDK.Net.PlayFab.PlayerDataManagementServerDeletePlayerCustomPropertiesResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetPlayerCustomPropertyAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerGetPlayerCustomPropertyRequest_System_Threading_CancellationToken_"></a> ServerGetPlayerCustomPropertyAsync\(PlayFabEntity, PlayerDataManagementServerGetPlayerCustomPropertyRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetPlayerCustomPropertyAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetPlayerCustomPropertyResult> ServerGetPlayerCustomPropertyAsync(PlayFabEntity entity, PlayerDataManagementServerGetPlayerCustomPropertyRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerGetPlayerCustomPropertyRequest](GDK.Net.PlayFab.PlayerDataManagementServerGetPlayerCustomPropertyRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetPlayerCustomPropertyResult](GDK.Net.PlayFab.PlayerDataManagementServerGetPlayerCustomPropertyResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetUserDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ServerGetUserDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetUserDataAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetUserDataResult> ServerGetUserDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetUserInternalDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ServerGetUserInternalDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetUserInternalDataAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetUserDataResult> ServerGetUserInternalDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetUserPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ServerGetUserPublisherDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetUserPublisherDataAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetUserDataResult> ServerGetUserPublisherDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetUserPublisherInternalDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ServerGetUserPublisherInternalDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetUserPublisherInternalDataAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetUserDataResult> ServerGetUserPublisherInternalDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetUserPublisherReadOnlyDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ServerGetUserPublisherReadOnlyDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetUserPublisherReadOnlyDataAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetUserDataResult> ServerGetUserPublisherReadOnlyDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerGetUserReadOnlyDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementGetUserDataRequest_System_Threading_CancellationToken_"></a> ServerGetUserReadOnlyDataAsync\(PlayFabEntity, PlayerDataManagementGetUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerGetUserReadOnlyDataAsync</code>.

```csharp
public static Task<PlayerDataManagementServerGetUserDataResult> ServerGetUserReadOnlyDataAsync(PlayFabEntity entity, PlayerDataManagementGetUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementGetUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementGetUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerListPlayerCustomPropertiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementListPlayerCustomPropertiesRequest_System_Threading_CancellationToken_"></a> ServerListPlayerCustomPropertiesAsync\(PlayFabEntity, PlayerDataManagementListPlayerCustomPropertiesRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerListPlayerCustomPropertiesAsync</code>.

```csharp
public static Task<PlayerDataManagementServerListPlayerCustomPropertiesResult> ServerListPlayerCustomPropertiesAsync(PlayFabEntity entity, PlayerDataManagementListPlayerCustomPropertiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementListPlayerCustomPropertiesRequest](GDK.Net.PlayFab.PlayerDataManagementListPlayerCustomPropertiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerListPlayerCustomPropertiesResult](GDK.Net.PlayFab.PlayerDataManagementServerListPlayerCustomPropertiesResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdatePlayerCustomPropertiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerUpdatePlayerCustomPropertiesRequest_System_Threading_CancellationToken_"></a> ServerUpdatePlayerCustomPropertiesAsync\(PlayFabEntity, PlayerDataManagementServerUpdatePlayerCustomPropertiesRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdatePlayerCustomPropertiesAsync</code>.

```csharp
public static Task<PlayerDataManagementServerUpdatePlayerCustomPropertiesResult> ServerUpdatePlayerCustomPropertiesAsync(PlayFabEntity entity, PlayerDataManagementServerUpdatePlayerCustomPropertiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerUpdatePlayerCustomPropertiesRequest](GDK.Net.PlayFab.PlayerDataManagementServerUpdatePlayerCustomPropertiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementServerUpdatePlayerCustomPropertiesResult](GDK.Net.PlayFab.PlayerDataManagementServerUpdatePlayerCustomPropertiesResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdateUserDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerUpdateUserDataRequest_System_Threading_CancellationToken_"></a> ServerUpdateUserDataAsync\(PlayFabEntity, PlayerDataManagementServerUpdateUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdateUserDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ServerUpdateUserDataAsync(PlayFabEntity entity, PlayerDataManagementServerUpdateUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementServerUpdateUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdateUserInternalDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementUpdateUserInternalDataRequest_System_Threading_CancellationToken_"></a> ServerUpdateUserInternalDataAsync\(PlayFabEntity, PlayerDataManagementUpdateUserInternalDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdateUserInternalDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ServerUpdateUserInternalDataAsync(PlayFabEntity entity, PlayerDataManagementUpdateUserInternalDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementUpdateUserInternalDataRequest](GDK.Net.PlayFab.PlayerDataManagementUpdateUserInternalDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdateUserPublisherDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerUpdateUserDataRequest_System_Threading_CancellationToken_"></a> ServerUpdateUserPublisherDataAsync\(PlayFabEntity, PlayerDataManagementServerUpdateUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdateUserPublisherDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ServerUpdateUserPublisherDataAsync(PlayFabEntity entity, PlayerDataManagementServerUpdateUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementServerUpdateUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdateUserPublisherInternalDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementUpdateUserInternalDataRequest_System_Threading_CancellationToken_"></a> ServerUpdateUserPublisherInternalDataAsync\(PlayFabEntity, PlayerDataManagementUpdateUserInternalDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdateUserPublisherInternalDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ServerUpdateUserPublisherInternalDataAsync(PlayFabEntity entity, PlayerDataManagementUpdateUserInternalDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementUpdateUserInternalDataRequest](GDK.Net.PlayFab.PlayerDataManagementUpdateUserInternalDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdateUserPublisherReadOnlyDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerUpdateUserDataRequest_System_Threading_CancellationToken_"></a> ServerUpdateUserPublisherReadOnlyDataAsync\(PlayFabEntity, PlayerDataManagementServerUpdateUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdateUserPublisherReadOnlyDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ServerUpdateUserPublisherReadOnlyDataAsync(PlayFabEntity entity, PlayerDataManagementServerUpdateUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementServerUpdateUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

### <a id="GDK_Net_PlayFab_PlayerDataManagement_ServerUpdateUserReadOnlyDataAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlayerDataManagementServerUpdateUserDataRequest_System_Threading_CancellationToken_"></a> ServerUpdateUserReadOnlyDataAsync\(PlayFabEntity, PlayerDataManagementServerUpdateUserDataRequest, CancellationToken\)

Calls <code>PFPlayerDataManagementServerUpdateUserReadOnlyDataAsync</code>.

```csharp
public static Task<PlayerDataManagementUpdateUserDataResult> ServerUpdateUserReadOnlyDataAsync(PlayFabEntity entity, PlayerDataManagementServerUpdateUserDataRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlayerDataManagementServerUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementServerUpdateUserDataRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayerDataManagementUpdateUserDataResult](GDK.Net.PlayFab.PlayerDataManagementUpdateUserDataResult.md)\>

