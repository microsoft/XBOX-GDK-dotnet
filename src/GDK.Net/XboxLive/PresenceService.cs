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
/// Xbox Live presence queries, rich presence updates and real-time presence notifications.
/// Mirrors <c>presence_c.h</c>.
/// </summary>
/// <remarks>
/// Real-time presence subscriptions and change events are backed by XSAPI Real-Time Activity. The
/// title must activate RTA for the owning <see cref="XboxLiveContext"/> before expecting device or
/// title presence notifications; activation is provided by the RTA service projection and is not a
/// compile-time dependency of this type.
/// </remarks>
public sealed unsafe class PresenceService
{
    private readonly XboxLiveContext _context;

    internal PresenceService(XboxLiveContext context) => _context = context;

    /// <summary>Sets rich presence for the context's user (<c>XblPresenceSetPresenceAsync</c>).</summary>
    /// <param name="isUserActiveInTitle">Whether the user is active in the current title.</param>
    /// <param name="richPresenceIds">Optional rich presence string identifiers.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task SetPresenceAsync(
        bool isUserActiveInTitle,
        PresenceRichPresenceIds? richPresenceIds = null,
        CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        NativeRichPresenceIds? nativeRichPresence = null;

        try
        {
            nativeRichPresence = richPresenceIds is null ? null : new NativeRichPresenceIds(richPresenceIds);

            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblPresenceSetPresenceAsync(
                    context,
                    isUserActiveInTitle ? (byte)1 : (byte)0,
                    nativeRichPresence is null ? null : nativeRichPresence.Pointer,
                    (XAsyncBlock*)block),
                block =>
                {
                    nativeRichPresence?.Dispose();
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            nativeRichPresence?.Dispose();
            throw;
        }
    }

    /// <summary>Gets one user's presence (<c>XblPresenceGetPresenceAsync</c>).</summary>
    /// <param name="xboxUserId">The user whose presence should be read.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<PresenceRecord> GetAsync(ulong xboxUserId, CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;

