using System;
using System.Collections.Generic;
using System.Globalization;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// Shared marshalling helpers for the Party projection.
/// </summary>
internal static unsafe class PartyInterop
{
    /// <summary>Throws when a <c>PartyError</c> is non-zero.</summary>
    internal static void Check(uint error)
    {
        if (error != 0)
        {
            throw new PartyException(error, DescribeParty(error));
        }
    }

    /// <summary>Throws when a <c>PartyError</c> from the Xbox Live extension is non-zero.</summary>
    internal static void CheckXbl(uint error)
    {
        if (error != 0)
        {
            throw new PartyException(error, DescribeXbl(error));
        }
    }

    internal static string DescribeParty(uint error)
    {
        byte* message = null;
        if (NativePlayFab.PartyGetErrorMessage(error, &message) == 0)
        {
            string? text = Utf8.ToString(message);
            if (!string.IsNullOrEmpty(text))
            {
                return text!;
            }
        }

        return Fallback(error);
    }

    internal static string DescribeXbl(uint error)
    {
        byte* message = null;
        if (NativePlayFab.PartyXblGetErrorMessage(error, &message) == 0)
        {
            string? text = Utf8.ToString(message);
            if (!string.IsNullOrEmpty(text))
            {
                return text!;
            }
        }

        return Fallback(error);
    }

    private static string Fallback(uint error) =>
        string.Format(CultureInfo.InvariantCulture, "PartyError 0x{0:X8}.", error);

    /// <summary>Copies a native <c>PartyString*</c> array into managed strings.</summary>
    internal static IReadOnlyList<string> ReadStrings(byte** values, uint count)
    {
        if (values is null || count == 0)
        {
            return Array.Empty<string>();
        }

        var result = new string[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = Utf8.ToString(values[i]) ?? string.Empty;
        }

        return result;
    }

    /// <summary>Copies a native byte buffer into a managed array.</summary>
    internal static byte[] ReadBuffer(void* buffer, uint byteCount)
    {
        if (buffer is null || byteCount == 0)
        {
            return Array.Empty<byte>();
        }

        var result = new byte[byteCount];
        fixed (byte* target = result)
        {
            Buffer.MemoryCopy(buffer, target, byteCount, byteCount);
        }

        return result;
    }

    /// <summary>Reads a fixed-size, NUL-terminated <c>char[N]</c> field.</summary>
    internal static string ReadFixed(byte* value, int capacity)
    {
        int length = 0;
        while (length < capacity && value[length] != 0)
        {
            length++;
        }

        return length == 0 ? string.Empty : Utf8.ToString(value, length);
    }

    /// <summary>
    /// Copies a handle array into an <see cref="IntPtr"/> array so no native pointer escapes into
    /// a managed collection.
    /// </summary>
    internal static IntPtr[] ReadHandles(IntPtr* handles, uint count)
    {
        if (handles is null || count == 0)
        {
            return Array.Empty<IntPtr>();
        }

        var result = new IntPtr[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = handles[i];
        }

        return result;
    }

    /// <summary>Writes a handle array into the arena for a native call.</summary>
    internal static IntPtr* WriteHandles(
        PlayFabArena arena, IReadOnlyList<IntPtr>? handles, out uint count)
    {
        count = handles is null ? 0u : (uint)handles.Count;
        if (count == 0)
        {
            return null;
        }

        IntPtr* buffer = arena.Alloc<IntPtr>((int)count);
        for (int i = 0; i < handles!.Count; i++)
        {
            buffer[i] = handles[i];
        }

        return buffer;
    }

    /// <summary>Converts Party's <c>PartyBool</c> (a <c>uint8_t</c>) to a managed bool.</summary>
    internal static bool ToBool(byte value) => value != 0;

    /// <summary>Converts a managed bool to Party's <c>PartyBool</c>.</summary>
    internal static byte FromBool(bool value) => value ? (byte)1 : (byte)0;
}
