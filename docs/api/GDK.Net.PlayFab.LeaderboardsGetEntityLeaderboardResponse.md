# <a id="GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardResponse"></a> Class LeaderboardsGetEntityLeaderboardResponse

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLeaderboardsGetEntityLeaderboardResponse</code>.

```csharp
public sealed class LeaderboardsGetEntityLeaderboardResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardsGetEntityLeaderboardResponse](GDK.Net.PlayFab.LeaderboardsGetEntityLeaderboardResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardResponse_Columns"></a> Columns

<code>Columns</code>.

```csharp
public IReadOnlyList<LeaderboardsLeaderboardColumn>? Columns { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardsLeaderboardColumn](GDK.Net.PlayFab.LeaderboardsLeaderboardColumn.md)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardResponse_EntryCount"></a> EntryCount

<code>EntryCount</code>.

```csharp
public uint EntryCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardResponse_NextReset"></a> NextReset

<code>NextReset</code>.

```csharp
public DateTimeOffset? NextReset { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardResponse_Rankings"></a> Rankings

<code>Rankings</code>.

```csharp
public IReadOnlyList<LeaderboardsEntityLeaderboardEntry>? Rankings { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardsEntityLeaderboardEntry](GDK.Net.PlayFab.LeaderboardsEntityLeaderboardEntry.md)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsGetEntityLeaderboardResponse_Version"></a> Version

<code>Version</code>.

```csharp
public uint Version { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

