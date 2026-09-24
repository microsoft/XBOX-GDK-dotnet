# The `XUser` Pilot: Shared Projection Contract

> **Purpose.** Every projection plan in this repository implements **one** vertical slice first, and
> it is always the same slice: **`XUser`**. `XUser` is the smallest subsystem that exercises *every*
> pattern in [`gdk-surface.md`](./gdk-surface.md), so proving it end-to-end in a language validates
> that language's answer to the whole surface. This document is the language-neutral **contract** for
> that slice: the exact native functions, enums, and structs each plan must project, and the
> patterns each one is there to prove.

Read [`gdk-surface.md`](./gdk-surface.md) first for the universal conventions; this document maps a
concrete subsystem onto them. Facts are from `windows\include\XUser.h` (edition `260404`),
which is byte-identical to the GameKit copy but authoritative here (see `gdk-surface.md` §1).

---

## 1. Why `XUser` is the pilot

`XUser` is the sign-in / identity subsystem. In one header it demonstrates:

| Universal pattern (gdk-surface §4) | `XUser` proof point |
|---|---|
| Runtime lifecycle + feature gating | `XGameRuntimeInitialize` / `XGameRuntimeIsFeatureAvailable(XUser)` |
| Opaque handle + Close/Duplicate/Compare | `XUserHandle`, `XUserCloseHandle`, `XUserDuplicateHandle`, `XUserCompare` |
| Async start/result pair → language async | `XUserAddAsync` → `XUserAddResult` |
| Async **sized** result (3-call) | `XUserGetGamerPictureAsync` → `...ResultSize` → `...Result` |
| Cancellation → `E_ABORT` | any `XAsyncBlock` op + `XAsyncCancel` |
| Cheap synchronous getters | `XUserGetId`, `XUserGetState`, `XUserGetIsGuest`, `XUserGetAgeGroup` |
| Two-call size buffer (string) | `XUserGetGamertag(component, size, buf, out used)` |
| Plain enum | `XUserState`, `XUserAgeGroup`, `XUserGamertagComponent`, `XUserGamerPictureSize` |
| Flag enum (`DEFINE_ENUM_FLAG_OPERATORS`) | `XUserAddOptions`, `XUserPrivilegeOptions` |
| Out-param + secondary out | `XUserCheckPrivilege(..., out hasPrivilege, out denyReason)` |
| Register/unregister event + token | `XUserRegisterForChangeEvent` / `XUserUnregisterForChangeEvent` |
| Second event stream + struct payload | `XUserRegisterForDeviceAssociationChanged` + `XUserDeviceAssociationChange` |
| Value-type identity struct | `XUserLocalId { uint64_t value; }` |
| UTF-8 / UTF-16 parallel APIs | `XUserGetTokenAndSignature` vs `...Utf16` |
| Deprecated-API handling | `XUserGetMsaTokenSilently*` (marked `__declspec(deprecated)`) |

If a projection expresses all of the above idiomatically, the remaining subsystems (`XStore`,
`XPackage`, `XGameSave`, XSAPI services, …) are the *same shapes again*.

---

## 2. The contract, grouped by pattern

Each group below lists the native declarations a plan must project and the **idiomatic obligation**
(what the projected API should feel like). Signatures are abbreviated; see the header for full SAL.

### 2.1 Lifecycle & feature gating
```c
STDAPI       XGameRuntimeInitialize();
STDAPI_(void) XGameRuntimeUninitialize();
STDAPI_(bool) XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature feature);   // XUser = 18
```
**Obligation:** a scoped runtime object (RAII / `IDisposable` / `AutoCloseable` / context manager /
`Drop`) whose construction initializes and disposal uninitializes; a `IsFeatureAvailable(...)` /
capability query. All `XUser` calls require the runtime to be initialized.

### 2.2 Handle, identity & lookup
```c
typedef struct XUser* XUserHandle;
struct XUserLocalId { uint64_t value; };

STDAPI        XUserDuplicateHandle(XUserHandle handle, XUserHandle* duplicatedHandle);
STDAPI_(void) XUserCloseHandle(XUserHandle user);
STDAPI_(int32_t) XUserCompare(XUserHandle user1, XUserHandle user2);     // ordering; nulls allowed

STDAPI XUserGetId(XUserHandle user, uint64_t* userId);
STDAPI XUserGetLocalId(XUserHandle user, XUserLocalId* userLocalId);
STDAPI XUserFindUserById(uint64_t userId, XUserHandle* handle);
STDAPI XUserFindUserByLocalId(XUserLocalId userLocalId, XUserHandle* handle);
STDAPI XUserFindForDevice(const APP_LOCAL_DEVICE_ID* deviceId, XUserHandle* handle);
STDAPI XUserGetMaxUsers(uint32_t* maxUsers);
```
**Obligation:** a `User` resource type with deterministic close, a `Duplicate()` that yields an
independent handle, value **equality/ordering** via `XUserCompare`, `Id`/`LocalId` accessors, and
static **find** helpers. `XUserLocalId` projects as a small value type (equatable/hashable). Note
`APP_LOCAL_DEVICE_ID` originates in the Windows SDK, a binding-generation dependency.

