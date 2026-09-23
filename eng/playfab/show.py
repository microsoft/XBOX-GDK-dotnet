"""Developer aid: dump parsed declarations matching a filter."""
import re
import sys

import pfmodel


def sig(fn: dict) -> str:
    params = ", ".join(
        f"{p['type']}{'*' * p['pointer']} {p['name']}" for p in fn["parameters"])
    return f"{fn['returns']} {fn['name']}({params})"


def main() -> int:
    model = pfmodel.load()
    pattern = re.compile(sys.argv[1], re.IGNORECASE)
    kinds = sys.argv[2] if len(sys.argv) > 2 else "fsec"
    if "f" in kinds:
        for fn in model.functions.values():
            if pattern.search(fn["name"]):
                print(f"[fn {fn['family']}] {sig(fn)}")
    if "s" in kinds:
        for entry in model.structs.values():
            if pattern.search(entry["name"]):
                print(f"[struct {entry['family']}] {entry['name']}")
                for f in entry["fields"]:
                    extra = f" [{f['sizeField']}]" if f.get("sizeField") else ""
                    arr = f"[{f['array']}]" if f.get("array") else ""
                    print(f"    {f['type']}{'*' * f['pointer']} {f['name']}{arr}{extra}")
    if "e" in kinds:
        for entry in model.enums.values():
            if pattern.search(entry["name"]):
                print(f"[enum {entry['family']}] {entry['name']}: "
                      + ", ".join(v["name"] for v in entry["values"]))
    if "c" in kinds:
        for name, value in model.constants.items():
            if pattern.search(name):
                print(f"[const] {name} = {value}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
