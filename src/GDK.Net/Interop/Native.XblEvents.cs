// P/Invoke declarations for xsapi-c\events_c.h -- the Xbox Live events service.
//
// Part of the NativeXbl partial class; see Native.Xbl.cs for the module's loading rules, the
// LibraryName constant and the two-shim convention.
//
// XblEventsSetStorageAllotment and XblEventsSetMaxFileSize are declared only under
// XSAPI_INTERNAL_EVENTS_SERVICE and are not exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK
// edition 260404. They are therefore intentionally not bound here.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial int XblEventsWriteInGameEvent(
        IntPtr xboxLiveContext,
        byte* eventName,
        byte* dimensionsJson,
        byte* measurementsJson);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblEventsWriteInGameEvent(
        IntPtr xboxLiveContext,
        byte* eventName,
        byte* dimensionsJson,
        byte* measurementsJson);
}

#endif
