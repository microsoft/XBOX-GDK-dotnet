using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// One page of leaderboard results, plus the means to fetch the next. Managed wrapper for a
/// caller-allocated <c>XblLeaderboardResult</c> buffer.
/// </summary>
/// <remarks>
/// <para>
/// XSAPI does not return a leaderboard result handle. The result APIs first report the byte count,
/// then write a complete pointer graph into a caller-allocated buffer and return an
/// <c>XblLeaderboardResult*</c> that points inside it. This type owns that unmanaged buffer and
/// releases it from <see cref="Dispose"/>.
/// </para>
/// <para>
/// Columns and rows are snapshotted into managed objects at construction, so they remain readable
/// after disposal. The native buffer is retained only because <see cref="GetNextAsync"/> must pass
/// the previous <c>XblLeaderboardResult*</c> back to XSAPI to obtain the next page.
/// </para>
/// </remarks>
public sealed class LeaderboardPage : IDisposable
{
    private readonly LeaderboardResultBufferHandle _buffer;
    private readonly XboxLiveContext _context;
    private readonly GameTaskQueue? _queue;
    private readonly IntPtr _result;
    private bool _disposed;

    internal unsafe LeaderboardPage(
        LeaderboardResultBufferHandle buffer,
        XblLeaderboardResult* result,
        XboxLiveContext context,
        GameTaskQueue? queue)
    {
        _buffer = buffer;
        _context = context;
        _queue = queue;
        _result = (IntPtr)result;

        TotalRowCount = result->TotalRowCount;
        HasNext = result->HasNext != 0;

        var columns = new LeaderboardColumn[(int)result->ColumnsCount];
        for (int i = 0; i < columns.Length; i++)
        {
            XblLeaderboardColumn* item = result->Columns + i;
            columns[i] = new LeaderboardColumn(
                Utf8.ToString(item->StatName) ?? string.Empty,
                (LeaderboardStatType)item->StatType);
        }

        var rows = new LeaderboardRow[(int)result->RowsCount];
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = LeaderboardRow.FromNative(result->Rows + i);
        }

