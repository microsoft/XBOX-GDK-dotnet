using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.Users;

/// <summary>
/// A signed-in user. Wraps <c>XUserHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// The native handle is owned by a <see cref="System.Runtime.InteropServices.SafeHandle"/>, so a
/// missed <see cref="Dispose"/> still releases it at finalization. <see cref="Duplicate"/> produces
/// an independent instance with its own handle.
/// </para>
/// <para>
/// Equality follows <c>XUserCompare</c>. Because a comparison function cannot yield a hash,
/// <see cref="GetHashCode"/> hashes the stable <see cref="LocalId"/>, which every handle for the
/// same user shares.
/// </para>
/// </remarks>
public sealed unsafe class User : IDisposable, IEquatable<User>
{
    private readonly UserHandle _handle;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    internal User(UserHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;

        ulong id;
        Hr.ThrowIfFailed(Native.XUserGetId(handle.DangerousGetHandle(), &id));
        Id = id;

        XUserLocalId localId;
        Hr.ThrowIfFailed(Native.XUserGetLocalId(handle.DangerousGetHandle(), &localId));
        LocalId = new UserLocalId(localId.Value);
    }

    /// <summary>The user's Xbox user id (<c>XUserGetId</c>). Cached; stable for the handle's lifetime.</summary>
    public ulong Id { get; }

    /// <summary>The machine-stable local id (<c>XUserGetLocalId</c>). Cached; also backs <see cref="GetHashCode"/>.</summary>
    public UserLocalId LocalId { get; }

    /// <summary>The user's current sign-in state (<c>XUserGetState</c>).</summary>
    public UserState State
    {
        get
        {
            XUserState state;
            Hr.ThrowIfFailed(Native.XUserGetState(Handle, &state));
            return (UserState)state;
        }
    }

    /// <summary>The user's age group (<c>XUserGetAgeGroup</c>).</summary>
    public UserAgeGroup AgeGroup
    {
        get
        {
            XUserAgeGroup ageGroup;
            Hr.ThrowIfFailed(Native.XUserGetAgeGroup(Handle, &ageGroup));
            return (UserAgeGroup)ageGroup;
        }
    }

    /// <summary>Whether the user is a guest (<c>XUserGetIsGuest</c>).</summary>
    public bool IsGuest
    {
        get
        {
            byte isGuest;
            Hr.ThrowIfFailed(Native.XUserGetIsGuest(Handle, &isGuest));
            return isGuest != 0;
        }
    }

