# <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChangeCollection"></a> Struct MatchmakingStateChangeCollection

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

The matchmaking state changes produced by one pump.

```csharp
public readonly struct MatchmakingStateChangeCollection : IEnumerable<MatchmakingStateChange>, IEnumerable
```

#### Implements

[IEnumerable<MatchmakingStateChange\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1), 
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChangeCollection_GetEnumerator"></a> GetEnumerator\(\)

Starts a batch of matchmaking state changes.

```csharp
public MatchmakingStateChangeCollection.Enumerator GetEnumerator()
```

#### Returns

 [MatchmakingStateChangeCollection](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChangeCollection.md).[Enumerator](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChangeCollection.Enumerator.md)

