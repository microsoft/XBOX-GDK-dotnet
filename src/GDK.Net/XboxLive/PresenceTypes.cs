using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Device family reported by Xbox Live presence. Mirrors <c>XblPresenceDeviceType</c>.</summary>
public enum PresenceDeviceType : uint
{
    /// <summary>The device is unknown.</summary>
    Unknown = 0,

    /// <summary>A Windows Phone device.</summary>
    WindowsPhone = 1,

    /// <summary>A Windows Phone 7 device.</summary>
    WindowsPhone7 = 2,

    /// <summary>A web endpoint, such as Xbox.com.</summary>
    Web = 3,

    /// <summary>An Xbox 360 console.</summary>
    Xbox360 = 4,

    /// <summary>A PC Games for Windows Live endpoint.</summary>
    Pc = 5,

    /// <summary>An Xbox Live for Windows device.</summary>
    Windows8 = 6,

    /// <summary>An Xbox One console.</summary>
    XboxOne = 7,

    /// <summary>A Windows OneCore device.</summary>
    WindowsOneCore = 8,

    /// <summary>A Windows OneCore Mobile device.</summary>
    WindowsOneCoreMobile = 9,

    /// <summary>An iOS device.</summary>
    Ios = 10,

    /// <summary>An Android device.</summary>
    Android = 11,

    /// <summary>An Apple TV device.</summary>
    AppleTV = 12,

    /// <summary>A Nintendo device.</summary>
    Nintendo = 13,

    /// <summary>A PlayStation device.</summary>
    PlayStation = 14,

    /// <summary>A Win32 device.</summary>
    Win32 = 15,

    /// <summary>An Xbox Series X|S console.</summary>
    Scarlett = 16,
}

/// <summary>A user's aggregate Xbox Live presence state. Mirrors <c>XblPresenceUserState</c>.</summary>
public enum PresenceUserState : uint
{
    /// <summary>The state is unknown.</summary>
    Unknown = 0,

    /// <summary>The user is signed in to Xbox Live and active in a title.</summary>
    Online = 1,

    /// <summary>The user is signed in to Xbox Live, but inactive in all titles.</summary>
    Away = 2,

    /// <summary>The user is not signed in to Xbox Live.</summary>
    Offline = 3,
}

/// <summary>Screen view state for a title presence record. Mirrors <c>XblPresenceTitleViewState</c>.</summary>
public enum PresenceTitleViewState : uint
{
    /// <summary>The view state is unknown.</summary>
    Unknown = 0,

    /// <summary>The title is using the full screen.</summary>
    FullScreen = 1,

    /// <summary>The title is using part of the screen with another application filled.</summary>
    Filled = 2,

    /// <summary>The title is snapped beside another application.</summary>
    Snapped = 3,

    /// <summary>The title is running in the background and is not visible.</summary>
    Background = 4,
}

/// <summary>How much presence detail a query should request. Mirrors <c>XblPresenceDetailLevel</c>.</summary>
public enum PresenceDetailLevel : uint
{
    /// <summary>The service default.</summary>
    Default = 0,

    /// <summary>User presence only, with no device or title records.</summary>
    User = 1,

    /// <summary>User and device presence, with no title records.</summary>
    Device = 2,

    /// <summary>User, device and title presence, with no rich presence strings.</summary>
    Title = 3,

    /// <summary>All available user, device, title and rich presence information.</summary>
    All = 4,
}

/// <summary>Media identifier type for media presence data. Mirrors <c>XblPresenceMediaIdType</c>.</summary>
public enum PresenceMediaIdType : uint
{
    /// <summary>The media id type is unknown.</summary>
    Unknown = 0,

    /// <summary>A Bing media id.</summary>
    Bing = 1,

    /// <summary>A provider-specific media id.</summary>
    MediaProvider = 2,
}

/// <summary>Title presence transition state. Mirrors <c>XblPresenceTitleState</c>.</summary>
public enum PresenceTitleState : uint
{
    /// <summary>The title state is unknown.</summary>
    Unknown = 0,

    /// <summary>The user started playing the title.</summary>
    Started = 1,

    /// <summary>The user ended playing the title.</summary>
    Ended = 2,
}

/// <summary>Broadcast provider for a presence record. Mirrors <c>XblPresenceBroadcastProvider</c>.</summary>
public enum PresenceBroadcastProvider : uint
{
    /// <summary>The broadcast provider is unknown.</summary>
    Unknown = 0,

