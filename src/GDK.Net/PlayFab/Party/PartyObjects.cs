using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// Base for the Party objects that are owned by the library and torn down through a state change
/// rather than by the caller.
/// </summary>
/// <remarks>
/// A Party object is only meaningful while the network it belongs to is alive. Once the matching
/// destruction state change has been returned from a pump, the wrapper is invalidated and every
/// member throws <see cref="ObjectDisposedException"/> instead of touching a stale handle.
/// </remarks>
public abstract class PartyObject
{
    private volatile bool _valid = true;

    private protected PartyObject(PartyManager owner, IntPtr handle)
    {
        Owner = owner;
        Handle = handle;
    }

    /// <summary>Whether the object is still usable.</summary>
    public bool IsValid => _valid;

    internal PartyManager Owner { get; }

    internal IntPtr Handle { get; }

    internal void Invalidate() => _valid = false;

    private protected IntPtr Checked
    {
        get
        {
            if (!_valid)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            return Handle;
        }
    }
}

/// <summary>
/// Identifies which Party object family a shared-property call targets, so the four identical
/// native property triples can share one implementation without capturing function pointers.
/// </summary>
internal enum PartyPropertyOwner
{
    Device,
    Endpoint,
    Network,
    ChatControl,
}

/// <summary>The shared-property triple that Device, Endpoint, Network and ChatControl all expose.</summary>
internal static unsafe class PartyProperties
{
    internal static IReadOnlyList<string> GetKeys(PartyPropertyOwner owner, IntPtr handle)
    {
        uint count;
        byte** keys;
        PartyInterop.Check(owner switch
        {
            PartyPropertyOwner.Device =>
                NativePlayFab.PartyDeviceGetSharedPropertyKeys(handle, &count, &keys),
            PartyPropertyOwner.Endpoint =>
                NativePlayFab.PartyEndpointGetSharedPropertyKeys(handle, &count, &keys),
            PartyPropertyOwner.Network =>
                NativePlayFab.PartyNetworkGetSharedPropertyKeys(handle, &count, &keys),
            _ => NativePlayFab.PartyChatControlGetSharedPropertyKeys(handle, &count, &keys),
        });

        return PartyInterop.ReadStrings(keys, count);
    }

    internal static byte[]? Get(PartyPropertyOwner owner, IntPtr handle, string key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        PARTY_DATA_BUFFER value;
        IntPtr text = Utf8.Allocate(key);
        try
        {
            PartyInterop.Check(owner switch
            {
                PartyPropertyOwner.Device =>
                    NativePlayFab.PartyDeviceGetSharedProperty(handle, (byte*)text, &value),
                PartyPropertyOwner.Endpoint =>
                    NativePlayFab.PartyEndpointGetSharedProperty(handle, (byte*)text, &value),
                PartyPropertyOwner.Network =>
                    NativePlayFab.PartyNetworkGetSharedProperty(handle, (byte*)text, &value),
                _ => NativePlayFab.PartyChatControlGetSharedProperty(handle, (byte*)text, &value),
            });
        }
        finally
        {
            Utf8.Free(text);
        }

        return value.Buffer is null ? null : PartyInterop.ReadBuffer(value.Buffer, value.BufferByteCount);
    }

