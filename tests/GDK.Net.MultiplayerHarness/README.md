# GDK.Net.MultiplayerHarness

A multi-process test for the PlayFab multiplayer projection. An orchestrator spawns _N_ participant
processes, each of which signs a distinct player in and drives its own PlayFab SDK instances, and
then walks them through scenarios that only mean anything across a process boundary: a lobby whose
membership and property changes have to reach every other client, and a Party network whose messages
have to actually travel.

Like [`GDK.Net.LiveHarness`](../GDK.Net.LiveHarness), this is manual and local-only: it needs a real
Gaming Runtime, a real PlayFab title and a network, so CI never runs it.

## Why a separate process per player

Lobby membership and Party message delivery are distributed state. Inside one process, "the guest
saw the host's property" can be satisfied by the SDK handing back the object the host just wrote: no
service round trip is proven, and no state-change queue other than the writer's is exercised. With a
process per player, every assertion is made by a different SDK instance with its own queue, its own
sockets and its own authentication, so the only way it can pass is if the data really went to the
service and came back.

It also catches whole classes of bug that a single process cannot. Two Party instances on one machine
fight over the same UDP port; two clients joining a lobby race each other; a stalled SDK shows up as
one participant timing out rather than as a deadlock in the middle of a test. Every one of those was
found here.

## What it proves

| Scenario | What has to be true |
|---|---|
| `lobby.membership-and-properties` | The host creates a lobby, every guest joins it by connection string, **all** processes observe the complete membership, the host posts a property and **all** processes see the new value on their own queue, then everyone leaves. |
| `party.network-messages` | Every process brings Party up and measures region latency, the host creates a network, all processes connect and create endpoints, a broadcast from the host reaches every endpoint, and a reply from a guest reaches the host. |

Verified green with 2, 3, 4 and 5 participants.

## Requirements

- The Microsoft GDK, edition `260404`, and a dev-unlocked machine: the same prerequisites as
  `GDK.Net.LiveHarness`.
- A network connection. Party will not report regions without one, and the lobby service is a
  service.
- A PlayFab title. The default is `10D176`; override it with `--title-id`.
- **A developer secret key for that title**, in one of `PLAYFAB_DEV_SECRET_KEY`,
  `PLAYFAB_SECRET_KEY` or `PLAYFAB_DEVELOPER_SECRET_KEY`.

### Why the secret key is needed

Title `10D176` has client-side account creation turned off, so `LoginWithCustomID` with
`CreateAccount = true` fails with `E_PF_PLAYER_CREATION_DISABLED`. Each participant is therefore
provisioned server-side first: `ServerLoginWithCustomID` with the secret key, which the setting does
not apply to, and only then signs in from the client with `CreateAccount = false`. That is also how
a real title with a backend does it.

Without a key the harness still runs and falls back to asking the client login to create the account,
which works on a permissive title and fails on this one. The banner says which path it took.

> **The participants are throwaway players.** Each run mints fresh custom ids of the form
> `gdknet-mp-<timestamp>-p<N>`, so it never borrows or mutates a real player's account. It does leave
> those players behind in the title.

## Running

```powershell
pwsh eng/run-local.ps1 -Project MultiplayerHarness
```

Or, having published once, run the executable directly:

```powershell
cd artifacts\local\GDK.Net.MultiplayerHarness
Start-Process .\GDK.Net.MultiplayerHarness.exe -ArgumentList '--participants','4' `
    -NoNewWindow -RedirectStandardOutput out.txt -Wait
Get-Content out.txt
```

Redirecting to a file rather than piping is worth the extra line: a PowerShell pipeline buffers the
child's output until it exits, which makes a running harness look like a hung one.

### Options

| Option | Meaning |
|---|---|
| `--participants <n>` | How many processes to spawn. Default 2. |
| `--only <prefix>` | Run only scenarios whose name starts with this: `lobby` or `party`. |
| `--title-id <id>` | PlayFab title. Default `10D176`. |
| `--out <dir>` | Where the JSON report goes. |
| `--verbose` | Echo every command, reply and state change, and heartbeat the pump while a command is outstanding. This is the switch that tells "the service never answered" apart from "the SDK call never returned". |

The orchestrator writes `mp-report.json` next to the LiveHarness report and exits non-zero if any
scenario failed.

## Things learned the hard way

These are properties of the native SDKs, not of this harness, and each one cost a debugging session.

- **An idle `PFMultiplayer` instance stalls.** Initializing the multiplayer library and then leaving
  it unused while the rest of the run gets going makes its first lobby operation hang, no failure
  code, no state change, forever. Initializing it at the moment there is work for it is reliable.
  The participant therefore creates it lazily, in `Multiplayer()`, rather than at login.
- **Public lobbies are unreliable to create.** `LobbyAccessPolicy.Public` additionally publishes the
  lobby to the title's searchable index, and that made creation intermittently exceed any sane
  frame-loop budget. Guests are handed the connection string directly, which works for a private
  lobby, so the harness creates private ones.
- **`PFMultiplayerUninitialize` hangs if the library was initialized too early.** The Game Core
  runtime and networking stack have to be up before `PFMultiplayerInitialize`; the header documents
  this but does not enforce it, and initializing early still returns `S_OK`. The library's PubSub
  connection is then never viable, and shutdown parks forever in
  `PubSubSubscriptionManager::Shutdown`, a poll loop with no timeout and no failure path.
  `GDK.Net` guards this in `PlayFabMultiplayer.Initialize`. Party has nothing to do with it; the
  two libraries can be initialized and shut down in any order.
- **Party binds a fixed UDP port**, so the second participant on the machine fails to bind. Setting
  `PartyLocalUdpSocketBindAddressOptions.ExcludeGameCorePreferredUdpMultiplayerPort` with port 0
  before `PartyManager.Initialize` gets a dynamically assigned one. Port 0 on its own is **not**
  enough: on Game Core that means "the Game Core preferred port", which is the contended one.
- **`PartyGetRegions` is asynchronous**, and `CreateNewNetwork`'s synchronously returned descriptor
  is a placeholder that cannot be serialized. The usable join token comes from the completion state
  change.
- **Signing several players in at once trips the title's request rate limit.** The logins are
  staggered and the transient PlayFab errors are retried with jittered backoff.

## Layout

| File | Role |
|---|---|
| `Program.cs` | Decides whether this process is the orchestrator or a participant. |
| `Orchestrator.cs` | Spawns the participants, logs them in, runs the scenarios, writes the report. |
| `ParticipantHandle.cs` | The orchestrator's side of one child process: command/reply correlation over stdio. |
| `Participant.cs` | The child process: a 60Hz frame loop that drains commands, pumps both SDKs and completes commands from state changes. |
| `Protocol.cs` | The line-delimited JSON messages and the verb names. |
| `Scenarios/` | The scenarios, written as readable step sequences against `ParticipantHandle`. |
