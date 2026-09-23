# <a id="GDK_Net_XboxLive_SocialRelationshipsChangedSocialManagerEvent"></a> Class SocialRelationshipsChangedSocialManagerEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One or more users' social relationships changed.

```csharp
public sealed record SocialRelationshipsChangedSocialManagerEvent : SocialManagerEvent, IEquatable<SocialManagerEvent>, IEquatable<SocialRelationshipsChangedSocialManagerEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerEvent](GDK.Net.XboxLive.SocialManagerEvent.md) ← 
[SocialRelationshipsChangedSocialManagerEvent](GDK.Net.XboxLive.SocialRelationshipsChangedSocialManagerEvent.md)

#### Implements

[IEquatable<SocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<SocialRelationshipsChangedSocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_XboxLive_SocialRelationshipsChangedSocialManagerEvent_Users"></a> Users

The users whose relationships changed.

```csharp
public IReadOnlyList<SocialManagerUser> Users { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SocialManagerUser](GDK.Net.XboxLive.SocialManagerUser.md)\>

