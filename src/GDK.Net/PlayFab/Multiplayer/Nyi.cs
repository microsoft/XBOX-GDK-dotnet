using System;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// Guards the PFMP entry points the shipped GDK declares but does not implement.
/// </summary>
/// <remarks>
/// <para>
/// The Microsoft GDK 260404 PFMP headers mark a set of functions <c>&lt;nyi /&gt;</c> -- not yet
/// implemented. The exports are present in <c>PlayFabMultiplayerGDK.dll</c> and link fine, so
/// nothing catches the mistake at build time, but there is no implementation behind them. Calling
/// one does not return a failure HRESULT; it faults. On .NET that surfaces as an
/// <see cref="AccessViolationException"/> that cannot be caught and takes the process down without
/// unwinding, which is a miserable thing to debug from a title's crash dump.
/// </para>
/// <para>
/// Every one of them is the <c>*WithEntityHandle</c> form of a call that also has a
/// <c>PFEntityKey</c> form, and the key form works. The projection therefore keeps the handle
/// overloads -- they are part of the header surface, and they will start working when the GDK
/// implements them -- but turns the fault into an ordinary, catchable
/// <see cref="PlatformNotSupportedException"/> that names the alternative.
/// </para>
/// </remarks>
internal static class Nyi
{
    /// <summary>
    /// Throws because <paramref name="api"/> is declared but unimplemented in this GDK edition.
    /// </summary>
    /// <param name="api">The native entry point, for the message.</param>
    /// <param name="instead">What the caller should use in its place.</param>
    internal static void Throw(string api, string instead) =>
        throw new PlatformNotSupportedException(
            $"{api} is declared by the Microsoft GDK PFMP headers but is not implemented in this " +
            $"edition -- the header marks it <nyi />, and calling it faults the process rather " +
            $"than returning a failure. Use {instead} instead. The entity's token must first be " +
            "given to PFMP with PlayFabMultiplayer.SetEntityToken.");
}
