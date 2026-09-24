# <a id="GDK_Net_PlayFab_Multiplayer_LobbyDisconnecting"></a> Class LobbyDisconnecting

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

The client lost its connection to the lobby and is trying to recover.

```csharp
public sealed record LobbyDisconnecting : LobbyStateChange, IEquatable<LobbyStateChange>, IEquatable<LobbyDisconnecting>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyDisconnecting](GDK.Net.PlayFab.Multiplayer.LobbyDisconnecting.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyDisconnecting\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyDisconnecting_Reason"></a> Reason

Why the client is disconnecting.

```csharp
public LobbyDisconnectingReason Reason { get; }
```

#### Property Value

 [LobbyDisconnectingReason](../GDK.Net.PlayFab.LobbyDisconnectingReason.md)

