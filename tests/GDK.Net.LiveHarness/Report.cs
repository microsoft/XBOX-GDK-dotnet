using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Records one pass/fail/skip line per check and serialises them to the JSON report that
/// <c>eng/run-package-tests.ps1</c> reads back.
/// </summary>
/// <remarks>
/// A packaged GDK title has no attached console, so stdout alone is not observable. The report file
/// is the only channel out of the process, which is why every check funnels through here.
/// </remarks>
internal sealed class Report
{
    /// <summary>The outcome written before a check runs, and rewritten once it finishes.</summary>
    /// <remarks>
    /// An access violation in native code cannot be caught in .NET 8 — the runtime tears the process
    /// down without unwinding, and no <c>catch</c> or runtimeconfig switch changes that. So the only
    /// way a crash does not cost every check after it is to leave a marker on disk before making the
    /// call. A step still marked running in a report the process did not finish writing names
    /// exactly the check that killed it.
    /// </remarks>
    private const string RunningOutcome = "running";

    private readonly HarnessOptions _options;
    private readonly List<StepResult> _steps = new();
    private readonly Dictionary<string, string> _prior = new(StringComparer.Ordinal);
    private readonly Stopwatch _clock = Stopwatch.StartNew();

    public Report(HarnessOptions options)
    {
        _options = options;

        if (options.Resume)
        {
            LoadPrior();
        }
    }

    public int Failed => _steps.Count(s => s.Outcome == "failed");

    /// <summary>
    /// Outcomes carried over from a previous, crashed run, keyed by check id. Empty unless
    /// <c>--resume</c> was passed.
    /// </summary>
    public IReadOnlyDictionary<string, string> Prior => _prior;

    /// <summary>
    /// Marks a check as in progress and flushes, so that a native crash inside it leaves evidence.
    /// </summary>
    public void Begin(string name) => Record(name, RunningOutcome, "In progress.", hresult: null);

    /// <summary>Records a check that completed successfully.</summary>
    public void Pass(string name, string detail) => Record(name, "passed", detail, hresult: null);

    public void Skip(string name, string detail) => Record(name, "skipped", detail, hresult: null);

    public void Fail(string name, Exception exception)
    {
        int? hresult = exception is GameRuntimeException gdk ? gdk.HResultCode : null;
        Record(name, "failed", $"{exception.GetType().Name}: {exception.Message}", hresult);
    }

    public void Save() => Write(final: true);

    /// <summary>
    /// Serializes the steps recorded so far.
    /// </summary>
    /// <remarks>
    /// Called after every step, not just at the end. A crash in native code — an access violation
    /// inside the Gaming Runtime, say — takes the process down without unwinding, so a report
    /// written only on the way out is exactly the report you never get when you most need it.
    /// Flushing per step costs nothing at this scale and means the file always names the last step
    /// that completed, which brackets the failure.
    /// </remarks>
    private void Write(bool final)
    {
        ReportPayload payload;
        lock (_steps)
        {
            payload = new ReportPayload(
                Tool: "GDK.Net.LiveHarness",
                GdkEdition: ReadGdkEdition(),
                TimestampUtc: DateTime.UtcNow.ToString("O"),
                ProcessPath: Environment.ProcessPath,
                DurationMs: _clock.ElapsedMilliseconds,
                Passed: _steps.Count(s => s.Outcome == "passed"),
                Failed: Failed,
                Skipped: _steps.Count(s => s.Outcome == "skipped"),
                Completed: final,
                Steps: new List<StepResult>(_steps));
        }

        // ReportJsonContext, not the reflection-based overload: this harness is published with
        // NativeAOT so that it exercises the same configuration an Xbox title uses, and the
        // reflection serializer would fail at run time there.
        File.WriteAllText(_options.ReportPath, JsonSerializer.Serialize(payload, ReportJsonContext.Default.ReportPayload));

        if (final)
        {
            Console.WriteLine($"Report written to {_options.ReportPath}");
        }
    }

