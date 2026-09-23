using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Users;

namespace GDK.Net.UserSample.Demos;

/// <summary>
/// The <c>XUser</c> APIs: signing a user in, reading their identity, checking privileges,
/// downloading a gamer picture, and cancelling an operation in flight.
/// </summary>
internal static class UserDemo
{
    public static async Task RunAsync(GameRuntime runtime, SampleOptions options)
    {
        Log.Write("");
        Log.Write("== Users ==");

        // XUserRegisterForChangeEvent is projected as an ordinary event. Unsubscribing is enough
        // to unregister; there is no token to hold on to.
        runtime.Users.UserChanged += OnUserChanged;

        // Adding the user is asynchronous in the GDK, so it is a Task here. There is no
        // XAsyncBlock, no completion callback and no XUserAddResult call to fetch the handle.
        using User user = await AddUserAsync(runtime, options.AllowUI).ConfigureAwait(false);

        ReadIdentity(user);
        ReadGamertags(user);
        CheckPrivileges(user);
        CompareHandles(user);

        await DownloadGamerPictureAsync(user, options.OutputDirectory).ConfigureAwait(false);
        await CancelAnOperationAsync(user).ConfigureAwait(false);
        await SignOutAsync(user, options.SignOut).ConfigureAwait(false);

        // Change events arrive on the process default task queue's thread pool, so there is
        // nothing to pump; wait briefly for any still in flight before unsubscribing.
        Thread.Sleep(TimeSpan.FromMilliseconds(50));
        runtime.Users.UserChanged -= OnUserChanged;
    }

    private static void OnUserChanged(object? sender, UserChangedEventArgs e) =>
        Log.Write($"  user changed: {e.Change} localId={e.LocalId} handle={(e.User is null ? "released" : "resolved")}");

    private static async Task<User> AddUserAsync(GameRuntime runtime, bool allowUI)
    {
        try
        {
            // The silent path is what a game does at startup: it succeeds when an account is
            // already signed in and never shows UI.
            User user = await runtime.Users
                .AddAsync(UserAddOptions.AddDefaultUserSilently)
                .ConfigureAwait(false);

            Log.Write("  added the default user silently");
            return user;
        }
        catch (UserException ex) when (allowUI)
        {
            // Failures are typed. UserException carries the reason a silent add was refused,
            // which is what tells a game to fall back to the account picker.
            Log.Write($"  silent add failed ({ex.Message}); falling back to the account picker");

            return await runtime.Users
                .AddAsync(UserAddOptions.AddDefaultUserAllowingUI)
                .ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Identity reads are plain properties. In C these are <c>XUserGetId</c>,
    /// <c>XUserGetLocalId</c>, <c>XUserGetState</c>, <c>XUserGetAgeGroup</c> and
    /// <c>XUserGetIsGuest</c>, each returning an <c>HRESULT</c> with the value in an out-parameter.
    /// </summary>
    private static void ReadIdentity(User user)
    {
        Log.Write($"  id        0x{user.Id:X16}");
        Log.Write($"  local id  {user.LocalId}");
        Log.Write($"  state     {user.State}");
        Log.Write($"  age group {user.AgeGroup}");
        Log.Write($"  guest     {user.IsGuest}");
    }

    /// <summary>
    /// <c>XUserGetGamertag</c> is a two-call API in C: once to size the buffer, once to fill it.
    /// The projection does both and returns a <see cref="string"/>.
    /// </summary>
    private static void ReadGamertags(User user)
    {
        foreach (GamertagComponent component in Enum.GetValues<GamertagComponent>())
        {
            Log.Write($"  gamertag ({component}) = {user.GetGamertag(component)}");
        }
    }

    /// <summary>
    /// A denied privilege is a normal answer, not a failure, so this returns <c>bool</c> with the
    /// reason as an out-parameter rather than throwing.
    /// </summary>
    private static void CheckPrivileges(User user)
    {
        UserPrivilege[] privileges =
        [
            UserPrivilege.Communications,
            UserPrivilege.Multiplayer,
            UserPrivilege.CrossPlay,
        ];

        foreach (UserPrivilege privilege in privileges)
        {
            bool granted = user.CheckPrivilege(privilege, out UserPrivilegeDenyReason reason);
            Log.Write($"  privilege {privilege} = {(granted ? "granted" : $"denied ({reason})")}");
        }
    }

    /// <summary>
    /// Two handles to the same account compare equal and hash the same, so a
    /// <see cref="User"/> can be used as a dictionary key. <c>XUserCompare</c> and
    /// <c>XUserDuplicateHandle</c> are behind <c>==</c> and <see cref="User.Duplicate"/>.
    /// </summary>
    private static void CompareHandles(User user)
    {
        using User duplicate = user.Duplicate();
        Log.Write($"  duplicate handle equals the original: {duplicate == user}");
    }

    private static async Task DownloadGamerPictureAsync(User user, string outputDirectory)
    {
        byte[] png = await user.GetGamerPictureAsync(GamerPictureSize.Small).ConfigureAwait(false);

        string path = Path.Combine(outputDirectory, "gamerpicture-small.png");
        File.WriteAllBytes(path, png);
        Log.Write($"  gamer picture: {png.Length} bytes written to {path}");
    }

    /// <summary>
    /// Cancellation uses <see cref="CancellationToken"/>. Under the hood the projection calls
    /// <c>XAsyncCancel</c>; the resulting <c>E_ABORT</c> becomes
    /// <see cref="OperationCanceledException"/> rather than a generic error.
    /// </summary>
    private static async Task CancelAnOperationAsync(User user)
    {
        using var cts = new CancellationTokenSource();

        Task<byte[]> pending = user.GetGamerPictureAsync(GamerPictureSize.ExtraLarge, cts.Token);
        cts.Cancel();

        try
        {
            byte[] png = await pending.ConfigureAwait(false);
            Log.Write($"  cancellation lost the race; the download finished ({png.Length} bytes)");
        }
        catch (OperationCanceledException)
        {
            Log.Write("  cancelled: E_ABORT surfaced as OperationCanceledException");
        }
    }

    private static async Task SignOutAsync(User user, bool signOut)
    {
        // XUserIsSignOutPresent was absent from the redistributable thunks DLL's export table
        // until GDK edition 260404, this projection's minimum, added it.
        Log.Write($"  sign-out UI present: {User.IsSignOutPresent()}");

        if (!signOut)
        {
            Log.Write("  skipping sign-out (pass --sign-out to actually sign the account out)");
            return;
        }

        await user.SignOutAsync().ConfigureAwait(false);
        Log.Write($"  signed out; state is now {user.State}");
    }
}
