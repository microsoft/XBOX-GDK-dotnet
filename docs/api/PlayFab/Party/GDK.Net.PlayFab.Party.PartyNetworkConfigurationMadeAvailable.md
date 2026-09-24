# <a id="GDK_Net_PlayFab_Party_PartyNetworkConfigurationMadeAvailable"></a> Class PartyNetworkConfigurationMadeAvailable

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The network's configuration became readable.

```csharp
public sealed record PartyNetworkConfigurationMadeAvailable : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyNetworkConfigurationMadeAvailable>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyNetworkConfigurationMadeAvailable](GDK.Net.PlayFab.Party.PartyNetworkConfigurationMadeAvailable.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyNetworkConfigurationMadeAvailable\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfigurationMadeAvailable__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyNetworkConfiguration_"></a> PartyNetworkConfigurationMadeAvailable\(PartyNetwork?, PartyNetworkConfiguration\)

The network's configuration became readable.

```csharp
public PartyNetworkConfigurationMadeAvailable(PartyNetwork? Network, PartyNetworkConfiguration Configuration)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`Configuration` [PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

The configuration.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfigurationMadeAvailable_Configuration"></a> Configuration

The configuration.

```csharp
public PartyNetworkConfiguration Configuration { get; init; }
```

#### Property Value

 [PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfigurationMadeAvailable_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

