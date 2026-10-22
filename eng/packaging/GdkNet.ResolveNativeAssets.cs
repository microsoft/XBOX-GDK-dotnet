using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

public sealed class GdkNetResolveNativeAssets : Task
{
    [Required] public string AssetsFile { get; set; }
    [Required] public string SdkToolsPath { get; set; }
    [Required] public string Edition { get; set; }
    [Required] public string WindowsVersion { get; set; }
    [Required] public string CoreVersion { get; set; }
    [Required] public string Architecture { get; set; }
    public string RuntimeIdentifier { get; set; }
    public bool IncludeXboxLive { get; set; }
    public bool IncludePlayFab { get; set; }
    public bool IncludePlayFabMultiplayer { get; set; }
    public bool IncludePlayFabParty { get; set; }
    [Output] public ITaskItem[] NativeAssets { get; set; }
    [Output] public string WindowsRoot { get; set; }
    [Output] public string CoreRoot { get; set; }

    public override bool Execute()
    {
        try
        {
            if (Architecture != "x64" && Architecture != "arm64")
                throw new InvalidOperationException("GdkRedistArchitecture must be x64 or arm64.");
            if (!string.IsNullOrEmpty(RuntimeIdentifier) &&
                RuntimeIdentifier != "win-x64" && RuntimeIdentifier != "win-arm64")
                throw new InvalidOperationException("GDK.Net supports only win-x64 and win-arm64 native runtime identifiers.");
            if (!string.IsNullOrEmpty(RuntimeIdentifier) && RuntimeIdentifier != "win-" + Architecture)
                throw new InvalidOperationException("GdkRedistArchitecture conflicts with RuntimeIdentifier.");
            if (!File.Exists(AssetsFile))
                throw new InvalidOperationException("Restore this project before resolving GDK.Net native assets.");

            // RoslynCodeTaskFactory's portable reference set intentionally predates the SDK's
            // NuGet runtime. Invoke its supported lockfile API without binding our task to a
            // particular System.Runtime/NuGet assembly version at compilation.
            var readerType = Assembly.LoadFrom(Path.Combine(SdkToolsPath, "NuGet.ProjectModel.dll"))
                .GetType("NuGet.ProjectModel.LockFileFormat", true);
            var assets = readerType.GetMethod("Read", new[] { typeof(string) })
                .Invoke(Activator.CreateInstance(readerType), new object[] { AssetsFile });
            if (assets == null)
                throw new InvalidOperationException("Could not read the restored NuGet assets file.");
            WindowsRoot = Path.Combine(FindPackage(assets, "Microsoft.GDK.Windows", WindowsVersion), "native", Edition);
            CoreRoot = Path.Combine(FindPackage(assets, "Microsoft.GDK.Core", CoreVersion), "native");
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "xgameruntime.thunks.dll" };
            if (IncludeXboxLive)
            {
                names.Add("Microsoft.Xbox.Services.C.Thunks.dll");
                names.Add("libHttpClient.dll");
            }
            if (IncludePlayFab || IncludePlayFabMultiplayer || IncludePlayFabParty)
            {
                names.UnionWith(new[] { "PlayFabCore.dll", "PlayFabServices.dll", "PlayFabGameSave.dll", "libHttpClient.dll" });
            }
            if (IncludePlayFabMultiplayer)
                names.Add("PlayFabMultiplayer.dll");
            if (IncludePlayFabParty)
                names.UnionWith(new[] { "Party.dll", "PartyXboxLive.dll" });

            var directory = Path.Combine(WindowsRoot, "windows", "bin", Architecture);
            NativeAssets = names.OrderBy(name => name, StringComparer.Ordinal).Select(name =>
            {
                var path = Path.Combine(directory, name);
                if (!File.Exists(path))
                    throw new InvalidOperationException("Required GDK.Net native asset is missing: " + path +
                        ". Restore the exact SDK packages; installed SDKs are not a fallback.");
                return (ITaskItem)new TaskItem(path);
            }).ToArray();
            return true;
        }
        catch (Exception error)
        {
            Log.LogError("GDKNET001: " + error.Message);
            return false;
        }
    }

    private static object Property(object value, string name)
    {
        return value.GetType().GetProperty(name).GetValue(value, null);
    }

    private static string FindPackage(object assets, string id, string expected)
    {
        var packages = ((IEnumerable)Property(assets, "Libraries")).Cast<object>().Where(library =>
            string.Equals((string)Property(library, "Name"), id, StringComparison.OrdinalIgnoreCase) &&
            string.Equals((string)Property(library, "Type"), "package", StringComparison.OrdinalIgnoreCase)).ToArray();
        if (packages.Length != 1 || !string.Equals(Property(packages[0], "Version").ToString(), expected, StringComparison.Ordinal))
        {
            var actual = string.Join(", ", packages.Select(package => Property(package, "Version").ToString()));
            throw new InvalidOperationException("GDK.Net requires " + id + " [" + expected +
                "] but restore resolved " + (actual.Length == 0 ? "no package" : actual) +
                ". Remove conflicting direct SDK references and restore again.");
        }
        foreach (var folder in ((IEnumerable)Property(assets, "PackageFolders")))
        {
            var path = Path.Combine((string)Property(folder, "Path"),
                ((string)Property(packages[0], "Path")).Replace('/', Path.DirectorySeparatorChar));
            if (Directory.Exists(path))
                return path;
        }
        throw new InvalidOperationException("The restored package directory for " + id + " was not found in packageFolders.");
    }
}
