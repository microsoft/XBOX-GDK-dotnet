using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net;
using GDK.Net.PlayFab;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the shape of the PlayFab projection. Almost all of it is generated from the GDK headers
/// by <c>eng/playfab/</c>, so these are the checks that a regenerated layer still honours
/// <c>eng/interop-conventions.md</c> without needing a console or a signed-in account.
/// </summary>
public class PlayFabContractTests
{
    private static Assembly Library => typeof(GameRuntime).Assembly;

    private static Type NativePlayFab =>
        Library.GetType("GDK.Net.Interop.NativePlayFab", throwOnError: true)!;

    /// <summary>The PlayFab extension libraries this projection is allowed to bind to.</summary>
    private static readonly string[] PlayFabModules =
    [
        "PlayFabCore.dll",
        "PlayFabServices.dll",
        "PlayFabGameSave.dll",
        "PlayFabMultiplayer.dll",
        "Party.dll",
        "PartyXboxLive.dll",
    ];

    [Fact]
    public void EveryPlayFabEntryPointTargetsAKnownModule()
    {
        // A stray module name compiles but fails at load with a DllNotFoundException that names
        // only the missing file, so catch it here instead.
        var declared = NativePlayFab
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(m => m.GetCustomAttribute<DllImportAttribute>())
            .Where(a => a is not null)
            .Select(a => a!.Value)
            .ToList();

        Assert.NotEmpty(declared);

        var strays = declared
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Where(v => !PlayFabModules.Contains(v, StringComparer.OrdinalIgnoreCase))
            .ToList();

        Assert.Empty(strays);
    }

