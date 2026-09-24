# Getting started

This page shows the shape of a GDK.Net title: initialising the runtime, signing in a user, and the
idioms the projection uses in place of the flat C API's conventions.

For build prerequisites and commands see [Building](building.md). For the complete type and member
listing see the [API reference](api/).

## The projection in one table

Every GDK.Net design decision follows from one rule: callers must never see a raw GDK
convention. What that replaces:

| Microsoft GDK (flat C) | GDK.Net |
|---|---|
| `HRESULT` return codes | Exceptions (`GameRuntimeException` and typed subclasses); the numeric code is preserved on `HResultCode` |
| `XAsyncBlock` + completion callback + a second "get result" call | `Task` / `Task<T>`, awaited normally |
| Two-call size-then-buffer reads | Ordinary properties and return values |
| Opaque handles with `X*CloseHandle` | `IDisposable` |
| `XUserRegisterForChangeEvent` + a registration token | C# `event`; unsubscribing unregisters |
| `E_ABORT` from a cancelled operation | `OperationCanceledException` via `CancellationToken` |
| `X*Flags` integer constants | `[Flags]` enums |

## Requirements to run

The Gaming Runtime **refuses to initialise in a process with no package identity**. On PC that
identity can come from a `MicrosoftGame.config` beside the executable rather than a registered
package, which is what makes `eng/run-local.ps1` work. You still need:

- a dev-unlocked Windows machine,
- an account signed in to the XBOX app, and
- `MicrosoftGame.config` and `xgameruntime.thunks.dll` next to the executable, both placed there
  automatically by `eng/packaging/GdkRedist.targets`.

## Initialising the runtime

`GameRuntime` is both the entry point and the lifetime owner. Disposing it unregisters every event
and calls `XGameRuntimeUninitialize`.

```csharp
using GDK.Net;

using GameRuntime runtime = GameRuntime.Initialize();

Console.WriteLine(runtime.IsFeatureAvailable(GameRuntimeFeature.User));
```

Family managers hang off the runtime instance: `runtime.Users`, `runtime.Activation`,
`runtime.GameUi`, `runtime.Networking`, `runtime.Capture` and `runtime.XboxLive`.

`Initialize(GameRuntimeOptions)` overrides where the game config is read from, via
`GameConfigSource` and `GameConfig`.

## Signing in a user

The silent path is what a title does at startup: it succeeds when an account is already signed in
and never shows UI. Falling back to the account picker is an ordinary `catch`.

```csharp
using GDK.Net.Users;

static async Task<User> AddUserAsync(GameRuntime runtime, bool allowUI)
{
    try
    {
        return await runtime.Users
            .AddAsync(UserAddOptions.AddDefaultUserSilently)
            .ConfigureAwait(false);
    }
    catch (UserException) when (allowUI)
    {
        return await runtime.Users
            .AddAsync(UserAddOptions.AddDefaultUserAllowingUI)
            .ConfigureAwait(false);
    }
}
```

`User` is `IDisposable`: it owns an `XUserHandle`:

```csharp
using User user = await AddUserAsync(runtime, allowUI: true).ConfigureAwait(false);

Console.WriteLine($"id        0x{user.Id:X16}");
Console.WriteLine($"local id  {user.LocalId}");
Console.WriteLine($"state     {user.State}");
```

In C those four reads are `XUserGetId`, `XUserGetLocalId`, `XUserGetState` and `XUserGetAgeGroup`,
each returning an `HRESULT` with the value in an out-parameter.

## Errors

Every native failure surfaces as an exception. The HRESULT is preserved for diagnostics but callers
never check a return code:

```csharp
catch (GameRuntimeException ex)
{
    Console.WriteLine($"GDK call failed: {ex.Message} (HRESULT 0x{ex.HResultCode:X8})");
}
```

`HResult` exposes the well-known codes as constants plus `Succeeded`, `Failed` and `IsGameUser`.
Cancellation is special-cased: `E_ABORT` becomes `OperationCanceledException`, never a generic
error; see `AGENTS.md` rule 4.

## Events

Change notifications are plain C# events. There is no registration token to keep alive;
unsubscribing is what unregisters.

```csharp
runtime.Users.UserChanged += OnUserChanged;
// ...
runtime.Users.UserChanged -= OnUserChanged;

static void OnUserChanged(object? sender, UserChangedEventArgs e) =>
    Console.WriteLine($"user changed: {e.Change} localId={e.LocalId}");
```

Callbacks arrive on the process default task queue's thread pool, so there is nothing to pump for
the `X*` families.

### Party is the exception

PlayFab Party is an object model, not a request/response service, and it must be pumped from the
title's frame loop. See `samples/GDK.Net.PlayFabSample` and the `PartyManager` reference.

## Where to go next

| Want to | See |
|---|---|
| Build, test and package | [building.md](building.md) |
| Understand the layering and target frameworks | [architecture.md](architecture.md) |
| Move to a new GDK edition | [gdk-edition.md](gdk-edition.md) |
| Look up any type or member | [API reference](api/) |
| Read working code | `samples/GDK.Net.UserSample`, `samples/GDK.Net.PlayFabSample` |
