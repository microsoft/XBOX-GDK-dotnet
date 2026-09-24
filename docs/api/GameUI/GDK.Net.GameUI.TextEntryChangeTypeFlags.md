# <a id="GDK_Net_GameUI_TextEntryChangeTypeFlags"></a> Enum TextEntryChangeTypeFlags

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

Flags describing what changed since the last <code>XGameUiTextEntryGetState</code> poll.
Mirrors <code>XGameUiTextEntryChangeTypeFlags</code>.

```csharp
[Flags]
public enum TextEntryChangeTypeFlags : uint
```

## Fields

`Dismissed = 2` 

The text-entry UI was dismissed.



`None = 0` 

No changes were reported.



`TextChanged = 1` 

The text content changed.



