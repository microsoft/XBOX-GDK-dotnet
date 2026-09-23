using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// A user's Xbox Live profile. Managed snapshot of <c>XblUserProfile</c>.
/// </summary>
/// <remarks>
/// The native struct carries fixed-size inline UTF-8 buffers rather than pointers, so this type is
/// a straight copy and stays valid indefinitely — nothing here points into runtime-owned memory.
/// </remarks>
public sealed class UserProfile
{
    internal UserProfile(
        ulong xboxUserId,
        string appDisplayName,
        string appDisplayPictureUri,
        string gameDisplayName,
        string gameDisplayPictureUri,
        string gamerscore,
        string gamertag,
        string modernGamertag,
        string modernGamertagSuffix,
        string uniqueModernGamertag)
    {
        XboxUserId = xboxUserId;
        AppDisplayName = appDisplayName;
        AppDisplayPictureUri = appDisplayPictureUri;
        GameDisplayName = gameDisplayName;
        GameDisplayPictureUri = gameDisplayPictureUri;
        Gamerscore = gamerscore;
        Gamertag = gamertag;
        ModernGamertag = modernGamertag;
        ModernGamertagSuffix = modernGamertagSuffix;
        UniqueModernGamertag = uniqueModernGamertag;
    }

    /// <summary>The user's Xbox user id.</summary>
    public ulong XboxUserId { get; }

    /// <summary>Display name for application UI. Always equal to <see cref="GameDisplayName"/>.</summary>
    public string AppDisplayName { get; }

    /// <summary>
    /// Resizable gamer-picture URI for application UI. Append
    /// <c>&amp;format=png&amp;w={width}&amp;h={height}</c> — 64, 208 and 424 are the supported sizes.
    /// </summary>
    public string AppDisplayPictureUri { get; }

    /// <summary>Display name for in-game UI. Always equal to <see cref="AppDisplayName"/>.</summary>
    public string GameDisplayName { get; }

    /// <summary>Resizable gamer-picture URI for in-game UI. See <see cref="AppDisplayPictureUri"/>.</summary>
    public string GameDisplayPictureUri { get; }

    /// <summary>The user's gamerscore, as the service formats it.</summary>
    public string Gamerscore { get; }

    /// <summary>The classic gamertag: ASCII only, with no suffix.</summary>
    public string Gamertag { get; }

    /// <summary>The modern gamertag, with no suffix. Not guaranteed unique.</summary>
    public string ModernGamertag { get; }

    /// <summary>The numeric suffix that makes <see cref="ModernGamertag"/> unique. May be empty.</summary>
    public string ModernGamertagSuffix { get; }

    /// <summary>The unique modern gamertag, formatted <c>modernGamertag#suffix</c>.</summary>
    public string UniqueModernGamertag { get; }

    /// <inheritdoc/>
    public override string ToString() =>
        UniqueModernGamertag.Length > 0 ? UniqueModernGamertag : Gamertag;

    internal static unsafe UserProfile FromNative(in XblUserProfile native)
    {
        fixed (XblUserProfile* p = &native)
        {
            return new UserProfile(
                p->XboxUserId,
                Utf8.ToString(p->AppDisplayName, XblUserProfile.DisplayNameCharSize),
                Utf8.ToString(p->AppDisplayPictureResizeUri, XblUserProfile.DisplayPicUrlRawCharSize),
                Utf8.ToString(p->GameDisplayName, XblUserProfile.DisplayNameCharSize),
                Utf8.ToString(p->GameDisplayPictureResizeUri, XblUserProfile.DisplayPicUrlRawCharSize),
                Utf8.ToString(p->Gamerscore, XblUserProfile.GamerscoreCharSize),
                Utf8.ToString(p->Gamertag, XblUserProfile.GamertagCharSize),
                Utf8.ToString(p->ModernGamertag, XblUserProfile.ModernGamertagCharSize),
                Utf8.ToString(p->ModernGamertagSuffix, XblUserProfile.ModernGamertagSuffixCharSize),
                Utf8.ToString(p->UniqueModernGamertag, XblUserProfile.UniqueModernGamertagCharSize));
        }
    }
}

/// <summary>
/// The social groups <see cref="ProfileService.GetForSocialGroupAsync"/> understands. These are the
/// only two names the service accepts.
/// </summary>
public enum SocialGroup
{
    /// <summary>Everyone on the user's friends list.</summary>
    People = 0,

    /// <summary>Only the users the signed-in user marked as favorites.</summary>
    Favorites = 1,
}

/// <summary>
/// Xbox Live profile lookups. Reached through <see cref="XboxLiveContext.Profiles"/>.
/// Mirrors <c>profile_c.h</c>.
/// </summary>
public sealed unsafe class ProfileService
{
    private readonly XboxLiveContext _context;

