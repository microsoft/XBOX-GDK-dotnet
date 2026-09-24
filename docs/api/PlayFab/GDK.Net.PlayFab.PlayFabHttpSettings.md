# <a id="GDK_Net_PlayFab_PlayFabHttpSettings"></a> Class PlayFabHttpSettings

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Process-wide HTTP behaviour for the PlayFab libraries (<code>PFHttpConfig.h</code>).

```csharp
public static class PlayFabHttpSettings
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabHttpSettings](GDK.Net.PlayFab.PlayFabHttpSettings.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PlayFabHttpSettings_AllowRetry"></a> AllowRetry

Whether a failed request may be retried (<code>PFHttpRetrySettings.allowRetry</code>).

```csharp
public static bool AllowRetry { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_PlayFabHttpSettings_MinimumRetryDelayInSeconds"></a> MinimumRetryDelayInSeconds

The shortest delay before a retry, in seconds.

```csharp
public static uint MinimumRetryDelayInSeconds { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_PlayFabHttpSettings_RequestResponseCompression"></a> RequestResponseCompression

Whether request and response bodies are compressed (<code>PFHttpSettings</code>).

```csharp
public static bool RequestResponseCompression { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_PlayFabHttpSettings_TimeoutWindowInSeconds"></a> TimeoutWindowInSeconds

How long a request may keep being retried, in seconds.

```csharp
public static uint TimeoutWindowInSeconds { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

