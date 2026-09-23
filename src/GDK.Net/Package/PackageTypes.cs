using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.Package;

// ---- enums -------------------------------------------------------------------

/// <summary>The kind of a GDK package. Mirrors <c>XPackageKind</c>.</summary>
public enum PackageKind : uint
{
    /// <summary>A game package.</summary>
    Game = 0,
    /// <summary>A content (DLC) package.</summary>
    Content = 1,
    /// <summary>A publisher-scoped content package shared across the publisher's titles.</summary>
    PublisherContent = 2,
}

/// <summary>The scope of a package enumeration. Mirrors <c>XPackageEnumerationScope</c>.</summary>
public enum PackageEnumerationScope : uint
{
    /// <summary>Enumerate only the current package.</summary>
    ThisOnly = 0,
    /// <summary>Enumerate this package and related packages.</summary>
    ThisAndRelated = 1,
    /// <summary>Enumerate all packages from the same publisher.</summary>
    ThisPublisher = 2,
}

/// <summary>The type of a chunk selector. Mirrors <c>XPackageChunkSelectorType</c>.</summary>
public enum PackageChunkSelectorType : uint
{
    /// <summary>Select chunks by installed language.</summary>
    Language = 0,
    /// <summary>Select chunks by tag.</summary>
    Tag = 1,
    /// <summary>Select a chunk by its numeric id.</summary>
    Chunk = 2,
    /// <summary>Select chunks by feature name.</summary>
    Feature = 3,
}

/// <summary>The installation availability of a chunk. Mirrors <c>XPackageChunkAvailability</c>.</summary>
public enum PackageChunkAvailability : uint
{
    /// <summary>The chunk is fully installed and ready to use.</summary>
    Ready = 0,
    /// <summary>The chunk is being installed.</summary>
    Pending = 1,
    /// <summary>The chunk can be installed on this device.</summary>
    Installable = 2,
    /// <summary>The chunk is not available on this device.</summary>
    Unavailable = 3,
}

// ---- value types / data carriers ---------------------------------------------

/// <summary>
/// A GDK package version. Mirrors the four <c>uint16_t</c> fields of <c>XVersion</c>.
/// </summary>
public readonly struct PackageVersion : IEquatable<PackageVersion>
{
    internal PackageVersion(XVersion native)
    {
        Major = native.Major;
        Minor = native.Minor;
        Build = native.Build;
        Revision = native.Revision;
    }

    /// <summary>Constructs a version from its four components.</summary>
    public PackageVersion(ushort major, ushort minor, ushort build, ushort revision)
    {
        Major = major;
        Minor = minor;
        Build = build;
        Revision = revision;
    }

    /// <inheritdoc cref="System.Version.Major"/>
    public ushort Major { get; }
    /// <inheritdoc cref="System.Version.Minor"/>
    public ushort Minor { get; }
    /// <inheritdoc cref="System.Version.Build"/>
    public ushort Build { get; }
    /// <inheritdoc cref="System.Version.Revision"/>
    public ushort Revision { get; }

    /// <inheritdoc/>
    public bool Equals(PackageVersion other) =>
        Major == other.Major && Minor == other.Minor && Build == other.Build && Revision == other.Revision;
    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is PackageVersion v && Equals(v);
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            int h = 17;
            h = h * 31 + Major.GetHashCode();
            h = h * 31 + Minor.GetHashCode();
            h = h * 31 + Build.GetHashCode();
            h = h * 31 + Revision.GetHashCode();
            return h;
        }
    }
    /// <inheritdoc/>
    public override string ToString() => $"{Major}.{Minor}.{Build}.{Revision}";

    /// <inheritdoc cref="Equals(PackageVersion)"/>
    public static bool operator ==(PackageVersion l, PackageVersion r) => l.Equals(r);
    /// <inheritdoc cref="Equals(PackageVersion)"/>
    public static bool operator !=(PackageVersion l, PackageVersion r) => !l.Equals(r);
}

