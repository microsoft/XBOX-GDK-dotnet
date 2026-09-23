// P/Invoke declarations for xsapi-c\title_managed_statistics_c.h -- the Xbox Live
// title-managed statistics write service.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblTitleManagedStatsWriteAsync(
        IntPtr xblContextHandle,
        ulong xboxUserId,
        XblTitleManagedStatistic* statistics,
        nuint statisticsCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleManagedStatsUpdateStatsAsync(
        IntPtr xblContextHandle,
        XblTitleManagedStatistic* statistics,
        nuint statisticsCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleManagedStatsDeleteStatsAsync(
        IntPtr xblContextHandle,
        byte** statisticNames,
        nuint statisticNamesCount,
        XAsyncBlock* async);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleManagedStatsWriteAsync(
        IntPtr xblContextHandle,
        ulong xboxUserId,
        XblTitleManagedStatistic* statistics,
        nuint statisticsCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleManagedStatsUpdateStatsAsync(
        IntPtr xblContextHandle,
        XblTitleManagedStatistic* statistics,
        nuint statisticsCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleManagedStatsDeleteStatsAsync(
        IntPtr xblContextHandle,
        byte** statisticNames,
        nuint statisticNamesCount,
        XAsyncBlock* async);
}

#endif
