"""Parse the Microsoft GDK PlayFab C headers into a JSON model.

The PlayFab headers are extremely regular, which is what makes a generated projection possible:
every service API is a `PF<Family><Api>Async` / `GetResultSize` / `GetResult` triple over
plain-old-data request and result structs. This module is deliberately *not* a general C parser --
it understands exactly the shapes the PlayFab headers use, and records anything else so an
unhandled construct is visible instead of silently disappearing from the projection.

Usage:
    python parse_headers.py [--gdk <edition root>] [--out model.json]
"""

from __future__ import annotations

import argparse
import json
import os
import re
import sys

# --------------------------------------------------------------------------------------------
# Preprocessing
# --------------------------------------------------------------------------------------------

# The projection targets the GDK platform only. Every other HC_PLATFORM branch is dropped.
_TRUE_TOKENS = {
    "HC_PLATFORM == HC_PLATFORM_GDK",
    "defined(__cplusplus)",
    "__cplusplus",
    "PFMULTIPLAYER_INCLUDE_SERVER_APIS",
    "1",
}

_FALSE_PREFIXES = (
    "HC_PLATFORM == HC_PLATFORM_",   # any non-GDK platform
    "HC_PLATFORM_IS_",
    "HC_PLATFORM != HC_PLATFORM_GDK",
    "PARTY_SAL_SUPPORT",
    "PF_ENABLE_XAL_AUTH",
    "__clang__",
)


def _eval_condition(expr: str) -> bool:
    """Evaluate the small subset of preprocessor expressions the PlayFab headers use."""
    expr = expr.strip()

    if expr.startswith("!"):
        return not _eval_condition(expr[1:].strip())

    if "||" in expr:
        return any(_eval_condition(part) for part in expr.split("||"))
    if "&&" in expr:
        return all(_eval_condition(part) for part in expr.split("&&"))

    expr = expr.strip()
    if expr.startswith("(") and expr.endswith(")") and expr.count("(") == 1:
        expr = expr[1:-1].strip()

    if expr in _TRUE_TOKENS:
        return True
    if expr == "0":
        return False
    if expr.startswith("defined"):
        inner = expr[len("defined"):].strip()
        if inner.startswith("("):
            inner = inner[1:-1]
        return _eval_condition(inner.strip())
    for prefix in _FALSE_PREFIXES:
        if expr.startswith(prefix):
            return False
    # Unknown macro: treat as undefined, which is what the C preprocessor does. SAL guards
    # (`#ifndef _Maybenull_`) therefore take their "define it" branch, which is harmless because the
    # declarations we care about are matched with SAL still present.
    return False


def preprocess(text: str) -> str:
    """Resolve #if/#ifdef/#else/#endif for the GDK platform and drop the remaining directives."""
    out: list[str] = []
    # (currently_emitting, this_chain_already_taken)
    stack: list[tuple[bool, bool]] = []

    for line in text.splitlines():
        stripped = line.strip()
        emitting = all(active for active, _ in stack)

        if stripped.startswith("#"):
            directive = stripped[1:].strip()
            keyword = directive.split(None, 1)[0] if directive else ""
            rest = directive[len(keyword):].strip()

            if keyword in ("if", "ifdef", "ifndef"):
                if keyword == "ifdef":
                    value = _eval_condition(f"defined({rest})")
                elif keyword == "ifndef":
                    value = not _eval_condition(f"defined({rest})")
                else:
                    value = _eval_condition(rest)
                parent = all(active for active, _ in stack)
                stack.append((parent and value, value))
                continue
            if keyword == "elif":
                if not stack:
                    continue
                _, taken = stack.pop()
                parent = all(active for active, _ in stack)
                value = (not taken) and _eval_condition(rest)
                stack.append((parent and value, taken or value))
                continue
            if keyword == "else":
                if not stack:
                    continue
                _, taken = stack.pop()
                parent = all(active for active, _ in stack)
                stack.append((parent and not taken, True))
                continue
            if keyword == "endif":
                if stack:
                    stack.pop()
                continue
            if emitting and keyword == "define":
                out.append(line)
            continue

        if emitting:
            out.append(line)

    return "\n".join(out)