        return AsyncOperation<PresenceRecord>.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblPresenceGetPresenceAsync(context, xboxUserId, (XAsyncBlock*)block),
            static (IntPtr block, out PresenceRecord value) => ReadSingleRecord(block, out value),
            cancellationToken);
    }

    /// <summary>Gets presence for several users in one request.</summary>
    /// <param name="xboxUserIds">The users whose presence should be read.</param>
    /// <param name="filters">Optional result filters.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<PresenceRecord>> GetAsync(
        IEnumerable<ulong> xboxUserIds,
        PresenceQueryFilters? filters = null,
        CancellationToken cancellationToken = default)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ulong[] ids = ToArray(xboxUserIds);
        if (ids.Length == 0)
        {
            return Task.FromResult<IReadOnlyList<PresenceRecord>>(Array.Empty<PresenceRecord>());
        }

        IntPtr context = _context.Handle;
        IntPtr idBuffer = Allocate(ids);
        NativePresenceQueryFilters? nativeFilters = null;

        try
        {
            nativeFilters = filters is null ? null : new NativePresenceQueryFilters(filters);

            return AsyncOperation<IReadOnlyList<PresenceRecord>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblPresenceGetPresenceForMultipleUsersAsync(
                    context,
                    (ulong*)idBuffer,
                    (nuint)ids.Length,
                    nativeFilters is null ? null : nativeFilters.Pointer,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<PresenceRecord> value) =>
                {
                    Marshal.FreeHGlobal(idBuffer);
                    nativeFilters?.Dispose();
                    return ReadMultipleRecords(block, multipleUsers: true, out value);
                },
                cancellationToken);
        }
        catch
        {
            Marshal.FreeHGlobal(idBuffer);
            nativeFilters?.Dispose();
            throw;
        }
    }

    /// <summary>Gets presence for several users in one request with no filters.</summary>
    /// <param name="xboxUserIds">The users whose presence should be read.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<PresenceRecord>> GetAsync(
        IEnumerable<ulong> xboxUserIds,
        CancellationToken cancellationToken) =>
        GetAsync(xboxUserIds, filters: null, cancellationToken: cancellationToken);

    /// <summary>Gets presence for a social group (<c>XblPresenceGetPresenceForSocialGroupAsync</c>).</summary>
    /// <param name="socialGroup">The social group to query.</param>
    /// <param name="socialGroupOwnerXuid">The owner of the group, or <see langword="null"/> for the context user.</param>
    /// <param name="filters">Optional result filters.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<PresenceRecord>> GetForSocialGroupAsync(
        PresenceSocialGroup socialGroup,
        ulong? socialGroupOwnerXuid = null,
        PresenceQueryFilters? filters = null,
        CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        IntPtr groupName = Utf8.Allocate(SocialGroupName(socialGroup));
        IntPtr ownerBuffer = IntPtr.Zero;
        NativePresenceQueryFilters? nativeFilters = null;

        try
        {
            if (socialGroupOwnerXuid.HasValue)
            {
                ownerBuffer = Marshal.AllocHGlobal(sizeof(ulong));
                *(ulong*)ownerBuffer = socialGroupOwnerXuid.GetValueOrDefault();
            }

            nativeFilters = filters is null ? null : new NativePresenceQueryFilters(filters);

            return AsyncOperation<IReadOnlyList<PresenceRecord>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblPresenceGetPresenceForSocialGroupAsync(
                    context,
                    (byte*)groupName,
                    ownerBuffer == IntPtr.Zero ? null : (ulong*)ownerBuffer,
                    nativeFilters is null ? null : nativeFilters.Pointer,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<PresenceRecord> value) =>
                {
                    Utf8.Free(groupName);
                    if (ownerBuffer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(ownerBuffer);
                    }

                    nativeFilters?.Dispose();
                    return ReadMultipleRecords(block, multipleUsers: false, out value);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(groupName);
            if (ownerBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ownerBuffer);
            }

            nativeFilters?.Dispose();
            throw;
        }
    }


    /// <summary>Tracks users for real-time device and title presence changes.</summary>
    /// <remarks>Changes arrive through <see cref="DevicePresenceChanged"/> and
    /// <see cref="TitlePresenceChanged"/>; XSAPI opens the real-time activity connection on demand.</remarks>
    /// <param name="xboxUserIds">Users to append to the tracked set.</param>
    public void TrackUsers(IEnumerable<ulong> xboxUserIds)
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
            Hr.ThrowIfFailed(NativeXbl.XblPresenceTrackUsers(_context.Handle, p, (nuint)ids.Length));
        }
    }

    /// <summary>Stops tracking users for real-time presence changes.</summary>
    /// <param name="xboxUserIds">Users to remove from the tracked set.</param>
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
            Hr.ThrowIfFailed(NativeXbl.XblPresenceStopTrackingUsers(_context.Handle, p, (nuint)ids.Length));
        }
    }

    /// <summary>Tracks additional title ids for real-time title presence changes.</summary>
    /// <remarks>The current title is tracked by default.</remarks>
    /// <param name="titleIds">Title ids to append to the tracked set.</param>
    public void TrackAdditionalTitles(IEnumerable<uint> titleIds)
    {
        if (titleIds is null)
        {
            throw new ArgumentNullException(nameof(titleIds));
        }

        uint[] ids = ToArray(titleIds);
        if (ids.Length == 0)
        {
            return;
        }

        fixed (uint* p = ids)
        {
            Hr.ThrowIfFailed(NativeXbl.XblPresenceTrackAdditionalTitles(_context.Handle, p, (nuint)ids.Length));
        }
    }

    /// <summary>Stops tracking additional title ids for real-time title presence changes.</summary>
    /// <param name="titleIds">Title ids to remove from the tracked set.</param>
    public void StopTrackingAdditionalTitles(IEnumerable<uint> titleIds)
    {
        if (titleIds is null)
        {
            throw new ArgumentNullException(nameof(titleIds));
        }

        uint[] ids = ToArray(titleIds);
        if (ids.Length == 0)
        {
            return;
        }

        fixed (uint* p = ids)
        {
            Hr.ThrowIfFailed(NativeXbl.XblPresenceStopTrackingAdditionalTitles(_context.Handle, p, (nuint)ids.Length));
        }
    }

    /// <summary>Raised when a tracked user's device presence changes.</summary>
    /// <remarks>
    /// Call <see cref="TrackUsers"/> first. XSAPI opens the real-time activity connection on demand,
    /// and callbacks arrive on an XSAPI-internal thread rather than this context's task queue.
    /// </remarks>
    public event EventHandler<DevicePresenceChangedEventArgs>? DevicePresenceChanged
    {
        add => PresenceChangeRegistry.AddDevice(_context, value);
        remove => PresenceChangeRegistry.RemoveDevice(_context, value);
    }

    /// <summary>Raised when a tracked user's title presence changes.</summary>
    /// <remarks>
    /// Call <see cref="TrackUsers"/> and, for non-current titles, <see cref="TrackAdditionalTitles"/>
    /// first. XSAPI opens the real-time activity connection on demand, and callbacks arrive on an
    /// XSAPI-internal thread rather than this context's task queue.
    /// </remarks>
    public event EventHandler<TitlePresenceChangedEventArgs>? TitlePresenceChanged
    {
        add => PresenceChangeRegistry.AddTitle(_context, value);
        remove => PresenceChangeRegistry.RemoveTitle(_context, value);
    }

    internal static string SocialGroupName(PresenceSocialGroup socialGroup) => socialGroup switch
    {
        PresenceSocialGroup.Favorites => "Favorites",
        PresenceSocialGroup.People => "People",
        PresenceSocialGroup.Friends => "Friends",
        _ => throw new ArgumentOutOfRangeException(
            nameof(socialGroup),
            socialGroup,
            "The Xbox Live presence service only recognizes Favorites, People and Friends."),
    };

    private static int ReadSingleRecord(IntPtr block, out PresenceRecord value)
    {
        value = null!;

        IntPtr raw;
        int hr = NativeXbl.XblPresenceGetPresenceResult((XAsyncBlock*)block, &raw);
        if (HResult.Failed(hr))
        {
            return hr;
        }

        using (var handle = new PresenceRecordHandle(raw))
        {
            value = PresenceRecord.FromHandle(handle);
        }

        return HResult.SOk;
    }

    private static int ReadMultipleRecords(IntPtr block, bool multipleUsers, out IReadOnlyList<PresenceRecord> value)
    {
        value = Array.Empty<PresenceRecord>();

        nuint count;
        int hr = multipleUsers
            ? NativeXbl.XblPresenceGetPresenceForMultipleUsersResultCount((XAsyncBlock*)block, &count)
            : NativeXbl.XblPresenceGetPresenceForSocialGroupResultCount((XAsyncBlock*)block, &count);
        if (HResult.Failed(hr) || count == 0)
        {
            return hr;
        }

        var rawHandles = new IntPtr[checked((int)count)];
        fixed (IntPtr* buffer = rawHandles)
        {
            hr = multipleUsers
                ? NativeXbl.XblPresenceGetPresenceForMultipleUsersResult((XAsyncBlock*)block, buffer, count)
                : NativeXbl.XblPresenceGetPresenceForSocialGroupResult((XAsyncBlock*)block, buffer, count);
        }

        if (HResult.Failed(hr))
        {
            return hr;
        }

        value = Materialize(rawHandles);
        return HResult.SOk;
    }

    private static IReadOnlyList<PresenceRecord> Materialize(IntPtr[] rawHandles)
    {
        var handles = new PresenceRecordHandle[rawHandles.Length];
        try
        {
            for (int i = 0; i < rawHandles.Length; i++)
            {
                handles[i] = new PresenceRecordHandle(rawHandles[i]);
            }

            var records = new PresenceRecord[handles.Length];
            for (int i = 0; i < handles.Length; i++)
            {
                records[i] = PresenceRecord.FromHandle(handles[i]);
            }

            return new ReadOnlyCollection<PresenceRecord>(records);
        }
        finally
        {
            for (int i = 0; i < handles.Length; i++)
            {
                handles[i]?.Dispose();
            }
        }
    }

    private static ulong[] ToArray(IEnumerable<ulong> ids)
    {
        if (ids is ulong[] array)
        {
            return (ulong[])array.Clone();
        }

        if (ids is ICollection<ulong> collection)
        {
            var copy = new ulong[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<ulong>(ids);
        return list.ToArray();
    }

    private static uint[] ToArray(IEnumerable<uint> ids)
    {
        if (ids is uint[] array)
        {
            return (uint[])array.Clone();
        }

        if (ids is ICollection<uint> collection)
        {
            var copy = new uint[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<uint>(ids);
        return list.ToArray();
    }

    private static IntPtr Allocate(ulong[] ids)
    {
        IntPtr buffer = Marshal.AllocHGlobal(ids.Length * sizeof(ulong));
        for (int i = 0; i < ids.Length; i++)
        {
            ((ulong*)buffer)[i] = ids[i];
        }

        return buffer;
    }
}


internal sealed class PresenceChangeRegistry : IXboxLiveHandlerRegistry
{
    private static readonly ConcurrentDictionary<IntPtr, PresenceChangeRegistry> Registrations = new();
    private static readonly ConditionalWeakTable<XboxLiveContext, PresenceChangeRegistry> ByContext = new();

    private readonly XboxLiveContext _context;
    private readonly object _gate = new();

    private EventHandler<DevicePresenceChangedEventArgs>? _deviceHandlers;
    private EventHandler<TitlePresenceChangedEventArgs>? _titleHandlers;
    private GCHandle _self;
    private IntPtr _key;
    private int _deviceFunctionContext;
    private int _titleFunctionContext;
    private bool _deviceRegistered;
    private bool _titleRegistered;

    private PresenceChangeRegistry(XboxLiveContext context) => _context = context;

    internal static void AddDevice(XboxLiveContext context, EventHandler<DevicePresenceChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        PresenceChangeRegistry registry = ByContext.GetValue(context, static ctx => new PresenceChangeRegistry(ctx));
        lock (registry._gate)
        {
            registry._deviceHandlers += handler;
            try
            {
                registry.EnsureDeviceRegistered();
            }
            catch
            {
                registry._deviceHandlers -= handler;
                throw;
            }
        }
    }

    internal static void RemoveDevice(XboxLiveContext context, EventHandler<DevicePresenceChangedEventArgs>? handler)
    {
        if (handler is null || !ByContext.TryGetValue(context, out PresenceChangeRegistry? registry))
        {
            return;
        }

        lock (registry._gate)
        {
            registry._deviceHandlers -= handler;
            if (registry._deviceHandlers is null)
            {
                registry.UnregisterDevice();
            }
        }
    }

    internal static void AddTitle(XboxLiveContext context, EventHandler<TitlePresenceChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        PresenceChangeRegistry registry = ByContext.GetValue(context, static ctx => new PresenceChangeRegistry(ctx));
        lock (registry._gate)
        {
            registry._titleHandlers += handler;
            try
            {
                registry.EnsureTitleRegistered();
            }
            catch
            {
                registry._titleHandlers -= handler;
                throw;
            }
        }
    }

    internal static void RemoveTitle(XboxLiveContext context, EventHandler<TitlePresenceChangedEventArgs>? handler)
    {
        if (handler is null || !ByContext.TryGetValue(context, out PresenceChangeRegistry? registry))
        {
            return;
        }

        lock (registry._gate)
        {
            registry._titleHandlers -= handler;
            if (registry._titleHandlers is null)
            {
                registry.UnregisterTitle();
            }
        }
    }

    internal static void DispatchDevice(
        IntPtr context,
        ulong xuid,
        XblPresenceDeviceType deviceType,
        bool isUserLoggedOnDevice)
    {
        if (!Registrations.TryGetValue(context, out PresenceChangeRegistry? registry))
        {
            return;
        }

        EventHandler<DevicePresenceChangedEventArgs>? handlers;
        lock (registry._gate)
        {
            handlers = registry._deviceHandlers;
        }

        handlers?.Invoke(
            registry._context,
            new DevicePresenceChangedEventArgs(xuid, (PresenceDeviceType)deviceType, isUserLoggedOnDevice));
    }

    internal static void DispatchTitle(
        IntPtr context,
        ulong xuid,
        uint titleId,
        XblPresenceTitleState titleState)
    {
        if (!Registrations.TryGetValue(context, out PresenceChangeRegistry? registry))
        {
            return;
        }

        EventHandler<TitlePresenceChangedEventArgs>? handlers;
        lock (registry._gate)
        {
            handlers = registry._titleHandlers;
        }

        handlers?.Invoke(registry._context, new TitlePresenceChangedEventArgs(xuid, titleId, (PresenceTitleState)titleState));
    }

    private void EnsureDeviceRegistered()
    {
        if (_deviceRegistered)
        {
            return;
        }

        EnsureKey();
        int functionContext = NativeXbl.XblPresenceAddDevicePresenceChangedHandler(
            _context.Handle,
            Trampolines.PresenceDevicePresenceChangedHandler,
            _key);
        if (functionContext == 0)
        {
            ReleaseKeyIfIdle();
            throw new GameRuntimeException(HResult.EFail, "Xbox Live rejected the device presence-change registration.");
        }

        _deviceFunctionContext = functionContext;
        _deviceRegistered = true;
        _context.TrackHandlerRegistry(this);
    }

    private void EnsureTitleRegistered()
    {
        if (_titleRegistered)
        {
            return;
        }

        EnsureKey();
        int functionContext = NativeXbl.XblPresenceAddTitlePresenceChangedHandler(
            _context.Handle,
            Trampolines.PresenceTitlePresenceChangedHandler,
            _key);
        if (functionContext == 0)
        {
            ReleaseKeyIfIdle();
            throw new GameRuntimeException(HResult.EFail, "Xbox Live rejected the title presence-change registration.");
        }

        _titleFunctionContext = functionContext;
        _titleRegistered = true;
        _context.TrackHandlerRegistry(this);
    }

    /// <inheritdoc />
    public void DetachAll()
    {
        lock (_gate)
        {
            _deviceHandlers = null;
            _titleHandlers = null;

            try
            {
                UnregisterDevice();
            }
            catch (GameRuntimeException)
            {
            }

            try
            {
                UnregisterTitle();
            }
            catch (GameRuntimeException)
            {
            }
        }
    }

    private void UnregisterDevice()
    {
        if (!_deviceRegistered)
        {
            return;
        }

        _deviceRegistered = false;
        try
        {
            Hr.ThrowIfFailed(NativeXbl.XblPresenceRemoveDevicePresenceChangedHandler(_context.Handle, _deviceFunctionContext));
        }
        finally
        {
            ReleaseKeyIfIdle();
        }
    }

    private void UnregisterTitle()
    {
        if (!_titleRegistered)
        {
            return;
        }

        _titleRegistered = false;
        try
        {
            Hr.ThrowIfFailed(NativeXbl.XblPresenceRemoveTitlePresenceChangedHandler(_context.Handle, _titleFunctionContext));
        }
        finally
        {
            ReleaseKeyIfIdle();
        }
    }

    private void EnsureKey()
    {
        if (_self.IsAllocated)
        {
            return;
        }

        _self = GCHandle.Alloc(this);
        _key = GCHandle.ToIntPtr(_self);
        Registrations[_key] = this;
    }

    private void ReleaseKeyIfIdle()
    {
        if (_deviceRegistered || _titleRegistered || !_self.IsAllocated)
        {
            return;
        }

        Registrations.TryRemove(_key, out _);
        _key = IntPtr.Zero;
        _self.Free();
    }
}

internal sealed unsafe class NativeRichPresenceIds : IDisposable
{
    private IntPtr _native;
    private IntPtr _presenceId;
    private IntPtr _tokenArray;
    private IntPtr[] _tokenBuffers = Array.Empty<IntPtr>();

    internal NativeRichPresenceIds(PresenceRichPresenceIds value)
    {
        _native = Marshal.AllocHGlobal(sizeof(XblPresenceRichPresenceIds));
        *Pointer = default;

        try
        {
            CopyScid(value.ServiceConfigurationId);
            _presenceId = Utf8.Allocate(value.PresenceId);
            Pointer->PresenceId = (byte*)_presenceId;

            if (value.PresenceTokenIds.Count > 0)
            {
                _tokenArray = Marshal.AllocHGlobal(value.PresenceTokenIds.Count * IntPtr.Size);
                _tokenBuffers = new IntPtr[value.PresenceTokenIds.Count];
                for (int i = 0; i < _tokenBuffers.Length; i++)
                {
                    _tokenBuffers[i] = Utf8.Allocate(value.PresenceTokenIds[i]);
                    ((IntPtr*)_tokenArray)[i] = _tokenBuffers[i];
                }

                Pointer->PresenceTokenIds = (byte**)_tokenArray;
                Pointer->PresenceTokenIdsCount = (nuint)_tokenBuffers.Length;
            }
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    internal XblPresenceRichPresenceIds* Pointer => (XblPresenceRichPresenceIds*)_native;

    public void Dispose()
    {
        for (int i = 0; i < _tokenBuffers.Length; i++)
        {
            Utf8.Free(_tokenBuffers[i]);
        }

        if (_tokenArray != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_tokenArray);
            _tokenArray = IntPtr.Zero;
        }

        Utf8.Free(_presenceId);
        _presenceId = IntPtr.Zero;

        if (_native != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_native);
            _native = IntPtr.Zero;
        }
    }

    private void CopyScid(string scid)
    {
        int byteCount = Encoding.UTF8.GetByteCount(scid);
        if (byteCount >= XblPresenceRichPresenceIds.ScidCharSize)
        {
            throw new ArgumentException("The Service Configuration ID is too long for XblPresenceRichPresenceIds.scid.", nameof(scid));
        }

        fixed (char* chars = scid)
        {
            byte* destination = Pointer->Scid;
            Encoding.UTF8.GetBytes(chars, scid.Length, destination, byteCount);
            destination[byteCount] = 0;
        }
    }
}

internal sealed unsafe class NativePresenceQueryFilters : IDisposable
{
    private IntPtr _native;
    private IntPtr _deviceTypes;
    private IntPtr _titleIds;

    internal NativePresenceQueryFilters(PresenceQueryFilters value)
    {
        _native = Marshal.AllocHGlobal(sizeof(XblPresenceQueryFilters));
        *Pointer = default;

        try
        {
            if (value.DeviceTypes.Count > 0)
            {
                _deviceTypes = Marshal.AllocHGlobal(value.DeviceTypes.Count * sizeof(XblPresenceDeviceType));
                for (int i = 0; i < value.DeviceTypes.Count; i++)
                {
                    ((XblPresenceDeviceType*)_deviceTypes)[i] = (XblPresenceDeviceType)value.DeviceTypes[i];
                }

                Pointer->DeviceTypes = (XblPresenceDeviceType*)_deviceTypes;
                Pointer->DeviceTypesCount = (nuint)value.DeviceTypes.Count;
            }

            if (value.TitleIds.Count > 0)
            {
                _titleIds = Marshal.AllocHGlobal(value.TitleIds.Count * sizeof(uint));
                for (int i = 0; i < value.TitleIds.Count; i++)
                {
                    ((uint*)_titleIds)[i] = value.TitleIds[i];
                }

                Pointer->TitleIds = (uint*)_titleIds;
                Pointer->TitleIdsCount = (nuint)value.TitleIds.Count;
            }

            Pointer->DetailLevel = (XblPresenceDetailLevel)value.DetailLevel;
            Pointer->OnlineOnly = value.OnlineOnly ? (byte)1 : (byte)0;
            Pointer->BroadcastingOnly = value.BroadcastingOnly ? (byte)1 : (byte)0;
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    internal XblPresenceQueryFilters* Pointer => (XblPresenceQueryFilters*)_native;

    public void Dispose()
    {
        if (_deviceTypes != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_deviceTypes);
            _deviceTypes = IntPtr.Zero;
        }

        if (_titleIds != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_titleIds);
            _titleIds = IntPtr.Zero;
        }

        if (_native != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_native);
            _native = IntPtr.Zero;
        }
    }
}
