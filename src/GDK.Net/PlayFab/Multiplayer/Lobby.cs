using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// A PlayFab lobby (<c>PFLobbyHandle</c>). Instances are identity-mapped by their owning
/// <see cref="PlayFabMultiplayer"/>, so the same native lobby always surfaces as the same object.
/// </summary>
/// <remarks>
/// <para>
/// A lobby's handle is owned by the multiplayer library, not by this wrapper, so there is nothing
/// to dispose. The lobby stays usable until its teardown change arrives —
/// <see cref="LeaveLobbyCompleted"/>, <see cref="LobbyDisconnected"/>,
/// <see cref="ServerLeaveLobbyAsServerCompleted"/> or <see cref="ServerDeleteLobbyCompleted"/> —
/// after which every member throws <see cref="ObjectDisposedException"/>.
/// </para>
/// <para>
/// Every getter snapshots into managed memory, so the values it returns stay valid outside the
/// state-change loop.
/// </para>
/// </remarks>
public sealed unsafe class Lobby
{
    private readonly PlayFabMultiplayer _owner;
    private bool _invalidated;

    internal Lobby(PlayFabMultiplayer owner, IntPtr handle)
    {
        _owner = owner;
        RawHandle = handle;
    }

    internal IntPtr RawHandle { get; }

    internal IntPtr Handle
    {
        get
        {
            if (_invalidated)
            {
                throw new ObjectDisposedException(nameof(Lobby));
            }

            return RawHandle;
        }
    }

    /// <summary>Whether the lobby is still usable.</summary>
    public bool IsValid => !_invalidated;

    /// <summary>The lobby's PlayFab id (<c>PFLobbyGetLobbyId</c>).</summary>
    public string? Id => ReadString(NativePlayFab.PFLobbyGetLobbyId);

    /// <summary>The connection string used to invite others (<c>PFLobbyGetConnectionString</c>).</summary>
    public string? ConnectionString => ReadString(NativePlayFab.PFLobbyGetConnectionString);

