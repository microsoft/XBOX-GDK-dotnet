# <a id="GDK_Net_SystemInfo_HdrModeInfo"></a> Class HdrModeInfo

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Luminance parameters returned by <xref href="GDK.Net.SystemInfo.GameDisplay.TryEnableHdrMode(GDK.Net.SystemInfo.HdrModePreference%2cGDK.Net.SystemInfo.HdrModeInfo%40)" data-throw-if-not-resolved="false"></xref> when HDR is
enabled. Mirrors <code>struct XDisplayHdrModeInfo</code> from XDisplay.h.

```csharp
public sealed class HdrModeInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HdrModeInfo](GDK.Net.SystemInfo.HdrModeInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_SystemInfo_HdrModeInfo_MaxFullFrameToneMapLuminance"></a> MaxFullFrameToneMapLuminance

Maximum full-frame tone-map luminance in nits.

```csharp
public float MaxFullFrameToneMapLuminance { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_SystemInfo_HdrModeInfo_MaxToneMapLuminance"></a> MaxToneMapLuminance

Maximum tone-map luminance in nits.

```csharp
public float MaxToneMapLuminance { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_SystemInfo_HdrModeInfo_MinToneMapLuminance"></a> MinToneMapLuminance

Minimum tone-map luminance in nits.

```csharp
public float MinToneMapLuminance { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

