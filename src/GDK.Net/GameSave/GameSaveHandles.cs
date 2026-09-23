using System;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net.GameSave;

/// <summary>Owns an <c>XGameSaveProviderHandle</c>; released with <c>XGameSaveCloseProvider</c>.</summary>
internal sealed class GameSaveProviderHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal GameSaveProviderHandle()
        : base(ownsHandle: true)
    {
    }

    internal GameSaveProviderHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XGameSaveCloseProvider(handle);
        return true;
    }
}

/// <summary>Owns an <c>XGameSaveContainerHandle</c>; released with <c>XGameSaveCloseContainer</c>.</summary>
internal sealed class GameSaveContainerHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal GameSaveContainerHandle()
        : base(ownsHandle: true)
    {
    }

    internal GameSaveContainerHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XGameSaveCloseContainer(handle);
        return true;
    }
}

/// <summary>Owns an <c>XGameSaveUpdateHandle</c>; released with <c>XGameSaveCloseUpdate</c>.</summary>
internal sealed class GameSaveUpdateHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal GameSaveUpdateHandle()
        : base(ownsHandle: true)
    {
    }

    internal GameSaveUpdateHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XGameSaveCloseUpdate(handle);
        return true;
    }
}
