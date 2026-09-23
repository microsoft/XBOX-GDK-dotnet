// P/Invoke declarations for xsapi-c\title_storage_c.h -- the Xbox Live title storage service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.
//
// All title_storage_c.h entry points present in GDK edition 260404 are exported by
// Microsoft.Xbox.Services.C.Thunks.dll and are declared below. The blob metadata result is a
// service-owned handle; blob upload and download operate on caller-owned buffers that must stay
// pinned until the async operation completes.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageGetQuotaAsync(
        IntPtr xboxLiveContext,
        byte* serviceConfigurationId,
        XblTitleStorageType storageType,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageGetQuotaResult(
        XAsyncBlock* async,
        nuint* usedBytes,
        nuint* quotaBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageGetBlobMetadataAsync(
        IntPtr xboxLiveContext,
        byte* serviceConfigurationId,
        XblTitleStorageType storageType,
        byte* blobPath,
        ulong xboxUserId,
        uint skipItems,
        uint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageGetBlobMetadataResult(XAsyncBlock* async, IntPtr* result);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageBlobMetadataResultGetItems(
        IntPtr resultHandle,
        XblTitleStorageBlobMetadata** items,
        nuint* itemsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageBlobMetadataResultHasNext(IntPtr resultHandle, byte* hasNext);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageBlobMetadataResultGetNextAsync(
        IntPtr resultHandle,
        uint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageBlobMetadataResultGetNextResult(XAsyncBlock* async, IntPtr* result);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageBlobMetadataResultDuplicateHandle(
        IntPtr handle,
        IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XblTitleStorageBlobMetadataResultCloseHandle(IntPtr handle);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageDeleteBlobAsync(
        IntPtr xboxLiveContext,
        XblTitleStorageBlobMetadata blobMetadata,
        byte deleteOnlyIfEtagMatches,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageDownloadBlobAsync(
        IntPtr xboxLiveContext,
        XblTitleStorageBlobMetadata blobMetadata,
        byte* blobBuffer,
        nuint blobBufferCount,
        XblTitleStorageETagMatchCondition etagMatchCondition,
        byte* selectQuery,
        nuint preferredDownloadBlockSize,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageDownloadBlobResult(
        XAsyncBlock* async,
        XblTitleStorageBlobMetadata* blobMetadata);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageUploadBlobAsync(
        IntPtr xboxLiveContext,
        XblTitleStorageBlobMetadata blobMetadata,
        byte* blobBuffer,
        nuint blobBufferCount,
        XblTitleStorageETagMatchCondition etagMatchCondition,
        nuint preferredUploadBlockSize,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblTitleStorageUploadBlobResult(
        XAsyncBlock* async,
        XblTitleStorageBlobMetadata* blobMetadata);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageGetQuotaAsync(
        IntPtr xboxLiveContext,
        byte* serviceConfigurationId,
        XblTitleStorageType storageType,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageGetQuotaResult(
        XAsyncBlock* async,
        nuint* usedBytes,
        nuint* quotaBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageGetBlobMetadataAsync(
        IntPtr xboxLiveContext,
        byte* serviceConfigurationId,
        XblTitleStorageType storageType,
        byte* blobPath,
        ulong xboxUserId,
        uint skipItems,
        uint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageGetBlobMetadataResult(XAsyncBlock* async, IntPtr* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageBlobMetadataResultGetItems(
        IntPtr resultHandle,
        XblTitleStorageBlobMetadata** items,
        nuint* itemsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageBlobMetadataResultHasNext(IntPtr resultHandle, byte* hasNext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageBlobMetadataResultGetNextAsync(
        IntPtr resultHandle,
        uint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageBlobMetadataResultGetNextResult(XAsyncBlock* async, IntPtr* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageBlobMetadataResultDuplicateHandle(
        IntPtr handle,
        IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblTitleStorageBlobMetadataResultCloseHandle(IntPtr handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageDeleteBlobAsync(
        IntPtr xboxLiveContext,
        XblTitleStorageBlobMetadata blobMetadata,
        byte deleteOnlyIfEtagMatches,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageDownloadBlobAsync(
        IntPtr xboxLiveContext,
        XblTitleStorageBlobMetadata blobMetadata,
        byte* blobBuffer,
        nuint blobBufferCount,
        XblTitleStorageETagMatchCondition etagMatchCondition,
        byte* selectQuery,
        nuint preferredDownloadBlockSize,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageDownloadBlobResult(
        XAsyncBlock* async,
        XblTitleStorageBlobMetadata* blobMetadata);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageUploadBlobAsync(
        IntPtr xboxLiveContext,
        XblTitleStorageBlobMetadata blobMetadata,
        byte* blobBuffer,
        nuint blobBufferCount,
        XblTitleStorageETagMatchCondition etagMatchCondition,
        nuint preferredUploadBlockSize,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblTitleStorageUploadBlobResult(
        XAsyncBlock* async,
        XblTitleStorageBlobMetadata* blobMetadata);
}

#endif
