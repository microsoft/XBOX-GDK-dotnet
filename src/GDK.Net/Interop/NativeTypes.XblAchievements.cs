// Blittable mirrors of the XSAPI achievement types -- xsapi-c\achievements_c.h, GDK edition 260404.
//
// See NativeTypes.Xbl.cs for the layout rules that apply across the XSAPI type mirrors. Note in
// particular that `time_t` is a signed 64-bit value on both x64 and arm64, so it maps to `long`,
// and C++ `bool` is one byte, so it maps to `byte`.
//
// Every pointer in XblAchievement points into memory owned by the XblAchievementsResultHandle the
// achievement came from. The projection snapshots the whole graph into managed objects while the
// handle is alive; see XboxLive\AchievementsService.cs.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

// ─── enums ────────────────────────────────────────────────────────────────────

/// <summary>Mirrors <c>XblAchievementType</c>.</summary>
internal enum XblAchievementType : uint
{
    Unknown = 0,
    All = 1,
    Persistent = 2,
    Challenge = 3,
}

/// <summary>Mirrors <c>XblAchievementOrderBy</c>.</summary>
internal enum XblAchievementOrderBy : uint
{
    DefaultOrder = 0,
    TitleId = 1,
    UnlockTime = 2,
}

/// <summary>Mirrors <c>XblAchievementProgressState</c>.</summary>
internal enum XblAchievementProgressState : uint
{
    Unknown = 0,
    Achieved = 1,
    NotStarted = 2,
    InProgress = 3,
}

/// <summary>Mirrors <c>XblAchievementMediaAssetType</c>.</summary>
internal enum XblAchievementMediaAssetType : uint
{
    Unknown = 0,
    Icon = 1,
    Art = 2,
}

/// <summary>Mirrors <c>XblAchievementParticipationType</c>.</summary>
internal enum XblAchievementParticipationType : uint
{
    Unknown = 0,
    Individual = 1,
    Group = 2,
}

/// <summary>Mirrors <c>XblAchievementRewardType</c>.</summary>
internal enum XblAchievementRewardType : uint
{
    Unknown = 0,
    Gamerscore = 1,
    InApp = 2,
    Art = 3,
}

/// <summary>Mirrors <c>XblAchievementRarityCategory</c>.</summary>
internal enum XblAchievementRarityCategory : uint
{
    Unset = 0,
    Rare = 1,
    Common = 2,
}

// ─── achievement graph ────────────────────────────────────────────────────────

/// <summary>Mirrors <c>XblAchievementTitleAssociation</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementTitleAssociation
{
    internal byte* Name;
    internal uint TitleId;
}

/// <summary>Mirrors <c>XblAchievementRequirement</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementRequirement
{
    internal byte* Id;
    internal byte* CurrentProgressValue;
    internal byte* TargetProgressValue;
}

/// <summary>Mirrors <c>XblAchievementProgression</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementProgression
{
    internal XblAchievementRequirement* Requirements;
    internal nuint RequirementsCount;

    /// <summary><c>time_t</c>: seconds since the Unix epoch.</summary>
    internal long TimeUnlocked;
}

/// <summary>Mirrors <c>XblAchievementTimeWindow</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblAchievementTimeWindow
{
    internal long StartDate;
    internal long EndDate;
}

/// <summary>Mirrors <c>XblAchievementMediaAsset</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementMediaAsset
{
    internal byte* Name;
    internal XblAchievementMediaAssetType MediaAssetType;
    internal byte* Url;
}

/// <summary>Mirrors <c>XblAchievementReward</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementReward
{
    internal byte* Name;
    internal byte* Description;
    internal byte* Value;
    internal XblAchievementRewardType RewardType;
    internal byte* ValueType;
    internal XblAchievementMediaAsset* MediaAsset;
}

/// <summary>Mirrors <c>XblAchievement</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievement
{
    internal byte* Id;
    internal byte* ServiceConfigurationId;
    internal byte* Name;
    internal XblAchievementTitleAssociation* TitleAssociations;
    internal nuint TitleAssociationsCount;
    internal XblAchievementProgressState ProgressState;
    internal XblAchievementProgression Progression;
    internal XblAchievementMediaAsset* MediaAssets;
    internal nuint MediaAssetsCount;
    internal byte** PlatformsAvailableOn;
    internal nuint PlatformsAvailableOnCount;
    internal byte IsSecret;
    internal byte* UnlockedDescription;
    internal byte* LockedDescription;
    internal byte* ProductId;
    internal XblAchievementType Type;
    internal XblAchievementParticipationType ParticipationType;
    internal XblAchievementTimeWindow Available;
    internal XblAchievementReward* Rewards;
    internal nuint RewardsCount;
    internal ulong EstimatedUnlockTime;
    internal byte* DeepLink;
    internal byte IsRevoked;
}

// ─── progress-change notifications ────────────────────────────────────────────

/// <summary>Mirrors <c>XblAchievementProgressChangeEntry</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementProgressChangeEntry
{
    internal byte* AchievementId;
    internal XblAchievementProgressState ProgressState;
    internal XblAchievementProgression Progression;
}

/// <summary>Mirrors <c>XblAchievementProgressChangeEventArgs</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblAchievementProgressChangeEventArgs
{
    internal XblAchievementProgressChangeEntry* UpdatedAchievementEntries;
    internal nuint EntryCount;
}
