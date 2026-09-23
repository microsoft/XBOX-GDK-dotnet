using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>The data type of a leaderboard statistic. Mirrors <c>XblLeaderboardStatType</c>.</summary>
public enum LeaderboardStatType : uint
{
    /// <summary>An unsigned 64-bit integer.</summary>
    UInt64 = 0,

    /// <summary>A Boolean value.</summary>
    Boolean = 1,

    /// <summary>A double-precision floating-point value.</summary>
    Double = 2,

    /// <summary>A string value.</summary>
    String = 3,

    /// <summary>A service-defined or unknown value type.</summary>
    Other = 4,
}

/// <summary>The order to sort a leaderboard in. Mirrors <c>XblLeaderboardSortOrder</c>.</summary>
public enum LeaderboardSortOrder : uint
{
    /// <summary>Highest value to lowest value.</summary>
    Descending = 0,

    /// <summary>Lowest value to highest value.</summary>
    Ascending = 1,
}

/// <summary>The social group a leaderboard query is scoped to. Mirrors <c>XblSocialGroupType</c>.</summary>
public enum SocialGroupType : uint
{
    /// <summary>No social group; request a global leaderboard.</summary>
    None = 0,

    /// <summary>People the requesting user follows.</summary>
    People = 1,

    /// <summary>People the requesting user tagged as favorites.</summary>
    Favorites = 2,
}

/// <summary>The backing store used by a leaderboard query. Mirrors <c>XblLeaderboardQueryType</c>.</summary>
public enum LeaderboardQueryType : uint
{
    /// <summary>An event-based user-stat leaderboard.</summary>
    UserStatBacked = 0,

    /// <summary>A global leaderboard backed by a title-managed stat.</summary>
    TitleManagedStatBackedGlobal = 1,

    /// <summary>A social leaderboard backed by a title-managed stat.</summary>
    TitleManagedStatBackedSocial = 2,
}

/// <summary>
/// Parameters for an Xbox Live leaderboard query. Managed equivalent of
/// <c>XblLeaderboardQuery</c>.
/// </summary>
public sealed class LeaderboardQuery
{
    private readonly string[] _additionalColumnLeaderboardNames;

    /// <summary>
    /// Creates a leaderboard query.
    /// </summary>
    /// <param name="xboxUserId">
    /// Optional Xbox user id of the requesting user. Set to 0 for a global leaderboard.
    /// </param>
    /// <param name="serviceConfigurationId">The title's service configuration id.</param>
    /// <param name="leaderboardName">
    /// Optional leaderboard name for an event-based user-stat leaderboard.
    /// </param>
    /// <param name="statisticName">
    /// Optional statistic name for a social or title-managed-stat-backed leaderboard.
    /// </param>
    /// <param name="socialGroup">The social group to request, or <see cref="SocialGroupType.None"/>.</param>
    /// <param name="additionalColumnLeaderboardNames">Optional additional statistic columns to return.</param>
    /// <param name="order">The sort order.</param>
    /// <param name="maxItems">Maximum rows to return. 0 lets the service choose.</param>
    /// <param name="skipToXboxUserId">Xbox user id to start at. 0 disables this skip.</param>
    /// <param name="skipResultToRank">Rank to start at. 0 disables this skip.</param>
    /// <param name="continuationToken">Optional continuation token for a query resumed by the service.</param>
    /// <param name="queryType">The leaderboard backing store to query.</param>
    public LeaderboardQuery(
        ulong xboxUserId,
        string serviceConfigurationId,
        string? leaderboardName = null,
        string? statisticName = null,
        SocialGroupType socialGroup = SocialGroupType.None,
        IEnumerable<string>? additionalColumnLeaderboardNames = null,
        LeaderboardSortOrder order = LeaderboardSortOrder.Descending,
        uint maxItems = 0,
        ulong skipToXboxUserId = 0,
        uint skipResultToRank = 0,
        string? continuationToken = null,
        LeaderboardQueryType queryType = LeaderboardQueryType.UserStatBacked)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        XboxUserId = xboxUserId;
        ServiceConfigurationId = serviceConfigurationId;
        LeaderboardName = leaderboardName;
        StatisticName = statisticName;
        SocialGroup = socialGroup;
        _additionalColumnLeaderboardNames = ToArray(additionalColumnLeaderboardNames);
        AdditionalColumnLeaderboardNames =
            new ReadOnlyCollection<string>(_additionalColumnLeaderboardNames);
        Order = order;
        MaxItems = maxItems;
        SkipToXboxUserId = skipToXboxUserId;
        SkipResultToRank = skipResultToRank;
        ContinuationToken = continuationToken;
        QueryType = queryType;
    }

    /// <summary>Optional Xbox user id of the requesting user.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The title's service configuration id.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>Optional leaderboard name for an event-based user-stat leaderboard.</summary>
    public string? LeaderboardName { get; }

    /// <summary>Optional statistic name for a social or title-managed-stat-backed leaderboard.</summary>
    public string? StatisticName { get; }

    /// <summary>The social group to request, or <see cref="SocialGroupType.None"/>.</summary>
    public SocialGroupType SocialGroup { get; }

    /// <summary>Optional additional statistic columns to return.</summary>
    public IReadOnlyList<string> AdditionalColumnLeaderboardNames { get; }

    /// <summary>The sort order.</summary>
    public LeaderboardSortOrder Order { get; }

    /// <summary>Maximum rows to return. 0 lets the service choose.</summary>
    public uint MaxItems { get; }

    /// <summary>Xbox user id to start at. 0 disables this skip.</summary>
    public ulong SkipToXboxUserId { get; }

    /// <summary>Rank to start at. 0 disables this skip.</summary>
    public uint SkipResultToRank { get; }

    /// <summary>Optional continuation token for a query resumed by the service.</summary>
    public string? ContinuationToken { get; }

    /// <summary>The leaderboard backing store to query.</summary>
    public LeaderboardQueryType QueryType { get; }

    internal string[] AdditionalColumnLeaderboardNamesArray => _additionalColumnLeaderboardNames;

    private static string[] ToArray(IEnumerable<string>? values)
    {
        if (values is null)
        {
            return Array.Empty<string>();
        }

        if (values is string[] array)
        {
            var copy = new string[array.Length];
            Array.Copy(array, copy, array.Length);
            ValidateNoNull(copy);
            return copy;
        }

        if (values is ICollection<string> collection)
        {
            var copy = new string[collection.Count];
            collection.CopyTo(copy, 0);
            ValidateNoNull(copy);
            return copy;
        }

        var list = new List<string>(values);
        string[] result = list.ToArray();
        ValidateNoNull(result);
        return result;
    }

    private static void ValidateNoNull(string[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] is null)
            {
                throw new ArgumentException("Additional column names cannot contain null.", nameof(values));
            }
        }
    }
}

