# <a id="GDK_Net_XboxLive_SocialManagerEventType"></a> Enum SocialManagerEventType

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Native social-manager event kind. Mirrors <code>XblSocialManagerEventType</code>.

```csharp
public enum SocialManagerEventType : uint
```

## Fields

`LocalUserAdded = 5` 

A local user's initial social graph finished loading.



`PresenceChanged = 2` 

One or more users' presence changed.



`ProfilesChanged = 3` 

One or more users' profiles changed.



`SocialRelationshipsChanged = 4` 

One or more users' social relationships changed.



`SocialUserGroupLoaded = 6` 

A social user group's initial set finished loading.



`SocialUserGroupUpdated = 7` 

A list-backed social user group finished updating.



`Unknown = 8` 

The event kind is unknown to this projection.



`UsersAddedToSocialGraph = 0` 

One or more users were added to the social graph.



`UsersRemovedFromSocialGraph = 1` 

One or more users were removed from the social graph.



