# <a id="GDK_Net_PlayFab_Party_PartyXblEntityIdMapping"></a> Struct PartyXblEntityIdMapping

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_XBL_XBOX_USER_ID_TO_PLAYFAB_ENTITY_ID_MAPPING</code>.

```csharp
public readonly record struct PartyXblEntityIdMapping : IEquatable<PartyXblEntityIdMapping>
```

#### Implements

[IEquatable<PartyXblEntityIdMapping\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblEntityIdMapping__ctor_System_UInt64_System_String_"></a> PartyXblEntityIdMapping\(ulong, string?\)

Projects <code>PARTY_XBL_XBOX_USER_ID_TO_PLAYFAB_ENTITY_ID_MAPPING</code>.

```csharp
public PartyXblEntityIdMapping(ulong XboxLiveUserId, string? PlayFabEntityId)
```

#### Parameters

`XboxLiveUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox Live user id.

`PlayFabEntityId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The matching PlayFab entity id, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the user has never signed in to
the title.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblEntityIdMapping_PlayFabEntityId"></a> PlayFabEntityId

The matching PlayFab entity id, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the user has never signed in to
the title.

```csharp
public string? PlayFabEntityId { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyXblEntityIdMapping_XboxLiveUserId"></a> XboxLiveUserId

The Xbox Live user id.

```csharp
public ulong XboxLiveUserId { get; init; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

