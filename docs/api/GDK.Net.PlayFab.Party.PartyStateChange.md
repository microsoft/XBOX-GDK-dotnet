# <a id="GDK_Net_PlayFab_Party_PartyStateChange"></a> Class PartyStateChange

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

One entry from a <xref href="GDK.Net.PlayFab.Party.PartyManager.ProcessStateChanges" data-throw-if-not-resolved="false"></xref> batch.

```csharp
public abstract record PartyStateChange : IEquatable<PartyStateChange>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md)

#### Derived

[PartyChatControlCreated](GDK.Net.PlayFab.Party.PartyChatControlCreated.md), 
[PartyChatControlDestroyed](GDK.Net.PlayFab.Party.PartyChatControlDestroyed.md), 
[PartyChatControlJoinedNetwork](GDK.Net.PlayFab.Party.PartyChatControlJoinedNetwork.md), 
[PartyChatControlLeftNetwork](GDK.Net.PlayFab.Party.PartyChatControlLeftNetwork.md), 
[PartyChatControlPropertiesChanged](GDK.Net.PlayFab.Party.PartyChatControlPropertiesChanged.md), 
[PartyChatTextReceived](GDK.Net.PlayFab.Party.PartyChatTextReceived.md), 
[PartyDataBuffersReturned](GDK.Net.PlayFab.Party.PartyDataBuffersReturned.md), 
[PartyDevicePropertiesChanged](GDK.Net.PlayFab.Party.PartyDevicePropertiesChanged.md), 
[PartyEndpointCreated](GDK.Net.PlayFab.Party.PartyEndpointCreated.md), 
[PartyEndpointDestroyed](GDK.Net.PlayFab.Party.PartyEndpointDestroyed.md), 
[PartyEndpointMessageReceived](GDK.Net.PlayFab.Party.PartyEndpointMessageReceived.md), 
[PartyEndpointPropertiesChanged](GDK.Net.PlayFab.Party.PartyEndpointPropertiesChanged.md), 
[PartyInvitationCreated](GDK.Net.PlayFab.Party.PartyInvitationCreated.md), 
[PartyInvitationDestroyed](GDK.Net.PlayFab.Party.PartyInvitationDestroyed.md), 
[PartyLocalChatAudioInputChanged](GDK.Net.PlayFab.Party.PartyLocalChatAudioInputChanged.md), 
[PartyLocalChatAudioOutputChanged](GDK.Net.PlayFab.Party.PartyLocalChatAudioOutputChanged.md), 
[PartyLocalUserKicked](GDK.Net.PlayFab.Party.PartyLocalUserKicked.md), 
[PartyLocalUserRemoved](GDK.Net.PlayFab.Party.PartyLocalUserRemoved.md), 
[PartyNetworkConfigurationMadeAvailable](GDK.Net.PlayFab.Party.PartyNetworkConfigurationMadeAvailable.md), 
[PartyNetworkDescriptorChanged](GDK.Net.PlayFab.Party.PartyNetworkDescriptorChanged.md), 
[PartyNetworkDestroyed](GDK.Net.PlayFab.Party.PartyNetworkDestroyed.md), 
[PartyNetworkPropertiesChanged](GDK.Net.PlayFab.Party.PartyNetworkPropertiesChanged.md), 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md), 
[PartyRemoteDeviceCreated](GDK.Net.PlayFab.Party.PartyRemoteDeviceCreated.md), 
[PartyRemoteDeviceDestroyed](GDK.Net.PlayFab.Party.PartyRemoteDeviceDestroyed.md), 
[PartyRemoteDeviceJoinedNetwork](GDK.Net.PlayFab.Party.PartyRemoteDeviceJoinedNetwork.md), 
[PartyRemoteDeviceLeftNetwork](GDK.Net.PlayFab.Party.PartyRemoteDeviceLeftNetwork.md), 
[PartySynchronizeMessagesBetweenEndpointsCompleted](GDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsCompleted.md), 
[PartyVoiceChatTranscriptionReceived](GDK.Net.PlayFab.Party.PartyVoiceChatTranscriptionReceived.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Party reports everything — operation completions, membership changes, chat and messaging —
through a single poll-drain queue, so the loop is the public surface. Every record snapshots
its values, so it stays valid after the batch is returned to Party; the Party objects it
references are invalidated when their teardown change is processed.

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyStateChange__ctor_GDK_Net_PlayFab_Party_PartyStateChangeType_"></a> PartyStateChange\(PartyStateChangeType\)

One entry from a <xref href="GDK.Net.PlayFab.Party.PartyManager.ProcessStateChanges" data-throw-if-not-resolved="false"></xref> batch.

```csharp
protected PartyStateChange(PartyStateChangeType Kind)
```

#### Parameters

`Kind` [PartyStateChangeType](GDK.Net.PlayFab.Party.PartyStateChangeType.md)

The discriminator matching the native <code>PartyStateChangeType</code>.

#### Remarks

Party reports everything — operation completions, membership changes, chat and messaging —
through a single poll-drain queue, so the loop is the public surface. Every record snapshots
its values, so it stays valid after the batch is returned to Party; the Party objects it
references are invalidated when their teardown change is processed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyStateChange_Kind"></a> Kind

The discriminator matching the native <code>PartyStateChangeType</code>.

```csharp
public PartyStateChangeType Kind { get; init; }
```

#### Property Value

 [PartyStateChangeType](GDK.Net.PlayFab.Party.PartyStateChangeType.md)

