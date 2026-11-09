using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.Telemetry;

namespace GDK.Net.Telemetry;

[EventData]
internal sealed class InitializationTelemetryPayload
{
    public ulong PartA_PrivTags { get; set; } = (ulong)PrivTags.ProductAndServiceUsage;
    public uint SchemaVersion { get; set; } = 1;
    public string ComponentId { get; set; } = "GDK.Net";
    public string ComponentVersion { get; set; } = "";
    public uint ComponentKind { get; set; } = 2;
    public ulong InitializationSequence { get; set; }
    public string EngineName { get; set; } = "";
    public string EngineVersion { get; set; } = "";
    public string ExecutionRuntimeName { get; set; } = "";
    public string ExecutionRuntimeVersion { get; set; } = "";
    public string GamingRuntimeVersion { get; set; } = "";
    public string GdkBuildSdkVersion { get; set; } = "";
    public uint DevelopmentContext { get; set; } = 3;
    public string ConfiguredTitlePackageFamilyName { get; set; } = "";
    public string PackagePublisherId { get; set; } = "";
    public uint DeviceType { get; set; }
    public string XboxTitleId { get; set; } = "";
    public string PlayFabTitleId { get; set; } = "";

    internal bool IsValid() =>
        PartA_PrivTags == (ulong)PrivTags.ProductAndServiceUsage && SchemaVersion == 1
        && ComponentId == "GDK.Net" && ComponentKind == 2 && DevelopmentContext == 3
        && InitializationSequence != 0
        && ValidText(ComponentVersion, 128, allowEmpty: false)
        && ValidText(EngineName, 128) && ValidText(EngineVersion, 128)
        && ValidText(ExecutionRuntimeName, 128) && ValidText(ExecutionRuntimeVersion, 128)
        && ValidText(GamingRuntimeVersion, 128) && ValidText(GdkBuildSdkVersion, 128)
        && ValidText(ConfiguredTitlePackageFamilyName, 64) && ValidText(PackagePublisherId, 13)
        && ValidTitleId(XboxTitleId) && ValidText(PlayFabTitleId, 256);

    internal static bool ValidText(string? value, int limit, bool allowEmpty = true)
    {
        if (value is null || value.Length > limit || (!allowEmpty && value.Length == 0))
        {
            return false;
        }

        for (int i = 0; i < value.Length; i++)
        {
            char character = value[i];
            if (char.IsControl(character))
            {
                return false;
            }

            if (char.IsHighSurrogate(character))
            {
                if (++i == value.Length || !char.IsLowSurrogate(value[i]))
                {
                    return false;
                }
            }
            else if (char.IsLowSurrogate(character))
            {
                return false;
            }
        }

        return true;
    }

    internal static string OptionalText(string? value, int limit) => ValidText(value, limit) ? value! : "";

    private static bool ValidTitleId(string value)
    {
        if (value is null || (value.Length != 0 && value.Length != 8))
        {
            return false;
        }

        foreach (char character in value)
        {
            if (!(character is >= '0' and <= '9' or >= 'A' and <= 'F'))
            {
                return false;
            }
        }

        return true;
    }
}
