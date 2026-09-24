# <a id="GDK_Net_GameUI_TextEntryUiRequest"></a> Class TextEntryUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to collect a line of text from the player (<code>XGameUiShowTextEntryUiCallback</code>).

```csharp
public sealed class TextEntryUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[TextEntryUiRequest](GDK.Net.GameUI.TextEntryUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_TextEntryUiRequest_DefaultText"></a> DefaultText

The text the field should start out containing.

```csharp
public string? DefaultText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_TextEntryUiRequest_DescriptionText"></a> DescriptionText

Explanatory text to show with the field.

```csharp
public string? DescriptionText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_TextEntryUiRequest_InputScope"></a> InputScope

The kind of text expected, which selects the on-screen keyboard layout.

```csharp
public TextEntryInputScope InputScope { get; }
```

#### Property Value

 [TextEntryInputScope](GDK.Net.GameUI.TextEntryInputScope.md)

### <a id="GDK_Net_GameUI_TextEntryUiRequest_MaxTextLength"></a> MaxTextLength

The maximum number of characters the title should accept.

```csharp
public uint MaxTextLength { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameUI_TextEntryUiRequest_TitleText"></a> TitleText

The title to show above the text field.

```csharp
public string? TitleText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="GDK_Net_GameUI_TextEntryUiRequest_Respond_System_String_"></a> Respond\(string\)

Reports the text the player entered (<code>XGameUiSetTextEntryUiResponse</code>).

```csharp
public void Respond(string response)
```

#### Parameters

`response` [string](https://learn.microsoft.com/dotnet/api/system.string)

The entered text. Pass <xref href="System.String.Empty" data-throw-if-not-resolved="false"></xref> when the player cancelled; the native API
requires a non-null string.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">response</code> is null.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

