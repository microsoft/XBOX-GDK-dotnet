# <a id="GDK_Net_PlayFab_Party_PartyXblManager"></a> Class PartyXblManager

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The Party Xbox Live extension (<code>PartyXboxLive.h</code>): maps Xbox Live users onto PlayFab
entities and supplies the chat permissions Xbox Live requires.

```csharp
public sealed class PartyXblManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblManager](GDK.Net.PlayFab.Party.PartyXblManager.md)

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

Like <xref href="GDK.Net.PlayFab.Party.PartyManager" data-throw-if-not-resolved="false"></xref>, this library is poll-driven: operations start synchronously and
complete on a later <xref href="GDK.Net.PlayFab.Party.PartyXblManager.ProcessStateChanges" data-throw-if-not-resolved="false"></xref> pump.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_ChatUsers"></a> ChatUsers

The chat users this device knows about (<code>PartyXblGetChatUsers</code>).

```csharp
public IReadOnlyList<PartyXblChatUser> ChatUsers { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)\>

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_CompleteGetTokenAndSignatureRequest_System_UInt32_System_String_System_String_"></a> CompleteGetTokenAndSignatureRequest\(uint, string?, string?\)

Answers a <xref href="GDK.Net.PlayFab.Party.PartyXblTokenAndSignatureRequested" data-throw-if-not-resolved="false"></xref> state change
(<code>PartyXblCompleteGetTokenAndSignatureRequest</code>).

```csharp
public void CompleteGetTokenAndSignatureRequest(uint correlationId, string? token, string? signature)
```

#### Parameters

`correlationId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The id from the state change being answered.

`token` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The Xbox Live token, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> on failure.

`signature` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The request signature, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> on failure.

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_CreateLocalChatUser_System_UInt64_"></a> CreateLocalChatUser\(ulong\)

Starts creating a chat user for a signed-in Xbox Live user
(<code>PartyXblCreateLocalChatUser</code>). Completion arrives as
<xref href="GDK.Net.PlayFab.Party.PartyXblCreateLocalChatUserCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public (PartyOperationId Operation, PartyXblChatUser ChatUser) CreateLocalChatUser(ulong xboxUserId)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox Live user id.

#### Returns

 \([PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md) Operation, [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md) ChatUser\)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_CreateRemoteChatUser_System_UInt64_"></a> CreateRemoteChatUser\(ulong\)

Creates a chat user for a remote Xbox Live user (<code>PartyXblCreateRemoteChatUser</code>).

```csharp
public PartyXblChatUser CreateRemoteChatUser(ulong xboxUserId)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox Live user id.

#### Returns

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_DestroyChatUser_GDK_Net_PlayFab_Party_PartyXblChatUser_"></a> DestroyChatUser\(PartyXblChatUser\)

Destroys a chat user (<code>PartyXblDestroyChatUser</code>).

```csharp
public void DestroyChatUser(PartyXblChatUser chatUser)
```

#### Parameters

`chatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)

The chat user to destroy.

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_Dispose"></a> Dispose\(\)

Shuts the extension down (<code>PartyXblCleanup</code>), invalidating every chat user it produced.

```csharp
public void Dispose()
```

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_GetEntityIdsFromXboxLiveUserIds_System_Collections_Generic_IReadOnlyList_System_UInt64__GDK_Net_PlayFab_Party_PartyXblChatUser_"></a> GetEntityIdsFromXboxLiveUserIds\(IReadOnlyList<ulong\>, PartyXblChatUser\)

Starts resolving Xbox Live user ids to PlayFab entity ids
(<code>PartyXblGetEntityIdsFromXboxLiveUserIds</code>). Completion arrives as
<xref href="GDK.Net.PlayFab.Party.PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted" data-throw-if-not-resolved="false"></xref>.

```csharp
public PartyOperationId GetEntityIdsFromXboxLiveUserIds(IReadOnlyList<ulong> xboxLiveUserIds, PartyXblChatUser localChatUser)
```

#### Parameters

`xboxLiveUserIds` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

The Xbox Live user ids to resolve.

`localChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)

The local chat user whose token authorizes the lookup.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_GetErrorMessage_System_UInt32_"></a> GetErrorMessage\(uint\)

The extension's description of an error code (<code>PartyXblGetErrorMessage</code>).

```csharp
public static string GetErrorMessage(uint error)
```

#### Parameters

`error` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> value to describe.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_GetThreadAffinityMask_GDK_Net_PlayFab_Party_PartyXblThreadId_"></a> GetThreadAffinityMask\(PartyXblThreadId\)

The processor affinity mask of the extension's web request thread
(<code>PartyXblGetThreadAffinityMask</code>).

```csharp
public static ulong GetThreadAffinityMask(PartyXblThreadId threadId)
```

#### Parameters

`threadId` [PartyXblThreadId](GDK.Net.PlayFab.Party.PartyXblThreadId.md)

The thread to query.

#### Returns

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_Initialize_GDK_Net_PlayFab_Party_PartyManager_System_String_"></a> Initialize\(PartyManager, string\)

Initializes the Xbox Live extension over an initialized Party library
(<code>PartyXblInitialize</code>).

```csharp
public static PartyXblManager Initialize(PartyManager party, string titleId)
```

#### Parameters

`party` [PartyManager](GDK.Net.PlayFab.Party.PartyManager.md)

The Party library instance the extension attaches to.

`titleId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The PlayFab title id.

#### Returns

 [PartyXblManager](GDK.Net.PlayFab.Party.PartyXblManager.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_ProcessStateChanges"></a> ProcessStateChanges\(\)

Drains the extension's state-change queue. Enumerate it once per frame; leaving the
<code>foreach</code> returns the batch (<code>PartyXblStartProcessingStateChanges</code> /
<code>PartyXblFinishProcessingStateChanges</code>).

```csharp
public PartyXblStateChangeCollection ProcessStateChanges()
```

#### Returns

 [PartyXblStateChangeCollection](GDK.Net.PlayFab.Party.PartyXblStateChangeCollection.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblManager_SetThreadAffinityMask_GDK_Net_PlayFab_Party_PartyXblThreadId_System_UInt64_"></a> SetThreadAffinityMask\(PartyXblThreadId, ulong\)

Pins the extension's web request thread to a set of cores
(<code>PartyXblSetThreadAffinityMask</code>).

```csharp
public static void SetThreadAffinityMask(PartyXblThreadId threadId, ulong affinityMask)
```

#### Parameters

`threadId` [PartyXblThreadId](GDK.Net.PlayFab.Party.PartyXblThreadId.md)

The thread to configure.

`affinityMask` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The processor affinity mask, or zero for no restriction.