    /// <summary>The user is streaming through Twitch.</summary>
    Twitch = 1,
}

/// <summary>Social group names accepted by <see cref="PresenceService.GetForSocialGroupAsync(PresenceSocialGroup, ulong?, PresenceQueryFilters?, System.Threading.CancellationToken)"/>.</summary>
public enum PresenceSocialGroup
{
    /// <summary>Favorites of the target user.</summary>
    Favorites = 0,

    /// <summary>Mutual friends and people the target user follows.</summary>
    People = 1,

    /// <summary>Mutual friends of the target user.</summary>
    Friends = 2,
}

/// <summary>Rich presence string identifiers supplied to <see cref="PresenceService.SetPresenceAsync"/>.</summary>
public sealed class PresenceRichPresenceIds
{
    /// <summary>
    /// Creates rich presence string identifiers for <c>XblPresenceSetPresenceAsync</c>.
    /// </summary>
    /// <param name="serviceConfigurationId">The SCID containing the presence strings.</param>
    /// <param name="presenceId">The presence string id defined in the service configuration.</param>
    /// <param name="presenceTokenIds">Optional replacement token ids used by the presence string.</param>
    public PresenceRichPresenceIds(
        string serviceConfigurationId,
        string presenceId,
        IEnumerable<string>? presenceTokenIds = null)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        if (presenceId is null)
        {
            throw new ArgumentNullException(nameof(presenceId));
        }

