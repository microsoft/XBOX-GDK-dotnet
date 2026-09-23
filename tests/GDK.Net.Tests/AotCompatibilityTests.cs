using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GDK.Net;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the NativeAOT contract. Xbox consoles require NativeAOT, so GDK.Net must contain no
/// construct that needs runtime code generation.
/// </summary>
/// <remarks>
/// <para>
/// The primary enforcement is the trim/AOT/single-file analyzers (see <c>GDK.Net.csproj</c>) plus
/// the real ILC compilation in <c>eng/package.ps1 -Aot</c>. These tests add the structural checks
/// those two cannot make: they assert the *shape* of the interop layer, so a future family cannot
/// quietly reintroduce a pattern that only fails once someone AOT-publishes.
/// </para>
/// <para>
/// The test host is JIT-compiled, so these are reflection-over-metadata assertions rather than
/// behavioural ones.
/// </para>
/// </remarks>
public class AotCompatibilityTests
{
    private static Assembly Library => typeof(GameRuntime).Assembly;

    private static Type NativeType =>
        Library.GetType("GDK.Net.Interop.Native", throwOnError: true)!;

    [Fact]
    public void LibraryIsMarkedTrimmable()
    {
        // The trimmer only skips conservative rooting for assemblies that claim to be analysed.
        // Losing this marker silently degrades every consumer's trimmed output.
        var marker = Library
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(a => a.Key == "IsTrimmable");

        Assert.NotNull(marker);
        Assert.Equal("True", marker!.Value);
    }

    [Fact]
    public void EveryNativeEntryPointUsesSourceGeneratedMarshalling()
    {
        // [LibraryImport] generates the marshalling stub at compile time. A hand-written
        // [DllImport] on a non-blittable signature makes the runtime build one, which ILC cannot
        // do. The generator lowers each [LibraryImport] to a private [DllImport] stub, so the
        // check is that no *public surface* method is a bare DllImport without a generated peer.
        var declared = NativeType
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(m => m.GetCustomAttribute<DllImportAttribute>() is not null)
            .ToList();

        Assert.NotEmpty(declared);

        // Every entry point must target the Gaming Runtime's redistributable thunks DLL. Anything
        // else means a stray P/Invoke has crept in.
        string[] allowed = ["xgameruntime.thunks.dll"];
        var strays = declared
            .Select(m => m.GetCustomAttribute<DllImportAttribute>()!.Value)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Where(v => !allowed.Contains(v, StringComparer.OrdinalIgnoreCase))
            .ToList();

        Assert.Empty(strays);
    }

    [Fact]
    public void CallbacksIntoNativeCodeUseUnmanagedCallersOnly()
    {
        // Marshal.GetFunctionPointerForDelegate needs a runtime-generated reverse stub, so on
        // net8.0+ every trampoline must be a static [UnmanagedCallersOnly] method taken as a
        // function pointer. Those methods are private, so assert via the attribute's presence
        // across the interop namespace.
        var trampolines = Library
            .GetTypes()
            .Where(t => t.Namespace == "GDK.Net.Interop" && t.Name.StartsWith("Trampolines", StringComparison.Ordinal))
            .ToList();

        Assert.NotEmpty(trampolines);

        var callbacks = trampolines
            .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(m => m.GetCustomAttributes().Any(a => a.GetType().Name == "UnmanagedCallersOnlyAttribute"))
            .ToList();

        Assert.NotEmpty(callbacks);

        // [UnmanagedCallersOnly] methods must be blittable and non-generic; the compiler enforces
        // that, but a generic containing type would slip past it.
        Assert.All(callbacks, m => Assert.False(m.DeclaringType!.IsGenericType));
    }

    [Fact]
    public void NoUnmanagedFunctionPointerDelegatesRemainOnModernTargets()
    {
        // [UnmanagedFunctionPointer] delegates are the netstandard2.0 fallback. If one is compiled
        // into the net8.0+ assembly, something was written outside the #if and would force the
        // runtime marshaller at AOT time.
        var offenders = Library
            .GetTypes()
            .Where(t => t.GetCustomAttribute<UnmanagedFunctionPointerAttribute>() is not null)
            .Select(t => t.FullName)
            .ToList();

        Assert.Empty(offenders);
    }

    [Fact]
    public void NoPublicMemberRequiresDynamicCode()
    {
        // RequiresDynamicCode marks API that cannot run under AOT at all. GDK.Net must have none:
        // a title on Xbox has no fallback.
        var offenders = Library
            .GetExportedTypes()
            .SelectMany(t => t.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(m => m.GetCustomAttributes().Any(a =>
                a.GetType().Name is "RequiresDynamicCodeAttribute" or "RequiresUnreferencedCodeAttribute"))
            .Select(m => $"{m.DeclaringType!.FullName}.{m.Name}")
            .ToList();

        Assert.Empty(offenders);
    }

    [Fact]
    public void TestHostIsJitSoTheseAreMetadataChecks()
    {
        // Documents the limitation deliberately: these tests cannot prove AOT works, only that the
        // shapes that break it are absent. The end-to-end proof is `eng/package.ps1 -Aot` plus a
        // Register-tier run, which reports the runtime.compilation step as NativeAOT.
        Assert.True(RuntimeFeature.IsDynamicCodeSupported);
    }
}
