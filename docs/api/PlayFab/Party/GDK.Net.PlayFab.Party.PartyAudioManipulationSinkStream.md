# <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStream"></a> Class PartyAudioManipulationSinkStream

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_AUDIO_MANIPULATION_SINK_STREAM_HANDLE</code>: a stream the title writes
processed audio buffers to.

```csharp
public sealed class PartyAudioManipulationSinkStream : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyAudioManipulationSinkStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStream.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStream_Configuration"></a> Configuration

The configuration the stream was created with.

```csharp
public PartyAudioManipulationSinkStreamConfiguration Configuration { get; }
```

#### Property Value

 [PartyAudioManipulationSinkStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStreamConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStream_Format"></a> Format

The format the stream expects.

```csharp
public PartyAudioFormat Format { get; }
```

#### Property Value

 [PartyAudioFormat](GDK.Net.PlayFab.Party.PartyAudioFormat.md)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStream_SubmitBuffer_System_Byte___"></a> SubmitBuffer\(byte\[\]\)

Submits a buffer of audio to the stream.

```csharp
public void SubmitBuffer(byte[] audio)
```

#### Parameters

`audio` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The audio payload in the stream's format.

