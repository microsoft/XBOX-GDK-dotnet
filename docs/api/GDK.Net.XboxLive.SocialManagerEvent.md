# <a id="GDK_Net_XboxLive_SocialManagerEvent"></a> Class SocialManagerEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Base record for one snapshotted <xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref> event.

```csharp
public abstract record SocialManagerEvent : IEquatable<SocialManagerEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerEvent](GDK.Net.XboxLive.SocialManagerEvent.md)

#### Derived

[LocalUserAddedSocialManagerEvent](GDK.Net.XboxLive.LocalUserAddedSocialManagerEvent.md), 
[PresenceChangedSocialManagerEvent](GDK.Net.XboxLive.PresenceChangedSocialManagerEvent.md), 
[ProfilesChangedSocialManagerEvent](GDK.Net.XboxLive.ProfilesChangedSocialManagerEvent.md), 
[SocialRelationshipsChangedSocialManagerEvent](GDK.Net.XboxLive.SocialRelationshipsChangedSocialManagerEvent.md), 
[SocialUserGroupLoadedSocialManagerEvent](GDK.Net.XboxLive.SocialUserGroupLoadedSocialManagerEvent.md), 
[SocialUserGroupUpdatedSocialManagerEvent](GDK.Net.XboxLive.SocialUserGroupUpdatedSocialManagerEvent.md), 
[UnknownSocialManagerEvent](GDK.Net.XboxLive.UnknownSocialManagerEvent.md), 
[UsersAddedToSocialGraphSocialManagerEvent](GDK.Net.XboxLive.UsersAddedToSocialGraphSocialManagerEvent.md), 
[UsersRemovedFromSocialGraphSocialManagerEvent](GDK.Net.XboxLive.UsersRemovedFromSocialGraphSocialManagerEvent.md)

#### Implements

[IEquatable<SocialManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

XSAPI's social manager differs from the other state-change subsystems: <code>DoWork</code> has no
matching <code>Finish</code>, and native event memory is valid only until the next pump. The .NET
projection therefore snapshots events into this record hierarchy before returning, while
keeping long-lived group handles as identity-mapped wrappers.

## Properties

### <a id="GDK_Net_XboxLive_SocialManagerEvent_Error"></a> Error

The operation error, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the native HRESULT was <code>S_OK</code>.

```csharp
public GameRuntimeException? Error { get; }
```

#### Property Value

 [GameRuntimeException](GDK.Net.GameRuntimeException.md)?

### <a id="GDK_Net_XboxLive_SocialManagerEvent_EventType"></a> EventType

The kind of event, mirroring <code>XblSocialManagerEvent::eventType</code>.

```csharp
public SocialManagerEventType EventType { get; }
```

#### Property Value

 [SocialManagerEventType](GDK.Net.XboxLive.SocialManagerEventType.md)

### <a id="GDK_Net_XboxLive_SocialManagerEvent_LocalUser"></a> LocalUser

The local user whose social graph produced the event, when XSAPI supplied one.

```csharp
public User? LocalUser { get; }
```

#### Property Value

 [User](GDK.Net.Users.User.md)?

### <a id="GDK_Net_XboxLive_SocialManagerEvent_Succeeded"></a> Succeeded

Whether the event's native HRESULT represented success.

```csharp
public bool Succeeded { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

