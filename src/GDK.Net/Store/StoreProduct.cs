// Deep-copy helpers: convert native XStore structs into managed StoreProduct and related types.
//
// The XStoreProduct struct and all nested arrays are only valid for the duration of the native
// enumeration callback. Every string and array is copied into managed memory before the callback
// returns. No native pointers escape this file.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

namespace GDK.Net.Store;

internal static unsafe class StoreProductFactory
{
    /// <summary>
    /// Converts a Unix timestamp from an <c>XStore</c> struct into a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <remarks>
    /// The Store returns sentinels, not just dates. Zero means "unset", and a licence that never
    /// expires comes back as a <c>time_t</c> far outside the range <see cref="DateTimeOffset"/>
    /// accepts: passing that straight to <see cref="DateTimeOffset.FromUnixTimeSeconds"/> throws
    /// <see cref="ArgumentOutOfRangeException"/> from the middle of a native enumeration callback,
    /// which is how the live harness first hit this against a real game licence. Saturating is the
    /// projection that preserves the meaning: an unrepresentably distant expiry is
    /// <see cref="DateTimeOffset.MaxValue"/>, which callers already compare against correctly.
    /// </remarks>
    internal static DateTimeOffset DateFromUnixSeconds(long seconds)
    {
        if (seconds == 0)
        {
            return DateTimeOffset.MinValue;
        }

        if (seconds >= DateTimeOffset.MaxValue.ToUnixTimeSeconds())
        {
            return DateTimeOffset.MaxValue;
        }

        return seconds <= DateTimeOffset.MinValue.ToUnixTimeSeconds()
            ? DateTimeOffset.MinValue
            : DateTimeOffset.FromUnixTimeSeconds(seconds);
    }

    internal static StoreProduct FromNative(XStoreProduct* src)
    {
        var product = new StoreProduct
        {
            StoreId = PtrToString(src->StoreId),
            Title = PtrToString(src->Title),
            Description = PtrToString(src->Description),
            Language = PtrToString(src->Language),
            InAppOfferToken = PtrToString(src->InAppOfferToken),
            LinkUri = PtrToString(src->LinkUri),
            ProductKind = (StoreProductKind)src->ProductKind,
            Price = PriceFromNative(&src->Price),
            HasDigitalDownload = src->HasDigitalDownload != 0,
            IsInUserCollection = src->IsInUserCollection != 0,
            Keywords = CopyPtrStringArray(src->Keywords, src->KeywordsCount),
            Skus = CopySkuArray(src->Skus, src->SkusCount),
            Images = CopyImageArray(src->Images, src->ImagesCount),
            Videos = CopyVideoArray(src->Videos, src->VideosCount),
        };
        return product;
    }

    internal static StorePrice PriceFromNative(XStorePrice* src)
    {
        return new StorePrice
        {
            BasePrice = src->BasePrice,
            Price = src->Price,
            RecurrencePrice = src->RecurrencePrice,
            CurrencyCode = PtrToString(src->CurrencyCode),
            FormattedBasePrice = FixedBufferToString(src->FormattedBasePrice, 16),
            FormattedPrice = FixedBufferToString(src->FormattedPrice, 16),
            FormattedRecurrencePrice = FixedBufferToString(src->FormattedRecurrencePrice, 16),
            IsOnSale = src->IsOnSale != 0,
            SaleEndDate = DateFromUnixSeconds(src->SaleEndDate),
        };
    }

    internal static StoreImage ImageFromNative(XStoreImage* src)
    {
        return new StoreImage
        {
            Uri = PtrToString(src->Uri),
            Height = src->Height,
            Width = src->Width,
            Caption = PtrToString(src->Caption),
            ImagePurposeTag = PtrToString(src->ImagePurposeTag),
        };
    }

    internal static StoreVideo VideoFromNative(XStoreVideo* src)
    {
        return new StoreVideo
        {
            Uri = PtrToString(src->Uri),
            Height = src->Height,
            Width = src->Width,
            Caption = PtrToString(src->Caption),
            VideoPurposeTag = PtrToString(src->VideoPurposeTag),
            PreviewImage = ImageFromNative(&src->PreviewImage),
        };
    }

    internal static StoreAvailability AvailabilityFromNative(XStoreAvailability* src)
    {
        return new StoreAvailability
        {
            AvailabilityId = PtrToString(src->AvailabilityId),
            Price = PriceFromNative(&src->Price),
            EndDate = DateFromUnixSeconds(src->EndDate),
        };
    }

