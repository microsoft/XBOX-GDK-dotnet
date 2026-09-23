// Raw interop types for XGameUI.h (GDK edition 260404).
// See Interop/NativeTypes.cs for the shared naming conventions.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

internal enum XGameUiMessageDialogButton : uint
{
    First = 0,
    Second = 1,
    Third = 2,
}

internal enum XGameUiNotificationPositionHint : uint
{
    BottomCenter = 0,
    BottomLeft = 1,
    BottomRight = 2,
    TopCenter = 3,
    TopLeft = 4,
    TopRight = 5,
}

internal enum XGameUiTextEntryInputScope : uint
{
    Default = 0,
    Url = 1,
    EmailSmtpAddress = 5,
    Number = 29,
    Password = 31,
    TelephoneNumber = 32,
    Alphanumeric = 40,
    Search = 50,
    ChatWithoutEmoji = 68,
}

[Flags]
internal enum XGameUiTextEntryChangeTypeFlags : uint
{
    None = 0x0,
    TextChanged = 0x1,
    Dismissed = 0x2,
}

[Flags]
internal enum XGameUiTextEntryVisibilityFlags : uint
{
    Default = 0x0,
    OnlyShowCandidates = 0x1,
}

internal enum XGameUiTextEntryPositionHint : uint
{
    Bottom = 0,
    Top = 1,
}

[Flags]
internal enum XGameUiWebAuthenticationOptions : uint
{
    None = 0x00,
    PreferFullscreen = 0x01,
}

/// <summary>Mirrors <c>struct XGameUiTextEntryOptions</c> from XGameUI.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XGameUiTextEntryOptions
{
    public XGameUiTextEntryInputScope inputScope;
    public XGameUiTextEntryPositionHint positionHint;
    public XGameUiTextEntryVisibilityFlags visibilityFlags;
}

/// <summary>Mirrors <c>struct XGameUiTextEntryExtents</c> from XGameUI.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XGameUiTextEntryExtents
{
    public float left;
    public float top;
    public float right;
    public float bottom;
}

/// <summary>Mirrors <c>struct XGameUiWebAuthenticationResultData</c> from XGameUI.h.</summary>
/// <remarks>
/// <c>responseCompletionUri</c> points into the caller-supplied result buffer; it must be copied
/// before the buffer is freed or unpinned.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XGameUiWebAuthenticationResultData
{
    public int responseStatus;
    public nuint responseCompletionUriSize;
    public byte* responseCompletionUri;
}

/// <summary>Mirrors <c>struct XGameUiPlayerPickerInfo</c> from XGameUI.h.</summary>
/// <remarks>
/// Delivered by pointer to <c>XGameUiShowPlayerPickerUiCallback</c> and owned by the Gaming
/// Runtime: every field is only valid for the duration of that callback, so the strings and player
/// arrays must be copied out before it returns.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XGameUiPlayerPickerInfo
{
    public IntPtr requestingUser;
    public byte* promptText;
    public uint selectFromPlayersCount;
    public ulong* selectFromPlayers;
    public uint preSelectedPlayersCount;
    public ulong* preSelectedPlayers;
    public uint minSelectionCount;
    public uint maxSelectionCount;
}

/// <summary>Mirrors <c>struct XGameUiUiCallbacks</c> from XGameUI.h.</summary>
/// <remarks>
/// <para>
/// Every callback field is a <c>__stdcall</c> function pointer. They are typed as
/// <see cref="IntPtr"/> rather than as <c>delegate* unmanaged[Stdcall]</c> so that one struct
/// definition serves both marshalling models: net8.0+ stores the address of an
/// <c>[UnmanagedCallersOnly]</c> method, netstandard2.0 stores
/// <c>Marshal.GetFunctionPointerForDelegate</c> of a rooted delegate. This matches how
/// <c>XErrorSetCallback</c> and the other callback registrations are declared.
/// </para>
/// <para>
/// A null field tells the Gaming Runtime the title does not implement that particular UI, which is
/// how <see cref="GDK.Net.GameUI.CustomGameUi"/> supports partial handler sets.
/// </para>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct XGameUiUiCallbacks
{
    public IntPtr context;
    public IntPtr showPlayerProfileCardCallback;
    public IntPtr showPlayerPickerCallback;
    public IntPtr showSendGameInviteCallback;
    public IntPtr showAchievementsCallback;
    public IntPtr showMultiplayerActivityGameInviteCallback;
    public IntPtr showMessageDialogCallback;
    public IntPtr showErrorDialogCallback;
    public IntPtr showTextEntryCallback;
}
