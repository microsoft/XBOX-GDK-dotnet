using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GDK.Net.MultiplayerHarness;

/// <summary>
/// One participant process, seen from the orchestrator: a child process plus the two pipes that
/// address it.
/// </summary>
internal sealed class ParticipantHandle : IDisposable
{
    private readonly Process _process;
    private readonly HarnessOptions _options;
    private readonly ConcurrentDictionary<int, TaskCompletionSource<Message>> _waiters = new();
    private readonly TaskCompletionSource<bool> _ready =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    private int _nextCommandId;

    private ParticipantHandle(string name, Process process, HarnessOptions options)
    {
        Name = name;
        _process = process;
        _options = options;
    }

    public string Name { get; }

    /// <summary>The participant's PlayFab entity id, learned from the login reply.</summary>
    public string EntityId { get; private set; } = string.Empty;

    /// <summary>
    /// Starts a participant. The child is this same executable with <c>--role participant</c>,
    /// which keeps the two halves of every scenario in one binary and one publish.
    /// </summary>
    public static ParticipantHandle Start(string name, HarnessOptions options)
    {
        string executable = Environment.ProcessPath
            ?? throw new InvalidOperationException("Could not determine this process's own path.");

        var info = new ProcessStartInfo(executable)
        {
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,

            // The child has to run from the publish directory: that is where the PlayFab DLLs and
            // MicrosoftGame.config sit, and the Gaming Runtime reads the config from the working
            // directory.
            WorkingDirectory = Path.GetDirectoryName(executable) ?? Environment.CurrentDirectory,
        };

        foreach (string argument in new[]
        {
            "--role", HarnessOptions.ParticipantRole,
            "--name", name,
            "--title-id", options.TitleId,
            "--out", options.OutputDirectory,
        })
        {
            info.ArgumentList.Add(argument);
        }

        if (options.Verbose)
        {
            info.ArgumentList.Add("--verbose");
        }

        Process process = Process.Start(info)
            ?? throw new InvalidOperationException($"Could not start participant '{name}'.");

        var handle = new ParticipantHandle(name, process, options);
        handle.BeginReading();
        return handle;
    }

    /// <summary>Waits for the participant to report that its process is alive.</summary>
    public Task ReadyAsync(TimeSpan timeout) => WithTimeout(_ready.Task, timeout, $"{Name} to start");

    /// <summary>
    /// Sends a command and waits for its reply. Every scenario step goes through here, so a failed
    /// reply becomes an exception naming the participant and the verb.
    /// </summary>
    public async Task<Message> SendAsync(
        string verb, Dictionary<string, string>? args = null, TimeSpan? timeout = null)
    {
        int id = Interlocked.Increment(ref _nextCommandId);
        var waiter = new TaskCompletionSource<Message>(TaskCreationOptions.RunContinuationsAsynchronously);
        _waiters[id] = waiter;

        string line = Protocol.Serialize(new Command(id, verb, args));
        if (_options.Verbose)
        {
            Console.WriteLine($"    -> {Name}: {line}");
        }

        await _process.StandardInput.WriteLineAsync(line).ConfigureAwait(false);
        await _process.StandardInput.FlushAsync().ConfigureAwait(false);

        Message reply = await WithTimeout(
            waiter.Task, timeout ?? TimeSpan.FromSeconds(60), $"{Name} to answer '{verb}'")
            .ConfigureAwait(false);

        return reply.Ok
            ? reply
            : throw new ScenarioException($"{Name} failed '{verb}': {reply.Text}");
    }

    public async Task StopAsync()
    {
        try
        {
            // Ask first, so the participant can release its PlayFab handles in order.
            await SendAsync(Verbs.Shutdown, timeout: TimeSpan.FromSeconds(15)).ConfigureAwait(false);
            _process.StandardInput.Close();

            if (!_process.WaitForExit(10_000))
            {
                _process.Kill(entireProcessTree: true);
            }
        }
        catch
        {
            try
            {
                _process.Kill(entireProcessTree: true);
            }
            catch
            {
                // Already gone.
            }
        }
    }

    public void Dispose() => _process.Dispose();

    private void BeginReading()
    {
        _process.OutputDataReceived += (_, e) =>
        {
            if (e.Data is null)
            {
                return;
            }

            if (_options.Verbose)
            {
                Console.WriteLine($"    <- {Name}: {e.Data}");
            }

            Message? message;
            try
            {
                message = Protocol.ParseMessage(e.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    [{Name}] unparseable line: {ex.Message}");
                return;
            }

            if (message is null)
            {
                return;
            }

            switch (message.Kind)
            {
                case Message.ReadyKind:
                    _ready.TrySetResult(true);
                    break;

                case Message.ReplyKind:
                    if (message.Value("entityId") is string entityId && entityId.Length != 0)
                    {
                        EntityId = entityId;
                    }

                    if (_waiters.TryRemove(message.Id, out TaskCompletionSource<Message>? waiter))
                    {
                        waiter.TrySetResult(message);
                    }

                    break;

                case Message.LogKind:
                    Console.WriteLine($"    [{Name}] {message.Text}");
                    break;
            }
        };

        // A participant's stderr is where its unhandled failures land, so it is surfaced rather
        // than swallowed -- a silent child that never replies is the worst thing to debug.
        _process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                Console.WriteLine($"    [{Name}:stderr] {e.Data}");
            }
        };

        _process.Exited += (_, _) =>
        {
            var failure = new ScenarioException(
                $"{Name} exited with code {_process.ExitCode} before answering.");

            _ready.TrySetException(failure);
            foreach (KeyValuePair<int, TaskCompletionSource<Message>> pair in _waiters)
            {
                pair.Value.TrySetException(failure);
            }
        };

        _process.EnableRaisingEvents = true;
        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();
    }

    private static async Task<T> WithTimeout<T>(Task<T> task, TimeSpan timeout, string what)
    {
        if (await Task.WhenAny(task, Task.Delay(timeout)).ConfigureAwait(false) != task)
        {
            throw new ScenarioException($"Timed out after {timeout.TotalSeconds:F0}s waiting for {what}.");
        }

        return await task.ConfigureAwait(false);
    }

    private static async Task WithTimeout(Task task, TimeSpan timeout, string what)
    {
        if (await Task.WhenAny(task, Task.Delay(timeout)).ConfigureAwait(false) != task)
        {
            throw new ScenarioException($"Timed out after {timeout.TotalSeconds:F0}s waiting for {what}.");
        }

        await task.ConfigureAwait(false);
    }
}

/// <summary>A scenario step did not do what it was supposed to.</summary>
internal sealed class ScenarioException : Exception
{
    public ScenarioException(string message)
        : base(message)
    {
    }
}
