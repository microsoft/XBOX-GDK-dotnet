# <a id="GDK_Net_PlayFab_LobbyDataUpdate"></a> Class LobbyDataUpdate

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLobbyDataUpdate</code>.

```csharp
public sealed class LobbyDataUpdate
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyDataUpdate](GDK.Net.PlayFab.LobbyDataUpdate.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_AccessPolicy"></a> AccessPolicy

<code>AccessPolicy</code>.

```csharp
public LobbyAccessPolicy? AccessPolicy { get; set; }
```

#### Property Value

 [LobbyAccessPolicy](GDK.Net.PlayFab.LobbyAccessPolicy.md)?

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_LobbyProperties"></a> LobbyProperties

The <code>LobbyPropertyKeys</code> / <code>LobbyPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? LobbyProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_MaxMemberCount"></a> MaxMemberCount

<code>MaxMemberCount</code>.

```csharp
public uint? MaxMemberCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_MembershipLock"></a> MembershipLock

<code>MembershipLock</code>.

```csharp
public LobbyMembershipLock? MembershipLock { get; set; }
```

#### Property Value

 [LobbyMembershipLock](GDK.Net.PlayFab.LobbyMembershipLock.md)?

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_NewOwner"></a> NewOwner

<code>NewOwner</code>.

```csharp
public EntityKey? NewOwner { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_RestrictInvitesToLobbyOwner"></a> RestrictInvitesToLobbyOwner

<code>RestrictInvitesToLobbyOwner</code>.

```csharp
public bool? RestrictInvitesToLobbyOwner { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="GDK_Net_PlayFab_LobbyDataUpdate_SearchProperties"></a> SearchProperties

The <code>SearchPropertyKeys</code> / <code>SearchPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? SearchProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

