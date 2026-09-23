# <a id="GDK_Net_PlayFab_Multiplayer_LobbyStateChange"></a> Class LobbyStateChange

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

One entry from the lobby state-change queue
(<code>PFLobbyStateChange</code>, drained by <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ProcessLobbyStateChanges" data-throw-if-not-resolved="false"></xref>).

```csharp
public abstract record LobbyStateChange : IEquatable<LobbyStateChange>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md)

#### Derived

[LobbyDisconnected](GDK.Net.PlayFab.Multiplayer.LobbyDisconnected.md), 
[LobbyDisconnecting](GDK.Net.PlayFab.Multiplayer.LobbyDisconnecting.md), 
[LobbyInviteListenerStatusChanged](GDK.Net.PlayFab.Multiplayer.LobbyInviteListenerStatusChanged.md), 
[LobbyInviteReceived](GDK.Net.PlayFab.Multiplayer.LobbyInviteReceived.md), 
[LobbyMemberAdded](GDK.Net.PlayFab.Multiplayer.LobbyMemberAdded.md), 
[LobbyMemberRemoved](GDK.Net.PlayFab.Multiplayer.LobbyMemberRemoved.md), 
[LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md), 
[LobbyUpdated](GDK.Net.PlayFab.Multiplayer.LobbyUpdated.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Native change memory is only valid between <code>StartProcessingLobbyStateChanges</code> and
<code>FinishProcessingLobbyStateChanges</code>, so every record snapshots the values it exposes while
the enumerator holds the batch. <xref href="GDK.Net.PlayFab.Multiplayer.LobbyStateChange.Lobby" data-throw-if-not-resolved="false"></xref> instances are identity-mapped, so the same
lobby always surfaces as the same object.

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyStateChange_ChangeType"></a> ChangeType

The native discriminator, mirroring <code>PFLobbyStateChange::stateChangeType</code>.

```csharp
public LobbyStateChangeType ChangeType { get; }
```

#### Property Value

 [LobbyStateChangeType](GDK.Net.PlayFab.LobbyStateChangeType.md)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyStateChange_Lobby"></a> Lobby

The lobby the change is about, when the change carries one.

```csharp
public Lobby? Lobby { get; }
```

#### Property Value

 [Lobby](GDK.Net.PlayFab.Multiplayer.Lobby.md)?

