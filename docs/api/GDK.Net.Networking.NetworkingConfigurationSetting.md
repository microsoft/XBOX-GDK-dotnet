# <a id="GDK_Net_Networking_NetworkingConfigurationSetting"></a> Enum NetworkingConfigurationSetting

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

Mirrors <code>XNetworkingConfigurationSetting</code> from XNetworking.h.
Identifies a per-partition TCP receive-buffer configuration value.

```csharp
public enum NetworkingConfigurationSetting : uint
```

## Fields

`MaxSystemTcpQueuedReceiveBufferSize = 1` 

Maximum queued TCP receive bytes for the system partition.



`MaxTitleTcpQueuedReceiveBufferSize = 0` 

Maximum queued TCP receive bytes for the title partition.



`MaxToolsTcpQueuedReceiveBufferSize = 2` 

Maximum queued TCP receive bytes for the tools partition.



