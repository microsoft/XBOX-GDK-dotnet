# <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted"></a> Class PartyCreateNewNetworkCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <code>PartyManager.CreateNewNetwork</code> call completed.

```csharp
public sealed record PartyCreateNewNetworkCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyCreateNewNetworkCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyCreateNewNetworkCompleted](GDK.Net.PlayFab.Party.PartyCreateNewNetworkCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyCreateNewNetworkCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyNetworkConfiguration_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyRegion__GDK_Net_PlayFab_Party_PartyNetworkDescriptor_System_String_"></a> PartyCreateNewNetworkCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyLocalUser?, PartyNetworkConfiguration, IReadOnlyList<PartyRegion\>, PartyNetworkDescriptor, string?\)

A <code>PartyManager.CreateNewNetwork</code> call completed.

```csharp
public PartyCreateNewNetworkCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyLocalUser? LocalUser, PartyNetworkConfiguration Configuration, IReadOnlyList<PartyRegion> Regions, PartyNetworkDescriptor Descriptor, string? AppliedInitialInvitationIdentifier)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user that created the network.

`Configuration` [PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

The network's configuration.

`Regions` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyRegion](GDK.Net.PlayFab.Party.PartyRegion.md)\>

The regions the network was allowed to use.

`Descriptor` [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

The descriptor other devices need in order to connect.

`AppliedInitialInvitationIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The identifier Party gave the initial invitation.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted_AppliedInitialInvitationIdentifier"></a> AppliedInitialInvitationIdentifier

The identifier Party gave the initial invitation.

```csharp
public string? AppliedInitialInvitationIdentifier { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted_Configuration"></a> Configuration

The network's configuration.

```csharp
public PartyNetworkConfiguration Configuration { get; init; }
```

#### Property Value

 [PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted_Descriptor"></a> Descriptor

The descriptor other devices need in order to connect.

```csharp
public PartyNetworkDescriptor Descriptor { get; init; }
```

#### Property Value

 [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

### <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted_LocalUser"></a> LocalUser

The user that created the network.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateNewNetworkCompleted_Regions"></a> Regions

The regions the network was allowed to use.

```csharp
public IReadOnlyList<PartyRegion> Regions { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyRegion](GDK.Net.PlayFab.Party.PartyRegion.md)\>

