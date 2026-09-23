"""Shared model helpers for the PlayFab projection generator.

Loads `model.json` (produced by `parse_headers.py`) and `exports.json` (produced by
`generate-playfab.ps1`) and exposes the indices the emitters need: the native-to-C# type map, the
per-header family assignment, and the classification of a struct field into one of the shapes the
PlayFab data model uses (string, optional scalar, array, dictionary, ...).
"""

from __future__ import annotations

import json
import os
import re

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.abspath(os.path.join(HERE, "..", ".."))
INTEROP_DIR = os.path.join(REPO, "src", "GDK.Net", "Interop")
PLAYFAB_DIR = os.path.join(REPO, "src", "GDK.Net", "PlayFab")
MODELS_DIR = os.path.join(PLAYFAB_DIR, "Models")

# --------------------------------------------------------------------------------------------
# Families
# --------------------------------------------------------------------------------------------

# One family per header pair (`PFXxx.h` + `PFXxxTypes.h`), matching the file-per-family rule in
# eng/interop-conventions.md.
_FAMILY_OVERRIDES = {
    "core/PFPal.h": "PFCore",
    "core/PFTypes.h": "PFCore",
    "core/PFCore.h": "PFCore",
    "core/PFErrors.h": "PFErrors",
    "core/PFTrace.h": "PFTrace",
    "core/PFPlatform.h": "PFPlatform",
    "services/PFTypes.h": "PFServices",
    "services/PFServices.h": "PFServices",
    "multiplayer/PFEntityKey.h": "PFMultiplayer",
    "multiplayer/PFMultiplayerPal.h": "PFMultiplayer",
    "multiplayer/PFMultiplayer.h": "PFMultiplayer",
    "party/PartyPal.h": "Party",
    "party/PartyTypes.h": "Party",
    "party/Party_c.h": "Party",
    "party/PartyXboxLive_c.h": "PartyXboxLive",
}

# Families whose idiomatic layer is generated. Everything else is bound as raw interop and given a
# hand-written idiomatic layer (the state-change families) or is infrastructure with no public
# surface of its own.
HANDWRITTEN_FAMILIES = {
    "PFCore",
    "PFErrors",
    "PFTrace",
    "PFPlatform",
    "PFServiceConfig",
    "PFEntity",
    "PFEventPipeline",
    "PFEvents",
    "PFHttpConfig",
    "PFLocalUser",
    "PFGameSaveFiles",
    "PFLobby",
    "PFMatchmaking",
    "PFMultiplayer",
    "Party",
    "PartyXboxLive",
}

# Families whose *models* are still generated even though the API is hand-written, because the
# hand-written layer projects the same plain-old-data structs.
MODEL_ONLY_FAMILIES = {"PFCore", "PFServices", "PFGameSaveFiles"}


def family_of(header: str) -> str:
    if header in _FAMILY_OVERRIDES:
        return _FAMILY_OVERRIDES[header]
    name = header.split("/")[-1]
    name = re.sub(r"\.h$", "", name)
    name = re.sub(r"(Types)?(_Xbox|_Steam)?$", "", name)
    return name


# --------------------------------------------------------------------------------------------
# Model
# --------------------------------------------------------------------------------------------

BUILTINS = {
    "void": "void",
    "char": "byte",
    "bool": "byte",
    "int8_t": "sbyte",
    "uint8_t": "byte",
    "int16_t": "short",
    "uint16_t": "ushort",
    "int32_t": "int",
    "uint32_t": "uint",
    "int64_t": "long",
    "uint64_t": "ulong",
    "float": "float",
    "double": "double",
    "size_t": "nuint",
    "time_t": "long",
    "HRESULT": "int",
    "XAsyncBlock": "XAsyncBlock",
    "XTaskQueueHandle": "IntPtr",
    "XUserHandle": "IntPtr",
    "XTaskQueueRegistrationToken": "XTaskQueueRegistrationToken",
    # libHttpClient enum reachable from PFEventPipeline; projected as its underlying type so the
    # projection does not take a dependency on the libHttpClient headers.
    "HCCompressionLevel": "uint",
}

DICTIONARY_SUFFIX = "DictionaryEntry"

CSHARP_KEYWORDS = {
    "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
    "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
    "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if",
    "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new",
    "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
    "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static",
    "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
    "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while",
}


