# <a id="GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest"></a> Class LeaderboardsGetLeaderboardAroundEntityRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLeaderboardsGetLeaderboardAroundEntityRequest</code>.

```csharp
public sealed class LeaderboardsGetLeaderboardAroundEntityRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardsGetLeaderboardAroundEntityRequest](GDK.Net.PlayFab.LeaderboardsGetLeaderboardAroundEntityRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest_LeaderboardName"></a> LeaderboardName

<code>LeaderboardName</code>.

```csharp
public string? LeaderboardName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest_MaxSurroundingEntries"></a> MaxSurroundingEntries

<code>MaxSurroundingEntries</code>.

```csharp
public uint MaxSurroundingEntries { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_LeaderboardsGetLeaderboardAroundEntityRequest_Version"></a> Version

<code>Version</code>.

```csharp
public uint? Version { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

