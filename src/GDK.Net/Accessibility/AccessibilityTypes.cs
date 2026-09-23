using System;
using GDK.Net.Interop;

namespace GDK.Net.Accessibility;

// --- XAccessibility.h: closed captions ---

/// <summary>Font edge style for closed captions. Mirrors <c>XClosedCaptionFontEdgeAttribute</c>.</summary>
public enum ClosedCaptionFontEdgeAttribute : uint
{
    /// <summary>Use the system default.</summary>
    Default           = 0,

    /// <summary>Render captions without a font edge effect.</summary>
    NoEdgeAttribute   = 1,

    /// <summary>Render captions with raised font edges.</summary>
    RaisedEdges       = 2,

    /// <summary>Render captions with depressed font edges.</summary>
    DepressedEdges    = 3,

    /// <summary>Render captions with uniform font edges.</summary>
    UniformedEdges    = 4,

    /// <summary>Render captions with drop-shadowed font edges.</summary>
    DropShadowedEdges = 5,
}

/// <summary>Font style for closed captions. Mirrors <c>XClosedCaptionFontStyle</c>.</summary>
public enum ClosedCaptionFontStyle : uint
{
    /// <summary>Use the system default.</summary>
    Default                   = 0,

    /// <summary>Use a monospaced font with serifs.</summary>
    MonospacedWithSerifs      = 1,

    /// <summary>Use a proportional font with serifs.</summary>
    ProportionalWithSerifs    = 2,

    /// <summary>Use a monospaced font without serifs.</summary>
    MonospacedWithoutSerifs   = 3,

    /// <summary>Use a proportional font without serifs.</summary>
    ProportionalWithoutSerifs = 4,

    /// <summary>Use a casual font style.</summary>
    Casual                    = 5,

    /// <summary>Use a cursive font style.</summary>
    Cursive                   = 6,

    /// <summary>Use a small-capitals font style.</summary>
    SmallCapitals             = 7,
}

