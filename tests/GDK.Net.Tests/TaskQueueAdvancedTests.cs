using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using GDK.Net;
using GDK.Net.Interop;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the extended XTaskQueue family and XGameRuntimeInitializeWithOptions.
/// No test here loads xgameruntime.thunks.dll directly; all assertions are pure layout and
/// enum-value checks, argument-validation checks, or "does not throw DllNotFoundException /
/// EntryPointNotFoundException" guards.
/// </summary>
[Collection(RuntimeCollection.Name)]
public sealed unsafe class TaskQueueAdvancedTests
{
    // ─── GameTaskQueuePortKind enum matches XTaskQueue.h ─────────────────────────

    [Fact]
    public void PortKindMatchesHeader()
    {
        // Written as a Fact rather than a Theory because GameTaskQueuePortKind is internal and
        // xUnit requires public test method signatures.
        Assert.Equal(0u, (uint)GameTaskQueuePortKind.Work);
        Assert.Equal(1u, (uint)GameTaskQueuePortKind.Completion);
        Assert.Equal(0u, (uint)(XTaskQueuePort)GameTaskQueuePortKind.Work);
        Assert.Equal(1u, (uint)(XTaskQueuePort)GameTaskQueuePortKind.Completion);
    }

    // ─── XTaskQueueRegistrationToken layout ──────────────────────────────────────

    [Fact]
    public void RegistrationTokenIsSingleUInt64()
    {
        // Already covered in InteropContractTests but pinned here for this family too.
        Assert.Equal(sizeof(ulong), Marshal.SizeOf<XTaskQueueRegistrationToken>());
    }

    // ─── XGameRuntimeOptions struct layout matches XGameRuntimeInit.h ─────────────

    [Fact]
    public void XGameRuntimeOptionsSizeIs16OnCurrentPlatform()
    {
        // uint32_t (4) + padding (4) + pointer (8) = 16 bytes on 64-bit.
        // On arm64 the same layout holds because pointers are 8-byte aligned.
        Assert.Equal(16, Marshal.SizeOf<XGameRuntimeOptions>());
    }

