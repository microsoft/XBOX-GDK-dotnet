# <a id="GDK_Net_XboxLive_AchievementsPage"></a> Class AchievementsPage

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One page of achievements, plus the means to fetch the next. Wraps
<code>XblAchievementsResultHandle</code>.

```csharp
public sealed class AchievementsPage : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsPage](GDK.Net.XboxLive.AchievementsPage.md)

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
The native handle owns the achievement data, so <xref href="GDK.Net.XboxLive.AchievementsPage.Achievements" data-throw-if-not-resolved="false"></xref> is materialized once
at construction and every element is a full managed copy. That means a page stays readable after
it is disposed — only <xref href="GDK.Net.XboxLive.AchievementsPage.GetNextAsync(System.UInt32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> needs the live handle.
</p>
<p>
This is <b>not</b> the reclaimed-batch pattern the <code>*_manager</code> layers use; each page is an
independently owned handle, so pages may be held concurrently.
</p>

## Properties

### <a id="GDK_Net_XboxLive_AchievementsPage_Achievements"></a> Achievements

The achievements in this page (<code>XblAchievementsResultGetAchievements</code>).

```csharp
public IReadOnlyList<Achievement> Achievements { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[Achievement](GDK.Net.XboxLive.Achievement.md)\>

### <a id="GDK_Net_XboxLive_AchievementsPage_HasNext"></a> HasNext

Whether another page is available (<code>XblAchievementsResultHasNext</code>). Cached at
construction; it cannot change for a given page.

```csharp
public bool HasNext { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_XboxLive_AchievementsPage_Dispose"></a> Dispose\(\)

Releases the native result handle (<code>XblAchievementsResultCloseHandle</code>).

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_AchievementsPage_GetNextAsync_System_UInt32_System_Threading_CancellationToken_"></a> GetNextAsync\(uint, CancellationToken\)

Fetches the next page (<code>XblAchievementsResultGetNextAsync</code>).

```csharp
public Task<AchievementsPage> GetNextAsync(uint maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum achievements to return. 0 — the default — lets the service choose.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AchievementsPage](GDK.Net.XboxLive.AchievementsPage.md)\>

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<xref href="GDK.Net.XboxLive.AchievementsPage.HasNext" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_AchievementsPage_ReadAllAsync_System_UInt32_System_Threading_CancellationToken_"></a> ReadAllAsync\(uint, CancellationToken\)

Enumerates this page and every page after it, fetching each on demand.

```csharp
public Task<IReadOnlyList<Achievement>> ReadAllAsync(uint maxItemsPerPage = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItemsPerPage` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[Achievement](GDK.Net.XboxLive.Achievement.md)\>\>

#### Remarks

Each page is disposed once the page after it has been fetched, so the caller only has to
dispose the page it started from. The <xref href="GDK.Net.XboxLive.Achievement" data-throw-if-not-resolved="false"></xref> objects handed out remain
valid because they are managed copies.

