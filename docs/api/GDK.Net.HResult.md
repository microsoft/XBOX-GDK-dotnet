# <a id="GDK_Net_HResult"></a> Class HResult

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

Well-known HRESULT values returned by the GDK, plus the standard COM codes the projection
special-cases. Values are taken verbatim from <code>XGameErr.h</code> (GDK edition 260404) and
<code>winerror.h</code>.

```csharp
public static class HResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HResult](GDK.Net.HResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GDK_Net_HResult_EAbort"></a> EAbort

The operation was aborted (<code>E_ABORT</code>).

```csharp
public const int EAbort = -2147467260
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EFail"></a> EFail

Unspecified failure (<code>E_FAIL</code>).

```csharp
public const int EFail = -2147467259
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageAppNotPackaged"></a> EGamePackageAppNotPackaged

The title must be run as a packaged GDK app; this process has no package identity.

```csharp
public const int EGamePackageAppNotPackaged = -1994108416
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageConfigNoMsaAppIdOrTitleId"></a> EGamePackageConfigNoMsaAppIdOrTitleId

MicrosoftGame.config is missing both MSA app id and title id.

```csharp
public const int EGamePackageConfigNoMsaAppIdOrTitleId = -1994108405
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageConfigNoRootNode"></a> EGamePackageConfigNoRootNode

MicrosoftGame.config is missing the root node.

```csharp
public const int EGamePackageConfigNoRootNode = -1994108407
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageConfigZeroVersion"></a> EGamePackageConfigZeroVersion

MicrosoftGame.config specifies a zero version.

```csharp
public const int EGamePackageConfigZeroVersion = -1994108406
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageDlcNotSupported"></a> EGamePackageDlcNotSupported

DLC is not supported for this package.

```csharp
public const int EGamePackageDlcNotSupported = -1994108410
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageDownloadRequired"></a> EGamePackageDownloadRequired

One or more required chunks must be downloaded to mount the package on this device.

```csharp
public const int EGamePackageDownloadRequired = -1994108412
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageDuplicateIdValues"></a> EGamePackageDuplicateIdValues

The package contains duplicate executable id attributes in the game config.

```csharp
public const int EGamePackageDuplicateIdValues = -1994108409
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageInvalidSelector"></a> EGamePackageInvalidSelector

A chunk selector was provided that does not resolve to a chunk in the package.

```csharp
public const int EGamePackageInvalidSelector = -1994108413
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageNoInstalledLanguages"></a> EGamePackageNoInstalledLanguages

No languages are installed for this package.

```csharp
public const int EGamePackageNoInstalledLanguages = -1994108415
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageNoPackageIdentifier"></a> EGamePackageNoPackageIdentifier

Could not resolve a package identifier; the package may not be installed.

```csharp
public const int EGamePackageNoPackageIdentifier = -1994108408
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageNoStoreId"></a> EGamePackageNoStoreId

The package does not have a store id.

```csharp
public const int EGamePackageNoStoreId = -1994108414
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGamePackageNoTagChange"></a> EGamePackageNoTagChange

Change installed chunks in this package using Features, not tags.

```csharp
public const int EGamePackageNoTagChange = -1994108411
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeDllNotFound"></a> EGameRuntimeDllNotFound

xgameruntime.thunks.dll could not be loaded. It must be redistributed next to the game executable from %GameDKCoreLatest%windows\bin\&lt;arch&gt;.

```csharp
public const int EGameRuntimeDllNotFound = -1994129151
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeGameConfigBadFormat"></a> EGameRuntimeGameConfigBadFormat

MicrosoftGame.config is malformed.

```csharp
public const int EGameRuntimeGameConfigBadFormat = -1994129141
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeInvalidHandle"></a> EGameRuntimeInvalidHandle

The GDK handle is invalid.

```csharp
public const int EGameRuntimeInvalidHandle = -1994129140
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeMissingDependency"></a> EGameRuntimeMissingDependency

A required Gaming Runtime dependency is missing.

```csharp
public const int EGameRuntimeMissingDependency = -1994129145
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeMultiplayerNotConfigured"></a> EGameRuntimeMultiplayerNotConfigured

Multiplayer is not configured for this title.

```csharp
public const int EGameRuntimeMultiplayerNotConfigured = -1994129146
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeNotInitialized"></a> EGameRuntimeNotInitialized

