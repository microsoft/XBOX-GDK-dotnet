# <a id="GDK_Net_XboxLive_TitleStorageBlobMetadataPage"></a> Class TitleStorageBlobMetadataPage

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One page of title storage blob metadata, plus the means to fetch the next. Wraps
<code>XblTitleStorageBlobMetadataResultHandle</code>.

```csharp
public sealed class TitleStorageBlobMetadataPage : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleStorageBlobMetadataPage](GDK.Net.XboxLive.TitleStorageBlobMetadataPage.md)

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

The native handle owns the metadata array. <xref href="GDK.Net.XboxLive.TitleStorageBlobMetadataPage.Items" data-throw-if-not-resolved="false"></xref> is materialized at construction,
so metadata stays readable after disposal; only <xref href="GDK.Net.XboxLive.TitleStorageBlobMetadataPage.GetNextAsync(System.UInt32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> requires the live
handle.

## Properties

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadataPage_HasNext"></a> HasNext

Whether another metadata page is available.

```csharp
public bool HasNext { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadataPage_Items"></a> Items

The metadata items in this page.

```csharp
public IReadOnlyList<TitleStorageBlobMetadata> Items { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)\>

## Methods

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadataPage_Dispose"></a> Dispose\(\)

Releases the native metadata result handle.

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadataPage_GetNextAsync_System_UInt32_System_Threading_CancellationToken_"></a> GetNextAsync\(uint, CancellationToken\)

Fetches the next metadata page.

```csharp
public Task<TitleStorageBlobMetadataPage> GetNextAsync(uint maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum items to return. 0 attempts to retrieve all remaining items.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TitleStorageBlobMetadataPage](GDK.Net.XboxLive.TitleStorageBlobMetadataPage.md)\>

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<xref href="GDK.Net.XboxLive.TitleStorageBlobMetadataPage.HasNext" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_TitleStorageBlobMetadataPage_ReadAllAsync_System_UInt32_System_Threading_CancellationToken_"></a> ReadAllAsync\(uint, CancellationToken\)

Enumerates this page and every page after it, fetching each on demand.

```csharp
public Task<IReadOnlyList<TitleStorageBlobMetadata>> ReadAllAsync(uint maxItemsPerPage = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`maxItemsPerPage` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum items per fetched page; 0 lets the service choose.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels a pending fetch.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)\>\>

#### Remarks

Each fetched page is disposed once the following page has been read. Returned metadata is a
managed copy and remains valid.

