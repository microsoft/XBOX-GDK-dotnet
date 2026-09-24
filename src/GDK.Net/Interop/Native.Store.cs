// P/Invoke declarations for the XStore family.
//
// Dual shim: [LibraryImport] on net7+ for AOT/trimming, [DllImport] on netstandard2.0.
// All parameters are blittable:  byte* for UTF-8 strings, byte for bool, IntPtr for opaque handles.
// Callback function pointers are always passed as IntPtr; the calling convention is enforced by the
// trampolines in StoreContext / StoreLicense / StoreProductQuery.
//
// ────────────────────────────────────────────────────────────────────────────────────────────────
// The whole of XStore.h is bound here. Seven of these entry points: the gifting UI, the legacy
// per-package-identifier update query, and the associated-products-by-store-id pair: were missing
// from xgameruntime.thunks.dll's export table until GDK edition 260404 added them. 260404 is this
// projection's minimum, so no XStore API needs a workaround any more.
// ────────────────────────────────────────────────────────────────────────────────────────────────

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XStore.h: context ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreCreateContext(
        IntPtr user,
        IntPtr* storeContextHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XStoreCloseContextHandle(IntPtr storeContextHandle);

    // --- XStore.h: game licence ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryGameLicenseAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryGameLicenseResult(
        XAsyncBlock* async,
        XStoreGameLicense* license);

    // --- XStore.h: add-on licences ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAddOnLicensesAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAddOnLicensesResultCount(
        XAsyncBlock* async,
        uint* count);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAddOnLicensesResult(
        XAsyncBlock* async,
        uint count,
        XStoreAddonLicense* addOnLicenses);

    // --- XStore.h: licence token ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryLicenseTokenAsync(
        IntPtr storeContextHandle,
        byte** productIds,
        nuint productIdsCount,
        byte* customDeveloperString,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryLicenseTokenResultSize(
        XAsyncBlock* async,
        nuint* size);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryLicenseTokenResult(
        XAsyncBlock* async,
        nuint size,
        byte* result);

    // --- XStore.h: acquire licence for package ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreAcquireLicenseForPackageAsync(
        IntPtr storeContextHandle,
        byte* packageIdentifier,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreAcquireLicenseForPackageResult(
        XAsyncBlock* async,
        IntPtr* storeLicenseHandle);

    // --- XStore.h: acquire licence for durables ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreAcquireLicenseForDurablesAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreAcquireLicenseForDurablesResult(
        XAsyncBlock* async,
        IntPtr* storeLicenseHandle);

    // --- XStore.h: can acquire licence ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreCanAcquireLicenseForPackageAsync(
        IntPtr storeContextHandle,
        byte* packageIdentifier,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreCanAcquireLicenseForPackageResult(
        XAsyncBlock* async,
        XStoreCanAcquireLicenseResult* result);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreCanAcquireLicenseForStoreIdAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreCanAcquireLicenseForStoreIdResult(
        XAsyncBlock* async,
        XStoreCanAcquireLicenseResult* result);

    // --- XStore.h: licence validity ---

    [LibraryImport(LibraryName)]
    internal static partial byte XStoreIsLicenseValid(IntPtr storeLicenseHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XStoreCloseLicenseHandle(IntPtr storeLicenseHandle);

    // --- XStore.h: game licence changed event ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreRegisterGameLicenseChanged(
        IntPtr storeContextHandle,
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XStoreUnregisterGameLicenseChanged(
        IntPtr storeContextHandle,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XStore.h: package licence lost event ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreRegisterPackageLicenseLost(
        IntPtr licenseHandle,
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XStoreUnregisterPackageLicenseLost(
        IntPtr licenseHandle,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XStore.h: product queries ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryProductsAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        byte** storeIds,
        nuint storeIdsCount,
        byte** actionFilters,
        nuint actionFiltersCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryProductsResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryProductForCurrentGameAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryProductForCurrentGameResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryProductForPackageAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        byte* packageIdentifier,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryProductForPackageResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAssociatedProductsAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        uint maxItemsToRetrievePerPage,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAssociatedProductsResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryEntitledProductsAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        uint maxItemsToRetrievePerPage,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryEntitledProductsResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    // --- XStore.h: product query enumeration and paging ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreEnumerateProductsQuery(
        IntPtr productQueryHandle,
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial byte XStoreProductsQueryHasMorePages(IntPtr productQueryHandle);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreProductsQueryNextPageAsync(
        IntPtr productQueryHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreProductsQueryNextPageResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XStoreCloseProductsQueryHandle(IntPtr productQueryHandle);

    // --- XStore.h: package identifier ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryPackageIdentifier(
        byte* storeId,
        nuint size,
        byte* packageIdentifier);

    // --- XStore.h: availability ---

    [LibraryImport(LibraryName)]
    internal static partial byte XStoreIsAvailabilityPurchasable(XStoreAvailability availability);

    // --- XStore.h: consumables ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryConsumableBalanceRemainingAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryConsumableBalanceRemainingResult(
        XAsyncBlock* async,
        XStoreConsumableResult* consumableResult);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreReportConsumableFulfillmentAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        uint quantity,
        Guid trackingId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreReportConsumableFulfillmentResult(
        XAsyncBlock* async,
        XStoreConsumableResult* consumableResult);

    // --- XStore.h: user purchase / collections IDs ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreGetUserPurchaseIdAsync(
        IntPtr storeContextHandle,
        byte* serviceTicket,
        byte* publisherUserId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreGetUserPurchaseIdResultSize(
        XAsyncBlock* async,
        nuint* size);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreGetUserPurchaseIdResult(
        XAsyncBlock* async,
        nuint size,
        byte* result);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreGetUserCollectionsIdAsync(
        IntPtr storeContextHandle,
        byte* serviceTicket,
        byte* publisherUserId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreGetUserCollectionsIdResultSize(
        XAsyncBlock* async,
        nuint* size);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreGetUserCollectionsIdResult(
        XAsyncBlock* async,
        nuint size,
        byte* result);

    // --- XStore.h: package updates ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryGameAndDlcPackageUpdatesAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryGameAndDlcPackageUpdatesResultCount(
        XAsyncBlock* async,
        uint* count);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryGameAndDlcPackageUpdatesResult(
        XAsyncBlock* async,
        uint count,
        XStorePackageUpdate* packageUpdates);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadPackageUpdatesAsync(
        IntPtr storeContextHandle,
        byte** packageIdentifiers,
        nuint packageIdentifiersCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadPackageUpdatesResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadAndInstallPackageUpdatesAsync(
        IntPtr storeContextHandle,
        byte** packageIdentifiers,
        nuint packageIdentifiersCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadAndInstallPackageUpdatesResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadAndInstallPackagesAsync(
        IntPtr storeContextHandle,
        byte** storeIds,
        nuint storeIdsCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadAndInstallPackagesResultCount(
        XAsyncBlock* async,
        uint* count);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreDownloadAndInstallPackagesResult(
        XAsyncBlock* async,
        uint count,
        byte* packageIdentifiers);

    // --- XStore.h: store UI ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowPurchaseUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        byte* name,
        byte* extendedJsonData,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowPurchaseUIResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowProductPageUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowProductPageUIResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowAssociatedProductsUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        XStoreProductKind productKinds,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowAssociatedProductsUIResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowRedeemTokenUIAsync(
        IntPtr storeContextHandle,
        byte* token,
        byte** allowedStoreIds,
        nuint allowedStoreIdsCount,
        byte disallowCsvRedemption,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowRedeemTokenUIResult(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowRateAndReviewUIAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowRateAndReviewUIResult(
        XAsyncBlock* async,
        XStoreRateAndReviewResult* result);

    // --- XStore.h: package updates (legacy, per-package-identifier) ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryPackageUpdatesAsync(
        IntPtr storeContextHandle,
        byte** packageIdentifiers,
        nuint packageIdentifiersCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryPackageUpdatesResultCount(XAsyncBlock* async, uint* count);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryPackageUpdatesResult(
        XAsyncBlock* async,
        uint count,
        XStorePackageUpdate* packageUpdates);

    // --- XStore.h: gifting UI ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowGiftingUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        byte* name,
        byte* extendedJsonData,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreShowGiftingUIResult(XAsyncBlock* async);

    // --- XStore.h: associated products for an explicit store id ---

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAssociatedProductsForStoreIdAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        XStoreProductKind productKinds,
        uint maxItemsToRetrievePerPage,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XStoreQueryAssociatedProductsForStoreIdResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);
}

#else

internal static unsafe partial class Native
{
    // --- XStore.h: context ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreCreateContext(
        IntPtr user,
        IntPtr* storeContextHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XStoreCloseContextHandle(IntPtr storeContextHandle);

    // --- XStore.h: game licence ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryGameLicenseAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryGameLicenseResult(
        XAsyncBlock* async,
        XStoreGameLicense* license);

    // --- XStore.h: add-on licences ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAddOnLicensesAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAddOnLicensesResultCount(
        XAsyncBlock* async,
        uint* count);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAddOnLicensesResult(
        XAsyncBlock* async,
        uint count,
        XStoreAddonLicense* addOnLicenses);

    // --- XStore.h: licence token ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryLicenseTokenAsync(
        IntPtr storeContextHandle,
        byte** productIds,
        nuint productIdsCount,
        byte* customDeveloperString,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryLicenseTokenResultSize(
        XAsyncBlock* async,
        nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryLicenseTokenResult(
        XAsyncBlock* async,
        nuint size,
        byte* result);

    // --- XStore.h: acquire licence for package ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreAcquireLicenseForPackageAsync(
        IntPtr storeContextHandle,
        byte* packageIdentifier,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreAcquireLicenseForPackageResult(
        XAsyncBlock* async,
        IntPtr* storeLicenseHandle);

    // --- XStore.h: acquire licence for durables ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreAcquireLicenseForDurablesAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreAcquireLicenseForDurablesResult(
        XAsyncBlock* async,
        IntPtr* storeLicenseHandle);

    // --- XStore.h: can acquire licence ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreCanAcquireLicenseForPackageAsync(
        IntPtr storeContextHandle,
        byte* packageIdentifier,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreCanAcquireLicenseForPackageResult(
        XAsyncBlock* async,
        XStoreCanAcquireLicenseResult* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreCanAcquireLicenseForStoreIdAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreCanAcquireLicenseForStoreIdResult(
        XAsyncBlock* async,
        XStoreCanAcquireLicenseResult* result);

    // --- XStore.h: licence validity ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XStoreIsLicenseValid(IntPtr storeLicenseHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XStoreCloseLicenseHandle(IntPtr storeLicenseHandle);

    // --- XStore.h: game licence changed event ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreRegisterGameLicenseChanged(
        IntPtr storeContextHandle,
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XStoreUnregisterGameLicenseChanged(
        IntPtr storeContextHandle,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XStore.h: package licence lost event ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreRegisterPackageLicenseLost(
        IntPtr licenseHandle,
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XStoreUnregisterPackageLicenseLost(
        IntPtr licenseHandle,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XStore.h: product queries ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryProductsAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        byte** storeIds,
        nuint storeIdsCount,
        byte** actionFilters,
        nuint actionFiltersCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryProductsResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryProductForCurrentGameAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryProductForCurrentGameResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryProductForPackageAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        byte* packageIdentifier,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryProductForPackageResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAssociatedProductsAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        uint maxItemsToRetrievePerPage,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAssociatedProductsResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryEntitledProductsAsync(
        IntPtr storeContextHandle,
        XStoreProductKind productKinds,
        uint maxItemsToRetrievePerPage,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryEntitledProductsResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    // --- XStore.h: product query enumeration and paging ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreEnumerateProductsQuery(
        IntPtr productQueryHandle,
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XStoreProductsQueryHasMorePages(IntPtr productQueryHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreProductsQueryNextPageAsync(
        IntPtr productQueryHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreProductsQueryNextPageResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XStoreCloseProductsQueryHandle(IntPtr productQueryHandle);

    // --- XStore.h: package identifier ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryPackageIdentifier(
        byte* storeId,
        nuint size,
        byte* packageIdentifier);

    // --- XStore.h: availability ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XStoreIsAvailabilityPurchasable(XStoreAvailability availability);

    // --- XStore.h: consumables ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryConsumableBalanceRemainingAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryConsumableBalanceRemainingResult(
        XAsyncBlock* async,
        XStoreConsumableResult* consumableResult);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreReportConsumableFulfillmentAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        uint quantity,
        Guid trackingId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreReportConsumableFulfillmentResult(
        XAsyncBlock* async,
        XStoreConsumableResult* consumableResult);

    // --- XStore.h: user purchase / collections IDs ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreGetUserPurchaseIdAsync(
        IntPtr storeContextHandle,
        byte* serviceTicket,
        byte* publisherUserId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreGetUserPurchaseIdResultSize(
        XAsyncBlock* async,
        nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreGetUserPurchaseIdResult(
        XAsyncBlock* async,
        nuint size,
        byte* result);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreGetUserCollectionsIdAsync(
        IntPtr storeContextHandle,
        byte* serviceTicket,
        byte* publisherUserId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreGetUserCollectionsIdResultSize(
        XAsyncBlock* async,
        nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreGetUserCollectionsIdResult(
        XAsyncBlock* async,
        nuint size,
        byte* result);

    // --- XStore.h: package updates ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryGameAndDlcPackageUpdatesAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryGameAndDlcPackageUpdatesResultCount(
        XAsyncBlock* async,
        uint* count);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryGameAndDlcPackageUpdatesResult(
        XAsyncBlock* async,
        uint count,
        XStorePackageUpdate* packageUpdates);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadPackageUpdatesAsync(
        IntPtr storeContextHandle,
        byte** packageIdentifiers,
        nuint packageIdentifiersCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadPackageUpdatesResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadAndInstallPackageUpdatesAsync(
        IntPtr storeContextHandle,
        byte** packageIdentifiers,
        nuint packageIdentifiersCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadAndInstallPackageUpdatesResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadAndInstallPackagesAsync(
        IntPtr storeContextHandle,
        byte** storeIds,
        nuint storeIdsCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadAndInstallPackagesResultCount(
        XAsyncBlock* async,
        uint* count);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreDownloadAndInstallPackagesResult(
        XAsyncBlock* async,
        uint count,
        byte* packageIdentifiers);

    // --- XStore.h: store UI ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowPurchaseUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        byte* name,
        byte* extendedJsonData,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowPurchaseUIResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowProductPageUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowProductPageUIResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowAssociatedProductsUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        XStoreProductKind productKinds,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowAssociatedProductsUIResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowRedeemTokenUIAsync(
        IntPtr storeContextHandle,
        byte* token,
        byte** allowedStoreIds,
        nuint allowedStoreIdsCount,
        byte disallowCsvRedemption,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowRedeemTokenUIResult(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowRateAndReviewUIAsync(
        IntPtr storeContextHandle,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowRateAndReviewUIResult(
        XAsyncBlock* async,
        XStoreRateAndReviewResult* result);

    // --- XStore.h: package updates (legacy, per-package-identifier) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryPackageUpdatesAsync(
        IntPtr storeContextHandle,
        byte** packageIdentifiers,
        nuint packageIdentifiersCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryPackageUpdatesResultCount(XAsyncBlock* async, uint* count);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryPackageUpdatesResult(
        XAsyncBlock* async,
        uint count,
        XStorePackageUpdate* packageUpdates);

    // --- XStore.h: gifting UI ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowGiftingUIAsync(
        IntPtr storeContextHandle,
        byte* storeId,
        byte* name,
        byte* extendedJsonData,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreShowGiftingUIResult(XAsyncBlock* async);

    // --- XStore.h: associated products for an explicit store id ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAssociatedProductsForStoreIdAsync(
        IntPtr storeContextHandle,
        byte* storeProductId,
        XStoreProductKind productKinds,
        uint maxItemsToRetrievePerPage,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XStoreQueryAssociatedProductsForStoreIdResult(
        XAsyncBlock* async,
        IntPtr* productQueryHandle);
}

#endif
