using System;
using System.Collections.Generic;

namespace GDK.Net;

/// <summary>
/// Well-known HRESULT values returned by the GDK, plus the standard COM codes the projection
/// special-cases. Values are taken verbatim from <c>XGameErr.h</c> (GDK edition 260404) and
/// <c>winerror.h</c>.
/// </summary>
public static class HResult
{
    /// <summary>Success (<c>S_OK</c>).</summary>
    public const int SOk = 0;
    /// <summary>Success with a negative or &quot;no work done&quot; result (<c>S_FALSE</c>).</summary>
    public const int SFalse = 1;

    /// <summary>The operation was aborted (<c>E_ABORT</c>).</summary>
    public const int EAbort = unchecked((int)0x80004004);
    /// <summary>Unspecified failure (<c>E_FAIL</c>).</summary>
    public const int EFail = unchecked((int)0x80004005);
    /// <summary>One or more arguments are invalid (<c>E_INVALIDARG</c>).</summary>
    public const int EInvalidArg = unchecked((int)0x80070057);
    /// <summary>Not enough memory to complete the operation (<c>E_OUTOFMEMORY</c>).</summary>
    public const int EOutOfMemory = unchecked((int)0x8007000E);
    /// <summary>The operation is not implemented (<c>E_NOTIMPL</c>).</summary>
    public const int ENotImpl = unchecked((int)0x80004001);
    /// <summary>A pointer argument was invalid (<c>E_POINTER</c>).</summary>
    public const int EPointer = unchecked((int)0x80004003);

    // E_GAMERUNTIME_* — runtime lifetime and configuration failures.
    /// <summary>The Gaming Runtime has not been initialized. Call GameRuntime.Initialize() first.</summary>
    public const int EGameRuntimeNotInitialized = unchecked((int)0x89240100);
    /// <summary>xgameruntime.thunks.dll could not be loaded. It must be redistributed next to the game executable from %GameDKCoreLatest%windows\bin\&lt;arch&gt;.</summary>
    public const int EGameRuntimeDllNotFound = unchecked((int)0x89240101);
    /// <summary>The installed Gaming Runtime does not match the GDK edition this library was built against.</summary>
    public const int EGameRuntimeVersionMismatch = unchecked((int)0x89240102);
    /// <summary>The operation requires the title window to be in the foreground.</summary>
    public const int EGameRuntimeWindowNotForeground = unchecked((int)0x89240103);
    /// <summary>The title is suspended and cannot service this call.</summary>
    public const int EGameRuntimeSuspended = unchecked((int)0x89240104);
    /// <summary>The Gaming Runtime cannot be uninitialized while GDK objects are still alive; dispose them first.</summary>
    public const int EGameRuntimeUninitializeActiveObjects = unchecked((int)0x89240105);
    /// <summary>Multiplayer is not configured for this title.</summary>
    public const int EGameRuntimeMultiplayerNotConfigured = unchecked((int)0x89240106);
    /// <summary>A required Gaming Runtime dependency is missing.</summary>
    public const int EGameRuntimeMissingDependency = unchecked((int)0x89240107);
    /// <summary>The title cannot suspend while GDK objects are still active.</summary>
    public const int EGameRuntimeSuspendActiveObjects = unchecked((int)0x89240108);
    /// <summary>The supplied runtime options do not match the options already in effect.</summary>
    public const int EGameRuntimeOptionsMismatch = unchecked((int)0x89240109);
    /// <summary>The supplied runtime options are not supported by the installed Gaming Runtime.</summary>
    public const int EGameRuntimeOptionsNotSupported = unchecked((int)0x8924010A);
    /// <summary>MicrosoftGame.config is malformed.</summary>
    public const int EGameRuntimeGameConfigBadFormat = unchecked((int)0x8924010B);
    /// <summary>The GDK handle is invalid.</summary>
    public const int EGameRuntimeInvalidHandle = unchecked((int)0x8924010C);
    /// <summary>The required Gaming service is not available.</summary>
    public const int EGameRuntimeServiceNotAvailable = unchecked((int)0x8924010D);

