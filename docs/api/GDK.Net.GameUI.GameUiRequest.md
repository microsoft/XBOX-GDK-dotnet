# <a id="GDK_Net_GameUI_GameUiRequest"></a> Class GameUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A pending request from the Gaming Runtime for the title to display a piece of UI that the system
would otherwise have drawn itself.

```csharp
public abstract class GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md)

#### Derived

[AchievementsUiRequest](GDK.Net.GameUI.AchievementsUiRequest.md), 
[ErrorDialogUiRequest](GDK.Net.GameUI.ErrorDialogUiRequest.md), 
[MessageDialogUiRequest](GDK.Net.GameUI.MessageDialogUiRequest.md), 
[MultiplayerActivityGameInviteUiRequest](GDK.Net.GameUI.MultiplayerActivityGameInviteUiRequest.md), 
[PlayerPickerUiRequest](GDK.Net.GameUI.PlayerPickerUiRequest.md), 
[PlayerProfileCardUiRequest](GDK.Net.GameUI.PlayerProfileCardUiRequest.md), 
[SendGameInviteUiRequest](GDK.Net.GameUI.SendGameInviteUiRequest.md), 
[TextEntryUiRequest](GDK.Net.GameUI.TextEntryUiRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Instances are handed to the handlers registered with <xref href="GDK.Net.GameUI.CustomGameUi.SetHandlers(GDK.Net.GameUI.CustomGameUiHandlers%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref>.
Exactly one <code>Respond</code> call must be made for each request, and until it is made the
<code>XGameUiShow*Async</code> operation that triggered the request stays pending. Responding is not
required to happen inside the handler: a title will normally capture the request, render its UI
over several frames, and respond once the player has chosen. Any thread may respond.
</p>
<p>
Every payload string and array has already been copied out of runtime-owned memory, so a request
stays valid indefinitely.
</p>

## Properties

### <a id="GDK_Net_GameUI_GameUiRequest_HasResponded"></a> HasResponded

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> once a response has been posted for this request.

```csharp
public bool HasResponded { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

