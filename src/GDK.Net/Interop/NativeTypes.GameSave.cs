// Raw blittable types for the XGameSave / XGameSaveFiles family.
//
// Sources: XGameSave.h, XGameSaveFiles.h (GDK edition 260404).
// Convention: only blittable fields; byte for C++ bool, byte* for const char*, long for time_t (int64_t on Win64).
// Struct layouts match the C++ ABI: LayoutKind.Sequential with default packing produces the same
// field offsets and sizeof as MSVC x64.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>
/// Mirrors <c>struct XGameSaveBlobInfo</c> from XGameSave.h.
/// Layout (x64): name(8) + size(4) + pad(4) = 16 bytes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeGameSaveBlobInfo
{
    /// <summary>Null-terminated UTF-8 blob name (unique within its container).</summary>
    public byte* name;

    /// <summary>Size of the saved blob data in bytes.</summary>
    public uint size;

    // 4 bytes of implicit trailing padding to align the struct to 8-byte boundary.
}

/// <summary>
/// Mirrors <c>struct XGameSaveBlob</c> from XGameSave.h.
/// Layout (x64): info(16) + data(8) = 24 bytes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeGameSaveBlob
{
    /// <summary>Blob metadata (name and size).</summary>
    public NativeGameSaveBlobInfo info;

    /// <summary>Pointer to the blob data within the result buffer.</summary>
    public byte* data;
}

/// <summary>
/// Mirrors <c>struct XGameSaveContainerInfo</c> from XGameSave.h.
/// Layout (x64): name(8) + displayName(8) + blobCount(4) + pad(4) + totalSize(8)
///               + lastModifiedTime(8) + needsSync(1) + pad(7) = 48 bytes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeGameSaveContainerInfo
{
    /// <summary>Null-terminated UTF-8 container name (unique within the provider).</summary>
    public byte* name;

    /// <summary>Null-terminated UTF-8 display name.</summary>
    public byte* displayName;

    /// <summary>Number of blobs in the container.</summary>
    public uint blobCount;

    // 4 bytes of implicit padding to align totalSize to an 8-byte boundary.

    /// <summary>Total size of all blobs in the container in bytes.</summary>
    public ulong totalSize;

    /// <summary>Last time the container was updated (seconds since Unix epoch).</summary>
    public long lastModifiedTime;

    /// <summary>Non-zero when the container is not yet synced with the cloud (<c>bool needsSync</c>).</summary>
    public byte needsSync;

    // 7 bytes of implicit trailing padding to align the struct to 8-byte boundary.
}

// ──────────────────────────────────────────────────────────────────────────────
// Callback delegate declarations — needed on netstandard2.0 where
// [UnmanagedCallersOnly] / delegate* are unavailable.  On NET5+ the trampolines
// in GameSaveCallbacks use static [UnmanagedCallersOnly] function pointers directly.
// ──────────────────────────────────────────────────────────────────────────────
#if !NET5_0_OR_GREATER

internal static unsafe class GameSaveNativeDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate byte ContainerInfoCallbackDelegate(NativeGameSaveContainerInfo* info, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate byte BlobInfoCallbackDelegate(NativeGameSaveBlobInfo* info, IntPtr context);
}

#endif
