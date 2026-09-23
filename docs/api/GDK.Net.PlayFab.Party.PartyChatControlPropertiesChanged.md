# <a id="GDK_Net_PlayFab_Party_PartyChatControlPropertiesChanged"></a> Class PartyChatControlPropertiesChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Properties shared on a chat control changed.

```csharp
public sealed record PartyChatControlPropertiesChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyChatControlPropertiesChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyChatControlPropertiesChanged](GDK.Net.PlayFab.Party.PartyChatControlPropertiesChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyChatControlPropertiesChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyChatControlPropertiesChanged__ctor_GDK_Net_PlayFab_Party_PartyChatControl_System_Collections_Generic_IReadOnlyList_System_String__"></a> PartyChatControlPropertiesChanged\(PartyChatControl?, IReadOnlyList<string\>\)

Properties shared on a chat control changed.

```csharp
public PartyChatControlPropertiesChanged(PartyChatControl? ChatControl, IReadOnlyList<string> Keys)
```

#### Parameters

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control.

`Keys` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The keys whose values changed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyChatControlPropertiesChanged_ChatControl"></a> ChatControl

The chat control.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControlPropertiesChanged_Keys"></a> Keys

The keys whose values changed.

```csharp
public IReadOnlyList<string> Keys { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

