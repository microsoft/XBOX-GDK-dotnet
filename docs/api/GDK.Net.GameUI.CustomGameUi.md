# <a id="GDK_Net_GameUI_CustomGameUi"></a> Class CustomGameUi

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

Lets a title render the Gaming Runtime's UI itself instead of letting the system draw it
(<code>XGameUiSetUiCallbacks</code> and the eight <code>XGameUiSet*UiResponse</code> completions).

```csharp
public static class CustomGameUi
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CustomGameUi](GDK.Net.GameUI.CustomGameUi.md)

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
By default, calls such as <xref href="GDK.Net.GameUI.GameUiManager.ShowMessageDialogAsync(System.String%2cSystem.String%2cSystem.String%2cSystem.String%2cSystem.String%2cGDK.Net.GameUI.MessageDialogButton%2cGDK.Net.GameUI.MessageDialogButton%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> render system UI.
After <xref href="GDK.Net.GameUI.CustomGameUi.SetHandlers(GDK.Net.GameUI.CustomGameUiHandlers%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref>, those same calls instead invoke the matching handler with a
<xref href="GDK.Net.GameUI.GameUiRequest" data-throw-if-not-resolved="false"></xref>; the title draws its own UI and answers by calling
<code>Respond</code> on the request, which completes the original operation. This is how a title keeps
a consistent visual style, and on platforms with no system UI it is the only way these APIs
work at all.
</p>
<p>
Handlers do not run on the Gaming Runtime's callback thread. The runtime raises these callbacks
on the work port of the task queue driving the originating operation, and anything done before
returning from the callback occupies that port -- so this projection copies the request payload,
hands the request to the thread pool, and returns immediately. A handler is therefore free to
block, to <code>await</code>, and to await further Gaming Runtime operations on that same queue
without deadlocking.
</p>
<p>
The consequence is that handlers run on a thread pool thread with no synchronization context. A
title that must touch its renderer will need to marshal to its own thread, exactly as it would
for any other background callback. Responding is safe from any thread at any time, and need not
happen inside the handler -- capturing the request and answering frames later is the expected
pattern.
</p>
<p>
A handler that throws will not take the process down, and if it throws before responding the
projection answers on its behalf with the neutral response for that request kind, so a title bug
cannot leave the caller's <code>XGameUiShow*Async</code> operation pending forever.
</p>
<p>
The registration is process-wide and last-writer-wins, mirroring the native API. Only handlers
that are non-null are registered, so the system keeps ownership of any UI the title does not
implement.
</p>

## Properties

### <a id="GDK_Net_GameUI_CustomGameUi_Handlers"></a> Handlers

The handler set currently registered, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the system is drawing
all UI.

```csharp
public static CustomGameUiHandlers? Handlers { get; }
```

#### Property Value

 [CustomGameUiHandlers](GDK.Net.GameUI.CustomGameUiHandlers.md)?

## Methods

### <a id="GDK_Net_GameUI_CustomGameUi_ClearHandlers"></a> ClearHandlers\(\)

Clears the registration so the system draws all UI again (<code>XGameUiSetUiCallbacks</code> with
a table whose every function pointer is null).

```csharp
public static void ClearHandlers()
```

#### Remarks

Note that this passes a zeroed table rather than a null pointer. The native entry point
dereferences the table unconditionally -- passing <code>nullptr</code> access-violates inside the
Gaming Runtime and takes the process down. "The title implements nothing" is expressed the
same way as "the title implements only some of these": by leaving slots null.

### <a id="GDK_Net_GameUI_CustomGameUi_DuplicateUser_System_IntPtr_"></a> DuplicateUser\(nint\)

Takes an owning copy of one of the borrowed <code>XUserHandle</code> values carried on a request
(<code>XUserDuplicateHandle</code>), so it can outlive the handler that received it.

```csharp
public static nint DuplicateUser(nint userHandle)
```

#### Parameters

`userHandle` [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

A handle from a request's <code>RequestingUserHandle</code> property.

#### Returns

 [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

A duplicated handle the caller owns and must eventually close, or <xref href="System.IntPtr.Zero" data-throw-if-not-resolved="false"></xref>
when <code class="paramref">userHandle</code> was <xref href="System.IntPtr.Zero" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_GameUI_CustomGameUi_SetHandlers_GDK_Net_GameUI_CustomGameUiHandlers_System_Boolean_"></a> SetHandlers\(CustomGameUiHandlers, bool\)

Registers the title's UI handlers (<code>XGameUiSetUiCallbacks</code>), replacing any previous
registration.

```csharp
public static void SetHandlers(CustomGameUiHandlers handlers, bool useSystemUiIfAvailable = false)
```

#### Parameters

`handlers` [CustomGameUiHandlers](GDK.Net.GameUI.CustomGameUiHandlers.md)

The UI requests the title will render. Handlers left <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> stay with the
system.

`useSystemUiIfAvailable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, the runtime prefers its own UI wherever it has one and only
falls back to the title's handlers when it does not. When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>, the
title's handlers always win.

#### Remarks

Must not be called from a thread marked time-sensitive: the native entry point asserts
against that internally (<code>XThreadAssertNotTimeSensitive</code>).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">handlers</code> is null.