    internal ProfileService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Gets one user's profile (<c>XblProfileGetUserProfileAsync</c>).
    /// </summary>
    /// <remarks>
    /// Use <see cref="GetAsync(IEnumerable{ulong}, CancellationToken)"/> when more than one profile
    /// is needed: the service batches them into a single request.
    /// </remarks>
    public Task<UserProfile> GetAsync(ulong xboxUserId, CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;

        return AsyncOperation<UserProfile>.RunAsync(
            _context.Queue.RawHandle(),
            block => NativeXbl.XblProfileGetUserProfileAsync(context, xboxUserId, (XAsyncBlock*)block),
            static (IntPtr block, out UserProfile value) =>
            {
                value = null!;

                XblUserProfile native;
                int hr = NativeXbl.XblProfileGetUserProfileResult((XAsyncBlock*)block, &native);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = UserProfile.FromNative(native);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Gets the profile of the user this context belongs to.
    /// </summary>
    public Task<UserProfile> GetOwnAsync(CancellationToken cancellationToken = default) =>
        GetAsync(_context.XboxUserId, cancellationToken);

    /// <summary>
    /// Gets several users' profiles in one request (<c>XblProfileGetUserProfilesAsync</c>).
    /// </summary>
    /// <param name="xboxUserIds">The users to look up. An empty sequence returns an empty result.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<IReadOnlyList<UserProfile>> GetAsync(
        IEnumerable<ulong> xboxUserIds,
        CancellationToken cancellationToken = default)
    {
        if (xboxUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxUserIds));
        }

        ulong[] ids = ToArray(xboxUserIds);
        if (ids.Length == 0)
        {
            return Task.FromResult<IReadOnlyList<UserProfile>>(Array.Empty<UserProfile>());
        }

        IntPtr context = _context.Handle;

        // Unmanaged rather than a pinned managed array, matching the rest of the projection: the
        // buffer is released from the result reader, which runs on every completion path including
        // cancellation, so there is exactly one owner and no pin outliving the call.
        IntPtr idBuffer = Marshal.AllocHGlobal(ids.Length * sizeof(ulong));
        try
        {
            for (int i = 0; i < ids.Length; i++)
            {
                ((ulong*)idBuffer)[i] = ids[i];
            }

            return AsyncOperation<IReadOnlyList<UserProfile>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblProfileGetUserProfilesAsync(
                    context,
                    (ulong*)idBuffer,
                    (nuint)ids.Length,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<UserProfile> value) =>
                {
                    Marshal.FreeHGlobal(idBuffer);
                    return ReadProfiles(block, out value);
                },
                cancellationToken);
        }
        catch
        {
            Marshal.FreeHGlobal(idBuffer);
            throw;
        }
    }

    /// <summary>
    /// Gets the profiles of everyone in a social group
    /// (<c>XblProfileGetUserProfilesForSocialGroupAsync</c>).
    /// </summary>
    public Task<IReadOnlyList<UserProfile>> GetForSocialGroupAsync(
        SocialGroup socialGroup,
        CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        IntPtr groupName = Utf8.Allocate(SocialGroupName(socialGroup));

        try
        {
            return AsyncOperation<IReadOnlyList<UserProfile>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblProfileGetUserProfilesForSocialGroupAsync(
                    context,
                    (byte*)groupName,
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<UserProfile> value) =>
                {
                    Utf8.Free(groupName);
                    return ReadSocialGroupProfiles(block, out value);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(groupName);
            throw;
        }
    }

    internal static string SocialGroupName(SocialGroup socialGroup) => socialGroup switch
    {
        SocialGroup.People => "People",
        SocialGroup.Favorites => "Favorites",
        _ => throw new ArgumentOutOfRangeException(
            nameof(socialGroup),
            socialGroup,
            "The Xbox Live service only recognizes the People and Favorites social groups."),
    };

    private static int ReadProfiles(IntPtr block, out IReadOnlyList<UserProfile> value)
    {
        value = Array.Empty<UserProfile>();

        nuint count;
        int hr = NativeXbl.XblProfileGetUserProfilesResultCount((XAsyncBlock*)block, &count);
        if (HResult.Failed(hr) || count == 0)
        {
            return hr;
        }

        var native = new XblUserProfile[(int)count];
        fixed (XblUserProfile* buffer = native)
        {
            hr = NativeXbl.XblProfileGetUserProfilesResult((XAsyncBlock*)block, count, buffer);
            if (HResult.Failed(hr))
            {
                return hr;
            }
        }

        value = Materialize(native);
        return HResult.SOk;
    }

    private static int ReadSocialGroupProfiles(IntPtr block, out IReadOnlyList<UserProfile> value)
    {
        value = Array.Empty<UserProfile>();

        nuint count;
        int hr = NativeXbl.XblProfileGetUserProfilesForSocialGroupResultCount((XAsyncBlock*)block, &count);
        if (HResult.Failed(hr) || count == 0)
        {
            return hr;
        }

        var native = new XblUserProfile[(int)count];
        fixed (XblUserProfile* buffer = native)
        {
            hr = NativeXbl.XblProfileGetUserProfilesForSocialGroupResult((XAsyncBlock*)block, count, buffer);
            if (HResult.Failed(hr))
            {
                return hr;
            }
        }

        value = Materialize(native);
        return HResult.SOk;
    }

    private static IReadOnlyList<UserProfile> Materialize(XblUserProfile[] native)
    {
        var profiles = new UserProfile[native.Length];
        for (int i = 0; i < native.Length; i++)
        {
            profiles[i] = UserProfile.FromNative(native[i]);
        }

        return new ReadOnlyCollection<UserProfile>(profiles);
    }

    private static ulong[] ToArray(IEnumerable<ulong> ids)
    {
        if (ids is ulong[] array)
        {
            return array;
        }

        if (ids is ICollection<ulong> collection)
        {
            var copy = new ulong[collection.Count];
            collection.CopyTo(copy, 0);
            return copy;
        }

        var list = new List<ulong>(ids);
        return list.ToArray();
    }
}
