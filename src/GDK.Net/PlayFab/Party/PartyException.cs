using System;
using System.Globalization;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// The failure reported by a PlayFab Party entry point or by a failed Party state change.
/// </summary>
/// <remarks>
/// Party reports failures as a <c>PartyError</c> rather than an <c>HRESULT</c>, so it is surfaced
/// as its own exception type carrying the raw code and the message Party supplies for it.
/// </remarks>
public sealed class PartyException : GameRuntimeException
{
    internal PartyException(uint error, string message)
        : base(unchecked((int)error), message)
    {
        ErrorCode = error;
    }

    /// <summary>The raw <c>PartyError</c> value.</summary>
    public uint ErrorCode { get; }
}

/// <summary>
/// Correlates a Party operation with the state change that completes it.
/// </summary>
/// <remarks>
/// Party operations start synchronously and report completion on a later pump rather than through
/// an <c>XAsyncBlock</c>, so they are not awaitable. Every start returns an id, which the matching
/// completion record echoes back in its <c>Operation</c> property.
/// </remarks>
public readonly struct PartyOperationId : IEquatable<PartyOperationId>
{
    internal PartyOperationId(long value)
    {
        Value = value;
    }

    internal long Value { get; }

    /// <summary>An id that matches no operation.</summary>
    public static PartyOperationId None => default;

    /// <summary>Whether this id refers to an operation the title started.</summary>
    public bool IsValid => Value != 0;

    /// <inheritdoc/>
    public bool Equals(PartyOperationId other) => Value == other.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is PartyOperationId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() =>
        Value.ToString(CultureInfo.InvariantCulture);

    /// <summary>Compares two ids for equality.</summary>
    public static bool operator ==(PartyOperationId left, PartyOperationId right) =>
        left.Equals(right);

    /// <summary>Compares two ids for inequality.</summary>
    public static bool operator !=(PartyOperationId left, PartyOperationId right) =>
        !left.Equals(right);
}
