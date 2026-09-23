# <a id="GDK_Net_PlayFab_PlayFabGameServerLoginResult"></a> Class PlayFabGameServerLoginResult

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The outcome of authenticating a game server entity, which also reports whether the entity was
created by this call.

```csharp
public sealed class PlayFabGameServerLoginResult : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabGameServerLoginResult](GDK.Net.PlayFab.PlayFabGameServerLoginResult.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PlayFabGameServerLoginResult_Entity"></a> Entity

The authenticated entity. Owned by this instance until <xref href="GDK.Net.PlayFab.PlayFabGameServerLoginResult.Detach" data-throw-if-not-resolved="false"></xref> is called.

```csharp
public PlayFabEntity Entity { get; }
```

#### Property Value

 [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

### <a id="GDK_Net_PlayFab_PlayFabGameServerLoginResult_NewlyCreated"></a> NewlyCreated

Whether the entity was created by this call rather than looked up.

```csharp
public bool NewlyCreated { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabGameServerLoginResult_Detach"></a> Detach\(\)

Transfers ownership of <xref href="GDK.Net.PlayFab.PlayFabGameServerLoginResult.Entity" data-throw-if-not-resolved="false"></xref> to the caller.

```csharp
public PlayFabEntity Detach()
```

#### Returns

 [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

### <a id="GDK_Net_PlayFab_PlayFabGameServerLoginResult_Dispose"></a> Dispose\(\)

Disposes the entity unless it was detached.

```csharp
public void Dispose()
```

