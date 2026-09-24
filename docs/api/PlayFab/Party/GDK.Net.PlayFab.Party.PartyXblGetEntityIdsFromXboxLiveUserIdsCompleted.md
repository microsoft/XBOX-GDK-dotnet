# <a id="GDK_Net_PlayFab_Party_PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted"></a> Class PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyXblManager.GetEntityIdsFromXboxLiveUserIds(System.Collections.Generic.IReadOnlyList%7bSystem.UInt64%7d%2cGDK.Net.PlayFab.Party.PartyXblChatUser)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted : PartyXblOperationCompleted, IEquatable<PartyXblStateChange>, IEquatable<PartyXblOperationCompleted>, IEquatable<PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblOperationCompleted](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md) ← 
[PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted](GDK.Net.PlayFab.Party.PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Party_PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyXblStateChangeResult_System_UInt32_System_String_GDK_Net_PlayFab_Party_PartyXblChatUser_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyXblEntityIdMapping__"></a> PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted\(PartyOperationId, PartyXblStateChangeResult, uint, string?, PartyXblChatUser?, IReadOnlyList<PartyXblEntityIdMapping\>\)

A <xref href="GDK.Net.PlayFab.Party.PartyXblManager.GetEntityIdsFromXboxLiveUserIds(System.Collections.Generic.IReadOnlyList%7bSystem.UInt64%7d%2cGDK.Net.PlayFab.Party.PartyXblChatUser)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted(PartyOperationId Operation, PartyXblStateChangeResult Result, uint ErrorDetail, string? XboxLiveSandbox, PartyXblChatUser? LocalChatUser, IReadOnlyList<PartyXblEntityIdMapping> Mappings)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyXblStateChangeResult](GDK.Net.PlayFab.Party.PartyXblStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`XboxLiveSandbox` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The sandbox the lookup was performed in.

`LocalChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The chat user that authorized the lookup.

`Mappings` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyXblEntityIdMapping](GDK.Net.PlayFab.Party.PartyXblEntityIdMapping.md)\>

The resolved Xbox Live user id to PlayFab entity id mappings.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted_LocalChatUser"></a> LocalChatUser

The chat user that authorized the lookup.

```csharp
public PartyXblChatUser? LocalChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted_Mappings"></a> Mappings

The resolved Xbox Live user id to PlayFab entity id mappings.

```csharp
public IReadOnlyList<PartyXblEntityIdMapping> Mappings { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyXblEntityIdMapping](GDK.Net.PlayFab.Party.PartyXblEntityIdMapping.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted_XboxLiveSandbox"></a> XboxLiveSandbox

The sandbox the lookup was performed in.

```csharp
public string? XboxLiveSandbox { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

