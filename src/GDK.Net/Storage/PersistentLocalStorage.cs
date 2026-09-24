using System;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.Storage;

/// <summary>
/// How much of the title's persistent local storage allocation is used and available, in bytes.
/// </summary>
public readonly struct PersistentLocalStorageSpaceInfo
{
    internal PersistentLocalStorageSpaceInfo(ulong availableFree, ulong totalFree, ulong used, ulong total)
    {
        AvailableFreeBytes = availableFree;
        TotalFreeBytes = totalFree;
        UsedBytes = used;
        TotalBytes = total;
    }

    /// <summary>Bytes that can be written right now.</summary>
    public ulong AvailableFreeBytes { get; }

    /// <summary>
    /// Bytes left in the allocation. Reaching these may require prompting the user to free space with
    /// <see cref="PersistentLocalStorage.PromptUserForSpaceAsync"/>.
    /// </summary>
    public ulong TotalFreeBytes { get; }

    /// <summary>Bytes already used.</summary>
    public ulong UsedBytes { get; }

    /// <summary>Maximum bytes the title may store.</summary>
    public ulong TotalBytes { get; }
}

/// <summary>
/// The title's persistent local storage: a per-title directory that survives updates and is not
/// synchronised to the cloud (<c>XPersistentLocalStorage.h</c>).
/// </summary>
public static unsafe class PersistentLocalStorage
{
    /// <summary>
    /// Mounts another package's persistent local storage
    /// (<c>XPersistentLocalStorageMountForPackage</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Unlike the rest of this type, which addresses the calling title's own storage, this reaches
    /// a package identified by <paramref name="packageIdentifier"/>: typically a related title in
    /// the same publisher family. The mount is synchronous; no download is triggered.
    /// </para>
    /// </remarks>
    /// <param name="packageIdentifier">
    /// The opaque package identifier. Obtain from
    /// <see cref="Package.GamePackage.GetCurrentPackageIdentifier"/> or
    /// <see cref="Package.GamePackage.EnumeratePackages"/>.
    /// </param>
    /// <returns>The mounted package. Dispose when the mount is no longer needed.</returns>
    public static Package.PackageMount MountForPackage(string packageIdentifier)
    {
        if (packageIdentifier is null)
        {
            throw new ArgumentNullException(nameof(packageIdentifier));
        }

        byte[] pkgId = Package.GamePackage.EncodeUtf8(packageIdentifier);
        IntPtr raw;

        fixed (byte* pPkgId = pkgId)
        {
            Hr.ThrowIfFailed(Native.XPersistentLocalStorageMountForPackage(pPkgId, &raw));
        }

        return new Package.PackageMount(new Package.PackageMountSafeHandle(raw));
    }

    /// <summary>
    /// Returns the absolute path of the title's persistent local storage directory
    /// (<c>XPersistentLocalStorageGetPathSize</c> then <c>XPersistentLocalStorageGetPath</c>).
    /// </summary>
    public static string GetPath()
    {
        nuint size;
        Hr.ThrowIfFailed(Native.XPersistentLocalStorageGetPathSize(&size));

        if (size == 0)
        {
            return string.Empty;
        }

        byte[] buffer = new byte[(int)size];
        nuint used;
        fixed (byte* pinned = buffer)
        {
            Hr.ThrowIfFailed(Native.XPersistentLocalStorageGetPath(size, pinned, &used));
            return Utf8.ToString(pinned, (int)used);
        }
    }

    /// <summary>
    /// Reports the title's storage quota and consumption
    /// (<c>XPersistentLocalStorageGetSpaceInfo</c>).
    /// </summary>
    public static PersistentLocalStorageSpaceInfo GetSpaceInfo()
    {
        XPersistentLocalStorageSpaceInfo info;
        Hr.ThrowIfFailed(Native.XPersistentLocalStorageGetSpaceInfo(&info));
        return new PersistentLocalStorageSpaceInfo(
            info.AvailableFreeBytes,
            info.TotalFreeBytes,
            info.UsedBytes,
            info.TotalBytes);
    }

    /// <summary>
    /// Shows the system UI that asks the user to free up storage
    /// (<c>XPersistentLocalStoragePromptUserForSpaceAsync</c>).
    /// </summary>
    /// <param name="requestedBytes">How many bytes the title needs.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public static Task PromptUserForSpaceAsync(
        ulong requestedBytes,
        CancellationToken cancellationToken = default)
    {
        return AsyncOperation.RunAsync(
            IntPtr.Zero,
            block => Native.XPersistentLocalStoragePromptUserForSpaceAsync(requestedBytes, (XAsyncBlock*)block),
            block => Native.XPersistentLocalStoragePromptUserForSpaceResult((XAsyncBlock*)block),
            cancellationToken);
    }
}
