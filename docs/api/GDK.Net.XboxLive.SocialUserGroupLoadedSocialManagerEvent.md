# <a id="GDK_Net_XboxLive_SocialUserGroupLoadedSocialManagerEvent"></a> Class SocialUserGroupLoadedSocialManagerEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A social user group's initial tracked set finished loading.

```csharp
public sealed record SocialUserGroupLoadedSocialManagerEvent : SocialManagerEvent, IEquatable<SocialManagerEvent>, IEquatable<SocialUserGroupLoadedSocialManagerEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerEvent](GDK.Net.XboxLive.SocialManagerEvent.md) ← 
[SocialUserGroupLoadedSocialManagerEvent](GDK.Net.XboxLive.SocialUserGroupLoadedSocialManagerEvent.md)

#### Implements

[IEquatable<SocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<SocialUserGroupLoadedSocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[SocialManagerEvent.EventType](GDK.Net.XboxLive.SocialManagerEvent.md\#GDK\_Net\_XboxLive\_SocialManagerEvent\_EventType), 
[SocialManagerEvent.LocalUser](GDK.Net.XboxLive.SocialManagerEvent.md\#GDK\_Net\_XboxLive\_SocialManagerEvent\_LocalUser), 
[SocialManagerEvent.Error](GDK.Net.XboxLive.SocialManagerEvent.md\#GDK\_Net\_XboxLive\_SocialManagerEvent\_Error), 
[SocialManagerEvent.Succeeded](GDK.Net.XboxLive.SocialManagerEvent.md\#GDK\_Net\_XboxLive\_SocialManagerEvent\_Succeeded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialUserGroupLoadedSocialManagerEvent_Group"></a> Group

The group that loaded, when XSAPI supplied a group handle.

```csharp
public SocialManagerUserGroup? Group { get; }
```

#### Property Value

 [SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)?

