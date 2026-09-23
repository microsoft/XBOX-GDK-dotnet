using System;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// Correlates a multiplayer operation with the state change that completes it.
/// </summary>
/// <remarks>
/// PFMP operations start synchronously and report completion on a later pump rather than through
/// an <c>XAsyncBlock</c>, so they are not awaitable. Every start returns an id, which the matching
/// completion record echoes back in its <c>Operation</c> property.
/// </remarks>
public readonly struct OperationId : IEquatable<OperationId>
{
    internal OperationId(long value)
    {
        Value = value;
    }

    internal long Value { get; }

    /// <summary>An id that matches no operation.</summary>
    public static OperationId None => default;

    /// <summary>Whether this id refers to an operation the title started.</summary>
    public bool IsValid => Value != 0;

    /// <inheritdoc/>
    public bool Equals(OperationId other) => Value == other.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is OperationId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => Value.ToString();

    /// <summary>Compares two ids for equality.</summary>
    public static bool operator ==(OperationId left, OperationId right) => left.Equals(right);

    /// <summary>Compares two ids for inequality.</summary>
    public static bool operator !=(OperationId left, OperationId right) => !left.Equals(right);
}
