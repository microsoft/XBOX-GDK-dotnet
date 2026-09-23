using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblStringVerifyTests
{
    [Theory]
    [InlineData(VerifyStringResultCode.Success, 0u)]
    [InlineData(VerifyStringResultCode.Offensive, 1u)]
    [InlineData(VerifyStringResultCode.TooLong, 2u)]
    [InlineData(VerifyStringResultCode.UnknownError, 3u)]
    public void StringVerificationResultCodeMatchesHeader(VerifyStringResultCode value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblVerifyStringResultCode)value);
    }

    [Fact]
    public void StringVerificationStructSizeMatchesHeader()
    {
        Assert.Equal(4, sizeof(XblVerifyStringResultCode));
        Assert.Equal(16, sizeof(XblVerifyStringResult));
    }

    [Theory]
    [InlineData(nameof(XblVerifyStringResult.ResultCode), 0)]
    [InlineData(nameof(XblVerifyStringResult.FirstOffendingSubstring), 8)]
    public void StringVerificationStructOffsetsMatchHeader(string field, int expected) =>
        Assert.Equal(expected, (int)Marshal.OffsetOf<XblVerifyStringResult>(field));

    [Fact]
    public void FailClosedStringVerificationResultIsAlwaysUnacceptable()
    {
        StringVerificationResult result = StringVerificationResult.FailClosed("chat text");

        Assert.False(result.WasVerified);
        Assert.False(result.IsAcceptable);
        Assert.Equal(VerifyStringResultCode.UnknownError, result.ResultCode);
        Assert.Equal("chat text", result.VerifiedString);
        Assert.Null(result.FirstOffendingSubstring);
    }

    [Fact]
    public void StringVerificationResultCannotBeAcceptableWhenCheckDidNotComplete()
    {
        var result = new StringVerificationResult(
            "clean text",
            wasVerified: false,
            resultCode: VerifyStringResultCode.Success,
            firstOffendingSubstring: null);

        Assert.False(result.IsAcceptable);
        Assert.Equal(VerifyStringResultCode.UnknownError, result.ResultCode);
    }

    [Fact]
    public void NativeSuccessfulStringVerificationResultIsAcceptable()
    {
        var native = new XblVerifyStringResult
        {
            ResultCode = XblVerifyStringResultCode.Success,
            FirstOffendingSubstring = null,
        };

        StringVerificationResult result = StringVerificationResult.FromNative("clean text", &native);

        Assert.True(result.WasVerified);
        Assert.True(result.IsAcceptable);
        Assert.Equal(VerifyStringResultCode.Success, result.ResultCode);
        Assert.Equal("clean text", result.VerifiedString);
        Assert.Null(result.FirstOffendingSubstring);
    }

    [Fact]
    public void NativeOffensiveStringVerificationResultIsSnapshotted()
    {
        byte[] substring = Encoding.UTF8.GetBytes("bad\0");
        StringVerificationResult result;

        fixed (byte* p = substring)
        {
            var native = new XblVerifyStringResult
            {
                ResultCode = XblVerifyStringResultCode.Offensive,
                FirstOffendingSubstring = p,
            };

            result = StringVerificationResult.FromNative("contains bad", &native);
            substring[0] = (byte)'x';
        }

        Assert.True(result.WasVerified);
        Assert.False(result.IsAcceptable);
        Assert.Equal(VerifyStringResultCode.Offensive, result.ResultCode);
        Assert.Equal("contains bad", result.VerifiedString);
        Assert.Equal("bad", result.FirstOffendingSubstring);
    }

    [Fact]
    public void StringVerificationPublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(StringVerificationService),
            typeof(StringVerificationResult),
        ];

        foreach (Type type in publicTypes)
        {
            foreach (MethodInfo method in type.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    AssertNotInterop(type, method.Name, parameter.ParameterType);
                }
            }
        }
    }

    [Fact]
    public void StringVerificationServiceIsNotPubliclyConstructible()
    {
        Assert.Empty(typeof(StringVerificationService).GetConstructors());
    }

    [Fact]
    public void StringVerificationPublicTypesAreSealed()
    {
        Assert.True(typeof(StringVerificationService).IsSealed);
        Assert.True(typeof(StringVerificationResult).IsSealed);
    }

    [Fact]
    public void VerificationMethodsReturnFailClosedResultObjects()
    {
        Assert.Equal(
            typeof(Task<StringVerificationResult>),
            typeof(StringVerificationService).GetMethod(nameof(StringVerificationService.VerifyStringAsync))!.ReturnType);
        Assert.Equal(
            typeof(Task<IReadOnlyList<StringVerificationResult>>),
            typeof(StringVerificationService).GetMethod(nameof(StringVerificationService.VerifyStringsAsync))!.ReturnType);
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
