using System;

namespace GDK.Net.XboxLive;

/// <summary>
/// State of the websocket connection to the Xbox Live real-time activity service. Mirrors
/// <c>XblRealTimeActivityConnectionState</c>.
/// </summary>
public enum RealTimeActivityConnectionState : uint
{
    /// <summary>The websocket is connected to the real-time activity service.</summary>
    Connected = 0,

    /// <summary>XSAPI is connecting the websocket to the real-time activity service.</summary>
    Connecting = 1,

    /// <summary>The websocket is disconnected from the real-time activity service.</summary>
    Disconnected = 2,
}

/// <summary>
/// Payload for <see cref="RealTimeActivityService.ConnectionStateChanged"/>.
/// </summary>
public sealed class RealTimeActivityConnectionStateChangedEventArgs : EventArgs
{
    internal RealTimeActivityConnectionStateChangedEventArgs(RealTimeActivityConnectionState state) =>
        State = state;

    /// <summary>The new websocket connection state.</summary>
    public RealTimeActivityConnectionState State { get; }
}
