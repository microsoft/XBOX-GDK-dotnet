# <a id="GDK_Net_PlayFab_Party_PartyXblAccessibilitySettings"></a> Class PartyXblAccessibilitySettings

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_XBL_ACCESSIBILITY_SETTINGS</code>: an Xbox Live user's chat accessibility
preferences.

```csharp
public sealed record PartyXblAccessibilitySettings : IEquatable<PartyXblAccessibilitySettings>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblAccessibilitySettings](GDK.Net.PlayFab.Party.PartyXblAccessibilitySettings.md)

#### Implements

[IEquatable<PartyXblAccessibilitySettings\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblAccessibilitySettings__ctor_System_Boolean_System_Boolean_System_String_GDK_Net_PlayFab_Party_PartyGender_"></a> PartyXblAccessibilitySettings\(bool, bool, string, PartyGender\)

Projects <code>PARTY_XBL_ACCESSIBILITY_SETTINGS</code>: an Xbox Live user's chat accessibility
preferences.

```csharp
public PartyXblAccessibilitySettings(bool SpeechToTextEnabled, bool TextToSpeechEnabled, string LanguageCode, PartyGender Gender)
```

#### Parameters

`SpeechToTextEnabled` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the user wants incoming voice transcribed.

`TextToSpeechEnabled` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the user wants outgoing text synthesized.

`LanguageCode` [string](https://learn.microsoft.com/dotnet/api/system.string)

The user's preferred language, as a BCP-47 tag.

`Gender` [PartyGender](GDK.Net.PlayFab.Party.PartyGender.md)

The synthetic voice gender the user prefers.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblAccessibilitySettings_Gender"></a> Gender

The synthetic voice gender the user prefers.

```csharp
public PartyGender Gender { get; init; }
```

#### Property Value

 [PartyGender](GDK.Net.PlayFab.Party.PartyGender.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblAccessibilitySettings_LanguageCode"></a> LanguageCode

The user's preferred language, as a BCP-47 tag.

```csharp
public string LanguageCode { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyXblAccessibilitySettings_SpeechToTextEnabled"></a> SpeechToTextEnabled

Whether the user wants incoming voice transcribed.

```csharp
public bool SpeechToTextEnabled { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyXblAccessibilitySettings_TextToSpeechEnabled"></a> TextToSpeechEnabled

Whether the user wants outgoing text synthesized.

```csharp
public bool TextToSpeechEnabled { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

