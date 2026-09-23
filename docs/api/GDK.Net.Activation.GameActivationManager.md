# <a id="GDK_Net_Activation_GameActivationManager"></a> Class GameActivationManager

Namespace: [GDK.Net.Activation](GDK.Net.Activation.md)  
Assembly: GDK.Net.dll  

Protocol and game-invite activation notifications. Reached through
<xref href="GDK.Net.GameRuntime.Activation" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class GameActivationManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameActivationManager](GDK.Net.Activation.GameActivationManager.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Each native registration is created lazily on the first subscription to the corresponding event
and released on <xref href="GDK.Net.Activation.GameActivationManager.Dispose" data-throw-if-not-resolved="false"></xref> with <code>wait: true</code>, so no callback can be in flight
once the manager is gone.
</p>
<p>
<code>XGameActivation.h</code> publishes <code>XGameActivationRegisterForEvent</code> as the single unified
entry point; it reports protocol launches, file launches and both pending and accepted invites,
discriminated by <xref href="GDK.Net.Activation.GameActivationEventArgs.Kind" data-throw-if-not-resolved="false"></xref>. The older per-kind
<code>XGameInvite</code> and <code>XGameProtocol</code> registrations are deprecated in the GDK headers and
are not projected.
</p>

## Methods

### <a id="GDK_Net_Activation_GameActivationManager_AcceptPendingInvite_System_String_"></a> AcceptPendingInvite\(string\)

Consumes a pending invite reported by <xref href="GDK.Net.Activation.GameActivationManager.Activated" data-throw-if-not-resolved="false"></xref> with
<xref href="GDK.Net.Activation.GameActivationType.PendingGameInvite" data-throw-if-not-resolved="false"></xref>, so it is not replayed again
(<code>XGameActivationAcceptPendingInvite</code>).

```csharp
public void AcceptPendingInvite(string inviteUri)
```

#### Parameters

`inviteUri` [string](https://learn.microsoft.com/dotnet/api/system.string)

The URI carried by the activation event.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">inviteUri</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">inviteUri</code> is empty.

### <a id="GDK_Net_Activation_GameActivationManager_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Activation_GameActivationManager_Activated"></a> Activated

Raised for every kind of activation through the unified
<code>XGameActivationRegisterForEvent</code> registration: protocol launches, file launches, and
both pending and accepted invites. <xref href="GDK.Net.Activation.GameActivationEventArgs.Kind" data-throw-if-not-resolved="false"></xref> discriminates.

```csharp
public event EventHandler<GameActivationEventArgs>? Activated
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameActivationEventArgs](GDK.Net.Activation.GameActivationEventArgs.md)\>?

#### Remarks

<p>
A <xref href="GDK.Net.Activation.GameActivationType.PendingGameInvite" data-throw-if-not-resolved="false"></xref> activation is not consumed until the title
calls <xref href="GDK.Net.Activation.GameActivationManager.AcceptPendingInvite(System.String)" data-throw-if-not-resolved="false"></xref> with the URI from the event.
</p>

