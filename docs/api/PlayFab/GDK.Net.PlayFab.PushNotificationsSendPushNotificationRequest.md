# <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest"></a> Class PushNotificationsSendPushNotificationRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFPushNotificationsSendPushNotificationRequest</code>.

```csharp
public sealed class PushNotificationsSendPushNotificationRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PushNotificationsSendPushNotificationRequest](GDK.Net.PlayFab.PushNotificationsSendPushNotificationRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_AdvancedPlatformDelivery"></a> AdvancedPlatformDelivery

<code>AdvancedPlatformDelivery</code>.

```csharp
public IReadOnlyList<PushNotificationsAdvancedPushPlatformMsg>? AdvancedPlatformDelivery { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PushNotificationsAdvancedPushPlatformMsg](GDK.Net.PlayFab.PushNotificationsAdvancedPushPlatformMsg.md)\>?

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_Message"></a> Message

<code>Message</code>.

```csharp
public string? Message { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_Package"></a> Package

<code>Package</code>.

```csharp
public PushNotificationsPushNotificationPackage? Package { get; set; }
```

#### Property Value

 [PushNotificationsPushNotificationPackage](GDK.Net.PlayFab.PushNotificationsPushNotificationPackage.md)?

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_Recipient"></a> Recipient

<code>Recipient</code>.

```csharp
public string? Recipient { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_Subject"></a> Subject

<code>Subject</code>.

```csharp
public string? Subject { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PushNotificationsSendPushNotificationRequest_TargetPlatforms"></a> TargetPlatforms

<code>TargetPlatforms</code>.

```csharp
public IReadOnlyList<PushNotificationPlatform>? TargetPlatforms { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PushNotificationPlatform](GDK.Net.PlayFab.PushNotificationPlatform.md)\>?

