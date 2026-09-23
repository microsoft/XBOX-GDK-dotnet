"""Developer aid: survey the distinct struct field shapes in the parsed PlayFab model."""
import collections

import pfmodel


def main() -> int:
    model = pfmodel.load()
    shapes: collections.Counter = collections.Counter()
    examples: dict[str, str] = {}
    for entry in model.structs.values():
        for field in entry["fields"]:
            t = field["type"]
            if model.is_struct(t):
                kind = "DICT" if t.endswith("DictionaryEntry") else "STRUCT"
            elif model.is_enum(t):
                kind = "ENUM"
            elif t in model.handles:
                kind = "HANDLE:" + t
            elif t in model.callbacks:
                kind = "CALLBACK"
            else:
                kind = t
            key = (f"{kind}{'*' * field['pointer']}"
                   f"{'[size]' if field['sizeField'] else ''}"
                   f"{'[fixed]' if field['array'] else ''}")
            shapes[key] += 1
            examples.setdefault(key, f"{entry['name']}.{field['name']}")
    for key, count in shapes.most_common():
        print(f"{count:6}  {key:32}  e.g. {examples[key]}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
