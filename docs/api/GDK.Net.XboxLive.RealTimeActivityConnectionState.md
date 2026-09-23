# <a id="GDK_Net_XboxLive_RealTimeActivityConnectionState"></a> Enum RealTimeActivityConnectionState

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

State of the websocket connection to the Xbox Live real-time activity service. Mirrors
<code>XblRealTimeActivityConnectionState</code>.

```csharp
public enum RealTimeActivityConnectionState : uint
```

## Fields

`Connected = 0` 

The websocket is connected to the real-time activity service.



`Connecting = 1` 

XSAPI is connecting the websocket to the real-time activity service.



`Disconnected = 2` 

The websocket is disconnected from the real-time activity service.



