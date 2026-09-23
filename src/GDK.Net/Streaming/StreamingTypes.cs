using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.Streaming;

/// <summary>
/// Identifies a streaming client connected to the game. Wraps the native
/// <c>XGameStreamingClientId</c> (a <c>uint64_t</c>).
/// </summary>
public readonly struct StreamingClientId : IEquatable<StreamingClientId>
{
    /// <summary>The null client id (<c>XGameStreamingNullClientId = 0</c>).</summary>
    public static StreamingClientId Null => default;

    internal StreamingClientId(ulong value) => Value = value;

    /// <summary>The raw 64-bit client id.</summary>
    public ulong Value { get; }

    /// <summary><see langword="true"/> when this is the null (unset) client id.</summary>
    public bool IsNull => Value == 0;

    /// <inheritdoc/>
    public bool Equals(StreamingClientId other) => Value == other.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is StreamingClientId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>Returns the client id formatted as a hexadecimal diagnostic string.</summary>
    public override string ToString() => $"StreamingClient(0x{Value:X16})";

    /// <summary>Equality operator.</summary>
    public static bool operator ==(StreamingClientId left, StreamingClientId right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(StreamingClientId left, StreamingClientId right) => !left.Equals(right);
}

/// <summary>The connection state of a streaming client. Mirrors <c>XGameStreamingConnectionState</c>.</summary>
public enum StreamingConnectionState : uint
{
    /// <summary>The client is not connected.</summary>
    Disconnected = 0,

    /// <summary>The client is connected and streaming.</summary>
    Connected = 1,
}

/// <summary>Which client property changed. Mirrors <c>XGameStreamingClientProperty</c>.</summary>
public enum StreamingClientProperty : uint
{
    /// <summary>No client property.</summary>
    None                     = 0,

    /// <summary>The client's stream physical dimensions changed.</summary>
    StreamPhysicalDimensions = 1,

    /// <summary>The client's touch-input enabled state changed.</summary>
    TouchInputEnabled        = 2,

    /// <summary>The client's touch bundle version changed.</summary>
    TouchBundleVersion       = 4,

    /// <summary>The client's IP address changed.</summary>
    IPAddress                = 5,

    /// <summary>The client's session id changed.</summary>
    SessionId                = 6,

    /// <summary>The client's display details changed.</summary>
    DisplayDetails           = 7,
}

/// <summary>Video capabilities reported by a streaming client. Mirrors <c>XGameStreamingVideoFlags</c>.</summary>
[Flags]
public enum StreamingVideoFlags : uint
{
    /// <summary>No special capabilities.</summary>
    None                      = 0x0,

    /// <summary>The client supports a custom aspect ratio.</summary>
    SupportsCustomAspectRatio = 0x1,

    /// <summary>The client supports present scaling.</summary>
    SupportsPresentScaling    = 0x2,

    /// <summary>All capability flags.</summary>
    All                       = 0x3,
}

/// <summary>The kind of touch-controls state operation. Mirrors <c>XGameStreamingTouchControlsStateOperationKind</c>.</summary>
public enum TouchControlsStateOperationKind : uint
{
    /// <summary>Replace the target state value.</summary>
    Replace = 0,
}

/// <summary>The kind of value carried by a <see cref="TouchControlsStateValue"/>. Mirrors <c>XGameStreamingTouchControlsStateValueKind</c>.</summary>
public enum TouchControlsStateValueKind : uint
{
    /// <summary>A Boolean state value.</summary>
    Boolean = 0,

    /// <summary>A signed integer state value.</summary>
    Integer = 1,

    /// <summary>A double-precision floating-point state value.</summary>
    Double  = 2,

    /// <summary>A string state value.</summary>
    String  = 3,
}

/// <summary>
/// A typed value for a touch-controls state operation.
/// Use the static factory methods <see cref="FromBoolean"/>, <see cref="FromInteger"/>,
/// <see cref="FromDouble"/>, and <see cref="FromString"/> to construct instances.
/// </summary>
public readonly struct TouchControlsStateValue
{
    private readonly TouchControlsStateValueKind _kind;
    private readonly long   _integer;
    private readonly double _double;
    private readonly string? _string;

    private TouchControlsStateValue(TouchControlsStateValueKind kind, long integer, double @double, string? str)
    {
        _kind    = kind;
        _integer = integer;
        _double  = @double;
        _string  = str;
    }

    /// <summary>The kind of value stored.</summary>
    public TouchControlsStateValueKind Kind => _kind;

    /// <summary>The boolean value when <see cref="Kind"/> is <see cref="TouchControlsStateValueKind.Boolean"/>.</summary>
    public bool BooleanValue => _integer != 0;

    /// <summary>The integer value when <see cref="Kind"/> is <see cref="TouchControlsStateValueKind.Integer"/>.</summary>
    public long IntegerValue => _integer;

    /// <summary>The double value when <see cref="Kind"/> is <see cref="TouchControlsStateValueKind.Double"/>.</summary>
    public double DoubleValue => _double;

    /// <summary>The string value when <see cref="Kind"/> is <see cref="TouchControlsStateValueKind.String"/>.</summary>
    public string? StringValue => _string;

    /// <summary>Creates a boolean state value (<c>XGameStreamingTouchControlsStateValueKind::Boolean</c>).</summary>
    public static TouchControlsStateValue FromBoolean(bool value) =>
        new(TouchControlsStateValueKind.Boolean, value ? 1L : 0L, 0.0, null);

    /// <summary>Creates an integer state value (<c>XGameStreamingTouchControlsStateValueKind::Integer</c>).</summary>
    public static TouchControlsStateValue FromInteger(long value) =>
        new(TouchControlsStateValueKind.Integer, value, 0.0, null);

    /// <summary>Creates a double state value (<c>XGameStreamingTouchControlsStateValueKind::Double</c>).</summary>
    public static TouchControlsStateValue FromDouble(double value) =>
        new(TouchControlsStateValueKind.Double, 0L, value, null);

    /// <summary>Creates a string state value (<c>XGameStreamingTouchControlsStateValueKind::String</c>).</summary>
    public static TouchControlsStateValue FromString(string? value) =>
        new(TouchControlsStateValueKind.String, 0L, 0.0, value);
}

/// <summary>
/// A single touch-controls state update operation passed to
/// <see cref="StreamingManager.UpdateTouchControlsState(IReadOnlyList{TouchControlsStateOperation}?)"/> and related methods.
/// Mirrors <c>XGameStreamingTouchControlsStateOperation</c>.
/// </summary>
public sealed class TouchControlsStateOperation
{
    /// <summary>
    /// Initializes a new operation.
    /// </summary>
    /// <param name="path">The JSON Pointer path of the state variable to update.</param>
    /// <param name="value">The new value for the state variable.</param>
    /// <param name="kind">The operation kind; defaults to <see cref="TouchControlsStateOperationKind.Replace"/>.</param>
    public TouchControlsStateOperation(
        string path,
        TouchControlsStateValue value,
        TouchControlsStateOperationKind kind = TouchControlsStateOperationKind.Replace)
    {
        if (path is null) throw new ArgumentNullException(nameof(path));
        Path  = path;
        Value = value;
        Kind  = kind;
    }

    /// <summary>The operation kind (<c>XGameStreamingTouchControlsStateOperationKind</c>).</summary>
    public TouchControlsStateOperationKind Kind { get; }

    /// <summary>JSON Pointer path of the state variable.</summary>
    public string Path { get; }

    /// <summary>The value to write.</summary>
    public TouchControlsStateValue Value { get; }
}

/// <summary>Physical dimensions of the streaming client's display, in millimetres.</summary>
public readonly struct StreamingPhysicalDimensions
{
    internal StreamingPhysicalDimensions(uint horizontalMm, uint verticalMm)
    {
        HorizontalMm = horizontalMm;
        VerticalMm   = verticalMm;
    }

    /// <summary>Horizontal physical extent in millimetres.</summary>
    public uint HorizontalMm { get; }

    /// <summary>Vertical physical extent in millimetres.</summary>
    public uint VerticalMm { get; }
}

/// <summary>Latency statistics reported by <c>XGameStreamingGetStreamAddedLatency</c>.</summary>
public readonly struct StreamingLatencyStats
{
    internal StreamingLatencyStats(uint avgInputUs, uint avgOutputUs, uint stdDevUs)
    {
        AverageInputLatencyUs    = avgInputUs;
        AverageOutputLatencyUs   = avgOutputUs;
        StandardDeviationUs      = stdDevUs;
    }

    /// <summary>Average input latency in microseconds.</summary>
    public uint AverageInputLatencyUs { get; }

    /// <summary>Average output latency in microseconds.</summary>
    public uint AverageOutputLatencyUs { get; }

    /// <summary>Standard deviation of latency in microseconds.</summary>
    public uint StandardDeviationUs { get; }
}

/// <summary>
/// Version and name of the touch-adaptation bundle active on a streaming client.
/// Returned by <see cref="StreamingManager.GetTouchBundleVersion"/>.
/// </summary>
public sealed class TouchBundleVersionInfo
{
    internal TouchBundleVersionInfo(Version version, string name)
    {
        Version = version;
        Name    = name;
    }

    /// <summary>The bundle version (<c>XVersion</c>).</summary>
    public Version Version { get; }

    /// <summary>The version name string.</summary>
    public string Name { get; }
}

/// <summary>
/// Display details for a streaming client, returned by
/// <see cref="StreamingManager.GetDisplayDetails"/>. Mirrors <c>XGameStreamingDisplayDetails</c>.
/// </summary>
public readonly struct StreamingDisplayDetails
{
    internal StreamingDisplayDetails(in XGameStreamingDisplayDetails raw)
    {
        PreferredWidth  = raw.PreferredWidth;
        PreferredHeight = raw.PreferredHeight;
        SafeAreaLeft    = raw.SafeAreaLeft;
        SafeAreaTop     = raw.SafeAreaTop;
        SafeAreaRight   = raw.SafeAreaRight;
        SafeAreaBottom  = raw.SafeAreaBottom;
        MaxPixels       = raw.MaxPixels;
        MaxWidth        = raw.MaxWidth;
        MaxHeight       = raw.MaxHeight;
        VideoFlags      = (StreamingVideoFlags)raw.Flags;
    }

    /// <summary>The client's preferred render width in pixels.</summary>
    public uint PreferredWidth { get; }

    /// <summary>The client's preferred render height in pixels.</summary>
    public uint PreferredHeight { get; }

    /// <summary>Left edge of the safe area (RECT.left).</summary>
    public int SafeAreaLeft { get; }

    /// <summary>Top edge of the safe area (RECT.top).</summary>
    public int SafeAreaTop { get; }

    /// <summary>Right edge of the safe area (RECT.right).</summary>
    public int SafeAreaRight { get; }

    /// <summary>Bottom edge of the safe area (RECT.bottom).</summary>
    public int SafeAreaBottom { get; }

    /// <summary>Maximum supported pixel count.</summary>
    public uint MaxPixels { get; }

    /// <summary>Maximum supported width in pixels.</summary>
    public uint MaxWidth { get; }

    /// <summary>Maximum supported height in pixels.</summary>
    public uint MaxHeight { get; }

    /// <summary>Video capability flags reported by the client.</summary>
    public StreamingVideoFlags VideoFlags { get; }
}

/// <summary>Payload for <see cref="StreamingManager.ConnectionStateChanged"/>.</summary>
public sealed class StreamingConnectionStateChangedEventArgs : EventArgs
{
    internal StreamingConnectionStateChangedEventArgs(StreamingClientId client, StreamingConnectionState state)
    {
        Client = client;
        State  = state;
    }

    /// <summary>The client whose connection state changed.</summary>
    public StreamingClientId Client { get; }

    /// <summary>The new connection state.</summary>
    public StreamingConnectionState State { get; }
}

/// <summary>Payload for <see cref="StreamingManager.ClientPropertiesChanged"/>.</summary>
public sealed class StreamingClientPropertiesChangedEventArgs : EventArgs
{
    internal StreamingClientPropertiesChangedEventArgs(
        StreamingClientId client,
        IReadOnlyList<StreamingClientProperty> updatedProperties)
    {
        Client            = client;
        UpdatedProperties = updatedProperties;
    }

    /// <summary>The client whose properties changed.</summary>
    public StreamingClientId Client { get; }

    /// <summary>Which properties changed.</summary>
    public IReadOnlyList<StreamingClientProperty> UpdatedProperties { get; }
}

/// <summary>
/// Reports which gamepad inputs came from physical hardware and which were synthesised by an
/// on-screen touch layout (<c>XGameStreamingGamepadPhysicality</c>).
/// </summary>
/// <remarks>
/// The low 32 bits are the <c>*Physical</c> flags and the high 32 bits the matching
/// <c>*Virtual</c> flags, so a single value describes both halves of a reading. Use it to suppress
/// gameplay that only makes sense for real hardware, such as rumble or aim assist tuning.
/// </remarks>
[Flags]
public enum StreamingGamepadPhysicality : ulong
{
    /// <summary>No inputs reported.</summary>
    None = 0x0000000000000000,

    /// <summary>D-pad up came from physical hardware.</summary>
    DPadUpPhysical = 0x0000000000000001,
    /// <summary>D-pad down came from physical hardware.</summary>
    DPadDownPhysical = 0x0000000000000002,
    /// <summary>D-pad left came from physical hardware.</summary>
    DPadLeftPhysical = 0x0000000000000004,
    /// <summary>D-pad right came from physical hardware.</summary>
    DPadRightPhysical = 0x0000000000000008,
    /// <summary>The Menu button came from physical hardware.</summary>
    MenuPhysical = 0x0000000000000010,
    /// <summary>The View button came from physical hardware.</summary>
    ViewPhysical = 0x0000000000000020,
    /// <summary>The left thumbstick click came from physical hardware.</summary>
    LeftThumbstickPhysical = 0x0000000000000040,
    /// <summary>The right thumbstick click came from physical hardware.</summary>
    RightThumbstickPhysical = 0x0000000000000080,
    /// <summary>The left shoulder button came from physical hardware.</summary>
    LeftShoulderPhysical = 0x0000000000000100,
    /// <summary>The right shoulder button came from physical hardware.</summary>
    RightShoulderPhysical = 0x0000000000000200,
    /// <summary>The A button came from physical hardware.</summary>
    APhysical = 0x0000000000001000,
    /// <summary>The B button came from physical hardware.</summary>
    BPhysical = 0x0000000000002000,
    /// <summary>The X button came from physical hardware.</summary>
    XPhysical = 0x0000000000004000,
    /// <summary>The Y button came from physical hardware.</summary>
    YPhysical = 0x0000000000008000,
    /// <summary>The left trigger came from physical hardware.</summary>
    LeftTriggerPhysical = 0x0000000000010000,
    /// <summary>The right trigger came from physical hardware.</summary>
    RightTriggerPhysical = 0x0000000000020000,
    /// <summary>The left thumbstick X axis came from physical hardware.</summary>
    LeftThumbstickXPhysical = 0x0000000000040000,
    /// <summary>The left thumbstick Y axis came from physical hardware.</summary>
    LeftThumbstickYPhysical = 0x0000000000080000,
    /// <summary>The right thumbstick X axis came from physical hardware.</summary>
    RightThumbstickXPhysical = 0x0000000000100000,
    /// <summary>The right thumbstick Y axis came from physical hardware.</summary>
    RightThumbstickYPhysical = 0x0000000000200000,
    /// <summary>All physical button flags.</summary>
    ButtonsPhysical = 0x000000000000F3FF,
    /// <summary>All physical analog flags.</summary>
    AnalogsPhysical = 0x00000000003F0000,
    /// <summary>All physical flags.</summary>
    AllPhysical = 0x00000000003FF3FF,

    /// <summary>D-pad up was synthesised by a touch layout.</summary>
    DPadUpVirtual = 0x0000000100000000,
    /// <summary>D-pad down was synthesised by a touch layout.</summary>
    DPadDownVirtual = 0x0000000200000000,
    /// <summary>D-pad left was synthesised by a touch layout.</summary>
    DPadLeftVirtual = 0x0000000400000000,
    /// <summary>D-pad right was synthesised by a touch layout.</summary>
    DPadRightVirtual = 0x0000000800000000,
    /// <summary>The Menu button was synthesised by a touch layout.</summary>
    MenuVirtual = 0x0000001000000000,
    /// <summary>The View button was synthesised by a touch layout.</summary>
    ViewVirtual = 0x0000002000000000,
    /// <summary>The left thumbstick click was synthesised by a touch layout.</summary>
    LeftThumbstickVirtual = 0x0000004000000000,
    /// <summary>The right thumbstick click was synthesised by a touch layout.</summary>
    RightThumbstickVirtual = 0x0000008000000000,
    /// <summary>The left shoulder button was synthesised by a touch layout.</summary>
    LeftShoulderVirtual = 0x0000010000000000,
    /// <summary>The right shoulder button was synthesised by a touch layout.</summary>
    RightShoulderVirtual = 0x0000020000000000,
    /// <summary>The A button was synthesised by a touch layout.</summary>
    AVirtual = 0x0000100000000000,
    /// <summary>The B button was synthesised by a touch layout.</summary>
    BVirtual = 0x0000200000000000,
    /// <summary>The X button was synthesised by a touch layout.</summary>
    XVirtual = 0x0000400000000000,
    /// <summary>The Y button was synthesised by a touch layout.</summary>
    YVirtual = 0x0000800000000000,
    /// <summary>The left trigger was synthesised by a touch layout.</summary>
    LeftTriggerVirtual = 0x0001000000000000,
    /// <summary>The right trigger was synthesised by a touch layout.</summary>
    RightTriggerVirtual = 0x0002000000000000,
    /// <summary>The left thumbstick X axis was synthesised by a touch layout.</summary>
    LeftThumbstickXVirtual = 0x0004000000000000,
    /// <summary>The left thumbstick Y axis was synthesised by a touch layout.</summary>
    LeftThumbstickYVirtual = 0x0008000000000000,
    /// <summary>The right thumbstick X axis was synthesised by a touch layout.</summary>
    RightThumbstickXVirtual = 0x0010000000000000,
    /// <summary>The right thumbstick Y axis was synthesised by a touch layout.</summary>
    RightThumbstickYVirtual = 0x0020000000000000,
    /// <summary>All virtual button flags.</summary>
    ButtonsVirtual = 0x0000F3FF00000000,
    /// <summary>All virtual analog flags.</summary>
    AnalogsVirtual = 0x003F000000000000,
    /// <summary>All virtual flags.</summary>
    AllVirtual = 0x003FF3FF00000000,
}