### 2.3 Add / sign-out (async start/result)
```c
enum class XUserAddOptions : uint32_t {           // FLAG enum
    None = 0x0, AddDefaultUserSilently = 0x1, AllowGuests = 0x2, AddDefaultUserAllowingUI = 0x4 };
DEFINE_ENUM_FLAG_OPERATORS(XUserAddOptions);

STDAPI XUserAddAsync(XUserAddOptions options, XAsyncBlock* async);
STDAPI XUserAddResult(XAsyncBlock* async, XUserHandle* newUser);

STDAPI_(bool) XUserIsSignOutPresent();
STDAPI XUserSignOutAsync(XUserHandle user, XAsyncBlock* async);
STDAPI XUserSignOutResult(XAsyncBlock* async);
```
**Obligation:** project the start/result pair as one async call in the language's idiom
(`Task<User>` / `Future<User>` / `Promise<User>` / `suspend fun` / blocking-with-context) that
allocates and keeps the `XAsyncBlock` alive until completion, then calls the `Result` function.
Wire **cancellation** to `XAsyncCancel` and translate the resulting **`E_ABORT`** to the language's
cancellation signal (not a generic error). `XUserAddOptions` projects as a **flag/bit-set** type.

### 2.4 Sized async result (3-call): gamer picture
```c
enum class XUserGamerPictureSize : uint32_t { Small=0, Medium=1, Large=2, ExtraLarge=3 };

STDAPI XUserGetGamerPictureAsync(XUserHandle user, XUserGamerPictureSize size, XAsyncBlock* async);
STDAPI XUserGetGamerPictureResultSize(XAsyncBlock* async, size_t* bufferSize);
STDAPI XUserGetGamerPictureResult(XAsyncBlock* async, size_t bufferSize,
                                  void* buffer, size_t* bufferUsed);
```
**Obligation:** hide the size→allocate→fill dance behind a single async call returning a native byte
array / image buffer.

### 2.5 Synchronous getters → properties / methods
```c
enum class XUserState    : uint32_t { SignedIn=0, SigningOut=1, SignedOut=2 };
enum class XUserAgeGroup : uint32_t { Unknown=0, Child=1, Teen=2, Adult=3 };

STDAPI XUserGetState(XUserHandle user, XUserState* state);
STDAPI XUserGetIsGuest(XUserHandle user, bool* isGuest);
STDAPI XUserGetAgeGroup(XUserHandle user, XUserAgeGroup* ageGroup);
```
**Obligation:** cheap, pure getters project as **properties** (or the language's nearest equivalent),
throwing / returning-error on failing `HRESULT`. Plain enums project as native enums.

### 2.6 Two-call string buffer: gamertag
```c
enum class XUserGamertagComponent : uint32_t { Classic=0, Modern=1, ModernSuffix=2, UniqueModern=3 };
const size_t XUserGamertagComponentClassicMaxBytes       = 16;
const size_t XUserGamertagComponentModernMaxBytes        = 97;
const size_t XUserGamertagComponentModernSuffixMaxBytes  = 15;
const size_t XUserGamertagComponentUniqueModernMaxBytes  = 101;

STDAPI XUserGetGamertag(XUserHandle user, XUserGamertagComponent component,
                        size_t gamertagSize, char* gamertag, size_t* gamertagUsed);
```
**Obligation:** a single method taking the component and returning a native **string**, sizing the
buffer internally (via the constant or a first sizing call) and decoding UTF-8.

### 2.7 Privilege check (out + secondary out)
```c
enum class XUserPrivilege : uint32_t { CrossPlay=185, Communications=252, Multiplayer=254, /*…*/ };
enum class XUserPrivilegeDenyReason : uint32_t { None=0, PurchaseRequired=1, Restricted=2, Banned=3,
                                                 Unknown=0xFFFFFFFF };
enum class XUserPrivilegeOptions : uint32_t { None=0x0, AllUsers=0x1 };   // FLAG enum
DEFINE_ENUM_FLAG_OPERATORS(XUserPrivilegeOptions);

STDAPI XUserCheckPrivilege(XUserHandle user, XUserPrivilegeOptions options, XUserPrivilege privilege,
                           bool* hasPrivilege, XUserPrivilegeDenyReason* reason);
STDAPI XUserResolvePrivilegeWithUiAsync(XUserHandle user, XUserPrivilegeOptions options,
                                        XUserPrivilege privilege, XAsyncBlock* async);
STDAPI XUserResolvePrivilegeWithUiResult(XAsyncBlock* async);
```
**Obligation:** project the primary `out bool` as the return value and the optional deny-reason as a
secondary output (tuple / out-param / richer result type), plus the async "resolve with UI" variant.

### 2.8 Events → first-class subscriptions
```c
enum class XUserChangeEvent : uint32_t {
    SignedInAgain=0, SigningOut=1, SignedOut=2, Gamertag=3, GamerPicture=4, Privileges=5 };
typedef void CALLBACK XUserChangeEventCallback(void* context, XUserLocalId id, XUserChangeEvent event);

STDAPI XUserRegisterForChangeEvent(XTaskQueueHandle queue, void* context,
                                   XUserChangeEventCallback* callback,
                                   XTaskQueueRegistrationToken* token);
STDAPI_(bool) XUserUnregisterForChangeEvent(XTaskQueueRegistrationToken token, bool wait);

struct XUserDeviceAssociationChange { APP_LOCAL_DEVICE_ID deviceId; XUserLocalId oldUser; XUserLocalId newUser; };
typedef void CALLBACK XUserDeviceAssociationChangedCallback(void* context,
                                   const XUserDeviceAssociationChange* change);

STDAPI XUserRegisterForDeviceAssociationChanged(XTaskQueueHandle queue, void* context,
                                   XUserDeviceAssociationChangedCallback* callback,
                                   XTaskQueueRegistrationToken* token);
STDAPI_(bool) XUserUnregisterForDeviceAssociationChanged(XTaskQueueRegistrationToken token, bool wait);
```
**Obligation:** expose each as the language's native event/subscription (event+delegate, listener +
closeable registration, `EventEmitter`, channel, …); keep the callback trampoline and context alive
for the subscription's lifetime; unregister (honoring `wait`) on dispose. Callbacks are delivered on
the supplied task queue: respect the projection's threading model (see gdk-surface §6).

