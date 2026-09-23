// Static enumerate-callback trampolines for the XGameSave family.
//
// Native enumerate functions call back synchronously with a pointer to a native info struct plus a
// void* context.  The context is a GCHandle pointing to the accumulator list.
//
// On NET5_0_OR_GREATER: [UnmanagedCallersOnly] static methods produce a raw function pointer with
// zero allocation.
// On netstandard2.0: static readonly delegates are marshalled once at class initialisation and
// rooted for the process lifetime to prevent collection while a P/Invoke is in flight.
//
// Exceptions must never cross the native boundary — every trampoline swallows them and returns 0
// (stop enumeration) so the caller can inspect a partial result or throw its own exception.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.GameSave;

internal static unsafe class GameSaveCallbacks
{
#if NET5_0_OR_GREATER

    /// <summary>
    /// Stdcall function pointer for <c>XGameSaveContainerInfoCallback</c>.
    /// Context is a <see cref="GCHandle"/> pointing to a <see cref="List{GameSaveContainerInfo}"/>.
    /// </summary>
    internal static IntPtr ContainerInfoCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<NativeGameSaveContainerInfo*, IntPtr, byte>)&OnContainerInfo;

    /// <summary>
    /// Stdcall function pointer for <c>XGameSaveBlobInfoCallback</c>.
    /// Context is a <see cref="GCHandle"/> pointing to a <see cref="List{GameSaveBlobInfo}"/>.
    /// </summary>
    internal static IntPtr BlobInfoCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<NativeGameSaveBlobInfo*, IntPtr, byte>)&OnBlobInfo;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnContainerInfo(NativeGameSaveContainerInfo* info, IntPtr context)
    {
        try
        {
            var list = (List<GameSaveContainerInfo>)GCHandle.FromIntPtr(context).Target!;
            list.Add(ParseContainerInfo(info));
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnBlobInfo(NativeGameSaveBlobInfo* info, IntPtr context)
    {
        try
        {
            var list = (List<GameSaveBlobInfo>)GCHandle.FromIntPtr(context).Target!;
            list.Add(ParseBlobInfo(info));
            return 1;
        }
        catch
        {
            return 0;
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte ContainerInfoCallbackDelegate(NativeGameSaveContainerInfo* info, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte BlobInfoCallbackDelegate(NativeGameSaveBlobInfo* info, IntPtr context);

    // Rooted for the process lifetime so the function pointers remain valid indefinitely.
    private static readonly ContainerInfoCallbackDelegate ContainerInfoCallbackKeepAlive =
        ContainerInfoCallbackImpl;
    private static readonly BlobInfoCallbackDelegate BlobInfoCallbackKeepAlive =
        BlobInfoCallbackImpl;

    internal static IntPtr ContainerInfoCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ContainerInfoCallbackKeepAlive);

    internal static IntPtr BlobInfoCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(BlobInfoCallbackKeepAlive);

    private static byte ContainerInfoCallbackImpl(NativeGameSaveContainerInfo* info, IntPtr context)
    {
        try
        {
            var list = (List<GameSaveContainerInfo>)GCHandle.FromIntPtr(context).Target!;
            list.Add(ParseContainerInfo(info));
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    private static byte BlobInfoCallbackImpl(NativeGameSaveBlobInfo* info, IntPtr context)
    {
        try
        {
            var list = (List<GameSaveBlobInfo>)GCHandle.FromIntPtr(context).Target!;
            list.Add(ParseBlobInfo(info));
            return 1;
        }
        catch
        {
            return 0;
        }
    }

#endif

    // ──────────────────────────────────────────────────────────────────────────
    // Shared helper: parse and deep-copy the native structs to managed objects.
    // The native pointers are valid only for the duration of the callback.
    // ──────────────────────────────────────────────────────────────────────────

    internal static GameSaveContainerInfo ParseContainerInfo(NativeGameSaveContainerInfo* info)
    {
        string name = PtrToStringUtf8(info->name);
        string displayName = PtrToStringUtf8(info->displayName);
        bool needsSync = info->needsSync != 0;
        DateTimeOffset lastModified = DateTimeOffset.FromUnixTimeSeconds(info->lastModifiedTime);

        return new GameSaveContainerInfo(
            name,
            displayName,
            info->blobCount,
            (long)info->totalSize,
            lastModified,
            needsSync);
    }

    internal static GameSaveBlobInfo ParseBlobInfo(NativeGameSaveBlobInfo* info)
    {
        string name = PtrToStringUtf8(info->name);
        return new GameSaveBlobInfo(name, info->size);
    }

    /// <summary>
    /// Converts a null-terminated UTF-8 byte pointer to a managed string.
    /// Returns <see cref="string.Empty"/> when <paramref name="ptr"/> is null.
    /// </summary>
    internal static string PtrToStringUtf8(byte* ptr)
    {
        if (ptr == null)
        {
            return string.Empty;
        }

        int len = 0;
        while (ptr[len] != 0)
        {
            len++;
        }

        return len == 0 ? string.Empty : Encoding.UTF8.GetString(ptr, len);
    }
}
