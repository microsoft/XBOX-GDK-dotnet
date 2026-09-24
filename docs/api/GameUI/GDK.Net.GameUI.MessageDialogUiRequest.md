# <a id="GDK_Net_GameUI_MessageDialogUiRequest"></a> Class MessageDialogUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to show a message dialog (<code>XGameUiShowMessageDialogUiCallback</code>).

```csharp
public sealed class MessageDialogUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[MessageDialogUiRequest](GDK.Net.GameUI.MessageDialogUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_CancelButton"></a> CancelButton

The button that a cancel gesture (B button, Escape) should select.

```csharp
public MessageDialogButton CancelButton { get; }
```

#### Property Value

 [MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_ContentText"></a> ContentText

The dialog body text.

```csharp
public string? ContentText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_DefaultButton"></a> DefaultButton

The button that should be focused initially.

```csharp
public MessageDialogButton DefaultButton { get; }
```

#### Property Value

 [MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_FirstButtonText"></a> FirstButtonText

The first button's label. Always present.

```csharp
public string? FirstButtonText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_SecondButtonText"></a> SecondButtonText

The second button's label, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when there is no second button.

```csharp
public string? SecondButtonText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_ThirdButtonText"></a> ThirdButtonText

The third button's label, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when there is no third button.

```csharp
public string? ThirdButtonText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_TitleText"></a> TitleText

The dialog title.

```csharp
public string? TitleText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="GDK_Net_GameUI_MessageDialogUiRequest_Respond_GDK_Net_GameUI_MessageDialogButton_"></a> Respond\(MessageDialogButton\)

Reports which button the player chose (<code>XGameUiSetMessageDialogUiResponse</code>).

```csharp
public void Respond(MessageDialogButton response)
```

#### Parameters

`response` [MessageDialogButton](GDK.Net.GameUI.MessageDialogButton.md)

The chosen button.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

