using System;
using System.Collections.Generic;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// One entry from a <see cref="PartyXblManager.ProcessStateChanges"/> batch.
/// </summary>
/// <remarks>
/// Every record snapshots its values, so it stays valid after the batch is returned to the
/// extension; the chat users it references are invalidated when their destruction change is
/// processed.
/// </remarks>
/// <param name="Kind">The discriminator matching the native <c>PartyXblStateChangeType</c>.</param>
public abstract record PartyXblStateChange(PartyXblStateChangeType Kind);

/// <summary>An Xbox Live extension state change that completes an operation.</summary>
/// <param name="Kind">The discriminator matching the native <c>PartyXblStateChangeType</c>.</param>
/// <param name="Operation">The id returned by the matching start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the operation failed.</param>
public abstract record PartyXblOperationCompleted(
    PartyXblStateChangeType Kind,
    PartyOperationId Operation,
    PartyXblStateChangeResult Result,
    uint ErrorDetail)
    : PartyXblStateChange(Kind)
{
    /// <summary>Whether the operation succeeded.</summary>
    public bool Succeeded => Result == PartyXblStateChangeResult.Succeeded;

    /// <summary>The failure, or <see langword="null"/> when the operation succeeded.</summary>
    public Exception? Error => Succeeded
        ? null
        : new PartyException(ErrorDetail, PartyInterop.DescribeXbl(ErrorDetail));
}

/// <summary>A <see cref="PartyXblManager.CreateLocalChatUser"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="LocalChatUser">The chat user that was created.</param>
public sealed record PartyXblCreateLocalChatUserCompleted(
    PartyOperationId Operation,
    PartyXblStateChangeResult Result,
    uint ErrorDetail,
    PartyXblChatUser? LocalChatUser)
    : PartyXblOperationCompleted(
        PartyXblStateChangeType.CreateLocalChatUserCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyXblChatUser.LoginToPlayFab"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="LocalChatUser">The chat user that logged in.</param>
/// <param name="EntityId">The PlayFab entity id the Xbox Live user maps to.</param>
/// <param name="TitlePlayerEntityToken">The entity token to pass to PlayFab services.</param>
/// <param name="ExpirationTime">When the token expires.</param>
public sealed record PartyXblLoginToPlayFabCompleted(
    PartyOperationId Operation,
    PartyXblStateChangeResult Result,
    uint ErrorDetail,
    PartyXblChatUser? LocalChatUser,
    string? EntityId,
    string? TitlePlayerEntityToken,
    DateTimeOffset ExpirationTime)
    : PartyXblOperationCompleted(
        PartyXblStateChangeType.LoginToPlayfabCompleted, Operation, Result, ErrorDetail);

/// <summary>
/// A <see cref="PartyXblManager.GetEntityIdsFromXboxLiveUserIds"/> call completed.
/// </summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="XboxLiveSandbox">The sandbox the lookup was performed in.</param>
/// <param name="LocalChatUser">The chat user that authorized the lookup.</param>
/// <param name="Mappings">The resolved Xbox Live user id to PlayFab entity id mappings.</param>
public sealed record PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted(
    PartyOperationId Operation,
    PartyXblStateChangeResult Result,
    uint ErrorDetail,
    string? XboxLiveSandbox,
    PartyXblChatUser? LocalChatUser,
    IReadOnlyList<PartyXblEntityIdMapping> Mappings)
    : PartyXblOperationCompleted(
        PartyXblStateChangeType.GetEntityIdsFromXboxLiveUserIdsCompleted,
        Operation,
        Result,
        ErrorDetail);

/// <summary>A local chat user was destroyed by the extension.</summary>
/// <param name="LocalChatUser">The chat user that was destroyed.</param>
/// <param name="Reason">Why it was destroyed.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail behind the reason.</param>
public sealed record PartyXblLocalChatUserDestroyed(
    PartyXblChatUser? LocalChatUser,
    PartyXblLocalChatUserDestroyedReason Reason,
    uint ErrorDetail)
    : PartyXblStateChange(PartyXblStateChangeType.LocalChatUserDestroyed);

/// <summary>
/// The chat permissions between two users changed. Query the new value with
/// <see cref="PartyXblChatUser.GetRequiredChatPermissionInfo"/>.
/// </summary>
/// <param name="LocalChatUser">The local user whose permissions changed.</param>
/// <param name="TargetChatUser">The user the permissions apply to.</param>
public sealed record PartyXblRequiredChatPermissionInfoChanged(
    PartyXblChatUser? LocalChatUser, PartyXblChatUser? TargetChatUser)
    : PartyXblStateChange(PartyXblStateChangeType.RequiredChatPermissionInfoChanged);

/// <summary>
/// The extension needs an Xbox Live token and signature for an HTTP request. The title must answer
/// with <see cref="PartyXblManager.CompleteGetTokenAndSignatureRequest"/>.
/// </summary>
/// <param name="CorrelationId">The id to echo back when answering.</param>
/// <param name="Method">The HTTP method of the request to sign.</param>
/// <param name="Url">The URL of the request to sign.</param>
/// <param name="Headers">The headers of the request to sign.</param>
/// <param name="Body">The body of the request to sign.</param>
/// <param name="ForceRefresh">Whether a cached token must not be used.</param>
/// <param name="AllUsers">Whether the token must cover every signed-in user.</param>
/// <param name="LocalChatUser">
/// The user the token is for, or <see langword="null"/> when <paramref name="AllUsers"/> is set.
/// </param>
public sealed record PartyXblTokenAndSignatureRequested(
    uint CorrelationId,
    string? Method,
    string? Url,
    IReadOnlyList<PartyXblHttpHeader> Headers,
    byte[] Body,
    bool ForceRefresh,
    bool AllUsers,
    PartyXblChatUser? LocalChatUser)
    : PartyXblStateChange(PartyXblStateChangeType.TokenAndSignatureRequested);
