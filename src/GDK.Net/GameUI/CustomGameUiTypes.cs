// Public types for the title-implemented UI path (XGameUiSetUiCallbacks and the eight
// XGameUiSet*UiResponse completions).
//
// SHAPE OF THE NATIVE CONTRACT
// ----------------------------
// When a title registers callbacks, the Gaming Runtime stops drawing the corresponding system UI
// and instead invokes the title's callback with an XGameUiCallbackHandle. The title renders
// whatever it likes and later calls the matching XGameUiSet*UiResponse with that handle to unblock
// the original XGameUiShow*Async caller. The handle is the correlation token: it is opaque, and
// exactly one response must be posted for each request.
//
// That maps to one request class per callback. Each carries the payload the runtime supplied and a
// Respond method that posts the matching completion, so it is not possible to answer a text-entry
// request with a message-dialog response.
//
// WHY EVERY PAYLOAD IS COPIED
// ---------------------------
// The runtime's callback delivers pointers into memory it owns, valid only until the callback
// returns. CustomGameUi copies all of it -- strings and player arrays alike -- before the request
// is handed to the title. That is what makes a request a self-contained value the title can hold
// for as long as it likes, and it is also what lets the handler run off the runtime's callback
// thread at all. See the threading notes on CustomGameUi.

using System;
using System.Collections.Generic;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.GameUI;

/// <summary>
/// A pending request from the Gaming Runtime for the title to display a piece of UI that the system
/// would otherwise have drawn itself.
/// </summary>
/// <remarks>
/// <para>
/// Instances are handed to the handlers registered with <see cref="CustomGameUi.SetHandlers"/>.
/// Exactly one <c>Respond</c> call must be made for each request, and until it is made the
/// <c>XGameUiShow*Async</c> operation that triggered the request stays pending. Responding is not
/// required to happen inside the handler: a title will normally capture the request, render its UI
/// over several frames, and respond once the player has chosen. Any thread may respond.
/// </para>
/// <para>
/// Every payload string and array has already been copied out of runtime-owned memory, so a request
/// stays valid indefinitely.
/// </para>
/// </remarks>
public abstract class GameUiRequest
{
    private int _responded;

    private protected GameUiRequest(IntPtr callbackHandle) => CallbackHandle = callbackHandle;

    /// <summary>
    /// The opaque <c>XGameUiCallbackHandle</c> correlating this request with the pending
    /// <c>XGameUiShow*Async</c> operation.
    /// </summary>
    internal IntPtr CallbackHandle { get; }

    /// <summary><see langword="true"/> once a response has been posted for this request.</summary>
    public bool HasResponded => Volatile.Read(ref _responded) != 0;

    /// <summary>
    /// Posts the least-surprising response for this request kind: the answer that means "the player
    /// did not choose anything". Used when no handler is registered for a UI the runtime asked for,
    /// and when a handler throws without responding -- either way the caller's async operation is
    /// completed rather than left pending forever.
    /// </summary>
    internal abstract void RespondNeutral();

    /// <summary>
    /// Marks this request as answered, throwing if it already was. Returns without throwing only
    /// for the first caller, so a double response cannot reach the native API.
    /// </summary>
    private protected void MarkResponded()
    {
        if (Interlocked.Exchange(ref _responded, 1) != 0)
        {
            throw new InvalidOperationException(
                $"This {GetType().Name} has already been answered. The Gaming Runtime expects " +
                "exactly one response per request; posting a second would target a callback " +
                "handle the runtime has already retired.");
        }
    }
}

/// <summary>
/// A request to show the profile card for <see cref="TargetPlayerId"/>
/// (<c>XGameUiShowPlayerProfileCardUiCallback</c>).
/// </summary>
public sealed class PlayerProfileCardUiRequest : GameUiRequest
{
    internal PlayerProfileCardUiRequest(
        IntPtr callbackHandle,
        IntPtr requestingUser,
        ulong targetPlayerId)
        : base(callbackHandle)
    {
        RequestingUserHandle = requestingUser;
        TargetPlayerId = targetPlayerId;
    }

    /// <summary>
    /// The <c>XUserHandle</c> of the user who asked for the card, as supplied by the runtime.
    /// Duplicate it with <see cref="CustomGameUi.DuplicateUser"/> before using it beyond the
    /// lifetime of the originating operation.
    /// </summary>
    public IntPtr RequestingUserHandle { get; }

    /// <summary>The Xbox user id whose profile card was requested.</summary>
    public ulong TargetPlayerId { get; }

