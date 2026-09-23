using System;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// Owns an <c>XblTitleStorageBlobMetadataResultHandle</c>; released with
/// <c>XblTitleStorageBlobMetadataResultCloseHandle</c>.
/// </summary>
/// <remarks>
/// The handle owns the <c>XblTitleStorageBlobMetadata</c> array returned by the service, so the
/// projection snapshots every metadata item before closing the handle.
/// </remarks>
internal sealed class TitleStorageBlobMetadataResultHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal TitleStorageBlobMetadataResultHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativeXbl.XblTitleStorageBlobMetadataResultCloseHandle(handle);
        return true;
    }
}
