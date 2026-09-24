# <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted"></a> Class PartyXblOperationCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

An Xbox Live extension state change that completes an operation.

```csharp
public abstract record PartyXblOperationCompleted : PartyXblStateChange, IEquatable<PartyXblStateChange>, IEquatable<PartyXblOperationCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblOperationCompleted](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md)

#### Derived

[PartyXblCreateLocalChatUserCompleted](GDK.Net.PlayFab.Party.PartyXblCreateLocalChatUserCompleted.md), 
[PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted](GDK.Net.PlayFab.Party.PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted.md), 
[PartyXblLoginToPlayFabCompleted](GDK.Net.PlayFab.Party.PartyXblLoginToPlayFabCompleted.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyXblStateChange.Kind](GDK.Net.PlayFab.Party.PartyXblStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyXblStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted__ctor_GDK_Net_PlayFab_Party_PartyXblStateChangeType_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyXblStateChangeResult_System_UInt32_"></a> PartyXblOperationCompleted\(PartyXblStateChangeType, PartyOperationId, PartyXblStateChangeResult, uint\)

An Xbox Live extension state change that completes an operation.

```csharp
protected PartyXblOperationCompleted(PartyXblStateChangeType Kind, PartyOperationId Operation, PartyXblStateChangeResult Result, uint ErrorDetail)
```

#### Parameters

`Kind` [PartyXblStateChangeType](GDK.Net.PlayFab.Party.PartyXblStateChangeType.md)

The discriminator matching the native <code>PartyXblStateChangeType</code>.

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the matching start call.

`Result` [PartyXblStateChangeResult](GDK.Net.PlayFab.Party.PartyXblStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the operation failed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted_Error"></a> Error

The failure, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the operation succeeded.

```csharp
public Exception? Error { get; }
```

#### Property Value

 [Exception](https://learn.microsoft.com/dotnet/api/system.exception)?

### <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the operation failed.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted_Operation"></a> Operation

The id returned by the matching start call.

```csharp
public PartyOperationId Operation { get; init; }
```

#### Property Value

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted_Result"></a> Result

Whether the operation succeeded.

```csharp
public PartyXblStateChangeResult Result { get; init; }
```

#### Property Value

 [PartyXblStateChangeResult](GDK.Net.PlayFab.Party.PartyXblStateChangeResult.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblOperationCompleted_Succeeded"></a> Succeeded

Whether the operation succeeded.

```csharp
public bool Succeeded { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

