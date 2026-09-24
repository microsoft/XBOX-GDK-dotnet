# <a id="GDK_Net_XboxLive_SocialUserGroupUpdatedSocialManagerEvent"></a> Class SocialUserGroupUpdatedSocialManagerEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A list-backed social user group finished updating.

```csharp
public sealed record SocialUserGroupUpdatedSocialManagerEvent : SocialManagerEvent, IEquatable<SocialManagerEvent>, IEquatable<SocialUserGroupUpdatedSocialManagerEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerEvent](GDK.Net.XboxLive.SocialManagerEvent.md) ← 
[SocialUserGroupUpdatedSocialManagerEvent](GDK.Net.XboxLive.SocialUserGroupUpdatedSocialManagerEvent.md)

#### Implements

[IEquatable<SocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<SocialUserGroupUpdatedSocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_XboxLive_SocialUserGroupUpdatedSocialManagerEvent_Group"></a> Group

The group that updated, when XSAPI supplied a group handle.

```csharp
public SocialManagerUserGroup? Group { get; }
```

#### Property Value

 [SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)?

