# <a id="GDK_Net_GameUI_GameUiManager"></a> Class GameUiManager

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

System UI overlays for GDK titles: dialogs, player pickers, achievements, web authentication
and non-modal text entry. Reached through <xref href="GDK.Net.GameRuntime.GameUi" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class GameUiManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiManager](GDK.Net.GameUI.GameUiManager.md)

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

All async methods use <code>XAsyncBlock</code> under the hood and are driven by the
<xref href="GDK.Net.GameRuntime" data-throw-if-not-resolved="false"></xref>'s task queue. Methods that accept a <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> extract the
native <code>XUserHandle</code> at call time; the user must remain alive until the operation
completes.

## Methods

### <a id="GDK_Net_GameUI_GameUiManager_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_GameUI_GameUiManager_OpenTextEntry_GDK_Net_GameUI_TextEntryOptions_System_UInt32_System_String_System_UInt32_"></a> OpenTextEntry\(TextEntryOptions, uint, string?, uint\)

Opens a non-modal IME text entry panel and returns a handle to it
(<code>XGameUiTextEntryOpen</code>).

```csharp
public GameTextEntry OpenTextEntry(TextEntryOptions options, uint maxLength, string? initialText = null, uint cursorIndex = 0)
```

#### Parameters

`options` [TextEntryOptions](GDK.Net.GameUI.TextEntryOptions.md)

Panel configuration.

`maxLength` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum character count.

`initialText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Pre-filled content, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for empty.

`cursorIndex` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Initial cursor position within <code class="paramref">initialText</code>.

#### Returns

 [GameTextEntry](GDK.Net.GameUI.GameTextEntry.md)

#### Remarks

Poll <xref href="GDK.Net.GameUI.GameTextEntry.GetState" data-throw-if-not-resolved="false"></xref> each frame to read input. Dispose the returned
instance when done (<code>XGameUiTextEntryClose</code>).

### <a id="GDK_Net_GameUI_GameUiManager_SetNotificationPositionHint_GDK_Net_GameUI_NotificationPositionHint_"></a> SetNotificationPositionHint\(NotificationPositionHint\)

Moves the system notification overlay to the specified screen region
(<code>XGameUiSetNotificationPositionHint</code>).

```csharp
public void SetNotificationPositionHint(NotificationPositionHint position)
```

#### Parameters

`position` [NotificationPositionHint](GDK.Net.GameUI.NotificationPositionHint.md)

Desired screen region.

### <a id="GDK_Net_GameUI_GameUiManager_ShowAchievementsAsync_GDK_Net_Users_User_System_UInt32_System_Threading_CancellationToken_"></a> ShowAchievementsAsync\(User, uint, CancellationToken\)

Shows the system achievements UI for the requesting user
(<code>XGameUiShowAchievementsAsync</code> / <code>XGameUiShowAchievementsResult</code>).

```csharp
public Task ShowAchievementsAsync(User user, uint titleId = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`titleId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Title id to display achievements for; use <code>0</code> for the current title.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_GameUI_GameUiManager_ShowErrorDialogAsync_System_Int32_System_String_System_Threading_CancellationToken_"></a> ShowErrorDialogAsync\(int, string?, CancellationToken\)

Shows a system error dialog for the supplied HRESULT
(<code>XGameUiShowErrorDialogAsync</code> / <code>XGameUiShowErrorDialogResult</code>).

```csharp
public Task ShowErrorDialogAsync(int errorCode, string? context = null, CancellationToken cancellationToken = default)
```

#### Parameters

`errorCode` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT to display.

`context` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional context string shown alongside the error.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_GameUI_GameUiManager_ShowMessageDialogAsync_System_String_System_String_System_String_System_String_System_String_GDK_Net_GameUI_MessageDialogButton_GDK_Net_GameUI_MessageDialogButton_System_Threading_CancellationToken_"></a> ShowMessageDialogAsync\(string, string, string?, string?, string?, MessageDialogButton, MessageDialogButton, CancellationToken\)

Shows a modal message dialog with up to three buttons and returns the button the user
activated (<code>XGameUiShowMessageDialogAsync</code> / <code>XGameUiShowMessageDialogResult</code>).

```csharp
public Task<MessageDialogButton> ShowMessageDialogAsync(string titleText, string contentText, string? firstButtonText = null, string? secondButtonText = null, string? thirdButtonText = null, MessageDialogButton defaultButton = MessageDialogButton.First, MessageDialogButton cancelButton = MessageDialogButton.First, CancellationToken cancellationToken = default)
```

#### Parameters

`titleText` [string](https://learn.microsoft.com/dotnet/api/system.string)

Dialog title (required).

`contentText` [string](https://learn.microsoft.com/dotnet/api/system.string)

Dialog body text (required).

`firstButtonText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Label for the first button, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to hide it.

`secondButtonText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Label for the second button, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to hide it.

`thirdButtonText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Label for the third button, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to hide it.

`defaultButton` [MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)

Button selected by default.

`cancelButton` [MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)

Button activated when the user dismisses the dialog.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)\>

