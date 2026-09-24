# <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStreamConfiguration"></a> Class PartyAudioManipulationSourceStreamConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION</code>.

```csharp
public sealed class PartyAudioManipulationSourceStreamConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyAudioManipulationSourceStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStreamConfiguration_Format"></a> Format

The requested format, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for Party's default.

```csharp
public PartyAudioFormat? Format { get; set; }
```

#### Property Value

 [PartyAudioFormat](GDK.Net.PlayFab.Party.PartyAudioFormat.md)?

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStreamConfiguration_MaxTotalAudioBufferSize"></a> MaxTotalAudioBufferSize

How much audio the stream may buffer before it drops data.

```csharp
public TimeSpan MaxTotalAudioBufferSize { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

