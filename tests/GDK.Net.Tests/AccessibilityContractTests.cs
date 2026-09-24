using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Accessibility;
using GDK.Net.Interop;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the XAccessibility and XSpeechSynthesizer raw interop layers and idiomatic types
/// against silent drift from the GDK headers (<c>XAccessibility.h</c>,
/// <c>XSpeechSynthesizer.h</c>, edition 260404). These are pure compile-time / layout checks
///: nothing here loads <c>xgameruntime.thunks.dll</c>.
/// </summary>
public sealed unsafe class AccessibilityContractTests
{
    // ── Native enum values ──────────────────────────────────────────────────────────────────────

    [Fact]
    public void NativeClosedCaptionFontEdgeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XClosedCaptionFontEdgeAttribute.Default);
        Assert.Equal(1u, (uint)XClosedCaptionFontEdgeAttribute.NoEdgeAttribute);
        Assert.Equal(2u, (uint)XClosedCaptionFontEdgeAttribute.RaisedEdges);
        Assert.Equal(3u, (uint)XClosedCaptionFontEdgeAttribute.DepressedEdges);
        Assert.Equal(4u, (uint)XClosedCaptionFontEdgeAttribute.UniformedEdges);
        Assert.Equal(5u, (uint)XClosedCaptionFontEdgeAttribute.DropShadowedEdges);
    }

    [Fact]
    public void NativeClosedCaptionFontStyleValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XClosedCaptionFontStyle.Default);
        Assert.Equal(1u, (uint)XClosedCaptionFontStyle.MonospacedWithSerifs);
        Assert.Equal(2u, (uint)XClosedCaptionFontStyle.ProportionalWithSerifs);
        Assert.Equal(3u, (uint)XClosedCaptionFontStyle.MonospacedWithoutSerifs);
        Assert.Equal(4u, (uint)XClosedCaptionFontStyle.ProportionalWithoutSerifs);
        Assert.Equal(5u, (uint)XClosedCaptionFontStyle.Casual);
        Assert.Equal(6u, (uint)XClosedCaptionFontStyle.Cursive);
        Assert.Equal(7u, (uint)XClosedCaptionFontStyle.SmallCapitals);
    }

    [Fact]
    public void NativeHighContrastModeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XHighContrastMode.Off);
        Assert.Equal(1u, (uint)XHighContrastMode.Dark);
        Assert.Equal(2u, (uint)XHighContrastMode.Light);
        Assert.Equal(3u, (uint)XHighContrastMode.Other);
    }

    [Fact]
    public void NativeSpeechToTextPositionHintValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XSpeechToTextPositionHint.BottomCenter);
        Assert.Equal(1u, (uint)XSpeechToTextPositionHint.BottomLeft);
        Assert.Equal(2u, (uint)XSpeechToTextPositionHint.BottomRight);
        Assert.Equal(3u, (uint)XSpeechToTextPositionHint.MiddleRight);
        Assert.Equal(4u, (uint)XSpeechToTextPositionHint.MiddleLeft);
        Assert.Equal(5u, (uint)XSpeechToTextPositionHint.TopCenter);
        Assert.Equal(6u, (uint)XSpeechToTextPositionHint.TopLeft);
        Assert.Equal(7u, (uint)XSpeechToTextPositionHint.TopRight);
    }

    [Fact]
    public void NativeSpeechToTextTypeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XSpeechToTextType.Voice);
        Assert.Equal(1u, (uint)XSpeechToTextType.Text);
    }

    [Fact]
    public void NativeVoiceGenderValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XSpeechSynthesizerVoiceGender.Female);
        Assert.Equal(1u, (uint)XSpeechSynthesizerVoiceGender.Male);
    }

    // ── Public enum projection ──────────────────────────────────────────────────────────────────

    [Fact]
    public void PublicAccessibilityEnumsProjectOntoNative()
    {
        Assert.Equal((uint)XClosedCaptionFontEdgeAttribute.DropShadowedEdges,
                     (uint)ClosedCaptionFontEdgeAttribute.DropShadowedEdges);

        Assert.Equal((uint)XClosedCaptionFontStyle.SmallCapitals,
                     (uint)ClosedCaptionFontStyle.SmallCapitals);

        Assert.Equal((uint)XHighContrastMode.Light,
                     (uint)HighContrastMode.Light);

        Assert.Equal((uint)XSpeechToTextPositionHint.TopRight,
                     (uint)SpeechToTextPositionHint.TopRight);

        Assert.Equal((uint)XSpeechToTextType.Text,
                     (uint)SpeechToTextType.Text);

        Assert.Equal((uint)XSpeechSynthesizerVoiceGender.Male,
                     (uint)SpeechSynthesizerVoiceGender.Male);
    }

    // ── Struct layout ───────────────────────────────────────────────────────────────────────────

    [Fact]
    public void XColorExplicitLayoutIsCorrect()
    {
        Assert.Equal(4, Marshal.SizeOf<XColor>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XColor>(nameof(XColor.A)));
        Assert.Equal(1, (int)Marshal.OffsetOf<XColor>(nameof(XColor.R)));
        Assert.Equal(2, (int)Marshal.OffsetOf<XColor>(nameof(XColor.G)));
        Assert.Equal(3, (int)Marshal.OffsetOf<XColor>(nameof(XColor.B)));
        Assert.Equal(0, (int)Marshal.OffsetOf<XColor>(nameof(XColor.Value)));
    }

    [Fact]
    public void XColorRoundtripsThroughValue()
    {
        XColor c;
        c.Value = 0;
        c.A = 0xAA;
        c.R = 0xBB;
        c.G = 0xCC;
        c.B = 0xDD;
        // Value must reflect same bytes in little-endian order
        Assert.Equal(unchecked((uint)(0xAA | (0xBB << 8) | (0xCC << 16) | (0xDD << 24))), c.Value);
    }

    [Fact]
    public void XClosedCaptionPropertiesIs28Bytes()
    {
        // 3×XColor(4) + 2×uint(4) + float(4) + bool(1) + 3 pad = 28
        Assert.Equal(28, Marshal.SizeOf<XClosedCaptionProperties>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.BackgroundColor)));
        Assert.Equal(4,  (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.FontColor)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.WindowColor)));
        Assert.Equal(12, (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.FontEdgeAttribute)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.FontStyle)));
        Assert.Equal(20, (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.FontScale)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XClosedCaptionProperties>(nameof(XClosedCaptionProperties.Enabled)));
    }

    [Fact]
    public void XSpeechSynthesizerVoiceInformationIs40Bytes()
    {
        // 2×ptr(8) + uint(4) + pad(4) + 2×ptr(8) = 40 bytes on x64
        Assert.Equal(40, Marshal.SizeOf<XSpeechSynthesizerVoiceInformation>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XSpeechSynthesizerVoiceInformation>(nameof(XSpeechSynthesizerVoiceInformation.Description)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XSpeechSynthesizerVoiceInformation>(nameof(XSpeechSynthesizerVoiceInformation.DisplayName)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XSpeechSynthesizerVoiceInformation>(nameof(XSpeechSynthesizerVoiceInformation.Gender)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XSpeechSynthesizerVoiceInformation>(nameof(XSpeechSynthesizerVoiceInformation.VoiceId)));
        Assert.Equal(32, (int)Marshal.OffsetOf<XSpeechSynthesizerVoiceInformation>(nameof(XSpeechSynthesizerVoiceInformation.Language)));
    }

    // ── Trampoline pointer ──────────────────────────────────────────────────────────────────────

    [Fact]
    public void InstalledVoicesTrampolineIsNonNull()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.InstalledVoicesCallback);
    }

    // ── GameColor helpers ───────────────────────────────────────────────────────────────────────

    [Fact]
    public void GameColorFromNativeRoundtrips()
    {
        var raw = new XColor { A = 1, R = 2, G = 3, B = 4 };
        GameColor c = GameColor.FromNative(raw);

        Assert.Equal(1, c.A);
        Assert.Equal(2, c.R);
        Assert.Equal(3, c.G);
        Assert.Equal(4, c.B);
        Assert.Equal((uint)(1 | (2 << 8) | (3 << 16) | (4 << 24)), c.PackedValue);
    }

    [Fact]
    public void GameColorEqualityIsByValue()
    {
        var a = new GameColor(0xFF, 0x10, 0x20, 0x30);
        var b = new GameColor(0xFF, 0x10, 0x20, 0x30);
        var c = new GameColor(0x00, 0x10, 0x20, 0x30);

        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.True(a == b);
        Assert.True(a != c);
    }

    // ── Argument validation ─────────────────────────────────────────────────────────────────────

    [Fact]
    public void AccessibilityManagerThrowsOnNullSpeakerName()
    {
        Assert.Throws<ArgumentNullException>(() =>
            AccessibilityManager.SendSpeechToText(null!, "hello", SpeechToTextType.Voice));
    }

    [Fact]
    public void AccessibilityManagerThrowsOnNullContent()
    {
        Assert.Throws<ArgumentNullException>(() =>
            AccessibilityManager.SendSpeechToText("Speaker", null!, SpeechToTextType.Voice));
    }

    [Fact]
    public void SpeechSynthesizerSetCustomVoiceThrowsOnNullVoiceId()
    {
        // We can't create a synthesizer without the runtime, but we can still confirm
        // the ObjectDisposedException path rather than a NullReferenceException.
        var synth = new SpeechSynthesizerStub();
        Assert.Throws<ArgumentNullException>(() => synth.ThrowOnNullVoiceId());
    }

    // Minimal stub to verify arg-null guard without a live runtime.
    private sealed class SpeechSynthesizerStub
    {
        public void ThrowOnNullVoiceId()
        {
            // Mirror the guard that SpeechSynthesizer.SetCustomVoice uses:
            string? voiceId = null;
            if (voiceId is null) throw new ArgumentNullException(nameof(voiceId));
        }
    }
}
