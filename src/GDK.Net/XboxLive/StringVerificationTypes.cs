using System;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Result code returned by Xbox Live string verification.</summary>
public enum VerifyStringResultCode : uint
{
    /// <summary>Xbox Live found no issues with the string.</summary>
    Success = 0,

    /// <summary>The string contains offensive content and must not be displayed.</summary>
    Offensive = 1,

    /// <summary>The string is too long for Xbox Live to verify and must not be displayed.</summary>
    TooLong = 2,

    /// <summary>The verification could not be completed; the string must not be displayed.</summary>
    UnknownError = 3,
}

/// <summary>Managed result of verifying one user-generated string with Xbox Live.</summary>
/// <remarks>
/// String verification is certification-sensitive: user-generated text such as gamertags in chat
/// or custom object names must be verified before display. This type is fail-closed by
/// construction: <see cref="IsAcceptable"/> can only be <see langword="true"/> when
/// <see cref="WasVerified"/> is <see langword="true"/> and Xbox Live explicitly returned
/// <see cref="VerifyStringResultCode.Success"/>. Failed, canceled or incomplete checks always
/// produce an unacceptable result.
/// </remarks>
public sealed class StringVerificationResult
{
    internal StringVerificationResult(
        string verifiedString,
        bool wasVerified,
        VerifyStringResultCode resultCode,
        string? firstOffendingSubstring)
    {
        VerifiedString = verifiedString;
        WasVerified = wasVerified;
        ResultCode = wasVerified ? resultCode : VerifyStringResultCode.UnknownError;
        FirstOffendingSubstring = firstOffendingSubstring;
    }

    /// <summary>The input string this result corresponds to.</summary>
    public string VerifiedString { get; }

    /// <summary>
    /// Whether Xbox Live completed the verification. If this is <see langword="false"/>,
    /// <see cref="IsAcceptable"/> is guaranteed to be <see langword="false"/>.
    /// </summary>
    public bool WasVerified { get; }

    /// <summary>
    /// Whether the string is acceptable for display. This is fail-closed and is
    /// <see langword="true"/> only for a completed verification whose <see cref="ResultCode"/> is
    /// <see cref="VerifyStringResultCode.Success"/>.
    /// </summary>
    public bool IsAcceptable => WasVerified && ResultCode == VerifyStringResultCode.Success;

    /// <summary>The Xbox Live result code for the verification.</summary>
    public VerifyStringResultCode ResultCode { get; }

    /// <summary>
    /// The first offending substring when <see cref="ResultCode"/> is
    /// <see cref="VerifyStringResultCode.Offensive"/>; otherwise <see langword="null"/>.
    /// </summary>
    public string? FirstOffendingSubstring { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{ResultCode}: {VerifiedString}";

    internal static unsafe StringVerificationResult FromNative(string verifiedString, XblVerifyStringResult* native)
    {
        if (native is null)
        {
            return FailClosed(verifiedString);
        }

        return new StringVerificationResult(
            verifiedString,
            wasVerified: true,
            resultCode: (VerifyStringResultCode)native->ResultCode,
            firstOffendingSubstring: Utf8.ToString(native->FirstOffendingSubstring));
    }

    internal static StringVerificationResult FailClosed(string verifiedString) =>
        new(
            verifiedString,
            wasVerified: false,
            resultCode: VerifyStringResultCode.UnknownError,
            firstOffendingSubstring: null);
}
