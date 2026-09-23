# APIs the Gaming Runtime does not export

`xgameruntime.thunks.dll` is the only module the GDK ships that a non-C++ projection can bind (see
[interop-conventions.md](interop-conventions.md) and the header comment in
`src/GDK.Net/Interop/Native.cs`). In GDK edition **260404** — this projection's minimum — it exports
**390** flat `__stdcall` entry points, while `xgameruntime.lib` defines **404** `X*` symbols.

The **14** APIs below exist in the public headers or in the static import library, but are **not**
exported by the thunks DLL. Declaring a `DllImport`/`LibraryImport` against
`xgameruntime.thunks.dll` for any of them produces an `EntryPointNotFoundException` the first time
it is called — which is easy to mistake for an environment problem, because `GameRuntime`
translates that exception into `E_GAMERUNTIME_VERSION_MISMATCH`.

**Rule: never bind anything on this list.** There is no second module to bind it to: the projection
ships no native code of its own.

## What changed in 260404

Edition 260400 exported only 355 entry points, and this repository carried a re-export shim
(`xgameruntime.extras.dll`, built from `src/native/XGameRuntimeExtras/`) to reach the 48 usable
names it omitted. **Edition 260404 exports 35 of those 48 directly**, which covers every API the
projection actually surfaced, so the shim was deleted along with `eng/build-shim.ps1` and the
`GameRuntimeExtras` availability probe. Nothing that was reachable before is unreachable now, with
three exceptions — see [Members removed with the shim](#members-removed-with-the-shim).

260404 also adds one new API, `XPackageGetPackageKind`, and one new `XPackageKind` member,
`PublisherContent`. Both are projected (`GamePackage.GetPackageKind`, `PackageKind.PublisherContent`).

## Regenerating this list

```powershell
$dumpbin = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\VC\Tools\MSVC\14.44.35207\bin\Hostx64\x64\dumpbin.exe"
$thunks = & $dumpbin /exports "$env:GameDKCoreLatest`windows\bin\x64\xgameruntime.thunks.dll" |
    ForEach-Object { if ($_ -match '^\s+\d+\s+[0-9A-F]+\s+[0-9A-F]{8}\s+(\S+)$') { $matches[1] } }
$lib = & $dumpbin /symbols "$env:GameDKCoreLatest`windows\lib\x64\xgameruntime.lib" |
    ForEach-Object { if ($_ -match '\|\s+(X[A-Za-z0-9_]+)\s*$') { $matches[1] } } | Sort-Object -Unique
$lib | Where-Object { $thunks -notcontains $_ }
```

`%GameDKCoreLatest%` — never `%GRDKLatest%` or `%GXDKLatest%` — is the pinned edition root.

## The list

None of these is behind a preprocessor guard — verified by preprocessing `XGameRuntime.h` with
`cl /EP /D_GAMING_DESKTOP`. That distinguishes them sharply from the XSAPI names discussed at the
bottom of this file, which *are* gated and are therefore not gaps at all. Eight are **declared in
the public headers and simply not exported** — a genuine header/DLL mismatch, and the trap this file
exists to document. Six have **no public declaration at all** and exist only as symbols inside
`xgameruntime.lib`, which is exactly why the retired static-linking shim could reach them and why
binding the DLL never can.

| Family | Unexported API | Declared? | Why it is not reachable |
|---|---|---|---|
| XError | `XErrorReport` | lib only | No public member. The GDK still calls it internally, so `GameErrorHandling.SetErrorCallback` and `SetOptions` — both exported — still observe and configure every error the runtime raises. |
| XGameInvite | `XGameInviteRegisterForPendingEvent`, `XGameInviteUnregisterForPendingEvent`, `XGameInviteAcceptPendingInvite` | header | Deprecated in the headers, and fully superseded. `XGameActivationRegisterForEvent` is exported and reports pending invites as `GameActivationType.PendingGameInvite`, so `GameActivationManager.Activated` covers the same ground. |
| XGameStreaming | `XGameStreamingGetAssociatedFrame` | header | `__declspec(deprecated)`. Takes an `IGameInputReading*` and returns a `D3D12XBOX_FRAME_PIPELINE_TOKEN` from the GXDK console tree — both out of scope (`AGENTS.md` rule 2). |
| XGameStreaming | `XGameStreamingSendDebugMessageToClient` | lib only | **Not declared in any 260404 header.** Same situation as `XGameUiShowManageSpace*` below. |
| XGameUI | `XGameUiShowManageSpaceAsync`, `XGameUiShowManageSpaceResult` | lib only | **Not declared in any 260404 header.** See below. |
| XNetworking | `XNetworkingSetConfigurationSetting` | header | No public member. `NetworkingManager.QueryConfigurationSetting` is exported and projected, so a title can still read the values it cannot write. |
| XPackage | `XPackageGetIdentifier` | lib only | **Not declared in any 260404 header.** `XPackageGetCurrentProcessPackageIdentifier` is exported, is projected as `GamePackage.GetCurrentPackageIdentifier`, and covers the common case. |
| XThread | `XThreadVerifyNotTimeSensitive` | lib only | No public member. `XThreadAssertNotTimeSensitive` is exported and projected as `GameThread.AssertNotTimeSensitive`; it makes the same check, in debug builds only. |
| XUser | `XUserGetMsaTokenSilentlyAsync`, `XUserGetMsaTokenSilentlyResult`, `XUserGetMsaTokenSilentlyResultSize` | header | Deprecated in the headers. `XUserGetTokenAndSignature*` is exported, is projected, and covers the token case. |

`XPackageMount` is a fifteenth name in `xgameruntime.lib` that the thunks DLL does not export, but
it is a dead stub rather than a gap — see below.

`ThunksBindingContractTests.StillUnexportedEntryPointsAreNotBound` pins this list, so an edition
that starts exporting one of these is noticed rather than assumed.

The only *exported* `X*` APIs this projection deliberately leaves unbound are the six low-level
`XAsync*` provider entry points (`XAsyncBegin`, `XAsyncComplete`, `XAsyncGetResult`,
`XAsyncGetResultSize`, `XAsyncRun`, `XAsyncSchedule`), which exist to *implement* async operations.
The projection implements them once in `AsyncOperation<T>` and exposes C# `Task`/`async`/`await`
instead, so a caller never needs them.

## Members removed with the shim

Three public members had no route other than the shim and were removed when it was deleted. Each has
an exported neighbour that covers most of its purpose:

| Removed | Native entry point | Use instead |
|---|---|---|
| `GameErrorHandling.Report` | `XErrorReport` | `GameErrorHandling.SetErrorCallback` still observes the runtime's own reports. |
| `NetworkingManager.SetConfigurationSetting` | `XNetworkingSetConfigurationSetting` | `NetworkingManager.QueryConfigurationSetting` to read; there is no write path. |
| `GameThread.VerifyNotTimeSensitive` | `XThreadVerifyNotTimeSensitive` | `GameThread.AssertNotTimeSensitive`, which asserts in debug builds rather than reporting in every build. |

`ThunksBindingContractTests.MembersThatOnlyTheRetiredShimCouldReachAreGone` pins their absence.

### `XPackageMount` is a dead stub

Disassembling `xgameruntime.lib` shows `XPackageMount`'s entire body is:

```
mov  ecx, 80004001h                  ; E_NOTIMPL
lea  rdx, ["Use XPackageMountWithUiAsync in..."]
call XErrorReport
mov  eax, 80004001h
ret
```

It never calls `QueryApiImpl` and has no interface GUID — there is nothing behind it. DLC mounting
is available through the exported `XPackageMountWithUiAsync`, which *is* bound in
`Native.Package.cs` and projected.

### `XGameUiShowManageSpace*` is real but undeclared

`XGameUiShowManageSpaceAsync` and `XGameUiShowManageSpaceResult` are in `xgameruntime.lib` and are
not stubs: disassembly shows `XGameUiShowManageSpaceAsync` is an ordinary three-argument
`QueryApiImpl` + vtable dispatch.

The problem is that **no header shipped with GDK edition 260404 declares either function** — not
`XGameUI.h`, not anything else under `windows\include`, and not the GRDK extension libraries. A
case-insensitive search of the entire GDK installation for `ManageSpace` returns nothing. The thunks
DLL does not export them either, so there is nothing to bind even if the signature were known.

Without a declaration there is no signature contract. The argument count is visible in the
disassembly but the argument *types* are not, and a P/Invoke with plausible-but-wrong types
mis-marshals silently rather than failing loudly. They stay unbound until Microsoft declares them.
`CustomGameUiContractTests.ManageSpaceIsNotBoundBecauseNoHeaderDeclaresIt` pins this decision so it
is revisited deliberately.

## Xbox Live (XSAPI)

**`Microsoft.Xbox.Services.C.Thunks.dll` has no meaningful gap on GDK.** This is the opposite of the
core runtime's situation and should not be described as "the same shape of gap".

Verified on 260404 by preprocessing `xsapi-c/services_c.h` with `cl /EP /D_GAMING_DESKTOP` against
`%GameDKCoreLatest%windows\include` and diffing against `dumpbin /exports`: **437 `Xbl*` names
survive the preprocessor and all but one are exported.** The exception is `XblSetApiType`, an
internal API whose `XblApiType` enum is `{ XblCApi, XblCPPApi }`; it is not projected and should not
be. Discount callback `typedef`s and enum operator casts before comparing — they match a naive
`Name(` regex and are not entry points.

Header names that appear absent are gated out of the GDK build, in two distinct ways:

| Gate | Why it is not a gap | APIs |
|---|---|---|
| `#if HC_PLATFORM == HC_PLATFORM_WIN32 \|\| HC_PLATFORM_IS_EXTERNAL` | **Non-GDK surface.** `config.h` sets `HC_PLATFORM = HC_PLATFORM_GDK` when `_GAMING_DESKTOP`/`_GAMING_XBOX` is defined, and `HC_PLATFORM_IS_EXTERNAL` covers only Switch/PS4/PS5/Generic. A GDK title cannot see or call these; the GDK build of the DLL does not contain them. The equivalent functionality is in the **core runtime** (`XGameInvite*`, `XUser*`). | `XblGameInviteRegisterForEventAsync`/`Result`, `XblGameInviteUnregisterForEventAsync`, `XblGameInvite{Add,Remove}NotificationHandler`, `XblAchievementUnlock{Add,Remove}NotificationHandler`, `XblMultiplayerActivity{Add,Remove}InviteHandler` |
| `#if HC_PLATFORM == HC_PLATFORM_IOS \|\| ANDROID [\|\| UWP]` | **Non-GDK surface**, same reasoning. | `XblNotification{Subscribe,Unsubscribe}{To,From}NotificationsAsync` |
| `#ifdef XSAPI_INTERNAL_EVENTS_SERVICE` | **Internal-only Microsoft build surface**, absent on every platform in a shipping build — not a GDK restriction. | `XblLocalStorageSetHandlers`, `XblLocalStorage{Read,Write,Clear}Complete`, `XblEventsSetMaxFileSize`, `XblEventsSetStorageAllotment` |

**Do not project any family as query-only.** An earlier revision of this file claimed the privacy
mute/block-list handlers and the social friend-request-count handlers were unavailable. They are
unguarded and **exported as of 260404** — `XblPrivacy{Add,Remove}{BlockList,MuteList}ChangedHandler`
and `XblSocial{Add,Remove}FriendRequestCountChangedHandler` can be bound directly. They were
genuinely unexported on 260400, which is where that claim came from, so this is edition-specific and
worth re-checking on the next bump.

`XblUserStatisticsTrackUsers` appears in older documentation but is absent from both the 260404
header and the export table.

**Verify with the preprocessor, not a text scan.** A raw grep over the header tree overstates the
XSAPI gap by roughly 20x. Note also that these headers cannot be preprocessed as a non-GDK build:
`Xal/xal_types.h` includes `Xal/xal_win32.h`, which the GDK does not ship.

**Rule: `eng/api-coverage.ps1 -Module XboxLive` is the authority.** Its "Unbound exports" list is
the set of functions that are genuinely exported and genuinely not yet bound; a header function that
never appears there is not reachable and must not be bound.

## When a future edition exports one of these

1. Confirm the export with the dumpbin snippet above, and confirm a header declares it.
2. Declare the P/Invoke against `Native.LibraryName` in the matching
   `src/GDK.Net/Interop/Native.<Subsystem>.cs`, in **both** the `NET7_0_OR_GREATER` and the
   `netstandard2.0` halves, following [interop-conventions.md](interop-conventions.md).
3. Move its name from `StillUnexportedEntryPoints` to `NewlyExportedEntryPoints` in
   `tests/GDK.Net.Tests/ThunksBindingContractTests.cs`.
4. Delete its row from [the list](#the-list) above, and raise the minimum edition in `README.md`,
   `AGENTS.md` and `eng/api-coverage.ps1` if the export is what the projection now depends on.
