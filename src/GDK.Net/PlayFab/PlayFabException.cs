using System;

namespace GDK.Net.PlayFab;

/// <summary>
/// Raised when a PlayFab call fails, so a title can catch PlayFab failures without inspecting
/// HRESULT values.
/// </summary>
/// <remarks>
/// The message names the <c>E_PF_*</c> symbol when this GDK edition defines one; unknown codes still
/// carry the raw HRESULT, because the service can return codes newer than the installed headers.
/// </remarks>
public class PlayFabException : GameRuntimeException
{
    /// <summary>Initialises a new PlayFab exception for <paramref name="hresult"/>.</summary>
    /// <param name="hresult">The failing PlayFab HRESULT.</param>
    public PlayFabException(int hresult)
        : base(hresult, Describe(hresult))
    {
    }

    /// <summary>Initialises a new PlayFab exception for <paramref name="hresult"/> with a custom message.</summary>
    /// <param name="hresult">The failing PlayFab HRESULT.</param>
    /// <param name="message">The exception message.</param>
    public PlayFabException(int hresult, string message)
        : base(hresult, message)
    {
    }

    /// <summary>Initialises a new PlayFab exception for <paramref name="hresult"/> with a custom message and inner exception.</summary>
    /// <param name="hresult">The failing PlayFab HRESULT.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The exception that caused this failure.</param>
    public PlayFabException(int hresult, string message, Exception innerException)
        : base(hresult, message, innerException)
    {
    }

    /// <summary>The <c>E_PF_*</c> symbol for this failure, or <see langword="null"/> when unknown.</summary>
    public string? ErrorName => PlayFabErrors.GetName(HResultCode);

    private static string Describe(int hresult)
    {
        string? name = PlayFabErrors.GetName(hresult);
        return name is null
            ? $"The PlayFab call failed with HRESULT 0x{hresult:X8}."
            : $"The PlayFab call failed with {name} (0x{hresult:X8}).";
    }
}
