# <a id="GDK_Net_Store_StoreContext"></a> Class StoreContext

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

The entry point for all Store operations (<code>XStoreContextHandle</code>).

```csharp
public sealed class StoreContext : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreContext](GDK.Net.Store.StoreContext.md)

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
Create a context with <xref href="GDK.Net.Store.StoreContext.Create" data-throw-if-not-resolved="false"></xref> (system context) or
<xref href="GDK.Net.Store.StoreContext.CreateForUser(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref> (user context). Most operations that display UI or query
entitlements require a user context backed by a signed-in <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
Dispose to release the underlying <code>XStoreContextHandle</code> and unregister any
<xref href="GDK.Net.Store.StoreContext.GameLicenseChanged" data-throw-if-not-resolved="false"></xref> subscription.
</p>

## Methods

### <a id="GDK_Net_Store_StoreContext_AcquireLicenseForDurablesAsync_System_String_System_Threading_CancellationToken_"></a> AcquireLicenseForDurablesAsync\(string, CancellationToken\)

Acquires a licence for a durable add-on (<code>XStoreAcquireLicenseForDurablesAsync</code> /
<code>XStoreAcquireLicenseForDurablesResult</code>).

```csharp
public Task<StoreLicense> AcquireLicenseForDurablesAsync(string storeId, CancellationToken cancellationToken = default)
```

#### Parameters

`storeId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreLicense](GDK.Net.Store.StoreLicense.md)\>

### <a id="GDK_Net_Store_StoreContext_AcquireLicenseForPackageAsync_System_String_System_Threading_CancellationToken_"></a> AcquireLicenseForPackageAsync\(string, CancellationToken\)

Acquires a package licence (<code>XStoreAcquireLicenseForPackageAsync</code> /
<code>XStoreAcquireLicenseForPackageResult</code>).

```csharp
public Task<StoreLicense> AcquireLicenseForPackageAsync(string packageIdentifier, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreLicense](GDK.Net.Store.StoreLicense.md)\>

### <a id="GDK_Net_Store_StoreContext_CanAcquireLicenseForPackageAsync_System_String_System_Threading_CancellationToken_"></a> CanAcquireLicenseForPackageAsync\(string, CancellationToken\)

Checks whether a package licence can be acquired
(<code>XStoreCanAcquireLicenseForPackageAsync</code> /
<code>XStoreCanAcquireLicenseForPackageResult</code>).

```csharp
public Task<StoreCanAcquireLicenseResult> CanAcquireLicenseForPackageAsync(string packageIdentifier, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreCanAcquireLicenseResult](GDK.Net.Store.StoreCanAcquireLicenseResult.md)\>

### <a id="GDK_Net_Store_StoreContext_CanAcquireLicenseForStoreIdAsync_System_String_System_Threading_CancellationToken_"></a> CanAcquireLicenseForStoreIdAsync\(string, CancellationToken\)

Checks whether a Store product licence can be acquired
(<code>XStoreCanAcquireLicenseForStoreIdAsync</code> /
<code>XStoreCanAcquireLicenseForStoreIdResult</code>).

```csharp
public Task<StoreCanAcquireLicenseResult> CanAcquireLicenseForStoreIdAsync(string storeProductId, CancellationToken cancellationToken = default)
```

#### Parameters

`storeProductId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreCanAcquireLicenseResult](GDK.Net.Store.StoreCanAcquireLicenseResult.md)\>

### <a id="GDK_Net_Store_StoreContext_Create"></a> Create\(\)

Creates a system Store context (<code>XStoreCreateContext</code> with null user).

```csharp
public static StoreContext Create()
```

#### Returns

 [StoreContext](GDK.Net.Store.StoreContext.md)

#### Remarks

Async operations and event callbacks name no task queue, so the Gaming Runtime resolves the
process default.

### <a id="GDK_Net_Store_StoreContext_CreateForUser_GDK_Net_Users_User_"></a> CreateForUser\(User\)

Creates a Store context bound to a specific signed-in user
(<code>XStoreCreateContext</code>).

```csharp
public static StoreContext CreateForUser(User user)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The signed-in user. Most purchase, entitlement and UI operations require a user context.

