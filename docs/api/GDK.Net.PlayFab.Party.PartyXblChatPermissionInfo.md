# <a id="GDK_Net_PlayFab_Party_PartyXblChatPermissionInfo"></a> Struct PartyXblChatPermissionInfo

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_XBL_CHAT_PERMISSION_INFO</code>: what one user is permitted to hear or say to
another, and why.

```csharp
public readonly record struct PartyXblChatPermissionInfo : IEquatable<PartyXblChatPermissionInfo>
```

#### Implements

[IEquatable<PartyXblChatPermissionInfo\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblChatPermissionInfo__ctor_GDK_Net_PlayFab_Party_PartyChatPermissionOptions_GDK_Net_PlayFab_Party_PartyXblChatPermissionMaskReason_"></a> PartyXblChatPermissionInfo\(PartyChatPermissionOptions, PartyXblChatPermissionMaskReason\)

Projects <code>PARTY_XBL_CHAT_PERMISSION_INFO</code>: what one user is permitted to hear or say to
another, and why.

```csharp
public PartyXblChatPermissionInfo(PartyChatPermissionOptions ChatPermissionMask, PartyXblChatPermissionMaskReason Reason)
```

#### Parameters

`ChatPermissionMask` [PartyChatPermissionOptions](GDK.Net.PlayFab.Party.PartyChatPermissionOptions.md)

The permitted chat directions.

`Reason` [PartyXblChatPermissionMaskReason](GDK.Net.PlayFab.Party.PartyXblChatPermissionMaskReason.md)

Why the mask is restricted.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblChatPermissionInfo_ChatPermissionMask"></a> ChatPermissionMask

The permitted chat directions.

```csharp
public PartyChatPermissionOptions ChatPermissionMask { get; init; }
```

#### Property Value

 [PartyChatPermissionOptions](GDK.Net.PlayFab.Party.PartyChatPermissionOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblChatPermissionInfo_Reason"></a> Reason

Why the mask is restricted.

```csharp
public PartyXblChatPermissionMaskReason Reason { get; init; }
```

#### Property Value

 [PartyXblChatPermissionMaskReason](GDK.Net.PlayFab.Party.PartyXblChatPermissionMaskReason.md)

