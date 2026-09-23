using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Users;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the raw interop layer against silent drift from the GDK headers
/// (%GameDKCoreLatest%windows\include, edition 260404). These are pure compile-time/layout checks —
/// nothing here loads xgameruntime.thunks.dll, so they run identically on a developer box and on a
/// hosted CI runner.
/// </summary>
public sealed unsafe class InteropContractTests
{
    [Fact]
    public void AsyncBlockMatchesTheNativeLayout()
    {
        // struct XAsyncBlock { XTaskQueueHandle queue; void* context; XAsyncCompletionRoutine* callback;
        //                      unsigned char internal[sizeof(void*) * 4]; }
        Assert.Equal(IntPtr.Size * 7, Marshal.SizeOf<XAsyncBlock>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XAsyncBlock>(nameof(XAsyncBlock.Queue)));
        Assert.Equal(IntPtr.Size, (int)Marshal.OffsetOf<XAsyncBlock>(nameof(XAsyncBlock.Context)));
        Assert.Equal(IntPtr.Size * 2, (int)Marshal.OffsetOf<XAsyncBlock>(nameof(XAsyncBlock.Callback)));
    }

    [Fact]
    public void IdentityStructsAreSingleUInt64s()
    {
        Assert.Equal(sizeof(ulong), Marshal.SizeOf<XUserLocalId>());
        Assert.Equal(sizeof(ulong), Marshal.SizeOf<XTaskQueueRegistrationToken>());
    }

    [Fact]
    public void CallbackThunksResolveToRealFunctionPointers()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.AsyncCompletionRoutine);
        Assert.NotEqual(IntPtr.Zero, Trampolines.UserChangeEventCallback);
        Assert.NotEqual(Trampolines.AsyncCompletionRoutine, Trampolines.UserChangeEventCallback);
    }

    [Fact]
    public void BindsTheThunksModuleAndNotXGameRuntime()
    {
        // XGameRuntime.dll exports only four private version-negotiation ordinals; every public X*
        // API is a statically linked stub in xgameruntime.lib. xgameruntime.thunks.dll is the GDK's
        // own redistributable re-export of that surface and is the only module a P/Invoke can bind
        // to. Binding the wrong one fails at runtime with EntryPointNotFoundException, and only
        // inside a packaged title, so it is pinned here.
        Assert.Equal("xgameruntime.thunks.dll", Native.LibraryName);
    }

    [Theory]
    // Values transcribed from XGameRuntimeFeature.h.
    [InlineData(GameRuntimeFeature.User, 18u)]
    [InlineData(GameRuntimeFeature.TaskQueue, 16u)]
    [InlineData(GameRuntimeFeature.Store, 14u)]
    [InlineData(GameRuntimeFeature.GameSave, 7u)]
    [InlineData(GameRuntimeFeature.GameStreaming, 21u)]
    public void GameFeatureMatchesTheHeader(GameRuntimeFeature feature, uint expected)
    {
        Assert.Equal(expected, (uint)feature);
    }

    [Fact]
    public void DispatchModeMatchesTheHeader()
    {
        // XTaskQueueDispatchMode from XTaskQueue.h — Manual is 0, not ThreadPool. Written as a Fact
        // rather than a Theory because GameTaskQueueDispatchMode is internal and xUnit requires
        // public test method signatures.
        Assert.Equal(0u, (uint)GameTaskQueueDispatchMode.Manual);
        Assert.Equal(1u, (uint)GameTaskQueueDispatchMode.ThreadPool);
        Assert.Equal(2u, (uint)GameTaskQueueDispatchMode.SerializedThreadPool);
        Assert.Equal(3u, (uint)GameTaskQueueDispatchMode.Immediate);

        Assert.Equal(0u, (uint)(XTaskQueueDispatchMode)GameTaskQueueDispatchMode.Manual);
        Assert.Equal(1u, (uint)(XTaskQueueDispatchMode)GameTaskQueueDispatchMode.ThreadPool);
        Assert.Equal(2u, (uint)(XTaskQueueDispatchMode)GameTaskQueueDispatchMode.SerializedThreadPool);
        Assert.Equal(3u, (uint)(XTaskQueueDispatchMode)GameTaskQueueDispatchMode.Immediate);
    }

    [Theory]
    // XUserPrivilege from XUser.h.
    [InlineData(UserPrivilege.CrossPlay, 185u)]
    [InlineData(UserPrivilege.Communications, 252u)]
    [InlineData(UserPrivilege.Multiplayer, 254u)]
    [InlineData(UserPrivilege.AddFriends, 255u)]
    public void PrivilegeMatchesTheHeader(UserPrivilege privilege, uint expected)
    {
        Assert.Equal(expected, (uint)privilege);
        Assert.Equal(expected, (uint)(XUserPrivilege)privilege);
    }

    [Theory]
    [InlineData(GamertagComponent.Classic, 0u)]
    [InlineData(GamertagComponent.Modern, 1u)]
    [InlineData(GamertagComponent.ModernSuffix, 2u)]
    [InlineData(GamertagComponent.UniqueModern, 3u)]
    public void GamertagComponentMatchesTheHeader(GamertagComponent component, uint expected)
    {
        Assert.Equal(expected, (uint)component);
        Assert.Equal(expected, (uint)(XUserGamertagComponent)component);
    }

    [Fact]
    public void PublicUserEnumsProjectOntoTheirNativeCounterparts()
    {
        Assert.Equal((uint)XUserState.SigningOut, (uint)UserState.SigningOut);
        Assert.Equal((uint)XUserAgeGroup.Adult, (uint)UserAgeGroup.Adult);
        Assert.Equal((uint)XUserGamerPictureSize.ExtraLarge, (uint)GamerPictureSize.ExtraLarge);
        Assert.Equal((uint)XUserChangeEvent.Privileges, (uint)UserChangeEvent.Privileges);
        Assert.Equal((uint)XUserPrivilegeDenyReason.Unknown, (uint)UserPrivilegeDenyReason.Unknown);
    }

    [Fact]
    public void AddOptionsIsAFlagsEnumMatchingTheHeader()
    {
        var options = UserAddOptions.AddDefaultUserSilently | UserAddOptions.AllowGuests;

        Assert.True(options.HasFlag(UserAddOptions.AddDefaultUserSilently));
        Assert.True(options.HasFlag(UserAddOptions.AllowGuests));
        Assert.False(options.HasFlag(UserAddOptions.AddDefaultUserAllowingUI));
        Assert.Equal(0x03u, (uint)options);
        Assert.Equal(0x04u, (uint)UserAddOptions.AddDefaultUserAllowingUI);
    }

    [Theory]
    [InlineData(null, 0u)]
    [InlineData(0, 0u)]
    [InlineData(-5, 0u)]
    [InlineData(250, 250u)]
    public void TimeoutConversionClampsToTheNativeMillisecondContract(int? milliseconds, uint expected)
    {
        TimeSpan? timeout = milliseconds is { } value ? TimeSpan.FromMilliseconds(value) : null;

        Assert.Equal(expected, GameTaskQueue.ToTimeoutMilliseconds(timeout));
    }

    [Fact]
    public void InfiniteTimeoutBecomesTheNativeInfiniteSentinel()
    {
        Assert.Equal(uint.MaxValue, GameTaskQueue.ToTimeoutMilliseconds(System.Threading.Timeout.InfiniteTimeSpan));
    }
}
