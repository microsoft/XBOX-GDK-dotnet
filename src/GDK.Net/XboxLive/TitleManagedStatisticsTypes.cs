using System;

namespace GDK.Net.XboxLive;

/// <summary>Kind of title-managed statistic value. Mirrors <c>XblTitleManagedStatType</c>.</summary>
public enum TitleManagedStatType : uint
{
    /// <summary>The statistic is backed by a JSON number.</summary>
    Number = 0,

    /// <summary>The statistic is backed by a JSON string.</summary>
    String = 1,
}

/// <summary>
/// Immutable discriminated value for a title-managed statistic.
/// </summary>
/// <remarks>
/// XSAPI carries both native payload fields in <c>XblTitleManagedStatistic</c> and uses the
/// <c>statisticType</c> tag to select one. This type keeps the same tag but exposes only the active
/// arm to callers.
/// </remarks>
public readonly struct TitleManagedStatisticValue : IEquatable<TitleManagedStatisticValue>
{
    private readonly double _numberValue;
    private readonly string? _stringValue;

    private TitleManagedStatisticValue(double numberValue)
    {
        Type = TitleManagedStatType.Number;
        _numberValue = numberValue;
        _stringValue = null;
    }

    private TitleManagedStatisticValue(string stringValue)
    {
        Type = TitleManagedStatType.String;
        _numberValue = 0;
        _stringValue = stringValue;
    }

    /// <summary>The active value arm.</summary>
    public TitleManagedStatType Type { get; }

    /// <summary>The numeric value.</summary>
    /// <exception cref="InvalidOperationException"><see cref="Type"/> is not <see cref="TitleManagedStatType.Number"/>.</exception>
    public double NumberValue
    {
        get
        {
            if (Type != TitleManagedStatType.Number)
            {
                throw new InvalidOperationException("The statistic value is not numeric.");
            }

            return _numberValue;
        }
    }

    /// <summary>The string value.</summary>
    /// <exception cref="InvalidOperationException"><see cref="Type"/> is not <see cref="TitleManagedStatType.String"/>.</exception>
    public string StringValue
    {
        get
        {
            if (Type != TitleManagedStatType.String)
            {
                throw new InvalidOperationException("The statistic value is not a string.");
            }

            return _stringValue!;
        }
    }

    /// <summary>Creates a numeric statistic value.</summary>
    public static TitleManagedStatisticValue FromNumber(double value) => new(value);

    /// <summary>Creates a string statistic value.</summary>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
    public static TitleManagedStatisticValue FromString(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new TitleManagedStatisticValue(value);
    }

    /// <inheritdoc/>
    public bool Equals(TitleManagedStatisticValue other)
    {
        if (Type != other.Type)
        {
            return false;
        }

        return Type == TitleManagedStatType.Number
            ? _numberValue.Equals(other._numberValue)
            : string.Equals(_stringValue, other._stringValue, StringComparison.Ordinal);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is TitleManagedStatisticValue other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() =>
        Type == TitleManagedStatType.Number
            ? ((int)Type * 397) ^ _numberValue.GetHashCode()
            : ((int)Type * 397) ^ StringComparer.Ordinal.GetHashCode(_stringValue ?? string.Empty);

    /// <inheritdoc/>
    public override string ToString() =>
        Type == TitleManagedStatType.Number ? _numberValue.ToString() : _stringValue ?? string.Empty;

    /// <summary>Equality operator.</summary>
    public static bool operator ==(TitleManagedStatisticValue left, TitleManagedStatisticValue right) =>
        left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(TitleManagedStatisticValue left, TitleManagedStatisticValue right) =>
        !left.Equals(right);
}

/// <summary>A title-managed statistic to write, update or delete.</summary>
public sealed class TitleManagedStatistic
{
    /// <summary>Creates a numeric title-managed statistic.</summary>
    /// <param name="statisticName">The case-insensitive statistic name.</param>
    /// <param name="numberValue">The numeric value to write.</param>
    public TitleManagedStatistic(string statisticName, double numberValue)
        : this(statisticName, TitleManagedStatisticValue.FromNumber(numberValue))
    {
    }

    /// <summary>Creates a string title-managed statistic.</summary>
    /// <param name="statisticName">The case-insensitive statistic name.</param>
    /// <param name="stringValue">The string value to write.</param>
    public TitleManagedStatistic(string statisticName, string stringValue)
        : this(statisticName, TitleManagedStatisticValue.FromString(stringValue))
    {
    }

    /// <summary>Creates a title-managed statistic from a discriminated value.</summary>
    /// <param name="statisticName">The case-insensitive statistic name.</param>
    /// <param name="value">The value to write.</param>
    /// <exception cref="ArgumentException"><paramref name="statisticName"/> is empty.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="statisticName"/> is <see langword="null"/>.</exception>
    public TitleManagedStatistic(string statisticName, TitleManagedStatisticValue value)
    {
        if (statisticName is null)
        {
            throw new ArgumentNullException(nameof(statisticName));
        }

        if (statisticName.Length == 0)
        {
            throw new ArgumentException("A statistic name is required.", nameof(statisticName));
        }

        StatisticName = statisticName;
        Value = value;
    }

    /// <summary>The case-insensitive statistic name.</summary>
    public string StatisticName { get; }

    /// <summary>The statistic value.</summary>
    public TitleManagedStatisticValue Value { get; }
}
