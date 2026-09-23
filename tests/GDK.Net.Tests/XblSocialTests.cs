// Contract tests for xsapi-c\social_c.h.
//
// Nothing here loads Microsoft.Xbox.Services.C.Thunks.dll. Layout constants were produced by
// compiling xsapi-c/services_c.h with _GAMING_DESKTOP defined (GDK edition 260404, x64) and
// printing sizeof/offsetof with MSVC.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblSocialTests
{
    [Theory]
    [InlineData(SocialRelationshipFilter.All, 0u)]
    [InlineData(SocialRelationshipFilter.Favorite, 1u)]
    [InlineData(SocialRelationshipFilter.LegacyXboxLiveFriends, 2u)]
    public void SocialRelationshipFilterMatchesHeader(SocialRelationshipFilter value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialRelationshipFilter)value);
    }

    [Theory]
    [InlineData(ReputationFeedbackType.FairPlayKillsTeammates, 0u)]
    [InlineData(ReputationFeedbackType.FairPlayCheater, 1u)]
    [InlineData(ReputationFeedbackType.FairPlayTampering, 2u)]
    [InlineData(ReputationFeedbackType.FairPlayQuitter, 3u)]
    [InlineData(ReputationFeedbackType.FairPlayKicked, 4u)]
    [InlineData(ReputationFeedbackType.CommunicationsInappropriateVideo, 5u)]
    [InlineData(ReputationFeedbackType.CommunicationsAbusiveVoice, 6u)]
    [InlineData(ReputationFeedbackType.InappropriateUserGeneratedContent, 7u)]
    [InlineData(ReputationFeedbackType.PositiveSkilledPlayer, 8u)]
    [InlineData(ReputationFeedbackType.PositiveHelpfulPlayer, 9u)]
    [InlineData(ReputationFeedbackType.PositiveHighQualityUserGeneratedContent, 10u)]
    [InlineData(ReputationFeedbackType.CommsPhishing, 11u)]
    [InlineData(ReputationFeedbackType.CommsPictureMessage, 12u)]
    [InlineData(ReputationFeedbackType.CommsSpam, 13u)]
    [InlineData(ReputationFeedbackType.CommsTextMessage, 14u)]
    [InlineData(ReputationFeedbackType.CommsVoiceMessage, 15u)]
    [InlineData(ReputationFeedbackType.FairPlayConsoleBanRequest, 16u)]
    [InlineData(ReputationFeedbackType.FairPlayIdler, 17u)]
    [InlineData(ReputationFeedbackType.FairPlayUserBanRequest, 18u)]
    [InlineData(ReputationFeedbackType.UserContentGamerpic, 19u)]
    [InlineData(ReputationFeedbackType.UserContentPersonalInfo, 20u)]
    [InlineData(ReputationFeedbackType.FairPlayUnsporting, 21u)]
    [InlineData(ReputationFeedbackType.FairPlayLeaderboardCheater, 22u)]
    public void ReputationFeedbackTypeMatchesHeader(ReputationFeedbackType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblReputationFeedbackType)value);
    }

    [Theory]
    [InlineData(SocialNotificationType.Unknown, 0u)]
    [InlineData(SocialNotificationType.Added, 1u)]
    [InlineData(SocialNotificationType.Changed, 2u)]
    [InlineData(SocialNotificationType.Removed, 3u)]
    [InlineData(SocialNotificationType.IncomingFriendRequestCountChanged, 4u)]
    public void SocialNotificationTypeMatchesHeader(SocialNotificationType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialNotificationType)value);
    }

    [Fact]
    public void SocialStructSizesMatchHeader()
    {
        Assert.Equal(32, sizeof(XblSocialRelationship));
        Assert.Equal(32, sizeof(XblSocialRelationshipChangeEventArgs));
        Assert.Equal(16, sizeof(XblSocialFriendRequestCountChangedEventArgs));
        Assert.Equal(40, sizeof(XblReputationFeedbackItem));
        Assert.Equal(240, sizeof(XblMultiplayerSessionReference));
    }

    [Theory]
    [InlineData(nameof(XblSocialRelationship.XboxUserId), 0)]
    [InlineData(nameof(XblSocialRelationship.IsFavorite), 8)]
    [InlineData(nameof(XblSocialRelationship.IsFriend), 9)]
    [InlineData(nameof(XblSocialRelationship.IsFollowingCaller), 10)]
    [InlineData(nameof(XblSocialRelationship.SocialNetworks), 16)]
    [InlineData(nameof(XblSocialRelationship.SocialNetworksCount), 24)]
    public void XblSocialRelationshipFieldOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblSocialRelationship>(field));
    }

    [Theory]
    [InlineData(nameof(XblSocialRelationshipChangeEventArgs.CallerXboxUserId), 0)]
    [InlineData(nameof(XblSocialRelationshipChangeEventArgs.SocialNotification), 8)]
    [InlineData(nameof(XblSocialRelationshipChangeEventArgs.XboxUserIds), 16)]
    [InlineData(nameof(XblSocialRelationshipChangeEventArgs.XboxUserIdsCount), 24)]
    public void XblSocialRelationshipChangeEventArgsFieldOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblSocialRelationshipChangeEventArgs>(field));
    }

    [Fact]
    public void XblSocialFriendRequestCountChangedEventArgsMatchesHeaderLayout()
    {
        Assert.Equal(
            0,
            (int)Marshal.OffsetOf<XblSocialFriendRequestCountChangedEventArgs>(
                nameof(XblSocialFriendRequestCountChangedEventArgs.CallerXboxUserId)));
        Assert.Equal(
            8,
            (int)Marshal.OffsetOf<XblSocialFriendRequestCountChangedEventArgs>(
                nameof(XblSocialFriendRequestCountChangedEventArgs.IncomingFriendRequestCount)));
    }

    [Theory]
    [InlineData(nameof(XblReputationFeedbackItem.XboxUserId), 0)]
    [InlineData(nameof(XblReputationFeedbackItem.FeedbackType), 8)]
    [InlineData(nameof(XblReputationFeedbackItem.SessionReference), 16)]
    [InlineData(nameof(XblReputationFeedbackItem.ReasonMessage), 24)]
    [InlineData(nameof(XblReputationFeedbackItem.EvidenceResourceId), 32)]
    public void XblReputationFeedbackItemFieldOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblReputationFeedbackItem>(field));
    }

    [Fact]
    public void XblMultiplayerSessionReferenceMatchesHeaderLayout()
    {
        Assert.Equal(40, XblMultiplayerSessionReference.ScidLength);
        Assert.Equal(100, XblMultiplayerSessionReference.SessionTemplateNameMaxLength);
        Assert.Equal(100, XblMultiplayerSessionReference.SessionNameMaxLength);
        Assert.Equal(0, (int)Marshal.OffsetOf<XblMultiplayerSessionReference>(nameof(XblMultiplayerSessionReference.Scid)));
        Assert.Equal(
            40,
            (int)Marshal.OffsetOf<XblMultiplayerSessionReference>(
                nameof(XblMultiplayerSessionReference.SessionTemplateName)));
        Assert.Equal(
            140,
            (int)Marshal.OffsetOf<XblMultiplayerSessionReference>(nameof(XblMultiplayerSessionReference.SessionName)));
    }

    [Fact]
    public void SocialPublicTypesAreSealed()
    {
        Assert.True(typeof(SocialService).IsSealed);
        Assert.True(typeof(SocialRelationshipsPage).IsSealed);
        Assert.True(typeof(SocialRelationship).IsSealed);
        Assert.True(typeof(SocialRelationshipChangedEventArgs).IsSealed);
        Assert.True(typeof(SocialMultiplayerSessionReference).IsSealed);
        Assert.True(typeof(ReputationFeedbackItem).IsSealed);
    }

    [Fact]
    public void SocialHandleOwningTypesAreDisposable()
    {
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(SocialRelationshipsPage)));
    }

    [Fact]
    public void SocialServiceIsNotPubliclyConstructible()
    {
        Assert.Empty(typeof(SocialService).GetConstructors());
        Assert.Empty(typeof(SocialRelationshipsPage).GetConstructors());
    }

    [Fact]
    public void SocialPublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(SocialService),
            typeof(SocialRelationshipsPage),
            typeof(SocialRelationship),
            typeof(SocialRelationshipChangedEventArgs),
            typeof(SocialMultiplayerSessionReference),
            typeof(ReputationFeedbackItem),
        ];

        foreach (Type type in publicTypes)
        {
            foreach (System.Reflection.MethodInfo method in type.GetMethods(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (System.Reflection.ParameterInfo parameter in method.GetParameters())
                {
                    AssertNotInterop(type, method.Name, parameter.ParameterType);
                }
            }
        }
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
