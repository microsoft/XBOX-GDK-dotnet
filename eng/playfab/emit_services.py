"""Emits the idiomatic PlayFab layer: managed models, public enums and service classes.

Run through `eng/generate-playfab.ps1`. Output lands in `src/GDK.Net/PlayFab/`:

* `Models/<Family>Models.cs` — one `public sealed class` per PlayFab request/result struct plus the
  public enums those models reference. Each model can read itself out of the native struct
  (`FromNative`) and write itself into one (`WriteTo`), so no caller ever sees an interop type.
* `<Family>.cs` — a static class per service family with one `Task`-returning method per
  `PFXxxAsync` / `PFXxxGetResult(Size)` triple.
"""

from __future__ import annotations

import os
from typing import Optional

import emit_native
import pfmodel
from pfmodel import camel, escape, managed_name, pascal

# --------------------------------------------------------------------------------------------
# Type mapping
# --------------------------------------------------------------------------------------------

SCALARS = {
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
}

# Structs the projection maps onto a primitive instead of a model class.
JSON_STRUCT = "PFJsonObject"

# Structs that must always have a model, even when no generated service references them.
EXTRA_ROOTS = (
    "PFEntityKey", "PFEntityToken", "PFAuthenticationEntityTokenResponse", "PFGameSaveDescriptor",
    "PFLobbyCreateConfiguration", "PFLobbyJoinConfiguration", "PFLobbyArrangedJoinConfiguration",
    "PFLobbyServerJoinConfiguration", "PFLobbyDataUpdate", "PFLobbyMemberDataUpdate",
    "PFLobbyServerDataUpdate", "PFLobbySearchConfiguration", "PFLobbySearchFriendsFilter",
    "PFLobbySearchResult", "PFLobbyMemberUpdateSummary", "PFMatchmakingTicketConfiguration",
    "PFMatchmakingServerBackfillTicketConfiguration", "PFMatchmakingMatchDetails",
    "PFMatchmakingMatchMember", "PFMultiplayerServerDetails")

# Families whose enums are projected publicly even though their APIs are hand-written.
EXTRA_ENUM_FAMILIES = {
    "PFGameSaveFiles", "PFGameSaveFilesUi", "PFLobby", "PFMatchmaking", "PFMultiplayer"}

# Families whose services are hand-written even though their models are generated.
NON_SERVICE_FAMILIES = {
    "PFCore", "PFEntity", "PFLocalUser", "PFServices", "PFGameSaveFiles", "PFGameSave",
    "PFLobby", "PFMatchmaking", "PFMultiplayer", "Party", "PartyXboxLive", "PFEventPipeline",
    "PFPlatform", "PFTrace", "PFHttpConfig", "PFServiceConfig",
}

# The `GetResult` out-parameter combinations that carry more than one value, mapped onto the
# hand-written result types in PlayFab/PlayFabLoginResults.cs.
COMPOSITE_RESULTS = {
    ("entity", "buffer"): "PlayFabLoginResult",
    ("tokenResponse", "buffer"): "PlayFabServerLoginResult",
    ("entity", "bool"): "PlayFabGameServerLoginResult",
}


class Unsupported(Exception):
    """Raised for a field or function shape the emitter does not know how to project."""


# --------------------------------------------------------------------------------------------
# Field classification
# --------------------------------------------------------------------------------------------


class Value:
    """One managed value: its C# type plus the code that reads and writes it."""

    def __init__(self, type_name: str, read: str, write: list[str]):
        self.type = type_name
        self.read = read
        self.write = write


def _is_json(model: pfmodel.Model, type_name: str) -> bool:
    return type_name == JSON_STRUCT


def _model_name(model: pfmodel.Model, type_name: str) -> str:
    return managed_name(type_name)


