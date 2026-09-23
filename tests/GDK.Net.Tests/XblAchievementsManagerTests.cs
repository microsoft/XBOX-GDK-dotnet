using System;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class XblAchievementsManagerTests
{
    [Theory]
    [InlineData(AchievementsManagerSortOrder.Unsorted, 0u)]
    [InlineData(AchievementsManagerSortOrder.Ascending, 1u)]
    [InlineData(AchievementsManagerSortOrder.Descending, 2u)]
    public void SortOrderMatchesHeader(AchievementsManagerSortOrder value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementsManagerSortOrder)value);
    }

    [Theory]
    [InlineData(AchievementsManagerEventType.LocalUserInitialStateSynced, 0u)]
    [InlineData(AchievementsManagerEventType.AchievementUnlocked, 1u)]
    [InlineData(AchievementsManagerEventType.AchievementProgressUpdated, 2u)]
    public void EventTypeMatchesHeader(AchievementsManagerEventType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblAchievementsManagerEventType)value);
    }

    [Fact]
    public void EventStructMatchesHeaderLayout()
    {
        Assert.Equal(56, sizeof(XblAchievementsManagerEvent));
        Assert.Equal(0, (int)Marshal.OffsetOf<XblAchievementsManagerEvent>(nameof(XblAchievementsManagerEvent.ProgressInfo)));
        Assert.Equal(40, (int)Marshal.OffsetOf<XblAchievementsManagerEvent>(nameof(XblAchievementsManagerEvent.XboxUserId)));
        Assert.Equal(48, (int)Marshal.OffsetOf<XblAchievementsManagerEvent>(nameof(XblAchievementsManagerEvent.EventType)));

        Assert.Equal(40, sizeof(XblAchievementProgressChangeEntry));
        Assert.Equal(0, (int)Marshal.OffsetOf<XblAchievementProgressChangeEntry>(nameof(XblAchievementProgressChangeEntry.AchievementId)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XblAchievementProgressChangeEntry>(nameof(XblAchievementProgressChangeEntry.ProgressState)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XblAchievementProgressChangeEntry>(nameof(XblAchievementProgressChangeEntry.Progression)));
    }

    [Fact]
    public void EventHierarchyIsAClosedRecordSet()
    {
        Assert.True(typeof(AchievementsManagerEvent).IsAbstract);
        Assert.True(typeof(AchievementsManagerEvent).IsAssignableFrom(typeof(AchievementsManagerLocalUserInitialStateSyncedEvent)));
        Assert.True(typeof(AchievementsManagerEvent).IsAssignableFrom(typeof(AchievementsManagerAchievementUnlockedEvent)));
        Assert.True(typeof(AchievementsManagerEvent).IsAssignableFrom(typeof(AchievementsManagerAchievementProgressUpdatedEvent)));
        Assert.True(typeof(AchievementsManagerLocalUserInitialStateSyncedEvent).IsSealed);
        Assert.True(typeof(AchievementsManagerAchievementUnlockedEvent).IsSealed);
        Assert.True(typeof(AchievementsManagerAchievementProgressUpdatedEvent).IsSealed);
    }

    [Fact]
    public void PublicManagerTypesHaveExpectedShape()
    {
        Assert.True(typeof(AchievementsManager).IsSealed);
        Assert.True(typeof(AchievementsManagerResult).IsSealed);
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(AchievementsManagerResult)));

        Assert.Empty(typeof(AchievementsManager).GetConstructors());
        Assert.Empty(typeof(AchievementsManagerResult).GetConstructors());
    }

    [Fact]
    public void PublicSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        [
            typeof(AchievementsManager),
            typeof(AchievementsManagerResult),
            typeof(AchievementsManagerEvent),
            typeof(AchievementsManagerLocalUserInitialStateSyncedEvent),
            typeof(AchievementsManagerAchievementUnlockedEvent),
            typeof(AchievementsManagerAchievementProgressUpdatedEvent),
        ];

        foreach (Type type in publicTypes)
        {
            foreach (System.Reflection.MethodInfo method in type.GetMethods(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (System.Reflection.ParameterInfo parameter in method.GetParameters())
                {
                    AssertNotInterop(type, method.Name, parameter.ParameterType);
                }
            }
        }
    }

    private static void AssertNotInterop(Type owner, string member, Type candidate)
    {
        Type target = candidate.IsByRef || candidate.IsPointer || candidate.IsArray
            ? candidate.GetElementType()!
            : candidate;

        Assert.False(
            target.Namespace is "GDK.Net.Interop",
            $"{owner.Name}.{member} exposes the interop type {target.Name}.");

        Assert.False(
            typeof(SafeHandle).IsAssignableFrom(target),
            $"{owner.Name}.{member} exposes the SafeHandle {target.Name}.");
    }
}
