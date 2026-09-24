# <a id="GDK_Net_GameUI_GameTextEntry"></a> Class GameTextEntry

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

An open non-modal text-entry session. Wraps <code>XGameUiTextEntryHandle</code>.

```csharp
public sealed class GameTextEntry : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameTextEntry](GDK.Net.GameUI.GameTextEntry.md)

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

Obtain an instance from <xref href="GDK.Net.GameUI.GameUiManager.OpenTextEntry(GDK.Net.GameUI.TextEntryOptions%2cSystem.UInt32%2cSystem.String%2cSystem.UInt32)" data-throw-if-not-resolved="false"></xref>. Poll <xref href="GDK.Net.GameUI.GameTextEntry.GetState" data-throw-if-not-resolved="false"></xref>
each frame to read user input; dispose the instance to close the IME panel
(<code>XGameUiTextEntryClose</code>).

## Methods

### <a id="GDK_Net_GameUI_GameTextEntry_Dispose"></a> Dispose\(\)

Closes the IME panel (<code>XGameUiTextEntryClose</code>).

```csharp
public void Dispose()
```

### <a id="GDK_Net_GameUI_GameTextEntry_GetExtents"></a> GetExtents\(\)

Returns the current screen-space bounds of the IME panel
(<code>XGameUiTextEntryGetExtents</code>).

```csharp
public TextEntryExtents GetExtents()
```

#### Returns

 [TextEntryExtents](GDK.Net.GameUI.TextEntryExtents.md)

### <a id="GDK_Net_GameUI_GameTextEntry_GetState"></a> GetState\(\)

Reads the current text, cursor position and change flags
(<code>XGameUiTextEntryGetState</code>).

```csharp
public TextEntryState GetState()
```

#### Returns

 [TextEntryState](GDK.Net.GameUI.TextEntryState.md)

#### Remarks

Call once per frame. The text buffer doubles on <code>ERROR_INSUFFICIENT_BUFFER</code>, up to
<code>64 KB</code>.

### <a id="GDK_Net_GameUI_GameTextEntry_UpdatePositionHint_GDK_Net_GameUI_TextEntryPositionHint_"></a> UpdatePositionHint\(TextEntryPositionHint\)

Moves the panel to the specified screen edge
(<code>XGameUiTextEntryUpdatePositionHint</code>).

```csharp
public void UpdatePositionHint(TextEntryPositionHint positionHint)
```

#### Parameters

`positionHint` [TextEntryPositionHint](GDK.Net.GameUI.TextEntryPositionHint.md)

Target screen edge.

### <a id="GDK_Net_GameUI_GameTextEntry_UpdateVisibility_GDK_Net_GameUI_TextEntryVisibilityFlags_"></a> UpdateVisibility\(TextEntryVisibilityFlags\)

Updates candidate window visibility (<code>XGameUiTextEntryUpdateVisibility</code>).

```csharp
public void UpdateVisibility(TextEntryVisibilityFlags visibilityFlags)
```

#### Parameters

`visibilityFlags` [TextEntryVisibilityFlags](GDK.Net.GameUI.TextEntryVisibilityFlags.md)

New visibility flags.