    [Fact]
    public void XGameRuntimeOptionsGameConfigSourceAtOffset0()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<XGameRuntimeOptions>(nameof(XGameRuntimeOptions.GameConfigSource)));
    }

    [Fact]
    public void XGameRuntimeOptionsGameConfigAtOffset8()
    {
        // On 64-bit, the pointer field sits at offset 8 after 4 bytes of padding.
        Assert.Equal(8, (int)Marshal.OffsetOf<XGameRuntimeOptions>(nameof(XGameRuntimeOptions.GameConfig)));
    }

    // ─── XGameRuntimeGameConfigSource enum values ─────────────────────────────────

    [Fact]
    public void NativeConfigSourceValuesMatchHeader()
    {
        // XGameRuntimeGameConfigSource from XGameRuntimeInit.h: Default=0, Inline=1, File=2
        Assert.Equal(0u, (uint)XGameRuntimeGameConfigSource.Default);
        Assert.Equal(1u, (uint)XGameRuntimeGameConfigSource.Inline);
        Assert.Equal(2u, (uint)XGameRuntimeGameConfigSource.File);
    }

    // ─── Public GameRuntimeGameConfigSource round-trips through native enum ───────────

    [Theory]
    [InlineData(GameRuntimeGameConfigSource.Default, 0u)]
    [InlineData(GameRuntimeGameConfigSource.Inline,  1u)]
    [InlineData(GameRuntimeGameConfigSource.File,    2u)]
    public void PublicConfigSourceCastsToNativeEnum(GameRuntimeGameConfigSource source, uint expected)
    {
        Assert.Equal(expected, (uint)source);
        Assert.Equal(expected, (uint)(XGameRuntimeGameConfigSource)source);
    }

    // ─── GameRuntimeOptions defaults ─────────────────────────────────────────────

    [Fact]
    public void GameRuntimeOptionsDefaultsToDefaultSource()
    {
        var opts = new GameRuntimeOptions();
        Assert.Equal(GameRuntimeGameConfigSource.Default, opts.GameConfigSource);
        Assert.Null(opts.GameConfig);
    }

    [Fact]
    public void GameRuntimeOptionsRoundTripsProperties()
    {
        var opts = new GameRuntimeOptions
        {
            GameConfigSource = GameRuntimeGameConfigSource.Inline,
            GameConfig = "<GameConfig/>",
        };
        Assert.Equal(GameRuntimeGameConfigSource.Inline, opts.GameConfigSource);
        Assert.Equal("<GameConfig/>", opts.GameConfig);
    }

    // ─── GameTaskQueuePort struct ─────────────────────────────────────────────────

    [Fact]
    public void DefaultGameTaskQueuePortIsNotValid()
    {
        var port = default(GameTaskQueuePort);
        Assert.False(port.IsValid);
    }

    // ─── GameTaskQueueMonitorEventArgs ────────────────────────────────────────────

    [Fact]
    public void MonitorEventArgsCarriesPort()
    {
        foreach (GameTaskQueuePortKind port in new[] { GameTaskQueuePortKind.Work, GameTaskQueuePortKind.Completion })
        {
            var args = CreateMonitorArgs(port);
            Assert.Equal(port, args.Port);
        }
    }

    // ─── GameTaskQueue.IsComposite ────────────────────────────────────────────────

    [Fact]
    public void CreateDoesNotSetIsComposite()
    {
        // GameTaskQueue.Create does not call native (queue creation fails outside a packaged title),
        // but the shape of the API is what we're testing: we only call it if the runtime is
        // available, and guard with try/catch for the unpackaged case.
        bool isComposite = false;
        try
        {
            using var q = GameTaskQueue.Create(GameTaskQueueDispatchMode.ThreadPool);
            isComposite = q.IsComposite;
        }
        catch (GameRuntimeException)
        {
            return; // Expected outside a packaged title: shape is verified above.
        }
        catch (DllNotFoundException)
        {
            return;
        }

        Assert.False(isComposite);
    }

    // ─── Argument validation: SubmitCallback ─────────────────────────────────────

    [Fact]
    public void SubmitCallbackThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ObjectDisposedException>(() => queue.SubmitCallback(() => { }));
    }

    [Fact]
    public void SubmitDelayedCallbackThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ObjectDisposedException>(() =>
            queue.SubmitDelayedCallback(() => { }, TimeSpan.Zero));
    }

    [Fact]
    public void RegisterWaiterThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        using var mre = new ManualResetEvent(false);
        Assert.Throws<ObjectDisposedException>(() =>
            queue.RegisterWaiter(mre, () => { }));
    }

    [Fact]
    public void RegisterMonitorThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ObjectDisposedException>(() =>
            queue.RegisterMonitor(_ => { }));
    }

    // ─── Null-argument validation ─────────────────────────────────────────────────

    [Fact]
    public void SubmitCallbackThrowsOnNullCallback()
    {
        // Null check fires before the native call; document it is ArgumentNullException not
        // ObjectDisposedException even on a disposed queue.
        var queue = CreateDisposedQueue();
        Assert.Throws<ArgumentNullException>("callback", () => queue.SubmitCallback(null!));
    }

    [Fact]
    public void SubmitDelayedCallbackThrowsOnNullCallback()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ArgumentNullException>("callback", () =>
            queue.SubmitDelayedCallback(null!, TimeSpan.Zero));
    }

    [Fact]
    public void RegisterWaiterThrowsOnNullWaitHandle()
    {
        // waitHandle null check fires before Handle access (before disposed check).
        var queue = CreateDisposedQueue();
        Assert.Throws<ArgumentNullException>("waitHandle", () =>
            queue.RegisterWaiter(null!, () => { }));
    }

    [Fact]
    public void RegisterMonitorThrowsOnNullCallback()
    {
        // callback null check fires before Handle access (before disposed check).
        var queue = CreateDisposedQueue();
        Assert.Throws<ArgumentNullException>("callback", () =>
            queue.RegisterMonitor(null!));
    }

    // ─── Trampoline function pointers are non-zero and distinct ──────────────────

    [Fact]
    public void TaskQueueTrampolinePointersAreNonZero()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.TaskQueueOneShotCallback);
        Assert.NotEqual(IntPtr.Zero, Trampolines.TaskQueueWaiterCallback);
        Assert.NotEqual(IntPtr.Zero, Trampolines.TaskQueueMonitorCallback);
    }

    [Fact]
    public void TaskQueueTrampolinePointersAreDistinct()
    {
        Assert.NotEqual(Trampolines.TaskQueueOneShotCallback, Trampolines.TaskQueueWaiterCallback);
        Assert.NotEqual(Trampolines.TaskQueueOneShotCallback, Trampolines.TaskQueueMonitorCallback);
        Assert.NotEqual(Trampolines.TaskQueueWaiterCallback, Trampolines.TaskQueueMonitorCallback);
    }

    // ─── ObjectDisposedException on all operations after Dispose ─────────────────

    [Fact]
    public void GetPortThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ObjectDisposedException>(() => queue.GetPort(GameTaskQueuePortKind.Work));
    }

    [Fact]
    public void DuplicateThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ObjectDisposedException>(() => queue.Duplicate());
    }

    [Fact]
    public void DispatchCompletionsThrowsAfterDispose()
    {
        var queue = CreateDisposedQueue();
        Assert.Throws<ObjectDisposedException>(() => queue.DispatchCompletions());
    }

    // ─── ProcessDefault does not throw expected types ────────────────────────────

    [Fact]
    public void ProcessDefaultGetterReturnsNullOrQueueWithoutArgumentException()
    {
        // Outside a packaged title, any native exception is acceptable (DllNotFoundException,
        // SEHException, GameRuntimeException). What must NOT happen is a managed
        // ArgumentNullException or NullReferenceException leaking from our managed code.
        // If the call succeeds, the result must be null or a valid non-null GameTaskQueue.
        GameTaskQueue? q = null;
        try
        {
            q = GameTaskQueue.ProcessDefault;
            // If we get here, verify we received a valid nullable GameTaskQueue.
            if (q is not null)
            {
                Assert.True(q.IsComposite, "A queue obtained from ProcessDefault must have IsComposite=true.");
            }
        }
        catch (ArgumentNullException)
        {
            Assert.Fail("ProcessDefault getter must not throw ArgumentNullException from managed code.");
        }
        catch (NullReferenceException)
        {
            Assert.Fail("ProcessDefault getter must not throw NullReferenceException from managed code.");
        }
        catch
        {
            // DllNotFoundException, SEHException, GameRuntimeException, etc. are all acceptable.
        }
        finally
        {
            q?.Dispose();
        }
    }

    [Fact]
    public void ProcessDefaultSetterAcceptsNullWithoutArgumentException()
    {
        // Pass null to clear the default. Must not throw ArgumentNullException from managed code.
        try
        {
            GameTaskQueue.ProcessDefault = null;
        }
        catch (ArgumentNullException)
        {
            Assert.Fail("ProcessDefault setter must not throw ArgumentNullException for null argument.");
        }
        catch (NullReferenceException)
        {
            Assert.Fail("ProcessDefault setter must not throw NullReferenceException from managed code.");
        }
        catch
        {
            // DllNotFoundException, SEHException, etc. are acceptable.
        }
    }

    // ─── GameRuntime.Initialize with options never throws DllNotFound / EntryPoint ─

    [Fact]
    public void InitializeWithOptionsNeverThrowsDllNotFound()
    {
        GameRuntime? rt = null;
        try
        {
            rt = GameRuntime.Initialize(new GameRuntimeOptions());
        }
        catch (GameRuntimeException)
        {
            return; // Expected outside packaged title.
        }
        catch (DllNotFoundException)
        {
            Assert.Fail("GameRuntime.Initialize(options) must not propagate DllNotFoundException.");
        }
        catch (EntryPointNotFoundException)
        {
            Assert.Fail("GameRuntime.Initialize(options) must not propagate EntryPointNotFoundException.");
        }
        finally
        {
            rt?.Dispose();
        }

        // If reached, we're actually inside a packaged title.
        Assert.NotNull(rt);
    }

    [Fact]
    public void InitializeWithNullOptionsNeverThrowsDllNotFound()
    {
        GameRuntime? rt = null;
        try
        {
            rt = GameRuntime.Initialize((GameRuntimeOptions?)null);
        }
        catch (GameRuntimeException)
        {
            return;
        }
        catch (DllNotFoundException)
        {
            Assert.Fail("GameRuntime.Initialize(null) must not propagate DllNotFoundException.");
        }
        catch (EntryPointNotFoundException)
        {
            Assert.Fail("GameRuntime.Initialize(null) must not propagate EntryPointNotFoundException.");
        }
        finally
        {
            rt?.Dispose();
        }

        Assert.NotNull(rt);
    }

    // ─── WaiterRegistration and MonitorRegistration are IDisposable ───────────────

    [Fact]
    public void WaiterRegistrationImplementsIDisposable()
    {
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(GameTaskQueueWaiterRegistration)));
    }

    [Fact]
    public void MonitorRegistrationImplementsIDisposable()
    {
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(GameTaskQueueMonitorRegistration)));
    }

    // ─── ToTimeoutMilliseconds (internal, but visible via InternalsVisibleTo) ─────

    [Theory]
    [InlineData(null,  0u)]
    [InlineData(0,     0u)]
    [InlineData(-100,  0u)]
    [InlineData(500,   500u)]
    public void TimeoutConversionRoundTrips(int? ms, uint expected)
    {
        TimeSpan? ts = ms.HasValue ? TimeSpan.FromMilliseconds(ms.Value) : (TimeSpan?)null;
        Assert.Equal(expected, GameTaskQueue.ToTimeoutMilliseconds(ts));
    }

    // ─── Helper factories ─────────────────────────────────────────────────────────

    private static GameTaskQueue CreateDisposedQueue()
    {
        // We need a disposed GameTaskQueue to test post-dispose guards.
        // The constructor is private and calls no native code itself; only the native call inside
        // Create() (or similar factories) touches xgameruntime.thunks.dll. So we invoke the
        // private constructor directly via reflection with a null-handled SafeHandleZeroOrMinusOneIsInvalid
        // (IntPtr.Zero → IsInvalid=true → ReleaseHandle is never called → no native call on Dispose).
        var ctor = typeof(GameTaskQueue).GetConstructors(
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotEmpty(ctor);

        // Build a zero-handle SafeHandle; the concrete type doesn't matter because it's stored as
        // the base SafeHandleZeroOrMinusOneIsInvalid, and IsInvalid=true prevents ReleaseHandle.
        using var dummySafeHandle = new Microsoft.Win32.SafeHandles.SafeFileHandle(
            IntPtr.Zero, ownsHandle: false);

        GameTaskQueue queue = (GameTaskQueue)ctor[0].Invoke(new object[]
        {
            dummySafeHandle,
            GameTaskQueueDispatchMode.ThreadPool,
            GameTaskQueueDispatchMode.ThreadPool,
            false,
        });

        queue.Dispose();
        return queue;
    }

    private static GameTaskQueueMonitorEventArgs CreateMonitorArgs(GameTaskQueuePortKind port)
        => (GameTaskQueueMonitorEventArgs)Activator.CreateInstance(
            typeof(GameTaskQueueMonitorEventArgs),
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
            binder: null,
            args: new object[] { port },
            culture: null)!;

    // ─── No queue means "let the runtime resolve the process default" ────────────

    [Fact]
    public void AnAbsentQueueResolvesToTheNullHandle()
    {
        // The projection never manufactures a task queue. Where the caller supplies none, every
        // async start and event registration passes a null XAsyncBlock::queue, which the Gaming
        // Runtime resolves to the process default queue at call time. This pins the one conversion
        // the whole policy rests on: absent must become IntPtr.Zero and nothing else, because a
        // stray non-zero value here would be dereferenced as a queue handle.
        GameTaskQueue? absent = null;
        Assert.Equal(IntPtr.Zero, absent.RawHandle());
    }

    [Fact]
    public void APresentQueueResolvesToItsOwnHandle()
    {
        GameTaskQueue queue;
        try
        {
            queue = GameTaskQueue.Create();
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException or GameRuntimeException)
        {
            return; // No Gaming Runtime on this machine; the null-handle case above still holds.
        }

        using (queue)
        {
            Assert.NotEqual(IntPtr.Zero, queue.RawHandle());
            Assert.Equal(queue.Handle, queue.RawHandle());
        }
    }

    [Fact]
    public void NoPublicApiMentionsATaskQueue()
    {
        // Task queues are an internal implementation detail: titles cannot create, name or pump
        // one, and every operation leaves XAsyncBlock::queue null so the Gaming Runtime resolves
        // the process default. This guard is what keeps that true: it fails the moment any public
        // signature reintroduces a queue, which is how the surface drifted before.
        //
        // Note the consequence: the pumped/Manual model described in docs/plan.md §7 is not
        // reachable by a title today. Exposing it means changing this test deliberately, not
        // discovering the gap by accident.
        var queueTypes = typeof(GameTaskQueue).Assembly
            .GetTypes()
            .Where(t => t.Name.StartsWith("GameTaskQueue", StringComparison.Ordinal))
            .ToHashSet();

        Assert.NotEmpty(queueTypes);
        Assert.All(queueTypes, t => Assert.False(t.IsPublic || t.IsNestedPublic, $"{t.Name} must not be public."));

        var offenders = new List<string>();

        foreach (Type type in typeof(GameTaskQueue).Assembly.GetExportedTypes())
        {
            const System.Reflection.BindingFlags Flags =
                System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly;

            foreach (System.Reflection.MethodBase method in
                type.GetMethods(Flags).Cast<System.Reflection.MethodBase>().Concat(type.GetConstructors(Flags)))
            {
                if (method is System.Reflection.MethodInfo info && Mentions(info.ReturnType))
                {
                    offenders.Add($"{type.FullName}.{method.Name} returns {info.ReturnType.Name}");
                }

                foreach (System.Reflection.ParameterInfo parameter in method.GetParameters())
                {
                    if (Mentions(parameter.ParameterType))
                    {
                        offenders.Add($"{type.FullName}.{method.Name} takes {parameter.ParameterType.Name} {parameter.Name}");
                    }
                }
            }

            foreach (System.Reflection.PropertyInfo property in type.GetProperties(Flags))
            {
                if (Mentions(property.PropertyType))
                {
                    offenders.Add($"{type.FullName}.{property.Name} is {property.PropertyType.Name}");
                }
            }

            foreach (System.Reflection.FieldInfo field in type.GetFields(Flags))
            {
                if (Mentions(field.FieldType))
                {
                    offenders.Add($"{type.FullName}.{field.Name} is {field.FieldType.Name}");
                }
            }
        }

        Assert.True(
            offenders.Count == 0,
            "Task queues must not appear in the public API. Offenders:" + Environment.NewLine
                + string.Join(Environment.NewLine, offenders));

        bool Mentions(Type candidate)
        {
            Type target = Nullable.GetUnderlyingType(candidate) ?? candidate;
            return queueTypes.Contains(target)
                || (target.IsGenericType && target.GetGenericArguments().Any(Mentions));
        }
    }
}