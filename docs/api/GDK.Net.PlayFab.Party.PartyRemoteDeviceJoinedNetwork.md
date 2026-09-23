# <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceJoinedNetwork"></a> Class PartyRemoteDeviceJoinedNetwork

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A remote device joined a network.

```csharp
public sealed record PartyRemoteDeviceJoinedNetwork : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyRemoteDeviceJoinedNetwork>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyRemoteDeviceJoinedNetwork](GDK.Net.PlayFab.Party.PartyRemoteDeviceJoinedNetwork.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyRemoteDeviceJoinedNetwork\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceJoinedNetwork__ctor_GDK_Net_PlayFab_Party_PartyDevice_GDK_Net_PlayFab_Party_PartyNetwork_"></a> PartyRemoteDeviceJoinedNetwork\(PartyDevice?, PartyNetwork?\)

A remote device joined a network.

```csharp
public PartyRemoteDeviceJoinedNetwork(PartyDevice? Device, PartyNetwork? Network)
```

#### Parameters

`Device` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

The device.

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network it joined.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceJoinedNetwork_Device"></a> Device

The device.

```csharp
public PartyDevice? Device { get; init; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceJoinedNetwork_Network"></a> Network

The network it joined.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

