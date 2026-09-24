# <a id="GDK_Net_PlayFab_PlayFabEntity"></a> Class PlayFabEntity

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

An authenticated PlayFab entity: the credential every Services call is made against. Wraps
<code>PFEntityHandle</code>.

```csharp
public sealed class PlayFabEntity : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

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

<p>
Instances come from a login (<xref href="GDK.Net.PlayFab.Authentication" data-throw-if-not-resolved="false"></xref>) or from
<xref href="GDK.Net.PlayFab.PlayFabLocalUser.LoginAsync(System.Boolean%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>; the projection never hands out the raw handle. The
handle is owned by a <xref href="System.Runtime.InteropServices.SafeHandle" data-throw-if-not-resolved="false"></xref>, so a missed
<xref href="GDK.Net.PlayFab.PlayFabEntity.Dispose" data-throw-if-not-resolved="false"></xref> still releases it at finalization.
</p>
<p>
The entity token is refreshed by the PlayFab library in the background; use
<xref href="GDK.Net.PlayFab.PlayFabEntity.GetEntityTokenAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> to read the current token.
</p>

## Properties

### <a id="GDK_Net_PlayFab_PlayFabEntity_ApiEndpoint"></a> ApiEndpoint

The PlayFab API endpoint this entity authenticated against (<code>PFEntityGetAPIEndpoint</code>).

```csharp
public string ApiEndpoint { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_PlayFabEntity_IsTitlePlayer"></a> IsTitlePlayer

Whether this entity is a title player (<code>PFEntityIsTitlePlayer</code>).

```csharp
public bool IsTitlePlayer { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_PlayFabEntity_Key"></a> Key

The entity's id and type (<code>PFEntityGetEntityKey</code>).

```csharp
public EntityKey Key { get; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)

### <a id="GDK_Net_PlayFab_PlayFabEntity_SecretKey"></a> SecretKey

The developer secret key this entity was created with (<code>PFEntityGetSecretKey</code>), or an
empty string for a client entity.

```csharp
public string SecretKey { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Only server entities carry a secret key; it must never be shipped in a client.

### <a id="GDK_Net_PlayFab_PlayFabEntity_TitleId"></a> TitleId

The PlayFab title id this entity authenticated against (<code>PFEntityGetTitleId</code>).

```csharp
public string TitleId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabEntity_Dispose"></a> Dispose\(\)

Releases the native handle.

```csharp
public void Dispose()
```

### <a id="GDK_Net_PlayFab_PlayFabEntity_Duplicate"></a> Duplicate\(\)

Returns an independent instance backed by its own native handle
(<code>PFEntityDuplicateHandle</code>).

```csharp
public PlayFabEntity Duplicate()
```

#### Returns

 [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

### <a id="GDK_Net_PlayFab_PlayFabEntity_GetEntityTokenAsync_System_Threading_CancellationToken_"></a> GetEntityTokenAsync\(CancellationToken\)

Reads the entity's current token, refreshing it if required
(<code>PFEntityGetEntityTokenAsync</code>).

```csharp
public Task<EntityToken> GetEntityTokenAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[EntityToken](GDK.Net.PlayFab.EntityToken.md)\>

