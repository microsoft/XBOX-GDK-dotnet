# <a id="GDK_Net_PlayFab_Party_PartyXblRequiredChatPermissionInfoChanged"></a> Class PartyXblRequiredChatPermissionInfoChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The chat permissions between two users changed. Query the new value with
<xref href="GDK.Net.PlayFab.Party.PartyXblChatUser.GetRequiredChatPermissionInfo(GDK.Net.PlayFab.Party.PartyXblChatUser)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed record PartyXblRequiredChatPermissionInfoChanged : PartyXblStateChange, IEquatable<PartyXblStateChange>, IEquatable<PartyXblRequiredChatPermissionInfoChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblRequiredChatPermissionInfoChanged](GDK.Net.PlayFab.Party.PartyXblRequiredChatPermissionInfoChanged.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblRequiredChatPermissionInfoChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyXblStateChange.Kind](GDK.Net.PlayFab.Party.PartyXblStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyXblStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblRequiredChatPermissionInfoChanged__ctor_GDK_Net_PlayFab_Party_PartyXblChatUser_GDK_Net_PlayFab_Party_PartyXblChatUser_"></a> PartyXblRequiredChatPermissionInfoChanged\(PartyXblChatUser?, PartyXblChatUser?\)

The chat permissions between two users changed. Query the new value with
<xref href="GDK.Net.PlayFab.Party.PartyXblChatUser.GetRequiredChatPermissionInfo(GDK.Net.PlayFab.Party.PartyXblChatUser)" data-throw-if-not-resolved="false"></xref>.

```csharp
public PartyXblRequiredChatPermissionInfoChanged(PartyXblChatUser? LocalChatUser, PartyXblChatUser? TargetChatUser)
```

#### Parameters

`LocalChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The local user whose permissions changed.

`TargetChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The user the permissions apply to.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblRequiredChatPermissionInfoChanged_LocalChatUser"></a> LocalChatUser

The local user whose permissions changed.

```csharp
public PartyXblChatUser? LocalChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyXblRequiredChatPermissionInfoChanged_TargetChatUser"></a> TargetChatUser

The user the permissions apply to.

```csharp
public PartyXblChatUser? TargetChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

