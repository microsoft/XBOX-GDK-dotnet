# <a id="GDK_Net_Package_PackageChunkAvailabilityInfo"></a> Class PackageChunkAvailabilityInfo

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Availability and a chunk selector, as returned by
<xref href="GDK.Net.Package.GamePackage.EnumerateChunkAvailability(System.String%2cGDK.Net.Package.PackageChunkSelectorType)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class PackageChunkAvailabilityInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PackageChunkAvailabilityInfo](GDK.Net.Package.PackageChunkAvailabilityInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Package_PackageChunkAvailabilityInfo_Availability"></a> Availability

The chunk's current availability on this device.

```csharp
public PackageChunkAvailability Availability { get; }
```

#### Property Value

 [PackageChunkAvailability](GDK.Net.Package.PackageChunkAvailability.md)

### <a id="GDK_Net_Package_PackageChunkAvailabilityInfo_Selector"></a> Selector

The chunk selector identifying this chunk.

```csharp
public PackageChunkSelector Selector { get; }
```

#### Property Value

 [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

