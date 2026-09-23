# <a id="GDK_Net_Users_UserDeviceAssociationChangedEventArgs"></a> Class UserDeviceAssociationChangedEventArgs

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Users.UserManager.DeviceAssociationChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class UserDeviceAssociationChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[UserDeviceAssociationChangedEventArgs](GDK.Net.Users.UserDeviceAssociationChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Users_UserDeviceAssociationChangedEventArgs_DeviceId"></a> DeviceId

The device whose user association changed.

```csharp
public AppLocalDeviceId DeviceId { get; }
```

#### Property Value

 [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

### <a id="GDK_Net_Users_UserDeviceAssociationChangedEventArgs_NewUser"></a> NewUser

The local id of the user now associated with the device, or <xref href="GDK.Net.Users.UserLocalId.Null" data-throw-if-not-resolved="false"></xref>.

```csharp
public UserLocalId NewUser { get; }
```

#### Property Value

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

### <a id="GDK_Net_Users_UserDeviceAssociationChangedEventArgs_OldUser"></a> OldUser

The local id of the user previously associated with the device, or <xref href="GDK.Net.Users.UserLocalId.Null" data-throw-if-not-resolved="false"></xref>.

```csharp
public UserLocalId OldUser { get; }
```

#### Property Value

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

