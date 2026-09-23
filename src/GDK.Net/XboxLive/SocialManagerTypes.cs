using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.XboxLive;

/// <summary>Extra social graph detail to load for a local user. Mirrors <c>XblSocialManagerExtraDetailLevel</c>.</summary>
[Flags]
public enum SocialManagerExtraDetailLevel : uint
{
    /// <summary>Only default People Hub information, such as profile and presence.</summary>
    NoExtraDetail = 0,

    /// <summary>Include title-history data for users in the graph.</summary>
    TitleHistory = 1,

    /// <summary>Include preferred shell color data for users in the graph.</summary>
    PreferredColor = 2,

    /// <summary>Include every supported extra-detail field.</summary>
    All = 3,
}

/// <summary>Presence filter for a social-manager filter group. Mirrors <c>XblPresenceFilter</c>.</summary>
public enum PresenceFilter : uint
{
    /// <summary>The filter is unknown.</summary>
    Unknown = 0,

    /// <summary>Users currently online and playing this title.</summary>
    TitleOnline = 1,

    /// <summary>Users offline who have played this title.</summary>
    TitleOffline = 2,

    /// <summary>Users online outside this title who have played it.</summary>
    TitleOnlineOutsideTitle = 3,

    /// <summary>All online users.</summary>
    AllOnline = 4,

    /// <summary>All offline users.</summary>
    AllOffline = 5,

    /// <summary>All users who have played or are playing this title.</summary>
    AllTitle = 6,

    /// <summary>All users.</summary>
    All = 7,
}

/// <summary>Relationship filter for a social-manager filter group. Mirrors <c>XblRelationshipFilter</c>.</summary>
public enum RelationshipFilter : uint
{
    /// <summary>The filter is unknown.</summary>
    Unknown = 0,

    /// <summary>Friends of the local user.</summary>
    Friends = 1,

    /// <summary>Favorites of the local user.</summary>
    Favorite = 2,
}

/// <summary>How a social-manager user group was created. Mirrors <c>XblSocialUserGroupType</c>.</summary>
public enum SocialUserGroupType : uint
{
    /// <summary>The group is backed by presence and relationship filters.</summary>
    Filter = 0,

    /// <summary>The group is backed by an explicit Xbox user id list.</summary>
    UserList = 1,
}

/// <summary>Native social-manager event kind. Mirrors <c>XblSocialManagerEventType</c>.</summary>
public enum SocialManagerEventType : uint
{
    /// <summary>One or more users were added to the social graph.</summary>
    UsersAddedToSocialGraph = 0,

    /// <summary>One or more users were removed from the social graph.</summary>
    UsersRemovedFromSocialGraph = 1,

    /// <summary>One or more users' presence changed.</summary>
    PresenceChanged = 2,

    /// <summary>One or more users' profiles changed.</summary>
    ProfilesChanged = 3,

    /// <summary>One or more users' social relationships changed.</summary>
    SocialRelationshipsChanged = 4,

    /// <summary>A local user's initial social graph finished loading.</summary>
    LocalUserAdded = 5,

    /// <summary>A social user group's initial set finished loading.</summary>
    SocialUserGroupLoaded = 6,

    /// <summary>A list-backed social user group finished updating.</summary>
    SocialUserGroupUpdated = 7,

    /// <summary>The event kind is unknown to this projection.</summary>
    Unknown = 8,
}

/// <summary>Filters associated with a filter-backed <see cref="SocialManagerUserGroup"/>.</summary>
public sealed class SocialManagerUserGroupFilters
{
    internal SocialManagerUserGroupFilters(
        PresenceFilter presenceFilter,
        RelationshipFilter relationshipFilter)
    {
        PresenceFilter = presenceFilter;
        RelationshipFilter = relationshipFilter;
    }

    /// <summary>The presence filter used by the group.</summary>
    public PresenceFilter PresenceFilter { get; }

    /// <summary>The relationship filter used by the group.</summary>
    public RelationshipFilter RelationshipFilter { get; }
}

