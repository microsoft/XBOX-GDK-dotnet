# <a id="GDK_Net_PlayFab_Party_PartyConfigureAudioManipulationVoiceStreamCompleted"></a> Class PartyConfigureAudioManipulationVoiceStreamCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyChatControl.ConfigureAudioManipulationVoiceStream(GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyConfigureAudioManipulationVoiceStreamCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyConfigureAudioManipulationVoiceStreamCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyConfigureAudioManipulationVoiceStreamCompleted](GDK.Net.PlayFab.Party.PartyConfigureAudioManipulationVoiceStreamCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyConfigureAudioManipulationVoiceStreamCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Party_PartyConfigureAudioManipulationVoiceStreamCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyChatControl_GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStreamConfiguration_"></a> PartyConfigureAudioManipulationVoiceStreamCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyChatControl?, PartyAudioManipulationSourceStreamConfiguration?\)

A <xref href="GDK.Net.PlayFab.Party.PartyChatControl.ConfigureAudioManipulationVoiceStream(GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyConfigureAudioManipulationVoiceStreamCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyChatControl? ChatControl, PartyAudioManipulationSourceStreamConfiguration? Configuration)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control.

`Configuration` [PartyAudioManipulationSourceStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration.md)?

The configuration that was requested, if any.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyConfigureAudioManipulationVoiceStreamCompleted_ChatControl"></a> ChatControl

The chat control.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyConfigureAudioManipulationVoiceStreamCompleted_Configuration"></a> Configuration

The configuration that was requested, if any.

```csharp
public PartyAudioManipulationSourceStreamConfiguration? Configuration { get; init; }
```

#### Property Value

 [PartyAudioManipulationSourceStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration.md)?

