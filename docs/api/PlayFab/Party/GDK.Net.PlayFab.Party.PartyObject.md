# <a id="GDK_Net_PlayFab_Party_PartyObject"></a> Class PartyObject

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Base for the Party objects that are owned by the library and torn down through a state change
rather than by the caller.

```csharp
public abstract class PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md)

#### Derived

[PartyAudioManipulationSinkStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStream.md), 
[PartyAudioManipulationSourceStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStream.md), 
[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md), 
[PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md), 
[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md), 
[PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md), 
[PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md), 
[PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md), 
[PartyTextToSpeechProfile](GDK.Net.PlayFab.Party.PartyTextToSpeechProfile.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

A Party object is only meaningful while the network it belongs to is alive. Once the matching
destruction state change has been returned from a pump, the wrapper is invalidated and every
member throws <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref> instead of touching a stale handle.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyObject_IsValid"></a> IsValid

Whether the object is still usable.

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

