# <a id="GDK_Net_XboxLive_LeaderboardQueryType"></a> Enum LeaderboardQueryType

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

The backing store used by a leaderboard query. Mirrors <code>XblLeaderboardQueryType</code>.

```csharp
public enum LeaderboardQueryType : uint
```

## Fields

`TitleManagedStatBackedGlobal = 1` 

A global leaderboard backed by a title-managed stat.



`TitleManagedStatBackedSocial = 2` 

A social leaderboard backed by a title-managed stat.



`UserStatBacked = 0` 

An event-based user-stat leaderboard.



