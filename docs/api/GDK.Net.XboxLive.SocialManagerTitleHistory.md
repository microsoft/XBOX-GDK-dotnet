# <a id="GDK_Net_XboxLive_SocialManagerTitleHistory"></a> Class SocialManagerTitleHistory

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Title-history data attached to a social-manager user.

```csharp
public sealed class SocialManagerTitleHistory
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerTitleHistory](GDK.Net.XboxLive.SocialManagerTitleHistory.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialManagerTitleHistory_HasUserPlayed"></a> HasUserPlayed

Whether the user has played this title.

```csharp
public bool HasUserPlayed { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerTitleHistory_LastTimeUserPlayed"></a> LastTimeUserPlayed

When the user last played this title, when XSAPI supplied a timestamp.

```csharp
public DateTimeOffset? LastTimeUserPlayed { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_XboxLive_SocialManagerTitleHistory_LastTimeUserPlayedText"></a> LastTimeUserPlayedText

Localized text describing when the user last played this title.

```csharp
public string LastTimeUserPlayedText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