    /// <summary>
    /// Reports that the title has finished showing the profile card
    /// (<c>XGameUiSetPlayerProfileCardUiResponse</c>).
    /// </summary>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public void Respond()
    {
        MarkResponded();
        Hr.ThrowIfFailed(Native.XGameUiSetPlayerProfileCardUiResponse(CallbackHandle));
    }

    internal override void RespondNeutral() => Respond();
}

/// <summary>
/// A request to let the player choose from a list of players
/// (<c>XGameUiShowPlayerPickerUiCallback</c>).
/// </summary>
public sealed class PlayerPickerUiRequest : GameUiRequest
{
    internal PlayerPickerUiRequest(
        IntPtr callbackHandle,
        IntPtr requestingUser,
        string? promptText,
        ulong[] selectFromPlayers,
        ulong[] preSelectedPlayers,
        uint minSelectionCount,
        uint maxSelectionCount)
        : base(callbackHandle)
    {
        RequestingUserHandle = requestingUser;
        PromptText = promptText;
        SelectFromPlayers = selectFromPlayers;
        PreSelectedPlayers = preSelectedPlayers;
        MinSelectionCount = minSelectionCount;
        MaxSelectionCount = maxSelectionCount;
    }

    /// <summary>The <c>XUserHandle</c> of the requesting user, as supplied by the runtime.</summary>
    public IntPtr RequestingUserHandle { get; }

    /// <summary>The prompt to display above the list.</summary>
    public string? PromptText { get; }

    /// <summary>The Xbox user ids the player may choose from.</summary>
    public IReadOnlyList<ulong> SelectFromPlayers { get; }

    /// <summary>The Xbox user ids that should start out selected.</summary>
    public IReadOnlyList<ulong> PreSelectedPlayers { get; }

    /// <summary>The fewest players the title should let the player select.</summary>
    public uint MinSelectionCount { get; }

    /// <summary>The most players the title should let the player select.</summary>
    public uint MaxSelectionCount { get; }

    /// <summary>
    /// Reports the players the user selected (<c>XGameUiSetPlayerPickerUiResponse</c>). Pass an
    /// empty list when the picker was dismissed without a selection.
    /// </summary>
    /// <param name="selectedPlayers">The chosen Xbox user ids.</param>
    /// <exception cref="ArgumentNullException"><paramref name="selectedPlayers"/> is null.</exception>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public unsafe void Respond(IReadOnlyList<ulong> selectedPlayers)
    {
        if (selectedPlayers is null)
        {
            throw new ArgumentNullException(nameof(selectedPlayers));
        }

        MarkResponded();

        int count = selectedPlayers.Count;
        if (count == 0)
        {
            Hr.ThrowIfFailed(Native.XGameUiSetPlayerPickerUiResponse(CallbackHandle, 0, null));
            return;
        }

        ulong[] players = new ulong[count];
        for (int i = 0; i < count; i++)
        {
            players[i] = selectedPlayers[i];
        }

        fixed (ulong* pPlayers = players)
        {
            Hr.ThrowIfFailed(
                Native.XGameUiSetPlayerPickerUiResponse(CallbackHandle, (uint)count, pPlayers));
        }
    }

    internal override void RespondNeutral() => Respond(Array.Empty<ulong>());
}

/// <summary>
/// A request to show the "send game invite" UI (<c>XGameUiShowSendGameInviteUiCallback</c>).
/// </summary>
public sealed class SendGameInviteUiRequest : GameUiRequest
{
    internal SendGameInviteUiRequest(
        IntPtr callbackHandle,
        IntPtr requestingUser,
        string? sessionConfigurationId,
        string? sessionTemplateName,
        string? sessionId,
        string? invitationText,
        string? customActivationContext)
        : base(callbackHandle)
    {
        RequestingUserHandle = requestingUser;
        SessionConfigurationId = sessionConfigurationId;
        SessionTemplateName = sessionTemplateName;
        SessionId = sessionId;
        InvitationText = invitationText;
        CustomActivationContext = customActivationContext;
    }

    /// <summary>The <c>XUserHandle</c> of the inviting user, as supplied by the runtime.</summary>
    public IntPtr RequestingUserHandle { get; }

    /// <summary>The multiplayer session's service configuration id.</summary>
    public string? SessionConfigurationId { get; }

    /// <summary>The multiplayer session template name.</summary>
    public string? SessionTemplateName { get; }

    /// <summary>The multiplayer session id.</summary>
    public string? SessionId { get; }

    /// <summary>Optional invitation text supplied by the title.</summary>
    public string? InvitationText { get; }

    /// <summary>Optional custom activation context carried with the invite.</summary>
    public string? CustomActivationContext { get; }

