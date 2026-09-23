using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// One page of title storage blob metadata, plus the means to fetch the next. Wraps
/// <c>XblTitleStorageBlobMetadataResultHandle</c>.
/// </summary>
/// <remarks>
/// The native handle owns the metadata array. <see cref="Items"/> is materialized at construction,
/// so metadata stays readable after disposal; only <see cref="GetNextAsync"/> requires the live
/// handle.
/// </remarks>
public sealed class TitleStorageBlobMetadataPage : IDisposable
{
    private readonly TitleStorageBlobMetadataResultHandle _handle;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    internal unsafe TitleStorageBlobMetadataPage(TitleStorageBlobMetadataResultHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;

        XblTitleStorageBlobMetadata* items;
        nuint count;
        Hr.ThrowIfFailed(NativeXbl.XblTitleStorageBlobMetadataResultGetItems(
            handle.DangerousGetHandle(),
            &items,
            &count));

        var managed = new TitleStorageBlobMetadata[(int)count];
        for (int i = 0; i < managed.Length; i++)
        {
            managed[i] = TitleStorageBlobMetadata.FromNative(items + i);
        }

        Items = new ReadOnlyCollection<TitleStorageBlobMetadata>(managed);

        byte hasNext;
        Hr.ThrowIfFailed(NativeXbl.XblTitleStorageBlobMetadataResultHasNext(
            handle.DangerousGetHandle(),
            &hasNext));
        HasNext = hasNext != 0;
    }

    /// <summary>The metadata items in this page.</summary>
    public IReadOnlyList<TitleStorageBlobMetadata> Items { get; }

    /// <summary>Whether another metadata page is available.</summary>
    public bool HasNext { get; }

