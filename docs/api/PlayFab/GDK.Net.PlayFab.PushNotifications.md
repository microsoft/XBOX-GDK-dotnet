# <a id="GDK_Net_PlayFab_PushNotifications"></a> Class PushNotifications

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab PushNotifications service (<code>PFPushNotifications.h</code>).

```csharp
public static class PushNotifications
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PushNotifications](GDK.Net.PlayFab.PushNotifications.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_PushNotifications_ServerSendPushNotificationAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_System_Threading_CancellationToken_"></a> ServerSendPushNotificationAsync\(PlayFabEntity, PushNotificationsSendPushNotificationRequest, CancellationToken\)

Calls <code>PFPushNotificationsServerSendPushNotificationAsync</code>.

```csharp
public static Task ServerSendPushNotificationAsync(PlayFabEntity entity, PushNotificationsSendPushNotificationRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PushNotificationsSendPushNotificationRequest](GDK.Net.PlayFab.PushNotificationsSendPushNotificationRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_PushNotifications_ServerSendPushNotificationFromTemplateAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_PushNotificationsSendPushNotificationFromTemplateRequest_System_Threading_CancellationToken_"></a> ServerSendPushNotificationFromTemplateAsync\(PlayFabEntity, PushNotificationsSendPushNotificationFromTemplateRequest, CancellationToken\)

Calls <code>PFPushNotificationsServerSendPushNotificationFromTemplateAsync</code>.

```csharp
public static Task ServerSendPushNotificationFromTemplateAsync(PlayFabEntity entity, PushNotificationsSendPushNotificationFromTemplateRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [PushNotificationsSendPushNotificationFromTemplateRequest](GDK.Net.PlayFab.PushNotificationsSendPushNotificationFromTemplateRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

