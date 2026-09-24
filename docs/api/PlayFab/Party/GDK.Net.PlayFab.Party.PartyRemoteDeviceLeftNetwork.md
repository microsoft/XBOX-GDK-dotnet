# <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceLeftNetwork"></a> Class PartyRemoteDeviceLeftNetwork

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A remote device left a network.

```csharp
public sealed record PartyRemoteDeviceLeftNetwork : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyRemoteDeviceLeftNetwork>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyRemoteDeviceLeftNetwork](GDK.Net.PlayFab.Party.PartyRemoteDeviceLeftNetwork.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyRemoteDeviceLeftNetwork\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceLeftNetwork__ctor_GDK_Net_PlayFab_Party_PartyDevice_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyDestroyedReason_System_UInt32_"></a> PartyRemoteDeviceLeftNetwork\(PartyDevice?, PartyNetwork?, PartyDestroyedReason, uint\)

A remote device left a network.

```csharp
public PartyRemoteDeviceLeftNetwork(PartyDevice? Device, PartyNetwork? Network, PartyDestroyedReason Reason, uint ErrorDetail)
```

#### Parameters

`Device` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

The device.

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network it left.

`Reason` [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

Why it left.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the departure was involuntary.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceLeftNetwork_Device"></a> Device

The device.

```csharp
public PartyDevice? Device { get; init; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceLeftNetwork_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the departure was involuntary.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceLeftNetwork_Network"></a> Network

The network it left.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceLeftNetwork_Reason"></a> Reason

Why it left.

```csharp
public PartyDestroyedReason Reason { get; init; }
```

#### Property Value

 [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

