using GDK.Net;

internal static class Program
{
    private static void Main()
    {
        // Keep a real public API call reachable by ILC without executing native code in the check.
        if (System.Environment.GetEnvironmentVariable("GDK_NET_CONSUMER_LIVE_CALL") == "explicit-opt-in")
        {
            using var runtime = GameRuntime.Initialize();
        }
    }
}
