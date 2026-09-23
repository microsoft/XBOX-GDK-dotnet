using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblPresenceTests
{
    [Theory]
    [InlineData(PresenceDeviceType.Unknown, 0u)]
    [InlineData(PresenceDeviceType.WindowsPhone, 1u)]
    [InlineData(PresenceDeviceType.WindowsPhone7, 2u)]
    [InlineData(PresenceDeviceType.Web, 3u)]
    [InlineData(PresenceDeviceType.Xbox360, 4u)]
    [InlineData(PresenceDeviceType.Pc, 5u)]
    [InlineData(PresenceDeviceType.Windows8, 6u)]
    [InlineData(PresenceDeviceType.XboxOne, 7u)]
    [InlineData(PresenceDeviceType.WindowsOneCore, 8u)]
    [InlineData(PresenceDeviceType.WindowsOneCoreMobile, 9u)]
    [InlineData(PresenceDeviceType.Ios, 10u)]
    [InlineData(PresenceDeviceType.Android, 11u)]
    [InlineData(PresenceDeviceType.AppleTV, 12u)]
    [InlineData(PresenceDeviceType.Nintendo, 13u)]
    [InlineData(PresenceDeviceType.PlayStation, 14u)]
    [InlineData(PresenceDeviceType.Win32, 15u)]
    [InlineData(PresenceDeviceType.Scarlett, 16u)]
    public void PresenceDeviceTypeMatchesHeader(PresenceDeviceType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceDeviceType)value);
    }

    [Theory]
    [InlineData(PresenceUserState.Unknown, 0u)]
    [InlineData(PresenceUserState.Online, 1u)]
    [InlineData(PresenceUserState.Away, 2u)]
    [InlineData(PresenceUserState.Offline, 3u)]
    public void PresenceUserStateMatchesHeader(PresenceUserState value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceUserState)value);
    }

    [Theory]
    [InlineData(PresenceTitleViewState.Unknown, 0u)]
    [InlineData(PresenceTitleViewState.FullScreen, 1u)]
    [InlineData(PresenceTitleViewState.Filled, 2u)]
    [InlineData(PresenceTitleViewState.Snapped, 3u)]
    [InlineData(PresenceTitleViewState.Background, 4u)]
    public void PresenceTitleViewStateMatchesHeader(PresenceTitleViewState value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceTitleViewState)value);
    }

    [Theory]
    [InlineData(PresenceDetailLevel.Default, 0u)]
    [InlineData(PresenceDetailLevel.User, 1u)]
    [InlineData(PresenceDetailLevel.Device, 2u)]
    [InlineData(PresenceDetailLevel.Title, 3u)]
    [InlineData(PresenceDetailLevel.All, 4u)]
    public void PresenceDetailLevelMatchesHeader(PresenceDetailLevel value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceDetailLevel)value);
    }

    [Theory]
    [InlineData(PresenceMediaIdType.Unknown, 0u)]
    [InlineData(PresenceMediaIdType.Bing, 1u)]
    [InlineData(PresenceMediaIdType.MediaProvider, 2u)]
    public void PresenceMediaIdTypeMatchesHeader(PresenceMediaIdType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceMediaIdType)value);
    }

    [Theory]
    [InlineData(PresenceTitleState.Unknown, 0u)]
    [InlineData(PresenceTitleState.Started, 1u)]
    [InlineData(PresenceTitleState.Ended, 2u)]
    public void PresenceTitleStateMatchesHeader(PresenceTitleState value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceTitleState)value);
    }

    [Theory]
    [InlineData(PresenceBroadcastProvider.Unknown, 0u)]
    [InlineData(PresenceBroadcastProvider.Twitch, 1u)]
    public void PresenceBroadcastProviderMatchesHeader(PresenceBroadcastProvider value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblPresenceBroadcastProvider)value);
    }

    [Fact]
    public void PresenceStructSizesMatchHeader()
    {
        Assert.Equal(24, sizeof(XblPresenceDeviceRecord));
        Assert.Equal(56, sizeof(XblPresenceTitleRecord));
        Assert.Equal(64, sizeof(XblPresenceBroadcastRecord));
        Assert.Equal(64, sizeof(XblPresenceRichPresenceIds));
        Assert.Equal(40, sizeof(XblPresenceQueryFilters));
    }

    [Fact]
    public void PresenceInlineBufferSizesMatchHeaderConstants()
    {
        Assert.Equal(40, XblPresenceBroadcastRecord.SessionCharSize);
        Assert.Equal(40, XblPresenceRichPresenceIds.ScidCharSize);
    }

    [Theory]
    [InlineData(nameof(XblPresenceDeviceRecord.DeviceType), 0)]
    [InlineData(nameof(XblPresenceDeviceRecord.TitleRecords), 8)]
    [InlineData(nameof(XblPresenceDeviceRecord.TitleRecordsCount), 16)]
    public void XblPresenceDeviceRecordOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPresenceDeviceRecord>(field));
    }

    [Theory]
    [InlineData(nameof(XblPresenceTitleRecord.TitleId), 0)]
    [InlineData(nameof(XblPresenceTitleRecord.TitleName), 8)]
    [InlineData(nameof(XblPresenceTitleRecord.LastModified), 16)]
    [InlineData(nameof(XblPresenceTitleRecord.TitleActive), 24)]
    [InlineData(nameof(XblPresenceTitleRecord.RichPresenceString), 32)]
    [InlineData(nameof(XblPresenceTitleRecord.ViewState), 40)]
    [InlineData(nameof(XblPresenceTitleRecord.BroadcastRecord), 48)]
    public void XblPresenceTitleRecordOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPresenceTitleRecord>(field));
    }

    [Theory]
    [InlineData(nameof(XblPresenceBroadcastRecord.BroadcastId), 0)]
    [InlineData(nameof(XblPresenceBroadcastRecord.Session), 8)]
    [InlineData(nameof(XblPresenceBroadcastRecord.Provider), 48)]
    [InlineData(nameof(XblPresenceBroadcastRecord.ViewerCount), 52)]
    [InlineData(nameof(XblPresenceBroadcastRecord.StartTime), 56)]
    public void XblPresenceBroadcastRecordOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPresenceBroadcastRecord>(field));
    }

    [Theory]
    [InlineData(nameof(XblPresenceRichPresenceIds.Scid), 0)]
    [InlineData(nameof(XblPresenceRichPresenceIds.PresenceId), 40)]
    [InlineData(nameof(XblPresenceRichPresenceIds.PresenceTokenIds), 48)]
    [InlineData(nameof(XblPresenceRichPresenceIds.PresenceTokenIdsCount), 56)]
    public void XblPresenceRichPresenceIdsOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPresenceRichPresenceIds>(field));
    }

    [Theory]
    [InlineData(nameof(XblPresenceQueryFilters.DeviceTypes), 0)]
    [InlineData(nameof(XblPresenceQueryFilters.DeviceTypesCount), 8)]
    [InlineData(nameof(XblPresenceQueryFilters.TitleIds), 16)]
    [InlineData(nameof(XblPresenceQueryFilters.TitleIdsCount), 24)]
    [InlineData(nameof(XblPresenceQueryFilters.DetailLevel), 32)]
    [InlineData(nameof(XblPresenceQueryFilters.OnlineOnly), 36)]
    [InlineData(nameof(XblPresenceQueryFilters.BroadcastingOnly), 37)]
    public void XblPresenceQueryFiltersOffsetsMatchHeader(string field, int expected)
    {
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblPresenceQueryFilters>(field));
    }

    [Fact]
    public void PresenceSocialGroupNamesMatchHeader()
    {
        Assert.Equal("Favorites", PresenceService.SocialGroupName(PresenceSocialGroup.Favorites));
        Assert.Equal("People", PresenceService.SocialGroupName(PresenceSocialGroup.People));
        Assert.Equal("Friends", PresenceService.SocialGroupName(PresenceSocialGroup.Friends));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => PresenceService.SocialGroupName((PresenceSocialGroup)42));
    }

    [Fact]
    public void PresenceQueryFiltersSnapshotInputs()
    {
        var devices = new[] { PresenceDeviceType.Pc };
        var titles = new[] { 123u };

        var filters = new PresenceQueryFilters(devices, titles, PresenceDetailLevel.All, onlineOnly: true);

        devices[0] = PresenceDeviceType.XboxOne;
        titles[0] = 456u;

        Assert.Equal(PresenceDeviceType.Pc, filters.DeviceTypes[0]);
        Assert.Equal(123u, filters.TitleIds[0]);
        Assert.Equal(PresenceDetailLevel.All, filters.DetailLevel);
        Assert.True(filters.OnlineOnly);
        Assert.False(filters.BroadcastingOnly);
    }

    [Fact]
    public void PresenceRichPresenceIdsSnapshotInputs()
    {
        var tokens = new[] { "map-1" };

        var richPresence = new PresenceRichPresenceIds("00000000-0000-0000-0000-000000000000", "playing", tokens);

        tokens[0] = "changed";

        Assert.Equal("00000000-0000-0000-0000-000000000000", richPresence.ServiceConfigurationId);
        Assert.Equal("playing", richPresence.PresenceId);
        Assert.Equal("map-1", richPresence.PresenceTokenIds[0]);
    }

    [Fact]
    public void PresenceServiceHasRequiredInternalConstructor()
    {
        ConstructorInfo? constructor = typeof(PresenceService).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            types: new[] { typeof(XboxLiveContext) },
            modifiers: null);

        Assert.NotNull(constructor);
        Assert.False(constructor!.IsPublic);
    }

    [Fact]
    public void PresencePublicTypesAreSealed()
    {
        Type[] types =
        [
            typeof(PresenceService),
            typeof(PresenceRichPresenceIds),
            typeof(PresenceQueryFilters),
            typeof(PresenceRecord),
            typeof(PresenceDeviceRecord),
            typeof(PresenceTitleRecord),
            typeof(PresenceBroadcastRecord),
            typeof(DevicePresenceChangedEventArgs),
            typeof(TitlePresenceChangedEventArgs),
        ];

        Assert.All(types, type => Assert.True(type.IsSealed, $"{type.Name} must be sealed."));
    }


    [Fact]
    public void PresenceTypesAreNotPubliclyConstructibleWhenNativeBacked()
    {
        Assert.Empty(typeof(PresenceService).GetConstructors());
        Assert.Empty(typeof(PresenceRecord).GetConstructors());
        Assert.Empty(typeof(PresenceDeviceRecord).GetConstructors());
        Assert.Empty(typeof(PresenceTitleRecord).GetConstructors());
        Assert.Empty(typeof(PresenceBroadcastRecord).GetConstructors());
    }

    [Fact]
    public void PresencePublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(PresenceService),
            typeof(PresenceRichPresenceIds),
            typeof(PresenceQueryFilters),
            typeof(PresenceRecord),
            typeof(PresenceDeviceRecord),
            typeof(PresenceTitleRecord),
            typeof(PresenceBroadcastRecord),
            typeof(DevicePresenceChangedEventArgs),
            typeof(TitlePresenceChangedEventArgs),
        ];

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
    public void PresenceCallbackThunksResolveToRealFunctionPointers()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.PresenceDevicePresenceChangedHandler);
        Assert.NotEqual(IntPtr.Zero, Trampolines.PresenceTitlePresenceChangedHandler);
        Assert.NotEqual(
            Trampolines.PresenceDevicePresenceChangedHandler,
            Trampolines.PresenceTitlePresenceChangedHandler);
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