    /// <summary>
    /// Reports that the title has finished handling the invite UI
    /// (<c>XGameUiSetSendGameInviteUiResponse</c>).
    /// </summary>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public void Respond()
    {
        MarkResponded();
        Hr.ThrowIfFailed(Native.XGameUiSetSendGameInviteUiResponse(CallbackHandle));
    }

    internal override void RespondNeutral() => Respond();
}

/// <summary>
/// A request to show the achievements UI (<c>XGameUiShowAchievementsUiCallback</c>).
/// </summary>
public sealed class AchievementsUiRequest : GameUiRequest
{
    internal AchievementsUiRequest(
        IntPtr callbackHandle,
        IntPtr requestingUser,
        uint titleId)
        : base(callbackHandle)
    {
        RequestingUserHandle = requestingUser;
        TitleId = titleId;
    }

    /// <summary>
    /// The <c>XUserHandle</c> whose achievements were requested, as supplied by the runtime.
    /// </summary>
    public IntPtr RequestingUserHandle { get; }

    /// <summary>The title id whose achievements should be shown.</summary>
    public uint TitleId { get; }

    /// <summary>
    /// Reports that the title has finished showing achievements
    /// (<c>XGameUiSetAchievementsUiResponse</c>).
    /// </summary>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public void Respond()
    {
        MarkResponded();
        Hr.ThrowIfFailed(Native.XGameUiSetAchievementsUiResponse(CallbackHandle));
    }

    internal override void RespondNeutral() => Respond();
}

/// <summary>
/// A request to show the Multiplayer Activity invite UI
/// (<c>XGameUiShowMultiplayerActivityGameInviteUiCallback</c>).
/// </summary>
public sealed class MultiplayerActivityGameInviteUiRequest : GameUiRequest
{
    internal MultiplayerActivityGameInviteUiRequest(
        IntPtr callbackHandle,
        IntPtr requestingUser)
        : base(callbackHandle)
    {
        RequestingUserHandle = requestingUser;
    }

    /// <summary>The <c>XUserHandle</c> of the inviting user, as supplied by the runtime.</summary>
    public IntPtr RequestingUserHandle { get; }

    /// <summary>
    /// Reports that the title has finished handling the invite UI
    /// (<c>XGameUiSetMultiplayerActivityGameInviteUiResponse</c>).
    /// </summary>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public void Respond()
    {
        MarkResponded();
        Hr.ThrowIfFailed(
            Native.XGameUiSetMultiplayerActivityGameInviteUiResponse(CallbackHandle));
    }

    internal override void RespondNeutral() => Respond();
}

/// <summary>
/// A request to show a message dialog (<c>XGameUiShowMessageDialogUiCallback</c>).
/// </summary>
public sealed class MessageDialogUiRequest : GameUiRequest
{
    internal MessageDialogUiRequest(
        IntPtr callbackHandle,
        string? titleText,
        string? contentText,
        string? firstButtonText,
        string? secondButtonText,
        string? thirdButtonText,
        MessageDialogButton defaultButton,
        MessageDialogButton cancelButton)
        : base(callbackHandle)
    {
        TitleText = titleText;
        ContentText = contentText;
        FirstButtonText = firstButtonText;
        SecondButtonText = secondButtonText;
        ThirdButtonText = thirdButtonText;
        DefaultButton = defaultButton;
        CancelButton = cancelButton;
    }

    /// <summary>The dialog title.</summary>
    public string? TitleText { get; }

    /// <summary>The dialog body text.</summary>
    public string? ContentText { get; }

    /// <summary>The first button's label. Always present.</summary>
    public string? FirstButtonText { get; }

    /// <summary>The second button's label, or <see langword="null"/> when there is no second button.</summary>
    public string? SecondButtonText { get; }

    /// <summary>The third button's label, or <see langword="null"/> when there is no third button.</summary>
    public string? ThirdButtonText { get; }

    /// <summary>The button that should be focused initially.</summary>
    public MessageDialogButton DefaultButton { get; }

    /// <summary>The button that a cancel gesture (B button, Escape) should select.</summary>
    public MessageDialogButton CancelButton { get; }

    /// <summary>
    /// Reports which button the player chose (<c>XGameUiSetMessageDialogUiResponse</c>).
    /// </summary>
    /// <param name="response">The chosen button.</param>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public void Respond(MessageDialogButton response)
    {
        MarkResponded();
        Hr.ThrowIfFailed(
            Native.XGameUiSetMessageDialogUiResponse(
                CallbackHandle,
                (XGameUiMessageDialogButton)response));
    }

    internal override void RespondNeutral() => Respond(CancelButton);
}

