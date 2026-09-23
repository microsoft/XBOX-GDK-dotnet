using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// Turns the native tagged union in <c>PartyXboxLive_c.h</c> into the public record hierarchy,
/// snapshotting every value while the batch is still open.
/// </summary>
internal static unsafe class PartyXblStateChangeReader
{
    internal static PartyXblStateChange Read(
        PartyXblManager owner, PARTY_XBL_STATE_CHANGE* change, List<PartyXblChatUser> retired)
    {
        switch ((PARTY_XBL_STATE_CHANGE_TYPE)change->StateChangeType)
        {
            case PARTY_XBL_STATE_CHANGE_TYPE.CreateLocalChatUserCompleted:
            {
                var typed = (PARTY_XBL_CREATE_LOCAL_CHAT_USER_COMPLETED_STATE_CHANGE*)change;
                PartyXblChatUser? chatUser = owner.Find(typed->LocalChatUser);
                if (typed->Result != PARTY_XBL_STATE_CHANGE_RESULT.Succeeded && chatUser is not null)
                {
                    retired.Add(chatUser);
                }

                return new PartyXblCreateLocalChatUserCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyXblStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    chatUser);
            }

            case PARTY_XBL_STATE_CHANGE_TYPE.LoginToPlayfabCompleted:
            {
                var typed = (PARTY_XBL_LOGIN_TO_PLAYFAB_COMPLETED_STATE_CHANGE*)change;
                return new PartyXblLoginToPlayFabCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyXblStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.Find(typed->LocalChatUser),
                    Utf8.ToString(typed->EntityId),
                    Utf8.ToString(typed->TitlePlayerEntityToken),
                    PlayFabTime.ToDateTimeOffset(typed->ExpirationTime));
            }

            case PARTY_XBL_STATE_CHANGE_TYPE.GetEntityIdsFromXboxLiveUserIdsCompleted:
            {
                var typed =
                    (PARTY_XBL_GET_ENTITY_IDS_FROM_XBOX_LIVE_USER_IDS_COMPLETED_STATE_CHANGE*)
                        change;
                return new PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyXblStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    Utf8.ToString(typed->XboxLiveSandbox),
                    owner.Find(typed->LocalChatUser),
                    ReadMappings(typed->EntityIdMappings, typed->EntityIdMappingCount));
            }

            case PARTY_XBL_STATE_CHANGE_TYPE.LocalChatUserDestroyed:
            {
                var typed = (PARTY_XBL_LOCAL_CHAT_USER_DESTROYED_STATE_CHANGE*)change;
                PartyXblChatUser? chatUser = owner.Find(typed->LocalChatUser);
                if (chatUser is not null)
                {
                    retired.Add(chatUser);
                }

                return new PartyXblLocalChatUserDestroyed(
                    chatUser,
                    (PartyXblLocalChatUserDestroyedReason)typed->Reason,
                    typed->ErrorDetail);
            }

            case PARTY_XBL_STATE_CHANGE_TYPE.RequiredChatPermissionInfoChanged:
            {
                var typed = (PARTY_XBL_REQUIRED_CHAT_PERMISSION_INFO_CHANGED_STATE_CHANGE*)change;
                return new PartyXblRequiredChatPermissionInfoChanged(
                    owner.Find(typed->LocalChatUser), owner.Find(typed->TargetChatUser));
            }

            case PARTY_XBL_STATE_CHANGE_TYPE.TokenAndSignatureRequested:
            {
                var typed = (PARTY_XBL_TOKEN_AND_SIGNATURE_REQUESTED_STATE_CHANGE*)change;
                return new PartyXblTokenAndSignatureRequested(
                    typed->CorrelationId,
                    Utf8.ToString(typed->Method),
                    Utf8.ToString(typed->Url),
                    ReadHeaders(typed->Headers, typed->HeaderCount),
                    PartyInterop.ReadBuffer(typed->Body, typed->BodySize),
                    PartyInterop.ToBool(typed->ForceRefresh),
                    PartyInterop.ToBool(typed->AllUsers),
                    owner.Find(typed->LocalChatUser));
            }

            default:
                throw new NotSupportedException(
                    $"Unknown PartyXblStateChangeType value {(uint)change->StateChangeType}.");
        }
    }

    private static IReadOnlyList<PartyXblEntityIdMapping> ReadMappings(
        PARTY_XBL_XBOX_USER_ID_TO_PLAYFAB_ENTITY_ID_MAPPING* mappings, uint count)
    {
        if (mappings is null || count == 0)
        {
            return Array.Empty<PartyXblEntityIdMapping>();
        }

        var result = new PartyXblEntityIdMapping[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = new PartyXblEntityIdMapping(
                mappings[i].XboxLiveUserId, Utf8.ToString(mappings[i].PlayfabEntityId));
        }

        return result;
    }

    private static IReadOnlyList<PartyXblHttpHeader> ReadHeaders(
        PARTY_XBL_HTTP_HEADER* headers, uint count)
    {
        if (headers is null || count == 0)
        {
            return Array.Empty<PartyXblHttpHeader>();
        }

        var result = new PartyXblHttpHeader[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = new PartyXblHttpHeader(
                Utf8.ToString(headers[i].Name) ?? string.Empty,
                Utf8.ToString(headers[i].Value) ?? string.Empty);
        }

        return result;
    }
}
