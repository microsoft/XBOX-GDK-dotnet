# <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteReceived"></a> Class LobbyInviteReceived

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

An invite to a lobby arrived for an entity the title is listening for.

```csharp
public sealed record LobbyInviteReceived : LobbyStateChange, IEquatable<LobbyStateChange>, IEquatable<LobbyInviteReceived>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyInviteReceived](GDK.Net.PlayFab.Multiplayer.LobbyInviteReceived.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyInviteReceived\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteReceived_ConnectionString"></a> ConnectionString

The connection string to pass to <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.JoinLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String%2cGDK.Net.PlayFab.LobbyJoinConfiguration)" data-throw-if-not-resolved="false"></xref>.

```csharp
public string? ConnectionString { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteReceived_InvitingEntity"></a> InvitingEntity

The entity that sent the invite.

```csharp
public EntityKey InvitingEntity { get; }
```

#### Property Value

 [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteReceived_ListeningEntity"></a> ListeningEntity

The local entity the invite was sent to.

```csharp
public EntityKey ListeningEntity { get; }
```

#### Property Value

 [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyInviteReceived_LobbyId"></a> LobbyId

The id of the lobby the invite is for.

```csharp
public string? LobbyId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

