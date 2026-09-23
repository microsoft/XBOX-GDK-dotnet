using System;
using System.Linq;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblSocialManagerTests
{
    [Theory]
    [InlineData(SocialManagerExtraDetailLevel.NoExtraDetail, 0u)]
    [InlineData(SocialManagerExtraDetailLevel.TitleHistory, 1u)]
    [InlineData(SocialManagerExtraDetailLevel.PreferredColor, 2u)]
    [InlineData(SocialManagerExtraDetailLevel.All, 3u)]
    public void ExtraDetailLevelMatchesHeader(SocialManagerExtraDetailLevel value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialManagerExtraDetailLevel)value);
    }

    [Theory]
    [InlineData(PresenceFilter.Unknown, 0u)]
    [InlineData(PresenceFilter.TitleOnline, 1u)]
    [InlineData(PresenceFilter.TitleOffline, 2u)]
    [InlineData(PresenceFilter.TitleOnlineOutsideTitle, 3u)]
    [InlineData(PresenceFilter.AllOnline, 4u)]
    [InlineData(PresenceFilter.AllOffline, 5u)]
    [InlineData(PresenceFilter.AllTitle, 6u)]
    [InlineData(PresenceFilter.All, 7u)]
    public void PresenceFilterMatchesHeader(PresenceFilter value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialManagerPresenceFilter)value);
    }

    [Theory]
    [InlineData(RelationshipFilter.Unknown, 0u)]
    [InlineData(RelationshipFilter.Friends, 1u)]
    [InlineData(RelationshipFilter.Favorite, 2u)]
    public void RelationshipFilterMatchesHeader(RelationshipFilter value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialManagerRelationshipFilter)value);
    }

    [Theory]
    [InlineData(SocialUserGroupType.Filter, 0u)]
    [InlineData(SocialUserGroupType.UserList, 1u)]
    public void UserGroupTypeMatchesHeader(SocialUserGroupType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialUserGroupType)value);
    }

    [Theory]
    [InlineData(SocialManagerEventType.UsersAddedToSocialGraph, 0u)]
    [InlineData(SocialManagerEventType.UsersRemovedFromSocialGraph, 1u)]
    [InlineData(SocialManagerEventType.PresenceChanged, 2u)]
    [InlineData(SocialManagerEventType.ProfilesChanged, 3u)]
    [InlineData(SocialManagerEventType.SocialRelationshipsChanged, 4u)]
    [InlineData(SocialManagerEventType.LocalUserAdded, 5u)]
    [InlineData(SocialManagerEventType.SocialUserGroupLoaded, 6u)]
    [InlineData(SocialManagerEventType.SocialUserGroupUpdated, 7u)]
    [InlineData(SocialManagerEventType.Unknown, 8u)]
    public void EventKindMatchesHeader(SocialManagerEventType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        XblSocialManagerEventType native = value == SocialManagerEventType.Unknown
            ? XblSocialManagerEventType.UnknownEvent
            : (XblSocialManagerEventType)value;
        Assert.Equal(expected, (uint)native);
    }

    [Fact]
    public void SocialManagerConstantsMatchHeader()
    {
        Assert.Equal(100, XblSocialManagerConstants.MaxUsersFromList);
        Assert.Equal(10, XblSocialManagerEvent.MaxAffectedUsers);
        Assert.Equal(6, XblSocialManagerPresenceRecord.NumPresenceRecords);
        Assert.Equal(90, XblSocialManagerUser.DisplayNameCharSize);
        Assert.Equal(765, XblSocialManagerUser.RealNameCharSize);
        Assert.Equal(675, XblSocialManagerUser.DisplayPicUrlRawCharSize);
        Assert.Equal(48, XblSocialManagerUser.GamerscoreCharSize);
        Assert.Equal(48, XblSocialManagerUser.GamertagCharSize);
        Assert.Equal(97, XblSocialManagerUser.ModernGamertagCharSize);
        Assert.Equal(15, XblSocialManagerUser.ModernGamertagSuffixCharSize);
        Assert.Equal(101, XblSocialManagerUser.UniqueModernGamertagCharSize);
        Assert.Equal(75, XblTitleHistory.LastTimePlayedCharSize);
        Assert.Equal(21, XblPreferredColor.ColorCharSize);
        Assert.Equal(300, XblSocialManagerPresenceTitleRecord.TitleNameCharSize);
        Assert.Equal(300, XblSocialManagerPresenceTitleRecord.RichPresenceCharSize);
    }

    [Fact]
    public void SocialManagerStructSizesMatchHeader()
    {
        Assert.Equal(96, sizeof(XblTitleHistory));
        Assert.Equal(63, sizeof(XblPreferredColor));
        Assert.Equal(616, sizeof(XblSocialManagerPresenceTitleRecord));
        Assert.Equal(3704, sizeof(XblSocialManagerPresenceRecord));
        Assert.Equal(5720, sizeof(XblSocialManagerUser));
        Assert.Equal(104, sizeof(XblSocialManagerEvent));
    }

    [Theory]
    [InlineData(nameof(XblTitleHistory.HasUserPlayed), 0)]
    [InlineData(nameof(XblTitleHistory.LastTimeUserPlayed), 8)]
    [InlineData(nameof(XblTitleHistory.LastTimeUserPlayedText), 16)]
    public void TitleHistoryOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblTitleHistory>(field));
    }

    [Theory]
    [InlineData(nameof(XblPreferredColor.PrimaryColor), 0)]
    [InlineData(nameof(XblPreferredColor.SecondaryColor), 21)]
    [InlineData(nameof(XblPreferredColor.TertiaryColor), 42)]
    public void PreferredColorOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPreferredColor>(field));
    }

    [Theory]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.TitleId), 0)]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.TitleName), 4)]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.IsTitleActive), 304)]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.PresenceText), 305)]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.IsBroadcasting), 605)]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.DeviceType), 608)]
    [InlineData(nameof(XblSocialManagerPresenceTitleRecord.IsPrimary), 612)]
    public void PresenceTitleRecordOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblSocialManagerPresenceTitleRecord>(field));
    }

    [Theory]
    [InlineData(nameof(XblSocialManagerPresenceRecord.UserState), 0)]
    [InlineData(nameof(XblSocialManagerPresenceRecord.PresenceTitleRecords), 4)]
    [InlineData(nameof(XblSocialManagerPresenceRecord.PresenceTitleRecordCount), 3700)]
    public void PresenceRecordOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblSocialManagerPresenceRecord>(field));
    }

    [Theory]
    [InlineData(nameof(XblSocialManagerUser.XboxUserId), 0)]
    [InlineData(nameof(XblSocialManagerUser.IsFavorite), 8)]
    [InlineData(nameof(XblSocialManagerUser.IsFriend), 9)]
    [InlineData(nameof(XblSocialManagerUser.IsFollowingUser), 10)]
    [InlineData(nameof(XblSocialManagerUser.IsFollowedByCaller), 11)]
    [InlineData(nameof(XblSocialManagerUser.DisplayName), 12)]
    [InlineData(nameof(XblSocialManagerUser.RealName), 102)]
    [InlineData(nameof(XblSocialManagerUser.DisplayPicUrlRaw), 867)]
    [InlineData(nameof(XblSocialManagerUser.UseAvatar), 1542)]
    [InlineData(nameof(XblSocialManagerUser.Gamerscore), 1543)]
    [InlineData(nameof(XblSocialManagerUser.Gamertag), 1591)]
    [InlineData(nameof(XblSocialManagerUser.ModernGamertag), 1639)]
    [InlineData(nameof(XblSocialManagerUser.ModernGamertagSuffix), 1736)]
    [InlineData(nameof(XblSocialManagerUser.UniqueModernGamertag), 1751)]
    [InlineData(nameof(XblSocialManagerUser.PresenceRecord), 1852)]
    [InlineData(nameof(XblSocialManagerUser.TitleHistory), 5560)]
    [InlineData(nameof(XblSocialManagerUser.PreferredColor), 5656)]
    public void SocialManagerUserOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblSocialManagerUser>(field));
    }

    [Theory]
    [InlineData(nameof(XblSocialManagerEvent.User), 0)]
    [InlineData(nameof(XblSocialManagerEvent.EventType), 8)]
    [InlineData(nameof(XblSocialManagerEvent.Hr), 12)]
    [InlineData(nameof(XblSocialManagerEvent.GroupAffected), 16)]
    [InlineData(nameof(XblSocialManagerEvent.UsersAffected0), 24)]
    [InlineData(nameof(XblSocialManagerEvent.UsersAffected9), 96)]
    public void SocialManagerEventOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblSocialManagerEvent>(field));
    }

    [Fact]
    public void AffectedUsersAreInlinePointerSlots()
    {
        Assert.Equal(8, sizeof(IntPtr));
        Assert.Equal(80, sizeof(XblSocialManagerEvent) - (int)Marshal.OffsetOf<XblSocialManagerEvent>(nameof(XblSocialManagerEvent.UsersAffected0)));
    }

    [Fact]
    public void EventHierarchyHasOneConcreteRecordPerNativeKind()
    {
        Type[] concreteEvents =
        [
            typeof(UsersAddedToSocialGraphSocialManagerEvent),
            typeof(UsersRemovedFromSocialGraphSocialManagerEvent),
            typeof(PresenceChangedSocialManagerEvent),
            typeof(ProfilesChangedSocialManagerEvent),
            typeof(SocialRelationshipsChangedSocialManagerEvent),
            typeof(LocalUserAddedSocialManagerEvent),
            typeof(SocialUserGroupLoadedSocialManagerEvent),
            typeof(SocialUserGroupUpdatedSocialManagerEvent),
            typeof(UnknownSocialManagerEvent),
        ];

        Assert.True(typeof(SocialManagerEvent).IsAbstract);
        Assert.All(concreteEvents, type =>
        {
            Assert.True(type.IsSealed);
            Assert.True(type.IsAssignableTo(typeof(SocialManagerEvent)));
        });
        Assert.Equal(Enum.GetValues(typeof(SocialManagerEventType)).Length, concreteEvents.Length);
    }

    [Fact]
    public void SocialManagerShapeDoesNotExposeInteropHandles()
    {
        Assert.True(typeof(SocialManager).IsSealed);
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(SocialManagerUserGroup)));
        Assert.Empty(typeof(SocialManager).GetConstructors());
        Assert.Empty(typeof(SocialManagerUserGroup).GetConstructors());
        Assert.DoesNotContain(
            typeof(SocialManager).GetMethods().Concat(typeof(SocialManagerUserGroup).GetMethods()),
            method => method.ReturnType.Namespace == "GDK.Net.Interop");
    }
}