        ServiceConfigurationId = serviceConfigurationId;
        PresenceId = presenceId;
        PresenceTokenIds = new ReadOnlyCollection<string>(ToStringArray(presenceTokenIds));
    }

    /// <summary>The SCID containing the presence strings.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>The presence string id defined in the service configuration.</summary>
    public string PresenceId { get; }

    /// <summary>The optional replacement token ids used by the presence string.</summary>
    public IReadOnlyList<string> PresenceTokenIds { get; }

    private static string[] ToStringArray(IEnumerable<string>? values)
    {
        if (values is null)
        {
            return Array.Empty<string>();
        }

        if (values is string[] array)
        {
            return (string[])array.Clone();
        }

        if (values is ICollection<string> collection)
        {
            var copy = new string[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<string>(values);
        return list.ToArray();
    }
}

/// <summary>Filters for batch and social-group presence queries. Mirrors <c>XblPresenceQueryFilters</c>.</summary>
public sealed class PresenceQueryFilters
{
    /// <summary>
    /// Creates filters for a presence query.
    /// </summary>
    /// <param name="deviceTypes">Device types to include, or <see langword="null"/> for all devices.</param>
    /// <param name="titleIds">Title ids to include, or <see langword="null"/> for all titles.</param>
    /// <param name="detailLevel">How much presence detail to request.</param>
    /// <param name="onlineOnly">Whether offline users should be filtered out.</param>
    /// <param name="broadcastingOnly">Whether users that are not broadcasting should be filtered out.</param>
    public PresenceQueryFilters(
        IEnumerable<PresenceDeviceType>? deviceTypes = null,
        IEnumerable<uint>? titleIds = null,
        PresenceDetailLevel detailLevel = PresenceDetailLevel.Title,
        bool onlineOnly = false,
        bool broadcastingOnly = false)
    {
        DeviceTypes = new ReadOnlyCollection<PresenceDeviceType>(ToArray(deviceTypes));
        TitleIds = new ReadOnlyCollection<uint>(ToArray(titleIds));
        DetailLevel = detailLevel;
        OnlineOnly = onlineOnly;
        BroadcastingOnly = broadcastingOnly;
    }

    /// <summary>Device types to include. Empty means the service default of all devices.</summary>
    public IReadOnlyList<PresenceDeviceType> DeviceTypes { get; }

    /// <summary>Title ids to include. Empty means the service default of all titles.</summary>
    public IReadOnlyList<uint> TitleIds { get; }

    /// <summary>How much presence detail to request.</summary>
    public PresenceDetailLevel DetailLevel { get; }

    /// <summary>Whether offline users should be filtered out.</summary>
    public bool OnlineOnly { get; }

    /// <summary>Whether users that are not broadcasting should be filtered out.</summary>
    public bool BroadcastingOnly { get; }

    private static PresenceDeviceType[] ToArray(IEnumerable<PresenceDeviceType>? values)
    {
        if (values is null)
        {
            return Array.Empty<PresenceDeviceType>();
        }

        if (values is PresenceDeviceType[] array)
        {
            return (PresenceDeviceType[])array.Clone();
        }

        if (values is ICollection<PresenceDeviceType> collection)
        {
            var copy = new PresenceDeviceType[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<PresenceDeviceType>(values);
        return list.ToArray();
    }

    private static uint[] ToArray(IEnumerable<uint>? values)
    {
        if (values is null)
        {
            return Array.Empty<uint>();
        }

        if (values is uint[] array)
        {
            return (uint[])array.Clone();
        }

        if (values is ICollection<uint> collection)
        {
            var copy = new uint[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<uint>(values);
        return list.ToArray();
    }
}

/// <summary>A user's Xbox Live presence. Managed snapshot of <c>XblPresenceRecordHandle</c>.</summary>
/// <remarks>
/// Native presence records own the arrays of device, title and broadcast records they expose. This
/// type copies that graph in full while the native handle is alive, so instances remain valid after
/// the handle returned by XSAPI has been closed.
/// </remarks>
public sealed class PresenceRecord
{
    internal PresenceRecord(ulong xboxUserId, PresenceUserState userState, IReadOnlyList<PresenceDeviceRecord> devices)
    {
        XboxUserId = xboxUserId;
        UserState = userState;
        Devices = devices;
    }

    /// <summary>The Xbox user id this presence record describes.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The user's aggregate presence state.</summary>
    public PresenceUserState UserState { get; }

    /// <summary>The device-level presence records for the user.</summary>
    public IReadOnlyList<PresenceDeviceRecord> Devices { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{XboxUserId}: {UserState}";

    internal static unsafe PresenceRecord FromHandle(PresenceRecordHandle handle)
    {
        IntPtr raw = handle.DangerousGetHandle();

        ulong xuid;
        Hr.ThrowIfFailed(NativeXbl.XblPresenceRecordGetXuid(raw, &xuid));

        XblPresenceUserState state;
        Hr.ThrowIfFailed(NativeXbl.XblPresenceRecordGetUserState(raw, &state));

        XblPresenceDeviceRecord* nativeDevices;
        nuint deviceCount;
        Hr.ThrowIfFailed(NativeXbl.XblPresenceRecordGetDeviceRecords(raw, &nativeDevices, &deviceCount));

        var devices = new PresenceDeviceRecord[checked((int)deviceCount)];
        for (int i = 0; i < devices.Length; i++)
        {
            devices[i] = PresenceDeviceRecord.FromNative(nativeDevices + i);
        }

        return new PresenceRecord(
            xuid,
            (PresenceUserState)state,
            new ReadOnlyCollection<PresenceDeviceRecord>(devices));
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

/// <summary>Presence for one device. Managed snapshot of <c>XblPresenceDeviceRecord</c>.</summary>
public sealed class PresenceDeviceRecord
{
    internal PresenceDeviceRecord(PresenceDeviceType deviceType, IReadOnlyList<PresenceTitleRecord> titles)
    {
        DeviceType = deviceType;
        Titles = titles;
    }

    /// <summary>The device type associated with this record.</summary>
    public PresenceDeviceType DeviceType { get; }

    /// <summary>The title presence records reported for this device.</summary>
    public IReadOnlyList<PresenceTitleRecord> Titles { get; }

    internal static unsafe PresenceDeviceRecord FromNative(XblPresenceDeviceRecord* native)
    {
        var titles = new PresenceTitleRecord[checked((int)native->TitleRecordsCount)];
        for (int i = 0; i < titles.Length; i++)
        {
            titles[i] = PresenceTitleRecord.FromNative(native->TitleRecords + i);
        }

        return new PresenceDeviceRecord(
            (PresenceDeviceType)native->DeviceType,
            new ReadOnlyCollection<PresenceTitleRecord>(titles));
    }
}

/// <summary>Presence for one title on a device. Managed snapshot of <c>XblPresenceTitleRecord</c>.</summary>
public sealed class PresenceTitleRecord
{
    internal PresenceTitleRecord(
        uint titleId,
        string titleName,
        DateTimeOffset lastModified,
        bool titleActive,
        string richPresenceString,
        PresenceTitleViewState viewState,
        PresenceBroadcastRecord? broadcast)
    {
        TitleId = titleId;
        TitleName = titleName;
        LastModified = lastModified;
        TitleActive = titleActive;
        RichPresenceString = richPresenceString;
        ViewState = viewState;
        Broadcast = broadcast;
    }

    /// <summary>The title id.</summary>
    public uint TitleId { get; }

    /// <summary>The localized title name.</summary>
    public string TitleName { get; }

    /// <summary>When the record was last updated.</summary>
    public DateTimeOffset LastModified { get; }

    /// <summary>Whether the user is active in the title.</summary>
    public bool TitleActive { get; }

    /// <summary>The formatted localized rich presence string, or empty when none was returned.</summary>
    public string RichPresenceString { get; }

    /// <summary>The title's view state.</summary>
    public PresenceTitleViewState ViewState { get; }

    /// <summary>Broadcast details when the user is broadcasting this title.</summary>
    public PresenceBroadcastRecord? Broadcast { get; }

    internal static unsafe PresenceTitleRecord FromNative(XblPresenceTitleRecord* native) =>
        new PresenceTitleRecord(
            native->TitleId,
            Utf8.ToString(native->TitleName) ?? string.Empty,
            PresenceRecord.FromUnixSeconds(native->LastModified),
            native->TitleActive != 0,
            Utf8.ToString(native->RichPresenceString) ?? string.Empty,
            (PresenceTitleViewState)native->ViewState,
            PresenceBroadcastRecord.FromNative(native->BroadcastRecord));
}

/// <summary>Broadcast details attached to a title presence record. Managed snapshot of <c>XblPresenceBroadcastRecord</c>.</summary>
public sealed class PresenceBroadcastRecord
{
    internal PresenceBroadcastRecord(
        string broadcastId,
        string sessionId,
        PresenceBroadcastProvider provider,
        uint viewerCount,
        DateTimeOffset startTime)
    {
        BroadcastId = broadcastId;
        SessionId = sessionId;
        Provider = provider;
        ViewerCount = viewerCount;
        StartTime = startTime;
    }

    /// <summary>The broadcast id assigned by the provider.</summary>
    public string BroadcastId { get; }

    /// <summary>The GUID string identifying the broadcast session.</summary>
    public string SessionId { get; }

    /// <summary>The streaming provider.</summary>
    public PresenceBroadcastProvider Provider { get; }

    /// <summary>Approximate current viewer count.</summary>
    public uint ViewerCount { get; }

    /// <summary>When the broadcast started.</summary>
    public DateTimeOffset StartTime { get; }

    internal static unsafe PresenceBroadcastRecord? FromNative(XblPresenceBroadcastRecord* native)
    {
        if (native is null)
        {
            return null;
        }

        string session = Utf8.ToString(native->Session, XblPresenceBroadcastRecord.SessionCharSize);

        return new PresenceBroadcastRecord(
            Utf8.ToString(native->BroadcastId) ?? string.Empty,
            session,
            (PresenceBroadcastProvider)native->Provider,
            native->ViewerCount,
            PresenceRecord.FromUnixSeconds(native->StartTime));
    }
}

/// <summary>Payload for <see cref="PresenceService.DevicePresenceChanged"/>.</summary>
public sealed class DevicePresenceChangedEventArgs : EventArgs
{
    internal DevicePresenceChangedEventArgs(ulong xboxUserId, PresenceDeviceType deviceType, bool isUserLoggedOnDevice)
    {
        XboxUserId = xboxUserId;
        DeviceType = deviceType;
        IsUserLoggedOnDevice = isUserLoggedOnDevice;
    }

    /// <summary>The user whose device presence changed.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The device whose presence changed.</summary>
    public PresenceDeviceType DeviceType { get; }

    /// <summary>Whether the user is now logged on to that device.</summary>
    public bool IsUserLoggedOnDevice { get; }
}

/// <summary>Payload for <see cref="PresenceService.TitlePresenceChanged"/>.</summary>
public sealed class TitlePresenceChangedEventArgs : EventArgs
{
    internal TitlePresenceChangedEventArgs(ulong xboxUserId, uint titleId, PresenceTitleState titleState)
    {
        XboxUserId = xboxUserId;
        TitleId = titleId;
        TitleState = titleState;
    }

    /// <summary>The user whose title presence changed.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The title whose presence changed.</summary>
    public uint TitleId { get; }

    /// <summary>The title presence transition.</summary>
    public PresenceTitleState TitleState { get; }
}
