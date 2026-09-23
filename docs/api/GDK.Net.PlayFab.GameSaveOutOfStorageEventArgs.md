# <a id="GDK_Net_PlayFab_GameSaveOutOfStorageEventArgs"></a> Class GameSaveOutOfStorageEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised when the user's cloud save quota cannot hold the pending upload
(<code>PFGameSaveFilesUiOutOfStorageCallback</code>).

```csharp
public sealed class GameSaveOutOfStorageEventArgs : GameSaveUserActionEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md) ← 
[GameSaveOutOfStorageEventArgs](GDK.Net.PlayFab.GameSaveOutOfStorageEventArgs.md)

#### Inherited Members

[GameSaveUserActionEventArgs.HasResponded](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md\#GDK\_Net\_PlayFab\_GameSaveUserActionEventArgs\_HasResponded), 
[GameSaveEventArgs.IsFor\(PlayFabLocalUser\)](GDK.Net.PlayFab.GameSaveEventArgs.md\#GDK\_Net\_PlayFab\_GameSaveEventArgs\_IsFor\_GDK\_Net\_PlayFab\_PlayFabLocalUser\_), 
[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GameSaveOutOfStorageEventArgs_RequiredBytes"></a> RequiredBytes

How many additional bytes of quota the upload needs.

```csharp
public ulong RequiredBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_PlayFab_GameSaveOutOfStorageEventArgs_Respond_GDK_Net_PlayFab_GameSaveFilesUiOutOfStorageUserAction_"></a> Respond\(GameSaveFilesUiOutOfStorageUserAction\)

Dismisses the dialog with the user's choice
(<code>PFGameSaveFilesSetUiOutOfStorageResponse</code>).

```csharp
public void Respond(GameSaveFilesUiOutOfStorageUserAction action)
```

#### Parameters

`action` [GameSaveFilesUiOutOfStorageUserAction](GDK.Net.PlayFab.GameSaveFilesUiOutOfStorageUserAction.md)

