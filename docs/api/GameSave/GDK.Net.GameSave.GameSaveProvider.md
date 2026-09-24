# <a id="GDK_Net_GameSave_GameSaveProvider"></a> Class GameSaveProvider

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

Binds a signed-in user and a service configuration ID to their game-save storage, exposing
container management, quota queries and cloud-synced persistence.

```csharp
public sealed class GameSaveProvider : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveProvider](GDK.Net.GameSave.GameSaveProvider.md)

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
A provider is the root object of the XGameSave family. Obtain one via
<xref href="GDK.Net.GameSave.GameSaveProvider.InitializeAsync(GDK.Net.Users.User%2cSystem.String%2cSystem.Boolean%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> (or the synchronous <xref href="GDK.Net.GameSave.GameSaveProvider.Initialize(GDK.Net.Users.User%2cSystem.String%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref>). Both require a
signed-in <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> and the service configuration ID (SCID) from
<code>MicrosoftGame.config</code>.
</p>
<p>
Lifecycle ordering: the provider must outlive all containers created from it, which must
outlive all updates created from those containers. Dispose in reverse-creation order.
</p>
<p>
The default provider quota is 256 MB. Each blob may be at most 16 MB.
</p>

## Methods

### <a id="GDK_Net_GameSave_GameSaveProvider_CreateContainer_System_String_"></a> CreateContainer\(string\)

Opens or creates a container with <code class="paramref">name</code> (<code>XGameSaveCreateContainer</code>).

```csharp
public GameSaveContainer CreateContainer(string name)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

Container name. Maximum 255 characters (GS_MAX_CONTAINER_NAME_SIZE).

#### Returns

 [GameSaveContainer](GDK.Net.GameSave.GameSaveContainer.md)

#### Remarks

Dispose the returned <xref href="GDK.Net.GameSave.GameSaveContainer" data-throw-if-not-resolved="false"></xref> before disposing this provider.

### <a id="GDK_Net_GameSave_GameSaveProvider_DeleteContainer_System_String_"></a> DeleteContainer\(string\)

Deletes a container and all its blobs synchronously (<code>XGameSaveDeleteContainer</code>).

```csharp
public void DeleteContainer(string name)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_GameSave_GameSaveProvider_DeleteContainerAsync_System_String_System_Threading_CancellationToken_"></a> DeleteContainerAsync\(string, CancellationToken\)

Deletes a container and all its blobs asynchronously
(<code>XGameSaveDeleteContainerAsync</code> / <code>XGameSaveDeleteContainerResult</code>).

```csharp
public Task DeleteContainerAsync(string name, CancellationToken cancellationToken = default)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

The name of the container to delete.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_GameSave_GameSaveProvider_Dispose"></a> Dispose\(\)

Closes the provider handle (<code>XGameSaveCloseProvider</code>). All containers and updates
derived from this provider must be disposed first.

```csharp
public void Dispose()
```

### <a id="GDK_Net_GameSave_GameSaveProvider_EnumerateContainers"></a> EnumerateContainers\(\)

Enumerates all containers in the provider (<code>XGameSaveEnumerateContainerInfo</code>).

```csharp
public IReadOnlyList<GameSaveContainerInfo> EnumerateContainers()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[GameSaveContainerInfo](GDK.Net.GameSave.GameSaveContainerInfo.md)\>

A snapshot list; deep-copied from native memory.

### <a id="GDK_Net_GameSave_GameSaveProvider_EnumerateContainersByPrefix_System_String_"></a> EnumerateContainersByPrefix\(string\)

Enumerates containers whose names begin with <code class="paramref">namePrefix</code>
(<code>XGameSaveEnumerateContainerInfoByName</code>).

```csharp
public IReadOnlyList<GameSaveContainerInfo> EnumerateContainersByPrefix(string namePrefix)
```

#### Parameters

`namePrefix` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[GameSaveContainerInfo](GDK.Net.GameSave.GameSaveContainerInfo.md)\>

### <a id="GDK_Net_GameSave_GameSaveProvider_GetContainerInfo_System_String_"></a> GetContainerInfo\(string\)

Retrieves the info for a single named container (<code>XGameSaveGetContainerInfo</code>).

```csharp
public GameSaveContainerInfo GetContainerInfo(string name)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [GameSaveContainerInfo](GDK.Net.GameSave.GameSaveContainerInfo.md)

#### Exceptions

 [GameRuntimeException](../Core/GDK.Net.GameRuntimeException.md)

Thrown when the container does not exist.

### <a id="GDK_Net_GameSave_GameSaveProvider_GetRemainingQuota"></a> GetRemainingQuota\(\)

Returns the remaining storage quota in bytes (<code>XGameSaveGetRemainingQuota</code>).

```csharp
public long GetRemainingQuota()
```

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_GameSave_GameSaveProvider_GetRemainingQuotaAsync_System_Threading_CancellationToken_"></a> GetRemainingQuotaAsync\(CancellationToken\)

Returns the remaining storage quota in bytes asynchronously
(<code>XGameSaveGetRemainingQuotaAsync</code> / <code>XGameSaveGetRemainingQuotaResult</code>).

```csharp
public Task<long> GetRemainingQuotaAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[long](https://learn.microsoft.com/dotnet/api/system.int64)\>

### <a id="GDK_Net_GameSave_GameSaveProvider_Initialize_GDK_Net_Users_User_System_String_System_Boolean_"></a> Initialize\(User, string, bool\)

Initialises a game-save provider synchronously (<code>XGameSaveInitializeProvider</code>).

```csharp
public static GameSaveProvider Initialize(User user, string serviceConfigurationId, bool syncOnDemand = false)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

The signed-in user whose saves are accessed.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The SCID from <code>MicrosoftGame.config</code>. Must not exceed 64 characters.

`syncOnDemand` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only container metadata is synced at init time; container data
is synced lazily on first access. When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> all data is synced upfront.

#### Returns

 [GameSaveProvider](GDK.Net.GameSave.GameSaveProvider.md)

#### Remarks

This call may block while performing an initial cloud sync when
<code class="paramref">syncOnDemand</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>. Prefer
<xref href="GDK.Net.GameSave.GameSaveProvider.InitializeAsync(GDK.Net.Users.User%2cSystem.String%2cSystem.Boolean%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> on the game thread.

### <a id="GDK_Net_GameSave_GameSaveProvider_InitializeAsync_GDK_Net_Users_User_System_String_System_Boolean_System_Threading_CancellationToken_"></a> InitializeAsync\(User, string, bool, CancellationToken\)

Initialises a game-save provider asynchronously (<code>XGameSaveInitializeProviderAsync</code> /
<code>XGameSaveInitializeProviderResult</code>).

```csharp
public static Task<GameSaveProvider> InitializeAsync(User user, string serviceConfigurationId, bool syncOnDemand = false, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

The signed-in user whose saves are accessed.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The SCID from <code>MicrosoftGame.config</code>. Must not exceed 64 characters.

`syncOnDemand` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only container metadata is synced at init time.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GameSaveProvider](GDK.Net.GameSave.GameSaveProvider.md)\>

