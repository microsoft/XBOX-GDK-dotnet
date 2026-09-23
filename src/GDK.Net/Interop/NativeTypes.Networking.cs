// Raw interop types for XNetworking.h (GDK edition 260404).
//
// Enums use the same values as the C++ originals. Structs use [StructLayout(LayoutKind.Sequential)]
// so the C# runtime inserts the same natural-alignment padding the C++ compiler would; layout is
// verified by NetworkingContractTests.
//
// Sources: XNetworking.h.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XNetworkingThumbprintType</c> from XNetworking.h.</summary>
internal enum XNetworkingThumbprintType : uint
{
    Leaf = 0,
    Issuer = 1,
    Root = 2,
}

/// <summary>Mirrors <c>struct XNetworkingThumbprint</c> from XNetworking.h.</summary>
/// <remarks>
/// On x64: thumbprintType (4) + implicit padding (4) + thumbprintBufferByteCount (8) +
/// thumbprintBuffer (8) = 24 bytes. <see cref="LayoutKind.Sequential"/> with the default packing
/// produces exactly that layout.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XNetworkingThumbprint
{
    public XNetworkingThumbprintType thumbprintType;
    public nuint thumbprintBufferByteCount;
    public byte* thumbprintBuffer;
}

/// <summary>Mirrors <c>struct XNetworkingSecurityInformation</c> from XNetworking.h.</summary>
/// <remarks>
/// On x64: enabledHttpSecurityProtocolFlags (4) + implicit padding (4) + thumbprintCount (8) +
/// thumbprints (8) = 24 bytes.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XNetworkingSecurityInformation
{
    public uint enabledHttpSecurityProtocolFlags;
    public nuint thumbprintCount;
    public XNetworkingThumbprint* thumbprints;
}

/// <summary>Mirrors <c>XNetworkingConnectivityLevelHint</c> from XNetworking.h.</summary>
internal enum XNetworkingConnectivityLevelHint : uint
{
    Unknown = 0,
    None = 1,
    LocalAccess = 2,
    InternetAccess = 3,
    ConstrainedInternetAccess = 4,
}

/// <summary>Mirrors <c>XNetworkingConnectivityCostHint</c> from XNetworking.h.</summary>
internal enum XNetworkingConnectivityCostHint : uint
{
    Unknown = 0,
    Unrestricted = 1,
    Fixed = 2,
    Variable = 3,
}

/// <summary>Mirrors <c>struct XNetworkingConnectivityHint</c> from XNetworking.h.</summary>
/// <remarks>
/// C++ <c>bool</c> is 1 byte on Windows. The four bool fields are declared as <see cref="byte"/>
/// per the interop conventions (bool is not blittable in netstandard2.0 P/Invoke signatures).
/// Layout: 3×uint32 (12) + 4×byte (4) = 16 bytes; no trailing padding required.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct XNetworkingConnectivityHint
{
    public XNetworkingConnectivityLevelHint connectivityLevel;
    public XNetworkingConnectivityCostHint connectivityCost;
    public uint ianaInterfaceType;
    public byte networkInitialized;
    public byte approachingDataLimit;
    public byte overDataLimit;
    public byte roaming;
}

/// <summary>Mirrors <c>XNetworkingConfigurationSetting</c> from XNetworking.h.</summary>
internal enum XNetworkingConfigurationSetting : uint
{
    MaxTitleTcpQueuedReceiveBufferSize = 0,
    MaxSystemTcpQueuedReceiveBufferSize = 1,
    MaxToolsTcpQueuedReceiveBufferSize = 2,
}

/// <summary>Mirrors <c>XNetworkingStatisticsType</c> from XNetworking.h.</summary>
internal enum XNetworkingStatisticsType : uint
{
    TitleTcpQueuedReceivedBufferUsage = 0,
    SystemTcpQueuedReceivedBufferUsage = 1,
    ToolsTcpQueuedReceivedBufferUsage = 2,
}

/// <summary>Mirrors <c>struct XNetworkingTcpQueuedReceivedBufferUsageStatistics</c> from XNetworking.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XNetworkingTcpQueuedReceivedBufferUsageStatistics
{
    public ulong numBytesCurrentlyQueued;
    public ulong peakNumBytesEverQueued;
    public ulong totalNumBytesQueued;
    public ulong numBytesDroppedForExceedingConfiguredMax;
    public ulong numBytesDroppedDueToAnyFailure;
}

/// <summary>Mirrors <c>union XNetworkingStatisticsBuffer</c> from XNetworking.h.</summary>
/// <remarks>
/// The union currently contains a single member. <see cref="LayoutKind.Explicit"/> is used to
/// match the native union layout; all members start at offset 0.
/// </remarks>
[StructLayout(LayoutKind.Explicit)]
internal struct XNetworkingStatisticsBuffer
{
    [System.Runtime.InteropServices.FieldOffset(0)]
    public XNetworkingTcpQueuedReceivedBufferUsageStatistics tcpQueuedReceiveBufferUsage;
}
