// Blittable mirrors of xsapi-c\achievements_manager_c.h, GDK edition 260404.
//
// Achievements manager events are returned from XblAchievementsManagerDoWork as a borrowed array
// valid only until the next DoWork call. The idiomatic layer immediately snapshots each event into
// managed records; see XboxLive\AchievementsManager.cs.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblAchievementsManagerSortOrder</c>.</summary>
internal enum XblAchievementsManagerSortOrder : uint
{
    Unsorted = 0,
    Ascending = 1,
    Descending = 2,
}

/// <summary>Mirrors <c>XblAchievementsManagerEventType</c>.</summary>
internal enum XblAchievementsManagerEventType : uint
{
    LocalUserInitialStateSynced = 0,
    AchievementUnlocked = 1,
    AchievementProgressUpdated = 2,
}

/// <summary>Mirrors <c>XblAchievementsManagerEvent</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblAchievementsManagerEvent
{
    internal XblAchievementProgressChangeEntry ProgressInfo;
    internal ulong XboxUserId;
    internal XblAchievementsManagerEventType EventType;
}
