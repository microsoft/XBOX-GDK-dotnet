# <a id="GDK_Net_Package_GamePackage"></a> Class GamePackage

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Package enumeration, identity queries, chunk management, write-stats and the
<xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref> event. All APIs require a packaged GDK process.

```csharp
public static class GamePackage
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GamePackage](GDK.Net.Package.GamePackage.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Most APIs in this class require the process to have a package identity.
Calling them from an unpackaged process fails with
<code>E_GAMEPACKAGE_APP_NOT_PACKAGED</code>. Use <xref href="GDK.Net.Package.GamePackage.IsPackagedProcess" data-throw-if-not-resolved="false"></xref> to guard.

## Properties

### <a id="GDK_Net_Package_GamePackage_IsPackagedProcess"></a> IsPackagedProcess

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the current process has a package identity
(<code>XPackageIsPackagedProcess</code>). Safe to call from any process.

```csharp
public static bool IsPackagedProcess { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_Package_GamePackage_ChangeChunkInstallOrder_System_String_GDK_Net_Package_PackageChunkSelector___"></a> ChangeChunkInstallOrder\(string, PackageChunkSelector\[\]\)

Changes the priority order in which chunks are installed
(<code>XPackageChangeChunkInstallOrder</code>).

```csharp
public static void ChangeChunkInstallOrder(string packageIdentifier, PackageChunkSelector[] selectors)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]

### <a id="GDK_Net_Package_GamePackage_EnumerateChunkAvailability_System_String_GDK_Net_Package_PackageChunkSelectorType_"></a> EnumerateChunkAvailability\(string, PackageChunkSelectorType\)

Enumerates availability for all chunks of a given selector type
(<code>XPackageEnumerateChunkAvailability</code>).

```csharp
public static IReadOnlyList<PackageChunkAvailabilityInfo> EnumerateChunkAvailability(string packageIdentifier, PackageChunkSelectorType type)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier.

`type` [PackageChunkSelectorType](GDK.Net.Package.PackageChunkSelectorType.md)

The selector type to enumerate (Language, Tag, Chunk or Feature).

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PackageChunkAvailabilityInfo](GDK.Net.Package.PackageChunkAvailabilityInfo.md)\>

### <a id="GDK_Net_Package_GamePackage_EnumerateFeatures_System_String_"></a> EnumerateFeatures\(string\)

Enumerates the feature set defined in the specified package's game config
(<code>XPackageEnumerateFeatures</code>).

```csharp
public static IReadOnlyList<PackageFeature> EnumerateFeatures(string packageIdentifier)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier. Pass the value from
<xref href="GDK.Net.Package.GamePackage.GetCurrentPackageIdentifier" data-throw-if-not-resolved="false"></xref> to query the running title.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PackageFeature](GDK.Net.Package.PackageFeature.md)\>

### <a id="GDK_Net_Package_GamePackage_EnumeratePackages_GDK_Net_Package_PackageKind_GDK_Net_Package_PackageEnumerationScope_"></a> EnumeratePackages\(PackageKind, PackageEnumerationScope\)

Enumerates installed packages visible to the current title
(<code>XPackageEnumeratePackages</code>).

```csharp
public static IReadOnlyList<PackageInfo> EnumeratePackages(PackageKind kind = PackageKind.Game, PackageEnumerationScope scope = PackageEnumerationScope.ThisAndRelated)
```

#### Parameters

`kind` [PackageKind](GDK.Net.Package.PackageKind.md)

Filter by package kind.

`scope` [PackageEnumerationScope](GDK.Net.Package.PackageEnumerationScope.md)

How broadly to search relative to the current title.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PackageInfo](GDK.Net.Package.PackageInfo.md)\>

A snapshot list of matching packages.

### <a id="GDK_Net_Package_GamePackage_EstimateDownloadSize_System_String_GDK_Net_Package_PackageChunkSelector___System_Boolean__"></a> EstimateDownloadSize\(string, PackageChunkSelector\[\], out bool\)

Estimates the download size required to install the given chunks
(<code>XPackageEstimateDownloadSize</code>).

```csharp
public static ulong EstimateDownloadSize(string packageIdentifier, PackageChunkSelector[] selectors, out bool shouldPromptUser)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier.

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]

The chunks to estimate for.

`shouldPromptUser` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Set to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the platform recommends showing a confirmation dialog
before downloading.

#### Returns

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Estimated download size in bytes.

### <a id="GDK_Net_Package_GamePackage_FindChunkAvailability_System_String_GDK_Net_Package_PackageChunkSelector___"></a> FindChunkAvailability\(string, PackageChunkSelector\[\]\)

Returns the aggregate availability for a specific set of chunk selectors
(<code>XPackageFindChunkAvailability</code>).

```csharp
public static PackageChunkAvailability FindChunkAvailability(string packageIdentifier, PackageChunkSelector[] selectors)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier.

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]

The chunk selectors to query.

#### Returns

 [PackageChunkAvailability](GDK.Net.Package.PackageChunkAvailability.md)

### <a id="GDK_Net_Package_GamePackage_GetCurrentPackageIdentifier"></a> GetCurrentPackageIdentifier\(\)

Returns the current process's package identifier
(<code>XPackageGetCurrentProcessPackageIdentifier</code>).

```csharp
public static string GetCurrentPackageIdentifier()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

The identifier is a durable, opaque, null-terminated string up to
<code>XPACKAGE_IDENTIFIER_MAX_LENGTH</code> (33) characters. It is stable across updates.
Requires a packaged process.

