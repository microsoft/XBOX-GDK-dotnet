// Idiomatic managed types for the XStore family.
//
// All types are deep copies of native data; no native pointers escape into managed code.

using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.Store;

/// <summary>The kind of a Store product. Mirrors <c>XStoreProductKind</c>.</summary>
[Flags]
public enum StoreProductKind : uint
{
    /// <summary>No product kind.</summary>
    None = 0x00,

    /// <summary>A consumable product (balance decrements on purchase).</summary>
    Consumable = 0x01,

    /// <summary>A durable product (permanent entitlement).</summary>
    Durable = 0x02,

    /// <summary>The base game.</summary>
    Game = 0x04,

    /// <summary>A pass (subscription or season pass).</summary>
    Pass = 0x08,

    /// <summary>An unmanaged consumable (the title tracks balance itself).</summary>
    UnmanagedConsumable = 0x10,
}

/// <summary>
/// Whether a licence can be acquired for a given product. Mirrors <c>XStoreCanLicenseStatus</c>.
/// </summary>
public enum StoreCanLicenseStatus : uint
{
    /// <summary>The user cannot be licensed for this product.</summary>
    NotLicensableToUser = 0,

    /// <summary>The user can be licensed for this product.</summary>
    Licensable = 1,

    /// <summary>The licence action is not applicable to this product type.</summary>
    LicenseActionNotApplicableToProduct = 2,
}

/// <summary>Unit of time for subscription billing and trial periods. Mirrors <c>XStoreDurationUnit</c>.</summary>
public enum StoreDurationUnit : uint
{
    /// <summary>Duration is measured in minutes.</summary>
    Minute = 0,

    /// <summary>Duration is measured in hours.</summary>
    Hour = 1,

    /// <summary>Duration is measured in days.</summary>
    Day = 2,

    /// <summary>Duration is measured in weeks.</summary>
    Week = 3,

    /// <summary>Duration is measured in months.</summary>
    Month = 4,

    /// <summary>Duration is measured in years.</summary>
    Year = 5,
}

/// <summary>
/// Price information for a product or SKU. Mirrors <c>XStorePrice</c>.
/// </summary>
public sealed class StorePrice
{
    internal StorePrice() { }

    /// <summary>The base (undiscounted) price.</summary>
    public float BasePrice { get; internal set; }

    /// <summary>The current price (may be a sale price).</summary>
    public float Price { get; internal set; }

    /// <summary>The recurring price for subscriptions.</summary>
    public float RecurrencePrice { get; internal set; }

    /// <summary>ISO 4217 currency code (e.g., "USD").</summary>
    public string CurrencyCode { get; internal set; } = string.Empty;

    /// <summary>Formatted base price string.</summary>
    public string FormattedBasePrice { get; internal set; } = string.Empty;

    /// <summary>Formatted current price string.</summary>
    public string FormattedPrice { get; internal set; } = string.Empty;

    /// <summary>Formatted recurring price string for subscriptions.</summary>
    public string FormattedRecurrencePrice { get; internal set; } = string.Empty;

    /// <summary><see langword="true"/> when the product is currently on sale.</summary>
    public bool IsOnSale { get; internal set; }

    /// <summary>When the current sale ends; <see cref="DateTimeOffset.MinValue"/> if not on sale.</summary>
    public DateTimeOffset SaleEndDate { get; internal set; }
}

/// <summary>
/// An image associated with a product or SKU. Mirrors <c>XStoreImage</c>.
/// </summary>
public sealed class StoreImage
{
    internal StoreImage() { }

    /// <summary>URI of the image.</summary>
    public string Uri { get; internal set; } = string.Empty;

    /// <summary>Height in pixels.</summary>
    public uint Height { get; internal set; }

    /// <summary>Width in pixels.</summary>
    public uint Width { get; internal set; }

    /// <summary>Descriptive caption.</summary>
    public string Caption { get; internal set; } = string.Empty;

    /// <summary>Tag describing the image's purpose (e.g., "BoxArt", "Screenshot").</summary>
    public string ImagePurposeTag { get; internal set; } = string.Empty;
}

