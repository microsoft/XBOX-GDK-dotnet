# <a id="GDK_Net_XboxLive_DevicePresenceChangedEventArgs"></a> Class DevicePresenceChangedEventArgs

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.XboxLive.PresenceService.DevicePresenceChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class DevicePresenceChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[DevicePresenceChangedEventArgs](GDK.Net.XboxLive.DevicePresenceChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_DevicePresenceChangedEventArgs_DeviceType"></a> DeviceType

The device whose presence changed.

```csharp
public PresenceDeviceType DeviceType { get; }
```

#### Property Value

 [PresenceDeviceType](GDK.Net.XboxLive.PresenceDeviceType.md)

### <a id="GDK_Net_XboxLive_DevicePresenceChangedEventArgs_IsUserLoggedOnDevice"></a> IsUserLoggedOnDevice

Whether the user is now logged on to that device.

```csharp
public bool IsUserLoggedOnDevice { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_DevicePresenceChangedEventArgs_XboxUserId"></a> XboxUserId

The user whose device presence changed.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

