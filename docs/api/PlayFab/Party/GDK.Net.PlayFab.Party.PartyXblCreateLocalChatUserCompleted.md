# <a id="GDK_Net_PlayFab_Party_PartyXblCreateLocalChatUserCompleted"></a> Class PartyXblCreateLocalChatUserCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyXblManager.CreateLocalChatUser(System.UInt64)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyXblCreateLocalChatUserCompleted : PartyXblOperationCompleted, IEquatable<PartyXblStateChange>, IEquatable<PartyXblOperationCompleted>, IEquatable<PartyXblCreateLocalChatUserCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblOperationCompleted](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md) ← 
[PartyXblCreateLocalChatUserCompleted](GDK.Net.PlayFab.Party.PartyXblCreateLocalChatUserCompleted.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblCreateLocalChatUserCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyXblOperationCompleted.Operation](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Operation), 
[PartyXblOperationCompleted.Result](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Result), 
[PartyXblOperationCompleted.ErrorDetail](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_ErrorDetail), 
[PartyXblOperationCompleted.Succeeded](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Succeeded), 
[PartyXblOperationCompleted.Error](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Error), 
[PartyXblStateChange.Kind](GDK.Net.PlayFab.Party.PartyXblStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyXblStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblCreateLocalChatUserCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyXblStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyXblChatUser_"></a> PartyXblCreateLocalChatUserCompleted\(PartyOperationId, PartyXblStateChangeResult, uint, PartyXblChatUser?\)

A <xref href="GDK.Net.PlayFab.Party.PartyXblManager.CreateLocalChatUser(System.UInt64)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyXblCreateLocalChatUserCompleted(PartyOperationId Operation, PartyXblStateChangeResult Result, uint ErrorDetail, PartyXblChatUser? LocalChatUser)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyXblStateChangeResult](GDK.Net.PlayFab.Party.PartyXblStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`LocalChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The chat user that was created.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblCreateLocalChatUserCompleted_LocalChatUser"></a> LocalChatUser

The chat user that was created.

```csharp
public PartyXblChatUser? LocalChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

