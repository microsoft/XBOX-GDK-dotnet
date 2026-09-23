using System;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Where a title storage blob is stored. Mirrors <c>XblTitleStorageType</c>.</summary>
public enum TitleStorageType : uint
{
    /// <summary>Per-user storage restricted to Xbox consoles.</summary>
    TrustedPlatformStorage = 0,

    /// <summary>Global title storage, writable only through title configuration tools.</summary>
    GlobalStorage = 1,

    /// <summary>Per-user storage available to Xbox consoles, Windows and mobile devices.</summary>
    Universal = 2,
}

/// <summary>The payload format of a title storage blob. Mirrors <c>XblTitleStorageBlobType</c>.</summary>
public enum TitleStorageBlobType : uint
{
    /// <summary>The blob type is unknown.</summary>
    Unknown = 0,

    /// <summary>Binary payload data.</summary>
    Binary = 1,

    /// <summary>JSON payload data.</summary>
    Json = 2,

    /// <summary>Configuration payload data.</summary>
    Config = 3,
}

/// <summary>
/// ETag condition used when reading or writing title storage. Mirrors
/// <c>XblTitleStorageETagMatchCondition</c>.
/// </summary>
/// <remarks>
/// ETags implement optimistic concurrency. Use <see cref="IfMatch"/> to update or delete only the
/// version you previously read, and <see cref="IfNotMatch"/> to skip a transfer when the service
/// already has the supplied version. <see cref="NotUsed"/> ignores the ETag.
/// </remarks>
public enum TitleStorageETagMatchCondition : uint
{
    /// <summary>No ETag condition is applied.</summary>
    NotUsed = 0,

    /// <summary>Perform the request only when the supplied ETag matches the service value.</summary>
    IfMatch = 1,

    /// <summary>Perform the request only when the supplied ETag does not match the service value.</summary>
    IfNotMatch = 2,
}

