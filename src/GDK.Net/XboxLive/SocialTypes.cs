using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Which relationships a social query returns. Mirrors <c>XblSocialRelationshipFilter</c>.</summary>
public enum SocialRelationshipFilter : uint
{
    /// <summary>All people on the user's people list.</summary>
    All = 0,

    /// <summary>Only people marked as favorites.</summary>
    Favorite = 1,

    /// <summary>Only legacy Xbox Live friends.</summary>
    LegacyXboxLiveFriends = 2,
}

/// <summary>Kind of reputation feedback to submit. Mirrors <c>XblReputationFeedbackType</c>.</summary>
public enum ReputationFeedbackType : uint
{
    /// <summary>The player killed a teammate.</summary>
    FairPlayKillsTeammates = 0,

    /// <summary>The player cheated.</summary>
    FairPlayCheater = 1,

    /// <summary>The player tampered with on-disk content.</summary>
    FairPlayTampering = 2,

    /// <summary>The player quit a game early.</summary>
    FairPlayQuitter = 3,

    /// <summary>The player was kicked or voted out.</summary>
    FairPlayKicked = 4,

    /// <summary>The player used inappropriate video communications.</summary>
    CommunicationsInappropriateVideo = 5,

    /// <summary>The player used abusive voice communications.</summary>
    CommunicationsAbusiveVoice = 6,

    /// <summary>The player contributed inappropriate user-generated content.</summary>
    InappropriateUserGeneratedContent = 7,

    /// <summary>The player was a skilled player.</summary>
    PositiveSkilledPlayer = 8,

    /// <summary>The player was helpful.</summary>
    PositiveHelpfulPlayer = 9,

    /// <summary>The player contributed high-quality user-generated content.</summary>
    PositiveHighQualityUserGeneratedContent = 10,

    /// <summary>The player sent a phishing communication.</summary>
    CommsPhishing = 11,

    /// <summary>The player sent an inappropriate picture message.</summary>
    CommsPictureMessage = 12,

    /// <summary>The player sent spam.</summary>
    CommsSpam = 13,

    /// <summary>The player sent an inappropriate text message.</summary>
    CommsTextMessage = 14,

    /// <summary>The player sent an inappropriate voice message.</summary>
    CommsVoiceMessage = 15,

    /// <summary>A console ban is requested.</summary>
    FairPlayConsoleBanRequest = 16,

    /// <summary>The player intentionally idled.</summary>
    FairPlayIdler = 17,

    /// <summary>A user ban is requested.</summary>
    FairPlayUserBanRequest = 18,

    /// <summary>The player used an inappropriate gamerpic.</summary>
    UserContentGamerpic = 19,

    /// <summary>The player used inappropriate biography or personal information.</summary>
    UserContentPersonalInfo = 20,

    /// <summary>The player behaved unsportingly.</summary>
    FairPlayUnsporting = 21,

    /// <summary>The player cheated on a leaderboard.</summary>
    FairPlayLeaderboardCheater = 22,
}

/// <summary>Kind of social relationship change. Mirrors <c>XblSocialNotificationType</c>.</summary>
public enum SocialNotificationType : uint
{
    /// <summary>The notification kind is unknown.</summary>
    Unknown = 0,

    /// <summary>Users were added.</summary>
    Added = 1,

    /// <summary>User data changed.</summary>
    Changed = 2,

    /// <summary>Users were removed.</summary>
    Removed = 3,

    /// <summary>The number of pending incoming friend requests changed.</summary>
    IncomingFriendRequestCountChanged = 4,
}

/// <summary>
/// Identifies an MPSD session related to reputation feedback. Mirrors
/// <c>XblMultiplayerSessionReference</c>.
/// </summary>
public sealed class SocialMultiplayerSessionReference
{
    /// <summary>
    /// Creates a session reference from its service configuration, template and session names.
    /// </summary>
    /// <param name="serviceConfigurationId">The case-sensitive service configuration id.</param>
    /// <param name="sessionTemplateName">The multiplayer session template name.</param>
    /// <param name="sessionName">The multiplayer session name.</param>
    public SocialMultiplayerSessionReference(
        string serviceConfigurationId,
        string sessionTemplateName,
        string sessionName)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        if (sessionTemplateName is null)
        {
            throw new ArgumentNullException(nameof(sessionTemplateName));
        }

        if (sessionName is null)
        {
            throw new ArgumentNullException(nameof(sessionName));
        }

        EnsureFits(serviceConfigurationId, XblMultiplayerSessionReference.ScidLength, nameof(serviceConfigurationId));
        EnsureFits(
            sessionTemplateName,
            XblMultiplayerSessionReference.SessionTemplateNameMaxLength,
            nameof(sessionTemplateName));
        EnsureFits(sessionName, XblMultiplayerSessionReference.SessionNameMaxLength, nameof(sessionName));

