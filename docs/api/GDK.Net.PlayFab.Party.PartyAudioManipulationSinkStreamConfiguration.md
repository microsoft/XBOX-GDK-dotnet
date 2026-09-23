# <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStreamConfiguration"></a> Class PartyAudioManipulationSinkStreamConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION</code>.

```csharp
public sealed class PartyAudioManipulationSinkStreamConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyAudioManipulationSinkStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStreamConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStreamConfiguration_Format"></a> Format

The requested format, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for Party's default.

```csharp
public PartyAudioFormat? Format { get; set; }
```

#### Property Value

 [PartyAudioFormat](GDK.Net.PlayFab.Party.PartyAudioFormat.md)?

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStreamConfiguration_MaxTotalAudioBufferSize"></a> MaxTotalAudioBufferSize

How much audio the stream may buffer before it drops data.

```csharp
public TimeSpan MaxTotalAudioBufferSize { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