### 2.9 Encoding variants, deferrals & deprecations (project or defer explicitly)
- **UTF-8 vs UTF-16 pairs:** `XUserGetTokenAndSignatureAsync` / `...ResultSize` / `...Result` and the
  parallel `...Utf16...` trio, with result structs `XUserGetTokenAndSignatureData` /
  `...Utf16Data`. A plan states which encoding it surfaces (typically the platform-natural one).
- **Sign-out deferral:** `XUserGetSignOutDeferral` / `XUserCloseSignOutDeferralHandle`
  (`XUserSignOutDeferralHandle`), a handle to hold off sign-out while the title saves state.
- **Audio endpoint:** `XUserGetDefaultAudioEndpointUtf16` + its register/unregister change event.
- **Deprecated:** `XUserGetMsaTokenSilentlyAsync/Result/ResultSize` are `__declspec(deprecated)`. A
  plan should **omit or clearly mark** deprecated members and describe how it filters them during
  binding generation.

---

## 3. Reference end-to-end scenario (what every pilot demonstrates)

The canonical happy path each language projection should be able to run against a live Gaming Runtime:

1. **Initialize** the runtime; assert `IsFeatureAvailable(XUser)`.
2. Create a **task queue** (thread-pool for a service/test harness, or a pumped `Manual` queue for a
   game loop).
3. **Add a user**: `AddAsync(AddDefaultUserSilently)` → obtain a `User`. (Fall back to
   `AddDefaultUserAllowingUI` when silent add yields `E_GAMEUSER_NO_DEFAULT_USER`.)
4. Read **`Id`**, **`State`**, **`AgeGroup`**, **`IsGuest`**, and **`Gamertag(UniqueModern)`**.
5. **Subscribe** to `UserChanged`; observe events (e.g. on sign-out).
6. Optionally fetch the **gamer picture** (sized async) and **check a privilege**
   (`Communications` / `Multiplayer`).
7. **Sign out** (`SignOutAsync`) if present; unsubscribe; close the user; dispose the queue; uninitialize.
8. Demonstrate **cancellation**: start an async op, cancel it, and confirm it surfaces as the
   language's cancellation signal (native `E_ABORT`).

---

## 4. Live-integration prerequisites

`XUser` cannot be meaningfully faked, and the projection wrapper is too thin to be worth mocking, so
**all** tests are validated **live** (as noted per-plan). See [`testing.md`](./testing.md) for the
full strategy; the requirements below are the baseline every live suite needs:

- An installed **GDK / Gaming Runtime** on the test host.
- The test harness is a **packaged GDK app** (has a `MicrosoftGame.config`) with a **registered title
  identity**.
- An authorized **sandbox** and a signed-in **test account**.
- Standard hosted CI cannot satisfy these; CI builds/lints (and runs the off-runtime layers), and the
  live pilot runs on a GDK-capable machine or self-hosted runner. Each plan repeats this as a risk.

---

*This contract is intentionally identical across languages. Differences live in each plan's
"idiomatic mapping table": how that language expresses handles, async, events, enums, buffers, and
errors, not in which `XUser` surface is covered.*
