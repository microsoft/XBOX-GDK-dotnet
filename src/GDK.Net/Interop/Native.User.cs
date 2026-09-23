// P/Invoke declarations for XUser.h (GDK edition 260404).
//
// The whole XUser surface that xgameruntime.thunks.dll exports lives here. See Native.cs for the
// module-level notes that govern this layer and for the dual-shim rule: [LibraryImport] on
// NET7_0_OR_GREATER, [DllImport] on netstandard2.0, both blocks signature-identical.
//
// Sign-out (XUserSignOutAsync / XUserSignOutResult / XUserIsSignOutPresent) and every
// XUserPlatform* entry point were absent from the export table until edition 260404 added them;
// they are bound below.
//
// Declared in XUser.h but still NOT exported, and therefore absent here: the deprecated
// XUserGetMsaTokenSilentlyAsync / XUserGetMsaTokenSilentlyResult / XUserGetMsaTokenSilentlyResultSize
// trio. XUserGetTokenAndSignature* covers the common token case. See eng/unexported-apis.md.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- handle lifetime ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XUserCloseHandle(IntPtr user);

    [LibraryImport(LibraryName)]
    internal static partial int XUserCompare(IntPtr user1, IntPtr user2);

    // --- sign-in ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserAddAsync(XUserAddOptions options, XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserAddResult(XAsyncBlock* async, IntPtr* newUser);

    [LibraryImport(LibraryName)]
    internal static partial int XUserAddByIdWithUiAsync(ulong userId, XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserAddByIdWithUiResult(XAsyncBlock* async, IntPtr* newUser);

    // --- sign-out deferral ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetSignOutDeferral(IntPtr* deferral);

    [LibraryImport(LibraryName)]
    internal static partial void XUserCloseSignOutDeferralHandle(IntPtr deferral);

    // --- user lookup ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetMaxUsers(uint* maxUsers);

    [LibraryImport(LibraryName)]
    internal static partial int XUserFindUserById(ulong userId, IntPtr* handle);

    [LibraryImport(LibraryName)]
    internal static partial int XUserFindUserByLocalId(XUserLocalId userLocalId, IntPtr* handle);

    [LibraryImport(LibraryName)]
    internal static partial int XUserFindForDevice(XAppLocalDeviceId* deviceId, IntPtr* handle);

    // --- synchronous getters ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetId(IntPtr user, ulong* userId);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetLocalId(IntPtr user, XUserLocalId* userLocalId);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetIsGuest(IntPtr user, byte* isGuest);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetState(IntPtr user, XUserState* state);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetAgeGroup(IntPtr user, XUserAgeGroup* ageGroup);

    [LibraryImport(LibraryName)]
    internal static partial byte XUserIsStoreUser(IntPtr user);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetGamertag(
        IntPtr user,
        XUserGamertagComponent gamertagComponent,
        nuint gamertagSize,
        byte* gamertag,
        nuint* gamertagUsed);

    // --- privileges ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserCheckPrivilege(
        IntPtr user,
        XUserPrivilegeOptions options,
        XUserPrivilege privilege,
        byte* hasPrivilege,
        XUserPrivilegeDenyReason* reason);

    [LibraryImport(LibraryName)]
    internal static partial int XUserResolvePrivilegeWithUiAsync(
        IntPtr user,
        XUserPrivilegeOptions options,
        XUserPrivilege privilege,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserResolvePrivilegeWithUiResult(XAsyncBlock* async);

    // --- gamer picture (sized async) ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetGamerPictureAsync(
        IntPtr user,
        XUserGamerPictureSize pictureSize,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetGamerPictureResultSize(XAsyncBlock* async, nuint* bufferSize);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetGamerPictureResult(
        XAsyncBlock* async,
        nuint bufferSize,
        byte* buffer,
        nuint* bufferUsed);

    // --- token and signature (UTF-8) ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetTokenAndSignatureAsync(
        IntPtr user,
        XUserGetTokenAndSignatureOptions options,
        byte* method,
        byte* url,
        nuint headerCount,
        XUserGetTokenAndSignatureHttpHeader* headers,
        nuint bodySize,
        void* bodyBuffer,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetTokenAndSignatureResultSize(
        XAsyncBlock* async,
        nuint* bufferSize);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetTokenAndSignatureResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XUserGetTokenAndSignatureData** ptrToBuffer,
        nuint* bufferUsed);

    // --- token and signature (UTF-16) ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetTokenAndSignatureUtf16Async(
        IntPtr user,
        XUserGetTokenAndSignatureOptions options,
        char* method,
        char* url,
        nuint headerCount,
        XUserGetTokenAndSignatureUtf16HttpHeader* headers,
        nuint bodySize,
        void* bodyBuffer,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetTokenAndSignatureUtf16ResultSize(
        XAsyncBlock* async,
        nuint* bufferSize);

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetTokenAndSignatureUtf16Result(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XUserGetTokenAndSignatureUtf16Data** ptrToBuffer,
        nuint* bufferUsed);

    // --- issue resolution with UI ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserResolveIssueWithUiAsync(
        IntPtr user,
        byte* url,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserResolveIssueWithUiResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserResolveIssueWithUiUtf16Async(
        IntPtr user,
        char* url,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserResolveIssueWithUiUtf16Result(XAsyncBlock* async);

    // --- controller association ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserFindControllerForUserWithUiAsync(IntPtr user, XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XUserFindControllerForUserWithUiResult(
        XAsyncBlock* async,
        XAppLocalDeviceId* deviceId);

    // --- default audio endpoint ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserGetDefaultAudioEndpointUtf16(
        XUserLocalId user,
        XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
        nuint endpointIdUtf16Count,
        char* endpointIdUtf16,
        nuint* endpointIdUtf16Used);

    // --- change events ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserRegisterForChangeEvent(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XUserUnregisterForChangeEvent(XTaskQueueRegistrationToken token, byte wait);

    [LibraryImport(LibraryName)]
    internal static partial int XUserRegisterForDeviceAssociationChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XUserUnregisterForDeviceAssociationChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    [LibraryImport(LibraryName)]
    internal static partial int XUserRegisterForDefaultAudioEndpointUtf16Changed(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XUserUnregisterForDefaultAudioEndpointUtf16Changed(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XUser.h: sign-out ---

    [LibraryImport(LibraryName)]
    internal static partial int XUserSignOutAsync(IntPtr user, XAsyncBlock* asyncBlock);

    [LibraryImport(LibraryName)]
    internal static partial int XUserSignOutResult(XAsyncBlock* asyncBlock);

    [LibraryImport(LibraryName)]
    internal static partial int XUserIsSignOutPresent(byte* present);

    // --- XUser.h: the platform sign-in prompt hooks ---
    //
    // These four are the platform-implementation hooks a title supplies so the Gaming Runtime can
    // ask it to draw sign-in prompts itself instead of using the system UI: the remote-connect
    // ("sign in from another device") prompt and the SPOP ("signed in on another device") prompt.
    // Unlike every other event source in this projection, they install *process-global* handler
    // tables rather than returning a per-registration token, which is why UserPlatform is a static
    // type.

    [LibraryImport(LibraryName)]
    internal static partial int XUserPlatformRemoteConnectSetEventHandlers(
        IntPtr queue,
        XUserPlatformRemoteConnectEventHandlers* handlers);

    [LibraryImport(LibraryName)]
    internal static partial int XUserPlatformRemoteConnectCancelPrompt(IntPtr operation);

    [LibraryImport(LibraryName)]
    internal static partial int XUserPlatformSpopPromptSetEventHandlers(
        IntPtr queue,
        IntPtr handler,
        IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XUserPlatformSpopPromptComplete(
        IntPtr operation,
        XUserPlatformSpopOperationResult result);
}

#else

internal static unsafe partial class Native
{
    // --- handle lifetime ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XUserCloseHandle(IntPtr user);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserCompare(IntPtr user1, IntPtr user2);

    // --- sign-in ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserAddAsync(XUserAddOptions options, XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserAddResult(XAsyncBlock* async, IntPtr* newUser);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserAddByIdWithUiAsync(ulong userId, XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserAddByIdWithUiResult(XAsyncBlock* async, IntPtr* newUser);

    // --- sign-out deferral ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetSignOutDeferral(IntPtr* deferral);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XUserCloseSignOutDeferralHandle(IntPtr deferral);

    // --- user lookup ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetMaxUsers(uint* maxUsers);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserFindUserById(ulong userId, IntPtr* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserFindUserByLocalId(XUserLocalId userLocalId, IntPtr* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserFindForDevice(XAppLocalDeviceId* deviceId, IntPtr* handle);

    // --- synchronous getters ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetId(IntPtr user, ulong* userId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetLocalId(IntPtr user, XUserLocalId* userLocalId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetIsGuest(IntPtr user, byte* isGuest);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetState(IntPtr user, XUserState* state);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetAgeGroup(IntPtr user, XUserAgeGroup* ageGroup);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XUserIsStoreUser(IntPtr user);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetGamertag(
        IntPtr user,
        XUserGamertagComponent gamertagComponent,
        nuint gamertagSize,
        byte* gamertag,
        nuint* gamertagUsed);

    // --- privileges ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserCheckPrivilege(
        IntPtr user,
        XUserPrivilegeOptions options,
        XUserPrivilege privilege,
        byte* hasPrivilege,
        XUserPrivilegeDenyReason* reason);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserResolvePrivilegeWithUiAsync(
        IntPtr user,
        XUserPrivilegeOptions options,
        XUserPrivilege privilege,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserResolvePrivilegeWithUiResult(XAsyncBlock* async);

    // --- gamer picture (sized async) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetGamerPictureAsync(
        IntPtr user,
        XUserGamerPictureSize pictureSize,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetGamerPictureResultSize(XAsyncBlock* async, nuint* bufferSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetGamerPictureResult(
        XAsyncBlock* async,
        nuint bufferSize,
        byte* buffer,
        nuint* bufferUsed);

    // --- token and signature (UTF-8) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetTokenAndSignatureAsync(
        IntPtr user,
        XUserGetTokenAndSignatureOptions options,
        byte* method,
        byte* url,
        nuint headerCount,
        XUserGetTokenAndSignatureHttpHeader* headers,
        nuint bodySize,
        void* bodyBuffer,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetTokenAndSignatureResultSize(
        XAsyncBlock* async,
        nuint* bufferSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetTokenAndSignatureResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XUserGetTokenAndSignatureData** ptrToBuffer,
        nuint* bufferUsed);

    // --- token and signature (UTF-16) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetTokenAndSignatureUtf16Async(
        IntPtr user,
        XUserGetTokenAndSignatureOptions options,
        char* method,
        char* url,
        nuint headerCount,
        XUserGetTokenAndSignatureUtf16HttpHeader* headers,
        nuint bodySize,
        void* bodyBuffer,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetTokenAndSignatureUtf16ResultSize(
        XAsyncBlock* async,
        nuint* bufferSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetTokenAndSignatureUtf16Result(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XUserGetTokenAndSignatureUtf16Data** ptrToBuffer,
        nuint* bufferUsed);

    // --- issue resolution with UI ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserResolveIssueWithUiAsync(
        IntPtr user,
        byte* url,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserResolveIssueWithUiResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserResolveIssueWithUiUtf16Async(
        IntPtr user,
        char* url,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserResolveIssueWithUiUtf16Result(XAsyncBlock* async);

    // --- controller association ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserFindControllerForUserWithUiAsync(IntPtr user, XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserFindControllerForUserWithUiResult(
        XAsyncBlock* async,
        XAppLocalDeviceId* deviceId);

    // --- default audio endpoint ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserGetDefaultAudioEndpointUtf16(
        XUserLocalId user,
        XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
        nuint endpointIdUtf16Count,
        char* endpointIdUtf16,
        nuint* endpointIdUtf16Used);

    // --- change events ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserRegisterForChangeEvent(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XUserUnregisterForChangeEvent(XTaskQueueRegistrationToken token, byte wait);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserRegisterForDeviceAssociationChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XUserUnregisterForDeviceAssociationChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserRegisterForDefaultAudioEndpointUtf16Changed(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XUserUnregisterForDefaultAudioEndpointUtf16Changed(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XUser.h: sign-out ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserSignOutAsync(IntPtr user, XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserSignOutResult(XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserIsSignOutPresent(byte* present);

    // --- XUser.h: the platform sign-in prompt hooks ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserPlatformRemoteConnectSetEventHandlers(
        IntPtr queue,
        XUserPlatformRemoteConnectEventHandlers* handlers);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserPlatformRemoteConnectCancelPrompt(IntPtr operation);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserPlatformSpopPromptSetEventHandlers(
        IntPtr queue,
        IntPtr handler,
        IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XUserPlatformSpopPromptComplete(
        IntPtr operation,
        XUserPlatformSpopOperationResult result);
}

#endif