    internal static void Set(
        PartyPropertyOwner owner, IntPtr handle, IReadOnlyDictionary<string, byte[]?> properties)
    {
        if (properties is null)
        {
            throw new ArgumentNullException(nameof(properties));
        }

        if (properties.Count == 0)
        {
            return;
        }

        var arena = new PlayFabArena();
        try
        {
            int count = properties.Count;
            byte** keys = (byte**)arena.Alloc<IntPtr>(count);
            PARTY_DATA_BUFFER* values = arena.Alloc<PARTY_DATA_BUFFER>(count);
            int index = 0;
            foreach (KeyValuePair<string, byte[]?> pair in properties)
            {
                keys[index] = arena.String(pair.Key);
                if (pair.Value is null || pair.Value.Length == 0)
                {
                    // A null buffer deletes the property.
                    values[index].Buffer = null;
                    values[index].BufferByteCount = 0;
                }
                else
                {
                    byte* buffer = arena.Alloc<byte>(pair.Value.Length);
                    fixed (byte* source = pair.Value)
                    {
                        System.Buffer.MemoryCopy(
                            source, buffer, pair.Value.Length, pair.Value.Length);
                    }

                    values[index].Buffer = buffer;
                    values[index].BufferByteCount = (uint)pair.Value.Length;
                }

                index++;
            }

            PartyInterop.Check(owner switch
            {
                PartyPropertyOwner.Device => NativePlayFab.PartyDeviceSetSharedProperties(
                    handle, (uint)count, keys, values),
                PartyPropertyOwner.Endpoint => NativePlayFab.PartyEndpointSetSharedProperties(
                    handle, (uint)count, keys, values),
                PartyPropertyOwner.Network => NativePlayFab.PartyNetworkSetSharedProperties(
                    handle, (uint)count, keys, values),
                _ => NativePlayFab.PartyChatControlSetSharedProperties(
                    handle, (uint)count, keys, values),
            });
        }
        finally
        {
            arena.Dispose();
        }
    }
}

/// <summary>
/// Projects <c>PARTY_LOCAL_USER_HANDLE</c>: a PlayFab entity signed in to the Party library on
/// this device.
/// </summary>
public sealed class PartyLocalUser : PartyObject
{
    internal PartyLocalUser(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>The PlayFab entity id.</summary>
    public unsafe string EntityId
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyLocalUserGetEntityId(Checked, &value));
            return Utf8.ToString(value) ?? string.Empty;
        }
    }

    /// <summary>The PlayFab entity type, for example <c>title_player_account</c>.</summary>
    public unsafe string EntityType
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyLocalUserGetEntityType(Checked, &value));
            return Utf8.ToString(value) ?? string.Empty;
        }
    }

    /// <summary>
    /// Supplies a refreshed PlayFab entity token before the previous one expires.
    /// </summary>
    /// <param name="entityToken">The new entity token.</param>
    public unsafe void UpdateEntityToken(string entityToken)
    {
        if (entityToken is null)
        {
            throw new ArgumentNullException(nameof(entityToken));
        }

        IntPtr text = Utf8.Allocate(entityToken);
        try
        {
            PartyInterop.Check(
                NativePlayFab.PartyLocalUserUpdateEntityToken(Checked, (byte*)text));
        }
        finally
        {
            Utf8.Free(text);
        }
    }
}

/// <summary>
/// Projects <c>PARTY_DEVICE_HANDLE</c>: a physical device participating in one or more networks.
/// </summary>
public sealed class PartyDevice : PartyObject
{
    internal PartyDevice(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>Whether this is the local device.</summary>
    public unsafe bool IsLocal
    {
        get
        {
            byte value;
            PartyInterop.Check(NativePlayFab.PartyDeviceIsLocal(Checked, &value));
            return PartyInterop.ToBool(value);
        }
    }

    /// <summary>The chat controls hosted on the device.</summary>
    public unsafe IReadOnlyList<PartyChatControl> ChatControls
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyDeviceGetChatControls(Checked, &count, &handles));
            return Owner.WrapChatControls(handles, count);
        }
    }

    /// <summary>The keys of every property shared on the device.</summary>
    public IReadOnlyList<string> GetSharedPropertyKeys() =>
        PartyProperties.GetKeys(PartyPropertyOwner.Device, Checked);

    /// <summary>Reads a shared property, or <see langword="null"/> when it is not set.</summary>
    /// <param name="key">The property key.</param>
    public byte[]? GetSharedProperty(string key) =>
        PartyProperties.Get(PartyPropertyOwner.Device, Checked, key);

    /// <summary>Sets or, for a <see langword="null"/> value, removes shared properties.</summary>
    /// <param name="properties">The properties to write.</param>
    public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties) =>
        PartyProperties.Set(PartyPropertyOwner.Device, Checked, properties);

    /// <summary>
    /// Starts creating a chat control for a local user on this (local) device.
    /// </summary>
    /// <param name="localUser">The local user the chat control speaks for.</param>
    /// <param name="languageCode">
    /// The BCP-47 language code for transcription and translation, or <see langword="null"/> for
    /// the platform default.
    /// </param>
    public unsafe PartyOperationId CreateChatControl(
        PartyLocalUser localUser, string? languageCode = null)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            IntPtr chatControl;
            PartyInterop.Check(NativePlayFab.PartyDeviceCreateChatControl(
                Checked,
                localUser.Handle,
                arena.String(languageCode),
                PartyManager.Context(operation),
                &chatControl));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts destroying a chat control hosted on this device.</summary>
    /// <param name="chatControl">The chat control to destroy.</param>
    public unsafe PartyOperationId DestroyChatControl(PartyChatControl chatControl)
    {
        if (chatControl is null)
        {
            throw new ArgumentNullException(nameof(chatControl));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyDeviceDestroyChatControl(
            Checked, chatControl.Handle, PartyManager.Context(operation)));
        return operation;
    }
}