/// <summary>Title-history data attached to a social-manager user.</summary>
public sealed class SocialManagerTitleHistory
{
    internal SocialManagerTitleHistory(bool hasUserPlayed, DateTimeOffset? lastTimeUserPlayed, string lastTimeUserPlayedText)
    {
        HasUserPlayed = hasUserPlayed;
        LastTimeUserPlayed = lastTimeUserPlayed;
        LastTimeUserPlayedText = lastTimeUserPlayedText;
    }

    /// <summary>Whether the user has played this title.</summary>
    public bool HasUserPlayed { get; }

    /// <summary>When the user last played this title, when XSAPI supplied a timestamp.</summary>
    public DateTimeOffset? LastTimeUserPlayed { get; }

    /// <summary>Localized text describing when the user last played this title.</summary>
    public string LastTimeUserPlayedText { get; }

    internal static unsafe SocialManagerTitleHistory FromNative(in XblTitleHistory native)
    {
        fixed (byte* text = native.LastTimeUserPlayedText)
        {
            return new SocialManagerTitleHistory(
                native.HasUserPlayed != 0,
                native.LastTimeUserPlayed == 0 ? null : FromUnixSeconds(native.LastTimeUserPlayed),
                Utf8.ToString(text, XblTitleHistory.LastTimePlayedCharSize));
        }
    }

    internal static DateTimeOffset FromUnixSeconds(long seconds)
    {
        const long MinSeconds = -62135596800;
        const long MaxSeconds = 253402300799;

        if (seconds <= MinSeconds)
        {
            return DateTimeOffset.MinValue;
        }

        return seconds >= MaxSeconds
            ? DateTimeOffset.MaxValue
            : DateTimeOffset.FromUnixTimeSeconds(seconds);
    }
}

/// <summary>Preferred shell colors attached to a social-manager user.</summary>
public sealed class SocialManagerPreferredColor
{
    internal SocialManagerPreferredColor(string primaryColor, string secondaryColor, string tertiaryColor)
    {
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        TertiaryColor = tertiaryColor;
    }

    /// <summary>The user's primary color.</summary>
    public string PrimaryColor { get; }

    /// <summary>The user's secondary color.</summary>
    public string SecondaryColor { get; }

    /// <summary>The user's tertiary color.</summary>
    public string TertiaryColor { get; }

    internal static unsafe SocialManagerPreferredColor FromNative(in XblPreferredColor native)
    {
        fixed (byte* primary = native.PrimaryColor)
        fixed (byte* secondary = native.SecondaryColor)
        fixed (byte* tertiary = native.TertiaryColor)
        {
            return new SocialManagerPreferredColor(
                Utf8.ToString(primary, XblPreferredColor.ColorCharSize),
                Utf8.ToString(secondary, XblPreferredColor.ColorCharSize),
                Utf8.ToString(tertiary, XblPreferredColor.ColorCharSize));
        }
    }
}

/// <summary>One title presence record in a social-manager presence snapshot.</summary>
public sealed class SocialManagerPresenceTitleRecord
{
    internal SocialManagerPresenceTitleRecord(
        uint titleId,
        string titleName,
        bool isTitleActive,
        string presenceText,
        bool isBroadcasting,
        PresenceDeviceType deviceType,
        bool isPrimary)
    {
        TitleId = titleId;
        TitleName = titleName;
        IsTitleActive = isTitleActive;
        PresenceText = presenceText;
        IsBroadcasting = isBroadcasting;
        DeviceType = deviceType;
        IsPrimary = isPrimary;
    }

    /// <summary>The title id.</summary>
    public uint TitleId { get; }

    /// <summary>The localized title name.</summary>
    public string TitleName { get; }

    /// <summary>Whether the user is active in the title.</summary>
    public bool IsTitleActive { get; }

    /// <summary>The formatted localized rich-presence string.</summary>
    public string PresenceText { get; }

    /// <summary>Whether the user is broadcasting this title.</summary>
    public bool IsBroadcasting { get; }

    /// <summary>The device reporting this title presence.</summary>
    public PresenceDeviceType DeviceType { get; }

    /// <summary>Whether this is the user's primary presence record.</summary>
    public bool IsPrimary { get; }

