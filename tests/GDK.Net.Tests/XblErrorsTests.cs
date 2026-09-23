using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed class XblErrorsTests
{
    [Theory]
    [InlineData(ErrorCondition.NoError, 0u)]
    [InlineData(ErrorCondition.GenericError, 1u)]
    [InlineData(ErrorCondition.GenericOutOfRange, 2u)]
    [InlineData(ErrorCondition.Auth, 3u)]
    [InlineData(ErrorCondition.Network, 4u)]
    [InlineData(ErrorCondition.HttpGeneric, 5u)]
    [InlineData(ErrorCondition.Http304NotModified, 6u)]
    [InlineData(ErrorCondition.Http404NotFound, 7u)]
    [InlineData(ErrorCondition.Http412PreconditionFailed, 8u)]
    [InlineData(ErrorCondition.Http429TooManyRequests, 9u)]
    [InlineData(ErrorCondition.HttpServiceTimeout, 10u)]
    [InlineData(ErrorCondition.Rta, 11u)]
    public void ErrorConditionMatchesHeader(ErrorCondition value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblErrorCondition)value);
    }

    [Fact]
    public void ErrorConditionEntryPointIsDeclared()
    {
        Type native = typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.NativeXbl", throwOnError: true)!;
        Assert.NotNull(native.GetMethod("XblGetErrorCondition", BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Fact]
    public void ErrorConditionHelperAcceptsThrownRuntimeExceptions()
    {
        Assert.NotNull(typeof(XboxLiveErrors).GetMethod(
            "GetCondition",
            BindingFlags.Public | BindingFlags.Static,
            binder: null,
            types: new[] { typeof(GameRuntimeException) },
            modifiers: null));

        Assert.NotNull(typeof(XboxLiveErrors).GetMethod(
            "GetXboxLiveErrorCondition",
            BindingFlags.Public | BindingFlags.Static,
            binder: null,
            types: new[] { typeof(GameRuntimeException) },
            modifiers: null));
    }

    [Fact]
    public void PublicErrorSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        {
            typeof(ErrorCondition),
            typeof(XboxLiveErrors),
        };

        foreach (Type type in publicTypes)
        {
            foreach (MethodInfo method in type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    AssertNotInterop(type, method.Name, parameter.ParameterType);
                }
            }
        }
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
