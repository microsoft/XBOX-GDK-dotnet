using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.XboxLive;

/// <summary>Process-global Xbox Live social manager. Mirrors <c>social_manager_c.h</c>.</summary>
/// <remarks>
/// <para>
/// Social manager is pumped: nothing happens: no local-user completions, group updates, rich
/// presence polling or notifications, unless the title calls <see cref="DoWork"/> regularly,
/// ideally once per frame.
/// </para>
/// <para>
/// This is XSAPI's first DoWork-pumped manager in this projection, and it differs from the
/// Start/Finish state-change systems: <c>XblSocialManagerDoWork</c> has no <c>Finish</c>. Native
/// events remain valid only until the next DoWork call, so this method deliberately snapshots the
/// batch into managed <see cref="SocialManagerEvent"/> records before returning. Long-lived social
/// user groups are not snapshotted; they are identity-mapped wrappers around native handles.
/// </para>
/// <para>
/// <see cref="DoWork"/> is not thread-safe against other social-manager calls. Drive the manager
/// from one thread and keep all social-manager calls on that thread.
/// </para>
/// </remarks>
public sealed unsafe class SocialManager
{
    private const int MaxUsersFromList = 100;

    private readonly XboxLiveService _service;
    private readonly ConcurrentDictionary<IntPtr, User> _localUsers = new();
    private readonly ConcurrentDictionary<IntPtr, SocialManagerUserGroup> _groups = new();

    internal SocialManager(XboxLiveService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>The number of local users currently tracked by social manager.</summary>
    public ulong LocalUserCount
    {
        get
        {
            ThrowIfNotInitialized();
            return (ulong)NativeXbl.XblSocialManagerGetLocalUserCount();
        }
    }

    /// <summary>
    /// Adds a local user to social manager (<c>XblSocialManagerAddLocalUser</c>).
    /// </summary>
    /// <remarks>
    /// The user must remain alive while it is added to social manager; call
    /// <see cref="RemoveLocalUser"/> before disposing the <see cref="User"/>. Completion is
    /// reported later as a <see cref="LocalUserAddedSocialManagerEvent"/> from <see cref="DoWork"/>.
    /// </remarks>
    /// <param name="user">The local user to add.</param>
    /// <param name="extraDetailLevel">Extra profile fields to populate for graph users.</param>
    public void AddLocalUser(
        User user,
        SocialManagerExtraDetailLevel extraDetailLevel = SocialManagerExtraDetailLevel.NoExtraDetail)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();

        IntPtr handle = user.Handle;
        Hr.ThrowIfFailed(
            NativeXbl.XblSocialManagerAddLocalUser(
                handle,
                (XblSocialManagerExtraDetailLevel)extraDetailLevel,
                IntPtr.Zero));

        _localUsers[handle] = user;
    }

    /// <summary>Removes a local user from social manager (<c>XblSocialManagerRemoveLocalUser</c>).</summary>
    /// <remarks>
    /// Removing a local user also destroys its social user groups. The corresponding managed group
    /// wrappers are invalidated and any further use throws <see cref="ObjectDisposedException"/>.
    /// </remarks>
    /// <param name="user">The user to remove. It must not already be disposed.</param>
    public void RemoveLocalUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();

