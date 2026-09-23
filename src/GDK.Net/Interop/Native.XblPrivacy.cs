// P/Invoke declarations for xsapi-c\privacy_c.h -- the Xbox Live privacy service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.
//
// XblPrivacyAddMuteListChangedHandler, XblPrivacyRemoveMuteListChangedHandler,
// XblPrivacyAddBlockListChangedHandler and XblPrivacyRemoveBlockListChangedHandler are declared in
// privacy_c.h, but are not exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK edition 260404.
// They are therefore intentionally not bound here.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyGetAvoidListAsync(
        IntPtr xblContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyGetAvoidListResultCount(
        XAsyncBlock* async,
        nuint* xuidCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyGetAvoidListResult(
        XAsyncBlock* async,
        nuint xuidCount,
        ulong* xuids);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyCheckPermissionAsync(
        IntPtr xblContextHandle,
        XblPermission permissionToCheck,
        ulong targetXuid,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyCheckPermissionResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyCheckPermissionResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblPermissionCheckResult** ptrToBuffer,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyCheckPermissionForAnonymousUserAsync(
        IntPtr xblContextHandle,
        XblPermission permissionToCheck,
        XblAnonymousUserType userType,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyCheckPermissionForAnonymousUserResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyCheckPermissionForAnonymousUserResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblPermissionCheckResult** ptrToBuffer,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyBatchCheckPermissionAsync(
        IntPtr xblContextHandle,
        XblPermission* permissionsToCheck,
        nuint permissionsCount,
        ulong* targetXuids,
        nuint xuidsCount,
        XblAnonymousUserType* targetAnonymousUserTypes,
        nuint targetAnonymousUserTypesCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyBatchCheckPermissionResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyBatchCheckPermissionResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblPermissionCheckResult** ptrToBufferResults,
        nuint* ptrToBufferCount,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyGetMuteListAsync(
        IntPtr xblContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyGetMuteListResultCount(
        XAsyncBlock* async,
        nuint* xuidCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblPrivacyGetMuteListResult(
        XAsyncBlock* async,
        nuint xuidCount,
        ulong* xuids);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyGetAvoidListAsync(
        IntPtr xblContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyGetAvoidListResultCount(
        XAsyncBlock* async,
        nuint* xuidCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyGetAvoidListResult(
        XAsyncBlock* async,
        nuint xuidCount,
        ulong* xuids);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyCheckPermissionAsync(
        IntPtr xblContextHandle,
        XblPermission permissionToCheck,
        ulong targetXuid,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyCheckPermissionResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyCheckPermissionResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblPermissionCheckResult** ptrToBuffer,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyCheckPermissionForAnonymousUserAsync(
        IntPtr xblContextHandle,
        XblPermission permissionToCheck,
        XblAnonymousUserType userType,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyCheckPermissionForAnonymousUserResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyCheckPermissionForAnonymousUserResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblPermissionCheckResult** ptrToBuffer,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyBatchCheckPermissionAsync(
        IntPtr xblContextHandle,
        XblPermission* permissionsToCheck,
        nuint permissionsCount,
        ulong* targetXuids,
        nuint xuidsCount,
        XblAnonymousUserType* targetAnonymousUserTypes,
        nuint targetAnonymousUserTypesCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyBatchCheckPermissionResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyBatchCheckPermissionResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblPermissionCheckResult** ptrToBufferResults,
        nuint* ptrToBufferCount,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyGetMuteListAsync(
        IntPtr xblContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyGetMuteListResultCount(
        XAsyncBlock* async,
        nuint* xuidCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblPrivacyGetMuteListResult(
        XAsyncBlock* async,
        nuint xuidCount,
        ulong* xuids);
}

#endif
