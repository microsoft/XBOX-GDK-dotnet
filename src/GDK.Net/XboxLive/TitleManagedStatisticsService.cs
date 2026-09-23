using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// Writes title-managed statistics for the signed-in user. Mirrors
/// <c>title_managed_statistics_c.h</c>.
/// </summary>
public sealed unsafe class TitleManagedStatisticsService
{
    private readonly XboxLiveContext _context;

    internal TitleManagedStatisticsService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Completely replaces the user's title-managed statistics
    /// (<c>XblTitleManagedStatsWriteAsync</c>).
    /// </summary>
    /// <param name="xboxUserId">The local user whose stats are being replaced.</param>
    /// <param name="statistics">
    /// The complete statistic set to submit. Any existing statistic not included is removed.
    /// </param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task WriteAsync(
        ulong xboxUserId,
        IEnumerable<TitleManagedStatistic> statistics,
        CancellationToken cancellationToken = default)
    {
        if (statistics is null)
        {
            throw new ArgumentNullException(nameof(statistics));
        }

        TitleManagedStatistic[] statisticArray = ToArray(statistics);
        IntPtr context = _context.Handle;
        var native = new NativeStatistics(statisticArray);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblTitleManagedStatsWriteAsync(
                    context,
                    xboxUserId,
                    native.Pointer,
                    native.Count,
                    (XAsyncBlock*)block),
                block => CompleteAndDispose(block, native),
                cancellationToken);
        }
        catch
        {
            native.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Partially updates existing title-managed statistics
    /// (<c>XblTitleManagedStatsUpdateStatsAsync</c>).
    /// </summary>
    /// <param name="statistics">The statistics to update. Statistics not included are unchanged.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task UpdateAsync(
        IEnumerable<TitleManagedStatistic> statistics,
        CancellationToken cancellationToken = default)
    {
        if (statistics is null)
        {
            throw new ArgumentNullException(nameof(statistics));
        }

        TitleManagedStatistic[] statisticArray = ToArray(statistics);
        IntPtr context = _context.Handle;
        var native = new NativeStatistics(statisticArray);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblTitleManagedStatsUpdateStatsAsync(
                    context,
                    native.Pointer,
                    native.Count,
                    (XAsyncBlock*)block),
                block => CompleteAndDispose(block, native),
                cancellationToken);
        }
        catch
        {
            native.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Deletes title-managed statistics for the signed-in user
    /// (<c>XblTitleManagedStatsDeleteStatsAsync</c>).
    /// </summary>
    /// <param name="statisticNames">The case-insensitive statistic names to delete.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task DeleteAsync(IEnumerable<string> statisticNames, CancellationToken cancellationToken = default)
    {
        if (statisticNames is null)
        {
            throw new ArgumentNullException(nameof(statisticNames));
        }

        string[] nameArray = ToArray(statisticNames);
        IntPtr context = _context.Handle;
        var native = new NativeStatisticNames(nameArray);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblTitleManagedStatsDeleteStatsAsync(
                    context,
                    native.Pointer,
                    native.Count,
                    (XAsyncBlock*)block),
                block => CompleteAndDispose(block, native),
                cancellationToken);
        }
        catch
        {
            native.Dispose();
            throw;
        }
    }

    private static int CompleteAndDispose(IntPtr block, IDisposable native)
    {
        try
        {
            return Native.XAsyncGetStatus((XAsyncBlock*)block, 0);
        }
        finally
        {
            native.Dispose();
        }
    }

    private static T[] ToArray<T>(IEnumerable<T> values)
    {
        if (values is T[] array)
        {
            return array;
        }

        if (values is ICollection<T> collection)
        {
            var copy = new T[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        return new List<T>(values).ToArray();
    }

    private static byte[] ToUtf8NullTerminated(string value) => Encoding.UTF8.GetBytes(value + "\0");

    private sealed class NativeStatistics : IDisposable
    {
        private readonly XblTitleManagedStatistic[] _statistics;
        private readonly GCHandle[] _nameHandles;
        private readonly GCHandle[] _stringHandles;
        private GCHandle _statisticsHandle;

        internal NativeStatistics(TitleManagedStatistic[] statistics)
        {
            _statistics = new XblTitleManagedStatistic[statistics.Length];
            _nameHandles = new GCHandle[statistics.Length];
            _stringHandles = new GCHandle[statistics.Length];

            try
            {
                for (int i = 0; i < statistics.Length; i++)
                {
                    TitleManagedStatistic statistic = statistics[i];
                    if (statistic is null)
                    {
                        throw new ArgumentException("Statistic collections cannot contain null entries.", nameof(statistics));
                    }

                    byte[] name = ToUtf8NullTerminated(statistic.StatisticName);
                    _nameHandles[i] = GCHandle.Alloc(name, GCHandleType.Pinned);

                    _statistics[i].StatisticName = (byte*)_nameHandles[i].AddrOfPinnedObject();
                    _statistics[i].StatisticType = (XblTitleManagedStatType)statistic.Value.Type;

                    if (statistic.Value.Type == TitleManagedStatType.Number)
                    {
                        _statistics[i].NumberValue = statistic.Value.NumberValue;
                        _statistics[i].StringValue = null;
                    }
                    else
                    {
                        byte[] value = ToUtf8NullTerminated(statistic.Value.StringValue);
                        _stringHandles[i] = GCHandle.Alloc(value, GCHandleType.Pinned);

                        _statistics[i].NumberValue = 0;
                        _statistics[i].StringValue = (byte*)_stringHandles[i].AddrOfPinnedObject();
                    }
                }

                if (_statistics.Length > 0)
                {
                    // XSAPI reads the struct array and the UTF-8 buffers after the starter returns,
                    // so every buffer it points at stays pinned until the async completion reader runs.
                    _statisticsHandle = GCHandle.Alloc(_statistics, GCHandleType.Pinned);
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal XblTitleManagedStatistic* Pointer =>
            _statistics.Length == 0 ? null : (XblTitleManagedStatistic*)_statisticsHandle.AddrOfPinnedObject();

        internal nuint Count => (nuint)_statistics.Length;

        public void Dispose()
        {
            if (_statisticsHandle.IsAllocated)
            {
                _statisticsHandle.Free();
            }

            Free(_nameHandles);
            Free(_stringHandles);
        }
    }

    private sealed class NativeStatisticNames : IDisposable
    {
        private readonly IntPtr[] _namePointers;
        private readonly GCHandle[] _nameHandles;
        private GCHandle _pointersHandle;

        internal NativeStatisticNames(string[] statisticNames)
        {
            _namePointers = new IntPtr[statisticNames.Length];
            _nameHandles = new GCHandle[statisticNames.Length];

            try
            {
                for (int i = 0; i < statisticNames.Length; i++)
                {
                    string name = statisticNames[i];
                    if (name is null)
                    {
                        throw new ArgumentException("Statistic name collections cannot contain null entries.", nameof(statisticNames));
                    }

                    if (name.Length == 0)
                    {
                        throw new ArgumentException("Statistic names cannot be empty.", nameof(statisticNames));
                    }

                    byte[] buffer = ToUtf8NullTerminated(name);
                    _nameHandles[i] = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    _namePointers[i] = _nameHandles[i].AddrOfPinnedObject();
                }

                if (_namePointers.Length > 0)
                {
                    // The native char** and every UTF-8 name it references must remain pinned for
                    // the whole async operation; completion disposes this owner.
                    _pointersHandle = GCHandle.Alloc(_namePointers, GCHandleType.Pinned);
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal byte** Pointer => _namePointers.Length == 0 ? null : (byte**)_pointersHandle.AddrOfPinnedObject();

        internal nuint Count => (nuint)_namePointers.Length;

        public void Dispose()
        {
            if (_pointersHandle.IsAllocated)
            {
                _pointersHandle.Free();
            }

            Free(_nameHandles);
        }
    }

    private static void Free(GCHandle[] handles)
    {
        for (int i = 0; i < handles.Length; i++)
        {
            if (handles[i].IsAllocated)
            {
                handles[i].Free();
            }
        }
    }
}
