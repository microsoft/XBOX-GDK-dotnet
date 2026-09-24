# <a id="GDK_Net_PlayFab_Multiplayer_Lobby"></a> Class Lobby

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A PlayFab lobby (<code>PFLobbyHandle</code>). Instances are identity-mapped by their owning
<xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer" data-throw-if-not-resolved="false"></xref>, so the same native lobby always surfaces as the same object.

```csharp
public sealed class Lobby
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
A lobby's handle is owned by the multiplayer library, not by this wrapper, so there is nothing
to dispose. The lobby stays usable until its teardown change arrives
(<xref href="GDK.Net.PlayFab.Multiplayer.LeaveLobbyCompleted" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.PlayFab.Multiplayer.LobbyDisconnected" data-throw-if-not-resolved="false"></xref>,
<xref href="GDK.Net.PlayFab.Multiplayer.ServerLeaveLobbyAsServerCompleted" data-throw-if-not-resolved="false"></xref> or <xref href="GDK.Net.PlayFab.Multiplayer.ServerDeleteLobbyCompleted" data-throw-if-not-resolved="false"></xref>)
after which every member throws <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
Every getter snapshots into managed memory, so the values it returns stay valid outside the
state-change loop.
</p>

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_AccessPolicy"></a> AccessPolicy

Who may find and join the lobby (<code>PFLobbyGetAccessPolicy</code>).

```csharp
public LobbyAccessPolicy AccessPolicy { get; }
```

#### Property Value

 [LobbyAccessPolicy](../GDK.Net.PlayFab.LobbyAccessPolicy.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ConnectionString"></a> ConnectionString

The connection string used to invite others (<code>PFLobbyGetConnectionString</code>).

```csharp
public string? ConnectionString { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_Id"></a> Id

The lobby's PlayFab id (<code>PFLobbyGetLobbyId</code>).

```csharp
public string? Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_IsValid"></a> IsValid

Whether the lobby is still usable.

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_MaxMemberCount"></a> MaxMemberCount

The maximum number of members (<code>PFLobbyGetMaxMemberCount</code>).

```csharp
public uint MaxMemberCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_Members"></a> Members

A snapshot of the lobby's members (<code>PFLobbyGetMembers</code>).

```csharp
public IReadOnlyList<EntityKey> Members { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[EntityKey](../GDK.Net.PlayFab.EntityKey.md)\>

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_MembershipLock"></a> MembershipLock

Whether new members may join (<code>PFLobbyGetMembershipLock</code>).

```csharp
public LobbyMembershipLock MembershipLock { get; }
```

#### Property Value

 [LobbyMembershipLock](../GDK.Net.PlayFab.LobbyMembershipLock.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_Owner"></a> Owner

The lobby owner, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it has none (<code>PFLobbyGetOwner</code>).

```csharp
public EntityKey? Owner { get; }
```

#### Property Value

 [EntityKey](../GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_OwnerMigrationPolicy"></a> OwnerMigrationPolicy

How ownership moves when the owner leaves (<code>PFLobbyGetOwnerMigrationPolicy</code>).

```csharp
public LobbyOwnerMigrationPolicy OwnerMigrationPolicy { get; }
```

#### Property Value

 [LobbyOwnerMigrationPolicy](../GDK.Net.PlayFab.LobbyOwnerMigrationPolicy.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_Properties"></a> Properties

The lobby's shared properties
(<code>PFLobbyGetLobbyPropertyKeys</code>, <code>PFLobbyGetLobbyProperty</code>).

```csharp
public IReadOnlyDictionary<string, string> Properties { get; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_RestrictInvitesToLobbyOwner"></a> RestrictInvitesToLobbyOwner

Whether only the owner may send invites (<code>PFLobbyGetRestrictInvitesToLobbyOwner</code>).

```csharp
public bool RestrictInvitesToLobbyOwner { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_SearchProperties"></a> SearchProperties

The lobby's searchable properties
(<code>PFLobbyGetSearchPropertyKeys</code>, <code>PFLobbyGetSearchProperty</code>).

```csharp
public IReadOnlyDictionary<string, string> SearchProperties { get; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_Server"></a> Server

The server entity, when the lobby has one (<code>PFLobbyGetServer</code>).

```csharp
public EntityKey? Server { get; }
```

#### Property Value

 [EntityKey](../GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ServerConnectionStatus"></a> ServerConnectionStatus

The server's connection status (<code>PFLobbyGetServerConnectionStatus</code>).

```csharp
public LobbyServerConnectionStatus ServerConnectionStatus { get; }
```

#### Property Value

 [LobbyServerConnectionStatus](../GDK.Net.PlayFab.LobbyServerConnectionStatus.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ServerProperties"></a> ServerProperties

The server's properties
(<code>PFLobbyGetServerPropertyKeys</code>, <code>PFLobbyGetServerProperty</code>).

```csharp
public IReadOnlyDictionary<string, string> ServerProperties { get; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_AddMember_GDK_Net_PlayFab_EntityKey_System_Collections_Generic_IReadOnlyDictionary_System_String_System_String__"></a> AddMember\(EntityKey, IReadOnlyDictionary<string, string\>?\)

Adds another local member to this lobby (<code>PFLobbyAddMember</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.AddMemberCompleted" data-throw-if-not-resolved="false"></xref> on a later pump.

```csharp
public OperationId AddMember(EntityKey localUser, IReadOnlyDictionary<string, string>? memberProperties = null)
```

#### Parameters

`localUser` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`memberProperties` [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ForceRemoveMember_GDK_Net_PlayFab_EntityKey_System_Boolean_"></a> ForceRemoveMember\(EntityKey, bool\)

Removes another member (<code>PFLobbyForceRemoveMember</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.ForceRemoveMemberCompleted" data-throw-if-not-resolved="false"></xref> on a later pump.

```csharp
public OperationId ForceRemoveMember(EntityKey targetMember, bool preventRejoin = false)
```

#### Parameters

`targetMember` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`preventRejoin` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_GetMemberConnectionStatus_GDK_Net_PlayFab_EntityKey_"></a> GetMemberConnectionStatus\(EntityKey\)

A member's connection status (<code>PFLobbyGetMemberConnectionStatus</code>).

```csharp
public LobbyMemberConnectionStatus GetMemberConnectionStatus(EntityKey member)
```

#### Parameters

`member` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

#### Returns

 [LobbyMemberConnectionStatus](../GDK.Net.PlayFab.LobbyMemberConnectionStatus.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_GetMemberProperties_GDK_Net_PlayFab_EntityKey_"></a> GetMemberProperties\(EntityKey\)

A member's properties
(<code>PFLobbyGetMemberPropertyKeys</code>, <code>PFLobbyGetMemberProperty</code>).

```csharp
public IReadOnlyDictionary<string, string> GetMemberProperties(EntityKey member)
```

#### Parameters

`member` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

#### Returns

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_Leave_GDK_Net_PlayFab_EntityKey_"></a> Leave\(EntityKey?\)

Leaves the lobby (<code>PFLobbyLeave</code>). Completes with <xref href="GDK.Net.PlayFab.Multiplayer.LeaveLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId Leave(EntityKey? localUser = null)
```

#### Parameters

`localUser` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)?

The local member to remove, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to remove every local member.

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_PostUpdate_GDK_Net_PlayFab_EntityKey_GDK_Net_PlayFab_LobbyDataUpdate_GDK_Net_PlayFab_LobbyMemberDataUpdate_"></a> PostUpdate\(EntityKey, LobbyDataUpdate?, LobbyMemberDataUpdate?\)

Posts an update to the lobby's shared or member data (<code>PFLobbyPostUpdate</code>). Completes
with <xref href="GDK.Net.PlayFab.Multiplayer.PostUpdateCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId PostUpdate(EntityKey localUser, LobbyDataUpdate? lobbyUpdate = null, LobbyMemberDataUpdate? memberUpdate = null)
```

#### Parameters

`localUser` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`lobbyUpdate` [LobbyDataUpdate](../GDK.Net.PlayFab.LobbyDataUpdate.md)?

`memberUpdate` [LobbyMemberDataUpdate](../GDK.Net.PlayFab.LobbyMemberDataUpdate.md)?

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_SendInvite_GDK_Net_PlayFab_EntityKey_GDK_Net_PlayFab_EntityKey_"></a> SendInvite\(EntityKey, EntityKey\)

Invites another entity to the lobby (<code>PFLobbySendInvite</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.SendInviteCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId SendInvite(EntityKey sender, EntityKey invitee)
```

#### Parameters

`sender` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`invitee` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ServerDeleteLobby"></a> ServerDeleteLobby\(\)

Deletes the lobby (<code>PFLobbyServerDeleteLobby</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.ServerDeleteLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId ServerDeleteLobby()
```

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ServerLeaveAsServer"></a> ServerLeaveAsServer\(\)

Removes the game server from the lobby (<code>PFLobbyServerLeaveAsServer</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.ServerLeaveLobbyAsServerCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId ServerLeaveAsServer()
```

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ServerPostUpdate_GDK_Net_PlayFab_LobbyDataUpdate_"></a> ServerPostUpdate\(LobbyDataUpdate\)

Posts a lobby update on behalf of the game server (<code>PFLobbyServerPostUpdate</code>).
Completes with <xref href="GDK.Net.PlayFab.Multiplayer.ServerPostUpdateCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId ServerPostUpdate(LobbyDataUpdate lobbyUpdate)
```

#### Parameters

`lobbyUpdate` [LobbyDataUpdate](../GDK.Net.PlayFab.LobbyDataUpdate.md)

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_Lobby_ServerPostUpdateAsServer_GDK_Net_PlayFab_LobbyServerDataUpdate_"></a> ServerPostUpdateAsServer\(LobbyServerDataUpdate\)

Posts a server-data update (<code>PFLobbyServerPostUpdateAsServer</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.ServerPostUpdateAsServerCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId ServerPostUpdateAsServer(LobbyServerDataUpdate serverUpdate)
```

#### Parameters

`serverUpdate` [LobbyServerDataUpdate](../GDK.Net.PlayFab.LobbyServerDataUpdate.md)

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