def read_value(
    model: pfmodel.Model,
    type_name: str,
    pointer: int,
    native: str,
    element: bool,
) -> tuple[str, str]:
    """Returns `(managed type, expression)` reading one native value."""
    if type_name == "char" and pointer == 1:
        return ("string", f"Utf8.ToString({native}) ?? string.Empty") if element \
            else ("string?", f"Utf8.ToString({native})")

    if _is_json(model, type_name):
        if pointer == 0:
            return "string?", f"Utf8.ToString({native}.StringValue)"
        if pointer == 1:
            return "string?", f"{native} is null ? null : Utf8.ToString({native}->StringValue)"

    if model.is_struct(type_name):
        name = _model_name(model, type_name)
        if pointer == 0:
            return name, f"{name}.FromNative(&{native})"
        if pointer == 1:
            if element:
                return name, f"{name}.FromNative({native})"
            return f"{name}?", f"{native} is null ? null : {name}.FromNative({native})"

    if model.is_enum(type_name):
        name = _model_name(model, type_name)
        if pointer == 0:
            return name, f"({name}){native}"
        if pointer == 1 and not element:
            return f"{name}?", f"{native} is null ? null : ({name}?)(*{native})"

    if type_name == "bool":
        if pointer == 0:
            return "bool", f"{native} != 0"
        if pointer == 1 and not element:
            return "bool?", f"{native} is null ? null : (bool?)(*{native} != 0)"

    if type_name == "time_t":
        if pointer == 0:
            return "DateTimeOffset", f"PlayFabTime.ToDateTimeOffset({native})"
        if pointer == 1 and not element:
            return ("DateTimeOffset?",
                    f"{native} is null ? null : (DateTimeOffset?)PlayFabTime.ToDateTimeOffset(*{native})")

    if type_name in SCALARS:
        managed = SCALARS[type_name]
        if pointer == 0:
            return managed, native
        if pointer == 1 and not element:
            return f"{managed}?", f"{native} is null ? null : ({managed}?)(*{native})"

    raise Unsupported(f"read {type_name}{'*' * pointer}")


def write_value(
    model: pfmodel.Model,
    type_name: str,
    pointer: int,
    native: str,
    managed: str,
    temp: str,
) -> list[str]:
    """Returns the statements writing one managed value into a native lvalue."""
    if type_name == "char" and pointer == 1:
        return [f"{native} = arena.String({managed});"]

    if _is_json(model, type_name):
        if pointer == 0:
            return [f"{native}.StringValue = arena.String({managed});"]
        if pointer == 1:
            return [f"{native} = arena.Json({managed});"]

    if model.is_struct(type_name):
        if pointer == 0:
            return [f"{managed}.WriteTo(&{native}, arena);"]
        if pointer == 1:
            return [
                f"if ({managed} is not null)",
                "{",
                f"    {type_name}* {temp} = arena.Alloc<{type_name}>(1);",
                f"    {managed}.WriteTo({temp}, arena);",
                f"    {native} = {temp};",
                "}",
            ]

    if model.is_enum(type_name):
        if pointer == 0:
            return [f"{native} = ({type_name}){managed};"]
        if pointer == 1:
            return [f"{native} = arena.Value(({type_name}?){managed});"]

    if type_name == "bool":
        if pointer == 0:
            return [f"{native} = {managed} ? (byte)1 : (byte)0;"]
        if pointer == 1:
            return [f"{native} = arena.Value({managed} is null ? null : "
                    f"({managed}.Value ? (byte?)1 : (byte?)0));"]

    if type_name == "time_t":
        if pointer == 0:
            return [f"{native} = PlayFabTime.ToUnixTime({managed});"]
        if pointer == 1:
            return [f"{native} = arena.Value({managed} is null ? null : "
                    f"(long?)PlayFabTime.ToUnixTime({managed}.Value));"]

    if type_name in SCALARS:
        if pointer == 0:
            return [f"{native} = {managed};"]
        if pointer == 1:
            return [f"{native} = arena.Value({managed});"]

    raise Unsupported(f"write {type_name}{'*' * pointer}")


class ModelField:
    """A projected struct field: property declaration plus read and write statements."""

    def __init__(self, name: str, decl_type: str, default: Optional[str]):
        self.name = name
        self.decl_type = decl_type
        self.default = default
        self.reads: list[str] = []
        self.writes: list[str] = []
        self.summary = ""


def _count_cast(model: pfmodel.Model, entry: dict, size_field: str) -> str:
    for field in entry["fields"]:
        if field["name"] == size_field:
            return model.raw_type(field["type"], field["pointer"])
    return "uint"


