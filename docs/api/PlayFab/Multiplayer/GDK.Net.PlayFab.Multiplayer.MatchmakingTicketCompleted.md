# <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicketCompleted"></a> Class MatchmakingTicketCompleted

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

The ticket reached a terminal state, with a match or a failure.

```csharp
public sealed record MatchmakingTicketCompleted : MatchmakingStateChange, IEquatable<MatchmakingStateChange>, IEquatable<MatchmakingTicketCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MatchmakingStateChange](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChange.md) ← 
[MatchmakingTicketCompleted](GDK.Net.PlayFab.Multiplayer.MatchmakingTicketCompleted.md)

#### Implements

[IEquatable<MatchmakingStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<MatchmakingTicketCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[MatchmakingStateChange.ChangeType](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_MatchmakingStateChange\_ChangeType), 
[MatchmakingStateChange.Ticket](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChange.md\#GDK\_Net\_PlayFab\_Multiplayer\_MatchmakingStateChange\_Ticket), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicketCompleted_Error"></a> Error

The failure, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when a match was found.

```csharp
public Exception? Error { get; }
```

#### Property Value

 [Exception](https://learn.microsoft.com/dotnet/api/system.exception)?

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicketCompleted_Failed"></a> Failed

Whether matchmaking failed.

```csharp
public bool Failed { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicketCompleted_Operation"></a> Operation

The id returned when the ticket was created.

```csharp
public OperationId Operation { get; }
```

#### Property Value

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicketCompleted_ResultCode"></a> ResultCode

The raw native HRESULT.

```csharp
public int ResultCode { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

