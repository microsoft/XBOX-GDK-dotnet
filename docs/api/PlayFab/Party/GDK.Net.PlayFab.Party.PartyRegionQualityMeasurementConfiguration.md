# <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration"></a> Class PartyRegionQualityMeasurementConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_REGION_QUALITY_MEASUREMENT_CONFIGURATION</code>.

```csharp
public sealed class PartyRegionQualityMeasurementConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyRegionQualityMeasurementConfiguration](GDK.Net.PlayFab.Party.PartyRegionQualityMeasurementConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration_HighLatencyHintInMilliseconds"></a> HighLatencyHintInMilliseconds

The latency above which a region is treated as high-latency.

```csharp
public ushort HighLatencyHintInMilliseconds { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration_IdealNumberOfSuccessfulResponses"></a> IdealNumberOfSuccessfulResponses

The number of successful responses Party aims for per region.

```csharp
public ushort IdealNumberOfSuccessfulResponses { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration_MaxRetriesWithNoResponse"></a> MaxRetriesWithNoResponse

How many times a silent region is retried.

```csharp
public ushort MaxRetriesWithNoResponse { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration_MaxTimeoutsAfterResponse"></a> MaxTimeoutsAfterResponse

How many timeouts are tolerated after the first response.

```csharp
public ushort MaxTimeoutsAfterResponse { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration_MinRequiredSuccessfulResponses"></a> MinRequiredSuccessfulResponses

The minimum number of successful responses required per region.

```csharp
public ushort MinRequiredSuccessfulResponses { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyRegionQualityMeasurementConfiguration_TotalMeasurementTimeout"></a> TotalMeasurementTimeout

The overall budget for a full measurement pass.

```csharp
public TimeSpan TotalMeasurementTimeout { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