    /// <summary>
    /// The raw <c>XUserHandle</c>. Only valid for the duration of a call made while this instance is
    /// alive; other families take it to pass a user to the runtime.
    /// </summary>
    internal IntPtr Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle.DangerousGetHandle();
        }
    }

    /// <summary>
    /// Returns an independent instance backed by its own native handle
    /// (<c>XUserDuplicateHandle</c>).
    /// </summary>
    public User Duplicate()
    {
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XUserDuplicateHandle(Handle, &raw));
        return new User(new UserHandle(raw), _queue);
    }

    /// <summary>
    /// Reads a component of the user's gamertag (<c>XUserGetGamertag</c>).
    /// </summary>
    /// <remarks>
    /// The native call is a sized two-call pattern. The GDK publishes a maximum byte count per
    /// component, so the first attempt uses that; the loop only re-runs if a future edition needs
    /// more room.
    /// </remarks>
    public string GetGamertag(GamertagComponent component = GamertagComponent.UniqueModern)
    {
        IntPtr handle = Handle;
        int capacity = MaxGamertagBytes(component);

        while (true)
        {
            byte[] buffer = new byte[capacity];
            nuint used;
            int hr;

            fixed (byte* pinned = buffer)
            {
                hr = Native.XUserGetGamertag(
                    handle,
                    (XUserGamertagComponent)component,
                    (nuint)capacity,
                    pinned,
                    &used);
            }

            if (hr == HResultInsufficientBuffer && capacity < MaxGamertagGrowth)
            {
                capacity *= 2;
                continue;
            }

            Hr.ThrowIfFailed(hr);

            // gamertagUsed counts the terminating null.
            int length = used == 0 ? 0 : (int)used - 1;
            return length <= 0 ? string.Empty : Encoding.UTF8.GetString(buffer, 0, length);
        }
    }

    /// <summary>
    /// Downloads the user's gamer picture at <paramref name="size"/>
    /// (<c>XUserGetGamerPictureAsync</c>).
    /// </summary>
    public Task<byte[]> GetGamerPictureAsync(
        GamerPictureSize size = GamerPictureSize.Medium,
        CancellationToken cancellationToken = default)
    {
        UserHandle handle = RentHandle();

        return AsyncOperation<byte[]>.RunAsync(
            _queue.RawHandle(),
            block => Native.XUserGetGamerPictureAsync(
                handle.DangerousGetHandle(),
                (XUserGamerPictureSize)size,
                (XAsyncBlock*)block),
            static (IntPtr block, out byte[] value) =>
            {
                value = Array.Empty<byte>();

                nuint bufferSize;
                int hr = Native.XUserGetGamerPictureResultSize((XAsyncBlock*)block, &bufferSize);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                if (bufferSize == 0)
                {
                    return HResult.SOk;
                }

                byte[] buffer = new byte[(int)bufferSize];
                nuint used;
                fixed (byte* pinned = buffer)
                {
                    hr = Native.XUserGetGamerPictureResult((XAsyncBlock*)block, bufferSize, pinned, &used);
                }

                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = (int)used == buffer.Length ? buffer : Truncate(buffer, (int)used);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Checks a privilege for this user (<c>XUserCheckPrivilege</c>).
    /// </summary>
    /// <param name="privilege">The privilege to test.</param>
    /// <param name="denyReason">Why the privilege was denied; <see cref="UserPrivilegeDenyReason.None"/> when granted.</param>
    /// <param name="options">Scope of the check.</param>
    /// <returns><see langword="true"/> when the user holds the privilege.</returns>
    public bool CheckPrivilege(
        UserPrivilege privilege,
        out UserPrivilegeDenyReason denyReason,
        UserPrivilegeOptions options = UserPrivilegeOptions.None)
    {
        byte hasPrivilege;
        XUserPrivilegeDenyReason reason;

        int hr = Native.XUserCheckPrivilege(
            Handle,
            (XUserPrivilegeOptions)options,
            (XUserPrivilege)privilege,
            &hasPrivilege,
            &reason);
        Hr.ThrowIfFailed(hr);

        denyReason = (UserPrivilegeDenyReason)reason;
        return hasPrivilege != 0;
    }

    /// <summary>
    /// Signs this user out (<c>XUserSignOutAsync</c>).
    /// </summary>
    /// <remarks>
    /// Sign-out is normally driven by the user through the system UI. Prefer observing
    /// <see cref="Users.UserManager.UserChanged"/> and <see cref="State"/> over calling this.
    /// </remarks>
    public Task SignOutAsync(CancellationToken cancellationToken = default)
    {
        IntPtr user = Handle;
        return AsyncOperation.RunAsync(
            _queue.RawHandle(),
            block => Native.XUserSignOutAsync(user, (XAsyncBlock*)block),
            block => Native.XUserSignOutResult((XAsyncBlock*)block),
            cancellationToken);
    }

    /// <summary>
    /// Returns <see langword="true"/> when a sign-out is currently in progress
    /// (<c>XUserIsSignOutPresent</c>).
    /// </summary>
    /// <remarks>
    /// See <see cref="SignOutAsync"/>.
    /// </remarks>
    public static unsafe bool IsSignOutPresent()
    {
        byte present;
        Hr.ThrowIfFailed(Native.XUserIsSignOutPresent(&present));
        return present != 0;
    }

    /// <summary>
    /// Returns <see langword="true"/> when this user is the store user
    /// (<c>XUserIsStoreUser</c>).
    /// </summary>
    public bool IsStoreUser
    {
        get
        {
            return Native.XUserIsStoreUser(Handle) != 0;
        }
    }

    /// <summary>
    /// Returns the default audio endpoint id for the given role
    /// (<c>XUserGetDefaultAudioEndpointUtf16</c>).
    /// </summary>
    /// <param name="kind">Which endpoint role to query.</param>
    /// <returns>
    /// The endpoint id string, or <see langword="null"/> when the user has no default endpoint
    /// for this role.
    /// </returns>
    public unsafe string? GetDefaultAudioEndpointUtf16(UserDefaultAudioEndpointKind kind)
    {
        XUserLocalId localId = new XUserLocalId { Value = LocalId.Value };

        // XUserAudioEndpointMaxUtf16Count = 56 from XUser.h, +1 for safety.
        const int MaxCount = 57;
        char[] buffer = new char[MaxCount];
        nuint used;

        fixed (char* bufPtr = buffer)
        {
            int hr = Native.XUserGetDefaultAudioEndpointUtf16(
                localId,
                (XUserDefaultAudioEndpointKind)kind,
                (nuint)MaxCount,
                bufPtr,
                &used);
            Hr.ThrowIfFailed(hr);
        }

        if (used == 0)
        {
            return null;
        }

        // used counts the null terminator; subtract 1 for the string length.
        int length = (int)used - 1;
        return length <= 0 ? string.Empty : new string(buffer, 0, length);
    }

    /// <summary>
    /// Requests an Xbox Live token and optional body signature for the specified HTTP request
    /// (<c>XUserGetTokenAndSignatureAsync</c>). Method and URL are marshalled as UTF-8.
    /// </summary>
    /// <param name="options">Request options (e.g. force-refresh).</param>
    /// <param name="method">The HTTP method string, e.g. <c>"GET"</c>.</param>
    /// <param name="url">The request URL.</param>
    /// <param name="headers">Optional HTTP headers to include in the signature computation.</param>
    /// <param name="body">Optional request body bytes to sign.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public unsafe Task<TokenAndSignature> GetTokenAndSignatureAsync(
        TokenAndSignatureOptions options,
        string method,
        string url,
        IReadOnlyList<TokenAndSignatureHttpHeader>? headers = null,
        byte[]? body = null,
        CancellationToken cancellationToken = default)
    {
        if (method is null) throw new ArgumentNullException(nameof(method));
        if (url is null) throw new ArgumentNullException(nameof(url));

        UserHandle handle = RentHandle();

        IntPtr nativeMethod = IntPtr.Zero;
        IntPtr nativeUrl = IntPtr.Zero;
        IntPtr nativeHeaders = IntPtr.Zero;
        IntPtr[]? headerStrings = null;
        GCHandle bodyPin = default;
        int headerCount = headers?.Count ?? 0;

        try
        {
            nativeMethod = Utf8.Allocate(method);
            nativeUrl = Utf8.Allocate(url);

            if (headerCount > 0)
            {
                int structSize = sizeof(XUserGetTokenAndSignatureHttpHeader);
                nativeHeaders = System.Runtime.InteropServices.Marshal.AllocHGlobal(structSize * headerCount);
                headerStrings = new IntPtr[headerCount * 2];

                for (int i = 0; i < headerCount; i++)
                {
                    TokenAndSignatureHttpHeader h = headers![i];
                    IntPtr namePtr = Utf8.Allocate(h.Name);
                    IntPtr valuePtr = Utf8.Allocate(h.Value);
                    headerStrings[i * 2] = namePtr;
                    headerStrings[i * 2 + 1] = valuePtr;

                    XUserGetTokenAndSignatureHttpHeader* hdrPtr =
                        (XUserGetTokenAndSignatureHttpHeader*)nativeHeaders + i;
                    hdrPtr->Name = (byte*)namePtr;
                    hdrPtr->Value = (byte*)valuePtr;
                }
            }

            if (body != null && body.Length > 0)
            {
                bodyPin = GCHandle.Alloc(body, GCHandleType.Pinned);
            }
        }
        catch
        {
            FreeTokenResources(nativeMethod, nativeUrl, nativeHeaders, headerStrings);
            if (bodyPin.IsAllocated) bodyPin.Free();
            throw;
        }

        try
        {
            return AsyncOperation<TokenAndSignature>.RunAsync(
                _queue.RawHandle(),
                block =>
                {
                    void* bodyPtr = bodyPin.IsAllocated ? (void*)bodyPin.AddrOfPinnedObject() : null;
                    nuint bodySize = (nuint)(body?.Length ?? 0);
                    return Native.XUserGetTokenAndSignatureAsync(
                        handle.DangerousGetHandle(),
                        (XUserGetTokenAndSignatureOptions)options,
                        (byte*)nativeMethod,
                        (byte*)nativeUrl,
                        (nuint)headerCount,
                        headerCount > 0 ? (XUserGetTokenAndSignatureHttpHeader*)nativeHeaders : null,
                        bodySize,
                        bodyPtr,
                        (XAsyncBlock*)block);
                },
                (IntPtr block, out TokenAndSignature value) =>
                {
                    FreeTokenResources(nativeMethod, nativeUrl, nativeHeaders, headerStrings);
                    if (bodyPin.IsAllocated) bodyPin.Free();

                    value = null!;

                    nuint bufferSize;
                    int hr = Native.XUserGetTokenAndSignatureResultSize((XAsyncBlock*)block, &bufferSize);
                    if (HResult.Failed(hr)) return hr;

                    if (bufferSize == 0)
                    {
                        value = new TokenAndSignature(string.Empty, string.Empty);
                        return HResult.SOk;
                    }

                    byte[] buffer = new byte[(int)bufferSize];
                    fixed (byte* bufPtr = buffer)
                    {
                        XUserGetTokenAndSignatureData* data;
                        nuint used;
                        hr = Native.XUserGetTokenAndSignatureResult(
                            (XAsyncBlock*)block, bufferSize, bufPtr, &data, &used);
                        if (HResult.Failed(hr)) return hr;

                        string token = data->Token != null
                            ? Utf8.ToString(data->Token) ?? string.Empty
                            : string.Empty;
                        string sig = data->Signature != null
                            ? Utf8.ToString(data->Signature) ?? string.Empty
                            : string.Empty;
                        value = new TokenAndSignature(token, sig);
                    }

                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            FreeTokenResources(nativeMethod, nativeUrl, nativeHeaders, headerStrings);
            if (bodyPin.IsAllocated) bodyPin.Free();
            throw;
        }
    }

    /// <summary>
    /// Requests an Xbox Live token and optional body signature for the specified HTTP request
    /// (<c>XUserGetTokenAndSignatureUtf16Async</c>). Method and URL are marshalled as UTF-16.
    /// </summary>
    /// <param name="options">Request options (e.g. force-refresh).</param>
    /// <param name="method">The HTTP method string, e.g. <c>"GET"</c>.</param>
    /// <param name="url">The request URL.</param>
    /// <param name="headers">Optional HTTP headers to include in the signature computation.</param>
    /// <param name="body">Optional request body bytes to sign.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public unsafe Task<TokenAndSignature> GetTokenAndSignatureUtf16Async(
        TokenAndSignatureOptions options,
        string method,
        string url,
        IReadOnlyList<TokenAndSignatureHttpHeader>? headers = null,
        byte[]? body = null,
        CancellationToken cancellationToken = default)
    {
        if (method is null) throw new ArgumentNullException(nameof(method));
        if (url is null) throw new ArgumentNullException(nameof(url));

        UserHandle handle = RentHandle();

        IntPtr nativeMethod = IntPtr.Zero;
        IntPtr nativeUrl = IntPtr.Zero;
        IntPtr nativeHeaders = IntPtr.Zero;
        IntPtr[]? headerStrings = null;
        GCHandle bodyPin = default;
        int headerCount = headers?.Count ?? 0;

        try
        {
            nativeMethod = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(method);
            nativeUrl = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(url);

            if (headerCount > 0)
            {
                int structSize = sizeof(XUserGetTokenAndSignatureUtf16HttpHeader);
                nativeHeaders = System.Runtime.InteropServices.Marshal.AllocHGlobal(structSize * headerCount);
                headerStrings = new IntPtr[headerCount * 2];

                for (int i = 0; i < headerCount; i++)
                {
                    TokenAndSignatureHttpHeader h = headers![i];
                    IntPtr namePtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(h.Name);
                    IntPtr valuePtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(h.Value);
                    headerStrings[i * 2] = namePtr;
                    headerStrings[i * 2 + 1] = valuePtr;

                    XUserGetTokenAndSignatureUtf16HttpHeader* hdrPtr =
                        (XUserGetTokenAndSignatureUtf16HttpHeader*)nativeHeaders + i;
                    hdrPtr->Name = (char*)namePtr;
                    hdrPtr->Value = (char*)valuePtr;
                }
            }

            if (body != null && body.Length > 0)
            {
                bodyPin = GCHandle.Alloc(body, GCHandleType.Pinned);
            }
        }
        catch
        {
            FreeUtf16Resources(nativeMethod, nativeUrl, nativeHeaders, headerStrings);
            if (bodyPin.IsAllocated) bodyPin.Free();
            throw;
        }

        try
        {
            return AsyncOperation<TokenAndSignature>.RunAsync(
                _queue.RawHandle(),
                block =>
                {
                    void* bodyPtr = bodyPin.IsAllocated ? (void*)bodyPin.AddrOfPinnedObject() : null;
                    nuint bodySize = (nuint)(body?.Length ?? 0);
                    return Native.XUserGetTokenAndSignatureUtf16Async(
                        handle.DangerousGetHandle(),
                        (XUserGetTokenAndSignatureOptions)options,
                        (char*)nativeMethod,
                        (char*)nativeUrl,
                        (nuint)headerCount,
                        headerCount > 0 ? (XUserGetTokenAndSignatureUtf16HttpHeader*)nativeHeaders : null,
                        bodySize,
                        bodyPtr,
                        (XAsyncBlock*)block);
                },
                (IntPtr block, out TokenAndSignature value) =>
                {
                    FreeUtf16Resources(nativeMethod, nativeUrl, nativeHeaders, headerStrings);
                    if (bodyPin.IsAllocated) bodyPin.Free();

                    value = null!;

                    nuint bufferSize;
                    int hr = Native.XUserGetTokenAndSignatureUtf16ResultSize((XAsyncBlock*)block, &bufferSize);
                    if (HResult.Failed(hr)) return hr;

                    if (bufferSize == 0)
                    {
                        value = new TokenAndSignature(string.Empty, string.Empty);
                        return HResult.SOk;
                    }

                    byte[] buffer = new byte[(int)bufferSize];
                    fixed (byte* bufPtr = buffer)
                    {
                        XUserGetTokenAndSignatureUtf16Data* data;
                        nuint used;
                        hr = Native.XUserGetTokenAndSignatureUtf16Result(
                            (XAsyncBlock*)block, bufferSize, bufPtr, &data, &used);
                        if (HResult.Failed(hr)) return hr;

                        string token = data->Token != null ? new string(data->Token) : string.Empty;
                        string sig = data->Signature != null ? new string(data->Signature) : string.Empty;
                        value = new TokenAndSignature(token, sig);
                    }

                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            FreeUtf16Resources(nativeMethod, nativeUrl, nativeHeaders, headerStrings);
            if (bodyPin.IsAllocated) bodyPin.Free();
            throw;
        }
    }

    /// <summary>
    /// Displays a system UI that lets the user resolve a pending account issue
    /// (<c>XUserResolveIssueWithUiAsync</c>). The URL and method are marshalled as UTF-8.
    /// </summary>
    /// <param name="url">An optional URL to open in the resolution UI, or <see langword="null"/>.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public unsafe Task ResolveIssueWithUiAsync(
        string? url = null,
        CancellationToken cancellationToken = default)
    {
        UserHandle handle = RentHandle();
        IntPtr nativeUrl = Utf8.Allocate(url);

        try
        {
            return AsyncOperation.RunAsync(
                _queue.RawHandle(),
                block => Native.XUserResolveIssueWithUiAsync(
                    handle.DangerousGetHandle(), (byte*)nativeUrl, (XAsyncBlock*)block),
                block =>
                {
                    Utf8.Free(nativeUrl);
                    return Native.XUserResolveIssueWithUiResult((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(nativeUrl);
            throw;
        }
    }

    /// <summary>
    /// Displays a system UI that lets the user resolve a pending account issue
    /// (<c>XUserResolveIssueWithUiUtf16Async</c>). The URL is marshalled as UTF-16.
    /// </summary>
    /// <param name="url">An optional URL to open in the resolution UI, or <see langword="null"/>.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public unsafe Task ResolveIssueWithUiUtf16Async(
        string? url = null,
        CancellationToken cancellationToken = default)
    {
        UserHandle handle = RentHandle();
        IntPtr nativeUrl = url != null
            ? System.Runtime.InteropServices.Marshal.StringToHGlobalUni(url)
            : IntPtr.Zero;

        try
        {
            return AsyncOperation.RunAsync(
                _queue.RawHandle(),
                block => Native.XUserResolveIssueWithUiUtf16Async(
                    handle.DangerousGetHandle(), (char*)nativeUrl, (XAsyncBlock*)block),
                block =>
                {
                    if (nativeUrl != IntPtr.Zero)
                        System.Runtime.InteropServices.Marshal.FreeHGlobal(nativeUrl);
                    return Native.XUserResolveIssueWithUiUtf16Result((XAsyncBlock*)block);
                },
                cancellationToken);
        }
        catch
        {
            if (nativeUrl != IntPtr.Zero)
                System.Runtime.InteropServices.Marshal.FreeHGlobal(nativeUrl);
            throw;
        }
    }

    /// <summary>
    /// Displays a system UI that lets the user resolve a missing or denied privilege
    /// (<c>XUserResolvePrivilegeWithUiAsync</c>).
    /// </summary>
    /// <param name="privilege">The privilege the title requires.</param>
    /// <param name="options">Scope options.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public unsafe Task ResolvePrivilegeWithUiAsync(
        UserPrivilege privilege,
        UserPrivilegeOptions options = UserPrivilegeOptions.None,
        CancellationToken cancellationToken = default)
    {
        UserHandle handle = RentHandle();

        return AsyncOperation.RunAsync(
            _queue.RawHandle(),
            block => Native.XUserResolvePrivilegeWithUiAsync(
                handle.DangerousGetHandle(),
                (XUserPrivilegeOptions)options,
                (XUserPrivilege)privilege,
                (XAsyncBlock*)block),
            block => Native.XUserResolvePrivilegeWithUiResult((XAsyncBlock*)block),
            cancellationToken);
    }

    /// <summary>
    /// Presents system UI to help the user pair a controller
    /// (<c>XUserFindControllerForUserWithUiAsync</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    /// <returns>
    /// The <see cref="AppLocalDeviceId"/> of the controller the user paired, or
    /// <see cref="AppLocalDeviceId.Null"/> when no controller was paired.
    /// </returns>
    public unsafe Task<AppLocalDeviceId> FindControllerWithUiAsync(
        CancellationToken cancellationToken = default)
    {
        UserHandle handle = RentHandle();

        return AsyncOperation<AppLocalDeviceId>.RunAsync(
            _queue.RawHandle(),
            block => Native.XUserFindControllerForUserWithUiAsync(
                handle.DangerousGetHandle(), (XAsyncBlock*)block),
            static (IntPtr block, out AppLocalDeviceId value) =>
            {
                value = default;
                XAppLocalDeviceId deviceId;
                int hr = Native.XUserFindControllerForUserWithUiResult((XAsyncBlock*)block, &deviceId);
                if (HResult.Failed(hr)) return hr;
                value = new AppLocalDeviceId(deviceId);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Compares two users with <c>XUserCompare</c>. Falls back to <see cref="LocalId"/> when either
    /// instance has already been disposed, so equality never throws.
    /// </summary>
    public bool Equals(User? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (_disposed || other._disposed)
        {
            return LocalId.Equals(other.LocalId);
        }

        return Native.XUserCompare(_handle.DangerousGetHandle(), other._handle.DangerousGetHandle()) == 0;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as User);

    /// <inheritdoc/>
    public override int GetHashCode() => LocalId.GetHashCode();

    /// <summary>Returns the Xbox user id and local id for diagnostics.</summary>
    public override string ToString() => $"User(Id=0x{Id:X16}, LocalId={LocalId})";

    /// <summary>Equality operator.</summary>
    public static bool operator ==(User? left, User? right)
        => left is null ? right is null : left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(User? left, User? right) => !(left == right);

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

    private const int HResultInsufficientBuffer = unchecked((int)0x8007007A);
    private const int MaxGamertagGrowth = 4096;

    private static int MaxGamertagBytes(GamertagComponent component) => component switch
    {
        // XUserGamertagComponent*MaxBytes from XUser.h.
        GamertagComponent.Classic => 16,
        GamertagComponent.Modern => 97,
        GamertagComponent.ModernSuffix => 15,
        _ => 101,
    };

    private static byte[] Truncate(byte[] buffer, int length)
    {
        byte[] result = new byte[length];
        Array.Copy(buffer, result, length);
        return result;
    }

    /// <summary>
    /// Returns the SafeHandle for capture by an async closure. Capturing the SafeHandle rather than
    /// a raw pointer keeps it rooted for the duration of the native call.
    /// </summary>
    private UserHandle RentHandle()
    {
        ThrowIfDisposed();
        return _handle;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(User));
        }
    }

    private static void FreeTokenResources(
        IntPtr method, IntPtr url, IntPtr headers, IntPtr[]? headerStrings)
    {
        Utf8.Free(method);
        Utf8.Free(url);
        if (headerStrings != null)
        {
            foreach (IntPtr ptr in headerStrings)
            {
                Utf8.Free(ptr);
            }
        }

        if (headers != IntPtr.Zero)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(headers);
        }
    }

    private static void FreeUtf16Resources(
        IntPtr method, IntPtr url, IntPtr headers, IntPtr[]? headerStrings)
    {
        if (method != IntPtr.Zero)
            System.Runtime.InteropServices.Marshal.FreeHGlobal(method);
        if (url != IntPtr.Zero)
            System.Runtime.InteropServices.Marshal.FreeHGlobal(url);
        if (headerStrings != null)
        {
            foreach (IntPtr ptr in headerStrings)
            {
                if (ptr != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);
            }
        }

        if (headers != IntPtr.Zero)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(headers);
        }
    }
}
