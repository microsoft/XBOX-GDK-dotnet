# <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptorChanged"></a> Class PartyNetworkDescriptorChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The network's descriptor changed and must be redistributed to joiners.

```csharp
public sealed record PartyNetworkDescriptorChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyNetworkDescriptorChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyNetworkDescriptorChanged](GDK.Net.PlayFab.Party.PartyNetworkDescriptorChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyNetworkDescriptorChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptorChanged__ctor_GDK_Net_PlayFab_Party_PartyNetwork_"></a> PartyNetworkDescriptorChanged\(PartyNetwork?\)

The network's descriptor changed and must be redistributed to joiners.

```csharp
public PartyNetworkDescriptorChanged(PartyNetwork? Network)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptorChanged_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

