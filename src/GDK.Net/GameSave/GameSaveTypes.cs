using System;

namespace GDK.Net.GameSave;

/// <summary>
/// Metadata for a game-save container returned by enumeration or a targeted query.
/// </summary>
/// <remarks>
/// Containers are logical groups of blobs that are written and read atomically.
/// A provider has a default quota of 256 MB shared across all its containers.
/// </remarks>
public sealed class GameSaveContainerInfo
{
    internal GameSaveContainerInfo(
        string name,
        string displayName,
        uint blobCount,
        long totalSize,
        DateTimeOffset lastModified,
        bool needsSync)
    {
        Name = name;
        DisplayName = displayName;
        BlobCount = blobCount;
        TotalSize = totalSize;
        LastModified = lastModified;
        NeedsSync = needsSync;
    }

    /// <summary>Unique container name within the provider.</summary>
    public string Name { get; }

    /// <summary>Human-readable display name for the container.</summary>
    public string DisplayName { get; }

    /// <summary>Number of blobs currently stored in the container.</summary>
    public uint BlobCount { get; }

    /// <summary>Total size of all blobs in the container, in bytes.</summary>
    public long TotalSize { get; }

    /// <summary>When the container was last modified (UTC).</summary>
    public DateTimeOffset LastModified { get; }

    /// <summary>
    /// <see langword="true"/> when the container has not yet synced with the cloud.
    /// Any operation on an unsynced container may trigger a network call when using SyncOnDemand.
    /// </summary>
    public bool NeedsSync { get; }

    /// <inheritdoc/>
    public override string ToString() =>
        $"GameSaveContainerInfo(Name={Name}, Blobs={BlobCount}, Size={TotalSize}, NeedsSync={NeedsSync})";
}

/// <summary>
/// Metadata for a blob within a container returned by enumeration.
/// </summary>
public sealed class GameSaveBlobInfo
{
    internal GameSaveBlobInfo(string name, uint size)
    {
        Name = name;
        Size = size;
    }

    /// <summary>Unique blob name within its container. Maximum 64 characters (GS_MAX_BLOB_NAME_SIZE).</summary>
    public string Name { get; }

    /// <summary>Size of the saved data in bytes. Maximum 16 MB (GS_MAX_BLOB_SIZE).</summary>
    public uint Size { get; }

    /// <inheritdoc/>
    public override string ToString() => $"GameSaveBlobInfo(Name={Name}, Size={Size})";
}

/// <summary>
/// A blob read from a container: the name, expected size, and raw data bytes.
/// </summary>
/// <remarks>
/// <see cref="Data"/> is a deep copy made before the native buffer was released; it is safe to
/// hold and inspect after the read operation completes.
/// </remarks>
public sealed class GameSaveBlob
{
    internal GameSaveBlob(string name, uint size, byte[] data)
    {
        Name = name;
        Size = size;
        Data = data;
    }

    /// <summary>Blob name.</summary>
    public string Name { get; }

    /// <summary>Declared size in bytes (matches <c>Data.Length</c>).</summary>
    public uint Size { get; }

    /// <summary>Raw blob data.</summary>
    public byte[] Data { get; }

    /// <inheritdoc/>
    public override string ToString() => $"GameSaveBlob(Name={Name}, Size={Size})";
}

/// <summary>
/// Current cloud synchronisation state of the game-save provider. Mirrors <c>XGameSaveSyncState</c>.
/// </summary>
public enum GameSaveSyncState : uint
{
    /// <summary>Synchronisation has not started.</summary>
    NotStarted = 0,

    /// <summary>Preparing the download phase.</summary>
    PreparingForDownload = 1,

    /// <summary>Actively downloading cloud data.</summary>
    Downloading = 2,

    /// <summary>Preparing the upload phase.</summary>
    PreparingForUpload = 3,

    /// <summary>Actively uploading local data to the cloud.</summary>
    Uploading = 4,

    /// <summary>Synchronisation completed successfully.</summary>
    SyncComplete = 5,
}
