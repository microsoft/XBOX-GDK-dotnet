// Blittable mirrors of the XSAPI leaderboard types -- xsapi-c\leaderboard_c.h, GDK edition 260404.
//
// See NativeTypes.Xbl.cs for the layout rules that apply across the XSAPI type mirrors. The
// leaderboard result is unusual for XSAPI: XblLeaderboardGetLeaderboardResult and
// XblLeaderboardResultGetNextResult write a complete pointer graph into a caller-allocated byte
// buffer and return a typed pointer into that same buffer. The projection keeps that buffer alive
// only while it may need to read the native result (including fetching a next page), and snapshots
// all columns, rows and string values into managed objects before the buffer is released.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblLeaderboardStatType</c>.</summary>
internal enum XblLeaderboardStatType : uint
{
    Uint64 = 0,
    Boolean = 1,
    Double = 2,
    String = 3,
    Other = 4,
}

/// <summary>Mirrors <c>XblLeaderboardSortOrder</c>.</summary>
internal enum XblLeaderboardSortOrder : uint
{
    Descending = 0,
    Ascending = 1,
}

/// <summary>Mirrors <c>XblSocialGroupType</c>.</summary>
internal enum XblSocialGroupType : uint
{
    None = 0,
    People = 1,
    Favorites = 2,
}

/// <summary>Mirrors <c>XblLeaderboardQueryType</c>.</summary>
internal enum XblLeaderboardQueryType : uint
{
    UserStatBacked = 0,
    TitleManagedStatBackedGlobal = 1,
    TitleManagedStatBackedSocial = 2,
}

/// <summary>Mirrors <c>XblLeaderboardColumn</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblLeaderboardColumn
{
    internal byte* StatName;
    internal XblLeaderboardStatType StatType;
}

/// <summary>Mirrors <c>XblLeaderboardRow</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblLeaderboardRow
{
    internal const int GamertagCharSize = 16 * 3;
    internal const int ModernGamertagCharSize = ((12 + 12) * 4) + 1;
    internal const int ModernGamertagSuffixCharSize = 14 + 1;
    internal const int UniqueModernGamertagCharSize = ModernGamertagCharSize + 1 + 3;

    internal fixed byte Gamertag[GamertagCharSize];
    internal fixed byte ModernGamertag[ModernGamertagCharSize];
    internal fixed byte ModernGamertagSuffix[ModernGamertagSuffixCharSize];
    internal fixed byte UniqueModernGamertag[UniqueModernGamertagCharSize];
    internal ulong XboxUserId;
    internal double Percentile;
    internal uint Rank;
    internal uint GlobalRank;
    internal byte** ColumnValues;
    internal nuint ColumnValuesCount;
}

/// <summary>Mirrors <c>XblLeaderboardQuery</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblLeaderboardQuery
{
    internal const int ScidLength = 40;

    internal ulong XboxUserId;
    internal fixed byte Scid[ScidLength];
    internal byte* LeaderboardName;
    internal byte* StatName;
    internal XblSocialGroupType SocialGroup;
    internal byte** AdditionalColumnLeaderboardNames;
    internal nuint AdditionalColumnLeaderboardNamesCount;
    internal XblLeaderboardSortOrder Order;
    internal uint MaxItems;
    internal ulong SkipToXboxUserId;
    internal uint SkipResultToRank;
    internal byte* ContinuationToken;
    internal XblLeaderboardQueryType QueryType;
}

/// <summary>Mirrors <c>XblLeaderboardResult</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblLeaderboardResult
{
    internal uint TotalRowCount;
    internal XblLeaderboardColumn* Columns;
    internal nuint ColumnsCount;
    internal XblLeaderboardRow* Rows;
    internal nuint RowsCount;
    internal byte HasNext;
    internal XblLeaderboardQuery NextQuery;
}
