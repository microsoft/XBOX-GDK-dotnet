"""Emits the public Party / PartyXboxLive enums.

Party's C surface spells everything in SCREAMING_SNAKE; the enum members are already folded to
Pascal case by ``emit_native``, so the public enum is a rename of the interop one and the plain
cast between the two is correct by construction.

The rest of the Party projection (handle objects, state changes, manager) is hand-written under
``src/GDK.Net/PlayFab/Party`` because Party is an object model, not a request/response service.
"""

from __future__ import annotations

import os

import emit_native
import pfmodel
from pfmodel import managed_name

PARTY_DIR = os.path.join(pfmodel.PLAYFAB_DIR, "Party")

HEADERS = {
    "party/Party_c.h": "Party",
    "party/PartyXboxLive_c.h": "PartyXboxLive",
}

# Purely internal plumbing that never reaches a caller.
SKIP = {
    "PARTY_THREAD_ID",
    "PARTY_WORK_MODE",
    "PARTY_OPTION",
}

USINGS = """using System;

namespace GDK.Net.PlayFab.Party;
"""


def emit_enum(model: pfmodel.Model, entry: dict) -> str:
    body = emit_native.emit_enum(model, entry, document=True).replace(
        f"internal enum {entry['name']} :",
        f"public enum {managed_name(entry['name'])} :",
        1)
    return f"/// <summary>Projects <c>{entry['name']}</c>.</summary>\n{body}"


def main() -> int:
    model = pfmodel.load()
    os.makedirs(PARTY_DIR, exist_ok=True)

    total = 0
    for header, family in HEADERS.items():
        entry = next(h for h in model.headers if h["header"] == header)
        blocks = [
            emit_enum(model, item)
            for item in entry["enums"]
            if item["name"] not in SKIP
        ]
        total += len(blocks)
        text = (
            pfmodel.banner(os.path.basename(__file__), family)
            + "\n#nullable enable\n\n"
            + USINGS
            + "\n"
            + "\n\n".join(blocks)
            + "\n"
        )
        path = os.path.join(PARTY_DIR, f"{family}Enums.cs")
        with open(path, "w", encoding="utf-8", newline="\r\n") as handle:
            handle.write(text)
        print(f"{path}: {len(blocks)} enums")

    print(f"total: {total} enums")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
