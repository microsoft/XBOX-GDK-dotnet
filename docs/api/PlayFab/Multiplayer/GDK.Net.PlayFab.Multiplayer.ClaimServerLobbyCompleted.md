# <a id="GDK_Net_PlayFab_Multiplayer_ClaimServerLobbyCompleted"></a> Class ClaimServerLobbyCompleted

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ClaimServerLobby(GDK.Net.PlayFab.EntityKey%2cSystem.String)" data-throw-if-not-resolved="false"></xref> operation finished.

```csharp
public sealed record ClaimServerLobbyCompleted : LobbyOperationCompleted, IEquatable<LobbyStateChange>, IEquatable<LobbyOperationCompleted>, IEquatable<ClaimServerLobbyCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md) ← 
[ClaimServerLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ClaimServerLobbyCompleted.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<ClaimServerLobbyCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_ClaimServerLobbyCompleted_LobbyId"></a> LobbyId

The id of the claimed lobby.

```csharp
public string? LobbyId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