/// <summary>
/// An ARGB colour used in closed-caption properties.
/// Mirrors the <c>XColor</c> union from XGameRuntimeTypes.h.
/// </summary>
public readonly struct GameColor : IEquatable<GameColor>
{
    /// <summary>Initializes a colour from individual channel bytes.</summary>
    public GameColor(byte a, byte r, byte g, byte b)
    {
        A = a; R = r; G = g; B = b;
    }

    /// <summary>Initializes a colour from a packed ARGB <c>uint</c>.</summary>
    public GameColor(uint argb)
    {
        A = (byte)(argb & 0xFF);
        R = (byte)((argb >> 8) & 0xFF);
        G = (byte)((argb >> 16) & 0xFF);
        B = (byte)((argb >> 24) & 0xFF);
    }

    internal static GameColor FromNative(XColor raw) => new GameColor(raw.A, raw.R, raw.G, raw.B);

    /// <summary>Alpha channel (0 = fully transparent, 255 = fully opaque).</summary>
    public byte A { get; }

    /// <summary>Red channel.</summary>
    public byte R { get; }

    /// <summary>Green channel.</summary>
    public byte G { get; }

    /// <summary>Blue channel.</summary>
    public byte B { get; }

    /// <summary>The colour packed as a little-endian ARGB <c>uint</c> (matching the native wire format).</summary>
    public uint PackedValue => (uint)(A | (R << 8) | (G << 16) | (B << 24));

    /// <inheritdoc/>
    public bool Equals(GameColor other) => A == other.A && R == other.R && G == other.G && B == other.B;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is GameColor c && Equals(c);

    /// <inheritdoc/>
    public override int GetHashCode() => (int)PackedValue;

    /// <summary>Returns the colour formatted as <c>#AARRGGBB</c>.</summary>
    public override string ToString() => $"#{A:X2}{R:X2}{G:X2}{B:X2}";

    /// <summary>Equality operator.</summary>
    public static bool operator ==(GameColor left, GameColor right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(GameColor left, GameColor right) => !left.Equals(right);
}

/// <summary>
/// Closed-caption display settings from the system (<c>XClosedCaptionGetProperties</c>).
/// Mirrors <c>XClosedCaptionProperties</c>.
/// </summary>
public readonly struct ClosedCaptionProperties
{
    internal ClosedCaptionProperties(in XClosedCaptionProperties raw)
    {
        BackgroundColor    = GameColor.FromNative(raw.BackgroundColor);
        FontColor          = GameColor.FromNative(raw.FontColor);
        WindowColor        = GameColor.FromNative(raw.WindowColor);
        FontEdgeAttribute  = (ClosedCaptionFontEdgeAttribute)raw.FontEdgeAttribute;
        FontStyle          = (ClosedCaptionFontStyle)raw.FontStyle;
        FontScale          = raw.FontScale;
        IsEnabled          = raw.Enabled != 0;
    }

    /// <summary>Caption background colour.</summary>
    public GameColor BackgroundColor { get; }

    /// <summary>Caption font colour.</summary>
    public GameColor FontColor { get; }

    /// <summary>Caption window colour.</summary>
    public GameColor WindowColor { get; }

    /// <summary>Font edge rendering style.</summary>
    public ClosedCaptionFontEdgeAttribute FontEdgeAttribute { get; }

    /// <summary>Font style.</summary>
    public ClosedCaptionFontStyle FontStyle { get; }

    /// <summary>Font scale factor (1.0 = 100%).</summary>
    public float FontScale { get; }

    /// <summary><see langword="true"/> when closed captions are enabled by the user.</summary>
    public bool IsEnabled { get; }
}

// --- XAccessibility.h: high contrast ---

/// <summary>High-contrast mode setting. Mirrors <c>XHighContrastMode</c>.</summary>
public enum HighContrastMode : uint
{
    /// <summary>High contrast is off.</summary>
    Off   = 0,

    /// <summary>A dark high-contrast theme is active.</summary>
    Dark  = 1,

    /// <summary>A light high-contrast theme is active.</summary>
    Light = 2,

    /// <summary>A high-contrast mode other than dark or light is active.</summary>
    Other = 3,
}

// --- XAccessibility.h: speech-to-text ---

/// <summary>
/// Screen position hint for speech-to-text overlay. Mirrors <c>XSpeechToTextPositionHint</c>.
/// </summary>
public enum SpeechToTextPositionHint : uint
{
    /// <summary>Prefer the bottom-centre of the screen.</summary>
    BottomCenter = 0,

    /// <summary>Prefer the bottom-left corner of the screen.</summary>
    BottomLeft   = 1,

    /// <summary>Prefer the bottom-right corner of the screen.</summary>
    BottomRight  = 2,

    /// <summary>Prefer the middle-right edge of the screen.</summary>
    MiddleRight  = 3,

    /// <summary>Prefer the middle-left edge of the screen.</summary>
    MiddleLeft   = 4,

    /// <summary>Prefer the top-centre of the screen.</summary>
    TopCenter    = 5,

    /// <summary>Prefer the top-left corner of the screen.</summary>
    TopLeft      = 6,

    /// <summary>Prefer the top-right corner of the screen.</summary>
    TopRight     = 7,
}

/// <summary>The type of speech-to-text string. Mirrors <c>XSpeechToTextType</c>.</summary>
public enum SpeechToTextType : uint
{
    /// <summary>The string was generated from voice input.</summary>
    Voice = 0,

    /// <summary>The string was manually entered as text.</summary>
    Text  = 1,
}

// --- XSpeechSynthesizer.h ---

/// <summary>Voice gender for speech synthesis. Mirrors <c>XSpeechSynthesizerVoiceGender</c>.</summary>
public enum SpeechSynthesizerVoiceGender : uint
{
    /// <summary>A female voice.</summary>
    Female = 0,

    /// <summary>A male voice.</summary>
    Male   = 1,
}

/// <summary>
/// Information about an installed speech-synthesis voice, returned by
/// <see cref="SpeechSynthesizer.GetInstalledVoices"/>.
/// Mirrors <c>XSpeechSynthesizerVoiceInformation</c>.
/// </summary>
public sealed class SpeechSynthesizerVoiceInfo
{
    internal SpeechSynthesizerVoiceInfo(
        string description,
        string displayName,
        SpeechSynthesizerVoiceGender gender,
        string voiceId,
        string language)
    {
        Description = description;
        DisplayName = displayName;
        Gender      = gender;
        VoiceId     = voiceId;
        Language    = language;
    }

    /// <summary>Human-readable description of the voice.</summary>
    public string Description { get; }

    /// <summary>Display name of the voice.</summary>
    public string DisplayName { get; }

    /// <summary>Voice gender.</summary>
    public SpeechSynthesizerVoiceGender Gender { get; }

    /// <summary>Unique voice identifier, passed to <c>XSpeechSynthesizerSetCustomVoice</c>.</summary>
    public string VoiceId { get; }

    /// <summary>BCP-47 language tag (e.g. <c>"en-US"</c>).</summary>
    public string Language { get; }

    /// <summary>Returns the display name, language and gender for diagnostics.</summary>
    public override string ToString() => $"{DisplayName} ({Language}, {Gender})";
}
