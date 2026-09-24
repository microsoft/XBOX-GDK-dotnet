# <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer"></a> Class PlayFabMultiplayer

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

The PlayFab Multiplayer library (<code>PFMultiplayer.h</code>, <code>PFLobby.h</code>,
<code>PFMatchmaking.h</code>): lobbies and matchmaking, driven by a per-frame state-change pump.

```csharp
public sealed class PlayFabMultiplayer : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabMultiplayer](GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Unlike the PlayFab Services APIs, PFMP operations are not awaitable. A start call returns
synchronously with an <xref href="GDK.Net.PlayFab.Multiplayer.OperationId" data-throw-if-not-resolved="false"></xref>, and its completion arrives later as a record
from <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ProcessLobbyStateChanges" data-throw-if-not-resolved="false"></xref> or <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ProcessMatchmakingStateChanges" data-throw-if-not-resolved="false"></xref>.
Both loops must be pumped once per frame from the title's update thread.
</p>
<p>
Native state-change memory is only valid between the library's <code>StartProcessing</code> and
<code>FinishProcessing</code> calls, which the enumerator brackets: <code>FinishProcessing</code> runs when
the <code>foreach</code> leaves scope. Each record snapshots the values it exposes, so a record may
safely outlive the loop; <xref href="GDK.Net.PlayFab.Multiplayer.Lobby" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.PlayFab.Multiplayer.MatchmakingTicket" data-throw-if-not-resolved="false"></xref> references stay
valid until their teardown change arrives.
</p>

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_ClaimServerLobby_GDK_Net_PlayFab_EntityKey_System_String_"></a> ClaimServerLobby\(EntityKey, string\)

Reclaims a server-owned lobby (<code>PFMultiplayerClaimServerLobby</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.ClaimServerLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) ClaimServerLobby(EntityKey server, string lobbyId)
```

#### Parameters

`server` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`lobbyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_ClaimServerLobby_GDK_Net_PlayFab_PlayFabEntity_System_String_"></a> ClaimServerLobby\(PlayFabEntity, string\)

Reclaims a server-owned lobby for an entity the SDK already owns
(<code>PFMultiplayerClaimServerLobbyWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) ClaimServerLobby(PlayFabEntity server, string lobbyId)
```

#### Parameters

`server` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`lobbyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_ConnectToLobby_GDK_Net_PlayFab_EntityKey_System_String_"></a> ConnectToLobby\(EntityKey, string\)

Reconnects to a lobby the entity is already a member of
(<code>PFMultiplayerConnectToLobby</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.ConnectToLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) ConnectToLobby(EntityKey newMember, string lobbyId)
```

#### Parameters

`newMember` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`lobbyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_ConnectToLobby_GDK_Net_PlayFab_PlayFabEntity_System_String_"></a> ConnectToLobby\(PlayFabEntity, string\)

Reconnects to a lobby as an entity the SDK already owns
(<code>PFMultiplayerConnectToLobbyWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) ConnectToLobby(PlayFabEntity newMember, string lobbyId)
```

#### Parameters

`newMember` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`lobbyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateAndClaimServerLobby_GDK_Net_PlayFab_EntityKey_GDK_Net_PlayFab_LobbyCreateConfiguration_"></a> CreateAndClaimServerLobby\(EntityKey, LobbyCreateConfiguration\)

Creates a lobby owned by a game server (<code>PFMultiplayerCreateAndClaimServerLobby</code>).
Completes with <xref href="GDK.Net.PlayFab.Multiplayer.CreateAndClaimServerLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) CreateAndClaimServerLobby(EntityKey server, LobbyCreateConfiguration createConfiguration)
```

#### Parameters

