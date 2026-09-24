# Support

## How to file issues and get help

This project uses GitHub Issues to track bugs and feature requests. Please search the existing
issues before filing new ones to avoid duplicates. For new issues, file your bug or feature request
as a new Issue.

When reporting a bug, it helps a great deal to include:

- The **GDK edition** you built against (this repository pins `260404`).
- The **target framework** (`net8.0`, `net10.0` or `netstandard2.0`) and whether the build was
  JIT or NativeAOT.
- Whether you were running **packaged**, or unpackaged with a `MicrosoftGame.config` beside the
  executable.
- The **HRESULT**, if one surfaced. `GameRuntimeException` preserves the numeric value even when the
  failure is projected as a more idiomatic exception type.

Please do **not** file security vulnerabilities as GitHub issues. See [SECURITY.md](SECURITY.md).

## Microsoft Support Policy

Support for this project is limited to the resources listed above.
