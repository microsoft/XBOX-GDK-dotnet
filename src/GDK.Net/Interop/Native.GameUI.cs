// P/Invoke declarations for XGameUI.h.
//
// This file covers the whole XGameUI.h surface, including the title-implemented UI path
// (XGameUiSetUiCallbacks and the eight XGameUiSet*UiResponse completions) and State Share. GDK
// edition 260404 added all eleven to xgameruntime.thunks.dll's export table.
//
// Two symbols from XGameUI.h's family remain unbound: XGameUiShowManageSpaceAsync and
// XGameUiShowManageSpaceResult. They exist in xgameruntime.lib but 260404 still declares them in no
// header at all, so there is no signature to bind against, and they are still absent from the
// thunks DLL's export table. See eng/unexported-apis.md.
//
// See Interop/Native.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XGameUI.h: message dialog ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowMessageDialogAsync(
        XAsyncBlock* async,
        byte* titleText,
        byte* contentText,
        byte* firstButtonText,
        byte* secondButtonText,
        byte* thirdButtonText,
        XGameUiMessageDialogButton defaultButton,
        XGameUiMessageDialogButton cancelButton);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowMessageDialogResult(
        XAsyncBlock* async,
        XGameUiMessageDialogButton* resultButton);

    // --- XGameUI.h: game invite ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowSendGameInviteAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* sessionConfigurationId,
        byte* sessionTemplateName,
        byte* sessionId,
        byte* invitationText,
        byte* customActivationContext);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowSendGameInviteResult(
        XAsyncBlock* async);

    // --- XGameUI.h: multiplayer activity game invite ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowMultiplayerActivityGameInviteAsync(
        XAsyncBlock* async,
        IntPtr requestingUser);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowMultiplayerActivityGameInviteResult(
        XAsyncBlock* async);

    // --- XGameUI.h: player profile card ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowPlayerProfileCardAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        ulong targetPlayer);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowPlayerProfileCardResult(
        XAsyncBlock* async);

    // --- XGameUI.h: achievements ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowAchievementsAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        uint titleId);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowAchievementsResult(
        XAsyncBlock* async);

    // --- XGameUI.h: player picker ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowPlayerPickerAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* promptText,
        uint selectFromPlayersCount,
        ulong* selectFromPlayers,
        uint preSelectedPlayersCount,
        ulong* preSelectedPlayers,
        uint minSelectionCount,
        uint maxSelectionCount);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowPlayerPickerResultCount(
        XAsyncBlock* async,
        uint* resultPlayersCount);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowPlayerPickerResult(
        XAsyncBlock* async,
        uint resultPlayersCount,
        ulong* resultPlayers,
        uint* resultPlayersUsed);

    // --- XGameUI.h: error dialog ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowErrorDialogAsync(
        XAsyncBlock* async,
        int errorCode,
        byte* context);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowErrorDialogResult(
        XAsyncBlock* async);

    // --- XGameUI.h: notification position hint ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetNotificationPositionHint(
        XGameUiNotificationPositionHint position);

    // --- XGameUI.h: text entry (modal, async) ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowTextEntryAsync(
        XAsyncBlock* async,
        byte* titleText,
        byte* descriptionText,
        byte* defaultText,
        XGameUiTextEntryInputScope inputScope,
        uint maxTextLength);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowTextEntryResultSize(
        XAsyncBlock* async,
        uint* resultTextBufferSize);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowTextEntryResult(
        XAsyncBlock* async,
        uint resultTextBufferSize,
        byte* resultTextBuffer,
        uint* resultTextBufferUsed);

    // --- XGameUI.h: web authentication ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowWebAuthenticationAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* requestUri,
        byte* completionUri);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowWebAuthenticationWithOptionsAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* requestUri,
        byte* completionUri,
        XGameUiWebAuthenticationOptions options);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowWebAuthenticationResultSize(
        XAsyncBlock* async,
        nuint* bufferSize);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowWebAuthenticationResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XGameUiWebAuthenticationResultData** ptrToBuffer,
        nuint* bufferUsed);

    // --- XGameUI.h: text entry (non-modal) ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiTextEntryOpen(
        XGameUiTextEntryOptions* options,
        uint maxLength,
        byte* initialText,
        uint cursorIndex,
        IntPtr* handle);

    [LibraryImport(LibraryName)]
    internal static partial void XGameUiTextEntryClose(
        IntPtr handle);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiTextEntryGetState(
        IntPtr handle,
        XGameUiTextEntryChangeTypeFlags* changeType,
        uint* cursorIndex,
        uint* imeClauseStartIndex,
        uint* imeClauseEndIndex,
        uint bufferSize,
        byte* buffer);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiTextEntryGetExtents(
        IntPtr handle,
        XGameUiTextEntryExtents* extents);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiTextEntryUpdatePositionHint(
        IntPtr handle,
        XGameUiTextEntryPositionHint positionHint);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiTextEntryUpdateVisibility(
        IntPtr handle,
        XGameUiTextEntryVisibilityFlags visibilityFlags);

    // --- XGameUI.h: title-implemented UI registration ---
    //
    // A title registers callbacks so that system UI requests are rendered by the game itself, then
    // answers each request by handle through the XGameUiSet*UiResponse group below.
    //
    // XGameUiSetUiCallbacks calls XThreadAssertNotTimeSensitive internally, so it must not be
    // invoked from a thread marked time-sensitive.
    //
    // useSystemUiIfAvailable is a C++ `bool` -- one byte. Passed as a byte rather than a marshalled
    // bool so the declaration is identical under both marshalling models.

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetUiCallbacks(
        XGameUiUiCallbacks* callbacks,
        byte useSystemUiIfAvailable);

    // --- XGameUI.h: title-implemented UI responses ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetMessageDialogUiResponse(
        IntPtr callbackHandle,
        XGameUiMessageDialogButton response);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetPlayerPickerUiResponse(
        IntPtr callbackHandle,
        uint playerCount,
        ulong* players);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetTextEntryUiResponse(
        IntPtr callbackHandle,
        byte* response);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetPlayerProfileCardUiResponse(
        IntPtr callbackHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetSendGameInviteUiResponse(
        IntPtr callbackHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetAchievementsUiResponse(
        IntPtr callbackHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetMultiplayerActivityGameInviteUiResponse(
        IntPtr callbackHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiSetErrorDialogUiResponse(
        IntPtr callbackHandle);

    // --- XGameUI.h: State Share ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowStateShareAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* linkToken);

    [LibraryImport(LibraryName)]
    internal static partial int XGameUiShowStateShareResult(
        XAsyncBlock* async);
}

#else

internal static unsafe partial class Native
{
    // --- XGameUI.h: message dialog ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowMessageDialogAsync(
        XAsyncBlock* async,
        byte* titleText,
        byte* contentText,
        byte* firstButtonText,
        byte* secondButtonText,
        byte* thirdButtonText,
        XGameUiMessageDialogButton defaultButton,
        XGameUiMessageDialogButton cancelButton);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowMessageDialogResult(
        XAsyncBlock* async,
        XGameUiMessageDialogButton* resultButton);

    // --- XGameUI.h: game invite ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowSendGameInviteAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* sessionConfigurationId,
        byte* sessionTemplateName,
        byte* sessionId,
        byte* invitationText,
        byte* customActivationContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowSendGameInviteResult(
        XAsyncBlock* async);

    // --- XGameUI.h: multiplayer activity game invite ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowMultiplayerActivityGameInviteAsync(
        XAsyncBlock* async,
        IntPtr requestingUser);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowMultiplayerActivityGameInviteResult(
        XAsyncBlock* async);

    // --- XGameUI.h: player profile card ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowPlayerProfileCardAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        ulong targetPlayer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowPlayerProfileCardResult(
        XAsyncBlock* async);

    // --- XGameUI.h: achievements ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowAchievementsAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        uint titleId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowAchievementsResult(
        XAsyncBlock* async);

    // --- XGameUI.h: player picker ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowPlayerPickerAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* promptText,
        uint selectFromPlayersCount,
        ulong* selectFromPlayers,
        uint preSelectedPlayersCount,
        ulong* preSelectedPlayers,
        uint minSelectionCount,
        uint maxSelectionCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowPlayerPickerResultCount(
        XAsyncBlock* async,
        uint* resultPlayersCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowPlayerPickerResult(
        XAsyncBlock* async,
        uint resultPlayersCount,
        ulong* resultPlayers,
        uint* resultPlayersUsed);

    // --- XGameUI.h: error dialog ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowErrorDialogAsync(
        XAsyncBlock* async,
        int errorCode,
        byte* context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowErrorDialogResult(
        XAsyncBlock* async);

    // --- XGameUI.h: notification position hint ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetNotificationPositionHint(
        XGameUiNotificationPositionHint position);

    // --- XGameUI.h: text entry (modal, async) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowTextEntryAsync(
        XAsyncBlock* async,
        byte* titleText,
        byte* descriptionText,
        byte* defaultText,
        XGameUiTextEntryInputScope inputScope,
        uint maxTextLength);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowTextEntryResultSize(
        XAsyncBlock* async,
        uint* resultTextBufferSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowTextEntryResult(
        XAsyncBlock* async,
        uint resultTextBufferSize,
        byte* resultTextBuffer,
        uint* resultTextBufferUsed);

    // --- XGameUI.h: web authentication ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowWebAuthenticationAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* requestUri,
        byte* completionUri);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowWebAuthenticationWithOptionsAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* requestUri,
        byte* completionUri,
        XGameUiWebAuthenticationOptions options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowWebAuthenticationResultSize(
        XAsyncBlock* async,
        nuint* bufferSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowWebAuthenticationResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XGameUiWebAuthenticationResultData** ptrToBuffer,
        nuint* bufferUsed);

    // --- XGameUI.h: text entry (non-modal) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiTextEntryOpen(
        XGameUiTextEntryOptions* options,
        uint maxLength,
        byte* initialText,
        uint cursorIndex,
        IntPtr* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameUiTextEntryClose(
        IntPtr handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiTextEntryGetState(
        IntPtr handle,
        XGameUiTextEntryChangeTypeFlags* changeType,
        uint* cursorIndex,
        uint* imeClauseStartIndex,
        uint* imeClauseEndIndex,
        uint bufferSize,
        byte* buffer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiTextEntryGetExtents(
        IntPtr handle,
        XGameUiTextEntryExtents* extents);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiTextEntryUpdatePositionHint(
        IntPtr handle,
        XGameUiTextEntryPositionHint positionHint);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiTextEntryUpdateVisibility(
        IntPtr handle,
        XGameUiTextEntryVisibilityFlags visibilityFlags);

    // --- XGameUI.h: title-implemented UI registration ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetUiCallbacks(
        XGameUiUiCallbacks* callbacks,
        byte useSystemUiIfAvailable);

    // --- XGameUI.h: title-implemented UI responses ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetMessageDialogUiResponse(
        IntPtr callbackHandle,
        XGameUiMessageDialogButton response);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetPlayerPickerUiResponse(
        IntPtr callbackHandle,
        uint playerCount,
        ulong* players);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetTextEntryUiResponse(
        IntPtr callbackHandle,
        byte* response);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetPlayerProfileCardUiResponse(
        IntPtr callbackHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetSendGameInviteUiResponse(
        IntPtr callbackHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetAchievementsUiResponse(
        IntPtr callbackHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetMultiplayerActivityGameInviteUiResponse(
        IntPtr callbackHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiSetErrorDialogUiResponse(
        IntPtr callbackHandle);

    // --- XGameUI.h: State Share ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowStateShareAsync(
        XAsyncBlock* async,
        IntPtr requestingUser,
        byte* linkToken);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameUiShowStateShareResult(
        XAsyncBlock* async);
}

#endif
