using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// Owns a caller-allocated <c>XblLeaderboardResult</c> buffer.
/// </summary>
/// <remarks>
/// Leaderboard results are not native handles. XSAPI writes a pointer graph into this unmanaged
/// buffer and returns an <c>XblLeaderboardResult*</c> that points inside it, so freeing the buffer
/// invalidates every native pointer in the graph.
/// </remarks>
internal sealed class LeaderboardResultBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal LeaderboardResultBufferHandle(nuint byteCount)
        : base(ownsHandle: true)
    {
        long size = checked((long)byteCount);
        SetHandle(Marshal.AllocHGlobal(size == 0 ? new IntPtr(1) : new IntPtr(size)));
    }

    protected override bool ReleaseHandle()
    {
        Marshal.FreeHGlobal(handle);
        return true;
    }
}
