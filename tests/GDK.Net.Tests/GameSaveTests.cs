using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.GameSave;
using GDK.Net.Interop;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the XGameSave / XGameSaveFiles projection.
///
/// Rules:
/// - No native calls. <c>xgameruntime.thunks.dll</c> is not present on CI runners.
/// - Assert HRESULT constant values against the header, struct layout, and public-type semantics.
/// </summary>
public sealed unsafe class GameSaveTests
{
    // ──────────────────────────────────────────────────────────────────────────
    // HRESULT constants (XGameSave.h, edition 260404)
    // ──────────────────────────────────────────────────────────────────────────

    [Theory]
    [InlineData(HResult.EGsInvalidContainerName, unchecked((int)0x80830001u))]
    [InlineData(HResult.EGsNoAccess, unchecked((int)0x80830002u))]
    [InlineData(HResult.EGsOutOfLocalStorage, unchecked((int)0x80830003u))]
    [InlineData(HResult.EGsUserCanceled, unchecked((int)0x80830004u))]
    [InlineData(HResult.EGsUpdateTooBig, unchecked((int)0x80830005u))]
    [InlineData(HResult.EGsQuotaExceeded, unchecked((int)0x80830006u))]
    [InlineData(HResult.EGsProvidedBufferTooSmall, unchecked((int)0x80830007u))]
    [InlineData(HResult.EGsBlobNotFound, unchecked((int)0x80830008u))]
    [InlineData(HResult.EGsNoServiceConfiguration, unchecked((int)0x80830009u))]
    [InlineData(HResult.EGsContainerNotInSync, unchecked((int)0x8083000Au))]
    [InlineData(HResult.EGsContainerSyncFailed, unchecked((int)0x8083000Bu))]
    [InlineData(HResult.EGsUserNotRegisteredInService, unchecked((int)0x8083000Cu))]
    [InlineData(HResult.EGsHandleExpired, unchecked((int)0x8083000Du))]
    [InlineData(HResult.EGsAsyncFunctionRequired, unchecked((int)0x8083000Eu))]
    [InlineData(HResult.EGsProviderMismatch, unchecked((int)0x8083000Fu))]
    [InlineData(HResult.EGsUserQuit, unchecked((int)0x80830010u))]
    public void GameSaveHResultMatchesHeader(int actual, int expected)
    {
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AllGameSaveHResultsAreFailing()
    {
        Assert.True(HResult.Failed(HResult.EGsInvalidContainerName));
        Assert.True(HResult.Failed(HResult.EGsNoAccess));
        Assert.True(HResult.Failed(HResult.EGsQuotaExceeded));
        Assert.True(HResult.Failed(HResult.EGsBlobNotFound));
        Assert.True(HResult.Failed(HResult.EGsUserQuit));
    }

    [Theory]
    [InlineData(HResult.EGsInvalidContainerName)]
    [InlineData(HResult.EGsNoAccess)]
    [InlineData(HResult.EGsOutOfLocalStorage)]
    [InlineData(HResult.EGsUserCanceled)]
    [InlineData(HResult.EGsUpdateTooBig)]
    [InlineData(HResult.EGsQuotaExceeded)]
    [InlineData(HResult.EGsProvidedBufferTooSmall)]
    [InlineData(HResult.EGsBlobNotFound)]
    [InlineData(HResult.EGsNoServiceConfiguration)]
    [InlineData(HResult.EGsContainerNotInSync)]
    [InlineData(HResult.EGsContainerSyncFailed)]
    [InlineData(HResult.EGsUserNotRegisteredInService)]
    [InlineData(HResult.EGsHandleExpired)]
    [InlineData(HResult.EGsAsyncFunctionRequired)]
    [InlineData(HResult.EGsProviderMismatch)]
    [InlineData(HResult.EGsUserQuit)]
    public void GameSaveHResultsThrowGameRuntimeException(int hresult)
    {
        // E_GS_* codes must project as GameRuntimeException (not raw or UserException).
        var ex = Assert.IsType<GameRuntimeException>(Hr.ToException(hresult));
        Assert.Equal(hresult, ex.HResultCode);
        Assert.False(string.IsNullOrWhiteSpace(ex.Message));
    }

    [Fact]
    public void GameSaveHResultMessagesAreDescriptive()
    {
        var ex = new GameRuntimeException(HResult.EGsQuotaExceeded);
        Assert.Contains("quota", ex.Message, StringComparison.OrdinalIgnoreCase);

        var ex2 = new GameRuntimeException(HResult.EGsBlobNotFound);
        Assert.Contains("blob", ex2.Message, StringComparison.OrdinalIgnoreCase);

        var ex3 = new GameRuntimeException(HResult.EGsInvalidContainerName);
        Assert.Contains("container", ex3.Message, StringComparison.OrdinalIgnoreCase);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Native struct layout
    // ──────────────────────────────────────────────────────────────────────────

    [Fact]
    public void NativeGameSaveBlobInfoHasCorrectLayout()
    {
        // struct XGameSaveBlobInfo { const char* name (8); uint32_t size (4); pad(4) } = 16 bytes
        Assert.Equal(16, Marshal.SizeOf<NativeGameSaveBlobInfo>());
        Assert.Equal(0, (int)Marshal.OffsetOf<NativeGameSaveBlobInfo>(nameof(NativeGameSaveBlobInfo.name)));
        Assert.Equal(8, (int)Marshal.OffsetOf<NativeGameSaveBlobInfo>(nameof(NativeGameSaveBlobInfo.size)));
    }

    [Fact]
    public void NativeGameSaveBlobHasCorrectLayout()
    {
        // struct XGameSaveBlob { XGameSaveBlobInfo info (16); uint8_t* data (8) } = 24 bytes
        Assert.Equal(24, Marshal.SizeOf<NativeGameSaveBlob>());
        Assert.Equal(0, (int)Marshal.OffsetOf<NativeGameSaveBlob>(nameof(NativeGameSaveBlob.info)));
        Assert.Equal(16, (int)Marshal.OffsetOf<NativeGameSaveBlob>(nameof(NativeGameSaveBlob.data)));
    }

    [Fact]
    public void NativeGameSaveContainerInfoHasCorrectLayout()
    {
        // struct XGameSaveContainerInfo:
        //   const char* name(8) + const char* displayName(8) + uint32_t blobCount(4) + pad(4)
        //   + uint64_t totalSize(8) + time_t lastModifiedTime(8) + bool needsSync(1) + pad(7)
        //   = 48 bytes
        Assert.Equal(48, Marshal.SizeOf<NativeGameSaveContainerInfo>());
        Assert.Equal(0, (int)Marshal.OffsetOf<NativeGameSaveContainerInfo>(nameof(NativeGameSaveContainerInfo.name)));
        Assert.Equal(8, (int)Marshal.OffsetOf<NativeGameSaveContainerInfo>(nameof(NativeGameSaveContainerInfo.displayName)));
        Assert.Equal(16, (int)Marshal.OffsetOf<NativeGameSaveContainerInfo>(nameof(NativeGameSaveContainerInfo.blobCount)));
        Assert.Equal(24, (int)Marshal.OffsetOf<NativeGameSaveContainerInfo>(nameof(NativeGameSaveContainerInfo.totalSize)));
        Assert.Equal(32, (int)Marshal.OffsetOf<NativeGameSaveContainerInfo>(nameof(NativeGameSaveContainerInfo.lastModifiedTime)));
        Assert.Equal(40, (int)Marshal.OffsetOf<NativeGameSaveContainerInfo>(nameof(NativeGameSaveContainerInfo.needsSync)));
    }

    // ──────────────────────────────────────────────────────────────────────────
    // GameSaveSyncState enum values (XGameSave.h)
    // ──────────────────────────────────────────────────────────────────────────

    [Theory]
    [InlineData(GameSaveSyncState.NotStarted, 0u)]
    [InlineData(GameSaveSyncState.PreparingForDownload, 1u)]
    [InlineData(GameSaveSyncState.Downloading, 2u)]
    [InlineData(GameSaveSyncState.PreparingForUpload, 3u)]
    [InlineData(GameSaveSyncState.Uploading, 4u)]
    [InlineData(GameSaveSyncState.SyncComplete, 5u)]
    public void GameSaveSyncStateMatchesHeader(GameSaveSyncState value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Callback trampolines
    // ──────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GameSaveCallbackPointersAreNonZeroAndDistinct()
    {
        Assert.NotEqual(IntPtr.Zero, GameSaveCallbacks.ContainerInfoCallback);
        Assert.NotEqual(IntPtr.Zero, GameSaveCallbacks.BlobInfoCallback);
        Assert.NotEqual(GameSaveCallbacks.ContainerInfoCallback, GameSaveCallbacks.BlobInfoCallback);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Public type constructors and properties
    // ──────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GameSaveContainerInfoExposesAllFields()
    {
        var now = DateTimeOffset.UtcNow;
        var info = new GameSaveContainerInfo("SaveSlot1", "Save Slot 1", 3, 1024L, now, needsSync: true);

        Assert.Equal("SaveSlot1", info.Name);
        Assert.Equal("Save Slot 1", info.DisplayName);
        Assert.Equal(3u, info.BlobCount);
        Assert.Equal(1024L, info.TotalSize);
        Assert.Equal(now, info.LastModified);
        Assert.True(info.NeedsSync);
    }

    [Fact]
    public void GameSaveBlobInfoExposesAllFields()
    {
        var info = new GameSaveBlobInfo("checkpoint", 512u);
        Assert.Equal("checkpoint", info.Name);
        Assert.Equal(512u, info.Size);
    }

    [Fact]
    public void GameSaveBlobExposesAllFields()
    {
        byte[] data = new byte[] { 1, 2, 3 };
        var blob = new GameSaveBlob("data", 3u, data);
        Assert.Equal("data", blob.Name);
        Assert.Equal(3u, blob.Size);
        Assert.Equal(data, blob.Data);
    }

    [Fact]
    public void GameSaveContainerInfoToStringIsReadable()
    {
        var info = new GameSaveContainerInfo("Slot1", "Slot 1", 2, 256L, DateTimeOffset.UtcNow, false);
        string s = info.ToString();
        Assert.Contains("Slot1", s, StringComparison.Ordinal);
        Assert.Contains("Blobs=2", s, StringComparison.Ordinal);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Error model: E_GS_* flows through Hr.ThrowIfFailed
    // ──────────────────────────────────────────────────────────────────────────

    [Fact]
    public void ThrowIfFailedRaisesOnGameSaveCodes()
    {
        Assert.Throws<GameRuntimeException>(() => Hr.ThrowIfFailed(HResult.EGsQuotaExceeded));
        Assert.Throws<GameRuntimeException>(() => Hr.ThrowIfFailed(HResult.EGsContainerSyncFailed));
        Assert.Throws<GameRuntimeException>(() => Hr.ThrowIfFailed(HResult.EGsNoServiceConfiguration));
    }

    [Fact]
    public void GameSaveExceptionPreservesHResultCode()
    {
        var ex = new GameRuntimeException(HResult.EGsContainerNotInSync);
        Assert.Equal(HResult.EGsContainerNotInSync, ex.HResultCode);
        Assert.Equal(HResult.EGsContainerNotInSync, ex.HResult);
        Assert.True(HResult.Failed(ex.HResultCode));
    }

    [Fact]
    public void GameSaveIsNotAUserException()
    {
        // E_GS_* codes are NOT in the E_GAMEUSER_* family.
        Assert.False(HResult.IsGameUser(HResult.EGsQuotaExceeded));
        Assert.False(HResult.IsGameUser(HResult.EGsBlobNotFound));

        var ex = Hr.ToException(HResult.EGsQuotaExceeded);
        Assert.IsType<GameRuntimeException>(ex);
        Assert.IsNotType<UserException>(ex);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // GameSaveProvider / GameSaveUpdate argument validation (no native calls)
    // ──────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GameSaveProviderInitializeThrowsOnNullArguments()
    {
        // Null guard fires before any native code. Using the sync overload avoids xUnit2014.
        Assert.Throws<ArgumentNullException>(() =>
            GameSaveProvider.Initialize(null!, "scid-1234"));
        Assert.Throws<ArgumentNullException>(() =>
            GameSaveProvider.Initialize(null!, null!));
    }

    [Fact]
    public void EncodeUtf8ProducesNullTerminatedBytes()
    {
        byte[] bytes = GameSaveProvider.EncodeUtf8("hello");
        Assert.Equal(6, bytes.Length);      // 5 chars + null terminator
        Assert.Equal((byte)'h', bytes[0]);
        Assert.Equal((byte)'o', bytes[4]);
        Assert.Equal(0, bytes[5]);          // null terminator
    }

    [Fact]
    public void EncodeUtf8HandlesEmptyString()
    {
        byte[] bytes = GameSaveProvider.EncodeUtf8(string.Empty);
        Assert.Single(bytes);
        Assert.Equal(0, bytes[0]);
    }

    [Fact]
    public void EncodeUtf8HandlesMultibyteCharacters()
    {
        // Snowman U+2603 encodes to 3 UTF-8 bytes: 0xE2 0x98 0x83
        byte[] bytes = GameSaveProvider.EncodeUtf8("\u2603");
        Assert.Equal(4, bytes.Length); // 3 bytes + null
        Assert.Equal(0xE2, bytes[0]);
        Assert.Equal(0x98, bytes[1]);
        Assert.Equal(0x83, bytes[2]);
        Assert.Equal(0, bytes[3]);
    }
}
