# <a id="GDK_Net_Networking_NetworkingManager"></a> Class NetworkingManager

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

Networking connectivity, UDP port negotiation, TLS certificate pinning, configuration and
statistics. Reached through <xref href="GDK.Net.GameRuntime.Networking" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class NetworkingManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NetworkingManager](GDK.Net.Networking.NetworkingManager.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Each native event registration is created lazily on the first subscription to the corresponding
event and released on <xref href="GDK.Net.Networking.NetworkingManager.Dispose" data-throw-if-not-resolved="false"></xref> with <code>wait: true</code>, so no callback can be in
flight once the manager is gone.
</p>

## Methods

### <a id="GDK_Net_Networking_NetworkingManager_Dispose"></a> Dispose\(\)

Releases all native registrations.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Networking_NetworkingManager_GetConnectivityHint"></a> GetConnectivityHint\(\)

Returns the current connectivity hint synchronously
(<code>XNetworkingGetConnectivityHint</code>).

```csharp
public NetworkingConnectivityHint GetConnectivityHint()
```

#### Returns

 [NetworkingConnectivityHint](GDK.Net.Networking.NetworkingConnectivityHint.md)

### <a id="GDK_Net_Networking_NetworkingManager_QueryConfigurationSetting_GDK_Net_Networking_NetworkingConfigurationSetting_"></a> QueryConfigurationSetting\(NetworkingConfigurationSetting\)

Queries a networking configuration setting
(<code>XNetworkingQueryConfigurationSetting</code>).

```csharp
public ulong QueryConfigurationSetting(NetworkingConfigurationSetting setting)
```

#### Parameters

`setting` [NetworkingConfigurationSetting](GDK.Net.Networking.NetworkingConfigurationSetting.md)

Which setting to read.

#### Returns

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The current value of the setting in bytes.

### <a id="GDK_Net_Networking_NetworkingManager_QueryPreferredLocalUdpMultiplayerPort"></a> QueryPreferredLocalUdpMultiplayerPort\(\)

Queries the current preferred local UDP port for multiplayer synchronously
(<code>XNetworkingQueryPreferredLocalUdpMultiplayerPort</code>).

```csharp
public ushort QueryPreferredLocalUdpMultiplayerPort()
```

#### Returns

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_Networking_NetworkingManager_QueryPreferredLocalUdpMultiplayerPortAsync_System_Threading_CancellationToken_"></a> QueryPreferredLocalUdpMultiplayerPortAsync\(CancellationToken\)

Asynchronously queries the preferred local UDP port for multiplayer
(<code>XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync</code> /
<code>XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult</code>).

```csharp
public Task<ushort> QueryPreferredLocalUdpMultiplayerPortAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ushort](https://learn.microsoft.com/dotnet/api/system.uint16)\>

### <a id="GDK_Net_Networking_NetworkingManager_QuerySecurityInformationForUrlAsync_System_String_System_Threading_CancellationToken_"></a> QuerySecurityInformationForUrlAsync\(string, CancellationToken\)

Asynchronously retrieves TLS security information for the given URL
(<code>XNetworkingQuerySecurityInformationForUrlAsync</code>). The returned
<xref href="GDK.Net.Networking.NetworkingSecurityInformation" data-throw-if-not-resolved="false"></xref> must be disposed after use.

```csharp
public Task<NetworkingSecurityInformation> QuerySecurityInformationForUrlAsync(string url, CancellationToken cancellationToken = default)
```

#### Parameters

`url` [string](https://learn.microsoft.com/dotnet/api/system.string)

The UTF-8 URL to query (e.g. <code>"https://contoso.com/"</code>).

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[NetworkingSecurityInformation](GDK.Net.Networking.NetworkingSecurityInformation.md)\>

### <a id="GDK_Net_Networking_NetworkingManager_QuerySecurityInformationForUrlUtf16Async_System_String_System_Threading_CancellationToken_"></a> QuerySecurityInformationForUrlUtf16Async\(string, CancellationToken\)

Asynchronously retrieves TLS security information for the given URL, passing it to the
Gaming Runtime as UTF-16 rather than UTF-8
(<code>XNetworkingQuerySecurityInformationForUrlUtf16Async</code>). The returned
<xref href="GDK.Net.Networking.NetworkingSecurityInformation" data-throw-if-not-resolved="false"></xref> must be disposed after use.

```csharp
public Task<NetworkingSecurityInformation> QuerySecurityInformationForUrlUtf16Async(string url, CancellationToken cancellationToken = default)
```

#### Parameters

`url` [string](https://learn.microsoft.com/dotnet/api/system.string)

The URL to query (e.g. <code>"https://contoso.com/"</code>).

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[NetworkingSecurityInformation](GDK.Net.Networking.NetworkingSecurityInformation.md)\>

#### Remarks

Functionally identical to <xref href="GDK.Net.Networking.NetworkingManager.QuerySecurityInformationForUrlAsync(System.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>; it exists
because the GDK publishes both encodings and titles that already hold UTF-16 strings avoid a
transcode. Prefer the UTF-8 overload when the encoding is not already decided.

### <a id="GDK_Net_Networking_NetworkingManager_QueryTcpStatistics_GDK_Net_Networking_NetworkingStatisticsType_"></a> QueryTcpStatistics\(NetworkingStatisticsType\)

Queries TCP receive-buffer statistics for the specified partition
(<code>XNetworkingQueryStatistics</code>).

```csharp
public NetworkingTcpStatistics QueryTcpStatistics(NetworkingStatisticsType statisticsType)
```

#### Parameters

`statisticsType` [NetworkingStatisticsType](GDK.Net.Networking.NetworkingStatisticsType.md)

Which partition's statistics to retrieve.

#### Returns

 [NetworkingTcpStatistics](GDK.Net.Networking.NetworkingTcpStatistics.md)

A snapshot of the TCP queued receive buffer usage counters.

### <a id="GDK_Net_Networking_NetworkingManager_VerifyServerCertificate_System_IntPtr_GDK_Net_Networking_NetworkingSecurityInformation_"></a> VerifyServerCertificate\(nint, NetworkingSecurityInformation\)

Verifies that the server certificate presented during a WinHTTP request matches the
pre-queried security information (<code>XNetworkingVerifyServerCertificate</code>).

```csharp
public void VerifyServerCertificate(nint requestHandle, NetworkingSecurityInformation securityInformation)
```

#### Parameters

`requestHandle` [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

The WinHTTP request handle (from <code>WinHttpOpenRequest</code>).

`securityInformation` [NetworkingSecurityInformation](GDK.Net.Networking.NetworkingSecurityInformation.md)

Security information retrieved via <xref href="GDK.Net.Networking.NetworkingManager.QuerySecurityInformationForUrlAsync(System.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Networking_NetworkingManager_ConnectivityHintChanged"></a> ConnectivityHintChanged

Raised when the device's connectivity hint changes
(<code>XNetworkingRegisterConnectivityHintChanged</code>).

```csharp
public event EventHandler<ConnectivityHintChangedEventArgs>? ConnectivityHintChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[ConnectivityHintChangedEventArgs](GDK.Net.Networking.ConnectivityHintChangedEventArgs.md)\>?

### <a id="GDK_Net_Networking_NetworkingManager_PreferredLocalUdpMultiplayerPortChanged"></a> PreferredLocalUdpMultiplayerPortChanged

Raised when the preferred local UDP multiplayer port changes
(<code>XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged</code>).

```csharp
public event EventHandler<PreferredLocalUdpMultiplayerPortChangedEventArgs>? PreferredLocalUdpMultiplayerPortChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[PreferredLocalUdpMultiplayerPortChangedEventArgs](GDK.Net.Networking.PreferredLocalUdpMultiplayerPortChangedEventArgs.md)\>?

#### Remarks

Delivered on the manager's task queue. When the manager names no queue the Gaming Runtime
resolves the process default, so handlers arrive on the thread pool; a manager constructed
over a manual queue delivers them on whichever thread pumps that queue.

