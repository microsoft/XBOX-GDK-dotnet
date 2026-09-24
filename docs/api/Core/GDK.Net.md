# <a id="GDK_Net"></a> Namespace GDK.Net

### Namespaces

 [GDK.Net.Accessibility](../Accessibility/GDK.Net.Accessibility.md)

 [GDK.Net.Activation](../Activation/GDK.Net.Activation.md)

 [GDK.Net.Capture](../Capture/GDK.Net.Capture.md)

 [GDK.Net.Events](../Events/GDK.Net.Events.md)

 [GDK.Net.GameSave](../GameSave/GDK.Net.GameSave.md)

 [GDK.Net.GameUI](../GameUI/GDK.Net.GameUI.md)

 [GDK.Net.Networking](../Networking/GDK.Net.Networking.md)

 [GDK.Net.Package](../Package/GDK.Net.Package.md)

 [GDK.Net.PlayFab](../PlayFab/GDK.Net.PlayFab.md)

 [GDK.Net.Storage](../Storage/GDK.Net.Storage.md)

 [GDK.Net.Store](../Store/GDK.Net.Store.md)

 [GDK.Net.Streaming](../Streaming/GDK.Net.Streaming.md)

 [GDK.Net.SystemInfo](../SystemInfo/GDK.Net.SystemInfo.md)

 [GDK.Net.Users](../Users/GDK.Net.Users.md)

 [GDK.Net.XboxLive](../XboxLive/GDK.Net.XboxLive.md)

### Classes

 [GameRuntime](GDK.Net.GameRuntime.md)

The entry point of the projection: initializes the Gaming Runtime, owns the default task queue,
and exposes the feature areas.

 [GameRuntimeException](GDK.Net.GameRuntimeException.md)

Raised when a GDK call returns a failing HRESULT.

 [GameRuntimeOptions](GDK.Net.GameRuntimeOptions.md)

Options for <xref href="GDK.Net.GameRuntime.Initialize(GDK.Net.GameRuntimeOptions)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>struct XGameRuntimeOptions</code> from XGameRuntimeInit.h.

 [HResult](GDK.Net.HResult.md)

Well-known HRESULT values returned by the GDK, plus the standard COM codes the projection
special-cases. Values are taken verbatim from <code>XGameErr.h</code> (GDK edition 260404) and
<code>winerror.h</code>.

 [Hr](GDK.Net.Hr.md)

HRESULT checking helpers. Every HRESULT-returning P/Invoke in this projection is funnelled
through <xref href="GDK.Net.Hr.ThrowIfFailed(System.Int32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

 [UserException](GDK.Net.UserException.md)

Raised for the <code>E_GAMEUSER_*</code> family so callers can catch user-identity failures
(wrong sandbox, signed out, no package identity, …) without inspecting HRESULT values.

### Enums

 [GameRuntimeFeature](GDK.Net.GameRuntimeFeature.md)

Gaming Runtime features that can be probed with
<xref href="GDK.Net.GameRuntime.IsFeatureAvailable(GDK.Net.GameRuntimeFeature)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XGameRuntimeFeature</code>.

 [GameRuntimeGameConfigSource](GDK.Net.GameRuntimeGameConfigSource.md)

Controls where <code>XGameRuntimeInitializeWithOptions</code> loads the game configuration from.
Mirrors <code>XGameRuntimeGameConfigSource</code> from XGameRuntimeInit.h.

