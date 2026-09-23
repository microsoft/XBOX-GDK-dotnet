using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// Turns the native tagged unions in <c>PFLobby.h</c> and <c>PFMatchmaking.h</c> into the public
/// record hierarchies, snapshotting every value while the batch is still open.
/// </summary>
internal static unsafe class LobbyStateChangeReader
{
    internal static LobbyStateChange Read(
        PlayFabMultiplayer owner, PFLobbyStateChange* change, List<Lobby> retired)
    {
        switch (change->StateChangeType)
        {
            case PFLobbyStateChangeType.CreateAndJoinLobbyCompleted:
            {
                var typed = (PFLobbyCreateAndJoinLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new CreateAndJoinLobbyCompleted(
                    lobby, PlayFabMultiplayer.FromContext(typed->AsyncContext), typed->Result);
            }

            case PFLobbyStateChangeType.JoinLobbyCompleted:
            {
                var typed = (PFLobbyJoinLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new JoinLobbyCompleted(
                    lobby,
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->NewMember));
            }

            case PFLobbyStateChangeType.ConnectToLobbyCompleted:
            {
                var typed = (PFLobbyConnectToLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new ConnectToLobbyCompleted(
                    lobby,
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->NewMember),
                    Utf8.ToString(typed->LobbyId));
            }

            case PFLobbyStateChangeType.MemberAdded:
            {
                var typed = (PFLobbyMemberAddedStateChange*)change;
                return new LobbyMemberAdded(
                    owner.Find(typed->Lobby), EntityKey.FromNative(&typed->Member));
            }

            case PFLobbyStateChangeType.AddMemberCompleted:
            {
                var typed = (PFLobbyAddMemberCompletedStateChange*)change;
                return new AddMemberCompleted(
                    owner.Find(typed->Lobby),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->LocalUser));
            }

            case PFLobbyStateChangeType.MemberRemoved:
            {
                var typed = (PFLobbyMemberRemovedStateChange*)change;
                return new LobbyMemberRemoved(
                    owner.Find(typed->Lobby),
                    EntityKey.FromNative(&typed->Member),
                    (LobbyMemberRemovedReason)typed->Reason);
            }

            case PFLobbyStateChangeType.ForceRemoveMemberCompleted:
            {
                var typed = (PFLobbyForceRemoveMemberCompletedStateChange*)change;
                return new ForceRemoveMemberCompleted(
                    owner.Find(typed->Lobby),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->TargetMember));
            }

            case PFLobbyStateChangeType.LeaveLobbyCompleted:
            {
                var typed = (PFLobbyLeaveLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                Retire(retired, lobby);
                return new LeaveLobbyCompleted(
                    lobby,
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->LocalUser is null ? null : EntityKey.FromNative(typed->LocalUser));
            }

            case PFLobbyStateChangeType.Updated:
            {
                var typed = (PFLobbyUpdatedStateChange*)change;
                var memberUpdates = new List<LobbyMemberUpdateSummary>((int)typed->MemberUpdateCount);
                for (uint i = 0; i < typed->MemberUpdateCount; i++)
                {
                    memberUpdates.Add(LobbyMemberUpdateSummary.FromNative(&typed->MemberUpdates[i]));
                }

                return new LobbyUpdated(
                    owner.Find(typed->Lobby),
                    typed->OwnerUpdated != 0,
                    typed->MaxMembersUpdated != 0,
                    typed->AccessPolicyUpdated != 0,
                    typed->MembershipLockUpdated != 0,
                    typed->RestrictInvitesToLobbyOwnerUpdated != 0,
                    typed->ServerUpdated != 0,
                    typed->ServerConnectionStatusUpdated != 0,
                    MultiplayerInterop.ReadStrings(
                        typed->UpdatedSearchPropertyKeys, typed->UpdatedSearchPropertyCount),
                    MultiplayerInterop.ReadStrings(
                        typed->UpdatedLobbyPropertyKeys, typed->UpdatedLobbyPropertyCount),
                    MultiplayerInterop.ReadStrings(
                        typed->UpdatedServerPropertyKeys, typed->UpdatedServerPropertyCount),
                    memberUpdates);
            }

            case PFLobbyStateChangeType.PostUpdateCompleted:
            {
                var typed = (PFLobbyPostUpdateCompletedStateChange*)change;
                return new PostUpdateCompleted(
                    owner.Find(typed->Lobby),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->LocalUser));
            }

            case PFLobbyStateChangeType.Disconnecting:
            {
                var typed = (PFLobbyDisconnectingStateChange*)change;
                return new LobbyDisconnecting(
                    owner.Find(typed->Lobby), (LobbyDisconnectingReason)typed->Reason);
            }

            case PFLobbyStateChangeType.Disconnected:
            {
                var typed = (PFLobbyDisconnectedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                Retire(retired, lobby);
                return new LobbyDisconnected(lobby);
            }

            case PFLobbyStateChangeType.JoinArrangedLobbyCompleted:
            {
                var typed = (PFLobbyJoinArrangedLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new JoinArrangedLobbyCompleted(
                    lobby,
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->NewMember));
            }

            case PFLobbyStateChangeType.FindLobbiesCompleted:
            {
                var typed = (PFLobbyFindLobbiesCompletedStateChange*)change;
                var results = new List<LobbySearchResult>((int)typed->SearchResultCount);
                for (uint i = 0; i < typed->SearchResultCount; i++)
                {
                    results.Add(LobbySearchResult.FromNative(&typed->SearchResults[i]));
                }

                return new FindLobbiesCompleted(
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->SearchingEntity),
                    results);
            }

            case PFLobbyStateChangeType.InviteReceived:
            {
                var typed = (PFLobbyInviteReceivedStateChange*)change;
                return new LobbyInviteReceived(
                    EntityKey.FromNative(&typed->ListeningEntity),
                    EntityKey.FromNative(&typed->InvitingEntity),
                    Utf8.ToString(typed->ConnectionString),
                    Utf8.ToString(typed->LobbyId));
            }

            case PFLobbyStateChangeType.InviteListenerStatusChanged:
            {
                var typed = (PFLobbyInviteListenerStatusChangedStateChange*)change;
                EntityKey listening = EntityKey.FromNative(&typed->ListeningEntity);
                return new LobbyInviteListenerStatusChanged(
                    listening, owner.GetLobbyInviteListenerStatus(listening));
            }

            case PFLobbyStateChangeType.SendInviteCompleted:
            {
                var typed = (PFLobbySendInviteCompletedStateChange*)change;
                return new SendInviteCompleted(
                    owner.Find(typed->Lobby),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->Sender),
                    EntityKey.FromNative(&typed->Invitee));
            }

