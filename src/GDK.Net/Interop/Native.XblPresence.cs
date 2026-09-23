// P/Invoke declarations for xsapi-c\presence_c.h -- the Xbox Live presence service.
//
// Every function in presence_c.h is exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK
// edition 260404 and is declared here. Other XSAPI notification-handler entry points have known
// export gaps; those are intentionally not declared in their service files unless the thunks DLL
// exports them.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceRecordGetXuid(IntPtr handle, ulong* xuid);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceRecordGetUserState(IntPtr handle, XblPresenceUserState* userState);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceRecordGetDeviceRecords(
        IntPtr handle,
        XblPresenceDeviceRecord** deviceRecords,
        nuint* deviceRecordsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceRecordDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XblPresenceRecordCloseHandle(IntPtr handle);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceSetPresenceAsync(
        IntPtr xblContextHandle,
        byte isUserActiveInTitle,
        XblPresenceRichPresenceIds* richPresenceIds,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceAsync(
        IntPtr xblContextHandle,
        ulong xuid,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceResult(
        XAsyncBlock* async,
        IntPtr* presenceRecordHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceForMultipleUsersAsync(
        IntPtr xblContextHandle,
        ulong* xuids,
        nuint xuidsCount,
        XblPresenceQueryFilters* filters,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceForMultipleUsersResultCount(
        XAsyncBlock* async,
        nuint* resultCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceForMultipleUsersResult(
        XAsyncBlock* async,
        IntPtr* presenceRecordHandles,
        nuint presenceRecordHandlesCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceForSocialGroupAsync(
        IntPtr xblContextHandle,
        byte* socialGroupName,
        ulong* socialGroupOwnerXuid,
        XblPresenceQueryFilters* filters,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceForSocialGroupResultCount(
        XAsyncBlock* async,
        nuint* resultCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceGetPresenceForSocialGroupResult(
        XAsyncBlock* async,
        IntPtr* presenceRecordHandles,
        nuint presenceRecordHandlesCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceAddDevicePresenceChangedHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceRemoveDevicePresenceChangedHandler(
        IntPtr xblContextHandle,
        int token);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceAddTitlePresenceChangedHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceRemoveTitlePresenceChangedHandler(
        IntPtr xblContextHandle,
        int token);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceTrackUsers(
        IntPtr xblContextHandle,
        ulong* xuids,
        nuint xuidsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceStopTrackingUsers(
        IntPtr xblContextHandle,
        ulong* xuids,
        nuint xuidsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceTrackAdditionalTitles(
        IntPtr xblContextHandle,
        uint* titleIds,
        nuint titleIdsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPresenceStopTrackingAdditionalTitles(
        IntPtr xblContextHandle,
        uint* titleIds,
        nuint titleIdsCount);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceRecordGetXuid(IntPtr handle, ulong* xuid);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceRecordGetUserState(IntPtr handle, XblPresenceUserState* userState);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceRecordGetDeviceRecords(
        IntPtr handle,
        XblPresenceDeviceRecord** deviceRecords,
        nuint* deviceRecordsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceRecordDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblPresenceRecordCloseHandle(IntPtr handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceSetPresenceAsync(
        IntPtr xblContextHandle,
        byte isUserActiveInTitle,
        XblPresenceRichPresenceIds* richPresenceIds,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceAsync(
        IntPtr xblContextHandle,
        ulong xuid,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceResult(
        XAsyncBlock* async,
        IntPtr* presenceRecordHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceForMultipleUsersAsync(
        IntPtr xblContextHandle,
        ulong* xuids,
        nuint xuidsCount,
        XblPresenceQueryFilters* filters,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceForMultipleUsersResultCount(
        XAsyncBlock* async,
        nuint* resultCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceForMultipleUsersResult(
        XAsyncBlock* async,
        IntPtr* presenceRecordHandles,
        nuint presenceRecordHandlesCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceForSocialGroupAsync(
        IntPtr xblContextHandle,
        byte* socialGroupName,
        ulong* socialGroupOwnerXuid,
        XblPresenceQueryFilters* filters,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceForSocialGroupResultCount(
        XAsyncBlock* async,
        nuint* resultCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceGetPresenceForSocialGroupResult(
        XAsyncBlock* async,
        IntPtr* presenceRecordHandles,
        nuint presenceRecordHandlesCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceAddDevicePresenceChangedHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceRemoveDevicePresenceChangedHandler(
        IntPtr xblContextHandle,
        int token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceAddTitlePresenceChangedHandler(
        IntPtr xblContextHandle,
        IntPtr handler,
        IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceRemoveTitlePresenceChangedHandler(
        IntPtr xblContextHandle,
        int token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceTrackUsers(
        IntPtr xblContextHandle,
        ulong* xuids,
        nuint xuidsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceStopTrackingUsers(
        IntPtr xblContextHandle,
        ulong* xuids,
        nuint xuidsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceTrackAdditionalTitles(
        IntPtr xblContextHandle,
        uint* titleIds,
        nuint titleIdsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPresenceStopTrackingAdditionalTitles(
        IntPtr xblContextHandle,
        uint* titleIds,
        nuint titleIdsCount);
}

#endif
