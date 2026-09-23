using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// The PlayFab Party library (<c>Party.h</c>): low-latency networking and voice/text chat, driven
/// by a per-frame state-change pump.
/// </summary>
/// <remarks>
/// <para>
/// Party operations are not awaitable. A start call returns synchronously with a
/// <see cref="PartyOperationId"/>, and its completion arrives later as a record from
/// <see cref="ProcessStateChanges"/>, which must be pumped once per frame from the title's update
/// thread.
/// </para>
/// <para>
/// Native state-change memory is only valid between the library's <c>StartProcessingStateChanges</c>
/// and <c>FinishProcessingStateChanges</c> calls, which the enumerator brackets: the batch is
/// returned when the <c>foreach</c> leaves scope. Each record snapshots the values it exposes, so a
/// record may safely outlive the loop; object references stay valid until their teardown change
/// arrives.
/// </para>
/// </remarks>
public sealed unsafe class PartyManager : IDisposable
{
    private readonly Dictionary<IntPtr, PartyObject> _objects = new();
    private readonly IntPtr _handle;
    private long _nextOperation;
    private bool _disposed;

    private PartyManager(IntPtr handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Initializes the Party library for a title (<c>PartyInitialize</c>).
    /// </summary>
    /// <param name="titleId">The PlayFab title id the library authenticates against.</param>
    /// <remarks>
    /// Last in the fixed startup order (see <see cref="SubsystemOrder"/>) and therefore first to be
    /// torn down, so the Gaming Runtime must already be up. Party also has to be shut down before
    /// the process exits -- leaving it running terminates the process with
    /// <c>STATUS_STACK_BUFFER_OVERRUN</c> (<c>0xC0000409</c>) instead of exiting cleanly -- so the
    /// instance is registered for teardown by <see cref="GameRuntime.Dispose"/> in case the title
    /// does not dispose it itself.
    /// </remarks>
    /// <exception cref="InvalidOperationException">The Gaming Runtime is not initialized.</exception>
    public static PartyManager Initialize(string titleId)
    {
        if (string.IsNullOrEmpty(titleId))
        {
            throw new ArgumentException("A title id is required.", nameof(titleId));
        }

        RuntimeLifetime.RequireGameRuntime(nameof(NativePlayFab.PartyInitialize));

        IntPtr text = Utf8.Allocate(titleId);
        try
        {
            PARTY_INITIALIZATION_CONFIGURATION configuration = default;
            configuration.TitleId = (byte*)text;

            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyInitialize(&configuration, &handle));

            var manager = new PartyManager(handle);
            RuntimeLifetime.Register(SubsystemOrder.PlayFabParty, manager);
            return manager;
        }
        finally
        {
            Utf8.Free(text);
        }
    }

    /// <summary>The library's description of an error code (<c>PartyGetErrorMessage</c>).</summary>
    /// <param name="error">The <c>PartyError</c> value to describe.</param>
    public static string GetErrorMessage(uint error) => PartyInterop.DescribeParty(error);

    /// <summary>
    /// Pins one of the library's internal threads to a set of cores
    /// (<c>PartySetThreadAffinityMask</c>).
    /// </summary>
    /// <param name="threadId">The thread to configure.</param>
    /// <param name="affinityMask">The processor affinity mask, or zero for no restriction.</param>
    public static void SetThreadAffinityMask(PartyThreadId threadId, ulong affinityMask) =>
        PartyInterop.Check(
            NativePlayFab.PartySetThreadAffinityMask((PARTY_THREAD_ID)threadId, affinityMask));

    /// <summary>
    /// The processor affinity mask for one of the library's threads
    /// (<c>PartyGetThreadAffinityMask</c>).
    /// </summary>
    /// <param name="threadId">The thread to query.</param>
    public static ulong GetThreadAffinityMask(PartyThreadId threadId)
    {
        ulong mask;
        PartyInterop.Check(
            NativePlayFab.PartyGetThreadAffinityMask((PARTY_THREAD_ID)threadId, &mask));
        return mask;
    }