### <a id="GDK_Net_GameUI_GameUiManager_ShowMultiplayerActivityGameInviteAsync_GDK_Net_Users_User_System_Threading_CancellationToken_"></a> ShowMultiplayerActivityGameInviteAsync\(User, CancellationToken\)

Shows the Multiplayer Activity game invite UI for the requesting user
(<code>XGameUiShowMultiplayerActivityGameInviteAsync</code> /
<code>XGameUiShowMultiplayerActivityGameInviteResult</code>).

```csharp
public Task ShowMultiplayerActivityGameInviteAsync(User user, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_GameUI_GameUiManager_ShowPlayerPickerAsync_GDK_Net_Users_User_System_String_System_UInt64___System_UInt64___System_UInt32_System_UInt32_System_Threading_CancellationToken_"></a> ShowPlayerPickerAsync\(User, string, ulong\[\], ulong\[\]?, uint, uint, CancellationToken\)

Shows the system player-picker UI and returns the selected player ids
(<code>XGameUiShowPlayerPickerAsync</code> / <code>XGameUiShowPlayerPickerResult</code>).

```csharp
public Task<ulong[]> ShowPlayerPickerAsync(User user, string promptText, ulong[] candidates, ulong[]? preSelected = null, uint minSelectionCount = 1, uint maxSelectionCount = 1, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`promptText` [string](https://learn.microsoft.com/dotnet/api/system.string)

Instructional text shown above the player list.

`candidates` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\[\]

The pool of players the user may select from.

`preSelected` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\[\]?

Players pre-selected when the dialog opens, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`minSelectionCount` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Minimum number of players the user must select.

`maxSelectionCount` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum number of players the user may select.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\[\]\>

Array of selected Xbox user ids.

### <a id="GDK_Net_GameUI_GameUiManager_ShowPlayerProfileCardAsync_GDK_Net_Users_User_System_UInt64_System_Threading_CancellationToken_"></a> ShowPlayerProfileCardAsync\(User, ulong, CancellationToken\)

Shows the system player profile card for <code class="paramref">targetPlayerId</code>
(<code>XGameUiShowPlayerProfileCardAsync</code> / <code>XGameUiShowPlayerProfileCardResult</code>).

```csharp
public Task ShowPlayerProfileCardAsync(User user, ulong targetPlayerId, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`targetPlayerId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Xbox user id of the player whose profile card to display.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_GameUI_GameUiManager_ShowSendGameInviteAsync_GDK_Net_Users_User_System_String_System_String_System_String_System_String_System_String_System_Threading_CancellationToken_"></a> ShowSendGameInviteAsync\(User, string, string, string, string?, string?, CancellationToken\)

Shows the system send-game-invite UI for a multiplayer session
(<code>XGameUiShowSendGameInviteAsync</code> / <code>XGameUiShowSendGameInviteResult</code>).

```csharp
public Task ShowSendGameInviteAsync(User user, string sessionConfigurationId, string sessionTemplateName, string sessionId, string? invitationText = null, string? customActivationContext = null, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`sessionConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Service configuration identifier.

`sessionTemplateName` [string](https://learn.microsoft.com/dotnet/api/system.string)

Multiplayer session template name.

`sessionId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Multiplayer session identifier.

`invitationText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional custom invitation message.

`customActivationContext` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional activation context passed to the invitee.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_GameUI_GameUiManager_ShowStateShareAsync_GDK_Net_Users_User_System_String_System_Threading_CancellationToken_"></a> ShowStateShareAsync\(User, string, CancellationToken\)

Shows the State Share UI so the player can share a deep link back into the current game
state (<code>XGameUiShowStateShareAsync</code> / <code>XGameUiShowStateShareResult</code>).

```csharp
public Task ShowStateShareAsync(User user, string linkToken, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`linkToken` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title-defined token identifying the state to share. It is handed back to the title when
the shared link is activated.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">user</code> or <code class="paramref">linkToken</code> is null.

### <a id="GDK_Net_GameUI_GameUiManager_ShowTextEntryAsync_System_String_System_String_System_String_GDK_Net_GameUI_TextEntryInputScope_System_UInt32_System_Threading_CancellationToken_"></a> ShowTextEntryAsync\(string?, string?, string?, TextEntryInputScope, uint, CancellationToken\)

Shows a modal virtual-keyboard text entry dialog and returns the user's input
(<code>XGameUiShowTextEntryAsync</code> / <code>XGameUiShowTextEntryResult</code>).

```csharp
public Task<string> ShowTextEntryAsync(string? titleText = null, string? descriptionText = null, string? defaultText = null, TextEntryInputScope inputScope = TextEntryInputScope.Default, uint maxTextLength = 256, CancellationToken cancellationToken = default)
```

#### Parameters

`titleText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Dialog title, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`descriptionText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Body text, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`defaultText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Pre-filled text, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for empty.

`inputScope` [TextEntryInputScope](GDK.Net.GameUI.TextEntryInputScope.md)

Keyboard layout hint.

`maxTextLength` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum number of characters the user may enter.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The string the user confirmed, or an empty string when dismissed.

### <a id="GDK_Net_GameUI_GameUiManager_ShowWebAuthenticationAsync_GDK_Net_Users_User_System_String_System_String_System_Threading_CancellationToken_"></a> ShowWebAuthenticationAsync\(User, string, string, CancellationToken\)

Shows the system web authentication browser
(<code>XGameUiShowWebAuthenticationAsync</code> / <code>XGameUiShowWebAuthenticationResult</code>).

```csharp
public Task<WebAuthenticationResult> ShowWebAuthenticationAsync(User user, string requestUri, string completionUri, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`requestUri` [string](https://learn.microsoft.com/dotnet/api/system.string)

Initial navigation URI.

`completionUri` [string](https://learn.microsoft.com/dotnet/api/system.string)

URI prefix that signals completion when the broker navigates to it.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[WebAuthenticationResult](GDK.Net.GameUI.WebAuthenticationResult.md)\>

### <a id="GDK_Net_GameUI_GameUiManager_ShowWebAuthenticationWithOptionsAsync_GDK_Net_Users_User_System_String_System_String_GDK_Net_GameUI_WebAuthenticationOptions_System_Threading_CancellationToken_"></a> ShowWebAuthenticationWithOptionsAsync\(User, string, string, WebAuthenticationOptions, CancellationToken\)

Shows the system web authentication browser with display options
(<code>XGameUiShowWebAuthenticationWithOptionsAsync</code> / <code>XGameUiShowWebAuthenticationResult</code>).

```csharp
public Task<WebAuthenticationResult> ShowWebAuthenticationWithOptionsAsync(User user, string requestUri, string completionUri, WebAuthenticationOptions options, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The requesting user.

`requestUri` [string](https://learn.microsoft.com/dotnet/api/system.string)

Initial navigation URI.

`completionUri` [string](https://learn.microsoft.com/dotnet/api/system.string)

URI prefix that signals completion when the broker navigates to it.

`options` [WebAuthenticationOptions](GDK.Net.GameUI.WebAuthenticationOptions.md)

Display options, e.g. <xref href="GDK.Net.GameUI.WebAuthenticationOptions.PreferFullscreen" data-throw-if-not-resolved="false"></xref>.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[WebAuthenticationResult](GDK.Net.GameUI.WebAuthenticationResult.md)\>

