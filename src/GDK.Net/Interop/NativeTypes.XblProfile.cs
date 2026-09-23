// Blittable mirrors of the XSAPI profile types -- xsapi-c\profile_c.h, GDK edition 260404.
//
// See NativeTypes.Xbl.cs for the layout rules that apply across the XSAPI type mirrors.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>
/// Mirrors <c>XblUserProfile</c>. Every string is a fixed-size inline UTF-8 buffer, not a pointer,
/// so the struct is copied by value and stays valid after the async block is freed.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblUserProfile
{
    internal const int DisplayNameCharSize = 30 * 3;
    internal const int DisplayPicUrlRawCharSize = 225 * 3;
    internal const int GamerscoreCharSize = 16 * 3;
    internal const int GamertagCharSize = 16 * 3;
    internal const int ModernGamertagCharSize = ((12 + 12) * 4) + 1;
    internal const int ModernGamertagSuffixCharSize = 14 + 1;
    internal const int UniqueModernGamertagCharSize = ModernGamertagCharSize + 1 + 3;

    internal ulong XboxUserId;
    internal fixed byte AppDisplayName[DisplayNameCharSize];
    internal fixed byte AppDisplayPictureResizeUri[DisplayPicUrlRawCharSize];
    internal fixed byte GameDisplayName[DisplayNameCharSize];
    internal fixed byte GameDisplayPictureResizeUri[DisplayPicUrlRawCharSize];
    internal fixed byte Gamerscore[GamerscoreCharSize];
    internal fixed byte Gamertag[GamertagCharSize];
    internal fixed byte ModernGamertag[ModernGamertagCharSize];
    internal fixed byte ModernGamertagSuffix[ModernGamertagSuffixCharSize];
    internal fixed byte UniqueModernGamertag[UniqueModernGamertagCharSize];
}
