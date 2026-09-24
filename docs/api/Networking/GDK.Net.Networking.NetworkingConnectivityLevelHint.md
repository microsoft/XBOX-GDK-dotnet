# <a id="GDK_Net_Networking_NetworkingConnectivityLevelHint"></a> Enum NetworkingConnectivityLevelHint

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

Mirrors <code>XNetworkingConnectivityLevelHint</code> from XNetworking.h.
Describes the internet connectivity level observed by the device.

```csharp
public enum NetworkingConnectivityLevelHint : uint
```

## Fields

`ConstrainedInternetAccess = 4` 

The device has internet access but it is behind a captive portal or similar constraint.



`InternetAccess = 3` 

The device has unrestricted internet access.



`LocalAccess = 2` 

The device can reach the local network only.



`None = 1` 

No network connectivity is available.



`Unknown = 0` 

The connectivity level cannot be determined.



