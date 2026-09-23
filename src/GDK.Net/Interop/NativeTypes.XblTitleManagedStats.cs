// Blittable mirrors of the XSAPI title-managed statistics types --
// xsapi-c\title_managed_statistics_c.h, GDK edition 260404.
//
// The GDK 260404 header models the value as a tagged struct with both payload fields present
// (not a C union): statisticType selects either numberValue or stringValue. The public projection
// exposes that as an immutable discriminated value instead of leaking the parallel fields.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblTitleManagedStatType</c>.</summary>
internal enum XblTitleManagedStatType : uint
{
    Number = 0,
    String = 1,
}

/// <summary>Mirrors <c>XblTitleManagedStatistic</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblTitleManagedStatistic
{
    internal byte* StatisticName;
    internal XblTitleManagedStatType StatisticType;
    internal double NumberValue;
    internal byte* StringValue;
}
