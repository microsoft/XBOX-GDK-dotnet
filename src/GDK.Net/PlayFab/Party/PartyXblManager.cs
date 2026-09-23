using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// Projects <c>PARTY_XBL_ACCESSIBILITY_SETTINGS</c>: an Xbox Live user's chat accessibility
/// preferences.
/// </summary>
/// <param name="SpeechToTextEnabled">Whether the user wants incoming voice transcribed.</param>
/// <param name="TextToSpeechEnabled">Whether the user wants outgoing text synthesized.</param>
/// <param name="LanguageCode">The user's preferred language, as a BCP-47 tag.</param>
/// <param name="Gender">The synthetic voice gender the user prefers.</param>
public sealed record PartyXblAccessibilitySettings(
    bool SpeechToTextEnabled, bool TextToSpeechEnabled, string LanguageCode, PartyGender Gender);

/// <summary>
/// Projects <c>PARTY_XBL_CHAT_PERMISSION_INFO</c>: what one user is permitted to hear or say to
/// another, and why.
/// </summary>
/// <param name="ChatPermissionMask">The permitted chat directions.</param>
/// <param name="Reason">Why the mask is restricted.</param>
public readonly record struct PartyXblChatPermissionInfo(
    PartyChatPermissionOptions ChatPermissionMask, PartyXblChatPermissionMaskReason Reason);

/// <summary>
/// Projects <c>PARTY_XBL_XBOX_USER_ID_TO_PLAYFAB_ENTITY_ID_MAPPING</c>.
/// </summary>
/// <param name="XboxLiveUserId">The Xbox Live user id.</param>
/// <param name="PlayFabEntityId">
/// The matching PlayFab entity id, or <see langword="null"/> when the user has never signed in to
/// the title.
/// </param>
public readonly record struct PartyXblEntityIdMapping(ulong XboxLiveUserId, string? PlayFabEntityId);

/// <summary>
/// Projects <c>PARTY_XBL_HTTP_HEADER</c>: one header of a token-and-signature request.
/// </summary>
/// <param name="Name">The header name.</param>
/// <param name="Value">The header value.</param>
public readonly record struct PartyXblHttpHeader(string Name, string Value);

/// <summary>
/// An Xbox Live user known to the Party Xbox Live extension
/// (<c>PARTY_XBL_CHAT_USER_HANDLE</c>).
/// </summary>
public sealed unsafe class PartyXblChatUser
{
    private bool _valid = true;

    internal PartyXblChatUser(PartyXblManager owner, IntPtr handle)
    {
        Owner = owner;
        Handle = handle;
    }

    internal PartyXblManager Owner { get; }

    internal IntPtr Handle { get; }

    internal void Invalidate() => _valid = false;

    private IntPtr Checked
    {
        get
        {
            if (!_valid)
            {
                throw new ObjectDisposedException(nameof(PartyXblChatUser));
            }

            return Handle;
        }
    }

    /// <summary>The user's Xbox Live user id (<c>PartyXblChatUserGetXboxUserId</c>).</summary>
    public ulong XboxUserId
    {
        get
        {
            ulong value;
            PartyInterop.CheckXbl(NativePlayFab.PartyXblChatUserGetXboxUserId(Checked, &value));
            return value;
        }
    }

    /// <summary>
    /// Whether this user is signed in on this device (<c>PartyXblChatUserIsLocal</c>).
    /// </summary>
    public bool IsLocal
    {
        get
        {
            byte value;
            PartyInterop.CheckXbl(NativePlayFab.PartyXblChatUserIsLocal(Checked, &value));
            return PartyInterop.ToBool(value);
        }
    }

    /// <summary>
    /// The user's chat accessibility preferences
    /// (<c>PartyXblLocalChatUserGetAccessibilitySettings</c>). Local users only.
    /// </summary>
    public PartyXblAccessibilitySettings AccessibilitySettings
    {
        get
        {
            PARTY_XBL_ACCESSIBILITY_SETTINGS value;
            PartyInterop.CheckXbl(
                NativePlayFab.PartyXblLocalChatUserGetAccessibilitySettings(Checked, &value));
            return new PartyXblAccessibilitySettings(
                PartyInterop.ToBool(value.SpeechToTextEnabled),
                PartyInterop.ToBool(value.TextToSpeechEnabled),
                PartyInterop.ReadFixed(value.LanguageCode, 85),
                (PartyGender)value.Gender);
        }
    }

