# <a id="GDK_Net_PlayFab_LobbySearchResult"></a> Class LobbySearchResult

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLobbySearchResult</code>.

```csharp
public sealed class LobbySearchResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbySearchResult](GDK.Net.PlayFab.LobbySearchResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LobbySearchResult_ConnectionString"></a> ConnectionString

<code>ConnectionString</code>.

```csharp
public string? ConnectionString { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_LobbySearchResult_CurrentMemberCount"></a> CurrentMemberCount

<code>CurrentMemberCount</code>.

```csharp
public uint CurrentMemberCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_LobbySearchResult_Friends"></a> Friends

<code>Friends</code>.

```csharp
public IReadOnlyList<EntityKey>? Friends { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[EntityKey](GDK.Net.PlayFab.EntityKey.md)\>?

### <a id="GDK_Net_PlayFab_LobbySearchResult_LobbyId"></a> LobbyId

<code>LobbyId</code>.

```csharp
public string? LobbyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_LobbySearchResult_MaxMemberCount"></a> MaxMemberCount

<code>MaxMemberCount</code>.

```csharp
public uint MaxMemberCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_LobbySearchResult_MembershipLock"></a> MembershipLock

<code>MembershipLock</code>.

```csharp
public LobbyMembershipLock MembershipLock { get; set; }
```

#### Property Value

 [LobbyMembershipLock](GDK.Net.PlayFab.LobbyMembershipLock.md)

### <a id="GDK_Net_PlayFab_LobbySearchResult_OwnerEntity"></a> OwnerEntity

<code>OwnerEntity</code>.

```csharp
public EntityKey? OwnerEntity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_LobbySearchResult_SearchProperties"></a> SearchProperties

The <code>SearchPropertyKeys</code> / <code>SearchPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? SearchProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

