# <a id="GDK_Net_XboxLive_SocialRelationshipsPage"></a> Class SocialRelationshipsPage

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One page of social relationships, plus the means to fetch the next. Wraps
<code>XblSocialRelationshipResultHandle</code>.

```csharp
public sealed class SocialRelationshipsPage : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialRelationshipsPage](GDK.Net.XboxLive.SocialRelationshipsPage.md)

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

The native handle owns the relationship data, so <xref href="GDK.Net.XboxLive.SocialRelationshipsPage.Relationships" data-throw-if-not-resolved="false"></xref> is materialized at
construction and every element is a full managed copy. The current page stays readable after it
is disposed; only <xref href="GDK.Net.XboxLive.SocialRelationshipsPage.GetNextAsync(System.UInt64%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> needs the live handle.

## Properties

### <a id="GDK_Net_XboxLive_SocialRelationshipsPage_HasNext"></a> HasNext

Whether another page is available.

```csharp
public bool HasNext { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialRelationshipsPage_Relationships"></a> Relationships

The relationships in this page.

```csharp
public IReadOnlyList<SocialRelationship> Relationships { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SocialRelationship](GDK.Net.XboxLive.SocialRelationship.md)\>

### <a id="GDK_Net_XboxLive_SocialRelationshipsPage_TotalCount"></a> TotalCount

The total number of relationships matching the query.

```csharp
public ulong TotalCount { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_SocialRelationshipsPage_Dispose"></a> Dispose\(\)

Releases the native result handle.

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_SocialRelationshipsPage_GetNextAsync_System_UInt64_System_Threading_CancellationToken_"></a> GetNextAsync\(ulong, CancellationToken\)

Fetches the next page (<code>XblSocialRelationshipResultGetNextAsync</code>).

```csharp
public Task<SocialRelationshipsPage> GetNextAsync(ulong maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItems` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Maximum relationships to return. 0 lets the service choose.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SocialRelationshipsPage](GDK.Net.XboxLive.SocialRelationshipsPage.md)\>

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<xref href="GDK.Net.XboxLive.SocialRelationshipsPage.HasNext" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_SocialRelationshipsPage_ReadAllAsync_System_UInt64_System_Threading_CancellationToken_"></a> ReadAllAsync\(ulong, CancellationToken\)

Enumerates this page and every page after it, fetching each on demand.

```csharp
public Task<IReadOnlyList<SocialRelationship>> ReadAllAsync(ulong maxItemsPerPage = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItemsPerPage` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Maximum relationships per fetched page. 0 lets the service choose.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels any page fetch; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SocialRelationship](GDK.Net.XboxLive.SocialRelationship.md)\>\>

