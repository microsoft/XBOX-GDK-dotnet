# <a id="GDK_Net_PlayFab_LobbyCreateConfiguration"></a> Class LobbyCreateConfiguration

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLobbyCreateConfiguration</code>.

```csharp
public sealed class LobbyCreateConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyCreateConfiguration](GDK.Net.PlayFab.LobbyCreateConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LobbyCreateConfiguration_AccessPolicy"></a> AccessPolicy

<code>AccessPolicy</code>.

```csharp
public LobbyAccessPolicy AccessPolicy { get; set; }
```

#### Property Value

 [LobbyAccessPolicy](GDK.Net.PlayFab.LobbyAccessPolicy.md)

### <a id="GDK_Net_PlayFab_LobbyCreateConfiguration_LobbyProperties"></a> LobbyProperties

The <code>LobbyPropertyKeys</code> / <code>LobbyPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? LobbyProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_LobbyCreateConfiguration_MaxMemberCount"></a> MaxMemberCount

<code>MaxMemberCount</code>.

```csharp
public uint MaxMemberCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_LobbyCreateConfiguration_OwnerMigrationPolicy"></a> OwnerMigrationPolicy

<code>OwnerMigrationPolicy</code>.

```csharp
public LobbyOwnerMigrationPolicy OwnerMigrationPolicy { get; set; }
```

#### Property Value

 [LobbyOwnerMigrationPolicy](GDK.Net.PlayFab.LobbyOwnerMigrationPolicy.md)

### <a id="GDK_Net_PlayFab_LobbyCreateConfiguration_RestrictInvitesToLobbyOwner"></a> RestrictInvitesToLobbyOwner

<code>RestrictInvitesToLobbyOwner</code>.

```csharp
public bool RestrictInvitesToLobbyOwner { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_LobbyCreateConfiguration_SearchProperties"></a> SearchProperties

The <code>SearchPropertyKeys</code> / <code>SearchPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? SearchProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

