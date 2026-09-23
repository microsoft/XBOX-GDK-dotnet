# <a id="GDK_Net_GameUI_TextEntryOptions"></a> Struct TextEntryOptions

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

Opening configuration for a non-modal text entry session
(<code>XGameUiTextEntryOpen</code>).

```csharp
public readonly struct TextEntryOptions
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_GameUI_TextEntryOptions__ctor_GDK_Net_GameUI_TextEntryInputScope_GDK_Net_GameUI_TextEntryPositionHint_GDK_Net_GameUI_TextEntryVisibilityFlags_"></a> TextEntryOptions\(TextEntryInputScope, TextEntryPositionHint, TextEntryVisibilityFlags\)

```csharp
public TextEntryOptions(TextEntryInputScope inputScope, TextEntryPositionHint positionHint = TextEntryPositionHint.Bottom, TextEntryVisibilityFlags visibilityFlags = TextEntryVisibilityFlags.Default)
```

#### Parameters

`inputScope` [TextEntryInputScope](GDK.Net.GameUI.TextEntryInputScope.md)

Keyboard input scope.

`positionHint` [TextEntryPositionHint](GDK.Net.GameUI.TextEntryPositionHint.md)

Preferred screen edge for the panel.

`visibilityFlags` [TextEntryVisibilityFlags](GDK.Net.GameUI.TextEntryVisibilityFlags.md)

IME candidate window visibility.

## Properties

### <a id="GDK_Net_GameUI_TextEntryOptions_InputScope"></a> InputScope

Keyboard input scope.

```csharp
public TextEntryInputScope InputScope { get; }
```

#### Property Value

 [TextEntryInputScope](GDK.Net.GameUI.TextEntryInputScope.md)

### <a id="GDK_Net_GameUI_TextEntryOptions_PositionHint"></a> PositionHint

Preferred screen edge for the panel.

```csharp
public TextEntryPositionHint PositionHint { get; }
```

#### Property Value

 [TextEntryPositionHint](GDK.Net.GameUI.TextEntryPositionHint.md)

### <a id="GDK_Net_GameUI_TextEntryOptions_VisibilityFlags"></a> VisibilityFlags

IME candidate window visibility.

```csharp
public TextEntryVisibilityFlags VisibilityFlags { get; }
```

#### Property Value

 [TextEntryVisibilityFlags](GDK.Net.GameUI.TextEntryVisibilityFlags.md)

