# <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration"></a> Class PartyNetworkConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_NETWORK_CONFIGURATION</code>.

```csharp
public sealed class PartyNetworkConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration_DirectPeerConnectivityOptions"></a> DirectPeerConnectivityOptions

Which direct peer-to-peer connections the network may attempt.

```csharp
public PartyDirectPeerConnectivityOptions DirectPeerConnectivityOptions { get; set; }
```

#### Property Value

 [PartyDirectPeerConnectivityOptions](GDK.Net.PlayFab.Party.PartyDirectPeerConnectivityOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration_MaxDeviceCount"></a> MaxDeviceCount

The maximum number of devices allowed in the network.

```csharp
public uint MaxDeviceCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration_MaxDevicesPerUserCount"></a> MaxDevicesPerUserCount

The maximum number of devices a single user may authenticate from.

```csharp
public uint MaxDevicesPerUserCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration_MaxEndpointsPerDeviceCount"></a> MaxEndpointsPerDeviceCount

The maximum number of endpoints a single device may create.

```csharp
public uint MaxEndpointsPerDeviceCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration_MaxUserCount"></a> MaxUserCount

The maximum number of users allowed in the network.

```csharp
public uint MaxUserCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkConfiguration_MaxUsersPerDeviceCount"></a> MaxUsersPerDeviceCount

The maximum number of users a single device may authenticate.

```csharp
public uint MaxUsersPerDeviceCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

