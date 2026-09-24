# <a id="GDK_Net_XboxLive_XboxLiveContextSettings"></a> Class XboxLiveContextSettings

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

HTTP and websocket tuning for one <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>xbox_live_context_settings_c.h</code>.

```csharp
public sealed class XboxLiveContextSettings
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[XboxLiveContextSettings](GDK.Net.XboxLive.XboxLiveContextSettings.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The defaults are what the service expects; changing them is a deliberate act, usually to cope
with a slow network. Every value is expressed as a <xref href="System.TimeSpan" data-throw-if-not-resolved="false"></xref> here even though the
native surface takes whole seconds, so a sub-second value is rounded down.

## Properties

### <a id="GDK_Net_XboxLive_XboxLiveContextSettings_HttpRetryDelay"></a> HttpRetryDelay

Delay before a failed call is retried
(<code>XblContextSettingsGet/SetHttpRetryDelay</code>).

```csharp
public TimeSpan HttpRetryDelay { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

### <a id="GDK_Net_XboxLive_XboxLiveContextSettings_HttpTimeoutWindow"></a> HttpTimeoutWindow

Total window an HTTP call, including retries, may occupy
(<code>XblContextSettingsGet/SetHttpTimeoutWindow</code>).

```csharp
public TimeSpan HttpTimeoutWindow { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

### <a id="GDK_Net_XboxLive_XboxLiveContextSettings_LongHttpTimeout"></a> LongHttpTimeout

Timeout applied to calls the service marks as long-running
(<code>XblContextSettingsGet/SetLongHttpTimeout</code>).

```csharp
public TimeSpan LongHttpTimeout { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

### <a id="GDK_Net_XboxLive_XboxLiveContextSettings_UseCrossPlatformQosServers"></a> UseCrossPlatformQosServers

Whether quality-of-service probes use the cross-platform servers
(<code>XblContextSettingsGet/SetUseCrossPlatformQosServers</code>).

```csharp
public bool UseCrossPlatformQosServers { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_XboxLiveContextSettings_WebsocketTimeoutWindow"></a> WebsocketTimeoutWindow

Total window a websocket connection attempt may occupy
(<code>XblContextSettingsGet/SetWebsocketTimeoutWindow</code>).

```csharp
public TimeSpan WebsocketTimeoutWindow { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

