using System;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// Owns an <c>XblAchievementsManagerResultHandle</c>; released with
/// <c>XblAchievementsManagerResultCloseHandle</c>.
/// </summary>
/// <remarks>
/// The handle owns the cached <c>XblAchievement</c> array returned by achievements-manager queries.
/// The projection snapshots those achievements before callers can close the handle.
/// </remarks>
internal sealed class AchievementsManagerResultHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal AchievementsManagerResultHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    internal unsafe AchievementsManagerResultHandle Duplicate()
    {
        IntPtr duplicated;
        Hr.ThrowIfFailed(NativeXbl.XblAchievementsManagerResultDuplicateHandle(handle, &duplicated));
        return new AchievementsManagerResultHandle(duplicated);
    }

    protected override bool ReleaseHandle()
    {
        NativeXbl.XblAchievementsManagerResultCloseHandle(handle);
        return true;
    }
}
