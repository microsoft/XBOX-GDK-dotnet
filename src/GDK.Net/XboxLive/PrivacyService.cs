using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// Xbox Live privacy permission checks and privacy lists. Mirrors <c>privacy_c.h</c>.
/// </summary>
/// <remarks>
/// <para>
/// Privacy checks are certification-sensitive gates for communication, multiplayer and
/// user-generated content. This projection fails closed by construction: permission-check methods
/// return <see cref="PrivacyPermissionCheckResult"/> objects whose <see cref="PrivacyPermissionCheckResult.IsAllowed"/>
/// is <see langword="false"/> whenever Xbox Live could not complete the check, including native
/// failures and cancellation.
/// </para>
/// <para>
/// The mute-list and block-list change handler functions are present in the header but are not
/// exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK edition 260404, so events are not
/// exposed for this family.
/// </para>
/// </remarks>
public sealed unsafe class PrivacyService
{
    private readonly XboxLiveContext _context;

    internal PrivacyService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Checks whether the signed-in user can perform an action with a target Xbox Live user
    /// (<c>XblPrivacyCheckPermissionAsync</c>).
    /// </summary>
    /// <param name="permission">The permission to check.</param>
    /// <param name="targetXboxUserId">The target user's Xbox user id.</param>
    /// <param name="cancellationToken">Cancels the native call; the returned result is denied.</param>
    /// <returns>
    /// A fail-closed permission result. <see cref="PrivacyPermissionCheckResult.IsAllowed"/> is
    /// <see langword="true"/> only when Xbox Live completed the check and explicitly granted the
    /// permission.
    /// </returns>
    public Task<PrivacyPermissionCheckResult> CheckPermissionAsync(
        Permission permission,
        ulong targetXboxUserId,
        CancellationToken cancellationToken = default)
    {
        PrivacyPermissionCheckResult failClosed = PrivacyPermissionCheckResult.FailClosed(
            permission,
            targetXboxUserId,
            AnonymousUserType.Unknown);

        try
        {
            IntPtr context = _context.Handle;
            return AsyncOperation<PrivacyPermissionCheckResult>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblPrivacyCheckPermissionAsync(
                    context,
                    (XblPermission)permission,
                    targetXboxUserId,
                    (XAsyncBlock*)block),
                (IntPtr block, out PrivacyPermissionCheckResult value) =>
                    ReadPermissionResult(block, anonymous: false, failClosed, out value),
                cancellationToken);
        }
        catch (Exception ex) when (IsPrivacyCheckCompletionFailure(ex))
        {
            return Task.FromResult(failClosed);
        }
    }

    /// <summary>
    /// Checks whether the signed-in user can perform an action with a class of non-Xbox Live users
    /// (<c>XblPrivacyCheckPermissionForAnonymousUserAsync</c>).
    /// </summary>
    /// <param name="permission">The permission to check.</param>
    /// <param name="targetUserType">The non-Xbox Live user class to check.</param>
    /// <param name="cancellationToken">Cancels the native call; the returned result is denied.</param>
    /// <returns>
    /// A fail-closed permission result. <see cref="PrivacyPermissionCheckResult.IsAllowed"/> is
    /// <see langword="true"/> only when Xbox Live completed the check and explicitly granted the
    /// permission.
    /// </returns>
    public Task<PrivacyPermissionCheckResult> CheckPermissionForAnonymousUserAsync(
        Permission permission,
        AnonymousUserType targetUserType,
        CancellationToken cancellationToken = default)
    {
        PrivacyPermissionCheckResult failClosed = PrivacyPermissionCheckResult.FailClosed(
            permission,
            0,
            targetUserType);

        try
        {
            IntPtr context = _context.Handle;
            return AsyncOperation<PrivacyPermissionCheckResult>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblPrivacyCheckPermissionForAnonymousUserAsync(
                    context,
                    (XblPermission)permission,
                    (XblAnonymousUserType)targetUserType,
                    (XAsyncBlock*)block),
                (IntPtr block, out PrivacyPermissionCheckResult value) =>
                    ReadPermissionResult(block, anonymous: true, failClosed, out value),
                cancellationToken);
        }
        catch (Exception ex) when (IsPrivacyCheckCompletionFailure(ex))
        {
            return Task.FromResult(failClosed);
        }
    }

    /// <summary>
    /// Checks several permissions against several Xbox Live and non-Xbox Live target classes
    /// (<c>XblPrivacyBatchCheckPermissionAsync</c>).
    /// </summary>
    /// <param name="permissions">The permissions to check. An empty sequence returns an empty result.</param>
    /// <param name="targetXboxUserIds">Xbox Live target users. An empty sequence checks only anonymous classes.</param>
    /// <param name="targetAnonymousUserTypes">
    /// Non-Xbox Live target classes. An empty sequence checks only Xbox Live target users.
    /// </param>
    /// <param name="cancellationToken">Cancels the native call; returned results are denied.</param>
    /// <returns>
    /// One fail-closed result for each permission-and-target combination. If the batch cannot be
    /// completed, every requested combination is returned as denied with
    /// <see cref="PrivacyPermissionCheckResult.WasChecked"/> <see langword="false"/>.
    /// </returns>
    public Task<IReadOnlyList<PrivacyPermissionCheckResult>> BatchCheckPermissionAsync(
        IEnumerable<Permission> permissions,
        IEnumerable<ulong> targetXboxUserIds,
        IEnumerable<AnonymousUserType> targetAnonymousUserTypes,
        CancellationToken cancellationToken = default)
    {
        if (permissions is null)
        {
            throw new ArgumentNullException(nameof(permissions));
        }

        if (targetXboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(targetXboxUserIds));
        }

        if (targetAnonymousUserTypes is null)
        {
            throw new ArgumentNullException(nameof(targetAnonymousUserTypes));
        }

        Permission[] permissionArray = ToArray(permissions);
        ulong[] xuidArray = ToArray(targetXboxUserIds);
        AnonymousUserType[] anonymousArray = ToArray(targetAnonymousUserTypes);
        IReadOnlyList<PrivacyPermissionCheckResult> failClosed = CreateFailClosedBatch(
            permissionArray,
            xuidArray,
            anonymousArray);

        if (permissionArray.Length == 0 || (xuidArray.Length == 0 && anonymousArray.Length == 0))
        {
            return Task.FromResult<IReadOnlyList<PrivacyPermissionCheckResult>>(
                Array.Empty<PrivacyPermissionCheckResult>());
        }

        IntPtr permissionBuffer = IntPtr.Zero;
        IntPtr xuidBuffer = IntPtr.Zero;
        IntPtr anonymousBuffer = IntPtr.Zero;

        try
        {
            permissionBuffer = AllocatePermissions(permissionArray);
            xuidBuffer = AllocateUlongs(xuidArray);
            anonymousBuffer = AllocateAnonymousUserTypes(anonymousArray);

            IntPtr context = _context.Handle;
            return AsyncOperation<IReadOnlyList<PrivacyPermissionCheckResult>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblPrivacyBatchCheckPermissionAsync(
                    context,
                    (XblPermission*)permissionBuffer,
                    (nuint)permissionArray.Length,
                    (ulong*)xuidBuffer,
                    (nuint)xuidArray.Length,
                    (XblAnonymousUserType*)anonymousBuffer,
                    (nuint)anonymousArray.Length,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<PrivacyPermissionCheckResult> value) =>
                {
                    Free(permissionBuffer);
                    Free(xuidBuffer);
                    Free(anonymousBuffer);
                    return ReadBatchPermissionResult(block, failClosed, out value);
                },
                cancellationToken);
        }
        catch (Exception ex) when (IsPrivacyCheckCompletionFailure(ex))
        {
            Free(permissionBuffer);
            Free(xuidBuffer);
            Free(anonymousBuffer);
            return Task.FromResult(failClosed);
        }
        catch
        {
            Free(permissionBuffer);
            Free(xuidBuffer);
            Free(anonymousBuffer);
            throw;
        }
    }

    /// <summary>
    /// Gets the Xbox user ids the signed-in user should avoid during multiplayer matchmaking
    /// (<c>XblPrivacyGetAvoidListAsync</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<ulong>> GetAvoidListAsync(CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        return AsyncOperation<IReadOnlyList<ulong>>.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblPrivacyGetAvoidListAsync(context, (XAsyncBlock*)block),
            ReadAvoidList,
            cancellationToken);
    }

    /// <summary>
    /// Gets the Xbox user ids the signed-in user should not hear during multiplayer matchmaking
    /// (<c>XblPrivacyGetMuteListAsync</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<ulong>> GetMuteListAsync(CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        return AsyncOperation<IReadOnlyList<ulong>>.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblPrivacyGetMuteListAsync(context, (XAsyncBlock*)block),
            ReadMuteList,
            cancellationToken);
    }

    private static int ReadPermissionResult(
        IntPtr block,
        bool anonymous,
        PrivacyPermissionCheckResult failClosed,
        out PrivacyPermissionCheckResult value)
    {
        value = failClosed;
        IntPtr buffer = IntPtr.Zero;

        try
        {
            nuint size;
            int hr = anonymous
                ? NativeXbl.XblPrivacyCheckPermissionForAnonymousUserResultSize((XAsyncBlock*)block, &size)
                : NativeXbl.XblPrivacyCheckPermissionResultSize((XAsyncBlock*)block, &size);
            if (HResult.Failed(hr) || size == 0)
            {
                return HResult.SOk;
            }

            buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
            XblPermissionCheckResult* result;
            nuint used;
            hr = anonymous
                ? NativeXbl.XblPrivacyCheckPermissionForAnonymousUserResult(
                    (XAsyncBlock*)block,
                    size,
                    (void*)buffer,
                    &result,
                    &used)
                : NativeXbl.XblPrivacyCheckPermissionResult(
                    (XAsyncBlock*)block,
                    size,
                    (void*)buffer,
                    &result,
                    &used);
            if (HResult.Failed(hr))
            {
                return HResult.SOk;
            }

            value = PrivacyPermissionCheckResult.FromNative(result);
            return HResult.SOk;
        }
        catch (Exception)
        {
            value = failClosed;
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static int ReadBatchPermissionResult(
        IntPtr block,
        IReadOnlyList<PrivacyPermissionCheckResult> failClosed,
        out IReadOnlyList<PrivacyPermissionCheckResult> value)
    {
        value = failClosed;
        IntPtr buffer = IntPtr.Zero;

        try
        {
            nuint size;
            int hr = NativeXbl.XblPrivacyBatchCheckPermissionResultSize((XAsyncBlock*)block, &size);
            if (HResult.Failed(hr) || size == 0)
            {
                return HResult.SOk;
            }

            buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
            XblPermissionCheckResult* results;
            nuint count;
            nuint used;
            hr = NativeXbl.XblPrivacyBatchCheckPermissionResult(
                (XAsyncBlock*)block,
                size,
                (void*)buffer,
                &results,
                &count,
                &used);
            if (HResult.Failed(hr) || count != (nuint)failClosed.Count)
            {
                return HResult.SOk;
            }

            var managed = new PrivacyPermissionCheckResult[(int)count];
            for (int i = 0; i < managed.Length; i++)
            {
                managed[i] = PrivacyPermissionCheckResult.FromNative(results + i);
            }

            value = new ReadOnlyCollection<PrivacyPermissionCheckResult>(managed);
            return HResult.SOk;
        }
        catch (Exception)
        {
            value = failClosed;
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static int ReadAvoidList(IntPtr block, out IReadOnlyList<ulong> value) =>
        ReadXuidList(
            block,
            static (XAsyncBlock* async, nuint* count) =>
                NativeXbl.XblPrivacyGetAvoidListResultCount(async, count),
            static (XAsyncBlock* async, nuint count, ulong* xuids) =>
                NativeXbl.XblPrivacyGetAvoidListResult(async, count, xuids),
            out value);

    private static int ReadMuteList(IntPtr block, out IReadOnlyList<ulong> value) =>
        ReadXuidList(
            block,
            static (XAsyncBlock* async, nuint* count) =>
                NativeXbl.XblPrivacyGetMuteListResultCount(async, count),
            static (XAsyncBlock* async, nuint count, ulong* xuids) =>
                NativeXbl.XblPrivacyGetMuteListResult(async, count, xuids),
            out value);

    private delegate int XuidListCountReader(XAsyncBlock* async, nuint* count);

    private delegate int XuidListReader(XAsyncBlock* async, nuint count, ulong* xuids);

    private static int ReadXuidList(
        IntPtr block,
        XuidListCountReader readCount,
        XuidListReader readList,
        out IReadOnlyList<ulong> value)
    {
        value = Array.Empty<ulong>();

        nuint count;
        int hr = readCount((XAsyncBlock*)block, &count);
        if (HResult.Failed(hr) || count == 0)
        {
            return hr;
        }

        var xuids = new ulong[(int)count];
        fixed (ulong* buffer = xuids)
        {
            hr = readList((XAsyncBlock*)block, count, buffer);
            if (HResult.Failed(hr))
            {
                return hr;
            }
        }

        value = new ReadOnlyCollection<ulong>(xuids);
        return HResult.SOk;
    }

    private static IReadOnlyList<PrivacyPermissionCheckResult> CreateFailClosedBatch(
        Permission[] permissions,
        ulong[] targetXboxUserIds,
        AnonymousUserType[] targetAnonymousUserTypes)
    {
        var results = new PrivacyPermissionCheckResult[
            checked(permissions.Length * (targetXboxUserIds.Length + targetAnonymousUserTypes.Length))];
        int index = 0;

        for (int i = 0; i < permissions.Length; i++)
        {
            for (int j = 0; j < targetXboxUserIds.Length; j++)
            {
                results[index++] = PrivacyPermissionCheckResult.FailClosed(
                    permissions[i],
                    targetXboxUserIds[j],
                    AnonymousUserType.Unknown);
            }

            for (int j = 0; j < targetAnonymousUserTypes.Length; j++)
            {
                results[index++] = PrivacyPermissionCheckResult.FailClosed(
                    permissions[i],
                    0,
                    targetAnonymousUserTypes[j]);
            }
        }

        return new ReadOnlyCollection<PrivacyPermissionCheckResult>(results);
    }

    private static IntPtr AllocatePermissions(Permission[] values)
    {
        if (values.Length == 0)
        {
            return IntPtr.Zero;
        }

        IntPtr buffer = Marshal.AllocHGlobal(checked(values.Length * sizeof(XblPermission)));
        XblPermission* native = (XblPermission*)buffer;
        for (int i = 0; i < values.Length; i++)
        {
            native[i] = (XblPermission)values[i];
        }

        return buffer;
    }

    private static IntPtr AllocateUlongs(ulong[] values)
    {
        if (values.Length == 0)
        {
            return IntPtr.Zero;
        }

        IntPtr buffer = Marshal.AllocHGlobal(checked(values.Length * sizeof(ulong)));
        ulong* native = (ulong*)buffer;
        for (int i = 0; i < values.Length; i++)
        {
            native[i] = values[i];
        }

        return buffer;
    }

    private static IntPtr AllocateAnonymousUserTypes(AnonymousUserType[] values)
    {
        if (values.Length == 0)
        {
            return IntPtr.Zero;
        }

        IntPtr buffer = Marshal.AllocHGlobal(checked(values.Length * sizeof(XblAnonymousUserType)));
        XblAnonymousUserType* native = (XblAnonymousUserType*)buffer;
        for (int i = 0; i < values.Length; i++)
        {
            native[i] = (XblAnonymousUserType)values[i];
        }

        return buffer;
    }

    private static T[] ToArray<T>(IEnumerable<T> values)
    {
        if (values is T[] array)
        {
            return array;
        }

        if (values is ICollection<T> collection)
        {
            var copy = new T[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        return new List<T>(values).ToArray();
    }

    private static bool IsPrivacyCheckCompletionFailure(Exception exception) =>
        exception is GameRuntimeException or OperationCanceledException or DllNotFoundException
            or EntryPointNotFoundException or BadImageFormatException;

    private static void Free(IntPtr buffer)
    {
        if (buffer != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}
