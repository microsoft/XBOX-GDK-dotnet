# <a id="GDK_Net_Networking_NetworkingSecurityInformation"></a> Class NetworkingSecurityInformation

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

TLS security information for a URL, used with <xref href="GDK.Net.Networking.NetworkingManager.VerifyServerCertificate(System.IntPtr%2cGDK.Net.Networking.NetworkingSecurityInformation)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XNetworkingSecurityInformation</code> from XNetworking.h.

```csharp
public sealed class NetworkingSecurityInformation : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NetworkingSecurityInformation](GDK.Net.Networking.NetworkingSecurityInformation.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
This object holds a pinned managed buffer whose address was given to the native result call.
The native <code>XNetworkingSecurityInformation*</code> pointer (and its embedded thumbprint pointers)
all point into that buffer, so the buffer must stay pinned and at the same address until the
object is disposed.
</p>
<p>
Call <xref href="GDK.Net.Networking.NetworkingSecurityInformation.Dispose" data-throw-if-not-resolved="false"></xref> when the object is no longer needed. After disposal it must not be
passed to <xref href="GDK.Net.Networking.NetworkingManager.VerifyServerCertificate(System.IntPtr%2cGDK.Net.Networking.NetworkingSecurityInformation)" data-throw-if-not-resolved="false"></xref>.
</p>

## Properties

### <a id="GDK_Net_Networking_NetworkingSecurityInformation_EnabledHttpSecurityProtocolFlags"></a> EnabledHttpSecurityProtocolFlags

The set of enabled HTTP security protocol flags.

```csharp
public uint EnabledHttpSecurityProtocolFlags { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Networking_NetworkingSecurityInformation_Thumbprints"></a> Thumbprints

The certificate thumbprints associated with this URL's server certificate chain.

```csharp
public IReadOnlyList<NetworkingThumbprint> Thumbprints { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[NetworkingThumbprint](GDK.Net.Networking.NetworkingThumbprint.md)\>

## Methods

### <a id="GDK_Net_Networking_NetworkingSecurityInformation_Dispose"></a> Dispose\(\)

Disposes the object and releases the pinned buffer. After this call the object
must not be passed to <xref href="GDK.Net.Networking.NetworkingManager.VerifyServerCertificate(System.IntPtr%2cGDK.Net.Networking.NetworkingSecurityInformation)" data-throw-if-not-resolved="false"></xref>.

```csharp
public void Dispose()
```