/// <summary>A column returned in a leaderboard page. Managed snapshot of <c>XblLeaderboardColumn</c>.</summary>
public sealed class LeaderboardColumn
{
    internal LeaderboardColumn(string statisticName, LeaderboardStatType statisticType)
    {
        StatisticName = statisticName;
        StatisticType = statisticType;
    }

    /// <summary>The statistic displayed in the column.</summary>
    public string StatisticName { get; }

    /// <summary>The statistic's data type.</summary>
    public LeaderboardStatType StatisticType { get; }
}

/// <summary>One row returned in a leaderboard page. Managed snapshot of <c>XblLeaderboardRow</c>.</summary>
public sealed class LeaderboardRow
{
    internal LeaderboardRow(
        string gamertag,
        string modernGamertag,
        string modernGamertagSuffix,
        string uniqueModernGamertag,
        ulong xboxUserId,
        double percentile,
        uint rank,
        uint globalRank,
        IReadOnlyList<string> columnValues)
    {
        Gamertag = gamertag;
        ModernGamertag = modernGamertag;
        ModernGamertagSuffix = modernGamertagSuffix;
        UniqueModernGamertag = uniqueModernGamertag;
        XboxUserId = xboxUserId;
        Percentile = percentile;
        Rank = rank;
        GlobalRank = globalRank;
        ColumnValues = columnValues;
    }

    /// <summary>The player's classic gamertag.</summary>
    public string Gamertag { get; }

    /// <summary>The player's modern gamertag, without suffix. Not guaranteed unique.</summary>
    public string ModernGamertag { get; }

    /// <summary>The suffix that makes <see cref="ModernGamertag"/> unique. May be empty.</summary>
    public string ModernGamertagSuffix { get; }

    /// <summary>The unique modern gamertag, formatted <c>modernGamertag#suffix</c>.</summary>
    public string UniqueModernGamertag { get; }

    /// <summary>The player's Xbox user id.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The player's percentile rank.</summary>
    public double Percentile { get; }

    /// <summary>The player's rank in this result.</summary>
    public uint Rank { get; }

    /// <summary>The player's global rank, or 0 when the player has no global rank.</summary>
    public uint GlobalRank { get; }

    /// <summary>JSON values for this row, one value for each leaderboard column.</summary>
    public IReadOnlyList<string> ColumnValues { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{Rank}: {UniqueModernGamertag} ({XboxUserId})";

    internal static unsafe LeaderboardRow FromNative(XblLeaderboardRow* native)
    {
        var columnValues = new string[(int)native->ColumnValuesCount];
        for (int i = 0; i < columnValues.Length; i++)
        {
            columnValues[i] = Utf8.ToString(native->ColumnValues[i]) ?? string.Empty;
        }

        return new LeaderboardRow(
            Utf8.ToString(native->Gamertag, XblLeaderboardRow.GamertagCharSize),
            Utf8.ToString(native->ModernGamertag, XblLeaderboardRow.ModernGamertagCharSize),
            Utf8.ToString(native->ModernGamertagSuffix, XblLeaderboardRow.ModernGamertagSuffixCharSize),
            Utf8.ToString(native->UniqueModernGamertag, XblLeaderboardRow.UniqueModernGamertagCharSize),
            native->XboxUserId,
            native->Percentile,
            native->Rank,
            native->GlobalRank,
            new ReadOnlyCollection<string>(columnValues));
    }
}