    /// <summary>
    /// Whether the user is allowed to communicate outside Xbox Live
    /// (<c>PartyXblLocalChatUserGetCrossNetworkCommunicationPrivacySetting</c>). Local users only.
    /// </summary>
    public PartyXblCrossNetworkCommunicationPrivacySetting CrossNetworkCommunicationPrivacySetting
    {
        get
        {
            PARTY_XBL_CROSS_NETWORK_COMMUNICATION_PRIVACY_SETTING value;
            PartyInterop.CheckXbl(
                NativePlayFab
                    .PartyXblLocalChatUserGetCrossNetworkCommunicationPrivacySetting(
                        Checked, &value));
            return (PartyXblCrossNetworkCommunicationPrivacySetting)value;
        }
    }

    /// <summary>
    /// What this local user may hear from and say to another user
    /// (<c>PartyXblLocalChatUserGetRequiredChatPermissionInfo</c>).
    /// </summary>
    /// <param name="targetChatUser">The user the permission applies to.</param>
    public PartyXblChatPermissionInfo GetRequiredChatPermissionInfo(
        PartyXblChatUser targetChatUser)
    {
        if (targetChatUser is null)
        {
            throw new ArgumentNullException(nameof(targetChatUser));
        }

        PARTY_XBL_CHAT_PERMISSION_INFO value;
        PartyInterop.CheckXbl(NativePlayFab.PartyXblLocalChatUserGetRequiredChatPermissionInfo(
            Checked, targetChatUser.Handle, &value));
        return new PartyXblChatPermissionInfo(
            (PartyChatPermissionOptions)value.ChatPermissionMask,
            (PartyXblChatPermissionMaskReason)value.Reason);
    }

    /// <summary>
    /// Starts exchanging this Xbox Live user's token for a PlayFab entity
    /// (<c>PartyXblLoginToPlayFab</c>). Completion arrives as
    /// <see cref="PartyXblLoginToPlayFabCompleted"/>. Local users only.
    /// </summary>
    public PartyOperationId LoginToPlayFab()
    {
        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.CheckXbl(
            NativePlayFab.PartyXblLoginToPlayFab(Checked, PartyManager.Context(operation)));
        return operation;
    }
}

/// <summary>
/// The Party Xbox Live extension (<c>PartyXboxLive.h</c>): maps Xbox Live users onto PlayFab
/// entities and supplies the chat permissions Xbox Live requires.
/// </summary>
/// <remarks>
/// Like <see cref="PartyManager"/>, this library is poll-driven: operations start synchronously and
/// complete on a later <see cref="ProcessStateChanges"/> pump.
/// </remarks>
public sealed unsafe class PartyXblManager : IDisposable
{
    private readonly Dictionary<IntPtr, PartyXblChatUser> _chatUsers = new();
    private readonly IntPtr _handle;
    private long _nextOperation;
    private bool _disposed;

