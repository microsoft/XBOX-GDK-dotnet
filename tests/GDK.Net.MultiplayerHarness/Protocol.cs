using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GDK.Net.MultiplayerHarness;

/// <summary>
/// The wire format between the orchestrator and its participant processes: one JSON object per
/// line, commands down each participant's stdin and messages back up its stdout.
/// </summary>
/// <remarks>
/// <para>
/// Newline-delimited JSON over the child's own stdio was chosen over sockets or named pipes because
/// it needs no ports, no cleanup and no rendezvous: the handles exist before the child does, a
/// crashed participant closes its pipe rather than leaking a listener, and the whole conversation is
/// a text file that can be read after the fact.
/// </para>
/// <para>
/// The one rule that follows from it: a participant must write <em>nothing</em> to stdout that is
/// not a protocol line. Human-readable logging goes to stderr, which the orchestrator forwards.
/// </para>
/// </remarks>
internal static class Protocol
{
    public static string Serialize(Command command) =>
        JsonSerializer.Serialize(command, ProtocolJsonContext.Default.Command);

    public static string Serialize(Message message) =>
        JsonSerializer.Serialize(message, ProtocolJsonContext.Default.Message);

    public static Command? ParseCommand(string line) =>
        JsonSerializer.Deserialize(line, ProtocolJsonContext.Default.Command);

    public static Message? ParseMessage(string line) =>
        JsonSerializer.Deserialize(line, ProtocolJsonContext.Default.Message);
}

/// <summary>An instruction from the orchestrator to one participant.</summary>
/// <param name="Id">Correlates the reply. Unique per participant.</param>
/// <param name="Verb">What to do. See <see cref="Verbs"/>.</param>
/// <param name="Args">Verb-specific arguments.</param>
internal sealed record Command(int Id, string Verb, Dictionary<string, string>? Args = null)
{
    public string Arg(string key) =>
        Args is not null && Args.TryGetValue(key, out string? value)
            ? value
            : throw new InvalidOperationException($"Command '{Verb}' is missing argument '{key}'.");

    public string? OptionalArg(string key) =>
        Args is not null && Args.TryGetValue(key, out string? value) ? value : null;

    public int IntArg(string key, int fallback) =>
        OptionalArg(key) is string text && int.TryParse(text, out int value) ? value : fallback;
}

/// <summary>Anything a participant sends back.</summary>
/// <param name="Kind">
/// <c>ready</c> once the process is alive, <c>reply</c> for a command outcome, <c>log</c> for a
/// human-readable line.
/// </param>
/// <param name="Id">The command this replies to, or 0 for unsolicited messages.</param>
/// <param name="Ok">Whether the command succeeded. Meaningless unless <see cref="Kind"/> is a reply.</param>
/// <param name="Text">A description, or the failure when <see cref="Ok"/> is false.</param>
/// <param name="Data">Verb-specific results the orchestrator forwards to other participants.</param>
internal sealed record Message(
    string Kind,
    int Id = 0,
    bool Ok = true,
    string? Text = null,
    Dictionary<string, string>? Data = null)
{
    public const string ReadyKind = "ready";
    public const string ReplyKind = "reply";
    public const string LogKind = "log";

    public string? Value(string key) =>
        Data is not null && Data.TryGetValue(key, out string? value) ? value : null;
}

/// <summary>
/// Every verb a participant understands. They are deliberately fine-grained, so that a scenario in
/// the orchestrator reads as the sequence of native calls it is testing rather than as one opaque
/// "run the lobby test" instruction: when something fails, the verb that failed names the API.
/// </summary>
internal static class Verbs
{
    // Lifetime.
    public const string Login = "login";
    public const string Shutdown = "shutdown";

    // Lobby.
    public const string LobbyCreate = "lobby-create";
    public const string LobbyJoin = "lobby-join";
    public const string LobbyAwaitMembers = "lobby-await-members";
    public const string LobbyPostUpdate = "lobby-post-update";
    public const string LobbyAwaitProperty = "lobby-await-property";
    public const string LobbyLeave = "lobby-leave";

    // Party network.
    public const string PartyPrepare = "party-prepare";
    public const string PartyCreateNetwork = "party-create-network";
    public const string PartyConnect = "party-connect";
    public const string PartyCreateEndpoint = "party-create-endpoint";
    public const string PartyAwaitEndpoints = "party-await-endpoints";
    public const string PartySend = "party-send";
    public const string PartyAwaitMessage = "party-await-message";
    public const string PartyLeave = "party-leave";
}

/// <summary>
/// Source-generated serialisation for the protocol. Reflection-based serialisation is switched off
/// in the project file, so this context is not an optimisation, without it nothing serialises at
/// all, which is what keeps the harness honest about being AOT-publishable.
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Command))]
[JsonSerializable(typeof(Message))]
internal sealed partial class ProtocolJsonContext : JsonSerializerContext;