    internal static unsafe SocialManagerPresenceTitleRecord FromNative(XblSocialManagerPresenceTitleRecord* native)
    {
        return new SocialManagerPresenceTitleRecord(
            native->TitleId,
            Utf8.ToString(native->TitleName, XblSocialManagerPresenceTitleRecord.TitleNameCharSize),
            native->IsTitleActive != 0,
            Utf8.ToString(native->PresenceText, XblSocialManagerPresenceTitleRecord.RichPresenceCharSize),
            native->IsBroadcasting != 0,
            (PresenceDeviceType)native->DeviceType,
            native->IsPrimary != 0);
    }
}

/// <summary>A social-manager presence snapshot for one user.</summary>
public sealed class SocialManagerPresenceRecord
{
    internal SocialManagerPresenceRecord(
        PresenceUserState userState,
        IReadOnlyList<SocialManagerPresenceTitleRecord> titleRecords)
    {
        UserState = userState;
        TitleRecords = titleRecords;
    }

    /// <summary>The user's aggregate presence state.</summary>
    public PresenceUserState UserState { get; }

    /// <summary>The title presence records reported for the user.</summary>
    public IReadOnlyList<SocialManagerPresenceTitleRecord> TitleRecords { get; }

    /// <summary>Returns <see langword="true"/> when this snapshot shows the user playing <paramref name="titleId"/>.</summary>
    /// <remarks>
    /// The native <c>XblSocialManagerPresenceRecordIsUserPlayingTitle</c> helper is bound for
    /// completeness, but this method evaluates the managed snapshot so it remains safe after the
    /// next <see cref="SocialManager.DoWork"/> call invalidates native event memory.
    /// </remarks>
    public bool IsUserPlayingTitle(uint titleId)
    {
        for (int i = 0; i < TitleRecords.Count; i++)
        {
            SocialManagerPresenceTitleRecord record = TitleRecords[i];
            if (record.TitleId == titleId && record.IsTitleActive)
            {
                return true;
            }
        }

        return false;
    }

    internal static unsafe SocialManagerPresenceRecord FromNative(XblSocialManagerPresenceRecord* native)
    {
        int count = checked((int)native->PresenceTitleRecordCount);
        if (count > XblSocialManagerPresenceRecord.NumPresenceRecords)
        {
            count = XblSocialManagerPresenceRecord.NumPresenceRecords;
        }

        var records = new SocialManagerPresenceTitleRecord[count];
        byte* fixedRecords = native->PresenceTitleRecords;
        var titleRecords = (XblSocialManagerPresenceTitleRecord*)fixedRecords;
        for (int i = 0; i < records.Length; i++)
        {
            records[i] = SocialManagerPresenceTitleRecord.FromNative(titleRecords + i);
        }

        return new SocialManagerPresenceRecord(
            (PresenceUserState)native->UserState,
            new ReadOnlyCollection<SocialManagerPresenceTitleRecord>(records));
    }
}

/// <summary>An Xbox user in the social-manager graph. Managed snapshot of <c>XblSocialManagerUser</c>.</summary>
public sealed class SocialManagerUser
{
    internal SocialManagerUser(
        ulong xboxUserId,
        bool isFavorite,
        bool isFriend,
        bool isFollowingUser,
        bool isFollowedByCaller,
        string displayName,
        string realName,
        string displayPictureUri,
        bool useAvatar,
        string gamerscore,
        string gamertag,
        string modernGamertag,
        string modernGamertagSuffix,
        string uniqueModernGamertag,
        SocialManagerPresenceRecord presenceRecord,
        SocialManagerTitleHistory titleHistory,
        SocialManagerPreferredColor preferredColor)
    {
        XboxUserId = xboxUserId;
        IsFavorite = isFavorite;
        IsFriend = isFriend;
        IsFollowingUser = isFollowingUser;
        IsFollowedByCaller = isFollowedByCaller;
        DisplayName = displayName;
        RealName = realName;
        DisplayPictureUri = displayPictureUri;
        UseAvatar = useAvatar;
        Gamerscore = gamerscore;
        Gamertag = gamertag;
        ModernGamertag = modernGamertag;
        ModernGamertagSuffix = modernGamertagSuffix;
        UniqueModernGamertag = uniqueModernGamertag;
        PresenceRecord = presenceRecord;
        TitleHistory = titleHistory;
        PreferredColor = preferredColor;
    }

