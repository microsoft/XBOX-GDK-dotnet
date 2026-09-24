# <a id="GDK_Net_XboxLive_PermissionDenyReason"></a> Enum PermissionDenyReason

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Reasons Xbox Live can report for denying a privacy permission check.

```csharp
public enum PermissionDenyReason : uint
```

## Fields

`BlockListRestrictsTarget = 5` 

The requester's block list restricts interaction with the target.



`CrossNetworkUserMustBeFriend = 12` 

The cross-network target must be an in-game friend before the action is allowed.



`MissingPrivilege = 3` 

The requester is missing a privilege required for the action.



`MuteListRestrictsTarget = 7` 

The requester's mute list restricts interaction with the target.



`NotAllowed = 2` 

The request completed successfully, but the action is not allowed.



`PrivacySettingRestrictsTarget = 9` 

A requester privacy setting restricts interaction with the target.



`PrivilegeRestrictsTarget = 4` 

A requester privilege restricts interaction with the target.



`Unknown = 0` 

No specific reason was supplied, or the permission check could not be completed.



