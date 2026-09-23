# <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived"></a> Class PartyChatTextReceived

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A chat message arrived.

```csharp
public sealed record PartyChatTextReceived : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyChatTextReceived>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyChatTextReceived](GDK.Net.PlayFab.Party.PartyChatTextReceived.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyChatTextReceived\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived__ctor_GDK_Net_PlayFab_Party_PartyChatControl_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyChatControl__System_String_System_String_System_String_System_Byte___System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyTranslation__GDK_Net_PlayFab_Party_PartyChatTextReceivedOptions_System_UInt32_"></a> PartyChatTextReceived\(PartyChatControl?, IReadOnlyList<PartyChatControl\>, string?, string?, string?, byte\[\], IReadOnlyList<PartyTranslation\>, PartyChatTextReceivedOptions, uint\)

A chat message arrived.

```csharp
public PartyChatTextReceived(PartyChatControl? SenderChatControl, IReadOnlyList<PartyChatControl> ReceiverChatControls, string? LanguageCode, string? ChatText, string? OriginalChatText, byte[] Data, IReadOnlyList<PartyTranslation> Translations, PartyChatTextReceivedOptions Options, uint ErrorDetail)
```

#### Parameters

`SenderChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control that sent it.

`ReceiverChatControls` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

The local chat controls it was addressed to.

`LanguageCode` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The language the sender wrote in.

`ChatText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The message text, after any filtering.

`OriginalChatText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The unfiltered text, when filtering changed it.

`Data` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Opaque data the sender attached.

`Translations` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyTranslation](GDK.Net.PlayFab.Party.PartyTranslation.md)\>

Machine translations Party generated.

`Options` [PartyChatTextReceivedOptions](GDK.Net.PlayFab.Party.PartyChatTextReceivedOptions.md)

Details about how the message was processed.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when processing partly failed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_ChatText"></a> ChatText

The message text, after any filtering.

```csharp
public string? ChatText { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_Data"></a> Data

Opaque data the sender attached.

```csharp
public byte[] Data { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when processing partly failed.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_LanguageCode"></a> LanguageCode

The language the sender wrote in.

```csharp
public string? LanguageCode { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_Options"></a> Options

Details about how the message was processed.

```csharp
public PartyChatTextReceivedOptions Options { get; init; }
```

#### Property Value

 [PartyChatTextReceivedOptions](GDK.Net.PlayFab.Party.PartyChatTextReceivedOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_OriginalChatText"></a> OriginalChatText

The unfiltered text, when filtering changed it.

```csharp
public string? OriginalChatText { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_ReceiverChatControls"></a> ReceiverChatControls

The local chat controls it was addressed to.

```csharp
public IReadOnlyList<PartyChatControl> ReceiverChatControls { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_SenderChatControl"></a> SenderChatControl

The chat control that sent it.

```csharp
public PartyChatControl? SenderChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatTextReceived_Translations"></a> Translations

Machine translations Party generated.

```csharp
public IReadOnlyList<PartyTranslation> Translations { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyTranslation](GDK.Net.PlayFab.Party.PartyTranslation.md)\>

