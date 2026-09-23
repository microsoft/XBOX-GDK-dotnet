// Raw interop types for the XUserPlatform* section of XUser.h (GDK edition 260404).
// See Interop/NativeTypes.cs for conventions.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XUserPlatformOperationResult</c> from XUser.h.</summary>
internal enum XUserPlatformOperationResult
{
    Success = 0,
    Failure = 1,
    Canceled = 2,
}

/// <summary>Mirrors <c>XUserPlatformSpopOperationResult</c> from XUser.h.</summary>
internal enum XUserPlatformSpopOperationResult
{
    SignInHere = 0,
    SwitchAccount = 1,
    Failure = 2,
    Canceled = 3,
}

/// <summary>Mirrors <c>struct XUserPlatformRemoteConnectEventHandlers</c> from XUser.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XUserPlatformRemoteConnectEventHandlers
{
    public IntPtr Show;
    public IntPtr Close;
    public IntPtr Context;
}
