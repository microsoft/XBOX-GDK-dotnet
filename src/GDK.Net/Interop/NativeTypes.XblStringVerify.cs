// Blittable mirrors of the XSAPI string verification types -- xsapi-c\string_verify_c.h,
// GDK edition 260404.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblVerifyStringResultCode</c>.</summary>
internal enum XblVerifyStringResultCode : uint
{
    Success = 0,
    Offensive = 1,
    TooLong = 2,
    UnknownError = 3,
}

/// <summary>Mirrors <c>XblVerifyStringResult</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblVerifyStringResult
{
    internal XblVerifyStringResultCode ResultCode;
    internal byte* FirstOffendingSubstring;
}