    private PartyXblManager(IntPtr handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Initializes the Xbox Live extension over an initialized Party library
    /// (<c>PartyXblInitialize</c>).
    /// </summary>
    /// <param name="party">The Party library instance the extension attaches to.</param>
    /// <param name="titleId">The PlayFab title id.</param>
    public static PartyXblManager Initialize(PartyManager party, string titleId)
    {
        if (party is null)
        {
            throw new ArgumentNullException(nameof(party));
        }

        if (string.IsNullOrEmpty(titleId))
        {
            throw new ArgumentException("A title id is required.", nameof(titleId));
        }

        IntPtr text = Utf8.Allocate(titleId);
        try
        {
            IntPtr handle;
            PartyInterop.CheckXbl(
                NativePlayFab.PartyXblInitialize(party.Handle, (byte*)text, &handle));
            return new PartyXblManager(handle);
        }
        finally
        {
            Utf8.Free(text);
        }
    }

    /// <summary>
    /// The extension's description of an error code (<c>PartyXblGetErrorMessage</c>).
    /// </summary>
    /// <param name="error">The <c>PartyError</c> value to describe.</param>
    public static string GetErrorMessage(uint error) => PartyInterop.DescribeXbl(error);

    /// <summary>
    /// Pins the extension's web request thread to a set of cores
    /// (<c>PartyXblSetThreadAffinityMask</c>).
    /// </summary>
    /// <param name="threadId">The thread to configure.</param>
    /// <param name="affinityMask">The processor affinity mask, or zero for no restriction.</param>
    public static void SetThreadAffinityMask(PartyXblThreadId threadId, ulong affinityMask) =>
        PartyInterop.CheckXbl(NativePlayFab.PartyXblSetThreadAffinityMask(
            (PARTY_XBL_THREAD_ID)threadId, affinityMask));

    /// <summary>
    /// The processor affinity mask of the extension's web request thread
    /// (<c>PartyXblGetThreadAffinityMask</c>).
    /// </summary>
    /// <param name="threadId">The thread to query.</param>
    public static ulong GetThreadAffinityMask(PartyXblThreadId threadId)
    {
        ulong mask;
        PartyInterop.CheckXbl(NativePlayFab.PartyXblGetThreadAffinityMask(
            (PARTY_XBL_THREAD_ID)threadId, &mask));
        return mask;
    }

    /// <summary>
    /// Starts creating a chat user for a signed-in Xbox Live user
    /// (<c>PartyXblCreateLocalChatUser</c>). Completion arrives as
    /// <see cref="PartyXblCreateLocalChatUserCompleted"/>.
    /// </summary>
    /// <param name="xboxUserId">The Xbox Live user id.</param>
    public (PartyOperationId Operation, PartyXblChatUser ChatUser) CreateLocalChatUser(
        ulong xboxUserId)
    {
        PartyOperationId operation = NextOperation();
        IntPtr handle;
        PartyInterop.CheckXbl(NativePlayFab.PartyXblCreateLocalChatUser(
            Handle, xboxUserId, PartyManager.Context(operation), &handle));
        return (operation, Track(handle));
    }

    /// <summary>
    /// Creates a chat user for a remote Xbox Live user (<c>PartyXblCreateRemoteChatUser</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox Live user id.</param>
    public PartyXblChatUser CreateRemoteChatUser(ulong xboxUserId)
    {
        IntPtr handle;
        PartyInterop.CheckXbl(
            NativePlayFab.PartyXblCreateRemoteChatUser(Handle, xboxUserId, &handle));
        return Track(handle);
    }

    /// <summary>Destroys a chat user (<c>PartyXblDestroyChatUser</c>).</summary>
    /// <param name="chatUser">The chat user to destroy.</param>
    public void DestroyChatUser(PartyXblChatUser chatUser)
    {
        if (chatUser is null)
        {
            throw new ArgumentNullException(nameof(chatUser));
        }

        PartyInterop.CheckXbl(
            NativePlayFab.PartyXblDestroyChatUser(Handle, chatUser.Handle));
        Retire(chatUser);
    }

    /// <summary>The chat users this device knows about (<c>PartyXblGetChatUsers</c>).</summary>
    public IReadOnlyList<PartyXblChatUser> ChatUsers
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.CheckXbl(
                NativePlayFab.PartyXblGetChatUsers(Handle, &count, &handles));
            if (handles is null || count == 0)
            {
                return Array.Empty<PartyXblChatUser>();
            }

            var result = new PartyXblChatUser[count];
            for (uint i = 0; i < count; i++)
            {
                result[i] = Track(handles[i]);
            }

            return result;
        }
    }

    /// <summary>
    /// Starts resolving Xbox Live user ids to PlayFab entity ids
    /// (<c>PartyXblGetEntityIdsFromXboxLiveUserIds</c>). Completion arrives as
    /// <see cref="PartyXblGetEntityIdsFromXboxLiveUserIdsCompleted"/>.
    /// </summary>
    /// <param name="xboxLiveUserIds">The Xbox Live user ids to resolve.</param>
    /// <param name="localChatUser">The local chat user whose token authorizes the lookup.</param>
    public PartyOperationId GetEntityIdsFromXboxLiveUserIds(
        IReadOnlyList<ulong> xboxLiveUserIds, PartyXblChatUser localChatUser)
    {
        if (xboxLiveUserIds is null)
        {
            throw new ArgumentNullException(nameof(xboxLiveUserIds));
        }

        if (localChatUser is null)
        {
            throw new ArgumentNullException(nameof(localChatUser));
        }

        PartyOperationId operation = NextOperation();
        var arena = new PlayFabArena();
        try
        {
            ulong* ids = null;
            if (xboxLiveUserIds.Count > 0)
            {
                ids = arena.Alloc<ulong>(xboxLiveUserIds.Count);
                for (int i = 0; i < xboxLiveUserIds.Count; i++)
                {
                    ids[i] = xboxLiveUserIds[i];
                }
            }

            PartyInterop.CheckXbl(NativePlayFab.PartyXblGetEntityIdsFromXboxLiveUserIds(
                Handle,
                (uint)xboxLiveUserIds.Count,
                ids,
                localChatUser.Handle,
                PartyManager.Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Answers a <see cref="PartyXblTokenAndSignatureRequested"/> state change
    /// (<c>PartyXblCompleteGetTokenAndSignatureRequest</c>).
    /// </summary>
    /// <param name="correlationId">The id from the state change being answered.</param>
    /// <param name="token">The Xbox Live token, or <see langword="null"/> on failure.</param>
    /// <param name="signature">The request signature, or <see langword="null"/> on failure.</param>
    public void CompleteGetTokenAndSignatureRequest(
        uint correlationId, string? token, string? signature)
    {
        bool succeeded = token is not null && signature is not null;
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.CheckXbl(NativePlayFab.PartyXblCompleteGetTokenAndSignatureRequest(
                Handle,
                correlationId,
                PartyInterop.FromBool(succeeded),
                arena.String(token),
                arena.String(signature)));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Drains the extension's state-change queue. Enumerate it once per frame; leaving the
    /// <c>foreach</c> returns the batch (<c>PartyXblStartProcessingStateChanges</c> /
    /// <c>PartyXblFinishProcessingStateChanges</c>).
    /// </summary>
    public PartyXblStateChangeCollection ProcessStateChanges() => new(this);

    /// <summary>
    /// Shuts the extension down (<c>PartyXblCleanup</c>), invalidating every chat user it produced.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        foreach (PartyXblChatUser chatUser in _chatUsers.Values)
        {
            chatUser.Invalidate();
        }

        _chatUsers.Clear();
        PartyInterop.CheckXbl(NativePlayFab.PartyXblCleanup(_handle));
    }

    internal IntPtr Handle
    {
        get
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PartyXblManager));
            }

            return _handle;
        }
    }

    internal PartyOperationId NextOperation() => new(Interlocked.Increment(ref _nextOperation));

    internal PartyXblChatUser Track(IntPtr handle)
    {
        if (!_chatUsers.TryGetValue(handle, out PartyXblChatUser? chatUser))
        {
            chatUser = new PartyXblChatUser(this, handle);
            _chatUsers[handle] = chatUser;
        }

        return chatUser;
    }

    internal PartyXblChatUser? Find(IntPtr handle) =>
        handle == IntPtr.Zero ? null : Track(handle);

    internal void Retire(PartyXblChatUser? chatUser)
    {
        if (chatUser is null)
        {
            return;
        }

        chatUser.Invalidate();
        _chatUsers.Remove(chatUser.Handle);
    }
}

