// P/Invoke declarations for xsapi-c\user_statistics_c.h -- the Xbox Live user statistics service.
//
// Every function declared by user_statistics_c.h in GDK edition 260404 is exported by
// Microsoft.Xbox.Services.C.Thunks.dll and is declared here. Older XSAPI docs mention
// XblUserStatisticsTrackUsers, but the 260404 header does not declare it and the thunks DLL does
// not export it, so there is deliberately no binding for that name.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetSingleUserStatisticAsync(
        IntPtr xblContextHandle,
        ulong xboxUserId,
        byte* serviceConfigurationId,
        byte* statisticName,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetSingleUserStatisticResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetSingleUserStatisticResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBuffer,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetSingleUserStatisticsAsync(
        IntPtr xblContextHandle,
        ulong xboxUserId,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetSingleUserStatisticsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetSingleUserStatisticsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBuffer,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetMultipleUserStatisticsAsync(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetMultipleUserStatisticsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetMultipleUserStatisticsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBuffer,
        nuint* resultsCount,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsAsync(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        uint xboxUserIdsCount,
        XblRequestedStatistics* requestedServiceConfigurationStatisticsCollection,
        uint requestedServiceConfigurationStatisticsCollectionCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBufferResults,
        nuint* resultsCount,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsAddStatisticChangedHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr handlerContext);

    [LibraryImport(LibraryName)]
    internal static partial void XblUserStatisticsRemoveStatisticChangedHandler(
        IntPtr xblContextHandle,
        int context);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsTrackStatistics(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsStopTrackingStatistics(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblUserStatisticsStopTrackingUsers(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetSingleUserStatisticAsync(
        IntPtr xblContextHandle,
        ulong xboxUserId,
        byte* serviceConfigurationId,
        byte* statisticName,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetSingleUserStatisticResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetSingleUserStatisticResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBuffer,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetSingleUserStatisticsAsync(
        IntPtr xblContextHandle,
        ulong xboxUserId,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetSingleUserStatisticsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetSingleUserStatisticsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBuffer,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetMultipleUserStatisticsAsync(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetMultipleUserStatisticsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetMultipleUserStatisticsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBuffer,
        nuint* resultsCount,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsAsync(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        uint xboxUserIdsCount,
        XblRequestedStatistics* requestedServiceConfigurationStatisticsCollection,
        uint requestedServiceConfigurationStatisticsCollectionCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsGetMultipleUserStatisticsForMultipleServiceConfigurationsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblUserStatisticsResult** ptrToBufferResults,
        nuint* resultsCount,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsAddStatisticChangedHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr handlerContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblUserStatisticsRemoveStatisticChangedHandler(
        IntPtr xblContextHandle,
        int context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsTrackStatistics(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsStopTrackingStatistics(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        byte* serviceConfigurationId,
        byte** statisticNames,
        nuint statisticNamesCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblUserStatisticsStopTrackingUsers(
        IntPtr xblContextHandle,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount);
}

#endif
