# <a id="GDK_Net_PlayFab_Party_PartyCreateChatControlCompleted"></a> Class PartyCreateChatControlCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyDevice.CreateChatControl(GDK.Net.PlayFab.Party.PartyLocalUser%2cSystem.String)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyCreateChatControlCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyCreateChatControlCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyCreateChatControlCompleted](GDK.Net.PlayFab.Party.PartyCreateChatControlCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyCreateChatControlCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Party_PartyCreateChatControlCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyDevice_GDK_Net_PlayFab_Party_PartyLocalUser_System_String_GDK_Net_PlayFab_Party_PartyChatControl_"></a> PartyCreateChatControlCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyDevice?, PartyLocalUser?, string?, PartyChatControl?\)

A <xref href="GDK.Net.PlayFab.Party.PartyDevice.CreateChatControl(GDK.Net.PlayFab.Party.PartyLocalUser%2cSystem.String)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyCreateChatControlCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyDevice? LocalDevice, PartyLocalUser? LocalUser, string? LanguageCode, PartyChatControl? ChatControl)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`LocalDevice` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

The device hosting the chat control.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user the chat control speaks for.

`LanguageCode` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The language the chat control was created with.

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The created chat control, when the call succeeded.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyCreateChatControlCompleted_ChatControl"></a> ChatControl

The created chat control, when the call succeeded.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateChatControlCompleted_LanguageCode"></a> LanguageCode

The language the chat control was created with.

```csharp
public string? LanguageCode { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateChatControlCompleted_LocalDevice"></a> LocalDevice

The device hosting the chat control.

```csharp
public PartyDevice? LocalDevice { get; init; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateChatControlCompleted_LocalUser"></a> LocalUser

The user the chat control speaks for.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

