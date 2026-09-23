using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GDK.Net.Interop;

/// <summary>
/// UTF-8 marshalling helpers. The GDK's string parameters are all null-terminated UTF-8
/// <c>const char*</c>, and returned strings are pointers into memory the runtime owns, so they must
/// be copied before the call that produced them returns.
/// </summary>
internal static unsafe class Utf8
{
    /// <summary>
    /// Copies a null-terminated UTF-8 string out of native memory. Returns <see langword="null"/>
    /// for a null pointer, which the GDK uses for absent optional strings.
    /// </summary>
    internal static string? ToString(byte* value)
    {
        if (value is null)
        {
            return null;
        }

        int length = 0;
        while (value[length] != 0)
        {
            length++;
        }

        return length == 0 ? string.Empty : Encoding.UTF8.GetString(value, length);
    }

    /// <summary>
    /// Copies a UTF-8 buffer of known length, stopping at the first null. The GDK's fixed-size
    /// buffers report a used length that includes the terminator.
    /// </summary>
    internal static string ToString(byte* value, int length)
    {
        if (value is null || length <= 0)
        {
            return string.Empty;
        }

        int end = 0;
        while (end < length && value[end] != 0)
        {
            end++;
        }

        return end == 0 ? string.Empty : Encoding.UTF8.GetString(value, end);
    }

    /// <summary>
    /// Allocates a null-terminated UTF-8 copy of <paramref name="value"/> in unmanaged memory, or
    /// <see cref="IntPtr.Zero"/> when it is <see langword="null"/>. The caller owns the result and
    /// must release it with <see cref="Free"/>.
    /// </summary>
    /// <remarks>
    /// Unmanaged memory rather than a pinned managed array: several GDK calls retain the pointer
    /// past the call, and pinning for that long fragments the heap.
    /// </remarks>
    internal static IntPtr Allocate(string? value)
    {
        if (value is null)
        {
            return IntPtr.Zero;
        }

        int byteCount = Encoding.UTF8.GetByteCount(value);
        IntPtr buffer = Marshal.AllocHGlobal(byteCount + 1);

        fixed (char* chars = value)
        {
            Encoding.UTF8.GetBytes(chars, value.Length, (byte*)buffer, byteCount);
        }

        ((byte*)buffer)[byteCount] = 0;
        return buffer;
    }

    /// <summary>
    /// Writes a null-terminated UTF-8 copy of <paramref name="value"/> into a fixed-size native
    /// buffer of <paramref name="capacity"/> bytes, terminator included.
    /// </summary>
    internal static void CopyFixed(byte* destination, int capacity, string? value)
    {
        if (destination is null || capacity <= 0)
        {
            return;
        }

        if (string.IsNullOrEmpty(value))
        {
            destination[0] = 0;
            return;
        }

        int byteCount = Encoding.UTF8.GetByteCount(value);
        if (byteCount >= capacity)
        {
            throw new ArgumentException(
                $"The value is {byteCount} UTF-8 bytes but the field holds at most {capacity - 1}.",
                nameof(value));
        }

        fixed (char* chars = value)
        {
            Encoding.UTF8.GetBytes(chars, value!.Length, destination, byteCount);
        }

        destination[byteCount] = 0;
    }

    /// <summary>Releases a buffer returned by <see cref="Allocate"/>. Safe on <see cref="IntPtr.Zero"/>.</summary>
    internal static void Free(IntPtr buffer)
    {
        if (buffer != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}
