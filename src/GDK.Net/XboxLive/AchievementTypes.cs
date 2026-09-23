using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Kind of achievement. Mirrors <c>XblAchievementType</c>.</summary>
public enum AchievementType : uint
{
    /// <summary>The type is unknown.</summary>
    Unknown = 0,

    /// <summary>Matches every type. Only meaningful as a query filter.</summary>
    All = 1,

    /// <summary>Unlockable at any time; may award gamerscore.</summary>
    Persistent = 2,

    /// <summary>Unlockable only within a time window; never awards gamerscore.</summary>
    Challenge = 3,
}

/// <summary>Sort order for an achievement query. Mirrors <c>XblAchievementOrderBy</c>.</summary>
public enum AchievementOrderBy : uint
{
    /// <summary>No guaranteed order.</summary>
    Default = 0,

    /// <summary>Ordered by title id.</summary>
    TitleId = 1,

    /// <summary>Ordered by unlock time.</summary>
    UnlockTime = 2,
}

/// <summary>
/// A player's progress towards an achievement. Mirrors <c>XblAchievementProgressState</c>.
/// </summary>
public enum AchievementProgressState : uint
{
    /// <summary>Progress is unknown.</summary>
    Unknown = 0,

    /// <summary>The achievement has been earned.</summary>
    Achieved = 1,

    /// <summary>Progress has not started.</summary>
    NotStarted = 2,

    /// <summary>Progress has started but the achievement is not yet earned.</summary>
    InProgress = 3,
}

/// <summary>Kind of media asset. Mirrors <c>XblAchievementMediaAssetType</c>.</summary>
public enum AchievementMediaAssetType : uint
{
    /// <summary>The asset type is unknown.</summary>
    Unknown = 0,

    /// <summary>An icon.</summary>
    Icon = 1,

    /// <summary>Artwork.</summary>
    Art = 2,
}

/// <summary>How an achievement is earned. Mirrors <c>XblAchievementParticipationType</c>.</summary>
public enum AchievementParticipationType : uint
{
    /// <summary>The participation type is unknown.</summary>
    Unknown = 0,

    /// <summary>Earned as an individual.</summary>
    Individual = 1,

    /// <summary>Earned as part of a group.</summary>
    Group = 2,
}

/// <summary>Kind of reward. Mirrors <c>XblAchievementRewardType</c>.</summary>
public enum AchievementRewardType : uint
{
    /// <summary>The reward type is unknown.</summary>
    Unknown = 0,

    /// <summary>Gamerscore.</summary>
    Gamerscore = 1,

    /// <summary>An in-app reward the title defines and delivers.</summary>
    InApp = 2,

    /// <summary>Digital art.</summary>
    Art = 3,
}

/// <summary>How rare an achievement is. Mirrors <c>XblAchievementRarityCategory</c>.</summary>
public enum AchievementRarityCategory : uint
{
    /// <summary>Rarity cannot be calculated yet.</summary>
    Unset = 0,

    /// <summary>Unlocked by 0 – 10.9% of players.</summary>
    Rare = 1,

    /// <summary>Unlocked by 11.0 – 100.0% of players.</summary>
    Common = 2,
}

/// <summary>An achievement's association with a title. Mirrors <c>XblAchievementTitleAssociation</c>.</summary>
public sealed class AchievementTitleAssociation
{
    internal AchievementTitleAssociation(string name, uint titleId)
    {
        Name = name;
        TitleId = titleId;
    }

    /// <summary>The title's name.</summary>
    public string Name { get; }

    /// <summary>The title id.</summary>
    public uint TitleId { get; }
}

/// <summary>
/// One requirement that makes up an achievement's progression. Mirrors
/// <c>XblAchievementRequirement</c>.
/// </summary>
/// <remarks>
/// The service reports progress values as strings because a requirement can be counted in any unit
/// the title chose. They are surfaced unparsed rather than guessed at.
/// </remarks>
public sealed class AchievementRequirement
{
    internal AchievementRequirement(string id, string currentProgressValue, string targetProgressValue)
    {
        Id = id;
        CurrentProgressValue = currentProgressValue;
        TargetProgressValue = targetProgressValue;
    }

    /// <summary>The requirement's id.</summary>
    public string Id { get; }

