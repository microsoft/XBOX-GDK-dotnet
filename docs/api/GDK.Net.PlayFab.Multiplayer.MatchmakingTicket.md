# <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket"></a> Class MatchmakingTicket

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

A matchmaking ticket (<code>PFMatchmakingTicketHandle</code>). Instances are identity-mapped by their
owning <xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class MatchmakingTicket
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The ticket stays usable until <xref href="GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.Destroy" data-throw-if-not-resolved="false"></xref> is called; the multiplayer library owns the
handle, so there is nothing else to release. Progress arrives as
<xref href="GDK.Net.PlayFab.Multiplayer.MatchmakingTicketStatusChanged" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.PlayFab.Multiplayer.MatchmakingTicketCompleted" data-throw-if-not-resolved="false"></xref> on the
matchmaking pump.

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket_IsValid"></a> IsValid

Whether the ticket is still usable.

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket_Match"></a> Match

The match this ticket produced, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> while matchmaking is still running
(<code>PFMatchmakingTicketGetMatch</code>).

```csharp
public MatchmakingMatchDetails? Match { get; }
```

#### Property Value

 [MatchmakingMatchDetails](GDK.Net.PlayFab.MatchmakingMatchDetails.md)?

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket_Status"></a> Status

Where the ticket is in the matchmaking flow (<code>PFMatchmakingTicketGetStatus</code>).

```csharp
public MatchmakingTicketStatus Status { get; }
```

#### Property Value

 [MatchmakingTicketStatus](GDK.Net.PlayFab.MatchmakingTicketStatus.md)

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket_TicketId"></a> TicketId

The service-assigned ticket id, available once the ticket has been created
(<code>PFMatchmakingTicketGetTicketId</code>).

```csharp
public string? TicketId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket_Cancel"></a> Cancel\(\)

Cancels matchmaking for this ticket (<code>PFMatchmakingTicketCancel</code>).

```csharp
public void Cancel()
```

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingTicket_Destroy"></a> Destroy\(\)

Releases the ticket (<code>PFMultiplayerDestroyMatchmakingTicket</code>). Later use of this
instance throws <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref>.

```csharp
public void Destroy()
```