`server` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`createConfiguration` [LobbyCreateConfiguration](../GDK.Net.PlayFab.LobbyCreateConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateAndClaimServerLobby_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LobbyCreateConfiguration_"></a> CreateAndClaimServerLobby\(PlayFabEntity, LobbyCreateConfiguration\)

Creates a server-owned lobby for an entity the SDK already owns
(<code>PFMultiplayerCreateAndClaimServerLobbyWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) CreateAndClaimServerLobby(PlayFabEntity server, LobbyCreateConfiguration createConfiguration)
```

#### Parameters

`server` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`createConfiguration` [LobbyCreateConfiguration](../GDK.Net.PlayFab.LobbyCreateConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateAndJoinLobby_GDK_Net_PlayFab_EntityKey_GDK_Net_PlayFab_LobbyCreateConfiguration_GDK_Net_PlayFab_LobbyJoinConfiguration_"></a> CreateAndJoinLobby\(EntityKey, LobbyCreateConfiguration, LobbyJoinConfiguration?\)

Creates a lobby and joins it (<code>PFMultiplayerCreateAndJoinLobby</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.CreateAndJoinLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) CreateAndJoinLobby(EntityKey creator, LobbyCreateConfiguration createConfiguration, LobbyJoinConfiguration? joinConfiguration = null)
```

#### Parameters

`creator` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`createConfiguration` [LobbyCreateConfiguration](../GDK.Net.PlayFab.LobbyCreateConfiguration.md)

`joinConfiguration` [LobbyJoinConfiguration](../GDK.Net.PlayFab.LobbyJoinConfiguration.md)?

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateAndJoinLobby_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LobbyCreateConfiguration_GDK_Net_PlayFab_LobbyJoinConfiguration_"></a> CreateAndJoinLobby\(PlayFabEntity, LobbyCreateConfiguration, LobbyJoinConfiguration?\)

Creates a lobby and joins it as an entity the SDK already owns
(<code>PFMultiplayerCreateAndJoinLobbyWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) CreateAndJoinLobby(PlayFabEntity creator, LobbyCreateConfiguration createConfiguration, LobbyJoinConfiguration? joinConfiguration = null)
```

#### Parameters

`creator` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`createConfiguration` [LobbyCreateConfiguration](../GDK.Net.PlayFab.LobbyCreateConfiguration.md)

`joinConfiguration` [LobbyJoinConfiguration](../GDK.Net.PlayFab.LobbyJoinConfiguration.md)?

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateMatchmakingTicket_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_EntityKey__System_Collections_Generic_IReadOnlyList_System_String__GDK_Net_PlayFab_MatchmakingTicketConfiguration_"></a> CreateMatchmakingTicket\(IReadOnlyList<EntityKey\>, IReadOnlyList<string\>, MatchmakingTicketConfiguration\)

Creates a matchmaking ticket (<code>PFMultiplayerCreateMatchmakingTicket</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.MatchmakingTicketCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, MatchmakingTicket Ticket) CreateMatchmakingTicket(IReadOnlyList<EntityKey> localUsers, IReadOnlyList<string> localUserAttributes, MatchmakingTicketConfiguration configuration)
```

#### Parameters

`localUsers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[EntityKey](../GDK.Net.PlayFab.EntityKey.md)\>

The local entities to match with.

`localUserAttributes` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

One JSON attribute document per local user, in the same order.

`configuration` [MatchmakingTicketConfiguration](../GDK.Net.PlayFab.MatchmakingTicketConfiguration.md)

The queue and timeout to match under.

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md) Ticket\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateMatchmakingTicket_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_PlayFabEntity__System_Collections_Generic_IReadOnlyList_System_String__GDK_Net_PlayFab_MatchmakingTicketConfiguration_"></a> CreateMatchmakingTicket\(IReadOnlyList<PlayFabEntity\>, IReadOnlyList<string\>, MatchmakingTicketConfiguration\)

Creates a matchmaking ticket for entities the SDK already owns
(<code>PFMultiplayerCreateMatchmakingTicketWithEntityHandles</code>).

```csharp
public (OperationId Operation, MatchmakingTicket Ticket) CreateMatchmakingTicket(IReadOnlyList<PlayFabEntity> localUsers, IReadOnlyList<string> localUserAttributes, MatchmakingTicketConfiguration configuration)
```

#### Parameters

`localUsers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)\>

`localUserAttributes` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

`configuration` [MatchmakingTicketConfiguration](../GDK.Net.PlayFab.MatchmakingTicketConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md) Ticket\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateServerBackfillTicket_GDK_Net_PlayFab_EntityKey_GDK_Net_PlayFab_MatchmakingServerBackfillTicketConfiguration_"></a> CreateServerBackfillTicket\(EntityKey, MatchmakingServerBackfillTicketConfiguration\)

