using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

namespace GDK.Net.Accessibility;

/// <summary>
/// Speech synthesizer. Wraps <c>XSpeechSynthesizerHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// Create an instance with the default constructor, optionally call
/// <see cref="SetDefaultVoice"/> or <see cref="SetCustomVoice"/> to choose a voice, then call
/// <see cref="SynthesizeText"/> or <see cref="SynthesizeSsml"/> to produce PCM audio.
/// </para>
/// <para>
/// <see cref="GetInstalledVoices"/> is a static helper that does not require a synthesizer
/// instance.
/// </para>
/// </remarks>
public sealed unsafe class SpeechSynthesizer : IDisposable
{
    private readonly SpeechSynthesizerHandle _handle;
    private bool _disposed;

    /// <summary>
    /// Creates a new speech synthesizer (<c>XSpeechSynthesizerCreate</c>).
    /// </summary>
    public SpeechSynthesizer()
    {
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XSpeechSynthesizerCreate(&raw));
        _handle = new SpeechSynthesizerHandle(raw);
    }

    // ── Static helpers ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns information about every installed speech-synthesis voice
    /// (<c>XSpeechSynthesizerEnumerateInstalledVoices</c>).
    /// Does not require a <see cref="SpeechSynthesizer"/> instance.
    /// </summary>
    public static IReadOnlyList<SpeechSynthesizerVoiceInfo> GetInstalledVoices()
    {
        var voices = new List<SpeechSynthesizerVoiceInfo>();
        var handle = GCHandle.Alloc(voices);
        try
        {
            Hr.ThrowIfFailed(Native.XSpeechSynthesizerEnumerateInstalledVoices(
                GCHandle.ToIntPtr(handle),
                Trampolines.InstalledVoicesCallback));
        }
        finally
        {
            handle.Free();
        }

        return voices;
    }

    // ── Voice selection ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Selects the system default voice for this synthesizer
    /// (<c>XSpeechSynthesizerSetDefaultVoice</c>).
    /// </summary>
    public void SetDefaultVoice()
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(Native.XSpeechSynthesizerSetDefaultVoice(_handle.DangerousGetHandle()));
    }

    /// <summary>
    /// Selects a specific installed voice by id (<c>XSpeechSynthesizerSetCustomVoice</c>).
    /// </summary>
    /// <param name="voiceId">The <see cref="SpeechSynthesizerVoiceInfo.VoiceId"/> of the desired voice.</param>
    public void SetCustomVoice(string voiceId)
    {
        if (voiceId is null) throw new ArgumentNullException(nameof(voiceId));
        ThrowIfDisposed();

        IntPtr voiceIdPtr = Utf8.Allocate(voiceId);
        try
        {
            Hr.ThrowIfFailed(Native.XSpeechSynthesizerSetCustomVoice(
                _handle.DangerousGetHandle(), (byte*)voiceIdPtr));
        }
        finally
        {
            Utf8.Free(voiceIdPtr);
        }
    }

    // ── Synthesis ───────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Synthesizes plain text to a PCM audio byte array
    /// (<c>XSpeechSynthesizerCreateStreamFromText</c> + <c>XSpeechSynthesizerGetStreamData</c>).
    /// </summary>
    /// <param name="text">The text to synthesize.</param>
    /// <returns>Raw PCM audio bytes.</returns>
    public byte[] SynthesizeText(string text)
    {
        if (text is null) throw new ArgumentNullException(nameof(text));
        ThrowIfDisposed();

        IntPtr textPtr = Utf8.Allocate(text);
        try
        {
            IntPtr stream;
            Hr.ThrowIfFailed(Native.XSpeechSynthesizerCreateStreamFromText(
                _handle.DangerousGetHandle(), (byte*)textPtr, &stream));
            return ReadStream(stream);
        }
        finally
        {
            Utf8.Free(textPtr);
        }
    }

    /// <summary>
    /// Synthesizes SSML markup to a PCM audio byte array
    /// (<c>XSpeechSynthesizerCreateStreamFromSsml</c> + <c>XSpeechSynthesizerGetStreamData</c>).
    /// </summary>
    /// <param name="ssml">SSML markup string.</param>
    /// <returns>Raw PCM audio bytes.</returns>
    public byte[] SynthesizeSsml(string ssml)
    {
        if (ssml is null) throw new ArgumentNullException(nameof(ssml));
        ThrowIfDisposed();

        IntPtr ssmlPtr = Utf8.Allocate(ssml);
        try
        {
            IntPtr stream;
            Hr.ThrowIfFailed(Native.XSpeechSynthesizerCreateStreamFromSsml(
                _handle.DangerousGetHandle(), (byte*)ssmlPtr, &stream));
            return ReadStream(stream);
        }
        finally
        {
            Utf8.Free(ssmlPtr);
        }
    }

    // ── Dispose ──────────────────────────────────────────────────────────────────────────────────

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    // ── Internal: called from Trampolines.Accessibility.cs ──────────────────────────────────────

    /// <summary>
    /// Appends a voice-information entry to the list held in <paramref name="context"/>.
    /// Called from the static <c>Trampolines.InstalledVoicesCallback</c> thunk.
    /// </summary>
    internal static void AddVoiceToList(
        XSpeechSynthesizerVoiceInformation* info,
        IntPtr context)
    {
        var handle = GCHandle.FromIntPtr(context);
        if (handle.Target is not List<SpeechSynthesizerVoiceInfo> list)
        {
            return;
        }

        list.Add(new SpeechSynthesizerVoiceInfo(
            description: Utf8.ToString(info->Description) ?? string.Empty,
            displayName: Utf8.ToString(info->DisplayName) ?? string.Empty,
            gender:      (SpeechSynthesizerVoiceGender)info->Gender,
            voiceId:     Utf8.ToString(info->VoiceId)     ?? string.Empty,
            language:    Utf8.ToString(info->Language)    ?? string.Empty));
    }

    // ── Private helpers ──────────────────────────────────────────────────────────────────────────

    private static byte[] ReadStream(IntPtr streamHandle)
    {
        // Two-call pattern: get size, then get data.
        try
        {
            nuint bufferSize;
            Hr.ThrowIfFailed(Native.XSpeechSynthesizerGetStreamDataSize(streamHandle, &bufferSize));

            if (bufferSize == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] buffer = new byte[(int)bufferSize];
            nuint used;
            fixed (byte* ptr = buffer)
            {
                Hr.ThrowIfFailed(Native.XSpeechSynthesizerGetStreamData(
                    streamHandle, bufferSize, ptr, &used));
            }

            return (int)used == buffer.Length ? buffer : Truncate(buffer, (int)used);
        }
        finally
        {
            Native.XSpeechSynthesizerCloseStreamHandle(streamHandle);
        }
    }

    private static byte[] Truncate(byte[] buffer, int length)
    {
        byte[] result = new byte[length];
        Array.Copy(buffer, result, length);
        return result;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(SpeechSynthesizer));
        }
    }
}
