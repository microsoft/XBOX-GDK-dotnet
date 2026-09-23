// StoreProductQuery — SafeHandle-backed paged product query.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Store;

/// <summary>
/// Owns an <c>XStoreProductQueryHandle</c>; released with <c>XStoreCloseProductsQueryHandle</c>.
/// </summary>
internal sealed class StoreProductQueryHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal StoreProductQueryHandle()
        : base(ownsHandle: true) { }

    internal StoreProductQueryHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XStoreCloseProductsQueryHandle(handle);
        return true;
    }
}

/// <summary>
/// A paged product query, produced by the <c>XStoreQuery*Products*</c> family.
/// </summary>
/// <remarks>
/// <para>
/// Call <see cref="EnumerateProducts"/> to read the products on the current page.
/// Check <see cref="HasMorePages"/> and call <see cref="NextPageAsync"/> to page forward.
/// </para>
/// <para>Dispose to close the underlying <c>XStoreProductQueryHandle</c>.</para>
/// </remarks>
public sealed unsafe class StoreProductQuery : IDisposable
{
    private readonly StoreProductQueryHandle _handle;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    internal StoreProductQuery(StoreProductQueryHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;
    }

    /// <summary>
    /// <see langword="true"/> when more pages of results are available
    /// (<c>XStoreProductsQueryHasMorePages</c>).
    /// </summary>
    public bool HasMorePages
    {
        get
        {
            ThrowIfDisposed();
            return Native.XStoreProductsQueryHasMorePages(_handle.DangerousGetHandle()) != 0;
        }
    }

    /// <summary>
    /// Enumerates the products on the current page by invoking the native
    /// <c>XStoreEnumerateProductsQuery</c> callback synchronously.
    /// </summary>
    /// <remarks>
    /// The returned list is a fully managed deep copy; all strings and arrays are copied out of
    /// the native callback before this method returns.
    /// </remarks>
    /// <returns>A snapshot of all products on the current page.</returns>
    public IReadOnlyList<StoreProduct> EnumerateProducts()
    {
        ThrowIfDisposed();

        var list = new List<StoreProduct>();
        var handle = GCHandle.Alloc(list);
        try
        {
            IntPtr contextPtr = GCHandle.ToIntPtr(handle);
            int hr = Native.XStoreEnumerateProductsQuery(
                _handle.DangerousGetHandle(),
                contextPtr,
                StoreProductQueryCallbacks.ProductQueryCallback);
            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            handle.Free();
        }

        return list;
    }

    /// <summary>
    /// Advances the query to the next page (<c>XStoreProductsQueryNextPageAsync</c> /
    /// <c>XStoreProductsQueryNextPageResult</c>).
    /// </summary>
    /// <remarks>
    /// Call <see cref="EnumerateProducts"/> on the returned <see cref="StoreProductQuery"/> to
    /// read the new page. This instance should be disposed after a successful
    /// <see cref="NextPageAsync"/> call.
    /// </remarks>
    public Task<StoreProductQuery> NextPageAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr queryHandle = _handle.DangerousGetHandle();
        GameTaskQueue? queue = _queue;

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreProductsQueryNextPageAsync(queryHandle, (XAsyncBlock*)block),
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreProductsQueryNextPageResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(StoreProductQuery));
        }
    }
}

/// <summary>
/// Static trampolines for <c>XStoreProductQueryCallback</c>.
/// </summary>
/// <remarks>
/// The callback is invoked synchronously by <c>XStoreEnumerateProductsQuery</c>. Each call
/// receives the context <see cref="GCHandle"/> that points to the <c>List&lt;StoreProduct&gt;</c>
/// accumulated by <see cref="StoreProductQuery.EnumerateProducts"/>.
/// Exceptions must not escape into native code.
/// </remarks>
internal static unsafe class StoreProductQueryCallbacks
{
#if NET5_0_OR_GREATER

    internal static IntPtr ProductQueryCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<XStoreProduct*, IntPtr, byte>)&OnProductQuery;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnProductQuery(XStoreProduct* product, IntPtr context)
    {
        try
        {
            var list = (List<StoreProduct>)GCHandle.FromIntPtr(context).Target!;
            list.Add(StoreProductFactory.FromNative(product));
            return 1; // continue enumeration
        }
        catch
        {
            return 0; // stop on error — never unwind into native code
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte ProductQueryCallbackDelegate(IntPtr product, IntPtr context);

    private static readonly ProductQueryCallbackDelegate KeepAlive = OnProductQuery;

    internal static IntPtr ProductQueryCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(KeepAlive);

    private static byte OnProductQuery(IntPtr productPtr, IntPtr context)
    {
        try
        {
            var product = (XStoreProduct*)productPtr;
            var list = (List<StoreProduct>)GCHandle.FromIntPtr(context).Target!;
            list.Add(StoreProductFactory.FromNative(product));
            return 1; // continue enumeration
        }
        catch
        {
            return 0; // stop on error — never unwind into native code
        }
    }

#endif
}