    /// <summary>The maximum number of members (<c>PFLobbyGetMaxMemberCount</c>).</summary>
    public uint MaxMemberCount
    {
        get
        {
            uint value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetMaxMemberCount(Handle, &value));
            return value;
        }
    }

    /// <summary>The lobby owner, or <see langword="null"/> when it has none (<c>PFLobbyGetOwner</c>).</summary>
    public EntityKey? Owner
    {
        get
        {
            PFEntityKey* value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetOwner(Handle, &value));
            return value is null ? null : EntityKey.FromNative(value);
        }
    }

    /// <summary>How ownership moves when the owner leaves (<c>PFLobbyGetOwnerMigrationPolicy</c>).</summary>
    public LobbyOwnerMigrationPolicy OwnerMigrationPolicy
    {
        get
        {
            PFLobbyOwnerMigrationPolicy value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetOwnerMigrationPolicy(Handle, &value));
            return (LobbyOwnerMigrationPolicy)value;
        }
    }

    /// <summary>Who may find and join the lobby (<c>PFLobbyGetAccessPolicy</c>).</summary>
    public LobbyAccessPolicy AccessPolicy
    {
        get
        {
            PFLobbyAccessPolicy value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetAccessPolicy(Handle, &value));
            return (LobbyAccessPolicy)value;
        }
    }

    /// <summary>Whether new members may join (<c>PFLobbyGetMembershipLock</c>).</summary>
    public LobbyMembershipLock MembershipLock
    {
        get
        {
            PFLobbyMembershipLock value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetMembershipLock(Handle, &value));
            return (LobbyMembershipLock)value;
        }
    }

    /// <summary>
    /// Whether only the owner may send invites (<c>PFLobbyGetRestrictInvitesToLobbyOwner</c>).
    /// </summary>
    public bool RestrictInvitesToLobbyOwner
    {
        get
        {
            byte value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetRestrictInvitesToLobbyOwner(Handle, &value));
            return value != 0;
        }
    }

    /// <summary>A snapshot of the lobby's members (<c>PFLobbyGetMembers</c>).</summary>
    public IReadOnlyList<EntityKey> Members
    {
        get
        {
            uint count;
            PFEntityKey* members;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetMembers(Handle, &count, &members));
            var list = new List<EntityKey>((int)count);
            for (uint i = 0; i < count; i++)
            {
                list.Add(EntityKey.FromNative(&members[i]));
            }

            return list;
        }
    }

    /// <summary>The server entity, when the lobby has one (<c>PFLobbyGetServer</c>).</summary>
    public EntityKey? Server
    {
        get
        {
            PFEntityKey* value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetServer(Handle, &value));
            return value is null ? null : EntityKey.FromNative(value);
        }
    }

    /// <summary>The server's connection status (<c>PFLobbyGetServerConnectionStatus</c>).</summary>
    public LobbyServerConnectionStatus ServerConnectionStatus
    {
        get
        {
            PFLobbyServerConnectionStatus value;
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyGetServerConnectionStatus(Handle, &value));
            return (LobbyServerConnectionStatus)value;
        }
    }

    /// <summary>
    /// The lobby's searchable properties
    /// (<c>PFLobbyGetSearchPropertyKeys</c>, <c>PFLobbyGetSearchProperty</c>).
    /// </summary>
    public IReadOnlyDictionary<string, string> SearchProperties => ReadProperties(
        NativePlayFab.PFLobbyGetSearchPropertyKeys, NativePlayFab.PFLobbyGetSearchProperty);

    /// <summary>
    /// The lobby's shared properties
    /// (<c>PFLobbyGetLobbyPropertyKeys</c>, <c>PFLobbyGetLobbyProperty</c>).
    /// </summary>
    public IReadOnlyDictionary<string, string> Properties => ReadProperties(
        NativePlayFab.PFLobbyGetLobbyPropertyKeys, NativePlayFab.PFLobbyGetLobbyProperty);

    /// <summary>
    /// The server's properties
    /// (<c>PFLobbyGetServerPropertyKeys</c>, <c>PFLobbyGetServerProperty</c>).
    /// </summary>
    public IReadOnlyDictionary<string, string> ServerProperties => ReadProperties(
        NativePlayFab.PFLobbyGetServerPropertyKeys, NativePlayFab.PFLobbyGetServerProperty);

    /// <summary>
    /// A member's properties
    /// (<c>PFLobbyGetMemberPropertyKeys</c>, <c>PFLobbyGetMemberProperty</c>).
    /// </summary>
    public IReadOnlyDictionary<string, string> GetMemberProperties(EntityKey member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        IntPtr handle = Handle;
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeMember = arena.Alloc<PFEntityKey>(1);
            member.WriteTo(nativeMember, arena);

            uint count;
            byte** keys;
            Hr.ThrowIfFailed(
                NativePlayFab.PFLobbyGetMemberPropertyKeys(handle, nativeMember, &count, &keys));

            var map = new Dictionary<string, string>((int)count, StringComparer.Ordinal);
            for (uint i = 0; i < count; i++)
            {
                string? key = Utf8.ToString(keys[i]);
                if (key is null)
                {
                    continue;
                }

                byte* value;
                Hr.ThrowIfFailed(
                    NativePlayFab.PFLobbyGetMemberProperty(handle, nativeMember, keys[i], &value));
                map[key] = Utf8.ToString(value) ?? string.Empty;
            }

            return map;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>A member's connection status (<c>PFLobbyGetMemberConnectionStatus</c>).</summary>
    public LobbyMemberConnectionStatus GetMemberConnectionStatus(EntityKey member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        IntPtr handle = Handle;
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeMember = arena.Alloc<PFEntityKey>(1);
            member.WriteTo(nativeMember, arena);

            PFLobbyMemberConnectionStatus status;
            Hr.ThrowIfFailed(
                NativePlayFab.PFLobbyGetMemberConnectionStatus(handle, nativeMember, &status));
            return (LobbyMemberConnectionStatus)status;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Adds another local member to this lobby (<c>PFLobbyAddMember</c>). Completes with
    /// <see cref="AddMemberCompleted"/> on a later pump.
    /// </summary>
    public OperationId AddMember(
        EntityKey localUser, IReadOnlyDictionary<string, string>? memberProperties = null)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeUser = MultiplayerInterop.Write(arena, localUser);
            MultiplayerInterop.WriteProperties(
                arena, memberProperties, out uint count, out byte** keys, out byte** values);
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyAddMember(
                handle, nativeUser, count, keys, values, PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Removes another member (<c>PFLobbyForceRemoveMember</c>). Completes with
    /// <see cref="ForceRemoveMemberCompleted"/> on a later pump.
    /// </summary>
    public OperationId ForceRemoveMember(EntityKey targetMember, bool preventRejoin = false)
    {
        if (targetMember is null)
        {
            throw new ArgumentNullException(nameof(targetMember));
        }

        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeTarget = MultiplayerInterop.Write(arena, targetMember);
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyForceRemoveMember(
                handle,
                nativeTarget,
                preventRejoin ? (byte)1 : (byte)0,
                PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Leaves the lobby (<c>PFLobbyLeave</c>). Completes with <see cref="LeaveLobbyCompleted"/>.
    /// </summary>
    /// <param name="localUser">
    /// The local member to remove, or <see langword="null"/> to remove every local member.
    /// </param>
    public OperationId Leave(EntityKey? localUser = null)
    {
        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeUser = MultiplayerInterop.Write(arena, localUser);
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyLeave(
                handle, nativeUser, PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Posts an update to the lobby's shared or member data (<c>PFLobbyPostUpdate</c>). Completes
    /// with <see cref="PostUpdateCompleted"/>.
    /// </summary>
    public OperationId PostUpdate(
        EntityKey localUser,
        LobbyDataUpdate? lobbyUpdate = null,
        LobbyMemberDataUpdate? memberUpdate = null)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeUser = MultiplayerInterop.Write(arena, localUser);

            PFLobbyDataUpdate* nativeLobby = null;
            if (lobbyUpdate is not null)
            {
                nativeLobby = arena.Alloc<PFLobbyDataUpdate>(1);
                lobbyUpdate.WriteTo(nativeLobby, arena);
            }

            PFLobbyMemberDataUpdate* nativeMember = null;
            if (memberUpdate is not null)
            {
                nativeMember = arena.Alloc<PFLobbyMemberDataUpdate>(1);
                memberUpdate.WriteTo(nativeMember, arena);
            }

            Hr.ThrowIfFailed(NativePlayFab.PFLobbyPostUpdate(
                handle,
                nativeUser,
                nativeLobby,
                nativeMember,
                PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Invites another entity to the lobby (<c>PFLobbySendInvite</c>). Completes with
    /// <see cref="SendInviteCompleted"/>.
    /// </summary>
    public OperationId SendInvite(EntityKey sender, EntityKey invitee)
    {
        if (sender is null)
        {
            throw new ArgumentNullException(nameof(sender));
        }

        if (invitee is null)
        {
            throw new ArgumentNullException(nameof(invitee));
        }

        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFEntityKey* nativeSender = MultiplayerInterop.Write(arena, sender);
            PFEntityKey* nativeInvitee = MultiplayerInterop.Write(arena, invitee);
            Hr.ThrowIfFailed(NativePlayFab.PFLobbySendInvite(
                handle, nativeSender, nativeInvitee, PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Posts a lobby update on behalf of the game server (<c>PFLobbyServerPostUpdate</c>).
    /// Completes with <see cref="ServerPostUpdateCompleted"/>.
    /// </summary>
    public OperationId ServerPostUpdate(LobbyDataUpdate lobbyUpdate)
    {
        if (lobbyUpdate is null)
        {
            throw new ArgumentNullException(nameof(lobbyUpdate));
        }

        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyDataUpdate* native = arena.Alloc<PFLobbyDataUpdate>(1);
            lobbyUpdate.WriteTo(native, arena);
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyServerPostUpdate(
                handle, native, PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Posts a server-data update (<c>PFLobbyServerPostUpdateAsServer</c>). Completes with
    /// <see cref="ServerPostUpdateAsServerCompleted"/>.
    /// </summary>
    public OperationId ServerPostUpdateAsServer(LobbyServerDataUpdate serverUpdate)
    {
        if (serverUpdate is null)
        {
            throw new ArgumentNullException(nameof(serverUpdate));
        }

        IntPtr handle = Handle;
        OperationId operation = _owner.NextOperationId();
        var arena = new PlayFabArena();
        try
        {
            PFLobbyServerDataUpdate* native = arena.Alloc<PFLobbyServerDataUpdate>(1);
            serverUpdate.WriteTo(native, arena);
            Hr.ThrowIfFailed(NativePlayFab.PFLobbyServerPostUpdateAsServer(
                handle, native, PlayFabMultiplayer.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Removes the game server from the lobby (<c>PFLobbyServerLeaveAsServer</c>). Completes with
    /// <see cref="ServerLeaveLobbyAsServerCompleted"/>.
    /// </summary>
    public OperationId ServerLeaveAsServer()
    {
        OperationId operation = _owner.NextOperationId();
        Hr.ThrowIfFailed(NativePlayFab.PFLobbyServerLeaveAsServer(
            Handle, PlayFabMultiplayer.Context(operation)));
        return operation;
    }

    /// <summary>
    /// Deletes the lobby (<c>PFLobbyServerDeleteLobby</c>). Completes with
    /// <see cref="ServerDeleteLobbyCompleted"/>.
    /// </summary>
    public OperationId ServerDeleteLobby()
    {
        OperationId operation = _owner.NextOperationId();
        Hr.ThrowIfFailed(NativePlayFab.PFLobbyServerDeleteLobby(
            Handle, PlayFabMultiplayer.Context(operation)));
        return operation;
    }

    internal void Invalidate() => _invalidated = true;

    private string? ReadString(StringGetter getter)
    {
        byte* value;
        Hr.ThrowIfFailed(getter(Handle, &value));
        return Utf8.ToString(value);
    }

    private IReadOnlyDictionary<string, string> ReadProperties(
        KeysGetter keysGetter, PropertyGetter propertyGetter)
    {
        IntPtr handle = Handle;
        uint count;
        byte** keys;
        Hr.ThrowIfFailed(keysGetter(handle, &count, &keys));

        var map = new Dictionary<string, string>((int)count, StringComparer.Ordinal);
        for (uint i = 0; i < count; i++)
        {
            string? key = Utf8.ToString(keys[i]);
            if (key is null)
            {
                continue;
            }

            byte* value;
            Hr.ThrowIfFailed(propertyGetter(handle, keys[i], &value));
            map[key] = Utf8.ToString(value) ?? string.Empty;
        }

        return map;
    }

    private delegate int StringGetter(IntPtr lobby, byte** value);

    private delegate int KeysGetter(IntPtr lobby, uint* count, byte*** keys);

    private delegate int PropertyGetter(IntPtr lobby, byte* key, byte** value);
}
