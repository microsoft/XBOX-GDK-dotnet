# <a id="GDK_Net_Package_PackageChunkAvailability"></a> Enum PackageChunkAvailability

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

The installation availability of a chunk. Mirrors <code>XPackageChunkAvailability</code>.

```csharp
public enum PackageChunkAvailability : uint
```

## Fields

`Installable = 2` 

The chunk can be installed on this device.



`Pending = 1` 

The chunk is being installed.



`Ready = 0` 

The chunk is fully installed and ready to use.



`Unavailable = 3` 

The chunk is not available on this device.



