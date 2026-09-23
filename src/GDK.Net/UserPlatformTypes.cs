using System;

namespace GDK.Net.Users;

/// <summary>
/// How the title resolved a "signed in on another device" (SPOP) prompt. Mirrors
/// <c>XUserPlatformSpopOperationResult</c>; passed to <see cref="SpopPromptEventArgs.Complete"/>.
/// </summary>
public enum SpopOperationResult
{
    /// <summary>The user chose to keep this device signed in and sign the other device out.</summary>
    SignInHere = 0,

    /// <summary>The user chose to switch to a different account on this device.</summary>
    SwitchAccount = 1,

    /// <summary>The prompt could not be shown or failed.</summary>
    Failure = 2,

    /// <summary>The user dismissed the prompt without choosing.</summary>
    Canceled = 3,
}

/// <summary>
/// The Gaming Runtime is asking the title to display a remote-connect prompt: the user should visit
/// <see cref="Url"/> on a second device and enter <see cref="Code"/> to finish signing in.
/// </summary>
/// <remarks>
/// The prompt stays up until either <see cref="UserPlatform.RemoteConnectClosePrompt"/> is
/// raised for the same <see cref="Operation"/>, or the title calls <see cref="Cancel"/>.
/// </remarks>
public sealed class RemoteConnectShowPromptEventArgs : EventArgs
{
    private readonly IntPtr _operation;

    internal RemoteConnectShowPromptEventArgs(uint userIdentifier, IntPtr operation, string url, string code, byte[] qrCode)
    {
        UserIdentifier = userIdentifier;
        _operation = operation;
        Url = url;
        Code = code;
        QrCode = qrCode;
    }

    /// <summary>The runtime's identifier for the user this prompt belongs to.</summary>
    public uint UserIdentifier { get; }

    /// <summary>The URL the user should open on their second device.</summary>
    public string Url { get; }

    /// <summary>The short code the user types at <see cref="Url"/>.</summary>
    public string Code { get; }

    /// <summary>
    /// An image of a QR code encoding <see cref="Url"/>, in whatever format the runtime supplied.
    /// Empty when the runtime did not provide one.
    /// </summary>
    /// <remarks>
    /// This is a private copy taken before the callback returned; the native buffer is only valid
    /// for the duration of the callback, so the bytes are safe to keep.
    /// </remarks>
    public byte[] QrCode { get; }

    /// <summary>The opaque operation token that correlates this prompt with its close notification.</summary>
    internal IntPtr Operation => _operation;

    /// <summary>
    /// Abandons the sign-in this prompt belongs to
    /// (<c>XUserPlatformRemoteConnectCancelPrompt</c>). Call this if the user dismisses the prompt.
    /// </summary>
    public void Cancel() => UserPlatform.CancelRemoteConnect(_operation);
}

/// <summary>
/// The Gaming Runtime has finished with a remote-connect prompt and the title should take it down.
/// </summary>
public sealed class RemoteConnectClosePromptEventArgs : EventArgs
{
    internal RemoteConnectClosePromptEventArgs(uint userIdentifier, IntPtr operation)
    {
        UserIdentifier = userIdentifier;
        Operation = operation;
    }

    /// <summary>The runtime's identifier for the user this prompt belonged to.</summary>
    public uint UserIdentifier { get; }

    /// <summary>
    /// The opaque operation token, matching the one carried by the
    /// <see cref="UserPlatform.RemoteConnectShowPrompt"/> event that opened this prompt.
    /// </summary>
    internal IntPtr Operation { get; }

    /// <summary>
    /// Whether this notification closes the prompt opened by <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The event args from the matching show notification.</param>
    /// <exception cref="ArgumentNullException"><paramref name="request"/> is <see langword="null"/>.</exception>
    public bool Matches(RemoteConnectShowPromptEventArgs request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return request.Operation == Operation;
    }
}

/// <summary>
/// The Gaming Runtime is asking the title to display a "signed in on another device" (SPOP) prompt
/// and report back what the user chose.
/// </summary>
/// <remarks>
/// The runtime blocks the sign-in until the title calls <see cref="Complete"/>. Always call it,
/// including on the failure paths, or the sign-in never resolves.
/// </remarks>
public sealed class SpopPromptEventArgs : EventArgs
{
    private readonly IntPtr _operation;

    internal SpopPromptEventArgs(uint userIdentifier, IntPtr operation, string gamertag, string? gamertagSuffix)
    {
        UserIdentifier = userIdentifier;
        _operation = operation;
        ModernGamertag = gamertag;
        ModernGamertagSuffix = gamertagSuffix;
    }

    /// <summary>The runtime's identifier for the user this prompt belongs to.</summary>
    public uint UserIdentifier { get; }

    /// <summary>The modern gamertag of the account already signed in elsewhere.</summary>
    public string ModernGamertag { get; }

    /// <summary>
    /// The numeric suffix that disambiguates <see cref="ModernGamertag"/>, or
    /// <see langword="null"/> when the gamertag is unique on its own.
    /// </summary>
    public string? ModernGamertagSuffix { get; }

    /// <summary>
    /// Reports the user's choice back to the Gaming Runtime
    /// (<c>XUserPlatformSpopPromptComplete</c>).
    /// </summary>
    /// <param name="result">What the user chose.</param>
    public void Complete(SpopOperationResult result) => UserPlatform.CompleteSpop(_operation, result);
}
