# <a id="GDK_Net_GameUI_TextEntryState"></a> Class TextEntryState

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

Current state of a non-modal text entry session
(<code>XGameUiTextEntryGetState</code>).

```csharp
public sealed class TextEntryState
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TextEntryState](GDK.Net.GameUI.TextEntryState.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_TextEntryState_ChangeType"></a> ChangeType

What changed since the last poll.

```csharp
public TextEntryChangeTypeFlags ChangeType { get; }
```

#### Property Value

 [TextEntryChangeTypeFlags](GDK.Net.GameUI.TextEntryChangeTypeFlags.md)

### <a id="GDK_Net_GameUI_TextEntryState_CursorIndex"></a> CursorIndex

Current insertion-point position in <xref href="GDK.Net.GameUI.TextEntryState.Text" data-throw-if-not-resolved="false"></xref>.

```csharp
public uint CursorIndex { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameUI_TextEntryState_ImeClauseEndIndex"></a> ImeClauseEndIndex

Exclusive end of the active IME composition clause in <xref href="GDK.Net.GameUI.TextEntryState.Text" data-throw-if-not-resolved="false"></xref>.

```csharp
public uint ImeClauseEndIndex { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameUI_TextEntryState_ImeClauseStartIndex"></a> ImeClauseStartIndex

Start of the active IME composition clause in <xref href="GDK.Net.GameUI.TextEntryState.Text" data-throw-if-not-resolved="false"></xref>.

```csharp
public uint ImeClauseStartIndex { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameUI_TextEntryState_Text"></a> Text

Current text content of the entry field.

```csharp
public string Text { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

