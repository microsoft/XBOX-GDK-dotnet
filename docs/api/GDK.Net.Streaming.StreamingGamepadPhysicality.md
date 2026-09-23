# <a id="GDK_Net_Streaming_StreamingGamepadPhysicality"></a> Enum StreamingGamepadPhysicality

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Reports which gamepad inputs came from physical hardware and which were synthesised by an
on-screen touch layout (<code>XGameStreamingGamepadPhysicality</code>).

```csharp
[Flags]
public enum StreamingGamepadPhysicality : ulong
```

## Fields

`APhysical = 4096` 

The A button came from physical hardware.



`AVirtual = 17592186044416` 

The A button was synthesised by a touch layout.



`AllPhysical = 4191231` 

All physical flags.



`AllVirtual = 18001200074981376` 

All virtual flags.



`AnalogsPhysical = 4128768` 

All physical analog flags.



`AnalogsVirtual = 17732923532771328` 

All virtual analog flags.



`BPhysical = 8192` 

The B button came from physical hardware.



`BVirtual = 35184372088832` 

The B button was synthesised by a touch layout.



`ButtonsPhysical = 62463` 

All physical button flags.



`ButtonsVirtual = 268276542210048` 

All virtual button flags.



`DPadDownPhysical = 2` 

D-pad down came from physical hardware.



`DPadDownVirtual = 8589934592` 

D-pad down was synthesised by a touch layout.



`DPadLeftPhysical = 4` 

D-pad left came from physical hardware.



`DPadLeftVirtual = 17179869184` 

D-pad left was synthesised by a touch layout.



`DPadRightPhysical = 8` 

D-pad right came from physical hardware.



`DPadRightVirtual = 34359738368` 

D-pad right was synthesised by a touch layout.



`DPadUpPhysical = 1` 

D-pad up came from physical hardware.



`DPadUpVirtual = 4294967296` 

D-pad up was synthesised by a touch layout.



`LeftShoulderPhysical = 256` 

The left shoulder button came from physical hardware.



`LeftShoulderVirtual = 1099511627776` 

The left shoulder button was synthesised by a touch layout.



`LeftThumbstickPhysical = 64` 

The left thumbstick click came from physical hardware.



`LeftThumbstickVirtual = 274877906944` 

The left thumbstick click was synthesised by a touch layout.



`LeftThumbstickXPhysical = 262144` 

The left thumbstick X axis came from physical hardware.



`LeftThumbstickXVirtual = 1125899906842624` 

The left thumbstick X axis was synthesised by a touch layout.



`LeftThumbstickYPhysical = 524288` 

The left thumbstick Y axis came from physical hardware.



`LeftThumbstickYVirtual = 2251799813685248` 

The left thumbstick Y axis was synthesised by a touch layout.



`LeftTriggerPhysical = 65536` 

The left trigger came from physical hardware.



`LeftTriggerVirtual = 281474976710656` 

The left trigger was synthesised by a touch layout.



`MenuPhysical = 16` 

The Menu button came from physical hardware.



`MenuVirtual = 68719476736` 

The Menu button was synthesised by a touch layout.



`None = 0` 

No inputs reported.



`RightShoulderPhysical = 512` 

The right shoulder button came from physical hardware.



`RightShoulderVirtual = 2199023255552` 

The right shoulder button was synthesised by a touch layout.



`RightThumbstickPhysical = 128` 

The right thumbstick click came from physical hardware.



`RightThumbstickVirtual = 549755813888` 

The right thumbstick click was synthesised by a touch layout.



`RightThumbstickXPhysical = 1048576` 

The right thumbstick X axis came from physical hardware.



`RightThumbstickXVirtual = 4503599627370496` 

The right thumbstick X axis was synthesised by a touch layout.



`RightThumbstickYPhysical = 2097152` 

The right thumbstick Y axis came from physical hardware.



`RightThumbstickYVirtual = 9007199254740992` 

The right thumbstick Y axis was synthesised by a touch layout.



`RightTriggerPhysical = 131072` 

The right trigger came from physical hardware.



`RightTriggerVirtual = 562949953421312` 

The right trigger was synthesised by a touch layout.



`ViewPhysical = 32` 

The View button came from physical hardware.



`ViewVirtual = 137438953472` 

The View button was synthesised by a touch layout.



`XPhysical = 16384` 

The X button came from physical hardware.



`XVirtual = 70368744177664` 

The X button was synthesised by a touch layout.



`YPhysical = 32768` 

The Y button came from physical hardware.



`YVirtual = 140737488355328` 

The Y button was synthesised by a touch layout.



## Remarks

The low 32 bits are the <code>*Physical</code> flags and the high 32 bits the matching
<code>*Virtual</code> flags, so a single value describes both halves of a reading. Use it to suppress
gameplay that only makes sense for real hardware, such as rumble or aim assist tuning.