def classify(model: pfmodel.Model, entry: dict, field: dict, index: int) -> Optional[ModelField]:
    names = pfmodel.struct_field_names(entry)
    native_name = names[field["name"]]
    prop = native_name
    if prop == managed_name(entry["name"]):
        prop += "Value"
    type_name = field["type"]
    pointer = field["pointer"]
    src = f"source->{native_name}"
    dst = f"target->{native_name}"
    temp = f"native{index}"

    if field["array"]:
        if type_name != "char" or pointer != 0:
            raise Unsupported(f"fixed array {entry['name']}.{field['name']}")
        length = model.constant(field["array"])
        projected = ModelField(prop, "string?", None)
        projected.reads = [f"value.{prop} = Utf8.ToString({src}, {length});"]
        projected.writes = [f"Utf8.CopyFixed({dst}, {length}, {prop});"]
        return projected

    if type_name == "XUserHandle" and pointer == 0:
        projected = ModelField("User", "User?", None)
        projected.writes = [f"{dst} = User is null ? IntPtr.Zero : User.Handle;"]
        projected.summary = "The Xbox user this request is made on behalf of."
        return projected

    if type_name in ("void", "XTaskQueueHandle") or type_name in model.callbacks \
            or type_name in model.handles:
        return None

    size_field = field["sizeField"]
    if size_field is None:
        managed_type, read = read_value(model, type_name, pointer, src, element=False)
        default = " = new();" if model.is_struct(type_name) and pointer == 0 \
            and not _is_json(model, type_name) else None
        projected = ModelField(prop, managed_type, default)
        projected.reads = [f"value.{prop} = {read};"]
        projected.writes = write_value(model, type_name, pointer, dst, prop, temp)
        return projected

    # Sized member: an array, a string array or a dictionary.
    count_name = names[size_field]
    count_type = _count_cast(model, entry, size_field)
    element_pointer = pointer - 1

    if type_name == "char" and element_pointer == 1:
        paired = _paired_property_map(entry, names, field, size_field)
        if paired is not None:
            keys_name, values_name, stem = paired
            if native_name == values_name:
                return None
            return _classify_property_map(
                entry, keys_name, values_name, stem, count_name, count_type, index)

    if type_name.endswith(pfmodel.DICTIONARY_SUFFIX) and element_pointer == 0:
        return _classify_dictionary(
            model, entry, type_name, prop, src, dst, count_name, count_type, index)

    element_type, element_read = read_value(
        model, type_name, element_pointer, f"{src}[i{index}]", element=True)
    projected = ModelField(prop, f"IReadOnlyList<{element_type}>?", None)
    projected.reads = [
        f"if ({src} is not null && source->{count_name} != 0)",
        "{",
        f"    var list{index} = new List<{element_type}>((int)source->{count_name});",
        f"    for (uint i{index} = 0; i{index} < source->{count_name}; i{index}++)",
        "    {",
        f"        list{index}.Add({element_read});",
        "    }",
        "",
        f"    value.{prop} = list{index};",
        "}",
    ]

    projected.writes = [
        f"target->{count_name} = ({count_type})({prop} is null ? 0 : {prop}.Count);",
    ]

    if type_name == "char" and element_pointer == 1:
        projected.writes.append(f"{dst} = arena.StringArray({prop});")
        return projected

    projected.writes += [
        f"if ({prop} is not null && {prop}.Count > 0)",
        "{",
    ]
    if element_pointer == 0:
        projected.writes.append(
            f"    {type_name}* array{index} = arena.Alloc<{type_name}>({prop}.Count);")
        projected.writes.append(f"    for (int i{index} = 0; i{index} < {prop}.Count; i{index}++)")
        projected.writes.append("    {")
        for line in write_value(
                model, type_name, 0, f"array{index}[i{index}]", f"{prop}[i{index}]",
                f"item{index}"):
            projected.writes.append("        " + line)
        projected.writes.append("    }")
    elif element_pointer == 1 and model.is_struct(type_name):
        projected.writes.append(
            f"    {type_name}** array{index} = arena.PointerArray<{type_name}>({prop}.Count);")
        projected.writes.append(f"    for (int i{index} = 0; i{index} < {prop}.Count; i{index}++)")
        projected.writes.append("    {")
        projected.writes.append(
            f"        {type_name}* item{index} = arena.Alloc<{type_name}>(1);")
        projected.writes.append(f"        {prop}[i{index}].WriteTo(item{index}, arena);")
        projected.writes.append(f"        array{index}[i{index}] = item{index};")
        projected.writes.append("    }")
    else:
        raise Unsupported(f"array {entry['name']}.{field['name']}")

    projected.writes.append("")
    projected.writes.append(f"    {dst} = array{index};")
    projected.writes.append("}")
    return projected


def _paired_property_map(
    entry: dict, names: dict, field: dict, size_field: str
) -> Optional[tuple[str, str, str]]:
    """
    Detects the multiplayer headers' parallel `<stem>Keys` / `<stem>Values` string arrays, which
    are one string-to-string map split across two members sharing a count.
    """
    native_name = names[field["name"]]
    for suffix in ("Keys", "Values"):
        if not native_name.endswith(suffix) or len(native_name) == len(suffix):
            continue
        stem = native_name[: -len(suffix)]
        keys_name = stem + "Keys"
        values_name = stem + "Values"
        found = {names[other["name"]] for other in entry["fields"]
                 if other["sizeField"] == size_field and other["type"] == "char"
                 and other["pointer"] == 2 and not other["array"]}
        if keys_name in found and values_name in found:
            return keys_name, values_name, stem
    return None


