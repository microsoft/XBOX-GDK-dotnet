# <a id="GDK_Net_PlayFab_Party_PartyEndpointPropertiesChanged"></a> Class PartyEndpointPropertiesChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Properties shared on an endpoint changed.

```csharp
public sealed record PartyEndpointPropertiesChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyEndpointPropertiesChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyEndpointPropertiesChanged](GDK.Net.PlayFab.Party.PartyEndpointPropertiesChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyEndpointPropertiesChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyEndpointPropertiesChanged__ctor_GDK_Net_PlayFab_Party_PartyEndpoint_System_Collections_Generic_IReadOnlyList_System_String__"></a> PartyEndpointPropertiesChanged\(PartyEndpoint?, IReadOnlyList<string\>\)

Properties shared on an endpoint changed.

```csharp
public PartyEndpointPropertiesChanged(PartyEndpoint? Endpoint, IReadOnlyList<string> Keys)
```

#### Parameters

`Endpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The endpoint.

`Keys` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The keys whose values changed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyEndpointPropertiesChanged_Endpoint"></a> Endpoint

The endpoint.

```csharp
public PartyEndpoint? Endpoint { get; init; }
```

#### Property Value

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpointPropertiesChanged_Keys"></a> Keys

The keys whose values changed.

```csharp
public IReadOnlyList<string> Keys { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

