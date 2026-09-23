# <a id="GDK_Net_Users_RemoteConnectClosePromptEventArgs"></a> Class RemoteConnectClosePromptEventArgs

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

The Gaming Runtime has finished with a remote-connect prompt and the title should take it down.

```csharp
public sealed class RemoteConnectClosePromptEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[RemoteConnectClosePromptEventArgs](GDK.Net.Users.RemoteConnectClosePromptEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Users_RemoteConnectClosePromptEventArgs_UserIdentifier"></a> UserIdentifier

The runtime's identifier for the user this prompt belonged to.

```csharp
public uint UserIdentifier { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_Users_RemoteConnectClosePromptEventArgs_Matches_GDK_Net_Users_RemoteConnectShowPromptEventArgs_"></a> Matches\(RemoteConnectShowPromptEventArgs\)

Whether this notification closes the prompt opened by <code class="paramref">request</code>.

```csharp
public bool Matches(RemoteConnectShowPromptEventArgs request)
```

#### Parameters

`request` [RemoteConnectShowPromptEventArgs](GDK.Net.Users.RemoteConnectShowPromptEventArgs.md)

The event args from the matching show notification.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">request</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

