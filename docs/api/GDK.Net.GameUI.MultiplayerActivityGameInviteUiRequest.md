# <a id="GDK_Net_GameUI_MultiplayerActivityGameInviteUiRequest"></a> Class MultiplayerActivityGameInviteUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to show the Multiplayer Activity invite UI
(<code>XGameUiShowMultiplayerActivityGameInviteUiCallback</code>).

```csharp
public sealed class MultiplayerActivityGameInviteUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[MultiplayerActivityGameInviteUiRequest](GDK.Net.GameUI.MultiplayerActivityGameInviteUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_MultiplayerActivityGameInviteUiRequest_RequestingUserHandle"></a> RequestingUserHandle

The <code>XUserHandle</code> of the inviting user, as supplied by the runtime.

```csharp
public nint RequestingUserHandle { get; }
```

#### Property Value

 [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

## Methods

### <a id="GDK_Net_GameUI_MultiplayerActivityGameInviteUiRequest_Respond"></a> Respond\(\)

Reports that the title has finished handling the invite UI
(<code>XGameUiSetMultiplayerActivityGameInviteUiResponse</code>).

```csharp
public void Respond()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

