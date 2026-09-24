using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// Marshalling helpers shared by the hand-written PFMP projection.
/// </summary>
/// <remarks>
/// The PFMP entry points copy everything they are given before returning, so an arena only has to
/// outlive the call itself: unlike the PlayFab Services calls, which read the request graph for
/// the whole HTTP round trip.
/// </remarks>
internal static unsafe class MultiplayerInterop
{
    internal static PFEntityKey* Write(PlayFabArena arena, EntityKey? key)
    {
        if (key is null)
        {
            return null;
        }

        PFEntityKey* native = arena.Alloc<PFEntityKey>(1);
        key.WriteTo(native, arena);
        return native;
    }

    internal static PFEntityKey* WriteArray(
        PlayFabArena arena, IReadOnlyList<EntityKey> keys)
    {
        PFEntityKey* native = arena.Alloc<PFEntityKey>(keys.Count);
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i].WriteTo(&native[i], arena);
        }

        return native;
    }

    internal static void WriteProperties(
        PlayFabArena arena,
        IReadOnlyDictionary<string, string>? properties,
        out uint count,
        out byte** keys,
        out byte** values)
    {
        if (properties is null || properties.Count == 0)
        {
            count = 0;
            keys = null;
            values = null;
            return;
        }

        count = (uint)properties.Count;
        keys = arena.PointerArray<byte>(properties.Count);
        values = arena.PointerArray<byte>(properties.Count);

        int next = 0;
        foreach (KeyValuePair<string, string> pair in properties)
        {
            keys[next] = arena.String(pair.Key);
            values[next] = arena.String(pair.Value);
            next++;
        }
    }

    internal static IReadOnlyList<string> ReadStrings(byte** values, uint count)
    {
        if (values is null || count == 0)
        {
            return Array.Empty<string>();
        }

        var list = new List<string>((int)count);
        for (uint i = 0; i < count; i++)
        {
            list.Add(Utf8.ToString(values[i]) ?? string.Empty);
        }

        return list;
    }
}
