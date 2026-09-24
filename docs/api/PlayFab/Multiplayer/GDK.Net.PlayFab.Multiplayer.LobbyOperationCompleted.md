# <a id="GDK_Net_PlayFab_Multiplayer_LobbyOperationCompleted"></a> Class LobbyOperationCompleted

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

Base class for the changes that complete an operation started earlier on the same manager.

```csharp
public abstract record LobbyOperationCompleted : LobbyStateChange, IEquatable<LobbyStateChange>, IEquatable<LobbyOperationCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md)

#### Derived

[AddMemberCompleted](GDK.Net.PlayFab.Multiplayer.AddMemberCompleted.md), 
[ClaimServerLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ClaimServerLobbyCompleted.md), 
[ConnectToLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ConnectToLobbyCompleted.md), 
[CreateAndClaimServerLobbyCompleted](GDK.Net.PlayFab.Multiplayer.CreateAndClaimServerLobbyCompleted.md), 
[CreateAndJoinLobbyCompleted](GDK.Net.PlayFab.Multiplayer.CreateAndJoinLobbyCompleted.md), 
[FindLobbiesCompleted](GDK.Net.PlayFab.Multiplayer.FindLobbiesCompleted.md), 
[ForceRemoveMemberCompleted](GDK.Net.PlayFab.Multiplayer.ForceRemoveMemberCompleted.md), 
[JoinArrangedLobbyCompleted](GDK.Net.PlayFab.Multiplayer.JoinArrangedLobbyCompleted.md), 
[JoinLobbyAsServerCompleted](GDK.Net.PlayFab.Multiplayer.JoinLobbyAsServerCompleted.md), 
[JoinLobbyCompleted](GDK.Net.PlayFab.Multiplayer.JoinLobbyCompleted.md), 
[LeaveLobbyCompleted](GDK.Net.PlayFab.Multiplayer.LeaveLobbyCompleted.md), 
[PostUpdateCompleted](GDK.Net.PlayFab.Multiplayer.PostUpdateCompleted.md), 
[SendInviteCompleted](GDK.Net.PlayFab.Multiplayer.SendInviteCompleted.md), 
[ServerDeleteLobbyCompleted](GDK.Net.PlayFab.Multiplayer.ServerDeleteLobbyCompleted.md), 
[ServerLeaveLobbyAsServerCompleted](GDK.Net.PlayFab.Multiplayer.ServerLeaveLobbyAsServerCompleted.md), 
[ServerPostUpdateAsServerCompleted](GDK.Net.PlayFab.Multiplayer.ServerPostUpdateAsServerCompleted.md), 
[ServerPostUpdateCompleted](GDK.Net.PlayFab.Multiplayer.ServerPostUpdateCompleted.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[LobbyStateChange.ChangeType](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyStateChange\_ChangeType), 
[LobbyStateChange.Lobby](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_LobbyStateChange\_Lobby), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyOperationCompleted_Error"></a> Error

The failure, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the operation succeeded. The exception is
created on demand, so a successful pump allocates nothing.

```csharp
public Exception? Error { get; }
```

#### Property Value

 [Exception](https://learn.microsoft.com/dotnet/api/system.exception)?

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyOperationCompleted_Failed"></a> Failed

Whether the operation failed.

```csharp
public bool Failed { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyOperationCompleted_Operation"></a> Operation

The id returned when the title started this operation.

```csharp
public OperationId Operation { get; }
```

#### Property Value

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyOperationCompleted_ResultCode"></a> ResultCode

The raw native HRESULT.

```csharp
public int ResultCode { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

