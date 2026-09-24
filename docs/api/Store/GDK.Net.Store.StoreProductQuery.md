# <a id="GDK_Net_Store_StoreProductQuery"></a> Class StoreProductQuery

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A paged product query, produced by the <code>XStoreQuery*Products*</code> family.

```csharp
public sealed class StoreProductQuery : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)

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
Call <xref href="GDK.Net.Store.StoreProductQuery.EnumerateProducts" data-throw-if-not-resolved="false"></xref> to read the products on the current page.
Check <xref href="GDK.Net.Store.StoreProductQuery.HasMorePages" data-throw-if-not-resolved="false"></xref> and call <xref href="GDK.Net.Store.StoreProductQuery.NextPageAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> to page forward.
</p>
<p>Dispose to close the underlying <code>XStoreProductQueryHandle</code>.</p>

## Properties

### <a id="GDK_Net_Store_StoreProductQuery_HasMorePages"></a> HasMorePages

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when more pages of results are available
(<code>XStoreProductsQueryHasMorePages</code>).

```csharp
public bool HasMorePages { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_Store_StoreProductQuery_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Store_StoreProductQuery_EnumerateProducts"></a> EnumerateProducts\(\)

Enumerates the products on the current page by invoking the native
<code>XStoreEnumerateProductsQuery</code> callback synchronously.

```csharp
public IReadOnlyList<StoreProduct> EnumerateProducts()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreProduct](GDK.Net.Store.StoreProduct.md)\>

A snapshot of all products on the current page.

#### Remarks

The returned list is a fully managed deep copy; all strings and arrays are copied out of
the native callback before this method returns.

### <a id="GDK_Net_Store_StoreProductQuery_NextPageAsync_System_Threading_CancellationToken_"></a> NextPageAsync\(CancellationToken\)

Advances the query to the next page (<code>XStoreProductsQueryNextPageAsync</code> /
<code>XStoreProductsQueryNextPageResult</code>).

```csharp
public Task<StoreProductQuery> NextPageAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

#### Remarks

Call <xref href="GDK.Net.Store.StoreProductQuery.EnumerateProducts" data-throw-if-not-resolved="false"></xref> on the returned <xref href="GDK.Net.Store.StoreProductQuery" data-throw-if-not-resolved="false"></xref> to
read the new page. This instance should be disposed after a successful
<xref href="GDK.Net.Store.StoreProductQuery.NextPageAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> call.