    // --- XNetworking / XAppCapture / XAppBroadcast ---
    // XGameErr.h (GDK edition 260404) defines no family-specific HRESULT codes for these APIs.
    // Standard codes (E_INVALIDARG, E_FAIL, E_POINTER) apply; no new constants are needed here.

    // --- XGameSave ---
    // E_GS_* — XGameSave failures (XGameSave.h, GDK edition 260404).
    /// <summary>The game-save container name is invalid.</summary>
    public const int EGsInvalidContainerName = unchecked((int)0x80830001);
    /// <summary>Access to the game-save provider was denied.</summary>
    public const int EGsNoAccess = unchecked((int)0x80830002);
    /// <summary>Insufficient local storage for the game-save operation.</summary>
    public const int EGsOutOfLocalStorage = unchecked((int)0x80830003);
    /// <summary>The user canceled the game-save operation.</summary>
    public const int EGsUserCanceled = unchecked((int)0x80830004);
    /// <summary>The game-save update exceeds the maximum allowed size.</summary>
    public const int EGsUpdateTooBig = unchecked((int)0x80830005);
    /// <summary>The game-save quota has been exceeded.</summary>
    public const int EGsQuotaExceeded = unchecked((int)0x80830006);
    /// <summary>The provided buffer is too small for the game-save result.</summary>
    public const int EGsProvidedBufferTooSmall = unchecked((int)0x80830007);
    /// <summary>The requested game-save blob was not found.</summary>
    public const int EGsBlobNotFound = unchecked((int)0x80830008);
    /// <summary>No service configuration is set for this title's game saves.</summary>
    public const int EGsNoServiceConfiguration = unchecked((int)0x80830009);
    /// <summary>The game-save container is not in sync with the cloud.</summary>
    public const int EGsContainerNotInSync = unchecked((int)0x8083000A);
    /// <summary>The game-save container failed to sync with the cloud.</summary>
    public const int EGsContainerSyncFailed = unchecked((int)0x8083000B);
    /// <summary>The user is not registered in the game-save service.</summary>
    public const int EGsUserNotRegisteredInService = unchecked((int)0x8083000C);
    /// <summary>The game-save handle has expired.</summary>
    public const int EGsHandleExpired = unchecked((int)0x8083000D);
    /// <summary>This game-save operation requires the async variant.</summary>
    public const int EGsAsyncFunctionRequired = unchecked((int)0x8083000E);
    /// <summary>The game-save update belongs to a different provider.</summary>
    public const int EGsProviderMismatch = unchecked((int)0x8083000F);
    /// <summary>The user quit the game-save operation.</summary>
    public const int EGsUserQuit = unchecked((int)0x80830010);

