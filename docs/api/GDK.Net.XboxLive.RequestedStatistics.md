# <a id="GDK_Net_XboxLive_RequestedStatistics"></a> Class RequestedStatistics

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Statistics requested for one service configuration in a multi-SCID batch. Mirrors
<code>XblRequestedStatistics</code> without exposing native buffers.

```csharp
public sealed class RequestedStatistics
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RequestedStatistics](GDK.Net.XboxLive.RequestedStatistics.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_RequestedStatistics__ctor_System_String_System_Collections_Generic_IEnumerable_System_String__"></a> RequestedStatistics\(string, IEnumerable<string\>\)

Creates a request for a service configuration and its statistic names.

```csharp
public RequestedStatistics(string serviceConfigurationId, IEnumerable<string> statisticNames)
```

#### Parameters

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service configuration id (SCID) to query.

`statisticNames` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Statistic names to query under the service configuration.

## Properties

### <a id="GDK_Net_XboxLive_RequestedStatistics_ServiceConfigurationId"></a> ServiceConfigurationId

The service configuration id (SCID) to query.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_RequestedStatistics_StatisticNames"></a> StatisticNames

The statistic names to query under <xref href="GDK.Net.XboxLive.RequestedStatistics.ServiceConfigurationId" data-throw-if-not-resolved="false"></xref>.

```csharp
public IReadOnlyList<string> StatisticNames { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

