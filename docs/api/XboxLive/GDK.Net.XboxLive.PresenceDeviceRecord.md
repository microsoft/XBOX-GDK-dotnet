# <a id="GDK_Net_XboxLive_PresenceDeviceRecord"></a> Class PresenceDeviceRecord

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Presence for one device. Managed snapshot of <code>XblPresenceDeviceRecord</code>.

```csharp
public sealed class PresenceDeviceRecord
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceDeviceRecord](GDK.Net.XboxLive.PresenceDeviceRecord.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_PresenceDeviceRecord_DeviceType"></a> DeviceType

The device type associated with this record.

```csharp
public PresenceDeviceType DeviceType { get; }
```

#### Property Value

 [PresenceDeviceType](GDK.Net.XboxLive.PresenceDeviceType.md)

### <a id="GDK_Net_XboxLive_PresenceDeviceRecord_Titles"></a> Titles

The title presence records reported for this device.

```csharp
public IReadOnlyList<PresenceTitleRecord> Titles { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PresenceTitleRecord](GDK.Net.XboxLive.PresenceTitleRecord.md)\>

