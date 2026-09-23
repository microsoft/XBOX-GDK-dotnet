# <a id="GDK_Net_GameSave_GameSaveContainer"></a> Class GameSaveContainer

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

A game-save container: a named, atomic group of blobs that can be read and written together.
Wraps <code>XGameSaveContainerHandle</code>.

```csharp
public sealed class GameSaveContainer : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveContainer](GDK.Net.GameSave.GameSaveContainer.md)

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
Obtain a container via <xref href="GDK.Net.GameSave.GameSaveProvider.CreateContainer(System.String)" data-throw-if-not-resolved="false"></xref>. The container must be
disposed before the provider that created it.
</p>
<p>
Blob writes are always atomic: create an <xref href="GDK.Net.GameSave.GameSaveUpdate" data-throw-if-not-resolved="false"></xref> via
<xref href="GDK.Net.GameSave.GameSaveContainer.CreateUpdate(System.String)" data-throw-if-not-resolved="false"></xref>, stage changes via <xref href="GDK.Net.GameSave.GameSaveUpdate.Write(System.String%2cSystem.Byte%5b%5d)" data-throw-if-not-resolved="false"></xref> and
<xref href="GDK.Net.GameSave.GameSaveUpdate.Delete(System.String)" data-throw-if-not-resolved="false"></xref>, then commit with
<xref href="GDK.Net.GameSave.GameSaveUpdate.SubmitAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.
</p>

## Methods

### <a id="GDK_Net_GameSave_GameSaveContainer_CreateUpdate_System_String_"></a> CreateUpdate\(string\)

Creates an update context for staging blob writes and deletes atomically
(<code>XGameSaveCreateUpdate</code>).

```csharp
public GameSaveUpdate CreateUpdate(string displayName)
```

#### Parameters

`displayName` [string](https://learn.microsoft.com/dotnet/api/system.string)

Human-readable display name for the container shown in the system UI.
Maximum 127 characters (GS_MAX_CONTAINER_DISPLAY_NAME_SIZE).

#### Returns

 [GameSaveUpdate](GDK.Net.GameSave.GameSaveUpdate.md)

#### Remarks

Dispose the returned <xref href="GDK.Net.GameSave.GameSaveUpdate" data-throw-if-not-resolved="false"></xref> before disposing this container.
An unsubmitted update discards all staged changes when disposed.

### <a id="GDK_Net_GameSave_GameSaveContainer_Dispose"></a> Dispose\(\)

Closes the container handle (<code>XGameSaveCloseContainer</code>). All updates derived from this
container must be disposed first.

```csharp
public void Dispose()
```

### <a id="GDK_Net_GameSave_GameSaveContainer_EnumerateBlobs"></a> EnumerateBlobs\(\)

Enumerates all blobs in the container (<code>XGameSaveEnumerateBlobInfo</code>).

```csharp
public IReadOnlyList<GameSaveBlobInfo> EnumerateBlobs()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[GameSaveBlobInfo](GDK.Net.GameSave.GameSaveBlobInfo.md)\>

A snapshot list; deep-copied from native memory.

### <a id="GDK_Net_GameSave_GameSaveContainer_EnumerateBlobsByPrefix_System_String_"></a> EnumerateBlobsByPrefix\(string\)

Enumerates blobs whose names begin with <code class="paramref">namePrefix</code>
(<code>XGameSaveEnumerateBlobInfoByName</code>).

```csharp
public IReadOnlyList<GameSaveBlobInfo> EnumerateBlobsByPrefix(string namePrefix)
```

#### Parameters

`namePrefix` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[GameSaveBlobInfo](GDK.Net.GameSave.GameSaveBlobInfo.md)\>

### <a id="GDK_Net_GameSave_GameSaveContainer_ReadBlobs_System_Collections_Generic_IReadOnlyList_System_String__"></a> ReadBlobs\(IReadOnlyList<string\>?\)

Reads blobs by name synchronously (<code>XGameSaveReadBlobData</code>).

```csharp
public IReadOnlyList<GameSaveBlob> ReadBlobs(IReadOnlyList<string>? names = null)
```

#### Parameters

`names` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

Names of the blobs to read. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or an empty
    list to read all blobs in the container.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[GameSaveBlob](GDK.Net.GameSave.GameSaveBlob.md)\>

#### Remarks

This call may trigger a network sync if the container has not yet been synced. Prefer
<xref href="GDK.Net.GameSave.GameSaveContainer.ReadBlobsAsync(System.Collections.Generic.IReadOnlyList%7bSystem.String%7d%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> on the game thread.

### <a id="GDK_Net_GameSave_GameSaveContainer_ReadBlobsAsync_System_Collections_Generic_IReadOnlyList_System_String__System_Threading_CancellationToken_"></a> ReadBlobsAsync\(IReadOnlyList<string\>?, CancellationToken\)

Reads blobs by name asynchronously
(<code>XGameSaveReadBlobDataAsync</code> / <code>XGameSaveReadBlobDataResult</code>).

```csharp
public Task<IReadOnlyList<GameSaveBlob>> ReadBlobsAsync(IReadOnlyList<string>? names = null, CancellationToken cancellationToken = default)
```

#### Parameters

`names` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

Names of the blobs to read. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or an empty
    list to read all blobs in the container.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[GameSaveBlob](GDK.Net.GameSave.GameSaveBlob.md)\>\>

