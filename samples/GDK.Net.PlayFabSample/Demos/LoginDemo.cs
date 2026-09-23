using System;
using System.Threading.Tasks;
using GDK.Net.PlayFab;
using GDK.Net.Users;

namespace GDK.Net.PlayFabSample.Demos;

/// <summary>
/// Getting an authenticated PlayFab entity, both ways: from the signed-in Xbox user (what a
/// shipping title does) and from a developer secret key (a server credential, shown only because
/// it works with nobody signed in).
/// </summary>
/// <remarks>
/// An <c>entity</c> is PlayFab's unit of identity, and almost every service call takes one. The
/// projection models it as an <see cref="IDisposable"/> object rather than a handle, so its
/// lifetime is the ordinary C# one.
/// </remarks>
internal static class LoginDemo
{
    /// <summary>
    /// Signs in and returns the player's entity, or <see langword="null"/> when no account is
    /// available. The caller owns the returned entity.
    /// </summary>
    public static async Task<PlayFabEntity?> RunAsync(
        GameRuntime runtime,
        PlayFabServiceConfig config,
        SampleOptions options)
    {
        Log.Write("");
        Log.Write("== Authentication ==");

        await TitleEntityAsync(config).ConfigureAwait(false);
        return await PlayerEntityAsync(runtime, config, options).ConfigureAwait(false);
    }

    /// <summary>The title-entity path: no account, just the title's own credential.</summary>
    private static async Task TitleEntityAsync(PlayFabServiceConfig config)
    {
        string? secretKey = SampleOptions.SecretKey;
        if (secretKey is null)
        {
            Log.Write("  no developer secret key in the environment; skipping the title-entity path");
            Log.Write("  (set GDKNET_PLAYFAB_SECRET_KEY to try it)");
            return;
        }

        // Every generated service call follows this shape: a static class named after the PlayFab
        // service, an entity, a request object and an awaited Task. No XAsyncBlock, no size probe,
        // no HRESULT.
        using PlayFabEntity title = await Authentication
            .GetEntityWithSecretKeyAsync(config, secretKey, new AuthenticationGetEntityRequest())
            .ConfigureAwait(false);

        EntityToken token = await title.GetEntityTokenAsync().ConfigureAwait(false);

        // The token is a credential, so only its shape is logged.
        Log.Write($"  title entity for {title.TitleId}: " +
                  $"{token.Token?.Length ?? 0}-character token expiring {token.Expiration:u}");
    }

    /// <summary>The path a shipping title uses: the signed-in Xbox user becomes a PlayFab player.</summary>
    private static async Task<PlayFabEntity?> PlayerEntityAsync(
        GameRuntime runtime,
        PlayFabServiceConfig config,
        SampleOptions options)
    {
        User? user = await AddUserAsync(runtime, options.AllowUI).ConfigureAwait(false);
        if (user is null)
        {
            Log.Write("  no Xbox user is signed in; skipping the player path");
            return null;
        }

        using (user)
        {
            // PlayFabLocalUser is the bridge. It holds the XUser and the service config, and its
            // login exchanges the Xbox identity for a PlayFab one.
            using PlayFabLocalUser localUser = PlayFabLocalUser.CreateForXboxUser(config, user);
            Log.Write($"  local user '{localUser.LocalId}' created for {user.GetGamertag(GamertagComponent.Modern)}");

            PlayFabLoginResult login = await localUser.LoginAsync().ConfigureAwait(false);
            Log.Write($"  signed in as PlayFab id {login.Result.PlayFabId} " +
                      $"(newly created: {login.Result.NewlyCreated})");

            // Detach takes ownership of the entity so it outlives the local user, which is what
            // lets the rest of the sample keep using it. Without it the entity would be released
            // when the local user is disposed at the end of this scope.
            return login.Detach();
        }
    }

    private static async Task<User?> AddUserAsync(GameRuntime runtime, bool allowUI)
    {
        try
        {
            // What a game does at startup: succeeds when an account is already signed in, and
            // never shows UI.
            return await runtime.Users
                .AddAsync(UserAddOptions.AddDefaultUserSilently)
                .ConfigureAwait(false);
        }
        catch (UserException ex)
        {
            // Failures are typed. UserException carries the reason the silent add was refused,
            // which is what tells a game to fall back to the account picker.
            if (!allowUI)
            {
                Log.Write($"  silent add failed ({ex.Message}); pass --allow-ui to pick an account");
                return null;
            }

            Log.Write($"  silent add failed ({ex.Message}); falling back to the account picker");
        }

        try
        {
            return await runtime.Users
                .AddAsync(UserAddOptions.AddDefaultUserAllowingUI)
                .ConfigureAwait(false);
        }
        catch (UserException ex)
        {
            Log.Write($"  the account picker did not return a user: {ex.Message}");
            return null;
        }
    }
}
