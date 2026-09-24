# <a id="GDK_Net_PlayFab_LobbyArrangedJoinConfiguration"></a> Class LobbyArrangedJoinConfiguration

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLobbyArrangedJoinConfiguration</code>.

```csharp
public sealed class LobbyArrangedJoinConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyArrangedJoinConfiguration](GDK.Net.PlayFab.LobbyArrangedJoinConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_AccessPolicy"></a> AccessPolicy

<code>AccessPolicy</code>.

```csharp
public LobbyAccessPolicy AccessPolicy { get; set; }
```

#### Property Value

 [LobbyAccessPolicy](GDK.Net.PlayFab.LobbyAccessPolicy.md)

### <a id="GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_MaxMemberCount"></a> MaxMemberCount

<code>MaxMemberCount</code>.

```csharp
public uint MaxMemberCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_MemberProperties"></a> MemberProperties

The <code>MemberPropertyKeys</code> / <code>MemberPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? MemberProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_OwnerMigrationPolicy"></a> OwnerMigrationPolicy

<code>OwnerMigrationPolicy</code>.

```csharp
public LobbyOwnerMigrationPolicy OwnerMigrationPolicy { get; set; }
```

#### Property Value

 [LobbyOwnerMigrationPolicy](GDK.Net.PlayFab.LobbyOwnerMigrationPolicy.md)

### <a id="GDK_Net_PlayFab_LobbyArrangedJoinConfiguration_RestrictInvitesToLobbyOwner"></a> RestrictInvitesToLobbyOwner

<code>RestrictInvitesToLobbyOwner</code>.

```csharp
public bool RestrictInvitesToLobbyOwner { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