The Gaming Runtime has not been initialized. Call GameRuntime.Initialize() first.

```csharp
public const int EGameRuntimeNotInitialized = -1994129152
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeOptionsMismatch"></a> EGameRuntimeOptionsMismatch

The supplied runtime options do not match the options already in effect.

```csharp
public const int EGameRuntimeOptionsMismatch = -1994129143
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeOptionsNotSupported"></a> EGameRuntimeOptionsNotSupported

The supplied runtime options are not supported by the installed Gaming Runtime.

```csharp
public const int EGameRuntimeOptionsNotSupported = -1994129142
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeServiceNotAvailable"></a> EGameRuntimeServiceNotAvailable

The required Gaming service is not available.

```csharp
public const int EGameRuntimeServiceNotAvailable = -1994129139
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeSuspendActiveObjects"></a> EGameRuntimeSuspendActiveObjects

The title cannot suspend while GDK objects are still active.

```csharp
public const int EGameRuntimeSuspendActiveObjects = -1994129144
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeSuspended"></a> EGameRuntimeSuspended

The title is suspended and cannot service this call.

```csharp
public const int EGameRuntimeSuspended = -1994129148
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeUninitializeActiveObjects"></a> EGameRuntimeUninitializeActiveObjects

The Gaming Runtime cannot be uninitialized while GDK objects are still alive; dispose them first.

```csharp
public const int EGameRuntimeUninitializeActiveObjects = -1994129147
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeVersionMismatch"></a> EGameRuntimeVersionMismatch

The installed Gaming Runtime does not match the GDK edition this library was built against.

```csharp
public const int EGameRuntimeVersionMismatch = -1994129150
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameRuntimeWindowNotForeground"></a> EGameRuntimeWindowNotForeground

The operation requires the title window to be in the foreground.

```csharp
public const int EGameRuntimeWindowNotForeground = -1994129149
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStoreAlreadyPurchased"></a> EGameStoreAlreadyPurchased

The product has already been purchased.

```csharp
public const int EGameStoreAlreadyPurchased = -1994108156
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStoreInsufficientQuantity"></a> EGameStoreInsufficientQuantity

Insufficient consumable quantity to fulfil the request.

```csharp
public const int EGameStoreInsufficientQuantity = -1994108157
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStoreLicenseActionNotApplicableToProduct"></a> EGameStoreLicenseActionNotApplicableToProduct

The licence action is not applicable to this product type.

```csharp
public const int EGameStoreLicenseActionNotApplicableToProduct = -1994108160
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStoreLicenseActionThrottled"></a> EGameStoreLicenseActionThrottled

The licence action has been throttled; retry after a delay.

```csharp
public const int EGameStoreLicenseActionThrottled = -1994108155
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStoreNetworkError"></a> EGameStoreNetworkError

A Store network error occurred.

```csharp
public const int EGameStoreNetworkError = -1994108159
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStoreServerError"></a> EGameStoreServerError

A Store server error occurred.

```csharp
public const int EGameStoreServerError = -1994108158
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingClientNotConnected"></a> EGameStreamingClientNotConnected

The streaming client is not connected.

```csharp
public const int EGameStreamingClientNotConnected = -1994107903
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingCustomResolutionNotSupported"></a> EGameStreamingCustomResolutionNotSupported

Custom stream resolution is not supported by the connected client.

```csharp
public const int EGameStreamingCustomResolutionNotSupported = -1994107897
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingCustomResolutionTooLarge"></a> EGameStreamingCustomResolutionTooLarge

The requested custom stream resolution is too large.

```csharp
public const int EGameStreamingCustomResolutionTooLarge = -1994107895
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingCustomResolutionTooManyPixels"></a> EGameStreamingCustomResolutionTooManyPixels

The requested custom stream resolution exceeds the maximum pixel count.

```csharp
public const int EGameStreamingCustomResolutionTooManyPixels = -1994107894
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingCustomResolutionTooSmall"></a> EGameStreamingCustomResolutionTooSmall

The requested custom stream resolution is too small.

```csharp
public const int EGameStreamingCustomResolutionTooSmall = -1994107896
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingInvalidCustomResolution"></a> EGameStreamingInvalidCustomResolution

The requested custom stream resolution is invalid.

