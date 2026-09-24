# <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceDestroyed"></a> Class PartyRemoteDeviceDestroyed

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A remote device is no longer known to the library.

```csharp
public sealed record PartyRemoteDeviceDestroyed : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyRemoteDeviceDestroyed>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyRemoteDeviceDestroyed](GDK.Net.PlayFab.Party.PartyRemoteDeviceDestroyed.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyRemoteDeviceDestroyed\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceDestroyed__ctor_GDK_Net_PlayFab_Party_PartyDevice_"></a> PartyRemoteDeviceDestroyed\(PartyDevice?\)

A remote device is no longer known to the library.

```csharp
public PartyRemoteDeviceDestroyed(PartyDevice? Device)
```

#### Parameters

`Device` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

The device.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRemoteDeviceDestroyed_Device"></a> Device

The device.

```csharp
public PartyDevice? Device { get; init; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

