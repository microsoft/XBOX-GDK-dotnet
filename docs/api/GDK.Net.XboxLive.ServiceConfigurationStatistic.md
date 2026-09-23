# <a id="GDK_Net_XboxLive_ServiceConfigurationStatistic"></a> Class ServiceConfigurationStatistic

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

The statistics returned for a single service configuration. Managed snapshot of
<code>XblServiceConfigurationStatistic</code>.

```csharp
public sealed class ServiceConfigurationStatistic
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ServiceConfigurationStatistic](GDK.Net.XboxLive.ServiceConfigurationStatistic.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_ServiceConfigurationStatistic_ServiceConfigurationId"></a> ServiceConfigurationId

The service configuration id (SCID) the statistics came from.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_ServiceConfigurationStatistic_Statistics"></a> Statistics

The statistics Xbox Live returned for this service configuration.

```csharp
public IReadOnlyList<Statistic> Statistics { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[Statistic](GDK.Net.XboxLive.Statistic.md)\>

