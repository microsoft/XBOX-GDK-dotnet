# <a id="GDK_Net_Networking_NetworkingConnectivityCostHint"></a> Enum NetworkingConnectivityCostHint

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

Mirrors <code>XNetworkingConnectivityCostHint</code> from XNetworking.h.
Describes the monetary or data cost of the active connection.

```csharp
public enum NetworkingConnectivityCostHint : uint
```

## Fields

`Fixed = 2` 

The connection has a fixed data cap.



`Unknown = 0` 

The cost type cannot be determined.



`Unrestricted = 1` 

The connection is unrestricted (e.g. wired Ethernet or Wi-Fi on a flat-rate plan).



`Variable = 3` 

The connection is metered (e.g. cellular).



