using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net;
using GDK.Net.GameUI;
using GDK.Net.Interop;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the title-implemented UI path (<c>XGameUiSetUiCallbacks</c> and the eight
/// <c>XGameUiSet*UiResponse</c> completions) and for State Share.
/// </summary>
/// <remarks>
/// None of these require the GDK to be present. They pin the parts that are wrong at runtime and
/// silent at compile time: which module each entry point binds to, the exact layout of the two
/// structs the runtime reads and writes, the arity and calling convention of the eight trampolines,
/// and the response bookkeeping that happens entirely in managed code.
/// </remarks>
public class CustomGameUiContractTests
{
    private static Type NativeType =>
        typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.Native", throwOnError: true)!;

    private static Type TrampolinesType =>
        typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.Trampolines", throwOnError: true)!;

    private static MethodInfo Method(string name) =>
        NativeType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)
        ?? throw new InvalidOperationException($"Native.{name} is not declared.");

    /// <summary>
    /// Whether <c>xgameruntime.thunks.dll</c> can be loaded in this process. Tests that would
    /// otherwise reach native code with a zero callback handle opt out when it can.
    /// </summary>
    private static bool GamingRuntimeIsLoadable =>
        NativeLibrary.TryLoad("xgameruntime.thunks.dll", out _);

    /// <summary>The nine custom-UI entry points plus the two State Share ones.</summary>
    public static TheoryData<string> CustomUiEntryPoints => new TheoryData<string>
    {
        "XGameUiSetUiCallbacks",
        "XGameUiSetMessageDialogUiResponse",
        "XGameUiSetPlayerPickerUiResponse",
        "XGameUiSetTextEntryUiResponse",
        "XGameUiSetPlayerProfileCardUiResponse",
        "XGameUiSetSendGameInviteUiResponse",
        "XGameUiSetAchievementsUiResponse",
        "XGameUiSetMultiplayerActivityGameInviteUiResponse",
        "XGameUiSetErrorDialogUiResponse",
        "XGameUiShowStateShareAsync",
        "XGameUiShowStateShareResult",
    };

    [Theory]
    [MemberData(nameof(CustomUiEntryPoints))]
    public void EntryPointIsDeclared(string name) => Assert.NotNull(Method(name));

    [Theory]
    [MemberData(nameof(CustomUiEntryPoints))]
    public void EntryPointBindsToTheThunks(string name)
    {
        // These XGameUI.h functions were absent from xgameruntime.thunks.dll's export table until
        // GDK edition 260404 added them. Binding one to anything else compiles, then fails at first
        // call with a DllNotFoundException -- so the module name is worth asserting.
        var import = Method(name).GetCustomAttribute<DllImportAttribute>();

        Assert.NotNull(import);
        Assert.Equal("xgameruntime.thunks.dll", import!.Value);
    }

    [Fact]
    public void ManageSpaceIsNotBoundBecauseNoHeaderDeclaresIt()
    {
        // XGameUiShowManageSpaceAsync/Result are present in xgameruntime.lib, but no header in GDK
        // edition 260404 declares either one and the thunks DLL still does not export them.
        // Without a declaration there is no signature to bind against, and a guessed one corrupts
        // the stack silently. If a future GDK edition declares them, delete this test and bind them.
        Assert.Null(NativeType.GetMethod(
            "XGameUiShowManageSpaceAsync", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.Null(NativeType.GetMethod(
            "XGameUiShowManageSpaceResult", BindingFlags.NonPublic | BindingFlags.Static));
    }

    // ─── Struct layout ───────────────────────────────────────────────────────────

    [Fact]
    public void UiCallbacksTableMatchesTheNativeLayout()
    {
        Type type = typeof(GameRuntime).Assembly
            .GetType("GDK.Net.Interop.XGameUiUiCallbacks", throwOnError: true)!;

        // context + eight function pointers, all pointer-sized, in header order. Verified against
        // the real header: sizeof(XGameUiUiCallbacks) == 72 on x64, showTextEntryCallback at 64.
        Assert.Equal(9 * IntPtr.Size, Marshal.SizeOf(type));

        string[] expected =
        {
            "context",
            "showPlayerProfileCardCallback",
            "showPlayerPickerCallback",
            "showSendGameInviteCallback",
            "showAchievementsCallback",
            "showMultiplayerActivityGameInviteCallback",
            "showMessageDialogCallback",
            "showErrorDialogCallback",
            "showTextEntryCallback",
        };

        string[] actual = type
            .GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Select(f => f.Name)
            .ToArray();

        // Field order is the layout: a transposition here silently mis-registers every callback.
        Assert.Equal(expected, actual);

        foreach (string name in expected)
        {
            Assert.Equal(
                IntPtr.Size,
                Marshal.SizeOf(type.GetField(name)!.FieldType));
        }
    }

    [Fact]
    public unsafe void PlayerPickerInfoMatchesTheNativeLayout()
    {
        Type type = typeof(GameRuntime).Assembly
            .GetType("GDK.Net.Interop.XGameUiPlayerPickerInfo", throwOnError: true)!;

        // XUserHandle, const char*, uint32 (+pad), const uint64_t*, uint32 (+pad),
        // const uint64_t*, uint32, uint32. Verified by compiling offsetof/sizeof against the real
        // XGameUI.h from GDK edition 260404: 56 bytes on x64, offsets 0/8/16/24/32/40/48/52.
        int expectedSize = IntPtr.Size == 8 ? 56 : 32;
        Assert.Equal(expectedSize, Marshal.SizeOf(type));

        Assert.Equal(0, (int)Marshal.OffsetOf(type, "requestingUser"));
        Assert.Equal(IntPtr.Size, (int)Marshal.OffsetOf(type, "promptText"));
        Assert.Equal(2 * IntPtr.Size, (int)Marshal.OffsetOf(type, "selectFromPlayersCount"));

        // The two count/pointer pairs must stay padded apart, or every array read is skewed.
        Assert.Equal(3 * IntPtr.Size, (int)Marshal.OffsetOf(type, "selectFromPlayers"));
        Assert.Equal(4 * IntPtr.Size, (int)Marshal.OffsetOf(type, "preSelectedPlayersCount"));
        Assert.Equal(5 * IntPtr.Size, (int)Marshal.OffsetOf(type, "preSelectedPlayers"));
        Assert.Equal(6 * IntPtr.Size, (int)Marshal.OffsetOf(type, "minSelectionCount"));
        Assert.Equal(6 * IntPtr.Size + 4, (int)Marshal.OffsetOf(type, "maxSelectionCount"));
    }

    // ─── Trampolines ─────────────────────────────────────────────────────────────

    /// <summary>The eight callback slots, with the argument count each native typedef declares.</summary>
    public static TheoryData<string, int> Trampolines => new TheoryData<string, int>
    {
        // handle, queue, user, targetPlayer, context
        { "GameUiShowPlayerProfileCardCallback", 5 },
        // handle, queue, info, context
        { "GameUiShowPlayerPickerCallback", 4 },
        // handle, queue, user, 5 strings, context
        { "GameUiShowSendGameInviteCallback", 9 },
        // handle, queue, user, titleId, context
        { "GameUiShowAchievementsCallback", 5 },
        // handle, queue, user, context
        { "GameUiShowMultiplayerActivityGameInviteCallback", 4 },
        // handle, queue, 5 strings, defaultButton, cancelButton, context
        { "GameUiShowMessageDialogCallback", 10 },
        // handle, queue, errorCode, serviceContext, context
        { "GameUiShowErrorDialogCallback", 5 },
        // handle, queue, 3 strings, inputScope, maxTextLength, context
        { "GameUiShowTextEntryCallback", 8 },
    };

    [Theory]
    [MemberData(nameof(Trampolines))]
    public void TrampolineIsExposedAndNonNull(string name, int expectedArity)
    {
        _ = expectedArity;

        var property = TrampolinesType.GetProperty(
            name, BindingFlags.NonPublic | BindingFlags.Static);

        Assert.NotNull(property);

        var value = (IntPtr)property!.GetValue(null)!;

        // A null slot would be reported to the runtime as "the title does not implement this UI",
        // which fails silently rather than loudly.
        Assert.NotEqual(IntPtr.Zero, value);
    }

    [Theory]
    [MemberData(nameof(Trampolines))]
    public void TrampolineHasTheNativeArityAndReturnsVoid(string name, int expectedArity)
    {
        // Map the property back to the method whose address it takes. Both the net8.0+
        // [UnmanagedCallersOnly] statics and the netstandard2.0 delegate targets use this name.
        string methodName = "On" + name
            .Replace("GameUi", string.Empty)
            .Replace("Callback", "Ui");

        MethodInfo? method = TrampolinesType.GetMethod(
            methodName, BindingFlags.NonPublic | BindingFlags.Static);

        Assert.True(method is not null, $"Trampolines.{methodName} is not declared.");
        Assert.Equal(typeof(void), method!.ReturnType);
        Assert.Equal(expectedArity, method.GetParameters().Length);

        // Every one of these is invoked by native code, so the last parameter is the context the
        // runtime echoes back from the callbacks table.
        Assert.Equal("context", method.GetParameters().Last().Name);
    }

#if NET7_0_OR_GREATER
    [Theory]
    [MemberData(nameof(Trampolines))]
    public void TrampolineIsUnmanagedCallersOnlyStdcall(string name, int expectedArity)
    {
        _ = expectedArity;

        string methodName = "On" + name
            .Replace("GameUi", string.Empty)
            .Replace("Callback", "Ui");

        MethodInfo method = TrampolinesType.GetMethod(
            methodName, BindingFlags.NonPublic | BindingFlags.Static)!;

        var attribute = method.GetCustomAttribute<UnmanagedCallersOnlyAttribute>();

        // Without [UnmanagedCallersOnly] the address would need a runtime-generated stub, which
        // NativeAOT cannot produce; with the wrong convention the stack is corrupted on x86.
        Assert.NotNull(attribute);
        Assert.Equal(new[] { typeof(CallConvStdcall) }, attribute!.CallConvs);
    }
#endif

    // ─── Public surface ──────────────────────────────────────────────────────────

    [Fact]
    public void EveryCallbackSlotHasAMatchingPublicHandler()
    {
        Type table = typeof(GameRuntime).Assembly
            .GetType("GDK.Net.Interop.XGameUiUiCallbacks", throwOnError: true)!;

        // "showPlayerPickerCallback" -> "PlayerPicker"
        var slots = table
            .GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Select(f => f.Name)
            .Where(n => n != "context")
            .Select(n => char.ToUpperInvariant(n[4]) + n.Substring(5, n.Length - 5 - "Callback".Length))
            .ToArray();

        var handlers = typeof(CustomGameUiHandlers)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name)
            .ToArray();

        // Catches a slot added to the native table with no way for a title to service it.
        Assert.Equal(slots.OrderBy(n => n, StringComparer.Ordinal),
                     handlers.OrderBy(n => n, StringComparer.Ordinal));
    }

    [Fact]
    public void EveryRequestTypeExposesExactlyOneRespondMethod()
    {
        var requestTypes = typeof(GameUiRequest).Assembly
            .GetTypes()
            .Where(t => t.IsPublic && !t.IsAbstract && typeof(GameUiRequest).IsAssignableFrom(t))
            .ToArray();

        Assert.Equal(8, requestTypes.Length);

        foreach (Type type in requestTypes)
        {
            var respond = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => m.Name == "Respond")
                .ToArray();

            // One and only one: the response shape is what distinguishes these requests, and an
            // overload set would let a title answer a request in a way the runtime rejects.
            Assert.True(respond.Length == 1, $"{type.Name} declares {respond.Length} Respond methods.");
            Assert.Equal(typeof(void), respond[0].ReturnType);
        }
    }

    [Fact]
    public void HandlersAreNullUntilRegistered()
    {
        // Registration needs a Gaming Runtime, so on CI this stays null for the whole run.
        if (!GamingRuntimeIsLoadable)
        {
            Assert.Null(CustomGameUi.Handlers);
        }
    }

    [Fact]
    public void SetHandlersRejectsNull()
    {
        // Argument validation must precede everything native, or the error a developer sees on a
        // machine without the Gaming Runtime hides the real mistake.
        Assert.Throws<ArgumentNullException>(() => CustomGameUi.SetHandlers(null!));
    }

    [Fact]
    public void AFailedRegistrationDoesNotPublishTheHandlers()
    {
        if (GamingRuntimeIsLoadable)
        {
            return;
        }

        var handlers = new CustomGameUiHandlers { MessageDialog = _ => { } };

        // SetHandlers publishes the handler set before the native registration so the runtime can
        // never call an unpublished slot. When the registration fails it has to unwind, or the
        // title is left believing it owns UI the system is still drawing.
        Assert.ThrowsAny<Exception>(() => CustomGameUi.SetHandlers(handlers));
        Assert.Null(CustomGameUi.Handlers);
    }

    [Fact]
    public void StateShareIsDeclaredWithTheExpectedSignature()
    {
        MethodInfo? method = typeof(GameUiManager).GetMethod(
            "ShowStateShareAsync", BindingFlags.Public | BindingFlags.Instance);

        Assert.NotNull(method);

        var parameters = method!.GetParameters();
        Assert.Equal(3, parameters.Length);
        Assert.Equal("user", parameters[0].Name);
        Assert.Equal("linkToken", parameters[1].Name);
        Assert.True(parameters[2].IsOptional);
    }

    // ─── Response bookkeeping ────────────────────────────────────────────────────

    [Fact]
    public void ARequestRefusesASecondResponse()
    {
        // Skipped where the Gaming Runtime can load: the first Respond would reach native code with
        // a zero callback handle, and this test is about the managed guard, not the runtime's
        // reaction.
        if (GamingRuntimeIsLoadable)
        {
            return;
        }

        var request = new MessageDialogUiRequest(
            IntPtr.Zero, "t", "c", "ok", null, null,
            MessageDialogButton.First, MessageDialogButton.First);

        Assert.False(request.HasResponded);

        // The first response is recorded, then fails at the P/Invoke for want of the runtime. What
        // matters is that a failed native call still consumes the request's single answer -- the
        // runtime may well have received it.
        Assert.ThrowsAny<Exception>(() => request.Respond(MessageDialogButton.First));
        Assert.True(request.HasResponded);

        var ex = Assert.Throws<InvalidOperationException>(
            () => request.Respond(MessageDialogButton.First));
        Assert.Contains("already been answered", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void RequestPayloadsSurviveTheHandler()
    {
        // Every string and array on a request is copied out of runtime-owned memory before the
        // handler runs, so holding a request past the callback is safe. Constructing one directly
        // pins that these are plain owned values, not pointers into native memory.
        var picker = new PlayerPickerUiRequest(
            IntPtr.Zero,
            IntPtr.Zero,
            "Choose",
            new ulong[] { 1, 2, 3 },
            new ulong[] { 2 },
            1,
            2);

        Assert.Equal("Choose", picker.PromptText);
        Assert.Equal(new ulong[] { 1, 2, 3 }, picker.SelectFromPlayers);
        Assert.Equal(new ulong[] { 2 }, picker.PreSelectedPlayers);
        Assert.Equal(1u, picker.MinSelectionCount);
        Assert.Equal(2u, picker.MaxSelectionCount);
    }

    [Fact]
    public void PlayerPickerRejectsANullSelection()
    {
        var picker = new PlayerPickerUiRequest(
            IntPtr.Zero, IntPtr.Zero, null,
            Array.Empty<ulong>(), Array.Empty<ulong>(), 0, 1);

        Assert.Throws<ArgumentNullException>(() => picker.Respond(null!));

        // A rejected response must not consume the request's single answer.
        Assert.False(picker.HasResponded);
    }

    [Fact]
    public void TextEntryRejectsANullResponse()
    {
        var entry = new TextEntryUiRequest(
            IntPtr.Zero, "t", "d", null, TextEntryInputScope.Default, 32);

        Assert.Throws<ArgumentNullException>(() => entry.Respond(null!));
        Assert.False(entry.HasResponded);
    }

    // ─── Dispatch must not occupy the runtime's callback thread ──────────────────

    [Fact]
    public async Task DispatchReturnsWithoutWaitingForTheHandler()
    {
        // The runtime raises these callbacks on the work port of the task queue driving the
        // originating operation. Running the title's handler inline would hold that port for the
        // handler's whole duration, starving every other callback on the queue and deadlocking any
        // handler that awaits another operation on it. So dispatch must hand off and return.
        //
        // This is asserted the only way that cannot pass by accident: the handler blocks
        // indefinitely, and the dispatch call is still required to complete.
        using var handlerEntered = new ManualResetEventSlim(false);
        using var releaseHandler = new ManualResetEventSlim(false);

        var handlers = new CustomGameUiHandlers
        {
            ErrorDialog = _ =>
            {
                handlerEntered.Set();
                releaseHandler.Wait();
            },
        };

        CustomGameUi.SetHandlersWithoutRegistering(handlers);
        try
        {
            Task dispatch = Task.Run(() => DispatchAnErrorDialog());

            // If the handler were invoked inline this never returns, and the timeout is what
            // reports it.
            Task completed = await Task.WhenAny(dispatch, Task.Delay(TimeSpan.FromSeconds(10)));
            Assert.True(
                ReferenceEquals(completed, dispatch),
                "Dispatch did not return while the handler was still running, so the handler was " +
                "invoked inline on the runtime's callback thread.");
            await dispatch;

            Assert.True(
                handlerEntered.Wait(TimeSpan.FromSeconds(10)),
                "The handler was never invoked after dispatch returned.");

            // Deliberately no assertion that the handler ran on a *different* thread. Once dispatch
            // returns, the pool is free to reuse that very thread for the queued handler, and on a
            // two-core CI runner it routinely does. The guarantee that matters -- and the one that
            // cannot pass by accident -- is that dispatch completed while the handler was blocked.
        }
        finally
        {
            releaseHandler.Set();
            CustomGameUi.SetHandlersWithoutRegistering(null);
        }
    }

    [Fact]
    public void AMissingHandlerStillAnswersTheRuntime()
    {
        // A null slot should never be called, but if the runtime calls one anyway the operation
        // must not hang. The fallback path posts the neutral response; here the runtime is absent
        // so it fails at the P/Invoke, and what is pinned is that the attempt is made off-thread
        // and that dispatch itself neither throws nor blocks.
        if (GamingRuntimeIsLoadable)
        {
            return;
        }

        CustomGameUi.SetHandlersWithoutRegistering(new CustomGameUiHandlers());
        try
        {
            DispatchAnErrorDialog();
        }
        finally
        {
            CustomGameUi.SetHandlersWithoutRegistering(null);
        }
    }

    private static unsafe void DispatchAnErrorDialog() =>
        CustomGameUi.DispatchErrorDialog(IntPtr.Zero, IntPtr.Zero, unchecked((int)0x80004005), null);
}