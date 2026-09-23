# <a id="GDK_Net_PlayFab_Multiplayer_SendInviteCompleted"></a> Class SendInviteCompleted

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.SendInvite(GDK.Net.PlayFab.EntityKey%2cGDK.Net.PlayFab.EntityKey)" data-throw-if-not-resolved="false"></xref> operation finished.

```csharp
public sealed record SendInviteCompleted : LobbyOperationCompleted, IEquatable<LobbyStateChange>, IEquatable<LobbyOperationCompleted>, IEquatable<SendInviteCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md) ← 
[SendInviteCompleted](GDK.Net.PlayFab.Multiplayer.SendInviteCompleted.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<SendInviteCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[LobbyOperationCompleted.Operation](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyOperationCompleted\_Operation), 
[LobbyOperationCompleted.ResultCode](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyOperationCompleted\_ResultCode), 
[LobbyOperationCompleted.Failed](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyOperationCompleted\_Failed), 
[LobbyOperationCompleted.Error](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyOperationCompleted\_Error), 
[LobbyStateChange.ChangeType](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyStateChange\_ChangeType), 
[LobbyStateChange.Lobby](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyStateChange\_Lobby), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_SendInviteCompleted_Invitee"></a> Invitee

The entity the invite was sent to.

```csharp
public EntityKey Invitee { get; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_SendInviteCompleted_Sender"></a> Sender

The entity that sent the invite.

```csharp
public EntityKey Sender { get; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)

