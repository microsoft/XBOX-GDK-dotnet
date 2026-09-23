// P/Invoke declarations for xsapi-c\social_c.h -- the Xbox Live social service.
//
// The GDK 260404 thunks DLL does not export
// XblSocialAddFriendRequestCountChangedHandler / XblSocialRemoveFriendRequestCountChangedHandler,
// even though they are declared in the header, so they are deliberately not bound here.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    // --- relationship queries ---

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialGetSocialRelationshipsAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        XblSocialRelationshipFilter socialRelationshipFilter,
        nuint startIndex,
        nuint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialGetSocialRelationshipsResult(XAsyncBlock* async, IntPtr* handle);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRelationshipResultGetRelationships(
        IntPtr resultHandle,
        XblSocialRelationship** relationships,
        nuint* relationshipsCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRelationshipResultHasNext(IntPtr resultHandle, byte* hasNext);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRelationshipResultGetTotalCount(IntPtr resultHandle, nuint* totalCount);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRelationshipResultGetNextAsync(
        IntPtr xboxLiveContext,
        IntPtr resultHandle,
        nuint maxItems,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRelationshipResultGetNextResult(XAsyncBlock* async, IntPtr* handle);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRelationshipResultDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XblSocialRelationshipResultCloseHandle(IntPtr handle);

    // --- relationship change notifications ---

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialAddSocialRelationshipChangedHandler(
        IntPtr xboxLiveContext,
        IntPtr handler,
        IntPtr handlerContext);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialRemoveSocialRelationshipChangedHandler(
        IntPtr xboxLiveContext,
        int handlerFunctionContext);

    // --- reputation feedback ---

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialSubmitReputationFeedbackAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        XblReputationFeedbackType reputationFeedbackType,
        XblMultiplayerSessionReference* sessionReference,
        byte* reasonMessage,
        byte* evidenceResourceId,
        XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial int XblSocialSubmitBatchReputationFeedbackAsync(
        IntPtr xboxLiveContext,
        XblReputationFeedbackItem* feedbackItems,
        nuint feedbackItemsCount,
        XAsyncBlock* async);
}

#else

internal static unsafe partial class NativeXbl
{
    // --- relationship queries ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialGetSocialRelationshipsAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        XblSocialRelationshipFilter socialRelationshipFilter,
        nuint startIndex,
        nuint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialGetSocialRelationshipsResult(XAsyncBlock* async, IntPtr* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRelationshipResultGetRelationships(
        IntPtr resultHandle,
        XblSocialRelationship** relationships,
        nuint* relationshipsCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRelationshipResultHasNext(IntPtr resultHandle, byte* hasNext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRelationshipResultGetTotalCount(IntPtr resultHandle, nuint* totalCount);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRelationshipResultGetNextAsync(
        IntPtr xboxLiveContext,
        IntPtr resultHandle,
        nuint maxItems,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRelationshipResultGetNextResult(XAsyncBlock* async, IntPtr* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRelationshipResultDuplicateHandle(IntPtr handle, IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblSocialRelationshipResultCloseHandle(IntPtr handle);

    // --- relationship change notifications ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialAddSocialRelationshipChangedHandler(
        IntPtr xboxLiveContext,
        IntPtr handler,
        IntPtr handlerContext);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialRemoveSocialRelationshipChangedHandler(
        IntPtr xboxLiveContext,
        int handlerFunctionContext);

    // --- reputation feedback ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialSubmitReputationFeedbackAsync(
        IntPtr xboxLiveContext,
        ulong xboxUserId,
        XblReputationFeedbackType reputationFeedbackType,
        XblMultiplayerSessionReference* sessionReference,
        byte* reasonMessage,
        byte* evidenceResourceId,
        XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSocialSubmitBatchReputationFeedbackAsync(
        IntPtr xboxLiveContext,
        XblReputationFeedbackItem* feedbackItems,
        nuint feedbackItemsCount,
        XAsyncBlock* async);
}

#endif
