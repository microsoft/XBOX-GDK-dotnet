using System;
using GDK.Net;
using GDK.Net.Users;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// End-to-end checks against the real binding.
/// </summary>
/// <remarks>
/// <para>
/// A live GDK exercise needs a packaged title, a signed-in account and a real sandbox
/// (docs/plan.md section 10), none of which exist in a unit-test host. What these tests do assert is
/// the contract that holds everywhere: the projection never leaks a raw marshalling failure.
/// </para>
/// <para>
/// Both environments are covered. xgameruntime.thunks.dll is redistributed inside a package layout
/// rather than installed system-wide, so a unit-test host never has it on the loader path: the
/// binding must translate <see cref="DllNotFoundException"/> into
/// <c>E_GAMERUNTIME_DLL_NOT_FOUND</c>. Where the DLL is reachable the runtime instead rejects the
/// unpackaged process with an <c>E_GAMERUNTIME_*</c> / <c>E_GAMEUSER_*</c> code. Either way the
/// caller sees a <see cref="GameRuntimeException"/>. Live packaged validation is
/// <c>eng/run-package-tests.ps1</c>, not this suite.
/// </para>
/// </remarks>
[Collection(RuntimeCollection.Name)]
public sealed class RuntimeSmokeTests
{
    [Fact]
    public void InitializeOutsideAPackagedTitleFailsAsAGameRuntimeException()
    {
        GameRuntime? runtime = null;
        try
        {
            // Any exception type other than GameRuntimeException fails the test — in particular a
            // raw DllNotFoundException or EntryPointNotFoundException escaping the interop layer.
            runtime = GameRuntime.Initialize();
        }
        catch (GameRuntimeException exception)
        {
            Assert.True(HResult.Failed(exception.HResultCode));
            Assert.False(string.IsNullOrWhiteSpace(exception.Message));
            return;
        }
        finally
        {
            runtime?.Dispose();
        }

        // Reached only when the test host really is a packaged GDK title.
        Assert.NotNull(runtime);
    }

    [Fact]
    public void LocalIdIsValueEqualAndHashesStably()
    {
        var a = new UserLocalId(0x1234_5678_9ABC_DEF0);
        var b = new UserLocalId(0x1234_5678_9ABC_DEF0);
        var c = new UserLocalId(1);

        Assert.True(a == b);
        Assert.False(a == c);
        Assert.True(a != c);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.Equal(a, (object)b);
    }

    [Fact]
    public void NullLocalIdIsRecognised()
    {
        Assert.True(UserLocalId.Null.IsNull);
        Assert.False(new UserLocalId(1).IsNull);
        Assert.Equal(0UL, UserLocalId.Null.Value);
    }

    [Fact]
    public void ChangedEventArgsCarryTheLocalIdEvenWithoutAResolvedUser()
    {
        var args = CreateArgs(new UserLocalId(42), UserChangeEvent.SignedOut);

        Assert.Equal(new UserLocalId(42), args.LocalId);
        Assert.Equal(UserChangeEvent.SignedOut, args.Change);
        Assert.Null(args.User);
    }

    private static UserChangedEventArgs CreateArgs(UserLocalId localId, UserChangeEvent change)
        => (UserChangedEventArgs)Activator.CreateInstance(
            typeof(UserChangedEventArgs),
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
            binder: null,
            args: new object?[] { localId, change, null },
            culture: null)!;
}