def _classify_property_map(
    entry: dict,
    keys_name: str,
    values_name: str,
    stem: str,
    count_name: str,
    count_type: str,
    index: int,
) -> ModelField:
    prop = (stem[:-1] + "ies") if stem.endswith("y") else (stem + "s")
    if prop == managed_name(entry["name"]):
        prop += "Value"

    projected = ModelField(prop, "IReadOnlyDictionary<string, string>?", None)
    projected.summary = f"The <c>{keys_name}</c> / <c>{values_name}</c> pairs."
    projected.reads = [
        f"if (source->{keys_name} is not null && source->{values_name} is not null "
        f"&& source->{count_name} != 0)",
        "{",
        f"    var map{index} = new Dictionary<string, string>("
        f"(int)source->{count_name}, StringComparer.Ordinal);",
        f"    for (uint i{index} = 0; i{index} < source->{count_name}; i{index}++)",
        "    {",
        f"        string? key{index} = Utf8.ToString(source->{keys_name}[i{index}]);",
        f"        if (key{index} is not null)",
        "        {",
        f"            map{index}[key{index}] = "
        f"Utf8.ToString(source->{values_name}[i{index}]) ?? string.Empty;",
        "        }",
        "    }",
        "",
        f"    value.{prop} = map{index};",
        "}",
    ]
    projected.writes = [
        f"target->{count_name} = ({count_type})({prop} is null ? 0 : {prop}.Count);",
        f"if ({prop} is not null && {prop}.Count > 0)",
        "{",
        f"    byte** keys{index} = arena.PointerArray<byte>({prop}.Count);",
        f"    byte** values{index} = arena.PointerArray<byte>({prop}.Count);",
        f"    int next{index} = 0;",
        f"    foreach (KeyValuePair<string, string> pair{index} in {prop})",
        "    {",
        f"        keys{index}[next{index}] = arena.String(pair{index}.Key);",
        f"        values{index}[next{index}] = arena.String(pair{index}.Value);",
        f"        next{index}++;",
        "    }",
        "",
        f"    target->{keys_name} = keys{index};",
        f"    target->{values_name} = values{index};",
        "}",
    ]
    return projected


def _classify_dictionary(
    model: pfmodel.Model,
    entry: dict,
    entry_type: str,
    prop: str,
    src: str,
    dst: str,
    count_name: str,
    count_type: str,
    index: int,
) -> ModelField:
    dict_entry = model.structs[entry_type]
    names = pfmodel.struct_field_names(dict_entry)
    value_field = next(f for f in dict_entry["fields"] if f["name"] != "key")
    key_name = names["key"]
    value_name = names[value_field["name"]]

    value_type, value_read = read_value(
        model, value_field["type"], value_field["pointer"],
        f"{src}[i{index}].{value_name}", element=True)

    projected = ModelField(prop, f"IReadOnlyDictionary<string, {value_type}>?", None)
    projected.reads = [
        f"if ({src} is not null && source->{count_name} != 0)",
        "{",
        f"    var map{index} = new Dictionary<string, {value_type}>("
        f"(int)source->{count_name}, StringComparer.Ordinal);",
        f"    for (uint i{index} = 0; i{index} < source->{count_name}; i{index}++)",
        "    {",
        f"        map{index}[Utf8.ToString({src}[i{index}].{key_name}) ?? string.Empty] "
        f"= {value_read};",
        "    }",
        "",
        f"    value.{prop} = map{index};",
        "}",
    ]

    writes = [
        f"target->{count_name} = ({count_type})({prop} is null ? 0 : {prop}.Count);",
        f"if ({prop} is not null && {prop}.Count > 0)",
        "{",
        f"    {entry_type}* entries{index} = arena.Alloc<{entry_type}>({prop}.Count);",
        f"    int next{index} = 0;",
        f"    foreach (KeyValuePair<string, {value_type}> pair{index} in {prop})",
        "    {",
        f"        entries{index}[next{index}].{key_name} = arena.String(pair{index}.Key);",
    ]
    for line in write_value(
            model, value_field["type"], value_field["pointer"],
            f"entries{index}[next{index}].{value_name}", f"pair{index}.Value", f"item{index}"):
        writes.append("        " + line)
    writes += [
        f"        next{index}++;",
        "    }",
        "",
        f"    {dst} = entries{index};",
        "}",
    ]
    projected.writes = writes
    return projected


