# <a id="GDK_Net_XboxLive_EventsService"></a> Class EventsService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live telemetry event writing. Mirrors <code>events_c.h</code>.

```csharp
public sealed class EventsService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventsService](GDK.Net.XboxLive.EventsService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

In-game events contain JSON dimensions for finite fields, such as map id or difficulty, and
JSON measurements for scalar metrics, such as score, time or counters. Event and field names
must match the title's Xbox Live service configuration; if they do not, the service drops the
event without notification. On GDK PC, the Gaming Runtime Services runtime must include the
XGameEvent feature or the native call returns <code>E_NOTIMPL</code>.

## Methods

### <a id="GDK_Net_XboxLive_EventsService_WriteInGameEvent_System_String_System_String_System_String_"></a> WriteInGameEvent\(string, string?, string?\)

Writes one configured in-game telemetry event
(<code>XblEventsWriteInGameEvent</code>).

```csharp
public void WriteInGameEvent(string eventName, string? dimensionsJson = null, string? measurementsJson = null)
```

#### Parameters

`eventName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The service-configured event name. It must contain only ASCII letters and digits.

`dimensionsJson` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional JSON object containing dimension fields with finite string, Boolean or numeric values.

`measurementsJson` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional JSON object containing scalar numeric measurement fields.

