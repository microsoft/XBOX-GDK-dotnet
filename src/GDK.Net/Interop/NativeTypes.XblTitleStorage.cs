// Blittable mirrors of the XSAPI title storage types -- xsapi-c\title_storage_c.h, GDK edition 260404.
//
// See NativeTypes.Xbl.cs for the layout rules that apply across the XSAPI type mirrors. The
// metadata struct uses fixed UTF-8 buffers, C++ bool is one byte in entry points, time_t is a
// signed 64-bit value, and size_t maps to nuint.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblTitleStorageType</c>.</summary>
internal enum XblTitleStorageType : uint
{
    TrustedPlatformStorage = 0,
    GlobalStorage = 1,
    Universal = 2,
}

/// <summary>Mirrors <c>XblTitleStorageBlobType</c>.</summary>
internal enum XblTitleStorageBlobType : uint
{
    Unknown = 0,
    Binary = 1,
    Json = 2,
    Config = 3,
}

/// <summary>Mirrors <c>XblTitleStorageETagMatchCondition</c>.</summary>
internal enum XblTitleStorageETagMatchCondition : uint
{
    NotUsed = 0,
    IfMatch = 1,
    IfNotMatch = 2,
}

/// <summary>Mirrors <c>XblTitleStorageBlobMetadata</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblTitleStorageBlobMetadata
{
    internal const int BlobPathMaxLength = 257 * 3;
    internal const int BlobDisplayNameMaxLength = 129 * 3;
    internal const int BlobETagMaxLength = 18 * 3;
    internal const int ServiceConfigurationIdLength = 40;

    internal fixed byte BlobPath[BlobPathMaxLength];
    internal XblTitleStorageBlobType BlobType;
    internal XblTitleStorageType StorageType;
    internal fixed byte DisplayName[BlobDisplayNameMaxLength];
    internal fixed byte ETag[BlobETagMaxLength];
    internal long ClientTimestamp;
    internal nuint Length;
    internal fixed byte ServiceConfigurationId[ServiceConfigurationIdLength];
    internal ulong XboxUserId;
}