    /// <summary>Progress so far, as the service formats it. Empty when the achievement is locked.</summary>
    public string CurrentProgressValue { get; }

    /// <summary>The value that satisfies the requirement, as the service formats it.</summary>
    public string TargetProgressValue { get; }
}

/// <summary>Progress towards an achievement. Mirrors <c>XblAchievementProgression</c>.</summary>
public sealed class AchievementProgression
{
    internal AchievementProgression(
        IReadOnlyList<AchievementRequirement> requirements,
        DateTimeOffset? timeUnlocked)
    {
        Requirements = requirements;
        TimeUnlocked = timeUnlocked;
    }

    /// <summary>The requirements that make up this achievement.</summary>
    public IReadOnlyList<AchievementRequirement> Requirements { get; }

    /// <summary>
    /// When the achievement was unlocked, or <see langword="null"/> when it has not been. The
    /// native field is a <c>time_t</c> of 0 in that case, which is a real instant rather than an
    /// absent one, so it is projected as null.
    /// </summary>
    public DateTimeOffset? TimeUnlocked { get; }
}

/// <summary>The window a challenge achievement is available in. Mirrors <c>XblAchievementTimeWindow</c>.</summary>
public readonly struct AchievementTimeWindow : IEquatable<AchievementTimeWindow>
{
    internal AchievementTimeWindow(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>When the achievement becomes available.</summary>
    public DateTimeOffset StartDate { get; }

    /// <summary>When the achievement stops being available.</summary>
    public DateTimeOffset EndDate { get; }

    /// <inheritdoc/>
    public bool Equals(AchievementTimeWindow other) =>
        StartDate == other.StartDate && EndDate == other.EndDate;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is AchievementTimeWindow other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => StartDate.GetHashCode() ^ EndDate.GetHashCode();

    /// <summary>Equality operator.</summary>
    public static bool operator ==(AchievementTimeWindow left, AchievementTimeWindow right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(AchievementTimeWindow left, AchievementTimeWindow right) => !left.Equals(right);
}

/// <summary>A media asset attached to an achievement. Mirrors <c>XblAchievementMediaAsset</c>.</summary>
public sealed class AchievementMediaAsset
{
    internal AchievementMediaAsset(string name, AchievementMediaAssetType mediaAssetType, string url)
    {
        Name = name;
        MediaAssetType = mediaAssetType;
        Url = url;
    }

    /// <summary>The asset's name.</summary>
    public string Name { get; }

    /// <summary>Whether the asset is an icon or artwork.</summary>
    public AchievementMediaAssetType MediaAssetType { get; }

    /// <summary>Where the asset can be fetched from.</summary>
    public string Url { get; }
}

/// <summary>A reward granted for unlocking an achievement. Mirrors <c>XblAchievementReward</c>.</summary>
public sealed class AchievementReward
{
    internal AchievementReward(
        string name,
        string description,
        string value,
        AchievementRewardType rewardType,
        string valueType,
        AchievementMediaAsset? mediaAsset)
    {
        Name = name;
        Description = description;
        Value = value;
        RewardType = rewardType;
        ValueType = valueType;
        MediaAsset = mediaAsset;
    }

    /// <summary>The reward's name.</summary>
    public string Name { get; }

    /// <summary>The reward's description.</summary>
    public string Description { get; }

    /// <summary>The reward's value, as the service formats it. Interpret with <see cref="ValueType"/>.</summary>
    public string Value { get; }

    /// <summary>Whether the reward is gamerscore, in-app or art.</summary>
    public AchievementRewardType RewardType { get; }

    /// <summary>The property type of <see cref="Value"/>.</summary>
    public string ValueType { get; }

    /// <summary>The reward's media asset, when it has one.</summary>
    public AchievementMediaAsset? MediaAsset { get; }
}

/// <summary>
/// One Xbox Live achievement. Managed snapshot of <c>XblAchievement</c>.
/// </summary>
/// <remarks>
/// The native struct is a graph of pointers into memory owned by the
/// <c>XblAchievementsResultHandle</c> it came from, and that memory dies with the handle. Every
/// achievement is therefore copied out in full while the handle is alive, so an instance stays
/// usable after the result it came from is disposed.
/// </remarks>
public sealed class Achievement
{
    internal Achievement(
        string id,
        string serviceConfigurationId,
        string name,
        IReadOnlyList<AchievementTitleAssociation> titleAssociations,
        AchievementProgressState progressState,
        AchievementProgression progression,
        IReadOnlyList<AchievementMediaAsset> mediaAssets,
        IReadOnlyList<string> platformsAvailableOn,
        bool isSecret,
        string unlockedDescription,
        string lockedDescription,
        string productId,
        AchievementType type,
        AchievementParticipationType participationType,
        AchievementTimeWindow available,
        IReadOnlyList<AchievementReward> rewards,
        TimeSpan estimatedUnlockTime,
        string deepLink,
        bool isRevoked)
    {
        Id = id;
        ServiceConfigurationId = serviceConfigurationId;
        Name = name;
        TitleAssociations = titleAssociations;
        ProgressState = progressState;
        Progression = progression;
        MediaAssets = mediaAssets;
        PlatformsAvailableOn = platformsAvailableOn;
        IsSecret = isSecret;
        UnlockedDescription = unlockedDescription;
        LockedDescription = lockedDescription;
        ProductId = productId;
        Type = type;
        ParticipationType = participationType;
        Available = available;
        Rewards = rewards;
        EstimatedUnlockTime = estimatedUnlockTime;
        DeepLink = deepLink;
        IsRevoked = isRevoked;
    }

    /// <summary>The achievement's id.</summary>
    public string Id { get; }

    /// <summary>The service configuration the achievement belongs to.</summary>
    public string ServiceConfigurationId { get; }

    /// <summary>The achievement's localized name.</summary>
    public string Name { get; }

    /// <summary>The titles this achievement is associated with.</summary>
    public IReadOnlyList<AchievementTitleAssociation> TitleAssociations { get; }

    /// <summary>The player's progress state.</summary>
    public AchievementProgressState ProgressState { get; }

    /// <summary>The player's progress detail.</summary>
    public AchievementProgression Progression { get; }

    /// <summary>Icons and artwork for the achievement.</summary>
    public IReadOnlyList<AchievementMediaAsset> MediaAssets { get; }

    /// <summary>The platforms the achievement can be unlocked on.</summary>
    public IReadOnlyList<string> PlatformsAvailableOn { get; }

    /// <summary>Whether the achievement's details are hidden until it is unlocked.</summary>
    public bool IsSecret { get; }

    /// <summary>Description shown once the achievement is unlocked.</summary>
    public string UnlockedDescription { get; }

    /// <summary>Description shown while the achievement is locked.</summary>
    public string LockedDescription { get; }

    /// <summary>The product the achievement belongs to.</summary>
    public string ProductId { get; }

    /// <summary>Whether the achievement is persistent or a challenge.</summary>
    public AchievementType Type { get; }

    /// <summary>Whether the achievement is earned individually or as a group.</summary>
    public AchievementParticipationType ParticipationType { get; }

    /// <summary>The window the achievement is available in.</summary>
    public AchievementTimeWindow Available { get; }

    /// <summary>What unlocking the achievement grants.</summary>
    public IReadOnlyList<AchievementReward> Rewards { get; }

    /// <summary>The service's estimate of how long the achievement takes to unlock.</summary>
    public TimeSpan EstimatedUnlockTime { get; }

    /// <summary>A deep link into the title that starts the relevant activity.</summary>
    public string DeepLink { get; }

    /// <summary>Whether the achievement has been revoked, for example after cheat detection.</summary>
    public bool IsRevoked { get; }

    /// <summary>
    /// <see langword="true"/> when <see cref="ProgressState"/> is
    /// <see cref="AchievementProgressState.Achieved"/> and the achievement has not been revoked.
    /// </summary>
    public bool IsUnlocked => ProgressState == AchievementProgressState.Achieved && !IsRevoked;

    /// <inheritdoc/>
    public override string ToString() => $"{Name} ({Id}): {ProgressState}";

    internal static unsafe Achievement FromNative(XblAchievement* native)
    {
        var titleAssociations = new AchievementTitleAssociation[(int)native->TitleAssociationsCount];
        for (int i = 0; i < titleAssociations.Length; i++)
        {
            XblAchievementTitleAssociation* item = native->TitleAssociations + i;
            titleAssociations[i] = new AchievementTitleAssociation(
                Utf8.ToString(item->Name) ?? string.Empty,
                item->TitleId);
        }

        var mediaAssets = new AchievementMediaAsset[(int)native->MediaAssetsCount];
        for (int i = 0; i < mediaAssets.Length; i++)
        {
            mediaAssets[i] = ReadMediaAsset(native->MediaAssets + i)!;
        }

        var platforms = new string[(int)native->PlatformsAvailableOnCount];
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = Utf8.ToString(native->PlatformsAvailableOn[i]) ?? string.Empty;
        }

        var rewards = new AchievementReward[(int)native->RewardsCount];
        for (int i = 0; i < rewards.Length; i++)
        {
            XblAchievementReward* item = native->Rewards + i;
            rewards[i] = new AchievementReward(
                Utf8.ToString(item->Name) ?? string.Empty,
                Utf8.ToString(item->Description) ?? string.Empty,
                Utf8.ToString(item->Value) ?? string.Empty,
                (AchievementRewardType)item->RewardType,
                Utf8.ToString(item->ValueType) ?? string.Empty,
                ReadMediaAsset(item->MediaAsset));
        }

        return new Achievement(
            Utf8.ToString(native->Id) ?? string.Empty,
            Utf8.ToString(native->ServiceConfigurationId) ?? string.Empty,
            Utf8.ToString(native->Name) ?? string.Empty,
            new ReadOnlyCollection<AchievementTitleAssociation>(titleAssociations),
            (AchievementProgressState)native->ProgressState,
            ReadProgression(native->Progression),
            new ReadOnlyCollection<AchievementMediaAsset>(mediaAssets),
            new ReadOnlyCollection<string>(platforms),
            native->IsSecret != 0,
            Utf8.ToString(native->UnlockedDescription) ?? string.Empty,
            Utf8.ToString(native->LockedDescription) ?? string.Empty,
            Utf8.ToString(native->ProductId) ?? string.Empty,
            (AchievementType)native->Type,
            (AchievementParticipationType)native->ParticipationType,
            new AchievementTimeWindow(
                FromUnixSeconds(native->Available.StartDate),
                FromUnixSeconds(native->Available.EndDate)),
            new ReadOnlyCollection<AchievementReward>(rewards),
            TimeSpan.FromSeconds(native->EstimatedUnlockTime),
            Utf8.ToString(native->DeepLink) ?? string.Empty,
            native->IsRevoked != 0);
    }

    internal static unsafe AchievementProgression ReadProgression(in XblAchievementProgression native)
    {
        var requirements = new AchievementRequirement[(int)native.RequirementsCount];
        for (int i = 0; i < requirements.Length; i++)
        {
            XblAchievementRequirement* item = native.Requirements + i;
            requirements[i] = new AchievementRequirement(
                Utf8.ToString(item->Id) ?? string.Empty,
                Utf8.ToString(item->CurrentProgressValue) ?? string.Empty,
                Utf8.ToString(item->TargetProgressValue) ?? string.Empty);
        }

        return new AchievementProgression(
            new ReadOnlyCollection<AchievementRequirement>(requirements),
            native.TimeUnlocked == 0 ? null : FromUnixSeconds(native.TimeUnlocked));
    }

    private static unsafe AchievementMediaAsset? ReadMediaAsset(XblAchievementMediaAsset* asset)
    {
        if (asset is null)
        {
            return null;
        }

        return new AchievementMediaAsset(
            Utf8.ToString(asset->Name) ?? string.Empty,
            (AchievementMediaAssetType)asset->MediaAssetType,
            Utf8.ToString(asset->Url) ?? string.Empty);
    }

    /// <summary>
    /// Converts a native <c>time_t</c> — seconds since the Unix epoch, UTC — to a .NET instant,
    /// clamping rather than throwing on a value the service should never send but could.
    /// </summary>
    internal static DateTimeOffset FromUnixSeconds(long seconds)
    {
        const long MinSeconds = -62135596800; // DateTimeOffset.MinValue
        const long MaxSeconds = 253402300799; // DateTimeOffset.MaxValue

        if (seconds <= MinSeconds)
        {
            return DateTimeOffset.MinValue;
        }

        return seconds >= MaxSeconds
            ? DateTimeOffset.MaxValue
            : DateTimeOffset.FromUnixTimeSeconds(seconds);
    }
}
