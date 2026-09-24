# <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo"></a> Class SpeechSynthesizerVoiceInfo

Namespace: [GDK.Net.Accessibility](GDK.Net.Accessibility.md)  
Assembly: GDK.Net.dll  

Information about an installed speech-synthesis voice, returned by
<xref href="GDK.Net.Accessibility.SpeechSynthesizer.GetInstalledVoices" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XSpeechSynthesizerVoiceInformation</code>.

```csharp
public sealed class SpeechSynthesizerVoiceInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SpeechSynthesizerVoiceInfo](GDK.Net.Accessibility.SpeechSynthesizerVoiceInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo_Description"></a> Description

Human-readable description of the voice.

```csharp
public string Description { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo_DisplayName"></a> DisplayName

Display name of the voice.

```csharp
public string DisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo_Gender"></a> Gender

Voice gender.

```csharp
public SpeechSynthesizerVoiceGender Gender { get; }
```

#### Property Value

 [SpeechSynthesizerVoiceGender](GDK.Net.Accessibility.SpeechSynthesizerVoiceGender.md)

### <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo_Language"></a> Language

BCP-47 language tag (e.g. <code>"en-US"</code>).

```csharp
public string Language { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo_VoiceId"></a> VoiceId

Unique voice identifier, passed to <code>XSpeechSynthesizerSetCustomVoice</code>.

```csharp
public string VoiceId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_Accessibility_SpeechSynthesizerVoiceInfo_ToString"></a> ToString\(\)

Returns the display name, language and gender for diagnostics.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

