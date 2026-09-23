// P/Invoke declarations for xsapi-c\leaderboard_c.h -- the Xbox Live leaderboard service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.
//
// Only the six leaderboard entry points below are exported by Microsoft.Xbox.Services.C.Thunks.dll
// in GDK edition 260404. The header defines no close/duplicate handle helpers because leaderboard
// results are caller-allocated buffers rather than service-owned handles.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblLeaderboardGetLeaderboardAsync(
        IntPtr xboxLiveContext,
        XblLeaderboardQuery leaderboardQuery,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblLeaderboardGetLeaderboardResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblLeaderboardGetLeaderboardResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblLeaderboardResult** ptrToBuffer,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblLeaderboardResultGetNextAsync(
        IntPtr xboxLiveContext,
        XblLeaderboardResult* leaderboardResult,
        uint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblLeaderboardResultGetNextResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblLeaderboardResultGetNextResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblLeaderboardResult** ptrToBuffer,
        nuint* bufferUsed);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblLeaderboardGetLeaderboardAsync(
        IntPtr xboxLiveContext,
        XblLeaderboardQuery leaderboardQuery,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblLeaderboardGetLeaderboardResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblLeaderboardGetLeaderboardResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblLeaderboardResult** ptrToBuffer,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblLeaderboardResultGetNextAsync(
        IntPtr xboxLiveContext,
        XblLeaderboardResult* leaderboardResult,
        uint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblLeaderboardResultGetNextResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblLeaderboardResultGetNextResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblLeaderboardResult** ptrToBuffer,
        nuint* bufferUsed);
}

#endif