/// <summary>
/// A video associated with a product or SKU. Mirrors <c>XStoreVideo</c>.
/// </summary>
public sealed class StoreVideo
{
    internal StoreVideo() { }

    /// <summary>URI of the video.</summary>
    public string Uri { get; internal set; } = string.Empty;

    /// <summary>Height in pixels.</summary>
    public uint Height { get; internal set; }

    /// <summary>Width in pixels.</summary>
    public uint Width { get; internal set; }

    /// <summary>Descriptive caption.</summary>
    public string Caption { get; internal set; } = string.Empty;

    /// <summary>Tag describing the video's purpose.</summary>
    public string VideoPurposeTag { get; internal set; } = string.Empty;

    /// <summary>Preview image for the video.</summary>
    public StoreImage PreviewImage { get; internal set; } = new StoreImage();
}

/// <summary>
/// Collection ownership data for a SKU. Mirrors <c>XStoreCollectionData</c>.
/// </summary>
public sealed class StoreCollectionData
{
    internal StoreCollectionData() { }

    /// <summary>Date the user acquired this SKU.</summary>
    public DateTimeOffset AcquiredDate { get; internal set; }

    /// <summary>Start of the entitlement window (for time-limited items).</summary>
    public DateTimeOffset StartDate { get; internal set; }

    /// <summary>End of the entitlement window; <see cref="DateTimeOffset.MinValue"/> for permanent items.</summary>
    public DateTimeOffset EndDate { get; internal set; }

    /// <summary><see langword="true"/> when this is a trial entitlement.</summary>
    public bool IsTrial { get; internal set; }

    /// <summary>Seconds remaining in the trial.</summary>
    public uint TrialTimeRemainingInSeconds { get; internal set; }

    /// <summary>Quantity owned (for consumables).</summary>
    public uint Quantity { get; internal set; }

    /// <summary>Campaign ID used when acquiring this item (may be empty).</summary>
    public string CampaignId { get; internal set; } = string.Empty;

    /// <summary>Developer offer ID used when acquiring this item (may be empty).</summary>
    public string DeveloperOfferId { get; internal set; } = string.Empty;
}

/// <summary>
/// Subscription billing details for a SKU. Mirrors <c>XStoreSubscriptionInfo</c>.
/// </summary>
public sealed class StoreSubscriptionInfo
{
    internal StoreSubscriptionInfo() { }

    /// <summary><see langword="true"/> when the subscription has a trial period.</summary>
    public bool HasTrialPeriod { get; internal set; }

    /// <summary>Unit for <see cref="TrialPeriod"/>.</summary>
    public StoreDurationUnit TrialPeriodUnit { get; internal set; }

    /// <summary>Length of the trial period in <see cref="TrialPeriodUnit"/> units.</summary>
    public uint TrialPeriod { get; internal set; }

    /// <summary>Unit for <see cref="BillingPeriod"/>.</summary>
    public StoreDurationUnit BillingPeriodUnit { get; internal set; }

    /// <summary>Billing frequency in <see cref="BillingPeriodUnit"/> units.</summary>
    public uint BillingPeriod { get; internal set; }
}

/// <summary>
/// A Stock Keeping Unit (SKU) within a product. Mirrors <c>XStoreSku</c>.
/// </summary>
public sealed class StoreSku
{
    internal StoreSku() { }

    /// <summary>The Store SKU identifier.</summary>
    public string SkuId { get; internal set; } = string.Empty;

    /// <summary>Localized title of the SKU.</summary>
    public string Title { get; internal set; } = string.Empty;

    /// <summary>Localized description of the SKU.</summary>
    public string Description { get; internal set; } = string.Empty;

    /// <summary>BCP-47 language tag for the localized strings.</summary>
    public string Language { get; internal set; } = string.Empty;

    /// <summary>Pricing information.</summary>
    public StorePrice Price { get; internal set; } = new StorePrice();

    /// <summary><see langword="true"/> when this SKU is a trial.</summary>
    public bool IsTrial { get; internal set; }

    /// <summary><see langword="true"/> when the user owns this SKU.</summary>
    public bool IsInUserCollection { get; internal set; }

