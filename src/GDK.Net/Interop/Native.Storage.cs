// P/Invoke declarations and raw types for the persistent-local-storage family
// (XPersistentLocalStorage.h).
//
// XPersistentLocalStorageMountForPackage was absent from xgameruntime.thunks.dll's export table
// until GDK edition 260404 added it. 260404 is this projection's minimum, so it is bound below.
//
// See Interop/Native.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XPersistentLocalStorageSpaceInfo</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XPersistentLocalStorageSpaceInfo
{
    internal ulong AvailableFreeBytes;
    internal ulong TotalFreeBytes;
    internal ulong UsedBytes;
    internal ulong TotalBytes;
}

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    [LibraryImport(LibraryName)]
    internal static partial int XPersistentLocalStorageGetPathSize(nuint* pathSize);

    [LibraryImport(LibraryName)]
    internal static partial int XPersistentLocalStorageGetPath(nuint pathSize, byte* path, nuint* pathUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XPersistentLocalStorageGetSpaceInfo(XPersistentLocalStorageSpaceInfo* spaceInfo);

    [LibraryImport(LibraryName)]
    internal static partial int XPersistentLocalStoragePromptUserForSpaceAsync(
        ulong requestedBytes,
        XAsyncBlock* asyncBlock);

    [LibraryImport(LibraryName)]
    internal static partial int XPersistentLocalStoragePromptUserForSpaceResult(XAsyncBlock* asyncBlock);

    // --- XPersistentLocalStorage.h: mounting another package's storage ---

    [LibraryImport(LibraryName)]
    internal static partial int XPersistentLocalStorageMountForPackage(
        byte* packageIdentifier,
        IntPtr* mountHandle);
}

#else

internal static unsafe partial class Native
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPersistentLocalStorageGetPathSize(nuint* pathSize);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPersistentLocalStorageGetPath(nuint pathSize, byte* path, nuint* pathUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPersistentLocalStorageGetSpaceInfo(XPersistentLocalStorageSpaceInfo* spaceInfo);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPersistentLocalStoragePromptUserForSpaceAsync(
        ulong requestedBytes,
        XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPersistentLocalStoragePromptUserForSpaceResult(XAsyncBlock* asyncBlock);

    // --- XPersistentLocalStorage.h: mounting another package's storage ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPersistentLocalStorageMountForPackage(
        byte* packageIdentifier,
        IntPtr* mountHandle);
}

#endif