Creates a backfill ticket for a running match
(<code>PFMultiplayerCreateServerBackfillTicket</code>).

```csharp
public (OperationId Operation, MatchmakingTicket Ticket) CreateServerBackfillTicket(EntityKey server, MatchmakingServerBackfillTicketConfiguration configuration)
```

#### Parameters

`server` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`configuration` [MatchmakingServerBackfillTicketConfiguration](../GDK.Net.PlayFab.MatchmakingServerBackfillTicketConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md) Ticket\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_CreateServerBackfillTicket_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_MatchmakingServerBackfillTicketConfiguration_"></a> CreateServerBackfillTicket\(PlayFabEntity, MatchmakingServerBackfillTicketConfiguration\)

Creates a backfill ticket for a server entity the SDK already owns
(<code>PFMultiplayerCreateServerBackfillTicketWithEntityHandle</code>).

```csharp
public (OperationId Operation, MatchmakingTicket Ticket) CreateServerBackfillTicket(PlayFabEntity server, MatchmakingServerBackfillTicketConfiguration configuration)
```

#### Parameters

`server` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`configuration` [MatchmakingServerBackfillTicketConfiguration](../GDK.Net.PlayFab.MatchmakingServerBackfillTicketConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md) Ticket\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_Dispose"></a> Dispose\(\)

Shuts the multiplayer library down (<code>PFMultiplayerUninitialize</code>), invalidating every
lobby and ticket it produced.

```csharp
public void Dispose()
```

#### Remarks

<p>
<code>PFMultiplayerUninitialize</code> blocks until the library's internal PubSub work has
drained, and that drain is a <code>while (pending &gt; 0) Sleep(...)</code> loop with no timeout
and no failure path. If the work was never dispatchable -- which is what starting the
library before the Gaming Runtime causes -- the loop never ends and the calling thread
parks in <code>PubSubSubscriptionManager::Shutdown</code> indefinitely.
</p>
<p>
<xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.Initialize(System.String)" data-throw-if-not-resolved="false"></xref> guards against causing that, but a <code>Dispose</code> that can wedge a
process is not something to leave to a precondition, so the native call is also bounded: it
runs on a background thread and is abandoned after
<xref href="GDK.Net.RuntimeLifetime.TeardownTimeout" data-throw-if-not-resolved="false"></xref>. Either way this object is disposed on
return and the process can exit. Party is not involved: the two libraries may be
initialized and shut down in either order, overlapped or not.
</p>
<p>
Leave any joined lobbies and let the corresponding completion state changes arrive before
disposing, as <code>PFMultiplayerUninitialize</code> documents.
</p>

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_FindLobbies_GDK_Net_PlayFab_EntityKey_GDK_Net_PlayFab_LobbySearchConfiguration_"></a> FindLobbies\(EntityKey, LobbySearchConfiguration\)

