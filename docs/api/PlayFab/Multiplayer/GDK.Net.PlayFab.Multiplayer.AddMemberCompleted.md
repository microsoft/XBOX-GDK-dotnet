# <a id="GDK_Net_PlayFab_Multiplayer_AddMemberCompleted"></a> Class AddMemberCompleted

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Multiplayer.Lobby.AddMember(GDK.Net.PlayFab.EntityKey%2cSystem.Collections.Generic.IReadOnlyDictionary%7bSystem.String%2cSystem.String%7d)" data-throw-if-not-resolved="false"></xref> operation finished.

```csharp
public sealed record AddMemberCompleted : LobbyOperationCompleted, IEquatable<LobbyStateChange>, IEquatable<LobbyOperationCompleted>, IEquatable<AddMemberCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyStateChange](GDK.Net.PlayFab.Multiplayer.LobbyStateChange.md) ← 
[LobbyOperationCompleted](GDK.Net.PlayFab.Multiplayer.LobbyOperationCompleted.md) ← 
[AddMemberCompleted](GDK.Net.PlayFab.Multiplayer.AddMemberCompleted.md)

#### Implements

[IEquatable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<LobbyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<AddMemberCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Multiplayer_AddMemberCompleted_LocalUser"></a> LocalUser

The local user that was added.

```csharp
public EntityKey LocalUser { get; }
```

#### Property Value

 [EntityKey](../GDK.Net.PlayFab.EntityKey.md)

