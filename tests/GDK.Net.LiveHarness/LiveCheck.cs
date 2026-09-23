using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDK.Net.Users;
using GDK.Net.XboxLive;

namespace GDK.Net.LiveHarness;

/// <summary>
/// One live check: an id, the prerequisites it needs, and the work it performs.
/// </summary>
/// <param name="Id">
/// Dotted, family-first (<c>xbl.profile.read</c>). The family prefix is what <c>--only</c> filters
/// on, and the id is what appears in the JSON report, so ids are treated as stable: renaming one
/// breaks report diffs across runs.
/// </param>
/// <param name="Requires">
/// Ids of checks that must have <b>passed</b> for this one to be meaningful. A check whose
/// prerequisite failed or was skipped is skipped in turn, naming the prerequisite. This is what
/// keeps a single failure from producing a screen of identical downstream errors.
/// </param>
/// <param name="Run">The check body. Returns the detail string recorded against the step.</param>
/// <param name="Replay">
/// Marks a check that establishes state in the <see cref="CheckContext"/> — a runtime, a user
/// handle, an <c>XblContext</c>. Native state does not survive the process, so when a crash forces
/// a relaunch these must run again even though the carried-over report already records them as
/// passed; otherwise every check after the crash fails for want of a handle. Their second result
/// overwrites the first, which is right: a setup step that stopped working is worth knowing about.
/// </param>
internal sealed record LiveCheck(
    string Id,
    IReadOnlyList<string> Requires,
    Func<CheckContext, Task<string>> Run,
    bool Replay = false)
{
    public static LiveCheck Sync(string id, Func<CheckContext, string> run, params string[] requires) =>
        new(id, requires, ctx => Task.FromResult(run(ctx)));

    public static LiveCheck Async(string id, Func<CheckContext, Task<string>> run, params string[] requires) =>
        new(id, requires, run);

    /// <summary>Marks this check as one that must run again after a crash-forced relaunch.</summary>
    public LiveCheck Replayable() => this with { Replay = true };

    /// <summary>The portion before the first dot, used by <c>--only</c>.</summary>
    public string Family
    {
        get
        {
            int dot = Id.IndexOf('.');
            return dot < 0 ? Id : Id[..dot];
        }
    }
}

/// <summary>
/// State threaded through a run: the report, the options, and the handles earlier checks produced.
/// </summary>
/// <remarks>
/// <para>
/// The handles are mutable and nullable because they are produced by checks rather than by the
/// runner. <see cref="Runtime"/> is set by <c>runtime.initialize</c>, <see cref="User"/> by
/// <c>users.add</c>, and so on. Anything that needs one declares the producing check in its
/// <see cref="LiveCheck.Requires"/> list, which is what makes the non-null assertion in
/// <see cref="RequireUser"/> safe: the runner will not call a check whose prerequisites did not pass.
/// </para>
/// <para>
/// This deliberately holds live native handles for the duration of the run rather than re-acquiring
/// them per check. Re-adding a user or rebuilding an <c>XblContext</c> for every check would be
/// slower, noisier in the report, and would stop the harness from exercising the thing most likely
/// to be wrong: whether a handle stays valid across a long sequence of unrelated calls.
/// </para>
/// </remarks>
internal sealed class CheckContext
{
    public CheckContext(Report report, HarnessOptions options)
    {
        Report = report;
        Options = options;
        Scid = options.Scid;
    }

    public Report Report { get; }

    public HarnessOptions Options { get; }

    /// <summary>
    /// The title's Service Configuration ID. Seeded from <c>--scid</c> when one was passed, and
    /// otherwise derived from the title id by <c>runtime.title-id</c>.
    /// </summary>
    public string? Scid { get; set; }

    public GameRuntime? Runtime { get; set; }

    public User? User { get; set; }

    public XboxLiveContext? XboxLiveContext { get; set; }

    /// <summary>Scratch space for checks that hand a value to a later check in the same family.</summary>
    public Dictionary<string, object> State { get; } = new(StringComparer.Ordinal);

    public GameRuntime RequireRuntime =>
        Runtime ?? throw new InvalidOperationException("runtime.initialize did not run.");

    public User RequireUser =>
        User ?? throw new InvalidOperationException("users.add did not run.");

    public XboxLiveContext RequireXboxLiveContext =>
        XboxLiveContext ?? throw new InvalidOperationException("xbl.context did not run.");

    public string RequireScid =>
        Scid ?? throw new InvalidOperationException("runtime.title-id did not run.");

    public T Get<T>(string key) => (T)State[key];

    public bool TryGet<T>(string key, out T value)
    {
        if (State.TryGetValue(key, out object? boxed) && boxed is T typed)
        {
            value = typed;
            return true;
        }

        value = default!;
        return false;
    }
}
