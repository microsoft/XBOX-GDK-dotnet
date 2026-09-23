# <a id="GDK_Net_Accessibility_AccessibilityManager"></a> Class AccessibilityManager

Namespace: [GDK.Net.Accessibility](GDK.Net.Accessibility.md)  
Assembly: GDK.Net.dll  

Accessibility settings: closed captions, high contrast, and speech-to-text.

```csharp
public static class AccessibilityManager
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AccessibilityManager](GDK.Net.Accessibility.AccessibilityManager.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

These APIs are stateless and always available after <code>GameRuntime.Initialize()</code>;
no separate subsystem initialization is required. All methods are static.

## Methods

### <a id="GDK_Net_Accessibility_AccessibilityManager_BeginHypothesisString_System_String_System_String_GDK_Net_Accessibility_SpeechToTextType_"></a> BeginHypothesisString\(string, string, SpeechToTextType\)

Begins a rolling hypothesis speech-to-text string
(<code>XSpeechToTextBeginHypothesisString</code>).

```csharp
public static uint BeginHypothesisString(string speakerName, string content, SpeechToTextType type)
```

#### Parameters

`speakerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

`content` [string](https://learn.microsoft.com/dotnet/api/system.string)

`type` [SpeechToTextType](GDK.Net.Accessibility.SpeechToTextType.md)

#### Returns

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

A hypothesis id to pass to <xref href="GDK.Net.Accessibility.AccessibilityManager.UpdateHypothesisString(System.UInt32%2cSystem.String)" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.Accessibility.AccessibilityManager.FinalizeHypothesisString(System.UInt32%2cSystem.String)" data-throw-if-not-resolved="false"></xref>, or <xref href="GDK.Net.Accessibility.AccessibilityManager.CancelHypothesisString(System.UInt32)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Accessibility_AccessibilityManager_CancelHypothesisString_System_UInt32_"></a> CancelHypothesisString\(uint\)

Cancels a rolling hypothesis string without finalizing it
(<code>XSpeechToTextCancelHypothesisString</code>).

```csharp
public static void CancelHypothesisString(uint hypothesisId)
```

#### Parameters

`hypothesisId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Accessibility_AccessibilityManager_FinalizeHypothesisString_System_UInt32_System_String_"></a> FinalizeHypothesisString\(uint, string\)

Finalizes a rolling hypothesis string as the confirmed transcription
(<code>XSpeechToTextFinalizeHypothesisString</code>).

```csharp
public static void FinalizeHypothesisString(uint hypothesisId, string content)
```

#### Parameters

`hypothesisId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`content` [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Accessibility_AccessibilityManager_GetClosedCaptionProperties"></a> GetClosedCaptionProperties\(\)

Returns the system closed-caption display properties set by the user
(<code>XClosedCaptionGetProperties</code>).

```csharp
public static ClosedCaptionProperties GetClosedCaptionProperties()
```

#### Returns

 [ClosedCaptionProperties](GDK.Net.Accessibility.ClosedCaptionProperties.md)

### <a id="GDK_Net_Accessibility_AccessibilityManager_GetHighContrastMode"></a> GetHighContrastMode\(\)

Returns the system high-contrast mode selected by the user
(<code>XHighContrastGetMode</code>).

```csharp
public static HighContrastMode GetHighContrastMode()
```

#### Returns

 [HighContrastMode](GDK.Net.Accessibility.HighContrastMode.md)

### <a id="GDK_Net_Accessibility_AccessibilityManager_SendSpeechToText_System_String_System_String_GDK_Net_Accessibility_SpeechToTextType_"></a> SendSpeechToText\(string, string, SpeechToTextType\)

Sends a finalized speech-to-text string for display
(<code>XSpeechToTextSendString</code>).

```csharp
public static void SendSpeechToText(string speakerName, string content, SpeechToTextType type)
```

#### Parameters

`speakerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

Display name for the speaker.

`content` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text to display.

`type` [SpeechToTextType](GDK.Net.Accessibility.SpeechToTextType.md)

Whether the text came from voice or was entered manually.

### <a id="GDK_Net_Accessibility_AccessibilityManager_SetClosedCaptionEnabled_System_Boolean_"></a> SetClosedCaptionEnabled\(bool\)

Overrides the closed-caption enabled flag (<code>XClosedCaptionSetEnabled</code>).

```csharp
public static void SetClosedCaptionEnabled(bool enabled)
```

#### Parameters

`enabled` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> to enable closed captions.

### <a id="GDK_Net_Accessibility_AccessibilityManager_SetSpeechToTextPositionHint_GDK_Net_Accessibility_SpeechToTextPositionHint_"></a> SetSpeechToTextPositionHint\(SpeechToTextPositionHint\)

Sets the screen position of the speech-to-text overlay
(<code>XSpeechToTextSetPositionHint</code>).

```csharp
public static void SetSpeechToTextPositionHint(SpeechToTextPositionHint position)
```

#### Parameters

`position` [SpeechToTextPositionHint](GDK.Net.Accessibility.SpeechToTextPositionHint.md)

### <a id="GDK_Net_Accessibility_AccessibilityManager_UpdateHypothesisString_System_UInt32_System_String_"></a> UpdateHypothesisString\(uint, string\)

Updates a rolling hypothesis string (<code>XSpeechToTextUpdateHypothesisString</code>).

```csharp
public static void UpdateHypothesisString(uint hypothesisId, string content)
```

#### Parameters

`hypothesisId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`content` [string](https://learn.microsoft.com/dotnet/api/system.string)