    // --- XPackage ---
    // E_GAMEPACKAGE_* — packaging, chunk and mount failures (XGameErr.h, GDK 260404).
    /// <summary>The title must be run as a packaged GDK app; this process has no package identity.</summary>
    public const int EGamePackageAppNotPackaged = unchecked((int)0x89245200);
    /// <summary>No languages are installed for this package.</summary>
    public const int EGamePackageNoInstalledLanguages = unchecked((int)0x89245201);
    /// <summary>The package does not have a store id.</summary>
    public const int EGamePackageNoStoreId = unchecked((int)0x89245202);
    /// <summary>A chunk selector was provided that does not resolve to a chunk in the package.</summary>
    public const int EGamePackageInvalidSelector = unchecked((int)0x89245203);
    /// <summary>One or more required chunks must be downloaded to mount the package on this device.</summary>
    public const int EGamePackageDownloadRequired = unchecked((int)0x89245204);
    /// <summary>Change installed chunks in this package using Features, not tags.</summary>
    public const int EGamePackageNoTagChange = unchecked((int)0x89245205);
    /// <summary>DLC is not supported for this package.</summary>
    public const int EGamePackageDlcNotSupported = unchecked((int)0x89245206);
    /// <summary>The package contains duplicate executable id attributes in the game config.</summary>
    public const int EGamePackageDuplicateIdValues = unchecked((int)0x89245207);
    /// <summary>Could not resolve a package identifier; the package may not be installed.</summary>
    public const int EGamePackageNoPackageIdentifier = unchecked((int)0x89245208);
    /// <summary>MicrosoftGame.config is missing the root node.</summary>
    public const int EGamePackageConfigNoRootNode = unchecked((int)0x89245209);
    /// <summary>MicrosoftGame.config specifies a zero version.</summary>
    public const int EGamePackageConfigZeroVersion = unchecked((int)0x8924520A);
    /// <summary>MicrosoftGame.config is missing both MSA app id and title id.</summary>
    public const int EGamePackageConfigNoMsaAppIdOrTitleId = unchecked((int)0x8924520B);

    // --- XStore ---
    // E_GAMESTORE_* — Store and licence operation failures (XGameErr.h, GDK 260404).
    /// <summary>The licence action is not applicable to this product type.</summary>
    public const int EGameStoreLicenseActionNotApplicableToProduct = unchecked((int)0x89245300);
    /// <summary>A Store network error occurred.</summary>
    public const int EGameStoreNetworkError = unchecked((int)0x89245301);
    /// <summary>A Store server error occurred.</summary>
    public const int EGameStoreServerError = unchecked((int)0x89245302);
    /// <summary>Insufficient consumable quantity to fulfil the request.</summary>
    public const int EGameStoreInsufficientQuantity = unchecked((int)0x89245303);
    /// <summary>The product has already been purchased.</summary>
    public const int EGameStoreAlreadyPurchased = unchecked((int)0x89245304);
    /// <summary>The licence action has been throttled; retry after a delay.</summary>
    public const int EGameStoreLicenseActionThrottled = unchecked((int)0x89245305);

    // --- XGameStreaming ---
    // E_GAMESTREAMING_* — Xbox Cloud Gaming failures (XGameErr.h, GDK 260404), range 0x5400–0x54FF.
    /// <summary>XGameStreamingInitialize has not been called. Call StreamingManager.Initialize() first.</summary>
    public const int EGameStreamingNotInitialized = unchecked((int)0x89245400);
    /// <summary>The streaming client is not connected.</summary>
    public const int EGameStreamingClientNotConnected = unchecked((int)0x89245401);
    /// <summary>No streaming data is available. On the converged (WebRTC) stack some deprecated APIs always return this code.</summary>
    public const int EGameStreamingNoData = unchecked((int)0x89245402);
    /// <summary>No streaming data center information is available.</summary>
    public const int EGameStreamingNoDataCenter = unchecked((int)0x89245403);
    /// <summary>The controller is not a streaming controller.</summary>
    public const int EGameStreamingNotStreamingController = unchecked((int)0x89245404);
    /// <summary>No matching streaming resource was found.</summary>
    public const int EGameStreamingNoMatch = unchecked((int)0x89245405);
    /// <summary>Too many concurrent streaming API calls.</summary>
    public const int EGameStreamingTooManyCalls = unchecked((int)0x89245406);
    /// <summary>Custom stream resolution is not supported by the connected client.</summary>
    public const int EGameStreamingCustomResolutionNotSupported = unchecked((int)0x89245407);
    /// <summary>The requested custom stream resolution is too small.</summary>
    public const int EGameStreamingCustomResolutionTooSmall = unchecked((int)0x89245408);
    /// <summary>The requested custom stream resolution is too large.</summary>
    public const int EGameStreamingCustomResolutionTooLarge = unchecked((int)0x89245409);
    /// <summary>The requested custom stream resolution exceeds the maximum pixel count.</summary>
    public const int EGameStreamingCustomResolutionTooManyPixels = unchecked((int)0x8924540A);
    /// <summary>The requested custom stream resolution is invalid.</summary>
    public const int EGameStreamingInvalidCustomResolution = unchecked((int)0x8924540B);

