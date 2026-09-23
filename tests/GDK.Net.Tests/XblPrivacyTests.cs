using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblPrivacyTests
{
    [Theory]
    [InlineData(PrivacySetting.Unknown, 0u)]
    [InlineData(PrivacySetting.ShareFriendList, 1u)]
    [InlineData(PrivacySetting.ShareGameHistory, 2u)]
    [InlineData(PrivacySetting.CommunicateUsingTextAndVoice, 3u)]
    [InlineData(PrivacySetting.SharePresence, 4u)]
    [InlineData(PrivacySetting.ShareProfile, 5u)]
    [InlineData(PrivacySetting.ShareVideoAndMusicStatus, 6u)]
    [InlineData(PrivacySetting.CommunicateUsingVideo, 7u)]
    [InlineData(PrivacySetting.CollectVoiceData, 8u)]
    [InlineData(PrivacySetting.ShareXboxMusicActivity, 9u)]
    [InlineData(PrivacySetting.ShareExerciseInfo, 11u)]
    [InlineData(PrivacySetting.ShareIdentity, 12u)]
    [InlineData(PrivacySetting.ShareIdentityInGame, 13u)]
    [InlineData(PrivacySetting.ShareRecordedGameSessions, 14u)]
    [InlineData(PrivacySetting.CollectLiveTvData, 15u)]
    [InlineData(PrivacySetting.CollectXboxVideoData, 16u)]
    [InlineData(PrivacySetting.ShareIdentityTransitively, 17u)]
    [InlineData(PrivacySetting.ShareVideoHistory, 18u)]
    [InlineData(PrivacySetting.ShareMusicHistory, 19u)]
    [InlineData(PrivacySetting.AllowUserCreatedContentViewing, 20u)]
    [InlineData(PrivacySetting.AllowProfileViewing, 21u)]
    [InlineData(PrivacySetting.ShowRealTimeActivity, 22u)]
    [InlineData(PrivacySetting.CollectVoiceDataXboxOneFull, 23u)]
    [InlineData(PrivacySetting.CanShareIdentity, 24u)]
    [InlineData(PrivacySetting.ShareContentToExternalNetworks, 25u)]
    [InlineData(PrivacySetting.CollectVoiceSearchData, 26u)]
    [InlineData(PrivacySetting.ShareClubMembership, 27u)]
    [InlineData(PrivacySetting.CollectVoiceGameChatData, 28u)]
    [InlineData(PrivacySetting.ShareActivityFeed, 29u)]
    [InlineData(PrivacySetting.CommunicateDuringCrossNetworkPlay, 30u)]
    public void PrivacySettingMatchesHeader(PrivacySetting value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPrivacySetting)value);
    }

    [Theory]
    [InlineData(Privilege.Unknown, 0u)]
    [InlineData(Privilege.AllowIngameVoiceCommunications, 205u)]
    [InlineData(Privilege.AllowVideoCommunications, 235u)]
    [InlineData(Privilege.AllowProfileViewing, 249u)]
    [InlineData(Privilege.AllowCommunications, 252u)]
    [InlineData(Privilege.AllowMultiplayer, 254u)]
    [InlineData(Privilege.AllowAddFriend, 255u)]
    public void PrivacyPrivilegeMatchesHeader(Privilege value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPrivilege)value);
    }

    [Theory]
    [InlineData(Permission.Unknown, 0u)]
    [InlineData(Permission.CommunicateUsingText, 1000u)]
    [InlineData(Permission.CommunicateUsingVideo, 1001u)]
    [InlineData(Permission.CommunicateUsingVoice, 1002u)]
    [InlineData(Permission.ViewTargetProfile, 1004u)]
    [InlineData(Permission.ViewTargetGameHistory, 1005u)]
    [InlineData(Permission.ViewTargetVideoHistory, 1006u)]
    [InlineData(Permission.ViewTargetMusicHistory, 1007u)]
    [InlineData(Permission.ViewTargetExerciseInfo, 1009u)]
    [InlineData(Permission.ViewTargetPresence, 1011u)]
    [InlineData(Permission.ViewTargetVideoStatus, 1012u)]
    [InlineData(Permission.ViewTargetMusicStatus, 1013u)]
    [InlineData(Permission.PlayMultiplayer, 1014u)]
    [InlineData(Permission.ViewTargetUserCreatedContent, 1018u)]
    [InlineData(Permission.BroadcastWithTwitch, 1019u)]
    [InlineData(Permission.WriteComment, 1022u)]
    [InlineData(Permission.ShareItem, 1024u)]
    [InlineData(Permission.ShareTargetContentToExternalNetworks, 1025u)]
    public void PrivacyPermissionMatchesHeader(Permission value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPermission)value);
    }

    [Theory]
    [InlineData(PermissionDenyReason.Unknown, 0u)]
    [InlineData(PermissionDenyReason.NotAllowed, 2u)]
    [InlineData(PermissionDenyReason.MissingPrivilege, 3u)]
    [InlineData(PermissionDenyReason.PrivilegeRestrictsTarget, 4u)]
    [InlineData(PermissionDenyReason.BlockListRestrictsTarget, 5u)]
    [InlineData(PermissionDenyReason.MuteListRestrictsTarget, 7u)]
    [InlineData(PermissionDenyReason.PrivacySettingRestrictsTarget, 9u)]
    [InlineData(PermissionDenyReason.CrossNetworkUserMustBeFriend, 12u)]
    public void PrivacyPermissionDenyReasonMatchesHeader(PermissionDenyReason value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPermissionDenyReason)value);
    }

    [Theory]
    [InlineData(AnonymousUserType.Unknown, 0u)]
    [InlineData(AnonymousUserType.CrossNetworkUser, 1u)]
    [InlineData(AnonymousUserType.CrossNetworkFriend, 2u)]
    public void PrivacyAnonymousUserTypeMatchesHeader(AnonymousUserType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAnonymousUserType)value);
    }

    [Fact]
    public void PrivacyListChangeTypeMatchesHeader()
    {
        Assert.Equal(0u, (uint)XblPrivacyListChangeType.Added);
        Assert.Equal(1u, (uint)XblPrivacyListChangeType.Removed);
    }

    [Fact]
    public void PrivacyStructSizesMatchHeader()
    {
        Assert.Equal(12, sizeof(XblPermissionDenyReasonDetails));
        Assert.Equal(40, sizeof(XblPermissionCheckResult));
        Assert.Equal(24, sizeof(XblPrivacyMuteListChangeEventArgs));
        Assert.Equal(24, sizeof(XblPrivacyBlockListChangeEventArgs));
    }

    [Theory]
    [InlineData(nameof(XblPermissionDenyReasonDetails.Reason), 0)]
    [InlineData(nameof(XblPermissionDenyReasonDetails.RestrictedPrivilege), 4)]
    [InlineData(nameof(XblPermissionDenyReasonDetails.RestrictedPrivacySetting), 8)]
    public void PermissionDenyReasonDetailsOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPermissionDenyReasonDetails>(field));

    [Theory]
    [InlineData(nameof(XblPermissionCheckResult.IsAllowed), 0)]
    [InlineData(nameof(XblPermissionCheckResult.TargetXuid), 8)]
    [InlineData(nameof(XblPermissionCheckResult.TargetUserType), 16)]
    [InlineData(nameof(XblPermissionCheckResult.PermissionRequested), 20)]
    [InlineData(nameof(XblPermissionCheckResult.Reasons), 24)]
    [InlineData(nameof(XblPermissionCheckResult.ReasonsCount), 32)]
    public void PermissionCheckResultOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPermissionCheckResult>(field));

    [Theory]
    [InlineData(typeof(XblPrivacyMuteListChangeEventArgs), nameof(XblPrivacyMuteListChangeEventArgs.ChangeType), 0)]
    [InlineData(typeof(XblPrivacyMuteListChangeEventArgs), nameof(XblPrivacyMuteListChangeEventArgs.Xuids), 8)]
    [InlineData(typeof(XblPrivacyMuteListChangeEventArgs), nameof(XblPrivacyMuteListChangeEventArgs.XuidsCount), 16)]
    [InlineData(typeof(XblPrivacyBlockListChangeEventArgs), nameof(XblPrivacyBlockListChangeEventArgs.ChangeType), 0)]
    [InlineData(typeof(XblPrivacyBlockListChangeEventArgs), nameof(XblPrivacyBlockListChangeEventArgs.Xuids), 8)]
    [InlineData(typeof(XblPrivacyBlockListChangeEventArgs), nameof(XblPrivacyBlockListChangeEventArgs.XuidsCount), 16)]
    public void PrivacyListChangeEventArgsOffsetsMatchHeader(Type type, string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf(type, field));

    [Fact]
    public void FailClosedPermissionResultIsAlwaysDenied()
    {
        PrivacyPermissionCheckResult result = PrivacyPermissionCheckResult.FailClosed(
            Permission.CommunicateUsingVoice,
            2814639012345678,
            AnonymousUserType.Unknown);

        Assert.False(result.WasChecked);
        Assert.False(result.IsAllowed);
        Assert.Equal(Permission.CommunicateUsingVoice, result.Permission);
        Assert.Equal(2814639012345678UL, result.TargetXboxUserId);
        PrivacyPermissionDenyReasonDetail reason = Assert.Single(result.DenyReasons);
        Assert.Equal(PermissionDenyReason.Unknown, reason.Reason);
    }

    [Fact]
    public void PermissionResultCannotBeAllowedWhenCheckDidNotComplete()
    {
        var result = new PrivacyPermissionCheckResult(
            wasChecked: false,
            isAllowed: true,
            targetXboxUserId: 1,
            targetAnonymousUserType: AnonymousUserType.Unknown,
            permission: Permission.PlayMultiplayer,
            denyReasons: Array.Empty<PrivacyPermissionDenyReasonDetail>());

        Assert.False(result.IsAllowed);
    }

    [Fact]
    public void NativePermissionResultIsSnapshotted()
    {
        var nativeReason = new XblPermissionDenyReasonDetails
        {
            Reason = XblPermissionDenyReason.MissingPrivilege,
            RestrictedPrivilege = XblPrivilege.AllowMultiplayer,
            RestrictedPrivacySetting = XblPrivacySetting.Unknown,
        };

        var native = new XblPermissionCheckResult
        {
            IsAllowed = 0,
            TargetXuid = 123,
            TargetUserType = XblAnonymousUserType.Unknown,
            PermissionRequested = XblPermission.PlayMultiplayer,
            Reasons = &nativeReason,
            ReasonsCount = 1,
        };

        PrivacyPermissionCheckResult result = PrivacyPermissionCheckResult.FromNative(&native);

        Assert.True(result.WasChecked);
        Assert.False(result.IsAllowed);
        Assert.Equal(123UL, result.TargetXboxUserId);
        Assert.Equal(Permission.PlayMultiplayer, result.Permission);
        PrivacyPermissionDenyReasonDetail reason = Assert.Single(result.DenyReasons);
        Assert.Equal(PermissionDenyReason.MissingPrivilege, reason.Reason);
        Assert.Equal(Privilege.AllowMultiplayer, reason.RestrictedPrivilege);
    }

    [Fact]
    public void PrivacyPublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(PrivacyService),
            typeof(PrivacyPermissionCheckResult),
            typeof(PrivacyPermissionDenyReasonDetail),
        ];

        foreach (Type type in publicTypes)
        {
            foreach (MethodInfo method in type.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    AssertNotInterop(type, method.Name, parameter.ParameterType);
                }
            }
        }
    }

    [Fact]
    public void PrivacyServiceIsNotPubliclyConstructible()
    {
        Assert.Empty(typeof(PrivacyService).GetConstructors());
    }

    [Fact]
    public void PermissionCheckMethodsDoNotExposeBooleanResults()
    {
        Assert.Equal(
            typeof(Task<PrivacyPermissionCheckResult>),
            typeof(PrivacyService).GetMethod(nameof(PrivacyService.CheckPermissionAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<PrivacyPermissionCheckResult>),
            typeof(PrivacyService).GetMethod(nameof(PrivacyService.CheckPermissionForAnonymousUserAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<IReadOnlyList<PrivacyPermissionCheckResult>>),
            typeof(PrivacyService).GetMethod(nameof(PrivacyService.BatchCheckPermissionAsync))!.ReturnType);
    }

    [Fact]
    public void PrivacyPublicTypesAreSealed()
    {
        Assert.True(typeof(PrivacyService).IsSealed);
        Assert.True(typeof(PrivacyPermissionCheckResult).IsSealed);
        Assert.True(typeof(PrivacyPermissionDenyReasonDetail).IsSealed);
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
