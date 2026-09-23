using System;
using System.Linq;
using System.Threading.Tasks;
using GDK.Net.PlayFab;

namespace GDK.Net.PlayFabSample.Demos;

/// <summary>
/// Calling the generated service layer: reading server time, title data, the player's account and
/// their entity profile.
/// </summary>
/// <remarks>
/// <para>
/// All 20 service classes are generated from the PlayFab headers and all follow the same shape: a
/// static class named after the service, a method per API taking an entity plus a request object,
/// and a <see cref="Task{TResult}"/> that either produces the result or throws.
/// </para>
/// <para>
/// Note what is absent. There is no <c>XAsyncBlock</c> to allocate, no completion callback, no
/// second call to fetch the result size, no handle to close and no <c>HRESULT</c> to test. A
/// failure arrives as <see cref="PlayFabException"/>, which names the <c>E_PF_*</c> symbol.
/// </para>
/// </remarks>
internal static class ServicesDemo
{
    public static async Task RunAsync(PlayFabServiceConfig config, PlayFabEntity? playerEntity)
    {
        Log.Write("");
        Log.Write("== Services ==");

        // The server APIs need a title credential, so they only run when a secret key is present.
        string? secretKey = SampleOptions.SecretKey;
        if (secretKey is not null)
        {
            using PlayFabEntity title = await Authentication
                .GetEntityWithSecretKeyAsync(config, secretKey, new AuthenticationGetEntityRequest())
                .ConfigureAwait(false);

            await ServerTimeAsync(title).ConfigureAwait(false);
            await TitleDataAsync(title).ConfigureAwait(false);
        }
        else
        {
            Log.Write("  no developer secret key in the environment; skipping the server APIs");
        }

        if (playerEntity is null)
        {
            Log.Write("  no player entity; skipping the client APIs");
            return;
        }

        await AccountInfoAsync(playerEntity).ConfigureAwait(false);
        await ProfileAsync(playerEntity).ConfigureAwait(false);
    }

    /// <summary>The smallest real round trip: no title configuration is needed for it to answer.</summary>
    private static async Task ServerTimeAsync(PlayFabEntity entity)
    {
        TitleDataManagementGetTimeResult result = await TitleDataManagement
            .ServerGetTimeAsync(entity)
            .ConfigureAwait(false);

        // The projection converts PlayFab's ISO-8601 timestamps to DateTimeOffset, so this is
        // ordinary .NET date arithmetic rather than string parsing at the call site.
        TimeSpan skew = result.Time - DateTimeOffset.UtcNow;
        Log.Write($"  server time {result.Time:u} ({skew.TotalSeconds:F1}s from this machine)");
    }

    private static async Task TitleDataAsync(PlayFabEntity entity)
    {
        TitleDataManagementGetTitleDataResult result = await TitleDataManagement
            .ServerGetTitleDataAsync(entity, new TitleDataManagementGetTitleDataRequest())
            .ConfigureAwait(false);

        // A PlayFab string map arrives as an IReadOnlyDictionary, not a parallel key/value array
        // pair with a count.
        if (result.Data is null || result.Data.Count == 0)
        {
            Log.Write("  title data: no keys are configured for this title");
            return;
        }

        Log.Write($"  title data: {result.Data.Count} key(s)");
        foreach (string key in result.Data.Keys.Take(5))
        {
            Log.Write($"    {key} = {Summarize(result.Data[key])}");
        }
    }

    private static async Task AccountInfoAsync(PlayFabEntity entity)
    {
        try
        {
            AccountManagementGetAccountInfoResult result = await AccountManagement
                .ClientGetAccountInfoAsync(entity, new AccountManagementGetAccountInfoRequest())
                .ConfigureAwait(false);

            Log.Write($"  account {result.AccountInfo?.PlayFabId} created {result.AccountInfo?.Created:u}");
        }
        catch (PlayFabException ex)
        {
            // A title that disables the client API answers with an ordinary service error. It is
            // configuration, not a projection failure, so the sample reports and continues.
            Log.Write($"  account info unavailable: {ex.Message}");
        }
    }

    private static async Task ProfileAsync(PlayFabEntity entity)
    {
        try
        {
            ProfilesGetEntityProfileResponse result = await Profiles
                .GetProfileAsync(entity, new ProfilesGetEntityProfileRequest())
                .ConfigureAwait(false);

            ProfilesEntityProfileBody? profile = result.Profile;
            if (profile is null)
            {
                Log.Write("  profile: the service returned none");
                return;
            }

            // EntityKey is a projected type, not a pair of char* fields the caller has to length
            // check before reading.
            Log.Write($"  profile {profile.Entity?.Id} ({profile.Entity?.Type}) created {profile.Created:u}");
            Log.Write($"    display name: {profile.DisplayName ?? "(unset)"}");
            Log.Write($"    files: {profile.Files?.Count ?? 0}, experiment variants: {profile.ExperimentVariants?.Count ?? 0}");
        }
        catch (PlayFabException ex)
        {
            Log.Write($"  profile unavailable: {ex.Message}");
        }
    }

    private static string Summarize(string value) =>
        value.Length <= 60 ? value : value[..57] + "...";
}