/// <summary>
/// A selector that identifies one or more chunks within a package by type and value.
/// Mirrors <c>XPackageChunkSelector</c>. Use the factory methods to construct.
/// </summary>
public readonly struct PackageChunkSelector : IEquatable<PackageChunkSelector>
{
    private readonly string? _stringValue;
    private readonly uint _chunkId;

    private PackageChunkSelector(PackageChunkSelectorType type, string value)
    {
        Type = type;
        _stringValue = value;
    }

    private PackageChunkSelector(uint chunkId)
    {
        Type = PackageChunkSelectorType.Chunk;
        _chunkId = chunkId;
    }

    /// <summary>The selector category.</summary>
    public PackageChunkSelectorType Type { get; }

    /// <summary>The string value for <see cref="PackageChunkSelectorType.Language"/>,
    /// <see cref="PackageChunkSelectorType.Tag"/>, or <see cref="PackageChunkSelectorType.Feature"/> selectors.</summary>
    public string? StringValue => _stringValue;

    /// <summary>The chunk id for <see cref="PackageChunkSelectorType.Chunk"/> selectors.</summary>
    public uint ChunkId => _chunkId;

    /// <summary>Creates a selector that picks chunks by installed language tag (e.g. <c>"en-US"</c>).</summary>
    public static PackageChunkSelector ByLanguage(string language)
    {
        if (language is null) throw new ArgumentNullException(nameof(language));
        return new PackageChunkSelector(PackageChunkSelectorType.Language, language);
    }

    /// <summary>Creates a selector that picks chunks by tag string.</summary>
    public static PackageChunkSelector ByTag(string tag)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        return new PackageChunkSelector(PackageChunkSelectorType.Tag, tag);
    }

    /// <summary>Creates a selector that picks a single chunk by its numeric id.</summary>
    public static PackageChunkSelector ByChunkId(uint chunkId) => new PackageChunkSelector(chunkId);

    /// <summary>Creates a selector that picks chunks belonging to a named feature.</summary>
    public static PackageChunkSelector ByFeature(string feature)
    {
        if (feature is null) throw new ArgumentNullException(nameof(feature));
        return new PackageChunkSelector(PackageChunkSelectorType.Feature, feature);
    }

    /// <inheritdoc/>
    public bool Equals(PackageChunkSelector other) =>
        Type == other.Type && _stringValue == other._stringValue && _chunkId == other._chunkId;
    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is PackageChunkSelector s && Equals(s);
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            int h = 17;
            h = h * 31 + Type.GetHashCode();
            h = h * 31 + (_stringValue?.GetHashCode() ?? 0);
            h = h * 31 + _chunkId.GetHashCode();
            return h;
        }
    }
    /// <inheritdoc/>
    public override string ToString() =>
        Type == PackageChunkSelectorType.Chunk ? $"Chunk({_chunkId})" : $"{Type}({_stringValue})";

    /// <inheritdoc cref="Equals(PackageChunkSelector)"/>
    public static bool operator ==(PackageChunkSelector l, PackageChunkSelector r) => l.Equals(r);
    /// <inheritdoc cref="Equals(PackageChunkSelector)"/>
    public static bool operator !=(PackageChunkSelector l, PackageChunkSelector r) => !l.Equals(r);
}

/// <summary>
/// Information about an installed or available package.
/// Mirrors <c>XPackageDetails</c>.
/// </summary>
/// <remarks>
/// <c>PackageIdentifier</c> is a durable opaque string that identifies a package across versions.
/// It is not human-readable; use <c>DisplayName</c> for UI.
/// </remarks>
public sealed class PackageInfo
{
    internal PackageInfo(
        string packageIdentifier,
        PackageVersion version,
        PackageKind kind,
        string displayName,
        string description,
        string publisher,
        string storeId,
        bool installing,
        uint index,
        uint count,
        bool ageRestricted,
        string titleId)
    {
        PackageIdentifier = packageIdentifier;
        Version = version;
        Kind = kind;
        DisplayName = displayName;
        Description = description;
        Publisher = publisher;
        StoreId = storeId;
        Installing = installing;
        Index = index;
        Count = count;
        AgeRestricted = ageRestricted;
        TitleId = titleId;
    }

    /// <summary>The opaque package identifier string (<c>XPACKAGE_IDENTIFIER_MAX_LENGTH</c> = 33 characters).</summary>
    public string PackageIdentifier { get; }

    /// <summary>The package version.</summary>
    public PackageVersion Version { get; }

    /// <summary>Whether this is a game or content package.</summary>
    public PackageKind Kind { get; }

    /// <summary>Human-readable display name.</summary>
    public string DisplayName { get; }

    /// <summary>Human-readable description.</summary>
    public string Description { get; }

    /// <summary>Publisher name.</summary>
    public string Publisher { get; }

    /// <summary>Store product id, or empty string if not available.</summary>
    public string StoreId { get; }

    /// <summary><see langword="true"/> while the package is being installed.</summary>
    public bool Installing { get; }

    /// <summary>Zero-based index within the current enumeration batch.</summary>
    public uint Index { get; }

    /// <summary>Total count in the current enumeration batch.</summary>
    public uint Count { get; }

    /// <summary><see langword="true"/> if the package is age-restricted on this device.</summary>
    public bool AgeRestricted { get; }

    /// <summary>The package title id string, or empty string if not available.</summary>
    public string TitleId { get; }