    /// <summary>
    /// Chooses whether Party drives one of its threads itself (<c>PartySetWorkMode</c>).
    /// </summary>
    /// <param name="threadId">The thread to configure.</param>
    /// <param name="workMode">The work mode to apply.</param>
    public static void SetWorkMode(PartyThreadId threadId, PartyWorkMode workMode) =>
        PartyInterop.Check(
            NativePlayFab.PartySetWorkMode((PARTY_THREAD_ID)threadId, (PARTY_WORK_MODE)workMode));

    /// <summary>The work mode of one of the library's threads (<c>PartyGetWorkMode</c>).</summary>
    /// <param name="threadId">The thread to query.</param>
    public static PartyWorkMode GetWorkMode(PartyThreadId threadId)
    {
        PARTY_WORK_MODE mode;
        PartyInterop.Check(NativePlayFab.PartyGetWorkMode((PARTY_THREAD_ID)threadId, &mode));
        return (PartyWorkMode)mode;
    }

    /// <summary>
    /// Performs one slice of work for a thread left in <see cref="PartyWorkMode.Manual"/>
    /// (<c>PartyDoWork</c>).
    /// </summary>
    /// <param name="threadId">The thread to pump.</param>
    public void DoWork(PartyThreadId threadId) =>
        PartyInterop.Check(NativePlayFab.PartyDoWork(Handle, (PARTY_THREAD_ID)threadId));

    /// <summary>
    /// Chooses the UDP socket Party binds for peer traffic
    /// (<c>PartySetOption</c> with <c>PartyOption::LocalUdpSocketBindAddress</c>).
    /// </summary>
    /// <param name="configuration">
    /// The bind configuration, or <see langword="null"/> to restore Party's default.
    /// </param>
    /// <remarks>
    /// <para>
    /// This is a process-wide setting and must be applied before <see cref="Initialize"/>: Party
    /// binds the socket during initialization, and changing it afterwards has no effect on the
    /// running instance.
    /// </para>
    /// <para>
    /// The default configuration binds a fixed port, so a second Party instance on the same machine
    /// fails to initialize a network with <c>FailedToBindToLocalUdpSocket</c>. Titles do not
    /// normally hit that, but any test that runs several clients on one box does -- give each
    /// process its own port, or pass a configuration with
    /// <see cref="PartyLocalUdpSocketBindAddressConfiguration.Port"/> 0 to let the platform
    /// assign one.
    /// </para>
    /// </remarks>
    public static void SetLocalUdpSocketBindAddress(
        PartyLocalUdpSocketBindAddressConfiguration? configuration)
    {
        if (configuration is null)
        {
            PartyInterop.Check(
                NativePlayFab.PartySetOption(null, PARTY_OPTION.LocalUdpSocketBindAddress, null));
            return;
        }

        PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_CONFIGURATION native = default;
        configuration.WriteTo(&native);
        PartyInterop.Check(
            NativePlayFab.PartySetOption(null, PARTY_OPTION.LocalUdpSocketBindAddress, &native));
    }

    /// <summary>
    /// The UDP socket configuration Party will bind, or has bound
    /// (<c>PartyGetOption</c> with <c>PartyOption::LocalUdpSocketBindAddress</c>).
    /// </summary>
    public static PartyLocalUdpSocketBindAddressConfiguration GetLocalUdpSocketBindAddress()
    {
        PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_CONFIGURATION native = default;
        PartyInterop.Check(
            NativePlayFab.PartyGetOption(null, PARTY_OPTION.LocalUdpSocketBindAddress, &native));
        return PartyLocalUdpSocketBindAddressConfiguration.FromNative(&native);
    }

    /// <summary>
    /// The Azure regions available to the title, ordered by measured latency
    /// (<c>PartyGetRegions</c>). The list is empty until the first
    /// <see cref="PartyRegionsChanged"/> state change arrives.
    /// </summary>
    public IReadOnlyList<PartyRegion> GetRegions()
    {
        uint count;
        PARTY_REGION* regions;
        PartyInterop.Check(NativePlayFab.PartyGetRegions(Handle, &count, &regions));
        return PartyStateChangeReader.ReadRegions(regions, count);
    }

