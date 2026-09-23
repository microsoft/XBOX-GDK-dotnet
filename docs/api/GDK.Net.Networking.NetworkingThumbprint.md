# <a id="GDK_Net_Networking_NetworkingThumbprint"></a> Class NetworkingThumbprint

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

A certificate thumbprint for TLS certificate pinning.
Mirrors <code>XNetworkingThumbprint</code> from XNetworking.h.

```csharp
public sealed class NetworkingThumbprint
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NetworkingThumbprint](GDK.Net.Networking.NetworkingThumbprint.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Networking_NetworkingThumbprint_Data"></a> Data

The raw thumbprint bytes.

```csharp
public IReadOnlyList<byte> Data { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

### <a id="GDK_Net_Networking_NetworkingThumbprint_ThumbprintType"></a> ThumbprintType

Which certificate in the chain this thumbprint corresponds to.

```csharp
public NetworkingThumbprintType ThumbprintType { get; }
```

#### Property Value

 [NetworkingThumbprintType](GDK.Net.Networking.NetworkingThumbprintType.md)

