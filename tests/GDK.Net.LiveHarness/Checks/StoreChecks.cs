using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDK.Net.Store;

namespace GDK.Net.LiveHarness;

/// <summary>
/// The commerce surface: licences, product catalogue queries and package updates.
/// </summary>
/// <remarks>
/// <para>
/// Everything here is a read. That is not squeamishness about side effects — the store's writes are
/// purchases and consumable fulfilments against real entitlements, and the ones that are not
/// purchases (<c>ShowPurchaseUIAsync</c> and friends) block on system UI that no one is present to
/// dismiss, so calling them would hang the run rather than test anything.
/// </para>
/// <para>
/// The results are still worth having: a licence query that returns the wrong shape, or a product
/// query whose paging is broken, is a real defect and shows up here.
/// </para>
/// </remarks>
internal static class StoreChecks
{
    private const string ContextKey = "store.context";

    public static IEnumerable<LiveCheck> All()
    {
        yield return LiveCheck.Sync("store.context", ctx =>
        {
            StoreContext store = StoreContext.CreateForUser(ctx.RequireUser);
            ctx.State[ContextKey] = store;
            return "XStoreCreateContext for the signed-in user";
        }, "users.add");

        yield return LiveCheck.Async("store.game-license", async ctx =>
        {
            StoreGameLicense license = await Store(ctx).QueryGameLicenseAsync().ConfigureAwait(false);
            return $"XStoreQueryGameLicenseAsync: skuStoreId='{license.SkuStoreId}' active={license.IsActive} " +
                   $"trial={license.IsTrial} discLicense={license.IsDiscLicense} " +
                   $"trialTimeRemaining={license.TrialTimeRemainingInSeconds}s";
        }, "store.context");

        yield return LiveCheck.Async("store.addon-licenses", async ctx =>
        {
            IReadOnlyList<StoreAddonLicense> licenses =
                await Store(ctx).QueryAddOnLicensesAsync().ConfigureAwait(false);
            return licenses.Count == 0
                ? "XStoreQueryAddOnLicensesAsync returned no add-on licences (expected for a title with no DLC)"
                : $"XStoreQueryAddOnLicensesAsync returned {licenses.Count}: " +
                  string.Join(", ", licenses.Select(l => l.SkuStoreId));
        }, "store.context");

        yield return LiveCheck.Async("store.product-for-current-game", async ctx =>
        {
            StoreProductQuery query;
            try
            {
                query = await Store(ctx).QueryProductForCurrentGameAsync().ConfigureAwait(false);
            }
            catch (GameRuntimeException ex) when (ex.HResultCode == ServiceGate.HttpNotFound)
            {
                throw ServiceGate.NotConfigured(ex,
                    "The running title has no Store catalogue entry, so there is no product to " +
                    "return. This resolves once the product is published");
            }

            using (query)
            {
                IReadOnlyList<StoreProduct> products = query.EnumerateProducts();
                if (products.Count == 0)
                {
                    throw new InvalidOperationException(
                        "XStoreQueryProductForCurrentGameAsync succeeded but returned no product for the " +
                        "running title.");
                }

                StoreProduct product = products[0];
                ctx.State["store.productId"] = product.StoreId;
                return $"XStoreQueryProductForCurrentGameAsync: '{product.Title}' storeId={product.StoreId} " +
                       $"kind={product.ProductKind} skus={product.Skus.Count} images={product.Images.Count}";
            }
        }, "store.context");

        yield return LiveCheck.Async("store.products-by-id", async ctx =>
        {
            var storeId = ctx.Get<string>("store.productId");
            using StoreProductQuery query = await Store(ctx)
                .QueryProductsAsync(StoreProductKind.Game, [storeId])
                .ConfigureAwait(false);

            IReadOnlyList<StoreProduct> products = query.EnumerateProducts();
            return products.Count == 1 && products[0].StoreId == storeId
                ? $"XStoreQueryProductsAsync round-tripped storeId {storeId}"
                : throw new InvalidOperationException(
                    $"XStoreQueryProductsAsync asked for '{storeId}' and got " +
                    $"{products.Count} product(s): {string.Join(", ", products.Select(p => p.StoreId))}");
        }, "store.product-for-current-game");

        yield return LiveCheck.Sync("store.package-identifier", ctx =>
        {
            string identifier = StoreContext.QueryPackageIdentifier(ctx.Get<string>("store.productId"));
            return $"XStoreQueryPackageIdentifier = '{identifier}'";
        }, "store.product-for-current-game");

        yield return LiveCheck.Async("store.associated-products", async ctx =>
        {
            using StoreProductQuery query = await Store(ctx)
                .QueryAssociatedProductsAsync(StoreProductKind.Durable | StoreProductKind.Consumable)
                .ConfigureAwait(false);

            return await SummarizePagesAsync("XStoreQueryAssociatedProductsAsync", query).ConfigureAwait(false);
        }, "store.context");

        yield return LiveCheck.Async("store.entitled-products", async ctx =>
        {
            using StoreProductQuery query = await Store(ctx)
                .QueryEntitledProductsAsync(StoreProductKind.Durable | StoreProductKind.Consumable)
                .ConfigureAwait(false);

            return await SummarizePagesAsync("XStoreQueryEntitledProductsAsync", query).ConfigureAwait(false);
        }, "store.context");

        yield return LiveCheck.Async("store.package-updates", async ctx =>
        {
            IReadOnlyList<StorePackageUpdate> updates =
                await Store(ctx).QueryGameAndDlcPackageUpdatesAsync().ConfigureAwait(false);
            return updates.Count == 0
                ? "XStoreQueryGameAndDlcPackageUpdatesAsync reports the title and its DLC are up to date"
                : $"XStoreQueryGameAndDlcPackageUpdatesAsync found {updates.Count} update(s): " +
                  string.Join(", ", updates.Select(u => $"{u.PackageIdentifier} mandatory={u.IsMandatory}"));
        }, "store.context");

        yield return LiveCheck.Sync("store.license-changed-event", ctx =>
        {
            // Registration round-trip only. Firing it needs the licence to actually change, which
            // is driven by the store service rather than by anything the harness can do.
            var observed = 0;
            void Handler(object? sender, EventArgs e) => observed++;

            StoreContext store = Store(ctx);
            store.GameLicenseChanged += Handler;
            store.GameLicenseChanged -= Handler;

            return "XStoreRegisterGameLicenseChanged and XStoreUnregisterGameLicenseChanged round-tripped";
        }, "store.context");
    }

