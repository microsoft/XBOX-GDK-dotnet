# <a id="GDK_Net_XboxLive_RealTimeActivityService"></a> Class RealTimeActivityService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Real-time activity websocket lifetime and notifications for an <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>real_time_activity_c.h</code>.

```csharp
public sealed class RealTimeActivityService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RealTimeActivityService](GDK.Net.XboxLive.RealTimeActivityService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Real-time activity is a long-lived websocket connection, not a request/response call. XSAPI opens
and closes it on demand: tracking presence, social relationship or statistic changes brings it
up, and dropping the last subscription takes it down. That lifetime is deliberately not
projected, because the native APIs that exposed it are all deprecated:
<code>XblRealTimeActivityActivate</code> and <code>Deactivate</code> are documented as no longer required,
<code>XblRealTimeActivityAddSubscriptionErrorHandler</code> is documented as never invoked because
XSAPI now handles those errors internally, and <code>XblRealTimeActivitySubscriptionGetState</code> and
<code>GetId</code> are documented as returning <code>Unknown</code> and a meaningless client-side id. This
service therefore exposes only the two live notifications.
</p>
<p>
XSAPI notification handlers take no task queue. The events are therefore raised on an
XSAPI-internal thread, not on the <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref> queue. Handlers must be
thread-safe or marshal to the game thread themselves.
</p>

### <a id="GDK_Net_XboxLive_RealTimeActivityService_ConnectionStateChanged"></a> ConnectionStateChanged

Raised when the real-time activity websocket connects, starts connecting or disconnects
(<code>XblRealTimeActivityAddConnectionStateChangeHandler</code>).

```csharp
public event EventHandler<RealTimeActivityConnectionStateChangedEventArgs>? ConnectionStateChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[RealTimeActivityConnectionStateChangedEventArgs](GDK.Net.XboxLive.RealTimeActivityConnectionStateChangedEventArgs.md)\>?

#### Remarks

Delivered on an XSAPI-internal thread, not on the projection's task queue.

### <a id="GDK_Net_XboxLive_RealTimeActivityService_ResyncRequired"></a> ResyncRequired

Raised when the real-time activity service reports that locally cached subscription state
may be stale (<code>XblRealTimeActivityAddResyncHandler</code>).

```csharp
public event EventHandler? ResyncRequired
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler)?

#### Remarks

<p>
A resync notification means the websocket may have dropped or reordered subscription
messages. A title must assume any data it cached from RTA notifications may be stale and
refetch the authoritative state with the corresponding REST APIs. Ignoring this event can
leave the title silently showing stale data.
</p>
<p>
XSAPI automatically resyncs some subscriptions and invokes their normal handlers again
where possible. Multiplayer session-changed subscriptions are not fully resynced by XSAPI;
titles must refetch their sessions themselves.
</p>
<p>
Delivered on an XSAPI-internal thread, not on the projection's task queue.
</p>

