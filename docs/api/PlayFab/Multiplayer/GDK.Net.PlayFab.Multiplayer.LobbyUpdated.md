# <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated"></a> Class LobbyUpdated

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

The lobby's shared state changed.

```csharp
public sealed record LobbyUpdated : LobbyStateChange, IEquatable<LobbyStateChange>, IEquatable<LobbyUpdated>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyUpdated](GDK.Net.PlayFab.Multiplayer.LobbyUpdated.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyUpdated\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_AccessPolicyUpdated"></a> AccessPolicyUpdated

Whether the access policy changed.

```csharp
public bool AccessPolicyUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_MaxMembersUpdated"></a> MaxMembersUpdated

Whether the maximum member count changed.

```csharp
public bool MaxMembersUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_MemberUpdates"></a> MemberUpdates

Per-member summaries of what changed.

```csharp
public IReadOnlyList<LobbyMemberUpdateSummary> MemberUpdates { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LobbyMemberUpdateSummary](../GDK.Net.PlayFab.LobbyMemberUpdateSummary.md)\>

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_MembershipLockUpdated"></a> MembershipLockUpdated

Whether the membership lock changed.

```csharp
public bool MembershipLockUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_OwnerUpdated"></a> OwnerUpdated

Whether the lobby owner changed.

```csharp
public bool OwnerUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_RestrictInvitesToLobbyOwnerUpdated"></a> RestrictInvitesToLobbyOwnerUpdated

Whether the invite restriction changed.

```csharp
public bool RestrictInvitesToLobbyOwnerUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_ServerConnectionStatusUpdated"></a> ServerConnectionStatusUpdated

Whether the server's connection status changed.

```csharp
public bool ServerConnectionStatusUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_ServerUpdated"></a> ServerUpdated

Whether the lobby's server changed.

```csharp
public bool ServerUpdated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_UpdatedLobbyPropertyKeys"></a> UpdatedLobbyPropertyKeys

The lobby properties that changed.

```csharp
public IReadOnlyList<string> UpdatedLobbyPropertyKeys { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_UpdatedSearchPropertyKeys"></a> UpdatedSearchPropertyKeys

The search properties that changed.

```csharp
public IReadOnlyList<string> UpdatedSearchPropertyKeys { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyUpdated_UpdatedServerPropertyKeys"></a> UpdatedServerPropertyKeys

The server properties that changed.

```csharp
public IReadOnlyList<string> UpdatedServerPropertyKeys { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

