# Interop conventions

Every GDK API family in this repository is projected the same way. Follow this document exactly:
consistency is what lets the surface be reviewed and extended without re-reading each family.

`eng/Generated/` (git-ignored, produced by `eng/generate-interop.ps1`) holds ClangSharp output for
the whole public surface. Treat it as the **authoritative signature reference** — never guess a
signature — but do not compile it. It emits `[assembly: DisableRuntimeMarshalling]` and uses `bool`
in P/Invoke signatures, neither of which works on `netstandard2.0`.

## File layout

For a family `Xyz` (for example `Store`, `Package`, `GameSave`):

| File | Contents |
|---|---|
| `src/GDK.Net/Interop/Native.Xyz.cs` | `internal static unsafe partial class Native` — the P/Invokes |
| `src/GDK.Net/Interop/NativeTypes.Xyz.cs` | raw enums and blittable structs, transcribed from the header |
| `src/GDK.Net/Xyz/*.cs` | the idiomatic public API |
| `tests/GDK.Net.Tests/XyzTests.cs` | contract tests |

Never edit another family's files. `Native.cs`, `NativeTypes.cs` and `Trampolines.cs` hold the
runtime core only — `XGameRuntimeInit.h`, `XGameRuntimeFeature.h`, `XTaskQueue.h` and `XAsync.h`.
Everything else, `XUser.h` included, lives in its own family file.

**Never name a file for when it was written.** `Xyz` is the API family, not a version or a
generation: `Native.User2.cs`, `NativeTypes.XyzExtended.cs` and similar are forbidden. This is a
greenfield projection with no legacy surface to preserve, so when a family grows, it grows inside
its own file. Where a family is large enough to warrant sub-files, split on the *header* boundary
and keep the family prefix — XSAPI does this: `*.Xbl.cs` is the Xbox Live core
(`xbox_live_global_c.h`, `xbox_live_context_c.h`, `xbox_live_context_settings_c.h`) and each
service gets `*.Xbl<Service>.cs` (`XblProfile` for `profile_c.h`, `XblAchievements` for
`achievements_c.h`).

## Raw interop

Every P/Invoke file carries both shims, and they must stay signature-identical:

```csharp
#if NET7_0_OR_GREATER
internal static unsafe partial class Native
{
    [LibraryImport(LibraryName)]
    internal static partial int XPackageGetWriteStats(/* ... */);
}
#else
internal static unsafe partial class Native
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XPackageGetWriteStats(/* ... */);
}
#endif
```

Only blittable parameters, so the two generate equivalent stubs and `[LibraryImport]` needs no
marshaller:

| Native | C# |
|---|---|
| `bool` (1 byte) | `byte` |
| `size_t` | `nuint` |
| `char*` / `const char*` (UTF-8) | `byte*` |
| opaque handle (`XStoreContextHandle`, …) | `IntPtr` |
| `HRESULT` | `int` |
| callback function pointer | `IntPtr`, or `delegate* unmanaged[Stdcall]<…>` under `NET5_0_OR_GREATER` |
| `XAsyncBlock*` | `XAsyncBlock*` |

## Idiomatic layer

- Async APIs (`…Async` + `…Result`) return `Task<T>`, built on `AsyncOperation<T>` in
  `src/GDK.Net/AsyncOperation.cs`. Accept an optional `CancellationToken`; it maps to `XAsyncCancel`,
  and `E_ABORT` surfaces as `OperationCanceledException`.
- Every HRESULT goes through `Hr.ThrowIfFailed`. Add family-specific codes to `HResult.cs` only if
  `XGameErr.h` defines them and give them a friendly message.
- Native handles are wrapped in a `SafeHandle` (see `src/GDK.Net/SafeHandles.cs`), never a bare
  `IntPtr` on the public surface.
- The "call once for the size, then again for the buffer" pattern is hidden: return `string`,
  `byte[]` or `IReadOnlyList<T>`. Where the header publishes a fixed maximum, use it and skip the
  size call.
- `Register…`/`Unregister…` pairs become .NET `event`s on a static class named after the family, with
  the registration token held privately. Callbacks must never let an exception cross the native
  boundary — follow `Trampolines.cs`.
- Public types are `sealed` unless designed for derivation, and carry XML doc comments. Enums are
  re-declared in idiomatic .NET casing and unit-tested against the raw values.
- **Keep the GDK's nouns.** The public API is a projection, not a redesign: a title that knows the
  GDK docs must be able to find the same concept here by name. Drop the `X`/`Xbl` prefix and the
  redundant family qualifier, convert to .NET casing, and then **preserve the header's terms
  verbatim** — do not paraphrase, abbreviate, expand or "improve" them. `XUserPlatformSpopPrompt…`
  keeps `Spop` and `Prompt`; `XblTitleStorageBlobMetadata` keeps `Blob` rather than becoming `File`.
  Never invent a family noun the header does not use: the `XUserPlatform` family is `UserPlatform`,
  not `UserPlatformPrompts`, even when the added word is accurate.

  Where .NET convention genuinely forces a change, make it and **say so in the XML docs, naming the
  native symbol**. Set the bar high: a conflict with the language or the framework counts, a
  stylistic preference does not. Imperative C callback names are kept where the imperative *is* the
  contract — `UserPlatform.RemoteConnectShowPrompt` tells the title to show a prompt, so it is not
  softened into a past-tense "…Requested". An unexplained rename is the defect; an explained one is
  a documented mapping.

  **Do not add a prefix to "avoid ambiguity."** The GDK does not collide with itself, so a name that
  is unambiguous in the header is unambiguous here, and the .NET namespace already supplies what the
  C prefix was for. Prefix only when the bare name is an actual compile error, and say so in the
  docs. `XUser` is projected as `User`, not `GameUser`, even though titles commonly have a `User` of
  their own — a consumer resolves that with a `using` alias in one line.
