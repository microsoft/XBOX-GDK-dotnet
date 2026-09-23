// P/Invoke declarations for the XNetworking family.
//
// Verified against xgameruntime.thunks.dll (edition 260404) with dumpbin /exports.
// All 17 XNetworking* functions listed below are exported. One function declared in the header is
// NOT exported and is therefore not declared here:
//
//   XNetworkingSetConfigurationSetting — absent from xgameruntime.thunks.dll. Binding it would
//   throw EntryPointNotFoundException at runtime. This function appears in the authoritative list
//   of 14 APIs that exist in xgameruntime.lib but are NOT re-exported by the thunks DLL. Every
//   P/Invoke in this file was cross-checked against that list before being committed.
//
// See Interop/Native.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XNetworking.h: preferred UDP port ---

    /// <summary>Synchronously queries the preferred local UDP multiplayer port (<c>XNetworkingQueryPreferredLocalUdpMultiplayerPort</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQueryPreferredLocalUdpMultiplayerPort(
        ushort* preferredLocalUdpMultiplayerPort);

    /// <summary>Starts an async query for the preferred local UDP multiplayer port (<c>XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(
        XAsyncBlock* asyncBlock);

    /// <summary>Retrieves the result of <c>XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync</c>.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(
        XAsyncBlock* asyncBlock,
        ushort* preferredLocalUdpMultiplayerPort);

    /// <summary>Registers for preferred-UDP-port change notifications (<c>XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    /// <summary>Unregisters from preferred-UDP-port change notifications (<c>XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial byte XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XNetworking.h: security information (UTF-8 URL) ---

    /// <summary>Starts an async query for TLS security information for a URL (<c>XNetworkingQuerySecurityInformationForUrlAsync</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQuerySecurityInformationForUrlAsync(
        byte* url,
        XAsyncBlock* asyncBlock);

    /// <summary>Retrieves the buffer size required by <c>XNetworkingQuerySecurityInformationForUrlAsyncResult</c>.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQuerySecurityInformationForUrlAsyncResultSize(
        XAsyncBlock* asyncBlock,
        nuint* securityInformationBufferByteCount);

    /// <summary>Retrieves the result of <c>XNetworkingQuerySecurityInformationForUrlAsync</c>.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQuerySecurityInformationForUrlAsyncResult(
        XAsyncBlock* asyncBlock,
        nuint securityInformationBufferByteCount,
        nuint* securityInformationBufferByteCountUsed,
        byte* securityInformationBuffer,
        XNetworkingSecurityInformation** securityInformation);

    // --- XNetworking.h: security information (UTF-16 URL) ---

    /// <summary>Starts an async query for TLS security information for a UTF-16 URL (<c>XNetworkingQuerySecurityInformationForUrlUtf16Async</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQuerySecurityInformationForUrlUtf16Async(
        char* url,
        XAsyncBlock* asyncBlock);

    /// <summary>Retrieves the buffer size required by <c>XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult</c>.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(
        XAsyncBlock* asyncBlock,
        nuint* securityInformationBufferByteCount);

    /// <summary>Retrieves the result of <c>XNetworkingQuerySecurityInformationForUrlUtf16Async</c>.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(
        XAsyncBlock* asyncBlock,
        nuint securityInformationBufferByteCount,
        nuint* securityInformationBufferByteCountUsed,
        byte* securityInformationBuffer,
        XNetworkingSecurityInformation** securityInformation);

    // --- XNetworking.h: certificate verification ---

    /// <summary>Verifies a server certificate against pre-queried security information (<c>XNetworkingVerifyServerCertificate</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingVerifyServerCertificate(
        IntPtr requestHandle,
        XNetworkingSecurityInformation* securityInformation);

    // --- XNetworking.h: connectivity hint ---

    /// <summary>Synchronously retrieves the current connectivity hint (<c>XNetworkingGetConnectivityHint</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingGetConnectivityHint(
        XNetworkingConnectivityHint* connectivityHint);

    /// <summary>Registers for connectivity-hint change notifications (<c>XNetworkingRegisterConnectivityHintChanged</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingRegisterConnectivityHintChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    /// <summary>Unregisters from connectivity-hint change notifications (<c>XNetworkingUnregisterConnectivityHintChanged</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial byte XNetworkingUnregisterConnectivityHintChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XNetworking.h: configuration setting ---

    /// <summary>Queries a networking configuration setting (<c>XNetworkingQueryConfigurationSetting</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQueryConfigurationSetting(
        XNetworkingConfigurationSetting configurationSetting,
        ulong* value);

    // --- XNetworking.h: statistics ---

    /// <summary>Queries networking statistics (<c>XNetworkingQueryStatistics</c>).</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XNetworkingQueryStatistics(
        XNetworkingStatisticsType statisticsType,
        XNetworkingStatisticsBuffer* statisticsBuffer);
}

#else

internal static unsafe partial class Native
{
    // --- XNetworking.h: preferred UDP port ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQueryPreferredLocalUdpMultiplayerPort(
        ushort* preferredLocalUdpMultiplayerPort);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(
        XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(
        XAsyncBlock* asyncBlock,
        ushort* preferredLocalUdpMultiplayerPort);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XNetworking.h: security information (UTF-8 URL) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQuerySecurityInformationForUrlAsync(
        byte* url,
        XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQuerySecurityInformationForUrlAsyncResultSize(
        XAsyncBlock* asyncBlock,
        nuint* securityInformationBufferByteCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQuerySecurityInformationForUrlAsyncResult(
        XAsyncBlock* asyncBlock,
        nuint securityInformationBufferByteCount,
        nuint* securityInformationBufferByteCountUsed,
        byte* securityInformationBuffer,
        XNetworkingSecurityInformation** securityInformation);

    // --- XNetworking.h: security information (UTF-16 URL) ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQuerySecurityInformationForUrlUtf16Async(
        char* url,
        XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(
        XAsyncBlock* asyncBlock,
        nuint* securityInformationBufferByteCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(
        XAsyncBlock* asyncBlock,
        nuint securityInformationBufferByteCount,
        nuint* securityInformationBufferByteCountUsed,
        byte* securityInformationBuffer,
        XNetworkingSecurityInformation** securityInformation);

    // --- XNetworking.h: certificate verification ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingVerifyServerCertificate(
        IntPtr requestHandle,
        XNetworkingSecurityInformation* securityInformation);

    // --- XNetworking.h: connectivity hint ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingGetConnectivityHint(
        XNetworkingConnectivityHint* connectivityHint);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingRegisterConnectivityHintChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XNetworkingUnregisterConnectivityHintChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XNetworking.h: configuration setting ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQueryConfigurationSetting(
        XNetworkingConfigurationSetting configurationSetting,
        ulong* value);

    // --- XNetworking.h: statistics ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XNetworkingQueryStatistics(
        XNetworkingStatisticsType statisticsType,
        XNetworkingStatisticsBuffer* statisticsBuffer);
}

#endif
