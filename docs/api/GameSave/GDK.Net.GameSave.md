# <a id="GDK_Net_GameSave"></a> Namespace GDK.Net.GameSave

### Classes

 [GameSaveBlob](GDK.Net.GameSave.GameSaveBlob.md)

A blob read from a container: the name, expected size, and raw data bytes.

 [GameSaveBlobInfo](GDK.Net.GameSave.GameSaveBlobInfo.md)

Metadata for a blob within a container returned by enumeration.

 [GameSaveContainer](GDK.Net.GameSave.GameSaveContainer.md)

A game-save container: a named, atomic group of blobs that can be read and written together.
Wraps <code>XGameSaveContainerHandle</code>.

 [GameSaveContainerInfo](GDK.Net.GameSave.GameSaveContainerInfo.md)

Metadata for a game-save container returned by enumeration or a targeted query.

 [GameSaveFiles](GDK.Net.GameSave.GameSaveFiles.md)

Simple-file access to game saves via the file system
(<code>XGameSaveFiles</code> family in <code>XGameSaveFiles.h</code>).

 [GameSaveProvider](GDK.Net.GameSave.GameSaveProvider.md)

Binds a signed-in user and a service configuration ID to their game-save storage, exposing
container management, quota queries and cloud-synced persistence.

 [GameSaveUpdate](GDK.Net.GameSave.GameSaveUpdate.md)

Stages a set of blob writes and deletes within a container for atomic commitment.
Wraps <code>XGameSaveUpdateHandle</code>.

### Enums

 [GameSaveSyncState](GDK.Net.GameSave.GameSaveSyncState.md)

Current cloud synchronisation state of the game-save provider. Mirrors <code>XGameSaveSyncState</code>.

