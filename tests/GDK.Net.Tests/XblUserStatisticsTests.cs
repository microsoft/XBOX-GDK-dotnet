using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblUserStatisticsTests
{
    [Fact]
    public void UserStatisticsStructSizesMatchHeader()
    {
        Assert.Equal(24, sizeof(XblStatistic));
        Assert.Equal(56, sizeof(XblServiceConfigurationStatistic));
        Assert.Equal(24, sizeof(XblUserStatisticsResult));
        Assert.Equal(56, sizeof(XblRequestedStatistics));
        Assert.Equal(72, sizeof(XblStatisticChangeEventArgs));
    }

    [Fact]
    public void UserStatisticsInlineBufferSizesMatchHeaderConstants()
    {
        Assert.Equal(40, XblServiceConfigurationStatistic.ServiceConfigurationIdLength);
        Assert.Equal(40, XblRequestedStatistics.ServiceConfigurationIdLength);
        Assert.Equal(40, XblStatisticChangeEventArgs.ServiceConfigurationIdLength);
    }

    [Theory]
    [InlineData(nameof(XblStatistic.StatisticName), 0)]
    [InlineData(nameof(XblStatistic.StatisticType), 8)]
    [InlineData(nameof(XblStatistic.Value), 16)]
    public void XblStatisticOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblStatistic>(field));

    [Theory]
    [InlineData(nameof(XblServiceConfigurationStatistic.ServiceConfigurationId), 0)]
    [InlineData(nameof(XblServiceConfigurationStatistic.Statistics), 40)]
    [InlineData(nameof(XblServiceConfigurationStatistic.StatisticsCount), 48)]
    public void XblServiceConfigurationStatisticOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblServiceConfigurationStatistic>(field));

    [Theory]
    [InlineData(nameof(XblUserStatisticsResult.XboxUserId), 0)]
    [InlineData(nameof(XblUserStatisticsResult.ServiceConfigStatistics), 8)]
    [InlineData(nameof(XblUserStatisticsResult.ServiceConfigStatisticsCount), 16)]
    public void XblUserStatisticsResultOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblUserStatisticsResult>(field));

    [Theory]
    [InlineData(nameof(XblRequestedStatistics.ServiceConfigurationId), 0)]
    [InlineData(nameof(XblRequestedStatistics.Statistics), 40)]
    [InlineData(nameof(XblRequestedStatistics.StatisticsCount), 48)]
    public void XblRequestedStatisticsOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblRequestedStatistics>(field));

    [Theory]
    [InlineData(nameof(XblStatisticChangeEventArgs.XboxUserId), 0)]
    [InlineData(nameof(XblStatisticChangeEventArgs.ServiceConfigurationId), 8)]
    [InlineData(nameof(XblStatisticChangeEventArgs.LatestStatistic), 48)]
    public void XblStatisticChangeEventArgsOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblStatisticChangeEventArgs>(field));

    [Fact]
    public void NativeStatisticGraphIsSnapshotted()
    {
        IntPtr name = Utf8.Allocate("kills");
        IntPtr type = Utf8.Allocate("Integer");
        IntPtr value = Utf8.Allocate("42");

        try
        {
            var statistic = new XblStatistic
            {
                StatisticName = (byte*)name,
                StatisticType = (byte*)type,
                Value = (byte*)value,
            };

            var serviceConfiguration = new XblServiceConfigurationStatistic
            {
                Statistics = &statistic,
                StatisticsCount = 1,
            };
            byte* serviceScid = serviceConfiguration.ServiceConfigurationId;
            CopyAscii(
                serviceScid,
                "00000000-0000-0000-0000-000000000000",
                XblServiceConfigurationStatistic.ServiceConfigurationIdLength);

            var native = new XblUserStatisticsResult
            {
                XboxUserId = 2814639012345678,
                ServiceConfigStatistics = &serviceConfiguration,
                ServiceConfigStatisticsCount = 1,
            };

            UserStatisticsResult result = UserStatisticsResult.FromNative(&native);

            Assert.Equal(2814639012345678UL, result.XboxUserId);
            ServiceConfigurationStatistic service = Assert.Single(result.ServiceConfigurations);
            Assert.Equal("00000000-0000-0000-0000-000000000000", service.ServiceConfigurationId);
            Statistic managed = Assert.Single(service.Statistics);
            Assert.Equal("kills", managed.Name);
            Assert.Equal("Integer", managed.Type);
            Assert.Equal("42", managed.Value);
        }
        finally
        {
            Utf8.Free(name);
            Utf8.Free(type);
            Utf8.Free(value);
        }
    }

    [Fact]
    public void NativeStatisticChangeEventIsSnapshotted()
    {
        IntPtr name = Utf8.Allocate("score");
        IntPtr type = Utf8.Allocate("Number");
        IntPtr value = Utf8.Allocate("9001");

        try
        {
            var native = new XblStatisticChangeEventArgs
            {
                XboxUserId = 123,
                LatestStatistic = new XblStatistic
                {
                    StatisticName = (byte*)name,
                    StatisticType = (byte*)type,
                    Value = (byte*)value,
                },
            };
            byte* eventScid = native.ServiceConfigurationId;
            CopyAscii(eventScid, "scid", XblStatisticChangeEventArgs.ServiceConfigurationIdLength);

            StatisticChangedEventArgs args = StatisticChangedEventArgs.FromNative(native);

            Assert.Equal(123UL, args.XboxUserId);
            Assert.Equal("scid", args.ServiceConfigurationId);
            Assert.Equal("score", args.LatestStatistic.Name);
            Assert.Equal("Number", args.LatestStatistic.Type);
            Assert.Equal("9001", args.LatestStatistic.Value);
        }
        finally
        {
            Utf8.Free(name);
            Utf8.Free(type);
            Utf8.Free(value);
        }
    }

    [Fact]
    public void RequestedStatisticsSnapshotInputs()
    {
        var names = new[] { "kills", "wins" };

        var request = new RequestedStatistics("scid", names);
        names[0] = "changed";

        Assert.Equal("scid", request.ServiceConfigurationId);
        Assert.Equal(new[] { "kills", "wins" }, request.StatisticNames);
    }

    [Fact]
    public void UserStatisticsServiceHasRequiredInternalConstructor()
    {
        ConstructorInfo? constructor = typeof(UserStatisticsService).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            types: new[] { typeof(XboxLiveContext) },
            modifiers: null);

        Assert.NotNull(constructor);
        Assert.False(constructor!.IsPublic);
    }

    [Fact]
    public void UserStatisticsPublicTypesAreSealed()
    {
        Type[] types =
        [
            typeof(UserStatisticsService),
            typeof(Statistic),
            typeof(ServiceConfigurationStatistic),
            typeof(UserStatisticsResult),
            typeof(RequestedStatistics),
            typeof(StatisticChangedEventArgs),
        ];

        Assert.All(types, type => Assert.True(type.IsSealed, $"{type.Name} must be sealed."));
    }


    [Fact]
    public void UserStatisticsTypesAreNotPubliclyConstructibleWhenNativeBacked()
    {
        Assert.Empty(typeof(UserStatisticsService).GetConstructors());
        Assert.Empty(typeof(Statistic).GetConstructors());
        Assert.Empty(typeof(ServiceConfigurationStatistic).GetConstructors());
        Assert.Empty(typeof(UserStatisticsResult).GetConstructors());
        Assert.Empty(typeof(StatisticChangedEventArgs).GetConstructors());
        Assert.Single(typeof(RequestedStatistics).GetConstructors());
    }

    [Fact]
    public void UserStatisticsPublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(UserStatisticsService),
            typeof(Statistic),
            typeof(ServiceConfigurationStatistic),
            typeof(UserStatisticsResult),
            typeof(RequestedStatistics),
            typeof(StatisticChangedEventArgs),
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
    public void UserStatisticsAsyncMethodsHideNativeResultBuffers()
    {
        Assert.Equal(
            typeof(Task<UserStatisticsResult>),
            typeof(UserStatisticsService).GetMethod(nameof(UserStatisticsService.GetSingleUserStatisticAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<UserStatisticsResult>),
            typeof(UserStatisticsService).GetMethod(nameof(UserStatisticsService.GetSingleUserStatisticsAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<IReadOnlyList<UserStatisticsResult>>),
            typeof(UserStatisticsService).GetMethod(nameof(UserStatisticsService.GetMultipleUserStatisticsAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<IReadOnlyList<UserStatisticsResult>>),
            typeof(UserStatisticsService).GetMethod(nameof(UserStatisticsService.GetMultipleUserStatisticsForMultipleServiceConfigurationsAsync))!.ReturnType);
    }

    [Fact]
    public void UserStatisticsCallbackThunkResolvesToRealFunctionPointer()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.UserStatisticChangedHandler);
    }

    private static void CopyAscii(byte* destination, string value, int length)
    {
        Assert.True(value.Length < length);
        for (int i = 0; i < value.Length; i++)
        {
            destination[i] = (byte)value[i];
        }

        destination[value.Length] = 0;
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
