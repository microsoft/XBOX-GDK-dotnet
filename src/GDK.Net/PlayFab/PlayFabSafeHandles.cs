using System;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net.PlayFab;

/// <summary>Owns a <c>PFEntityHandle</c>; released with <c>PFEntityCloseHandle</c>.</summary>
internal sealed class PlayFabEntityHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PlayFabEntityHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativePlayFab.PFEntityCloseHandle(handle);
        return true;
    }
}

/// <summary>Owns a <c>PFServiceConfigHandle</c>; released with <c>PFServiceConfigCloseHandle</c>.</summary>
internal sealed class PlayFabServiceConfigHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PlayFabServiceConfigHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativePlayFab.PFServiceConfigCloseHandle(handle);
        return true;
    }
}

/// <summary>Owns a <c>PFLocalUserHandle</c>; released with <c>PFLocalUserCloseHandle</c>.</summary>
internal sealed class PlayFabLocalUserHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PlayFabLocalUserHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativePlayFab.PFLocalUserCloseHandle(handle);
        return true;
    }
}

/// <summary>
/// Shared plumbing for the PlayFab two-call size/buffer idiom, which appears on every handle
/// accessor in <c>PFEntity.h</c>, <c>PFServiceConfig.h</c> and <c>PFLocalUser.h</c>.
/// </summary>
internal static unsafe class PlayFabInterop
{
    /// <summary>Queries the buffer size a value needs, in bytes.</summary>
    internal delegate int SizeQuery(IntPtr handle, nuint* size);

    /// <summary>Fills a caller-supplied UTF-8 buffer and reports how much of it was used.</summary>
    internal delegate int StringQuery(IntPtr handle, nuint size, byte* buffer, nuint* used);

    /// <summary>
    /// Runs a size/read pair and copies the result out as a managed string, so the two-call idiom
    /// never reaches a caller.
    /// </summary>
    internal static string GetString(IntPtr handle, SizeQuery sizeQuery, StringQuery stringQuery)
    {
        nuint size;
        Hr.ThrowIfFailed(sizeQuery(handle, &size));
        if (size == 0)
        {
            return string.Empty;
        }

        IntPtr buffer = Marshal.AllocHGlobal(checked((int)size));
        try
        {
            nuint used;
            Hr.ThrowIfFailed(stringQuery(handle, size, (byte*)buffer, &used));
            return Utf8.ToString((byte*)buffer, checked((int)used));
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}
