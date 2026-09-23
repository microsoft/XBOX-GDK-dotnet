"""Emit the raw PlayFab interop layer: `NativeTypes.PF*.cs` and `Native.PF*.cs`."""

from __future__ import annotations

import os

import pfmodel
from pfmodel import camel, escape, pascal


def _enum_member_name(enum_name: str, member: str, style: str) -> str:
    if style == "c":
        # `PARTY_STATE_CHANGE_TYPE_REGIONS_CHANGED` on `PARTY_STATE_CHANGE_TYPE`.
        prefix = enum_name + "_"
        if member.startswith(prefix):
            member = member[len(prefix):]
        return pascal(member) or "None"
    return pascal(member)


def emit_enum(model: pfmodel.Model, entry: dict, document: bool = False) -> str:
    """Emit the enum declaration.

    ``document`` adds a ``<summary>`` to every member naming the C constant it projects. The
    interop enums are ``internal`` and need no XML docs, but the public Party and service enums
    reuse this emitter and would otherwise trip CS1591 on every member.
    """
    base = pfmodel.BUILTINS.get(entry["base"].strip(), "uint")
    lines: list[str] = []
    if entry["flags"]:
        lines.append("[Flags]")
    lines.append(f"internal enum {entry['name']} : {base}")
    lines.append("{")

    used: set[str] = set()
    auto = 0
    for member in entry["members"]:
        name = _enum_member_name(entry["name"], member["name"], entry["style"])
        while name in used:
            name += "_"
        used.add(name)

        if document:
            # C-style members are already fully qualified constants
            # (`PARTY_STATE_CHANGE_TYPE_REGIONS_CHANGED`); `enum class` members need the enum name
            # to be meaningful, since the projected name is otherwise identical to the C one.
            spelling = (member["name"] if entry["style"] == "c"
                        else f"{entry['name']}::{member['name']}")
            lines.append(f"    /// <summary>Projects <c>{spelling}</c>.</summary>")

        value = member["value"]
        if value is None:
            lines.append(f"    {name} = {auto},")
            auto += 1
        else:
            value = value.strip()
            # Members can reference an earlier member of the same enum.
            for other in entry["members"]:
                other_name = _enum_member_name(entry["name"], other["name"], entry["style"])
                if value == other["name"]:
                    value = other_name
            lines.append(f"    {name} = {value},")
            try:
                auto = int(value, 0) + 1
            except ValueError:
                auto += 1
    lines.append("}")
    return "\n".join(lines)


def emit_struct(model: pfmodel.Model, entry: dict) -> str:
    lines = ["[StructLayout(LayoutKind.Sequential)]", f"internal unsafe struct {entry['name']}", "{"]
    names = pfmodel.struct_field_names(entry)
    for field in entry["fields"]:
        name = names[field["name"]]
        if field["array"]:
            length = model.constant(field["array"])
            element = model.raw_type(field["type"], 0)
            lines.append(f"    internal fixed {element} {name}[{length}];")
            continue
        lines.append(f"    internal {model.raw_type(field['type'], field['pointer'])} {name};")
    if len(lines) == 3:
        lines.append("    internal byte Reserved;")
    lines.append("}")
    return "\n".join(lines)


def emit_types(model: pfmodel.Model) -> None:
    by_family: dict[str, list[str]] = {}

    for entry in model.enums.values():
        by_family.setdefault(entry["family"], []).append(emit_enum(model, entry))
    for entry in model.structs.values():
        by_family.setdefault(entry["family"], []).append(emit_struct(model, entry))

    for family, blocks in sorted(by_family.items()):
        text = pfmodel.banner("emit_native.py", family)
        text += (
            "\nusing System;\nusing System.Runtime.InteropServices;\n\n"
            "namespace GDK.Net.Interop;\n\n"
        )
        text += "\n\n".join(blocks) + "\n"
        path = os.path.join(pfmodel.INTEROP_DIR, f"NativeTypes.{family}.cs")
        with open(path, "w", encoding="utf-8", newline="\r\n") as handle:
            handle.write(text)


def _signature(model: pfmodel.Model, function: dict) -> tuple[str, str]:
    raw_return = function["returns"].replace("const", "").strip()
    pointer = raw_return.count("*")
    base = raw_return.replace("*", "").strip()
    returns = "void" if (base == "void" and pointer == 0) else model.raw_type(base, pointer)
    parts = []
    used: set[str] = set()
    for parameter in function["parameters"]:
        name = camel(parameter["name"])
        while name in used:
            name += "Value"
        used.add(name)
        parts.append(f"{model.raw_type(parameter['type'], parameter['pointer'])} {escape(name)}")
    return returns, ", ".join(parts)


def emit_functions(model: pfmodel.Model) -> None:
    by_family: dict[str, list[tuple[str, str, str, str]]] = {}

    for name, function in model.functions.items():
        library = model.exports.get(name)
        if library is None:
            # Declared in the header but not exported by the shipped module: nothing to bind.
            continue
        returns, parameters = _signature(model, function)
        by_family.setdefault(function["family"], []).append(
            (name, returns, parameters, pfmodel.LIBRARIES[library])
        )

    for family, entries in sorted(by_family.items()):
        entries.sort()
        text = pfmodel.banner("emit_native.py", family)
        text += (
            "\nusing System;\nusing System.Runtime.InteropServices;\n\n"
            "namespace GDK.Net.Interop;\n\n"
            "#if NET7_0_OR_GREATER\n\n"
            "internal static unsafe partial class NativePlayFab\n{\n"
        )
        for name, returns, parameters, library in entries:
            text += f"    [LibraryImport({library})]\n"
            text += f"    internal static partial {returns} {name}({parameters});\n\n"
        text = text.rstrip("\n") + "\n}\n\n#else\n\n"
        text += "internal static unsafe partial class NativePlayFab\n{\n"
        for name, returns, parameters, library in entries:
            text += (
                f"    [DllImport({library}, CallingConvention = CallingConvention.StdCall, "
                "ExactSpelling = true)]\n"
            )
            text += f"    internal static extern {returns} {name}({parameters});\n\n"
        text = text.rstrip("\n") + "\n}\n\n#endif\n"
        path = os.path.join(pfmodel.INTEROP_DIR, f"Native.{family}.cs")
        with open(path, "w", encoding="utf-8", newline="\r\n") as handle:
            handle.write(text)


def main() -> int:
    model = pfmodel.load()
    os.makedirs(pfmodel.INTEROP_DIR, exist_ok=True)
    emit_types(model)
    emit_functions(model)
    print(f"emitted interop for {len(model.structs)} structs, {len(model.enums)} enums, "
          f"{sum(1 for n in model.functions if n in model.exports)} functions")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
