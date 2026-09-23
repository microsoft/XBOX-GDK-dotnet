# <a id="GDK_Net_XboxLive_MultiplayerActivityInfo"></a> Class MultiplayerActivityInfo

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Multiplayer activity information. Managed snapshot of <code>XblMultiplayerActivityInfo</code>.

```csharp
public sealed class MultiplayerActivityInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MultiplayerActivityInfo](GDK.Net.XboxLive.MultiplayerActivityInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

When used with <xref href="GDK.Net.XboxLive.MultiplayerActivityService.SetActivityAsync(GDK.Net.XboxLive.MultiplayerActivityInfo%2cSystem.Boolean%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>, the
<xref href="GDK.Net.XboxLive.MultiplayerActivityInfo.ConnectionString" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.XboxLive.MultiplayerActivityInfo.GroupId" data-throw-if-not-resolved="false"></xref> must describe the joinable activity
accurately. The advertised activity is what lets friends join; stale or orphaned activity data
is a certification failure.

## Constructors

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo__ctor_System_UInt64_System_String_GDK_Net_XboxLive_MultiplayerActivityJoinRestriction_System_UInt32_System_UInt32_System_String_GDK_Net_XboxLive_MultiplayerActivityPlatform_"></a> MultiplayerActivityInfo\(ulong, string?, MultiplayerActivityJoinRestriction, uint, uint, string?, MultiplayerActivityPlatform\)

Creates multiplayer activity information.

```csharp
public MultiplayerActivityInfo(ulong xboxUserId, string? connectionString, MultiplayerActivityJoinRestriction joinRestriction = MultiplayerActivityJoinRestriction.Public, uint maxPlayers = 0, uint currentPlayers = 0, string? groupId = null, MultiplayerActivityPlatform platform = MultiplayerActivityPlatform.Unknown)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Xbox user id that owns the activity.

`connectionString` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Connection string passed to a joining client.

`joinRestriction` [MultiplayerActivityJoinRestriction](GDK.Net.XboxLive.MultiplayerActivityJoinRestriction.md)

Who can join the activity.

`maxPlayers` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum joinable players. 0 means no players can join, or lets XSAPI ignore it when setting.

`currentPlayers` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Current players in the activity. 0 means no other players, or lets XSAPI ignore it when setting.

`groupId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Title-defined identifier shared by users in the same activity.

`platform` [MultiplayerActivityPlatform](GDK.Net.XboxLive.MultiplayerActivityPlatform.md)

Platform on which the activity is happening.

## Properties

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_ConnectionString"></a> ConnectionString

Connection string passed to a joining client. Queries can return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when
privacy or join restrictions hide the join data.

```csharp
public string? ConnectionString { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_CurrentPlayers"></a> CurrentPlayers

Current players in the activity.

```csharp
public uint CurrentPlayers { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_GroupId"></a> GroupId

Title-defined identifier shared by users in the same activity.

```csharp
public string? GroupId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_JoinRestriction"></a> JoinRestriction

Who can join the activity.

```csharp
public MultiplayerActivityJoinRestriction JoinRestriction { get; }
```

#### Property Value

 [MultiplayerActivityJoinRestriction](GDK.Net.XboxLive.MultiplayerActivityJoinRestriction.md)

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_MaxPlayers"></a> MaxPlayers

Maximum joinable players.

```csharp
public uint MaxPlayers { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_Platform"></a> Platform

Platform on which the activity is happening.

```csharp
public MultiplayerActivityPlatform Platform { get; }
```

#### Property Value

 [MultiplayerActivityPlatform](GDK.Net.XboxLive.MultiplayerActivityPlatform.md)

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_XboxUserId"></a> XboxUserId

The Xbox user id that owns the activity.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_MultiplayerActivityInfo_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

