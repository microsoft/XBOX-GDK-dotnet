// Contract tests for the XSAPI (Xbox Live) family.
//
// Nothing here loads Microsoft.Xbox.Services.C.Thunks.dll; all tests run on hosted CI with no GDK
// installed. Checks: enum values against header constants, struct sizes/field offsets against what
// the MSVC toolchain reports for the real headers, and idiomatic API shape.
//
// The expected sizes and offsets below were produced by compiling xsapi-c/services_c.h with
// _GAMING_DESKTOP defined (GDK edition 260404, x64) and printing sizeof/offsetof. They are the
// contract: if a future GDK edition changes a layout, these fail rather than the projection
// silently reading the wrong bytes at runtime.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XboxLiveTests
{
    // -----------------------------------------------------------------------
    // Enum value contracts: values taken verbatim from achievements_c.h (GDK 260404).
    // Each case asserts the public enum and the interop enum agree, so the casts the
    // projection performs between them are identity casts.
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(AchievementType.Unknown, 0u)]
    [InlineData(AchievementType.All, 1u)]
    [InlineData(AchievementType.Persistent, 2u)]
    [InlineData(AchievementType.Challenge, 3u)]
    public void AchievementTypeMatchesHeader(AchievementType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementType)value);
    }

    [Theory]
    [InlineData(AchievementOrderBy.Default, 0u)]
    [InlineData(AchievementOrderBy.TitleId, 1u)]
    [InlineData(AchievementOrderBy.UnlockTime, 2u)]
    public void AchievementOrderByMatchesHeader(AchievementOrderBy value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementOrderBy)value);
    }

    [Theory]
    [InlineData(AchievementProgressState.Unknown, 0u)]
    [InlineData(AchievementProgressState.Achieved, 1u)]
    [InlineData(AchievementProgressState.NotStarted, 2u)]
    [InlineData(AchievementProgressState.InProgress, 3u)]
    public void AchievementProgressStateMatchesHeader(AchievementProgressState value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementProgressState)value);
    }

    [Theory]
    [InlineData(AchievementMediaAssetType.Unknown, 0u)]
    [InlineData(AchievementMediaAssetType.Icon, 1u)]
    [InlineData(AchievementMediaAssetType.Art, 2u)]
    public void AchievementMediaAssetTypeMatchesHeader(AchievementMediaAssetType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementMediaAssetType)value);
    }

    [Theory]
    [InlineData(AchievementParticipationType.Unknown, 0u)]
    [InlineData(AchievementParticipationType.Individual, 1u)]
    [InlineData(AchievementParticipationType.Group, 2u)]
    public void AchievementParticipationTypeMatchesHeader(AchievementParticipationType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementParticipationType)value);
    }

    [Theory]
    [InlineData(AchievementRewardType.Unknown, 0u)]
    [InlineData(AchievementRewardType.Gamerscore, 1u)]
    [InlineData(AchievementRewardType.InApp, 2u)]
    [InlineData(AchievementRewardType.Art, 3u)]
    public void AchievementRewardTypeMatchesHeader(AchievementRewardType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementRewardType)value);
    }

    [Theory]
    [InlineData(AchievementRarityCategory.Unset, 0u)]
    [InlineData(AchievementRarityCategory.Rare, 1u)]
    [InlineData(AchievementRarityCategory.Common, 2u)]
    public void AchievementRarityCategoryMatchesHeader(AchievementRarityCategory value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementRarityCategory)value);
    }

    // -----------------------------------------------------------------------
    // XblInitArgs: the layout most likely to be got wrong.
    //
    // A GDK title compiles as HC_PLATFORM_GDK, so the struct is exactly { queue, scid }.
    // The WIN32 build additionally has localStoragePath; if that field ever crept into the mirror,
    // the SCID pointer would land in the wrong slot and XblInitialize would read garbage. 16 bytes
    // is the whole assertion.
    // -----------------------------------------------------------------------

    [Fact]
    public void XblInitArgsMatchesGdkPlatformLayout()
    {
        Assert.Equal(16, sizeof(XblInitArgs));
        Assert.Equal(0, (int)Marshal.OffsetOf<XblInitArgs>(nameof(XblInitArgs.Queue)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XblInitArgs>(nameof(XblInitArgs.Scid)));
    }

    // -----------------------------------------------------------------------
    // XblUserProfile: fixed inline UTF-8 buffers, so every offset is a function of the
    // *_CHAR_SIZE constants in profile_c.h. Getting one buffer size wrong shifts every field
    // after it, which shows up as a garbled gamertag rather than a crash.
    // -----------------------------------------------------------------------

    [Fact]
    public void XblUserProfileMatchesHeaderLayout()
    {
        Assert.Equal(1848, sizeof(XblUserProfile));

        var profile = default(XblUserProfile);
        byte* origin = (byte*)&profile;

        Assert.Equal(0, (int)((byte*)&profile.XboxUserId - origin));
        Assert.Equal(8, (int)(profile.AppDisplayName - origin));
        Assert.Equal(98, (int)(profile.AppDisplayPictureResizeUri - origin));
        Assert.Equal(773, (int)(profile.GameDisplayName - origin));
        Assert.Equal(863, (int)(profile.GameDisplayPictureResizeUri - origin));
        Assert.Equal(1538, (int)(profile.Gamerscore - origin));
        Assert.Equal(1586, (int)(profile.Gamertag - origin));
        Assert.Equal(1634, (int)(profile.ModernGamertag - origin));
        Assert.Equal(1731, (int)(profile.ModernGamertagSuffix - origin));
        Assert.Equal(1746, (int)(profile.UniqueModernGamertag - origin));
    }

    [Fact]
    public void XblUserProfileBufferSizesMatchHeaderConstants()
    {
        Assert.Equal(90, XblUserProfile.DisplayNameCharSize);
        Assert.Equal(675, XblUserProfile.DisplayPicUrlRawCharSize);
        Assert.Equal(48, XblUserProfile.GamerscoreCharSize);
        Assert.Equal(48, XblUserProfile.GamertagCharSize);
        Assert.Equal(97, XblUserProfile.ModernGamertagCharSize);
        Assert.Equal(15, XblUserProfile.ModernGamertagSuffixCharSize);
        Assert.Equal(101, XblUserProfile.UniqueModernGamertagCharSize);
    }

    // -----------------------------------------------------------------------
    // Achievement structs. XblAchievement is a graph of pointers into result-handle memory,
    // so its layout has to be exact for the snapshot walk to be safe.
    // -----------------------------------------------------------------------

    [Fact]
    public void AchievementStructSizesMatchHeader()
    {
        Assert.Equal(16, sizeof(XblAchievementTitleAssociation));
        Assert.Equal(24, sizeof(XblAchievementRequirement));
        Assert.Equal(24, sizeof(XblAchievementProgression));
        Assert.Equal(16, sizeof(XblAchievementTimeWindow));
        Assert.Equal(24, sizeof(XblAchievementMediaAsset));
        Assert.Equal(48, sizeof(XblAchievementReward));
        Assert.Equal(200, sizeof(XblAchievement));
        Assert.Equal(40, sizeof(XblAchievementProgressChangeEntry));
        Assert.Equal(16, sizeof(XblAchievementProgressChangeEventArgs));
    }

    [Theory]
    [InlineData(nameof(XblAchievement.Id), 0)]
    [InlineData(nameof(XblAchievement.ServiceConfigurationId), 8)]
    [InlineData(nameof(XblAchievement.Name), 16)]
    [InlineData(nameof(XblAchievement.TitleAssociations), 24)]
    [InlineData(nameof(XblAchievement.TitleAssociationsCount), 32)]
    [InlineData(nameof(XblAchievement.ProgressState), 40)]
    [InlineData(nameof(XblAchievement.Progression), 48)]
    [InlineData(nameof(XblAchievement.MediaAssets), 72)]
    [InlineData(nameof(XblAchievement.MediaAssetsCount), 80)]
    [InlineData(nameof(XblAchievement.PlatformsAvailableOn), 88)]
    [InlineData(nameof(XblAchievement.PlatformsAvailableOnCount), 96)]
    [InlineData(nameof(XblAchievement.IsSecret), 104)]
    [InlineData(nameof(XblAchievement.UnlockedDescription), 112)]
    [InlineData(nameof(XblAchievement.LockedDescription), 120)]
    [InlineData(nameof(XblAchievement.ProductId), 128)]
    [InlineData(nameof(XblAchievement.Type), 136)]
    [InlineData(nameof(XblAchievement.ParticipationType), 140)]
    [InlineData(nameof(XblAchievement.Available), 144)]
    [InlineData(nameof(XblAchievement.Rewards), 160)]
    [InlineData(nameof(XblAchievement.RewardsCount), 168)]
    [InlineData(nameof(XblAchievement.EstimatedUnlockTime), 176)]
    [InlineData(nameof(XblAchievement.DeepLink), 184)]
    [InlineData(nameof(XblAchievement.IsRevoked), 192)]
    public void XblAchievementFieldOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblAchievement>(field));
    }

    [Fact]
    public void XblAchievementProgressionMatchesHeaderLayout()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<XblAchievementProgression>(nameof(XblAchievementProgression.Requirements)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XblAchievementProgression>(nameof(XblAchievementProgression.RequirementsCount)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XblAchievementProgression>(nameof(XblAchievementProgression.TimeUnlocked)));
    }

    // -----------------------------------------------------------------------
    // API shape. The projection's rule is that nothing native leaks: no HRESULTs, handles,
    // XAsyncBlocks, registration tokens or two-call size buffers on any public member.
    // -----------------------------------------------------------------------

    [Fact]
    public void PublicTypesAreSealed()
    {
        Assert.True(typeof(XboxLiveService).IsSealed);
        Assert.True(typeof(XboxLiveContext).IsSealed);
        Assert.True(typeof(XboxLiveContextSettings).IsSealed);
        Assert.True(typeof(ProfileService).IsSealed);
        Assert.True(typeof(UserProfile).IsSealed);
        Assert.True(typeof(AchievementsService).IsSealed);
        Assert.True(typeof(AchievementsPage).IsSealed);
        Assert.True(typeof(Achievement).IsSealed);
    }

    [Fact]
    public void HandleOwningTypesAreDisposable()
    {
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(XboxLiveService)));
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(XboxLiveContext)));
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(AchievementsPage)));
    }

    [Fact]
    public void ServicesAreNotPubliclyConstructible()
    {
        // These are only reachable through GameRuntime.XboxLive and XboxLiveService.CreateContext,
        // which is what keeps the native lifetimes correct.
        Assert.Empty(typeof(XboxLiveService).GetConstructors());
        Assert.Empty(typeof(XboxLiveContext).GetConstructors());
        Assert.Empty(typeof(ProfileService).GetConstructors());
        Assert.Empty(typeof(AchievementsService).GetConstructors());
        Assert.Empty(typeof(AchievementsPage).GetConstructors());
    }

    [Fact]
    public void PublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(XboxLiveService),
            typeof(XboxLiveContext),
            typeof(XboxLiveContextSettings),
            typeof(ProfileService),
            typeof(UserProfile),
            typeof(AchievementsService),
            typeof(AchievementsPage),
            typeof(Achievement),
            typeof(AchievementProgressChange),
            typeof(AchievementProgressChangedEventArgs),
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

    /// <summary>
    /// Every per-context notification registry has to be detachable, because XSAPI keeps calling a
    /// handler until it is explicitly removed and <see cref="XboxLiveContext.Dispose"/> closes the
    /// handle those handlers are keyed to. A registry that does not implement
    /// <c>IXboxLiveHandlerRegistry</c> would leave XSAPI calling back against a closed handle.
    /// </summary>
    [Fact]
    public void EveryXboxLiveRegistryIsDetachable()
    {
        Type detachable = typeof(XboxLiveContext).Assembly.GetType(
            "GDK.Net.XboxLive.IXboxLiveHandlerRegistry",
            throwOnError: true)!;

        foreach (Type type in typeof(XboxLiveContext).Assembly.GetTypes())
        {
            if (type.Namespace is not "GDK.Net.XboxLive" ||
                !type.IsClass ||
                !type.Name.EndsWith("Registry", StringComparison.Ordinal))
            {
                continue;
            }

            Assert.True(
                detachable.IsAssignableFrom(type),
                $"{type.Name} registers XSAPI handlers but does not implement IXboxLiveHandlerRegistry, " +
                "so XboxLiveContext.Dispose cannot detach it.");
        }
    }

    [Fact]
    public void GameRuntimeExposesXboxLive()
    {
        System.Reflection.PropertyInfo? property = typeof(GameRuntime).GetProperty(nameof(GameRuntime.XboxLive));

        Assert.NotNull(property);
        Assert.Equal(typeof(XboxLiveService), property!.PropertyType);
        Assert.Null(property.SetMethod);
    }

    // -----------------------------------------------------------------------
    // Value-type semantics.
    // -----------------------------------------------------------------------

    [Fact]
    public void AchievementTimeWindowHasValueEquality()
    {
        var a = new AchievementTimeWindow(
            DateTimeOffset.FromUnixTimeSeconds(1_000),
            DateTimeOffset.FromUnixTimeSeconds(2_000));
        var b = new AchievementTimeWindow(
            DateTimeOffset.FromUnixTimeSeconds(1_000),
            DateTimeOffset.FromUnixTimeSeconds(2_000));
        var c = new AchievementTimeWindow(
            DateTimeOffset.FromUnixTimeSeconds(1_000),
            DateTimeOffset.FromUnixTimeSeconds(3_000));

        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.NotEqual(a, c);
        Assert.True(a != c);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}
