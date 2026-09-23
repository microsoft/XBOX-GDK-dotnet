# <a id="GDK_Net_PlayFab_Multiplayer_LobbyMemberRemoved"></a> Class LobbyMemberRemoved

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A member left or was removed from the lobby.

```csharp
public sealed record LobbyMemberRemoved : LobbyStateChange, IEquatable<LobbyStateChange>, IEquatable<LobbyMemberRemoved>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyMemberRemoved](GDK.Net.PlayFab.Multiplayer.LobbyMemberRemoved.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyMemberRemoved\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyMemberRemoved_Member"></a> Member

The member that left.

```csharp
public EntityKey Member { get; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyMemberRemoved_Reason"></a> Reason

Why the member left.

```csharp
public LobbyMemberRemovedReason Reason { get; }
```

#### Property Value

 [LobbyMemberRemovedReason](GDK.Net.PlayFab.LobbyMemberRemovedReason.md)

