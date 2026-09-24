# <a id="GDK_Net_PlayFab_Party_PartyManager"></a> Class PartyManager

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The PlayFab Party library (<code>Party.h</code>): low-latency networking and voice/text chat, driven
by a per-frame state-change pump.

```csharp
public sealed class PartyManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyManager](GDK.Net.PlayFab.Party.PartyManager.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Party operations are not awaitable. A start call returns synchronously with a
<xref href="GDK.Net.PlayFab.Party.PartyOperationId" data-throw-if-not-resolved="false"></xref>, and its completion arrives later as a record from
<xref href="GDK.Net.PlayFab.Party.PartyManager.ProcessStateChanges" data-throw-if-not-resolved="false"></xref>, which must be pumped once per frame from the title's update
thread.
</p>
<p>
Native state-change memory is only valid between the library's <code>StartProcessingStateChanges</code>
and <code>FinishProcessingStateChanges</code> calls, which the enumerator brackets: the batch is
returned when the <code>foreach</code> leaves scope. Each record snapshots the values it exposes, so a
record may safely outlive the loop; object references stay valid until their teardown change
arrives.
</p>

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyManager_ChatControls"></a> ChatControls

Every chat control visible to this device (<code>PartyGetChatControls</code>).

```csharp
public IReadOnlyList<PartyChatControl> ChatControls { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyManager_LocalDevice"></a> LocalDevice

This device (<code>PartyGetLocalDevice</code>).

```csharp
public PartyDevice LocalDevice { get; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)

### <a id="GDK_Net_PlayFab_Party_PartyManager_LocalUsers"></a> LocalUsers

The local users the title has created (<code>PartyGetLocalUsers</code>).

```csharp
public IReadOnlyList<PartyLocalUser> LocalUsers { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyManager_Networks"></a> Networks

The networks this device belongs to (<code>PartyGetNetworks</code>).

```csharp
public IReadOnlyList<PartyNetwork> Networks { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)\>

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyManager_ConnectToNetwork_GDK_Net_PlayFab_Party_PartyNetworkDescriptor_"></a> ConnectToNetwork\(PartyNetworkDescriptor\)

Starts connecting to an existing network (<code>PartyConnectToNetwork</code>). Completion arrives
as <xref href="GDK.Net.PlayFab.Party.PartyConnectToNetworkCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (PartyOperationId Operation, PartyNetwork Network) ConnectToNetwork(PartyNetworkDescriptor descriptor)
```

#### Parameters

`descriptor` [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

The descriptor published by the network's creator.

#### Returns

 \([PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md) Operation, [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md) Network\)

### <a id="GDK_Net_PlayFab_Party_PartyManager_CreateLocalUser_GDK_Net_PlayFab_PlayFabEntity_"></a> CreateLocalUser\(PlayFabEntity\)

Creates a local user from an authenticated PlayFab entity
(<code>PartyCreateLocalUser</code>).

```csharp
public PartyLocalUser CreateLocalUser(PlayFabEntity entity)
```

#### Parameters

`entity` [PlayFabEntity](../GDK.Net.PlayFab.PlayFabEntity.md)

The signed-in PlayFab entity the local user represents.

#### Returns

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

### <a id="GDK_Net_PlayFab_Party_PartyManager_CreateNewNetwork_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyNetworkConfiguration_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyRegion__GDK_Net_PlayFab_Party_PartyInvitationConfiguration_"></a> CreateNewNetwork\(PartyLocalUser, PartyNetworkConfiguration, IReadOnlyList<PartyRegion\>, PartyInvitationConfiguration?\)

Starts creating a new network (<code>PartyCreateNewNetwork</code>). Completion arrives as
<xref href="GDK.Net.PlayFab.Party.PartyCreateNewNetworkCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (PartyOperationId Operation, PartyNetworkDescriptor Descriptor, string InvitationIdentifier) CreateNewNetwork(PartyLocalUser localUser, PartyNetworkConfiguration configuration, IReadOnlyList<PartyRegion> regions, PartyInvitationConfiguration? initialInvitation = null)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user that will own the network.

`configuration` [PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

The network's size and connectivity limits.

`regions` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyRegion](GDK.Net.PlayFab.Party.PartyRegion.md)\>

The Azure regions to consider, in preference order. Pass the list from
<xref href="GDK.Net.PlayFab.Party.PartyManager.GetRegions" data-throw-if-not-resolved="false"></xref> to let Party choose the lowest-latency region.

`initialInvitation` [PartyInvitationConfiguration](GDK.Net.PlayFab.Party.PartyInvitationConfiguration.md)?

The invitation to create alongside the network, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the default
invitation that admits anyone.

#### Returns

 \([PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md) Operation, [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md) Descriptor, [string](https://learn.microsoft.com/dotnet/api/system.string) InvitationIdentifier\)

The operation id, the descriptor to distribute to joiners, and the identifier of the
invitation that was applied.

### <a id="GDK_Net_PlayFab_Party_PartyManager_DestroyLocalUser_GDK_Net_PlayFab_Party_PartyLocalUser_"></a> DestroyLocalUser\(PartyLocalUser\)

Starts destroying a local user (<code>PartyDestroyLocalUser</code>). Completion arrives as
<xref href="GDK.Net.PlayFab.Party.PartyDestroyLocalUserCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public PartyOperationId DestroyLocalUser(PartyLocalUser localUser)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user to destroy.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyManager_Dispose"></a> Dispose\(\)

Shuts the Party library down (<code>PartyCleanup</code>), invalidating every object it produced.

```csharp
public void Dispose()
```

#### Remarks

Bounded like the other native teardowns: the call runs on a background thread and is
abandoned after <xref href="GDK.Net.RuntimeLifetime.TeardownTimeout" data-throw-if-not-resolved="false"></xref> so a stalled network teardown
cannot stop a title's process from exiting. This object is disposed on return either way.

### <a id="GDK_Net_PlayFab_Party_PartyManager_DoWork_GDK_Net_PlayFab_Party_PartyThreadId_"></a> DoWork\(PartyThreadId\)

Performs one slice of work for a thread left in <xref href="GDK.Net.PlayFab.Party.PartyWorkMode.Manual" data-throw-if-not-resolved="false"></xref>
(<code>PartyDoWork</code>).

```csharp
public void DoWork(PartyThreadId threadId)
```

#### Parameters

`threadId` [PartyThreadId](GDK.Net.PlayFab.Party.PartyThreadId.md)

The thread to pump.

### <a id="GDK_Net_PlayFab_Party_PartyManager_GetErrorMessage_System_UInt32_"></a> GetErrorMessage\(uint\)

The library's description of an error code (<code>PartyGetErrorMessage</code>).

```csharp
public static string GetErrorMessage(uint error)
```

#### Parameters

`error` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> value to describe.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyManager_GetLocalUdpSocketBindAddress"></a> GetLocalUdpSocketBindAddress\(\)

The UDP socket configuration Party will bind, or has bound
(<code>PartyGetOption</code> with <code>PartyOption::LocalUdpSocketBindAddress</code>).

```csharp
public static PartyLocalUdpSocketBindAddressConfiguration GetLocalUdpSocketBindAddress()
```

#### Returns

 [PartyLocalUdpSocketBindAddressConfiguration](GDK.Net.PlayFab.Party.PartyLocalUdpSocketBindAddressConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyManager_GetRegions"></a> GetRegions\(\)

The Azure regions available to the title, ordered by measured latency
(<code>PartyGetRegions</code>). The list is empty until the first
<xref href="GDK.Net.PlayFab.Party.PartyRegionsChanged" data-throw-if-not-resolved="false"></xref> state change arrives.

```csharp
public IReadOnlyList<PartyRegion> GetRegions()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyRegion](GDK.Net.PlayFab.Party.PartyRegion.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyManager_GetThreadAffinityMask_GDK_Net_PlayFab_Party_PartyThreadId_"></a> GetThreadAffinityMask\(PartyThreadId\)

The processor affinity mask for one of the library's threads
(<code>PartyGetThreadAffinityMask</code>).

```csharp
public static ulong GetThreadAffinityMask(PartyThreadId threadId)
```

#### Parameters

`threadId` [PartyThreadId](GDK.Net.PlayFab.Party.PartyThreadId.md)

The thread to query.

#### Returns

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_PlayFab_Party_PartyManager_GetWorkMode_GDK_Net_PlayFab_Party_PartyThreadId_"></a> GetWorkMode\(PartyThreadId\)

The work mode of one of the library's threads (<code>PartyGetWorkMode</code>).

```csharp
public static PartyWorkMode GetWorkMode(PartyThreadId threadId)
```

#### Parameters

`threadId` [PartyThreadId](GDK.Net.PlayFab.Party.PartyThreadId.md)

The thread to query.

#### Returns

 [PartyWorkMode](GDK.Net.PlayFab.Party.PartyWorkMode.md)

### <a id="GDK_Net_PlayFab_Party_PartyManager_Initialize_System_String_"></a> Initialize\(string\)

Initializes the Party library for a title (<code>PartyInitialize</code>).

```csharp
public static PartyManager Initialize(string titleId)
```

#### Parameters

`titleId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The PlayFab title id the library authenticates against.

#### Returns

 [PartyManager](GDK.Net.PlayFab.Party.PartyManager.md)

#### Remarks

Last in the fixed startup order (see <xref href="GDK.Net.SubsystemOrder" data-throw-if-not-resolved="false"></xref>) and therefore first to be
torn down, so the Gaming Runtime must already be up. Party also has to be shut down before
the process exits -- leaving it running terminates the process with
<code>STATUS_STACK_BUFFER_OVERRUN</code> (<code>0xC0000409</code>) instead of exiting cleanly -- so the
instance is registered for teardown by <xref href="GDK.Net.GameRuntime.Dispose" data-throw-if-not-resolved="false"></xref> in case the title
does not dispose it itself.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The Gaming Runtime is not initialized.

### <a id="GDK_Net_PlayFab_Party_PartyManager_ProcessStateChanges"></a> ProcessStateChanges\(\)

Drains the state-change queue. Enumerate it once per frame; leaving the <code>foreach</code>
returns the batch to the library (<code>PartyStartProcessingStateChanges</code> /
<code>PartyFinishProcessingStateChanges</code>).

```csharp
public PartyStateChangeCollection ProcessStateChanges()
```

#### Returns

 [PartyStateChangeCollection](GDK.Net.PlayFab.Party.PartyStateChangeCollection.md)

### <a id="GDK_Net_PlayFab_Party_PartyManager_SetLocalUdpSocketBindAddress_GDK_Net_PlayFab_Party_PartyLocalUdpSocketBindAddressConfiguration_"></a> SetLocalUdpSocketBindAddress\(PartyLocalUdpSocketBindAddressConfiguration?\)

Chooses the UDP socket Party binds for peer traffic
(<code>PartySetOption</code> with <code>PartyOption::LocalUdpSocketBindAddress</code>).

```csharp
public static void SetLocalUdpSocketBindAddress(PartyLocalUdpSocketBindAddressConfiguration? configuration)
```

#### Parameters

`configuration` [PartyLocalUdpSocketBindAddressConfiguration](GDK.Net.PlayFab.Party.PartyLocalUdpSocketBindAddressConfiguration.md)?

The bind configuration, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to restore Party's default.

#### Remarks

<p>
This is a process-wide setting and must be applied before <xref href="GDK.Net.PlayFab.Party.PartyManager.Initialize(System.String)" data-throw-if-not-resolved="false"></xref>: Party
binds the socket during initialization, and changing it afterwards has no effect on the
running instance.
</p>
<p>
The default configuration binds a fixed port, so a second Party instance on the same machine
fails to initialize a network with <code>FailedToBindToLocalUdpSocket</code>. Titles do not
normally hit that, but any test that runs several clients on one box does -- give each
process its own port, or pass a configuration with
<xref href="GDK.Net.PlayFab.Party.PartyLocalUdpSocketBindAddressConfiguration.Port" data-throw-if-not-resolved="false"></xref> 0 to let the platform
assign one.
</p>

### <a id="GDK_Net_PlayFab_Party_PartyManager_SetThreadAffinityMask_GDK_Net_PlayFab_Party_PartyThreadId_System_UInt64_"></a> SetThreadAffinityMask\(PartyThreadId, ulong\)

Pins one of the library's internal threads to a set of cores
(<code>PartySetThreadAffinityMask</code>).

```csharp
public static void SetThreadAffinityMask(PartyThreadId threadId, ulong affinityMask)
```

#### Parameters

`threadId` [PartyThreadId](GDK.Net.PlayFab.Party.PartyThreadId.md)

The thread to configure.

`affinityMask` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The processor affinity mask, or zero for no restriction.

### <a id="GDK_Net_PlayFab_Party_PartyManager_SetWorkMode_GDK_Net_PlayFab_Party_PartyThreadId_GDK_Net_PlayFab_Party_PartyWorkMode_"></a> SetWorkMode\(PartyThreadId, PartyWorkMode\)

Chooses whether Party drives one of its threads itself (<code>PartySetWorkMode</code>).

```csharp
public static void SetWorkMode(PartyThreadId threadId, PartyWorkMode workMode)
```

#### Parameters

`threadId` [PartyThreadId](GDK.Net.PlayFab.Party.PartyThreadId.md)

The thread to configure.

`workMode` [PartyWorkMode](GDK.Net.PlayFab.Party.PartyWorkMode.md)

The work mode to apply.

### <a id="GDK_Net_PlayFab_Party_PartyManager_SynchronizeMessagesBetweenEndpoints_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsOptions_"></a> SynchronizeMessagesBetweenEndpoints\(IReadOnlyList<PartyEndpoint\>, PartySynchronizeMessagesBetweenEndpointsOptions\)

Starts a synchronization point across a set of endpoints
(<code>PartySynchronizeMessagesBetweenEndpoints</code>). Completion arrives as
<xref href="GDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public PartyOperationId SynchronizeMessagesBetweenEndpoints(IReadOnlyList<PartyEndpoint> endpoints, PartySynchronizeMessagesBetweenEndpointsOptions options = PartySynchronizeMessagesBetweenEndpointsOptions.None)
```

#### Parameters

`endpoints` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The endpoints to synchronize.

`options` [PartySynchronizeMessagesBetweenEndpointsOptions](GDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsOptions.md)

Which message classes participate in the synchronization.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