    /// <summary>Collection (entitlement) data when <see cref="IsInUserCollection"/> is true.</summary>
    public StoreCollectionData CollectionData { get; internal set; } = new StoreCollectionData();

    /// <summary><see langword="true"/> when this SKU is a subscription.</summary>
    public bool IsSubscription { get; internal set; }

    /// <summary>Subscription billing details when <see cref="IsSubscription"/> is true.</summary>
    public StoreSubscriptionInfo SubscriptionInfo { get; internal set; } = new StoreSubscriptionInfo();

    /// <summary>Store IDs of the SKUs bundled into this SKU.</summary>
    public IReadOnlyList<string> BundledSkus { get; internal set; } = Array.Empty<string>();

    /// <summary>Images associated with this SKU.</summary>
    public IReadOnlyList<StoreImage> Images { get; internal set; } = Array.Empty<StoreImage>();

    /// <summary>Videos associated with this SKU.</summary>
    public IReadOnlyList<StoreVideo> Videos { get; internal set; } = Array.Empty<StoreVideo>();

    /// <summary>Available purchase windows for this SKU.</summary>
    public IReadOnlyList<StoreAvailability> Availabilities { get; internal set; } = Array.Empty<StoreAvailability>();
}

/// <summary>
/// A purchase availability window for a SKU. Mirrors <c>XStoreAvailability</c>.
/// </summary>
public sealed class StoreAvailability
{
    internal StoreAvailability() { }

    /// <summary>Unique identifier for this availability.</summary>
    public string AvailabilityId { get; internal set; } = string.Empty;

    /// <summary>Price for this availability.</summary>
    public StorePrice Price { get; internal set; } = new StorePrice();

    /// <summary>UTC end date of this availability window; zero means no expiry.</summary>
    public DateTimeOffset EndDate { get; internal set; }
}

/// <summary>
/// A Store product. Mirrors <c>XStoreProduct</c>.
/// </summary>
/// <remarks>
/// Returned from product queries. The <c>XStoreProduct</c> struct and all nested arrays are only
/// valid during the native enumeration callback; this managed type is a complete deep copy.
/// </remarks>
public sealed class StoreProduct
{
    internal StoreProduct() { }

    /// <summary>The product's Store ID (e.g., "9WZDNCRFJBMP").</summary>
    public string StoreId { get; internal set; } = string.Empty;

    /// <summary>Localized display title.</summary>
    public string Title { get; internal set; } = string.Empty;

    /// <summary>Localized description.</summary>
    public string Description { get; internal set; } = string.Empty;

    /// <summary>BCP-47 language tag for the localized strings.</summary>
    public string Language { get; internal set; } = string.Empty;

    /// <summary>In-app offer token (for add-ons listed via in-app offers).</summary>
    public string InAppOfferToken { get; internal set; } = string.Empty;

    /// <summary>Deep-link URI to the product's Store page.</summary>
    public string LinkUri { get; internal set; } = string.Empty;

    /// <summary>The type of product.</summary>
    public StoreProductKind ProductKind { get; internal set; }

    /// <summary>Pricing information for the default SKU.</summary>
    public StorePrice Price { get; internal set; } = new StorePrice();

    /// <summary><see langword="true"/> when this product has a digital download.</summary>
    public bool HasDigitalDownload { get; internal set; }

    /// <summary><see langword="true"/> when the current user owns this product.</summary>
    public bool IsInUserCollection { get; internal set; }

    /// <summary>Searchable keywords.</summary>
    public IReadOnlyList<string> Keywords { get; internal set; } = Array.Empty<string>();

    /// <summary>All SKUs for this product.</summary>
    public IReadOnlyList<StoreSku> Skus { get; internal set; } = Array.Empty<StoreSku>();

    /// <summary>Images associated with this product.</summary>
    public IReadOnlyList<StoreImage> Images { get; internal set; } = Array.Empty<StoreImage>();

    /// <summary>Videos associated with this product.</summary>
    public IReadOnlyList<StoreVideo> Videos { get; internal set; } = Array.Empty<StoreVideo>();

