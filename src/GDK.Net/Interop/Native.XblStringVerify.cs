// P/Invoke declarations for xsapi-c\string_verify_c.h -- the Xbox Live string verification service.
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
    internal static partial int XblStringVerifyStringAsync(
        IntPtr xboxLiveContext,
        byte* stringToVerify,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblStringVerifyStringResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblStringVerifyStringResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblVerifyStringResult** ptrToBuffer,
        nuint* bufferUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XblStringVerifyStringsAsync(
        IntPtr xboxLiveContext,
        byte** stringsToVerify,
        ulong stringsCount,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblStringVerifyStringsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [LibraryImport(LibraryName)]
    internal static partial int XblStringVerifyStringsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblVerifyStringResult** ptrToBufferStrings,
        nuint* stringsCount,
        nuint* bufferUsed);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblStringVerifyStringAsync(
        IntPtr xboxLiveContext,
        byte* stringToVerify,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblStringVerifyStringResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblStringVerifyStringResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblVerifyStringResult** ptrToBuffer,
        nuint* bufferUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblStringVerifyStringsAsync(
        IntPtr xboxLiveContext,
        byte** stringsToVerify,
        ulong stringsCount,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblStringVerifyStringsResultSize(
        XAsyncBlock* async,
        nuint* resultSizeInBytes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblStringVerifyStringsResult(
        XAsyncBlock* async,
        nuint bufferSize,
        void* buffer,
        XblVerifyStringResult** ptrToBufferStrings,
        nuint* stringsCount,
        nuint* bufferUsed);
}

#endif
