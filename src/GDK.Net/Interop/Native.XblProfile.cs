// P/Invoke declarations for xsapi-c\profile_c.h -- the Xbox Live profile service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfileAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfileResult(XAsyncBlock* async, XblUserProfile* profile);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfilesAsync(
        IntPtr xboxLiveContext,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfilesResultCount(XAsyncBlock* async, nuint* profileCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfilesResult(
        XAsyncBlock* async,
        nuint profilesCount,
        XblUserProfile* profiles);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfilesForSocialGroupAsync(
        IntPtr xboxLiveContext,
        byte* socialGroup,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfilesForSocialGroupResultCount(
        XAsyncBlock* async,
        nuint* profileCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblProfileGetUserProfilesForSocialGroupResult(
        XAsyncBlock* async,
        nuint profilesCount,
        XblUserProfile* profiles);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfileAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfileResult(XAsyncBlock* async, XblUserProfile* profile);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfilesAsync(
        IntPtr xboxLiveContext,
        ulong* xboxUserIds,
        nuint xboxUserIdsCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfilesResultCount(XAsyncBlock* async, nuint* profileCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfilesResult(
        XAsyncBlock* async,
        nuint profilesCount,
        XblUserProfile* profiles);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfilesForSocialGroupAsync(
        IntPtr xboxLiveContext,
        byte* socialGroup,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfilesForSocialGroupResultCount(
        XAsyncBlock* async,
        nuint* profileCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblProfileGetUserProfilesForSocialGroupResult(
        XAsyncBlock* async,
        nuint profilesCount,
        XblUserProfile* profiles);
}

#endif
