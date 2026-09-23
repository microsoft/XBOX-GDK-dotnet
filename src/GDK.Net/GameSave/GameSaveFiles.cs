using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.GameSave;

/// <summary>
/// Simple-file access to game saves via the file system
/// (<c>XGameSaveFiles</c> family in <c>XGameSaveFiles.h</c>).
/// </summary>
/// <remarks>
/// <para>
/// <c>XGameSaveFiles</c> is an alternative to the blob-based <see cref="GameSaveProvider"/> API.
/// Instead of named blobs, it exposes a folder path that the title can read and write using
/// ordinary file I/O. Cloud sync is handled by the Gaming Runtime in the background.
/// </para>
/// <para>
/// Requires a signed-in <see cref="User"/> and the service configuration ID from
/// <c>MicrosoftGame.config</c>, just like <see cref="GameSaveProvider"/>.
/// </para>
/// </remarks>
public static class GameSaveFiles
{
    // Initial buffer size for the folder path. Grown if the path is longer than expected.
    private const int InitialFolderBufferSize = 1024;
    private const int MaxFolderBufferSize = 8192;

    /// <summary>
    /// Returns the local folder path for this user's game saves, showing system UI to resolve
    /// sync conflicts when necessary (<c>XGameSaveFilesGetFolderWithUiAsync</c> /
    /// <c>XGameSaveFilesGetFolderWithUiResult</c>).
    /// </summary>
    /// <param name="user">The signed-in user whose save folder is requested.</param>
    /// <param name="serviceConfigurationId">The SCID from <c>MicrosoftGame.config</c>.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <returns>Absolute path to the folder; writable by the title.</returns>
    public static unsafe Task<string> GetFolderWithUiAsync(
        User user,
        string serviceConfigurationId,
        CancellationToken cancellationToken = default)
    {
        GameTaskQueue? queue = null;
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (serviceConfigurationId is null) throw new ArgumentNullException(nameof(serviceConfigurationId));

        IntPtr userHandle = user.Handle;
        {
            byte[] scidBytes = GameSaveProvider.EncodeUtf8(serviceConfigurationId);

            // scidPtr is only needed during XGameSaveFilesGetFolderWithUiAsync (the _In_ start
            // call), which is synchronous.  Pin for the duration of AsyncOperation.Start only.
            GCHandle scidPin = GCHandle.Alloc(scidBytes, GCHandleType.Pinned);
            Task<string> innerTask;
            try
            {
                byte* scidPtr = (byte*)scidPin.AddrOfPinnedObject();
                innerTask = AsyncOperation<string>.RunAsync(
                    queue.RawHandle(),
                    block => Native.XGameSaveFilesGetFolderWithUiAsync(
                        userHandle, scidPtr, (XAsyncBlock*)block),
                    static (IntPtr block, out string value) =>
                    {
                        value = string.Empty;
                        int bufSize = InitialFolderBufferSize;
                        while (bufSize <= MaxFolderBufferSize)
                        {
                            byte[] buf = new byte[bufSize];
                            int hr2;
                            fixed (byte* p = buf)
                            {
                                hr2 = Native.XGameSaveFilesGetFolderWithUiResult(
                                    (XAsyncBlock*)block, (nuint)bufSize, p);
                            }

                            if (hr2 == HResult.EGsProvidedBufferTooSmall)
                            {
                                bufSize *= 2;
                                continue;
                            }

                            if (HResult.Failed(hr2)) return hr2;

                            fixed (byte* p = buf)
                            {
                                value = GameSaveCallbacks.PtrToStringUtf8(p);
                            }

                            return HResult.SOk;
                        }

                        return HResult.EGsProvidedBufferTooSmall;
                    },
                    cancellationToken);
            }
            finally
            {
                scidPin.Free();
            }

            return innerTask;
        }
    }

    /// <summary>
    /// Returns the remaining quota in bytes for this user's game-save files
    /// (<c>XGameSaveFilesGetRemainingQuota</c>).
    /// </summary>
    /// <param name="user">The signed-in user.</param>
    /// <param name="serviceConfigurationId">The SCID from <c>MicrosoftGame.config</c>.</param>
    public static unsafe long GetRemainingQuota(User user, string serviceConfigurationId)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (serviceConfigurationId is null) throw new ArgumentNullException(nameof(serviceConfigurationId));

        byte[] scidBytes = GameSaveProvider.EncodeUtf8(serviceConfigurationId);
        long quota;
        int hr;
        fixed (byte* scidPtr = scidBytes)
        {
            hr = Native.XGameSaveFilesGetRemainingQuota(user.Handle, scidPtr, &quota);
        }

        Hr.ThrowIfFailed(hr);
        return quota;
    }
}