/// <summary>
/// Projects <c>PARTY_ENDPOINT_HANDLE</c>: a messaging endpoint inside a network.
/// </summary>
public sealed class PartyEndpoint : PartyObject
{
    internal PartyEndpoint(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>Whether the endpoint belongs to the local device.</summary>
    public unsafe bool IsLocal
    {
        get
        {
            byte value;
            PartyInterop.Check(NativePlayFab.PartyEndpointIsLocal(Checked, &value));
            return PartyInterop.ToBool(value);
        }
    }

    /// <summary>The endpoint's per-network unique identifier.</summary>
    public unsafe ushort UniqueIdentifier
    {
        get
        {
            ushort value;
            PartyInterop.Check(
                NativePlayFab.PartyEndpointGetUniqueIdentifier(Checked, &value));
            return value;
        }
    }

    /// <summary>The PlayFab entity id behind the endpoint, if it is user-owned.</summary>
    public unsafe string? EntityId
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyEndpointGetEntityId(Checked, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>The PlayFab entity type behind the endpoint, if it is user-owned.</summary>
    public unsafe string? EntityType
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyEndpointGetEntityType(Checked, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>The local user that owns the endpoint, or <see langword="null"/> for a device endpoint.</summary>
    public unsafe PartyLocalUser? LocalUser
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyEndpointGetLocalUser(Checked, &handle));
            return handle == IntPtr.Zero ? null : Owner.WrapLocalUser(handle);
        }
    }