# --------------------------------------------------------------------------------------------
# Reachability
# --------------------------------------------------------------------------------------------


def service_functions(model: pfmodel.Model) -> dict[str, list[dict]]:
    """Groups the generated `...Async` entry points by family."""
    families: dict[str, list[dict]] = {}
    for name, fn in sorted(model.functions.items()):
        if not name.endswith("Async") or fn["family"] in NON_SERVICE_FAMILIES:
            continue
        families.setdefault(fn["family"], []).append(fn)
    return families


def reachable_structs(model: pfmodel.Model, functions: list[dict]) -> set[str]:
    roots: set[str] = set(EXTRA_ROOTS)
    for fn in functions:
        for param in fn["parameters"]:
            if model.is_struct(param["type"]):
                roots.add(param["type"])
        stem = fn["name"][:-len("Async")]
        result = model.functions.get(stem + "GetResult")
        if result is None:
            continue
        for param in result["parameters"]:
            if model.is_struct(param["type"]):
                roots.add(param["type"])

    seen: set[str] = set()
    queue = list(roots)
    while queue:
        name = queue.pop()
        if name in seen or name == JSON_STRUCT:
            continue
        seen.add(name)
        for field in model.structs[name]["fields"]:
            child = field["type"]
            if not model.is_struct(child):
                continue
            if child.endswith(pfmodel.DICTIONARY_SUFFIX):
                for nested in model.structs[child]["fields"]:
                    if model.is_struct(nested["type"]):
                        queue.append(nested["type"])
                continue
            queue.append(child)
    seen.discard(JSON_STRUCT)
    return seen


# --------------------------------------------------------------------------------------------
# Emission
# --------------------------------------------------------------------------------------------


def emit_enum(model: pfmodel.Model, entry: dict) -> str:
    # Reuses the interop emitter so the public enum's member names and values are, by construction,
    # identical to the native one; that is what makes the plain cast between them correct.
    body = emit_native.emit_enum(model, entry, document=True).replace(
        f"internal enum {entry['name']} :",
        f"public enum {managed_name(entry['name'])} :",
        1)
    return f"/// <summary>Projects <c>{entry['name']}</c>.</summary>\n{body}"


def emit_model(model: pfmodel.Model, entry: dict) -> tuple[str, set[str]]:
    name = managed_name(entry["name"])
    fields: list[ModelField] = []
    enums: set[str] = set()
    for index, field in enumerate(entry["fields"]):
        if any(other["sizeField"] == field["name"] for other in entry["fields"]):
            continue
        projected = classify(model, entry, field, index)
        if projected is None:
            continue
        fields.append(projected)
        if model.is_enum(field["type"]):
            enums.add(field["type"])

    lines = [
        f"/// <summary>Projects <c>{entry['name']}</c>.</summary>",
        f"public sealed class {name}",
        "{",
    ]
    for projected in fields:
        summary = projected.summary or f"<c>{projected.name}</c>."
        lines.append(f"    /// <summary>{summary}</summary>")
        suffix = projected.default if projected.default else ""
        lines.append(f"    public {projected.decl_type} {projected.name} {{ get; set; }}{suffix}")
        lines.append("")

    lines.append(f"    internal static unsafe {name} FromNative({entry['name']}* source)")
    lines.append("    {")
    lines.append(f"        var value = new {name}();")
    for projected in fields:
        for line in projected.reads:
            lines.append("        " + line if line else "")
    lines.append("        return value;")
    lines.append("    }")
    lines.append("")
    lines.append(f"    internal unsafe void WriteTo({entry['name']}* target, PlayFabArena arena)")
    lines.append("    {")
    lines.append("        *target = default;")
    for projected in fields:
        for line in projected.writes:
            lines.append("        " + line if line else "")
    lines.append("    }")
    lines.append("}")
    return "\n".join(lines), enums


def _file(path: str, family: str, usings: list[str], body: list[str]) -> None:
    text = [pfmodel.banner("emit_services.py", family), "", "#nullable enable", ""]
    text += [f"using {u};" for u in usings]
    text += ["", "namespace GDK.Net.PlayFab;", ""]
    text.append("\n\n".join(body))
    text.append("")
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8", newline="\r\n") as handle:
        handle.write("\n".join(text))


