using System;

namespace GDK.Net.GameUI;

/// <summary>
/// Which button was activated in a message dialog. Mirrors <c>XGameUiMessageDialogButton</c>.
/// </summary>
public enum MessageDialogButton : uint
{
    /// <summary>The first button.</summary>
    First = 0,

    /// <summary>The second button.</summary>
    Second = 1,

    /// <summary>The third button.</summary>
    Third = 2,
}

/// <summary>
/// Screen corner hint for toast notifications. Mirrors <c>XGameUiNotificationPositionHint</c>.
/// </summary>
public enum NotificationPositionHint : uint
{
    /// <summary>Prefer the bottom-centre of the screen.</summary>
    BottomCenter = 0,

    /// <summary>Prefer the bottom-left corner of the screen.</summary>
    BottomLeft = 1,

    /// <summary>Prefer the bottom-right corner of the screen.</summary>
    BottomRight = 2,

    /// <summary>Prefer the top-centre of the screen.</summary>
    TopCenter = 3,

    /// <summary>Prefer the top-left corner of the screen.</summary>
    TopLeft = 4,

    /// <summary>Prefer the top-right corner of the screen.</summary>
    TopRight = 5,
}

/// <summary>
/// Keyboard input mode for text-entry UI. Mirrors <c>XGameUiTextEntryInputScope</c>.
/// </summary>
public enum TextEntryInputScope : uint
{
    /// <summary>Use the default text input scope.</summary>
    Default = 0,

    /// <summary>Optimise input for URLs.</summary>
    Url = 1,

    /// <summary>Optimise input for SMTP email addresses.</summary>
    EmailSmtpAddress = 5,

    /// <summary>Optimise input for numbers.</summary>
    Number = 29,

    /// <summary>Optimise input for passwords.</summary>
    Password = 31,

    /// <summary>Optimise input for telephone numbers.</summary>
    TelephoneNumber = 32,

    /// <summary>Optimise input for alphanumeric text.</summary>
    Alphanumeric = 40,

    /// <summary>Optimise input for search text.</summary>
    Search = 50,

    /// <summary>Optimise input for chat text without emoji.</summary>
    ChatWithoutEmoji = 68,
}

/// <summary>
/// Flags describing what changed since the last <c>XGameUiTextEntryGetState</c> poll.
/// Mirrors <c>XGameUiTextEntryChangeTypeFlags</c>.
/// </summary>
[Flags]
public enum TextEntryChangeTypeFlags : uint
{
    /// <summary>No changes were reported.</summary>
    None = 0x0,

    /// <summary>The text content changed.</summary>
    TextChanged = 0x1,

    /// <summary>The text-entry UI was dismissed.</summary>
    Dismissed = 0x2,
}

/// <summary>
/// Controls the IME candidate window visibility. Mirrors <c>XGameUiTextEntryVisibilityFlags</c>.
/// </summary>
[Flags]
public enum TextEntryVisibilityFlags : uint
{
    /// <summary>Use the default text-entry visibility behaviour.</summary>
    Default = 0x0,

    /// <summary>Only show IME candidates.</summary>
    OnlyShowCandidates = 0x1,
}

/// <summary>
/// Preferred screen edge for the non-modal text entry panel.
/// Mirrors <c>XGameUiTextEntryPositionHint</c>.
/// </summary>
public enum TextEntryPositionHint : uint
{
    /// <summary>Prefer the bottom edge of the screen.</summary>
    Bottom = 0,

    /// <summary>Prefer the top edge of the screen.</summary>
    Top = 1,
}

/// <summary>
/// Display options for the web authentication browser.
/// Mirrors <c>XGameUiWebAuthenticationOptions</c>.
/// </summary>
[Flags]
public enum WebAuthenticationOptions : uint
{
    /// <summary>No special web-authentication options.</summary>
    None = 0x00,

    /// <summary>Prefer a full-screen web-authentication UI.</summary>
    PreferFullscreen = 0x01,
}

