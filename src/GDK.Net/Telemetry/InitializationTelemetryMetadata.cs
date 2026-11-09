using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml;
using GDK.Net.Interop;

namespace GDK.Net.Telemetry;

internal static class InitializationTelemetryMetadata
{
    internal const int MaximumConfigBytes = 1024 * 1024;
    private static int _reportedFailure;

    internal static unsafe InitializationTelemetryPayload Collect(
        GameRuntimeGameConfigSource source, string? config, ulong sequence)
    {
        var payload = new InitializationTelemetryPayload
        {
            InitializationSequence = sequence,
            ComponentVersion = typeof(GameRuntime).Assembly.GetName().Version?.ToString() ?? "",
        };
        string runtime = RuntimeInformation.FrameworkDescription;
        int separator = runtime.LastIndexOf(' ');
        if (separator > 0)
        {
            payload.ExecutionRuntimeName = InitializationTelemetryPayload.OptionalText(runtime.Substring(0, separator), 128);
            payload.ExecutionRuntimeVersion = InitializationTelemetryPayload.OptionalText(runtime.Substring(separator + 1), 128);
        }

        try
        {
            XVersion version = Native.XSystemGetRuntimeInfo().RuntimeVersion;
            if (version.Value != 0)
            {
                payload.GamingRuntimeVersion = string.Format(CultureInfo.InvariantCulture,
                    "{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }

            payload.DeviceType = (uint)Native.XSystemGetDeviceType();
            uint titleId;
            if (HResult.Succeeded(Native.XGameGetXboxTitleId(&titleId)))
            {
                payload.XboxTitleId = titleId.ToString("X8", CultureInfo.InvariantCulture);
            }
        }
        catch (Exception exception) when (exception is DllNotFoundException or EntryPointNotFoundException)
        {
            ReportFailure();
        }

        (payload.ConfiguredTitlePackageFamilyName, payload.PackagePublisherId) = ReadConfiguredIdentity(source, config);
        return payload;
    }

    internal static (string Family, string PublisherId) ReadConfiguredIdentity(
        GameRuntimeGameConfigSource source, string? config)
    {
        try
        {
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null,
                MaxCharactersInDocument = MaximumConfigBytes,
            };
            if (source == GameRuntimeGameConfigSource.Inline)
            {
                if (config is null || config.Length > MaximumConfigBytes)
                {
                    ReportFailure();
                    return ("", "");
                }

                using var text = new StringReader(config);
                using var xml = XmlReader.Create(text, settings);
                return ReadIdentity(xml);
            }

            string path = source == GameRuntimeGameConfigSource.File
                ? config ?? ""
                : Path.Combine(AppContext.BaseDirectory, "MicrosoftGame.config");
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            long length = file.Length;
            if (length > MaximumConfigBytes)
            {
                ReportFailure();
                return ("", "");
            }

            byte[] bytes = new byte[(int)length];
            int count = 0;
            while (count < bytes.Length)
            {
                int read = file.Read(bytes, count, bytes.Length - count);
                if (read == 0)
                {
                    break;
                }

                count += read;
            }

            using var stream = new MemoryStream(bytes, 0, count, writable: false);
            using var reader = XmlReader.Create(stream, settings);
            return ReadIdentity(reader);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException
            or SecurityException or XmlException or ArgumentException or NotSupportedException
            or CryptographicException)
        {
            ReportFailure();
            return ("", "");
        }
    }

    private static (string Family, string PublisherId) ReadIdentity(XmlReader xml)
    {
        while (xml.Read())
        {
            if (xml.NodeType == XmlNodeType.Element && xml.LocalName == "Identity")
            {
                return DeriveIdentity(xml.GetAttribute("Name"), xml.GetAttribute("Publisher"));
            }
        }

        return ("", "");
    }

    internal static (string Family, string PublisherId) DeriveIdentity(string? name, string? publisher)
    {
        if (name is null || name.Length < 3 || name.Length > 50
            || !InitializationTelemetryPayload.ValidText(publisher, 8192, allowEmpty: false))
        {
            return ("", "");
        }

        foreach (char character in name)
        {
            if (!(character is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9' or '.' or '-'))
            {
                return ("", "");
            }
        }

        using SHA256 sha = SHA256.Create();
        byte[] digest = sha.ComputeHash(Encoding.Unicode.GetBytes(publisher!));
        const string alphabet = "0123456789abcdefghjkmnpqrstvwxyz";
        var encoded = new char[13];
        for (int character = 0; character < encoded.Length; character++)
        {
            int value = 0;
            for (int bit = 0; bit < 5; bit++)
            {
                int position = character * 5 + bit;
                value = (value << 1) | (position < 64 ? (digest[position / 8] >> (7 - position % 8)) & 1 : 0);
            }

            encoded[character] = alphabet[value];
        }

        string publisherId = new(encoded);
        return (name + "_" + publisherId, publisherId);
    }

    private static void ReportFailure()
    {
        if (Interlocked.Exchange(ref _reportedFailure, 1) == 0)
        {
            Trace.TraceWarning("GDK.Net initialization telemetry could not read optional local metadata.");
        }
    }
}
