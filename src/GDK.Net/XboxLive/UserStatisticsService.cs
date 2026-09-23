using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// Xbox Live user statistic queries and real-time statistic change notifications. Mirrors
/// <c>user_statistics_c.h</c>.
/// </summary>
/// <remarks>
/// Real-time statistic tracking and notifications are backed by XSAPI Real-Time Activity. The title
/// must activate <see cref="XboxLiveContext.RealTimeActivity"/> before expecting statistic-change
/// notifications; this service does not take a compile-time dependency on the RTA service.
/// </remarks>
public sealed unsafe class UserStatisticsService
{
    private readonly XboxLiveContext _context;

    internal UserStatisticsService(XboxLiveContext context) => _context = context;

    /// <summary>Gets one statistic for one user (<c>XblUserStatisticsGetSingleUserStatisticAsync</c>).</summary>
    /// <param name="xboxUserId">The Xbox user id whose statistic should be read.</param>
    /// <param name="serviceConfigurationId">The service configuration id (SCID) to query.</param>
    /// <param name="statisticName">The statistic name to query.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<UserStatisticsResult> GetSingleUserStatisticAsync(
        ulong xboxUserId,
        string serviceConfigurationId,
        string statisticName,
        CancellationToken cancellationToken = default)
    {
        ThrowIfNullServiceConfigurationId(serviceConfigurationId);
        ThrowIfMissingStatisticName(statisticName, nameof(statisticName));

        IntPtr context = _context.Handle;
        IntPtr scid = Utf8.Allocate(serviceConfigurationId);
        IntPtr name = IntPtr.Zero;

        try
        {
            name = Utf8.Allocate(statisticName);

            return AsyncOperation<UserStatisticsResult>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblUserStatisticsGetSingleUserStatisticAsync(
                    context,
                    xboxUserId,
                    (byte*)scid,
                    (byte*)name,
                    (XAsyncBlock*)block),
                (IntPtr block, out UserStatisticsResult value) =>
                {
                    Utf8.Free(scid);
                    Utf8.Free(name);
                    return ReadSingleResult(
                        block,
                        static (XAsyncBlock* async, nuint* size) =>
                            NativeXbl.XblUserStatisticsGetSingleUserStatisticResultSize(async, size),
                        static (XAsyncBlock* async, nuint size, void* buffer, XblUserStatisticsResult** result, nuint* used) =>
                            NativeXbl.XblUserStatisticsGetSingleUserStatisticResult(async, size, buffer, result, used),
                        out value);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(scid);
            Utf8.Free(name);
            throw;
        }
    }

    /// <summary>Gets several statistics for one user (<c>XblUserStatisticsGetSingleUserStatisticsAsync</c>).</summary>
    /// <param name="xboxUserId">The Xbox user id whose statistics should be read.</param>
    /// <param name="serviceConfigurationId">The service configuration id (SCID) to query.</param>
    /// <param name="statisticNames">Statistic names to query.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<UserStatisticsResult> GetSingleUserStatisticsAsync(
        ulong xboxUserId,
        string serviceConfigurationId,
        IEnumerable<string> statisticNames,
        CancellationToken cancellationToken = default)
    {
        ThrowIfNullServiceConfigurationId(serviceConfigurationId);
        string[] names = SnapshotStatisticNames(statisticNames);
        if (names.Length == 0)
        {
            return Task.FromResult(new UserStatisticsResult(
                xboxUserId,
                new ReadOnlyCollection<ServiceConfigurationStatistic>(Array.Empty<ServiceConfigurationStatistic>())));
        }

        IntPtr context = _context.Handle;
        IntPtr scid = Utf8.Allocate(serviceConfigurationId);
        NativeUtf8StringArray? nativeNames = null;

        try
        {
            nativeNames = new NativeUtf8StringArray(names);

            return AsyncOperation<UserStatisticsResult>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblUserStatisticsGetSingleUserStatisticsAsync(
                    context,
                    xboxUserId,
                    (byte*)scid,
                    nativeNames.Pointer,
                    (nuint)names.Length,
                    (XAsyncBlock*)block),
                (IntPtr block, out UserStatisticsResult value) =>
                {
                    Utf8.Free(scid);
                    nativeNames.Dispose();
                    return ReadSingleResult(
                        block,
                        static (XAsyncBlock* async, nuint* size) =>
                            NativeXbl.XblUserStatisticsGetSingleUserStatisticsResultSize(async, size),
                        static (XAsyncBlock* async, nuint size, void* buffer, XblUserStatisticsResult** result, nuint* used) =>
                            NativeXbl.XblUserStatisticsGetSingleUserStatisticsResult(async, size, buffer, result, used),
                        out value);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(scid);
            nativeNames?.Dispose();
            throw;
        }
    }

    /// <summary>Gets statistics for several users under one service configuration.</summary>
    /// <param name="xboxUserIds">Xbox user ids whose statistics should be read.</param>
    /// <param name="serviceConfigurationId">The service configuration id (SCID) to query.</param>
    /// <param name="statisticNames">Statistic names to query.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<UserStatisticsResult>> GetMultipleUserStatisticsAsync(
        IEnumerable<ulong> xboxUserIds,
        string serviceConfigurationId,
        IEnumerable<string> statisticNames,
        CancellationToken cancellationToken = default)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ThrowIfNullServiceConfigurationId(serviceConfigurationId);
        ulong[] ids = ToArray(xboxUserIds);
        string[] names = SnapshotStatisticNames(statisticNames);
        if (ids.Length == 0 || names.Length == 0)
        {
            return Task.FromResult<IReadOnlyList<UserStatisticsResult>>(Array.Empty<UserStatisticsResult>());
        }

        IntPtr context = _context.Handle;
        IntPtr idBuffer = AllocateUlongs(ids);
        IntPtr scid = IntPtr.Zero;
        NativeUtf8StringArray? nativeNames = null;

        try
        {
            scid = Utf8.Allocate(serviceConfigurationId);
            nativeNames = new NativeUtf8StringArray(names);

            return AsyncOperation<IReadOnlyList<UserStatisticsResult>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblUserStatisticsGetMultipleUserStatisticsAsync(
                    context,
                    (ulong*)idBuffer,
                    (nuint)ids.Length,
                    (byte*)scid,
                    nativeNames.Pointer,
                    (nuint)names.Length,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<UserStatisticsResult> value) =>
                {
                    Free(idBuffer);
                    Utf8.Free(scid);
                    nativeNames.Dispose();
                    return ReadMultipleResults(
                        block,
                        static (XAsyncBlock* async, nuint* size) =>
                            NativeXbl.XblUserStatisticsGetMultipleUserStatisticsResultSize(async, size),
                        static (XAsyncBlock* async, nuint size, void* buffer, XblUserStatisticsResult** results, nuint* count, nuint* used) =>
                            NativeXbl.XblUserStatisticsGetMultipleUserStatisticsResult(async, size, buffer, results, count, used),
                        out value);
                },
                cancellationToken);
        }
        catch
        {
            Free(idBuffer);
            Utf8.Free(scid);
            nativeNames?.Dispose();
            throw;
        }
    }

    /// <summary>Gets statistics for several users across several service configurations.</summary>
    /// <param name="xboxUserIds">Xbox user ids whose statistics should be read.</param>
    /// <param name="requestedStatistics">Service configurations and statistic names to query.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<UserStatisticsResult>> GetMultipleUserStatisticsForMultipleServiceConfigurationsAsync(
        IEnumerable<ulong> xboxUserIds,
        IEnumerable<RequestedStatistics> requestedStatistics,
        CancellationToken cancellationToken = default)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        if (requestedStatistics is null)
        {
            throw new ArgumentNullException(nameof(requestedStatistics));
        }

        ulong[] ids = ToArray(xboxUserIds);
        RequestedStatistics[] requests = ToArray(requestedStatistics);
        if (ids.Length == 0 || requests.Length == 0)
        {
            return Task.FromResult<IReadOnlyList<UserStatisticsResult>>(Array.Empty<UserStatisticsResult>());
        }

        IntPtr context = _context.Handle;
        IntPtr idBuffer = AllocateUlongs(ids);
        NativeRequestedStatistics? nativeRequests = null;

        try
        {
            nativeRequests = new NativeRequestedStatistics(requests);

            return AsyncOperation<IReadOnlyList<UserStatisticsResult>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsAsync(
                    context,
                    (ulong*)idBuffer,
                    checked((uint)ids.Length),
                    nativeRequests.Pointer,
                    checked((uint)requests.Length),
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<UserStatisticsResult> value) =>
                {
                    Free(idBuffer);
                    nativeRequests.Dispose();
                    return ReadMultipleResults(
                        block,
                        static (XAsyncBlock* async, nuint* size) =>
                            NativeXbl.XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsResultSize(async, size),
                        static (XAsyncBlock* async, nuint size, void* buffer, XblUserStatisticsResult** results, nuint* count, nuint* used) =>
                            NativeXbl.XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsResult(async, size, buffer, results, count, used),
                        out value);
                },
                cancellationToken);
        }
        catch
        {
            Free(idBuffer);
            nativeRequests?.Dispose();
            throw;
        }
    }


    /// <summary>Tracks statistics for real-time change notifications.</summary>
    /// <remarks>
    /// Changes arrive through <see cref="StatisticChanged"/>; XSAPI opens the real-time activity
    /// connection on demand.
    /// </remarks>
    /// <param name="xboxUserIds">Users to append to the tracked set.</param>
    /// <param name="serviceConfigurationId">The service configuration id (SCID) to watch.</param>
    /// <param name="statisticNames">Statistic names to watch.</param>
    public void TrackStatistics(
        IEnumerable<ulong> xboxUserIds,
        string serviceConfigurationId,
        IEnumerable<string> statisticNames) =>
        TrackStatisticsCore(xboxUserIds, serviceConfigurationId, statisticNames, track: true);

    /// <summary>Stops tracking statistics for real-time change notifications.</summary>
    /// <param name="xboxUserIds">Users to remove for the named statistics.</param>
    /// <param name="serviceConfigurationId">The service configuration id (SCID) being watched.</param>
    /// <param name="statisticNames">Statistic names to stop watching.</param>
    public void StopTrackingStatistics(
        IEnumerable<ulong> xboxUserIds,
        string serviceConfigurationId,
        IEnumerable<string> statisticNames) =>
        TrackStatisticsCore(xboxUserIds, serviceConfigurationId, statisticNames, track: false);

    /// <summary>Stops tracking all statistics for the provided users.</summary>
    /// <param name="xboxUserIds">Users whose statistic tracking should be cancelled.</param>
    public void StopTrackingUsers(IEnumerable<ulong> xboxUserIds)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ulong[] ids = ToArray(xboxUserIds);
        if (ids.Length == 0)
        {
            return;
        }

        fixed (ulong* p = ids)
        {
            Hr.ThrowIfFailed(NativeXbl.XblUserStatisticsStopTrackingUsers(_context.Handle, p, (nuint)ids.Length));
        }
    }

    /// <summary>Raised when a tracked statistic changes.</summary>
    /// <remarks>
    /// Call <see cref="TrackStatistics"/> first for the statistics you want to receive. XSAPI opens
    /// the real-time activity connection on demand, and callbacks arrive on an XSAPI-internal thread
    /// rather than this context's task queue.
    /// </remarks>
    public event EventHandler<StatisticChangedEventArgs>? StatisticChanged
    {
        add => UserStatisticChangeRegistry.Add(_context, value);
        remove => UserStatisticChangeRegistry.Remove(_context, value);
    }

    internal static T[] ToArray<T>(IEnumerable<T> values)
    {
        if (values is T[] array)
        {
            return (T[])array.Clone();
        }

        if (values is ICollection<T> collection)
        {
            var copy = new T[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        return new List<T>(values).ToArray();
    }

    internal static void ThrowIfMissingStatisticName(string? statisticName, string parameterName)
    {
        if (string.IsNullOrEmpty(statisticName))
        {
            throw new ArgumentException("A statistic name is required.", parameterName);
        }
    }

    private static void ThrowIfNullServiceConfigurationId(string? serviceConfigurationId)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }
    }

    private static string[] SnapshotStatisticNames(IEnumerable<string> statisticNames)
    {
        if (statisticNames is null)
        {
            throw new ArgumentNullException(nameof(statisticNames));
        }

        string[] names = ToArray(statisticNames);
        for (int i = 0; i < names.Length; i++)
        {
            ThrowIfMissingStatisticName(names[i], nameof(statisticNames));
        }

        return names;
    }

    private void TrackStatisticsCore(
        IEnumerable<ulong> xboxUserIds,
        string serviceConfigurationId,
        IEnumerable<string> statisticNames,
        bool track)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ThrowIfNullServiceConfigurationId(serviceConfigurationId);
        ulong[] ids = ToArray(xboxUserIds);
        string[] names = SnapshotStatisticNames(statisticNames);
        if (ids.Length == 0 || names.Length == 0)
        {
            return;
        }

        IntPtr scid = Utf8.Allocate(serviceConfigurationId);
        NativeUtf8StringArray? nativeNames = null;

        try
        {
            nativeNames = new NativeUtf8StringArray(names);
            fixed (ulong* p = ids)
            {
                int hr = track
                    ? NativeXbl.XblUserStatisticsTrackStatistics(
                        _context.Handle,
                        p,
                        (nuint)ids.Length,
                        (byte*)scid,
                        nativeNames.Pointer,
                        (nuint)names.Length)
                    : NativeXbl.XblUserStatisticsStopTrackingStatistics(
                        _context.Handle,
                        p,
                        (nuint)ids.Length,
                        (byte*)scid,
                        nativeNames.Pointer,
                        (nuint)names.Length);
                Hr.ThrowIfFailed(hr);
            }
        }
        finally
        {
            Utf8.Free(scid);
            nativeNames?.Dispose();
        }
    }

    private delegate int SingleResultReader(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** result,
        nuint* bufferUsed);

    private delegate int MultipleResultReader(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** results,
        nuint* resultsCount,
        nuint* bufferUsed);

    private delegate int ResultSizeReader(XAsyncBlock* async, nuint* resultSizeInBytes);

    private static int ReadSingleResult(
        IntPtr block,
        ResultSizeReader readSize,
        SingleResultReader readResult,
        out UserStatisticsResult value)
    {
        value = null!;
        IntPtr buffer = IntPtr.Zero;

        try
        {
            nuint size;
            int hr = readSize((XAsyncBlock*)block, &size);
            if (HResult.Failed(hr))
            {
                return hr;
            }

            buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
            XblUserStatisticsResult* result;
            nuint used;
            hr = readResult((XAsyncBlock*)block, size, (void*)buffer, &result, &used);
            if (HResult.Failed(hr))
            {
                return hr;
            }

            value = UserStatisticsResult.FromNative(result);
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static int ReadMultipleResults(
        IntPtr block,
        ResultSizeReader readSize,
        MultipleResultReader readResult,
        out IReadOnlyList<UserStatisticsResult> value)
    {
        value = Array.Empty<UserStatisticsResult>();
        IntPtr buffer = IntPtr.Zero;

        try
        {
            nuint size;
            int hr = readSize((XAsyncBlock*)block, &size);
            if (HResult.Failed(hr) || size == 0)
            {
                return hr;
            }

            buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
            XblUserStatisticsResult* results;
            nuint count;
            nuint used;
            hr = readResult((XAsyncBlock*)block, size, (void*)buffer, &results, &count, &used);
            if (HResult.Failed(hr))
            {
                return hr;
            }

            var managed = new UserStatisticsResult[(int)count];
            for (int i = 0; i < managed.Length; i++)
            {
                managed[i] = UserStatisticsResult.FromNative(results + i);
            }

            value = new ReadOnlyCollection<UserStatisticsResult>(managed);
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static IntPtr AllocateUlongs(ulong[] values)
    {
        IntPtr buffer = Marshal.AllocHGlobal(checked(values.Length * sizeof(ulong)));
        ulong* native = (ulong*)buffer;
        for (int i = 0; i < values.Length; i++)
        {
            native[i] = values[i];
        }

        return buffer;
    }

    private static void Free(IntPtr buffer)
    {
        if (buffer != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}


internal sealed class UserStatisticChangeRegistry : IXboxLiveHandlerRegistry
{
    private static readonly ConcurrentDictionary<IntPtr, UserStatisticChangeRegistry> Registrations = new();
    private static readonly ConditionalWeakTable<XboxLiveContext, UserStatisticChangeRegistry> ByContext = new();

    private readonly XboxLiveContext _context;
    private readonly object _gate = new();

    private EventHandler<StatisticChangedEventArgs>? _handlers;
    private GCHandle _self;
    private IntPtr _key;
    private int _functionContext;
    private bool _registered;

    private UserStatisticChangeRegistry(XboxLiveContext context) => _context = context;

    internal static void Add(XboxLiveContext context, EventHandler<StatisticChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        UserStatisticChangeRegistry registry = ByContext.GetValue(context, static ctx => new UserStatisticChangeRegistry(ctx));
        lock (registry._gate)
        {
            registry._handlers += handler;
            try
            {
                registry.EnsureRegistered();
            }
            catch
            {
                registry._handlers -= handler;
                throw;
            }
        }
    }

    internal static void Remove(XboxLiveContext context, EventHandler<StatisticChangedEventArgs>? handler)
    {
        if (handler is null || !ByContext.TryGetValue(context, out UserStatisticChangeRegistry? registry))
        {
            return;
        }

        lock (registry._gate)
        {
            registry._handlers -= handler;
            if (registry._handlers is null)
            {
                registry.Unregister();
            }
        }
    }

    internal static void Dispatch(XblStatisticChangeEventArgs eventArgs, IntPtr context)
    {
        if (!Registrations.TryGetValue(context, out UserStatisticChangeRegistry? registry))
        {
            return;
        }

        EventHandler<StatisticChangedEventArgs>? handlers;
        lock (registry._gate)
        {
            handlers = registry._handlers;
        }

        handlers?.Invoke(registry._context, StatisticChangedEventArgs.FromNative(eventArgs));
    }

    private void EnsureRegistered()
    {
        if (_registered)
        {
            return;
        }

        _self = GCHandle.Alloc(this);
        _key = GCHandle.ToIntPtr(_self);
        Registrations[_key] = this;

        int functionContext = NativeXbl.XblUserStatisticsAddStatisticChangedHandler(
            _context.Handle,
            Trampolines.UserStatisticChangedHandler,
            _key);
        if (functionContext == 0)
        {
            Registrations.TryRemove(_key, out _);
            _key = IntPtr.Zero;
            _self.Free();
            throw new GameRuntimeException(
                HResult.EFail,
                "Xbox Live rejected the statistic-change registration.");
        }

        _functionContext = functionContext;
        _registered = true;
        _context.TrackHandlerRegistry(this);
    }

    /// <inheritdoc />
    public void DetachAll()
    {
        lock (_gate)
        {
            _handlers = null;
            try
            {
                Unregister();
            }
            catch (GameRuntimeException)
            {
            }
        }
    }

    private void Unregister()
    {
        if (!_registered)
        {
            return;
        }

        _registered = false;
        try
        {
            NativeXbl.XblUserStatisticsRemoveStatisticChangedHandler(_context.Handle, _functionContext);
        }
        finally
        {
            if (_self.IsAllocated)
            {
                Registrations.TryRemove(_key, out _);
                _key = IntPtr.Zero;
                _self.Free();
            }
        }
    }
}

internal sealed unsafe class NativeUtf8StringArray : IDisposable
{
    private IntPtr _array;
    private IntPtr[] _buffers;

    internal NativeUtf8StringArray(IReadOnlyList<string> values)
    {
        _buffers = new IntPtr[values.Count];
        _array = Marshal.AllocHGlobal(checked(values.Count * IntPtr.Size));

        try
        {
            for (int i = 0; i < values.Count; i++)
            {
                _buffers[i] = Utf8.Allocate(values[i]);
                ((IntPtr*)_array)[i] = _buffers[i];
            }
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    internal byte** Pointer => (byte**)_array;

    public void Dispose()
    {
        for (int i = 0; i < _buffers.Length; i++)
        {
            Utf8.Free(_buffers[i]);
            _buffers[i] = IntPtr.Zero;
        }

        if (_array != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_array);
            _array = IntPtr.Zero;
        }
    }
}

internal sealed unsafe class NativeRequestedStatistics : IDisposable
{
    private IntPtr _native;
    private NativeUtf8StringArray[] _statistics;

    internal NativeRequestedStatistics(IReadOnlyList<RequestedStatistics> values)
    {
        _statistics = new NativeUtf8StringArray[values.Count];
        _native = Marshal.AllocHGlobal(checked(values.Count * sizeof(XblRequestedStatistics)));
        for (int i = 0; i < values.Count; i++)
        {
            Pointer[i] = default;
        }

        try
        {
            for (int i = 0; i < values.Count; i++)
            {
                XblRequestedStatistics* item = Pointer + i;
                CopyScid(item, values[i].ServiceConfigurationId);
                _statistics[i] = new NativeUtf8StringArray(values[i].StatisticNames);
                item->Statistics = _statistics[i].Pointer;
                item->StatisticsCount = checked((uint)values[i].StatisticNames.Count);
            }
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    internal XblRequestedStatistics* Pointer => (XblRequestedStatistics*)_native;

    public void Dispose()
    {
        for (int i = 0; i < _statistics.Length; i++)
        {
            _statistics[i]?.Dispose();
        }

        if (_native != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_native);
            _native = IntPtr.Zero;
        }
    }

    private static void CopyScid(XblRequestedStatistics* item, string serviceConfigurationId)
    {
        int byteCount = Encoding.UTF8.GetByteCount(serviceConfigurationId);
        if (byteCount >= XblRequestedStatistics.ServiceConfigurationIdLength)
        {
            throw new ArgumentException(
                "The service configuration id is too long for XblRequestedStatistics.serviceConfigurationId.",
                nameof(serviceConfigurationId));
        }

        fixed (char* chars = serviceConfigurationId)
        {
            byte* destination = item->ServiceConfigurationId;
            Encoding.UTF8.GetBytes(chars, serviceConfigurationId.Length, destination, byteCount);
            destination[byteCount] = 0;
        }
    }
}