    /// <summary>
    /// Disposes the store context. Separate from the checks so a failure to close the handle is
    /// reported rather than hidden in a <c>finally</c>.
    /// </summary>
    public static IEnumerable<LiveCheck> Teardown()
    {
        yield return LiveCheck.Sync("store.context.dispose", ctx =>
        {
            Store(ctx).Dispose();
            ctx.State.Remove(ContextKey);
            return "XStoreCloseContextHandle";
        }, "store.context");
    }

    private static StoreContext Store(CheckContext ctx) => ctx.Get<StoreContext>(ContextKey);

    /// <summary>
    /// Walks every page, which is the only way to prove <c>XStoreProductsQueryHasMorePages</c> and
    /// <c>XStoreProductsQueryNextPageAsync</c> agree with each other.
    /// </summary>
    private static async Task<string> SummarizePagesAsync(string api, StoreProductQuery first)
    {
        var titles = new List<string>();
        var pages = 1;

        StoreProductQuery page = first;
        try
        {
            while (true)
            {
                titles.AddRange(page.EnumerateProducts().Select(p => p.StoreId));

                if (!page.HasMorePages)
                {
                    break;
                }

                StoreProductQuery next = await page.NextPageAsync().ConfigureAwait(false);
                if (!ReferenceEquals(page, first))
                {
                    page.Dispose();
                }

                page = next;
                pages++;
            }
        }
        finally
        {
            if (!ReferenceEquals(page, first))
            {
                page.Dispose();
            }
        }

        return titles.Count == 0
            ? $"{api} returned no products (expected for a title with no add-ons configured)"
            : $"{api} returned {titles.Count} product(s) across {pages} page(s): {string.Join(", ", titles)}";
    }
}
