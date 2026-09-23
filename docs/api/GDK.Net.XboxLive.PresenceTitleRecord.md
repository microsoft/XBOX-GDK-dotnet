# <a id="GDK_Net_XboxLive_PresenceTitleRecord"></a> Class PresenceTitleRecord

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Presence for one title on a device. Managed snapshot of <code>XblPresenceTitleRecord</code>.

```csharp
public sealed class PresenceTitleRecord
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceTitleRecord](GDK.Net.XboxLive.PresenceTitleRecord.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_Broadcast"></a> Broadcast

Broadcast details when the user is broadcasting this title.

```csharp
public PresenceBroadcastRecord? Broadcast { get; }
```

#### Property Value

 [PresenceBroadcastRecord](GDK.Net.XboxLive.PresenceBroadcastRecord.md)?

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_LastModified"></a> LastModified

When the record was last updated.

```csharp
public DateTimeOffset LastModified { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_RichPresenceString"></a> RichPresenceString

The formatted localized rich presence string, or empty when none was returned.

```csharp
public string RichPresenceString { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_TitleActive"></a> TitleActive

Whether the user is active in the title.

```csharp
public bool TitleActive { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_TitleId"></a> TitleId

The title id.

```csharp
public uint TitleId { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_TitleName"></a> TitleName

The localized title name.

```csharp
public string TitleName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_PresenceTitleRecord_ViewState"></a> ViewState

The title's view state.

```csharp
public PresenceTitleViewState ViewState { get; }
```

#### Property Value

 [PresenceTitleViewState](GDK.Net.XboxLive.PresenceTitleViewState.md)

