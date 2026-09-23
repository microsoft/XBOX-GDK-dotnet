# <a id="GDK_Net_PlayFab_Party_PartyLocalUserRemoved"></a> Class PartyLocalUserRemoved

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A local user left the network.

```csharp
public sealed record PartyLocalUserRemoved : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyLocalUserRemoved>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyLocalUserRemoved](GDK.Net.PlayFab.Party.PartyLocalUserRemoved.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyLocalUserRemoved\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserRemoved__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyLocalUserRemovedReason_"></a> PartyLocalUserRemoved\(PartyNetwork?, PartyLocalUser?, PartyLocalUserRemovedReason\)

A local user left the network.

```csharp
public PartyLocalUserRemoved(PartyNetwork? Network, PartyLocalUser? LocalUser, PartyLocalUserRemovedReason Reason)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user that left.

`Reason` [PartyLocalUserRemovedReason](GDK.Net.PlayFab.Party.PartyLocalUserRemovedReason.md)

Why the user left.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserRemoved_LocalUser"></a> LocalUser

The user that left.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserRemoved_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserRemoved_Reason"></a> Reason

Why the user left.

```csharp
public PartyLocalUserRemovedReason Reason { get; init; }
```

#### Property Value

 [PartyLocalUserRemovedReason](GDK.Net.PlayFab.Party.PartyLocalUserRemovedReason.md)

