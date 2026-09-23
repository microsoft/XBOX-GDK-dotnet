using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Package;

/// <summary>
/// Package enumeration, identity queries, chunk management, write-stats and the
/// <see cref="PackageInstalled"/> event. All APIs require a packaged GDK process.
/// </summary>
/// <remarks>
/// Most APIs in this class require the process to have a package identity.
/// Calling them from an unpackaged process fails with
/// <c>E_GAMEPACKAGE_APP_NOT_PACKAGED</c>. Use <see cref="IsPackagedProcess"/> to guard.
/// </remarks>
public static unsafe class GamePackage
{
    // Maximum length of a package identifier string (including null terminator) per XPackage.h.
    private const int PackageIdentifierMaxLength = 33;

    // ---- process identity -------------------------------------------------------

    /// <summary>
    /// Returns <see langword="true"/> when the current process has a package identity
    /// (<c>XPackageIsPackagedProcess</c>). Safe to call from any process.
    /// </summary>
    public static bool IsPackagedProcess => Native.XPackageIsPackagedProcess() != 0;

    /// <summary>
    /// Returns the current process's package identifier
    /// (<c>XPackageGetCurrentProcessPackageIdentifier</c>).
    /// </summary>
    /// <remarks>
    /// The identifier is a durable, opaque, null-terminated string up to
    /// <c>XPACKAGE_IDENTIFIER_MAX_LENGTH</c> (33) characters. It is stable across updates.
    /// Requires a packaged process.
    /// </remarks>
    public static string GetCurrentPackageIdentifier()
    {
        byte[] buffer = new byte[PackageIdentifierMaxLength];
        int hr;
        fixed (byte* pBuf = buffer)
        {
            hr = Native.XPackageGetCurrentProcessPackageIdentifier((nuint)buffer.Length, pBuf);
        }
        Hr.ThrowIfFailed(hr);
        return ReadUtf8Array(buffer);
    }

    // ---- locale / write stats ---------------------------------------------------

    /// <summary>
    /// Returns the user locale configured for the current package
    /// (<c>XPackageGetUserLocale</c>), e.g. <c>"en-US"</c>.
    /// </summary>
    /// <remarks>Requires a packaged process.</remarks>
    public static string GetUserLocale()
    {
        // No maximum length published; start with 16 and grow if needed.
        int capacity = 16;
        while (true)
        {
            byte[] buf = new byte[capacity];
            int hr;
            fixed (byte* pBuf = buf)
            {
                hr = Native.XPackageGetUserLocale((nuint)capacity, pBuf);
            }

            if (hr == EInsufficientBuffer && capacity < 256)
            {
                capacity *= 2;
                continue;
            }

            Hr.ThrowIfFailed(hr);
            return ReadUtf8Array(buf);
        }
    }

    /// <summary>
    /// Returns current write-budget statistics for the packaged process
    /// (<c>XPackageGetWriteStats</c>).
    /// </summary>
    public static PackageWriteStats GetWriteStats()
    {
        XPackageWriteStats native;
        Hr.ThrowIfFailed(Native.XPackageGetWriteStats(&native));
        return new PackageWriteStats(native);
    }

    // ---- enumeration ------------------------------------------------------------

    /// <summary>
    /// Returns the kind of an installed package (<c>XPackageGetPackageKind</c>).
    /// </summary>
    /// <param name="packageIdentifier">The identifier of the package to classify.</param>
    /// <remarks>
    /// Added in GDK edition 260404. <see cref="PackageKind.PublisherContent"/> identifies content
    /// shared across a publisher's titles rather than owned by a single game.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="packageIdentifier"/> is null.</exception>
    public static PackageKind GetPackageKind(string packageIdentifier)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        byte[] pkgId = EncodeUtf8(packageIdentifier);
        XPackageKind kind;
        fixed (byte* pPkgId = pkgId)
        {
            Hr.ThrowIfFailed(Native.XPackageGetPackageKind(pPkgId, &kind));
        }

