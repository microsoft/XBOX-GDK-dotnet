# <a id="GDK_Net_PlayFab_Party_PartyNetwork"></a> Class PartyNetwork

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_NETWORK_HANDLE</code>: a Party network the local device has joined.

```csharp
public sealed class PartyNetwork : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_ChatControls"></a> ChatControls

The chat controls connected to the network.

```csharp
public IReadOnlyList<PartyChatControl> ChatControls { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_Configuration"></a> Configuration

The network's size and connectivity limits.

```csharp
public PartyNetworkConfiguration Configuration { get; }
```

#### Property Value

 [PartyNetworkConfiguration](GDK.Net.PlayFab.Party.PartyNetworkConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_Descriptor"></a> Descriptor

The descriptor that other devices need in order to connect.

```csharp
public PartyNetworkDescriptor Descriptor { get; }
```

#### Property Value

 [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_Devices"></a> Devices

Every device currently in the network.

```csharp
public IReadOnlyList<PartyDevice> Devices { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_Endpoints"></a> Endpoints

Every endpoint currently in the network.

```csharp
public IReadOnlyList<PartyEndpoint> Endpoints { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_Invitations"></a> Invitations

The invitations currently outstanding for the network.

```csharp
public IReadOnlyList<PartyInvitation> Invitations { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_LocalUsers"></a> LocalUsers

The local users authenticated into the network.

```csharp
public IReadOnlyList<PartyLocalUser> LocalUsers { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)\>

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_AuthenticateLocalUser_GDK_Net_PlayFab_Party_PartyLocalUser_System_String_"></a> AuthenticateLocalUser\(PartyLocalUser, string?\)

Starts authenticating a local user into the network.

```csharp
public PartyOperationId AuthenticateLocalUser(PartyLocalUser localUser, string? invitationIdentifier = null)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user to authenticate.

`invitationIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The invitation that admits the user, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the network's initial
invitation applies.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_ConnectChatControl_GDK_Net_PlayFab_Party_PartyChatControl_"></a> ConnectChatControl\(PartyChatControl\)

Starts connecting a local chat control to the network.

```csharp
public PartyOperationId ConnectChatControl(PartyChatControl chatControl)
```

#### Parameters

`chatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The chat control to connect.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_CreateEndpoint_GDK_Net_PlayFab_Party_PartyLocalUser_System_Collections_Generic_IReadOnlyDictionary_System_String_System_Byte____"></a> CreateEndpoint\(PartyLocalUser?, IReadOnlyDictionary<string, byte\[\]?\>?\)

Starts creating a messaging endpoint in the network.

```csharp
public PartyOperationId CreateEndpoint(PartyLocalUser? localUser, IReadOnlyDictionary<string, byte[]?>? initialProperties = null)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The local user that owns the endpoint, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for a device endpoint.

`initialProperties` [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?\>?

Properties published with the endpoint.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_CreateInvitation_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyInvitationConfiguration_"></a> CreateInvitation\(PartyLocalUser, PartyInvitationConfiguration\)

Starts creating an additional invitation to the network.

```csharp
public PartyOperationId CreateInvitation(PartyLocalUser localUser, PartyInvitationConfiguration configuration)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user that owns the invitation.

`configuration` [PartyInvitationConfiguration](GDK.Net.PlayFab.Party.PartyInvitationConfiguration.md)

The invitation's identifier, revocability and allow list.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_DestroyEndpoint_GDK_Net_PlayFab_Party_PartyEndpoint_"></a> DestroyEndpoint\(PartyEndpoint\)

Starts destroying a local endpoint.

```csharp
public PartyOperationId DestroyEndpoint(PartyEndpoint endpoint)
```

#### Parameters

`endpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)

The endpoint to destroy.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_DisconnectChatControl_GDK_Net_PlayFab_Party_PartyChatControl_"></a> DisconnectChatControl\(PartyChatControl\)

Starts disconnecting a local chat control from the network.

```csharp
public PartyOperationId DisconnectChatControl(PartyChatControl chatControl)
```

#### Parameters

`chatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The chat control to disconnect.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_FindEndpoint_System_UInt16_"></a> FindEndpoint\(ushort\)

Looks up an endpoint by its per-network unique identifier.

```csharp
public PartyEndpoint? FindEndpoint(ushort uniqueIdentifier)
```

#### Parameters

`uniqueIdentifier` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

The identifier from <xref href="GDK.Net.PlayFab.Party.PartyEndpoint.UniqueIdentifier" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The endpoint, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when no endpoint matches.

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_GetDeviceConnectionType_GDK_Net_PlayFab_Party_PartyDevice_"></a> GetDeviceConnectionType\(PartyDevice\)

How the local device reaches a remote device in this network.

```csharp
public PartyDeviceConnectionType GetDeviceConnectionType(PartyDevice device)
```

#### Parameters

`device` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)

The remote device.

#### Returns

 [PartyDeviceConnectionType](GDK.Net.PlayFab.Party.PartyDeviceConnectionType.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_GetEndpoints_GDK_Net_PlayFab_Party_PartyEndpointUserTypeFilter_GDK_Net_PlayFab_Party_PartyEndpointLocationFilter_"></a> GetEndpoints\(PartyEndpointUserTypeFilter, PartyEndpointLocationFilter\)

Filters the network's endpoints by owner and location.

```csharp
public IReadOnlyList<PartyEndpoint> GetEndpoints(PartyEndpointUserTypeFilter userTypeFilter, PartyEndpointLocationFilter locationFilter)
```

#### Parameters

`userTypeFilter` [PartyEndpointUserTypeFilter](GDK.Net.PlayFab.Party.PartyEndpointUserTypeFilter.md)

Whether to include user endpoints, device endpoints or both.

`locationFilter` [PartyEndpointLocationFilter](GDK.Net.PlayFab.Party.PartyEndpointLocationFilter.md)

Whether to include local endpoints, remote endpoints or both.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_GetSharedProperty_System_String_"></a> GetSharedProperty\(string\)

Reads a shared property, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it is not set.

```csharp
public byte[]? GetSharedProperty(string key)
```

#### Parameters

`key` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property key.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_GetSharedPropertyKeys"></a> GetSharedPropertyKeys\(\)

The keys of every property shared on the network.

```csharp
public IReadOnlyList<string> GetSharedPropertyKeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_GetStatistics_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyNetworkStatistic__"></a> GetStatistics\(IReadOnlyList<PartyNetworkStatistic\>\)

Reads network-wide statistics.

```csharp
public IReadOnlyDictionary<PartyNetworkStatistic, ulong> GetStatistics(IReadOnlyList<PartyNetworkStatistic> statistics)
```

#### Parameters

`statistics` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyNetworkStatistic](GDK.Net.PlayFab.Party.PartyNetworkStatistic.md)\>

The statistics to read.

#### Returns

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[PartyNetworkStatistic](GDK.Net.PlayFab.Party.PartyNetworkStatistic.md), [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_KickDevice_GDK_Net_PlayFab_Party_PartyDevice_"></a> KickDevice\(PartyDevice\)

Starts removing a device from the network.

```csharp
public PartyOperationId KickDevice(PartyDevice device)
```

#### Parameters

`device` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)

The device to kick.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_KickUser_System_String_"></a> KickUser\(string\)

Starts removing a user from the network.

```csharp
public PartyOperationId KickUser(string entityId)
```

#### Parameters

`entityId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The PlayFab entity id of the user to kick.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_Leave"></a> Leave\(\)

Starts leaving the network.

```csharp
public PartyOperationId Leave()
```

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_RemoveLocalUser_GDK_Net_PlayFab_Party_PartyLocalUser_"></a> RemoveLocalUser\(PartyLocalUser\)

Starts removing a local user from the network.

```csharp
public PartyOperationId RemoveLocalUser(PartyLocalUser localUser)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user to remove.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_RevokeInvitation_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyInvitation_"></a> RevokeInvitation\(PartyLocalUser, PartyInvitation\)

Starts revoking an invitation to the network.

```csharp
public PartyOperationId RevokeInvitation(PartyLocalUser localUser, PartyInvitation invitation)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user revoking the invitation.

`invitation` [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)

The invitation to revoke.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetwork_SetSharedProperties_System_Collections_Generic_IReadOnlyDictionary_System_String_System_Byte____"></a> SetSharedProperties\(IReadOnlyDictionary<string, byte\[\]?\>\)

Sets or, for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> value, removes shared properties.

```csharp
public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties)
```

#### Parameters

`properties` [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?\>

The properties to write.

