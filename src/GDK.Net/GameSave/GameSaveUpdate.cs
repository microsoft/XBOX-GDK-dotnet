using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.GameSave;

/// <summary>
/// Stages a set of blob writes and deletes within a container for atomic commitment.
/// Wraps <c>XGameSaveUpdateHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// Obtain an update via <see cref="GameSaveContainer.CreateUpdate"/>. Stage changes using
/// <see cref="Write"/> and <see cref="Delete"/>, then commit atomically with
/// <see cref="SubmitAsync"/> (or the synchronous <see cref="Submit"/>).
/// If any blob in the update fails, the entire update is rolled back.
/// </para>
/// <para>
/// <b>Data lifetime</b>: <see cref="Write"/> copies blob data into unmanaged memory owned by
/// this update. The unmanaged copy remains valid until the update is submitted (or disposed).
/// Do NOT assume the data is written to storage immediately; it is persisted only when the submit
/// call completes.
/// </para>
/// <para>
/// Disposing without submitting silently discards all staged changes.
/// </para>
/// </remarks>
public sealed unsafe class GameSaveUpdate : IDisposable
{
    private readonly GameSaveUpdateHandle _handle;
    private readonly GameTaskQueue? _queue;

    // Unmanaged copies of blob data that must remain valid until the update is submitted.
    // Transferred to the async closure on submit; freed on dispose if submit was never called.
    private readonly List<IntPtr> _unmanagedAllocations = new();

    private bool _disposed;

    internal GameSaveUpdate(GameSaveUpdateHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Staging
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Stages a blob write (<c>XGameSaveSubmitBlobWrite</c>).
    /// </summary>
    /// <remarks>
    /// The supplied data is copied into unmanaged memory owned by this update and is held there
    /// until <see cref="Submit"/> / <see cref="SubmitAsync"/> completes, or until this update is
    /// disposed.
    /// </remarks>
    /// <param name="name">
    /// Blob name. Maximum 64 characters (GS_MAX_BLOB_NAME_SIZE minus the null terminator).
    /// </param>
    /// <param name="data">Data to write. Maximum 16 MB (GS_MAX_BLOB_SIZE).</param>
    public void Write(string name, byte[] data)
    {
        ThrowIfDisposed();
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (data is null) throw new ArgumentNullException(nameof(data));

        // Copy data to unmanaged memory so it remains valid for the update's lifetime.
        IntPtr unmanagedData = Marshal.AllocHGlobal(data.Length > 0 ? data.Length : 1);
        try
        {
            if (data.Length > 0)
            {
                Marshal.Copy(data, 0, unmanagedData, data.Length);
            }

            byte[] nameBytes = GameSaveProvider.EncodeUtf8(name);
            int hr;
            fixed (byte* namePtr = nameBytes)
            {
                hr = Native.XGameSaveSubmitBlobWrite(
                    _handle.DangerousGetHandle(),
                    namePtr,
                    (byte*)unmanagedData,
                    (nuint)data.Length);
            }

            Hr.ThrowIfFailed(hr);

            // Track only after a successful XGameSaveSubmitBlobWrite.
            _unmanagedAllocations.Add(unmanagedData);
        }
        catch
        {
            Marshal.FreeHGlobal(unmanagedData);
            throw;
        }
    }

    /// <summary>
    /// Stages a blob deletion (<c>XGameSaveSubmitBlobDelete</c>).
    /// </summary>
    /// <param name="name">Name of the blob to delete.</param>
    public void Delete(string name)
    {
        ThrowIfDisposed();
        if (name is null) throw new ArgumentNullException(nameof(name));

        byte[] nameBytes = GameSaveProvider.EncodeUtf8(name);
        int hr;
        fixed (byte* namePtr = nameBytes)
        {
            hr = Native.XGameSaveSubmitBlobDelete(_handle.DangerousGetHandle(), namePtr);
        }

        Hr.ThrowIfFailed(hr);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Commit
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Commits all staged changes synchronously (<c>XGameSaveSubmitUpdate</c>).
    /// </summary>
    /// <remarks>
    /// After a successful commit the unmanaged blob-data copies are freed.
    /// </remarks>
    public void Submit()
    {
        ThrowIfDisposed();

        List<IntPtr> allocations = TransferAllocations();
        try
        {
            Hr.ThrowIfFailed(Native.XGameSaveSubmitUpdate(_handle.DangerousGetHandle()));
        }
        finally
        {
            FreeAllocations(allocations);
        }
    }

    /// <summary>
    /// Commits all staged changes asynchronously
    /// (<c>XGameSaveSubmitUpdateAsync</c> / <c>XGameSaveSubmitUpdateResult</c>).
    /// </summary>
    /// <remarks>
    /// The unmanaged blob-data copies are freed after the async operation completes (whether
    /// successfully or not), so they are valid for the entire duration of the write.
    /// </remarks>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task SubmitAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr updateHandle = _handle.DangerousGetHandle();
        List<IntPtr> allocations = TransferAllocations();

        return AsyncOperation<bool>.RunAsync(
            _queue.RawHandle(),
            block => Native.XGameSaveSubmitUpdateAsync(updateHandle, (XAsyncBlock*)block),
            (IntPtr block, out bool value) =>
            {
                value = true;
                try
                {
                    return Native.XGameSaveSubmitUpdateResult((XAsyncBlock*)block);
                }
                finally
                {
                    FreeAllocations(allocations);
                }
            },
            cancellationToken);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // IDisposable
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Closes the update handle (<c>XGameSaveCloseUpdate</c>) and frees any unsubmitted blob-data
    /// copies. Staged changes are discarded.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        FreeAllocations(_unmanagedAllocations);
        _unmanagedAllocations.Clear();
        _handle.Dispose();
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Helpers
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Transfers ownership of the unmanaged allocations to the caller (e.g. the async closure).
    /// The internal list is cleared so Dispose does not double-free.
    /// </summary>
    private List<IntPtr> TransferAllocations()
    {
        var copy = new List<IntPtr>(_unmanagedAllocations);
        _unmanagedAllocations.Clear();
        return copy;
    }

    private static void FreeAllocations(List<IntPtr> allocations)
    {
        foreach (IntPtr p in allocations)
        {
            Marshal.FreeHGlobal(p);
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameSaveUpdate));
        }
    }
}
