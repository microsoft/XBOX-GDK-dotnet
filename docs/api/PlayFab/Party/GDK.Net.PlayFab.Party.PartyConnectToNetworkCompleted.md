# <a id="GDK_Net_PlayFab_Party_PartyConnectToNetworkCompleted"></a> Class PartyConnectToNetworkCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <code>PartyManager.ConnectToNetwork</code> call completed.

```csharp
public sealed record PartyConnectToNetworkCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyConnectToNetworkCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyConnectToNetworkCompleted](GDK.Net.PlayFab.Party.PartyConnectToNetworkCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyConnectToNetworkCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Party_PartyConnectToNetworkCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyNetworkDescriptor_GDK_Net_PlayFab_Party_PartyNetwork_"></a> PartyConnectToNetworkCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyNetworkDescriptor, PartyNetwork?\)

A <code>PartyManager.ConnectToNetwork</code> call completed.

```csharp
public PartyConnectToNetworkCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyNetworkDescriptor Descriptor, PartyNetwork? Network)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`Descriptor` [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

The descriptor that was connected to.

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The connected network, when the call succeeded.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyConnectToNetworkCompleted_Descriptor"></a> Descriptor

The descriptor that was connected to.

```csharp
public PartyNetworkDescriptor Descriptor { get; init; }
```

#### Property Value

 [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

### <a id="GDK_Net_PlayFab_Party_PartyConnectToNetworkCompleted_Network"></a> Network

The connected network, when the call succeeded.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

