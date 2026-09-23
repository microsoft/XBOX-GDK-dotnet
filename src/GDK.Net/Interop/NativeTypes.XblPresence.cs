// Blittable mirrors of the XSAPI presence types -- xsapi-c\presence_c.h, GDK edition 260404.
//
// Memory returned through XblPresenceRecordHandle is owned by that handle. The public projection
// snapshots every pointer graph while the handle is alive; see XboxLive\PresenceTypes.cs.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblPresenceDeviceType</c>.</summary>
internal enum XblPresenceDeviceType : uint
{
    Unknown = 0,
    WindowsPhone = 1,
    WindowsPhone7 = 2,
    Web = 3,
    Xbox360 = 4,
    PC = 5,
    Windows8 = 6,
    XboxOne = 7,
    WindowsOneCore = 8,
    WindowsOneCoreMobile = 9,
    iOS = 10,
    Android = 11,
    AppleTV = 12,
    Nintendo = 13,
    PlayStation = 14,
    Win32 = 15,
    Scarlett = 16,
}

/// <summary>Mirrors <c>XblPresenceUserState</c>.</summary>
internal enum XblPresenceUserState : uint
{
    Unknown = 0,
    Online = 1,
    Away = 2,
    Offline = 3,
}

/// <summary>Mirrors <c>XblPresenceTitleViewState</c>.</summary>
internal enum XblPresenceTitleViewState : uint
{
    Unknown = 0,
    FullScreen = 1,
    Filled = 2,
    Snapped = 3,
    Background = 4,
}

/// <summary>Mirrors <c>XblPresenceDetailLevel</c>.</summary>
internal enum XblPresenceDetailLevel : uint
{
    Default = 0,
    User = 1,
    Device = 2,
    Title = 3,
    All = 4,
}

/// <summary>Mirrors <c>XblPresenceMediaIdType</c>.</summary>
internal enum XblPresenceMediaIdType : uint
{
    Unknown = 0,
    Bing = 1,
    MediaProvider = 2,
}

/// <summary>Mirrors <c>XblPresenceTitleState</c>.</summary>
internal enum XblPresenceTitleState : uint
{
    Unknown = 0,
    Started = 1,
    Ended = 2,
}

/// <summary>Mirrors <c>XblPresenceBroadcastProvider</c>.</summary>
internal enum XblPresenceBroadcastProvider : uint
{
    Unknown = 0,
    Twitch = 1,
}

/// <summary>Mirrors <c>XblPresenceDeviceRecord</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPresenceDeviceRecord
{
    internal XblPresenceDeviceType DeviceType;
    internal XblPresenceTitleRecord* TitleRecords;
    internal nuint TitleRecordsCount;
}

/// <summary>Mirrors <c>XblPresenceTitleRecord</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPresenceTitleRecord
{
    internal uint TitleId;
    internal byte* TitleName;
    internal long LastModified;
    internal byte TitleActive;
    internal byte* RichPresenceString;
    internal XblPresenceTitleViewState ViewState;
    internal XblPresenceBroadcastRecord* BroadcastRecord;
}

/// <summary>Mirrors <c>XblPresenceBroadcastRecord</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPresenceBroadcastRecord
{
    internal const int SessionCharSize = 40;

    internal byte* BroadcastId;
    internal fixed byte Session[SessionCharSize];
    internal XblPresenceBroadcastProvider Provider;
    internal uint ViewerCount;
    internal long StartTime;
}

/// <summary>Mirrors <c>XblPresenceRichPresenceIds</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPresenceRichPresenceIds
{
    internal const int ScidCharSize = 40;

    internal fixed byte Scid[ScidCharSize];
    internal byte* PresenceId;
    internal byte** PresenceTokenIds;
    internal nuint PresenceTokenIdsCount;
}

/// <summary>Mirrors <c>XblPresenceQueryFilters</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPresenceQueryFilters
{
    internal XblPresenceDeviceType* DeviceTypes;
    internal nuint DeviceTypesCount;
    internal uint* TitleIds;
    internal nuint TitleIdsCount;
    internal XblPresenceDetailLevel DetailLevel;
    internal byte OnlineOnly;
    internal byte BroadcastingOnly;
}
