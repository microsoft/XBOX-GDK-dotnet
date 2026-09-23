# <a id="GDK_Net_PlayFab_Party_PartyNetworkPropertiesChanged"></a> Class PartyNetworkPropertiesChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Properties shared on a network changed.

```csharp
public sealed record PartyNetworkPropertiesChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyNetworkPropertiesChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyNetworkPropertiesChanged](GDK.Net.PlayFab.Party.PartyNetworkPropertiesChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyNetworkPropertiesChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyNetworkPropertiesChanged__ctor_GDK_Net_PlayFab_Party_PartyNetwork_System_Collections_Generic_IReadOnlyList_System_String__"></a> PartyNetworkPropertiesChanged\(PartyNetwork?, IReadOnlyList<string\>\)

Properties shared on a network changed.

```csharp
public PartyNetworkPropertiesChanged(PartyNetwork? Network, IReadOnlyList<string> Keys)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`Keys` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The keys whose values changed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyNetworkPropertiesChanged_Keys"></a> Keys

The keys whose values changed.

```csharp
public IReadOnlyList<string> Keys { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetworkPropertiesChanged_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

