# <a id="GDK_Net_PlayFab_AccountManagement"></a> Class AccountManagement

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab AccountManagement service (<code>PFAccountManagement.h</code>).

```csharp
public static class AccountManagement
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AccountManagement](GDK.Net.PlayFab.AccountManagement.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_AccountManagement_ClientAddOrUpdateContactEmailAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementAddOrUpdateContactEmailRequest_System_Threading_CancellationToken_"></a> ClientAddOrUpdateContactEmailAsync\(PlayFabEntity, AccountManagementAddOrUpdateContactEmailRequest, CancellationToken\)

Calls <code>PFAccountManagementClientAddOrUpdateContactEmailAsync</code>.

```csharp
public static Task ClientAddOrUpdateContactEmailAsync(PlayFabEntity entity, AccountManagementAddOrUpdateContactEmailRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementAddOrUpdateContactEmailRequest](GDK.Net.PlayFab.AccountManagementAddOrUpdateContactEmailRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetAccountInfoAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetAccountInfoRequest_System_Threading_CancellationToken_"></a> ClientGetAccountInfoAsync\(PlayFabEntity, AccountManagementGetAccountInfoRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetAccountInfoAsync</code>.

```csharp
public static Task<AccountManagementGetAccountInfoResult> ClientGetAccountInfoAsync(PlayFabEntity entity, AccountManagementGetAccountInfoRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetAccountInfoRequest](GDK.Net.PlayFab.AccountManagementGetAccountInfoRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetAccountInfoResult](GDK.Net.PlayFab.AccountManagementGetAccountInfoResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromBattleNetAccountIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromBattleNetAccountIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromBattleNetAccountIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromBattleNetAccountIdsResult> ClientGetPlayFabIDsFromBattleNetAccountIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromBattleNetAccountIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromBattleNetAccountIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromFacebookIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromFacebookIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromFacebookIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromFacebookIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromFacebookIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromFacebookIDsResult> ClientGetPlayFabIDsFromFacebookIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromFacebookIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromFacebookIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromFacebookIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromFacebookInstantGamesIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromFacebookInstantGamesIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromFacebookInstantGamesIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsResult> ClientGetPlayFabIDsFromFacebookInstantGamesIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromGameCenterIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromGameCenterIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromGameCenterIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromGameCenterIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromGameCenterIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromGameCenterIDsResult> ClientGetPlayFabIDsFromGameCenterIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromGameCenterIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromGameCenterIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromGameCenterIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromGameCenterIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromGameCenterIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromGoogleIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromGoogleIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromGoogleIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromGoogleIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromGoogleIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromGoogleIDsResult> ClientGetPlayFabIDsFromGoogleIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromGoogleIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromGoogleIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromGoogleIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromGoogleIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromGoogleIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromGooglePlayGamesPlayerIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromGooglePlayGamesPlayerIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromGooglePlayGamesPlayerIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsResult> ClientGetPlayFabIDsFromGooglePlayGamesPlayerIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromGooglePlayGamesPlayerIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromKongregateIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromKongregateIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromKongregateIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromKongregateIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromKongregateIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromKongregateIDsResult> ClientGetPlayFabIDsFromKongregateIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromKongregateIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromKongregateIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromKongregateIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromKongregateIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromKongregateIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromNintendoServiceAccountIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromNintendoServiceAccountIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromNintendoServiceAccountIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsResult> ClientGetPlayFabIDsFromNintendoServiceAccountIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsResult> ClientGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromPSNAccountIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromPSNAccountIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromPSNAccountIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromPSNAccountIDsResult> ClientGetPlayFabIDsFromPSNAccountIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromPSNAccountIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNAccountIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromPSNOnlineIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromPSNOnlineIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromPSNOnlineIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromPSNOnlineIDsResult> ClientGetPlayFabIDsFromPSNOnlineIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromPSNOnlineIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNOnlineIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromSteamIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromSteamIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromSteamIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromSteamIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromSteamIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromSteamIDsResult> ClientGetPlayFabIDsFromSteamIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromSteamIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromSteamIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromSteamIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromSteamNamesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromSteamNamesRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromSteamNamesAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromSteamNamesRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromSteamNamesAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromSteamNamesResult> ClientGetPlayFabIDsFromSteamNamesAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromSteamNamesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromSteamNamesRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamNamesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromSteamNamesResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamNamesResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromTwitchIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromTwitchIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromTwitchIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromTwitchIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromTwitchIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromTwitchIDsResult> ClientGetPlayFabIDsFromTwitchIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromTwitchIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromTwitchIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromTwitchIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromTwitchIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromTwitchIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayFabIDsFromXboxLiveIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest_System_Threading_CancellationToken_"></a> ClientGetPlayFabIDsFromXboxLiveIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayFabIDsFromXboxLiveIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromXboxLiveIDsResult> ClientGetPlayFabIDsFromXboxLiveIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromXboxLiveIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromXboxLiveIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayerCombinedInfoAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayerCombinedInfoRequest_System_Threading_CancellationToken_"></a> ClientGetPlayerCombinedInfoAsync\(PlayFabEntity, AccountManagementGetPlayerCombinedInfoRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayerCombinedInfoAsync</code>.

```csharp
public static Task<AccountManagementGetPlayerCombinedInfoResult> ClientGetPlayerCombinedInfoAsync(PlayFabEntity entity, AccountManagementGetPlayerCombinedInfoRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayerCombinedInfoRequest](GDK.Net.PlayFab.AccountManagementGetPlayerCombinedInfoRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayerCombinedInfoResult](GDK.Net.PlayFab.AccountManagementGetPlayerCombinedInfoResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientGetPlayerProfileAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayerProfileRequest_System_Threading_CancellationToken_"></a> ClientGetPlayerProfileAsync\(PlayFabEntity, AccountManagementGetPlayerProfileRequest, CancellationToken\)

Calls <code>PFAccountManagementClientGetPlayerProfileAsync</code>.

```csharp
public static Task<AccountManagementGetPlayerProfileResult> ClientGetPlayerProfileAsync(PlayFabEntity entity, AccountManagementGetPlayerProfileRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayerProfileRequest](GDK.Net.PlayFab.AccountManagementGetPlayerProfileRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayerProfileResult](GDK.Net.PlayFab.AccountManagementGetPlayerProfileResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientLinkBattleNetAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementClientLinkBattleNetAccountRequest_System_Threading_CancellationToken_"></a> ClientLinkBattleNetAccountAsync\(PlayFabEntity, AccountManagementClientLinkBattleNetAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementClientLinkBattleNetAccountAsync</code>.

```csharp
public static Task ClientLinkBattleNetAccountAsync(PlayFabEntity entity, AccountManagementClientLinkBattleNetAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementClientLinkBattleNetAccountRequest](GDK.Net.PlayFab.AccountManagementClientLinkBattleNetAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientLinkCustomIDAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkCustomIDRequest_System_Threading_CancellationToken_"></a> ClientLinkCustomIDAsync\(PlayFabEntity, AccountManagementLinkCustomIDRequest, CancellationToken\)

Calls <code>PFAccountManagementClientLinkCustomIDAsync</code>.

```csharp
public static Task ClientLinkCustomIDAsync(PlayFabEntity entity, AccountManagementLinkCustomIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkCustomIDRequest](GDK.Net.PlayFab.AccountManagementLinkCustomIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientLinkOpenIdConnectAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkOpenIdConnectRequest_System_Threading_CancellationToken_"></a> ClientLinkOpenIdConnectAsync\(PlayFabEntity, AccountManagementLinkOpenIdConnectRequest, CancellationToken\)

Calls <code>PFAccountManagementClientLinkOpenIdConnectAsync</code>.

```csharp
public static Task ClientLinkOpenIdConnectAsync(PlayFabEntity entity, AccountManagementLinkOpenIdConnectRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkOpenIdConnectRequest](GDK.Net.PlayFab.AccountManagementLinkOpenIdConnectRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientLinkSteamAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkSteamAccountRequest_System_Threading_CancellationToken_"></a> ClientLinkSteamAccountAsync\(PlayFabEntity, AccountManagementLinkSteamAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementClientLinkSteamAccountAsync</code>.

```csharp
public static Task ClientLinkSteamAccountAsync(PlayFabEntity entity, AccountManagementLinkSteamAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkSteamAccountRequest](GDK.Net.PlayFab.AccountManagementLinkSteamAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientLinkXboxAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementClientLinkXboxAccountRequest_System_Threading_CancellationToken_"></a> ClientLinkXboxAccountAsync\(PlayFabEntity, AccountManagementClientLinkXboxAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementClientLinkXboxAccountAsync</code>.

```csharp
public static Task ClientLinkXboxAccountAsync(PlayFabEntity entity, AccountManagementClientLinkXboxAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementClientLinkXboxAccountRequest](GDK.Net.PlayFab.AccountManagementClientLinkXboxAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientRemoveContactEmailAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementRemoveContactEmailRequest_System_Threading_CancellationToken_"></a> ClientRemoveContactEmailAsync\(PlayFabEntity, AccountManagementRemoveContactEmailRequest, CancellationToken\)

Calls <code>PFAccountManagementClientRemoveContactEmailAsync</code>.

```csharp
public static Task ClientRemoveContactEmailAsync(PlayFabEntity entity, AccountManagementRemoveContactEmailRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementRemoveContactEmailRequest](GDK.Net.PlayFab.AccountManagementRemoveContactEmailRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientReportPlayerAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementReportPlayerClientRequest_System_Threading_CancellationToken_"></a> ClientReportPlayerAsync\(PlayFabEntity, AccountManagementReportPlayerClientRequest, CancellationToken\)

Calls <code>PFAccountManagementClientReportPlayerAsync</code>.

```csharp
public static Task<AccountManagementReportPlayerClientResult> ClientReportPlayerAsync(PlayFabEntity entity, AccountManagementReportPlayerClientRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementReportPlayerClientRequest](GDK.Net.PlayFab.AccountManagementReportPlayerClientRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementReportPlayerClientResult](GDK.Net.PlayFab.AccountManagementReportPlayerClientResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUnlinkBattleNetAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementClientUnlinkBattleNetAccountRequest_System_Threading_CancellationToken_"></a> ClientUnlinkBattleNetAccountAsync\(PlayFabEntity, AccountManagementClientUnlinkBattleNetAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUnlinkBattleNetAccountAsync</code>.

```csharp
public static Task ClientUnlinkBattleNetAccountAsync(PlayFabEntity entity, AccountManagementClientUnlinkBattleNetAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementClientUnlinkBattleNetAccountRequest](GDK.Net.PlayFab.AccountManagementClientUnlinkBattleNetAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUnlinkCustomIDAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUnlinkCustomIDRequest_System_Threading_CancellationToken_"></a> ClientUnlinkCustomIDAsync\(PlayFabEntity, AccountManagementUnlinkCustomIDRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUnlinkCustomIDAsync</code>.

```csharp
public static Task ClientUnlinkCustomIDAsync(PlayFabEntity entity, AccountManagementUnlinkCustomIDRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUnlinkCustomIDRequest](GDK.Net.PlayFab.AccountManagementUnlinkCustomIDRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUnlinkOpenIdConnectAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUnlinkOpenIdConnectRequest_System_Threading_CancellationToken_"></a> ClientUnlinkOpenIdConnectAsync\(PlayFabEntity, AccountManagementUnlinkOpenIdConnectRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUnlinkOpenIdConnectAsync</code>.

```csharp
public static Task ClientUnlinkOpenIdConnectAsync(PlayFabEntity entity, AccountManagementUnlinkOpenIdConnectRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUnlinkOpenIdConnectRequest](GDK.Net.PlayFab.AccountManagementUnlinkOpenIdConnectRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUnlinkSteamAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUnlinkSteamAccountRequest_System_Threading_CancellationToken_"></a> ClientUnlinkSteamAccountAsync\(PlayFabEntity, AccountManagementUnlinkSteamAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUnlinkSteamAccountAsync</code>.

```csharp
public static Task ClientUnlinkSteamAccountAsync(PlayFabEntity entity, AccountManagementUnlinkSteamAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUnlinkSteamAccountRequest](GDK.Net.PlayFab.AccountManagementUnlinkSteamAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUnlinkXboxAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementClientUnlinkXboxAccountRequest_System_Threading_CancellationToken_"></a> ClientUnlinkXboxAccountAsync\(PlayFabEntity, AccountManagementClientUnlinkXboxAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUnlinkXboxAccountAsync</code>.

```csharp
public static Task ClientUnlinkXboxAccountAsync(PlayFabEntity entity, AccountManagementClientUnlinkXboxAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementClientUnlinkXboxAccountRequest](GDK.Net.PlayFab.AccountManagementClientUnlinkXboxAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUpdateAvatarUrlAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementClientUpdateAvatarUrlRequest_System_Threading_CancellationToken_"></a> ClientUpdateAvatarUrlAsync\(PlayFabEntity, AccountManagementClientUpdateAvatarUrlRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUpdateAvatarUrlAsync</code>.

```csharp
public static Task ClientUpdateAvatarUrlAsync(PlayFabEntity entity, AccountManagementClientUpdateAvatarUrlRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementClientUpdateAvatarUrlRequest](GDK.Net.PlayFab.AccountManagementClientUpdateAvatarUrlRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ClientUpdateUserTitleDisplayNameAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUpdateUserTitleDisplayNameRequest_System_Threading_CancellationToken_"></a> ClientUpdateUserTitleDisplayNameAsync\(PlayFabEntity, AccountManagementUpdateUserTitleDisplayNameRequest, CancellationToken\)

Calls <code>PFAccountManagementClientUpdateUserTitleDisplayNameAsync</code>.

```csharp
public static Task<AccountManagementUpdateUserTitleDisplayNameResult> ClientUpdateUserTitleDisplayNameAsync(PlayFabEntity entity, AccountManagementUpdateUserTitleDisplayNameRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUpdateUserTitleDisplayNameRequest](GDK.Net.PlayFab.AccountManagementUpdateUserTitleDisplayNameRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementUpdateUserTitleDisplayNameResult](GDK.Net.PlayFab.AccountManagementUpdateUserTitleDisplayNameResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_GetTitlePlayersFromXboxLiveIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetTitlePlayersFromXboxLiveIDsRequest_System_Threading_CancellationToken_"></a> GetTitlePlayersFromXboxLiveIDsAsync\(PlayFabEntity, AccountManagementGetTitlePlayersFromXboxLiveIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementGetTitlePlayersFromXboxLiveIDsAsync</code>.

```csharp
public static Task<AccountManagementGetTitlePlayersFromProviderIDsResponse> GetTitlePlayersFromXboxLiveIDsAsync(PlayFabEntity entity, AccountManagementGetTitlePlayersFromXboxLiveIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetTitlePlayersFromXboxLiveIDsRequest](GDK.Net.PlayFab.AccountManagementGetTitlePlayersFromXboxLiveIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetTitlePlayersFromProviderIDsResponse](GDK.Net.PlayFab.AccountManagementGetTitlePlayersFromProviderIDsResponse.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerBanUsersAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementBanUsersRequest_System_Threading_CancellationToken_"></a> ServerBanUsersAsync\(PlayFabEntity, AccountManagementBanUsersRequest, CancellationToken\)

Calls <code>PFAccountManagementServerBanUsersAsync</code>.

```csharp
public static Task<AccountManagementBanUsersResult> ServerBanUsersAsync(PlayFabEntity entity, AccountManagementBanUsersRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementBanUsersRequest](GDK.Net.PlayFab.AccountManagementBanUsersRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementBanUsersResult](GDK.Net.PlayFab.AccountManagementBanUsersResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerDeletePlayerAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementDeletePlayerRequest_System_Threading_CancellationToken_"></a> ServerDeletePlayerAsync\(PlayFabEntity, AccountManagementDeletePlayerRequest, CancellationToken\)

Calls <code>PFAccountManagementServerDeletePlayerAsync</code>.

```csharp
public static Task ServerDeletePlayerAsync(PlayFabEntity entity, AccountManagementDeletePlayerRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementDeletePlayerRequest](GDK.Net.PlayFab.AccountManagementDeletePlayerRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromBattleNetAccountIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromBattleNetAccountIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromBattleNetAccountIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromBattleNetAccountIdsResult> ServerGetPlayFabIDsFromBattleNetAccountIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromBattleNetAccountIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromBattleNetAccountIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromBattleNetAccountIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromFacebookIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromFacebookIDsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromFacebookIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromFacebookIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromFacebookIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromFacebookIDsResult> ServerGetPlayFabIDsFromFacebookIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromFacebookIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromFacebookIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromFacebookIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromFacebookInstantGamesIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromFacebookInstantGamesIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromFacebookInstantGamesIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsResult> ServerGetPlayFabIDsFromFacebookInstantGamesIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromFacebookInstantGamesIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromNintendoServiceAccountIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromNintendoServiceAccountIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromNintendoServiceAccountIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsResult> ServerGetPlayFabIDsFromNintendoServiceAccountIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoServiceAccountIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsResult> ServerGetPlayFabIDsFromNintendoSwitchDeviceIdsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromNintendoSwitchDeviceIdsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromPSNAccountIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromPSNAccountIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromPSNAccountIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromPSNAccountIDsResult> ServerGetPlayFabIDsFromPSNAccountIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNAccountIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromPSNAccountIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNAccountIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromPSNOnlineIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromPSNOnlineIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromPSNOnlineIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromPSNOnlineIDsResult> ServerGetPlayFabIDsFromPSNOnlineIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNOnlineIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromPSNOnlineIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromPSNOnlineIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromSteamIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromSteamIDsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromSteamIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromSteamIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromSteamIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromSteamIDsResult> ServerGetPlayFabIDsFromSteamIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromSteamIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromSteamIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromSteamIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromSteamNamesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromSteamNamesRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromSteamNamesAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromSteamNamesRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromSteamNamesAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromSteamNamesResult> ServerGetPlayFabIDsFromSteamNamesAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromSteamNamesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromSteamNamesRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamNamesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromSteamNamesResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromSteamNamesResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromTwitchIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromTwitchIDsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromTwitchIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromTwitchIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromTwitchIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromTwitchIDsResult> ServerGetPlayFabIDsFromTwitchIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromTwitchIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromTwitchIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromTwitchIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromTwitchIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromTwitchIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayFabIDsFromXboxLiveIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest_System_Threading_CancellationToken_"></a> ServerGetPlayFabIDsFromXboxLiveIDsAsync\(PlayFabEntity, AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayFabIDsFromXboxLiveIDsAsync</code>.

```csharp
public static Task<AccountManagementGetPlayFabIDsFromXboxLiveIDsResult> ServerGetPlayFabIDsFromXboxLiveIDsAsync(PlayFabEntity entity, AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromXboxLiveIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayFabIDsFromXboxLiveIDsResult](GDK.Net.PlayFab.AccountManagementGetPlayFabIDsFromXboxLiveIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayerCombinedInfoAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayerCombinedInfoRequest_System_Threading_CancellationToken_"></a> ServerGetPlayerCombinedInfoAsync\(PlayFabEntity, AccountManagementGetPlayerCombinedInfoRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayerCombinedInfoAsync</code>.

```csharp
public static Task<AccountManagementGetPlayerCombinedInfoResult> ServerGetPlayerCombinedInfoAsync(PlayFabEntity entity, AccountManagementGetPlayerCombinedInfoRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayerCombinedInfoRequest](GDK.Net.PlayFab.AccountManagementGetPlayerCombinedInfoRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayerCombinedInfoResult](GDK.Net.PlayFab.AccountManagementGetPlayerCombinedInfoResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetPlayerProfileAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetPlayerProfileRequest_System_Threading_CancellationToken_"></a> ServerGetPlayerProfileAsync\(PlayFabEntity, AccountManagementGetPlayerProfileRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetPlayerProfileAsync</code>.

```csharp
public static Task<AccountManagementGetPlayerProfileResult> ServerGetPlayerProfileAsync(PlayFabEntity entity, AccountManagementGetPlayerProfileRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetPlayerProfileRequest](GDK.Net.PlayFab.AccountManagementGetPlayerProfileRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetPlayerProfileResult](GDK.Net.PlayFab.AccountManagementGetPlayerProfileResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetServerCustomIDsFromPlayFabIDsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetServerCustomIDsFromPlayFabIDsRequest_System_Threading_CancellationToken_"></a> ServerGetServerCustomIDsFromPlayFabIDsAsync\(PlayFabEntity, AccountManagementGetServerCustomIDsFromPlayFabIDsRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetServerCustomIDsFromPlayFabIDsAsync</code>.

```csharp
public static Task<AccountManagementGetServerCustomIDsFromPlayFabIDsResult> ServerGetServerCustomIDsFromPlayFabIDsAsync(PlayFabEntity entity, AccountManagementGetServerCustomIDsFromPlayFabIDsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetServerCustomIDsFromPlayFabIDsRequest](GDK.Net.PlayFab.AccountManagementGetServerCustomIDsFromPlayFabIDsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetServerCustomIDsFromPlayFabIDsResult](GDK.Net.PlayFab.AccountManagementGetServerCustomIDsFromPlayFabIDsResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetUserAccountInfoAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetUserAccountInfoRequest_System_Threading_CancellationToken_"></a> ServerGetUserAccountInfoAsync\(PlayFabEntity, AccountManagementGetUserAccountInfoRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetUserAccountInfoAsync</code>.

```csharp
public static Task<AccountManagementGetUserAccountInfoResult> ServerGetUserAccountInfoAsync(PlayFabEntity entity, AccountManagementGetUserAccountInfoRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetUserAccountInfoRequest](GDK.Net.PlayFab.AccountManagementGetUserAccountInfoRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetUserAccountInfoResult](GDK.Net.PlayFab.AccountManagementGetUserAccountInfoResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerGetUserBansAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementGetUserBansRequest_System_Threading_CancellationToken_"></a> ServerGetUserBansAsync\(PlayFabEntity, AccountManagementGetUserBansRequest, CancellationToken\)

Calls <code>PFAccountManagementServerGetUserBansAsync</code>.

```csharp
public static Task<AccountManagementGetUserBansResult> ServerGetUserBansAsync(PlayFabEntity entity, AccountManagementGetUserBansRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementGetUserBansRequest](GDK.Net.PlayFab.AccountManagementGetUserBansRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementGetUserBansResult](GDK.Net.PlayFab.AccountManagementGetUserBansResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkBattleNetAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerLinkBattleNetAccountRequest_System_Threading_CancellationToken_"></a> ServerLinkBattleNetAccountAsync\(PlayFabEntity, AccountManagementServerLinkBattleNetAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkBattleNetAccountAsync</code>.

```csharp
public static Task ServerLinkBattleNetAccountAsync(PlayFabEntity entity, AccountManagementServerLinkBattleNetAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerLinkBattleNetAccountRequest](GDK.Net.PlayFab.AccountManagementServerLinkBattleNetAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkNintendoServiceAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerLinkNintendoServiceAccountRequest_System_Threading_CancellationToken_"></a> ServerLinkNintendoServiceAccountAsync\(PlayFabEntity, AccountManagementServerLinkNintendoServiceAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkNintendoServiceAccountAsync</code>.

```csharp
public static Task ServerLinkNintendoServiceAccountAsync(PlayFabEntity entity, AccountManagementServerLinkNintendoServiceAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerLinkNintendoServiceAccountRequest](GDK.Net.PlayFab.AccountManagementServerLinkNintendoServiceAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkNintendoServiceAccountSubjectAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkNintendoServiceAccountSubjectRequest_System_Threading_CancellationToken_"></a> ServerLinkNintendoServiceAccountSubjectAsync\(PlayFabEntity, AccountManagementLinkNintendoServiceAccountSubjectRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkNintendoServiceAccountSubjectAsync</code>.

```csharp
public static Task ServerLinkNintendoServiceAccountSubjectAsync(PlayFabEntity entity, AccountManagementLinkNintendoServiceAccountSubjectRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkNintendoServiceAccountSubjectRequest](GDK.Net.PlayFab.AccountManagementLinkNintendoServiceAccountSubjectRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkNintendoSwitchDeviceIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerLinkNintendoSwitchDeviceIdRequest_System_Threading_CancellationToken_"></a> ServerLinkNintendoSwitchDeviceIdAsync\(PlayFabEntity, AccountManagementServerLinkNintendoSwitchDeviceIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkNintendoSwitchDeviceIdAsync</code>.

```csharp
public static Task ServerLinkNintendoSwitchDeviceIdAsync(PlayFabEntity entity, AccountManagementServerLinkNintendoSwitchDeviceIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerLinkNintendoSwitchDeviceIdRequest](GDK.Net.PlayFab.AccountManagementServerLinkNintendoSwitchDeviceIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkPSNAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerLinkPSNAccountRequest_System_Threading_CancellationToken_"></a> ServerLinkPSNAccountAsync\(PlayFabEntity, AccountManagementServerLinkPSNAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkPSNAccountAsync</code>.

```csharp
public static Task ServerLinkPSNAccountAsync(PlayFabEntity entity, AccountManagementServerLinkPSNAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerLinkPSNAccountRequest](GDK.Net.PlayFab.AccountManagementServerLinkPSNAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkPSNIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkPSNIdRequest_System_Threading_CancellationToken_"></a> ServerLinkPSNIdAsync\(PlayFabEntity, AccountManagementLinkPSNIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkPSNIdAsync</code>.

```csharp
public static Task ServerLinkPSNIdAsync(PlayFabEntity entity, AccountManagementLinkPSNIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkPSNIdRequest](GDK.Net.PlayFab.AccountManagementLinkPSNIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkServerCustomIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkServerCustomIdRequest_System_Threading_CancellationToken_"></a> ServerLinkServerCustomIdAsync\(PlayFabEntity, AccountManagementLinkServerCustomIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkServerCustomIdAsync</code>.

```csharp
public static Task ServerLinkServerCustomIdAsync(PlayFabEntity entity, AccountManagementLinkServerCustomIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkServerCustomIdRequest](GDK.Net.PlayFab.AccountManagementLinkServerCustomIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkSteamIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementLinkSteamIdRequest_System_Threading_CancellationToken_"></a> ServerLinkSteamIdAsync\(PlayFabEntity, AccountManagementLinkSteamIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkSteamIdAsync</code>.

```csharp
public static Task ServerLinkSteamIdAsync(PlayFabEntity entity, AccountManagementLinkSteamIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementLinkSteamIdRequest](GDK.Net.PlayFab.AccountManagementLinkSteamIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerLinkXboxAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerLinkXboxAccountRequest_System_Threading_CancellationToken_"></a> ServerLinkXboxAccountAsync\(PlayFabEntity, AccountManagementServerLinkXboxAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerLinkXboxAccountAsync</code>.

```csharp
public static Task ServerLinkXboxAccountAsync(PlayFabEntity entity, AccountManagementServerLinkXboxAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerLinkXboxAccountRequest](GDK.Net.PlayFab.AccountManagementServerLinkXboxAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerRevokeAllBansForUserAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementRevokeAllBansForUserRequest_System_Threading_CancellationToken_"></a> ServerRevokeAllBansForUserAsync\(PlayFabEntity, AccountManagementRevokeAllBansForUserRequest, CancellationToken\)

Calls <code>PFAccountManagementServerRevokeAllBansForUserAsync</code>.

```csharp
public static Task<AccountManagementRevokeAllBansForUserResult> ServerRevokeAllBansForUserAsync(PlayFabEntity entity, AccountManagementRevokeAllBansForUserRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementRevokeAllBansForUserRequest](GDK.Net.PlayFab.AccountManagementRevokeAllBansForUserRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementRevokeAllBansForUserResult](GDK.Net.PlayFab.AccountManagementRevokeAllBansForUserResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerRevokeBansAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementRevokeBansRequest_System_Threading_CancellationToken_"></a> ServerRevokeBansAsync\(PlayFabEntity, AccountManagementRevokeBansRequest, CancellationToken\)

Calls <code>PFAccountManagementServerRevokeBansAsync</code>.

```csharp
public static Task<AccountManagementRevokeBansResult> ServerRevokeBansAsync(PlayFabEntity entity, AccountManagementRevokeBansRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementRevokeBansRequest](GDK.Net.PlayFab.AccountManagementRevokeBansRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementRevokeBansResult](GDK.Net.PlayFab.AccountManagementRevokeBansResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_ServerSendCustomAccountRecoveryEmailAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementSendCustomAccountRecoveryEmailRequest_System_Threading_CancellationToken_"></a> ServerSendCustomAccountRecoveryEmailAsync\(PlayFabEntity, AccountManagementSendCustomAccountRecoveryEmailRequest, CancellationToken\)

Calls <code>PFAccountManagementServerSendCustomAccountRecoveryEmailAsync</code>.

```csharp
public static Task ServerSendCustomAccountRecoveryEmailAsync(PlayFabEntity entity, AccountManagementSendCustomAccountRecoveryEmailRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementSendCustomAccountRecoveryEmailRequest](GDK.Net.PlayFab.AccountManagementSendCustomAccountRecoveryEmailRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerSendEmailFromTemplateAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementSendEmailFromTemplateRequest_System_Threading_CancellationToken_"></a> ServerSendEmailFromTemplateAsync\(PlayFabEntity, AccountManagementSendEmailFromTemplateRequest, CancellationToken\)

Calls <code>PFAccountManagementServerSendEmailFromTemplateAsync</code>.

```csharp
public static Task ServerSendEmailFromTemplateAsync(PlayFabEntity entity, AccountManagementSendEmailFromTemplateRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementSendEmailFromTemplateRequest](GDK.Net.PlayFab.AccountManagementSendEmailFromTemplateRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkBattleNetAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerUnlinkBattleNetAccountRequest_System_Threading_CancellationToken_"></a> ServerUnlinkBattleNetAccountAsync\(PlayFabEntity, AccountManagementServerUnlinkBattleNetAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkBattleNetAccountAsync</code>.

```csharp
public static Task ServerUnlinkBattleNetAccountAsync(PlayFabEntity entity, AccountManagementServerUnlinkBattleNetAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerUnlinkBattleNetAccountRequest](GDK.Net.PlayFab.AccountManagementServerUnlinkBattleNetAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkNintendoServiceAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerUnlinkNintendoServiceAccountRequest_System_Threading_CancellationToken_"></a> ServerUnlinkNintendoServiceAccountAsync\(PlayFabEntity, AccountManagementServerUnlinkNintendoServiceAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkNintendoServiceAccountAsync</code>.

```csharp
public static Task ServerUnlinkNintendoServiceAccountAsync(PlayFabEntity entity, AccountManagementServerUnlinkNintendoServiceAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerUnlinkNintendoServiceAccountRequest](GDK.Net.PlayFab.AccountManagementServerUnlinkNintendoServiceAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkNintendoSwitchDeviceIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerUnlinkNintendoSwitchDeviceIdRequest_System_Threading_CancellationToken_"></a> ServerUnlinkNintendoSwitchDeviceIdAsync\(PlayFabEntity, AccountManagementServerUnlinkNintendoSwitchDeviceIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkNintendoSwitchDeviceIdAsync</code>.

```csharp
public static Task ServerUnlinkNintendoSwitchDeviceIdAsync(PlayFabEntity entity, AccountManagementServerUnlinkNintendoSwitchDeviceIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerUnlinkNintendoSwitchDeviceIdRequest](GDK.Net.PlayFab.AccountManagementServerUnlinkNintendoSwitchDeviceIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkPSNAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerUnlinkPSNAccountRequest_System_Threading_CancellationToken_"></a> ServerUnlinkPSNAccountAsync\(PlayFabEntity, AccountManagementServerUnlinkPSNAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkPSNAccountAsync</code>.

```csharp
public static Task ServerUnlinkPSNAccountAsync(PlayFabEntity entity, AccountManagementServerUnlinkPSNAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerUnlinkPSNAccountRequest](GDK.Net.PlayFab.AccountManagementServerUnlinkPSNAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkServerCustomIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUnlinkServerCustomIdRequest_System_Threading_CancellationToken_"></a> ServerUnlinkServerCustomIdAsync\(PlayFabEntity, AccountManagementUnlinkServerCustomIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkServerCustomIdAsync</code>.

```csharp
public static Task ServerUnlinkServerCustomIdAsync(PlayFabEntity entity, AccountManagementUnlinkServerCustomIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUnlinkServerCustomIdRequest](GDK.Net.PlayFab.AccountManagementUnlinkServerCustomIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkSteamIdAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUnlinkSteamIdRequest_System_Threading_CancellationToken_"></a> ServerUnlinkSteamIdAsync\(PlayFabEntity, AccountManagementUnlinkSteamIdRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkSteamIdAsync</code>.

```csharp
public static Task ServerUnlinkSteamIdAsync(PlayFabEntity entity, AccountManagementUnlinkSteamIdRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUnlinkSteamIdRequest](GDK.Net.PlayFab.AccountManagementUnlinkSteamIdRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUnlinkXboxAccountAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerUnlinkXboxAccountRequest_System_Threading_CancellationToken_"></a> ServerUnlinkXboxAccountAsync\(PlayFabEntity, AccountManagementServerUnlinkXboxAccountRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUnlinkXboxAccountAsync</code>.

```csharp
public static Task ServerUnlinkXboxAccountAsync(PlayFabEntity entity, AccountManagementServerUnlinkXboxAccountRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerUnlinkXboxAccountRequest](GDK.Net.PlayFab.AccountManagementServerUnlinkXboxAccountRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUpdateAvatarUrlAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementServerUpdateAvatarUrlRequest_System_Threading_CancellationToken_"></a> ServerUpdateAvatarUrlAsync\(PlayFabEntity, AccountManagementServerUpdateAvatarUrlRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUpdateAvatarUrlAsync</code>.

```csharp
public static Task ServerUpdateAvatarUrlAsync(PlayFabEntity entity, AccountManagementServerUpdateAvatarUrlRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementServerUpdateAvatarUrlRequest](GDK.Net.PlayFab.AccountManagementServerUpdateAvatarUrlRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_AccountManagement_ServerUpdateBansAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementUpdateBansRequest_System_Threading_CancellationToken_"></a> ServerUpdateBansAsync\(PlayFabEntity, AccountManagementUpdateBansRequest, CancellationToken\)

Calls <code>PFAccountManagementServerUpdateBansAsync</code>.

```csharp
public static Task<AccountManagementUpdateBansResult> ServerUpdateBansAsync(PlayFabEntity entity, AccountManagementUpdateBansRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementUpdateBansRequest](GDK.Net.PlayFab.AccountManagementUpdateBansRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementUpdateBansResult](GDK.Net.PlayFab.AccountManagementUpdateBansResult.md)\>

### <a id="GDK_Net_PlayFab_AccountManagement_SetDisplayNameAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_AccountManagementSetDisplayNameRequest_System_Threading_CancellationToken_"></a> SetDisplayNameAsync\(PlayFabEntity, AccountManagementSetDisplayNameRequest, CancellationToken\)

Calls <code>PFAccountManagementSetDisplayNameAsync</code>.

```csharp
public static Task<AccountManagementSetDisplayNameResponse> SetDisplayNameAsync(PlayFabEntity entity, AccountManagementSetDisplayNameRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [AccountManagementSetDisplayNameRequest](GDK.Net.PlayFab.AccountManagementSetDisplayNameRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AccountManagementSetDisplayNameResponse](GDK.Net.PlayFab.AccountManagementSetDisplayNameResponse.md)\>

