using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>One Xbox Live user statistic. Managed snapshot of <c>XblStatistic</c>.</summary>
public sealed class Statistic
{
    internal Statistic(string name, string type, string value)
    {
        Name = name;
        Type = type;
        Value = value;
    }

    /// <summary>The statistic's name.</summary>
    public string Name { get; }

    /// <summary>The service-reported statistic type.</summary>
    public string Type { get; }

    /// <summary>The statistic's value, as formatted by Xbox Live.</summary>
    public string Value { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{Name}={Value}";

    internal static unsafe Statistic FromNative(XblStatistic* native)
    {
        return new Statistic(
            Utf8.ToString(native->StatisticName) ?? string.Empty,
            Utf8.ToString(native->StatisticType) ?? string.Empty,
            Utf8.ToString(native->Value) ?? string.Empty);
    }
}

/// <summary>
/// The statistics returned for a single service configuration. Managed snapshot of
/// <c>XblServiceConfigurationStatistic</c>.
/// </summary>
public sealed class ServiceConfigurationStatistic
{
    internal ServiceConfigurationStatistic(string serviceConfigurationId, IReadOnlyList<Statistic> statistics)
    {
        ServiceConfigurationId = serviceConfigurationId;
        Statistics = statistics;
    }

    /// <summary>The service configuration id (SCID) the statistics came from.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>The statistics Xbox Live returned for this service configuration.</summary>
    public IReadOnlyList<Statistic> Statistics { get; }

    internal static unsafe ServiceConfigurationStatistic FromNative(XblServiceConfigurationStatistic* native)
    {
        string serviceConfigurationId;
        byte* scid = native->ServiceConfigurationId;
        serviceConfigurationId = Utf8.ToString(scid, XblServiceConfigurationStatistic.ServiceConfigurationIdLength);

        var statistics = new Statistic[(int)native->StatisticsCount];
        for (int i = 0; i < statistics.Length; i++)
        {
            statistics[i] = Statistic.FromNative(native->Statistics + i);
        }

        return new ServiceConfigurationStatistic(
            serviceConfigurationId,
            new ReadOnlyCollection<Statistic>(statistics));
    }
}

/// <summary>Statistics returned for one Xbox user. Managed snapshot of <c>XblUserStatisticsResult</c>.</summary>
public sealed class UserStatisticsResult
{
    internal UserStatisticsResult(ulong xboxUserId, IReadOnlyList<ServiceConfigurationStatistic> serviceConfigurations)
    {
        XboxUserId = xboxUserId;
        ServiceConfigurations = serviceConfigurations;
    }

    /// <summary>The Xbox user id the statistics describe.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The service configurations and statistics Xbox Live returned for the user.</summary>
    public IReadOnlyList<ServiceConfigurationStatistic> ServiceConfigurations { get; }

    internal static unsafe UserStatisticsResult FromNative(XblUserStatisticsResult* native)
    {
        var serviceConfigurations = new ServiceConfigurationStatistic[(int)native->ServiceConfigStatisticsCount];
        for (int i = 0; i < serviceConfigurations.Length; i++)
        {
            serviceConfigurations[i] = ServiceConfigurationStatistic.FromNative(native->ServiceConfigStatistics + i);
        }

        return new UserStatisticsResult(
            native->XboxUserId,
            new ReadOnlyCollection<ServiceConfigurationStatistic>(serviceConfigurations));
    }
}

/// <summary>
/// Statistics requested for one service configuration in a multi-SCID batch. Mirrors
/// <c>XblRequestedStatistics</c> without exposing native buffers.
/// </summary>
public sealed class RequestedStatistics
{
    /// <summary>Creates a request for a service configuration and its statistic names.</summary>
    /// <param name="serviceConfigurationId">The service configuration id (SCID) to query.</param>
    /// <param name="statisticNames">Statistic names to query under the service configuration.</param>
    public RequestedStatistics(string serviceConfigurationId, IEnumerable<string> statisticNames)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        if (statisticNames is null)
        {
            throw new ArgumentNullException(nameof(statisticNames));
        }

        ServiceConfigurationId = serviceConfigurationId;
        StatisticNames = new ReadOnlyCollection<string>(SnapshotStatisticNames(statisticNames));
    }

    /// <summary>The service configuration id (SCID) to query.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>The statistic names to query under <see cref="ServiceConfigurationId"/>.</summary>
    public IReadOnlyList<string> StatisticNames { get; }

    private static string[] SnapshotStatisticNames(IEnumerable<string> statisticNames)
    {
        string[] names = UserStatisticsService.ToArray(statisticNames);
        for (int i = 0; i < names.Length; i++)
        {
            UserStatisticsService.ThrowIfMissingStatisticName(names[i], nameof(statisticNames));
        }

        return names;
    }
}

/// <summary>
/// Payload for <see cref="UserStatisticsService.StatisticChanged"/>. Managed snapshot of
/// <c>XblStatisticChangeEventArgs</c>.
/// </summary>
public sealed class StatisticChangedEventArgs : EventArgs
{
    internal StatisticChangedEventArgs(
        ulong xboxUserId,
        string serviceConfigurationId,
        Statistic latestStatistic)
    {
        XboxUserId = xboxUserId;
        ServiceConfigurationId = serviceConfigurationId;
        LatestStatistic = latestStatistic;
    }

    /// <summary>The Xbox user id whose statistic changed.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The service configuration id (SCID) the statistic belongs to.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>The latest statistic value reported by Xbox Live.</summary>
    public Statistic LatestStatistic { get; }

    internal static unsafe StatisticChangedEventArgs FromNative(XblStatisticChangeEventArgs native)
    {
        byte* scid = native.ServiceConfigurationId;
        string serviceConfigurationId = Utf8.ToString(
            scid,
            XblStatisticChangeEventArgs.ServiceConfigurationIdLength);
        XblStatistic statistic = native.LatestStatistic;

        return new StatisticChangedEventArgs(
            native.XboxUserId,
            serviceConfigurationId,
            Statistic.FromNative(&statistic));
    }
}