        return (PackageKind)kind;
    }

    /// <summary>
    /// Enumerates installed packages visible to the current title
    /// (<c>XPackageEnumeratePackages</c>).
    /// </summary>
    /// <param name="kind">Filter by package kind.</param>
    /// <param name="scope">How broadly to search relative to the current title.</param>
    /// <returns>A snapshot list of matching packages.</returns>
    public static IReadOnlyList<PackageInfo> EnumeratePackages(
        PackageKind kind = PackageKind.Game,
        PackageEnumerationScope scope = PackageEnumerationScope.ThisAndRelated)
    {
        var results = new List<PackageInfo>();
        var handle = GCHandle.Alloc(results);
        try
        {
            int hr = Native.XPackageEnumeratePackages(
                (XPackageKind)kind,
                (XPackageEnumerationScope)scope,
                GCHandle.ToIntPtr(handle),
                s_enumerationCallback);
            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            handle.Free();
        }
        return results;
    }

    /// <summary>
    /// Enumerates the feature set defined in the specified package's game config
    /// (<c>XPackageEnumerateFeatures</c>).
    /// </summary>
    /// <param name="packageIdentifier">
    /// The opaque package identifier. Pass the value from
    /// <see cref="GetCurrentPackageIdentifier"/> to query the running title.
    /// </param>
    public static IReadOnlyList<PackageFeature> EnumerateFeatures(string packageIdentifier)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));

        var results = new List<PackageFeature>();
        var handle = GCHandle.Alloc(results);
        try
        {
            byte[] pkgId = EncodeUtf8(packageIdentifier);
            int hr;
            fixed (byte* pPkgId = pkgId)
            {
                hr = Native.XPackageEnumerateFeatures(pPkgId, GCHandle.ToIntPtr(handle), s_featureCallback);
            }
            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            handle.Free();
        }
        return results;
    }

    // ---- chunk availability -----------------------------------------------------

    /// <summary>
    /// Enumerates availability for all chunks of a given selector type
    /// (<c>XPackageEnumerateChunkAvailability</c>).
    /// </summary>
    /// <param name="packageIdentifier">The opaque package identifier.</param>
    /// <param name="type">The selector type to enumerate (Language, Tag, Chunk or Feature).</param>
    public static IReadOnlyList<PackageChunkAvailabilityInfo> EnumerateChunkAvailability(
        string packageIdentifier,
        PackageChunkSelectorType type)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));

        var results = new List<PackageChunkAvailabilityInfo>();
        var handle = GCHandle.Alloc(results);
        try
        {
            byte[] pkgId = EncodeUtf8(packageIdentifier);
            int hr;
            fixed (byte* pPkgId = pkgId)
            {
                hr = Native.XPackageEnumerateChunkAvailability(
                    pPkgId,
                    (XPackageChunkSelectorType)type,
                    GCHandle.ToIntPtr(handle),
                    s_chunkAvailabilityCallback);
            }
            Hr.ThrowIfFailed(hr);
        }
        finally
        {
            handle.Free();
        }
        return results;
    }

    /// <summary>
    /// Returns the aggregate availability for a specific set of chunk selectors
    /// (<c>XPackageFindChunkAvailability</c>).
    /// </summary>
    /// <param name="packageIdentifier">The opaque package identifier.</param>
    /// <param name="selectors">The chunk selectors to query.</param>
    public static PackageChunkAvailability FindChunkAvailability(
        string packageIdentifier,
        PackageChunkSelector[] selectors)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        if (selectors is null) throw new ArgumentNullException(nameof(selectors));

        byte[] pkgId = EncodeUtf8(packageIdentifier);
        XPackageChunkAvailability result = default;

        if (selectors.Length == 0)
        {
            fixed (byte* pPkgId = pkgId)
            {
                Hr.ThrowIfFailed(Native.XPackageFindChunkAvailability(pPkgId, 0, null, &result));
            }
        }
        else
        {
            XPackageChunkSelector[] native = new XPackageChunkSelector[selectors.Length];
            GCHandle[] handles = new GCHandle[selectors.Length];
            try
            {
                FillNativeSelectors(selectors, native, handles);
                fixed (byte* pPkgId = pkgId)
                fixed (XPackageChunkSelector* pSels = native)
                {
                    Hr.ThrowIfFailed(Native.XPackageFindChunkAvailability(
                        pPkgId, (uint)native.Length, pSels, &result));
                }
            }
            finally
            {
                FreeNativeSelectors(handles);
            }
        }
        return (PackageChunkAvailability)result;
    }

    // ---- download size / install order ------------------------------------------

    /// <summary>
    /// Estimates the download size required to install the given chunks
    /// (<c>XPackageEstimateDownloadSize</c>).
    /// </summary>
    /// <param name="packageIdentifier">The opaque package identifier.</param>
    /// <param name="selectors">The chunks to estimate for.</param>
    /// <param name="shouldPromptUser">
    /// Set to <see langword="true"/> if the platform recommends showing a confirmation dialog
    /// before downloading.
    /// </param>
    /// <returns>Estimated download size in bytes.</returns>
    public static ulong EstimateDownloadSize(
        string packageIdentifier,
        PackageChunkSelector[] selectors,
        out bool shouldPromptUser)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        if (selectors is null) throw new ArgumentNullException(nameof(selectors));

        byte[] pkgId = EncodeUtf8(packageIdentifier);
        ulong size = 0;
        byte prompt = 0;

        if (selectors.Length == 0)
        {
            fixed (byte* pPkgId = pkgId)
            {
                Hr.ThrowIfFailed(Native.XPackageEstimateDownloadSize(pPkgId, 0, null, &size, &prompt));
            }
        }
        else
        {
            XPackageChunkSelector[] native = new XPackageChunkSelector[selectors.Length];
            GCHandle[] handles = new GCHandle[selectors.Length];
            try
            {
                FillNativeSelectors(selectors, native, handles);
                fixed (byte* pPkgId = pkgId)
                fixed (XPackageChunkSelector* pSels = native)
                {
                    Hr.ThrowIfFailed(Native.XPackageEstimateDownloadSize(
                        pPkgId, (uint)native.Length, pSels, &size, &prompt));
                }
            }
            finally
            {
                FreeNativeSelectors(handles);
            }
        }
        shouldPromptUser = prompt != 0;
        return size;
    }

    /// <summary>
    /// Changes the priority order in which chunks are installed
    /// (<c>XPackageChangeChunkInstallOrder</c>).
    /// </summary>
    public static void ChangeChunkInstallOrder(string packageIdentifier, PackageChunkSelector[] selectors)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        if (selectors is null) throw new ArgumentNullException(nameof(selectors));

        byte[] pkgId = EncodeUtf8(packageIdentifier);
        WithNativeSelectors(selectors, (pSelectors, count) =>
        {
            fixed (byte* pPkgId = pkgId)
            {
                return Native.XPackageChangeChunkInstallOrder(pPkgId, count, pSelectors);
            }
        });
    }

    // ---- chunk install / uninstall ----------------------------------------------

    /// <summary>
    /// Starts an asynchronous chunk installation and returns a monitor for tracking progress
    /// (<c>XPackageInstallChunksAsync</c> / <c>XPackageInstallChunksResult</c>).
    /// </summary>
    /// <param name="packageIdentifier">The opaque package identifier.</param>
    /// <param name="selectors">Chunks to install.</param>
    /// <param name="minimumUpdateIntervalMs">
    /// Minimum milliseconds between progress-changed callbacks; 0 means no throttling.
    /// </param>
    /// <param name="suppressUserConfirmation">
    /// <see langword="true"/> to suppress the system download confirmation UI.
    /// </param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <returns>A monitor that tracks the installation. Dispose when no longer needed.</returns>
    public static Task<PackageInstallationMonitor> InstallChunksAsync(
        string packageIdentifier,
        PackageChunkSelector[] selectors,
        uint minimumUpdateIntervalMs = 0,
        bool suppressUserConfirmation = false,
        CancellationToken cancellationToken = default)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        if (selectors is null) throw new ArgumentNullException(nameof(selectors));

        // Encode selector strings and pin them; they must stay alive until the async call starts.
        byte[] pkgId = EncodeUtf8(packageIdentifier);
        XPackageChunkSelector[] nativeSelectors = new XPackageChunkSelector[selectors.Length];
        GCHandle[] handles = new GCHandle[selectors.Length];
        byte suppress = suppressUserConfirmation ? (byte)1 : (byte)0;
        uint minMs = minimumUpdateIntervalMs;

        try
        {
            FillNativeSelectors(selectors, nativeSelectors, handles);

            return AsyncOperation<PackageInstallationMonitor>.RunAsync(
                IntPtr.Zero,
                block =>
                {
                    fixed (byte* pPkgId = pkgId)
                    fixed (XPackageChunkSelector* pSelectors = nativeSelectors)
                    {
                        return Native.XPackageInstallChunksAsync(
                            pPkgId, (uint)nativeSelectors.Length, pSelectors,
                            minMs, suppress, (XAsyncBlock*)block);
                    }
                },
                static (IntPtr block, out PackageInstallationMonitor value) =>
                {
                    value = null!;
                    IntPtr raw;
                    int hr = Native.XPackageInstallChunksResult((XAsyncBlock*)block, &raw);
                    if (HResult.Failed(hr)) return hr;
                    value = new PackageInstallationMonitor(raw);
                    return HResult.SOk;
                },
                cancellationToken);
        }
        finally
        {
            // Safe to release pins once the native async call has been started.
            FreeNativeSelectors(handles);
        }
    }

    /// <summary>
    /// Synchronously starts a chunk installation and returns a monitor
    /// (<c>XPackageInstallChunks</c>).
    /// </summary>
    public static PackageInstallationMonitor InstallChunks(
        string packageIdentifier,
        PackageChunkSelector[] selectors,
        uint minimumUpdateIntervalMs = 0,
        bool suppressUserConfirmation = false)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        if (selectors is null) throw new ArgumentNullException(nameof(selectors));

        byte[] pkgId = EncodeUtf8(packageIdentifier);
        IntPtr monitor = IntPtr.Zero;
        byte suppress = suppressUserConfirmation ? (byte)1 : (byte)0;

        if (selectors.Length == 0)
        {
            fixed (byte* pPkgId = pkgId)
            {
                Hr.ThrowIfFailed(Native.XPackageInstallChunks(
                    pPkgId, 0, null, minimumUpdateIntervalMs, suppress, IntPtr.Zero, &monitor));
            }
        }
        else
        {
            XPackageChunkSelector[] native = new XPackageChunkSelector[selectors.Length];
            GCHandle[] handles = new GCHandle[selectors.Length];
            try
            {
                FillNativeSelectors(selectors, native, handles);
                fixed (byte* pPkgId = pkgId)
                fixed (XPackageChunkSelector* pSels = native)
                {
                    Hr.ThrowIfFailed(Native.XPackageInstallChunks(
                        pPkgId, (uint)native.Length, pSels, minimumUpdateIntervalMs,
                        suppress, IntPtr.Zero, &monitor));
                }
            }
            finally
            {
                FreeNativeSelectors(handles);
            }
        }

        return new PackageInstallationMonitor(monitor);
    }

    /// <summary>
    /// Uninstalls previously installed chunks (<c>XPackageUninstallChunks</c>).
    /// </summary>
    public static void UninstallChunks(string packageIdentifier, PackageChunkSelector[] selectors)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        if (selectors is null) throw new ArgumentNullException(nameof(selectors));

        byte[] pkgId = EncodeUtf8(packageIdentifier);
        WithNativeSelectors(selectors, (pSelectors, count) =>
        {
            fixed (byte* pPkgId = pkgId)
            {
                return Native.XPackageUninstallChunks(pPkgId, count, pSelectors);
            }
        });
    }

    // ---- uninstall --------------------------------------------------------------

    /// <summary>
    /// Uninstalls the package identified by <paramref name="packageIdentifier"/>
    /// (<c>XPackageUninstallPackage</c>).
    /// </summary>
    /// <returns><see langword="true"/> when the uninstall was queued successfully.</returns>
    public static bool UninstallPackage(string packageIdentifier)
    {
        if (packageIdentifier is null) throw new ArgumentNullException(nameof(packageIdentifier));
        byte[] pkgId = EncodeUtf8(packageIdentifier);
        fixed (byte* pPkgId = pkgId)
        {
            return Native.XPackageUninstallPackage(pPkgId) != 0;
        }
    }

    /// <summary>
    /// Uninstalls a UWP app by package family name
    /// (<c>XPackageUninstallUWPInstance</c>).
    /// </summary>
    public static void UninstallUwpInstance(string packageName)
    {
        if (packageName is null) throw new ArgumentNullException(nameof(packageName));
        byte[] name = EncodeUtf8(packageName);
        fixed (byte* pName = name)
        {
            Hr.ThrowIfFailed(Native.XPackageUninstallUWPInstance(pName));
        }
    }

    // ---- PackageInstalled event -------------------------------------------------

    /// <summary>
    /// Raised when a new package is installed while the title is running
    /// (<c>XPackageRegisterPackageInstalled</c>).
    /// </summary>
    /// <remarks>
    /// Registration with the native runtime is deferred until the first subscriber is added.
    /// If the process is not packaged the registration call fails and the exception is propagated
    /// to the subscribing code.
    /// </remarks>
    public static event EventHandler<PackageInstalledEventArgs>? PackageInstalled
    {
        add
        {
            lock (s_gate)
            {
                s_packageInstalled += value;
                EnsureInstalledRegistered();
            }
        }
        remove
        {
            lock (s_gate)
            {
                s_packageInstalled -= value;
            }
        }
    }

    /// <summary>
    /// Tears down the native registration behind <see cref="PackageInstalled"/> and drops every
    /// subscriber (<c>XPackageUnregisterPackageInstalled</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="PackageInstalled"/> is a static event with no owning instance, so the registration
    /// otherwise lives for the lifetime of the process. Call this when the title is done listening —
    /// typically during shutdown, before <c>XGameRuntimeUninitialize</c>. Subscribing again after
    /// this call re-registers.
    /// </para>
    /// <para>
    /// This waits for any in-flight callback to finish, so it must <b>not</b> be called from inside
    /// a <see cref="PackageInstalled"/> handler.
    /// </para>
    /// </remarks>
    public static void UnregisterPackageInstalled()
    {
        lock (s_gate)
        {
            s_packageInstalled = null;

            if (!s_installedRegistered)
            {
                return;
            }

            // wait: true — returns only once no callback is running.
            Native.XPackageUnregisterPackageInstalled(s_installedToken, wait: 1);

            if (s_installedContextHandle.IsAllocated)
            {
                s_installedContextHandle.Free();
            }

            s_installedToken = default;
            s_installedRegistered = false;
        }
    }

    // ---- internal helpers -------------------------------------------------------

    /// <summary>
    /// Creates a monitor that tracks already-installed chunk progress without starting an install.
    /// (<c>XPackageCreateInstallationMonitor</c>).
    /// </summary>
    internal static PackageInstallationMonitor CreateInstallationMonitor(
        string packageIdentifier,
        PackageChunkSelector[]? selectors,
        uint minimumUpdateIntervalMs)
    {
        byte[] pkgId = EncodeUtf8(packageIdentifier);
        IntPtr monitor = IntPtr.Zero;

        if (selectors == null || selectors.Length == 0)
        {
            fixed (byte* pPkgId = pkgId)
            {
                Hr.ThrowIfFailed(Native.XPackageCreateInstallationMonitor(
                    pPkgId, 0, null, minimumUpdateIntervalMs, IntPtr.Zero, &monitor));
            }
        }
        else
        {
            XPackageChunkSelector[] nativeSels = new XPackageChunkSelector[selectors.Length];
            GCHandle[] handles = new GCHandle[selectors.Length];
            try
            {
                FillNativeSelectors(selectors, nativeSels, handles);
                fixed (byte* pPkgId = pkgId)
                fixed (XPackageChunkSelector* pSelectors = nativeSels)
                {
                    Hr.ThrowIfFailed(Native.XPackageCreateInstallationMonitor(
                        pPkgId, (uint)nativeSels.Length, pSelectors,
                        minimumUpdateIntervalMs, IntPtr.Zero, &monitor));
                }
            }
            finally
            {
                FreeNativeSelectors(handles);
            }
        }

        return new PackageInstallationMonitor(monitor);
    }

    // ---- private: gate & state --------------------------------------------------

    private static readonly object s_gate = new object();
    private static EventHandler<PackageInstalledEventArgs>? s_packageInstalled;
    private static XTaskQueueRegistrationToken s_installedToken;
    private static GCHandle s_installedContextHandle;
    private static bool s_installedRegistered;

    private static void EnsureInstalledRegistered()
    {
        if (s_installedRegistered) return;

        // The handle carries this class itself (as a sentinel context); the static dictionary
        // lookup in the callback uses it to find the event.
        s_installedContextHandle = GCHandle.Alloc(s_gate);
        IntPtr context = GCHandle.ToIntPtr(s_installedContextHandle);

        XTaskQueueRegistrationToken token;
        int hr = Native.XPackageRegisterPackageInstalled(
            IntPtr.Zero, context, s_packageInstalledCallback, &token);

        if (HResult.Failed(hr))
        {
            s_installedContextHandle.Free();
            Hr.ThrowIfFailed(hr);
        }

        s_installedToken = token;
        s_installedRegistered = true;
    }

    internal static void DispatchPackageInstalled(PackageInfo info)
    {
        EventHandler<PackageInstalledEventArgs>? handler;
        lock (s_gate)
        {
            handler = s_packageInstalled;
        }
        handler?.Invoke(null, new PackageInstalledEventArgs(info));
    }

    // ---- private: native string helpers -----------------------------------------

    internal static byte[] EncodeUtf8(string s) => Encoding.UTF8.GetBytes(s + "\0");

    internal static string ReadUtf8Array(byte[] buffer)
    {
        int len = 0;
        while (len < buffer.Length && buffer[len] != 0) len++;
        return len == 0 ? string.Empty : Encoding.UTF8.GetString(buffer, 0, len);
    }

    internal static unsafe string ReadNativeString(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero) return string.Empty;
        byte* p = (byte*)ptr.ToPointer();
        int len = 0;
        while (p[len] != 0) len++;
        if (len == 0) return string.Empty;
        byte[] buf = new byte[len];
        for (int i = 0; i < len; i++) buf[i] = p[i];
        return Encoding.UTF8.GetString(buf, 0, len);
    }

    // ---- private: selector marshaling -------------------------------------------

    private static void FillNativeSelectors(
        PackageChunkSelector[] managed,
        XPackageChunkSelector[] native,
        GCHandle[] handles)
    {
        for (int i = 0; i < managed.Length; i++)
        {
            native[i].Type = (XPackageChunkSelectorType)managed[i].Type;
            if (managed[i].StringValue != null)
            {
                byte[] encoded = EncodeUtf8(managed[i].StringValue!);
                handles[i] = GCHandle.Alloc(encoded, GCHandleType.Pinned);
                native[i].Value = handles[i].AddrOfPinnedObject();
            }
            else
            {
                native[i].Value = new IntPtr((long)managed[i].ChunkId);
            }
        }
    }

    private static void FreeNativeSelectors(GCHandle[] handles)
    {
        for (int i = 0; i < handles.Length; i++)
        {
            if (handles[i].IsAllocated) handles[i].Free();
        }
    }

    // Custom delegate to avoid CS0306 (pointer types cannot be generic type arguments).
    private unsafe delegate int NativeSelectorBody(XPackageChunkSelector* selectors, uint count);

    /// <summary>
    /// Runs <paramref name="body"/> with a pointer to a pinned native selector array.
    /// Pins string values for the duration of the call, then releases them.
    /// Throws any HRESULT failure returned by <paramref name="body"/>.
    /// </summary>
    private static unsafe void WithNativeSelectors(
        PackageChunkSelector[] selectors,
        NativeSelectorBody body)
    {
        if (selectors.Length == 0)
        {
            Hr.ThrowIfFailed(body(null, 0));
            return;
        }

        XPackageChunkSelector[] native = new XPackageChunkSelector[selectors.Length];
        GCHandle[] handles = new GCHandle[selectors.Length];
        try
        {
            FillNativeSelectors(selectors, native, handles);
            fixed (XPackageChunkSelector* pSelectors = native)
            {
                Hr.ThrowIfFailed(body(pSelectors, (uint)native.Length));
            }
        }
        finally
        {
            FreeNativeSelectors(handles);
        }
    }

    // ---- private: chunk availability from native --------------------------------

    private static unsafe PackageChunkSelector ReadNativeSelector(XPackageChunkSelector* sel)
    {
        switch (sel->Type)
        {
            case XPackageChunkSelectorType.Language:
                return PackageChunkSelector.ByLanguage(ReadNativeString(sel->Value));
            case XPackageChunkSelectorType.Tag:
                return PackageChunkSelector.ByTag(ReadNativeString(sel->Value));
            case XPackageChunkSelectorType.Feature:
                return PackageChunkSelector.ByFeature(ReadNativeString(sel->Value));
            default: // Chunk
                return PackageChunkSelector.ByChunkId((uint)sel->Value.ToInt64());
        }
    }

    // ---- private: PackageInfo and PackageFeature from native --------------------

    private static unsafe PackageInfo ReadPackageInfo(XPackageDetails* d) =>
        new PackageInfo(
            ReadNativeString(d->packageIdentifier),
            new PackageVersion(d->version),
            (PackageKind)d->kind,
            ReadNativeString(d->displayName),
            ReadNativeString(d->description),
            ReadNativeString(d->publisher),
            ReadNativeString(d->storeId),
            d->installing != 0,
            d->index,
            d->count,
            d->ageRestricted != 0,
            ReadNativeString(d->titleId));

    private static unsafe PackageFeature ReadPackageFeature(XPackageFeature* f)
    {
        uint n = f->storeIdCount;
        string[] storeIds = new string[n];
        if (n > 0 && f->storeIds != IntPtr.Zero)
        {
            byte** ptrs = (byte**)f->storeIds.ToPointer();
            for (uint i = 0; i < n; i++)
            {
                storeIds[i] = ReadNativeString(new IntPtr(ptrs[i]));
            }
        }
        return new PackageFeature(
            ReadNativeString(f->id),
            ReadNativeString(f->displayName),
            ReadNativeString(f->tags),
            f->hidden != 0,
            storeIds);
    }

    // ---- trampolines: NET5+ (UnmanagedCallersOnly) vs netstandard2.0 (delegate) -