    // E_GAMEUSER_* — sign-in, sandbox and token failures.
    /// <summary>There is no authenticated user.</summary>
    public const int EGameUserNoAuthUser = unchecked((int)0x87DD0013);
    /// <summary>The user does not have access to the current sandbox.</summary>
    public const int EGameUserUserNotInSandbox = unchecked((int)0x8015DC12);
    /// <summary>The maximum number of users has already been added.</summary>
    public const int EGameUserMaxUsersAdded = unchecked((int)0x89245100);
    /// <summary>The user is signed out.</summary>
    public const int EGameUserSignedOut = unchecked((int)0x89245101);
    /// <summary>The user must resolve an account issue before continuing.</summary>
    public const int EGameUserResolveUserIssueRequired = unchecked((int)0x89245102);
    /// <summary>A sign-out deferral is not available.</summary>
    public const int EGameUserDeferralNotAvailable = unchecked((int)0x89245103);
    /// <summary>The user could not be found.</summary>
    public const int EGameUserUserNotFound = unchecked((int)0x89245104);
    /// <summary>No token is required for this operation.</summary>
    public const int EGameUserNoTokenRequired = unchecked((int)0x89245105);
    /// <summary>There is no default user to add silently.</summary>
    public const int EGameUserNoDefaultUser = unchecked((int)0x89245106);
    /// <summary>The user could not be resolved.</summary>
    public const int EGameUserFailedToResolve = unchecked((int)0x89245107);
    /// <summary>The title id is missing from MicrosoftGame.config.</summary>
    public const int EGameUserNoTitleId = unchecked((int)0x89245108);
    /// <summary>The game identity is unknown to the Gaming Runtime.</summary>
    public const int EGameUserUnknownGameIdentity = unchecked((int)0x89245109);
    /// <summary>The process has no package identity; run the title as a packaged GDK app.</summary>
    public const int EGameUserNoPackageIdentity = unchecked((int)0x89245110);
    /// <summary>A user token could not be acquired.</summary>
    public const int EGameUserFailedToGetToken = unchecked((int)0x89245111);
    /// <summary>The app configuration is invalid.</summary>
    public const int EGameUserInvalidAppConfiguration = unchecked((int)0x89245112);
    /// <summary>The MSA app id in MicrosoftGame.config is malformed.</summary>
    public const int EGameUserMalformedMsaAppId = unchecked((int)0x89245113);
    /// <summary>The MSA app id and title id in MicrosoftGame.config are inconsistent.</summary>
    public const int EGameUserInconsistentMsaAppIdAndTitleId = unchecked((int)0x89245114);
    /// <summary>MicrosoftGame.config does not declare an MSA app id.</summary>
    public const int EGameUserNoMsaAppId = unchecked((int)0x89245115);

    /// <summary>Returns <see langword="true"/> when <paramref name="hresult"/> denotes success.</summary>
    public static bool Succeeded(int hresult) => hresult >= 0;

    /// <summary>Returns <see langword="true"/> when <paramref name="hresult"/> denotes failure.</summary>
    public static bool Failed(int hresult) => hresult < 0;

    /// <summary>Returns <see langword="true"/> for codes in the <c>E_GAMEUSER_*</c> family.</summary>
    public static bool IsGameUser(int hresult) => UserMessages.ContainsKey(hresult);

    internal static string Describe(int hresult)
    {
        if (UserMessages.TryGetValue(hresult, out var userMessage))
        {
            return userMessage;
        }

        return RuntimeMessages.TryGetValue(hresult, out var runtimeMessage)
            ? runtimeMessage
            : $"The GDK call failed with HRESULT 0x{hresult:X8}.";
    }