    /// <summary>Returns a string for diagnostics.</summary>
    public override string ToString() => $"StoreProduct(Id={StoreId}, Kind={ProductKind}, Title={Title})";
}

/// <summary>
/// The game's licence. Mirrors <c>XStoreGameLicense</c>.
/// </summary>
public sealed class StoreGameLicense
{
    internal StoreGameLicense() { }

    /// <summary>The Store SKU identifier the licence is for.</summary>
    public string SkuStoreId { get; internal set; } = string.Empty;

    /// <summary><see langword="true"/> when the licence is currently active.</summary>
    public bool IsActive { get; internal set; }

    /// <summary><see langword="true"/> when the trial was purchased by the current user.</summary>
    public bool IsTrialOwnedByThisUser { get; internal set; }

    /// <summary><see langword="true"/> when this is a disc (offline) licence.</summary>
    public bool IsDiscLicense { get; internal set; }

    /// <summary><see langword="true"/> when this is a trial licence.</summary>
    public bool IsTrial { get; internal set; }

    /// <summary>Seconds remaining in the trial; 0 if not a trial.</summary>
    public uint TrialTimeRemainingInSeconds { get; internal set; }

    /// <summary>Unique identifier for the trial.</summary>
    public string TrialUniqueId { get; internal set; } = string.Empty;

    /// <summary>Expiry date of the licence; <see cref="DateTimeOffset.MinValue"/> for permanent licences.</summary>
    public DateTimeOffset ExpirationDate { get; internal set; }
}

/// <summary>
/// An add-on (DLC) licence. Mirrors <c>XStoreAddonLicense</c>.
/// </summary>
public sealed class StoreAddonLicense
{
    internal StoreAddonLicense() { }

    /// <summary>The Store SKU identifier the add-on licence is for.</summary>
    public string SkuStoreId { get; internal set; } = string.Empty;

    /// <summary>The in-app offer token associated with this add-on.</summary>
    public string InAppOfferToken { get; internal set; } = string.Empty;

    /// <summary><see langword="true"/> when the add-on licence is currently active.</summary>
    public bool IsActive { get; internal set; }

    /// <summary>Expiry date of the add-on licence; <see cref="DateTimeOffset.MinValue"/> for permanent add-ons.</summary>
    public DateTimeOffset ExpirationDate { get; internal set; }
}

/// <summary>
/// A pending package update. Mirrors <c>XStorePackageUpdate</c>.
/// </summary>
public sealed class StorePackageUpdate
{
    internal StorePackageUpdate() { }

    /// <summary>Package identifier of the package with a pending update.</summary>
    public string PackageIdentifier { get; internal set; } = string.Empty;

    /// <summary><see langword="true"/> when the update is mandatory.</summary>
    public bool IsMandatory { get; internal set; }
}

/// <summary>
/// Result of a can-acquire-licence check. Mirrors <c>XStoreCanAcquireLicenseResult</c>.
/// </summary>
public sealed class StoreCanAcquireLicenseResult
{
    internal StoreCanAcquireLicenseResult() { }

    /// <summary>The SKU identifier that can be licensed (when <see cref="Status"/> is <see cref="StoreCanLicenseStatus.Licensable"/>).</summary>
    public string LicensableSku { get; internal set; } = string.Empty;

    /// <summary>The status of the licence check.</summary>
    public StoreCanLicenseStatus Status { get; internal set; }
}

/// <summary>
/// Result of a consumable balance or fulfillment query. Mirrors <c>XStoreConsumableResult</c>.
/// </summary>
public readonly struct StoreConsumableResult
{
    /// <summary>The current quantity remaining in the consumable balance.</summary>
    public uint Quantity { get; }

    internal StoreConsumableResult(uint quantity) => Quantity = quantity;
}

/// <summary>
/// Result of a rate-and-review UI call. Mirrors <c>XStoreRateAndReviewResult</c>.
/// </summary>
public readonly struct StoreRateAndReviewResult
{
    /// <summary><see langword="true"/> when the user submitted or updated a review.</summary>
    public bool WasUpdated { get; }

    internal StoreRateAndReviewResult(bool wasUpdated) => WasUpdated = wasUpdated;
}
