# <a id="GDK_Net_XboxLive_LeaderboardPage"></a> Class LeaderboardPage

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One page of leaderboard results, plus the means to fetch the next. Managed wrapper for a
caller-allocated <code>XblLeaderboardResult</code> buffer.

```csharp
public sealed class LeaderboardPage : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardPage](GDK.Net.XboxLive.LeaderboardPage.md)

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

<p>
XSAPI does not return a leaderboard result handle. The result APIs first report the byte count,
then write a complete pointer graph into a caller-allocated buffer and return an
<code>XblLeaderboardResult*</code> that points inside it. This type owns that unmanaged buffer and
releases it from <xref href="GDK.Net.XboxLive.LeaderboardPage.Dispose" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
Columns and rows are snapshotted into managed objects at construction, so they remain readable
after disposal. The native buffer is retained only because <xref href="GDK.Net.XboxLive.LeaderboardPage.GetNextAsync(System.UInt32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> must pass
the previous <code>XblLeaderboardResult*</code> back to XSAPI to obtain the next page.
</p>

## Properties

### <a id="GDK_Net_XboxLive_LeaderboardPage_Columns"></a> Columns

The columns returned for each row.

```csharp
public IReadOnlyList<LeaderboardColumn> Columns { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardColumn](GDK.Net.XboxLive.LeaderboardColumn.md)\>

### <a id="GDK_Net_XboxLive_LeaderboardPage_HasNext"></a> HasNext

Whether another page is available.

```csharp
public bool HasNext { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_LeaderboardPage_Rows"></a> Rows

The rows in this page.

```csharp
public IReadOnlyList<LeaderboardRow> Rows { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardRow](GDK.Net.XboxLive.LeaderboardRow.md)\>

### <a id="GDK_Net_XboxLive_LeaderboardPage_TotalRowCount"></a> TotalRowCount

The total number of rows in the full leaderboard, not just this page.

```csharp
public uint TotalRowCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_XboxLive_LeaderboardPage_Dispose"></a> Dispose\(\)

Releases the caller-allocated native result buffer.

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_LeaderboardPage_GetNextAsync_System_UInt32_System_Threading_CancellationToken_"></a> GetNextAsync\(uint, CancellationToken\)

Fetches the next leaderboard page (<code>XblLeaderboardResultGetNextAsync</code>).

```csharp
public Task<LeaderboardPage> GetNextAsync(uint maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum rows to return. 0 attempts to retrieve all remaining rows.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[LeaderboardPage](GDK.Net.XboxLive.LeaderboardPage.md)\>

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<xref href="GDK.Net.XboxLive.LeaderboardPage.HasNext" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_LeaderboardPage_ReadAllRowsAsync_System_UInt32_System_Threading_CancellationToken_"></a> ReadAllRowsAsync\(uint, CancellationToken\)

Enumerates this page and every page after it, fetching each on demand.

```csharp
public Task<IReadOnlyList<LeaderboardRow>> ReadAllRowsAsync(uint maxItemsPerPage = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItemsPerPage` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardRow](GDK.Net.XboxLive.LeaderboardRow.md)\>\>

#### Remarks

Each page fetched by this method is disposed once the page after it has been materialized,
so the caller only has to dispose the page it started from. The returned rows are managed
copies and remain valid.