MODEL_USINGS = [
    "System",
    "System.Collections.Generic",
    "GDK.Net.Interop",
    "GDK.Net.Users",
]

SERVICE_USINGS = [
    "System",
    "System.Collections.Generic",
    "System.Runtime.InteropServices",
    "System.Threading",
    "System.Threading.Tasks",
    "GDK.Net.Interop",
    "GDK.Net.Users",
]

CONTEXTS = {
    "PFEntityHandle": ("PlayFabEntity", "entity"),
    "PFServiceConfigHandle": ("PlayFabServiceConfig", "serviceConfig"),
}


class Api:
    """One `PFXxxAsync` entry point and the result reader that goes with it."""

    def __init__(self, model: pfmodel.Model, fn: dict):
        self.fn = fn
        self.name = fn["name"]
        self.stem = self.name[: -len("Async")]
        self.result = model.functions.get(self.stem + "GetResult")
        self.size = model.functions.get(self.stem + "GetResultSize")

        parameters = fn["parameters"]
        if not parameters or parameters[-1]["type"] != "XAsyncBlock":
            raise Unsupported(f"{self.name}: no trailing XAsyncBlock")

        context = parameters[0]
        if context["type"] not in CONTEXTS:
            raise Unsupported(f"{self.name}: unsupported context {context['type']}")
        self.context_type, self.context_name = CONTEXTS[context["type"]]

        self.secret_key = False
        self.request: Optional[str] = None
        for param in parameters[1:-1]:
            if param["type"] == "char" and param["pointer"] == 1:
                self.secret_key = True
            elif model.is_struct(param["type"]) and param["pointer"] == 1:
                self.request = param["type"]
            else:
                raise Unsupported(f"{self.name}: unsupported parameter {param['name']}")

        self.extras: list[tuple[str, str]] = []
        self.buffer_type: Optional[str] = None
        self.fixed_type: Optional[str] = None
        if self.result is not None:
            rest = self.result["parameters"][1:]
            split = next(
                (i for i, p in enumerate(rest)
                 if p["name"] == "bufferSize" and p["pointer"] == 0), None)
            extras = rest if split is None else rest[:split]
            for param in extras:
                if param["type"] == "PFEntityHandle" and param["pointer"] == 1:
                    self.extras.append(("entity", "entityHandle"))
                elif param["type"] == "bool" and param["pointer"] == 1:
                    self.extras.append(("bool", "newlyCreated"))
                elif model.is_struct(param["type"]) and param["pointer"] == 2:
                    self.extras.append(("tokenResponse", param["type"]))
                elif model.is_struct(param["type"]) and param["pointer"] == 1 and split is None:
                    self.fixed_type = param["type"]
                else:
                    raise Unsupported(f"{self.stem}GetResult: unsupported out {param['name']}")
            if split is not None:
                quartet = rest[split:]
                if len(quartet) != 4 or quartet[2]["pointer"] != 2:
                    raise Unsupported(f"{self.stem}GetResult: unexpected buffer quartet")
                self.buffer_type = quartet[2]["type"]

    @property
    def kinds(self) -> tuple[str, ...]:
        return tuple(kind for kind, _ in self.extras)

    def return_type(self) -> Optional[str]:
        if self.result is None:
            return None
        if not self.extras:
            if self.buffer_type is not None:
                return managed_name(self.buffer_type)
            if self.fixed_type is not None:
                return managed_name(self.fixed_type)
            raise Unsupported(f"{self.stem}GetResult: no projected output")
        key = (self.kinds[0], "buffer" if self.buffer_type else self.kinds[-1])
        if key in COMPOSITE_RESULTS:
            return COMPOSITE_RESULTS[key]
        if self.kinds == ("entity",) and self.buffer_type is None:
            return "PlayFabEntity"
        raise Unsupported(f"{self.stem}GetResult: unmapped output {self.kinds}")