    internal static StoreCollectionData CollectionDataFromNative(XStoreCollectionData* src)
    {
        return new StoreCollectionData
        {
            AcquiredDate = DateFromUnixSeconds(src->AcquiredDate),
            StartDate = DateFromUnixSeconds(src->StartDate),
            EndDate = DateFromUnixSeconds(src->EndDate),
            IsTrial = src->IsTrial != 0,
            TrialTimeRemainingInSeconds = src->TrialTimeRemainingInSeconds,
            Quantity = src->Quantity,
            CampaignId = PtrToString(src->CampaignId),
            DeveloperOfferId = PtrToString(src->DeveloperOfferId),
        };
    }

    internal static StoreSubscriptionInfo SubscriptionInfoFromNative(XStoreSubscriptionInfo* src)
    {
        return new StoreSubscriptionInfo
        {
            HasTrialPeriod = src->HasTrialPeriod != 0,
            TrialPeriodUnit = (StoreDurationUnit)src->TrialPeriodUnit,
            TrialPeriod = src->TrialPeriod,
            BillingPeriodUnit = (StoreDurationUnit)src->BillingPeriodUnit,
            BillingPeriod = src->BillingPeriod,
        };
    }

    internal static StoreSku SkuFromNative(XStoreSku* src)
    {
        return new StoreSku
        {
            SkuId = PtrToString(src->SkuId),
            Title = PtrToString(src->Title),
            Description = PtrToString(src->Description),
            Language = PtrToString(src->Language),
            Price = PriceFromNative(&src->Price),
            IsTrial = src->IsTrial != 0,
            IsInUserCollection = src->IsInUserCollection != 0,
            CollectionData = CollectionDataFromNative(&src->CollectionData),
            IsSubscription = src->IsSubscription != 0,
            SubscriptionInfo = SubscriptionInfoFromNative(&src->SubscriptionInfo),
            BundledSkus = CopyPtrStringArray(src->BundledSkus, src->BundledSkusCount),
            Images = CopyImageArray(src->Images, src->ImagesCount),
            Videos = CopyVideoArray(src->Videos, src->VideosCount),
            Availabilities = CopyAvailabilityArray(src->Availabilities, src->AvailabilitiesCount),
        };
    }

    // --- helpers ---

    internal static string PtrToString(byte* ptr)
    {
        if (ptr == null)
        {
            return string.Empty;
        }

        int len = 0;
        while (ptr[len] != 0)
        {
            len++;
        }

        if (len == 0)
        {
            return string.Empty;
        }

        byte[] buf = new byte[len];
        Marshal.Copy((IntPtr)ptr, buf, 0, len);
        return Encoding.UTF8.GetString(buf, 0, len);
    }

    internal static string FixedBufferToString(byte* ptr, int maxLen)
    {
        int len = 0;
        while (len < maxLen && ptr[len] != 0)
        {
            len++;
        }

        if (len == 0)
        {
            return string.Empty;
        }

        byte[] buf = new byte[len];
        Marshal.Copy((IntPtr)ptr, buf, 0, len);
        return Encoding.UTF8.GetString(buf, 0, len);
    }

    private static IReadOnlyList<string> CopyPtrStringArray(byte** ptrs, uint count)
    {
        if (ptrs == null || count == 0)
        {
            return Array.Empty<string>();
        }

        var result = new string[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = PtrToString(ptrs[i]);
        }

        return result;
    }

    private static IReadOnlyList<StoreImage> CopyImageArray(XStoreImage* src, uint count)
    {
        if (src == null || count == 0)
        {
            return Array.Empty<StoreImage>();
        }

        var result = new StoreImage[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = ImageFromNative(&src[i]);
        }

        return result;
    }

    private static IReadOnlyList<StoreVideo> CopyVideoArray(XStoreVideo* src, uint count)
    {
        if (src == null || count == 0)
        {
            return Array.Empty<StoreVideo>();
        }

        var result = new StoreVideo[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = VideoFromNative(&src[i]);
        }

        return result;
    }

    private static IReadOnlyList<StoreSku> CopySkuArray(XStoreSku* src, uint count)
    {
        if (src == null || count == 0)
        {
            return Array.Empty<StoreSku>();
        }

        var result = new StoreSku[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = SkuFromNative(&src[i]);
        }

        return result;
    }

    private static IReadOnlyList<StoreAvailability> CopyAvailabilityArray(XStoreAvailability* src, uint count)
    {
        if (src == null || count == 0)
        {
            return Array.Empty<StoreAvailability>();
        }

        var result = new StoreAvailability[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = AvailabilityFromNative(&src[i]);
        }

        return result;
    }
}
