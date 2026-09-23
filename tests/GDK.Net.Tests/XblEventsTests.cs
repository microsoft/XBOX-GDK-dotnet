using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed class XblEventsTests
{
    [Theory]
    [InlineData("MatchCompleted")]
    [InlineData("score2")]
    [InlineData("A1B2C3")]
    public void EventNamesAllowOnlyAsciiLettersAndDigits(string eventName)
    {
        Assert.True(EventsService.IsValidEventName(eventName));
    }

    [Theory]
    [InlineData("")]
    [InlineData("match_completed")]
    [InlineData("match-completed")]
    [InlineData("match completed")]
    [InlineData("Évent")]
    public void EventNamesRejectValuesTheNativeServiceWouldDrop(string eventName)
    {
        Assert.False(EventsService.IsValidEventName(eventName));
    }

    [Fact]
    public void EventsServiceIsNotPubliclyConstructible()
    {
        Assert.Empty(typeof(EventsService).GetConstructors());
    }

    [Fact]
    public void EventsServiceIsSealed()
    {
        Assert.True(typeof(EventsService).IsSealed);
    }

    [Fact]
    public void EventsPublicSurfaceExposesNoInteropTypes()
    {
        foreach (MethodInfo method in typeof(EventsService).GetMethods(
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
        {
            AssertNotInterop(method.Name, method.ReturnType);
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                AssertNotInterop(method.Name, parameter.ParameterType);
            }
        }
    }

    [Fact]
    public void WriteInGameEventIsSynchronousAndManagedOnly()
    {
        MethodInfo method = typeof(EventsService).GetMethod(nameof(EventsService.WriteInGameEvent))!;

        Assert.Equal(typeof(void), method.ReturnType);
        ParameterInfo[] parameters = method.GetParameters();
        Assert.Equal(3, parameters.Length);
        Assert.All(parameters, p => Assert.Equal(typeof(string), p.ParameterType));
        Assert.True(parameters[1].IsOptional);
        Assert.True(parameters[2].IsOptional);
    }

    private static void AssertNotInterop(string member, Type candidate)
    {
        Type target = candidate.IsByRef || candidate.IsPointer || candidate.IsArray
            ? candidate.GetElementType()!
            : candidate;

        Assert.False(
            target.Namespace is "GDK.Net.Interop",
            $"{nameof(EventsService)}.{member} exposes the interop type {target.Name}.");

        Assert.False(
            typeof(SafeHandle).IsAssignableFrom(target),
            $"{nameof(EventsService)}.{member} exposes the SafeHandle {target.Name}.");
    }
}