class Model:
    def __init__(self, model_path: str, exports_path: str) -> None:
        with open(model_path, "r", encoding="utf-8") as handle:
            raw = json.load(handle)
        with open(exports_path, "r", encoding="utf-8") as handle:
            self.exports: dict[str, str] = json.load(handle)

        self.headers = raw["headers"]
        self.enums: dict[str, dict] = {}
        self.structs: dict[str, dict] = {}
        self.handles: set[str] = set()
        self.callbacks: set[str] = set()
        self.aliases: dict[str, dict] = {}
        self.constants: dict[str, int] = {}
        self.functions: dict[str, dict] = {}

        for header in self.headers:
            family = family_of(header["header"])
            for entry in header["enums"]:
                self.enums.setdefault(entry["name"], dict(entry, family=family))
            for entry in header["structs"]:
                self.structs.setdefault(entry["name"], dict(entry, family=family))
            for entry in header["handles"]:
                self.handles.add(entry["name"])
            for entry in header["callbacks"]:
                self.callbacks.add(entry["name"])
            for entry in header["aliases"]:
                self.aliases.setdefault(entry["name"], entry)
            for entry in header["constants"]:
                try:
                    self.constants[entry["name"]] = int(entry["value"].rstrip("UL"), 0)
                except ValueError:
                    pass
            for entry in header["functions"]:
                self.functions.setdefault(entry["name"], dict(entry, family=family))

        # Handle typedefs also appear as aliases when a header spells them without `struct`.
        for name in self.handles:
            self.aliases.pop(name, None)

    # -- type resolution ---------------------------------------------------------------------

    def resolve(self, base: str, pointer: int) -> tuple[str, int]:
        """Follow alias typedefs to a concrete base type and pointer depth."""
        seen: set[str] = set()
        while base in self.aliases and base not in seen:
            seen.add(base)
            alias = self.aliases[base]
            pointer += alias["pointer"]
            base = alias["type"]
        return base, pointer

    def raw_type(self, base: str, pointer: int) -> str:
        base, pointer = self.resolve(base, pointer)

        if base in self.handles:
            # Opaque handles cross the boundary as IntPtr, per eng/interop-conventions.md. The
            # typedef already carries the indirection (`typedef struct PFEntity* PFEntityHandle`),
            # so an out-parameter (`PFEntityHandle*`) is still one level of indirection here.
            return "IntPtr" + "*" * pointer

        if base in self.callbacks:
            # Callback typedefs name the function *type*, so `Handler*` is the function pointer
            # itself and collapses to a single IntPtr.
            return "IntPtr" + "*" * max(pointer - 1, 0)

        if base in self.enums or base in self.structs:
            text = base
        elif base in BUILTINS:
            text = BUILTINS[base]
        else:
            raise KeyError(f"unmapped native type {base!r}")

        return text + "*" * pointer

    def is_struct(self, base: str) -> bool:
        base, _ = self.resolve(base, 0)
        return base in self.structs

    def is_enum(self, base: str) -> bool:
        base, _ = self.resolve(base, 0)
        return base in self.enums

    def constant(self, expression: str) -> int:
        expression = expression.strip()
        total = 0
        for term in expression.split("+"):
            term = term.strip()
            if term.isdigit():
                total += int(term)
            elif term in self.constants:
                total += self.constants[term]
            else:
                raise KeyError(f"unknown constant expression {expression!r}")
        return total


def load() -> Model:
    return Model(os.path.join(HERE, "model.json"), os.path.join(HERE, "exports.json"))


# --------------------------------------------------------------------------------------------
# Naming
# --------------------------------------------------------------------------------------------


def pascal(name: str) -> str:
    if not name:
        return name
    if name.isupper() or "_" in name:
        return "".join(part.capitalize() for part in name.split("_") if part)
    return name[0].upper() + name[1:]


def camel(name: str) -> str:
    if not name:
        return name
    text = pascal(name)
    return text[0].lower() + text[1:]


def escape(name: str) -> str:
    return "@" + name if name in CSHARP_KEYWORDS else name


def managed_name(native: str) -> str:
    """`PFFriendsFriendInfo` -> `FriendsFriendInfo`: drop only the C namespace prefix.

    Party spells its C names in SCREAMING_SNAKE; folding them to Pascal case reproduces the
    official `Party.h` C++ names verbatim (`PARTY_CHAT_PERMISSION_OPTIONS` ->
    `PartyChatPermissionOptions`), so those are kept prefix and all.
    """
    if native.startswith("PARTY_"):
        return pascal(native)
    return native[2:] if native.startswith("PF") and len(native) > 2 else native


def struct_field_names(entry: dict) -> dict[str, str]:
    """Map native field name -> emitted C# field name, applying the same de-duplication both
    emitters must agree on."""
    names: dict[str, str] = {}
    used: set[str] = set()
    for field in entry["fields"]:
        name = pascal(field["name"])
        if name == entry["name"] or name in used:
            name += "Value"
        used.add(name)
        names[field["name"]] = name
    return names


LIBRARIES = {
    "PlayFabCore.dll": "PlayFabCoreLibrary",
    "PlayFabServices.dll": "PlayFabServicesLibrary",
    "PlayFabGameSave.dll": "PlayFabGameSaveLibrary",
    "PlayFabMultiplayer.dll": "PlayFabMultiplayerLibrary",
    "Party.dll": "PartyLibrary",
    "PartyXboxLive.dll": "PartyXboxLiveLibrary",
}


HEADER_BANNER = """// <auto-generated>
//   Generated by eng/playfab/{tool} from the Microsoft GDK {edition} PlayFab headers.
//   Source header family: {family}
//   Do not edit by hand: re-run `pwsh -NoProfile -File eng/generate-playfab.ps1`.
// </auto-generated>
"""


def banner(tool: str, family: str, edition: str = "260404") -> str:
    return HEADER_BANNER.format(tool=tool, family=family, edition=edition)
