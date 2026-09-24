# <a id="GDK_Net_Users_RemoteConnectShowPromptEventArgs"></a> Class RemoteConnectShowPromptEventArgs

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

The Gaming Runtime is asking the title to display a remote-connect prompt: the user should visit
<xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Url" data-throw-if-not-resolved="false"></xref> on a second device and enter <xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Code" data-throw-if-not-resolved="false"></xref> to finish signing in.

```csharp
public sealed class RemoteConnectShowPromptEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[RemoteConnectShowPromptEventArgs](GDK.Net.Users.RemoteConnectShowPromptEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The prompt stays up until either <xref href="GDK.Net.Users.UserPlatform.RemoteConnectClosePrompt" data-throw-if-not-resolved="false"></xref> is
raised for the same <xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Operation" data-throw-if-not-resolved="false"></xref>, or the title calls <xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Cancel" data-throw-if-not-resolved="false"></xref>.

## Properties

### <a id="GDK_Net_Users_RemoteConnectShowPromptEventArgs_Code"></a> Code

The short code the user types at <xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Url" data-throw-if-not-resolved="false"></xref>.

```csharp
public string Code { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Users_RemoteConnectShowPromptEventArgs_QrCode"></a> QrCode

An image of a QR code encoding <xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Url" data-throw-if-not-resolved="false"></xref>, in whatever format the runtime supplied.
Empty when the runtime did not provide one.

```csharp
public byte[] QrCode { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

#### Remarks

This is a private copy taken before the callback returned; the native buffer is only valid
for the duration of the callback, so the bytes are safe to keep.

### <a id="GDK_Net_Users_RemoteConnectShowPromptEventArgs_Url"></a> Url

The URL the user should open on their second device.

```csharp
public string Url { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Users_RemoteConnectShowPromptEventArgs_UserIdentifier"></a> UserIdentifier

The runtime's identifier for the user this prompt belongs to.

```csharp
public uint UserIdentifier { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_Users_RemoteConnectShowPromptEventArgs_Cancel"></a> Cancel\(\)

Abandons the sign-in this prompt belongs to
(<code>XUserPlatformRemoteConnectCancelPrompt</code>). Call this if the user dismisses the prompt.

```csharp
public void Cancel()
```

