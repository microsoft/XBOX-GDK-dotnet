# <a id="GDK_Net_GameSave_GameSaveUpdate"></a> Class GameSaveUpdate

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

Stages a set of blob writes and deletes within a container for atomic commitment.
Wraps <code>XGameSaveUpdateHandle</code>.

```csharp
public sealed class GameSaveUpdate : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveUpdate](GDK.Net.GameSave.GameSaveUpdate.md)

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
Obtain an update via <xref href="GDK.Net.GameSave.GameSaveContainer.CreateUpdate(System.String)" data-throw-if-not-resolved="false"></xref>. Stage changes using
<xref href="GDK.Net.GameSave.GameSaveUpdate.Write(System.String%2cSystem.Byte%5b%5d)" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.GameSave.GameSaveUpdate.Delete(System.String)" data-throw-if-not-resolved="false"></xref>, then commit atomically with
<xref href="GDK.Net.GameSave.GameSaveUpdate.SubmitAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> (or the synchronous <xref href="GDK.Net.GameSave.GameSaveUpdate.Submit" data-throw-if-not-resolved="false"></xref>).
If any blob in the update fails, the entire update is rolled back.
</p>
<p>
<b>Data lifetime</b>: <xref href="GDK.Net.GameSave.GameSaveUpdate.Write(System.String%2cSystem.Byte%5b%5d)" data-throw-if-not-resolved="false"></xref> copies blob data into unmanaged memory owned by
this update. The unmanaged copy remains valid until the update is submitted (or disposed).
Do NOT assume the data is written to storage immediately; it is persisted only when the submit
call completes.
</p>
<p>
Disposing without submitting silently discards all staged changes.
</p>

## Methods

### <a id="GDK_Net_GameSave_GameSaveUpdate_Delete_System_String_"></a> Delete\(string\)

Stages a blob deletion (<code>XGameSaveSubmitBlobDelete</code>).

```csharp
public void Delete(string name)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

Name of the blob to delete.

### <a id="GDK_Net_GameSave_GameSaveUpdate_Dispose"></a> Dispose\(\)

Closes the update handle (<code>XGameSaveCloseUpdate</code>) and frees any unsubmitted blob-data
copies. Staged changes are discarded.

```csharp
public void Dispose()
```

### <a id="GDK_Net_GameSave_GameSaveUpdate_Submit"></a> Submit\(\)

Commits all staged changes synchronously (<code>XGameSaveSubmitUpdate</code>).

```csharp
public void Submit()
```

#### Remarks

After a successful commit the unmanaged blob-data copies are freed.

### <a id="GDK_Net_GameSave_GameSaveUpdate_SubmitAsync_System_Threading_CancellationToken_"></a> SubmitAsync\(CancellationToken\)

Commits all staged changes asynchronously
(<code>XGameSaveSubmitUpdateAsync</code> / <code>XGameSaveSubmitUpdateResult</code>).

```csharp
public Task SubmitAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

The unmanaged blob-data copies are freed after the async operation completes (whether
successfully or not), so they are valid for the entire duration of the write.

### <a id="GDK_Net_GameSave_GameSaveUpdate_Write_System_String_System_Byte___"></a> Write\(string, byte\[\]\)

Stages a blob write (<code>XGameSaveSubmitBlobWrite</code>).

```csharp
public void Write(string name, byte[] data)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

Blob name. Maximum 64 characters (GS_MAX_BLOB_NAME_SIZE minus the null terminator).

`data` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Data to write. Maximum 16 MB (GS_MAX_BLOB_SIZE).

#### Remarks

The supplied data is copied into unmanaged memory owned by this update and is held there
until <xref href="GDK.Net.GameSave.GameSaveUpdate.Submit" data-throw-if-not-resolved="false"></xref> / <xref href="GDK.Net.GameSave.GameSaveUpdate.SubmitAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> completes, or until this update is
disposed.

