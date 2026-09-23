# <a id="GDK_Net_PlayFab_Party_PartyXblLocalChatUserDestroyed"></a> Class PartyXblLocalChatUserDestroyed

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A local chat user was destroyed by the extension.

```csharp
public sealed record PartyXblLocalChatUserDestroyed : PartyXblStateChange, IEquatable<PartyXblStateChange>, IEquatable<PartyXblLocalChatUserDestroyed>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblLocalChatUserDestroyed](GDK.Net.PlayFab.Party.PartyXblLocalChatUserDestroyed.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblLocalChatUserDestroyed\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyXblStateChange.Kind](GDK.Net.PlayFab.Party.PartyXblStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyXblStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblLocalChatUserDestroyed__ctor_GDK_Net_PlayFab_Party_PartyXblChatUser_GDK_Net_PlayFab_Party_PartyXblLocalChatUserDestroyedReason_System_UInt32_"></a> PartyXblLocalChatUserDestroyed\(PartyXblChatUser?, PartyXblLocalChatUserDestroyedReason, uint\)

A local chat user was destroyed by the extension.

```csharp
public PartyXblLocalChatUserDestroyed(PartyXblChatUser? LocalChatUser, PartyXblLocalChatUserDestroyedReason Reason, uint ErrorDetail)
```

#### Parameters

`LocalChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The chat user that was destroyed.

`Reason` [PartyXblLocalChatUserDestroyedReason](GDK.Net.PlayFab.Party.PartyXblLocalChatUserDestroyedReason.md)

Why it was destroyed.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail behind the reason.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblLocalChatUserDestroyed_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail behind the reason.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyXblLocalChatUserDestroyed_LocalChatUser"></a> LocalChatUser

The chat user that was destroyed.

```csharp
public PartyXblChatUser? LocalChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyXblLocalChatUserDestroyed_Reason"></a> Reason

Why it was destroyed.

```csharp
public PartyXblLocalChatUserDestroyedReason Reason { get; init; }
```

#### Property Value

 [PartyXblLocalChatUserDestroyedReason](GDK.Net.PlayFab.Party.PartyXblLocalChatUserDestroyedReason.md)