        Columns = new ReadOnlyCollection<LeaderboardColumn>(columns);
        Rows = new ReadOnlyCollection<LeaderboardRow>(rows);
    }

    /// <summary>The total number of rows in the full leaderboard, not just this page.</summary>
    public uint TotalRowCount { get; }

    /// <summary>The columns returned for each row.</summary>
    public IReadOnlyList<LeaderboardColumn> Columns { get; }

    /// <summary>The rows in this page.</summary>
    public IReadOnlyList<LeaderboardRow> Rows { get; }

    /// <summary>Whether another page is available.</summary>
    public bool HasNext { get; }

    /// <summary>
    /// Fetches the next leaderboard page (<c>XblLeaderboardResultGetNextAsync</c>).
    /// </summary>
    /// <param name="maxItems">Maximum rows to return. 0 attempts to retrieve all remaining rows.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <exception cref="InvalidOperationException"><see cref="HasNext"/> is <see langword="false"/>.</exception>
    public Task<LeaderboardPage> GetNextAsync(
        uint maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (!HasNext)
        {
            throw new InvalidOperationException("There are no more leaderboard pages to fetch.");
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _queue;
        var buffer = _buffer;
        bool addedRef = false;
        buffer.DangerousAddRef(ref addedRef);

        try
        {
            unsafe
            {
                return AsyncOperation<LeaderboardPage>.RunAsync(
                    queue.RawHandle(),
                    block => NativeXbl.XblLeaderboardResultGetNextAsync(
                        context,
                        (XblLeaderboardResult*)_result,
                        maxItems,
                        (XAsyncBlock*)block),
                    (IntPtr block, out LeaderboardPage value) =>
                    {
                        try
                        {
                            return ReadNextResult(block, _context, queue, out value);
                        }
                        finally
                        {
                            if (addedRef)
                            {
                                buffer.DangerousRelease();
                            }
                        }
                    },
                    cancellationToken);
            }
        }
        catch
        {
            if (addedRef)
            {
                buffer.DangerousRelease();
            }

            throw;
        }
    }

    /// <summary>
    /// Enumerates this page and every page after it, fetching each on demand.
    /// </summary>
    /// <remarks>
    /// Each page fetched by this method is disposed once the page after it has been materialized,
    /// so the caller only has to dispose the page it started from. The returned rows are managed
    /// copies and remain valid.
    /// </remarks>
    public async Task<IReadOnlyList<LeaderboardRow>> ReadAllRowsAsync(
        uint maxItemsPerPage = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        var all = new List<LeaderboardRow>(Rows);
        LeaderboardPage current = this;

        while (current.HasNext)
        {
            cancellationToken.ThrowIfCancellationRequested();

            LeaderboardPage next = await current.GetNextAsync(maxItemsPerPage, cancellationToken)
                .ConfigureAwait(false);

            if (!ReferenceEquals(current, this))
            {
                current.Dispose();
            }

            all.AddRange(next.Rows);
            current = next;
        }

        if (!ReferenceEquals(current, this))
        {
            current.Dispose();
        }

        return new ReadOnlyCollection<LeaderboardRow>(all.ToArray());
    }

    /// <summary>Releases the caller-allocated native result buffer.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _buffer.Dispose();
    }

    internal static unsafe int ReadInitialResult(
        IntPtr block,
        XboxLiveContext context,
        GameTaskQueue? queue,
        out LeaderboardPage value)
    {
        value = null!;

        nuint size;
        int hr = NativeXbl.XblLeaderboardGetLeaderboardResultSize((XAsyncBlock*)block, &size);
        if (HResult.Failed(hr))
        {
            return hr;
        }

        return ReadResult(
            block,
            size,
            context,
            queue,
            static (XAsyncBlock* async, nuint bufferSize, void* buffer, XblLeaderboardResult** result, nuint* used) =>
                NativeXbl.XblLeaderboardGetLeaderboardResult(async, bufferSize, buffer, result, used),
            out value);
    }

    internal static unsafe int ReadNextResult(
        IntPtr block,
        XboxLiveContext context,
        GameTaskQueue? queue,
        out LeaderboardPage value)
    {
        value = null!;

        nuint size;
        int hr = NativeXbl.XblLeaderboardResultGetNextResultSize((XAsyncBlock*)block, &size);
        if (HResult.Failed(hr))
        {
            return hr;
        }

        return ReadResult(
            block,
            size,
            context,
            queue,
            static (XAsyncBlock* async, nuint bufferSize, void* buffer, XblLeaderboardResult** result, nuint* used) =>
                NativeXbl.XblLeaderboardResultGetNextResult(async, bufferSize, buffer, result, used),
            out value);
    }

    private static unsafe int ReadResult(
        IntPtr block,
        nuint size,
        XboxLiveContext context,
        GameTaskQueue? queue,
        LeaderboardResultReader reader,
        out LeaderboardPage value)
    {
        value = null!;

        var buffer = new LeaderboardResultBufferHandle(size);
        XblLeaderboardResult* result;
        nuint used;
        int hr = reader((XAsyncBlock*)block, size, (void*)buffer.DangerousGetHandle(), &result, &used);
        if (HResult.Failed(hr))
        {
            buffer.Dispose();
            return hr;
        }

        if (result is null)
        {
            buffer.Dispose();
            return HResult.EPointer;
        }

        value = new LeaderboardPage(buffer, result, context, queue);
        return HResult.SOk;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(LeaderboardPage));
        }
    }

    private unsafe delegate int LeaderboardResultReader(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblLeaderboardResult** result,
        nuint* bufferUsed);
}

/// <summary>
/// Xbox Live leaderboard queries. Mirrors <c>leaderboard_c.h</c>.
/// </summary>
public sealed unsafe class LeaderboardService
{
    private readonly XboxLiveContext _context;