    /// <summary>The user's Xbox user id.</summary>
    public ulong XboxUserId { get; }

    /// <summary>Whether the user is marked as a favorite.</summary>
    public bool IsFavorite { get; }

    /// <summary>Whether this user is a friend of the local user.</summary>
    public bool IsFriend { get; }

    /// <summary>Compatibility field derived by XSAPI from <see cref="IsFriend"/>.</summary>
    public bool IsFollowingUser { get; }

    /// <summary>Compatibility field derived by XSAPI from <see cref="IsFriend"/>.</summary>
    public bool IsFollowedByCaller { get; }

    /// <summary>The user's display name, when XSAPI returned one.</summary>
    public string DisplayName { get; }

    /// <summary>The user's real name, when available to the title.</summary>
    public string RealName { get; }

    /// <summary>The raw display-picture URI.</summary>
    public string DisplayPictureUri { get; }

    /// <summary>Whether the shell avatar should be used.</summary>
    public bool UseAvatar { get; }

    /// <summary>The user's gamerscore as a formatted string.</summary>
    public string Gamerscore { get; }

    /// <summary>The user's classic gamertag.</summary>
    public string Gamertag { get; }

    /// <summary>The user's modern gamertag, without suffix.</summary>
    public string ModernGamertag { get; }

    /// <summary>The suffix that disambiguates <see cref="ModernGamertag"/>.</summary>
    public string ModernGamertagSuffix { get; }

    /// <summary>The modern gamertag and suffix combined.</summary>
    public string UniqueModernGamertag { get; }

    /// <summary>The user's presence snapshot.</summary>
    public SocialManagerPresenceRecord PresenceRecord { get; }

    /// <summary>The user's title-history snapshot.</summary>
    public SocialManagerTitleHistory TitleHistory { get; }

    /// <summary>The user's preferred color snapshot.</summary>
    public SocialManagerPreferredColor PreferredColor { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{UniqueModernGamertag} ({XboxUserId})";

    internal static unsafe SocialManagerUser FromNative(XblSocialManagerUser* native)
    {
        return new SocialManagerUser(
            native->XboxUserId,
            native->IsFavorite != 0,
            native->IsFriend != 0,
            native->IsFollowingUser != 0,
            native->IsFollowedByCaller != 0,
            Utf8.ToString(native->DisplayName, XblSocialManagerUser.DisplayNameCharSize),
            Utf8.ToString(native->RealName, XblSocialManagerUser.RealNameCharSize),
            Utf8.ToString(native->DisplayPicUrlRaw, XblSocialManagerUser.DisplayPicUrlRawCharSize),
            native->UseAvatar != 0,
            Utf8.ToString(native->Gamerscore, XblSocialManagerUser.GamerscoreCharSize),
            Utf8.ToString(native->Gamertag, XblSocialManagerUser.GamertagCharSize),
            Utf8.ToString(native->ModernGamertag, XblSocialManagerUser.ModernGamertagCharSize),
            Utf8.ToString(native->ModernGamertagSuffix, XblSocialManagerUser.ModernGamertagSuffixCharSize),
            Utf8.ToString(native->UniqueModernGamertag, XblSocialManagerUser.UniqueModernGamertagCharSize),
            SocialManagerPresenceRecord.FromNative(&native->PresenceRecord),
            SocialManagerTitleHistory.FromNative(native->TitleHistory),
            SocialManagerPreferredColor.FromNative(native->PreferredColor));
    }
}

/// <summary>A long-lived social-manager user group backed by an XSAPI native handle.</summary>
/// <remarks>
/// Instances are resolved through a handle-to-wrapper identity map, so a group carried by a
/// <see cref="SocialUserGroupLoadedSocialManagerEvent"/> or
/// <see cref="SocialUserGroupUpdatedSocialManagerEvent"/> is reference-equal to the group returned
/// by the create call. Disposing a group destroys the native handle, removes it from that map and
/// makes further use throw <see cref="ObjectDisposedException"/>.
/// </remarks>
public sealed class SocialManagerUserGroup : IDisposable
{
    private readonly SocialManager _manager;
    private readonly IntPtr _handle;
    private User? _localUser;
    private bool _disposed;

