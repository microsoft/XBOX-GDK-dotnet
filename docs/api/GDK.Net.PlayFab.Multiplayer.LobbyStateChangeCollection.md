# <a id="GDK_Net_PlayFab_Multiplayer_LobbyStateChangeCollection"></a> Struct LobbyStateChangeCollection

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

The lobby state changes produced by one pump. Enumerating starts the batch; leaving the loop
returns it to the multiplayer library.

```csharp
public readonly struct LobbyStateChangeCollection : IEnumerable<LobbyStateChange>, IEnumerable
```

#### Implements

[IEnumerable<LobbyStateChange\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1), 
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_LobbyStateChangeCollection_GetEnumerator"></a> GetEnumerator\(\)

Starts a batch of lobby state changes.

```csharp
public LobbyStateChangeCollection.Enumerator GetEnumerator()
```

#### Returns

 [LobbyStateChangeCollection](GDK.Net.PlayFab.Multiplayer.LobbyStateChangeCollection.md).[Enumerator](GDK.Net.PlayFab.Multiplayer.LobbyStateChangeCollection.Enumerator.md)

