# <a id="GDK_Net_PlayFab_GameSaveEventArgs"></a> Class GameSaveEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Base class for the PlayFab game save notifications, which all identify the local user the
notification is about.

```csharp
public abstract class GameSaveEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md)

#### Derived

[GameSaveActiveDeviceChangedEventArgs](GDK.Net.PlayFab.GameSaveActiveDeviceChangedEventArgs.md), 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The native callbacks hand back a borrowed <code>PFLocalUserHandle</code>. Rather than surface a
handle, or duplicate one the title would have to dispose, these arguments only let the title
ask which of its own <xref href="GDK.Net.PlayFab.PlayFabLocalUser" data-throw-if-not-resolved="false"></xref> instances the notification belongs to.

## Methods

### <a id="GDK_Net_PlayFab_GameSaveEventArgs_IsFor_GDK_Net_PlayFab_PlayFabLocalUser_"></a> IsFor\(PlayFabLocalUser\)

Whether this notification is about <code class="paramref">user</code>
(<code>PFLocalUserHandleCompare</code>).

```csharp
public bool IsFor(PlayFabLocalUser user)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

