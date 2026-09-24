using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab;

/// <summary>
/// A bump allocator for the unmanaged memory a PlayFab request graph needs.
/// </summary>
/// <remarks>
/// <para>
/// PlayFab request structs are deep trees of <c>const char*</c>, optional scalars and arrays that
/// the native library reads while the call is in flight. Marshalling each node with its own
/// <c>AllocHGlobal</c>/<c>FreeHGlobal</c> pair would need a bookkeeping graph to unwind; instead
/// every allocation for one call is recorded here and the whole arena is released once, after the
/// call completes.
/// </para>
/// <para>
/// Unmanaged memory rather than pinned managed arrays: the native library holds the pointers for
/// the duration of an HTTP round trip, and pinning a request graph for that long fragments the
/// heap.
/// </para>
/// </remarks>
internal sealed unsafe class PlayFabArena : IDisposable
{
    private readonly List<IntPtr> _blocks = new();
    private bool _disposed;

    /// <summary>
    /// Copies <paramref name="value"/> into the arena as a null-terminated UTF-8 string, or returns
    /// <see langword="null"/> when it is <see langword="null"/>: the encoding PlayFab uses for an
    /// absent optional string.
    /// </summary>
    internal byte* String(string? value)
    {
        if (value is null)
        {
            return null;
        }

        int byteCount = Encoding.UTF8.GetByteCount(value);
        var buffer = (byte*)Rent(byteCount + 1);

        if (value.Length != 0)
        {
            fixed (char* chars = value)
            {
                Encoding.UTF8.GetBytes(chars, value.Length, buffer, byteCount);
            }
        }

        buffer[byteCount] = 0;
        return buffer;
    }

    /// <summary>Allocates a zeroed array of <paramref name="count"/> values.</summary>
    internal T* Alloc<T>(int count)
        where T : unmanaged
    {
        if (count <= 0)
        {
            return null;
        }

        var block = (T*)Rent(sizeof(T) * count);
        for (int i = 0; i < count; i++)
        {
            block[i] = default;
        }

        return block;
    }

    /// <summary>
    /// Boxes an optional value into the arena. PlayFab spells "field not set" as a null pointer to
    /// the scalar, so <see langword="null"/> in and null pointer out is the whole contract.
    /// </summary>
    internal T* Value<T>(T? value)
        where T : unmanaged
    {
        if (value is null)
        {
            return null;
        }

        T* slot = Alloc<T>(1);
        *slot = value.Value;
        return slot;
    }

    /// <summary>Copies a list of strings into the arena as a <c>const char* const*</c> array.</summary>
    internal byte** StringArray(IReadOnlyList<string?>? values)
    {
        if (values is null || values.Count == 0)
        {
            return null;
        }

        var array = (byte**)Rent(sizeof(byte*) * values.Count);
        for (int i = 0; i < values.Count; i++)
        {
            array[i] = String(values[i]);
        }

        return array;
    }

    /// <summary>Allocates a null-filled array of <paramref name="count"/> pointers.</summary>
    internal T** PointerArray<T>(int count)
        where T : unmanaged
    {
        if (count <= 0)
        {
            return null;
        }

        var array = (T**)Rent(sizeof(T*) * count);
        for (int i = 0; i < count; i++)
        {
            array[i] = null;
        }

        return array;
    }

    /// <summary>
    /// Copies a JSON document into the arena. PlayFab models free-form JSON as a struct wrapping a
    /// single string, which this projection surfaces as a plain <see cref="string"/>.
    /// </summary>
    internal PFJsonObject* Json(string? value)
    {
        if (value is null)
        {
            return null;
        }

        PFJsonObject* slot = Alloc<PFJsonObject>(1);
        slot->StringValue = String(value);
        return slot;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        for (int i = 0; i < _blocks.Count; i++)
        {
            Marshal.FreeHGlobal(_blocks[i]);
        }

        _blocks.Clear();
    }

    private IntPtr Rent(int byteCount)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(PlayFabArena));
        }

        IntPtr block = Marshal.AllocHGlobal(byteCount);
        _blocks.Add(block);
        return block;
    }
}
