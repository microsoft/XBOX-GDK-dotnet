# GDK.Net

Idiomatic .NET APIs for the Microsoft GDK, including Xbox Live and PlayFab.
The same architecture-neutral assembly supports `net8.0`, `net10.0` and
`netstandard2.0`. Native runtime inputs support Windows x64 and arm64.

Each package version, `<GDK edition>.<library major>.<library minor>`, requires
**exact** matching `Microsoft.GDK.Windows` and `Microsoft.GDK.Core` versions.
They restore as dependencies; their native SDK payload is not bundled here.
Do not override either dependency with another version.

Build and publish automatically copy `xgameruntime.thunks.dll` from the restored
Windows SDK, selected by `win-x64` or `win-arm64` (x64 when no RID is specified).
Installed SDK environment variables are ignored. Optional native families are
enabled in the application project:

```xml
<PropertyGroup>
  <GdkNetIncludeXboxLive>true</GdkNetIncludeXboxLive>
  <GdkNetIncludePlayFab>true</GdkNetIncludePlayFab>
  <GdkNetIncludePlayFabMultiplayer>true</GdkNetIncludePlayFabMultiplayer>
  <GdkNetIncludePlayFabParty>true</GdkNetIncludePlayFabParty>
</PropertyGroup>
```

All optional families default to false. Multiplayer and Party imply the PlayFab
core family even when `GdkNetIncludePlayFab` is explicitly false. Missing selected
DLLs or conflicting SDK versions fail the build. No source generation, Python,
PowerShell, SDK installation or runtime registration is performed by this package.
Consumer build integration uses the .NET SDK's MSBuild and NuGet assemblies.

NativeAOT publishing is supported for modern .NET Windows applications;
`netstandard2.0` is a library compatibility target, not an AOT publish target.
Runtime use still requires Gaming Runtime prerequisites and the title's own
identity/configuration. Compilation is not a live-service validation.

[Getting started](https://github.com/microsoft/XBOX-GDK-dotnet/blob/main/docs/getting-started.md)
· [Build and test](https://github.com/microsoft/XBOX-GDK-dotnet/blob/main/docs/building.md)
· [Source](https://github.com/microsoft/XBOX-GDK-dotnet)

Licensed under MIT; see the included `LICENSE`. SDK dependencies retain their
own licenses and redistribution terms.
