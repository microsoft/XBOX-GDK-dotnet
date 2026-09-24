# <a id="GDK_Net_XboxLive_XboxLiveService"></a> Class XboxLiveService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live Services (XSAPI). Reached through <xref href="GDK.Net.GameRuntime.XboxLive" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class XboxLiveService : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[XboxLiveService](GDK.Net.XboxLive.XboxLiveService.md)

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
XSAPI is a <b>second native module</b>: <code>Microsoft.Xbox.Services.C.Thunks.dll</code>, with a hard
dependency on <code>libHttpClient.dll</code>. Neither is installed system-wide, so both must be
redistributed in the package layout. Loading is lazy: a title that never calls
<xref href="GDK.Net.XboxLive.XboxLiveService.Initialize(GDK.Net.XboxLive.XboxLiveOptions)" data-throw-if-not-resolved="false"></xref> never loads them, and a missing module surfaces as a
<xref href="GDK.Net.GameRuntimeException" data-throw-if-not-resolved="false"></xref> carrying <code>E_GAMERUNTIME_DLL_NOT_FOUND</code> rather than a raw
<xref href="System.DllNotFoundException" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
Initialization is synchronous and needs the title's SCID. <b>Teardown is not:</b>
<code>XblCleanupAsync</code> has no synchronous form, so <xref href="GDK.Net.XboxLive.XboxLiveService.CleanupAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> must be awaited
before the task queue it ran on is disposed. <xref href="GDK.Net.GameRuntime.Dispose" data-throw-if-not-resolved="false"></xref> cannot await, so
it falls back to a bounded synchronous wait; a title that cares about clean shutdown should
await <xref href="GDK.Net.XboxLive.XboxLiveService.CleanupAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> itself first.
</p>
<p>
XSAPI state is process-wide, so this type refuses a second
<xref href="GDK.Net.XboxLive.XboxLiveService.Initialize(GDK.Net.XboxLive.XboxLiveOptions)" data-throw-if-not-resolved="false"></xref> while already initialized.
</p>

## Properties

### <a id="GDK_Net_XboxLive_XboxLiveService_AchievementsManager"></a> AchievementsManager

The achievements manager: a locally cached view of every added user's achievements, pumped
by <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> once per frame.

```csharp
public AchievementsManager AchievementsManager { get; }
```

#### Property Value

 [AchievementsManager](GDK.Net.XboxLive.AchievementsManager.md)

#### Remarks

Process-global rather than per-context. Its queries read the warm cache and return
synchronously, unlike the service calls on <xref href="GDK.Net.XboxLive.XboxLiveContext.Achievements" data-throw-if-not-resolved="false"></xref>, which
go to the network every time.

### <a id="GDK_Net_XboxLive_XboxLiveService_IsInitialized"></a> IsInitialized

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> between a successful initialize and a completed cleanup.

```csharp
public bool IsInitialized { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_XboxLiveService_Scid"></a> Scid

The Service Configuration ID XSAPI is running against (<code>XblGetScid</code>).

```csharp
public string Scid { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live has not been initialized.

### <a id="GDK_Net_XboxLive_XboxLiveService_SocialManager"></a> SocialManager

The social manager: a locally cached, event-driven view of the social graph, pumped by
<xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref> once per frame.

```csharp
public SocialManager SocialManager { get; }
```

#### Property Value

 [SocialManager](GDK.Net.XboxLive.SocialManager.md)

#### Remarks

Process-global rather than per-context, which is why it hangs off the service instead of an
<xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>: users are added to it individually. Prefer it over
<xref href="GDK.Net.XboxLive.XboxLiveContext.Social" data-throw-if-not-resolved="false"></xref> when a title needs a live friends list rather than a
one-off query.

## Methods

### <a id="GDK_Net_XboxLive_XboxLiveService_CleanupAsync_System_Threading_CancellationToken_"></a> CleanupAsync\(CancellationToken\)

Shuts Xbox Live Services down (<code>XblCleanupAsync</code>).

```csharp
public Task CleanupAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Every <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref> must be disposed first, and the returned task must be
awaited before the task queue is torn down. Calling this when not initialized is a no-op, so
it is safe in a <code>finally</code>.

### <a id="GDK_Net_XboxLive_XboxLiveService_CreateContext_GDK_Net_Users_User_"></a> CreateContext\(User\)

Creates an Xbox Live context for <code class="paramref">user</code> (<code>XblContextCreateHandle</code>).

```csharp
public XboxLiveContext CreateContext(User user)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

#### Returns

 [XboxLiveContext](GDK.Net.XboxLive.XboxLiveContext.md)

#### Remarks

The returned context is valid only while <code class="paramref">user</code> stays signed in. Dispose and
recreate it when <xref href="GDK.Net.Users.UserManager.UserChanged" data-throw-if-not-resolved="false"></xref> reports
<xref href="GDK.Net.Users.UserChangeEvent.SignedInAgain" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.Users.UserChangeEvent.SigningOut" data-throw-if-not-resolved="false"></xref>
or <xref href="GDK.Net.Users.UserChangeEvent.SignedOut" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_XboxLiveService_DisableThrottlingAssertsInDevSandboxes"></a> DisableThrottlingAssertsInDevSandboxes\(\)

Suppresses XSAPI's debug asserts for Xbox Live throttling while running in a development
sandbox (<code>XblDisableAssertsForXboxLiveThrottlingInDevSandboxes</code>).

```csharp
public void DisableThrottlingAssertsInDevSandboxes()
```

#### Remarks

A development aid only. Throttling still happens; only the assert is silenced, and retail
sandboxes are unaffected.

### <a id="GDK_Net_XboxLive_XboxLiveService_Dispose"></a> Dispose\(\)

Runs <code>XblCleanupAsync</code> and waits for it, because the runtime offers no synchronous
teardown. Prefer awaiting <xref href="GDK.Net.XboxLive.XboxLiveService.CleanupAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> before disposal.

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_XboxLiveService_Initialize_GDK_Net_XboxLive_XboxLiveOptions_"></a> Initialize\(XboxLiveOptions\)

Initializes Xbox Live Services (<code>XblInitialize</code>). This is the call that first loads the
XSAPI native modules.

```csharp
public void Initialize(XboxLiveOptions options)
```

#### Parameters

`options` [XboxLiveOptions](GDK.Net.XboxLive.XboxLiveOptions.md)

Initialization options; <xref href="GDK.Net.XboxLive.XboxLiveOptions.Scid" data-throw-if-not-resolved="false"></xref> is required.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">options</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

The SCID is empty.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live is already initialized.

### <a id="GDK_Net_XboxLive_XboxLiveService_SetOverrideLocale_System_String_"></a> SetOverrideLocale\(string\)

Overrides the locale XSAPI reports to the service (<code>XblSetOverrideLocale</code>), for example
<code>"en-US"</code>.

```csharp
public void SetOverrideLocale(string locale)
```

#### Parameters

`locale` [string](https://learn.microsoft.com/dotnet/api/system.string)

