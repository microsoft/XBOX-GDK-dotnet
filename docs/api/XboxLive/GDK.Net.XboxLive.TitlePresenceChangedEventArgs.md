# <a id="GDK_Net_XboxLive_TitlePresenceChangedEventArgs"></a> Class TitlePresenceChangedEventArgs

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.XboxLive.PresenceService.TitlePresenceChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class TitlePresenceChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[TitlePresenceChangedEventArgs](GDK.Net.XboxLive.TitlePresenceChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_TitlePresenceChangedEventArgs_TitleId"></a> TitleId

The title whose presence changed.

```csharp
public uint TitleId { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_TitlePresenceChangedEventArgs_TitleState"></a> TitleState

The title presence transition.

```csharp
public PresenceTitleState TitleState { get; }
```

#### Property Value

 [PresenceTitleState](GDK.Net.XboxLive.PresenceTitleState.md)

### <a id="GDK_Net_XboxLive_TitlePresenceChangedEventArgs_XboxUserId"></a> XboxUserId

The user whose title presence changed.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

