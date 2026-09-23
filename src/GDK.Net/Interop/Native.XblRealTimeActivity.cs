// P/Invoke declarations for xsapi-c\real_time_activity_c.h -- the Xbox Live real-time activity
// service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules,
// LibraryName and the two-shim convention.
//
// All entry points declared below are exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK
// edition 260404. The notification registrations are XSAPI-style handlers: Add returns the
// XblFunctionContext directly (0 means failure), and Remove returns an HRESULT.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblRealTimeActivityAddConnectionStateChangeHandler(
        IntPtr xboxLiveContext,
        IntPtr handler,
        IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XblRealTimeActivityRemoveConnectionStateChangeHandler(
        IntPtr xboxLiveContext,
        int token);

    [LibraryImport(LibraryName)]
    internal static partial int XblRealTimeActivityAddResyncHandler(
        IntPtr xboxLiveContext,
        IntPtr handler,
        IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XblRealTimeActivityRemoveResyncHandler(
        IntPtr xboxLiveContext,
        int token);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblRealTimeActivityAddConnectionStateChangeHandler(
        IntPtr xboxLiveContext,
        IntPtr handler,
        IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblRealTimeActivityRemoveConnectionStateChangeHandler(
        IntPtr xboxLiveContext,
        int token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblRealTimeActivityAddResyncHandler(
        IntPtr xboxLiveContext,
        IntPtr handler,
        IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblRealTimeActivityRemoveResyncHandler(
        IntPtr xboxLiveContext,
        int token);
}

#endif
