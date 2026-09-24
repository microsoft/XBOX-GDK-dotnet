using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.GameUI;

/// <summary>
/// System UI overlays for GDK titles: dialogs, player pickers, achievements, web authentication
/// and non-modal text entry. Reached through <see cref="GameRuntime.GameUi"/>.
/// </summary>
/// <remarks>
/// All async methods use <c>XAsyncBlock</c> under the hood and are driven by the
/// <see cref="GameRuntime"/>'s task queue. Methods that accept a <see cref="User"/> extract the
/// native <c>XUserHandle</c> at call time; the user must remain alive until the operation
/// completes.
/// </remarks>
public sealed unsafe class GameUiManager : IDisposable
{
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    // HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER)
    private const int HResultInsufficientBuffer = unchecked((int)0x8007007A);

    internal GameUiManager(GameTaskQueue? queue)
    {
        _queue = queue;
    }

    // -----------------------------------------------------------------------
    // Notification position hint (synchronous)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Moves the system notification overlay to the specified screen region
    /// (<c>XGameUiSetNotificationPositionHint</c>).
    /// </summary>
    /// <param name="position">Desired screen region.</param>
    public void SetNotificationPositionHint(NotificationPositionHint position)
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(
            Native.XGameUiSetNotificationPositionHint((XGameUiNotificationPositionHint)position));
    }

    // -----------------------------------------------------------------------
    // Message dialog
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows a modal message dialog with up to three buttons and returns the button the user
    /// activated (<c>XGameUiShowMessageDialogAsync</c> / <c>XGameUiShowMessageDialogResult</c>).
    /// </summary>
    /// <param name="titleText">Dialog title (required).</param>
    /// <param name="contentText">Dialog body text (required).</param>
    /// <param name="firstButtonText">Label for the first button, or <see langword="null"/> to hide it.</param>
    /// <param name="secondButtonText">Label for the second button, or <see langword="null"/> to hide it.</param>
    /// <param name="thirdButtonText">Label for the third button, or <see langword="null"/> to hide it.</param>
    /// <param name="defaultButton">Button selected by default.</param>
    /// <param name="cancelButton">Button activated when the user dismisses the dialog.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<MessageDialogButton> ShowMessageDialogAsync(
        string titleText,
        string contentText,
        string? firstButtonText = null,
        string? secondButtonText = null,
        string? thirdButtonText = null,
        MessageDialogButton defaultButton = MessageDialogButton.First,
        MessageDialogButton cancelButton = MessageDialogButton.First,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr titleUtf8 = Utf8.Allocate(titleText);
        IntPtr contentUtf8 = Utf8.Allocate(contentText);
        IntPtr firstUtf8 = Utf8.Allocate(firstButtonText);
        IntPtr secondUtf8 = Utf8.Allocate(secondButtonText);
        IntPtr thirdUtf8 = Utf8.Allocate(thirdButtonText);
        try
        {
            XGameUiMessageDialogButton nativeDefault = (XGameUiMessageDialogButton)defaultButton;
            XGameUiMessageDialogButton nativeCancel = (XGameUiMessageDialogButton)cancelButton;

            return AsyncOperation<MessageDialogButton>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowMessageDialogAsync(
                    (XAsyncBlock*)block,
                    (byte*)titleUtf8,
                    (byte*)contentUtf8,
                    (byte*)firstUtf8,
                    (byte*)secondUtf8,
                    (byte*)thirdUtf8,
                    nativeDefault,
                    nativeCancel),
                static (IntPtr block, out MessageDialogButton value) =>
                {
                    value = MessageDialogButton.First;
                    XGameUiMessageDialogButton result;
                    int hr = Native.XGameUiShowMessageDialogResult((XAsyncBlock*)block, &result);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    value = (MessageDialogButton)result;
                    return HResult.SOk;
                },
                cancellationToken);
        }
        finally
        {
            Utf8.Free(titleUtf8);
            Utf8.Free(contentUtf8);
            Utf8.Free(firstUtf8);
            Utf8.Free(secondUtf8);
            Utf8.Free(thirdUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // Error dialog
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows a system error dialog for the supplied HRESULT
    /// (<c>XGameUiShowErrorDialogAsync</c> / <c>XGameUiShowErrorDialogResult</c>).
    /// </summary>
    /// <param name="errorCode">The failing HRESULT to display.</param>
    /// <param name="context">Optional context string shown alongside the error.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task ShowErrorDialogAsync(
        int errorCode,
        string? context = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr contextUtf8 = Utf8.Allocate(context);
        try
        {
            return AsyncOperation<bool>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowErrorDialogAsync((XAsyncBlock*)block, errorCode, (byte*)contextUtf8),
                static (IntPtr block, out bool value) =>
                {
                    value = true;
                    return Native.XGameUiShowErrorDialogResult((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        finally
        {
            Utf8.Free(contextUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // Text entry (modal, async)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows a modal virtual-keyboard text entry dialog and returns the user's input
    /// (<c>XGameUiShowTextEntryAsync</c> / <c>XGameUiShowTextEntryResult</c>).
    /// </summary>
    /// <param name="titleText">Dialog title, or <see langword="null"/> for none.</param>
    /// <param name="descriptionText">Body text, or <see langword="null"/> for none.</param>
    /// <param name="defaultText">Pre-filled text, or <see langword="null"/> for empty.</param>
    /// <param name="inputScope">Keyboard layout hint.</param>
    /// <param name="maxTextLength">Maximum number of characters the user may enter.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <returns>The string the user confirmed, or an empty string when dismissed.</returns>
    public Task<string> ShowTextEntryAsync(
        string? titleText = null,
        string? descriptionText = null,
        string? defaultText = null,
        TextEntryInputScope inputScope = TextEntryInputScope.Default,
        uint maxTextLength = 256,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr titleUtf8 = Utf8.Allocate(titleText);
        IntPtr descUtf8 = Utf8.Allocate(descriptionText);
        IntPtr defaultUtf8 = Utf8.Allocate(defaultText);
        XGameUiTextEntryInputScope nativeScope = (XGameUiTextEntryInputScope)inputScope;
        try
        {
            return AsyncOperation<string>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowTextEntryAsync(
                    (XAsyncBlock*)block,
                    (byte*)titleUtf8,
                    (byte*)descUtf8,
                    (byte*)defaultUtf8,
                    nativeScope,
                    maxTextLength),
                static (IntPtr block, out string value) =>
                {
                    value = string.Empty;

                    uint bufferSize;
                    int hr = Native.XGameUiShowTextEntryResultSize((XAsyncBlock*)block, &bufferSize);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    if (bufferSize == 0)
                    {
                        return HResult.SOk;
                    }

                    byte[] buffer = new byte[(int)bufferSize];
                    uint used;
                    fixed (byte* ptr = buffer)
                    {
                        hr = Native.XGameUiShowTextEntryResult(
                            (XAsyncBlock*)block, bufferSize, ptr, &used);
                    }

                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    // used includes the null terminator.
                    int charCount = (int)used > 0 ? (int)used - 1 : 0;
                    if (charCount > 0)
                    {
                        fixed (byte* ptr = buffer)
                        {
                            value = Utf8.ToString(ptr, charCount);
                        }
                    }

                    return HResult.SOk;
                },
                cancellationToken);
        }
        finally
        {
            Utf8.Free(titleUtf8);
            Utf8.Free(descUtf8);
            Utf8.Free(defaultUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // Text entry (non-modal)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Opens a non-modal IME text entry panel and returns a handle to it
    /// (<c>XGameUiTextEntryOpen</c>).
    /// </summary>
    /// <remarks>
    /// Poll <see cref="GameTextEntry.GetState"/> each frame to read input. Dispose the returned
    /// instance when done (<c>XGameUiTextEntryClose</c>).
    /// </remarks>
    /// <param name="options">Panel configuration.</param>
    /// <param name="maxLength">Maximum character count.</param>
    /// <param name="initialText">Pre-filled content, or <see langword="null"/> for empty.</param>
    /// <param name="cursorIndex">Initial cursor position within <paramref name="initialText"/>.</param>
    public GameTextEntry OpenTextEntry(
        TextEntryOptions options,
        uint maxLength,
        string? initialText = null,
        uint cursorIndex = 0)
    {
        ThrowIfDisposed();

        IntPtr initialTextUtf8 = Utf8.Allocate(initialText);
        try
        {
            var nativeOptions = new XGameUiTextEntryOptions
            {
                inputScope = (XGameUiTextEntryInputScope)options.InputScope,
                positionHint = (XGameUiTextEntryPositionHint)options.PositionHint,
                visibilityFlags = (XGameUiTextEntryVisibilityFlags)options.VisibilityFlags,
            };

            IntPtr handle;
            Hr.ThrowIfFailed(
                Native.XGameUiTextEntryOpen(
                    &nativeOptions,
                    maxLength,
                    (byte*)initialTextUtf8,
                    cursorIndex,
                    &handle));

            return new GameTextEntry(handle);
        }
        finally
        {
            Utf8.Free(initialTextUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // Game invite (requires user)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the system send-game-invite UI for a multiplayer session
    /// (<c>XGameUiShowSendGameInviteAsync</c> / <c>XGameUiShowSendGameInviteResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="sessionConfigurationId">Service configuration identifier.</param>
    /// <param name="sessionTemplateName">Multiplayer session template name.</param>
    /// <param name="sessionId">Multiplayer session identifier.</param>
    /// <param name="invitationText">Optional custom invitation message.</param>
    /// <param name="customActivationContext">Optional activation context passed to the invitee.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task ShowSendGameInviteAsync(
        User user,
        string sessionConfigurationId,
        string sessionTemplateName,
        string sessionId,
        string? invitationText = null,
        string? customActivationContext = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr userHandle = user.Handle;

        IntPtr sessionConfigUtf8 = Utf8.Allocate(sessionConfigurationId);
        IntPtr templateNameUtf8 = Utf8.Allocate(sessionTemplateName);
        IntPtr sessionIdUtf8 = Utf8.Allocate(sessionId);
        IntPtr invitationUtf8 = Utf8.Allocate(invitationText);
        IntPtr activationContextUtf8 = Utf8.Allocate(customActivationContext);
        try
        {
            return AsyncOperation<bool>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowSendGameInviteAsync(
                    (XAsyncBlock*)block,
                    userHandle,
                    (byte*)sessionConfigUtf8,
                    (byte*)templateNameUtf8,
                    (byte*)sessionIdUtf8,
                    (byte*)invitationUtf8,
                    (byte*)activationContextUtf8),
                static (IntPtr block, out bool value) =>
                {
                    value = true;
                    return Native.XGameUiShowSendGameInviteResult((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        finally
        {
            Utf8.Free(sessionConfigUtf8);
            Utf8.Free(templateNameUtf8);
            Utf8.Free(sessionIdUtf8);
            Utf8.Free(invitationUtf8);
            Utf8.Free(activationContextUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // Multiplayer activity game invite (requires user)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the Multiplayer Activity game invite UI for the requesting user
    /// (<c>XGameUiShowMultiplayerActivityGameInviteAsync</c> /
    /// <c>XGameUiShowMultiplayerActivityGameInviteResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task ShowMultiplayerActivityGameInviteAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr userHandle = user.Handle;

        return AsyncOperation<bool>.RunAsync(
            _queue.RawHandle(),
            block => Native.XGameUiShowMultiplayerActivityGameInviteAsync((XAsyncBlock*)block, userHandle),
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XGameUiShowMultiplayerActivityGameInviteResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    // -----------------------------------------------------------------------
    // Player profile card (requires user)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the system player profile card for <paramref name="targetPlayerId"/>
    /// (<c>XGameUiShowPlayerProfileCardAsync</c> / <c>XGameUiShowPlayerProfileCardResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="targetPlayerId">Xbox user id of the player whose profile card to display.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task ShowPlayerProfileCardAsync(
        User user,
        ulong targetPlayerId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr userHandle = user.Handle;

        return AsyncOperation<bool>.RunAsync(
            _queue.RawHandle(),
            block => Native.XGameUiShowPlayerProfileCardAsync((XAsyncBlock*)block, userHandle, targetPlayerId),
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XGameUiShowPlayerProfileCardResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    // -----------------------------------------------------------------------
    // Achievements (requires user)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the system achievements UI for the requesting user
    /// (<c>XGameUiShowAchievementsAsync</c> / <c>XGameUiShowAchievementsResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="titleId">Title id to display achievements for; use <c>0</c> for the current title.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task ShowAchievementsAsync(
        User user,
        uint titleId = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr userHandle = user.Handle;

        return AsyncOperation<bool>.RunAsync(
            _queue.RawHandle(),
            block => Native.XGameUiShowAchievementsAsync((XAsyncBlock*)block, userHandle, titleId),
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XGameUiShowAchievementsResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    // -----------------------------------------------------------------------
    // Player picker (requires user)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the system player-picker UI and returns the selected player ids
    /// (<c>XGameUiShowPlayerPickerAsync</c> / <c>XGameUiShowPlayerPickerResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="promptText">Instructional text shown above the player list.</param>
    /// <param name="candidates">The pool of players the user may select from.</param>
    /// <param name="preSelected">Players pre-selected when the dialog opens, or <see langword="null"/> for none.</param>
    /// <param name="minSelectionCount">Minimum number of players the user must select.</param>
    /// <param name="maxSelectionCount">Maximum number of players the user may select.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <returns>Array of selected Xbox user ids.</returns>
    public Task<ulong[]> ShowPlayerPickerAsync(
        User user,
        string promptText,
        ulong[] candidates,
        ulong[]? preSelected = null,
        uint minSelectionCount = 1,
        uint maxSelectionCount = 1,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        if (candidates == null)
        {
            throw new ArgumentNullException(nameof(candidates));
        }

        IntPtr userHandle = user.Handle;
        IntPtr promptUtf8 = Utf8.Allocate(promptText);

        // Pin the arrays for the synchronous native start call. The GDK copies them before
        // returning, so the pins only need to outlive the AsyncStarter lambda.
        GCHandle candidatesGch = GCHandle.Alloc(candidates, GCHandleType.Pinned);

        ulong[]? preSelArr = preSelected != null && preSelected.Length > 0 ? preSelected : null;
        GCHandle preSelGch = GCHandle.Alloc(
            (object?)preSelArr ?? Array.Empty<ulong>(), GCHandleType.Pinned);
        try
        {
            ulong* candidatesPtr = candidates.Length > 0
                ? (ulong*)candidatesGch.AddrOfPinnedObject()
                : null;
            ulong* preSelPtr = preSelArr != null
                ? (ulong*)preSelGch.AddrOfPinnedObject()
                : null;
            uint preSelCount = preSelArr != null ? (uint)preSelArr.Length : 0u;

            return AsyncOperation<ulong[]>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowPlayerPickerAsync(
                    (XAsyncBlock*)block,
                    userHandle,
                    (byte*)promptUtf8,
                    (uint)candidates.Length,
                    candidatesPtr,
                    preSelCount,
                    preSelPtr,
                    minSelectionCount,
                    maxSelectionCount),
                static (IntPtr block, out ulong[] value) =>
                {
                    value = Array.Empty<ulong>();

                    uint count;
                    int hr = Native.XGameUiShowPlayerPickerResultCount((XAsyncBlock*)block, &count);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    if (count == 0)
                    {
                        return HResult.SOk;
                    }

                    ulong[] result = new ulong[(int)count];
                    uint used;
                    fixed (ulong* resultPtr = result)
                    {
                        hr = Native.XGameUiShowPlayerPickerResult(
                            (XAsyncBlock*)block, count, resultPtr, &used);
                    }

                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    if ((int)used < result.Length)
                    {
                        ulong[] trimmed = new ulong[(int)used];
                        Array.Copy(result, trimmed, (int)used);
                        value = trimmed;
                    }
                    else
                    {
                        value = result;
                    }

                    return HResult.SOk;
                },
                cancellationToken);
        }
        finally
        {
            candidatesGch.Free();
            preSelGch.Free();
            Utf8.Free(promptUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // Web authentication (requires user)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the system web authentication browser
    /// (<c>XGameUiShowWebAuthenticationAsync</c> / <c>XGameUiShowWebAuthenticationResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="requestUri">Initial navigation URI.</param>
    /// <param name="completionUri">URI prefix that signals completion when the broker navigates to it.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<WebAuthenticationResult> ShowWebAuthenticationAsync(
        User user,
        string requestUri,
        string completionUri,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr userHandle = user.Handle;

        IntPtr requestUriUtf8 = Utf8.Allocate(requestUri);
        IntPtr completionUriUtf8 = Utf8.Allocate(completionUri);
        try
        {
            return AsyncOperation<WebAuthenticationResult>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowWebAuthenticationAsync(
                    (XAsyncBlock*)block,
                    userHandle,
                    (byte*)requestUriUtf8,
                    (byte*)completionUriUtf8),
                static (IntPtr block, out WebAuthenticationResult value) =>
                    ReadWebAuthResult(block, out value),
                cancellationToken);
        }
        finally
        {
            Utf8.Free(requestUriUtf8);
            Utf8.Free(completionUriUtf8);
        }
    }

    /// <summary>
    /// Shows the system web authentication browser with display options
    /// (<c>XGameUiShowWebAuthenticationWithOptionsAsync</c> / <c>XGameUiShowWebAuthenticationResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="requestUri">Initial navigation URI.</param>
    /// <param name="completionUri">URI prefix that signals completion when the broker navigates to it.</param>
    /// <param name="options">Display options, e.g. <see cref="WebAuthenticationOptions.PreferFullscreen"/>.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<WebAuthenticationResult> ShowWebAuthenticationWithOptionsAsync(
        User user,
        string requestUri,
        string completionUri,
        WebAuthenticationOptions options,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr userHandle = user.Handle;

        IntPtr requestUriUtf8 = Utf8.Allocate(requestUri);
        IntPtr completionUriUtf8 = Utf8.Allocate(completionUri);
        XGameUiWebAuthenticationOptions nativeOptions = (XGameUiWebAuthenticationOptions)options;
        try
        {
            return AsyncOperation<WebAuthenticationResult>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowWebAuthenticationWithOptionsAsync(
                    (XAsyncBlock*)block,
                    userHandle,
                    (byte*)requestUriUtf8,
                    (byte*)completionUriUtf8,
                    nativeOptions),
                static (IntPtr block, out WebAuthenticationResult value) =>
                    ReadWebAuthResult(block, out value),
                cancellationToken);
        }
        finally
        {
            Utf8.Free(requestUriUtf8);
            Utf8.Free(completionUriUtf8);
        }
    }

    // -----------------------------------------------------------------------
    // IDisposable
    // -----------------------------------------------------------------------

    /// <inheritdoc/>
    public void Dispose()
    {
        _disposed = true;
    }

    // -----------------------------------------------------------------------
    // Private helpers
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shared result reader for both web authentication overloads. Must be a static method so it
    /// can be referenced from a static lambda without capturing <c>this</c>.
    /// </summary>
    private static int ReadWebAuthResult(IntPtr block, out WebAuthenticationResult value)
    {
        value = null!;

        nuint bufferSize;
        int hr = Native.XGameUiShowWebAuthenticationResultSize((XAsyncBlock*)block, &bufferSize);
        if (HResult.Failed(hr))
        {
            return hr;
        }

        int resultStatus = 0;
        string? completionUri = null;

        if (bufferSize > 0)
        {
            byte[] buffer = new byte[(int)bufferSize];
            fixed (byte* bufferPtr = buffer)
            {
                XGameUiWebAuthenticationResultData* data = null;
                nuint used;
                hr = Native.XGameUiShowWebAuthenticationResult(
                    (XAsyncBlock*)block,
                    bufferSize,
                    bufferPtr,
                    &data,
                    &used);

                if (!HResult.Failed(hr) && data != null)
                {
                    resultStatus = data->responseStatus;
                    if (data->responseCompletionUri != null)
                    {
                        // responseCompletionUri points into buffer: copy before unpinning.
                        completionUri = Utf8.ToString(data->responseCompletionUri);
                    }
                }
            }
        }

        if (HResult.Failed(hr))
        {
            return hr;
        }

        value = new WebAuthenticationResult(resultStatus, completionUri);
        return HResult.SOk;
    }

    // -----------------------------------------------------------------------
    // State Share
    // -----------------------------------------------------------------------

    /// <summary>
    /// Shows the State Share UI so the player can share a deep link back into the current game
    /// state (<c>XGameUiShowStateShareAsync</c> / <c>XGameUiShowStateShareResult</c>).
    /// </summary>
    /// <param name="user">The requesting user.</param>
    /// <param name="linkToken">
    /// The title-defined token identifying the state to share. It is handed back to the title when
    /// the shared link is activated.
    /// </param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <exception cref="ArgumentNullException"><paramref name="user"/> or <paramref name="linkToken"/> is null.</exception>
    public Task ShowStateShareAsync(
        User user,
        string linkToken,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (linkToken is null)
        {
            throw new ArgumentNullException(nameof(linkToken));
        }

        IntPtr userHandle = user.Handle;
        IntPtr linkTokenUtf8 = Utf8.Allocate(linkToken);
        try
        {
            return AsyncOperation<bool>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameUiShowStateShareAsync(
                    (XAsyncBlock*)block, userHandle, (byte*)linkTokenUtf8),
                static (IntPtr block, out bool value) =>
                {
                    value = true;
                    return Native.XGameUiShowStateShareResult((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        finally
        {
            Utf8.Free(linkTokenUtf8);
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameUiManager));
        }
    }
}
