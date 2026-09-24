// Raw interop types for XGameStreaming.h (GDK edition 260404).
// Sources: XGameStreaming.h, XGameRuntimeTypes.h.
// See Interop/NativeTypes.cs for conventions.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

internal enum XGameStreamingConnectionState : uint
{
    Disconnected = 0,
    Connected    = 1,
}

internal enum XGameStreamingClientProperty : uint
{
    None                    = 0,
    StreamPhysicalDimensions = 1,
    TouchInputEnabled        = 2,
    TouchBundleVersion       = 4,
    IPAddress                = 5,
    SessionId                = 6,
    DisplayDetails           = 7,
}

[Flags]
internal enum XGameStreamingVideoFlags : uint
{
    None                     = 0x0,
    SupportsCustomAspectRatio = 0x1,
    SupportsPresentScaling   = 0x2,
    All                      = 0x3,
}

internal enum XGameStreamingTouchControlsStateOperationKind : uint
{
    Replace = 0,
}

internal enum XGameStreamingTouchControlsStateValueKind : uint
{
    Boolean = 0,
    Integer = 1,
    Double  = 2,
    String  = 3,
}

/// <summary>
/// Mirrors <c>XGameStreamingTouchControlsStateValue</c> from XGameStreaming.h.
/// The anonymous union (bool / int64 / double / const char*) starts at offset 8 because
/// <c>int64_t</c> and pointer fields require 8-byte alignment. Total size: 16 bytes.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal unsafe struct XGameStreamingTouchControlsStateValue
{
    [FieldOffset(0)]  public XGameStreamingTouchControlsStateValueKind ValueKind;
    [FieldOffset(8)]  public byte   BooleanValue;   // bool in C++: first byte of the union
    [FieldOffset(8)]  public long   IntegerValue;   // int64_t
    [FieldOffset(8)]  public double DoubleValue;
    [FieldOffset(8)]  public byte*  StringValue;    // const char* (UTF-8)
}

/// <summary>
/// Mirrors <c>XGameStreamingTouchControlsStateOperation</c> from XGameStreaming.h.
/// Layout on x64/arm64: operationKind(4) + pad(4) + path*(8) + value(16) = 32 bytes.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal unsafe struct XGameStreamingTouchControlsStateOperation
{
    [FieldOffset(0)]  public XGameStreamingTouchControlsStateOperationKind OperationKind;
    [FieldOffset(8)]  public byte*                                          Path;   // const char* (UTF-8)
    [FieldOffset(16)] public XGameStreamingTouchControlsStateValue          Value;
}

/// <summary>
/// Mirrors <c>XGameStreamingDisplayDetails</c> from XGameStreaming.h.
/// The <c>RECT safeArea</c> is flattened to four <c>int</c> fields.
/// Total size: 40 bytes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XGameStreamingDisplayDetails
{
    public uint PreferredWidth;
    public uint PreferredHeight;
    public int  SafeAreaLeft;
    public int  SafeAreaTop;
    public int  SafeAreaRight;
    public int  SafeAreaBottom;
    public uint MaxPixels;
    public uint MaxWidth;
    public uint MaxHeight;
    public XGameStreamingVideoFlags Flags;
}

/// <summary>Mirrors <c>XGameStreamingGamepadPhysicality</c> from XGameStreaming.h.</summary>
/// <remarks>
/// A 64-bit flag enum. The low 32 bits report which inputs were produced by physical hardware; the
/// high 32 bits mirror them for inputs synthesised by an on-screen touch layout.
/// </remarks>
[Flags]
internal enum XGameStreamingGamepadPhysicality : ulong
{
    None = 0x0000000000000000,

    DPadUpPhysical = 0x0000000000000001,
    DPadDownPhysical = 0x0000000000000002,
    DPadLeftPhysical = 0x0000000000000004,
    DPadRightPhysical = 0x0000000000000008,
    MenuPhysical = 0x0000000000000010,
    ViewPhysical = 0x0000000000000020,
    LeftThumbstickPhysical = 0x0000000000000040,
    RightThumbstickPhysical = 0x0000000000000080,
    LeftShoulderPhysical = 0x0000000000000100,
    RightShoulderPhysical = 0x0000000000000200,
    APhysical = 0x0000000000001000,
    BPhysical = 0x0000000000002000,
    XPhysical = 0x0000000000004000,
    YPhysical = 0x0000000000008000,
    LeftTriggerPhysical = 0x0000000000010000,
    RightTriggerPhysical = 0x0000000000020000,
    LeftThumbstickXPhysical = 0x0000000000040000,
    LeftThumbstickYPhysical = 0x0000000000080000,
    RightThumbstickXPhysical = 0x0000000000100000,
    RightThumbstickYPhysical = 0x0000000000200000,
    ButtonsPhysical = 0x000000000000F3FF,
    AnalogsPhysical = 0x00000000003F0000,
    AllPhysical = 0x00000000003FF3FF,

    DPadUpVirtual = 0x0000000100000000,
    DPadDownVirtual = 0x0000000200000000,
    DPadLeftVirtual = 0x0000000400000000,
    DPadRightVirtual = 0x0000000800000000,
    MenuVirtual = 0x0000001000000000,
    ViewVirtual = 0x0000002000000000,
    LeftThumbstickVirtual = 0x0000004000000000,
    RightThumbstickVirtual = 0x0000008000000000,
    LeftShoulderVirtual = 0x0000010000000000,
    RightShoulderVirtual = 0x0000020000000000,
    AVirtual = 0x0000100000000000,
    BVirtual = 0x0000200000000000,
    XVirtual = 0x0000400000000000,
    YVirtual = 0x0000800000000000,
    LeftTriggerVirtual = 0x0001000000000000,
    RightTriggerVirtual = 0x0002000000000000,
    LeftThumbstickXVirtual = 0x0004000000000000,
    LeftThumbstickYVirtual = 0x0008000000000000,
    RightThumbstickXVirtual = 0x0010000000000000,
    RightThumbstickYVirtual = 0x0020000000000000,
    ButtonsVirtual = 0x0000F3FF00000000,
    AnalogsVirtual = 0x003F000000000000,
    AllVirtual = 0x003FF3FF00000000,
}
