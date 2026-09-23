# <a id="GDK_Net_GameUI_CustomGameUiHandlers"></a> Class CustomGameUiHandlers

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

The set of UI requests a title is willing to render itself. Passed to
<xref href="GDK.Net.GameUI.CustomGameUi.SetHandlers(GDK.Net.GameUI.CustomGameUiHandlers%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class CustomGameUiHandlers
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CustomGameUiHandlers](GDK.Net.GameUI.CustomGameUiHandlers.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Every handler is optional. A handler left <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is reported to the Gaming
Runtime as a null function pointer, which leaves that particular UI with the system, so a title
can take over just the dialogs it cares about.

## Properties

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_Achievements"></a> Achievements

Invoked to show the achievements UI.

```csharp
public Action<AchievementsUiRequest>? Achievements { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[AchievementsUiRequest](GDK.Net.GameUI.AchievementsUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_ErrorDialog"></a> ErrorDialog

Invoked to show an error dialog.

```csharp
public Action<ErrorDialogUiRequest>? ErrorDialog { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[ErrorDialogUiRequest](GDK.Net.GameUI.ErrorDialogUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_MessageDialog"></a> MessageDialog

Invoked to show a message dialog.

```csharp
public Action<MessageDialogUiRequest>? MessageDialog { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[MessageDialogUiRequest](GDK.Net.GameUI.MessageDialogUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_MultiplayerActivityGameInvite"></a> MultiplayerActivityGameInvite

Invoked to show the Multiplayer Activity invite UI.

```csharp
public Action<MultiplayerActivityGameInviteUiRequest>? MultiplayerActivityGameInvite { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[MultiplayerActivityGameInviteUiRequest](GDK.Net.GameUI.MultiplayerActivityGameInviteUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_PlayerPicker"></a> PlayerPicker

Invoked to let the player pick from a list of players.

```csharp
public Action<PlayerPickerUiRequest>? PlayerPicker { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[PlayerPickerUiRequest](GDK.Net.GameUI.PlayerPickerUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_PlayerProfileCard"></a> PlayerProfileCard

Invoked to show a player's profile card.

```csharp
public Action<PlayerProfileCardUiRequest>? PlayerProfileCard { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[PlayerProfileCardUiRequest](GDK.Net.GameUI.PlayerProfileCardUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_SendGameInvite"></a> SendGameInvite

Invoked to show the send-game-invite UI.

```csharp
public Action<SendGameInviteUiRequest>? SendGameInvite { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[SendGameInviteUiRequest](GDK.Net.GameUI.SendGameInviteUiRequest.md)\>?

### <a id="GDK_Net_GameUI_CustomGameUiHandlers_TextEntry"></a> TextEntry

Invoked to collect a line of text from the player.

```csharp
public Action<TextEntryUiRequest>? TextEntry { get; set; }
```

#### Property Value

 [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<[TextEntryUiRequest](GDK.Net.GameUI.TextEntryUiRequest.md)\>?

