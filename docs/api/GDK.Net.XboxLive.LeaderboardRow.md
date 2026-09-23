# <a id="GDK_Net_XboxLive_LeaderboardRow"></a> Class LeaderboardRow

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One row returned in a leaderboard page. Managed snapshot of <code>XblLeaderboardRow</code>.

```csharp
public sealed class LeaderboardRow
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardRow](GDK.Net.XboxLive.LeaderboardRow.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_LeaderboardRow_ColumnValues"></a> ColumnValues

JSON values for this row, one value for each leaderboard column.

```csharp
public IReadOnlyList<string> ColumnValues { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_XboxLive_LeaderboardRow_Gamertag"></a> Gamertag

The player's classic gamertag.

```csharp
public string Gamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_LeaderboardRow_GlobalRank"></a> GlobalRank

The player's global rank, or 0 when the player has no global rank.

```csharp
public uint GlobalRank { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_LeaderboardRow_ModernGamertag"></a> ModernGamertag

The player's modern gamertag, without suffix. Not guaranteed unique.

```csharp
public string ModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_LeaderboardRow_ModernGamertagSuffix"></a> ModernGamertagSuffix

The suffix that makes <xref href="GDK.Net.XboxLive.LeaderboardRow.ModernGamertag" data-throw-if-not-resolved="false"></xref> unique. May be empty.

```csharp
public string ModernGamertagSuffix { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_LeaderboardRow_Percentile"></a> Percentile

The player's percentile rank.

```csharp
public double Percentile { get; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

### <a id="GDK_Net_XboxLive_LeaderboardRow_Rank"></a> Rank

The player's rank in this result.

```csharp
public uint Rank { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_LeaderboardRow_UniqueModernGamertag"></a> UniqueModernGamertag

The unique modern gamertag, formatted <code>modernGamertag#suffix</code>.

```csharp
public string UniqueModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_LeaderboardRow_XboxUserId"></a> XboxUserId

The player's Xbox user id.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_LeaderboardRow_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

