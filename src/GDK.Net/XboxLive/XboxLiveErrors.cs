using System;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Actionable Xbox Live error condition buckets. Mirrors <c>XblErrorCondition</c>.</summary>
public enum ErrorCondition : uint
{
    /// <summary>No error.</summary>
    NoError = 0,

    /// <summary>A generic error condition.</summary>
    GenericError = 1,

    /// <summary>An object or value was out of range.</summary>
    GenericOutOfRange = 2,

    /// <summary>Authentication failed or must be refreshed.</summary>
    Auth = 3,

    /// <summary>Network connectivity failed.</summary>
    Network = 4,

    /// <summary>A generic HTTP failure occurred.</summary>
    HttpGeneric = 5,

    /// <summary>The resource was not modified.</summary>
    Http304NotModified = 6,

    /// <summary>The resource was not found.</summary>
    Http404NotFound = 7,

    /// <summary>An HTTP precondition failed.</summary>
    Http412PreconditionFailed = 8,

    /// <summary>The caller is being rate limited.</summary>
    Http429TooManyRequests = 9,

    /// <summary>The service timed out while processing the request.</summary>
    HttpServiceTimeout = 10,

    /// <summary>A real-time activity error occurred.</summary>
    Rta = 11,
}

/// <summary>Helpers for mapping Xbox Live HRESULTs to actionable error conditions.</summary>
public static class XboxLiveErrors
{
    /// <summary>
    /// Maps an XSAPI HRESULT to an actionable condition (<c>XblGetErrorCondition</c>).
    /// </summary>
    /// <param name="hresult">The HRESULT returned by Xbox Live.</param>
    public static ErrorCondition GetCondition(int hresult) =>
        (ErrorCondition)NativeXbl.XblGetErrorCondition(hresult);

    /// <summary>
    /// Maps the HRESULT carried by a thrown <see cref="GameRuntimeException"/> to an actionable
    /// condition (<c>XblGetErrorCondition</c>).
    /// </summary>
    /// <param name="exception">The exception thrown by <see cref="Hr.ThrowIfFailed(int, System.Threading.CancellationToken)"/>.</param>
    /// <exception cref="ArgumentNullException"><paramref name="exception"/> is <see langword="null"/>.</exception>
    public static ErrorCondition GetCondition(GameRuntimeException exception)
    {
        if (exception is null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        return GetCondition(exception.HResultCode);
    }

    /// <summary>
    /// Maps the HRESULT carried by a thrown <see cref="GameRuntimeException"/> to an actionable
    /// condition (<c>XblGetErrorCondition</c>).
    /// </summary>
    /// <param name="exception">The Xbox Live exception to classify.</param>
    /// <exception cref="ArgumentNullException"><paramref name="exception"/> is <see langword="null"/>.</exception>
    public static ErrorCondition GetXboxLiveErrorCondition(this GameRuntimeException exception) =>
        GetCondition(exception);
}
