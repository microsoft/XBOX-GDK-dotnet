# <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChangeCollection_Enumerator"></a> Struct MatchmakingStateChangeCollection.Enumerator

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

Walks one batch of matchmaking state changes.

```csharp
public struct MatchmakingStateChangeCollection.Enumerator : IEnumerator<MatchmakingStateChange>, IEnumerator, IDisposable
```

#### Implements

[IEnumerator<MatchmakingStateChange\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator\-1), 
[IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.ienumerator), 
[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChangeCollection_Enumerator_Current"></a> Current

Gets the element in the collection at the current position of the enumerator.

```csharp
public MatchmakingStateChange Current { get; }
```

#### Property Value

 [MatchmakingStateChange](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChange.md)

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChangeCollection_Enumerator_Dispose"></a> Dispose\(\)

Returns the batch to the multiplayer library.

```csharp
public void Dispose()
```

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChangeCollection_Enumerator_MoveNext"></a> MoveNext\(\)

Advances the enumerator to the next element of the collection.

```csharp
public bool MoveNext()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the enumerator was successfully advanced to the next element; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> if the enumerator has passed the end of the collection.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The collection was modified after the enumerator was created.

