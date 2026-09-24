# <a id="GDK_Net_PlayFab_Party_PartyStateChangeCollection"></a> Struct PartyStateChangeCollection

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The Party state changes produced by one pump. Enumerating starts the batch; leaving the loop
returns it to the Party library.

```csharp
public readonly struct PartyStateChangeCollection : IEnumerable<PartyStateChange>, IEnumerable
```

#### Implements

[IEnumerable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1), 
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyStateChangeCollection_GetEnumerator"></a> GetEnumerator\(\)

Starts a batch of state changes.

```csharp
public PartyStateChangeCollection.Enumerator GetEnumerator()
```

#### Returns

 [PartyStateChangeCollection](GDK.Net.PlayFab.Party.PartyStateChangeCollection.md).[Enumerator](GDK.Net.PlayFab.Party.PartyStateChangeCollection.Enumerator.md)

