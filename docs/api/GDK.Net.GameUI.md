# <a id="GDK_Net_GameUI"></a> Namespace GDK.Net.GameUI

### Classes

 [AchievementsUiRequest](GDK.Net.GameUI.AchievementsUiRequest.md)

A request to show the achievements UI (<code>XGameUiShowAchievementsUiCallback</code>).

 [CustomGameUi](GDK.Net.GameUI.CustomGameUi.md)

Lets a title render the Gaming Runtime's UI itself instead of letting the system draw it
(<code>XGameUiSetUiCallbacks</code> and the eight <code>XGameUiSet*UiResponse</code> completions).

 [CustomGameUiHandlers](GDK.Net.GameUI.CustomGameUiHandlers.md)

The set of UI requests a title is willing to render itself. Passed to
<xref href="GDK.Net.GameUI.CustomGameUi.SetHandlers(GDK.Net.GameUI.CustomGameUiHandlers%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref>.

 [ErrorDialogUiRequest](GDK.Net.GameUI.ErrorDialogUiRequest.md)

A request to show an error dialog (<code>XGameUiShowErrorDialogUiCallback</code>).

 [GameTextEntry](GDK.Net.GameUI.GameTextEntry.md)

An open non-modal text-entry session. Wraps <code>XGameUiTextEntryHandle</code>.

 [GameUiManager](GDK.Net.GameUI.GameUiManager.md)

System UI overlays for GDK titles: dialogs, player pickers, achievements, web authentication
and non-modal text entry. Reached through <xref href="GDK.Net.GameRuntime.GameUi" data-throw-if-not-resolved="false"></xref>.

 [GameUiRequest](GDK.Net.GameUI.GameUiRequest.md)

A pending request from the Gaming Runtime for the title to display a piece of UI that the system
would otherwise have drawn itself.

 [MessageDialogUiRequest](GDK.Net.GameUI.MessageDialogUiRequest.md)

A request to show a message dialog (<code>XGameUiShowMessageDialogUiCallback</code>).

 [MultiplayerActivityGameInviteUiRequest](GDK.Net.GameUI.MultiplayerActivityGameInviteUiRequest.md)

A request to show the Multiplayer Activity invite UI
(<code>XGameUiShowMultiplayerActivityGameInviteUiCallback</code>).

 [PlayerPickerUiRequest](GDK.Net.GameUI.PlayerPickerUiRequest.md)

A request to let the player choose from a list of players
(<code>XGameUiShowPlayerPickerUiCallback</code>).

 [PlayerProfileCardUiRequest](GDK.Net.GameUI.PlayerProfileCardUiRequest.md)

A request to show the profile card for <xref href="GDK.Net.GameUI.PlayerProfileCardUiRequest.TargetPlayerId" data-throw-if-not-resolved="false"></xref>
(<code>XGameUiShowPlayerProfileCardUiCallback</code>).

 [SendGameInviteUiRequest](GDK.Net.GameUI.SendGameInviteUiRequest.md)

A request to show the "send game invite" UI (<code>XGameUiShowSendGameInviteUiCallback</code>).

 [TextEntryState](GDK.Net.GameUI.TextEntryState.md)

Current state of a non-modal text entry session
(<code>XGameUiTextEntryGetState</code>).

 [TextEntryUiRequest](GDK.Net.GameUI.TextEntryUiRequest.md)

A request to collect a line of text from the player (<code>XGameUiShowTextEntryUiCallback</code>).

 [WebAuthenticationResult](GDK.Net.GameUI.WebAuthenticationResult.md)

Result of a <xref href="GDK.Net.GameUI.GameUiManager.ShowWebAuthenticationAsync(GDK.Net.Users.User%2cSystem.String%2cSystem.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.GameUI.GameUiManager.ShowWebAuthenticationWithOptionsAsync(GDK.Net.Users.User%2cSystem.String%2cSystem.String%2cGDK.Net.GameUI.WebAuthenticationOptions%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> call
(<code>XGameUiShowWebAuthenticationResult</code>).

### Structs

 [TextEntryExtents](GDK.Net.GameUI.TextEntryExtents.md)

Screen-space extents of the non-modal text entry panel
(<code>XGameUiTextEntryGetExtents</code>).

 [TextEntryOptions](GDK.Net.GameUI.TextEntryOptions.md)

Opening configuration for a non-modal text entry session
(<code>XGameUiTextEntryOpen</code>).

### Enums

 [MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)

Which button was activated in a message dialog. Mirrors <code>XGameUiMessageDialogButton</code>.

 [NotificationPositionHint](GDK.Net.GameUI.NotificationPositionHint.md)

Screen corner hint for toast notifications. Mirrors <code>XGameUiNotificationPositionHint</code>.

 [TextEntryChangeTypeFlags](GDK.Net.GameUI.TextEntryChangeTypeFlags.md)

Flags describing what changed since the last <code>XGameUiTextEntryGetState</code> poll.
Mirrors <code>XGameUiTextEntryChangeTypeFlags</code>.

 [TextEntryInputScope](GDK.Net.GameUI.TextEntryInputScope.md)

Keyboard input mode for text-entry UI. Mirrors <code>XGameUiTextEntryInputScope</code>.

 [TextEntryPositionHint](GDK.Net.GameUI.TextEntryPositionHint.md)

Preferred screen edge for the non-modal text entry panel.
Mirrors <code>XGameUiTextEntryPositionHint</code>.

 [TextEntryVisibilityFlags](GDK.Net.GameUI.TextEntryVisibilityFlags.md)

Controls the IME candidate window visibility. Mirrors <code>XGameUiTextEntryVisibilityFlags</code>.

 [WebAuthenticationOptions](GDK.Net.GameUI.WebAuthenticationOptions.md)

Display options for the web authentication browser.
Mirrors <code>XGameUiWebAuthenticationOptions</code>.

