# <a id="GDK_Net_XboxLive_PresenceRecord"></a> Class PresenceRecord

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A user's Xbox Live presence. Managed snapshot of <code>XblPresenceRecordHandle</code>.

```csharp
public sealed class PresenceRecord
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceRecord](GDK.Net.XboxLive.PresenceRecord.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Native presence records own the arrays of device, title and broadcast records they expose. This
type copies that graph in full while the native handle is alive, so instances remain valid after
the handle returned by XSAPI has been closed.

## Properties

### <a id="GDK_Net_XboxLive_PresenceRecord_Devices"></a> Devices

The device-level presence records for the user.

```csharp
public IReadOnlyList<PresenceDeviceRecord> Devices { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PresenceDeviceRecord](GDK.Net.XboxLive.PresenceDeviceRecord.md)\>

### <a id="GDK_Net_XboxLive_PresenceRecord_UserState"></a> UserState

The user's aggregate presence state.

```csharp
public PresenceUserState UserState { get; }
```

#### Property Value

 [PresenceUserState](GDK.Net.XboxLive.PresenceUserState.md)

### <a id="GDK_Net_XboxLive_PresenceRecord_XboxUserId"></a> XboxUserId

The Xbox user id this presence record describes.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_PresenceRecord_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