```csharp
public const int EGameStreamingInvalidCustomResolution = -1994107893
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingNoData"></a> EGameStreamingNoData

No streaming data is available. On the converged (WebRTC) stack some deprecated APIs always return this code.

```csharp
public const int EGameStreamingNoData = -1994107902
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingNoDataCenter"></a> EGameStreamingNoDataCenter

No streaming data center information is available.

```csharp
public const int EGameStreamingNoDataCenter = -1994107901
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingNoMatch"></a> EGameStreamingNoMatch

No matching streaming resource was found.

```csharp
public const int EGameStreamingNoMatch = -1994107899
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingNotInitialized"></a> EGameStreamingNotInitialized

XGameStreamingInitialize has not been called. Call StreamingManager.Initialize() first.

```csharp
public const int EGameStreamingNotInitialized = -1994107904
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingNotStreamingController"></a> EGameStreamingNotStreamingController

The controller is not a streaming controller.

```csharp
public const int EGameStreamingNotStreamingController = -1994107900
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameStreamingTooManyCalls"></a> EGameStreamingTooManyCalls

Too many concurrent streaming API calls.

```csharp
public const int EGameStreamingTooManyCalls = -1994107898
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserDeferralNotAvailable"></a> EGameUserDeferralNotAvailable

A sign-out deferral is not available.

```csharp
public const int EGameUserDeferralNotAvailable = -1994108669
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserFailedToGetToken"></a> EGameUserFailedToGetToken

A user token could not be acquired.

```csharp
public const int EGameUserFailedToGetToken = -1994108655
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserFailedToResolve"></a> EGameUserFailedToResolve

The user could not be resolved.

```csharp
public const int EGameUserFailedToResolve = -1994108665
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserInconsistentMsaAppIdAndTitleId"></a> EGameUserInconsistentMsaAppIdAndTitleId

The MSA app id and title id in MicrosoftGame.config are inconsistent.

```csharp
public const int EGameUserInconsistentMsaAppIdAndTitleId = -1994108652
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserInvalidAppConfiguration"></a> EGameUserInvalidAppConfiguration

The app configuration is invalid.

```csharp
public const int EGameUserInvalidAppConfiguration = -1994108654
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserMalformedMsaAppId"></a> EGameUserMalformedMsaAppId

The MSA app id in MicrosoftGame.config is malformed.

```csharp
public const int EGameUserMalformedMsaAppId = -1994108653
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserMaxUsersAdded"></a> EGameUserMaxUsersAdded

The maximum number of users has already been added.

```csharp
public const int EGameUserMaxUsersAdded = -1994108672
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserNoAuthUser"></a> EGameUserNoAuthUser

There is no authenticated user.

```csharp
public const int EGameUserNoAuthUser = -2015559661
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserNoDefaultUser"></a> EGameUserNoDefaultUser

There is no default user to add silently.

```csharp
public const int EGameUserNoDefaultUser = -1994108666
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserNoMsaAppId"></a> EGameUserNoMsaAppId

MicrosoftGame.config does not declare an MSA app id.

```csharp
public const int EGameUserNoMsaAppId = -1994108651
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserNoPackageIdentity"></a> EGameUserNoPackageIdentity

The process has no package identity; run the title as a packaged GDK app.

```csharp
public const int EGameUserNoPackageIdentity = -1994108656
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserNoTitleId"></a> EGameUserNoTitleId

The title id is missing from MicrosoftGame.config.

```csharp
public const int EGameUserNoTitleId = -1994108664
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserNoTokenRequired"></a> EGameUserNoTokenRequired

No token is required for this operation.

```csharp
public const int EGameUserNoTokenRequired = -1994108667
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserResolveUserIssueRequired"></a> EGameUserResolveUserIssueRequired

The user must resolve an account issue before continuing.

```csharp
public const int EGameUserResolveUserIssueRequired = -1994108670
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserSignedOut"></a> EGameUserSignedOut

The user is signed out.

```csharp
public const int EGameUserSignedOut = -1994108671
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserUnknownGameIdentity"></a> EGameUserUnknownGameIdentity

The game identity is unknown to the Gaming Runtime.

```csharp
public const int EGameUserUnknownGameIdentity = -1994108663
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserUserNotFound"></a> EGameUserUserNotFound

The user could not be found.

```csharp
public const int EGameUserUserNotFound = -1994108668
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGameUserUserNotInSandbox"></a> EGameUserUserNotInSandbox