/// <summary>
/// A request to show an error dialog (<c>XGameUiShowErrorDialogUiCallback</c>).
/// </summary>
public sealed class ErrorDialogUiRequest : GameUiRequest
{
    internal ErrorDialogUiRequest(
        IntPtr callbackHandle,
        int errorCode,
        string? serviceContext)
        : base(callbackHandle)
    {
        ErrorCode = errorCode;
        ServiceContext = serviceContext;
    }

    /// <summary>The HRESULT the runtime wants reported to the player.</summary>
    public int ErrorCode { get; }

    /// <summary>Optional service context string describing where the error came from.</summary>
    public string? ServiceContext { get; }

    /// <summary>
    /// Reports that the title has finished showing the error
    /// (<c>XGameUiSetErrorDialogUiResponse</c>).
    /// </summary>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public void Respond()
    {
        MarkResponded();
        Hr.ThrowIfFailed(Native.XGameUiSetErrorDialogUiResponse(CallbackHandle));
    }

    internal override void RespondNeutral() => Respond();
}

/// <summary>
/// A request to collect a line of text from the player (<c>XGameUiShowTextEntryUiCallback</c>).
/// </summary>
public sealed class TextEntryUiRequest : GameUiRequest
{
    internal TextEntryUiRequest(
        IntPtr callbackHandle,
        string? titleText,
        string? descriptionText,
        string? defaultText,
        TextEntryInputScope inputScope,
        uint maxTextLength)
        : base(callbackHandle)
    {
        TitleText = titleText;
        DescriptionText = descriptionText;
        DefaultText = defaultText;
        InputScope = inputScope;
        MaxTextLength = maxTextLength;
    }

    /// <summary>The title to show above the text field.</summary>
    public string? TitleText { get; }

    /// <summary>Explanatory text to show with the field.</summary>
    public string? DescriptionText { get; }

    /// <summary>The text the field should start out containing.</summary>
    public string? DefaultText { get; }

    /// <summary>The kind of text expected, which selects the on-screen keyboard layout.</summary>
    public TextEntryInputScope InputScope { get; }

    /// <summary>The maximum number of characters the title should accept.</summary>
    public uint MaxTextLength { get; }

    /// <summary>
    /// Reports the text the player entered (<c>XGameUiSetTextEntryUiResponse</c>).
    /// </summary>
    /// <param name="response">
    /// The entered text. Pass <see cref="string.Empty"/> when the player cancelled; the native API
    /// requires a non-null string.
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="response"/> is null.</exception>
    /// <exception cref="InvalidOperationException">A response was already posted.</exception>
    public unsafe void Respond(string response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        MarkResponded();

        IntPtr utf8 = Utf8.Allocate(response);
        try
        {
            Hr.ThrowIfFailed(Native.XGameUiSetTextEntryUiResponse(CallbackHandle, (byte*)utf8));
        }
        finally
        {
            Utf8.Free(utf8);
        }
    }

    internal override void RespondNeutral() => Respond(string.Empty);
}

/// <summary>
/// The set of UI requests a title is willing to render itself. Passed to
/// <see cref="CustomGameUi.SetHandlers"/>.
/// </summary>
/// <remarks>
/// Every handler is optional. A handler left <see langword="null"/> is reported to the Gaming
/// Runtime as a null function pointer, which leaves that particular UI with the system, so a title
/// can take over just the dialogs it cares about.
/// </remarks>
public sealed class CustomGameUiHandlers
{
    /// <summary>Invoked to show a player's profile card.</summary>
    public Action<PlayerProfileCardUiRequest>? PlayerProfileCard { get; set; }

    /// <summary>Invoked to let the player pick from a list of players.</summary>
    public Action<PlayerPickerUiRequest>? PlayerPicker { get; set; }

    /// <summary>Invoked to show the send-game-invite UI.</summary>
    public Action<SendGameInviteUiRequest>? SendGameInvite { get; set; }

    /// <summary>Invoked to show the achievements UI.</summary>
    public Action<AchievementsUiRequest>? Achievements { get; set; }

    /// <summary>Invoked to show the Multiplayer Activity invite UI.</summary>
    public Action<MultiplayerActivityGameInviteUiRequest>? MultiplayerActivityGameInvite { get; set; }

    /// <summary>Invoked to show a message dialog.</summary>
    public Action<MessageDialogUiRequest>? MessageDialog { get; set; }

    /// <summary>Invoked to show an error dialog.</summary>
    public Action<ErrorDialogUiRequest>? ErrorDialog { get; set; }

    /// <summary>Invoked to collect a line of text from the player.</summary>
    public Action<TextEntryUiRequest>? TextEntry { get; set; }
}
