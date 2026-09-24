# <a id="GDK_Net_GameUI_SendGameInviteUiRequest"></a> Class SendGameInviteUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to show the "send game invite" UI (<code>XGameUiShowSendGameInviteUiCallback</code>).

```csharp
public sealed class SendGameInviteUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[SendGameInviteUiRequest](GDK.Net.GameUI.SendGameInviteUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_CustomActivationContext"></a> CustomActivationContext

Optional custom activation context carried with the invite.

```csharp
public string? CustomActivationContext { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_InvitationText"></a> InvitationText

Optional invitation text supplied by the title.

```csharp
public string? InvitationText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_RequestingUserHandle"></a> RequestingUserHandle

The <code>XUserHandle</code> of the inviting user, as supplied by the runtime.

```csharp
public nint RequestingUserHandle { get; }
```

#### Property Value

 [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_SessionConfigurationId"></a> SessionConfigurationId

The multiplayer session's service configuration id.

```csharp
public string? SessionConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_SessionId"></a> SessionId

The multiplayer session id.

```csharp
public string? SessionId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_SessionTemplateName"></a> SessionTemplateName

The multiplayer session template name.

```csharp
public string? SessionTemplateName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="GDK_Net_GameUI_SendGameInviteUiRequest_Respond"></a> Respond\(\)

Reports that the title has finished handling the invite UI
(<code>XGameUiSetSendGameInviteUiResponse</code>).

```csharp
public void Respond()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

