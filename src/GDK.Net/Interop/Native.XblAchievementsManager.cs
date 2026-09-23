// P/Invoke declarations for xsapi-c\achievements_manager_c.h -- the process-global Xbox Live
// achievements manager cache.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerResultGetAchievements(
        IntPtr resultHandle,
        XblAchievement** achievements,
        ulong* achievementsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerResultDuplicateHandle(
        IntPtr handle,
        IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XblAchievementsManagerResultCloseHandle(IntPtr handle);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerAddLocalUser(IntPtr user, IntPtr queue);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerRemoveLocalUser(IntPtr user);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerIsUserInitialized(ulong xboxUserId);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerDoWork(
        XblAchievementsManagerEvent** achievementsEvents,
        nuint* achievementsEventsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerGetAchievement(
        ulong xboxUserId,
        byte* achievementId,
        IntPtr* achievementResult);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerGetAchievements(
        ulong xboxUserId,
        XblAchievementOrderBy sortField,
        XblAchievementsManagerSortOrder sortOrder,
        IntPtr* achievementsResult);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerGetAchievementsByState(
        ulong xboxUserId,
        XblAchievementOrderBy sortField,
        XblAchievementsManagerSortOrder sortOrder,
        XblAchievementProgressState achievementState,
        IntPtr* achievementsResult);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsManagerUpdateAchievement(
        ulong xboxUserId,
        byte* achievementId,
        byte currentProgress);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerResultGetAchievements(
        IntPtr resultHandle,
        XblAchievement** achievements,
        ulong* achievementsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerResultDuplicateHandle(
        IntPtr handle,
        IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblAchievementsManagerResultCloseHandle(IntPtr handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerAddLocalUser(IntPtr user, IntPtr queue);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerRemoveLocalUser(IntPtr user);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerIsUserInitialized(ulong xboxUserId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerDoWork(
        XblAchievementsManagerEvent** achievementsEvents,
        nuint* achievementsEventsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerGetAchievement(
        ulong xboxUserId,
        byte* achievementId,
        IntPtr* achievementResult);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerGetAchievements(
        ulong xboxUserId,
        XblAchievementOrderBy sortField,
        XblAchievementsManagerSortOrder sortOrder,
        IntPtr* achievementsResult);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerGetAchievementsByState(
        ulong xboxUserId,
        XblAchievementOrderBy sortField,
        XblAchievementsManagerSortOrder sortOrder,
        XblAchievementProgressState achievementState,
        IntPtr* achievementsResult);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsManagerUpdateAchievement(
        ulong xboxUserId,
        byte* achievementId,
        byte currentProgress);
}

#endif
