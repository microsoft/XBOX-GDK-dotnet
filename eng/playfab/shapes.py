"""Developer aid: report the distinct shapes of the PlayFab async/result function triples."""
import collections

import pfmodel


def shape(fn: dict) -> str:
    model = pfmodel.load()
    parts = []
    for p in fn["parameters"]:
        t, ptr = p["type"], p["pointer"]
        if t in ("XAsyncBlock", "XTaskQueueHandle"):
            token = t
        elif t.endswith("Handle"):
            token = t + "*" * ptr
        elif t == "char" and ptr == 1:
            token = "string"
        elif t in ("bool", "size_t", "void", "uint32_t", "int32_t", "time_t"):
            token = t + "*" * ptr
        elif model.is_struct(t):
            token = ("REQ" if p["name"] == "request" else "STRUCT") + "*" * ptr
        else:
            token = t + "*" * ptr
        parts.append(token)
    return "(" + ", ".join(parts) + ")"


def main() -> int:
    model = pfmodel.load()
    async_shapes: collections.Counter = collections.Counter()
    result_shapes: collections.Counter = collections.Counter()
    examples: dict[str, str] = {}
    orphans = []
    for name, fn in sorted(model.functions.items()):
        if not name.endswith("Async") or name in ("PFUninitializeAsync", "PFServicesUninitializeAsync"):
            continue
        stem = name[:-5]
        result = model.functions.get(stem + "GetResult")
        size = model.functions.get(stem + "GetResultSize")
        async_shapes[shape(fn)] += 1
        examples.setdefault(shape(fn), name)
        key = shape(result) if result else "<none>"
        if size and not result:
            orphans.append(name)
        result_shapes[key] += 1
        examples.setdefault(key, name)

    print("=== Async shapes ===")
    for key, count in async_shapes.most_common():
        print(f"{count:5}  {key}   e.g. {examples[key]}")
    print("=== GetResult shapes ===")
    for key, count in result_shapes.most_common():
        print(f"{count:5}  {key}   e.g. {examples[key]}")
    if orphans:
        print("=== size without result ===")
        print("\n".join(orphans))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