def strip_comments(text: str) -> str:
    text = re.sub(r"/\*.*?\*/", "", text, flags=re.S)
    text = re.sub(r"(?m)//.*$", "", text)
    return text


# --------------------------------------------------------------------------------------------
# Parsing
# --------------------------------------------------------------------------------------------

# SAL annotations carry information the projection needs (nullability, array counts), so they are
# captured per declaration rather than blanket-stripped.
# A SAL annotation is an identifier that starts and ends with an underscore and is spelled in the
# SAL casing convention. Matching the shape rather than an explicit list means a macro this
# projection has not seen before is still recognised instead of being mistaken for a type.
_SAL_WITH_ARGS = re.compile(r"\b(_[A-Z][A-Za-z0-9_]*_)\s*\([^()]*\)")
_SAL_PLAIN = re.compile(r"\b(_[A-Z][A-Za-z0-9_]*_)(?![\w(])")

# SAL macros whose argument names the element count of the annotated pointer.
_SIZE_MACROS = (
    "_Field_size_",
    "_Field_size_opt_",
    "_Field_size_bytes_",
    "_In_reads_",
    "_In_reads_opt_",
    "_In_reads_bytes_",
    "_Outptr_result_buffer_",
    "_Outptr_result_buffer_maybenull_",
)

_ENUM_CLASS = re.compile(
    r"enum\s+class\s+(?P<name>\w+)\s*(?::\s*(?P<base>[\w ]+?))?\s*\{(?P<body>[^}]*)\}\s*;", re.S
)
_ENUM_C = re.compile(
    r"typedef\s+enum\s+(?P<tag>\w+)\s*\{(?P<body>[^}]*)\}\s*(?P<name>\w+)\s*;", re.S
)
_STRUCT = re.compile(
    r"typedef\s+struct\s+(?P<tag>\w+)?\s*\{(?P<body>[^{}]*?)\}\s*(?P<name>\w+)\s*;", re.S
)
# PFMP and Party declare several structs in the plain C++ form, with no typedef.
_STRUCT_PLAIN = re.compile(
    r"(?<!typedef )\bstruct\s+(?P<name>\w+)\s*(?::\s*(?:public\s+)?(?P<base>\w+)\s*)?"
    r"\{(?P<body>[^{}]*?)\}\s*;",
    re.S,
)
_HANDLE = re.compile(
    r"typedef\s+(?:const\s+)?struct\s+(?P<tag>\w+)\s*\*\s*(?P<name>\w+)\s*;"
)
# `typedef uint64_t PFRegistrationToken;` / `typedef const char* PartyString;`
_TYPEDEF_ALIAS = re.compile(
    r"typedef\s+(?!struct\b|enum\b|union\b|void\b)(?P<from>(?:const\s+)?\w+\s*\**)\s*(?P<name>\w+)\s*;"
)
# Function-pointer typedefs: `typedef void (__stdcall* Name)(...)` (Party, PFMP, GameSave).
_CALLBACK_PTR = re.compile(
    r"typedef\s+(?P<ret>(?:[^;{}()]|\([^()]*\))*?)\(\s*(?:__stdcall|PARTY_API|PARTY_CALLBACK|PFMULTIPLAYER_API|CALLBACK)"
    r"\s*\*\s*(?P<name>\w+)\s*\)\s*\((?P<params>[^;]*?)\)\s*;",
    re.S,
)
# Function-type typedefs: `typedef void CALLBACK Name(...)` (PlayFab Core event handlers).
_CALLBACK_FUNC = re.compile(
    r"typedef\s+(?P<ret>[\w \*]*?)\s*\b(?:CALLBACK|PARTY_CALLBACK|STDAPIVCALLTYPE)\s+(?P<name>\w+)"
    r"\s*\((?P<params>[^;]*?)\)\s*;",
    re.S,
)
_DEFINE_INT = re.compile(
    r"(?m)^\s*#define\s+(?P<name>[A-Z][A-Z0-9_]*)\s+(?P<value>-?(?:0x[0-9A-Fa-f]+|\d+)[UL]*)\s*$"
)

