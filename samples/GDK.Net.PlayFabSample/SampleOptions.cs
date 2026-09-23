using System;
using System.IO;

namespace GDK.Net.PlayFabSample;

/// <summary>
/// Where the sample writes its output.
/// </summary>
/// <remarks>
/// A packaged GDK title is launched by the shell and has no attached console, so
/// <see cref="Console"/> output is invisible in a real run. Everything is mirrored to a log file so
/// there is something to read afterwards.
/// </remarks>
internal static class Log
{
    private static readonly object Gate = new();

    public static string Path { get; private set; } = DefaultPath();

    public static void UseDirectory(string directory)
    {
        Directory.CreateDirectory(directory);
        Path = System.IO.Path.Combine(directory, "playfab-sample.log");
        File.WriteAllText(Path, string.Empty);
    }

    public static void Write(string message)
    {
        Console.WriteLine(message);

        lock (Gate)
        {
            try
            {
                File.AppendAllText(Path, message + Environment.NewLine);
            }
            catch (IOException)
            {
                // The log is a convenience. Losing a line must not fail the run.
            }
        }
    }

    private static string DefaultPath() => System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "GDK.Net.PlayFabSample",
        "playfab-sample.log");
}

/// <summary>Command line for the sample.</summary>
internal sealed class SampleOptions
{
    /// <summary>
    /// The PlayFab title the sample talks to. This is a *PlayFab* title id and has nothing to do
    /// with the Xbox title id in <c>MicrosoftGame.config</c>: PlayFab keeps its own title registry,
    /// so the projection has to be told which one to use.
    /// </summary>
    public const string DefaultTitleId = "10D176";

    private SampleOptions(string outputDirectory, string titleId, bool allowUI)
    {
        OutputDirectory = outputDirectory;
        TitleId = titleId;
        AllowUI = allowUI;
    }

    /// <summary>Where the log is written.</summary>
    public string OutputDirectory { get; }

    /// <summary>The PlayFab title id.</summary>
    public string TitleId { get; }

    /// <summary>Permit the account-picker UI when no default user is signed in.</summary>
    public bool AllowUI { get; }

    /// <summary>The PlayFab API endpoint derived from the title id.</summary>
    public string ApiEndpoint => $"https://{TitleId}.playfabapi.com";

    /// <summary>
    /// The developer secret key, or <see langword="null"/> when none is set.
    /// </summary>
    /// <remarks>
    /// Deliberately read from the environment only, never from a command-line flag: a title secret
    /// is a *server* credential, and a flag would leave it in shell history and CI logs. A shipping
    /// game never holds one at all — it authenticates as the player through
    /// <see cref="GDK.Net.PlayFab.PlayFabLocalUser"/> instead. The sample uses it only to show the
    /// title-entity path working on a machine with nobody signed in.
    /// </remarks>
    public static string? SecretKey =>
        FirstNonEmpty(
            Environment.GetEnvironmentVariable("GDKNET_PLAYFAB_SECRET_KEY"),
            Environment.GetEnvironmentVariable("PLAYFAB_DEVELOPER_SECRET_KEY"),
            Environment.GetEnvironmentVariable("PLAYFAB_SECRET_KEY"));

    public static SampleOptions Parse(string[] args)
    {
        string? outputDirectory = null;
        string? titleId = null;
        bool allowUI = false;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--out" when i + 1 < args.Length:
                    outputDirectory = args[++i];
                    break;
                case "--title-id" when i + 1 < args.Length:
                    titleId = args[++i];
                    break;
                case "--allow-ui":
                    allowUI = true;
                    break;
            }
        }

        outputDirectory ??= Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GDK.Net.PlayFabSample");

        titleId ??= Environment.GetEnvironmentVariable("GDKNET_PLAYFAB_TITLE_ID") ?? DefaultTitleId;

        Log.UseDirectory(outputDirectory);
        return new SampleOptions(outputDirectory, titleId, allowUI);
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        foreach (string? value in values)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        return null;
    }
}
