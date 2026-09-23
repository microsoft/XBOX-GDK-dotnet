using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.GameSave;

/// <summary>
/// Binds a signed-in user and a service configuration ID to their game-save storage, exposing
/// container management, quota queries and cloud-synced persistence.
/// </summary>
/// <remarks>
/// <para>
/// A provider is the root object of the XGameSave family. Obtain one via
/// <see cref="InitializeAsync"/> (or the synchronous <see cref="Initialize"/>). Both require a
/// signed-in <see cref="User"/> and the service configuration ID (SCID) from
/// <c>MicrosoftGame.config</c>.
/// </para>
/// <para>
/// Lifecycle ordering: the provider must outlive all containers created from it, which must
/// outlive all updates created from those containers. Dispose in reverse-creation order.
/// </para>
/// <para>
/// The default provider quota is 256 MB. Each blob may be at most 16 MB.
/// </para>
/// </remarks>
public sealed unsafe class GameSaveProvider : IDisposable
{
    private readonly GameSaveProviderHandle _handle;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    private GameSaveProvider(GameSaveProviderHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Factory methods
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Initialises a game-save provider synchronously (<c>XGameSaveInitializeProvider</c>).
    /// </summary>
    /// <remarks>
    /// This call may block while performing an initial cloud sync when
    /// <paramref name="syncOnDemand"/> is <see langword="false"/>. Prefer
    /// <see cref="InitializeAsync"/> on the game thread.
    /// </remarks>
    /// <param name="user">The signed-in user whose saves are accessed.</param>
    /// <param name="serviceConfigurationId">
    /// The SCID from <c>MicrosoftGame.config</c>. Must not exceed 64 characters.
    /// </param>
    /// <param name="syncOnDemand">
    /// When <see langword="true"/> only container metadata is synced at init time; container data
    /// is synced lazily on first access. When <see langword="false"/> all data is synced upfront.
    /// </param>
    public static GameSaveProvider Initialize(
        User user,
        string serviceConfigurationId,
        bool syncOnDemand = false)
    {
        GameTaskQueue? queue = null;
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (serviceConfigurationId is null) throw new ArgumentNullException(nameof(serviceConfigurationId));

        IntPtr userHandle = user.Handle;
        {
            byte[] scidBytes = EncodeUtf8(serviceConfigurationId);

            IntPtr raw;
            int hr;
            fixed (byte* scidPtr = scidBytes)
            {
                hr = Native.XGameSaveInitializeProvider(
                    userHandle,
                    scidPtr,
                    syncOnDemand ? (byte)1 : (byte)0,
                    &raw);
            }

            Hr.ThrowIfFailed(hr);
            return new GameSaveProvider(new GameSaveProviderHandle(raw), queue);
        }
    }

    /// <summary>
    /// Initialises a game-save provider asynchronously (<c>XGameSaveInitializeProviderAsync</c> /
    /// <c>XGameSaveInitializeProviderResult</c>).
    /// </summary>
    /// <param name="user">The signed-in user whose saves are accessed.</param>
    /// <param name="serviceConfigurationId">
    /// The SCID from <c>MicrosoftGame.config</c>. Must not exceed 64 characters.
    /// </param>
    /// <param name="syncOnDemand">
    /// When <see langword="true"/> only container metadata is synced at init time.
    /// </param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public static Task<GameSaveProvider> InitializeAsync(
        User user,
        string serviceConfigurationId,
        bool syncOnDemand = false,
        CancellationToken cancellationToken = default)
    {
        GameTaskQueue? queue = null;
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (serviceConfigurationId is null) throw new ArgumentNullException(nameof(serviceConfigurationId));

        IntPtr userHandle = user.Handle;
        {
            byte[] scidBytes = EncodeUtf8(serviceConfigurationId);
            byte syncByte = syncOnDemand ? (byte)1 : (byte)0;

            // Pin scidBytes for the duration of the async start call only (_In_ annotation).
            GCHandle scidPin = GCHandle.Alloc(scidBytes, GCHandleType.Pinned);
            try
            {
                byte* scidPtr = (byte*)scidPin.AddrOfPinnedObject();

                return AsyncOperation<GameSaveProvider>.RunAsync(
                    queue.RawHandle(),
                    block => Native.XGameSaveInitializeProviderAsync(
                        userHandle, scidPtr, syncByte, (XAsyncBlock*)block),
                    (IntPtr block, out GameSaveProvider value) =>
                    {
                        value = null!;
                        IntPtr raw;
                        int hr = Native.XGameSaveInitializeProviderResult((XAsyncBlock*)block, &raw);
                        if (HResult.Failed(hr)) return hr;
                        value = new GameSaveProvider(new GameSaveProviderHandle(raw), queue);
                        return HResult.SOk;
                    },
                    cancellationToken);
            }
            finally
            {
                scidPin.Free();
            }
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Quota
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the remaining storage quota in bytes (<c>XGameSaveGetRemainingQuota</c>).
    /// </summary>
    public long GetRemainingQuota()
    {
        ThrowIfDisposed();
        long quota;
        Hr.ThrowIfFailed(Native.XGameSaveGetRemainingQuota(_handle.DangerousGetHandle(), &quota));
        return quota;
    }

    /// <summary>
    /// Returns the remaining storage quota in bytes asynchronously
    /// (<c>XGameSaveGetRemainingQuotaAsync</c> / <c>XGameSaveGetRemainingQuotaResult</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<long> GetRemainingQuotaAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr providerHandle = _handle.DangerousGetHandle();

        return AsyncOperation<long>.RunAsync(
            _queue.RawHandle(),
            block => Native.XGameSaveGetRemainingQuotaAsync(providerHandle, (XAsyncBlock*)block),
            static (IntPtr block, out long value) =>
            {
                value = 0;
                long quota;
                int hr = Native.XGameSaveGetRemainingQuotaResult((XAsyncBlock*)block, &quota);
                if (HResult.Failed(hr)) return hr;
                value = quota;
                return HResult.SOk;
            },
            cancellationToken);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Container enumeration
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Enumerates all containers in the provider (<c>XGameSaveEnumerateContainerInfo</c>).
    /// </summary>
    /// <returns>A snapshot list; deep-copied from native memory.</returns>
    public IReadOnlyList<GameSaveContainerInfo> EnumerateContainers()
    {
        ThrowIfDisposed();
        return EnumerateContainersCore(prefixBytes: null);
    }

    /// <summary>
    /// Enumerates containers whose names begin with <paramref name="namePrefix"/>
    /// (<c>XGameSaveEnumerateContainerInfoByName</c>).
    /// </summary>
    public IReadOnlyList<GameSaveContainerInfo> EnumerateContainersByPrefix(string namePrefix)
    {
        ThrowIfDisposed();
        if (namePrefix is null) throw new ArgumentNullException(nameof(namePrefix));
        return EnumerateContainersCore(EncodeUtf8(namePrefix));
    }

    /// <summary>
    /// Retrieves the info for a single named container (<c>XGameSaveGetContainerInfo</c>).
    /// </summary>
    /// <exception cref="GameRuntimeException">Thrown when the container does not exist.</exception>
    public GameSaveContainerInfo GetContainerInfo(string name)
    {
        ThrowIfDisposed();
        if (name is null) throw new ArgumentNullException(nameof(name));

        var list = new List<GameSaveContainerInfo>(1);
        var gcHandle = GCHandle.Alloc(list);
        try
        {
            byte[] nameBytes = EncodeUtf8(name);
            int hr;
            fixed (byte* namePtr = nameBytes)
            {
                hr = Native.XGameSaveGetContainerInfo(
                    _handle.DangerousGetHandle(),
                    namePtr,
                    GCHandle.ToIntPtr(gcHandle),
                    GameSaveCallbacks.ContainerInfoCallback);
            }

            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            gcHandle.Free();
        }

        return list.Count > 0
            ? list[0]
            : throw new GameRuntimeException(HResult.EFail, $"Container '{name}' was not found.");
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Container lifecycle
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Opens or creates a container with <paramref name="name"/> (<c>XGameSaveCreateContainer</c>).
    /// </summary>
    /// <remarks>
    /// Dispose the returned <see cref="GameSaveContainer"/> before disposing this provider.
    /// </remarks>
    /// <param name="name">
    /// Container name. Maximum 255 characters (GS_MAX_CONTAINER_NAME_SIZE).
    /// </param>
    public GameSaveContainer CreateContainer(string name)
    {
        ThrowIfDisposed();
        if (name is null) throw new ArgumentNullException(nameof(name));

        byte[] nameBytes = EncodeUtf8(name);
        IntPtr raw;
        int hr;
        fixed (byte* namePtr = nameBytes)
        {
            hr = Native.XGameSaveCreateContainer(_handle.DangerousGetHandle(), namePtr, &raw);
        }

        Hr.ThrowIfFailed(hr);
        return new GameSaveContainer(new GameSaveContainerHandle(raw), _queue);
    }

    /// <summary>
    /// Deletes a container and all its blobs synchronously (<c>XGameSaveDeleteContainer</c>).
    /// </summary>
    public void DeleteContainer(string name)
    {
        ThrowIfDisposed();
        if (name is null) throw new ArgumentNullException(nameof(name));

        byte[] nameBytes = EncodeUtf8(name);
        int hr;
        fixed (byte* namePtr = nameBytes)
        {
            hr = Native.XGameSaveDeleteContainer(_handle.DangerousGetHandle(), namePtr);
        }

        Hr.ThrowIfFailed(hr);
    }

    /// <summary>
    /// Deletes a container and all its blobs asynchronously
    /// (<c>XGameSaveDeleteContainerAsync</c> / <c>XGameSaveDeleteContainerResult</c>).
    /// </summary>
    /// <param name="name">The name of the container to delete.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task DeleteContainerAsync(string name, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        if (name is null) throw new ArgumentNullException(nameof(name));

        IntPtr providerHandle = _handle.DangerousGetHandle();
        byte[] nameBytes = EncodeUtf8(name);

        GCHandle namePin = GCHandle.Alloc(nameBytes, GCHandleType.Pinned);
        try
        {
            byte* namePtr = (byte*)namePin.AddrOfPinnedObject();

            return AsyncOperation<bool>.RunAsync(
                _queue.RawHandle(),
                block => Native.XGameSaveDeleteContainerAsync(providerHandle, namePtr, (XAsyncBlock*)block),
                static (IntPtr block, out bool value) =>
                {
                    value = true;
                    return Native.XGameSaveDeleteContainerResult((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        finally
        {
            namePin.Free();
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // IDisposable
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Closes the provider handle (<c>XGameSaveCloseProvider</c>). All containers and updates
    /// derived from this provider must be disposed first.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Helpers
    // ──────────────────────────────────────────────────────────────────────────

    private IReadOnlyList<GameSaveContainerInfo> EnumerateContainersCore(byte[]? prefixBytes)
    {
        var list = new List<GameSaveContainerInfo>();
        var gcHandle = GCHandle.Alloc(list);
        try
        {
            IntPtr providerHandle = _handle.DangerousGetHandle();
            IntPtr context = GCHandle.ToIntPtr(gcHandle);
            IntPtr callback = GameSaveCallbacks.ContainerInfoCallback;

            int hr;
            if (prefixBytes is null)
            {
                hr = Native.XGameSaveEnumerateContainerInfo(providerHandle, context, callback);
            }
            else
            {
                fixed (byte* prefixPtr = prefixBytes)
                {
                    hr = Native.XGameSaveEnumerateContainerInfoByName(
                        providerHandle, prefixPtr, context, callback);
                }
            }

            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            gcHandle.Free();
        }

        return list;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameSaveProvider));
        }
    }

    /// <summary>Encodes a string as a null-terminated UTF-8 byte array.</summary>
    internal static byte[] EncodeUtf8(string s)
    {
        int byteCount = Encoding.UTF8.GetByteCount(s);
        byte[] bytes = new byte[byteCount + 1]; // +1 for null terminator; byte[] is zero-initialized
        Encoding.UTF8.GetBytes(s, 0, s.Length, bytes, 0);
        return bytes;
    }
}