/// <summary>
/// Opening configuration for a non-modal text entry session
/// (<c>XGameUiTextEntryOpen</c>).
/// </summary>
public readonly struct TextEntryOptions
{
    /// <summary>Keyboard input scope.</summary>
    public TextEntryInputScope InputScope { get; }

    /// <summary>Preferred screen edge for the panel.</summary>
    public TextEntryPositionHint PositionHint { get; }

    /// <summary>IME candidate window visibility.</summary>
    public TextEntryVisibilityFlags VisibilityFlags { get; }

    /// <param name="inputScope">Keyboard input scope.</param>
    /// <param name="positionHint">Preferred screen edge for the panel.</param>
    /// <param name="visibilityFlags">IME candidate window visibility.</param>
    public TextEntryOptions(
        TextEntryInputScope inputScope,
        TextEntryPositionHint positionHint = TextEntryPositionHint.Bottom,
        TextEntryVisibilityFlags visibilityFlags = TextEntryVisibilityFlags.Default)
    {
        InputScope = inputScope;
        PositionHint = positionHint;
        VisibilityFlags = visibilityFlags;
    }
}

/// <summary>
/// Screen-space extents of the non-modal text entry panel
/// (<c>XGameUiTextEntryGetExtents</c>).
/// </summary>
public readonly struct TextEntryExtents
{
    /// <summary>Left edge in normalized screen coordinates.</summary>
    public float Left { get; }

    /// <summary>Top edge in normalized screen coordinates.</summary>
    public float Top { get; }

    /// <summary>Right edge in normalized screen coordinates.</summary>
    public float Right { get; }

    /// <summary>Bottom edge in normalized screen coordinates.</summary>
    public float Bottom { get; }

    internal TextEntryExtents(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}

/// <summary>
/// Result of a <see cref="GameUiManager.ShowWebAuthenticationAsync"/> or
/// <see cref="GameUiManager.ShowWebAuthenticationWithOptionsAsync"/> call
/// (<c>XGameUiShowWebAuthenticationResult</c>).
/// </summary>
public sealed class WebAuthenticationResult
{
    internal WebAuthenticationResult(int responseStatus, string? completionUri)
    {
        ResponseStatus = responseStatus;
        CompletionUri = completionUri;
    }

    /// <summary>
    /// The HRESULT status returned by the web authentication broker.
    /// A negative value indicates failure (see <see cref="HResult.Failed"/>).
    /// </summary>
    public int ResponseStatus { get; }

    /// <summary><see langword="true"/> when <see cref="ResponseStatus"/> is a success code.</summary>
    public bool Succeeded => HResult.Succeeded(ResponseStatus);

    /// <summary>
    /// The URI to which the broker navigated at completion, or <see langword="null"/> when the
    /// authentication did not complete successfully.
    /// </summary>
    public string? CompletionUri { get; }
}

/// <summary>
/// Current state of a non-modal text entry session
/// (<c>XGameUiTextEntryGetState</c>).
/// </summary>
public sealed class TextEntryState
{
    internal TextEntryState(
        TextEntryChangeTypeFlags changeType,
        uint cursorIndex,
        uint imeClauseStartIndex,
        uint imeClauseEndIndex,
        string text)
    {
        ChangeType = changeType;
        CursorIndex = cursorIndex;
        ImeClauseStartIndex = imeClauseStartIndex;
        ImeClauseEndIndex = imeClauseEndIndex;
        Text = text;
    }

    /// <summary>What changed since the last poll.</summary>
    public TextEntryChangeTypeFlags ChangeType { get; }

    /// <summary>Current insertion-point position in <see cref="Text"/>.</summary>
    public uint CursorIndex { get; }

    /// <summary>Start of the active IME composition clause in <see cref="Text"/>.</summary>
    public uint ImeClauseStartIndex { get; }

    /// <summary>Exclusive end of the active IME composition clause in <see cref="Text"/>.</summary>
    public uint ImeClauseEndIndex { get; }

    /// <summary>Current text content of the entry field.</summary>
    public string Text { get; }
}
