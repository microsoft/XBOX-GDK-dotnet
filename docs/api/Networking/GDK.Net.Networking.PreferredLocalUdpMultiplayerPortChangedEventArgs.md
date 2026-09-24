# <a id="GDK_Net_Networking_PreferredLocalUdpMultiplayerPortChangedEventArgs"></a> Class PreferredLocalUdpMultiplayerPortChangedEventArgs

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Networking.NetworkingManager.PreferredLocalUdpMultiplayerPortChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class PreferredLocalUdpMultiplayerPortChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[PreferredLocalUdpMultiplayerPortChangedEventArgs](GDK.Net.Networking.PreferredLocalUdpMultiplayerPortChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Networking_PreferredLocalUdpMultiplayerPortChangedEventArgs_Port"></a> Port

The new preferred local UDP multiplayer port.

```csharp
public ushort Port { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

