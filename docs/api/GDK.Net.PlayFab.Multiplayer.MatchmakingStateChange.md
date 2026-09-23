# <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChange"></a> Class MatchmakingStateChange

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

One entry from the matchmaking state-change queue (<code>PFMatchmakingStateChange</code>, drained by
<xref href="GDK.Net.PlayFab.Multiplayer.PlayFabMultiplayer.ProcessMatchmakingStateChanges" data-throw-if-not-resolved="false"></xref>).

```csharp
public abstract record MatchmakingStateChange : IEquatable<MatchmakingStateChange>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MatchmakingStateChange](GDK.Net.PlayFab.Multiplayer.MatchmakingStateChange.md)

#### Derived

[MatchmakingTicketCompleted](GDK.Net.PlayFab.Multiplayer.MatchmakingTicketCompleted.md), 
[MatchmakingTicketStatusChanged](GDK.Net.PlayFab.Multiplayer.MatchmakingTicketStatusChanged.md)

#### Implements

[IEquatable<MatchmakingStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChange_ChangeType"></a> ChangeType

The native discriminator, mirroring <code>PFMatchmakingStateChange::stateChangeType</code>.

```csharp
public MatchmakingStateChangeType ChangeType { get; }
```

#### Property Value

 [MatchmakingStateChangeType](GDK.Net.PlayFab.MatchmakingStateChangeType.md)

### <a id="GDK_Net_PlayFab_Multiplayer_MatchmakingStateChange_Ticket"></a> Ticket

The ticket the change is about.

```csharp
public MatchmakingTicket Ticket { get; }
```

#### Property Value

 [MatchmakingTicket](GDK.Net.PlayFab.Multiplayer.MatchmakingTicket.md)

