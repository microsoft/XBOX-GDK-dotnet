# <a id="GDK_Net_Users_UserPlatform"></a> Class UserPlatform

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

The <code>XUserPlatform</code> family: lets the title draw the Gaming Runtime's sign-in prompts itself
instead of using the system UI — the remote-connect prompt ("open this URL on another device")
and the SPOP prompt ("this account is already signed in somewhere else").

```csharp
public static class UserPlatform
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserPlatform](GDK.Net.Users.UserPlatform.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
This type is static because the underlying <code>XUserPlatform*</code> APIs install <b>process-global</b>
handler tables rather than returning a per-registration token like every other event source in
this projection. There is consequently no way to uninstall them: the native handlers are set on
the first subscription and stay for the lifetime of the process. Removing the last managed
subscriber simply means the thunk has nothing to call.
</p>
<p>
A title that opts in takes on a contract: it <b>must</b> resolve every prompt it is shown.
Ignoring a <xref href="GDK.Net.Users.UserPlatform.SpopPrompt" data-throw-if-not-resolved="false"></xref> event leaves the sign-in hanging forever, because
the runtime waits for <xref href="GDK.Net.Users.SpopPromptEventArgs.Complete(GDK.Net.Users.SpopOperationResult)" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
The events are named for the native handlers they replace:
<xref href="GDK.Net.Users.UserPlatform.RemoteConnectShowPrompt" data-throw-if-not-resolved="false"></xref> for <code>XUserPlatformRemoteConnectShowPromptEventHandler</code>,
<xref href="GDK.Net.Users.UserPlatform.RemoteConnectClosePrompt" data-throw-if-not-resolved="false"></xref> for its <code>ClosePrompt</code> sibling, and
<xref href="GDK.Net.Users.UserPlatform.SpopPrompt" data-throw-if-not-resolved="false"></xref> for <code>XUserPlatformSpopPromptEventHandler</code>. They keep the header's
imperative wording rather than the past tense a .NET event usually takes, because that wording is
the contract: the title is being told to show or close a prompt, not notified that something
happened.
</p>

### <a id="GDK_Net_Users_UserPlatform_RemoteConnectClosePrompt"></a> RemoteConnectClosePrompt

Raised when the runtime is finished with a remote-connect prompt and the title should take it
down. Use <xref href="GDK.Net.Users.RemoteConnectClosePromptEventArgs.Matches(GDK.Net.Users.RemoteConnectShowPromptEventArgs)" data-throw-if-not-resolved="false"></xref> to pair it with the event
that opened the prompt.

```csharp
public static event EventHandler<RemoteConnectClosePromptEventArgs>? RemoteConnectClosePrompt
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[RemoteConnectClosePromptEventArgs](GDK.Net.Users.RemoteConnectClosePromptEventArgs.md)\>?

### <a id="GDK_Net_Users_UserPlatform_RemoteConnectShowPrompt"></a> RemoteConnectShowPrompt

Raised when the runtime wants the title to put up a remote-connect prompt.

```csharp
public static event EventHandler<RemoteConnectShowPromptEventArgs>? RemoteConnectShowPrompt
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[RemoteConnectShowPromptEventArgs](GDK.Net.Users.RemoteConnectShowPromptEventArgs.md)\>?

#### Remarks

Installing the native handler table also installs the one behind
<xref href="GDK.Net.Users.UserPlatform.RemoteConnectClosePrompt" data-throw-if-not-resolved="false"></xref>; the two are a single native registration and cannot
be subscribed independently at the native layer.

### <a id="GDK_Net_Users_UserPlatform_SpopPrompt"></a> SpopPrompt

Raised when the runtime wants the title to ask the user how to resolve an account that is
already signed in on another device.

```csharp
public static event EventHandler<SpopPromptEventArgs>? SpopPrompt
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[SpopPromptEventArgs](GDK.Net.Users.SpopPromptEventArgs.md)\>?

#### Remarks

The handler must call <xref href="GDK.Net.Users.SpopPromptEventArgs.Complete(GDK.Net.Users.SpopOperationResult)" data-throw-if-not-resolved="false"></xref> exactly once, on every path.

