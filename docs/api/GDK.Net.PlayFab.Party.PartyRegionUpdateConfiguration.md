# <a id="GDK_Net_PlayFab_Party_PartyRegionUpdateConfiguration"></a> Class PartyRegionUpdateConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_REGION_UPDATE_CONFIGURATION</code>.

```csharp
public sealed class PartyRegionUpdateConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyRegionUpdateConfiguration](GDK.Net.PlayFab.Party.PartyRegionUpdateConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRegionUpdateConfiguration_Mode"></a> Mode

When Party refreshes its region latency measurements.

```csharp
public PartyRegionUpdateMode Mode { get; set; }
```

#### Property Value

 [PartyRegionUpdateMode](GDK.Net.PlayFab.Party.PartyRegionUpdateMode.md)

### <a id="GDK_Net_PlayFab_Party_PartyRegionUpdateConfiguration_RefreshInterval"></a> RefreshInterval

How often the measurements are refreshed in deferred mode.

```csharp
public TimeSpan RefreshInterval { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