The user does not have access to the current sandbox.

```csharp
public const int EGameUserUserNotInSandbox = -2146051054
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsAsyncFunctionRequired"></a> EGsAsyncFunctionRequired

This game-save operation requires the async variant.

```csharp
public const int EGsAsyncFunctionRequired = -2138898418
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsBlobNotFound"></a> EGsBlobNotFound

The requested game-save blob was not found.

```csharp
public const int EGsBlobNotFound = -2138898424
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsContainerNotInSync"></a> EGsContainerNotInSync

The game-save container is not in sync with the cloud.

```csharp
public const int EGsContainerNotInSync = -2138898422
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsContainerSyncFailed"></a> EGsContainerSyncFailed

The game-save container failed to sync with the cloud.

```csharp
public const int EGsContainerSyncFailed = -2138898421
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsHandleExpired"></a> EGsHandleExpired

The game-save handle has expired.

```csharp
public const int EGsHandleExpired = -2138898419
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsInvalidContainerName"></a> EGsInvalidContainerName

The game-save container name is invalid.

```csharp
public const int EGsInvalidContainerName = -2138898431
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsNoAccess"></a> EGsNoAccess

Access to the game-save provider was denied.

```csharp
public const int EGsNoAccess = -2138898430
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsNoServiceConfiguration"></a> EGsNoServiceConfiguration

No service configuration is set for this title's game saves.

```csharp
public const int EGsNoServiceConfiguration = -2138898423
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsOutOfLocalStorage"></a> EGsOutOfLocalStorage

Insufficient local storage for the game-save operation.

```csharp
public const int EGsOutOfLocalStorage = -2138898429
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsProvidedBufferTooSmall"></a> EGsProvidedBufferTooSmall

The provided buffer is too small for the game-save result.

```csharp
public const int EGsProvidedBufferTooSmall = -2138898425
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsProviderMismatch"></a> EGsProviderMismatch

The game-save update belongs to a different provider.

```csharp
public const int EGsProviderMismatch = -2138898417
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsQuotaExceeded"></a> EGsQuotaExceeded

The game-save quota has been exceeded.

```csharp
public const int EGsQuotaExceeded = -2138898426
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsUpdateTooBig"></a> EGsUpdateTooBig

The game-save update exceeds the maximum allowed size.

```csharp
public const int EGsUpdateTooBig = -2138898427
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsUserCanceled"></a> EGsUserCanceled

The user canceled the game-save operation.

```csharp
public const int EGsUserCanceled = -2138898428
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsUserNotRegisteredInService"></a> EGsUserNotRegisteredInService

The user is not registered in the game-save service.

```csharp
public const int EGsUserNotRegisteredInService = -2138898420
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EGsUserQuit"></a> EGsUserQuit

The user quit the game-save operation.

```csharp
public const int EGsUserQuit = -2138898416
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EInvalidArg"></a> EInvalidArg

One or more arguments are invalid (<code>E_INVALIDARG</code>).

```csharp
public const int EInvalidArg = -2147024809
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_ENotImpl"></a> ENotImpl

The operation is not implemented (<code>E_NOTIMPL</code>).

```csharp
public const int ENotImpl = -2147467263
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EOutOfMemory"></a> EOutOfMemory

Not enough memory to complete the operation (<code>E_OUTOFMEMORY</code>).

```csharp
public const int EOutOfMemory = -2147024882
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_EPointer"></a> EPointer

A pointer argument was invalid (<code>E_POINTER</code>).

```csharp
public const int EPointer = -2147467261
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_SFalse"></a> SFalse

Success with a negative or "no work done" result (<code>S_FALSE</code>).

```csharp
public const int SFalse = 1
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_HResult_SOk"></a> SOk

Success (<code>S_OK</code>).

```csharp
public const int SOk = 0
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### <a id="GDK_Net_HResult_Failed_System_Int32_"></a> Failed\(int\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when <code class="paramref">hresult</code> denotes failure.

```csharp
public static bool Failed(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_HResult_IsGameUser_System_Int32_"></a> IsGameUser\(int\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for codes in the <code>E_GAMEUSER_*</code> family.

```csharp
public static bool IsGameUser(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_HResult_Succeeded_System_Int32_"></a> Succeeded\(int\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when <code class="paramref">hresult</code> denotes success.

```csharp
public static bool Succeeded(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

