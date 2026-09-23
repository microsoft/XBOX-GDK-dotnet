using System;
using System.IO;

namespace GDK.Net.UserSample;

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
        Path = System.IO.Path.Combine(directory, "sample.log");
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
        "GDK.Net.UserSample",
        "sample.log");
}

/// <summary>Command line for the sample.</summary>
internal sealed class SampleOptions
{
    private SampleOptions(string outputDirectory, bool allowUI, bool signOut)
    {
        OutputDirectory = outputDirectory;
        AllowUI = allowUI;
        SignOut = signOut;
    }

    /// <summary>Where the log and the downloaded gamer picture are written.</summary>
    public string OutputDirectory { get; }

    /// <summary>Permit the account-picker UI when no default user is signed in.</summary>
    public bool AllowUI { get; }

    /// <summary>Also sign the user out at the end. Destructive, so it is opt-in.</summary>
    public bool SignOut { get; }

    public static SampleOptions Parse(string[] args)
    {
        string? outputDirectory = null;
        bool allowUI = false;
        bool signOut = false;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--out" when i + 1 < args.Length:
                    outputDirectory = args[++i];
                    break;
                case "--allow-ui":
                    allowUI = true;
                    break;
                case "--sign-out":
                    signOut = true;
                    break;
            }
        }

        outputDirectory ??= Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GDK.Net.UserSample");

        Log.UseDirectory(outputDirectory);
        return new SampleOptions(outputDirectory, allowUI, signOut);
    }
}
