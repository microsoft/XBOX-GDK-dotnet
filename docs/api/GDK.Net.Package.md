# <a id="GDK_Net_Package"></a> Namespace GDK.Net.Package

### Classes

 [GamePackage](GDK.Net.Package.GamePackage.md)

Package enumeration, identity queries, chunk management, write-stats and the
<xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref> event. All APIs require a packaged GDK process.

 [PackageChunkAvailabilityInfo](GDK.Net.Package.PackageChunkAvailabilityInfo.md)

Availability and a chunk selector, as returned by
<xref href="GDK.Net.Package.GamePackage.EnumerateChunkAvailability(System.String%2cGDK.Net.Package.PackageChunkSelectorType)" data-throw-if-not-resolved="false"></xref>.

 [PackageFeature](GDK.Net.Package.PackageFeature.md)

A feature entry returned by <xref href="GDK.Net.Package.GamePackage.EnumerateFeatures(System.String)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XPackageFeature</code>.

 [PackageInfo](GDK.Net.Package.PackageInfo.md)

Information about an installed or available package.
Mirrors <code>XPackageDetails</code>.

 [PackageInstallationMonitor](GDK.Net.Package.PackageInstallationMonitor.md)

Tracks the installation progress of one or more package chunks.

 [PackageInstalledEventArgs](GDK.Net.Package.PackageInstalledEventArgs.md)

Event arguments for <xref href="GDK.Net.Package.GamePackage.PackageInstalled" data-throw-if-not-resolved="false"></xref>.

 [PackageMount](GDK.Net.Package.PackageMount.md)

A mounted package. Owns an <code>XPackageMountHandle</code> and exposes the mount path.

 [PackageProgressChangedEventArgs](GDK.Net.Package.PackageProgressChangedEventArgs.md)

Event arguments for <xref href="GDK.Net.Package.PackageInstallationMonitor.ProgressChanged" data-throw-if-not-resolved="false"></xref>.

### Structs

 [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

A selector that identifies one or more chunks within a package by type and value.
Mirrors <code>XPackageChunkSelector</code>. Use the factory methods to construct.

 [PackageInstallationProgress](GDK.Net.Package.PackageInstallationProgress.md)

A snapshot of a package's installation progress.
Mirrors <code>XPackageInstallationProgress</code>.

 [PackageVersion](GDK.Net.Package.PackageVersion.md)

A GDK package version. Mirrors the four <code>uint16_t</code> fields of <code>XVersion</code>.

 [PackageWriteStats](GDK.Net.Package.PackageWriteStats.md)

Write-budget statistics for the current packaged process.
Mirrors <code>XPackageWriteStats</code>.

### Enums

 [PackageChunkAvailability](GDK.Net.Package.PackageChunkAvailability.md)

The installation availability of a chunk. Mirrors <code>XPackageChunkAvailability</code>.

 [PackageChunkSelectorType](GDK.Net.Package.PackageChunkSelectorType.md)

The type of a chunk selector. Mirrors <code>XPackageChunkSelectorType</code>.

 [PackageEnumerationScope](GDK.Net.Package.PackageEnumerationScope.md)

The scope of a package enumeration. Mirrors <code>XPackageEnumerationScope</code>.

 [PackageKind](GDK.Net.Package.PackageKind.md)

The kind of a GDK package. Mirrors <code>XPackageKind</code>.

