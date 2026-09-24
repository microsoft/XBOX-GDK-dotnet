using System;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// Owns an <c>XTaskQueueHandle</c>.
/// </summary>
/// <remarks>
/// Task queues are the shutdown special case called out in docs/plan.md section 6: every handle,
/// including one produced by <c>XTaskQueueDuplicateHandle</c>, terminates and waits for termination
/// before closing, so no callback can be in flight once the handle is gone.
/// </remarks>
internal sealed class TaskQueueHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal TaskQueueHandle()
        : base(ownsHandle: true)
    {
    }

    internal TaskQueueHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XTaskQueueTerminate(handle, wait: 1, IntPtr.Zero, IntPtr.Zero);
        Native.XTaskQueueCloseHandle(handle);
        return true;
    }
}

/// <summary>Owns an <c>XUserHandle</c>; released with <c>XUserCloseHandle</c>.</summary>
internal sealed class UserHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal UserHandle()
        : base(ownsHandle: true)
    {
    }

    internal UserHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XUserCloseHandle(handle);
        return true;
    }
}

/// <summary>
/// Owns an <c>XSpeechSynthesizerHandle</c>; released with
/// <c>XSpeechSynthesizerCloseHandle</c>.
/// </summary>
internal sealed class SpeechSynthesizerHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal SpeechSynthesizerHandle()
        : base(ownsHandle: true)
    {
    }

    internal SpeechSynthesizerHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        // Return value is an HRESULT, ignored here: ReleaseHandle must not throw.
        Native.XSpeechSynthesizerCloseHandle(handle);
        return true;
    }
}

/// <summary>
/// Owns an <c>XblContextHandle</c>; released with <c>XblContextCloseHandle</c>.
/// </summary>
/// <remarks>
/// An Xbox Live context is per-user *and* per-sign-in: it is built from an <c>XUserHandle</c>, so
/// it stops being valid when that user signs out or is replaced. See docs/plan.md section 13.1:
/// the projection couples the handle's lifetime to its <c>User</c> and rebuilds on user change
/// rather than caching one for the process.
/// </remarks>
internal sealed class XboxLiveContextHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal XboxLiveContextHandle()
        : base(ownsHandle: true)
    {
    }

    internal XboxLiveContextHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativeXbl.XblContextCloseHandle(handle);
        return true;
    }
}

/// <summary>
/// Owns an <c>XblAchievementsResultHandle</c>; released with
/// <c>XblAchievementsResultCloseHandle</c>.
/// </summary>
/// <remarks>
/// The handle owns the <c>XblAchievement</c> array the service returned, so every achievement is
/// snapshot into managed objects while the handle is alive rather than being handed out as
/// pointers into runtime-owned memory.
/// </remarks>
internal sealed class AchievementsResultHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal AchievementsResultHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        NativeXbl.XblAchievementsResultCloseHandle(handle);
        return true;
    }
}

/// <summary>
/// Owns an <c>XUserSignOutDeferralHandle</c>; released with
/// <c>XUserCloseSignOutDeferralHandle</c>.
/// </summary>
internal sealed class SignOutDeferralHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal SignOutDeferralHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XUserCloseSignOutDeferralHandle(handle);
        return true;
    }
}
