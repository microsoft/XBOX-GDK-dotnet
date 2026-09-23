using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for xsapi-c\multiplayer_activity_c.h (GDK edition 260404). These do not load
/// Microsoft.Xbox.Services.C.Thunks.dll; they pin enum values, native layouts and public shape.
/// </summary>
public sealed unsafe class XblMultiplayerActivityTests
{
    private static Type NativeXblType =>
        typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.NativeXbl", throwOnError: true)!;

    public static TheoryData<string> BoundEntryPoints => new()
    {
        "XblMultiplayerActivitySetActivityAsync",
        "XblMultiplayerActivityDeleteActivityAsync",
        "XblMultiplayerActivityGetActivityAsync",
        "XblMultiplayerActivityGetActivityResultSize",
        "XblMultiplayerActivityGetActivityResult",
        "XblMultiplayerActivitySendInvitesAsync",
        "XblMultiplayerActivityUpdateRecentPlayers",
        "XblMultiplayerActivityFlushRecentPlayersAsync",
    };

    [Theory]
    [InlineData(MultiplayerActivityPlatform.Unknown, 0u)]
    [InlineData(MultiplayerActivityPlatform.XboxOne, 1u)]
    [InlineData(MultiplayerActivityPlatform.WindowsOneCore, 2u)]
    [InlineData(MultiplayerActivityPlatform.Win32, 3u)]
    [InlineData(MultiplayerActivityPlatform.Scarlett, 4u)]
    [InlineData(MultiplayerActivityPlatform.Ios, 20u)]
    [InlineData(MultiplayerActivityPlatform.Android, 30u)]
    [InlineData(MultiplayerActivityPlatform.Nintendo, 40u)]
    [InlineData(MultiplayerActivityPlatform.PlayStation, 50u)]
    [InlineData(MultiplayerActivityPlatform.All, 60u)]
    public void PlatformMatchesHeader(MultiplayerActivityPlatform value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblMultiplayerActivityPlatform)value);
    }

    [Theory]
    [InlineData(MultiplayerActivityJoinRestriction.Public, 0u)]
    [InlineData(MultiplayerActivityJoinRestriction.InviteOnly, 1u)]
    [InlineData(MultiplayerActivityJoinRestriction.Followed, 2u)]
    public void JoinRestrictionMatchesHeader(MultiplayerActivityJoinRestriction value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblMultiplayerActivityJoinRestriction)value);
    }

    [Theory]
    [InlineData(MultiplayerActivityEncounterType.Default, 0u)]
    [InlineData(MultiplayerActivityEncounterType.Teammate, 1u)]
    [InlineData(MultiplayerActivityEncounterType.Opponent, 2u)]
    public void EncounterTypeMatchesHeader(MultiplayerActivityEncounterType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblMultiplayerActivityEncounterType)value);
    }

    [Fact]
    public void MultiplayerActivityStructSizesMatchHeader()
    {
        Assert.Equal(56, sizeof(XblMultiplayerActivityInfo));
        Assert.Equal(16, sizeof(XblMultiplayerActivityRecentPlayerUpdate));
    }

    [Theory]
    [InlineData(nameof(XblMultiplayerActivityInfo.Xuid), 0)]
    [InlineData(nameof(XblMultiplayerActivityInfo.ConnectionString), 8)]
    [InlineData(nameof(XblMultiplayerActivityInfo.JoinRestriction), 16)]
    [InlineData(nameof(XblMultiplayerActivityInfo.MaxPlayers), 24)]
    [InlineData(nameof(XblMultiplayerActivityInfo.CurrentPlayers), 32)]
    [InlineData(nameof(XblMultiplayerActivityInfo.GroupId), 40)]
    [InlineData(nameof(XblMultiplayerActivityInfo.Platform), 48)]
    public void MultiplayerActivityInfoOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblMultiplayerActivityInfo>(field));

    [Theory]
    [InlineData(nameof(XblMultiplayerActivityRecentPlayerUpdate.Xuid), 0)]
    [InlineData(nameof(XblMultiplayerActivityRecentPlayerUpdate.EncounterType), 8)]
    public void RecentPlayerUpdateOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblMultiplayerActivityRecentPlayerUpdate>(field));

    [Theory]
    [MemberData(nameof(BoundEntryPoints))]
    public void MultiplayerActivityEntryPointsAreDeclared(string name)
    {
        Assert.NotNull(NativeXblType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Theory]
    [MemberData(nameof(BoundEntryPoints))]
    public void MultiplayerActivityEntryPointsBindToXsapiThunks(string name)
    {
        MethodInfo method = NativeXblType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)!;
        var import = method.GetCustomAttribute<DllImportAttribute>();

        Assert.NotNull(import);
        Assert.Equal("Microsoft.Xbox.Services.C.Thunks.dll", import!.Value);
    }

    [Fact]
    public void InviteNotificationEntryPointsAreNotDeclaredBecauseTheyAreNotExported()
    {
        Assert.Null(NativeXblType.GetMethod(
            "XblMultiplayerActivityAddInviteHandler",
            BindingFlags.NonPublic | BindingFlags.Static));
        Assert.Null(NativeXblType.GetMethod(
            "XblMultiplayerActivityRemoveInviteHandler",
            BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Fact]
    public void NativeActivityInfoIsSnapshotted()
    {
        IntPtr connection = Utf8.Allocate("join://session");
        IntPtr group = Utf8.Allocate("group-1");

        try
        {
            var native = new XblMultiplayerActivityInfo
            {
                Xuid = 2814639012345678,
                ConnectionString = (byte*)connection,
                JoinRestriction = XblMultiplayerActivityJoinRestriction.Followed,
                MaxPlayers = 8,
                CurrentPlayers = 3,
                GroupId = (byte*)group,
                Platform = XblMultiplayerActivityPlatform.Win32,
            };

            MultiplayerActivityInfo activity = MultiplayerActivityInfo.FromNative(&native);

            Assert.Equal(2814639012345678UL, activity.XboxUserId);
            Assert.Equal("join://session", activity.ConnectionString);
            Assert.Equal(MultiplayerActivityJoinRestriction.Followed, activity.JoinRestriction);
            Assert.Equal(8u, activity.MaxPlayers);
            Assert.Equal(3u, activity.CurrentPlayers);
            Assert.Equal("group-1", activity.GroupId);
            Assert.Equal(MultiplayerActivityPlatform.Win32, activity.Platform);
        }
        finally
        {
            Utf8.Free(connection);
            Utf8.Free(group);
        }
    }

    [Fact]
    public void MultiplayerActivityServiceHasRequiredInternalConstructor()
    {
        ConstructorInfo[] constructors = typeof(MultiplayerActivityService).GetConstructors(
            BindingFlags.NonPublic | BindingFlags.Instance);

        ConstructorInfo constructor = Assert.Single(constructors);
        Assert.True(constructor.IsAssembly);
        Assert.Equal(new[] { typeof(XboxLiveContext) }, constructor.GetParameters().Select(p => p.ParameterType));
    }

    [Fact]
    public void MultiplayerActivityPublicTypesAreSealed()
    {
        Assert.True(typeof(MultiplayerActivityService).IsSealed);
        Assert.True(typeof(MultiplayerActivityInfo).IsSealed);
        Assert.True(typeof(MultiplayerActivityRecentPlayerUpdate).IsSealed);
    }

    [Fact]
    public void MultiplayerActivityServicesAreNotPubliclyConstructible()
    {
        Assert.Empty(typeof(MultiplayerActivityService).GetConstructors());
    }

    [Fact]
    public void MultiplayerActivityPublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        {
            typeof(MultiplayerActivityService),
            typeof(MultiplayerActivityInfo),
            typeof(MultiplayerActivityRecentPlayerUpdate),
        };

        foreach (Type type in publicTypes)
        {
            IEnumerable<MemberInfo> members = type.GetMethods(
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.DeclaredOnly)
                .Cast<MemberInfo>()
                .Concat(type.GetConstructors());

            foreach (MemberInfo member in members)
            {
                if (member is MethodInfo method)
                {
                    AssertNotInterop(type, method.Name, method.ReturnType);
                    foreach (ParameterInfo parameter in method.GetParameters())
                    {
                        AssertNotInterop(type, method.Name, parameter.ParameterType);
                    }
                }
                else if (member is ConstructorInfo constructor)
                {
                    foreach (ParameterInfo parameter in constructor.GetParameters())
                    {
                        AssertNotInterop(type, ".ctor", parameter.ParameterType);
                    }
                }
            }
        }
    }

    [Fact]
    public void MultiplayerActivityServiceUsesIdiomaticAsyncShapes()
    {
        Assert.Equal(
            typeof(Task),
            typeof(MultiplayerActivityService).GetMethod(nameof(MultiplayerActivityService.SetActivityAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task),
            typeof(MultiplayerActivityService).GetMethod(nameof(MultiplayerActivityService.DeleteActivityAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<IReadOnlyList<MultiplayerActivityInfo>>),
            typeof(MultiplayerActivityService).GetMethod(nameof(MultiplayerActivityService.GetActivitiesAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task),
            typeof(MultiplayerActivityService).GetMethod(nameof(MultiplayerActivityService.SendInvitesAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task),
            typeof(MultiplayerActivityService).GetMethod(nameof(MultiplayerActivityService.FlushRecentPlayersAsync))!.ReturnType);
        Assert.Equal(
            typeof(void),
            typeof(MultiplayerActivityService).GetMethod(nameof(MultiplayerActivityService.UpdateRecentPlayers))!.ReturnType);
    }

    private static void AssertNotInterop(Type owner, string member, Type candidate)
    {
        Type target = candidate.IsByRef || candidate.IsPointer || candidate.IsArray
            ? candidate.GetElementType()!
            : candidate;

        Assert.False(
            target.Namespace is "GDK.Net.Interop",
            $"{owner.Name}.{member} exposes the interop type {target.Name}.");

        Assert.False(
            typeof(SafeHandle).IsAssignableFrom(target),
            $"{owner.Name}.{member} exposes the SafeHandle {target.Name}.");
    }
}
