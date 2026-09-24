# <a id="GDK_Net_PlayFab_Party_PartyTextToSpeechProfile"></a> Class PartyTextToSpeechProfile

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_TEXT_TO_SPEECH_PROFILE_HANDLE</code>: a synthetic voice available for
text-to-speech.

```csharp
public sealed class PartyTextToSpeechProfile : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyTextToSpeechProfile](GDK.Net.PlayFab.Party.PartyTextToSpeechProfile.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyTextToSpeechProfile_Gender"></a> Gender

The voice's gender.

```csharp
public PartyGender Gender { get; }
```

#### Property Value

 [PartyGender](GDK.Net.PlayFab.Party.PartyGender.md)

### <a id="GDK_Net_PlayFab_Party_PartyTextToSpeechProfile_Identifier"></a> Identifier

The identifier used to select the profile.

```csharp
public string Identifier { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyTextToSpeechProfile_LanguageCode"></a> LanguageCode

The BCP-47 language code the profile speaks.

```csharp
public string LanguageCode { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyTextToSpeechProfile_Name"></a> Name

The profile's display name.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

