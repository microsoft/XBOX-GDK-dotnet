using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net;

/// <summary>Starts a native async call against the supplied <c>XAsyncBlock*</c>.</summary>
internal delegate int AsyncStarter(IntPtr asyncBlock);

/// <summary>Reads the result of a completed native async call.</summary>
internal delegate int AsyncResultReader<TValue>(IntPtr asyncBlock, out TValue value);

/// <summary>Completes a native async call that produces no value.</summary>
internal delegate int AsyncCompleter(IntPtr asyncBlock);

/// <summary>
/// Non-generic half of the async engine, so the native completion thunk has a single, non-generic
/// entry point to dispatch through (<c>[UnmanagedCallersOnly]</c> methods cannot live on a generic
/// type).
/// </summary>
internal abstract unsafe class AsyncOperation
{
    private readonly object _gate = new();
    private readonly CancellationToken _cancellationToken;
    private CancellationTokenRegistration _registration;
    private GCHandle _self;
    private IntPtr _block;
    private bool _finished;

    private protected AsyncOperation(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    private protected CancellationToken CancellationToken => _cancellationToken;

    /// <summary>
    /// Runs a native async call that produces no value, returning a <see cref="Task"/> that completes
    /// when the runtime does.
    /// </summary>
    internal static Task RunAsync(
        IntPtr queueHandle,
        AsyncStarter starter,
        AsyncCompleter completer,
        CancellationToken cancellationToken)
    {
        return AsyncOperation<bool>.RunAsync(
            queueHandle,
            starter,
            (IntPtr block, out bool value) =>
            {
                value = true;
                return completer(block);
            },
            cancellationToken);
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> when the Gaming Runtime completes a call.</summary>
    internal static void Dispatch(IntPtr asyncBlock)
    {
        var block = (XAsyncBlock*)asyncBlock;
        var handle = GCHandle.FromIntPtr(block->Context);
        if (handle.Target is not AsyncOperation operation)
        {
            return;
        }

        try
        {
            operation.OnCompleted(asyncBlock);
        }
        catch (Exception ex)
        {
            operation.SetException(ex);
        }
        finally
        {
            operation.Cleanup();
        }
    }

    /// <summary>
    /// Allocates the async block, wires the completion thunk and cancellation, then starts the call.
    /// On a failing start nothing is left allocated and the mapped exception is thrown.
    /// </summary>
    private protected void Start(IntPtr queueHandle, AsyncStarter starter)
    {
        // The block is unmanaged memory rather than a pinned managed struct: the Gaming Runtime
        // holds the pointer until completion, and unmanaged memory cannot be moved or collected.
        _block = Marshal.AllocHGlobal(sizeof(XAsyncBlock));
        var block = (XAsyncBlock*)_block;
        *block = default;
        block->Queue = queueHandle;
        block->Callback = Trampolines.AsyncCompletionRoutine;

        _self = GCHandle.Alloc(this);
        block->Context = GCHandle.ToIntPtr(_self);

        int hr;
        try
        {
            hr = starter(_block);
        }
        catch
        {
            Cleanup();
            throw;
        }

        if (HResult.Failed(hr))
        {
            Cleanup();
            throw Hr.ToException(hr, _cancellationToken);
        }

        if (_cancellationToken.CanBeCanceled)
        {
            // Registered only after a successful start: XAsyncCancel is undefined on a block that
            // was never handed to the runtime.
            _registration = _cancellationToken.Register(static state => ((AsyncOperation)state!).Cancel(), this);
        }
    }

    private protected abstract void OnCompleted(IntPtr asyncBlock);

    private protected abstract void SetException(Exception exception);

    private void Cancel()
    {
        lock (_gate)
        {
            if (_finished || _block == IntPtr.Zero)
            {
                return;
            }

            Native.XAsyncCancel((XAsyncBlock*)_block);
        }
    }

    private void Cleanup()
    {
        IntPtr block;
        lock (_gate)
        {
            if (_finished)
            {
                return;
            }

            _finished = true;
            block = _block;
            _block = IntPtr.Zero;
        }

        // Outside the lock: Dispose blocks until an in-flight cancellation callback returns, and
        // that callback takes the same lock. Once _finished is set the callback is a no-op.
        _registration.Dispose();

        if (_self.IsAllocated)
        {
            _self.Free();
        }

        if (block != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(block);
        }
    }
}

/// <summary>
/// Bridges one native async call onto a <see cref="Task{TResult}"/>: the caller's
/// <see cref="CancellationToken"/> drives <c>XAsyncCancel</c>, and the resulting <c>E_ABORT</c>
/// completes the task as canceled rather than faulted.
/// </summary>
internal sealed unsafe class AsyncOperation<TResult> : AsyncOperation
{
    private readonly TaskCompletionSource<TResult> _completion =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    private readonly AsyncResultReader<TResult> _reader;

    private AsyncOperation(AsyncResultReader<TResult> reader, CancellationToken cancellationToken)
        : base(cancellationToken)
    {
        _reader = reader;
    }

    internal static Task<TResult> RunAsync(
        IntPtr queueHandle,
        AsyncStarter starter,
        AsyncResultReader<TResult> reader,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var operation = new AsyncOperation<TResult>(reader, cancellationToken);
        operation.Start(queueHandle, starter);
        return operation._completion.Task;
    }

    private protected override void OnCompleted(IntPtr asyncBlock)
    {
        int hr = _reader(asyncBlock, out TResult value);
        if (HResult.Failed(hr))
        {
            if (hr == HResult.EAbort)
            {
                _completion.TrySetCanceled(
                    CancellationToken.IsCancellationRequested ? CancellationToken : default);
            }
            else
            {
                _completion.TrySetException(Hr.ToException(hr, CancellationToken));
            }

            return;
        }

        _completion.TrySetResult(value);
    }

    private protected override void SetException(Exception exception) => _completion.TrySetException(exception);
}