    /// <summary>
    /// Creates a local user from an authenticated PlayFab entity
    /// (<c>PartyCreateLocalUser</c>).
    /// </summary>
    /// <param name="entity">The signed-in PlayFab entity the local user represents.</param>
    public PartyLocalUser CreateLocalUser(PlayFabEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        IntPtr localUser;
        PartyInterop.Check(
            NativePlayFab.PartyCreateLocalUser(Handle, entity.Handle, &localUser));
        return Wrap<PartyLocalUser>(localUser);
    }

    /// <summary>
    /// Starts destroying a local user (<c>PartyDestroyLocalUser</c>). Completion arrives as
    /// <see cref="PartyDestroyLocalUserCompleted"/>.
    /// </summary>
    /// <param name="localUser">The local user to destroy.</param>
    public PartyOperationId DestroyLocalUser(PartyLocalUser localUser)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        PartyOperationId operation = NextOperation();
        PartyInterop.Check(NativePlayFab.PartyDestroyLocalUser(
            Handle, localUser.Handle, Context(operation)));
        return operation;
    }

    /// <summary>The local users the title has created (<c>PartyGetLocalUsers</c>).</summary>
    public IReadOnlyList<PartyLocalUser> LocalUsers
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(NativePlayFab.PartyGetLocalUsers(Handle, &count, &handles));
            return WrapLocalUsers(handles, count);
        }
    }

    /// <summary>This device (<c>PartyGetLocalDevice</c>).</summary>
    public PartyDevice LocalDevice
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyGetLocalDevice(Handle, &handle));
            return Wrap<PartyDevice>(handle);
        }
    }

    /// <summary>The networks this device belongs to (<c>PartyGetNetworks</c>).</summary>
    public IReadOnlyList<PartyNetwork> Networks
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(NativePlayFab.PartyGetNetworks(Handle, &count, &handles));
            return WrapNetworks(handles, count);
        }
    }

    /// <summary>Every chat control visible to this device (<c>PartyGetChatControls</c>).</summary>
    public IReadOnlyList<PartyChatControl> ChatControls
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(NativePlayFab.PartyGetChatControls(Handle, &count, &handles));
            return WrapChatControls(handles, count);
        }
    }

    /// <summary>
    /// Starts creating a new network (<c>PartyCreateNewNetwork</c>). Completion arrives as
    /// <see cref="PartyCreateNewNetworkCompleted"/>.
    /// </summary>
    /// <param name="localUser">The local user that will own the network.</param>
    /// <param name="configuration">The network's size and connectivity limits.</param>
    /// <param name="regions">
    /// The Azure regions to consider, in preference order. Pass the list from
    /// <see cref="GetRegions"/> to let Party choose the lowest-latency region.
    /// </param>
    /// <param name="initialInvitation">
    /// The invitation to create alongside the network, or <see langword="null"/> for the default
    /// invitation that admits anyone.
    /// </param>
    /// <returns>
    /// The operation id, the descriptor to distribute to joiners, and the identifier of the
    /// invitation that was applied.
    /// </returns>
    public (PartyOperationId Operation, PartyNetworkDescriptor Descriptor, string InvitationIdentifier)
        CreateNewNetwork(
            PartyLocalUser localUser,
            PartyNetworkConfiguration configuration,
            IReadOnlyList<PartyRegion> regions,
            PartyInvitationConfiguration? initialInvitation = null)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (regions is null)
        {
            throw new ArgumentNullException(nameof(regions));
        }

        PartyOperationId operation = NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PARTY_NETWORK_CONFIGURATION native = default;
            configuration.WriteTo(&native);

            PARTY_REGION* nativeRegions = null;
            if (regions.Count > 0)
            {
                nativeRegions = arena.Alloc<PARTY_REGION>(regions.Count);
                for (int i = 0; i < regions.Count; i++)
                {
                    regions[i].WriteTo(&nativeRegions[i]);
                }
            }

            PARTY_INVITATION_CONFIGURATION invitation = default;
            PARTY_INVITATION_CONFIGURATION* invitationPtr = null;
            if (initialInvitation is not null)
            {
                initialInvitation.WriteTo(&invitation, arena);
                invitationPtr = &invitation;
            }

            PARTY_NETWORK_DESCRIPTOR descriptor;

            // PARTY_MAX_INVITATION_IDENTIFIER_STRING_LENGTH + 1.
            byte* identifier = stackalloc byte[128];
            PartyInterop.Check(NativePlayFab.PartyCreateNewNetwork(
                Handle,
                localUser.Handle,
                &native,
                (uint)regions.Count,
                nativeRegions,
                invitationPtr,
                Context(operation),
                &descriptor,
                identifier));

            return (
                operation,
                new PartyNetworkDescriptor(&descriptor),
                Utf8.ToString(identifier) ?? string.Empty);
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Starts connecting to an existing network (<c>PartyConnectToNetwork</c>). Completion arrives
    /// as <see cref="PartyConnectToNetworkCompleted"/>.
    /// </summary>
    /// <param name="descriptor">The descriptor published by the network's creator.</param>
    public (PartyOperationId Operation, PartyNetwork Network) ConnectToNetwork(
        PartyNetworkDescriptor descriptor)
    {
        if (descriptor is null)
        {
            throw new ArgumentNullException(nameof(descriptor));
        }

        PartyOperationId operation = NextOperation();
        PARTY_NETWORK_DESCRIPTOR native;
        descriptor.WriteTo(&native);

        IntPtr network;
        PartyInterop.Check(NativePlayFab.PartyConnectToNetwork(
            Handle, &native, Context(operation), &network));
        return (operation, Wrap<PartyNetwork>(network));
    }

    /// <summary>
    /// Starts a synchronization point across a set of endpoints
    /// (<c>PartySynchronizeMessagesBetweenEndpoints</c>). Completion arrives as
    /// <see cref="PartySynchronizeMessagesBetweenEndpointsCompleted"/>.
    /// </summary>
    /// <param name="endpoints">The endpoints to synchronize.</param>
    /// <param name="options">Which message classes participate in the synchronization.</param>
    public PartyOperationId SynchronizeMessagesBetweenEndpoints(
        IReadOnlyList<PartyEndpoint> endpoints,
        PartySynchronizeMessagesBetweenEndpointsOptions options =
            PartySynchronizeMessagesBetweenEndpointsOptions.None)
    {
        if (endpoints is null)
        {
            throw new ArgumentNullException(nameof(endpoints));
        }

        PartyOperationId operation = NextOperation();
        var arena = new PlayFabArena();
        try
        {
            IntPtr* handles = null;
            if (endpoints.Count > 0)
            {
                handles = arena.Alloc<IntPtr>(endpoints.Count);
                for (int i = 0; i < endpoints.Count; i++)
                {
                    handles[i] = endpoints[i].Handle;
                }
            }

            PartyInterop.Check(NativePlayFab.PartySynchronizeMessagesBetweenEndpoints(
                Handle,
                (uint)endpoints.Count,
                handles,
                (PARTY_SYNCHRONIZE_MESSAGES_BETWEEN_ENDPOINTS_OPTIONS)options,
                Context(operation)));
            return operation;
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>
    /// Drains the state-change queue. Enumerate it once per frame; leaving the <c>foreach</c>
    /// returns the batch to the library (<c>PartyStartProcessingStateChanges</c> /
    /// <c>PartyFinishProcessingStateChanges</c>).
    /// </summary>
    public PartyStateChangeCollection ProcessStateChanges() => new(this);

    /// <summary>
    /// Shuts the Party library down (<c>PartyCleanup</c>), invalidating every object it produced.
    /// </summary>
    /// <remarks>
    /// Bounded like the other native teardowns: the call runs on a background thread and is
    /// abandoned after <see cref="RuntimeLifetime.TeardownTimeout"/> so a stalled network teardown
    /// cannot stop a title's process from exiting. This object is disposed on return either way.
    /// </remarks>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        RuntimeLifetime.Unregister(this);

        foreach (PartyObject value in _objects.Values)
        {
            value.Invalidate();
        }

        _objects.Clear();

        IntPtr handle = _handle;
        uint error = 0;
        bool completed = RuntimeLifetime.RunBounded(
            () => error = NativePlayFab.PartyCleanup(handle),
            RuntimeLifetime.TeardownTimeout);

        if (completed)
        {
            PartyInterop.Check(error);
        }
    }

    internal IntPtr Handle
    {
        get
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PartyManager));
            }

            return _handle;
        }
    }

    internal static void* Context(PartyOperationId operation) => (void*)(IntPtr)operation.Value;

    internal static PartyOperationId FromContext(void* context) => new((long)(IntPtr)context);

    internal PartyOperationId NextOperation() => new(Interlocked.Increment(ref _nextOperation));

    internal PartyLocalUser WrapLocalUser(IntPtr handle) => Wrap<PartyLocalUser>(handle);

    internal IReadOnlyList<PartyLocalUser> WrapLocalUsers(IntPtr* handles, uint count) =>
        WrapMany<PartyLocalUser>(handles, count);

    internal PartyDevice WrapDevice(IntPtr handle) => Wrap<PartyDevice>(handle);

    internal IReadOnlyList<PartyDevice> WrapDevices(IntPtr* handles, uint count) =>
        WrapMany<PartyDevice>(handles, count);

    internal PartyEndpoint WrapEndpoint(IntPtr handle) => Wrap<PartyEndpoint>(handle);

    internal IReadOnlyList<PartyEndpoint> WrapEndpoints(IntPtr* handles, uint count) =>
        WrapMany<PartyEndpoint>(handles, count);

    internal PartyNetwork WrapNetwork(IntPtr handle) => Wrap<PartyNetwork>(handle);

    internal IReadOnlyList<PartyNetwork> WrapNetworks(IntPtr* handles, uint count) =>
        WrapMany<PartyNetwork>(handles, count);

    internal PartyChatControl WrapChatControl(IntPtr handle) => Wrap<PartyChatControl>(handle);

    internal IReadOnlyList<PartyChatControl> WrapChatControls(IntPtr* handles, uint count) =>
        WrapMany<PartyChatControl>(handles, count);

    internal PartyInvitation WrapInvitation(IntPtr handle) => Wrap<PartyInvitation>(handle);

    internal IReadOnlyList<PartyInvitation> WrapInvitations(IntPtr* handles, uint count) =>
        WrapMany<PartyInvitation>(handles, count);

    internal PartyTextToSpeechProfile WrapTextToSpeechProfile(IntPtr handle) =>
        Wrap<PartyTextToSpeechProfile>(handle);

    internal IReadOnlyList<PartyTextToSpeechProfile> WrapTextToSpeechProfiles(
        IntPtr* handles, uint count) => WrapMany<PartyTextToSpeechProfile>(handles, count);

    internal PartyAudioManipulationSourceStream WrapSourceStream(IntPtr handle) =>
        Wrap<PartyAudioManipulationSourceStream>(handle);

    internal PartyAudioManipulationSinkStream WrapSinkStream(IntPtr handle) =>
        Wrap<PartyAudioManipulationSinkStream>(handle);

    internal T Wrap<T>(IntPtr handle)
        where T : PartyObject
    {
        if (handle == IntPtr.Zero)
        {
            throw new ArgumentException("A null Party handle cannot be projected.", nameof(handle));
        }

        if (!_objects.TryGetValue(handle, out PartyObject? existing))
        {
            existing = Create<T>(handle);
            _objects[handle] = existing;
        }

        return (T)existing;
    }

    internal T? WrapOrNull<T>(IntPtr handle)
        where T : PartyObject => handle == IntPtr.Zero ? null : Wrap<T>(handle);

    internal T? Find<T>(IntPtr handle)
        where T : PartyObject => WrapOrNull<T>(handle);

    internal void Retire(PartyObject? value)
    {
        if (value is null)
        {
            return;
        }

        value.Invalidate();
        _objects.Remove(value.Handle);
    }

    private PartyObject Create<T>(IntPtr handle)
        where T : PartyObject
    {
        if (typeof(T) == typeof(PartyLocalUser))
        {
            return new PartyLocalUser(this, handle);
        }

        if (typeof(T) == typeof(PartyDevice))
        {
            return new PartyDevice(this, handle);
        }

        if (typeof(T) == typeof(PartyEndpoint))
        {
            return new PartyEndpoint(this, handle);
        }

        if (typeof(T) == typeof(PartyNetwork))
        {
            return new PartyNetwork(this, handle);
        }

        if (typeof(T) == typeof(PartyChatControl))
        {
            return new PartyChatControl(this, handle);
        }

        if (typeof(T) == typeof(PartyInvitation))
        {
            return new PartyInvitation(this, handle);
        }

        if (typeof(T) == typeof(PartyTextToSpeechProfile))
        {
            return new PartyTextToSpeechProfile(this, handle);
        }

        if (typeof(T) == typeof(PartyAudioManipulationSourceStream))
        {
            return new PartyAudioManipulationSourceStream(this, handle);
        }

        if (typeof(T) == typeof(PartyAudioManipulationSinkStream))
        {
            return new PartyAudioManipulationSinkStream(this, handle);
        }

        throw new NotSupportedException($"{typeof(T)} is not a Party object type.");
    }

    private IReadOnlyList<T> WrapMany<T>(IntPtr* handles, uint count)
        where T : PartyObject
    {
        if (handles is null || count == 0)
        {
            return Array.Empty<T>();
        }

        var result = new T[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = Wrap<T>(handles[i]);
        }

        return result;
    }
}

