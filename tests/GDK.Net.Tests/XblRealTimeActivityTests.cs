// Contract tests for xsapi-c\real_time_activity_c.h (GDK edition 260404).
//
// These tests do not load Microsoft.Xbox.Services.C.Thunks.dll. They pin enum values, binding
// shape and public API shape so the projection stays idiomatic and AOT-safe.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblRealTimeActivityTests
{
    private static Type NativeXblType =>
        typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.NativeXbl", throwOnError: true)!;

    private static MethodInfo NativeMethod(string name) =>
        NativeXblType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)
        ?? throw new InvalidOperationException($"NativeXbl.{name} is not declared.");

    public static TheoryData<RealTimeActivityConnectionState, uint> ConnectionStates => new()
    {
        { RealTimeActivityConnectionState.Connected, 0u },
        { RealTimeActivityConnectionState.Connecting, 1u },
        { RealTimeActivityConnectionState.Disconnected, 2u },
    };

    public static TheoryData<string> EntryPoints => new()
    {
        "XblRealTimeActivityAddConnectionStateChangeHandler",
        "XblRealTimeActivityRemoveConnectionStateChangeHandler",
        "XblRealTimeActivityAddResyncHandler",
        "XblRealTimeActivityRemoveResyncHandler",
    };


    [Theory]
    [MemberData(nameof(ConnectionStates))]
    public void ConnectionStateMatchesHeader(RealTimeActivityConnectionState value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblRealTimeActivityConnectionState)value);
    }

    [Theory]
    [MemberData(nameof(EntryPoints))]
    public void RealTimeActivityEntryPointsAreDeclared(string name) => Assert.NotNull(NativeMethod(name));

    [Theory]
    [MemberData(nameof(EntryPoints))]
    public void RealTimeActivityEntryPointsBindToXsapiThunks(string name)
    {
        var import = NativeMethod(name).GetCustomAttribute<DllImportAttribute>();

        Assert.NotNull(import);
        Assert.Equal("Microsoft.Xbox.Services.C.Thunks.dll", import!.Value);
    }


    [Fact]
    public void CallbackThunksResolveToDistinctFunctionPointers()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.RealTimeActivityConnectionStateChangeHandler);
        Assert.NotEqual(IntPtr.Zero, Trampolines.RealTimeActivityResyncHandler);
        Assert.NotEqual(
            Trampolines.RealTimeActivityConnectionStateChangeHandler,
            Trampolines.RealTimeActivityResyncHandler);
    }

    [Fact]
    public void PublicTypesAreSealed()
    {
        Assert.True(typeof(RealTimeActivityService).IsSealed);
        Assert.True(typeof(RealTimeActivityConnectionStateChangedEventArgs).IsSealed);
    }

    [Fact]
    public void ServiceHasOnlyTheInternalContextConstructor()
    {
        Assert.Empty(typeof(RealTimeActivityService).GetConstructors());

        ConstructorInfo[] constructors = typeof(RealTimeActivityService).GetConstructors(
            BindingFlags.NonPublic | BindingFlags.Instance);

        ConstructorInfo constructor = Assert.Single(constructors);
        Assert.True(constructor.IsAssembly);
        Assert.Equal(new[] { typeof(XboxLiveContext) }, constructor.GetParameters().Select(p => p.ParameterType));
    }

    [Fact]
    public void ServiceExposesRealTimeActivityEvents()
    {
        AssertEvent<RealTimeActivityConnectionStateChangedEventArgs>(
            nameof(RealTimeActivityService.ConnectionStateChanged));
        AssertEvent(nameof(RealTimeActivityService.ResyncRequired), typeof(EventHandler));
    }

    [Fact]
    public void PublicSurfaceExposesNoInteropTypesOrRawHandles()
    {
        Type[] publicTypes =
        [
            typeof(RealTimeActivityService),
            typeof(RealTimeActivityConnectionStateChangedEventArgs),
        ];

        foreach (Type type in publicTypes)
        {
            foreach (MemberInfo member in type.GetMembers(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly))
            {
                if (member is MethodInfo method)
                {
                    AssertNotInterop(type, method.Name, method.ReturnType);
                    foreach (ParameterInfo parameter in method.GetParameters())
                    {
                        AssertNotInterop(type, method.Name, parameter.ParameterType);
                    }
                }
            }
        }
    }

    private static void AssertEvent<TArgs>(string name)
        where TArgs : EventArgs =>
        AssertEvent(name, typeof(EventHandler<TArgs>));

    private static void AssertEvent(string name, Type handlerType)
    {
        EventInfo? eventInfo = typeof(RealTimeActivityService).GetEvent(name);

        Assert.NotNull(eventInfo);
        Assert.Equal(handlerType, eventInfo!.EventHandlerType);
    }

    private static void AssertNotInterop(Type owner, string member, Type candidate)
    {
        Type target = candidate.IsByRef || candidate.IsPointer || candidate.IsArray
            ? candidate.GetElementType()!
            : candidate;

        Assert.False(
            target.Namespace is "GDK.Net.Interop",
            $"{owner.Name}.{member} exposes the interop type {target.Name}.");

        Assert.False(
            typeof(SafeHandle).IsAssignableFrom(target),
            $"{owner.Name}.{member} exposes the SafeHandle {target.Name}.");
    }
}