    /// <summary>Fetches the next metadata page.</summary>
    /// <param name="maxItems">Maximum items to return. 0 attempts to retrieve all remaining items.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <exception cref="InvalidOperationException"><see cref="HasNext"/> is <see langword="false"/>.</exception>
    public unsafe Task<TitleStorageBlobMetadataPage> GetNextAsync(
        uint maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (!HasNext)
        {
            throw new InvalidOperationException("There are no more title storage metadata pages to fetch.");
        }

        IntPtr handle = _handle.DangerousGetHandle();
        GameTaskQueue? queue = _queue;

        return AsyncOperation<TitleStorageBlobMetadataPage>.RunAsync(
            queue.RawHandle(),
            block => NativeXbl.XblTitleStorageBlobMetadataResultGetNextAsync(
                handle,
                maxItems,
                (XAsyncBlock*)block),
            (IntPtr block, out TitleStorageBlobMetadataPage value) =>
            {
                value = null!;

                IntPtr raw;
                int hr = NativeXbl.XblTitleStorageBlobMetadataResultGetNextResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new TitleStorageBlobMetadataPage(new TitleStorageBlobMetadataResultHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Enumerates this page and every page after it, fetching each on demand.
    /// </summary>
    /// <remarks>
    /// Each fetched page is disposed once the following page has been read. Returned metadata is a
    /// managed copy and remains valid.
    /// </remarks>
    /// <param name="maxItemsPerPage">Maximum items per fetched page; 0 lets the service choose.</param>
    /// <param name="cancellationToken">Cancels a pending fetch.</param>
    public async Task<IReadOnlyList<TitleStorageBlobMetadata>> ReadAllAsync(
        uint maxItemsPerPage = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        var all = new List<TitleStorageBlobMetadata>(Items);
        TitleStorageBlobMetadataPage current = this;

        while (current.HasNext)
        {
            cancellationToken.ThrowIfCancellationRequested();

            TitleStorageBlobMetadataPage next = await current.GetNextAsync(maxItemsPerPage, cancellationToken)
                .ConfigureAwait(false);

            if (!ReferenceEquals(current, this))
            {
                current.Dispose();
            }

            all.AddRange(next.Items);
            current = next;
        }

        if (!ReferenceEquals(current, this))
        {
            current.Dispose();
        }

        return new ReadOnlyCollection<TitleStorageBlobMetadata>(all.ToArray());
    }

    /// <summary>Releases the native metadata result handle.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(TitleStorageBlobMetadataPage));
        }
    }
}

/// <summary>Xbox Live title storage quota, metadata and blob transfer operations.</summary>
/// <remarks>
/// Binary uploads and downloads may be chunked by XSAPI. Pass a preferred chunk size to influence
/// transfer granularity, or 0 to let the runtime choose. Upload buffers are pinned for the full
/// asynchronous operation; callers must still avoid mutating the array until the returned task has
/// completed.
/// </remarks>
public sealed unsafe class TitleStorageService
{
    private readonly XboxLiveContext _context;

    internal TitleStorageService(XboxLiveContext context) => _context = context;

    /// <summary>Gets title storage usage and quota for a service configuration.</summary>
    /// <param name="serviceConfigurationId">The title's case-sensitive service configuration id.</param>
    /// <param name="storageType">The title storage area to query.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<TitleStorageQuota> GetQuotaAsync(
        string serviceConfigurationId,
        TitleStorageType storageType,
        CancellationToken cancellationToken = default)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        IntPtr scid = Utf8.Allocate(serviceConfigurationId);

        try
        {
            return AsyncOperation<TitleStorageQuota>.RunAsync(
                queue.RawHandle(),
                block => NativeXbl.XblTitleStorageGetQuotaAsync(
                    context,
                    (byte*)scid,
                    (XblTitleStorageType)storageType,
                    (XAsyncBlock*)block),
                (IntPtr block, out TitleStorageQuota value) =>
                {
                    Utf8.Free(scid);

                    nuint used;
                    nuint quota;
                    int hr = NativeXbl.XblTitleStorageGetQuotaResult((XAsyncBlock*)block, &used, &quota);
                    value = HResult.Failed(hr)
                        ? default
                        : new TitleStorageQuota((ulong)used, (ulong)quota);
                    return hr;
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(scid);
            throw;
        }
    }

    /// <summary>Gets a page of blob metadata under a path.</summary>
    /// <param name="serviceConfigurationId">The title's service configuration id.</param>
    /// <param name="storageType">The title storage area to enumerate.</param>
    /// <param name="blobPath">Root path to enumerate. Empty enumerates from the storage root.</param>
    /// <param name="xboxUserId">Owner Xbox user id; ignored for global storage.</param>
    /// <param name="skipItems">Number of items to skip before returning results.</param>
    /// <param name="maxItems">Maximum items to return. 0 attempts to retrieve all items.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<TitleStorageBlobMetadataPage> GetBlobMetadataAsync(
        string serviceConfigurationId,
        TitleStorageType storageType,
        string blobPath = "",
        ulong xboxUserId = 0,
        uint skipItems = 0,
        uint maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        if (blobPath is null)
        {
            throw new ArgumentNullException(nameof(blobPath));
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        IntPtr scid = Utf8.Allocate(serviceConfigurationId);
        IntPtr path = IntPtr.Zero;

        try
        {
            path = Utf8.Allocate(blobPath);

            return AsyncOperation<TitleStorageBlobMetadataPage>.RunAsync(
                queue.RawHandle(),
                block => NativeXbl.XblTitleStorageGetBlobMetadataAsync(
                    context,
                    (byte*)scid,
                    (XblTitleStorageType)storageType,
                    (byte*)path,
                    xboxUserId,
                    skipItems,
                    maxItems,
                    (XAsyncBlock*)block),
                (IntPtr block, out TitleStorageBlobMetadataPage value) =>
                {
                    Utf8.Free(scid);
                    Utf8.Free(path);
                    return ToMetadataPage(block, queue, out value);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(scid);
            Utf8.Free(path);
            throw;
        }
    }

    /// <summary>Deletes a blob from title storage.</summary>
    /// <param name="blobMetadata">Metadata identifying the blob to delete.</param>
    /// <param name="deleteOnlyIfETagMatches">
    /// When <see langword="true"/>, delete only if <paramref name="blobMetadata"/>'s ETag matches
    /// the service value.
    /// </param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task DeleteBlobAsync(
        TitleStorageBlobMetadata blobMetadata,
        bool deleteOnlyIfETagMatches = false,
        CancellationToken cancellationToken = default)
    {
        if (blobMetadata is null)
        {
            throw new ArgumentNullException(nameof(blobMetadata));
        }

        IntPtr context = _context.Handle;
        XblTitleStorageBlobMetadata native = blobMetadata.ToNative();

        return AsyncOperation.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblTitleStorageDeleteBlobAsync(
                context,
                native,
                deleteOnlyIfETagMatches ? (byte)1 : (byte)0,
                (XAsyncBlock*)block),
            block => HResult.SOk,
            cancellationToken);
    }

    /// <summary>Downloads a blob into a managed byte array.</summary>
    /// <remarks>
    /// The array is sized from <see cref="TitleStorageBlobMetadata.Length"/> and pinned until the
    /// async operation completes. For binary blobs, <paramref name="preferredDownloadBlockSize"/>
    /// controls the chunk size XSAPI asks the service to use; pass 0 for the default. The ETag
    /// condition can skip the transfer when the supplied metadata is stale or already current.
    /// </remarks>
    /// <param name="blobMetadata">Metadata identifying the blob to download.</param>
    /// <param name="eTagMatchCondition">Optional ETag condition for optimistic concurrency.</param>
    /// <param name="selectQuery">Optional config filter or JSON property selector.</param>
    /// <param name="preferredDownloadBlockSize">Preferred binary download chunk size in bytes; 0 uses the default.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<TitleStorageBlobDownloadResult> DownloadBlobAsync(
        TitleStorageBlobMetadata blobMetadata,
        TitleStorageETagMatchCondition eTagMatchCondition = TitleStorageETagMatchCondition.NotUsed,
        string? selectQuery = null,
        ulong preferredDownloadBlockSize = 0,
        CancellationToken cancellationToken = default)
    {
        if (blobMetadata is null)
        {
            throw new ArgumentNullException(nameof(blobMetadata));
        }

        if (blobMetadata.Length > int.MaxValue)
        {
            throw new InvalidOperationException("The blob is too large to fit in a managed byte array.");
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        XblTitleStorageBlobMetadata native = blobMetadata.ToNative();
        byte[] data = new byte[(int)blobMetadata.Length];
        var pins = new TransferPins(data, selectQuery);
        nuint preferredBlockSize = TitleStorageBlobMetadata.ToNativeSize(
            preferredDownloadBlockSize,
            nameof(preferredDownloadBlockSize));

        try
        {
            return AsyncOperation<TitleStorageBlobDownloadResult>.RunAsync(
                queue.RawHandle(),
                block => NativeXbl.XblTitleStorageDownloadBlobAsync(
                    context,
                    native,
                    pins.DataPointer,
                    (nuint)data.Length,
                    (XblTitleStorageETagMatchCondition)eTagMatchCondition,
                    pins.SelectQueryPointer,
                    preferredBlockSize,
                    (XAsyncBlock*)block),
                (IntPtr block, out TitleStorageBlobDownloadResult value) =>
                {
                    try
                    {
                        XblTitleStorageBlobMetadata resultMetadata;
                        int hr = NativeXbl.XblTitleStorageDownloadBlobResult(
                            (XAsyncBlock*)block,
                            &resultMetadata);
                        if (HResult.Failed(hr))
                        {
                            value = null!;
                            return hr;
                        }

                        value = new TitleStorageBlobDownloadResult(
                            TitleStorageBlobMetadata.FromNative(&resultMetadata),
                            data);
                        return HResult.SOk;
                    }
                    finally
                    {
                        pins.Dispose();
                    }
                },
                cancellationToken);
        }
        catch
        {
            pins.Dispose();
            throw;
        }
    }

    /// <summary>Uploads a managed byte array to title storage.</summary>
    /// <remarks>
    /// The buffer is pinned for the whole async operation because XSAPI reads it after the native
    /// start call returns. Do not mutate <paramref name="data"/> until the task completes. For
    /// binary blobs, <paramref name="preferredUploadBlockSize"/> controls chunking. XSAPI defaults
    /// out-of-range values; the GDK 260404 range is 1 KiB to 4 MiB, with a 256 KiB default. Use an
    /// ETag condition to avoid overwriting a newer blob version.
    /// </remarks>
    /// <param name="blobMetadata">Metadata identifying the blob to upload.</param>
    /// <param name="data">Blob bytes to upload. The array is pinned until completion.</param>
    /// <param name="eTagMatchCondition">Optional ETag condition for optimistic concurrency.</param>
    /// <param name="preferredUploadBlockSize">Preferred binary upload chunk size in bytes; 0 uses the default.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<TitleStorageBlobMetadata> UploadBlobAsync(
        TitleStorageBlobMetadata blobMetadata,
        byte[] data,
        TitleStorageETagMatchCondition eTagMatchCondition = TitleStorageETagMatchCondition.NotUsed,
        ulong preferredUploadBlockSize = 0,
        CancellationToken cancellationToken = default)
    {
        if (blobMetadata is null)
        {
            throw new ArgumentNullException(nameof(blobMetadata));
        }

        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        XblTitleStorageBlobMetadata native = blobMetadata.ToNative((ulong)data.Length);
        var pins = new TransferPins(data, selectQuery: null);
        nuint preferredBlockSize = TitleStorageBlobMetadata.ToNativeSize(
            preferredUploadBlockSize,
            nameof(preferredUploadBlockSize));

        try
        {
            return AsyncOperation<TitleStorageBlobMetadata>.RunAsync(
                queue.RawHandle(),
                block => NativeXbl.XblTitleStorageUploadBlobAsync(
                    context,
                    native,
                    pins.DataPointer,
                    (nuint)data.Length,
                    (XblTitleStorageETagMatchCondition)eTagMatchCondition,
                    preferredBlockSize,
                    (XAsyncBlock*)block),
                (IntPtr block, out TitleStorageBlobMetadata value) =>
                {
                    try
                    {
                        XblTitleStorageBlobMetadata resultMetadata;
                        int hr = NativeXbl.XblTitleStorageUploadBlobResult((XAsyncBlock*)block, &resultMetadata);
                        value = HResult.Failed(hr)
                            ? null!
                            : TitleStorageBlobMetadata.FromNative(&resultMetadata);
                        return hr;
                    }
                    finally
                    {
                        pins.Dispose();
                    }
                },
                cancellationToken);
        }
        catch
        {
            pins.Dispose();
            throw;
        }
    }

    private static int ToMetadataPage(IntPtr block, GameTaskQueue? queue, out TitleStorageBlobMetadataPage value)
    {
        unsafe
        {
            IntPtr raw;
            int hr = NativeXbl.XblTitleStorageGetBlobMetadataResult((XAsyncBlock*)block, &raw);
            if (HResult.Failed(hr))
            {
                value = null!;
                return hr;
            }

            value = new TitleStorageBlobMetadataPage(new TitleStorageBlobMetadataResultHandle(raw), queue);
            return HResult.SOk;
        }
    }

    private sealed class TransferPins : IDisposable
    {
        private GCHandle _dataHandle;
        private IntPtr _selectQuery;

        internal TransferPins(byte[] data, string? selectQuery)
        {
            if (data.Length != 0)
            {
                _dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
                DataPointer = (byte*)_dataHandle.AddrOfPinnedObject();
            }

            _selectQuery = Utf8.Allocate(selectQuery);
            SelectQueryPointer = (byte*)_selectQuery;
        }

        internal byte* DataPointer { get; private set; }

        internal byte* SelectQueryPointer { get; private set; }

        public void Dispose()
        {
            if (_dataHandle.IsAllocated)
            {
                _dataHandle.Free();
                DataPointer = null;
            }

            Utf8.Free(_selectQuery);
            _selectQuery = IntPtr.Zero;
            SelectQueryPointer = null;
        }
    }
}
