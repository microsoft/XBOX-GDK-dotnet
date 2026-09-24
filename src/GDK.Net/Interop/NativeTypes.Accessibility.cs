// Raw interop types for XAccessibility.h and XSpeechSynthesizer.h (GDK edition 260404).
// See Interop/NativeTypes.cs for conventions.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

// --- XAccessibility.h: closed captions ---

internal enum XClosedCaptionFontEdgeAttribute : uint
{
    Default           = 0,
    NoEdgeAttribute   = 1,
    RaisedEdges       = 2,
    DepressedEdges    = 3,
    UniformedEdges    = 4,
    DropShadowedEdges = 5,
}

internal enum XClosedCaptionFontStyle : uint
{
    Default                   = 0,
    MonospacedWithSerifs      = 1,
    ProportionalWithSerifs    = 2,
    MonospacedWithoutSerifs   = 3,
    ProportionalWithoutSerifs = 4,
    Casual                    = 5,
    Cursive                   = 6,
    SmallCapitals             = 7,
}

/// <summary>
/// Mirrors the <c>XColor</c> union from XGameRuntimeTypes.h.
/// Explicit layout exposes both the individual A/R/G/B byte channels and the packed
/// <see cref="Value"/> at the same offset. Size: 4 bytes.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal struct XColor
{
    [FieldOffset(0)] public byte A;
    [FieldOffset(1)] public byte R;
    [FieldOffset(2)] public byte G;
    [FieldOffset(3)] public byte B;
    [FieldOffset(0)] public uint Value;
}

/// <summary>
/// Mirrors <c>XClosedCaptionProperties</c> from XAccessibility.h.
/// The trailing <c>bool Enabled</c> is modelled as <c>byte</c> for blittability
/// (<c>bool</c> in a C++ struct is 1 byte). Sequential layout; total size: 28 bytes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XClosedCaptionProperties
{
    public XColor                         BackgroundColor;
    public XColor                         FontColor;
    public XColor                         WindowColor;
    public XClosedCaptionFontEdgeAttribute FontEdgeAttribute;
    public XClosedCaptionFontStyle         FontStyle;
    public float                           FontScale;
    public byte                            Enabled;  // bool in the header; byte here for blittability
}

// --- XAccessibility.h: high contrast ---

internal enum XHighContrastMode : uint
{
    Off   = 0,
    Dark  = 1,
    Light = 2,
    Other = 3,
}

// --- XAccessibility.h: speech-to-text ---

internal enum XSpeechToTextPositionHint : uint
{
    BottomCenter = 0,
    BottomLeft   = 1,
    BottomRight  = 2,
    MiddleRight  = 3,
    MiddleLeft   = 4,
    TopCenter    = 5,
    TopLeft      = 6,
    TopRight     = 7,
}

internal enum XSpeechToTextType : uint
{
    Voice = 0,
    Text  = 1,
}

// --- XSpeechSynthesizer.h ---

internal enum XSpeechSynthesizerVoiceGender : uint
{
    Female = 0,
    Male   = 1,
}

/// <summary>
/// Mirrors <c>XSpeechSynthesizerVoiceInformation</c> from XSpeechSynthesizer.h.
/// All string pointer fields point into memory owned by the GDK runtime; copy them before
/// the enumerating call returns. Natural padding is inserted before <c>VoiceId</c> so the
/// pointer aligns to 8 bytes. Total size: 40 bytes on x64/arm64.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XSpeechSynthesizerVoiceInformation
{
    public byte* Description;   // const char*: copied to managed string in the trampoline
    public byte* DisplayName;
    public XSpeechSynthesizerVoiceGender Gender;
    // 4 bytes natural padding here for 8-byte alignment of the next pointer field
    public byte* VoiceId;
    public byte* Language;
}
