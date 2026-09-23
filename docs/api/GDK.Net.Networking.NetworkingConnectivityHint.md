# <a id="GDK_Net_Networking_NetworkingConnectivityHint"></a> Struct NetworkingConnectivityHint

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

A snapshot of the device's network connectivity state.
Mirrors <code>XNetworkingConnectivityHint</code> from XNetworking.h.

```csharp
public readonly struct NetworkingConnectivityHint
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_ApproachingDataLimit"></a> ApproachingDataLimit

Whether the device is approaching its data limit.

```csharp
public bool ApproachingDataLimit { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_ConnectivityCost"></a> ConnectivityCost

The monetary or data cost associated with the active network connection.

```csharp
public NetworkingConnectivityCostHint ConnectivityCost { get; }
```

#### Property Value

 [NetworkingConnectivityCostHint](GDK.Net.Networking.NetworkingConnectivityCostHint.md)

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_ConnectivityLevel"></a> ConnectivityLevel

The device's current internet connectivity level.

```csharp
public NetworkingConnectivityLevelHint ConnectivityLevel { get; }
```

#### Property Value

 [NetworkingConnectivityLevelHint](GDK.Net.Networking.NetworkingConnectivityLevelHint.md)

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_IanaInterfaceType"></a> IanaInterfaceType

The IANA interface type (see <code>IF_TYPE_*</code> in ipifcons.h).

```csharp
public uint IanaInterfaceType { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_NetworkInitialized"></a> NetworkInitialized

Whether the network stack has fully initialized.

```csharp
public bool NetworkInitialized { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_OverDataLimit"></a> OverDataLimit

Whether the device has exceeded its data limit.

```csharp
public bool OverDataLimit { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Networking_NetworkingConnectivityHint_Roaming"></a> Roaming

Whether the device is roaming.

```csharp
public bool Roaming { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