    internal SocialManagerUserGroup(SocialManager manager, IntPtr handle)
    {
        _manager = manager;
        _handle = handle;
    }

    /// <summary>How the group was created.</summary>
    public SocialUserGroupType Type => _manager.GetGroupType(this);

    /// <summary>The local user associated with this group.</summary>
    public User LocalUser => _manager.GetGroupLocalUser(this);

    /// <summary>
    /// The filters for a filter-backed group, or <see langword="null"/> for a list-backed group.
    /// </summary>
    public SocialManagerUserGroupFilters? Filters => _manager.GetGroupFilters(this);

    /// <summary>Snapshots users currently in the group.</summary>
    public IReadOnlyList<SocialManagerUser> GetUsers() => _manager.GetGroupUsers(this);

    /// <summary>Snapshots Xbox user ids currently tracked by the group.</summary>
    public IReadOnlyList<ulong> GetUsersTrackedByGroup() => _manager.GetUsersTrackedByGroup(this);

    /// <summary>Replaces the tracked Xbox user ids for a list-backed group.</summary>
    /// <param name="xboxUserIds">The replacement set. It cannot exceed 100 users.</param>
    public void UpdateUsers(IEnumerable<ulong> xboxUserIds) => _manager.UpdateSocialUserGroup(this, xboxUserIds);

    /// <summary>Destroys the native social user group.</summary>
    public void Dispose() => _manager.DestroySocialUserGroup(this);

    internal IntPtr Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle;
        }
    }

    internal void Invalidate() => _disposed = true;

    internal User? CachedLocalUser => _localUser;

    internal void SetLocalUser(User? localUser)
    {
        if (localUser is not null)
        {
            _localUser = localUser;
        }
    }

    internal bool TryMarkDisposed(out IntPtr handle)
    {
        if (_disposed)
        {
            handle = IntPtr.Zero;
            return false;
        }

        _disposed = true;
        handle = _handle;
        return true;
    }

    internal void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(SocialManagerUserGroup));
        }
    }
}

/// <summary>Base record for one snapshotted <see cref="SocialManager.DoWork"/> event.</summary>
/// <remarks>
/// XSAPI's social manager differs from the other state-change subsystems: <c>DoWork</c> has no
/// matching <c>Finish</c>, and native event memory is valid only until the next pump. The .NET
/// projection therefore snapshots events into this record hierarchy before returning, while
/// keeping long-lived group handles as identity-mapped wrappers.
/// </remarks>
public abstract record SocialManagerEvent
{
    private protected SocialManagerEvent(
        SocialManagerEventType eventType,
        User? localUser,
        GameRuntimeException? error)
    {
        EventType = eventType;
        LocalUser = localUser;
        Error = error;
    }

    /// <summary>The kind of event, mirroring <c>XblSocialManagerEvent::eventType</c>.</summary>
    public SocialManagerEventType EventType { get; }

    /// <summary>The local user whose social graph produced the event, when XSAPI supplied one.</summary>
    public User? LocalUser { get; }

    /// <summary>The operation error, or <see langword="null"/> when the native HRESULT was <c>S_OK</c>.</summary>
    public GameRuntimeException? Error { get; }

    /// <summary>Whether the event's native HRESULT represented success.</summary>
    public bool Succeeded => Error is null;
}

/// <summary>One or more users were added to the social graph.</summary>
public sealed record UsersAddedToSocialGraphSocialManagerEvent : SocialManagerEvent
{
    internal UsersAddedToSocialGraphSocialManagerEvent(User? localUser, IReadOnlyList<SocialManagerUser> users)
        : base(SocialManagerEventType.UsersAddedToSocialGraph, localUser, null)
    {
        Users = users;
    }

