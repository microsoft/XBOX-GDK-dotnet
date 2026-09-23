// P/Invoke declarations for xsapi-c\multiplayer_activity_c.h -- the Xbox Live multiplayer
// activity service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.
//
// XblMultiplayerActivityAddInviteHandler and XblMultiplayerActivityRemoveInviteHandler are
// declared in the header for non-GDK platforms, but are not exported by
// Microsoft.Xbox.Services.C.Thunks.dll in GDK edition 260404. They are intentionally not bound; the
// projected surface is query-and-write only.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivityUpdateRecentPlayers(
        IntPtr xblContext,
        XblMultiplayerActivityRecentPlayerUpdate* updates,
        nuint updatesCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivityFlushRecentPlayersAsync(
        IntPtr xblContext,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivitySetActivityAsync(
        IntPtr xblContext,
        XblMultiplayerActivityInfo* activityInfo,
        byte allowCrossPlatformJoin,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivityGetActivityAsync(
        IntPtr xblContext,
        ulong* xuids,
        nuint xuidsCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivityGetActivityResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivityGetActivityResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblMultiplayerActivityInfo** ptrToBufferResults,
        nuint* resultCount,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivityDeleteActivityAsync(
        IntPtr xblContext,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblMultiplayerActivitySendInvitesAsync(
        IntPtr xblContext,
        ulong* xuids,
        nuint xuidsCount,
        byte allowCrossPlatformJoin,
        byte* connectionString,
        XAsyncBlock* async);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivityUpdateRecentPlayers(
        IntPtr xblContext,
        XblMultiplayerActivityRecentPlayerUpdate* updates,
        nuint updatesCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivityFlushRecentPlayersAsync(
        IntPtr xblContext,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivitySetActivityAsync(
        IntPtr xblContext,
        XblMultiplayerActivityInfo* activityInfo,
        byte allowCrossPlatformJoin,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivityGetActivityAsync(
        IntPtr xblContext,
        ulong* xuids,
        nuint xuidsCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivityGetActivityResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivityGetActivityResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblMultiplayerActivityInfo** ptrToBufferResults,
        nuint* resultCount,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivityDeleteActivityAsync(
        IntPtr xblContext,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblMultiplayerActivitySendInvitesAsync(
        IntPtr xblContext,
        ulong* xuids,
        nuint xuidsCount,
        byte allowCrossPlatformJoin,
        byte* connectionString,
        XAsyncBlock* async);
}

#endif