    /// <inheritdoc/>
    public override string ToString() => $"PackageInfo({PackageIdentifier}, {Kind}, {Version})";
}

/// <summary>
/// A feature entry returned by <see cref="GamePackage.EnumerateFeatures"/>.
/// Mirrors <c>XPackageFeature</c>.
/// </summary>
public sealed class PackageFeature
{
    internal PackageFeature(
        string id,
        string displayName,
        string tags,
        bool hidden,
        IReadOnlyList<string> storeIds)
    {
        Id = id;
        DisplayName = displayName;
        Tags = tags;
        Hidden = hidden;
        StoreIds = storeIds;
    }

    /// <summary>The feature's unique identifier string.</summary>
    public string Id { get; }

    /// <summary>Human-readable display name.</summary>
    public string DisplayName { get; }

    /// <summary>Space-separated tag string associated with this feature.</summary>
    public string Tags { get; }

    /// <summary><see langword="true"/> if the feature should not be shown in UI.</summary>
    public bool Hidden { get; }

    /// <summary>The store product ids for the content packs that provide this feature.</summary>
    public IReadOnlyList<string> StoreIds { get; }
}

/// <summary>
/// A snapshot of a package's installation progress.
/// Mirrors <c>XPackageInstallationProgress</c>.
/// </summary>
public readonly struct PackageInstallationProgress
{
    internal PackageInstallationProgress(XPackageInstallationProgress native)
    {
        TotalBytes = native.totalBytes;
        InstalledBytes = native.installedBytes;
        LaunchBytes = native.launchBytes;
        Launchable = native.launchable != 0;
        Completed = native.completed != 0;
    }

    /// <summary>Total bytes that will be installed.</summary>
    public ulong TotalBytes { get; }

    /// <summary>Bytes installed so far.</summary>
    public ulong InstalledBytes { get; }

    /// <summary>Bytes required before the title can be launched.</summary>
    public ulong LaunchBytes { get; }

    /// <summary><see langword="true"/> when enough data is installed for the title to launch.</summary>
    public bool Launchable { get; }

    /// <summary><see langword="true"/> when installation is fully complete.</summary>
    public bool Completed { get; }

    /// <summary>A value in [0, 1] representing download fraction, or 0 when total is unknown.</summary>
    public double Fraction => TotalBytes > 0 ? (double)InstalledBytes / TotalBytes : 0.0;
}

/// <summary>
/// Write-budget statistics for the current packaged process.
/// Mirrors <c>XPackageWriteStats</c>.
/// </summary>
/// <remarks>
/// GDK limits how many bytes a title can write per interval to protect storage performance.
/// Monitor this to avoid exceeding the budget.
/// </remarks>
public readonly struct PackageWriteStats
{
    internal PackageWriteStats(XPackageWriteStats native)
    {
        Interval = native.interval;
        Budget = native.budget;
        Elapsed = native.elapsed;
        BytesWritten = native.bytesWritten;
    }

    /// <summary>Budget interval in milliseconds.</summary>
    public ulong Interval { get; }

    /// <summary>Byte budget for the current interval.</summary>
    public ulong Budget { get; }

    /// <summary>Milliseconds elapsed in the current interval.</summary>
    public ulong Elapsed { get; }

    /// <summary>Bytes written so far in the current interval.</summary>
    public ulong BytesWritten { get; }
}

/// <summary>
/// Availability and a chunk selector, as returned by
/// <see cref="GamePackage.EnumerateChunkAvailability"/>.
/// </summary>
public sealed class PackageChunkAvailabilityInfo
{
    internal PackageChunkAvailabilityInfo(PackageChunkSelector selector, PackageChunkAvailability availability)
    {
        Selector = selector;
        Availability = availability;
    }

    /// <summary>The chunk selector identifying this chunk.</summary>
    public PackageChunkSelector Selector { get; }

    /// <summary>The chunk's current availability on this device.</summary>
    public PackageChunkAvailability Availability { get; }
}

/// <summary>
/// Event arguments for <see cref="GamePackage.PackageInstalled"/>.
/// </summary>
public sealed class PackageInstalledEventArgs : EventArgs
{
    internal PackageInstalledEventArgs(PackageInfo info) => Info = info;

    /// <summary>Details about the newly installed package.</summary>
    public PackageInfo Info { get; }
}

/// <summary>
/// Event arguments for <see cref="PackageInstallationMonitor.ProgressChanged"/>.
/// </summary>
public sealed class PackageProgressChangedEventArgs : EventArgs
{
    internal PackageProgressChangedEventArgs(PackageInstallationProgress progress) => Progress = progress;

    /// <summary>The latest installation progress snapshot.</summary>
    public PackageInstallationProgress Progress { get; }
}