    private static readonly Dictionary<int, string> RuntimeMessages = new()
    {
        [EGameRuntimeNotInitialized] = "The Gaming Runtime has not been initialized. Call GameRuntime.Initialize() first.",
        [EGameRuntimeDllNotFound] = "xgameruntime.thunks.dll could not be loaded. It must be redistributed next to the game executable from %GameDKCoreLatest%windows\\bin\\<arch>.",
        [EGameRuntimeVersionMismatch] = "The installed Gaming Runtime does not match the GDK edition this library was built against.",
        [EGameRuntimeWindowNotForeground] = "The operation requires the title window to be in the foreground.",
        [EGameRuntimeSuspended] = "The title is suspended and cannot service this call.",
        [EGameRuntimeUninitializeActiveObjects] = "The Gaming Runtime cannot be uninitialized while GDK objects are still alive; dispose them first.",
        [EGameRuntimeMultiplayerNotConfigured] = "Multiplayer is not configured for this title.",
        [EGameRuntimeMissingDependency] = "A required Gaming Runtime dependency is missing.",
        [EGameRuntimeSuspendActiveObjects] = "The title cannot suspend while GDK objects are still active.",
        [EGameRuntimeOptionsMismatch] = "The supplied runtime options do not match the options already in effect.",
        [EGameRuntimeOptionsNotSupported] = "The supplied runtime options are not supported by the installed Gaming Runtime.",
        [EGameRuntimeGameConfigBadFormat] = "MicrosoftGame.config is malformed.",
        [EGameRuntimeInvalidHandle] = "The GDK handle is invalid.",
        [EGameRuntimeServiceNotAvailable] = "The required Gaming service is not available.",
        // --- XGameSave ---
        [EGsInvalidContainerName] = "The game-save container name is invalid.",
        [EGsNoAccess] = "Access to the game-save provider was denied.",
        [EGsOutOfLocalStorage] = "Insufficient local storage for the game-save operation.",
        [EGsUserCanceled] = "The user canceled the game-save operation.",
        [EGsUpdateTooBig] = "The game-save update exceeds the maximum allowed size.",
        [EGsQuotaExceeded] = "The game-save quota has been exceeded.",
        [EGsProvidedBufferTooSmall] = "The provided buffer is too small for the game-save result.",
        [EGsBlobNotFound] = "The requested game-save blob was not found.",
        [EGsNoServiceConfiguration] = "No service configuration is set for this title's game saves.",
        [EGsContainerNotInSync] = "The game-save container is not in sync with the cloud.",
        [EGsContainerSyncFailed] = "The game-save container failed to sync with the cloud.",
        [EGsUserNotRegisteredInService] = "The user is not registered in the game-save service.",
        [EGsHandleExpired] = "The game-save handle has expired.",
        [EGsAsyncFunctionRequired] = "This game-save operation requires the async variant.",
        [EGsProviderMismatch] = "The game-save update belongs to a different provider.",
        [EGsUserQuit] = "The user quit the game-save operation.",

        // --- XPackage ---
        [EGamePackageAppNotPackaged] = "The title must be run as a packaged GDK app; this process has no package identity.",
        [EGamePackageNoInstalledLanguages] = "No languages are installed for this package.",
        [EGamePackageNoStoreId] = "The package does not have a store id.",
        [EGamePackageInvalidSelector] = "A chunk selector was provided that does not resolve to a chunk in the package.",
        [EGamePackageDownloadRequired] = "One or more required chunks must be downloaded to mount the package on this device.",
        [EGamePackageNoTagChange] = "Change installed chunks in this package using Features, not tags.",
        [EGamePackageDlcNotSupported] = "DLC is not supported for this package.",
        [EGamePackageDuplicateIdValues] = "The package contains duplicate executable id attributes in the game config.",
        [EGamePackageNoPackageIdentifier] = "Could not resolve a package identifier; the package may not be installed.",
        [EGamePackageConfigNoRootNode] = "MicrosoftGame.config is missing the root node.",
        [EGamePackageConfigZeroVersion] = "MicrosoftGame.config specifies a zero version.",
        [EGamePackageConfigNoMsaAppIdOrTitleId] = "MicrosoftGame.config is missing both MSA app id and title id.",

        // --- XStore ---
        [EGameStoreLicenseActionNotApplicableToProduct] = "The licence action is not applicable to this product type.",
        [EGameStoreNetworkError] = "A Store network error occurred.",
        [EGameStoreServerError] = "A Store server error occurred.",
        [EGameStoreInsufficientQuantity] = "Insufficient consumable quantity to fulfil the request.",
        [EGameStoreAlreadyPurchased] = "The product has already been purchased.",
        [EGameStoreLicenseActionThrottled] = "The licence action has been throttled; retry after a delay.",

        // --- XGameStreaming ---
        [EGameStreamingNotInitialized] = "XGameStreamingInitialize has not been called. Call StreamingManager.Initialize() first.",
        [EGameStreamingClientNotConnected] = "The streaming client is not connected.",
        [EGameStreamingNoData] = "No streaming data is available. On the converged (WebRTC) stack some deprecated APIs always return this code.",
        [EGameStreamingNoDataCenter] = "No streaming data center information is available.",
        [EGameStreamingNotStreamingController] = "The controller is not a streaming controller.",
        [EGameStreamingNoMatch] = "No matching streaming resource was found.",
        [EGameStreamingTooManyCalls] = "Too many concurrent streaming API calls.",
        [EGameStreamingCustomResolutionNotSupported] = "Custom stream resolution is not supported by the connected client.",
        [EGameStreamingCustomResolutionTooSmall] = "The requested custom stream resolution is too small.",
        [EGameStreamingCustomResolutionTooLarge] = "The requested custom stream resolution is too large.",
        [EGameStreamingCustomResolutionTooManyPixels] = "The requested custom stream resolution exceeds the maximum pixel count.",
        [EGameStreamingInvalidCustomResolution] = "The requested custom stream resolution is invalid.",
    };

