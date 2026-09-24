# <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest"></a> Class StatisticsCreateStatisticDefinitionRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFStatisticsCreateStatisticDefinitionRequest</code>.

```csharp
public sealed class StatisticsCreateStatisticDefinitionRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StatisticsCreateStatisticDefinitionRequest](GDK.Net.PlayFab.StatisticsCreateStatisticDefinitionRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_AggregationSources"></a> AggregationSources

<code>AggregationSources</code>.

```csharp
public IReadOnlyList<string>? AggregationSources { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_Columns"></a> Columns

<code>Columns</code>.

```csharp
public IReadOnlyList<StatisticsStatisticColumn>? Columns { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StatisticsStatisticColumn](GDK.Net.PlayFab.StatisticsStatisticColumn.md)\>?

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_EntityType"></a> EntityType

<code>EntityType</code>.

```csharp
public string? EntityType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_EventEmissionConfig"></a> EventEmissionConfig

<code>EventEmissionConfig</code>.

```csharp
public StatisticsStatisticsEventEmissionConfig? EventEmissionConfig { get; set; }
```

#### Property Value

 [StatisticsStatisticsEventEmissionConfig](GDK.Net.PlayFab.StatisticsStatisticsEventEmissionConfig.md)?

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_Name"></a> Name

<code>Name</code>.

```csharp
public string? Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_StatisticsCreateStatisticDefinitionRequest_VersionConfiguration"></a> VersionConfiguration

<code>VersionConfiguration</code>.

```csharp
public VersionConfiguration? VersionConfiguration { get; set; }
```

#### Property Value

 [VersionConfiguration](GDK.Net.PlayFab.VersionConfiguration.md)?

