using System;
using System.Threading;

namespace GDK.Net;

/// <summary>
/// Raised when a GDK call returns a failing HRESULT.
/// </summary>
/// <remarks>
/// Binary serialization is deliberately not supported: it is obsolete on modern .NET and the
/// exception carries only a numeric HRESULT, which round-trips through <see cref="HResultCode"/>.
/// </remarks>
public class GameRuntimeException : Exception
{
    /// <summary>Initialises a new exception for <paramref name="hresult"/>.</summary>
    /// <param name="hresult">The failing HRESULT.</param>
    public GameRuntimeException(int hresult)
        : this(hresult, GDK.Net.HResult.Describe(hresult))
    {
    }

    /// <summary>Initialises a new exception for <paramref name="hresult"/> with a custom message.</summary>
    /// <param name="hresult">The failing HRESULT.</param>
    /// <param name="message">The exception message.</param>
    public GameRuntimeException(int hresult, string message)
        : base(message)
    {
        HResultCode = hresult;
        HResult = hresult;
    }

    /// <summary>Initialises a new exception for <paramref name="hresult"/> with a custom message and inner exception.</summary>
    /// <param name="hresult">The failing HRESULT.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The exception that caused this failure.</param>
    public GameRuntimeException(int hresult, string message, Exception innerException)
        : base(message, innerException)
    {
        HResultCode = hresult;
        HResult = hresult;
    }

    /// <summary>The raw HRESULT returned by the GDK.</summary>
    public int HResultCode { get; }
}

/// <summary>
/// Raised for the <c>E_GAMEUSER_*</c> family so callers can catch user-identity failures
/// (wrong sandbox, signed out, no package identity, …) without inspecting HRESULT values.
/// </summary>
public class UserException : GameRuntimeException
{
    /// <summary>Initialises a new user exception for <paramref name="hresult"/>.</summary>
    /// <param name="hresult">The failing HRESULT.</param>
    public UserException(int hresult)
        : base(hresult)
    {
    }

    /// <summary>Initialises a new user exception for <paramref name="hresult"/> with a custom message.</summary>
    /// <param name="hresult">The failing HRESULT.</param>
    /// <param name="message">The exception message.</param>
    public UserException(int hresult, string message)
        : base(hresult, message)
    {
    }

    /// <summary>Initialises a new user exception for <paramref name="hresult"/> with a custom message and inner exception.</summary>
    /// <param name="hresult">The failing HRESULT.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The exception that caused this failure.</param>
    public UserException(int hresult, string message, Exception innerException)
        : base(hresult, message, innerException)
    {
    }
}

/// <summary>
/// HRESULT checking helpers. Every HRESULT-returning P/Invoke in this projection is funnelled
/// through <see cref="ThrowIfFailed(int, CancellationToken)"/>.
/// </summary>
public static class Hr
{
    /// <summary>
    /// Throws the mapped exception when <paramref name="hresult"/> denotes failure.
    /// <c>E_ABORT</c> always surfaces as <see cref="OperationCanceledException"/> so a canceled
    /// operation never reaches a caller as a runtime fault.
    /// </summary>
    public static void ThrowIfFailed(int hresult, CancellationToken cancellationToken = default)
    {
        if (hresult >= 0)
        {
            return;
        }

        throw ToException(hresult, cancellationToken);
    }

    /// <summary>Maps a failing HRESULT onto the exception this projection would throw for it.</summary>
    public static Exception ToException(int hresult, CancellationToken cancellationToken = default)
    {
        if (hresult == HResult.EAbort)
        {
            return new OperationCanceledException(cancellationToken);
        }

        if (PlayFab.PlayFabErrors.IsPlayFab(hresult))
        {
            return new PlayFab.PlayFabException(hresult);
        }

        return HResult.IsGameUser(hresult)
            ? new UserException(hresult)
            : new GameRuntimeException(hresult);
    }
}
