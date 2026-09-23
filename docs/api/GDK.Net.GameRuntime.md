# <a id="GDK_Net_GameRuntime"></a> Class GameRuntime

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

The entry point of the projection: initializes the Gaming Runtime, owns the default task queue,
and exposes the feature areas.

```csharp
public sealed class GameRuntime : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameRuntime](GDK.Net.GameRuntime.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
The Gaming Runtime is only available to a packaged GDK title with a valid
<code>MicrosoftGame.config</code>. On any other process the underlying calls fail, and this type
surfaces that as a <xref href="GDK.Net.GameRuntimeException" data-throw-if-not-resolved="false"></xref> rather than a raw
<xref href="System.DllNotFoundException" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
Disposal order matters: all GDK objects must be released before
<code>XGameRuntimeUninitialize</code>, otherwise the runtime reports
<code>E_GAMERUNTIME_UNINITIALIZE_ACTIVEOBJECTS</code>. <xref href="GDK.Net.GameRuntime.Dispose" data-throw-if-not-resolved="false"></xref> therefore tears down the
feature areas and the owned queue first. That includes the process-wide PlayFab libraries,
which are not owned by this object: they are torn down in the exact reverse of the fixed
startup order described on <xref href="GDK.Net.SubsystemOrder" data-throw-if-not-resolved="false"></xref>, so a title never has to sequence the
libraries by hand and cannot leave one running into <code>XGameRuntimeUninitialize</code>.
</p>

## Properties

### <a id="GDK_Net_GameRuntime_Activation"></a> Activation

Protocol and game-invite activation events.

```csharp
public GameActivationManager Activation { get; }
```

#### Property Value

 [GameActivationManager](GDK.Net.Activation.GameActivationManager.md)

### <a id="GDK_Net_GameRuntime_Capture"></a> Capture

Screenshot, clip recording and broadcast integration.

```csharp
public AppCaptureManager Capture { get; }
```

#### Property Value

 [AppCaptureManager](GDK.Net.Capture.AppCaptureManager.md)

### <a id="GDK_Net_GameRuntime_GameUi"></a> GameUi

System-provided UI dialogs: text entry, player picker, profile cards and error dialogs.

```csharp
public GameUiManager GameUi { get; }
```

#### Property Value

 [GameUiManager](GDK.Net.GameUI.GameUiManager.md)

### <a id="GDK_Net_GameRuntime_Networking"></a> Networking

Network connectivity, cost policy and privilege checks.

```csharp
public NetworkingManager Networking { get; }
```

#### Property Value

 [NetworkingManager](GDK.Net.Networking.NetworkingManager.md)

### <a id="GDK_Net_GameRuntime_Users"></a> Users

User sign-in, identity and change events.

```csharp
public UserManager Users { get; }
```

#### Property Value

 [UserManager](GDK.Net.Users.UserManager.md)

### <a id="GDK_Net_GameRuntime_XboxLive"></a> XboxLive

Xbox Live Services (XSAPI): profiles, achievements and the rest of the Xbox Live surface.

```csharp
public XboxLiveService XboxLive { get; }
```

#### Property Value

 [XboxLiveService](GDK.Net.XboxLive.XboxLiveService.md)

#### Remarks

Inert until <xref href="GDK.Net.XboxLive.XboxLiveService.Initialize(GDK.Net.XboxLive.XboxLiveOptions)" data-throw-if-not-resolved="false"></xref> is called with the
title's SCID. Accessing this property loads nothing — XSAPI's native modules are only pulled
in by the first real call — so a title that does not use Xbox Live is unaffected.

## Methods

### <a id="GDK_Net_GameRuntime_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_GameRuntime_Initialize"></a> Initialize\(\)

Initializes the Gaming Runtime (<code>XGameRuntimeInitialize</code>).

```csharp
public static GameRuntime Initialize()
```

#### Returns

 [GameRuntime](GDK.Net.GameRuntime.md)

#### Remarks

Async calls and event registrations name no task queue, so the Gaming Runtime resolves the
process default at call time — a thread-pool queue on both ports unless the host process
replaced it. Continuations and events therefore arrive on the thread pool, and a title that
must touch its renderer marshals to its own thread as it would for any other background
callback.

### <a id="GDK_Net_GameRuntime_Initialize_GDK_Net_GameRuntimeOptions_"></a> Initialize\(GameRuntimeOptions?\)

Initializes the Gaming Runtime with custom options (<code>XGameRuntimeInitializeWithOptions</code>).

```csharp
public static GameRuntime Initialize(GameRuntimeOptions? options)
```

#### Parameters

`options` [GameRuntimeOptions](GDK.Net.GameRuntimeOptions.md)?

Initialization options. When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> this overload behaves identically to
<xref href="GDK.Net.GameRuntime.Initialize" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [GameRuntime](GDK.Net.GameRuntime.md)

### <a id="GDK_Net_GameRuntime_IsFeatureAvailable_GDK_Net_GameRuntimeFeature_"></a> IsFeatureAvailable\(GameRuntimeFeature\)

Reports whether the installed Gaming Runtime provides <code class="paramref">feature</code>
(<code>XGameRuntimeIsFeatureAvailable</code>).

```csharp
public bool IsFeatureAvailable(GameRuntimeFeature feature)
```

#### Parameters

`feature` [GameRuntimeFeature](GDK.Net.GameRuntimeFeature.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

