# <a id="GDK_Net_PlayFab_Authentication"></a> Class Authentication

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Authentication service (<code>PFAuthentication.h</code>).

```csharp
public static class Authentication
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Authentication](GDK.Net.PlayFab.Authentication.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Authentication_AuthenticateGameServerWithCustomIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationAuthenticateCustomIdRequest_System_Threading_CancellationToken_"></a> AuthenticateGameServerWithCustomIdAsync\(PlayFabEntity, AuthenticationAuthenticateCustomIdRequest, CancellationToken\)

Calls <code>PFAuthenticationAuthenticateGameServerWithCustomIdAsync</code>.

```csharp
public static Task<PlayFabGameServerLoginResult> AuthenticateGameServerWithCustomIdAsync(PlayFabEntity entity, AuthenticationAuthenticateCustomIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationAuthenticateCustomIdRequest](GDK.Net.PlayFab.AuthenticationAuthenticateCustomIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabGameServerLoginResult](GDK.Net.PlayFab.PlayFabGameServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_DeleteAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationDeleteRequest_System_Threading_CancellationToken_"></a> DeleteAsync\(PlayFabEntity, AuthenticationDeleteRequest, CancellationToken\)

Calls <code>PFAuthenticationDeleteAsync</code>.

```csharp
public static Task DeleteAsync(PlayFabEntity entity, AuthenticationDeleteRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationDeleteRequest](GDK.Net.PlayFab.AuthenticationDeleteRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_GetEntityAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationGetEntityRequest_System_Threading_CancellationToken_"></a> GetEntityAsync\(PlayFabEntity, AuthenticationGetEntityRequest, CancellationToken\)

Calls <code>PFAuthenticationGetEntityAsync</code>.

```csharp
public static Task<PlayFabEntity> GetEntityAsync(PlayFabEntity entity, AuthenticationGetEntityRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationGetEntityRequest](GDK.Net.PlayFab.AuthenticationGetEntityRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)\>

### <a id="GDK_Net_PlayFab_Authentication_GetEntityWithSecretKeyAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationGetEntityRequest_System_Threading_CancellationToken_"></a> GetEntityWithSecretKeyAsync\(PlayFabServiceConfig, string, AuthenticationGetEntityRequest, CancellationToken\)

Calls <code>PFAuthenticationGetEntityWithSecretKeyAsync</code>.

```csharp
public static Task<PlayFabEntity> GetEntityWithSecretKeyAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationGetEntityRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationGetEntityRequest](GDK.Net.PlayFab.AuthenticationGetEntityRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)\>

### <a id="GDK_Net_PlayFab_Authentication_LoginWithBattleNetAsync_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_PlayFab_AuthenticationLoginWithBattleNetRequest_System_Threading_CancellationToken_"></a> LoginWithBattleNetAsync\(PlayFabServiceConfig, AuthenticationLoginWithBattleNetRequest, CancellationToken\)

Calls <code>PFAuthenticationLoginWithBattleNetAsync</code>.

```csharp
public static Task<PlayFabLoginResult> LoginWithBattleNetAsync(PlayFabServiceConfig serviceConfig, AuthenticationLoginWithBattleNetRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`request` [AuthenticationLoginWithBattleNetRequest](GDK.Net.PlayFab.AuthenticationLoginWithBattleNetRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_LoginWithCustomIDAsync_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_PlayFab_AuthenticationLoginWithCustomIDRequest_System_Threading_CancellationToken_"></a> LoginWithCustomIDAsync\(PlayFabServiceConfig, AuthenticationLoginWithCustomIDRequest, CancellationToken\)

Calls <code>PFAuthenticationLoginWithCustomIDAsync</code>.

```csharp
public static Task<PlayFabLoginResult> LoginWithCustomIDAsync(PlayFabServiceConfig serviceConfig, AuthenticationLoginWithCustomIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`request` [AuthenticationLoginWithCustomIDRequest](GDK.Net.PlayFab.AuthenticationLoginWithCustomIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_LoginWithOpenIdConnectAsync_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_PlayFab_AuthenticationLoginWithOpenIdConnectRequest_System_Threading_CancellationToken_"></a> LoginWithOpenIdConnectAsync\(PlayFabServiceConfig, AuthenticationLoginWithOpenIdConnectRequest, CancellationToken\)

Calls <code>PFAuthenticationLoginWithOpenIdConnectAsync</code>.

```csharp
public static Task<PlayFabLoginResult> LoginWithOpenIdConnectAsync(PlayFabServiceConfig serviceConfig, AuthenticationLoginWithOpenIdConnectRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`request` [AuthenticationLoginWithOpenIdConnectRequest](GDK.Net.PlayFab.AuthenticationLoginWithOpenIdConnectRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_LoginWithSteamAsync_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_PlayFab_AuthenticationLoginWithSteamRequest_System_Threading_CancellationToken_"></a> LoginWithSteamAsync\(PlayFabServiceConfig, AuthenticationLoginWithSteamRequest, CancellationToken\)

Calls <code>PFAuthenticationLoginWithSteamAsync</code>.

```csharp
public static Task<PlayFabLoginResult> LoginWithSteamAsync(PlayFabServiceConfig serviceConfig, AuthenticationLoginWithSteamRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`request` [AuthenticationLoginWithSteamRequest](GDK.Net.PlayFab.AuthenticationLoginWithSteamRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_LoginWithXUserAsync_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_System_Threading_CancellationToken_"></a> LoginWithXUserAsync\(PlayFabServiceConfig, AuthenticationLoginWithXUserRequest, CancellationToken\)

Calls <code>PFAuthenticationLoginWithXUserAsync</code>.

```csharp
public static Task<PlayFabLoginResult> LoginWithXUserAsync(PlayFabServiceConfig serviceConfig, AuthenticationLoginWithXUserRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`request` [AuthenticationLoginWithXUserRequest](GDK.Net.PlayFab.AuthenticationLoginWithXUserRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_LoginWithXboxAsync_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_PlayFab_AuthenticationLoginWithXboxRequest_System_Threading_CancellationToken_"></a> LoginWithXboxAsync\(PlayFabServiceConfig, AuthenticationLoginWithXboxRequest, CancellationToken\)

Calls <code>PFAuthenticationLoginWithXboxAsync</code>.

```csharp
public static Task<PlayFabLoginResult> LoginWithXboxAsync(PlayFabServiceConfig serviceConfig, AuthenticationLoginWithXboxRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`request` [AuthenticationLoginWithXboxRequest](GDK.Net.PlayFab.AuthenticationLoginWithXboxRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ReLoginWithBattleNetAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationLoginWithBattleNetRequest_System_Threading_CancellationToken_"></a> ReLoginWithBattleNetAsync\(PlayFabEntity, AuthenticationLoginWithBattleNetRequest, CancellationToken\)

Calls <code>PFAuthenticationReLoginWithBattleNetAsync</code>.

```csharp
public static Task ReLoginWithBattleNetAsync(PlayFabEntity entity, AuthenticationLoginWithBattleNetRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationLoginWithBattleNetRequest](GDK.Net.PlayFab.AuthenticationLoginWithBattleNetRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_ReLoginWithCustomIDAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationLoginWithCustomIDRequest_System_Threading_CancellationToken_"></a> ReLoginWithCustomIDAsync\(PlayFabEntity, AuthenticationLoginWithCustomIDRequest, CancellationToken\)

Calls <code>PFAuthenticationReLoginWithCustomIDAsync</code>.

```csharp
public static Task ReLoginWithCustomIDAsync(PlayFabEntity entity, AuthenticationLoginWithCustomIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationLoginWithCustomIDRequest](GDK.Net.PlayFab.AuthenticationLoginWithCustomIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_ReLoginWithOpenIdConnectAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationLoginWithOpenIdConnectRequest_System_Threading_CancellationToken_"></a> ReLoginWithOpenIdConnectAsync\(PlayFabEntity, AuthenticationLoginWithOpenIdConnectRequest, CancellationToken\)

Calls <code>PFAuthenticationReLoginWithOpenIdConnectAsync</code>.

```csharp
public static Task ReLoginWithOpenIdConnectAsync(PlayFabEntity entity, AuthenticationLoginWithOpenIdConnectRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationLoginWithOpenIdConnectRequest](GDK.Net.PlayFab.AuthenticationLoginWithOpenIdConnectRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_ReLoginWithSteamAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationLoginWithSteamRequest_System_Threading_CancellationToken_"></a> ReLoginWithSteamAsync\(PlayFabEntity, AuthenticationLoginWithSteamRequest, CancellationToken\)

Calls <code>PFAuthenticationReLoginWithSteamAsync</code>.

```csharp
public static Task ReLoginWithSteamAsync(PlayFabEntity entity, AuthenticationLoginWithSteamRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationLoginWithSteamRequest](GDK.Net.PlayFab.AuthenticationLoginWithSteamRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_ReLoginWithXUserAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_System_Threading_CancellationToken_"></a> ReLoginWithXUserAsync\(PlayFabEntity, AuthenticationLoginWithXUserRequest, CancellationToken\)

Calls <code>PFAuthenticationReLoginWithXUserAsync</code>.

```csharp
public static Task ReLoginWithXUserAsync(PlayFabEntity entity, AuthenticationLoginWithXUserRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationLoginWithXUserRequest](GDK.Net.PlayFab.AuthenticationLoginWithXUserRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_ReLoginWithXboxAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationLoginWithXboxRequest_System_Threading_CancellationToken_"></a> ReLoginWithXboxAsync\(PlayFabEntity, AuthenticationLoginWithXboxRequest, CancellationToken\)

Calls <code>PFAuthenticationReLoginWithXboxAsync</code>.

```csharp
public static Task ReLoginWithXboxAsync(PlayFabEntity entity, AuthenticationLoginWithXboxRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationLoginWithXboxRequest](GDK.Net.PlayFab.AuthenticationLoginWithXboxRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithAndroidDeviceIDAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationServerLoginWithAndroidDeviceIDRequest_System_Threading_CancellationToken_"></a> ServerLoginWithAndroidDeviceIDAsync\(PlayFabServiceConfig, string, AuthenticationServerLoginWithAndroidDeviceIDRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithAndroidDeviceIDAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithAndroidDeviceIDAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationServerLoginWithAndroidDeviceIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationServerLoginWithAndroidDeviceIDRequest](GDK.Net.PlayFab.AuthenticationServerLoginWithAndroidDeviceIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithBattleNetAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationServerLoginWithBattleNetRequest_System_Threading_CancellationToken_"></a> ServerLoginWithBattleNetAsync\(PlayFabServiceConfig, string, AuthenticationServerLoginWithBattleNetRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithBattleNetAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithBattleNetAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationServerLoginWithBattleNetRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationServerLoginWithBattleNetRequest](GDK.Net.PlayFab.AuthenticationServerLoginWithBattleNetRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithCustomIDAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationServerLoginWithCustomIDRequest_System_Threading_CancellationToken_"></a> ServerLoginWithCustomIDAsync\(PlayFabServiceConfig, string, AuthenticationServerLoginWithCustomIDRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithCustomIDAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithCustomIDAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationServerLoginWithCustomIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationServerLoginWithCustomIDRequest](GDK.Net.PlayFab.AuthenticationServerLoginWithCustomIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithIOSDeviceIDAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationServerLoginWithIOSDeviceIDRequest_System_Threading_CancellationToken_"></a> ServerLoginWithIOSDeviceIDAsync\(PlayFabServiceConfig, string, AuthenticationServerLoginWithIOSDeviceIDRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithIOSDeviceIDAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithIOSDeviceIDAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationServerLoginWithIOSDeviceIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationServerLoginWithIOSDeviceIDRequest](GDK.Net.PlayFab.AuthenticationServerLoginWithIOSDeviceIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithPSNAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationServerLoginWithPSNRequest_System_Threading_CancellationToken_"></a> ServerLoginWithPSNAsync\(PlayFabServiceConfig, string, AuthenticationServerLoginWithPSNRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithPSNAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithPSNAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationServerLoginWithPSNRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationServerLoginWithPSNRequest](GDK.Net.PlayFab.AuthenticationServerLoginWithPSNRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithServerCustomIdAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationLoginWithServerCustomIdRequest_System_Threading_CancellationToken_"></a> ServerLoginWithServerCustomIdAsync\(PlayFabServiceConfig, string, AuthenticationLoginWithServerCustomIdRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithServerCustomIdAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithServerCustomIdAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationLoginWithServerCustomIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationLoginWithServerCustomIdRequest](GDK.Net.PlayFab.AuthenticationLoginWithServerCustomIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithSteamIdAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationLoginWithSteamIdRequest_System_Threading_CancellationToken_"></a> ServerLoginWithSteamIdAsync\(PlayFabServiceConfig, string, AuthenticationLoginWithSteamIdRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithSteamIdAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithSteamIdAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationLoginWithSteamIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationLoginWithSteamIdRequest](GDK.Net.PlayFab.AuthenticationLoginWithSteamIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithXboxAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationServerLoginWithXboxRequest_System_Threading_CancellationToken_"></a> ServerLoginWithXboxAsync\(PlayFabServiceConfig, string, AuthenticationServerLoginWithXboxRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithXboxAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithXboxAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationServerLoginWithXboxRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationServerLoginWithXboxRequest](GDK.Net.PlayFab.AuthenticationServerLoginWithXboxRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ServerLoginWithXboxIdAsync_GDK_Net_PlayFab_PlayFabServiceConfig_System_String_GDK_Net_PlayFab_AuthenticationLoginWithXboxIdRequest_System_Threading_CancellationToken_"></a> ServerLoginWithXboxIdAsync\(PlayFabServiceConfig, string, AuthenticationLoginWithXboxIdRequest, CancellationToken\)

Calls <code>PFAuthenticationServerLoginWithXboxIdAsync</code>.

```csharp
public static Task<PlayFabServerLoginResult> ServerLoginWithXboxIdAsync(PlayFabServiceConfig serviceConfig, string secretKey, AuthenticationLoginWithXboxIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`secretKey` [string](https://learn.microsoft.com/dotnet/api/system.string)

`request` [AuthenticationLoginWithXboxIdRequest](GDK.Net.PlayFab.AuthenticationLoginWithXboxIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)\>

### <a id="GDK_Net_PlayFab_Authentication_ValidateEntityTokenAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AuthenticationValidateEntityTokenRequest_System_Threading_CancellationToken_"></a> ValidateEntityTokenAsync\(PlayFabEntity, AuthenticationValidateEntityTokenRequest, CancellationToken\)

Calls <code>PFAuthenticationValidateEntityTokenAsync</code>.

```csharp
public static Task<AuthenticationValidateEntityTokenResponse> ValidateEntityTokenAsync(PlayFabEntity entity, AuthenticationValidateEntityTokenRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AuthenticationValidateEntityTokenRequest](GDK.Net.PlayFab.AuthenticationValidateEntityTokenRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticationValidateEntityTokenResponse](GDK.Net.PlayFab.AuthenticationValidateEntityTokenResponse.md)\>

