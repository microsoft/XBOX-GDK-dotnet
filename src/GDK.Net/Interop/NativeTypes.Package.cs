// Raw interop types for XPackage.h (GDK edition 260404).
// Only blittable types: IntPtr for pointer fields, byte for bool, uint/ulong for integral types.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

// ---- enums -------------------------------------------------------------------

internal enum XPackageChunkSelectorType : uint
{
    Language = 0,
    Tag = 1,
    Chunk = 2,
    Feature = 3,
}

internal enum XPackageChunkAvailability : uint
{
    Ready = 0,
    Pending = 1,
    Installable = 2,
    Unavailable = 3,
}

internal enum XPackageKind : uint
{
    Game = 0,
    Content = 1,
    PublisherContent = 2,
}

internal enum XPackageEnumerationScope : uint
{
    ThisOnly = 0,
    ThisAndRelated = 1,
    ThisPublisher = 2,
}

// ---- structs -----------------------------------------------------------------


/// <summary>
/// Mirrors <c>struct XPackageChunkSelector</c> from XPackage.h.
/// The union field is stored as an <see cref="IntPtr"/>: string-type selectors hold a pointer to
/// a pinned UTF-8 byte array; <see cref="XPackageChunkSelectorType.Chunk"/> stores the chunk id
/// cast to <see cref="IntPtr"/> (low 32 bits on x64).
/// Sequential layout: uint32 Type + natural alignment padding + pointer-sized Value = 16 bytes on x64.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XPackageChunkSelector
{
    public XPackageChunkSelectorType Type;
    // 4 bytes of natural alignment padding on x64 before the pointer-sized field.
    public IntPtr Value;
}

/// <summary>
/// Mirrors <c>struct XPackageDetails</c> from XPackage.h.
/// Explicit layout locks field offsets to the x64 C compiler layout.
/// String fields are <see cref="IntPtr"/> to keep the struct blittable.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal struct XPackageDetails
{
    [FieldOffset(0)]  public IntPtr packageIdentifier; // const char*
    [FieldOffset(8)]  public XVersion version;
    [FieldOffset(16)] public XPackageKind kind;
    // 4 bytes padding at 20 (pointer alignment on x64)
    [FieldOffset(24)] public IntPtr displayName;       // const char*
    [FieldOffset(32)] public IntPtr description;       // const char*
    [FieldOffset(40)] public IntPtr publisher;         // const char*
    [FieldOffset(48)] public IntPtr storeId;           // const char*
    [FieldOffset(56)] public byte installing;
    // 3 bytes padding at 57 (uint32 alignment)
    [FieldOffset(60)] public uint index;
    [FieldOffset(64)] public uint count;
    [FieldOffset(68)] public byte ageRestricted;
    // 7 bytes padding at 69 (pointer alignment on x64)
    [FieldOffset(76)] public IntPtr titleId;           // const char*
    // Struct size = 84 bytes raw → padded to 88 for 8-byte alignment
}

/// <summary>
/// Mirrors <c>struct XPackageFeature</c> from XPackage.h.
/// Explicit layout for x64. String fields are <see cref="IntPtr"/>.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal struct XPackageFeature
{
    [FieldOffset(0)]  public IntPtr id;           // const char*
    [FieldOffset(8)]  public IntPtr displayName;  // const char*
    [FieldOffset(16)] public IntPtr tags;         // const char*
    [FieldOffset(24)] public byte hidden;
    // 3 bytes padding at 25 (uint32 alignment)
    [FieldOffset(28)] public uint storeIdCount;
    [FieldOffset(32)] public IntPtr storeIds;     // const char**
    // Struct size = 40 bytes (aligned to 8)
}

/// <summary>
/// Mirrors <c>struct XPackageInstallationProgress</c> from XPackage.h.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XPackageInstallationProgress
{
    public ulong totalBytes;
    public ulong installedBytes;
    public ulong launchBytes;
    public byte launchable;
    public byte completed;
    // 6 bytes padding (8-byte struct alignment)
}

/// <summary>
/// Mirrors <c>struct XPackageWriteStats</c> from XPackage.h.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XPackageWriteStats
{
    public ulong interval;
    public ulong budget;
    public ulong elapsed;
    public ulong bytesWritten;
}