        IntPtr handle = user.Handle;
        Hr.ThrowIfFailed(NativeXbl.XblSocialManagerRemoveLocalUser(handle));
        _localUsers.TryRemove(handle, out _);
        InvalidateGroupsForLocalUser(user);
    }

    /// <summary>Returns the local users currently tracked by social manager.</summary>
    /// <remarks>
    /// XSAPI returns borrowed handles that must not be closed. This method resolves those handles
    /// back to the <see cref="User"/> instances passed to <see cref="AddLocalUser"/> when
    /// possible; if an unknown handle is returned, it is duplicated before being wrapped.
    /// </remarks>
    public IReadOnlyList<User> GetLocalUsers()
    {
        ThrowIfNotInitialized();

        nuint nativeCount = NativeXbl.XblSocialManagerGetLocalUserCount();
        int count = ToInt32(nativeCount, "local user count");
        if (count == 0)
        {
            return Array.Empty<User>();
        }

        IntPtr* users = stackalloc IntPtr[count];
        Hr.ThrowIfFailed(NativeXbl.XblSocialManagerGetLocalUsers(nativeCount, users));

        var managed = new User[count];
        for (int i = 0; i < managed.Length; i++)
        {
            managed[i] = ResolveLocalUser(users[i]);
        }

        return new ReadOnlyCollection<User>(managed);
    }

    /// <summary>
    /// Creates a filter-backed social user group
    /// (<c>XblSocialManagerCreateSocialUserGroupFromFilters</c>).
    /// </summary>
    /// <remarks>
    /// The returned group exists immediately but is empty until a
    /// <see cref="SocialUserGroupLoadedSocialManagerEvent"/> is returned from <see cref="DoWork"/>.
    /// </remarks>
    public SocialManagerUserGroup CreateSocialUserGroupFromFilters(
        User user,
        PresenceFilter presenceFilter,
        RelationshipFilter relationshipFilter)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();

        IntPtr raw;
        Hr.ThrowIfFailed(
            NativeXbl.XblSocialManagerCreateSocialUserGroupFromFilters(
                user.Handle,
                (XblSocialManagerPresenceFilter)presenceFilter,
                (XblSocialManagerRelationshipFilter)relationshipFilter,
                &raw));

        SocialManagerUserGroup group = GetOrCreateGroup(raw);
        group.SetLocalUser(user);
        return group;
    }

    /// <summary>
    /// Creates a list-backed social user group
    /// (<c>XblSocialManagerCreateSocialUserGroupFromList</c>).
    /// </summary>
    /// <remarks>
    /// The list cannot exceed 100 Xbox user ids. The returned group exists immediately but is
    /// empty until a <see cref="SocialUserGroupLoadedSocialManagerEvent"/> is returned from
    /// <see cref="DoWork"/>.
    /// </remarks>
    public SocialManagerUserGroup CreateSocialUserGroupFromList(User user, IEnumerable<ulong> xboxUserIds)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ulong[] xuids = ToXuidArray(xboxUserIds);
        ThrowIfNotInitialized();

        IntPtr raw;
        fixed (ulong* pinned = xuids)
        {
            Hr.ThrowIfFailed(
                NativeXbl.XblSocialManagerCreateSocialUserGroupFromList(
                    user.Handle,
                    xuids.Length == 0 ? null : pinned,
                    (nuint)xuids.Length,
                    &raw));
        }

        SocialManagerUserGroup group = GetOrCreateGroup(raw);
        group.SetLocalUser(user);
        return group;
    }

    /// <summary>Destroys a social user group (<c>XblSocialManagerDestroySocialUserGroup</c>).</summary>
    /// <param name="group">The group to destroy.</param>
    public void DestroySocialUserGroup(SocialManagerUserGroup group)
    {
        if (group is null)
        {
            throw new ArgumentNullException(nameof(group));
        }

        if (!group.TryMarkDisposed(out IntPtr handle))
        {
            return;
        }

        try
        {
            ThrowIfNotInitialized();
            Hr.ThrowIfFailed(NativeXbl.XblSocialManagerDestroySocialUserGroup(handle));
        }
        finally
        {
            _groups.TryRemove(handle, out _);
            group.Invalidate();
        }
    }

    /// <summary>
    /// Enables or disables XSAPI rich-presence polling for a local user
    /// (<c>XblSocialManagerSetRichPresencePollingStatus</c>).
    /// </summary>
    public void SetRichPresencePollingStatus(User user, bool shouldEnablePolling)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();
        Hr.ThrowIfFailed(
            NativeXbl.XblSocialManagerSetRichPresencePollingStatus(
                user.Handle,
                shouldEnablePolling ? (byte)1 : (byte)0));
    }

    /// <summary>
    /// Pumps social manager and returns a managed snapshot of the events in native order.
    /// </summary>
    /// <remarks>
    /// Call this regularly, ideally once per frame. Unlike the Start/Finish state-change pattern
    /// used by PFMP and Party, XSAPI social manager has no <c>Finish</c> call; native events are
    /// valid only until the next <c>XblSocialManagerDoWork</c>. This method therefore snapshots the
    /// batch before returning instead of exposing borrowed views.
    /// </remarks>
    public IReadOnlyList<SocialManagerEvent> DoWork()
    {
        ThrowIfNotInitialized();

        XblSocialManagerEvent* nativeEvents;
        nuint nativeCount;
        Hr.ThrowIfFailed(NativeXbl.XblSocialManagerDoWork(&nativeEvents, &nativeCount));

        int count = ToInt32(nativeCount, "social-manager event count");
        if (nativeEvents is null || count == 0)
        {
            return Array.Empty<SocialManagerEvent>();
        }

        var events = new SocialManagerEvent[count];
        for (int i = 0; i < events.Length; i++)
        {
            events[i] = SnapshotEvent(nativeEvents + i);
        }

        return new ReadOnlyCollection<SocialManagerEvent>(events);
    }

    internal SocialUserGroupType GetGroupType(SocialManagerUserGroup group)
    {
        ThrowIfNotInitialized();

        XblSocialUserGroupType type;
        Hr.ThrowIfFailed(NativeXbl.XblSocialManagerUserGroupGetType(group.Handle, &type));
        return (SocialUserGroupType)type;
    }

    internal User GetGroupLocalUser(SocialManagerUserGroup group)
    {
        ThrowIfNotInitialized();

        group.ThrowIfDisposed();
        if (group.CachedLocalUser is not null)
        {
            return group.CachedLocalUser;
        }

        IntPtr user;
        Hr.ThrowIfFailed(NativeXbl.XblSocialManagerUserGroupGetLocalUser(group.Handle, &user));
        User resolved = ResolveLocalUser(user);
        group.SetLocalUser(resolved);
        return resolved;
    }

    internal SocialManagerUserGroupFilters? GetGroupFilters(SocialManagerUserGroup group)
    {
        if (GetGroupType(group) != SocialUserGroupType.Filter)
        {
            return null;
        }

        XblSocialManagerPresenceFilter presenceFilter;
        XblSocialManagerRelationshipFilter relationshipFilter;
        Hr.ThrowIfFailed(
            NativeXbl.XblSocialManagerUserGroupGetFilters(
                group.Handle,
                &presenceFilter,
                &relationshipFilter));

        return new SocialManagerUserGroupFilters(
            (PresenceFilter)presenceFilter,
            (RelationshipFilter)relationshipFilter);
    }

    internal IReadOnlyList<SocialManagerUser> GetGroupUsers(SocialManagerUserGroup group)
    {
        ThrowIfNotInitialized();

        XblSocialManagerUser** nativeUsers;
        nuint nativeCount;
        Hr.ThrowIfFailed(NativeXbl.XblSocialManagerUserGroupGetUsers(group.Handle, &nativeUsers, &nativeCount));
        return SnapshotUsers(nativeUsers, nativeCount);
    }

    internal IReadOnlyList<ulong> GetUsersTrackedByGroup(SocialManagerUserGroup group)
    {
        ThrowIfNotInitialized();

        ulong* nativeUsers;
        nuint nativeCount;
        Hr.ThrowIfFailed(
            NativeXbl.XblSocialManagerUserGroupGetUsersTrackedByGroup(group.Handle, &nativeUsers, &nativeCount));

        int count = ToInt32(nativeCount, "tracked user count");
        if (nativeUsers is null || count == 0)
        {
            return Array.Empty<ulong>();
        }

        var xuids = new ulong[count];
        for (int i = 0; i < xuids.Length; i++)
        {
            xuids[i] = nativeUsers[i];
        }

        return new ReadOnlyCollection<ulong>(xuids);
    }

    internal void UpdateSocialUserGroup(SocialManagerUserGroup group, IEnumerable<ulong> xboxUserIds)
    {
        ulong[] xuids = ToXuidArray(xboxUserIds);
        ThrowIfNotInitialized();

        fixed (ulong* pinned = xuids)
        {
            Hr.ThrowIfFailed(
                NativeXbl.XblSocialManagerUpdateSocialUserGroup(
                    group.Handle,
                    xuids.Length == 0 ? null : pinned,
                    (nuint)xuids.Length));
        }
    }

    private SocialManagerEvent SnapshotEvent(XblSocialManagerEvent* native)
    {
        User? localUser = native->User == IntPtr.Zero ? null : ResolveLocalUser(native->User);
        GameRuntimeException? error = HResult.Failed(native->Hr) ? new GameRuntimeException(native->Hr) : null;
        IReadOnlyList<SocialManagerUser> users = SnapshotAffectedUsers(native);

        switch (native->EventType)
        {
            case XblSocialManagerEventType.UsersAddedToSocialGraph:
                return new UsersAddedToSocialGraphSocialManagerEvent(localUser, users);
            case XblSocialManagerEventType.UsersRemovedFromSocialGraph:
                return new UsersRemovedFromSocialGraphSocialManagerEvent(localUser, users);
            case XblSocialManagerEventType.PresenceChanged:
                return new PresenceChangedSocialManagerEvent(localUser, users);
            case XblSocialManagerEventType.ProfilesChanged:
                return new ProfilesChangedSocialManagerEvent(localUser, users);
            case XblSocialManagerEventType.SocialRelationshipsChanged:
                return new SocialRelationshipsChangedSocialManagerEvent(localUser, users);
            case XblSocialManagerEventType.LocalUserAdded:
                return new LocalUserAddedSocialManagerEvent(localUser, error);
            case XblSocialManagerEventType.SocialUserGroupLoaded:
                SocialManagerUserGroup? loadedGroup = TryGetGroup(native->GroupAffected);
                loadedGroup?.SetLocalUser(localUser);
                return new SocialUserGroupLoadedSocialManagerEvent(localUser, error, loadedGroup);
            case XblSocialManagerEventType.SocialUserGroupUpdated:
                SocialManagerUserGroup? updatedGroup = TryGetGroup(native->GroupAffected);
                updatedGroup?.SetLocalUser(localUser);
                return new SocialUserGroupUpdatedSocialManagerEvent(localUser, error, updatedGroup);
            default:
                return new UnknownSocialManagerEvent(localUser, error);
        }
    }

    private IReadOnlyList<SocialManagerUser> SnapshotAffectedUsers(XblSocialManagerEvent* native)
    {
        IntPtr* first = &native->UsersAffected0;
        var users = new List<SocialManagerUser>(XblSocialManagerEvent.MaxAffectedUsers);
        for (int i = 0; i < XblSocialManagerEvent.MaxAffectedUsers; i++)
        {
            var user = (XblSocialManagerUser*)first[i];
            if (user is not null)
            {
                users.Add(SocialManagerUser.FromNative(user));
            }
        }

        return new ReadOnlyCollection<SocialManagerUser>(users);
    }

    private static IReadOnlyList<SocialManagerUser> SnapshotUsers(
        XblSocialManagerUser** nativeUsers,
        nuint nativeCount)
    {
        int count = ToInt32(nativeCount, "social-manager user count");
        if (nativeUsers is null || count == 0)
        {
            return Array.Empty<SocialManagerUser>();
        }

        var users = new SocialManagerUser[count];
        for (int i = 0; i < users.Length; i++)
        {
            users[i] = SocialManagerUser.FromNative(nativeUsers[i]);
        }

        return new ReadOnlyCollection<SocialManagerUser>(users);
    }

    private SocialManagerUserGroup GetOrCreateGroup(IntPtr handle)
    {
        if (handle == IntPtr.Zero)
        {
            throw new GameRuntimeException(HResult.EFail, "XSAPI returned a null social user group handle.");
        }

        return _groups.GetOrAdd(handle, key => new SocialManagerUserGroup(this, key));
    }

    private SocialManagerUserGroup? TryGetGroup(IntPtr handle) =>
        handle == IntPtr.Zero ? null : GetOrCreateGroup(handle);

    private User ResolveLocalUser(IntPtr handle)
    {
        if (handle == IntPtr.Zero)
        {
            throw new GameRuntimeException(HResult.EFail, "XSAPI returned a null local user handle.");
        }

        if (_localUsers.TryGetValue(handle, out User? user))
        {
            return user;
        }

        IntPtr duplicate;
        Hr.ThrowIfFailed(Native.XUserDuplicateHandle(handle, &duplicate));
        return new User(new UserHandle(duplicate), null);
    }

    private void InvalidateGroupsForLocalUser(User user)
    {
        foreach (KeyValuePair<IntPtr, SocialManagerUserGroup> entry in _groups)
        {
            SocialManagerUserGroup group = entry.Value;
            if (group.CachedLocalUser == user)
            {
                group.Invalidate();
                _groups.TryRemove(entry.Key, out _);
            }
        }
    }

    private static ulong[] ToXuidArray(IEnumerable<ulong> xboxUserIds)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        if (xboxUserIds is ICollection<ulong> collection)
        {
            if (collection.Count > MaxUsersFromList)
            {
                throw new ArgumentException("A social-manager list group cannot contain more than 100 users.", nameof(xboxUserIds));
            }

            var array = new ulong[collection.Count];
            collection.CopyTo(array, 0);
            return array;
        }

        var users = new List<ulong>();
        foreach (ulong xuid in xboxUserIds)
        {
            if (users.Count == MaxUsersFromList)
            {
                throw new ArgumentException("A social-manager list group cannot contain more than 100 users.", nameof(xboxUserIds));
            }

            users.Add(xuid);
        }

        return users.ToArray();
    }

    private static int ToInt32(nuint value, string description)
    {
        if (value > (nuint)int.MaxValue)
        {
            throw new InvalidOperationException($"The {description} does not fit in a managed array.");
        }

        return (int)value;
    }

    private void ThrowIfNotInitialized()
    {
        if (!_service.IsInitialized)
        {
            throw new InvalidOperationException(
                "Xbox Live Services are not initialized. Call GameRuntime.XboxLive.Initialize first.");
        }
    }
}
