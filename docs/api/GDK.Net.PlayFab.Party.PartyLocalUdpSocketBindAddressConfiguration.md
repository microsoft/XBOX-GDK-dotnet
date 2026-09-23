# <a id="GDK_Net_PlayFab_Party_PartyLocalUdpSocketBindAddressConfiguration"></a> Class PartyLocalUdpSocketBindAddressConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_CONFIGURATION</code>.

```csharp
public sealed class PartyLocalUdpSocketBindAddressConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyLocalUdpSocketBindAddressConfiguration](GDK.Net.PlayFab.Party.PartyLocalUdpSocketBindAddressConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyLocalUdpSocketBindAddressConfiguration_Options"></a> Options

How the bind address is chosen.

```csharp
public PartyLocalUdpSocketBindAddressOptions Options { get; set; }
```

#### Property Value

 [PartyLocalUdpSocketBindAddressOptions](GDK.Net.PlayFab.Party.PartyLocalUdpSocketBindAddressOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyLocalUdpSocketBindAddressConfiguration_Port"></a> Port

The UDP port to bind, or 0 to let the platform choose.

```csharp
public ushort Port { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

