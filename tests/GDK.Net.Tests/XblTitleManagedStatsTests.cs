using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblTitleManagedStatsTests
{
    [Theory]
    [InlineData(TitleManagedStatType.Number, 0u)]
    [InlineData(TitleManagedStatType.String, 1u)]
    public void StatisticTypeMatchesHeader(TitleManagedStatType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblTitleManagedStatType)value);
    }

    [Fact]
    public void TitleManagedStatisticStructSizeMatchesHeader() =>
        Assert.Equal(32, sizeof(XblTitleManagedStatistic));

    [Theory]
    [InlineData(nameof(XblTitleManagedStatistic.StatisticName), 0)]
    [InlineData(nameof(XblTitleManagedStatistic.StatisticType), 8)]
    [InlineData(nameof(XblTitleManagedStatistic.NumberValue), 16)]
    [InlineData(nameof(XblTitleManagedStatistic.StringValue), 24)]
    public void TitleManagedStatisticOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblTitleManagedStatistic>(field));

    [Fact]
    public void StatisticValueExposesOnlyActiveArm()
    {
        TitleManagedStatisticValue number = TitleManagedStatisticValue.FromNumber(42.5);
        Assert.Equal(TitleManagedStatType.Number, number.Type);
        Assert.Equal(42.5, number.NumberValue);
        Assert.Throws<InvalidOperationException>(() => number.StringValue);

        TitleManagedStatisticValue text = TitleManagedStatisticValue.FromString("gold");
        Assert.Equal(TitleManagedStatType.String, text.Type);
        Assert.Equal("gold", text.StringValue);
        Assert.Throws<InvalidOperationException>(() => text.NumberValue);
    }

    [Fact]
    public void StatisticValueEqualityHonorsDiscriminator()
    {
        Assert.Equal(TitleManagedStatisticValue.FromNumber(10), TitleManagedStatisticValue.FromNumber(10));
        Assert.NotEqual(TitleManagedStatisticValue.FromNumber(10), TitleManagedStatisticValue.FromString("10"));
        Assert.Equal(TitleManagedStatisticValue.FromString("10"), TitleManagedStatisticValue.FromString("10"));
    }

    [Fact]
    public void StatisticRejectsInvalidNames()
    {
        Assert.Throws<ArgumentNullException>(() => new TitleManagedStatistic(null!, 1));
        Assert.Throws<ArgumentException>(() => new TitleManagedStatistic(string.Empty, 1));
    }

    [Fact]
    public void TitleManagedStatsEntryPointsAreDeclared()
    {
        Type native = typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.NativeXbl", throwOnError: true)!;
        Assert.NotNull(native.GetMethod("XblTitleManagedStatsWriteAsync", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblTitleManagedStatsUpdateStatsAsync", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblTitleManagedStatsDeleteStatsAsync", BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Fact]
    public void PublicTitleManagedStatsTypesAreSealed()
    {
        Assert.True(typeof(TitleManagedStatistic).IsSealed);
        Assert.True(typeof(TitleManagedStatisticsService).IsSealed);
    }

    [Fact]
    public void TitleManagedStatsServiceIsNotPubliclyConstructible() =>
        Assert.Empty(typeof(TitleManagedStatisticsService).GetConstructors());

    [Fact]
    public void PublicTitleManagedStatsSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        {
            typeof(TitleManagedStatisticValue),
            typeof(TitleManagedStatistic),
            typeof(TitleManagedStatisticsService),
        };

        foreach (Type type in publicTypes)
        {
            foreach (MethodInfo method in type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (ParameterInfo parameter in method.GetParameters())
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
