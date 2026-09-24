# <a id="GDK_Net_PlayFab_Events"></a> Class Events

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Events service (<code>PFEvents.h</code>).

```csharp
public static class Events
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Events](GDK.Net.PlayFab.Events.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Events_WriteEventsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_EventsWriteEventsRequest_System_Threading_CancellationToken_"></a> WriteEventsAsync\(PlayFabEntity, EventsWriteEventsRequest, CancellationToken\)

Calls <code>PFEventsWriteEventsAsync</code>.

```csharp
public static Task<EventsWriteEventsResponse> WriteEventsAsync(PlayFabEntity entity, EventsWriteEventsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [EventsWriteEventsRequest](GDK.Net.PlayFab.EventsWriteEventsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[EventsWriteEventsResponse](GDK.Net.PlayFab.EventsWriteEventsResponse.md)\>

### <a id="GDK_Net_PlayFab_Events_WriteTelemetryEventsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_EventsWriteEventsRequest_System_Threading_CancellationToken_"></a> WriteTelemetryEventsAsync\(PlayFabEntity, EventsWriteEventsRequest, CancellationToken\)

Calls <code>PFEventsWriteTelemetryEventsAsync</code>.

```csharp
public static Task<EventsWriteEventsResponse> WriteTelemetryEventsAsync(PlayFabEntity entity, EventsWriteEventsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [EventsWriteEventsRequest](GDK.Net.PlayFab.EventsWriteEventsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[EventsWriteEventsResponse](GDK.Net.PlayFab.EventsWriteEventsResponse.md)\>

