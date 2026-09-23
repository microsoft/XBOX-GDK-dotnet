using System;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// Owns an <c>XblPresenceRecordHandle</c>; released with
/// <c>XblPresenceRecordCloseHandle</c>.
/// </summary>
/// <remarks>
/// The handle owns all device, title and broadcast records reachable from the presence record, so
/// the Xbox Live presence projection snapshots that graph before this handle is closed.
/// </remarks>
internal sealed class PresenceRecordHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PresenceRecordHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativeXbl.XblPresenceRecordCloseHandle(handle);
        return true;
    }
}