    /// <summary>The network the endpoint belongs to.</summary>
    public unsafe PartyNetwork Network
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyEndpointGetNetwork(Checked, &handle));
            return Owner.WrapNetwork(handle);
        }
    }

    /// <summary>The device hosting the endpoint.</summary>
    public unsafe PartyDevice Device
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyEndpointGetDevice(Checked, &handle));
            return Owner.WrapDevice(handle);
        }
    }

    /// <summary>The keys of every property shared on the endpoint.</summary>
    public IReadOnlyList<string> GetSharedPropertyKeys() =>
        PartyProperties.GetKeys(PartyPropertyOwner.Endpoint, Checked);

    /// <summary>Reads a shared property, or <see langword="null"/> when it is not set.</summary>
    /// <param name="key">The property key.</param>
    public byte[]? GetSharedProperty(string key) =>
        PartyProperties.Get(PartyPropertyOwner.Endpoint, Checked, key);

    /// <summary>Sets or, for a <see langword="null"/> value, removes shared properties.</summary>
    /// <param name="properties">The properties to write.</param>
    public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties) =>
        PartyProperties.Set(PartyPropertyOwner.Endpoint, Checked, properties);

    /// <summary>Queues a message from this local endpoint to the given targets.</summary>
    /// <param name="targets">
    /// The receiving endpoints, or an empty list to broadcast to every endpoint in the network.
    /// </param>
    /// <param name="message">The message payload.</param>
    /// <param name="options">Delivery options.</param>
    /// <param name="queuing">Optional priority and timeout for the queued message.</param>
    /// <returns>
    /// The identity used to correlate the later <see cref="PartyDataBuffersReturned"/> state change.
    /// </returns>
    public unsafe PartyOperationId SendMessage(
        IReadOnlyList<PartyEndpoint> targets,
        byte[] message,
        PartySendMessageOptions options = PartySendMessageOptions.Default,
        PartySendMessageQueuingConfiguration? queuing = null)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            uint targetCount;
            IntPtr* targetHandles = WriteTargets(arena, targets, out targetCount);

            var buffer = default(PARTY_DATA_BUFFER);
            byte* payload = null;
            if (message.Length > 0)
            {
                payload = arena.Alloc<byte>(message.Length);
                fixed (byte* source = message)
                {
                    Buffer.MemoryCopy(source, payload, message.Length, message.Length);
                }
            }

            buffer.Buffer = payload;
            buffer.BufferByteCount = (uint)message.Length;

            PARTY_SEND_MESSAGE_QUEUING_CONFIGURATION* queueConfig = null;
            if (queuing is not null)
            {
                queueConfig = arena.Alloc<PARTY_SEND_MESSAGE_QUEUING_CONFIGURATION>(1);
                queuing.WriteTo(queueConfig);
            }

            PartyInterop.Check(NativePlayFab.PartyEndpointSendMessage(
                Checked,
                targetCount,
                targetHandles,
                (PARTY_SEND_MESSAGE_OPTIONS)options,
                queueConfig,
                1,
                &buffer,
                PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Cancels queued messages that match the supplied filter.</summary>
    /// <param name="targets">The targets to filter on, or an empty list for all targets.</param>
    /// <param name="filter">How the identity mask is interpreted.</param>
    /// <param name="messageIdentityFilterMask">The bits of the identity that participate.</param>
    /// <param name="filteredMessageIdentitiesToMatch">The masked identity value to match.</param>
    /// <returns>The number of messages that were cancelled.</returns>
    public unsafe uint CancelMessages(
        IReadOnlyList<PartyEndpoint> targets,
        PartyCancelMessagesFilterExpression filter,
        uint messageIdentityFilterMask,
        uint filteredMessageIdentitiesToMatch)
    {
        var arena = new PlayFabArena();
        try
        {
            uint targetCount;
            IntPtr* targetHandles = WriteTargets(arena, targets, out targetCount);
            uint cancelled;
            PartyInterop.Check(NativePlayFab.PartyEndpointCancelMessages(
                Checked,
                targetCount,
                targetHandles,
                (PARTY_CANCEL_MESSAGES_FILTER_EXPRESSION)filter,
                messageIdentityFilterMask,
                filteredMessageIdentitiesToMatch,
                &cancelled));
            return cancelled;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>Requests that queued messages to the given targets be sent immediately.</summary>
    /// <param name="targets">The targets to flush, or an empty list for all targets.</param>
    public unsafe void FlushMessages(IReadOnlyList<PartyEndpoint> targets)
    {
        var arena = new PlayFabArena();
        try
        {
            uint targetCount;
            IntPtr* targetHandles = WriteTargets(arena, targets, out targetCount);
            PartyInterop.Check(
                NativePlayFab.PartyEndpointFlushMessages(Checked, targetCount, targetHandles));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>Reads per-endpoint queuing statistics.</summary>
    /// <param name="targets">The targets to aggregate over, or an empty list for all targets.</param>
    /// <param name="statistics">The statistics to read.</param>
    public unsafe IReadOnlyDictionary<PartyEndpointStatistic, ulong> GetStatistics(
        IReadOnlyList<PartyEndpoint> targets, IReadOnlyList<PartyEndpointStatistic> statistics)
    {
        if (statistics is null)
        {
            throw new ArgumentNullException(nameof(statistics));
        }

        var result = new Dictionary<PartyEndpointStatistic, ulong>(statistics.Count);
        if (statistics.Count == 0)
        {
            return result;
        }

        var arena = new PlayFabArena();
        try
        {
            uint targetCount;
            IntPtr* targetHandles = WriteTargets(arena, targets, out targetCount);
            PARTY_ENDPOINT_STATISTIC* types =
                arena.Alloc<PARTY_ENDPOINT_STATISTIC>(statistics.Count);
            ulong* values = arena.Alloc<ulong>(statistics.Count);
            for (int i = 0; i < statistics.Count; i++)
            {
                types[i] = (PARTY_ENDPOINT_STATISTIC)statistics[i];
            }

            PartyInterop.Check(NativePlayFab.PartyEndpointGetEndpointStatistics(
                Checked, targetCount, targetHandles, (uint)statistics.Count, types, values));

            for (int i = 0; i < statistics.Count; i++)
            {
                result[statistics[i]] = values[i];
            }
        }
        finally
        {
            arena.Dispose();
        }

        return result;
    }

    private static unsafe IntPtr* WriteTargets(
        PlayFabArena arena, IReadOnlyList<PartyEndpoint>? targets, out uint count)
    {
        count = targets is null ? 0u : (uint)targets.Count;
        if (count == 0)
        {
            return null;
        }

        IntPtr* buffer = arena.Alloc<IntPtr>((int)count);
        for (int i = 0; i < targets!.Count; i++)
        {
            buffer[i] = targets[i].Handle;
        }

        return buffer;
    }
}

/// <summary>
/// Projects <c>PARTY_INVITATION_HANDLE</c>: an outstanding invitation to a network.
/// </summary>
public sealed class PartyInvitation : PartyObject
{
    internal PartyInvitation(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>The entity id of the user that created the invitation.</summary>
    public unsafe string? CreatorEntityId
    {
        get
        {
            byte* value;
            PartyInterop.Check(
                NativePlayFab.PartyInvitationGetCreatorEntityId(Checked, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>The invitation's identifier, revocability and admitted entity ids.</summary>
    public unsafe PartyInvitationConfiguration Configuration
    {
        get
        {
            PARTY_INVITATION_CONFIGURATION* value;
            PartyInterop.Check(
                NativePlayFab.PartyInvitationGetInvitationConfiguration(Checked, &value));
            return PartyInvitationConfiguration.FromNative(value);
        }
    }
}

/// <summary>
/// Projects <c>PARTY_TEXT_TO_SPEECH_PROFILE_HANDLE</c>: a synthetic voice available for
/// text-to-speech.
/// </summary>
public sealed class PartyTextToSpeechProfile : PartyObject
{
    internal PartyTextToSpeechProfile(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>The identifier used to select the profile.</summary>
    public unsafe string Identifier
    {
        get
        {
            byte* value;
            PartyInterop.Check(
                NativePlayFab.PartyTextToSpeechProfileGetIdentifier(Checked, &value));
            return Utf8.ToString(value) ?? string.Empty;
        }
    }

    /// <summary>The profile's display name.</summary>
    public unsafe string Name
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyTextToSpeechProfileGetName(Checked, &value));
            return Utf8.ToString(value) ?? string.Empty;
        }
    }

    /// <summary>The BCP-47 language code the profile speaks.</summary>
    public unsafe string LanguageCode
    {
        get
        {
            byte* value;
            PartyInterop.Check(
                NativePlayFab.PartyTextToSpeechProfileGetLanguageCode(Checked, &value));
            return Utf8.ToString(value) ?? string.Empty;
        }
    }

    /// <summary>The voice's gender.</summary>
    public unsafe PartyGender Gender
    {
        get
        {
            PARTY_GENDER value;
            PartyInterop.Check(
                NativePlayFab.PartyTextToSpeechProfileGetGender(Checked, &value));
            return (PartyGender)value;
        }
    }
}

/// <summary>
/// Projects <c>PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_HANDLE</c>: a stream the title reads
/// pre-encode audio buffers from.
/// </summary>
public sealed class PartyAudioManipulationSourceStream : PartyObject
{
    internal PartyAudioManipulationSourceStream(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>The format of the buffers the stream produces.</summary>
    public unsafe PartyAudioFormat Format
    {
        get
        {
            PARTY_AUDIO_FORMAT value;
            PartyInterop.Check(
                NativePlayFab.PartyAudioManipulationSourceStreamGetFormat(Checked, &value));
            return PartyAudioFormat.FromNative(&value);
        }
    }

    /// <summary>The configuration the stream was created with.</summary>
    public unsafe PartyAudioManipulationSourceStreamConfiguration Configuration
    {
        get
        {
            PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION value;
            PartyInterop.Check(
                NativePlayFab.PartyAudioManipulationSourceStreamGetConfiguration(Checked, &value));
            return PartyAudioManipulationSourceStreamConfiguration.FromNative(&value);
        }
    }

    /// <summary>How many buffers are waiting to be read.</summary>
    public unsafe uint AvailableBufferCount
    {
        get
        {
            uint value;
            PartyInterop.Check(
                NativePlayFab.PartyAudioManipulationSourceStreamGetAvailableBufferCount(
                    Checked, &value));
            return value;
        }
    }

    /// <summary>
    /// Reads the next buffer, copying it into managed memory and immediately returning the native
    /// buffer to Party.
    /// </summary>
    /// <returns>The audio payload, or <see langword="null"/> when no buffer is available.</returns>
    public unsafe byte[]? ReadNextBuffer()
    {
        PARTY_MUTABLE_DATA_BUFFER buffer;
        PartyInterop.Check(
            NativePlayFab.PartyAudioManipulationSourceStreamGetNextBuffer(Checked, &buffer));
        if (buffer.Buffer is null || buffer.BufferByteCount == 0)
        {
            return null;
        }

        byte[] payload = PartyInterop.ReadBuffer(buffer.Buffer, buffer.BufferByteCount);
        PartyInterop.Check(NativePlayFab.PartyAudioManipulationSourceStreamReturnBuffer(
            Checked, buffer.Buffer));
        return payload;
    }
}

/// <summary>
/// Projects <c>PARTY_AUDIO_MANIPULATION_SINK_STREAM_HANDLE</c>: a stream the title writes
/// processed audio buffers to.
/// </summary>
public sealed class PartyAudioManipulationSinkStream : PartyObject
{
    internal PartyAudioManipulationSinkStream(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>The format the stream expects.</summary>
    public unsafe PartyAudioFormat Format
    {
        get
        {
            PARTY_AUDIO_FORMAT value;
            PartyInterop.Check(
                NativePlayFab.PartyAudioManipulationSinkStreamGetFormat(Checked, &value));
            return PartyAudioFormat.FromNative(&value);
        }
    }

    /// <summary>The configuration the stream was created with.</summary>
    public unsafe PartyAudioManipulationSinkStreamConfiguration Configuration
    {
        get
        {
            PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION value;
            PartyInterop.Check(
                NativePlayFab.PartyAudioManipulationSinkStreamGetConfiguration(Checked, &value));
            return PartyAudioManipulationSinkStreamConfiguration.FromNative(&value);
        }
    }

    /// <summary>Submits a buffer of audio to the stream.</summary>
    /// <param name="audio">The audio payload in the stream's format.</param>
    public unsafe void SubmitBuffer(byte[] audio)
    {
        if (audio is null)
        {
            throw new ArgumentNullException(nameof(audio));
        }

        if (audio.Length == 0)
        {
            return;
        }

        fixed (byte* source = audio)
        {
            var buffer = default(PARTY_DATA_BUFFER);
            buffer.Buffer = source;
            buffer.BufferByteCount = (uint)audio.Length;
            PartyInterop.Check(
                NativePlayFab.PartyAudioManipulationSinkStreamSubmitBuffer(Checked, &buffer));
        }
    }
}