#if NET5_0_OR_GREATER

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnPackageEnumerated(IntPtr context, IntPtr detailsPtr)
    {
        try
        {
            var handle = GCHandle.FromIntPtr(context);
            if (handle.Target is List<PackageInfo> list)
            {
                list.Add(ReadPackageInfo((XPackageDetails*)detailsPtr.ToPointer()));
            }
        }
        catch { }
        return 1;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnFeatureEnumerated(IntPtr context, IntPtr featurePtr)
    {
        try
        {
            var handle = GCHandle.FromIntPtr(context);
            if (handle.Target is List<PackageFeature> list)
            {
                list.Add(ReadPackageFeature((XPackageFeature*)featurePtr.ToPointer()));
            }
        }
        catch { }
        return 1;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnChunkAvailabilityEnumerated(
        IntPtr context,
        IntPtr selectorPtr,
        XPackageChunkAvailability availability)
    {
        try
        {
            var handle = GCHandle.FromIntPtr(context);
            if (handle.Target is List<PackageChunkAvailabilityInfo> list)
            {
                var selector = ReadNativeSelector((XPackageChunkSelector*)selectorPtr.ToPointer());
                list.Add(new PackageChunkAvailabilityInfo(selector, (PackageChunkAvailability)availability));
            }
        }
        catch { }
        return 1;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnPackageInstalledEvent(IntPtr context, IntPtr detailsPtr)
    {
        try
        {
            DispatchPackageInstalled(ReadPackageInfo((XPackageDetails*)detailsPtr.ToPointer()));
        }
        catch { }
    }

    private static readonly IntPtr s_enumerationCallback =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnPackageEnumerated;

    private static readonly IntPtr s_featureCallback =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnFeatureEnumerated;

    private static readonly IntPtr s_chunkAvailabilityCallback =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, XPackageChunkAvailability, byte>)&OnChunkAvailabilityEnumerated;

    private static readonly IntPtr s_packageInstalledCallback =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, void>)&OnPackageInstalledEvent;

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte PackageEnumerationDelegate(IntPtr context, IntPtr detailsPtr);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte FeatureEnumerationDelegate(IntPtr context, IntPtr featurePtr);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte ChunkAvailabilityDelegate(IntPtr context, IntPtr selectorPtr, XPackageChunkAvailability availability);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void PackageInstalledDelegate(IntPtr context, IntPtr detailsPtr);

    private static byte OnPackageEnumerated(IntPtr context, IntPtr detailsPtr)
    {
        try
        {
            var handle = GCHandle.FromIntPtr(context);
            if (handle.Target is List<PackageInfo> list)
            {
                unsafe { list.Add(ReadPackageInfo((XPackageDetails*)detailsPtr.ToPointer())); }
            }
        }
        catch { }
        return 1;
    }

    private static byte OnFeatureEnumerated(IntPtr context, IntPtr featurePtr)
    {
        try
        {
            var handle = GCHandle.FromIntPtr(context);
            if (handle.Target is List<PackageFeature> list)
            {
                unsafe { list.Add(ReadPackageFeature((XPackageFeature*)featurePtr.ToPointer())); }
            }
        }
        catch { }
        return 1;
    }

    private static byte OnChunkAvailabilityEnumerated(
        IntPtr context, IntPtr selectorPtr, XPackageChunkAvailability availability)
    {
        try
        {
            var handle = GCHandle.FromIntPtr(context);
            if (handle.Target is List<PackageChunkAvailabilityInfo> list)
            {
                unsafe
                {
                    var selector = ReadNativeSelector((XPackageChunkSelector*)selectorPtr.ToPointer());
                    list.Add(new PackageChunkAvailabilityInfo(selector, (PackageChunkAvailability)availability));
                }
            }
        }
        catch { }
        return 1;
    }

    private static void OnPackageInstalledEvent(IntPtr context, IntPtr detailsPtr)
    {
        try
        {
            unsafe { DispatchPackageInstalled(ReadPackageInfo((XPackageDetails*)detailsPtr.ToPointer())); }
        }
        catch { }
    }

    private static readonly PackageEnumerationDelegate s_enumerationCallbackDelegate = OnPackageEnumerated;
    private static readonly FeatureEnumerationDelegate s_featureCallbackDelegate = OnFeatureEnumerated;
    private static readonly ChunkAvailabilityDelegate s_chunkAvailabilityCallbackDelegate = OnChunkAvailabilityEnumerated;
    private static readonly PackageInstalledDelegate s_packageInstalledCallbackDelegate = OnPackageInstalledEvent;

    private static readonly IntPtr s_enumerationCallback =
        Marshal.GetFunctionPointerForDelegate(s_enumerationCallbackDelegate);

    private static readonly IntPtr s_featureCallback =
        Marshal.GetFunctionPointerForDelegate(s_featureCallbackDelegate);

    private static readonly IntPtr s_chunkAvailabilityCallback =
        Marshal.GetFunctionPointerForDelegate(s_chunkAvailabilityCallbackDelegate);

    private static readonly IntPtr s_packageInstalledCallback =
        Marshal.GetFunctionPointerForDelegate(s_packageInstalledCallbackDelegate);

#endif

    // HRESULT from winerror.h: ERROR_INSUFFICIENT_BUFFER.
    private const int EInsufficientBuffer = unchecked((int)0x8007007A);
}
