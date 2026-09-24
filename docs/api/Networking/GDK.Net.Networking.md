# <a id="GDK_Net_Networking"></a> Namespace GDK.Net.Networking

### Classes

 [ConnectivityHintChangedEventArgs](GDK.Net.Networking.ConnectivityHintChangedEventArgs.md)

Payload for <xref href="GDK.Net.Networking.NetworkingManager.ConnectivityHintChanged" data-throw-if-not-resolved="false"></xref>.

 [NetworkingManager](GDK.Net.Networking.NetworkingManager.md)

Networking connectivity, UDP port negotiation, TLS certificate pinning, configuration and
statistics. Reached through <xref href="GDK.Net.GameRuntime.Networking" data-throw-if-not-resolved="false"></xref>.

 [NetworkingSecurityInformation](GDK.Net.Networking.NetworkingSecurityInformation.md)

TLS security information for a URL, used with <xref href="GDK.Net.Networking.NetworkingManager.VerifyServerCertificate(System.IntPtr%2cGDK.Net.Networking.NetworkingSecurityInformation)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XNetworkingSecurityInformation</code> from XNetworking.h.

 [NetworkingThumbprint](GDK.Net.Networking.NetworkingThumbprint.md)

A certificate thumbprint for TLS certificate pinning.
Mirrors <code>XNetworkingThumbprint</code> from XNetworking.h.

 [PreferredLocalUdpMultiplayerPortChangedEventArgs](GDK.Net.Networking.PreferredLocalUdpMultiplayerPortChangedEventArgs.md)

Payload for <xref href="GDK.Net.Networking.NetworkingManager.PreferredLocalUdpMultiplayerPortChanged" data-throw-if-not-resolved="false"></xref>.

### Structs

 [NetworkingConnectivityHint](GDK.Net.Networking.NetworkingConnectivityHint.md)

A snapshot of the device's network connectivity state.
Mirrors <code>XNetworkingConnectivityHint</code> from XNetworking.h.

 [NetworkingTcpStatistics](GDK.Net.Networking.NetworkingTcpStatistics.md)

TCP receive-buffer usage statistics. Mirrors <code>XNetworkingTcpQueuedReceivedBufferUsageStatistics</code>.

### Enums

 [NetworkingConfigurationSetting](GDK.Net.Networking.NetworkingConfigurationSetting.md)

Mirrors <code>XNetworkingConfigurationSetting</code> from XNetworking.h.
Identifies a per-partition TCP receive-buffer configuration value.

 [NetworkingConnectivityCostHint](GDK.Net.Networking.NetworkingConnectivityCostHint.md)

Mirrors <code>XNetworkingConnectivityCostHint</code> from XNetworking.h.
Describes the monetary or data cost of the active connection.

 [NetworkingConnectivityLevelHint](GDK.Net.Networking.NetworkingConnectivityLevelHint.md)

Mirrors <code>XNetworkingConnectivityLevelHint</code> from XNetworking.h.
Describes the internet connectivity level observed by the device.

 [NetworkingStatisticsType](GDK.Net.Networking.NetworkingStatisticsType.md)

Mirrors <code>XNetworkingStatisticsType</code> from XNetworking.h.
Identifies a statistics counter set to query.

 [NetworkingThumbprintType](GDK.Net.Networking.NetworkingThumbprintType.md)

Mirrors <code>XNetworkingThumbprintType</code> from XNetworking.h.

