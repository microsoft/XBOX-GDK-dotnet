# <a id="GDK_Net_PlayFab_PlayFabRuntime"></a> Class PlayFabRuntime

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Lifetime of the PlayFab libraries: <code>PFCore</code> (the authentication and HTTP layer) and
<code>PFServices</code> (the generated service APIs).

```csharp
public static class PlayFabRuntime
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabRuntime](GDK.Net.PlayFab.PlayFabRuntime.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
<xref href="GDK.Net.PlayFab.PlayFabRuntime.Initialize" data-throw-if-not-resolved="false"></xref> must be called before any other PlayFab API and, per the GDK, after
<xref href="GDK.Net.GameRuntime" data-throw-if-not-resolved="false"></xref> is initialized. <xref href="GDK.Net.PlayFab.PlayFabRuntime.UninitializeAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> waits for the
library's background work to drain, so it must not be called from a completion callback.
</p>
<p>
A task queue is never surfaced: the projection passes <code>nullptr</code> so the PlayFab libraries
use their own background queue, matching the "never surface a task queue" rule in
eng/interop-conventions.md.
</p>

## Properties

### <a id="GDK_Net_PlayFab_PlayFabRuntime_IsInitialized"></a> IsInitialized

Whether <xref href="GDK.Net.PlayFab.PlayFabRuntime.Initialize" data-throw-if-not-resolved="false"></xref> has completed without a matching uninitialize.

```csharp
public static bool IsInitialized { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabRuntime_Initialize"></a> Initialize\(\)

Initializes PlayFab Core and Services (<code>PFInitialize</code>, <code>PFServicesInitialize</code>).

```csharp
public static void Initialize()
```

#### Remarks

Second in the fixed startup order (see <xref href="GDK.Net.SubsystemOrder" data-throw-if-not-resolved="false"></xref>), so the Gaming Runtime
must already be up: PlayFab's asynchronous work runs on the process default task queue,
which the Gaming Runtime owns.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The Gaming Runtime is not initialized.

### <a id="GDK_Net_PlayFab_PlayFabRuntime_UninitializeAsync_System_Threading_CancellationToken_"></a> UninitializeAsync\(CancellationToken\)

Shuts PlayFab down and waits for its background work to drain
(<code>PFServicesUninitializeAsync</code>, <code>PFUninitializeAsync</code>).

```csharp
public static Task UninitializeAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Every <xref href="GDK.Net.PlayFab.PlayFabEntity" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.PlayFab.PlayFabServiceConfig" data-throw-if-not-resolved="false"></xref> and
<xref href="GDK.Net.PlayFab.PlayFabLocalUser" data-throw-if-not-resolved="false"></xref> must be disposed first; PlayFab fails the call while handles
are outstanding.

