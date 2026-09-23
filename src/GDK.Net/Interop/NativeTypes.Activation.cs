// Raw interop types for XGameActivation.h (GDK edition 260404).
// See Interop/NativeTypes.cs for conventions.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XGameActivationType</c> from XGameActivation.h.</summary>
internal enum XGameActivationType : uint
{
    Protocol = 0,
    File = 1,
    PendingGameInvite = 2,
    AcceptedGameInvite = 3,
}

/// <summary>Mirrors <c>struct XGameActivationInfo</c> from XGameActivation.h.</summary>
/// <remarks>
/// The native struct ends in an anonymous union of <c>protocolUri</c>, <c>file</c> and
/// <c>inviteUri</c>. All three members are <c>const char*</c>, so the union collapses to a single
/// pointer field here; <see cref="Type"/> says which name applies. Sequential layout reproduces the
/// C struct exactly, including the padding the compiler inserts between the 4-byte enum and the
/// pointer on 64-bit targets.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XGameActivationInfo
{
    public XGameActivationType Type;
    public byte* Uri;
}