def _reader(api: Api, indent: str) -> list[str]:
    """Emits the lambda that turns a completed async block into the managed result."""
    result_type = api.return_type()
    if result_type is None:
        return [f"{indent}static block => Native.XAsyncGetStatus((XAsyncBlock*)block, 0),"]

    lines = [f"{indent}static (IntPtr block, out {result_type} value) =>", f"{indent}{{"]
    body = indent + "    "
    lines.append(f"{body}value = null!;")

    declarations = []
    arguments = []
    for kind, detail in api.extras:
        if kind == "entity":
            declarations.append("IntPtr entityHandle;")
            arguments.append("&entityHandle")
        elif kind == "bool":
            declarations.append("byte newlyCreated;")
            arguments.append("&newlyCreated")
        else:
            declarations.append(f"{detail}* tokenResponse;")
            arguments.append("&tokenResponse")

    if api.buffer_type is None:
        if api.fixed_type is not None:
            lines.append(f"{body}{api.fixed_type} native = default;")
            arguments.append("&native")
        for declaration in declarations:
            lines.append(f"{body}{declaration}")
        call = ", ".join(["(XAsyncBlock*)block"] + arguments)
        lines.append(f"{body}int hr = NativePlayFab.{api.stem}GetResult({call});")
        lines.append(f"{body}if (HResult.Failed(hr))")
        lines.append(f"{body}{{")
        lines.append(f"{body}    return hr;")
        lines.append(f"{body}}}")
        lines.append("")
        lines.append(f"{body}value = {_construct(api)};")
        lines.append(f"{body}return HResult.SOk;")
        lines.append(f"{indent}}},")
        return lines

    lines.append(f"{body}nuint size;")
    lines.append(
        f"{body}int hr = NativePlayFab.{api.stem}GetResultSize((XAsyncBlock*)block, &size);")
    lines.append(f"{body}if (HResult.Failed(hr))")
    lines.append(f"{body}{{")
    lines.append(f"{body}    return hr;")
    lines.append(f"{body}}}")
    lines.append("")
    lines.append(f"{body}IntPtr buffer = Marshal.AllocHGlobal(checked((int)size));")
    lines.append(f"{body}try")
    lines.append(f"{body}{{")
    inner = body + "    "
    for declaration in declarations:
        lines.append(f"{inner}{declaration}")
    lines.append(f"{inner}{api.buffer_type}* result;")
    lines.append(f"{inner}nuint used;")
    call = ", ".join(
        ["(XAsyncBlock*)block"] + arguments + ["size", "(void*)buffer", "&result", "&used"])
    lines.append(f"{inner}hr = NativePlayFab.{api.stem}GetResult({call});")
    lines.append(f"{inner}if (HResult.Failed(hr))")
    lines.append(f"{inner}{{")
    lines.append(f"{inner}    return hr;")
    lines.append(f"{inner}}}")
    lines.append("")
    lines.append(f"{inner}value = {_construct(api)};")
    lines.append(f"{inner}return HResult.SOk;")
    lines.append(f"{body}}}")
    lines.append(f"{body}finally")
    lines.append(f"{body}{{")
    lines.append(f"{body}    Marshal.FreeHGlobal(buffer);")
    lines.append(f"{body}}}")
    lines.append(f"{indent}}},")
    return lines


def _construct(api: Api) -> str:
    payload = None
    if api.buffer_type is not None:
        payload = f"{managed_name(api.buffer_type)}.FromNative(result)"
    elif api.fixed_type is not None:
        payload = f"{managed_name(api.fixed_type)}.FromNative(&native)"

    if not api.extras:
        return payload  # type: ignore[return-value]

    kinds = api.kinds
    if kinds == ("entity",) and payload is None:
        return "new PlayFabEntity(entityHandle)"
    if kinds == ("entity",) and payload is not None:
        return f"new PlayFabLoginResult(new PlayFabEntity(entityHandle), {payload})"
    if kinds == ("entity", "bool"):
        return "new PlayFabGameServerLoginResult(new PlayFabEntity(entityHandle), newlyCreated != 0)"
    if kinds == ("tokenResponse",):
        token = managed_name(api.extras[0][1])
        return f"new PlayFabServerLoginResult({token}.FromNative(tokenResponse), {payload})"
    raise Unsupported(f"{api.stem}: unmapped construction {kinds}")