    /// <summary>
    /// Reads the <c>GdkEdition</c> assembly metadata the build stamps onto GDK.Net.
    /// </summary>
    /// <remarks>
    /// Assembly-level custom attributes survive trimming and NativeAOT, so this stays reflection
    /// over attributes only — it never reflects over members, which ILC could not resolve.
    /// </remarks>
    private static string? ReadGdkEdition() =>
        typeof(GameRuntime).Assembly
            .GetCustomAttributes(typeof(AssemblyMetadataAttribute), inherit: false)
            .Cast<AssemblyMetadataAttribute>()
            .FirstOrDefault(a => a.Key == "GdkEdition")?.Value;

    private void Record(string name, string outcome, string detail, int? hresult)
    {
        var result = new StepResult(name, outcome, detail, hresult is null ? null : $"0x{hresult.Value:X8}");

        lock (_steps)
        {
            // Replace rather than append: every check first records itself as running, and the real
            // outcome overwrites that marker in place so the report keeps one line per check in
            // execution order.
            int existing = _steps.FindIndex(s => s.Name == name);
            if (existing >= 0)
            {
                _steps[existing] = result;
            }
            else
            {
                _steps.Add(result);
            }
        }

        if (outcome != RunningOutcome)
        {
            Console.WriteLine($"[{outcome,-7}] {name} — {detail}");
        }

        try
        {
            Write(final: false);
        }
        catch (IOException)
        {
            // The incremental flush is a diagnostic aid. Losing one must not fail the run; the
            // final Save has the last word.
        }
    }

    /// <summary>
    /// Reads the report left by a previous run so this one can carry its results forward.
    /// </summary>
    /// <remarks>
    /// A check still marked running is the one that crashed the process. It is converted to a
    /// failure here, which is what makes a relaunch loop terminate: the crashing check is now
    /// accounted for, so the next run skips straight past it instead of dying in the same place.
    /// </remarks>
    private void LoadPrior()
    {
        ReportPayload? payload;
        try
        {
            if (!File.Exists(_options.ReportPath))
            {
                return;
            }

            payload = JsonSerializer.Deserialize(
                File.ReadAllText(_options.ReportPath),
                ReportJsonContext.Default.ReportPayload);
        }
        catch (Exception ex) when (ex is IOException or JsonException)
        {
            // A truncated report is exactly what a crash mid-flush produces. Starting over is
            // wasteful but correct; refusing to start is neither.
            return;
        }

        if (payload is null)
        {
            return;
        }

        foreach (StepResult step in payload.Steps)
        {
            StepResult carried = step.Outcome == RunningOutcome
                ? step with
                {
                    Outcome = "failed",
                    Detail = "The process crashed while this check was running; it was not retried.",
                }
                : step;

            _steps.Add(carried);
            _prior[carried.Name] = carried.Outcome;
        }
    }
}

internal sealed record StepResult(string Name, string Outcome, string Detail, string? HResult);

/// <summary>
/// The shape <c>eng/run-package-tests.ps1</c> reads back. A named type rather than an anonymous
/// one: anonymous types cannot be source-generated, and NativeAOT has no reflection serializer.
/// </summary>
internal sealed record ReportPayload(
    string Tool,
    string? GdkEdition,
    string TimestampUtc,
    string? ProcessPath,
    long DurationMs,
    int Passed,
    int Failed,
    int Skipped,
    bool Completed,
    IReadOnlyList<StepResult> Steps);

/// <summary>
/// Source-generated serialization for the report.
/// </summary>
/// <remarks>
/// <c>JsonSerializer.Serialize(object, JsonSerializerOptions)</c> walks the type graph with
/// reflection, which is unavailable under NativeAOT — the exact configuration an Xbox title ships
/// in. Generating the converters at compile time keeps the report identical (camelCase, indented)
/// while removing the run-time reflection entirely.
/// </remarks>
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ReportPayload))]
internal sealed partial class ReportJsonContext : JsonSerializerContext;
