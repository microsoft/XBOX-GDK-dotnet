# <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord"></a> Class SocialManagerPresenceTitleRecord

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One title presence record in a social-manager presence snapshot.

```csharp
public sealed class SocialManagerPresenceTitleRecord
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerPresenceTitleRecord](GDK.Net.XboxLive.SocialManagerPresenceTitleRecord.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_DeviceType"></a> DeviceType

The device reporting this title presence.

```csharp
public PresenceDeviceType DeviceType { get; }
```

#### Property Value

 [PresenceDeviceType](GDK.Net.XboxLive.PresenceDeviceType.md)

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_IsBroadcasting"></a> IsBroadcasting

Whether the user is broadcasting this title.

```csharp
public bool IsBroadcasting { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_IsPrimary"></a> IsPrimary

Whether this is the user's primary presence record.

```csharp
public bool IsPrimary { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_IsTitleActive"></a> IsTitleActive

Whether the user is active in the title.

```csharp
public bool IsTitleActive { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_PresenceText"></a> PresenceText

The formatted localized rich-presence string.

```csharp
public string PresenceText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_TitleId"></a> TitleId

The title id.

```csharp
public uint TitleId { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_SocialManagerPresenceTitleRecord_TitleName"></a> TitleName

The localized title name.

```csharp
public string TitleName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