# `PF_API Name(...) noexcept;` and `PF_API_(ret) Name(...) noexcept;`
_PF_API = re.compile(
    r"\bPF_API(?:_\((?P<ret>[^)]*)\))?\s+(?P<name>\w+)\s*\((?P<params>[^;]*?)\)\s*(?:noexcept)?\s*;", re.S
)
# Party / PFMP style: `<attrs> <ret> PARTY_API Name(...) noexcept;`
_MACRO_API = re.compile(
    r"(?P<ret>(?:const\s+)?[\w:]+\s*\**)\s*\b(?:PARTY_API|PFMULTIPLAYER_API)\s+(?P<name>\w+)"
    r"\s*\((?P<params>[^;]*?)\)\s*(?:noexcept)?\s*;",
    re.S,
)


def _clean_sal(text: str) -> tuple[str, set[str], str | None]:
    """Return (text without SAL, set of SAL tokens, the `_Field_size_`-style argument if any)."""
    field_size: str | None = None
    tokens: set[str] = set()

    def take_args(match: re.Match[str]) -> str:
        nonlocal field_size
        macro = match.group(1)
        args = match.group(0)[len(macro):].strip()[1:-1].strip()
        tokens.add(macro)
        if macro in _SIZE_MACROS:
            field_size = args
        return " "

    text = _SAL_WITH_ARGS.sub(take_args, text)

    def take_plain(match: re.Match[str]) -> str:
        tokens.add(match.group(1))
        return " "

    text = _SAL_PLAIN.sub(take_plain, text)
    return re.sub(r"\s+", " ", text).strip(), tokens, field_size


def _split_top_level(text: str, separator: str = ",") -> list[str]:
    parts: list[str] = []
    depth = 0
    current: list[str] = []
    for ch in text:
        if ch in "([{":
            depth += 1
        elif ch in ")]}":
            depth -= 1
        if ch == separator and depth == 0:
            parts.append("".join(current))
            current = []
        else:
            current.append(ch)
    if "".join(current).strip():
        parts.append("".join(current))
    return [p.strip() for p in parts if p.strip()]


_ARRAY_SUFFIX = re.compile(r"^(?P<name>\w+)\[(?P<count>[^\]]*)\]$")


def _is_nullable(sal: set[str]) -> bool:
    return any("_opt_" in token or token.lower().endswith("maybenull_") for token in sal)


def _parse_declaration(decl: str, index: int = 0) -> dict | None:
    """Parse one struct field or function parameter into a small descriptor dictionary."""
    decl, sal, field_size = _clean_sal(decl)
    decl = re.sub(r"\bstruct\b", " ", decl).strip()
    decl = re.sub(r"\s+", " ", decl)
    # `char name[MAX_LEN + 1]` must tokenise as two tokens, not four.
    decl = re.sub(r"\[\s*([^\]]*?)\s*\]", lambda m: "[" + re.sub(r"\s+", "", m.group(1)) + "]", decl)
    if not decl or decl in ("void", "..."):
        return None

    if "(" in decl:
        raise ValueError(f"unsupported declaration: {decl!r}")

    tokens = decl.split()

    # An unnamed parameter carries only a type. Name it positionally so the projection can still
    # emit a signature for it.
    if len(tokens) == 1 and not tokens[0].startswith("*"):
        name = f"value{index}"
        type_tokens = tokens
    else:
        name = tokens[-1]
        type_tokens = tokens[:-1]

    array_len: str | None = None
    match = _ARRAY_SUFFIX.match(name)
    if match:
        name = match.group("name")
        array_len = match.group("count").strip() or None

    pointer = 0
    while name.startswith("*"):
        pointer += 1
        name = name[1:]
    type_text = " ".join(type_tokens)
    pointer += type_text.count("*")
    type_text = type_text.replace("*", " ")

    base = " ".join(t for t in type_text.split() if t not in ("const", "volatile", "enum"))
    if not base or not re.fullmatch(r"\w+", name or ""):
        raise ValueError(f"unsupported declaration: {decl!r}")

    return {
        "name": name,
        "type": base,
        "pointer": pointer,
        "const": "const" in type_text.split(),
        "array": array_len,
        "nullable": _is_nullable(sal),
        "sizeField": field_size,
        "sal": sorted(sal),
    }


