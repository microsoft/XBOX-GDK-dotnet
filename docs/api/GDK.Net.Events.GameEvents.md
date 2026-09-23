# <a id="GDK_Net_Events_GameEvents"></a> Class GameEvents

Namespace: [GDK.Net.Events](GDK.Net.Events.md)  
Assembly: GDK.Net.dll  

Writes in-game telemetry events (<code>XGameEventWrite</code>).

```csharp
public static class GameEvents
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameEvents](GDK.Net.Events.GameEvents.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Events are declared in Partner Center. The dimension and measurement payloads are JSON objects
whose property names are the event's declared fields; the runtime validates them against the
title's event manifest at write time.

## Methods

### <a id="GDK_Net_Events_GameEvents_Write_GDK_Net_Users_User_System_String_System_String_System_String_System_String_System_String_"></a> Write\(User, string, string, string, string?, string?\)

Writes a single in-game event for <code class="paramref">user</code>.

```csharp
public static void Write(User user, string serviceConfigId, string playSessionId, string eventName, string? dimensionsJson = null, string? measurementsJson = null)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The user the event is attributed to.

`serviceConfigId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's service configuration id (SCID).

`playSessionId` [string](https://learn.microsoft.com/dotnet/api/system.string)

An identifier that groups events from one play session. Any stable string chosen by the title.

`eventName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The event name as declared in Partner Center.

`dimensionsJson` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The event's dimension fields as a JSON object, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`measurementsJson` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The event's measurement fields as a JSON object, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

