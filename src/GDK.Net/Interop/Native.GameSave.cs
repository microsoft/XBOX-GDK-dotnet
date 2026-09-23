// P/Invoke declarations for the XGameSave and XGameSaveFiles families.
//
// Sources: XGameSave.h, XGameSaveFiles.h (GDK edition 260404).
// Both shims must stay signature-identical; see interop-conventions.md.
// Callbacks are passed as IntPtr on all TFMs; GameSaveCallbacks provides the typed function pointer.
//
// Verified against the 49-API unexported list (xgameruntime.lib vs xgameruntime.thunks.dll):
// ALL 30 XGameSave* and XGameSaveFiles* functions declared below ARE exported by the thunks DLL.
// None of the 49 unexported APIs belong to these families, so no declarations were omitted.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // ── XGameSave: Provider ────────────────────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveInitializeProvider(
        IntPtr requestingUser,
        byte* configurationId,
        byte syncOnDemand,
        IntPtr* provider);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveInitializeProviderAsync(
        IntPtr requestingUser,
        byte* configurationId,
        byte syncOnDemand,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveInitializeProviderResult(
        XAsyncBlock* async,
        IntPtr* provider);

    [LibraryImport(LibraryName)]
    internal static partial void XGameSaveCloseProvider(IntPtr provider);

    // ── XGameSave: Quota ───────────────────────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveGetRemainingQuota(
        IntPtr provider,
        long* remainingQuota);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveGetRemainingQuotaAsync(
        IntPtr provider,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveGetRemainingQuotaResult(
        XAsyncBlock* async,
        long* remainingQuota);

    // ── XGameSave: Container management ───────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveDeleteContainer(
        IntPtr provider,
        byte* containerName);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveDeleteContainerAsync(
        IntPtr provider,
        byte* containerName,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveDeleteContainerResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveGetContainerInfo(
        IntPtr provider,
        byte* containerName,
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveEnumerateContainerInfo(
        IntPtr provider,
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveEnumerateContainerInfoByName(
        IntPtr provider,
        byte* containerNamePrefix,
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveCreateContainer(
        IntPtr provider,
        byte* containerName,
        IntPtr* containerContext);

    [LibraryImport(LibraryName)]
    internal static partial void XGameSaveCloseContainer(IntPtr context);

    // ── XGameSave: Blob enumeration ────────────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveEnumerateBlobInfo(
        IntPtr container,
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveEnumerateBlobInfoByName(
        IntPtr container,
        byte* blobNamePrefix,
        IntPtr context,
        IntPtr callback);

    // ── XGameSave: Blob read ───────────────────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveReadBlobData(
        IntPtr container,
        byte** blobNames,
        uint* countOfBlobs,
        nuint blobsSize,
        NativeGameSaveBlob* blobData);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveReadBlobDataAsync(
        IntPtr container,
        byte** blobNames,
        uint countOfBlobs,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveReadBlobDataResult(
        XAsyncBlock* async,
        nuint blobsSize,
        NativeGameSaveBlob* blobData,
        uint* countOfBlobs);

    // ── XGameSave: Update ──────────────────────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveCreateUpdate(
        IntPtr container,
        byte* containerDisplayName,
        IntPtr* updateContext);

    [LibraryImport(LibraryName)]
    internal static partial void XGameSaveCloseUpdate(IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveSubmitBlobWrite(
        IntPtr updateContext,
        byte* blobName,
        byte* data,
        nuint byteCount);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveSubmitBlobDelete(
        IntPtr updateContext,
        byte* blobName);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveSubmitUpdate(IntPtr updateContext);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveSubmitUpdateAsync(
        IntPtr updateContext,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveSubmitUpdateResult(XAsyncBlock* async);

    // ── XGameSaveFiles ─────────────────────────────────────────────────────────

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveFilesGetFolderWithUiAsync(
        IntPtr requestingUser,
        byte* configurationId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveFilesGetFolderWithUiResult(
        XAsyncBlock* async,
        nuint folderSize,
        byte* folderResult);

    [LibraryImport(LibraryName)]
    internal static partial int XGameSaveFilesGetRemainingQuota(
        IntPtr userContext,
        byte* configurationId,
        long* remainingQuota);
}

#else

internal static unsafe partial class Native
{
    // ── XGameSave: Provider ────────────────────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveInitializeProvider(
        IntPtr requestingUser,
        byte* configurationId,
        byte syncOnDemand,
        IntPtr* provider);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveInitializeProviderAsync(
        IntPtr requestingUser,
        byte* configurationId,
        byte syncOnDemand,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveInitializeProviderResult(
        XAsyncBlock* async,
        IntPtr* provider);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameSaveCloseProvider(IntPtr provider);

    // ── XGameSave: Quota ───────────────────────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveGetRemainingQuota(
        IntPtr provider,
        long* remainingQuota);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveGetRemainingQuotaAsync(
        IntPtr provider,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveGetRemainingQuotaResult(
        XAsyncBlock* async,
        long* remainingQuota);

    // ── XGameSave: Container management ───────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveDeleteContainer(
        IntPtr provider,
        byte* containerName);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveDeleteContainerAsync(
        IntPtr provider,
        byte* containerName,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveDeleteContainerResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveGetContainerInfo(
        IntPtr provider,
        byte* containerName,
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveEnumerateContainerInfo(
        IntPtr provider,
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveEnumerateContainerInfoByName(
        IntPtr provider,
        byte* containerNamePrefix,
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveCreateContainer(
        IntPtr provider,
        byte* containerName,
        IntPtr* containerContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameSaveCloseContainer(IntPtr context);

    // ── XGameSave: Blob enumeration ────────────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveEnumerateBlobInfo(
        IntPtr container,
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveEnumerateBlobInfoByName(
        IntPtr container,
        byte* blobNamePrefix,
        IntPtr context,
        IntPtr callback);

    // ── XGameSave: Blob read ───────────────────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveReadBlobData(
        IntPtr container,
        byte** blobNames,
        uint* countOfBlobs,
        nuint blobsSize,
        NativeGameSaveBlob* blobData);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveReadBlobDataAsync(
        IntPtr container,
        byte** blobNames,
        uint countOfBlobs,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveReadBlobDataResult(
        XAsyncBlock* async,
        nuint blobsSize,
        NativeGameSaveBlob* blobData,
        uint* countOfBlobs);

    // ── XGameSave: Update ──────────────────────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveCreateUpdate(
        IntPtr container,
        byte* containerDisplayName,
        IntPtr* updateContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameSaveCloseUpdate(IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveSubmitBlobWrite(
        IntPtr updateContext,
        byte* blobName,
        byte* data,
        nuint byteCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveSubmitBlobDelete(
        IntPtr updateContext,
        byte* blobName);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveSubmitUpdate(IntPtr updateContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveSubmitUpdateAsync(
        IntPtr updateContext,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveSubmitUpdateResult(XAsyncBlock* async);

    // ── XGameSaveFiles ─────────────────────────────────────────────────────────

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveFilesGetFolderWithUiAsync(
        IntPtr requestingUser,
        byte* configurationId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveFilesGetFolderWithUiResult(
        XAsyncBlock* async,
        nuint folderSize,
        byte* folderResult);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameSaveFilesGetRemainingQuota(
        IntPtr userContext,
        byte* configurationId,
        long* remainingQuota);
}

#endif