- **Never surface a task queue.** `GameTaskQueue` and everything named `GameTaskQueue*` are
  `internal`. No public method, constructor, property or field may accept, return or expose one, and
  no public API may offer a queue as an optional parameter "for flexibility". Pass `IntPtr.Zero` (or
  a null `GameTaskQueue?`, which `RawHandle()` maps to `IntPtr.Zero`) so the Gaming Runtime resolves
  the process default. The rationale is in `docs/plan.md` §7: the projection already hops runtime
  callbacks to the thread pool to stay deadlock-free, which forfeits the ordering guarantee a manual
  completion port exists to provide. The compiler enforces most of this through accessibility, and
  `TaskQueueAdvancedTests.NoPublicApiMentionsATaskQueue` catches the rest.

### `DoWork`-pumped managers

XSAPI's manager layers — social, achievements and multiplayer — do not use `XAsyncBlock` at all.
They report every result and every unsolicited notification through one polled queue drained by
`Xbl…ManagerDoWork(const Event** events, size_t* count)`, which the title calls once per frame.
[`docs/reference/state-change.md`](../docs/reference/state-change.md) is the language-neutral
contract for that shape; these are the .NET-specific decisions it leaves open.

- **`DoWork` snapshots; it does not lend.** PFMP and Party bracket a batch with `Start…`/`Finish…`,
  which C# can hold open with an enumerator's `Dispose`. XSAPI has **no `Finish`** — the array is
  valid only until the *next* `DoWork` — so there is no scope to bind a borrow to, and a record
  handed to the caller would silently dangle one frame later. `DoWork` therefore copies everything
  it needs into managed objects before returning. This is a deliberate deviation from
  `state-change.md` §6.4 and each manager's XML docs say so.
- **One list, one order.** `DoWork` returns `IReadOnlyList<…ManagerEvent>`, an abstract `record`
  hierarchy with one derived record per native event type, completions and notifications
  interleaved in native order. Do not demultiplex a drain into .NET `event`s or `Task`s: the total
  order is the reason the loop exists.
- **Correlate by operation id, not by `await`.** Where the native call takes a `void* context` that
  the completion echoes back, project it as an opaque typed id returned from the start call and
  carried on the completion record. The raw pointer never reaches the caller, and these operations
  have no cancellation.
- **Long-lived handles keep their identity.** Anything that outlives the batch — a social user
  group, an achievements result handle — is a wrapper resolved through a handle-to-wrapper map, so
  the object inside an event is reference-equal to the one the caller created. Use after teardown
  throws `ObjectDisposedException`.
- **Say that the pump is mandatory.** Nothing progresses without it, and `DoWork` is not safe to
  call concurrently with other calls on the same manager. Both facts belong in the type-level docs.

## NativeAOT

Xbox consoles forbid JIT, so every line added here must survive ahead-of-time compilation. The
analyzers are on (`EnableAotAnalyzer`, `EnableTrimAnalyzer`, `EnableSingleFileAnalyzer`) and
warnings are errors, so most violations stop the build — but the analyzers cannot see everything, so
follow these rules directly:

- **`[LibraryImport]` on `net7.0`+, never `[DllImport]`.** The generator emits marshalling code at
  compile time; `[DllImport]` with non-blittable parameters builds a stub at run time, which AOT
  cannot do. This is why the blittable-types table above is a hard requirement rather than a
  preference — keep `bool` as `byte` and convert at the call site.
- **Callbacks are `static` + `[UnmanagedCallersOnly]`.** Never `Marshal.GetFunctionPointerForDelegate`
  outside a `netstandard2.0` `#else` branch. Per-call state travels through the `context` pointer the
  GDK API already provides (see `Trampolines.cs`), not through a captured closure.
- **No reflection on the public surface.** No `Activator.CreateInstance`, no `Type.GetType`, no
  `MakeGenericType`. `Marshal.SizeOf<T>()` is acceptable only for a blittable struct the trimmer can
  see statically; prefer `sizeof(T)` in an `unsafe` context.
- **No anonymous types in serialized payloads.** `System.Text.Json` cannot source-generate them.
  Declare a named type and add it to a `JsonSerializerContext`.
- **Never suppress with `[RequiresDynamicCode]` or `[RequiresUnreferencedCode]`.** Propagating the
  attribute pushes the failure onto the game developer, who cannot fix it. Redesign instead.

`tests/GDK.Net.Tests/AotCompatibilityTests.cs` asserts these structurally over the built assembly,
so adding a non-conforming entry point fails an ordinary `dotnet test`. Definitive proof is a real
ILC run: CI AOT-publishes the sample on every push, and
`eng/run-package-tests.ps1 -Tier Layout,Register -Aot` executes the AOT build inside a real package.

## Validation

`Directory.Build.props` sets `TreatWarningsAsErrors` and `Nullable=enable`, so this must be clean:

```powershell
dotnet build -c Release
dotnet test -c Release --no-build
```

Tests must never call native code: they run on hosted CI runners with no Gaming Runtime. Assert enum
values against the header, struct sizes and field offsets, and error-model behaviour. Live coverage
belongs in `tests/GDK.Net.LiveHarness` and `eng/run-package-tests.ps1`.
