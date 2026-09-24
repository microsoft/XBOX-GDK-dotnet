# <a id="GDK_Net_PlayFab_Party_PartyEndpointCreated"></a> Class PartyEndpointCreated

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

An endpoint appeared in the network.

```csharp
public sealed record PartyEndpointCreated : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyEndpointCreated>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyEndpointCreated](GDK.Net.PlayFab.Party.PartyEndpointCreated.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyEndpointCreated\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyEndpointCreated__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyEndpoint_"></a> PartyEndpointCreated\(PartyNetwork?, PartyEndpoint?\)

An endpoint appeared in the network.

```csharp
public PartyEndpointCreated(PartyNetwork? Network, PartyEndpoint? Endpoint)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`Endpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The new endpoint.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyEndpointCreated_Endpoint"></a> Endpoint

The new endpoint.

```csharp
public PartyEndpoint? Endpoint { get; init; }
```

#### Property Value

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpointCreated_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