Searches for joinable lobbies (<code>PFMultiplayerFindLobbies</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.FindLobbiesCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public OperationId FindLobbies(EntityKey searchingEntity, LobbySearchConfiguration searchConfiguration)
```

#### Parameters

`searchingEntity` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`searchConfiguration` [LobbySearchConfiguration](../GDK.Net.PlayFab.LobbySearchConfiguration.md)

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_FindLobbies_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_LobbySearchConfiguration_"></a> FindLobbies\(PlayFabEntity, LobbySearchConfiguration\)

Searches for joinable lobbies as an entity the SDK already owns
(<code>PFMultiplayerFindLobbiesWithEntityHandle</code>).

```csharp
public OperationId FindLobbies(PlayFabEntity searchingEntity, LobbySearchConfiguration searchConfiguration)
```

#### Parameters

`searchingEntity` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`searchConfiguration` [LobbySearchConfiguration](../GDK.Net.PlayFab.LobbySearchConfiguration.md)

#### Returns

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_GetErrorMessage_System_Int32_"></a> GetErrorMessage\(int\)

The library's description of an error code (<code>PFMultiplayerGetErrorMessage</code>).

```csharp
public static string? GetErrorMessage(int errorCode)
```

#### Parameters

`errorCode` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

#### Exceptions

 [PlatformNotSupportedException](https://learn.microsoft.com/dotnet/api/system.platformnotsupportedexception)

Always, on GDK 260404: the header marks this entry point <code>&lt;nyi /&gt;</code>. Use
<xref href="GDK.Net.PlayFab.PlayFabErrors.GetName(System.Int32)" data-throw-if-not-resolved="false"></xref> for a symbolic name instead.

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_GetLobbyInviteListenerStatus_GDK_Net_PlayFab_EntityKey_"></a> GetLobbyInviteListenerStatus\(EntityKey\)

The state of an entity's invite listener
(<code>PFMultiplayerGetLobbyInviteListenerStatus</code>).

```csharp
public LobbyInviteListenerStatus GetLobbyInviteListenerStatus(EntityKey listeningEntity)
```

#### Parameters

`listeningEntity` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

#### Returns

 [LobbyInviteListenerStatus](../GDK.Net.PlayFab.LobbyInviteListenerStatus.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_Initialize_System_String_"></a> Initialize\(string\)

Initializes the multiplayer library for a title (<code>PFMultiplayerInitialize</code>).

```csharp
public static PlayFabMultiplayer Initialize(string titleId)
```

#### Parameters

`titleId` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PlayFabMultiplayer](GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.md)

#### Remarks

<p>
Third in the fixed startup order (see <xref href="GDK.Net.SubsystemOrder" data-throw-if-not-resolved="false"></xref>), so the Gaming Runtime
must already be up. That requirement is documented on <code>PFMultiplayerInitialize</code> but not
enforced by it: starting early still returns <code>S_OK</code> and a usable-looking handle, but
the library's PubSub worker is never dispatched, so operations never complete and
<xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.Dispose" data-throw-if-not-resolved="false"></xref> then parks forever in <code>PubSubSubscriptionManager::Shutdown</code>. This
checks the runtime up front so the mistake is reported here rather than at an unrelated
teardown much later.
</p>
<p>
The Game Core networking stack also has to be up, so this waits for it. Because the stack
usually comes up within a few hundred milliseconds of process start, omitting the wait makes
the same hang a race that only shows up under load or on a cold boot -- the worst possible
shape for a title to debug. Removing this wait reproduces the hang in roughly one run in
three.
</p>

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The Gaming Runtime is not initialized.

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinArrangedLobby_GDK_Net_PlayFab_EntityKey_System_String_GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_"></a> JoinArrangedLobby\(EntityKey, string, LobbyArrangedJoinConfiguration\)

Joins the lobby described by a matchmaking arrangement string
(<code>PFMultiplayerJoinArrangedLobby</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.JoinArrangedLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) JoinArrangedLobby(EntityKey newMember, string arrangementString, LobbyArrangedJoinConfiguration configuration)
```

#### Parameters

`newMember` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`arrangementString` [string](https://learn.microsoft.com/dotnet/api/system.string)

`configuration` [LobbyArrangedJoinConfiguration](../GDK.Net.PlayFab.LobbyArrangedJoinConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinArrangedLobby_GDK_Net_PlayFab_PlayFabEntity_System_String_GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_"></a> JoinArrangedLobby\(PlayFabEntity, string, LobbyArrangedJoinConfiguration\)

Joins an arranged lobby as an entity the SDK already owns
(<code>PFMultiplayerJoinArrangedLobbyWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) JoinArrangedLobby(PlayFabEntity newMember, string arrangementString, LobbyArrangedJoinConfiguration configuration)
```

#### Parameters

`newMember` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`arrangementString` [string](https://learn.microsoft.com/dotnet/api/system.string)

`configuration` [LobbyArrangedJoinConfiguration](../GDK.Net.PlayFab.LobbyArrangedJoinConfiguration.md)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinLobby_GDK_Net_PlayFab_EntityKey_System_String_GDK_Net_PlayFab_LobbyJoinConfiguration_"></a> JoinLobby\(EntityKey, string, LobbyJoinConfiguration?\)

Joins a lobby from a connection string (<code>PFMultiplayerJoinLobby</code>). Completes with
<xref href="GDK.Net.PlayFab.Multiplayer.JoinLobbyCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) JoinLobby(EntityKey newMember, string connectionString, LobbyJoinConfiguration? configuration = null)
```

#### Parameters

`newMember` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`connectionString` [string](https://learn.microsoft.com/dotnet/api/system.string)

`configuration` [LobbyJoinConfiguration](../GDK.Net.PlayFab.LobbyJoinConfiguration.md)?

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinLobby_GDK_Net_PlayFab_PlayFabEntity_System_String_GDK_Net_PlayFab_LobbyJoinConfiguration_"></a> JoinLobby\(PlayFabEntity, string, LobbyJoinConfiguration?\)

Joins a lobby as an entity the SDK already owns
(<code>PFMultiplayerJoinLobbyWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) JoinLobby(PlayFabEntity newMember, string connectionString, LobbyJoinConfiguration? configuration = null)
```

#### Parameters

`newMember` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`connectionString` [string](https://learn.microsoft.com/dotnet/api/system.string)

`configuration` [LobbyJoinConfiguration](../GDK.Net.PlayFab.LobbyJoinConfiguration.md)?

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinLobbyAsServer_GDK_Net_PlayFab_EntityKey_System_String_GDK_Net_PlayFab_LobbyServerJoinConfiguration_"></a> JoinLobbyAsServer\(EntityKey, string, LobbyServerJoinConfiguration?\)

Joins an existing lobby as its game server (<code>PFMultiplayerJoinLobbyAsServer</code>).
Completes with <xref href="GDK.Net.PlayFab.Multiplayer.JoinLobbyAsServerCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (OperationId Operation, Lobby Lobby) JoinLobbyAsServer(EntityKey server, string connectionString, LobbyServerJoinConfiguration? configuration = null)
```

#### Parameters

`server` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`connectionString` [string](https://learn.microsoft.com/dotnet/api/system.string)

`configuration` [LobbyServerJoinConfiguration](../GDK.Net.PlayFab.LobbyServerJoinConfiguration.md)?

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinLobbyAsServer_GDK_Net_PlayFab_PlayFabEntity_System_String_GDK_Net_PlayFab_LobbyServerJoinConfiguration_"></a> JoinLobbyAsServer\(PlayFabEntity, string, LobbyServerJoinConfiguration?\)

Joins a lobby as a game server entity the SDK already owns
(<code>PFMultiplayerJoinLobbyAsServerWithEntityHandle</code>).

```csharp
public (OperationId Operation, Lobby Lobby) JoinLobbyAsServer(PlayFabEntity server, string connectionString, LobbyServerJoinConfiguration? configuration = null)
```

#### Parameters

`server` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

`connectionString` [string](https://learn.microsoft.com/dotnet/api/system.string)

`configuration` [LobbyServerJoinConfiguration](../GDK.Net.PlayFab.LobbyServerJoinConfiguration.md)?

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md) Lobby\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinMatchmakingTicketFromId_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_EntityKey__System_Collections_Generic_IReadOnlyList_System_String__System_String_System_String_"></a> JoinMatchmakingTicketFromId\(IReadOnlyList<EntityKey\>, IReadOnlyList<string\>, string, string\)

Joins an existing matchmaking ticket by id
(<code>PFMultiplayerJoinMatchmakingTicketFromId</code>).

```csharp
public (OperationId Operation, MatchmakingTicket Ticket) JoinMatchmakingTicketFromId(IReadOnlyList<EntityKey> localUsers, IReadOnlyList<string> localUserAttributes, string ticketId, string queueName)
```

#### Parameters

`localUsers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[EntityKey](../GDK.Net.PlayFab.EntityKey.md)\>

`localUserAttributes` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

`ticketId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`queueName` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md) Ticket\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_JoinMatchmakingTicketFromId_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_PlayFabEntity__System_Collections_Generic_IReadOnlyList_System_String__System_String_System_String_"></a> JoinMatchmakingTicketFromId\(IReadOnlyList<PlayFabEntity\>, IReadOnlyList<string\>, string, string\)

Joins an existing matchmaking ticket for entities the SDK already owns
(<code>PFMultiplayerJoinMatchmakingTicketFromIdWithEntityHandles</code>).

```csharp
public (OperationId Operation, MatchmakingTicket Ticket) JoinMatchmakingTicketFromId(IReadOnlyList<PlayFabEntity> localUsers, IReadOnlyList<string> localUserAttributes, string ticketId, string queueName)
```

#### Parameters

`localUsers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)\>

`localUserAttributes` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

`ticketId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`queueName` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 \([OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md) Operation, [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md) Ticket\)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_ProcessLobbyStateChanges"></a> ProcessLobbyStateChanges\(\)

Drains the lobby state-change queue. Enumerate it once per frame; leaving the
<code>foreach</code> returns the batch to the library
(<code>PFMultiplayerStartProcessingLobbyStateChanges</code> /
<code>PFMultiplayerFinishProcessingLobbyStateChanges</code>).

```csharp
public LobbyStateChangeCollection ProcessLobbyStateChanges()
```

#### Returns

 [LobbyStateChangeCollection](GDK.Net.PlayFab.Multiplayer.LobbyStateChangeCollection.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_ProcessMatchmakingStateChanges"></a> ProcessMatchmakingStateChanges\(\)

Drains the matchmaking state-change queue. Enumerate it once per frame
(<code>PFMultiplayerStartProcessingMatchmakingStateChanges</code> /
<code>PFMultiplayerFinishProcessingMatchmakingStateChanges</code>).

```csharp
public MatchmakingStateChangeCollection ProcessMatchmakingStateChanges()
```

#### Returns

 [MatchmakingStateChangeCollection](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChangeCollection.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_SetEntityToken_GDK_Net_PlayFab_EntityKey_System_String_"></a> SetEntityToken\(EntityKey, string\)

Supplies or refreshes an entity's PlayFab token (<code>PFMultiplayerSetEntityToken</code>).

```csharp
public void SetEntityToken(EntityKey entity, string entityToken)
```

#### Parameters

`entity` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

`entityToken` [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_SetThreadAffinityMask_GDK_Net_PlayFab_MultiplayerThreadId_System_UInt64_"></a> SetThreadAffinityMask\(MultiplayerThreadId, ulong\)

Pins one of the library's internal threads to a set of cores
(<code>PFMultiplayerSetThreadAffinityMask</code>).

```csharp
public static void SetThreadAffinityMask(MultiplayerThreadId threadId, ulong affinityMask)
```

#### Parameters

`threadId` [MultiplayerThreadId](../GDK.Net.PlayFab.MultiplayerThreadId.md)

`affinityMask` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_StartListeningForLobbyInvites_GDK_Net_PlayFab_EntityKey_"></a> StartListeningForLobbyInvites\(EntityKey\)

Starts delivering lobby invites for an entity
(<code>PFMultiplayerStartListeningForLobbyInvites</code>).

```csharp
public void StartListeningForLobbyInvites(EntityKey listeningEntity)
```

#### Parameters

`listeningEntity` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_StartListeningForLobbyInvites_GDK_Net_PlayFab_PlayFabEntity_"></a> StartListeningForLobbyInvites\(PlayFabEntity\)

Starts delivering lobby invites for an entity the SDK already owns
(<code>PFMultiplayerStartListeningForLobbyInvitesWithEntityHandle</code>).

```csharp
public void StartListeningForLobbyInvites(PlayFabEntity listeningEntity)
```

#### Parameters

`listeningEntity` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_StopListeningForLobbyInvites_GDK_Net_PlayFab_EntityKey_"></a> StopListeningForLobbyInvites\(EntityKey\)

Stops delivering lobby invites for an entity
(<code>PFMultiplayerStopListeningForLobbyInvites</code>).

```csharp
public void StopListeningForLobbyInvites(EntityKey listeningEntity)
```

#### Parameters

`listeningEntity` [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_PlayFabMultiplayer_StopListeningForLobbyInvites_GDK_Net_PlayFab_PlayFabEntity_"></a> StopListeningForLobbyInvites\(PlayFabEntity\)

Stops delivering lobby invites for an entity the SDK already owns
(<code>PFMultiplayerStopListeningForLobbyInvitesWithEntityHandle</code>).

```csharp
public void StopListeningForLobbyInvites(PlayFabEntity listeningEntity)
```

#### Parameters

`listeningEntity` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

