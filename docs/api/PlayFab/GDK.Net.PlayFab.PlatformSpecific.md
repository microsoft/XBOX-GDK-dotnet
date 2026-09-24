# <a id="GDK_Net_PlayFab_PlatformSpecific"></a> Class PlatformSpecific

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab PlatformSpecific service (<code>PFPlatformSpecific.h</code>).

```csharp
public static class PlatformSpecific
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlatformSpecific](GDK.Net.PlayFab.PlatformSpecific.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_PlatformSpecific_ServerAwardSteamAchievementAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PlatformSpecificAwardSteamAchievementRequest_System_Threading_CancellationToken_"></a> ServerAwardSteamAchievementAsync\(PlayFabEntity, PlatformSpecificAwardSteamAchievementRequest, CancellationToken\)

Calls <code>PFPlatformSpecificServerAwardSteamAchievementAsync</code>.

```csharp
public static Task<PlatformSpecificAwardSteamAchievementResult> ServerAwardSteamAchievementAsync(PlayFabEntity entity, PlatformSpecificAwardSteamAchievementRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PlatformSpecificAwardSteamAchievementRequest](GDK.Net.PlayFab.PlatformSpecificAwardSteamAchievementRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlatformSpecificAwardSteamAchievementResult](GDK.Net.PlayFab.PlatformSpecificAwardSteamAchievementResult.md)\>

