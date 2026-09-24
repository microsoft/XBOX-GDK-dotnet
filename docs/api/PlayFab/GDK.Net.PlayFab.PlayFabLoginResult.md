# <a id="GDK_Net_PlayFab_PlayFabLoginResult"></a> Class PlayFabLoginResult

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The outcome of a PlayFab client login: the authenticated entity plus the login payload.

```csharp
public sealed class PlayFabLoginResult : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)

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

The native APIs return these as two out-parameters of a single <code>GetResult</code> call. They are
paired here so a caller receives one value and cannot lose ownership of the entity handle.

## Properties

### <a id="GDK_Net_PlayFab_PlayFabLoginResult_Entity"></a> Entity

The authenticated entity. Owned by this instance until <xref href="GDK.Net.PlayFab.PlayFabLoginResult.Detach" data-throw-if-not-resolved="false"></xref> is called.

```csharp
public PlayFabEntity Entity { get; }
```

#### Property Value

 [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

### <a id="GDK_Net_PlayFab_PlayFabLoginResult_Result"></a> Result

The login payload PlayFab returned (account info, treatment assignments, ...).

```csharp
public AuthenticationLoginResult Result { get; }
```

#### Property Value

 [AuthenticationLoginResult](GDK.Net.PlayFab.AuthenticationLoginResult.md)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabLoginResult_Detach"></a> Detach\(\)

Transfers ownership of <xref href="GDK.Net.PlayFab.PlayFabLoginResult.Entity" data-throw-if-not-resolved="false"></xref> to the caller, so disposing this instance no
longer closes it.

```csharp
public PlayFabEntity Detach()
```

#### Returns

 [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

### <a id="GDK_Net_PlayFab_PlayFabLoginResult_Dispose"></a> Dispose\(\)

Disposes the entity unless it was detached.

```csharp
public void Dispose()
```