    internal LeaderboardService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Gets a leaderboard page (<c>XblLeaderboardGetLeaderboardAsync</c>).
    /// </summary>
    /// <param name="query">The leaderboard query to execute.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <returns>
    /// A page of results. Dispose it when no more pages are needed; the rows and columns already
    /// read from it remain valid after disposal.
    /// </returns>
    public Task<LeaderboardPage> GetAsync(
        LeaderboardQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        var native = new NativeQuery(query);

        try
        {
            return AsyncOperation<LeaderboardPage>.RunAsync(
                queue.RawHandle(),
                block => NativeXbl.XblLeaderboardGetLeaderboardAsync(
                    context,
                    native.Query,
                    (XAsyncBlock*)block),
                (IntPtr block, out LeaderboardPage value) =>
                {
                    native.Dispose();
                    return LeaderboardPage.ReadInitialResult(block, _context, queue, out value);
                },
                cancellationToken);
        }
        catch
        {
            native.Dispose();
            throw;
        }
    }

    private sealed class NativeQuery : IDisposable
    {
        private readonly IntPtr[] _additionalNameBuffers;
        private IntPtr _leaderboardName;
        private IntPtr _statName;
        private IntPtr _additionalNames;
        private IntPtr _continuationToken;

        internal NativeQuery(LeaderboardQuery query)
        {
            _additionalNameBuffers = new IntPtr[query.AdditionalColumnLeaderboardNamesArray.Length];

            try
            {
                Query = default;
                Query.XboxUserId = query.XboxUserId;
                fixed (byte* scid = Query.Scid)
                {
                    CopyUtf8ToFixed(
                        query.ServiceConfigurationId,
                        scid,
                        XblLeaderboardQuery.ScidLength,
                        nameof(query.ServiceConfigurationId));
                }

                _leaderboardName = Utf8.Allocate(query.LeaderboardName);
                Query.LeaderboardName = (byte*)_leaderboardName;

                _statName = Utf8.Allocate(query.StatisticName);
                Query.StatName = (byte*)_statName;

                Query.SocialGroup = (XblSocialGroupType)query.SocialGroup;

                for (int i = 0; i < _additionalNameBuffers.Length; i++)
                {
                    _additionalNameBuffers[i] = Utf8.Allocate(query.AdditionalColumnLeaderboardNamesArray[i]);
                }

                if (_additionalNameBuffers.Length > 0)
                {
                    _additionalNames = Marshal.AllocHGlobal(_additionalNameBuffers.Length * IntPtr.Size);
                    for (int i = 0; i < _additionalNameBuffers.Length; i++)
                    {
                        ((IntPtr*)_additionalNames)[i] = _additionalNameBuffers[i];
                    }

                    Query.AdditionalColumnLeaderboardNames = (byte**)_additionalNames;
                    Query.AdditionalColumnLeaderboardNamesCount = (nuint)_additionalNameBuffers.Length;
                }

                Query.Order = (XblLeaderboardSortOrder)query.Order;
                Query.MaxItems = query.MaxItems;
                Query.SkipToXboxUserId = query.SkipToXboxUserId;
                Query.SkipResultToRank = query.SkipResultToRank;

                _continuationToken = Utf8.Allocate(query.ContinuationToken);
                Query.ContinuationToken = (byte*)_continuationToken;

                Query.QueryType = (XblLeaderboardQueryType)query.QueryType;
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal XblLeaderboardQuery Query;

        public void Dispose()
        {
            Utf8.Free(_leaderboardName);
            _leaderboardName = IntPtr.Zero;

            Utf8.Free(_statName);
            _statName = IntPtr.Zero;

            for (int i = 0; i < _additionalNameBuffers.Length; i++)
            {
                Utf8.Free(_additionalNameBuffers[i]);
                _additionalNameBuffers[i] = IntPtr.Zero;
            }

            if (_additionalNames != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_additionalNames);
                _additionalNames = IntPtr.Zero;
            }

            Utf8.Free(_continuationToken);
            _continuationToken = IntPtr.Zero;
        }

        private static void CopyUtf8ToFixed(string value, byte* destination, int destinationLength, string parameterName)
        {
            int byteCount = Encoding.UTF8.GetByteCount(value);
            if (byteCount >= destinationLength)
            {
                throw new ArgumentException(
                    "The value is too long for the native fixed buffer.",
                    parameterName);
            }

            for (int i = 0; i < destinationLength; i++)
            {
                destination[i] = 0;
            }

            fixed (char* chars = value)
            {
                Encoding.UTF8.GetBytes(chars, value.Length, destination, byteCount);
            }
        }
    }
}
