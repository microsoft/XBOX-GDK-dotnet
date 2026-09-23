"""Emits `src/GDK.Net/PlayFab/PlayFabErrors.cs` from `playfab/core/PFErrors.h`.

The header spells every code as `MAKE_E_HC(0x....L)` with the resolved HRESULT in a trailing
comment; that comment is the source of truth here so the projection never has to reimplement the
libHttpClient facility macro.
"""

from __future__ import annotations

import os
import re

import pfmodel

PATTERN = re.compile(
    r"^#define\s+(E_PF_\w+)\s+MAKE_E_HC\(0x[0-9A-Fa-f]+L\)\s*//\s*(0x[0-9A-Fa-f]+)",
    re.MULTILINE)


def managed(name: str) -> str:
    parts = [part for part in name[len("E_PF_"):].split("_") if part]
    return "".join(part[:1].upper() + part[1:].lower() for part in parts)


def main() -> int:
    header = os.path.join(
        os.environ["GameDKCoreLatest"], "windows", "include", "playfab", "core", "PFErrors.h")
    with open(header, "r", encoding="utf-8-sig") as handle:
        text = handle.read()

    codes: list[tuple[str, str, int]] = []
    seen: set[str] = set()
    for native, value in PATTERN.findall(text):
        name = managed(native)
        while name in seen:
            name += "Code"
        seen.add(name)
        codes.append((name, native, int(value, 16)))

    lines = [
        pfmodel.banner("emit_errors.py", "core/PFErrors.h"),
        "",
        "#nullable enable",
        "",
        "namespace GDK.Net.PlayFab;",
        "",
        "/// <summary>",
        "/// The <c>E_PF_*</c> HRESULT codes PlayFab returns (<c>playfab/core/PFErrors.h</c>).",
        "/// </summary>",
        "/// <remarks>",
        "/// A failing PlayFab call surfaces as <see cref=\"PlayFabException\"/>, whose",
        "/// <see cref=\"GameRuntimeException.HResultCode\"/> is one of these values. They are",
        "/// constants rather than an enum because the service may add codes this projection's GDK",
        "/// edition does not know about, and an unknown enum member is worse than an unknown int.",
        "/// </remarks>",
        "public static class PlayFabErrors",
        "{",
    ]

    for name, native, value in codes:
        lines.append(f"    /// <summary><c>{native}</c>.</summary>")
        lines.append(f"    public const int {name} = unchecked((int)0x{value:08X});")
        lines.append("")

    lines += [
        "    /// <summary>",
        "    /// Whether <paramref name=\"hresult\"/> is in the facility PlayFab and libHttpClient",
        "    /// share (<c>FACILITY_XBOX</c> + <c>0x23</c>).",
        "    /// </summary>",
        "    public static bool IsPlayFab(int hresult) => ((uint)hresult >> 16) == 0x8923;",
        "",
        "    /// <summary>",
        "    /// Returns the <c>E_PF_*</c> symbol for a code, or <see langword=\"null\"/> when this",
        "    /// GDK edition does not define one.",
        "    /// </summary>",
        "    public static string? GetName(int hresult) => hresult switch",
        "    {",
    ]
    for name, native, _ in codes:
        lines.append(f"        {name} => \"{native}\",")
    lines += [
        "        _ => null,",
        "    };",
        "}",
        "",
    ]

    path = os.path.join(pfmodel.PLAYFAB_DIR, "PlayFabErrors.cs")
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8", newline="\r\n") as handle:
        handle.write("\n".join(lines))
    print(f"emitted {len(codes)} PlayFab error codes")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
