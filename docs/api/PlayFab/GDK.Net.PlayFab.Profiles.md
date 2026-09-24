# <a id="GDK_Net_PlayFab_Profiles"></a> Class Profiles

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Profiles service (<code>PFProfiles.h</code>).

```csharp
public static class Profiles
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Profiles](GDK.Net.PlayFab.Profiles.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Profiles_GetProfileAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_ProfilesGetEntityProfileRequest_System_Threading_CancellationToken_"></a> GetProfileAsync\(PlayFabEntity, ProfilesGetEntityProfileRequest, CancellationToken\)

Calls <code>PFProfilesGetProfileAsync</code>.

```csharp
public static Task<ProfilesGetEntityProfileResponse> GetProfileAsync(PlayFabEntity entity, ProfilesGetEntityProfileRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [ProfilesGetEntityProfileRequest](GDK.Net.PlayFab.ProfilesGetEntityProfileRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ProfilesGetEntityProfileResponse](GDK.Net.PlayFab.ProfilesGetEntityProfileResponse.md)\>

### <a id="GDK_Net_PlayFab_Profiles_GetProfilesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_ProfilesGetEntityProfilesRequest_System_Threading_CancellationToken_"></a> GetProfilesAsync\(PlayFabEntity, ProfilesGetEntityProfilesRequest, CancellationToken\)

Calls <code>PFProfilesGetProfilesAsync</code>.

```csharp
public static Task<ProfilesGetEntityProfilesResponse> GetProfilesAsync(PlayFabEntity entity, ProfilesGetEntityProfilesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [ProfilesGetEntityProfilesRequest](GDK.Net.PlayFab.ProfilesGetEntityProfilesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ProfilesGetEntityProfilesResponse](GDK.Net.PlayFab.ProfilesGetEntityProfilesResponse.md)\>

### <a id="GDK_Net_PlayFab_Profiles_GetTitlePlayersFromMasterPlayerAccountIdsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_ProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequest_System_Threading_CancellationToken_"></a> GetTitlePlayersFromMasterPlayerAccountIdsAsync\(PlayFabEntity, ProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequest, CancellationToken\)

Calls <code>PFProfilesGetTitlePlayersFromMasterPlayerAccountIdsAsync</code>.

```csharp
public static Task<ProfilesGetTitlePlayersFromMasterPlayerAccountIdsResponse> GetTitlePlayersFromMasterPlayerAccountIdsAsync(PlayFabEntity entity, ProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [ProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequest](GDK.Net.PlayFab.ProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ProfilesGetTitlePlayersFromMasterPlayerAccountIdsResponse](GDK.Net.PlayFab.ProfilesGetTitlePlayersFromMasterPlayerAccountIdsResponse.md)\>

### <a id="GDK_Net_PlayFab_Profiles_SetProfileLanguageAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_ProfilesSetProfileLanguageRequest_System_Threading_CancellationToken_"></a> SetProfileLanguageAsync\(PlayFabEntity, ProfilesSetProfileLanguageRequest, CancellationToken\)

Calls <code>PFProfilesSetProfileLanguageAsync</code>.

```csharp
public static Task<ProfilesSetProfileLanguageResponse> SetProfileLanguageAsync(PlayFabEntity entity, ProfilesSetProfileLanguageRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [ProfilesSetProfileLanguageRequest](GDK.Net.PlayFab.ProfilesSetProfileLanguageRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ProfilesSetProfileLanguageResponse](GDK.Net.PlayFab.ProfilesSetProfileLanguageResponse.md)\>

### <a id="GDK_Net_PlayFab_Profiles_SetProfilePolicyAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_ProfilesSetEntityProfilePolicyRequest_System_Threading_CancellationToken_"></a> SetProfilePolicyAsync\(PlayFabEntity, ProfilesSetEntityProfilePolicyRequest, CancellationToken\)

Calls <code>PFProfilesSetProfilePolicyAsync</code>.

```csharp
public static Task<ProfilesSetEntityProfilePolicyResponse> SetProfilePolicyAsync(PlayFabEntity entity, ProfilesSetEntityProfilePolicyRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [ProfilesSetEntityProfilePolicyRequest](GDK.Net.PlayFab.ProfilesSetEntityProfilePolicyRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ProfilesSetEntityProfilePolicyResponse](GDK.Net.PlayFab.ProfilesSetEntityProfilePolicyResponse.md)\>