### <a id="GDK_Net_Package_GamePackage_GetPackageKind_System_String_"></a> GetPackageKind\(string\)

Returns the kind of an installed package (<code>XPackageGetPackageKind</code>).

```csharp
public static PackageKind GetPackageKind(string packageIdentifier)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier of the package to classify.

#### Returns

 [PackageKind](GDK.Net.Package.PackageKind.md)

#### Remarks

Added in GDK edition 260404. <xref href="GDK.Net.Package.PackageKind.PublisherContent" data-throw-if-not-resolved="false"></xref> identifies content
shared across a publisher's titles rather than owned by a single game.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">packageIdentifier</code> is null.

### <a id="GDK_Net_Package_GamePackage_GetUserLocale"></a> GetUserLocale\(\)

Returns the user locale configured for the current package
(<code>XPackageGetUserLocale</code>), e.g. <code>"en-US"</code>.

```csharp
public static string GetUserLocale()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Requires a packaged process.

### <a id="GDK_Net_Package_GamePackage_GetWriteStats"></a> GetWriteStats\(\)

Returns current write-budget statistics for the packaged process
(<code>XPackageGetWriteStats</code>).

```csharp
public static PackageWriteStats GetWriteStats()
```

#### Returns

 [PackageWriteStats](GDK.Net.Package.PackageWriteStats.md)

### <a id="GDK_Net_Package_GamePackage_InstallChunks_System_String_GDK_Net_Package_PackageChunkSelector___System_UInt32_System_Boolean_"></a> InstallChunks\(string, PackageChunkSelector\[\], uint, bool\)

Synchronously starts a chunk installation and returns a monitor
(<code>XPackageInstallChunks</code>).

```csharp
public static PackageInstallationMonitor InstallChunks(string packageIdentifier, PackageChunkSelector[] selectors, uint minimumUpdateIntervalMs = 0, bool suppressUserConfirmation = false)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]

`minimumUpdateIntervalMs` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`suppressUserConfirmation` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [PackageInstallationMonitor](GDK.Net.Package.PackageInstallationMonitor.md)

### <a id="GDK_Net_Package_GamePackage_InstallChunksAsync_System_String_GDK_Net_Package_PackageChunkSelector___System_UInt32_System_Boolean_System_Threading_CancellationToken_"></a> InstallChunksAsync\(string, PackageChunkSelector\[\], uint, bool, CancellationToken\)

Starts an asynchronous chunk installation and returns a monitor for tracking progress
(<code>XPackageInstallChunksAsync</code> / <code>XPackageInstallChunksResult</code>).

```csharp
public static Task<PackageInstallationMonitor> InstallChunksAsync(string packageIdentifier, PackageChunkSelector[] selectors, uint minimumUpdateIntervalMs = 0, bool suppressUserConfirmation = false, CancellationToken cancellationToken = default)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

The opaque package identifier.

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]

Chunks to install.

`minimumUpdateIntervalMs` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Minimum milliseconds between progress-changed callbacks; 0 means no throttling.

`suppressUserConfirmation` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> to suppress the system download confirmation UI.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PackageInstallationMonitor](GDK.Net.Package.PackageInstallationMonitor.md)\>

A monitor that tracks the installation. Dispose when no longer needed.

### <a id="GDK_Net_Package_GamePackage_UninstallChunks_System_String_GDK_Net_Package_PackageChunkSelector___"></a> UninstallChunks\(string, PackageChunkSelector\[\]\)

Uninstalls previously installed chunks (<code>XPackageUninstallChunks</code>).

```csharp
public static void UninstallChunks(string packageIdentifier, PackageChunkSelector[] selectors)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

`selectors` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)\[\]

### <a id="GDK_Net_Package_GamePackage_UninstallPackage_System_String_"></a> UninstallPackage\(string\)

Uninstalls the package identified by <code class="paramref">packageIdentifier</code>
(<code>XPackageUninstallPackage</code>).

```csharp
public static bool UninstallPackage(string packageIdentifier)
```

#### Parameters

`packageIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the uninstall was queued successfully.

### <a id="GDK_Net_Package_GamePackage_UninstallUwpInstance_System_String_"></a> UninstallUwpInstance\(string\)

Uninstalls a UWP app by package family name
(<code>XPackageUninstallUWPInstance</code>).

```csharp
public static void UninstallUwpInstance(string packageName)
```

#### Parameters

`packageName` [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Package_GamePackage_UnregisterPackageInstalled"></a> UnregisterPackageInstalled\(\)

Tears down the native registration behind <xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref> and drops every
subscriber (<code>XPackageUnregisterPackageInstalled</code>).

```csharp
public static void UnregisterPackageInstalled()
```

#### Remarks

<p>
<xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref> is a static event with no owning instance, so the registration
otherwise lives for the lifetime of the process. Call this when the title is done listening —
typically during shutdown, before <code>XGameRuntimeUninitialize</code>. Subscribing again after
this call re-registers.
</p>
<p>
This waits for any in-flight callback to finish, so it must <b>not</b> be called from inside
a <xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref> handler.
</p>

### <a id="GDK_Net_Package_GamePackage_PackageInstalled"></a> PackageInstalled

Raised when a new package is installed while the title is running
(<code>XPackageRegisterPackageInstalled</code>).

```csharp
public static event EventHandler<PackageInstalledEventArgs>? PackageInstalled
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[PackageInstalledEventArgs](GDK.Net.Package.PackageInstalledEventArgs.md)\>?

#### Remarks

Registration with the native runtime is deferred until the first subscriber is added.
If the process is not packaged the registration call fails and the exception is propagated
to the subscribing code.

