# <a id="GDK_Net_PlayFab_Multiplayer_ConnectToLobbyCompleted"></a> Class ConnectToLobbyCompleted

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ConnectToLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String)" data-throw-if-not-resolved="false"></xref> operation finished.

```csharp
public sealed record ConnectToLobbyCompleted : LobbyOperationCompleted, IEquatable<LobbyStateChange>, IEquatable<LobbyOperationCompleted>, IEquatable<ConnectToLobbyCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md) ← 
[ConnectToLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ConnectToLobbyCompleted.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<ConnectToLobbyCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_ConnectToLobbyCompleted_LobbyId"></a> LobbyId

The id of the lobby that was connected to.

```csharp
public string? LobbyId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Multiplayer_ConnectToLobbyCompleted_NewMember"></a> NewMember

The entity that connected.

```csharp
public EntityKey NewMember { get; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)

