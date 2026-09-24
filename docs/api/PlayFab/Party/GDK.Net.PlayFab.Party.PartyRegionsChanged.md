# <a id="GDK_Net_PlayFab_Party_PartyRegionsChanged"></a> Class PartyRegionsChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Party finished measuring latency to the Azure regions.

```csharp
public sealed record PartyRegionsChanged : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyRegionsChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyRegionsChanged](GDK.Net.PlayFab.Party.PartyRegionsChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyRegionsChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyOperationCompleted.Operation](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Operation), 
[PartyOperationCompleted.Result](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Result), 
[PartyOperationCompleted.ErrorDetail](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_ErrorDetail), 
[PartyOperationCompleted.Succeeded](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Succeeded), 
[PartyOperationCompleted.Error](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Error), 
[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyRegionsChanged__ctor_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyRegion__"></a> PartyRegionsChanged\(PartyStateChangeResult, uint, IReadOnlyList<PartyRegion\>\)

Party finished measuring latency to the Azure regions.

```csharp
public PartyRegionsChanged(PartyStateChangeResult Result, uint ErrorDetail, IReadOnlyList<PartyRegion> Regions)
```

#### Parameters

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the measurement succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`Regions` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyRegion](GDK.Net.PlayFab.Party.PartyRegion.md)\>

The regions and their measured latencies.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRegionsChanged_Regions"></a> Regions

The regions and their measured latencies.

```csharp
public IReadOnlyList<PartyRegion> Regions { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyRegion](GDK.Net.PlayFab.Party.PartyRegion.md)\>

