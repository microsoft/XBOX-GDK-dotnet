using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.GameUI;
using GDK.Net.Interop;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the XGameUI interop layer against silent drift from the GDK headers
/// (%GameDKCoreLatest%windows\include\XGameUI.h, edition 260404).
/// Pure compile-time and layout checks — nothing here loads xgameruntime.thunks.dll.
/// All tests that require a live Gaming Runtime belong in <c>eng/run-package-tests.ps1</c>.
/// </summary>
public sealed unsafe class GameUIContractTests
{
    // -----------------------------------------------------------------------
    // Enum values — raw vs. public projection
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(MessageDialogButton.First, 0u)]
    [InlineData(MessageDialogButton.Second, 1u)]
    [InlineData(MessageDialogButton.Third, 2u)]
    public void MessageDialogButtonMatchesTheHeader(MessageDialogButton value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiMessageDialogButton)value);
    }

    [Theory]
    [InlineData(NotificationPositionHint.BottomCenter, 0u)]
    [InlineData(NotificationPositionHint.BottomLeft, 1u)]
    [InlineData(NotificationPositionHint.BottomRight, 2u)]
    [InlineData(NotificationPositionHint.TopCenter, 3u)]
    [InlineData(NotificationPositionHint.TopLeft, 4u)]
    [InlineData(NotificationPositionHint.TopRight, 5u)]
    public void NotificationPositionHintMatchesTheHeader(NotificationPositionHint value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiNotificationPositionHint)value);
    }

    [Theory]
    [InlineData(TextEntryInputScope.Default, 0u)]
    [InlineData(TextEntryInputScope.Url, 1u)]
    [InlineData(TextEntryInputScope.EmailSmtpAddress, 5u)]
    [InlineData(TextEntryInputScope.Number, 29u)]
    [InlineData(TextEntryInputScope.Password, 31u)]
    [InlineData(TextEntryInputScope.TelephoneNumber, 32u)]
    [InlineData(TextEntryInputScope.Alphanumeric, 40u)]
    [InlineData(TextEntryInputScope.Search, 50u)]
    [InlineData(TextEntryInputScope.ChatWithoutEmoji, 68u)]
    public void TextEntryInputScopeMatchesTheHeader(TextEntryInputScope value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiTextEntryInputScope)value);
    }

    [Theory]
    [InlineData(TextEntryChangeTypeFlags.None, 0x0u)]
    [InlineData(TextEntryChangeTypeFlags.TextChanged, 0x1u)]
    [InlineData(TextEntryChangeTypeFlags.Dismissed, 0x2u)]
    public void TextEntryChangeTypeFlagsMatchTheHeader(TextEntryChangeTypeFlags value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiTextEntryChangeTypeFlags)value);
    }

    [Fact]
    public void TextEntryChangeTypeIsAFlagsEnum()
    {
        var combined = TextEntryChangeTypeFlags.TextChanged | TextEntryChangeTypeFlags.Dismissed;
        Assert.Equal(0x3u, (uint)combined);
    }

    [Theory]
    [InlineData(TextEntryVisibilityFlags.Default, 0x0u)]
    [InlineData(TextEntryVisibilityFlags.OnlyShowCandidates, 0x1u)]
    public void TextEntryVisibilityFlagsMatchTheHeader(TextEntryVisibilityFlags value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiTextEntryVisibilityFlags)value);
    }

    [Theory]
    [InlineData(TextEntryPositionHint.Bottom, 0u)]
    [InlineData(TextEntryPositionHint.Top, 1u)]
    public void TextEntryPositionHintMatchesTheHeader(TextEntryPositionHint value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiTextEntryPositionHint)value);
    }

    [Theory]
    [InlineData(WebAuthenticationOptions.None, 0x0u)]
    [InlineData(WebAuthenticationOptions.PreferFullscreen, 0x1u)]
    public void WebAuthenticationOptionsMatchTheHeader(WebAuthenticationOptions value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XGameUiWebAuthenticationOptions)value);
    }

    [Fact]
    public void WebAuthenticationOptionsIsAFlagsEnum()
    {
        Assert.True(Attribute.IsDefined(
            typeof(WebAuthenticationOptions), typeof(FlagsAttribute)));
    }

    // -----------------------------------------------------------------------
    // Struct layout
    // -----------------------------------------------------------------------

    [Fact]
    public void TextEntryOptionsLayoutMatchesTheHeader()
    {
        // struct XGameUiTextEntryOptions { inputScope, positionHint, visibilityFlags } — all uint32
        Assert.Equal(12, Marshal.SizeOf<XGameUiTextEntryOptions>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XGameUiTextEntryOptions>(nameof(XGameUiTextEntryOptions.inputScope)));
        Assert.Equal(4, (int)Marshal.OffsetOf<XGameUiTextEntryOptions>(nameof(XGameUiTextEntryOptions.positionHint)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XGameUiTextEntryOptions>(nameof(XGameUiTextEntryOptions.visibilityFlags)));
    }

    [Fact]
    public void TextEntryExtentsLayoutMatchesTheHeader()
    {
        // struct XGameUiTextEntryExtents { left, top, right, bottom } — all float
        Assert.Equal(16, Marshal.SizeOf<XGameUiTextEntryExtents>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XGameUiTextEntryExtents>(nameof(XGameUiTextEntryExtents.left)));
        Assert.Equal(4, (int)Marshal.OffsetOf<XGameUiTextEntryExtents>(nameof(XGameUiTextEntryExtents.top)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XGameUiTextEntryExtents>(nameof(XGameUiTextEntryExtents.right)));
        Assert.Equal(12, (int)Marshal.OffsetOf<XGameUiTextEntryExtents>(nameof(XGameUiTextEntryExtents.bottom)));
    }

    [Fact]
    public void WebAuthenticationResultDataLayoutMatchesTheHeader()
    {
        // struct XGameUiWebAuthenticationResultData { HRESULT(4), pad, size_t, const char* }
        // x64: int(4) + pad(4) + nuint(8) + ptr(8) = 24 bytes
        // x86: int(4) + nuint(4) + ptr(4)          = 12 bytes
        int expected = IntPtr.Size == 8 ? 24 : 12;
        Assert.Equal(expected, Marshal.SizeOf<XGameUiWebAuthenticationResultData>());
    }

    // -----------------------------------------------------------------------
    // TextEntryOptions public struct
    // -----------------------------------------------------------------------

    [Fact]
    public void TextEntryOptionsDefaultsAreCorrect()
    {
        var opts = new TextEntryOptions(TextEntryInputScope.Search);
        Assert.Equal(TextEntryInputScope.Search, opts.InputScope);
        Assert.Equal(TextEntryPositionHint.Bottom, opts.PositionHint);
        Assert.Equal(TextEntryVisibilityFlags.Default, opts.VisibilityFlags);
    }

    [Fact]
    public void TextEntryOptionsFullConstructorRoundTrips()
    {
        var opts = new TextEntryOptions(
            TextEntryInputScope.Password,
            TextEntryPositionHint.Top,
            TextEntryVisibilityFlags.OnlyShowCandidates);

        Assert.Equal(TextEntryInputScope.Password, opts.InputScope);
        Assert.Equal(TextEntryPositionHint.Top, opts.PositionHint);
        Assert.Equal(TextEntryVisibilityFlags.OnlyShowCandidates, opts.VisibilityFlags);
    }

    // -----------------------------------------------------------------------
    // TextEntryExtents public struct
    // -----------------------------------------------------------------------

    [Fact]
    public void TextEntryExtentsValuesRoundTrip()
    {
        // Internal constructor — reached via GameTextEntry.GetExtents at runtime;
        // test via the internal access granted by InternalsVisibleTo.
        var extents = new TextEntryExtents(0.1f, 0.2f, 0.8f, 0.9f);
        Assert.Equal(0.1f, extents.Left);
        Assert.Equal(0.2f, extents.Top);
        Assert.Equal(0.8f, extents.Right);
        Assert.Equal(0.9f, extents.Bottom);
    }

    // -----------------------------------------------------------------------
    // WebAuthenticationResult
    // -----------------------------------------------------------------------

    [Fact]
    public void WebAuthenticationResultSucceededReflectsResponseStatus()
    {
        var ok = new WebAuthenticationResult(HResult.SOk, "https://example.com/callback");
        Assert.True(ok.Succeeded);
        Assert.Equal("https://example.com/callback", ok.CompletionUri);

        var fail = new WebAuthenticationResult(HResult.EFail, null);
        Assert.False(fail.Succeeded);
        Assert.Null(fail.CompletionUri);
    }

    // -----------------------------------------------------------------------
    // TextEntryState
    // -----------------------------------------------------------------------

    [Fact]
    public void TextEntryStatePropertiesRoundTrip()
    {
        var state = new TextEntryState(
            TextEntryChangeTypeFlags.TextChanged,
            cursorIndex: 3,
            imeClauseStartIndex: 1,
            imeClauseEndIndex: 3,
            text: "abc");

        Assert.Equal(TextEntryChangeTypeFlags.TextChanged, state.ChangeType);
        Assert.Equal(3u, state.CursorIndex);
        Assert.Equal(1u, state.ImeClauseStartIndex);
        Assert.Equal(3u, state.ImeClauseEndIndex);
        Assert.Equal("abc", state.Text);
    }

    // -----------------------------------------------------------------------
    // Argument validation (does NOT call into native code)
    // -----------------------------------------------------------------------

    [Fact]
    public void GameTextEntryThrowsAfterDispose()
    {
        // GameTextEntry's ctor is internal (accessible via InternalsVisibleTo).
        // IntPtr.Zero is safe: Dispose guards the XGameUiTextEntryClose call behind a
        // non-zero check, so no native code is invoked here.
        var entry = new GameTextEntry(IntPtr.Zero);
        entry.Dispose();

        Assert.Throws<ObjectDisposedException>(() => entry.GetExtents());
        Assert.Throws<ObjectDisposedException>(() => entry.GetState());
        Assert.Throws<ObjectDisposedException>(
            () => entry.UpdatePositionHint(TextEntryPositionHint.Bottom));
        Assert.Throws<ObjectDisposedException>(
            () => entry.UpdateVisibility(TextEntryVisibilityFlags.Default));
    }

    [Fact]
    public void GameTextEntryDisposeIsSafeToCallMultipleTimes()
    {
        var entry = new GameTextEntry(IntPtr.Zero);
        entry.Dispose();
        entry.Dispose(); // must not throw
    }
}
