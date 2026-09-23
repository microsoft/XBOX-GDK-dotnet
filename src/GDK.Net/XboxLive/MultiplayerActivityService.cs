using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// Xbox Live multiplayer activity, invites and recent-player reporting. Mirrors
/// <c>multiplayer_activity_c.h</c>.
/// </summary>
/// <remarks>
/// <para>
/// Multiplayer activity is certification-sensitive. The activity advertised through
/// <see cref="SetActivityAsync"/> must always describe the real joinable session because it is what
/// lets friends join in progress. Update it when the join state changes and call
/// <see cref="DeleteActivityAsync"/> when the session ends; leaving a stale or orphaned activity is
/// a certification failure.
/// </para>
/// <para>
/// Recent-player updates are privacy-sensitive. <see cref="UpdateRecentPlayers"/> only queues local
/// encounter data; XSAPI uploads the batch later, or immediately when
/// <see cref="FlushRecentPlayersAsync"/> is called.
/// </para>
/// <para>
/// The invite notification handler functions are present in the header for non-GDK platforms but
/// are not exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK edition 260404, so this service
/// exposes query-and-write APIs only.
/// </para>
/// </remarks>
public sealed unsafe class MultiplayerActivityService
{
    private readonly XboxLiveContext _context;

    internal MultiplayerActivityService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Sets or updates the local user's multiplayer activity
    /// (<c>XblMultiplayerActivitySetActivityAsync</c>).
    /// </summary>
    /// <param name="activityInfo">Accurate activity information for the local user's current joinable session.</param>
    /// <param name="allowCrossPlatformJoin">Whether the activity should be joinable on other title-supported platforms.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <remarks>
    /// This is a certification-sensitive advertisement. Keep it current while the player is in a
    /// multiplayer session and always call <see cref="DeleteActivityAsync"/> when that session ends;
    /// <see cref="MultiplayerActivityInfo.ConnectionString"/> and
    /// <see cref="MultiplayerActivityInfo.GroupId"/> must not be empty when setting an activity.
    /// </remarks>
    public Task SetActivityAsync(
        MultiplayerActivityInfo activityInfo,
        bool allowCrossPlatformJoin = false,
        CancellationToken cancellationToken = default)
    {
        if (activityInfo is null)
        {
            throw new ArgumentNullException(nameof(activityInfo));
        }

        ValidateActivityForSet(activityInfo);

        IntPtr context = _context.Handle;
        var native = new NativeActivityInfo(activityInfo);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblMultiplayerActivitySetActivityAsync(
                    context,
                    native.Pointer,
                    allowCrossPlatformJoin ? (byte)1 : (byte)0,
                    (XAsyncBlock*)block),
                block =>
                {
                    native.Dispose();
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            native.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Gets multiplayer activity for up to 30 users
    /// (<c>XblMultiplayerActivityGetActivityAsync</c>).
    /// </summary>
    /// <param name="xboxUserIds">Xbox user ids to query. An empty sequence returns an empty result.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <returns>Managed snapshots of the returned activities; no returned object points into native memory.</returns>
    public Task<IReadOnlyList<MultiplayerActivityInfo>> GetActivitiesAsync(
        IEnumerable<ulong> xboxUserIds,
        CancellationToken cancellationToken = default)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ulong[] ids = ToArray(xboxUserIds);
        if (ids.Length == 0)
        {
            return Task.FromResult<IReadOnlyList<MultiplayerActivityInfo>>(Array.Empty<MultiplayerActivityInfo>());
        }

        if (ids.Length > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(xboxUserIds), "At most 30 users can be queried at once.");
        }

        IntPtr context = _context.Handle;
        IntPtr idBuffer = AllocateUlongs(ids);

        try
        {
            return AsyncOperation<IReadOnlyList<MultiplayerActivityInfo>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblMultiplayerActivityGetActivityAsync(
                    context,
                    (ulong*)idBuffer,
                    (nuint)ids.Length,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<MultiplayerActivityInfo> value) =>
                {
                    try
                    {
                        return ReadGetActivityResult(block, out value);
                    }
                    finally
                    {
                        Free(idBuffer);
                    }
                },
                cancellationToken);
        }
        catch
        {
            Free(idBuffer);
            throw;
        }
    }

    /// <summary>
    /// Clears the local user's multiplayer activity
    /// (<c>XblMultiplayerActivityDeleteActivityAsync</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <remarks>
    /// Calling this when the multiplayer session ends is not optional. A stale or orphaned activity
    /// advertises a join path that no longer exists and is a certification failure.
    /// </remarks>
    public Task DeleteActivityAsync(CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        return AsyncOperation.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblMultiplayerActivityDeleteActivityAsync(context, (XAsyncBlock*)block),
            block => HResult.SOk,
            cancellationToken);
    }

    /// <summary>
    /// Sends invites for the caller's current activity
    /// (<c>XblMultiplayerActivitySendInvitesAsync</c>).
    /// </summary>
    /// <param name="xboxUserIds">Xbox user ids to invite. An empty sequence returns a completed task.</param>
    /// <param name="allowCrossPlatformJoin">Whether to send cross-platform invites when the title is configured for them.</param>
    /// <param name="connectionString">Optional connection string to pass to invitees.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task SendInvitesAsync(
        IEnumerable<ulong> xboxUserIds,
        bool allowCrossPlatformJoin = false,
        string? connectionString = null,
        CancellationToken cancellationToken = default)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ulong[] ids = ToArray(xboxUserIds);
        if (ids.Length == 0)
        {
            return Task.CompletedTask;
        }

        IntPtr context = _context.Handle;
        IntPtr idBuffer = AllocateUlongs(ids);
        IntPtr connection = Utf8.Allocate(connectionString);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblMultiplayerActivitySendInvitesAsync(
                    context,
                    (ulong*)idBuffer,
                    (nuint)ids.Length,
                    allowCrossPlatformJoin ? (byte)1 : (byte)0,
                    (byte*)connection,
                    (XAsyncBlock*)block),
                block =>
                {
                    Free(idBuffer);
                    Utf8.Free(connection);
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            Free(idBuffer);
            Utf8.Free(connection);
            throw;
        }
    }

    /// <summary>
    /// Queues recent-player encounters locally
    /// (<c>XblMultiplayerActivityUpdateRecentPlayers</c>).
    /// </summary>
    /// <param name="updates">Recent-player encounters to append or update. An empty sequence is a no-op.</param>
    /// <remarks>
    /// This synchronous call does not upload immediately; it only adds privacy-sensitive encounter
    /// data to XSAPI's local batch. XSAPI periodically flushes the batch on its background queue, or
    /// you can call <see cref="FlushRecentPlayersAsync"/> to send pending updates now.
    /// </remarks>
    public void UpdateRecentPlayers(IEnumerable<MultiplayerActivityRecentPlayerUpdate> updates)
    {
        if (updates is null)
        {
            throw new ArgumentNullException(nameof(updates));
        }

        XblMultiplayerActivityRecentPlayerUpdate[] native = ToNativeUpdates(updates);
        if (native.Length == 0)
        {
            return;
        }

        fixed (XblMultiplayerActivityRecentPlayerUpdate* buffer = native)
        {
            Hr.ThrowIfFailed(NativeXbl.XblMultiplayerActivityUpdateRecentPlayers(
                _context.Handle,
                buffer,
                (nuint)native.Length));
        }
    }

    /// <summary>
    /// Uploads pending recent-player updates immediately
    /// (<c>XblMultiplayerActivityFlushRecentPlayersAsync</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <remarks>
    /// Use this after <see cref="UpdateRecentPlayers"/> when the title needs privacy-sensitive
    /// encounter data to be visible before XSAPI's periodic background upload.
    /// </remarks>
    public Task FlushRecentPlayersAsync(CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        return AsyncOperation.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblMultiplayerActivityFlushRecentPlayersAsync(context, (XAsyncBlock*)block),
            block => HResult.SOk,
            cancellationToken);
    }

    private static int ReadGetActivityResult(IntPtr block, out IReadOnlyList<MultiplayerActivityInfo> value)
    {
        value = Array.Empty<MultiplayerActivityInfo>();

        nuint size;
        int hr = NativeXbl.XblMultiplayerActivityGetActivityResultSize((XAsyncBlock*)block, &size);
        if (HResult.Failed(hr) || size == 0)
        {
            return hr;
        }

        IntPtr buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
        try
        {
            XblMultiplayerActivityInfo* results;
            nuint count;
            nuint used;
            hr = NativeXbl.XblMultiplayerActivityGetActivityResult(
                (XAsyncBlock*)block,
                size,
                (void*)buffer,
                &results,
                &count,
                &used);
            if (HResult.Failed(hr))
            {
                return hr;
            }

            if (results is null && count != 0)
            {
                return HResult.EPointer;
            }

            var managed = new MultiplayerActivityInfo[checked((int)count)];
            for (int i = 0; i < managed.Length; i++)
            {
                managed[i] = MultiplayerActivityInfo.FromNative(results + i);
            }

            value = new ReadOnlyCollection<MultiplayerActivityInfo>(managed);
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static void ValidateActivityForSet(MultiplayerActivityInfo activityInfo)
    {
        if (activityInfo.XboxUserId == 0)
        {
            throw new ArgumentException("An Xbox user id is required when setting activity.", nameof(activityInfo));
        }

        if (string.IsNullOrEmpty(activityInfo.ConnectionString))
        {
            throw new ArgumentException("A connection string is required when setting activity.", nameof(activityInfo));
        }

        if (string.IsNullOrEmpty(activityInfo.GroupId))
        {
            throw new ArgumentException("A group id is required when setting activity.", nameof(activityInfo));
        }
    }

    private static ulong[] ToArray(IEnumerable<ulong> values)
    {
        if (values is ulong[] array)
        {
            return (ulong[])array.Clone();
        }

        if (values is ICollection<ulong> collection)
        {
            var copy = new ulong[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<ulong>(values);
        return list.ToArray();
    }

    private static XblMultiplayerActivityRecentPlayerUpdate[] ToNativeUpdates(
        IEnumerable<MultiplayerActivityRecentPlayerUpdate> values)
    {
        if (values is ICollection<MultiplayerActivityRecentPlayerUpdate> collection)
        {
            var native = new XblMultiplayerActivityRecentPlayerUpdate[collection.Count];
            int index = 0;
            foreach (MultiplayerActivityRecentPlayerUpdate value in collection)
            {
                native[index++] = ToNative(value);
            }

            return native;
        }

        var list = new List<XblMultiplayerActivityRecentPlayerUpdate>();
        foreach (MultiplayerActivityRecentPlayerUpdate value in values)
        {
            list.Add(ToNative(value));
        }

        return list.ToArray();
    }

    private static XblMultiplayerActivityRecentPlayerUpdate ToNative(MultiplayerActivityRecentPlayerUpdate value)
    {
        if (value is null)
        {
            throw new ArgumentException("Recent-player updates cannot contain null.", nameof(value));
        }

        return new XblMultiplayerActivityRecentPlayerUpdate
        {
            Xuid = value.XboxUserId,
            EncounterType = (XblMultiplayerActivityEncounterType)value.EncounterType,
        };
    }

    private static IntPtr AllocateUlongs(ulong[] values)
    {
        IntPtr buffer = Marshal.AllocHGlobal(checked(values.Length * sizeof(ulong)));
        for (int i = 0; i < values.Length; i++)
        {
            ((ulong*)buffer)[i] = values[i];
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

    private sealed class NativeActivityInfo : IDisposable
    {
        private IntPtr _native;
        private IntPtr _connectionString;
        private IntPtr _groupId;

        internal NativeActivityInfo(MultiplayerActivityInfo value)
        {
            _native = Marshal.AllocHGlobal(sizeof(XblMultiplayerActivityInfo));
            *Pointer = default;

            try
            {
                _connectionString = Utf8.Allocate(value.ConnectionString);
                _groupId = Utf8.Allocate(value.GroupId);

                Pointer->Xuid = value.XboxUserId;
                Pointer->ConnectionString = (byte*)_connectionString;
                Pointer->JoinRestriction = (XblMultiplayerActivityJoinRestriction)value.JoinRestriction;
                Pointer->MaxPlayers = (nuint)value.MaxPlayers;
                Pointer->CurrentPlayers = (nuint)value.CurrentPlayers;
                Pointer->GroupId = (byte*)_groupId;
                Pointer->Platform = (XblMultiplayerActivityPlatform)value.Platform;
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal XblMultiplayerActivityInfo* Pointer => (XblMultiplayerActivityInfo*)_native;

        public void Dispose()
        {
            Utf8.Free(_connectionString);
            _connectionString = IntPtr.Zero;

            Utf8.Free(_groupId);
            _groupId = IntPtr.Zero;

            if (_native != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_native);
                _native = IntPtr.Zero;
            }
        }
    }
}