/// <summary>How much title storage quota is used and available, in bytes.</summary>
public readonly struct TitleStorageQuota : IEquatable<TitleStorageQuota>
{
    internal TitleStorageQuota(ulong usedBytes, ulong quotaBytes)
    {
        UsedBytes = usedBytes;
        QuotaBytes = quotaBytes;
    }

    /// <summary>Bytes currently used in the requested title storage area.</summary>
    public ulong UsedBytes { get; }

    /// <summary>
    /// Soft quota for the requested title storage area. The service may report usage above this
    /// value.
    /// </summary>
    public ulong QuotaBytes { get; }

    /// <inheritdoc/>
    public bool Equals(TitleStorageQuota other) =>
        UsedBytes == other.UsedBytes && QuotaBytes == other.QuotaBytes;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is TitleStorageQuota other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => UsedBytes.GetHashCode() ^ QuotaBytes.GetHashCode();

    /// <summary>Equality operator.</summary>
    public static bool operator ==(TitleStorageQuota left, TitleStorageQuota right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(TitleStorageQuota left, TitleStorageQuota right) => !left.Equals(right);
}

/// <summary>
/// Managed snapshot of <c>XblTitleStorageBlobMetadata</c>.
/// </summary>
/// <remarks>
/// Native metadata returned from enumeration, upload and download belongs to the result handle or
/// async result that produced it. Instances of this type copy all fixed UTF-8 buffers and scalar
/// fields so they remain valid after the native owner is released.
/// </remarks>
public sealed class TitleStorageBlobMetadata
{
    /// <summary>Creates metadata for a title storage blob.</summary>
    /// <param name="blobPath">Unique blob path, for example <c>foo\bar\blob.json</c>.</param>
    /// <param name="blobType">The blob's payload format.</param>
    /// <param name="storageType">The title storage area containing the blob.</param>
    /// <param name="serviceConfigurationId">The title's service configuration id.</param>
    /// <param name="displayName">Optional friendly display name.</param>
    /// <param name="eTag">Optional ETag used for optimistic concurrency.</param>
    /// <param name="clientTimestamp">Optional client-supplied timestamp.</param>
    /// <param name="length">Known payload length in bytes.</param>
    /// <param name="xboxUserId">Owner Xbox user id; ignored for global storage.</param>
    public TitleStorageBlobMetadata(
        string blobPath,
        TitleStorageBlobType blobType,
        TitleStorageType storageType,
        string serviceConfigurationId,
        string? displayName = null,
        string? eTag = null,
        DateTimeOffset? clientTimestamp = null,
        ulong length = 0,
        ulong xboxUserId = 0)
    {
        if (string.IsNullOrEmpty(blobPath))
        {
            throw new ArgumentException("A blob path is required.", nameof(blobPath));
        }

        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        BlobPath = blobPath;
        BlobType = blobType;
        StorageType = storageType;
        DisplayName = displayName ?? string.Empty;
        ETag = eTag ?? string.Empty;
        ClientTimestamp = clientTimestamp;
        Length = length;
        ServiceConfigurationId = serviceConfigurationId;
        XboxUserId = xboxUserId;
    }

    private TitleStorageBlobMetadata(
        string blobPath,
        TitleStorageBlobType blobType,
        TitleStorageType storageType,
        string displayName,
        string eTag,
        DateTimeOffset? clientTimestamp,
        ulong length,
        string serviceConfigurationId,
        ulong xboxUserId)
    {
        BlobPath = blobPath;
        BlobType = blobType;
        StorageType = storageType;
        DisplayName = displayName;
        ETag = eTag;
        ClientTimestamp = clientTimestamp;
        Length = length;
        ServiceConfigurationId = serviceConfigurationId;
        XboxUserId = xboxUserId;
    }

    /// <summary>Unique blob path, for example <c>foo\bar\blob.json</c>.</summary>
    public string BlobPath { get; }

    /// <summary>The blob's payload format.</summary>
    public TitleStorageBlobType BlobType { get; }

    /// <summary>The title storage area containing the blob.</summary>
    public TitleStorageType StorageType { get; }

    /// <summary>Friendly display name supplied by the title, or an empty string.</summary>
    public string DisplayName { get; }

    /// <summary>
    /// Service ETag for this blob. Pass it back with
    /// <see cref="TitleStorageETagMatchCondition.IfMatch"/> to avoid overwriting a newer version.
    /// </summary>
    public string ETag { get; }

    /// <summary>Optional timestamp assigned by the title.</summary>
    public DateTimeOffset? ClientTimestamp { get; }

    /// <summary>Blob length in bytes.</summary>
    public ulong Length { get; }

    /// <summary>The title's service configuration id.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>Owner Xbox user id; 0 for global storage.</summary>
    public ulong XboxUserId { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{BlobPath} ({BlobType}, {Length} bytes)";

    internal static unsafe TitleStorageBlobMetadata FromNative(XblTitleStorageBlobMetadata* native)
    {
        return new TitleStorageBlobMetadata(
            Utf8.ToString(native->BlobPath, XblTitleStorageBlobMetadata.BlobPathMaxLength),
            (TitleStorageBlobType)native->BlobType,
            (TitleStorageType)native->StorageType,
            Utf8.ToString(native->DisplayName, XblTitleStorageBlobMetadata.BlobDisplayNameMaxLength),
            Utf8.ToString(native->ETag, XblTitleStorageBlobMetadata.BlobETagMaxLength),
            native->ClientTimestamp == 0 ? null : FromUnixSeconds(native->ClientTimestamp),
            (ulong)native->Length,
            Utf8.ToString(
                native->ServiceConfigurationId,
                XblTitleStorageBlobMetadata.ServiceConfigurationIdLength),
            native->XboxUserId);
    }

    internal unsafe XblTitleStorageBlobMetadata ToNative(ulong? lengthOverride = null)
    {
        XblTitleStorageBlobMetadata native = default;

        CopyUtf8ToFixed(BlobPath, native.BlobPath, XblTitleStorageBlobMetadata.BlobPathMaxLength, nameof(BlobPath));
        CopyUtf8ToFixed(
            DisplayName,
            native.DisplayName,
            XblTitleStorageBlobMetadata.BlobDisplayNameMaxLength,
            nameof(DisplayName));
        CopyUtf8ToFixed(ETag, native.ETag, XblTitleStorageBlobMetadata.BlobETagMaxLength, nameof(ETag));
        CopyUtf8ToFixed(
            ServiceConfigurationId,
            native.ServiceConfigurationId,
            XblTitleStorageBlobMetadata.ServiceConfigurationIdLength,
            nameof(ServiceConfigurationId));

        native.BlobType = (XblTitleStorageBlobType)BlobType;
        native.StorageType = (XblTitleStorageType)StorageType;
        native.ClientTimestamp = ClientTimestamp.HasValue ? ClientTimestamp.GetValueOrDefault().ToUnixTimeSeconds() : 0;
        native.Length = ToNativeSize(lengthOverride.GetValueOrDefault(Length), nameof(Length));
        native.XboxUserId = XboxUserId;
        return native;
    }

    internal static DateTimeOffset FromUnixSeconds(long seconds)
    {
        const long MinSeconds = -62135596800;
        const long MaxSeconds = 253402300799;

        if (seconds <= MinSeconds)
        {
            return DateTimeOffset.MinValue;
        }

        return seconds >= MaxSeconds
            ? DateTimeOffset.MaxValue
            : DateTimeOffset.FromUnixTimeSeconds(seconds);
    }

    internal static nuint ToNativeSize(ulong value, string parameterName)
    {
        if (IntPtr.Size == 4 && value > uint.MaxValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, "The value is too large for size_t.");
        }

        return (nuint)value;
    }

    private static unsafe void CopyUtf8ToFixed(string value, byte* destination, int destinationLength, string parameterName)
    {
        IntPtr buffer = Utf8.Allocate(value);
        try
        {
            int i = 0;
            byte* source = (byte*)buffer;
            while (source[i] != 0)
            {
                if (i >= destinationLength - 1)
                {
                    throw new ArgumentException(
                        "The value is too long for the native fixed buffer.",
                        parameterName);
                }

                destination[i] = source[i];
                i++;
            }

            while (i < destinationLength)
            {
                destination[i] = 0;
                i++;
            }
        }
        finally
        {
            Utf8.Free(buffer);
        }
    }
}

/// <summary>Blob bytes returned by a title storage download, plus the service metadata.</summary>
public sealed class TitleStorageBlobDownloadResult
{
    internal TitleStorageBlobDownloadResult(TitleStorageBlobMetadata metadata, byte[] data)
    {
        Metadata = metadata;
        Data = data;
    }

    /// <summary>Metadata returned by <c>XblTitleStorageDownloadBlobResult</c>.</summary>
    public TitleStorageBlobMetadata Metadata { get; }

    /// <summary>Downloaded blob bytes. The array is owned by the caller.</summary>
    public byte[] Data { get; }
}

