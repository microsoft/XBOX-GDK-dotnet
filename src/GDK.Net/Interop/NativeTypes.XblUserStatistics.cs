// Blittable mirrors of the XSAPI user statistics types -- xsapi-c\user_statistics_c.h,
// GDK edition 260404.
//
// Result buffers returned by the user statistics APIs contain internal pointers into the caller's
// byte buffer. The public projection snapshots every pointer graph before freeing that buffer.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblStatistic</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblStatistic
{
    internal byte* StatisticName;
    internal byte* StatisticType;
    internal byte* Value;
}

/// <summary>Mirrors <c>XblServiceConfigurationStatistic</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblServiceConfigurationStatistic
{
    internal const int ServiceConfigurationIdLength = 40;

    internal fixed byte ServiceConfigurationId[ServiceConfigurationIdLength];
    internal XblStatistic* Statistics;
    internal uint StatisticsCount;
}

/// <summary>Mirrors <c>XblUserStatisticsResult</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblUserStatisticsResult
{
    internal ulong XboxUserId;
    internal XblServiceConfigurationStatistic* ServiceConfigStatistics;
    internal uint ServiceConfigStatisticsCount;
}

/// <summary>Mirrors <c>XblRequestedStatistics</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblRequestedStatistics
{
    internal const int ServiceConfigurationIdLength = 40;

    internal fixed byte ServiceConfigurationId[ServiceConfigurationIdLength];
    internal byte** Statistics;
    internal uint StatisticsCount;
}

/// <summary>Mirrors <c>XblStatisticChangeEventArgs</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblStatisticChangeEventArgs
{
    internal const int ServiceConfigurationIdLength = 40;

    internal ulong XboxUserId;
    internal fixed byte ServiceConfigurationId[ServiceConfigurationIdLength];
    internal XblStatistic LatestStatistic;
}