#### Returns

 [StoreContext](GDK.Net.Store.StoreContext.md)

### <a id="GDK_Net_Store_StoreContext_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Store_StoreContext_DownloadAndInstallPackageUpdatesAsync_System_Collections_Generic_IReadOnlyList_System_String__System_Threading_CancellationToken_"></a> DownloadAndInstallPackageUpdatesAsync\(IReadOnlyList<string\>, CancellationToken\)

Downloads and installs the pending package updates
(<code>XStoreDownloadAndInstallPackageUpdatesAsync</code> /
<code>XStoreDownloadAndInstallPackageUpdatesResult</code>).

```csharp
public Task DownloadAndInstallPackageUpdatesAsync(IReadOnlyList<string> packageIdentifiers, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifiers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_Store_StoreContext_DownloadAndInstallPackagesAsync_System_Collections_Generic_IReadOnlyList_System_String__System_Threading_CancellationToken_"></a> DownloadAndInstallPackagesAsync\(IReadOnlyList<string\>, CancellationToken\)

Downloads and installs packages from the Store
(<code>XStoreDownloadAndInstallPackagesAsync</code> / count / result).

```csharp
public Task<IReadOnlyList<string>> DownloadAndInstallPackagesAsync(IReadOnlyList<string> storeIds, CancellationToken cancellationToken = default)
```

#### Parameters

`storeIds` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>\>

The package identifiers of the installed packages.

### <a id="GDK_Net_Store_StoreContext_DownloadPackageUpdatesAsync_System_Collections_Generic_IReadOnlyList_System_String__System_Threading_CancellationToken_"></a> DownloadPackageUpdatesAsync\(IReadOnlyList<string\>, CancellationToken\)

Downloads (but does not install) the pending package updates
(<code>XStoreDownloadPackageUpdatesAsync</code> / <code>XStoreDownloadPackageUpdatesResult</code>).

```csharp
public Task DownloadPackageUpdatesAsync(IReadOnlyList<string> packageIdentifiers, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifiers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_Store_StoreContext_GetUserCollectionsIdAsync_System_String_System_String_System_Threading_CancellationToken_"></a> GetUserCollectionsIdAsync\(string, string, CancellationToken\)

Gets a collections ID token for server-side entitlement verification
(<code>XStoreGetUserCollectionsIdAsync</code> / size / result).

```csharp
public Task<string> GetUserCollectionsIdAsync(string serviceTicket, string publisherUserId, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceTicket` [string](https://learn.microsoft.com/dotnet/api/system.string)

`publisherUserId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

#### Remarks

Requires a user context and a signed-in user with the Games privilege.

### <a id="GDK_Net_Store_StoreContext_GetUserPurchaseIdAsync_System_String_System_String_System_Threading_CancellationToken_"></a> GetUserPurchaseIdAsync\(string, string, CancellationToken\)

Gets a purchase ID token for server-side purchase validation
(<code>XStoreGetUserPurchaseIdAsync</code> / size / result).

```csharp
public Task<string> GetUserPurchaseIdAsync(string serviceTicket, string publisherUserId, CancellationToken cancellationToken = default)
```

#### Parameters

`serviceTicket` [string](https://learn.microsoft.com/dotnet/api/system.string)

`publisherUserId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

#### Remarks

Requires a user context and a signed-in user with the Games privilege.

### <a id="GDK_Net_Store_StoreContext_IsAvailabilityPurchasable_GDK_Net_Store_StoreAvailability_"></a> IsAvailabilityPurchasable\(StoreAvailability\)

Checks whether an availability can be purchased
(<code>XStoreIsAvailabilityPurchasable</code>).

```csharp
public static bool IsAvailabilityPurchasable(StoreAvailability availability)
```

#### Parameters

`availability` [StoreAvailability](GDK.Net.Store.StoreAvailability.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreContext_QueryAddOnLicensesAsync_System_Threading_CancellationToken_"></a> QueryAddOnLicensesAsync\(CancellationToken\)

Queries all add-on (DLC) licences (<code>XStoreQueryAddOnLicensesAsync</code> /
<code>XStoreQueryAddOnLicensesResultCount</code> / <code>XStoreQueryAddOnLicensesResult</code>).

```csharp
public Task<IReadOnlyList<StoreAddonLicense>> QueryAddOnLicensesAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StoreAddonLicense](GDK.Net.Store.StoreAddonLicense.md)\>\>

#### Remarks

Requires a user context and a signed-in user.

### <a id="GDK_Net_Store_StoreContext_QueryAssociatedProductsAsync_GDK_Net_Store_StoreProductKind_System_UInt32_System_Threading_CancellationToken_"></a> QueryAssociatedProductsAsync\(StoreProductKind, uint, CancellationToken\)

Queries products associated with the current game
(<code>XStoreQueryAssociatedProductsAsync</code> / <code>XStoreQueryAssociatedProductsResult</code>).

```csharp
public Task<StoreProductQuery> QueryAssociatedProductsAsync(StoreProductKind productKinds, uint maxItemsPerPage = 25, CancellationToken cancellationToken = default)
```

#### Parameters

`productKinds` [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

`maxItemsPerPage` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

#### Remarks

This queries products associated with the currently running game (by title ID). To query the
products associated with a different game, use
<xref href="GDK.Net.Store.StoreContext.QueryAssociatedProductsForStoreIdAsync(System.String%2cGDK.Net.Store.StoreProductKind%2cSystem.UInt32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Store_StoreContext_QueryAssociatedProductsForStoreIdAsync_System_String_GDK_Net_Store_StoreProductKind_System_UInt32_System_Threading_CancellationToken_"></a> QueryAssociatedProductsForStoreIdAsync\(string, StoreProductKind, uint, CancellationToken\)

Queries the products associated with an explicit Store ID rather than with the currently
running game (<code>XStoreQueryAssociatedProductsForStoreIdAsync</code> /
<code>XStoreQueryAssociatedProductsForStoreIdResult</code>).

```csharp
public Task<StoreProductQuery> QueryAssociatedProductsForStoreIdAsync(string storeProductId, StoreProductKind productKinds, uint maxItemsPerPage = 25, CancellationToken cancellationToken = default)
```

#### Parameters

`storeProductId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Store ID of the product whose add-ons are wanted.

`productKinds` [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

The product kinds to include.

`maxItemsPerPage` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Page size for the returned query.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

### <a id="GDK_Net_Store_StoreContext_QueryConsumableBalanceRemainingAsync_System_String_System_Threading_CancellationToken_"></a> QueryConsumableBalanceRemainingAsync\(string, CancellationToken\)

Queries the remaining balance of a consumable
(<code>XStoreQueryConsumableBalanceRemainingAsync</code> /
<code>XStoreQueryConsumableBalanceRemainingResult</code>).

```csharp
public Task<StoreConsumableResult> QueryConsumableBalanceRemainingAsync(string storeProductId, CancellationToken cancellationToken = default)
```

#### Parameters

`storeProductId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreConsumableResult](GDK.Net.Store.StoreConsumableResult.md)\>

#### Remarks

Requires a user context and a signed-in user.

### <a id="GDK_Net_Store_StoreContext_QueryEntitledProductsAsync_GDK_Net_Store_StoreProductKind_System_UInt32_System_Threading_CancellationToken_"></a> QueryEntitledProductsAsync\(StoreProductKind, uint, CancellationToken\)

Queries products the user has entitlements to
(<code>XStoreQueryEntitledProductsAsync</code> / <code>XStoreQueryEntitledProductsResult</code>).

```csharp
public Task<StoreProductQuery> QueryEntitledProductsAsync(StoreProductKind productKinds, uint maxItemsPerPage = 25, CancellationToken cancellationToken = default)
```

#### Parameters

`productKinds` [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

`maxItemsPerPage` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

#### Remarks

Requires a user context and a signed-in user.

### <a id="GDK_Net_Store_StoreContext_QueryGameAndDlcPackageUpdatesAsync_System_Threading_CancellationToken_"></a> QueryGameAndDlcPackageUpdatesAsync\(CancellationToken\)

Queries pending updates for the current game and its DLC
(<code>XStoreQueryGameAndDlcPackageUpdatesAsync</code> / count / result).

```csharp
public Task<IReadOnlyList<StorePackageUpdate>> QueryGameAndDlcPackageUpdatesAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StorePackageUpdate](GDK.Net.Store.StorePackageUpdate.md)\>\>

#### Remarks

This is the current API and covers the running game plus its DLC. To query updates for an
explicit set of packages instead, use <xref href="GDK.Net.Store.StoreContext.QueryPackageUpdatesAsync(System.Collections.Generic.IReadOnlyList%7bSystem.String%7d%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Store_StoreContext_QueryGameLicenseAsync_System_Threading_CancellationToken_"></a> QueryGameLicenseAsync\(CancellationToken\)

Queries the game licence (<code>XStoreQueryGameLicenseAsync</code> /
<code>XStoreQueryGameLicenseResult</code>).

```csharp
public Task<StoreGameLicense> QueryGameLicenseAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreGameLicense](GDK.Net.Store.StoreGameLicense.md)\>

#### Remarks

Requires a user context and a signed-in user.

### <a id="GDK_Net_Store_StoreContext_QueryLicenseTokenAsync_System_Collections_Generic_IReadOnlyList_System_String__System_String_System_Threading_CancellationToken_"></a> QueryLicenseTokenAsync\(IReadOnlyList<string\>, string?, CancellationToken\)

Queries the licence token (<code>XStoreQueryLicenseTokenAsync</code> /
<code>XStoreQueryLicenseTokenResultSize</code> / <code>XStoreQueryLicenseTokenResult</code>).

```csharp
public Task<string> QueryLicenseTokenAsync(IReadOnlyList<string> productIds, string? customDeveloperString = null, CancellationToken cancellationToken = default)
```

#### Parameters

`productIds` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The product IDs to include in the token.

`customDeveloperString` [string](https://learn.microsoft.com/dotnet/api/system.string)?

An optional developer-defined string embedded in the token.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_Store_StoreContext_QueryPackageIdentifier_System_String_"></a> QueryPackageIdentifier\(string\)

Looks up the package identifier for a Store product ID
(<code>XStoreQueryPackageIdentifier</code>).

```csharp
public static string QueryPackageIdentifier(string storeId)
```

#### Parameters

`storeId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Store product ID to look up.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The package identifier string (up to 33 bytes).

### <a id="GDK_Net_Store_StoreContext_QueryPackageUpdatesAsync_System_Collections_Generic_IReadOnlyList_System_String__System_Threading_CancellationToken_"></a> QueryPackageUpdatesAsync\(IReadOnlyList<string\>, CancellationToken\)

Queries pending updates for an explicit set of packages
(<code>XStoreQueryPackageUpdatesAsync</code> / count / result).

```csharp
public Task<IReadOnlyList<StorePackageUpdate>> QueryPackageUpdatesAsync(IReadOnlyList<string> packageIdentifiers, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifiers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The package identifiers to check for updates.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StorePackageUpdate](GDK.Net.Store.StorePackageUpdate.md)\>\>

#### Remarks

Prefer <xref href="GDK.Net.Store.StoreContext.QueryGameAndDlcPackageUpdatesAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> unless the title needs to scope the
query to specific packages; that method needs no argument.

### <a id="GDK_Net_Store_StoreContext_QueryProductForCurrentGameAsync_System_Threading_CancellationToken_"></a> QueryProductForCurrentGameAsync\(CancellationToken\)

Queries the product record for the current running game
(<code>XStoreQueryProductForCurrentGameAsync</code> / <code>XStoreQueryProductForCurrentGameResult</code>).

```csharp
public Task<StoreProductQuery> QueryProductForCurrentGameAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

### <a id="GDK_Net_Store_StoreContext_QueryProductForPackageAsync_GDK_Net_Store_StoreProductKind_System_String_System_Threading_CancellationToken_"></a> QueryProductForPackageAsync\(StoreProductKind, string, CancellationToken\)

Queries the product record for a specific package
(<code>XStoreQueryProductForPackageAsync</code> / <code>XStoreQueryProductForPackageResult</code>).

```csharp
public Task<StoreProductQuery> QueryProductForPackageAsync(StoreProductKind productKinds, string packageIdentifier, CancellationToken cancellationToken = default)
```

#### Parameters

`productKinds` [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

### <a id="GDK_Net_Store_StoreContext_QueryProductsAsync_GDK_Net_Store_StoreProductKind_System_Collections_Generic_IReadOnlyList_System_String__System_Collections_Generic_IReadOnlyList_System_String__System_Threading_CancellationToken_"></a> QueryProductsAsync\(StoreProductKind, IReadOnlyList<string\>, IReadOnlyList<string\>?, CancellationToken\)

Queries products by Store IDs (<code>XStoreQueryProductsAsync</code> /
<code>XStoreQueryProductsResult</code>).

```csharp
public Task<StoreProductQuery> QueryProductsAsync(StoreProductKind productKinds, IReadOnlyList<string> storeIds, IReadOnlyList<string>? actionFilters = null, CancellationToken cancellationToken = default)
```

#### Parameters

`productKinds` [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

Filter by kind.

`storeIds` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Store IDs to query.

`actionFilters` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

Optional action filter strings.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreProductQuery](GDK.Net.Store.StoreProductQuery.md)\>

### <a id="GDK_Net_Store_StoreContext_ReportConsumableFulfillmentAsync_System_String_System_UInt32_System_Guid_System_Threading_CancellationToken_"></a> ReportConsumableFulfillmentAsync\(string, uint, Guid, CancellationToken\)

Reports fulfillment of a consumable quantity
(<code>XStoreReportConsumableFulfillmentAsync</code> /
<code>XStoreReportConsumableFulfillmentResult</code>).

```csharp
public Task<StoreConsumableResult> ReportConsumableFulfillmentAsync(string storeProductId, uint quantity, Guid trackingId, CancellationToken cancellationToken = default)
```

#### Parameters

`storeProductId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The consumable product's Store ID.

`quantity` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The quantity to report as fulfilled.

`trackingId` [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

A unique GUID for idempotent fulfillment tracking.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreConsumableResult](GDK.Net.Store.StoreConsumableResult.md)\>

#### Remarks

Requires a user context and a signed-in user.

### <a id="GDK_Net_Store_StoreContext_ShowAssociatedProductsUIAsync_System_String_GDK_Net_Store_StoreProductKind_System_Threading_CancellationToken_"></a> ShowAssociatedProductsUIAsync\(string, StoreProductKind, CancellationToken\)

Shows the associated products UI (<code>XStoreShowAssociatedProductsUIAsync</code> /
<code>XStoreShowAssociatedProductsUIResult</code>).

```csharp
public Task ShowAssociatedProductsUIAsync(string storeId, StoreProductKind productKinds, CancellationToken cancellationToken = default)
```

#### Parameters

`storeId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`productKinds` [StoreProductKind](GDK.Net.Store.StoreProductKind.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Requires the title window to be in the foreground.

### <a id="GDK_Net_Store_StoreContext_ShowGiftingUIAsync_System_String_System_String_System_String_System_Threading_CancellationToken_"></a> ShowGiftingUIAsync\(string, string?, string?, CancellationToken\)

Shows the Store gifting UI for a product (<code>XStoreShowGiftingUIAsync</code> /
<code>XStoreShowGiftingUIResult</code>).

```csharp
public Task ShowGiftingUIAsync(string storeId, string? name = null, string? extendedJsonData = null, CancellationToken cancellationToken = default)
```

#### Parameters

`storeId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Store ID of the product to gift.

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional friendly product name shown in the UI.

`extendedJsonData` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional extended JSON payload passed to the Store.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Requires the title window to be in the foreground.

### <a id="GDK_Net_Store_StoreContext_ShowProductPageUIAsync_System_String_System_Threading_CancellationToken_"></a> ShowProductPageUIAsync\(string, CancellationToken\)

Shows the Store product page UI (<code>XStoreShowProductPageUIAsync</code> /
<code>XStoreShowProductPageUIResult</code>).

```csharp
public Task ShowProductPageUIAsync(string storeId, CancellationToken cancellationToken = default)
```

#### Parameters

`storeId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Requires the title window to be in the foreground.

### <a id="GDK_Net_Store_StoreContext_ShowPurchaseUIAsync_System_String_System_String_System_String_System_Threading_CancellationToken_"></a> ShowPurchaseUIAsync\(string, string?, string?, CancellationToken\)

Shows the Store purchase UI for a product (<code>XStoreShowPurchaseUIAsync</code> /
<code>XStoreShowPurchaseUIResult</code>).

```csharp
public Task ShowPurchaseUIAsync(string storeId, string? name = null, string? extendedJsonData = null, CancellationToken cancellationToken = default)
```

#### Parameters

`storeId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`extendedJsonData` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Requires the title window to be in the foreground.

### <a id="GDK_Net_Store_StoreContext_ShowRateAndReviewUIAsync_System_Threading_CancellationToken_"></a> ShowRateAndReviewUIAsync\(CancellationToken\)

Shows the rate-and-review UI (<code>XStoreShowRateAndReviewUIAsync</code> /
<code>XStoreShowRateAndReviewUIResult</code>).

```csharp
public Task<StoreRateAndReviewResult> ShowRateAndReviewUIAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StoreRateAndReviewResult](GDK.Net.Store.StoreRateAndReviewResult.md)\>

#### Remarks

Requires the title window to be in the foreground and a signed-in user.

### <a id="GDK_Net_Store_StoreContext_ShowRedeemTokenUIAsync_System_String_System_Collections_Generic_IReadOnlyList_System_String__System_Boolean_System_Threading_CancellationToken_"></a> ShowRedeemTokenUIAsync\(string, IReadOnlyList<string\>?, bool, CancellationToken\)

Shows the redeem token UI (<code>XStoreShowRedeemTokenUIAsync</code> /
<code>XStoreShowRedeemTokenUIResult</code>).

```csharp
public Task ShowRedeemTokenUIAsync(string token, IReadOnlyList<string>? allowedStoreIds = null, bool disallowCsvRedemption = false, CancellationToken cancellationToken = default)
```

#### Parameters

`token` [string](https://learn.microsoft.com/dotnet/api/system.string)

The redemption token string.

`allowedStoreIds` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

Store IDs to accept; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> accepts any.

`disallowCsvRedemption` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, CSV codes are not accepted.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Requires the title window to be in the foreground.

### <a id="GDK_Net_Store_StoreContext_GameLicenseChanged"></a> GameLicenseChanged

Raised when the game's licence state changes (<code>XStoreRegisterGameLicenseChanged</code>).

```csharp
public event EventHandler? GameLicenseChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler)?

#### Remarks

The registration is lazily created on the first subscription and released with
<code>wait: true</code> on <xref href="GDK.Net.Store.StoreContext.Dispose" data-throw-if-not-resolved="false"></xref>. Requires the Gaming Runtime.

