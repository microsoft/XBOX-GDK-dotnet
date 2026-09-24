# <a id="GDK_Net_Accessibility_SpeechSynthesizer"></a> Class SpeechSynthesizer

Namespace: [GDK.Net.Accessibility](GDK.Net.Accessibility.md)  
Assembly: GDK.Net.dll  

Speech synthesizer. Wraps <code>XSpeechSynthesizerHandle</code>.

```csharp
public sealed class SpeechSynthesizer : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SpeechSynthesizer](GDK.Net.Accessibility.SpeechSynthesizer.md)

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

<p>
Create an instance with the default constructor, optionally call
<xref href="GDK.Net.Accessibility.SpeechSynthesizer.SetDefaultVoice" data-throw-if-not-resolved="false"></xref> or <xref href="GDK.Net.Accessibility.SpeechSynthesizer.SetCustomVoice(System.String)" data-throw-if-not-resolved="false"></xref> to choose a voice, then call
<xref href="GDK.Net.Accessibility.SpeechSynthesizer.SynthesizeText(System.String)" data-throw-if-not-resolved="false"></xref> or <xref href="GDK.Net.Accessibility.SpeechSynthesizer.SynthesizeSsml(System.String)" data-throw-if-not-resolved="false"></xref> to produce PCM audio.
</p>
<p>
<xref href="GDK.Net.Accessibility.SpeechSynthesizer.GetInstalledVoices" data-throw-if-not-resolved="false"></xref> is a static helper that does not require a synthesizer
instance.
</p>

## Constructors

### <a id="GDK_Net_Accessibility_SpeechSynthesizer__ctor"></a> SpeechSynthesizer\(\)

Creates a new speech synthesizer (<code>XSpeechSynthesizerCreate</code>).

```csharp
public SpeechSynthesizer()
```

## Methods

### <a id="GDK_Net_Accessibility_SpeechSynthesizer_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Accessibility_SpeechSynthesizer_GetInstalledVoices"></a> GetInstalledVoices\(\)

Returns information about every installed speech-synthesis voice
(<code>XSpeechSynthesizerEnumerateInstalledVoices</code>).
Does not require a <xref href="GDK.Net.Accessibility.SpeechSynthesizer" data-throw-if-not-resolved="false"></xref> instance.

```csharp
public static IReadOnlyList<SpeechSynthesizerVoiceInfo> GetInstalledVoices()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SpeechSynthesizerVoiceInfo](GDK.Net.Accessibility.SpeechSynthesizerVoiceInfo.md)\>

### <a id="GDK_Net_Accessibility_SpeechSynthesizer_SetCustomVoice_System_String_"></a> SetCustomVoice\(string\)

Selects a specific installed voice by id (<code>XSpeechSynthesizerSetCustomVoice</code>).

```csharp
public void SetCustomVoice(string voiceId)
```

#### Parameters

`voiceId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The <xref href="GDK.Net.Accessibility.SpeechSynthesizerVoiceInfo.VoiceId" data-throw-if-not-resolved="false"></xref> of the desired voice.

### <a id="GDK_Net_Accessibility_SpeechSynthesizer_SetDefaultVoice"></a> SetDefaultVoice\(\)

Selects the system default voice for this synthesizer
(<code>XSpeechSynthesizerSetDefaultVoice</code>).

```csharp
public void SetDefaultVoice()
```

### <a id="GDK_Net_Accessibility_SpeechSynthesizer_SynthesizeSsml_System_String_"></a> SynthesizeSsml\(string\)

Synthesizes SSML markup to a PCM audio byte array
(<code>XSpeechSynthesizerCreateStreamFromSsml</code> + <code>XSpeechSynthesizerGetStreamData</code>).

```csharp
public byte[] SynthesizeSsml(string ssml)
```

#### Parameters

`ssml` [string](https://learn.microsoft.com/dotnet/api/system.string)

SSML markup string.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Raw PCM audio bytes.

### <a id="GDK_Net_Accessibility_SpeechSynthesizer_SynthesizeText_System_String_"></a> SynthesizeText\(string\)

Synthesizes plain text to a PCM audio byte array
(<code>XSpeechSynthesizerCreateStreamFromText</code> + <code>XSpeechSynthesizerGetStreamData</code>).

```csharp
public byte[] SynthesizeText(string text)
```

#### Parameters

`text` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text to synthesize.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Raw PCM audio bytes.

