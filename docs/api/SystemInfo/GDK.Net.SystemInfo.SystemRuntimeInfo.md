# <a id="GDK_Net_SystemInfo_SystemRuntimeInfo"></a> Class SystemRuntimeInfo

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Runtime and available GDK version information (<code>XSystemRuntimeInfo</code> from XSystem.h).

```csharp
public sealed class SystemRuntimeInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SystemRuntimeInfo](GDK.Net.SystemInfo.SystemRuntimeInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_SystemInfo_SystemRuntimeInfo_AvailableVersion"></a> AvailableVersion

The highest version available on this device.

```csharp
public SystemVersion AvailableVersion { get; }
```

#### Property Value

 [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

### <a id="GDK_Net_SystemInfo_SystemRuntimeInfo_RuntimeVersion"></a> RuntimeVersion

The version of the Gaming Runtime currently running.

```csharp
public SystemVersion RuntimeVersion { get; }
```

#### Property Value

 [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

## Methods

### <a id="GDK_Net_SystemInfo_SystemRuntimeInfo_ToString"></a> ToString\(\)

Returns the runtime and available versions for diagnostics.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

