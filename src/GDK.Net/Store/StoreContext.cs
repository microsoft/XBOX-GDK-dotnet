// StoreContext — the entry point for all XStore operations.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.Users;
using Microsoft.Win32.SafeHandles;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Store;

/// <summary>
/// Owns an <c>XStoreContextHandle</c>; released with <c>XStoreCloseContextHandle</c>.
/// </summary>
internal sealed class StoreContextHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal StoreContextHandle()
        : base(ownsHandle: true) { }

    internal StoreContextHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XStoreCloseContextHandle(handle);
        return true;
    }
}

/// <summary>
/// The entry point for all Store operations (<c>XStoreContextHandle</c>).
/// </summary>
/// <remarks>
/// <para>
/// Create a context with <see cref="Create"/> (system context) or
/// <see cref="CreateForUser"/> (user context). Most operations that display UI or query
/// entitlements require a user context backed by a signed-in <see cref="User"/>.
/// </para>
/// <para>
/// Dispose to release the underlying <c>XStoreContextHandle</c> and unregister any
/// <see cref="GameLicenseChanged"/> subscription.
/// </para>
/// </remarks>
public sealed unsafe class StoreContext : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, StoreContext> LicenseChangedRegistrations = new();

    private readonly StoreContextHandle _handle;
    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private EventHandler? _gameLicenseChanged;
    private XTaskQueueRegistrationToken _licenseToken;
    private GCHandle _self;
    private bool _registered;
    private bool _disposed;

    private StoreContext(StoreContextHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;
    }

    // --- factory ---

    /// <summary>
    /// Creates a system Store context (<c>XStoreCreateContext</c> with null user).
    /// </summary>
    /// <remarks>
    /// Async operations and event callbacks name no task queue, so the Gaming Runtime resolves the
    /// process default.
    /// </remarks>
    public static StoreContext Create() => CreateCore(IntPtr.Zero, queue: null);

    /// <summary>
    /// Creates a Store context bound to a specific signed-in user
    /// (<c>XStoreCreateContext</c>).
    /// </summary>
    /// <param name="user">
    /// The signed-in user. Most purchase, entitlement and UI operations require a user context.
    /// </param>
    public static StoreContext CreateForUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return CreateCore(user.Handle, queue: null);
    }

    // --- GameLicenseChanged event ---

    /// <summary>
    /// Raised when the game's licence state changes (<c>XStoreRegisterGameLicenseChanged</c>).
    /// </summary>
    /// <remarks>
    /// The registration is lazily created on the first subscription and released with
    /// <c>wait: true</c> on <see cref="Dispose"/>. Requires the Gaming Runtime.
    /// </remarks>
    public event EventHandler? GameLicenseChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _gameLicenseChanged += value;
                EnsureGameLicenseRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _gameLicenseChanged -= value;
            }
        }
    }

    /// <summary>Entry point called by the static trampoline.</summary>
    internal static void DispatchGameLicenseChanged(IntPtr context)
    {
        if (!LicenseChangedRegistrations.TryGetValue(context, out StoreContext? ctx))
        {
            return;
        }

        ctx.RaiseGameLicenseChanged();
    }

    // --- licences ---

    /// <summary>
    /// Queries the game licence (<c>XStoreQueryGameLicenseAsync</c> /
    /// <c>XStoreQueryGameLicenseResult</c>).
    /// </summary>
    /// <remarks>Requires a user context and a signed-in user.</remarks>
    public Task<StoreGameLicense> QueryGameLicenseAsync(
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<StoreGameLicense>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreQueryGameLicenseAsync(ctx, (XAsyncBlock*)block),
            static (IntPtr block, out StoreGameLicense value) =>
            {
                value = null!;
                XStoreGameLicense native = default;
                int hr = Native.XStoreQueryGameLicenseResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = GameLicenseFromNative(&native);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries all add-on (DLC) licences (<c>XStoreQueryAddOnLicensesAsync</c> /
    /// <c>XStoreQueryAddOnLicensesResultCount</c> / <c>XStoreQueryAddOnLicensesResult</c>).
    /// </summary>
    /// <remarks>Requires a user context and a signed-in user.</remarks>
    public Task<IReadOnlyList<StoreAddonLicense>> QueryAddOnLicensesAsync(
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<IReadOnlyList<StoreAddonLicense>>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreQueryAddOnLicensesAsync(ctx, (XAsyncBlock*)block),
            static (IntPtr block, out IReadOnlyList<StoreAddonLicense> value) =>
            {
                value = Array.Empty<StoreAddonLicense>();

                uint count;
                int hr = Native.XStoreQueryAddOnLicensesResultCount((XAsyncBlock*)block, &count);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                if (count == 0)
                {
                    return HResult.SOk;
                }

                XStoreAddonLicense* buf = stackalloc XStoreAddonLicense[(int)count];
                hr = Native.XStoreQueryAddOnLicensesResult((XAsyncBlock*)block, count, buf);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                var list = new StoreAddonLicense[count];
                for (int i = 0; i < (int)count; i++)
                {
                    list[i] = AddonLicenseFromNative(&buf[i]);
                }

                value = list;
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries the licence token (<c>XStoreQueryLicenseTokenAsync</c> /
    /// <c>XStoreQueryLicenseTokenResultSize</c> / <c>XStoreQueryLicenseTokenResult</c>).
    /// </summary>
    /// <param name="productIds">The product IDs to include in the token.</param>
    /// <param name="customDeveloperString">An optional developer-defined string embedded in the token.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<string> QueryLicenseTokenAsync(
        IReadOnlyList<string> productIds,
        string? customDeveloperString = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        byte[][] utf8Ids = ToUtf8Array(productIds);
        byte[] customStr = ToUtf8NullTerminated(customDeveloperString);

        var customGch = GCHandle.Alloc(customStr, GCHandleType.Pinned);
        Task<string> task;
        try
        {
            IntPtr customPinned = customGch.AddrOfPinnedObject();
            task = AsyncOperation<string>.RunAsync(
                queue.RawHandle(),
                block => WithStringPtrArray(utf8Ids, (ptrs, count) =>
                    Native.XStoreQueryLicenseTokenAsync(ctx, ptrs, count, (byte*)customPinned, (XAsyncBlock*)block)),
                static (IntPtr block, out string value) =>
                {
                    value = string.Empty;
                    nuint size;
                    int hr = Native.XStoreQueryLicenseTokenResultSize((XAsyncBlock*)block, &size);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    if (size == 0)
                    {
                        return HResult.SOk;
                    }

                    byte[] buf = new byte[(int)size];
                    fixed (byte* bufPtr = buf)
                    {
                        hr = Native.XStoreQueryLicenseTokenResult((XAsyncBlock*)block, size, bufPtr);
                    }

                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    int strLen = (int)size > 0 ? (int)size - 1 : 0;
                    value = strLen > 0 ? Encoding.UTF8.GetString(buf, 0, strLen) : string.Empty;
                    return HResult.SOk;
                },
                cancellationToken);
        }
        finally
        {
            customGch.Free();
        }

        return task;
    }

    /// <summary>
    /// Acquires a package licence (<c>XStoreAcquireLicenseForPackageAsync</c> /
    /// <c>XStoreAcquireLicenseForPackageResult</c>).
    /// </summary>
    public Task<StoreLicense> AcquireLicenseForPackageAsync(
        string packageIdentifier,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(packageIdentifier);

        return AsyncOperation<StoreLicense>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreAcquireLicenseForPackageAsync(ctx, ptr, (XAsyncBlock*)block);
                }
            },
            (IntPtr block, out StoreLicense value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreAcquireLicenseForPackageResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreLicense(new StoreLicenseHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Acquires a licence for a durable add-on (<c>XStoreAcquireLicenseForDurablesAsync</c> /
    /// <c>XStoreAcquireLicenseForDurablesResult</c>).
    /// </summary>
    public Task<StoreLicense> AcquireLicenseForDurablesAsync(
        string storeId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeId);

        return AsyncOperation<StoreLicense>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreAcquireLicenseForDurablesAsync(ctx, ptr, (XAsyncBlock*)block);
                }
            },
            (IntPtr block, out StoreLicense value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreAcquireLicenseForDurablesResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreLicense(new StoreLicenseHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Checks whether a package licence can be acquired
    /// (<c>XStoreCanAcquireLicenseForPackageAsync</c> /
    /// <c>XStoreCanAcquireLicenseForPackageResult</c>).
    /// </summary>
    public Task<StoreCanAcquireLicenseResult> CanAcquireLicenseForPackageAsync(
        string packageIdentifier,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(packageIdentifier);

        return AsyncOperation<StoreCanAcquireLicenseResult>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreCanAcquireLicenseForPackageAsync(ctx, ptr, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out StoreCanAcquireLicenseResult value) =>
            {
                value = null!;
                XStoreCanAcquireLicenseResult native = default;
                int hr = Native.XStoreCanAcquireLicenseForPackageResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = CanAcquireResultFromNative(&native);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Checks whether a Store product licence can be acquired
    /// (<c>XStoreCanAcquireLicenseForStoreIdAsync</c> /
    /// <c>XStoreCanAcquireLicenseForStoreIdResult</c>).
    /// </summary>
    public Task<StoreCanAcquireLicenseResult> CanAcquireLicenseForStoreIdAsync(
        string storeProductId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeProductId);

        return AsyncOperation<StoreCanAcquireLicenseResult>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreCanAcquireLicenseForStoreIdAsync(ctx, ptr, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out StoreCanAcquireLicenseResult value) =>
            {
                value = null!;
                XStoreCanAcquireLicenseResult native = default;
                int hr = Native.XStoreCanAcquireLicenseForStoreIdResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = CanAcquireResultFromNative(&native);
                return HResult.SOk;
            },
            cancellationToken);
    }

    // --- products ---

    /// <summary>
    /// Queries products by Store IDs (<c>XStoreQueryProductsAsync</c> /
    /// <c>XStoreQueryProductsResult</c>).
    /// </summary>
    /// <param name="productKinds">Filter by kind.</param>
    /// <param name="storeIds">Store IDs to query.</param>
    /// <param name="actionFilters">Optional action filter strings.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<StoreProductQuery> QueryProductsAsync(
        StoreProductKind productKinds,
        IReadOnlyList<string> storeIds,
        IReadOnlyList<string>? actionFilters = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        byte[][] utf8Ids = ToUtf8Array(storeIds);
        byte[][] utf8Filters = actionFilters is { Count: > 0 } ? ToUtf8Array(actionFilters) : Array.Empty<byte[]>();

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block => WithStringPtrArray(utf8Ids, (idPtrs, idCount) =>
                WithStringPtrArray(utf8Filters, (fPtrs, fCount) =>
                    Native.XStoreQueryProductsAsync(ctx, (XStoreProductKind)productKinds, idPtrs, idCount, fPtrs, fCount, (XAsyncBlock*)block))),
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreQueryProductsResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries the product record for the current running game
    /// (<c>XStoreQueryProductForCurrentGameAsync</c> / <c>XStoreQueryProductForCurrentGameResult</c>).
    /// </summary>
    public Task<StoreProductQuery> QueryProductForCurrentGameAsync(
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreQueryProductForCurrentGameAsync(ctx, (XAsyncBlock*)block),
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreQueryProductForCurrentGameResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries the product record for a specific package
    /// (<c>XStoreQueryProductForPackageAsync</c> / <c>XStoreQueryProductForPackageResult</c>).
    /// </summary>
    public Task<StoreProductQuery> QueryProductForPackageAsync(
        StoreProductKind productKinds,
        string packageIdentifier,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(packageIdentifier);

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreQueryProductForPackageAsync(
                        ctx, (XStoreProductKind)productKinds, ptr, (XAsyncBlock*)block);
                }
            },
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreQueryProductForPackageResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries products associated with the current game
    /// (<c>XStoreQueryAssociatedProductsAsync</c> / <c>XStoreQueryAssociatedProductsResult</c>).
    /// </summary>
    /// <remarks>
    /// This queries products associated with the currently running game (by title ID). To query the
    /// products associated with a different game, use
    /// <see cref="QueryAssociatedProductsForStoreIdAsync"/>.
    /// </remarks>
    public Task<StoreProductQuery> QueryAssociatedProductsAsync(
        StoreProductKind productKinds,
        uint maxItemsPerPage = 25,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreQueryAssociatedProductsAsync(
                ctx, (XStoreProductKind)productKinds, maxItemsPerPage, (XAsyncBlock*)block),
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreQueryAssociatedProductsResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries the products associated with an explicit Store ID rather than with the currently
    /// running game (<c>XStoreQueryAssociatedProductsForStoreIdAsync</c> /
    /// <c>XStoreQueryAssociatedProductsForStoreIdResult</c>).
    /// </summary>
    /// <param name="storeProductId">The Store ID of the product whose add-ons are wanted.</param>
    /// <param name="productKinds">The product kinds to include.</param>
    /// <param name="maxItemsPerPage">Page size for the returned query.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<StoreProductQuery> QueryAssociatedProductsForStoreIdAsync(
        string storeProductId,
        StoreProductKind productKinds,
        uint maxItemsPerPage = 25,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (storeProductId is null)
        {
            throw new ArgumentNullException(nameof(storeProductId));
        }

        if (storeProductId.Length == 0)
        {
            throw new ArgumentException("The Store product ID must not be empty.", nameof(storeProductId));
        }

        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeProductId);

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* idPtr = idBytes)
                {
                    return Native.XStoreQueryAssociatedProductsForStoreIdAsync(
                        ctx, idPtr, (XStoreProductKind)productKinds, maxItemsPerPage, (XAsyncBlock*)block);
                }
            },
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreQueryAssociatedProductsForStoreIdResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries products the user has entitlements to
    /// (<c>XStoreQueryEntitledProductsAsync</c> / <c>XStoreQueryEntitledProductsResult</c>).
    /// </summary>
    /// <remarks>Requires a user context and a signed-in user.</remarks>
    public Task<StoreProductQuery> QueryEntitledProductsAsync(
        StoreProductKind productKinds,
        uint maxItemsPerPage = 25,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<StoreProductQuery>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreQueryEntitledProductsAsync(
                ctx, (XStoreProductKind)productKinds, maxItemsPerPage, (XAsyncBlock*)block),
            (IntPtr block, out StoreProductQuery value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XStoreQueryEntitledProductsResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreProductQuery(new StoreProductQueryHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    // --- package identifier ---

    /// <summary>
    /// Looks up the package identifier for a Store product ID
    /// (<c>XStoreQueryPackageIdentifier</c>).
    /// </summary>
    /// <param name="storeId">The Store product ID to look up.</param>
    /// <returns>The package identifier string (up to 33 bytes).</returns>
    public static string QueryPackageIdentifier(string storeId)
    {
        byte[] idBytes = ToUtf8NullTerminated(storeId);
        const int MaxLen = 33;
        byte[] result = new byte[MaxLen];

        int hr;
        fixed (byte* storePtr = idBytes)
        fixed (byte* resultPtr = result)
        {
            hr = Native.XStoreQueryPackageIdentifier(storePtr, (nuint)MaxLen, resultPtr);
        }

        Hr.ThrowIfFailed(hr);
        return StoreProductFactory.PtrToString((byte*)System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(result, 0));
    }

    // --- availability ---

    /// <summary>
    /// Checks whether an availability can be purchased
    /// (<c>XStoreIsAvailabilityPurchasable</c>).
    /// </summary>
    public static bool IsAvailabilityPurchasable(StoreAvailability availability)
    {
        if (availability is null)
        {
            throw new ArgumentNullException(nameof(availability));
        }

        byte[] avIdBytes = ToUtf8NullTerminated(availability.AvailabilityId);
        byte[] ccBytes = ToUtf8NullTerminated(availability.Price.CurrencyCode);
        byte[] fbpBytes = ToUtf8Fixed(availability.Price.FormattedBasePrice, 16);
        byte[] fpBytes = ToUtf8Fixed(availability.Price.FormattedPrice, 16);
        byte[] frpBytes = ToUtf8Fixed(availability.Price.FormattedRecurrencePrice, 16);

        fixed (byte* avIdPtr = avIdBytes)
        fixed (byte* ccPtr = ccBytes)
        fixed (byte* fbpPtr = fbpBytes)
        fixed (byte* fpPtr = fpBytes)
        fixed (byte* frpPtr = frpBytes)
        {
            XStoreAvailability native = default;
            native.AvailabilityId = avIdPtr;
            native.Price.BasePrice = availability.Price.BasePrice;
            native.Price.Price = availability.Price.Price;
            native.Price.RecurrencePrice = availability.Price.RecurrencePrice;
            native.Price.CurrencyCode = ccPtr;
            CopyFixed(fbpPtr, native.Price.FormattedBasePrice, 16);
            CopyFixed(fpPtr, native.Price.FormattedPrice, 16);
            CopyFixed(frpPtr, native.Price.FormattedRecurrencePrice, 16);
            native.Price.IsOnSale = availability.Price.IsOnSale ? (byte)1 : (byte)0;
            native.Price.SaleEndDate = availability.Price.SaleEndDate == DateTimeOffset.MinValue
                ? 0L
                : availability.Price.SaleEndDate.ToUnixTimeSeconds();
            native.EndDate = availability.EndDate == DateTimeOffset.MinValue
                ? 0L
                : availability.EndDate.ToUnixTimeSeconds();

            return Native.XStoreIsAvailabilityPurchasable(native) != 0;
        }
    }

    // --- consumables ---

    /// <summary>
    /// Queries the remaining balance of a consumable
    /// (<c>XStoreQueryConsumableBalanceRemainingAsync</c> /
    /// <c>XStoreQueryConsumableBalanceRemainingResult</c>).
    /// </summary>
    /// <remarks>Requires a user context and a signed-in user.</remarks>
    public Task<StoreConsumableResult> QueryConsumableBalanceRemainingAsync(
        string storeProductId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeProductId);

        return AsyncOperation<StoreConsumableResult>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreQueryConsumableBalanceRemainingAsync(ctx, ptr, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out StoreConsumableResult value) =>
            {
                value = default;
                XStoreConsumableResult native = default;
                int hr = Native.XStoreQueryConsumableBalanceRemainingResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreConsumableResult(native.Quantity);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Reports fulfillment of a consumable quantity
    /// (<c>XStoreReportConsumableFulfillmentAsync</c> /
    /// <c>XStoreReportConsumableFulfillmentResult</c>).
    /// </summary>
    /// <param name="storeProductId">The consumable product's Store ID.</param>
    /// <param name="quantity">The quantity to report as fulfilled.</param>
    /// <param name="trackingId">A unique GUID for idempotent fulfillment tracking.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <remarks>Requires a user context and a signed-in user.</remarks>
    public Task<StoreConsumableResult> ReportConsumableFulfillmentAsync(
        string storeProductId,
        uint quantity,
        Guid trackingId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeProductId);

        return AsyncOperation<StoreConsumableResult>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreReportConsumableFulfillmentAsync(
                        ctx, ptr, quantity, trackingId, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out StoreConsumableResult value) =>
            {
                value = default;
                XStoreConsumableResult native = default;
                int hr = Native.XStoreReportConsumableFulfillmentResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreConsumableResult(native.Quantity);
                return HResult.SOk;
            },
            cancellationToken);
    }

    // --- purchase / collections IDs ---

    /// <summary>
    /// Gets a purchase ID token for server-side purchase validation
    /// (<c>XStoreGetUserPurchaseIdAsync</c> / size / result).
    /// </summary>
    /// <remarks>Requires a user context and a signed-in user with the Games privilege.</remarks>
    public Task<string> GetUserPurchaseIdAsync(
        string serviceTicket,
        string publisherUserId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] ticketBytes = ToUtf8NullTerminated(serviceTicket);
        byte[] uidBytes = ToUtf8NullTerminated(publisherUserId);

        return RunSizedStringAsync(
            queue,
            block =>
            {
                fixed (byte* tPtr = ticketBytes)
                fixed (byte* uPtr = uidBytes)
                {
                    return Native.XStoreGetUserPurchaseIdAsync(ctx, tPtr, uPtr, (XAsyncBlock*)block);
                }
            },
            static (XAsyncBlock* block, nuint* size) =>
                Native.XStoreGetUserPurchaseIdResultSize(block, size),
            static (XAsyncBlock* block, nuint size, byte* buf) =>
                Native.XStoreGetUserPurchaseIdResult(block, size, buf),
            cancellationToken);
    }

    /// <summary>
    /// Gets a collections ID token for server-side entitlement verification
    /// (<c>XStoreGetUserCollectionsIdAsync</c> / size / result).
    /// </summary>
    /// <remarks>Requires a user context and a signed-in user with the Games privilege.</remarks>
    public Task<string> GetUserCollectionsIdAsync(
        string serviceTicket,
        string publisherUserId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] ticketBytes = ToUtf8NullTerminated(serviceTicket);
        byte[] uidBytes = ToUtf8NullTerminated(publisherUserId);

        return RunSizedStringAsync(
            queue,
            block =>
            {
                fixed (byte* tPtr = ticketBytes)
                fixed (byte* uPtr = uidBytes)
                {
                    return Native.XStoreGetUserCollectionsIdAsync(ctx, tPtr, uPtr, (XAsyncBlock*)block);
                }
            },
            static (XAsyncBlock* block, nuint* size) =>
                Native.XStoreGetUserCollectionsIdResultSize(block, size),
            static (XAsyncBlock* block, nuint size, byte* buf) =>
                Native.XStoreGetUserCollectionsIdResult(block, size, buf),
            cancellationToken);
    }

    // --- package updates ---

    /// <summary>
    /// Queries pending updates for the current game and its DLC
    /// (<c>XStoreQueryGameAndDlcPackageUpdatesAsync</c> / count / result).
    /// </summary>
    /// <remarks>
    /// This is the current API and covers the running game plus its DLC. To query updates for an
    /// explicit set of packages instead, use <see cref="QueryPackageUpdatesAsync"/>.
    /// </remarks>
    public Task<IReadOnlyList<StorePackageUpdate>> QueryGameAndDlcPackageUpdatesAsync(
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<IReadOnlyList<StorePackageUpdate>>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreQueryGameAndDlcPackageUpdatesAsync(ctx, (XAsyncBlock*)block),
            static (IntPtr block, out IReadOnlyList<StorePackageUpdate> value) =>
            {
                value = Array.Empty<StorePackageUpdate>();
                uint count;
                int hr = Native.XStoreQueryGameAndDlcPackageUpdatesResultCount((XAsyncBlock*)block, &count);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                if (count == 0)
                {
                    return HResult.SOk;
                }

                XStorePackageUpdate* buf = stackalloc XStorePackageUpdate[(int)count];
                hr = Native.XStoreQueryGameAndDlcPackageUpdatesResult((XAsyncBlock*)block, count, buf);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                var list = new StorePackageUpdate[count];
                for (int i = 0; i < (int)count; i++)
                {
                    list[i] = PackageUpdateFromNative(&buf[i]);
                }

                value = list;
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Queries pending updates for an explicit set of packages
    /// (<c>XStoreQueryPackageUpdatesAsync</c> / count / result).
    /// </summary>
    /// <param name="packageIdentifiers">The package identifiers to check for updates.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <remarks>
    /// Prefer <see cref="QueryGameAndDlcPackageUpdatesAsync"/> unless the title needs to scope the
    /// query to specific packages; that method needs no argument.
    /// </remarks>
    public Task<IReadOnlyList<StorePackageUpdate>> QueryPackageUpdatesAsync(
        IReadOnlyList<string> packageIdentifiers,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[][] utf8Ids = ToUtf8Array(packageIdentifiers);

        return AsyncOperation<IReadOnlyList<StorePackageUpdate>>.RunAsync(
            queue.RawHandle(),
            block => WithStringPtrArray(utf8Ids, (ptrs, count) =>
                Native.XStoreQueryPackageUpdatesAsync(ctx, ptrs, count, (XAsyncBlock*)block)),
            static (IntPtr block, out IReadOnlyList<StorePackageUpdate> value) =>
            {
                value = Array.Empty<StorePackageUpdate>();
                uint count;
                int hr = Native.XStoreQueryPackageUpdatesResultCount((XAsyncBlock*)block, &count);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                if (count == 0)
                {
                    return HResult.SOk;
                }

                XStorePackageUpdate* buf = stackalloc XStorePackageUpdate[(int)count];
                hr = Native.XStoreQueryPackageUpdatesResult((XAsyncBlock*)block, count, buf);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                var list = new StorePackageUpdate[count];
                for (int i = 0; i < (int)count; i++)
                {
                    list[i] = PackageUpdateFromNative(&buf[i]);
                }

                value = list;
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Downloads (but does not install) the pending package updates
    /// (<c>XStoreDownloadPackageUpdatesAsync</c> / <c>XStoreDownloadPackageUpdatesResult</c>).
    /// </summary>
    public Task DownloadPackageUpdatesAsync(
        IReadOnlyList<string> packageIdentifiers,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[][] utf8Ids = ToUtf8Array(packageIdentifiers);

        return AsyncOperation<bool>.RunAsync(
            queue.RawHandle(),
            block => WithStringPtrArray(utf8Ids, (ptrs, count) =>
                Native.XStoreDownloadPackageUpdatesAsync(ctx, ptrs, count, (XAsyncBlock*)block)),
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XStoreDownloadPackageUpdatesResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    /// <summary>
    /// Downloads and installs the pending package updates
    /// (<c>XStoreDownloadAndInstallPackageUpdatesAsync</c> /
    /// <c>XStoreDownloadAndInstallPackageUpdatesResult</c>).
    /// </summary>
    public Task DownloadAndInstallPackageUpdatesAsync(
        IReadOnlyList<string> packageIdentifiers,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[][] utf8Ids = ToUtf8Array(packageIdentifiers);

        return AsyncOperation<bool>.RunAsync(
            queue.RawHandle(),
            block => WithStringPtrArray(utf8Ids, (ptrs, count) =>
                Native.XStoreDownloadAndInstallPackageUpdatesAsync(ctx, ptrs, count, (XAsyncBlock*)block)),
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XStoreDownloadAndInstallPackageUpdatesResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    /// <summary>
    /// Downloads and installs packages from the Store
    /// (<c>XStoreDownloadAndInstallPackagesAsync</c> / count / result).
    /// </summary>
    /// <returns>The package identifiers of the installed packages.</returns>
    public Task<IReadOnlyList<string>> DownloadAndInstallPackagesAsync(
        IReadOnlyList<string> storeIds,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[][] utf8Ids = ToUtf8Array(storeIds);

        return AsyncOperation<IReadOnlyList<string>>.RunAsync(
            queue.RawHandle(),
            block => WithStringPtrArray(utf8Ids, (ptrs, count) =>
                Native.XStoreDownloadAndInstallPackagesAsync(ctx, ptrs, count, (XAsyncBlock*)block)),
            static (IntPtr block, out IReadOnlyList<string> value) =>
            {
                value = Array.Empty<string>();
                uint count;
                int hr = Native.XStoreDownloadAndInstallPackagesResultCount((XAsyncBlock*)block, &count);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                if (count == 0)
                {
                    return HResult.SOk;
                }

                const int IdLen = 33;
                byte[] buf = new byte[(int)count * IdLen];
                fixed (byte* bufPtr = buf)
                {
                    hr = Native.XStoreDownloadAndInstallPackagesResult((XAsyncBlock*)block, count, bufPtr);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    var list = new string[count];
                    for (int i = 0; i < (int)count; i++)
                    {
                        list[i] = StoreProductFactory.FixedBufferToString(bufPtr + i * IdLen, IdLen);
                    }

                    value = list;
                }

                return HResult.SOk;
            },
            cancellationToken);
    }

    // --- UI ---

    /// <summary>
    /// Shows the Store gifting UI for a product (<c>XStoreShowGiftingUIAsync</c> /
    /// <c>XStoreShowGiftingUIResult</c>).
    /// </summary>
    /// <param name="storeId">The Store ID of the product to gift.</param>
    /// <param name="name">Optional friendly product name shown in the UI.</param>
    /// <param name="extendedJsonData">Optional extended JSON payload passed to the Store.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <remarks>Requires the title window to be in the foreground.</remarks>
    public Task ShowGiftingUIAsync(
        string storeId,
        string? name = null,
        string? extendedJsonData = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeId);
        byte[] nameBytes = ToUtf8NullTerminated(name);
        byte[] extBytes = ToUtf8NullTerminated(extendedJsonData);

        return AsyncOperation<bool>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* idPtr = idBytes)
                fixed (byte* nPtr = nameBytes)
                fixed (byte* ePtr = extBytes)
                {
                    return Native.XStoreShowGiftingUIAsync(ctx, idPtr, nPtr, ePtr, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XStoreShowGiftingUIResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    /// <summary>
    /// Shows the Store purchase UI for a product (<c>XStoreShowPurchaseUIAsync</c> /
    /// <c>XStoreShowPurchaseUIResult</c>).
    /// </summary>
    /// <remarks>Requires the title window to be in the foreground.</remarks>
    public Task ShowPurchaseUIAsync(
        string storeId,
        string? name = null,
        string? extendedJsonData = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeId);
        byte[] nameBytes = ToUtf8NullTerminated(name);
        byte[] extBytes = ToUtf8NullTerminated(extendedJsonData);

        return AsyncOperation<bool>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* idPtr = idBytes)
                fixed (byte* nPtr = nameBytes)
                fixed (byte* ePtr = extBytes)
                {
                    return Native.XStoreShowPurchaseUIAsync(ctx, idPtr, nPtr, ePtr, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XStoreShowPurchaseUIResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    /// <summary>
    /// Shows the Store product page UI (<c>XStoreShowProductPageUIAsync</c> /
    /// <c>XStoreShowProductPageUIResult</c>).
    /// </summary>
    /// <remarks>Requires the title window to be in the foreground.</remarks>
    public Task ShowProductPageUIAsync(
        string storeId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeId);

        return AsyncOperation<bool>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreShowProductPageUIAsync(ctx, ptr, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XStoreShowProductPageUIResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    /// <summary>
    /// Shows the associated products UI (<c>XStoreShowAssociatedProductsUIAsync</c> /
    /// <c>XStoreShowAssociatedProductsUIResult</c>).
    /// </summary>
    /// <remarks>Requires the title window to be in the foreground.</remarks>
    public Task ShowAssociatedProductsUIAsync(
        string storeId,
        StoreProductKind productKinds,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] idBytes = ToUtf8NullTerminated(storeId);

        return AsyncOperation<bool>.RunAsync(
            queue.RawHandle(),
            block =>
            {
                fixed (byte* ptr = idBytes)
                {
                    return Native.XStoreShowAssociatedProductsUIAsync(
                        ctx, ptr, (XStoreProductKind)productKinds, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out bool value) =>
            {
                value = true;
                return Native.XStoreShowAssociatedProductsUIResult((XAsyncBlock*)block);
            },
            cancellationToken);
    }

    /// <summary>
    /// Shows the redeem token UI (<c>XStoreShowRedeemTokenUIAsync</c> /
    /// <c>XStoreShowRedeemTokenUIResult</c>).
    /// </summary>
    /// <param name="token">The redemption token string.</param>
    /// <param name="allowedStoreIds">Store IDs to accept; <see langword="null"/> accepts any.</param>
    /// <param name="disallowCsvRedemption">When <see langword="true"/>, CSV codes are not accepted.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <remarks>Requires the title window to be in the foreground.</remarks>
    public Task ShowRedeemTokenUIAsync(
        string token,
        IReadOnlyList<string>? allowedStoreIds = null,
        bool disallowCsvRedemption = false,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;
        byte[] tokenBytes = ToUtf8NullTerminated(token);
        byte[][] utf8Ids = allowedStoreIds is { Count: > 0 } ? ToUtf8Array(allowedStoreIds) : Array.Empty<byte[]>();

        var tokenGch = GCHandle.Alloc(tokenBytes, GCHandleType.Pinned);
        Task<bool> tokenTask;
        try
        {
            IntPtr tokenPinned = tokenGch.AddrOfPinnedObject();
            tokenTask = AsyncOperation<bool>.RunAsync(
                queue.RawHandle(),
                block => WithStringPtrArray(utf8Ids, (ptrs, count) =>
                    Native.XStoreShowRedeemTokenUIAsync(
                        ctx, (byte*)tokenPinned, ptrs, count,
                        disallowCsvRedemption ? (byte)1 : (byte)0,
                        (XAsyncBlock*)block)),
                static (IntPtr block, out bool value) =>
                {
                    value = true;
                    return Native.XStoreShowRedeemTokenUIResult((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        finally
        {
            tokenGch.Free();
        }

        return tokenTask;
    }

    /// <summary>
    /// Shows the rate-and-review UI (<c>XStoreShowRateAndReviewUIAsync</c> /
    /// <c>XStoreShowRateAndReviewUIResult</c>).
    /// </summary>
    /// <remarks>Requires the title window to be in the foreground and a signed-in user.</remarks>
    public Task<StoreRateAndReviewResult> ShowRateAndReviewUIAsync(
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        IntPtr ctx = Handle;
        GameTaskQueue? queue = _queue;

        return AsyncOperation<StoreRateAndReviewResult>.RunAsync(
            queue.RawHandle(),
            block => Native.XStoreShowRateAndReviewUIAsync(ctx, (XAsyncBlock*)block),
            static (IntPtr block, out StoreRateAndReviewResult value) =>
            {
                value = default;
                XStoreRateAndReviewResult native = default;
                int hr = Native.XStoreShowRateAndReviewUIResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new StoreRateAndReviewResult(native.WasUpdated != 0);
                return HResult.SOk;
            },
            cancellationToken);
    }

    // --- IDisposable ---

    /// <inheritdoc/>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _gameLicenseChanged = null;

            if (_registered)
            {
                _registered = false;
                Native.XStoreUnregisterGameLicenseChanged(
                    _handle.DangerousGetHandle(), _licenseToken, wait: 1);
                _licenseToken = default;
            }

            if (_self.IsAllocated)
            {
                LicenseChangedRegistrations.TryRemove(GCHandle.ToIntPtr(_self), out _);
                _self.Free();
            }
        }

        _handle.Dispose();
    }

    // --- internal helpers ---

    private IntPtr Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle.DangerousGetHandle();
        }
    }

    private static StoreContext CreateCore(IntPtr userHandle, GameTaskQueue? queue)
    {
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XStoreCreateContext(userHandle, &raw));
        return new StoreContext(new StoreContextHandle(raw), queue);
    }

    private void EnsureGameLicenseRegistered()
    {
        if (_registered)
        {
            return;
        }

        _self = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_self);
        LicenseChangedRegistrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XStoreRegisterGameLicenseChanged(
            _handle.DangerousGetHandle(),
            _queue.RawHandle(),
            context,
            StoreContextCallbacks.GameLicenseChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            LicenseChangedRegistrations.TryRemove(context, out _);
            _self.Free();
            Hr.ThrowIfFailed(hr);
        }

        _licenseToken = token;
        _registered = true;
    }

    private void RaiseGameLicenseChanged()
    {
        EventHandler? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _gameLicenseChanged;
        }

        handler?.Invoke(this, EventArgs.Empty);
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(StoreContext));
        }
    }

    // --- native-to-managed converters (static, no capture) ---

    private static StoreGameLicense GameLicenseFromNative(XStoreGameLicense* src)
    {
        return new StoreGameLicense
        {
            SkuStoreId = StoreProductFactory.FixedBufferToString(src->SkuStoreId, 18),
            IsActive = src->IsActive != 0,
            IsTrialOwnedByThisUser = src->IsTrialOwnedByThisUser != 0,
            IsDiscLicense = src->IsDiscLicense != 0,
            IsTrial = src->IsTrial != 0,
            TrialTimeRemainingInSeconds = src->TrialTimeRemainingInSeconds,
            TrialUniqueId = StoreProductFactory.FixedBufferToString(src->TrialUniqueId, 64),
            ExpirationDate = StoreProductFactory.DateFromUnixSeconds(src->ExpirationDate),
        };
    }

    private static StoreAddonLicense AddonLicenseFromNative(XStoreAddonLicense* src)
    {
        return new StoreAddonLicense
        {
            SkuStoreId = StoreProductFactory.FixedBufferToString(src->SkuStoreId, 18),
            InAppOfferToken = StoreProductFactory.FixedBufferToString(src->InAppOfferToken, 64),
            IsActive = src->IsActive != 0,
            ExpirationDate = StoreProductFactory.DateFromUnixSeconds(src->ExpirationDate),
        };
    }

    private static StorePackageUpdate PackageUpdateFromNative(XStorePackageUpdate* src)
    {
        return new StorePackageUpdate
        {
            PackageIdentifier = StoreProductFactory.FixedBufferToString(src->PackageIdentifier, 33),
            IsMandatory = src->IsMandatory != 0,
        };
    }

    private static StoreCanAcquireLicenseResult CanAcquireResultFromNative(XStoreCanAcquireLicenseResult* src)
    {
        return new StoreCanAcquireLicenseResult
        {
            LicensableSku = StoreProductFactory.FixedBufferToString(src->LicensableSku, 5),
            Status = (StoreCanLicenseStatus)src->Status,
        };
    }

    // --- string helpers ---

    internal static byte[] ToUtf8NullTerminated(string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return new byte[1]; // just the null terminator
        }

        byte[] encoded = Encoding.UTF8.GetBytes(s);
        byte[] result = new byte[encoded.Length + 1];
        encoded.CopyTo(result, 0);
        return result;
    }

    private static byte[] ToUtf8Fixed(string s, int maxLen)
    {
        byte[] result = new byte[maxLen];
        if (!string.IsNullOrEmpty(s))
        {
            byte[] encoded = Encoding.UTF8.GetBytes(s);
            int copyLen = Math.Min(encoded.Length, maxLen - 1);
            Array.Copy(encoded, result, copyLen);
        }

        return result;
    }

    private static byte[][] ToUtf8Array(IReadOnlyList<string> strings)
    {
        if (strings is null || strings.Count == 0)
        {
            return Array.Empty<byte[]>();
        }

        var result = new byte[strings.Count][];
        for (int i = 0; i < strings.Count; i++)
        {
            result[i] = ToUtf8NullTerminated(strings[i]);
        }

        return result;
    }

    private unsafe delegate int StringPtrArrayAction(byte** ptrs, nuint count);

    private static unsafe int WithStringPtrArray(byte[][] strings, StringPtrArrayAction action)
    {
        if (strings.Length == 0)
        {
            return action(null, 0);
        }

        IntPtr[] ptrs = new IntPtr[strings.Length];
        GCHandle[]? handles = null;
        try
        {
            handles = new GCHandle[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                handles[i] = GCHandle.Alloc(strings[i], GCHandleType.Pinned);
                ptrs[i] = handles[i].AddrOfPinnedObject();
            }

            fixed (IntPtr* pPtrs = ptrs)
            {
                return action((byte**)pPtrs, (nuint)strings.Length);
            }
        }
        finally
        {
            if (handles != null)
            {
                foreach (var h in handles)
                {
                    if (h.IsAllocated)
                    {
                        h.Free();
                    }
                }
            }
        }
    }

    // Delegate types for the sized-string async pattern.
    private unsafe delegate int SizeGetter(XAsyncBlock* block, nuint* size);
    private unsafe delegate int ResultReader(XAsyncBlock* block, nuint size, byte* buf);

    private static unsafe Task<string> RunSizedStringAsync(
        GameTaskQueue? queue,
        AsyncStarter starter,
        SizeGetter sizeGetter,
        ResultReader resultReader,
        CancellationToken cancellationToken)
    {
        // Capture delegates as state to avoid lambda capture of unsafe pointers.
        var state = (sizeGetter, resultReader);

        return AsyncOperation<string>.RunAsync(
            queue.RawHandle(),
            starter,
            (IntPtr block, out string value) =>
            {
                value = string.Empty;
                nuint size;
                int hr = state.sizeGetter((XAsyncBlock*)block, &size);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                if (size == 0)
                {
                    return HResult.SOk;
                }

                byte[] buf = new byte[(int)size];
                fixed (byte* bufPtr = buf)
                {
                    hr = state.resultReader((XAsyncBlock*)block, size, bufPtr);
                }

                if (HResult.Failed(hr))
                {
                    return hr;
                }

                // buf is null-terminated; find length.
                int len = 0;
                while (len < buf.Length && buf[len] != 0)
                {
                    len++;
                }

                value = len == 0 ? string.Empty : Encoding.UTF8.GetString(buf, 0, len);
                return HResult.SOk;
            },
            cancellationToken);
    }

    private static unsafe void CopyFixed(byte* src, byte* dest, int maxLen)
    {
        for (int i = 0; i < maxLen; i++)
        {
            dest[i] = src[i];
        }
    }
}

/// <summary>
/// Static trampolines for <c>XStoreGameLicenseChangedCallback</c>.
/// </summary>
internal static unsafe class StoreContextCallbacks
{
#if NET5_0_OR_GREATER

    internal static IntPtr GameLicenseChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, void>)&OnGameLicenseChanged;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static void OnGameLicenseChanged(IntPtr context)
        {
            try
            {
                StoreContext.DispatchGameLicenseChanged(context);
            }
            catch
            {
                // Never let a managed exception cross back into the Gaming Runtime.
            }
        }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void GameLicenseChangedDelegate(IntPtr context);

    private static readonly GameLicenseChangedDelegate KeepAlive = OnGameLicenseChanged;

    internal static IntPtr GameLicenseChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(KeepAlive);

    private static void OnGameLicenseChanged(IntPtr context)
    {
        try
        {
            StoreContext.DispatchGameLicenseChanged(context);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
