using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net.Package;

/// <summary>
/// Owns an <c>XPackageMountHandle</c>; released with <c>XPackageCloseMountHandle</c>.
/// </summary>
internal sealed class PackageMountSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PackageMountSafeHandle()
        : base(ownsHandle: true)
    {
    }

    internal PackageMountSafeHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XPackageCloseMountHandle(handle);
        return true;
    }
}

/// <summary>
/// A mounted package. Owns an <c>XPackageMountHandle</c> and exposes the mount path.
/// </summary>
/// <remarks>
/// Obtain an instance via <see cref="MountWithUiAsync"/>. Dispose when the mount is no longer
/// needed; the native handle is released with <c>XPackageCloseMountHandle</c>.
/// </remarks>
public sealed unsafe class PackageMount : IDisposable
{
    private readonly PackageMountSafeHandle _handle;
    private bool _disposed;

    internal PackageMount(PackageMountSafeHandle handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Mounts a package after prompting the user to download any missing content
    /// (<c>XPackageMountWithUiAsync</c> / <c>XPackageMountWithUiResult</c>).
    /// </summary>
    /// <remarks>
    /// The deprecated synchronous <c>XPackageMount</c> is not available in
    /// <c>xgameruntime.thunks.dll</c> and is therefore not projected; this async
    /// overload is the only supported mount path.
    /// </remarks>
    /// <param name="packageIdentifier">
    /// The opaque package identifier. Obtain from
    /// <see cref="GamePackage.GetCurrentPackageIdentifier"/> or
    /// <see cref="GamePackage.EnumeratePackages"/>.
    /// </param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <returns>A <see cref="PackageMount"/> instance. Dispose when done.</returns>
    public static Task<PackageMount> MountWithUiAsync(
        string packageIdentifier,
        CancellationToken cancellationToken = default)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));

        byte[] pkgId = GamePackage.EncodeUtf8(packageIdentifier);

        return AsyncOperation<PackageMount>.RunAsync(
            IntPtr.Zero,
            block =>
            {
                fixed (byte* pPkgId = pkgId)
                {
                    return Native.XPackageMountWithUiAsync(pPkgId, (XAsyncBlock*)block);
                }
            },
            static (IntPtr block, out PackageMount value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XPackageMountWithUiResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr)) return hr;
                value = new PackageMount(new PackageMountSafeHandle(raw));
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// The file-system path to the mounted package
    /// (<c>XPackageGetMountPathSize</c> + <c>XPackageGetMountPath</c>).
    /// </summary>
    public string MountPath
    {
        get
        {
            ThrowIfDisposed();
            IntPtr h = _handle.DangerousGetHandle();

            nuint pathSize;
            Hr.ThrowIfFailed(Native.XPackageGetMountPathSize(h, &pathSize));

            if (pathSize == 0) return string.Empty;

            byte[] buf = new byte[(int)pathSize];
            fixed (byte* pBuf = buf)
            {
                Hr.ThrowIfFailed(Native.XPackageGetMountPath(h, pathSize, pBuf));
            }
            return GamePackage.ReadUtf8Array(buf);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _handle.Dispose();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(PackageMount));
    }
}
