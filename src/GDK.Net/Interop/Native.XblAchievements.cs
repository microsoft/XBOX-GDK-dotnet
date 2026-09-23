// P/Invoke declarations for xsapi-c\achievements_c.h -- the Xbox Live achievements service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.
//
// XblAchievementUnlockAddNotificationHandler / ...RemoveNotificationHandler are declared only for
// HC_PLATFORM_WIN32 and HC_PLATFORM_IS_EXTERNAL, so a GDK title cannot reach them and the thunks
// DLL does not export them. The progress-change pair *is* exported and is declared below.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    // --- queries ---

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsGetAchievementAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        byte* serviceConfigurationId,
        byte* achievementId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsGetAchievementResult(XAsyncBlock* async, IntPtr* result);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsGetAchievementsForTitleIdAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        uint titleId,
        XblAchievementType type,
        byte unlockedOnly,
        XblAchievementOrderBy orderBy,
        uint skipItems,
        uint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsGetAchievementsForTitleIdResult(XAsyncBlock* async, IntPtr* result);

    // --- progress updates ---

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsUpdateAchievementAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        byte* achievementId,
        uint percentComplete,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsUpdateAchievementForTitleIdAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        uint titleId,
        byte* serviceConfigurationId,
        byte* achievementId,
        uint percentComplete,
        XAsyncBlock* async);

    // --- result handle: paging and lifetime ---

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsResultGetAchievements(
        IntPtr resultHandle,
        XblAchievement** achievements,
        nuint* achievementsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsResultHasNext(IntPtr resultHandle, byte* hasNext);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsResultGetNextAsync(
        IntPtr resultHandle,
        uint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsResultGetNextResult(XAsyncBlock* async, IntPtr* result);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsResultDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XblAchievementsResultCloseHandle(IntPtr handle);

    // --- progress-change notifications ---

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsAddAchievementProgressChangeHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr handlerContext);

    [LibraryImport(LibraryName)]
    internal static partial int XblAchievementsRemoveAchievementProgressChangeHandler(
        IntPtr xblContextHandle,
        int functionContext);
}

#else

internal static unsafe partial class NativeXbl
{
    // --- queries ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsGetAchievementAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        byte* serviceConfigurationId,
        byte* achievementId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsGetAchievementResult(XAsyncBlock* async, IntPtr* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsGetAchievementsForTitleIdAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        uint titleId,
        XblAchievementType type,
        byte unlockedOnly,
        XblAchievementOrderBy orderBy,
        uint skipItems,
        uint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsGetAchievementsForTitleIdResult(XAsyncBlock* async, IntPtr* result);

    // --- progress updates ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsUpdateAchievementAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        byte* achievementId,
        uint percentComplete,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsUpdateAchievementForTitleIdAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        uint titleId,
        byte* serviceConfigurationId,
        byte* achievementId,
        uint percentComplete,
        XAsyncBlock* async);

    // --- result handle: paging and lifetime ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsResultGetAchievements(
        IntPtr resultHandle,
        XblAchievement** achievements,
        nuint* achievementsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsResultHasNext(IntPtr resultHandle, byte* hasNext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsResultGetNextAsync(
        IntPtr resultHandle,
        uint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsResultGetNextResult(XAsyncBlock* async, IntPtr* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsResultDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblAchievementsResultCloseHandle(IntPtr handle);

    // --- progress-change notifications ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsAddAchievementProgressChangeHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr handlerContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblAchievementsRemoveAchievementProgressChangeHandler(
        IntPtr xblContextHandle,
        int functionContext);
}

#endif