        ServiceConfigurationId = serviceConfigurationId;
        SessionTemplateName = sessionTemplateName;
        SessionName = sessionName;
    }

    /// <summary>The case-sensitive service configuration id.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>The multiplayer session template name.</summary>
    public string SessionTemplateName { get; }

    /// <summary>The multiplayer session name.</summary>
    public string SessionName { get; }

    internal unsafe XblMultiplayerSessionReference ToNative()
    {
        var native = default(XblMultiplayerSessionReference);

        byte* scid = native.Scid;
        byte* templateName = native.SessionTemplateName;
        byte* sessionName = native.SessionName;

        WriteFixedUtf8(ServiceConfigurationId, scid, XblMultiplayerSessionReference.ScidLength);
        WriteFixedUtf8(SessionTemplateName, templateName, XblMultiplayerSessionReference.SessionTemplateNameMaxLength);
        WriteFixedUtf8(SessionName, sessionName, XblMultiplayerSessionReference.SessionNameMaxLength);

        return native;
    }

    private static void EnsureFits(string value, int capacity, string paramName)
    {
        if (Encoding.UTF8.GetByteCount(value) >= capacity)
        {
            throw new ArgumentException(
                $"The UTF-8 value must be shorter than {capacity} bytes including the null terminator.",
                paramName);
        }
    }

    private static unsafe void WriteFixedUtf8(string value, byte* destination, int capacity)
    {
        int byteCount = Encoding.UTF8.GetByteCount(value);
        fixed (char* chars = value)
        {
            Encoding.UTF8.GetBytes(chars, value.Length, destination, byteCount);
        }

        destination[byteCount] = 0;
    }
}

/// <summary>One reputation feedback item for batch submission. Mirrors <c>XblReputationFeedbackItem</c>.</summary>
public sealed class ReputationFeedbackItem
{
    /// <summary>Creates one feedback item.</summary>
    /// <param name="xboxUserId">The Xbox user id to submit feedback about.</param>
    /// <param name="feedbackType">The kind of feedback to submit.</param>
    /// <param name="sessionReference">Optional MPSD session the feedback relates to.</param>
    /// <param name="reasonMessage">Optional user-supplied explanation.</param>
    /// <param name="evidenceResourceId">Optional resource id for supporting evidence.</param>
    public ReputationFeedbackItem(
        ulong xboxUserId,
        ReputationFeedbackType feedbackType,
        SocialMultiplayerSessionReference? sessionReference = null,
        string? reasonMessage = null,
        string? evidenceResourceId = null)
    {
        XboxUserId = xboxUserId;
        FeedbackType = feedbackType;
        SessionReference = sessionReference;
        ReasonMessage = reasonMessage ?? string.Empty;
        EvidenceResourceId = evidenceResourceId;
    }

    /// <summary>The Xbox user id to submit feedback about.</summary>
    public ulong XboxUserId { get; }

    /// <summary>The kind of feedback to submit.</summary>
    public ReputationFeedbackType FeedbackType { get; }

    /// <summary>The optional MPSD session the feedback relates to.</summary>
    public SocialMultiplayerSessionReference? SessionReference { get; }

    /// <summary>User-supplied explanation text. Empty when no reason was supplied.</summary>
    public string ReasonMessage { get; }

    /// <summary>Optional resource id for supporting evidence.</summary>
    public string? EvidenceResourceId { get; }
}

/// <summary>Represents the relationship between the signed-in user and another Xbox user.</summary>
public sealed class SocialRelationship
{
    internal SocialRelationship(
        ulong xboxUserId,
        bool isFavorite,
        bool isFriend,
        bool isFollowingCaller,
        IReadOnlyList<string> socialNetworks)
    {
        XboxUserId = xboxUserId;
        IsFavorite = isFavorite;
        IsFriend = isFriend;
        IsFollowingCaller = isFollowingCaller;
        SocialNetworks = socialNetworks;
    }

    /// <summary>The related person's Xbox user id.</summary>
    public ulong XboxUserId { get; }

    /// <summary>Whether this person is marked as a favorite.</summary>
    public bool IsFavorite { get; }

    /// <summary>Whether there is a mutual follower/following relationship with this person.</summary>
    public bool IsFriend { get; }

    /// <summary>
    /// Compatibility field derived by XSAPI from <see cref="IsFriend"/> rather than a distinct
    /// following relationship.
    /// </summary>
    public bool IsFollowingCaller { get; }

    /// <summary>The social networks on which this relationship exists.</summary>
    public IReadOnlyList<string> SocialNetworks { get; }

    internal static unsafe SocialRelationship FromNative(XblSocialRelationship* native)
    {
        var networks = new string[(int)native->SocialNetworksCount];
        for (int i = 0; i < networks.Length; i++)
        {
            networks[i] = Utf8.ToString(native->SocialNetworks[i]) ?? string.Empty;
        }

        return new SocialRelationship(
            native->XboxUserId,
            native->IsFavorite != 0,
            native->IsFriend != 0,
            native->IsFollowingCaller != 0,
            new ReadOnlyCollection<string>(networks));
    }
}

/// <summary>Payload for <see cref="SocialService.RelationshipChanged"/>.</summary>
public sealed class SocialRelationshipChangedEventArgs : EventArgs
{
    internal SocialRelationshipChangedEventArgs(
        ulong callerXboxUserId,
        SocialNotificationType notification,
        IReadOnlyList<ulong> xboxUserIds)
    {
        CallerXboxUserId = callerXboxUserId;
        Notification = notification;
        XboxUserIds = xboxUserIds;
    }

    /// <summary>The Xbox user id whose social graph changed.</summary>
    public ulong CallerXboxUserId { get; }

    /// <summary>The kind of relationship change.</summary>
    public SocialNotificationType Notification { get; }

    /// <summary>The Xbox user ids affected by the change.</summary>
    public IReadOnlyList<ulong> XboxUserIds { get; }
}
