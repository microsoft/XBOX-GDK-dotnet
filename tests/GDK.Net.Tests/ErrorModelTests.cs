using System;
using System.Threading;
using GDK.Net;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Covers docs/plan.md section 5: every failing HRESULT maps onto a specific exception type, and
/// cancellation is never reported as a runtime fault.
/// </summary>
public sealed class ErrorModelTests
{
    [Fact]
    public void SuccessCodesDoNotThrow()
    {
        Hr.ThrowIfFailed(HResult.SOk);
        Hr.ThrowIfFailed(HResult.SFalse);
    }

    [Fact]
    public void GameRuntimeExceptionPreservesNumericHResult()
    {
        var exception = new GameRuntimeException(HResult.EFail);

        Assert.Equal(HResult.EFail, exception.HResultCode);
        Assert.Equal(HResult.EFail, exception.HResult);
    }

    [Theory]
    [InlineData(HResult.EGameRuntimeNotInitialized)]
    [InlineData(HResult.EGameRuntimeDllNotFound)]
    [InlineData(HResult.EGameRuntimeUninitializeActiveObjects)]
    [InlineData(HResult.EFail)]
    public void RuntimeFailuresMapToGameRuntimeException(int hresult)
    {
        var exception = Assert.IsType<GameRuntimeException>(Hr.ToException(hresult));

        Assert.Equal(hresult, exception.HResultCode);
    }

    [Theory]
    [InlineData(HResult.EGameUserSignedOut)]
    [InlineData(HResult.EGameUserNoDefaultUser)]
    [InlineData(HResult.EGameUserUserNotInSandbox)]
    [InlineData(HResult.EGameUserNoPackageIdentity)]
    public void UserFailuresMapToUserException(int hresult)
    {
        var exception = Assert.IsType<UserException>(Hr.ToException(hresult));

        Assert.Equal(hresult, exception.HResultCode);
        Assert.IsAssignableFrom<GameRuntimeException>(exception);
    }

    [Fact]
    public void AbortMapsToOperationCanceledCarryingTheCallersToken()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var exception = Assert.IsType<OperationCanceledException>(Hr.ToException(HResult.EAbort, cts.Token));

        Assert.Equal(cts.Token, exception.CancellationToken);
    }

    [Fact]
    public void AbortIsNeverAGameRuntimeException()
    {
        Assert.Throws<OperationCanceledException>(() => Hr.ThrowIfFailed(HResult.EAbort));
    }

    [Fact]
    public void KnownCodesGetADescriptiveMessage()
    {
        var noPackage = new UserException(HResult.EGameUserNoPackageIdentity);

        Assert.Contains("package identity", noPackage.Message, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("0x", noPackage.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void UnknownCodesFallBackToTheHexHResult()
    {
        const int unknown = unchecked((int)0x80001234);

        Assert.Contains("0x80001234", new GameRuntimeException(unknown).Message, StringComparison.Ordinal);
    }
}
