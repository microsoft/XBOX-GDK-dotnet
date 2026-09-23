# <a id="GDK_Net_PlayFab_Multiplayer"></a> Namespace GDK.Net.PlayFab.Multiplayer

### Classes

 [AddMemberCompleted](GDK.Net.PlayFab.Multiplayer.AddMemberCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.AddMember(GDK.Net.PlayFab.EntityKey%2cSystem.Collections.Generic.IReadOnlyDictionary%7bSystem.String%2cSystem.String%7d)" data-throw-if-not-resolved="false"></xref> operation finished.

 [ClaimServerLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ClaimServerLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ClaimServerLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String)" data-throw-if-not-resolved="false"></xref> operation finished.

 [ConnectToLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ConnectToLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ConnectToLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String)" data-throw-if-not-resolved="false"></xref> operation finished.

 [CreateAndClaimServerLobbyCompleted](GDK.Net.PlayFab.Multiplayer.CreateAndClaimServerLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.CreateAndClaimServerLobby(GDK.Net.PlayFab.EntityKey%2cGDK.Net.PlayFab.LobbyCreateConfiguration)" data-throw-if-not-resolved="false"></xref> operation finished.

 [CreateAndJoinLobbyCompleted](GDK.Net.PlayFab.Multiplayer.CreateAndJoinLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.CreateAndJoinLobby(GDK.Net.PlayFab.EntityKey%2cGDK.Net.PlayFab.LobbyCreateConfiguration%2cGDK.Net.PlayFab.LobbyJoinConfiguration)" data-throw-if-not-resolved="false"></xref> operation finished.

 [FindLobbiesCompleted](GDK.Net.PlayFab.Multiplayer.FindLobbiesCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.FindLobbies(GDK.Net.PlayFab.EntityKey%2cGDK.Net.PlayFab.LobbySearchConfiguration)" data-throw-if-not-resolved="false"></xref> operation finished.

 [ForceRemoveMemberCompleted](GDK.Net.PlayFab.Multiplayer.ForceRemoveMemberCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.ForceRemoveMember(GDK.Net.PlayFab.EntityKey%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref> operation finished.

 [JoinArrangedLobbyCompleted](GDK.Net.PlayFab.Multiplayer.JoinArrangedLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.JoinArrangedLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String%2cGDK.Net.PlayFab.LobbyArrangedJoinConfiguration)" data-throw-if-not-resolved="false"></xref> operation finished.

 [JoinLobbyAsServerCompleted](GDK.Net.PlayFab.Multiplayer.JoinLobbyAsServerCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.JoinLobbyAsServer(GDK.Net.PlayFab.EntityKey%2cSystem.String%2cGDK.Net.PlayFab.LobbyServerJoinConfiguration)" data-throw-if-not-resolved="false"></xref> operation finished.

 [JoinLobbyCompleted](GDK.Net.PlayFab.Multiplayer.JoinLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.JoinLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String%2cGDK.Net.PlayFab.LobbyJoinConfiguration)" data-throw-if-not-resolved="false"></xref> operation finished.

 [LeaveLobbyCompleted](GDK.Net.PlayFab.Multiplayer.LeaveLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.Leave(GDK.Net.PlayFab.EntityKey)" data-throw-if-not-resolved="false"></xref> operation finished; the lobby is now invalid.

 [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md)

A PlayFab lobby (<code>PFLobbyHandle</code>). Instances are identity-mapped by their owning
<xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer" data-throw-if-not-resolved="false"></xref>, so the same native lobby always surfaces as the same object.

 [LobbyDisconnected](GDK.Net.PlayFab.Multiplayer.LobbyDisconnected.md)

The lobby was disconnected and is now invalid.

 [LobbyDisconnecting](GDK.Net.PlayFab.Multiplayer.LobbyDisconnecting.md)

The client lost its connection to the lobby and is trying to recover.

 [LobbyInviteListenerStatusChanged](GDK.Net.PlayFab.Multiplayer.LobbyInviteListenerStatusChanged.md)

An invite listener's status changed.

 [LobbyInviteReceived](GDK.Net.PlayFab.Multiplayer.LobbyInviteReceived.md)

An invite to a lobby arrived for an entity the title is listening for.

 [LobbyMemberAdded](GDK.Net.PlayFab.Multiplayer.LobbyMemberAdded.md)

A member joined the lobby.

 [LobbyMemberRemoved](GDK.Net.PlayFab.Multiplayer.LobbyMemberRemoved.md)

A member left or was removed from the lobby.

 [LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md)

Base class for the changes that complete an operation started earlier on the same manager.

 [LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md)

One entry from the lobby state-change queue
(<code>PFLobbyStateChange</code>, drained by <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ProcessLobbyStateChanges" data-throw-if-not-resolved="false"></xref>).

 [LobbyUpdated](GDK.Net.PlayFab.Multiplayer.LobbyUpdated.md)

The lobby's shared state changed.

 [MatchmakingStateChange](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChange.md)

One entry from the matchmaking state-change queue (<code>PFMatchmakingStateChange</code>, drained by
<xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ProcessMatchmakingStateChanges" data-throw-if-not-resolved="false"></xref>).

 [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md)

A matchmaking ticket (<code>PFMatchmakingTicketHandle</code>). Instances are identity-mapped by their
owning <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer" data-throw-if-not-resolved="false"></xref>.

 [MatchmakingTicketCompleted](GDK.Net.PlayFab.Multiplayer.MatchmakingTicketCompleted.md)

The ticket reached a terminal state, with a match or a failure.

 [MatchmakingTicketStatusChanged](GDK.Net.PlayFab.Multiplayer.MatchmakingTicketStatusChanged.md)

The ticket moved to a new status; read it from <xref href="GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.Status" data-throw-if-not-resolved="false"></xref>.

 [PlayFabMultiplayer](GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.md)

The PlayFab Multiplayer library (<code>PFMultiplayer.h</code>, <code>PFLobby.h</code>,
<code>PFMatchmaking.h</code>): lobbies and matchmaking, driven by a per-frame state-change pump.

 [PostUpdateCompleted](GDK.Net.PlayFab.Multiplayer.PostUpdateCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.PostUpdate(GDK.Net.PlayFab.EntityKey%2cGDK.Net.PlayFab.LobbyDataUpdate%2cGDK.Net.PlayFab.LobbyMemberDataUpdate)" data-throw-if-not-resolved="false"></xref> operation finished.

 [SendInviteCompleted](GDK.Net.PlayFab.Multiplayer.SendInviteCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.SendInvite(GDK.Net.PlayFab.EntityKey%2cGDK.Net.PlayFab.EntityKey)" data-throw-if-not-resolved="false"></xref> operation finished.

 [ServerDeleteLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ServerDeleteLobbyCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.ServerDeleteLobby" data-throw-if-not-resolved="false"></xref> operation finished; the lobby is now invalid.

 [ServerLeaveLobbyAsServerCompleted](GDK.Net.PlayFab.Multiplayer.ServerLeaveLobbyAsServerCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.ServerLeaveAsServer" data-throw-if-not-resolved="false"></xref> operation finished; the lobby is now invalid.

 [ServerPostUpdateAsServerCompleted](GDK.Net.PlayFab.Multiplayer.ServerPostUpdateAsServerCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.ServerPostUpdateAsServer(GDK.Net.PlayFab.LobbyServerDataUpdate)" data-throw-if-not-resolved="false"></xref> operation finished.

 [ServerPostUpdateCompleted](GDK.Net.PlayFab.Multiplayer.ServerPostUpdateCompleted.md)

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.ServerPostUpdate(GDK.Net.PlayFab.LobbyDataUpdate)" data-throw-if-not-resolved="false"></xref> operation finished.

### Structs

 [MatchmakingStateChangeCollection.Enumerator](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChangeCollection.Enumerator.md)

Walks one batch of matchmaking state changes.

 [LobbyStateChangeCollection.Enumerator](GDK.Net.PlayFab.Multiplayer.LobbyStateChangeCollection.Enumerator.md)

Walks one batch of lobby state changes and returns it on <xref href="GDK.Net.PlayFab.Multiplayer.LobbyStateChangeCollection.Enumerator.Dispose" data-throw-if-not-resolved="false"></xref>.

 [LobbyStateChangeCollection](GDK.Net.PlayFab.Multiplayer.LobbyStateChangeCollection.md)

The lobby state changes produced by one pump. Enumerating starts the batch; leaving the loop
returns it to the multiplayer library.

 [MatchmakingStateChangeCollection](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChangeCollection.md)

The matchmaking state changes produced by one pump.

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

Correlates a multiplayer operation with the state change that completes it.

