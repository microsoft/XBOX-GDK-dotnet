using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.GameSave;

/// <summary>
/// A game-save container: a named, atomic group of blobs that can be read and written together.
/// Wraps <c>XGameSaveContainerHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// Obtain a container via <see cref="GameSaveProvider.CreateContainer"/>. The container must be
/// disposed before the provider that created it.
/// </para>
/// <para>
/// Blob writes are always atomic: create an <see cref="GameSaveUpdate"/> via
/// <see cref="CreateUpdate"/>, stage changes via <see cref="GameSaveUpdate.Write"/> and
/// <see cref="GameSaveUpdate.Delete"/>, then commit with
/// <see cref="GameSaveUpdate.SubmitAsync"/>.
/// </para>
/// </remarks>
public sealed unsafe class GameSaveContainer : IDisposable
{
    private readonly GameSaveContainerHandle _handle;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    internal GameSaveContainer(GameSaveContainerHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Blob enumeration
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Enumerates all blobs in the container (<c>XGameSaveEnumerateBlobInfo</c>).
    /// </summary>
    /// <returns>A snapshot list; deep-copied from native memory.</returns>
    public IReadOnlyList<GameSaveBlobInfo> EnumerateBlobs()
    {
        ThrowIfDisposed();
        return EnumerateBlobsCore(prefixBytes: null);
    }

    /// <summary>
    /// Enumerates blobs whose names begin with <paramref name="namePrefix"/>
    /// (<c>XGameSaveEnumerateBlobInfoByName</c>).
    /// </summary>
    public IReadOnlyList<GameSaveBlobInfo> EnumerateBlobsByPrefix(string namePrefix)
    {
        ThrowIfDisposed();
        if (namePrefix is null) throw new ArgumentNullException(nameof(namePrefix));
        return EnumerateBlobsCore(GameSaveProvider.EncodeUtf8(namePrefix));
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Blob read
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Reads blobs by name synchronously (<c>XGameSaveReadBlobData</c>).
    /// </summary>
    /// <remarks>
    /// This call may trigger a network sync if the container has not yet been synced. Prefer
    /// <see cref="ReadBlobsAsync"/> on the game thread.
    /// </remarks>
    /// <param name="names">Names of the blobs to read. Pass <see langword="null"/> or an empty
    /// list to read all blobs in the container.</param>
    public IReadOnlyList<GameSaveBlob> ReadBlobs(IReadOnlyList<string>? names = null)
    {
        ThrowIfDisposed();

        // Enumerate first to compute the output buffer size.
        IReadOnlyList<GameSaveBlobInfo> blobInfos = names is null || names.Count == 0
            ? EnumerateBlobs()
            : FilteredBlobInfos(names);

        if (blobInfos.Count == 0)
        {
            return Array.Empty<GameSaveBlob>();
        }

        nuint bufferSize = ComputeBufferSize(blobInfos);
        IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
        try
        {
            return ReadBlobsWithBuffer(blobInfos, buffer, bufferSize);
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    /// <summary>
    /// Reads blobs by name asynchronously
    /// (<c>XGameSaveReadBlobDataAsync</c> / <c>XGameSaveReadBlobDataResult</c>).
    /// </summary>
    /// <param name="names">Names of the blobs to read. Pass <see langword="null"/> or an empty
    /// list to read all blobs in the container.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<IReadOnlyList<GameSaveBlob>> ReadBlobsAsync(
        IReadOnlyList<string>? names = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        // Enumerate synchronously (fast, local metadata) to compute the result buffer size.
        IReadOnlyList<GameSaveBlobInfo> blobInfos = names is null || names.Count == 0
            ? EnumerateBlobs()
            : FilteredBlobInfos(names);

        if (blobInfos.Count == 0)
        {
            return Task.FromResult<IReadOnlyList<GameSaveBlob>>(Array.Empty<GameSaveBlob>());
        }

        nuint bufferSize = ComputeBufferSize(blobInfos);

        // The result buffer must survive until XGameSaveReadBlobDataResult returns.
        IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);

        IntPtr containerHandle = _handle.DangerousGetHandle();

        // Prepare name pointer array in unmanaged memory (valid only during the async start call).
        // XGameSaveReadBlobDataAsync only reads blobNames during the call (_In_ annotation).
        byte[][] nameByteArrays = BuildNameByteArrays(blobInfos);
        uint count = (uint)nameByteArrays.Length;

        return AsyncOperation<IReadOnlyList<GameSaveBlob>>.RunAsync(
            _queue.RawHandle(),
            asyncBlock =>
            {
                IntPtr nameStorage = PackNamePointers(nameByteArrays);
                try
                {
                    return Native.XGameSaveReadBlobDataAsync(
                        containerHandle,
                        (byte**)nameStorage,
                        count,
                        (XAsyncBlock*)asyncBlock);
                }
                finally
                {
                    Marshal.FreeHGlobal(nameStorage);
                }
            },
            (IntPtr asyncBlock, out IReadOnlyList<GameSaveBlob> value) =>
            {
                value = Array.Empty<GameSaveBlob>();
                try
                {
                    uint actualCount;
                    int hr = Native.XGameSaveReadBlobDataResult(
                        (XAsyncBlock*)asyncBlock,
                        bufferSize,
                        (NativeGameSaveBlob*)buffer,
                        &actualCount);
                    if (HResult.Failed(hr)) return hr;
                    value = ParseBlobResults((NativeGameSaveBlob*)buffer, actualCount);
                    return HResult.SOk;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            },
            cancellationToken);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Update
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Creates an update context for staging blob writes and deletes atomically
    /// (<c>XGameSaveCreateUpdate</c>).
    /// </summary>
    /// <remarks>
    /// Dispose the returned <see cref="GameSaveUpdate"/> before disposing this container.
    /// An unsubmitted update discards all staged changes when disposed.
    /// </remarks>
    /// <param name="displayName">
    /// Human-readable display name for the container shown in the system UI.
    /// Maximum 127 characters (GS_MAX_CONTAINER_DISPLAY_NAME_SIZE).
    /// </param>
    public GameSaveUpdate CreateUpdate(string displayName)
    {
        ThrowIfDisposed();
        if (displayName is null) throw new ArgumentNullException(nameof(displayName));

        byte[] displayNameBytes = GameSaveProvider.EncodeUtf8(displayName);
        IntPtr raw;
        int hr;
        fixed (byte* displayNamePtr = displayNameBytes)
        {
            hr = Native.XGameSaveCreateUpdate(_handle.DangerousGetHandle(), displayNamePtr, &raw);
        }

        Hr.ThrowIfFailed(hr);
        return new GameSaveUpdate(new GameSaveUpdateHandle(raw), _queue);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // IDisposable
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Closes the container handle (<c>XGameSaveCloseContainer</c>). All updates derived from this
    /// container must be disposed first.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Helpers
    // ──────────────────────────────────────────────────────────────────────────

    private IReadOnlyList<GameSaveBlob> ReadBlobsWithBuffer(
        IReadOnlyList<GameSaveBlobInfo> blobInfos,
        IntPtr buffer,
        nuint bufferSize)
    {
        byte[][] nameByteArrays = BuildNameByteArrays(blobInfos);
        uint count = (uint)nameByteArrays.Length;

        IntPtr nameStorage = PackNamePointers(nameByteArrays);
        try
        {
            int hr = Native.XGameSaveReadBlobData(
                _handle.DangerousGetHandle(),
                (byte**)nameStorage,
                &count,
                bufferSize,
                (NativeGameSaveBlob*)buffer);
            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            Marshal.FreeHGlobal(nameStorage);
        }

        return ParseBlobResults((NativeGameSaveBlob*)buffer, count);
    }

    private IReadOnlyList<GameSaveBlobInfo> FilteredBlobInfos(IReadOnlyList<string> names)
    {
        // Enumerate all blobs and filter to the requested names, preserving the caller's order.
        var all = EnumerateBlobs();
        var byName = new Dictionary<string, GameSaveBlobInfo>(all.Count, StringComparer.Ordinal);
        foreach (var info in all)
        {
            byName[info.Name] = info;
        }

        var result = new List<GameSaveBlobInfo>(names.Count);
        foreach (var name in names)
        {
            if (byName.TryGetValue(name, out var info))
            {
                result.Add(info);
            }
        }

        return result;
    }

    private IReadOnlyList<GameSaveBlobInfo> EnumerateBlobsCore(byte[]? prefixBytes)
    {
        var list = new List<GameSaveBlobInfo>();
        var gcHandle = GCHandle.Alloc(list);
        try
        {
            IntPtr containerHandle = _handle.DangerousGetHandle();
            IntPtr context = GCHandle.ToIntPtr(gcHandle);
            IntPtr callback = GameSaveCallbacks.BlobInfoCallback;

            int hr;
            if (prefixBytes is null)
            {
                hr = Native.XGameSaveEnumerateBlobInfo(containerHandle, context, callback);
            }
            else
            {
                fixed (byte* prefixPtr = prefixBytes)
                {
                    hr = Native.XGameSaveEnumerateBlobInfoByName(
                        containerHandle, prefixPtr, context, callback);
                }
            }

            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            gcHandle.Free();
        }

        return list;
    }

    /// <summary>
    /// Computes the minimum output buffer size for XGameSaveReadBlobData / Result.
    /// Layout: [N × sizeof(NativeGameSaveBlob)] + [name strings] + [data bytes].
    /// GS_MAX_BLOB_NAME_SIZE (65) is used for each name to avoid a second enumerate call.
    /// </summary>
    private static nuint ComputeBufferSize(IReadOnlyList<GameSaveBlobInfo> blobInfos)
    {
        const int MaxBlobNameSize = 65; // GS_MAX_BLOB_NAME_SIZE from XGameSave.h
        int structSize = Marshal.SizeOf<NativeGameSaveBlob>();

        nuint total = (nuint)(blobInfos.Count * structSize);
        foreach (var info in blobInfos)
        {
            total += MaxBlobNameSize;      // null-terminated name (conservative)
            total += info.Size;            // blob data
        }

        return total;
    }

    private static byte[][] BuildNameByteArrays(IReadOnlyList<GameSaveBlobInfo> blobInfos)
    {
        var arrays = new byte[blobInfos.Count][];
        for (int i = 0; i < blobInfos.Count; i++)
        {
            arrays[i] = GameSaveProvider.EncodeUtf8(blobInfos[i].Name);
        }

        return arrays;
    }

    /// <summary>
    /// Packs null-terminated name strings and a pointer-array header into one unmanaged allocation.
    /// Layout: [count × IntPtr (pointers)] [name bytes ...].
    /// The caller must free the returned pointer with <c>Marshal.FreeHGlobal</c>.
    /// </summary>
    private static unsafe IntPtr PackNamePointers(byte[][] nameByteArrays)
    {
        int count = nameByteArrays.Length;
        int ptrArrayBytes = count * IntPtr.Size;
        int totalNameBytes = 0;
        foreach (var n in nameByteArrays) totalNameBytes += n.Length;

        IntPtr storage = Marshal.AllocHGlobal(ptrArrayBytes + totalNameBytes);

        byte** ptrs = (byte**)storage;
        byte* nameData = (byte*)(storage + ptrArrayBytes);

        for (int i = 0; i < count; i++)
        {
            ptrs[i] = nameData;
            Marshal.Copy(nameByteArrays[i], 0, (IntPtr)nameData, nameByteArrays[i].Length);
            nameData += nameByteArrays[i].Length;
        }

        return storage;
    }

    private static unsafe IReadOnlyList<GameSaveBlob> ParseBlobResults(
        NativeGameSaveBlob* blobData, uint count)
    {
        var result = new List<GameSaveBlob>((int)count);
        for (uint i = 0; i < count; i++)
        {
            ref var b = ref blobData[i];
            string name = GameSaveCallbacks.PtrToStringUtf8(b.info.name);
            uint size = b.info.size;

            byte[] data;
            if (b.data != null && size > 0)
            {
                data = new byte[(int)size];
                fixed (byte* dest = data)
                {
                    Buffer.MemoryCopy(b.data, dest, size, size);
                }
            }
            else
            {
                data = Array.Empty<byte>();
            }

            result.Add(new GameSaveBlob(name, size, data));
        }

        return result;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameSaveContainer));
        }
    }
}
