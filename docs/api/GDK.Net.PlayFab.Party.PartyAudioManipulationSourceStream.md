# <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStream"></a> Class PartyAudioManipulationSourceStream

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_HANDLE</code>: a stream the title reads
pre-encode audio buffers from.

```csharp
public sealed class PartyAudioManipulationSourceStream : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyAudioManipulationSourceStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStream.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStream_AvailableBufferCount"></a> AvailableBufferCount

How many buffers are waiting to be read.

```csharp
public uint AvailableBufferCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStream_Configuration"></a> Configuration

The configuration the stream was created with.

```csharp
public PartyAudioManipulationSourceStreamConfiguration Configuration { get; }
```

#### Property Value

 [PartyAudioManipulationSourceStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStream_Format"></a> Format

The format of the buffers the stream produces.

```csharp
public PartyAudioFormat Format { get; }
```

#### Property Value

 [PartyAudioFormat](GDK.Net.PlayFab.Party.PartyAudioFormat.md)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStream_ReadNextBuffer"></a> ReadNextBuffer\(\)

Reads the next buffer, copying it into managed memory and immediately returning the native
buffer to Party.

```csharp
public byte[]? ReadNextBuffer()
```

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

The audio payload, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when no buffer is available.

