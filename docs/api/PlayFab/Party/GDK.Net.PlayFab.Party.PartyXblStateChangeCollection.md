# <a id="GDK_Net_PlayFab_Party_PartyXblStateChangeCollection"></a> Struct PartyXblStateChangeCollection

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The Xbox Live extension state changes produced by one pump.

```csharp
public readonly struct PartyXblStateChangeCollection : IEnumerable<PartyXblStateChange>, IEnumerable
```

#### Implements

[IEnumerable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1), 
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyXblStateChangeCollection_GetEnumerator"></a> GetEnumerator\(\)

Starts a batch of state changes.

```csharp
public PartyXblStateChangeCollection.Enumerator GetEnumerator()
```

#### Returns

 [PartyXblStateChangeCollection](GDK.Net.PlayFab.Party.PartyXblStateChangeCollection.md).[Enumerator](GDK.Net.PlayFab.Party.PartyXblStateChangeCollection.Enumerator.md)