def _parse_enum_body(body: str) -> list[dict]:
    members: list[dict] = []
    for entry in _split_top_level(body):
        entry = entry.strip().rstrip(",")
        if not entry:
            continue
        if "=" in entry:
            name, _, value = entry.partition("=")
            members.append({"name": name.strip(), "value": value.strip()})
        else:
            members.append({"name": entry.strip(), "value": None})
    return members


def parse_header(path: str, relative: str) -> dict:
    with open(path, "r", encoding="utf-8-sig", errors="replace") as handle:
        raw = handle.read()

    text = preprocess(strip_comments(raw))
    # Handle, alias and callback typedefs carry SAL in their *return type*, which the declaration
    # grammar below cannot see past. Match those three shapes against a SAL-free copy.
    sal_free = _SAL_WITH_ARGS.sub(" ", _SAL_PLAIN.sub(" ", text))

    model: dict = {
        "header": relative,
        "enums": [],
        "structs": [],
        "handles": [],
        "aliases": [],
        "callbacks": [],
        "functions": [],
        "constants": [],
        "unparsed": [],
    }

    for match in _ENUM_CLASS.finditer(text):
        model["enums"].append(
            {
                "name": match.group("name"),
                "base": (match.group("base") or "int").strip(),
                "flags": f'DEFINE_ENUM_FLAG_OPERATORS({match.group("name")})' in text,
                "style": "cpp",
                "members": _parse_enum_body(match.group("body")),
            }
        )

    for match in _ENUM_C.finditer(text):
        model["enums"].append(
            {
                "name": match.group("name"),
                "base": "uint32_t",
                "flags": False,
                "style": "c",
                "members": _parse_enum_body(match.group("body")),
            }
        )

    for match in _HANDLE.finditer(sal_free):
        model["handles"].append({"name": match.group("name"), "tag": match.group("tag")})

    handle_names = {h["name"] for h in model["handles"]}
    for match in _TYPEDEF_ALIAS.finditer(sal_free):
        name = match.group("name")
        if name in handle_names:
            continue
        source, _, _ = _clean_sal(match.group("from"))
        source = source.strip()
        pointer = source.count("*")
        base = " ".join(t for t in source.replace("*", " ").split() if t != "const")
        if not base:
            continue
        model["aliases"].append({"name": name, "type": base, "pointer": pointer})

    for pattern in (_CALLBACK_PTR, _CALLBACK_FUNC):
        for match in pattern.finditer(sal_free):
            params = []
            for index, part in enumerate(_split_top_level(match.group("params"))):
                try:
                    parsed = _parse_declaration(part, index)
                except ValueError:
                    parsed = None
                if parsed:
                    params.append(parsed)
            returns, _, _ = _clean_sal(match.group("ret"))
            model["callbacks"].append(
                {"name": match.group("name"), "returns": returns.strip(), "parameters": params}
            )

    callback_names = {c["name"] for c in model["callbacks"]}

    for pattern in (_STRUCT, _STRUCT_PLAIN):
        for match in pattern.finditer(text):
            name = match.group("name")
            if any(s["name"] == name for s in model["structs"]):
                continue
            fields: list[dict] = []
            failure: str | None = None
            for index, part in enumerate(_split_top_level(match.group("body"), ";")):
                part = part.strip()
                if not part:
                    continue
                try:
                    parsed = _parse_declaration(part, index)
                except ValueError as error:
                    failure = str(error)
                    break
                if parsed:
                    fields.append(parsed)
            if failure:
                model["unparsed"].append(f"struct {name}: {failure}")
                continue
            model["structs"].append(
                {
                    "name": name,
                    "tag": match.groupdict().get("tag"),
                    "base": match.groupdict().get("base"),
                    "fields": fields,
                }
            )

    seen: set[str] = set()
    for pattern in (_PF_API, _MACRO_API):
        for match in pattern.finditer(text):
            name = match.group("name")
            if name in seen or name in callback_names:
                continue
            returns = (match.groupdict().get("ret") or "HRESULT").strip() or "HRESULT"
            returns = returns.replace("const", "").strip()
            params = []
            failure: str | None = None
            for index, part in enumerate(_split_top_level(match.group("params"))):
                try:
                    parsed = _parse_declaration(part, index)
                except ValueError as error:
                    failure = str(error)
                    break
                if parsed:
                    params.append(parsed)
            if failure:
                model["unparsed"].append(f"function {name}: {failure}")
                continue
            seen.add(name)
            model["functions"].append({"name": name, "returns": returns, "parameters": params})

    for match in _DEFINE_INT.finditer(text):
        model["constants"].append({"name": match.group("name"), "value": match.group("value")})

    return model


