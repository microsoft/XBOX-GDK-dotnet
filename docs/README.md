# GDK.Net documentation

Full documentation tree for the .NET projection of the Microsoft GDK.

## Start here

- [**Getting started**](getting-started.md): the idioms the projection uses, initialising
  `GameRuntime`, signing in a user
- [**Building**](building.md): prerequisites, build and test, running unpackaged, packaging, the
  generation tooling
- [**Architecture**](architecture.md): the two layers, the native modules, the three target
  frameworks, the AOT contract, threading
- [**Repository layout**](repository-layout.md): where things live, and what is generated or
  vendored rather than authored

## Reference

- [**API reference**](api/): every public type and member, generated from the XML doc comments,
  indexed by area
- [**Projection status**](status.md): which families are projected, which module each binds to,
  what is out of scope, and the current coverage numbers
- [**The pinned GDK edition**](gdk-edition.md): minimum version and the full re-pin procedure

## Guides

- [**NativeAOT**](native-aot.md): why console requires it, how it is enforced, and how to
  AOT-publish a title
- [**Title-implemented UI**](custom-game-ui.md): drawing the runtime's dialogs yourself, and the
  threading rules that come with it

## Specification

`plan.md` and everything under `reference/` are **vendored**: one-way copies of the shared
specification from the `gdk-projections-plans` meta repo. Do not edit them here; fix the source and
re-copy.

- [**`plan.md`**](plan.md): the authoritative implementation specification for this repository
- [**`reference/`**](reference/): the shared, language-neutral reference documents:
  [gdk-surface](reference/gdk-surface.md), [xuser-pilot](reference/xuser-pilot.md),
  [state-change](reference/state-change.md), [multiplayer-pilot](reference/multiplayer-pilot.md),
  [roadmap](reference/roadmap.md), [security-privacy](reference/security-privacy.md),
  [compliance](reference/compliance.md), [glossary](reference/glossary.md),
  [testing](reference/testing.md)