/// <summary>
/// The Party state changes produced by one pump. Enumerating starts the batch; leaving the loop
/// returns it to the Party library.
/// </summary>
public readonly struct PartyStateChangeCollection : IEnumerable<PartyStateChange>
{
    private readonly PartyManager _owner;

    internal PartyStateChangeCollection(PartyManager owner)
    {
        _owner = owner;
    }

    /// <summary>Starts a batch of state changes.</summary>
    public Enumerator GetEnumerator() => new(_owner);

    /// <inheritdoc/>
    IEnumerator<PartyStateChange> IEnumerable<PartyStateChange>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Walks one batch of state changes and returns it on <see cref="Dispose"/>.</summary>
    public unsafe struct Enumerator : IEnumerator<PartyStateChange>
    {
        private readonly PartyManager _owner;
        private readonly PARTY_STATE_CHANGE** _changes;
        private readonly uint _count;
        private readonly List<PartyObject> _retired;
        private uint _index;
        private PartyStateChange? _current;
        private bool _finished;

        internal Enumerator(PartyManager owner)
        {
            _owner = owner;
            _retired = new List<PartyObject>();
            _index = 0;
            _current = null;
            _finished = false;

            uint count;
            PARTY_STATE_CHANGE** changes;
            PartyInterop.Check(NativePlayFab.PartyStartProcessingStateChanges(
                owner.Handle, &count, &changes));
            _count = count;
            _changes = changes;
        }

        /// <inheritdoc/>
        public PartyStateChange Current => _current!;

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

            _current = PartyStateChangeReader.Read(_owner, _changes[_index], _retired);
            _index++;
            return true;
        }

        /// <summary>Returns the batch to the Party library and retires torn-down objects.</summary>
        public void Dispose()
        {
            if (_finished)
            {
                return;
            }

            _finished = true;
            PartyInterop.Check(NativePlayFab.PartyFinishProcessingStateChanges(
                _owner.Handle, _count, _changes));

            foreach (PartyObject value in _retired)
            {
                _owner.Retire(value);
            }
        }

        /// <inheritdoc/>
        void IEnumerator.Reset() => throw new NotSupportedException();
    }
}
