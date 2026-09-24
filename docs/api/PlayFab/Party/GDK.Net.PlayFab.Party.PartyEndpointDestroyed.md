# <a id="GDK_Net_PlayFab_Party_PartyEndpointDestroyed"></a> Class PartyEndpointDestroyed

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

An endpoint left the network.

```csharp
public sealed record PartyEndpointDestroyed : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyEndpointDestroyed>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyEndpointDestroyed](GDK.Net.PlayFab.Party.PartyEndpointDestroyed.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyEndpointDestroyed\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyEndpointDestroyed__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyEndpoint_GDK_Net_PlayFab_Party_PartyDestroyedReason_System_UInt32_"></a> PartyEndpointDestroyed\(PartyNetwork?, PartyEndpoint?, PartyDestroyedReason, uint\)

An endpoint left the network.

```csharp
public PartyEndpointDestroyed(PartyNetwork? Network, PartyEndpoint? Endpoint, PartyDestroyedReason Reason, uint ErrorDetail)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`Endpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The endpoint that went away.

`Reason` [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

Why it went away.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the teardown was involuntary.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyEndpointDestroyed_Endpoint"></a> Endpoint

The endpoint that went away.

```csharp
public PartyEndpoint? Endpoint { get; init; }
```

#### Property Value

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpointDestroyed_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the teardown was involuntary.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyEndpointDestroyed_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpointDestroyed_Reason"></a> Reason

Why it went away.

```csharp
public PartyDestroyedReason Reason { get; init; }
```

#### Property Value

 [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

