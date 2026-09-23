# <a id="GDK_Net_SystemInfo_SystemAnalyticsInfo"></a> Class SystemAnalyticsInfo

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Analytics information about the current device (<code>XSystemAnalyticsInfo</code> from XSystem.h).

```csharp
public sealed class SystemAnalyticsInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SystemAnalyticsInfo](GDK.Net.SystemInfo.SystemAnalyticsInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_SystemInfo_SystemAnalyticsInfo_Family"></a> Family

Device family string (e.g. <code>"XboxOne"</code>).

```csharp
public string Family { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_SystemInfo_SystemAnalyticsInfo_Form"></a> Form

Device form factor string (e.g. <code>"Xbox One S"</code>).

```csharp
public string Form { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_SystemInfo_SystemAnalyticsInfo_HostingOsVersion"></a> HostingOsVersion

The hosting OS version (relevant on streamed titles).

```csharp
public SystemVersion HostingOsVersion { get; }
```

#### Property Value

 [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

### <a id="GDK_Net_SystemInfo_SystemAnalyticsInfo_OsVersion"></a> OsVersion

The OS version running on the device.

```csharp
public SystemVersion OsVersion { get; }
```

#### Property Value

 [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

## Methods

### <a id="GDK_Net_SystemInfo_SystemAnalyticsInfo_ToString"></a> ToString\(\)

Returns the family, form factor and OS version for diagnostics.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

