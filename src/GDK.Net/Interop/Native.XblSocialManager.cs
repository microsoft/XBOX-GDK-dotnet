// P/Invoke declarations for xsapi-c\social_manager_c.h -- the Xbox Live social manager.
//
// XblSocialManagerDoWork has no matching Finish API. It returns an XSAPI-owned event array that is
// valid only until the next DoWork call, so the public layer snapshots events before returning.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    [LibraryImport(LibraryName)]
    internal static partial byte XblSocialManagerPresenceRecordIsUserPlayingTitle(
        XblSocialManagerPresenceRecord* presenceRecord,
        uint titleId);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerUserGroupGetType(
        IntPtr group,
        XblSocialUserGroupType* type);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerUserGroupGetLocalUser(
        IntPtr group,
        IntPtr* localUser);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerUserGroupGetFilters(
        IntPtr group,
        XblSocialManagerPresenceFilter* presenceFilter,
        XblSocialManagerRelationshipFilter* relationshipFilter);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerUserGroupGetUsers(
        IntPtr group,
        XblSocialManagerUser*** users,
        nuint* usersCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerUserGroupGetUsersTrackedByGroup(
        IntPtr group,
        ulong** trackedUsers,
        nuint* trackedUsersCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerAddLocalUser(
        IntPtr user,
        XblSocialManagerExtraDetailLevel extraLevelDetail,
        IntPtr queue);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerRemoveLocalUser(IntPtr user);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerDoWork(
        XblSocialManagerEvent** socialEvents,
        nuint* socialEventsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerCreateSocialUserGroupFromFilters(
        IntPtr user,
        XblSocialManagerPresenceFilter presenceFilter,
        XblSocialManagerRelationshipFilter relationshipFilter,
        IntPtr* group);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerCreateSocialUserGroupFromList(
        IntPtr user,
        ulong* xboxUserIdList,
        nuint xboxUserIdListCount,
        IntPtr* group);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerDestroySocialUserGroup(IntPtr group);

    [LibraryImport(LibraryName)]
    internal static partial nuint XblSocialManagerGetLocalUserCount();

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerGetLocalUsers(
        nuint usersCount,
        IntPtr* users);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerUpdateSocialUserGroup(
        IntPtr group,
        ulong* users,
        nuint usersCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialManagerSetRichPresencePollingStatus(
        IntPtr user,
        byte shouldEnablePolling);
}

#else

internal static unsafe partial class NativeXbl
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XblSocialManagerPresenceRecordIsUserPlayingTitle(
        XblSocialManagerPresenceRecord* presenceRecord,
        uint titleId);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerUserGroupGetType(
        IntPtr group,
        XblSocialUserGroupType* type);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerUserGroupGetLocalUser(
        IntPtr group,
        IntPtr* localUser);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerUserGroupGetFilters(
        IntPtr group,
        XblSocialManagerPresenceFilter* presenceFilter,
        XblSocialManagerRelationshipFilter* relationshipFilter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerUserGroupGetUsers(
        IntPtr group,
        XblSocialManagerUser*** users,
        nuint* usersCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerUserGroupGetUsersTrackedByGroup(
        IntPtr group,
        ulong** trackedUsers,
        nuint* trackedUsersCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerAddLocalUser(
        IntPtr user,
        XblSocialManagerExtraDetailLevel extraLevelDetail,
        IntPtr queue);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerRemoveLocalUser(IntPtr user);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerDoWork(
        XblSocialManagerEvent** socialEvents,
        nuint* socialEventsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerCreateSocialUserGroupFromFilters(
        IntPtr user,
        XblSocialManagerPresenceFilter presenceFilter,
        XblSocialManagerRelationshipFilter relationshipFilter,
        IntPtr* group);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerCreateSocialUserGroupFromList(
        IntPtr user,
        ulong* xboxUserIdList,
        nuint xboxUserIdListCount,
        IntPtr* group);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerDestroySocialUserGroup(IntPtr group);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern nuint XblSocialManagerGetLocalUserCount();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerGetLocalUsers(
        nuint usersCount,
        IntPtr* users);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerUpdateSocialUserGroup(
        IntPtr group,
        ulong* users,
        nuint usersCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialManagerSetRichPresencePollingStatus(
        IntPtr user,
        byte shouldEnablePolling);
}

#endif
