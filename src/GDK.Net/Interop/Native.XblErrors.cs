// P/Invoke declarations for xsapi-c\errors_c.h -- Xbox Live HRESULT condition mapping.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial XblErrorCondition XblGetErrorCondition(int hr);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern XblErrorCondition XblGetErrorCondition(int hr);
}

#endif
