# <a id="GDK_Net_PlayFab_GameSaveUserActionEventArgs"></a> Class GameSaveUserActionEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Base class for the notifications that ask the title to show a game save dialog and report the
user's choice back with <code>Respond</code>.

```csharp
public abstract class GameSaveUserActionEventArgs : GameSaveEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md)

#### Derived

[GameSaveActiveDeviceContentionEventArgs](GDK.Net.PlayFab.GameSaveActiveDeviceContentionEventArgs.md), 
[GameSaveConflictEventArgs](GDK.Net.PlayFab.GameSaveConflictEventArgs.md), 
[GameSaveOutOfStorageEventArgs](GDK.Net.PlayFab.GameSaveOutOfStorageEventArgs.md), 
[GameSaveSyncFailedEventArgs](GDK.Net.PlayFab.GameSaveSyncFailedEventArgs.md), 
[GameSaveSyncProgressEventArgs](GDK.Net.PlayFab.GameSaveSyncProgressEventArgs.md)

#### Inherited Members

[GameSaveEventArgs.IsFor\(PlayFabLocalUser\)](GDK.Net.PlayFab.GameSaveEventArgs.md\#GDK\_Net\_PlayFab\_GameSaveEventArgs\_IsFor\_GDK\_Net\_PlayFab\_PlayFabLocalUser\_), 
[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The sync stays blocked until a response is given, so every handler must respond exactly once,
even if only with the cancel action.

## Properties

### <a id="GDK_Net_PlayFab_GameSaveUserActionEventArgs_HasResponded"></a> HasResponded

Whether a response has already been given.

```csharp
public bool HasResponded { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

