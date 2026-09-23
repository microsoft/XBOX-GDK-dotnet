using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the XSAPI leaderboard projection. These do not load
/// <c>Microsoft.Xbox.Services.C.Thunks.dll</c>; they pin enum values, native layouts and public
/// API shape against GDK edition 260404.
/// </summary>
public sealed unsafe class XblLeaderboardTests
{
    [Theory]
    [InlineData(LeaderboardStatType.UInt64, 0u)]
    [InlineData(LeaderboardStatType.Boolean, 1u)]
    [InlineData(LeaderboardStatType.Double, 2u)]
    [InlineData(LeaderboardStatType.String, 3u)]
    [InlineData(LeaderboardStatType.Other, 4u)]
    public void StatisticTypeMatchesHeader(LeaderboardStatType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblLeaderboardStatType)value);
    }

    [Theory]
    [InlineData(LeaderboardSortOrder.Descending, 0u)]
    [InlineData(LeaderboardSortOrder.Ascending, 1u)]
    public void SortOrderMatchesHeader(LeaderboardSortOrder value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblLeaderboardSortOrder)value);
    }

    [Theory]
    [InlineData(SocialGroupType.None, 0u)]
    [InlineData(SocialGroupType.People, 1u)]
    [InlineData(SocialGroupType.Favorites, 2u)]
    public void SocialGroupMatchesHeader(SocialGroupType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblSocialGroupType)value);
    }

    [Theory]
    [InlineData(LeaderboardQueryType.UserStatBacked, 0u)]
    [InlineData(LeaderboardQueryType.TitleManagedStatBackedGlobal, 1u)]
    [InlineData(LeaderboardQueryType.TitleManagedStatBackedSocial, 2u)]
    public void QueryTypeMatchesHeader(LeaderboardQueryType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblLeaderboardQueryType)value);
    }

    [Fact]
    public void LeaderboardBufferSizesMatchHeaderConstants()
    {
        Assert.Equal(48, XblLeaderboardRow.GamertagCharSize);
        Assert.Equal(97, XblLeaderboardRow.ModernGamertagCharSize);
        Assert.Equal(15, XblLeaderboardRow.ModernGamertagSuffixCharSize);
        Assert.Equal(101, XblLeaderboardRow.UniqueModernGamertagCharSize);
        Assert.Equal(40, XblLeaderboardQuery.ScidLength);
    }

    [Fact]
    public void LeaderboardStructSizesMatchHeader()
    {
        Assert.Equal(16, sizeof(XblLeaderboardColumn));
        Assert.Equal(304, sizeof(XblLeaderboardRow));
        Assert.Equal(128, sizeof(XblLeaderboardQuery));
        Assert.Equal(176, sizeof(XblLeaderboardResult));
    }

    [Theory]
    [InlineData(nameof(XblLeaderboardColumn.StatName), 0)]
    [InlineData(nameof(XblLeaderboardColumn.StatType), 8)]
    public void XblLeaderboardColumnFieldOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblLeaderboardColumn>(field));

    [Fact]
    public void XblLeaderboardRowFieldOffsetsMatchHeader()
    {
        var row = default(XblLeaderboardRow);
        byte* origin = (byte*)&row;

        Assert.Equal(0, (int)(row.Gamertag - origin));
        Assert.Equal(48, (int)(row.ModernGamertag - origin));
        Assert.Equal(145, (int)(row.ModernGamertagSuffix - origin));
        Assert.Equal(160, (int)(row.UniqueModernGamertag - origin));
        Assert.Equal(264, (int)((byte*)&row.XboxUserId - origin));
        Assert.Equal(272, (int)((byte*)&row.Percentile - origin));
        Assert.Equal(280, (int)((byte*)&row.Rank - origin));
        Assert.Equal(284, (int)((byte*)&row.GlobalRank - origin));
        Assert.Equal(288, (int)((byte*)&row.ColumnValues - origin));
        Assert.Equal(296, (int)((byte*)&row.ColumnValuesCount - origin));
    }

    [Fact]
    public void XblLeaderboardQueryFieldOffsetsMatchHeader()
    {
        var query = default(XblLeaderboardQuery);
        byte* origin = (byte*)&query;

        Assert.Equal(0, (int)((byte*)&query.XboxUserId - origin));
        Assert.Equal(8, (int)(query.Scid - origin));
        Assert.Equal(48, (int)((byte*)&query.LeaderboardName - origin));
        Assert.Equal(56, (int)((byte*)&query.StatName - origin));
        Assert.Equal(64, (int)((byte*)&query.SocialGroup - origin));
        Assert.Equal(72, (int)((byte*)&query.AdditionalColumnLeaderboardNames - origin));
        Assert.Equal(80, (int)((byte*)&query.AdditionalColumnLeaderboardNamesCount - origin));
        Assert.Equal(88, (int)((byte*)&query.Order - origin));
        Assert.Equal(92, (int)((byte*)&query.MaxItems - origin));
        Assert.Equal(96, (int)((byte*)&query.SkipToXboxUserId - origin));
        Assert.Equal(104, (int)((byte*)&query.SkipResultToRank - origin));
        Assert.Equal(112, (int)((byte*)&query.ContinuationToken - origin));
        Assert.Equal(120, (int)((byte*)&query.QueryType - origin));
    }

    [Theory]
    [InlineData(nameof(XblLeaderboardResult.TotalRowCount), 0)]
    [InlineData(nameof(XblLeaderboardResult.Columns), 8)]
    [InlineData(nameof(XblLeaderboardResult.ColumnsCount), 16)]
    [InlineData(nameof(XblLeaderboardResult.Rows), 24)]
    [InlineData(nameof(XblLeaderboardResult.RowsCount), 32)]
    [InlineData(nameof(XblLeaderboardResult.HasNext), 40)]
    [InlineData(nameof(XblLeaderboardResult.NextQuery), 48)]
    public void XblLeaderboardResultFieldOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblLeaderboardResult>(field));

    [Fact]
    public void LeaderboardEntryPointsAreDeclared()
    {
        Type native = typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.NativeXbl", throwOnError: true)!;
        Assert.NotNull(native.GetMethod("XblLeaderboardGetLeaderboardAsync", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblLeaderboardGetLeaderboardResultSize", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblLeaderboardGetLeaderboardResult", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblLeaderboardResultGetNextAsync", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblLeaderboardResultGetNextResultSize", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.NotNull(native.GetMethod("XblLeaderboardResultGetNextResult", BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Fact]
    public void PublicLeaderboardTypesAreSealed()
    {
        Assert.True(typeof(LeaderboardQuery).IsSealed);
        Assert.True(typeof(LeaderboardColumn).IsSealed);
        Assert.True(typeof(LeaderboardRow).IsSealed);
        Assert.True(typeof(LeaderboardPage).IsSealed);
        Assert.True(typeof(LeaderboardService).IsSealed);
    }

    [Fact]
    public void LeaderboardHandleOwningTypesAreDisposable() =>
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(LeaderboardPage)));

    [Fact]
    public void LeaderboardServicesAreNotPubliclyConstructible()
    {
        Assert.Empty(typeof(LeaderboardService).GetConstructors());
        Assert.Empty(typeof(LeaderboardPage).GetConstructors());
    }

    [Fact]
    public void PublicLeaderboardSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        {
            typeof(LeaderboardQuery),
            typeof(LeaderboardColumn),
            typeof(LeaderboardRow),
            typeof(LeaderboardPage),
            typeof(LeaderboardService),
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

    [Fact]
    public void LeaderboardQueryCopiesAdditionalColumnNames()
    {
        string[] columns = { "score", "kills" };
        var query = new LeaderboardQuery(0, "00000000-0000-0000-0000-000000000000", additionalColumnLeaderboardNames: columns);
        columns[0] = "mutated";

        Assert.Equal("score", query.AdditionalColumnLeaderboardNames[0]);
        Assert.Equal("kills", query.AdditionalColumnLeaderboardNames[1]);
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
