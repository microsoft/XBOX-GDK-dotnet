# <a id="GDK_Net_PlayFab_Party_PartyAudioFormat"></a> Class PartyAudioFormat

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_AUDIO_FORMAT</code>.

```csharp
public sealed class PartyAudioFormat
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyAudioFormat](GDK.Net.PlayFab.Party.PartyAudioFormat.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAudioFormat_BitsPerSample"></a> BitsPerSample

The number of bits in a single sample.

```csharp
public ushort BitsPerSample { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyAudioFormat_ChannelCount"></a> ChannelCount

The number of channels.

```csharp
public ushort ChannelCount { get; set; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_PlayFab_Party_PartyAudioFormat_ChannelMask"></a> ChannelMask

The speaker-position mask describing the channel layout.

```csharp
public uint ChannelMask { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyAudioFormat_Interleaved"></a> Interleaved

Whether channels are interleaved within a buffer.

```csharp
public bool Interleaved { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyAudioFormat_SampleType"></a> SampleType

Whether samples are integer or floating point.

```csharp
public PartyAudioSampleType SampleType { get; set; }
```

#### Property Value

 [PartyAudioSampleType](GDK.Net.PlayFab.Party.PartyAudioSampleType.md)

### <a id="GDK_Net_PlayFab_Party_PartyAudioFormat_SamplesPerSecond"></a> SamplesPerSecond

The sample rate in hertz.

```csharp
public uint SamplesPerSecond { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