            case PFLobbyStateChangeType.CreateAndClaimServerLobbyCompleted:
            {
                var typed = (PFLobbyCreateAndClaimServerLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new CreateAndClaimServerLobbyCompleted(
                    lobby, PlayFabMultiplayer.FromContext(typed->AsyncContext), typed->Result);
            }

            case PFLobbyStateChangeType.ClaimServerLobbyCompleted:
            {
                var typed = (PFLobbyClaimServerLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new ClaimServerLobbyCompleted(
                    lobby,
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    Utf8.ToString(typed->LobbyId));
            }

            case PFLobbyStateChangeType.JoinLobbyAsServerCompleted:
            {
                var typed = (PFLobbyJoinLobbyAsServerCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                RetireOnFailure(retired, lobby, typed->Result);
                return new JoinLobbyAsServerCompleted(
                    lobby,
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result,
                    EntityKey.FromNative(&typed->NewServer));
            }

            case PFLobbyStateChangeType.ServerPostUpdateCompleted:
            {
                var typed = (PFLobbyServerPostUpdateCompletedStateChange*)change;
                return new ServerPostUpdateCompleted(
                    owner.Find(typed->Lobby),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result);
            }

            case PFLobbyStateChangeType.ServerPostUpdateAsServerCompleted:
            {
                var typed = (PFLobbyServerPostUpdateAsServerCompletedStateChange*)change;
                return new ServerPostUpdateAsServerCompleted(
                    owner.Find(typed->Lobby),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result);
            }

            case PFLobbyStateChangeType.ServerLeaveLobbyAsServerCompleted:
            {
                var typed = (PFLobbyServerLeaveLobbyAsServerCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                Retire(retired, lobby);
                return new ServerLeaveLobbyAsServerCompleted(
                    lobby, PlayFabMultiplayer.FromContext(typed->AsyncContext));
            }

            case PFLobbyStateChangeType.ServerDeleteLobbyCompleted:
            {
                var typed = (PFLobbyServerDeleteLobbyCompletedStateChange*)change;
                Lobby? lobby = owner.Find(typed->Lobby);
                Retire(retired, lobby);
                return new ServerDeleteLobbyCompleted(
                    lobby, PlayFabMultiplayer.FromContext(typed->AsyncContext));
            }

            default:
                throw new NotSupportedException(
                    $"Unknown PFLobbyStateChangeType value {(uint)change->StateChangeType}.");
        }
    }

    internal static MatchmakingStateChange Read(
        PlayFabMultiplayer owner, PFMatchmakingStateChange* change)
    {
        switch (change->StateChangeType)
        {
            case PFMatchmakingStateChangeType.TicketStatusChanged:
            {
                var typed = (PFMatchmakingTicketStatusChangedStateChange*)change;
                return new MatchmakingTicketStatusChanged(owner.TrackTicket(typed->Ticket));
            }

            case PFMatchmakingStateChangeType.TicketCompleted:
            {
                var typed = (PFMatchmakingTicketCompletedStateChange*)change;
                return new MatchmakingTicketCompleted(
                    owner.TrackTicket(typed->Ticket),
                    PlayFabMultiplayer.FromContext(typed->AsyncContext),
                    typed->Result);
            }

            default:
                throw new NotSupportedException(
                    $"Unknown PFMatchmakingStateChangeType value {(uint)change->StateChangeType}.");
        }
    }

    private static void Retire(List<Lobby> retired, Lobby? lobby)
    {
        if (lobby is not null)
        {
            retired.Add(lobby);
        }
    }

    private static void RetireOnFailure(List<Lobby> retired, Lobby? lobby, int resultCode)
    {
        if (HResult.Failed(resultCode))
        {
            Retire(retired, lobby);
        }
    }
}