def emit_api(model: pfmodel.Model, family: str, api: Api) -> str:
    method = api.name[len(family):]
    result_type = api.return_type()
    signature = "Task" if result_type is None else f"Task<{result_type}>"

    parameters = [f"{api.context_type} {api.context_name}"]
    if api.secret_key:
        parameters.append("string secretKey")
    if api.request is not None:
        parameters.append(f"{managed_name(api.request)} request")
    parameters.append("CancellationToken cancellationToken = default")

    lines = [
        f"    /// <summary>Calls <c>{api.name}</c>.</summary>",
        f"    public static {signature} {method}(",
    ]
    for index, parameter in enumerate(parameters):
        suffix = "," if index < len(parameters) - 1 else ")"
        lines.append(f"        {parameter}{suffix}")
    lines.append("    {")
    lines.append(f"        if ({api.context_name} is null)")
    lines.append("        {")
    lines.append(f"            throw new ArgumentNullException(nameof({api.context_name}));")
    lines.append("        }")
    lines.append("")
    if api.secret_key:
        lines += [
            "        if (secretKey is null)",
            "        {",
            "            throw new ArgumentNullException(nameof(secretKey));",
            "        }",
            "",
        ]
    if api.request is not None:
        lines += [
            "        if (request is null)",
            "        {",
            "            throw new ArgumentNullException(nameof(request));",
            "        }",
            "",
        ]

    lines.append(f"        IntPtr context = {api.context_name}.Handle;")
    lines.append("        var arena = new PlayFabArena();")
    lines.append("        try")
    lines.append("        {")

    arguments = ["context"]
    if api.secret_key:
        lines.append("            IntPtr secretKeyPointer = (IntPtr)arena.String(secretKey);")
        arguments.append("(byte*)secretKeyPointer")
    if api.request is not None:
        lines.append(
            f"            {api.request}* nativeRequest = arena.Alloc<{api.request}>(1);")
        lines.append("            request.WriteTo(nativeRequest, arena);")
        lines.append("            IntPtr requestPointer = (IntPtr)nativeRequest;")
        arguments.append(f"({api.request}*)requestPointer")
    lines.append("")
    arguments.append("(XAsyncBlock*)block")

    lines.append("            return PlayFabCall.InvokeAsync(")
    lines.append("                arena,")
    lines.append(f"                block => NativePlayFab.{api.name}({', '.join(arguments)}),")
    lines += _reader(api, "                ")
    lines.append("                cancellationToken);")
    lines.append("        }")
    lines.append("        catch")
    lines.append("        {")
    lines.append("            arena.Dispose();")
    lines.append("            throw;")
    lines.append("        }")
    lines.append("    }")
    return "\n".join(lines)


def emit_service(model: pfmodel.Model, family: str, apis: list[Api], type_names: set[str]) -> str:
    name = managed_name(family)
    if name in type_names:
        name += "Service"
    lines = [
        f"/// <summary>The PlayFab {managed_name(family)} service ("
        f"<c>{family}.h</c>).</summary>",
        f"public static unsafe class {name}",
        "{",
    ]
    bodies = [emit_api(model, family, api) for api in apis]
    lines.append("\n\n".join(bodies))
    lines.append("}")
    return "\n".join(lines)


# --------------------------------------------------------------------------------------------
# Entry point
# --------------------------------------------------------------------------------------------


def main() -> int:
    model = pfmodel.load()
    families = service_functions(model)

    apis: dict[str, list[Api]] = {}
    skipped: list[str] = []
    for family, functions in families.items():
        for fn in functions:
            try:
                apis.setdefault(family, []).append(Api(model, fn))
            except Unsupported as error:
                skipped.append(str(error))

    flattened = [api.fn for family_apis in apis.values() for api in family_apis]
    structs = reachable_structs(model, flattened)

    type_names = {managed_name(name) for name in structs}
    enums_used: set[str] = set()
    by_family: dict[str, list[str]] = {}
    for name in sorted(structs):
        entry = model.structs[name]
        try:
            text, enums = emit_model(model, entry)
        except Unsupported as error:
            skipped.append(f"{name}: {error}")
            continue
        by_family.setdefault(entry["family"], []).append(text)
        enums_used |= enums

    for name in sorted(enums_used):
        entry = model.enums[name]
        by_family.setdefault(entry["family"], []).insert(0, emit_enum(model, entry))

    for entry in model.enums.values():
        if entry["family"] in EXTRA_ENUM_FAMILIES and entry["name"] not in enums_used:
            by_family.setdefault(entry["family"], []).insert(0, emit_enum(model, entry))
            enums_used.add(entry["name"])

    for family, bodies in sorted(by_family.items()):
        path = os.path.join(pfmodel.MODELS_DIR, f"{managed_name(family)}Models.cs")
        _file(path, family, MODEL_USINGS, bodies)

    for family, family_apis in sorted(apis.items()):
        path = os.path.join(pfmodel.PLAYFAB_DIR, f"{managed_name(family)}.cs")
        _file(path, family, SERVICE_USINGS, [emit_service(model, family, family_apis, type_names)])

    print(f"emitted {sum(len(v) for v in apis.values())} APIs across {len(apis)} services, "
          f"{len(structs)} models, {len(enums_used)} enums")
    if skipped:
        print(f"skipped {len(skipped)}:")
        for item in sorted(set(skipped)):
            print("  " + item)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
