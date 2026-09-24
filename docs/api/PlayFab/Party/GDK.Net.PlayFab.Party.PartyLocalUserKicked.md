# <a id="GDK_Net_PlayFab_Party_PartyLocalUserKicked"></a> Class PartyLocalUserKicked

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A local user was kicked from the network by its owner.

```csharp
public sealed record PartyLocalUserKicked : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyLocalUserKicked>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyLocalUserKicked](GDK.Net.PlayFab.Party.PartyLocalUserKicked.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyLocalUserKicked\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserKicked__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyLocalUser_"></a> PartyLocalUserKicked\(PartyNetwork?, PartyLocalUser?\)

A local user was kicked from the network by its owner.

```csharp
public PartyLocalUserKicked(PartyNetwork? Network, PartyLocalUser? LocalUser)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user that was kicked.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserKicked_LocalUser"></a> LocalUser

The user that was kicked.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyLocalUserKicked_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

