using System;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

namespace GDK.Net.Accessibility;

/// <summary>
/// Accessibility settings: closed captions, high contrast, and speech-to-text.
/// </summary>
/// <remarks>
/// These APIs are stateless and always available after <c>GameRuntime.Initialize()</c>;
/// no separate subsystem initialization is required. All methods are static.
/// </remarks>
public static unsafe class AccessibilityManager
{
    // ── Closed captions ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the system closed-caption display properties set by the user
    /// (<c>XClosedCaptionGetProperties</c>).
    /// </summary>
    public static ClosedCaptionProperties GetClosedCaptionProperties()
    {
        XClosedCaptionProperties raw;
        Hr.ThrowIfFailed(Native.XClosedCaptionGetProperties(&raw));
        return new ClosedCaptionProperties(in raw);
    }

    /// <summary>
    /// Overrides the closed-caption enabled flag (<c>XClosedCaptionSetEnabled</c>).
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable closed captions.</param>
    public static void SetClosedCaptionEnabled(bool enabled)
    {
        Hr.ThrowIfFailed(Native.XClosedCaptionSetEnabled(enabled ? (byte)1 : (byte)0));
    }

    // ── High contrast ───────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the system high-contrast mode selected by the user
    /// (<c>XHighContrastGetMode</c>).
    /// </summary>
    public static HighContrastMode GetHighContrastMode()
    {
        XHighContrastMode mode;
        Hr.ThrowIfFailed(Native.XHighContrastGetMode(&mode));
        return (HighContrastMode)mode;
    }

    // ── Speech-to-text ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Sets the screen position of the speech-to-text overlay
    /// (<c>XSpeechToTextSetPositionHint</c>).
    /// </summary>
    public static void SetSpeechToTextPositionHint(SpeechToTextPositionHint position)
    {
        Hr.ThrowIfFailed(Native.XSpeechToTextSetPositionHint((XSpeechToTextPositionHint)position));
    }

    /// <summary>
    /// Sends a finalized speech-to-text string for display
    /// (<c>XSpeechToTextSendString</c>).
    /// </summary>
    /// <param name="speakerName">Display name for the speaker.</param>
    /// <param name="content">The text to display.</param>
    /// <param name="type">Whether the text came from voice or was entered manually.</param>
    public static void SendSpeechToText(string speakerName, string content, SpeechToTextType type)
    {
        if (speakerName is null) throw new ArgumentNullException(nameof(speakerName));
        if (content     is null) throw new ArgumentNullException(nameof(content));

        IntPtr speakerPtr = Utf8.Allocate(speakerName);
        IntPtr contentPtr = Utf8.Allocate(content);
        try
        {
            Hr.ThrowIfFailed(Native.XSpeechToTextSendString(
                (byte*)speakerPtr,
                (byte*)contentPtr,
                (XSpeechToTextType)type));
        }
        finally
        {
            Utf8.Free(contentPtr);
            Utf8.Free(speakerPtr);
        }
    }

    /// <summary>
    /// Begins a rolling hypothesis speech-to-text string
    /// (<c>XSpeechToTextBeginHypothesisString</c>).
    /// </summary>
    /// <returns>A hypothesis id to pass to <see cref="UpdateHypothesisString"/>, <see cref="FinalizeHypothesisString"/>, or <see cref="CancelHypothesisString"/>.</returns>
    public static uint BeginHypothesisString(
        string speakerName,
        string content,
        SpeechToTextType type)
    {
        if (speakerName is null) throw new ArgumentNullException(nameof(speakerName));
        if (content     is null) throw new ArgumentNullException(nameof(content));

        IntPtr speakerPtr = Utf8.Allocate(speakerName);
        IntPtr contentPtr = Utf8.Allocate(content);
        try
        {
            uint hypothesisId;
            Hr.ThrowIfFailed(Native.XSpeechToTextBeginHypothesisString(
                (byte*)speakerPtr,
                (byte*)contentPtr,
                (XSpeechToTextType)type,
                &hypothesisId));
            return hypothesisId;
        }
        finally
        {
            Utf8.Free(contentPtr);
            Utf8.Free(speakerPtr);
        }
    }

    /// <summary>
    /// Updates a rolling hypothesis string (<c>XSpeechToTextUpdateHypothesisString</c>).
    /// </summary>
    public static void UpdateHypothesisString(uint hypothesisId, string content)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));
        IntPtr contentPtr = Utf8.Allocate(content);
        try
        {
            Hr.ThrowIfFailed(Native.XSpeechToTextUpdateHypothesisString(
                hypothesisId, (byte*)contentPtr));
        }
        finally
        {
            Utf8.Free(contentPtr);
        }
    }

    /// <summary>
    /// Finalizes a rolling hypothesis string as the confirmed transcription
    /// (<c>XSpeechToTextFinalizeHypothesisString</c>).
    /// </summary>
    public static void FinalizeHypothesisString(uint hypothesisId, string content)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));
        IntPtr contentPtr = Utf8.Allocate(content);
        try
        {
            Hr.ThrowIfFailed(Native.XSpeechToTextFinalizeHypothesisString(
                hypothesisId, (byte*)contentPtr));
        }
        finally
        {
            Utf8.Free(contentPtr);
        }
    }

    /// <summary>
    /// Cancels a rolling hypothesis string without finalizing it
    /// (<c>XSpeechToTextCancelHypothesisString</c>).
    /// </summary>
    public static void CancelHypothesisString(uint hypothesisId)
    {
        Hr.ThrowIfFailed(Native.XSpeechToTextCancelHypothesisString(hypothesisId));
    }
}
