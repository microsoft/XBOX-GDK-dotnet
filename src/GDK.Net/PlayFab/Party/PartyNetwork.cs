using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// Projects <c>PARTY_NETWORK_HANDLE</c>: a Party network the local device has joined.
/// </summary>
public sealed class PartyNetwork : PartyObject
{
    internal PartyNetwork(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>The descriptor that other devices need in order to connect.</summary>
    public unsafe PartyNetworkDescriptor Descriptor
    {
        get
        {
            PARTY_NETWORK_DESCRIPTOR value;
            PartyInterop.Check(
                NativePlayFab.PartyNetworkGetNetworkDescriptor(Checked, &value));
            return new PartyNetworkDescriptor(&value);
        }
    }

    /// <summary>The network's size and connectivity limits.</summary>
    public unsafe PartyNetworkConfiguration Configuration
    {
        get
        {
            PARTY_NETWORK_CONFIGURATION* value;
            PartyInterop.Check(
                NativePlayFab.PartyNetworkGetNetworkConfiguration(Checked, &value));
            return PartyNetworkConfiguration.FromNative(value);
        }
    }

    /// <summary>Every endpoint currently in the network.</summary>
    public unsafe IReadOnlyList<PartyEndpoint> Endpoints
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyNetworkGetEndpoints(Checked, &count, &handles));
            return Owner.WrapEndpoints(handles, count);
        }
    }

    /// <summary>Every device currently in the network.</summary>
    public unsafe IReadOnlyList<PartyDevice> Devices
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(NativePlayFab.PartyNetworkGetDevices(Checked, &count, &handles));
            return Owner.WrapDevices(handles, count);
        }
    }

    /// <summary>The local users authenticated into the network.</summary>
    public unsafe IReadOnlyList<PartyLocalUser> LocalUsers
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyNetworkGetLocalUsers(Checked, &count, &handles));
            return Owner.WrapLocalUsers(handles, count);
        }
    }

    /// <summary>The chat controls connected to the network.</summary>
    public unsafe IReadOnlyList<PartyChatControl> ChatControls
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyNetworkGetChatControls(Checked, &count, &handles));
            return Owner.WrapChatControls(handles, count);
        }
    }

    /// <summary>The invitations currently outstanding for the network.</summary>
    public unsafe IReadOnlyList<PartyInvitation> Invitations
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyNetworkGetInvitations(Checked, &count, &handles));
            return Owner.WrapInvitations(handles, count);
        }
    }

    /// <summary>Filters the network's endpoints by owner and location.</summary>
    /// <param name="userTypeFilter">Whether to include user endpoints, device endpoints or both.</param>
    /// <param name="locationFilter">Whether to include local endpoints, remote endpoints or both.</param>
    public unsafe IReadOnlyList<PartyEndpoint> GetEndpoints(
        PartyEndpointUserTypeFilter userTypeFilter, PartyEndpointLocationFilter locationFilter)
    {
        uint count;
        IntPtr* handles;
        PartyInterop.Check(NativePlayFab.PartyNetworkGetEndpointsByUserType(
            Checked,
            (PARTY_ENDPOINT_USER_TYPE_FILTER)userTypeFilter,
            (PARTY_ENDPOINT_LOCATION_FILTER)locationFilter,
            &count,
            &handles));
        return Owner.WrapEndpoints(handles, count);
    }

    /// <summary>Looks up an endpoint by its per-network unique identifier.</summary>
    /// <param name="uniqueIdentifier">The identifier from <see cref="PartyEndpoint.UniqueIdentifier"/>.</param>
    /// <returns>The endpoint, or <see langword="null"/> when no endpoint matches.</returns>
    public unsafe PartyEndpoint? FindEndpoint(ushort uniqueIdentifier)
    {
        IntPtr handle;
        PartyInterop.Check(NativePlayFab.PartyNetworkFindEndpointByUniqueIdentifier(
            Checked, uniqueIdentifier, &handle));
        return handle == IntPtr.Zero ? null : Owner.WrapEndpoint(handle);
    }

    /// <summary>How the local device reaches a remote device in this network.</summary>
    /// <param name="device">The remote device.</param>
    public unsafe PartyDeviceConnectionType GetDeviceConnectionType(PartyDevice device)
    {
        if (device is null)
        {
            throw new ArgumentNullException(nameof(device));
        }

        PARTY_DEVICE_CONNECTION_TYPE value;
        PartyInterop.Check(NativePlayFab.PartyNetworkGetDeviceConnectionType(
            Checked, device.Handle, &value));
        return (PartyDeviceConnectionType)value;
    }

    /// <summary>Reads network-wide statistics.</summary>
    /// <param name="statistics">The statistics to read.</param>
    public unsafe IReadOnlyDictionary<PartyNetworkStatistic, ulong> GetStatistics(
        IReadOnlyList<PartyNetworkStatistic> statistics)
    {
        if (statistics is null)
        {
            throw new ArgumentNullException(nameof(statistics));
        }

        var result = new Dictionary<PartyNetworkStatistic, ulong>(statistics.Count);
        if (statistics.Count == 0)
        {
            return result;
        }

        var arena = new PlayFabArena();
        try
        {
            PARTY_NETWORK_STATISTIC* types = arena.Alloc<PARTY_NETWORK_STATISTIC>(statistics.Count);
            ulong* values = arena.Alloc<ulong>(statistics.Count);
            for (int i = 0; i < statistics.Count; i++)
            {
                types[i] = (PARTY_NETWORK_STATISTIC)statistics[i];
            }

            PartyInterop.Check(NativePlayFab.PartyNetworkGetNetworkStatistics(
                Checked, (uint)statistics.Count, types, values));

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

    /// <summary>The keys of every property shared on the network.</summary>
    public IReadOnlyList<string> GetSharedPropertyKeys() =>
        PartyProperties.GetKeys(PartyPropertyOwner.Network, Checked);

    /// <summary>Reads a shared property, or <see langword="null"/> when it is not set.</summary>
    /// <param name="key">The property key.</param>
    public byte[]? GetSharedProperty(string key) =>
        PartyProperties.Get(PartyPropertyOwner.Network, Checked, key);

    /// <summary>Sets or, for a <see langword="null"/> value, removes shared properties.</summary>
    /// <param name="properties">The properties to write.</param>
    public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties) =>
        PartyProperties.Set(PartyPropertyOwner.Network, Checked, properties);

    /// <summary>Starts authenticating a local user into the network.</summary>
    /// <param name="localUser">The local user to authenticate.</param>
    /// <param name="invitationIdentifier">
    /// The invitation that admits the user, or <see langword="null"/> when the network's initial
    /// invitation applies.
    /// </param>
    public unsafe PartyOperationId AuthenticateLocalUser(
        PartyLocalUser localUser, string? invitationIdentifier = null)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyNetworkAuthenticateLocalUser(
                Checked,
                localUser.Handle,
                arena.String(invitationIdentifier),
                PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts removing a local user from the network.</summary>
    /// <param name="localUser">The local user to remove.</param>
    public unsafe PartyOperationId RemoveLocalUser(PartyLocalUser localUser)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyNetworkRemoveLocalUser(
            Checked, localUser.Handle, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts creating an additional invitation to the network.</summary>
    /// <param name="localUser">The local user that owns the invitation.</param>
    /// <param name="configuration">The invitation's identifier, revocability and allow list.</param>
    public unsafe PartyOperationId CreateInvitation(
        PartyLocalUser localUser, PartyInvitationConfiguration configuration)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PARTY_INVITATION_CONFIGURATION* native =
                arena.Alloc<PARTY_INVITATION_CONFIGURATION>(1);
            configuration.WriteTo(native, arena);
            IntPtr invitation;
            PartyInterop.Check(NativePlayFab.PartyNetworkCreateInvitation(
                Checked,
                localUser.Handle,
                native,
                PartyManager.Context(operation),
                &invitation));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts revoking an invitation to the network.</summary>
    /// <param name="localUser">The local user revoking the invitation.</param>
    /// <param name="invitation">The invitation to revoke.</param>
    public unsafe PartyOperationId RevokeInvitation(
        PartyLocalUser localUser, PartyInvitation invitation)
    {
        if (localUser is null)
        {
            throw new ArgumentNullException(nameof(localUser));
        }

        if (invitation is null)
        {
            throw new ArgumentNullException(nameof(invitation));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyNetworkRevokeInvitation(
            Checked, localUser.Handle, invitation.Handle, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts creating a messaging endpoint in the network.</summary>
    /// <param name="localUser">
    /// The local user that owns the endpoint, or <see langword="null"/> for a device endpoint.
    /// </param>
    /// <param name="initialProperties">Properties published with the endpoint.</param>
    public unsafe PartyOperationId CreateEndpoint(
        PartyLocalUser? localUser,
        IReadOnlyDictionary<string, byte[]?>? initialProperties = null)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            uint count = initialProperties is null ? 0u : (uint)initialProperties.Count;
            byte** keys = null;
            PARTY_DATA_BUFFER* values = null;
            if (count > 0)
            {
                keys = (byte**)arena.Alloc<IntPtr>((int)count);
                values = arena.Alloc<PARTY_DATA_BUFFER>((int)count);
                int index = 0;
                foreach (KeyValuePair<string, byte[]?> pair in initialProperties!)
                {
                    keys[index] = arena.String(pair.Key);
                    if (pair.Value is null || pair.Value.Length == 0)
                    {
                        values[index].Buffer = null;
                        values[index].BufferByteCount = 0;
                    }
                    else
                    {
                        byte* buffer = arena.Alloc<byte>(pair.Value.Length);
                        fixed (byte* source = pair.Value)
                        {
                            Buffer.MemoryCopy(
                                source, buffer, pair.Value.Length, pair.Value.Length);
                        }

                        values[index].Buffer = buffer;
                        values[index].BufferByteCount = (uint)pair.Value.Length;
                    }

                    index++;
                }
            }

            IntPtr endpoint;
            PartyInterop.Check(NativePlayFab.PartyNetworkCreateEndpoint(
                Checked,
                localUser is null ? IntPtr.Zero : localUser.Handle,
                count,
                keys,
                values,
                PartyManager.Context(operation),
                &endpoint));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts destroying a local endpoint.</summary>
    /// <param name="endpoint">The endpoint to destroy.</param>
    public unsafe PartyOperationId DestroyEndpoint(PartyEndpoint endpoint)
    {
        if (endpoint is null)
        {
            throw new ArgumentNullException(nameof(endpoint));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyNetworkDestroyEndpoint(
            Checked, endpoint.Handle, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts connecting a local chat control to the network.</summary>
    /// <param name="chatControl">The chat control to connect.</param>
    public unsafe PartyOperationId ConnectChatControl(PartyChatControl chatControl)
    {
        if (chatControl is null)
        {
            throw new ArgumentNullException(nameof(chatControl));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyNetworkConnectChatControl(
            Checked, chatControl.Handle, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts disconnecting a local chat control from the network.</summary>
    /// <param name="chatControl">The chat control to disconnect.</param>
    public unsafe PartyOperationId DisconnectChatControl(PartyChatControl chatControl)
    {
        if (chatControl is null)
        {
            throw new ArgumentNullException(nameof(chatControl));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyNetworkDisconnectChatControl(
            Checked, chatControl.Handle, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts removing a device from the network.</summary>
    /// <param name="device">The device to kick.</param>
    public unsafe PartyOperationId KickDevice(PartyDevice device)
    {
        if (device is null)
        {
            throw new ArgumentNullException(nameof(device));
        }

        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyNetworkKickDevice(
            Checked, device.Handle, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts removing a user from the network.</summary>
    /// <param name="entityId">The PlayFab entity id of the user to kick.</param>
    public unsafe PartyOperationId KickUser(string entityId)
    {
        if (entityId is null)
        {
            throw new ArgumentNullException(nameof(entityId));
        }

        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyNetworkKickUser(
                Checked, arena.String(entityId), PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts leaving the network.</summary>
    public unsafe PartyOperationId Leave()
    {
        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(
            NativePlayFab.PartyNetworkLeaveNetwork(Checked, PartyManager.Context(operation)));
        return operation;
    }
}