    /// <summary>The users added to the graph.</summary>
    public IReadOnlyList<SocialManagerUser> Users { get; }
}

/// <summary>One or more users were removed from the social graph.</summary>
public sealed record UsersRemovedFromSocialGraphSocialManagerEvent : SocialManagerEvent
{
    internal UsersRemovedFromSocialGraphSocialManagerEvent(User? localUser, IReadOnlyList<SocialManagerUser> users)
        : base(SocialManagerEventType.UsersRemovedFromSocialGraph, localUser, null)
    {
        Users = users;
    }

    /// <summary>The users removed from the graph.</summary>
    public IReadOnlyList<SocialManagerUser> Users { get; }
}

/// <summary>One or more users' presence changed.</summary>
public sealed record PresenceChangedSocialManagerEvent : SocialManagerEvent
{
    internal PresenceChangedSocialManagerEvent(User? localUser, IReadOnlyList<SocialManagerUser> users)
        : base(SocialManagerEventType.PresenceChanged, localUser, null)
    {
        Users = users;
    }

    /// <summary>The users whose presence changed.</summary>
    public IReadOnlyList<SocialManagerUser> Users { get; }
}

/// <summary>One or more users' profile fields changed.</summary>
public sealed record ProfilesChangedSocialManagerEvent : SocialManagerEvent
{
    internal ProfilesChangedSocialManagerEvent(User? localUser, IReadOnlyList<SocialManagerUser> users)
        : base(SocialManagerEventType.ProfilesChanged, localUser, null)
    {
        Users = users;
    }

    /// <summary>The users whose profile fields changed.</summary>
    public IReadOnlyList<SocialManagerUser> Users { get; }
}

/// <summary>One or more users' social relationships changed.</summary>
public sealed record SocialRelationshipsChangedSocialManagerEvent : SocialManagerEvent
{
    internal SocialRelationshipsChangedSocialManagerEvent(User? localUser, IReadOnlyList<SocialManagerUser> users)
        : base(SocialManagerEventType.SocialRelationshipsChanged, localUser, null)
    {
        Users = users;
    }

    /// <summary>The users whose relationships changed.</summary>
    public IReadOnlyList<SocialManagerUser> Users { get; }
}

/// <summary>A local user's initial social graph finished loading.</summary>
public sealed record LocalUserAddedSocialManagerEvent : SocialManagerEvent
{
    internal LocalUserAddedSocialManagerEvent(User? localUser, GameRuntimeException? error)
        : base(SocialManagerEventType.LocalUserAdded, localUser, error)
    {
    }
}

/// <summary>A social user group's initial tracked set finished loading.</summary>
public sealed record SocialUserGroupLoadedSocialManagerEvent : SocialManagerEvent
{
    internal SocialUserGroupLoadedSocialManagerEvent(
        User? localUser,
        GameRuntimeException? error,
        SocialManagerUserGroup? group)
        : base(SocialManagerEventType.SocialUserGroupLoaded, localUser, error)
    {
        Group = group;
    }

    /// <summary>The group that loaded, when XSAPI supplied a group handle.</summary>
    public SocialManagerUserGroup? Group { get; }
}

/// <summary>A list-backed social user group finished updating.</summary>
public sealed record SocialUserGroupUpdatedSocialManagerEvent : SocialManagerEvent
{
    internal SocialUserGroupUpdatedSocialManagerEvent(
        User? localUser,
        GameRuntimeException? error,
        SocialManagerUserGroup? group)
        : base(SocialManagerEventType.SocialUserGroupUpdated, localUser, error)
    {
        Group = group;
    }

    /// <summary>The group that updated, when XSAPI supplied a group handle.</summary>
    public SocialManagerUserGroup? Group { get; }
}

/// <summary>An event kind unknown to this projection was returned by XSAPI.</summary>
public sealed record UnknownSocialManagerEvent : SocialManagerEvent
{
    internal UnknownSocialManagerEvent(User? localUser, GameRuntimeException? error)
        : base(SocialManagerEventType.Unknown, localUser, error)
    {
    }
}