HEADER_GROUPS = ("core", "services", "gamesave", "multiplayer", "party")

# The C++ wrapper headers restate the flat C surface as classes; the projection binds only the
# flat surface, so they are not parsed.
SKIPPED_HEADERS = {
    "Party.h",
    "PartyImpl.h",
    "PartyXboxLive.h",
    "PartyXboxLiveImpl.h",
    "PFPlatformAndroid.h",
}


def _infer_size_fields(headers: list[dict]) -> None:
    """
    Infers the count field for array members the headers leave un-annotated.

    Most PlayFab headers mark arrays with `_Field_size_`, but the multiplayer headers instead rely
    on the convention that an array is immediately followed by its `<singular>Count`.
    """
    counters = {"uint32_t", "uint64_t", "size_t", "int32_t"}
    for header in headers:
        for entry in header["structs"]:
            fields = entry["fields"]
            for index, field in enumerate(fields[:-1]):
                if field["sizeField"] or field["pointer"] == 0 or field["array"]:
                    continue
                following = fields[index + 1]
                if following["type"] not in counters or following["pointer"]:
                    continue
                name = following["name"]
                if not name.endswith("Count") or len(name) == len("Count"):
                    continue
                stem = name[: -len("Count")]
                if field["name"].lower().startswith(stem.lower()):
                    field["sizeField"] = name


def _flatten_bases(headers: list[dict]) -> None:
    """Inlines C++ base-struct fields, which the state-change hierarchies rely on."""
    by_name = {s["name"]: s for header in headers for s in header["structs"]}

    def resolve(entry: dict, seen: set[str]) -> list[dict]:
        base = entry.get("base")
        if not base or base not in by_name or entry["name"] in seen:
            return entry["fields"]
        seen.add(entry["name"])
        return resolve(by_name[base], seen) + entry["fields"]

    for entry in by_name.values():
        entry["fields"] = resolve(entry, set())
        for index, field in enumerate(entry["fields"]):
            field["index"] = index


def build_model(include_root: str) -> dict:
    headers: list[dict] = []
    for group in HEADER_GROUPS:
        directory = os.path.join(include_root, group)
        if not os.path.isdir(directory):
            continue
        for name in sorted(os.listdir(directory)):
            if not name.endswith(".h") or name in SKIPPED_HEADERS:
                continue
            headers.append(parse_header(os.path.join(directory, name), f"{group}/{name}"))
    _infer_size_fields(headers)
    _flatten_bases(headers)
    return {"headers": headers}


def main(argv: list[str]) -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--gdk", default=os.environ.get("GameDKCoreLatest"))
    parser.add_argument("--out", default=os.path.join(os.path.dirname(__file__), "model.json"))
    args = parser.parse_args(argv)

    if not args.gdk:
        print("GameDKCoreLatest is not set and --gdk was not supplied.", file=sys.stderr)
        return 1

    include_root = os.path.join(args.gdk, "windows", "include", "playfab")
    if not os.path.isdir(include_root):
        print(f"Not found: {include_root}", file=sys.stderr)
        return 1

    model = build_model(include_root)
    with open(args.out, "w", encoding="utf-8") as handle:
        json.dump(model, handle, indent=1)

    functions = sum(len(h["functions"]) for h in model["headers"])
    structs = sum(len(h["structs"]) for h in model["headers"])
    enums = sum(len(h["enums"]) for h in model["headers"])
    unparsed = [e for h in model["headers"] for e in h["unparsed"]]
    print(f"headers={len(model['headers'])} functions={functions} structs={structs} enums={enums}")
    if unparsed:
        print(f"unparsed={len(unparsed)}")
        for entry in unparsed[:60]:
            print(f"  {entry}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main(sys.argv[1:]))
