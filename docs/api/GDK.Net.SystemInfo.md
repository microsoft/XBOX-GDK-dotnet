# <a id="GDK_Net_SystemInfo"></a> Namespace GDK.Net.SystemInfo

### Classes

 [DisplayTimeoutDeferral](GDK.Net.SystemInfo.DisplayTimeoutDeferral.md)

Wraps an <code>XDisplayTimeoutDeferralHandle</code>; released with
<code>XDisplayCloseTimeoutDeferralHandle</code> on disposal.

 [GameDisplay](GDK.Net.SystemInfo.GameDisplay.md)

HDR mode control and display-timeout deferral APIs.

 [GameErrorHandling](GDK.Net.SystemInfo.GameErrorHandling.md)

GDK error-reporting hooks. Wrap <code>XErrorSetCallback</code> and <code>XErrorSetOptions</code> from
XError.h.

 [GameLauncher](GDK.Net.SystemInfo.GameLauncher.md)

Title identity and cross-game launch APIs.

 [GameSystem](GDK.Net.SystemInfo.GameSystem.md)

Device and runtime identity for the current GDK title. All members call directly into the
Gaming Runtime; initialize the runtime with <xref href="GDK.Net.GameRuntime.Initialize" data-throw-if-not-resolved="false"></xref> before use.

 [GameThread](GDK.Net.SystemInfo.GameThread.md)

Thread time-sensitivity markers. APIs that perform substantial work call
<xref href="GDK.Net.SystemInfo.GameThread.AssertNotTimeSensitive" data-throw-if-not-resolved="false"></xref> to detect accidental invocations from audio or
render threads.

 [HdrModeInfo](GDK.Net.SystemInfo.HdrModeInfo.md)

Luminance parameters returned by <xref href="GDK.Net.SystemInfo.GameDisplay.TryEnableHdrMode(GDK.Net.SystemInfo.HdrModePreference%2cGDK.Net.SystemInfo.HdrModeInfo%40)" data-throw-if-not-resolved="false"></xref> when HDR is
enabled. Mirrors <code>struct XDisplayHdrModeInfo</code> from XDisplay.h.

 [SystemAnalyticsInfo](GDK.Net.SystemInfo.SystemAnalyticsInfo.md)

Analytics information about the current device (<code>XSystemAnalyticsInfo</code> from XSystem.h).

 [SystemRuntimeInfo](GDK.Net.SystemInfo.SystemRuntimeInfo.md)

Runtime and available GDK version information (<code>XSystemRuntimeInfo</code> from XSystem.h).

### Structs

 [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

A GDK version number (<code>XVersion</code> from XGameRuntimeTypes.h). The four 16-bit components
pack into a single uint64 for ordered comparisons.

### Enums

 [ErrorOptions](GDK.Net.SystemInfo.ErrorOptions.md)

Error reporting options for <xref href="GDK.Net.SystemInfo.GameErrorHandling.SetOptions(GDK.Net.SystemInfo.ErrorOptions%2cGDK.Net.SystemInfo.ErrorOptions)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>enum class XErrorOptions</code> from XError.h.

 [HdrModePreference](GDK.Net.SystemInfo.HdrModePreference.md)

HDR display mode preference for <xref href="GDK.Net.SystemInfo.GameDisplay.TryEnableHdrMode(GDK.Net.SystemInfo.HdrModePreference%2cGDK.Net.SystemInfo.HdrModeInfo%40)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>enum class XDisplayHdrModePreference</code> from XDisplay.h.

 [HdrModeResult](GDK.Net.SystemInfo.HdrModeResult.md)

Result of a call to <xref href="GDK.Net.SystemInfo.GameDisplay.TryEnableHdrMode(GDK.Net.SystemInfo.HdrModePreference%2cGDK.Net.SystemInfo.HdrModeInfo%40)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>enum class XDisplayHdrModeResult</code> from XDisplay.h.

 [SystemDeviceType](GDK.Net.SystemInfo.SystemDeviceType.md)

The device type of the current machine. Mirrors <code>enum class XSystemDeviceType</code> from
XSystem.h.

 [SystemHandleCallbackReason](GDK.Net.SystemInfo.SystemHandleCallbackReason.md)

Whether a GDK handle was created or destroyed. Mirrors
<code>enum class XSystemHandleCallbackReason</code> from XSystem.h.

 [SystemHandleType](GDK.Net.SystemInfo.SystemHandleType.md)

The type of a GDK handle passed to <code>XSystemHandleCallback</code>. Mirrors
<code>enum class XSystemHandleType</code> from XSystem.h.

### Delegates

 [GdkErrorCallback](GDK.Net.SystemInfo.GdkErrorCallback.md)

Callback invoked by the GDK when an error is reported via <code>XErrorReport</code>.
Return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> to suppress the error (mark it as handled);
return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> to let the GDK apply its default behaviour.

