# <a id="GDK_Net_PlayFab_Localization"></a> Class Localization

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Localization service (<code>PFLocalization.h</code>).

```csharp
public static class Localization
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Localization](GDK.Net.PlayFab.Localization.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Localization_GetLanguageListAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LocalizationGetLanguageListRequest_System_Threading_CancellationToken_"></a> GetLanguageListAsync\(PlayFabEntity, LocalizationGetLanguageListRequest, CancellationToken\)

Calls <code>PFLocalizationGetLanguageListAsync</code>.

```csharp
public static Task<LocalizationGetLanguageListResponse> GetLanguageListAsync(PlayFabEntity entity, LocalizationGetLanguageListRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [LocalizationGetLanguageListRequest](GDK.Net.PlayFab.LocalizationGetLanguageListRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LocalizationGetLanguageListResponse](GDK.Net.PlayFab.LocalizationGetLanguageListResponse.md)\>

