using System;

namespace GDK.Net.PlayFab;

/// <summary>
/// The outcome of a PlayFab client login: the authenticated entity plus the login payload.
/// </summary>
/// <remarks>
/// The native APIs return these as two out-parameters of a single <c>GetResult</c> call. They are
/// paired here so a caller receives one value and cannot lose ownership of the entity handle.
/// </remarks>
public sealed class PlayFabLoginResult : IDisposable
{
    internal PlayFabLoginResult(PlayFabEntity entity, AuthenticationLoginResult result)
    {
        Entity = entity;
        Result = result;
    }

    /// <summary>The authenticated entity. Owned by this instance until <see cref="Detach"/> is called.</summary>
    public PlayFabEntity Entity { get; private set; }

    /// <summary>The login payload PlayFab returned (account info, treatment assignments, ...).</summary>
    public AuthenticationLoginResult Result { get; }

    /// <summary>
    /// Transfers ownership of <see cref="Entity"/> to the caller, so disposing this instance no
    /// longer closes it.
    /// </summary>
    public PlayFabEntity Detach()
    {
        PlayFabEntity entity = Entity;
        Entity = null!;
        return entity;
    }

    /// <summary>Disposes the entity unless it was detached.</summary>
    public void Dispose() => Entity?.Dispose();
}

/// <summary>
/// The outcome of a PlayFab server login: the entity token response plus the login payload.
/// </summary>
/// <remarks>
/// Server logins authenticate with a developer secret key and hand back a token rather than an
/// entity handle, so there is nothing to dispose.
/// </remarks>
public sealed class PlayFabServerLoginResult
{
    internal PlayFabServerLoginResult(
        AuthenticationEntityTokenResponse entityToken,
        AuthenticationLoginResult result)
    {
        EntityToken = entityToken;
        Result = result;
    }

    /// <summary>The entity token the server may use for subsequent calls.</summary>
    public AuthenticationEntityTokenResponse EntityToken { get; }

    /// <summary>The login payload PlayFab returned.</summary>
    public AuthenticationLoginResult Result { get; }
}

/// <summary>
/// The outcome of authenticating a game server entity, which also reports whether the entity was
/// created by this call.
/// </summary>
public sealed class PlayFabGameServerLoginResult : IDisposable
{
    internal PlayFabGameServerLoginResult(PlayFabEntity entity, bool newlyCreated)
    {
        Entity = entity;
        NewlyCreated = newlyCreated;
    }

    /// <summary>The authenticated entity. Owned by this instance until <see cref="Detach"/> is called.</summary>
    public PlayFabEntity Entity { get; private set; }

    /// <summary>Whether the entity was created by this call rather than looked up.</summary>
    public bool NewlyCreated { get; }

    /// <summary>Transfers ownership of <see cref="Entity"/> to the caller.</summary>
    public PlayFabEntity Detach()
    {
        PlayFabEntity entity = Entity;
        Entity = null!;
        return entity;
    }

    /// <summary>Disposes the entity unless it was detached.</summary>
    public void Dispose() => Entity?.Dispose();
}
