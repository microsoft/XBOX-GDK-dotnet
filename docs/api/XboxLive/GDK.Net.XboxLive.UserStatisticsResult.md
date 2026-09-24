# <a id="GDK_Net_XboxLive_UserStatisticsResult"></a> Class UserStatisticsResult

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Statistics returned for one Xbox user. Managed snapshot of <code>XblUserStatisticsResult</code>.

```csharp
public sealed class UserStatisticsResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserStatisticsResult](GDK.Net.XboxLive.UserStatisticsResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_UserStatisticsResult_ServiceConfigurations"></a> ServiceConfigurations

The service configurations and statistics Xbox Live returned for the user.

```csharp
public IReadOnlyList<ServiceConfigurationStatistic> ServiceConfigurations { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ServiceConfigurationStatistic](GDK.Net.XboxLive.ServiceConfigurationStatistic.md)\>

### <a id="GDK_Net_XboxLive_UserStatisticsResult_XboxUserId"></a> XboxUserId

The Xbox user id the statistics describe.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