    [Fact]
    public void EveryPlayFabModuleIsActuallyBound()
    {
        // The six DLLs are the whole point of the projection; if a regeneration drops one, the
        // matching family silently disappears from the public surface.
        var bound = NativePlayFab
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(m => m.GetCustomAttribute<DllImportAttribute>())
            .Where(a => a is not null)
            .Select(a => a!.Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        Assert.All(PlayFabModules, module => Assert.Contains(module, bound));
    }

    [Fact]
    public void ErrorConstantsAreAllInThePlayFabFacility()
    {
        var codes = PlayFabErrorConstants().ToList();

        // The header defines roughly a thousand E_PF_* codes; a parser regression usually shows up
        // as a near-empty file rather than as a wrong value.
        Assert.True(codes.Count > 900, $"Only {codes.Count} E_PF_* constants were generated.");
        Assert.All(codes, code => Assert.True(
            PlayFabErrors.IsPlayFab(code.Value),
            $"PlayFabErrors.{code.Key} = 0x{code.Value:X8} is outside facility 0x8923."));
    }

    [Fact]
    public void EveryErrorConstantHasASymbolicName()
    {
        Assert.All(PlayFabErrorConstants(), code => Assert.StartsWith(
            "E_PF_",
            PlayFabErrors.GetName(code.Value),
            StringComparison.Ordinal));
    }

    [Fact]
    public void UnknownCodesHaveNoName()
    {
        Assert.Null(PlayFabErrors.GetName(0));
        Assert.Null(PlayFabErrors.GetName(unchecked((int)0x8923FFFF)));
    }

    [Fact]
    public void FacilityIsClassifiedAsPlayFab()
    {
        Assert.True(PlayFabErrors.IsPlayFab(unchecked((int)0x89235400)));
        Assert.False(PlayFabErrors.IsPlayFab(unchecked((int)0x89245100))); // XUser
        Assert.False(PlayFabErrors.IsPlayFab(HResult.EAbort));
    }

    [Fact]
    public void FailingPlayFabHResultBecomesAPlayFabException()
    {
        // Hr.ToException is the single funnel every projected call goes through, so the mapping
        // has to hold there rather than only in the PlayFab layer.
        Exception mapped = Hr.ToException(PlayFabErrors.CoreNotInitialized);

        var playFab = Assert.IsType<PlayFabException>(mapped);
        Assert.Equal(PlayFabErrors.CoreNotInitialized, playFab.HResultCode);
        Assert.Equal("E_PF_CORE_NOT_INITIALIZED", playFab.ErrorName);
        Assert.Contains("E_PF_CORE_NOT_INITIALIZED", playFab.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void UnknownPlayFabCodeStillReportsItsHResult()
    {
        var playFab = new PlayFabException(unchecked((int)0x8923FFFF));

        Assert.Null(playFab.ErrorName);
        Assert.Contains("0x8923FFFF", playFab.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void PublicPlayFabSurfaceExposesNoInteropTypes()
    {
        // No HRESULT, handle, XAsyncBlock or native pointer may reach a caller
        // (eng/interop-conventions.md).
        var offenders = new List<string>();

        foreach (Type type in PublicPlayFabTypes())
        {
            foreach (MemberInfo member in type.GetMembers(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                foreach ((string what, Type candidate) in SignatureTypes(member))
                {
                    if (IsInterop(candidate))
                    {
                        offenders.Add($"{type.FullName}.{member.Name} exposes {what} {candidate.Name}.");
                    }
                }
            }
        }

        Assert.Empty(offenders);
    }

    [Fact]
    public void GeneratedServicesAreAsynchronous()
    {
        // The Services layer is a straight projection of PFxxxAsync; a synchronous method on a
        // generated service class means the generator emitted a blocking wait on an XAsyncBlock.
        // The hand-written core (PlayFabRuntime, PlayFabGameSaveFiles, ...) is excluded: it wraps
        // genuinely synchronous entry points too, and is named with the PlayFab prefix.
        var services = PublicPlayFabTypes()
            .Where(t => t.IsAbstract && t.IsSealed && t.Namespace == "GDK.Net.PlayFab")
            .Where(t => !t.Name.StartsWith("PlayFab", StringComparison.Ordinal))
            .Where(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                         .Any(m => typeof(Task).IsAssignableFrom(m.ReturnType)))
            .ToList();

        // Twenty-odd service families are projected (AccountManagement, Catalog, Inventory, ...).
        Assert.True(services.Count >= 15, $"Only {services.Count} PlayFab service classes were found.");

        var synchronous = services
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(m => !m.IsSpecialName)
            .Where(m => !typeof(Task).IsAssignableFrom(m.ReturnType))
            .Select(m => $"{m.DeclaringType!.Name}.{m.Name}")
            .ToList();

        Assert.Empty(synchronous);
    }

    [Fact]
    public void EveryServiceCallTakesACancellationToken()
    {
        // A PlayFab call is a network round trip; a title must be able to abandon one.
        var offenders = PublicPlayFabTypes()
            .Where(t => t.IsAbstract && t.IsSealed && t.Namespace == "GDK.Net.PlayFab")
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(m => typeof(Task).IsAssignableFrom(m.ReturnType))
            .Where(m => !m.GetParameters().Any(p => p.ParameterType == typeof(CancellationToken)))
            .Select(m => $"{m.DeclaringType!.Name}.{m.Name}")
            .ToList();

        Assert.Empty(offenders);
    }

    [Fact]
    public void GameSaveCallbacksUseUnmanagedCallersOnly()
    {
        // PlayFab GameSave hands native code a function pointer; a delegate there needs a
        // runtime-generated reverse stub, which ILC cannot produce.
        Type trampolines = Library.GetType("GDK.Net.Interop.Trampolines", throwOnError: false)
            ?? Library.GetType("GDK.Net.Interop.TrampolinesPlayFabGameSave", throwOnError: false)
            ?? Library.GetTypes().First(t =>
                t.Namespace == "GDK.Net.Interop" &&
                t.Name.StartsWith("Trampolines", StringComparison.Ordinal));

        Assert.NotNull(trampolines);

        var callbacks = Library
            .GetTypes()
            .Where(t => t.Namespace == "GDK.Net.Interop" && t.Name.StartsWith("Trampolines", StringComparison.Ordinal))
            .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(m => m.GetCustomAttributes().Any(a => a.GetType().Name == "UnmanagedCallersOnlyAttribute"))
            .ToList();

        Assert.NotEmpty(callbacks);
    }

    internal static IEnumerable<Type> PublicPlayFabTypes() =>
        Library
            .GetExportedTypes()
            .Where(t => t.Namespace is not null &&
                        (t.Namespace == "GDK.Net.PlayFab" || t.Namespace.StartsWith("GDK.Net.PlayFab.", StringComparison.Ordinal)));

    private static IEnumerable<KeyValuePair<string, int>> PlayFabErrorConstants() =>
        typeof(PlayFabErrors)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.IsLiteral && f.FieldType == typeof(int))
            .Select(f => new KeyValuePair<string, int>(f.Name, (int)f.GetRawConstantValue()!));

    internal static IEnumerable<(string What, Type Type)> SignatureTypes(MemberInfo member)
    {
        switch (member)
        {
            case MethodInfo method:
                yield return ("return type", method.ReturnType);
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    yield return ($"parameter '{parameter.Name}'", parameter.ParameterType);
                }

                break;

            case PropertyInfo property:
                yield return ("property type", property.PropertyType);
                break;

            case FieldInfo field:
                yield return ("field type", field.FieldType);
                break;

            case ConstructorInfo constructor:
                foreach (ParameterInfo parameter in constructor.GetParameters())
                {
                    yield return ($"parameter '{parameter.Name}'", parameter.ParameterType);
                }

                break;
        }
    }

    internal static bool IsInterop(Type candidate)
    {
        Type target = candidate;
        while (target.IsByRef || target.IsPointer || target.IsArray)
        {
            target = target.GetElementType()!;
        }

        if (target.IsGenericType)
        {
            return target.GetGenericArguments().Any(IsInterop);
        }

        return target.Namespace is "GDK.Net.Interop"
            || typeof(SafeHandle).IsAssignableFrom(target)
            || target == typeof(IntPtr)
            || target == typeof(UIntPtr);
    }
}