    private static readonly Dictionary<int, string> UserMessages = new()
    {
        [EGameUserNoAuthUser] = "There is no authenticated user.",
        [EGameUserUserNotInSandbox] = "The user does not have access to the current sandbox.",
        [EGameUserMaxUsersAdded] = "The maximum number of users has already been added.",
        [EGameUserSignedOut] = "The user is signed out.",
        [EGameUserResolveUserIssueRequired] = "The user must resolve an account issue before continuing.",
        [EGameUserDeferralNotAvailable] = "A sign-out deferral is not available.",
        [EGameUserUserNotFound] = "The user could not be found.",
        [EGameUserNoTokenRequired] = "No token is required for this operation.",
        [EGameUserNoDefaultUser] = "There is no default user to add silently.",
        [EGameUserFailedToResolve] = "The user could not be resolved.",
        [EGameUserNoTitleId] = "The title id is missing from MicrosoftGame.config.",
        [EGameUserUnknownGameIdentity] = "The game identity is unknown to the Gaming Runtime.",
        [EGameUserNoPackageIdentity] = "The process has no package identity; run the title as a packaged GDK app.",
        [EGameUserFailedToGetToken] = "A user token could not be acquired.",
        [EGameUserInvalidAppConfiguration] = "The app configuration is invalid.",
        [EGameUserMalformedMsaAppId] = "The MSA app id in MicrosoftGame.config is malformed.",
        [EGameUserInconsistentMsaAppIdAndTitleId] = "The MSA app id and title id in MicrosoftGame.config are inconsistent.",
        [EGameUserNoMsaAppId] = "MicrosoftGame.config does not declare an MSA app id.",
    };
}
