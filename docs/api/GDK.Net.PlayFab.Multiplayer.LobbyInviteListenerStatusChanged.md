# <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteListenerStatusChanged"></a> Class LobbyInviteListenerStatusChanged

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

An invite listener's status changed.

```csharp
public sealed record LobbyInviteListenerStatusChanged : LobbyStateChange, IEquatable<LobbyStateChange>, IEquatable<LobbyInviteListenerStatusChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyInviteListenerStatusChanged](GDK.Net.PlayFab.Multiplayer.LobbyInviteListenerStatusChanged.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyInviteListenerStatusChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[LobbyStateChange.ChangeType](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyStateChange\_ChangeType), 
[LobbyStateChange.Lobby](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyStateChange\_Lobby), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteListenerStatusChanged_ListeningEntity"></a> ListeningEntity

The entity whose listener changed.

```csharp
public EntityKey ListeningEntity { get; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteListenerStatusChanged_Status"></a> Status

The listener's new status.

```csharp
public LobbyInviteListenerStatus Status { get; }
```

#### Property Value

 [LobbyInviteListenerStatus](GDK.Net.PlayFab.LobbyInviteListenerStatus.md)