/// <summary>
/// The Xbox Live extension state changes produced by one pump.
/// </summary>
public readonly struct PartyXblStateChangeCollection : IEnumerable<PartyXblStateChange>
{
    private readonly PartyXblManager _owner;

    internal PartyXblStateChangeCollection(PartyXblManager owner)
    {
        _owner = owner;
    }

    /// <summary>Starts a batch of state changes.</summary>
    public Enumerator GetEnumerator() => new(_owner);

    /// <inheritdoc/>
    IEnumerator<PartyXblStateChange> IEnumerable<PartyXblStateChange>.GetEnumerator() =>
        GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Walks one batch of state changes and returns it on <see cref="Dispose"/>.</summary>
    public unsafe struct Enumerator : IEnumerator<PartyXblStateChange>
    {
        private readonly PartyXblManager _owner;
        private readonly PARTY_XBL_STATE_CHANGE** _changes;
        private readonly uint _count;
        private readonly List<PartyXblChatUser> _retired;
        private uint _index;
        private PartyXblStateChange? _current;
        private bool _finished;

        internal Enumerator(PartyXblManager owner)
        {
            _owner = owner;
            _retired = new List<PartyXblChatUser>();
            _index = 0;
            _current = null;
            _finished = false;

            uint count;
            PARTY_XBL_STATE_CHANGE** changes;
            PartyInterop.CheckXbl(NativePlayFab.PartyXblStartProcessingStateChanges(
                owner.Handle, &count, &changes));
            _count = count;
            _changes = changes;
        }

        /// <inheritdoc/>
        public PartyXblStateChange Current => _current!;

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (_index >= _count)
            {
                _current = null;
                return false;
            }

            _current = PartyXblStateChangeReader.Read(_owner, _changes[_index], _retired);
            _index++;
            return true;
        }

        /// <summary>Returns the batch to the extension and retires destroyed chat users.</summary>
        public void Dispose()
        {
            if (_finished)
            {
                return;
            }

            _finished = true;
            PartyInterop.CheckXbl(NativePlayFab.PartyXblFinishProcessingStateChanges(
                _owner.Handle, _count, _changes));

            foreach (PartyXblChatUser chatUser in _retired)
            {
                _owner.Retire(chatUser);
            }
        }

        /// <inheritdoc/>
        void IEnumerator.Reset() => throw new NotSupportedException();
    }
}
