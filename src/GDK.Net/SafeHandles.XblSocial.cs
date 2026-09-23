using System;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// Owns an <c>XblSocialRelationshipResultHandle</c>; released with
/// <c>XblSocialRelationshipResultCloseHandle</c>.
/// </summary>
/// <remarks>
/// The handle owns the <c>XblSocialRelationship</c> array and its string graph, so the social
/// projection snapshots everything into managed objects before callers can dispose the page.
/// </remarks>
internal sealed class SocialRelationshipResultHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal SocialRelationshipResultHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativeXbl.XblSocialRelationshipResultCloseHandle(handle);
        return true;
    }
}


