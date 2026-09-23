// P/Invoke declarations for XPackage.h.
// All parameters are blittable: byte for bool, IntPtr for opaque handles and callbacks,
// byte* for UTF-8 char*, nuint for size_t.
//
// Handles:
//   XPackageMountHandle          -> IntPtr
//   XPackageInstallationMonitorHandle -> IntPtr
//   XTaskQueueHandle             -> IntPtr
//
// Callbacks (all via IntPtr function-pointer; trampolines in the Package layer):
//   XPackageEnumerationCallback           -> IntPtr
//   XPackageChunkAvailabilityCallback     -> IntPtr
//   XPackageFeatureEnumerationCallback    -> IntPtr
//   XPackageInstalledCallback             -> IntPtr
//   XPackageInstallationProgressCallback  -> IntPtr
//
// UNEXPORTED APIs — NOT declared here (verified against xgameruntime.thunks.dll export table):
//
//   XPackageMount               — deprecated; superseded by XPackageMountWithUiAsync.
//                                 Absent from the thunks DLL; would throw EntryPointNotFoundException.
//   XPackageGetIdentifier       — absent from the thunks DLL; use
//                                 XPackageGetCurrentProcessPackageIdentifier instead.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XPackage: process identity ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetCurrentProcessPackageIdentifier(
        nuint bufferSize,
        byte* buffer);

    [LibraryImport(LibraryName)]
    internal static partial byte XPackageIsPackagedProcess();

    // --- XPackage: installation monitor ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageCreateInstallationMonitor(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        uint minimumUpdateIntervalMs,
        IntPtr queue,
        IntPtr* installationMonitor);

    [LibraryImport(LibraryName)]
    internal static partial void XPackageCloseInstallationMonitorHandle(IntPtr installationMonitor);

    [LibraryImport(LibraryName)]
    internal static partial void XPackageGetInstallationProgress(
        IntPtr installationMonitor,
        XPackageInstallationProgress* progress);

    [LibraryImport(LibraryName)]
    internal static partial byte XPackageUpdateInstallationMonitor(IntPtr installationMonitor);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageRegisterInstallationProgressChanged(
        IntPtr installationMonitor,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XPackageUnregisterInstallationProgressChanged(
        IntPtr installationMonitor,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XPackage: locale ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetUserLocale(
        nuint localeSize,
        byte* locale);

    // --- XPackage: chunk availability ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageFindChunkAvailability(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        XPackageChunkAvailability* availability);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageEnumerateChunkAvailability(
        byte* packageIdentifier,
        XPackageChunkSelectorType type,
        IntPtr context,
        IntPtr callback);

    // --- XPackage: chunk install management ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageChangeChunkInstallOrder(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageInstallChunks(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        uint minimumUpdateIntervalMs,
        byte suppressUserConfirmation,
        IntPtr queue,
        IntPtr* installationMonitor);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageInstallChunksAsync(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        uint minimumUpdateIntervalMs,
        byte suppressUserConfirmation,
        XAsyncBlock* asyncBlock);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageInstallChunksResult(
        XAsyncBlock* asyncBlock,
        IntPtr* installationMonitor);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageEstimateDownloadSize(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        ulong* downloadSize,
        byte* shouldPresentUserConfirmation);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageUninstallChunks(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetPackageKind(
        byte* packageIdentifier,
        XPackageKind* kind);

    // --- XPackage: enumeration ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageEnumeratePackages(
        XPackageKind kind,
        XPackageEnumerationScope scope,
        IntPtr context,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageRegisterPackageInstalled(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XPackageUnregisterPackageInstalled(
        XTaskQueueRegistrationToken token,
        byte wait);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageEnumerateFeatures(
        byte* packageIdentifier,
        IntPtr context,
        IntPtr callback);

    // --- XPackage: mount ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageMountWithUiAsync(
        byte* packageIdentifier,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageMountWithUiResult(
        XAsyncBlock* async,
        IntPtr* mount);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetMountPathSize(
        IntPtr mount,
        nuint* pathSize);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetMountPath(
        IntPtr mount,
        nuint pathSize,
        byte* path);

    [LibraryImport(LibraryName)]
    internal static partial void XPackageCloseMountHandle(IntPtr mount);

    // --- XPackage: write stats / uninstall ---

    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetWriteStats(XPackageWriteStats* writeStats);

    [LibraryImport(LibraryName)]
    internal static partial int XPackageUninstallUWPInstance(byte* packageName);

    [LibraryImport(LibraryName)]
    internal static partial byte XPackageUninstallPackage(byte* packageIdentifier);

}

#else

internal static unsafe partial class Native
{
    // --- XPackage: process identity ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetCurrentProcessPackageIdentifier(
        nuint bufferSize,
        byte* buffer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XPackageIsPackagedProcess();

    // --- XPackage: installation monitor ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageCreateInstallationMonitor(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        uint minimumUpdateIntervalMs,
        IntPtr queue,
        IntPtr* installationMonitor);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XPackageCloseInstallationMonitorHandle(IntPtr installationMonitor);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XPackageGetInstallationProgress(
        IntPtr installationMonitor,
        XPackageInstallationProgress* progress);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XPackageUpdateInstallationMonitor(IntPtr installationMonitor);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageRegisterInstallationProgressChanged(
        IntPtr installationMonitor,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XPackageUnregisterInstallationProgressChanged(
        IntPtr installationMonitor,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XPackage: locale ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetUserLocale(
        nuint localeSize,
        byte* locale);

    // --- XPackage: chunk availability ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageFindChunkAvailability(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        XPackageChunkAvailability* availability);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageEnumerateChunkAvailability(
        byte* packageIdentifier,
        XPackageChunkSelectorType type,
        IntPtr context,
        IntPtr callback);

    // --- XPackage: chunk install management ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageChangeChunkInstallOrder(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageInstallChunks(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        uint minimumUpdateIntervalMs,
        byte suppressUserConfirmation,
        IntPtr queue,
        IntPtr* installationMonitor);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageInstallChunksAsync(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        uint minimumUpdateIntervalMs,
        byte suppressUserConfirmation,
        XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageInstallChunksResult(
        XAsyncBlock* asyncBlock,
        IntPtr* installationMonitor);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageEstimateDownloadSize(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors,
        ulong* downloadSize,
        byte* shouldPresentUserConfirmation);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageUninstallChunks(
        byte* packageIdentifier,
        uint selectorCount,
        XPackageChunkSelector* selectors);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetPackageKind(
        byte* packageIdentifier,
        XPackageKind* kind);

    // --- XPackage: enumeration ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageEnumeratePackages(
        XPackageKind kind,
        XPackageEnumerationScope scope,
        IntPtr context,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageRegisterPackageInstalled(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XPackageUnregisterPackageInstalled(
        XTaskQueueRegistrationToken token,
        byte wait);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageEnumerateFeatures(
        byte* packageIdentifier,
        IntPtr context,
        IntPtr callback);

    // --- XPackage: mount ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageMountWithUiAsync(
        byte* packageIdentifier,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageMountWithUiResult(
        XAsyncBlock* async,
        IntPtr* mount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetMountPathSize(
        IntPtr mount,
        nuint* pathSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetMountPath(
        IntPtr mount,
        nuint pathSize,
        byte* path);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XPackageCloseMountHandle(IntPtr mount);

    // --- XPackage: write stats / uninstall ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetWriteStats(XPackageWriteStats* writeStats);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageUninstallUWPInstance(byte* packageName);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XPackageUninstallPackage(byte* packageIdentifier);

}

#endif
