// Raw interop types for the XStore family.
//
// Types mirror the XStore.h header (GDK edition 260404) one-for-one and keep native X* names.
// Rules:
//   bool      -> byte     (1 byte, blittable on all TFMs)
//   char*     -> byte*    (UTF-8; no marshalling)
//   size_t    -> nuint
//   time_t    -> long     (int64_t on all supported platforms)
//   char[N]   -> fixed byte[N] inside unsafe struct
//   HRESULT   -> int

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XStoreProductKind</c> from XStore.h.</summary>
[Flags]
internal enum XStoreProductKind : uint
{
    None = 0x00,
    Consumable = 0x01,
    Durable = 0x02,
    Game = 0x04,
    Pass = 0x08,
    UnmanagedConsumable = 0x10,
}

/// <summary>Mirrors <c>XStoreCanLicenseStatus</c> from XStore.h.</summary>
internal enum XStoreCanLicenseStatus : uint
{
    NotLicensableToUser = 0,
    Licensable = 1,
    LicenseActionNotApplicableToProduct = 2,
}

/// <summary>Mirrors <c>XStoreDurationUnit</c> from XStore.h.</summary>
internal enum XStoreDurationUnit : uint
{
    Minute = 0,
    Hour = 1,
    Day = 2,
    Week = 3,
    Month = 4,
    Year = 5,
}

/// <summary>Mirrors <c>XStorePrice</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStorePrice
{
    public float BasePrice;
    public float Price;
    public float RecurrencePrice;
    // 4 bytes padding (LayoutKind.Sequential aligns byte* to 8)
    private uint _pad;
    public byte* CurrencyCode;
    public fixed byte FormattedBasePrice[16];
    public fixed byte FormattedPrice[16];
    public fixed byte FormattedRecurrencePrice[16];
    public byte IsOnSale;
    // 7 bytes padding (LayoutKind.Sequential aligns long to 8)
    private fixed byte _pad2[7];
    public long SaleEndDate;
}

/// <summary>Mirrors <c>XStoreAvailability</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreAvailability
{
    public byte* AvailabilityId;
    public XStorePrice Price;
    public long EndDate;
}

/// <summary>Mirrors <c>XStoreImage</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreImage
{
    public byte* Uri;
    public uint Height;
    public uint Width;
    public byte* Caption;
    public byte* ImagePurposeTag;
}

/// <summary>Mirrors <c>XStoreVideo</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreVideo
{
    public byte* Uri;
    public uint Height;
    public uint Width;
    public byte* Caption;
    public byte* VideoPurposeTag;
    public XStoreImage PreviewImage;
}

/// <summary>Mirrors <c>XStoreCollectionData</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreCollectionData
{
    public long AcquiredDate;
    public long StartDate;
    public long EndDate;
    public byte IsTrial;
    // 3 bytes padding
    public uint TrialTimeRemainingInSeconds;
    public uint Quantity;
    // 4 bytes padding
    private uint _pad;
    public byte* CampaignId;
    public byte* DeveloperOfferId;
}

/// <summary>Mirrors <c>XStoreSubscriptionInfo</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XStoreSubscriptionInfo
{
    public byte HasTrialPeriod;
    // 3 bytes padding
    public XStoreDurationUnit TrialPeriodUnit;
    public uint TrialPeriod;
    public XStoreDurationUnit BillingPeriodUnit;
    public uint BillingPeriod;
}

/// <summary>Mirrors <c>XStoreSku</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreSku
{
    public byte* SkuId;
    public byte* Title;
    public byte* Description;
    public byte* Language;
    public XStorePrice Price;
    public byte IsTrial;
    public byte IsInUserCollection;
    // 6 bytes padding (XStoreCollectionData has 8-byte alignment due to long fields)
    private fixed byte _pad[6];
    public XStoreCollectionData CollectionData;
    public byte IsSubscription;
    // 3 bytes padding (XStoreSubscriptionInfo has 4-byte alignment)
    private fixed byte _pad2[3];
    public XStoreSubscriptionInfo SubscriptionInfo;
    public uint BundledSkusCount;
    // 4 bytes padding (pointer needs 8-byte alignment)
    private uint _pad3;
    public byte** BundledSkus;
    public uint ImagesCount;
    // 4 bytes padding
    private uint _pad4;
    public XStoreImage* Images;
    public uint VideosCount;
    // 4 bytes padding
    private uint _pad5;
    public XStoreVideo* Videos;
    public uint AvailabilitiesCount;
    // 4 bytes padding
    private uint _pad6;
    public XStoreAvailability* Availabilities;
}

/// <summary>Mirrors <c>XStoreProduct</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreProduct
{
    public byte* StoreId;
    public byte* Title;
    public byte* Description;
    public byte* Language;
    public byte* InAppOfferToken;
    public byte* LinkUri;
    public XStoreProductKind ProductKind;
    // 4 bytes padding (XStorePrice has 8-byte alignment due to pointer/long)
    private uint _pad;
    public XStorePrice Price;
    public byte HasDigitalDownload;
    public byte IsInUserCollection;
    // 2 bytes padding (keywordsCount is uint32_t, 4-byte alignment)
    private fixed byte _pad2[2];
    public uint KeywordsCount;
    public byte** Keywords;
    public uint SkusCount;
    // 4 bytes padding
    private uint _pad3;
    public XStoreSku* Skus;
    public uint ImagesCount;
    // 4 bytes padding
    private uint _pad4;
    public XStoreImage* Images;
    public uint VideosCount;
    // 4 bytes padding
    private uint _pad5;
    public XStoreVideo* Videos;
}

/// <summary>Mirrors <c>XStoreGameLicense</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreGameLicense
{
    public fixed byte SkuStoreId[18];
    public byte IsActive;
    public byte IsTrialOwnedByThisUser;
    public byte IsDiscLicense;
    public byte IsTrial;
    // 2 bytes padding (trialTimeRemainingInSeconds is uint32_t, 4-byte alignment)
    private fixed byte _pad[2];
    public uint TrialTimeRemainingInSeconds;
    public fixed byte TrialUniqueId[64];
    // 4 bytes padding (expirationDate is time_t/long, 8-byte alignment)
    private uint _pad2;
    public long ExpirationDate;
}

/// <summary>Mirrors <c>XStoreAddonLicense</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreAddonLicense
{
    public fixed byte SkuStoreId[18];
    public fixed byte InAppOfferToken[64];
    public byte IsActive;
    // 5 bytes padding (expirationDate is time_t/long, 8-byte alignment: 18+64+1=83 → 88)
    private fixed byte _pad[5];
    public long ExpirationDate;
}

/// <summary>Mirrors <c>XStorePackageUpdate</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStorePackageUpdate
{
    public fixed byte PackageIdentifier[33];
    public byte IsMandatory;
}

/// <summary>Mirrors <c>XStoreCanAcquireLicenseResult</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XStoreCanAcquireLicenseResult
{
    public fixed byte LicensableSku[5];
    // 3 bytes padding (XStoreCanLicenseStatus is uint32_t, 4-byte alignment)
    private fixed byte _pad[3];
    public XStoreCanLicenseStatus Status;
}

/// <summary>Mirrors <c>XStoreConsumableResult</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XStoreConsumableResult
{
    public uint Quantity;
}

/// <summary>Mirrors <c>XStoreRateAndReviewResult</c> from XStore.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XStoreRateAndReviewResult
{
    public byte WasUpdated;
}
