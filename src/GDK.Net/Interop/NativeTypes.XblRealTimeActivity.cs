// Blittable mirrors of the XSAPI real-time activity types -- xsapi-c\real_time_activity_c.h,
// GDK edition 260404.
//
// XblRealTimeActivitySubscriptionState has no mirror here: the two APIs that produced it are
// deprecated and documented as always reporting Unknown, so the projection does not bind them.

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblRealTimeActivityConnectionState</c>.</summary>
internal enum XblRealTimeActivityConnectionState : uint
{
    Connected = 0,
    Connecting = 1,
    Disconnected = 2,
}
