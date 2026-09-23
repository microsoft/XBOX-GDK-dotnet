using GDK.Net.Networking;

namespace GDK.Net.UserSample.Demos;

/// <summary>
/// Reading the Gaming Runtime's networking configuration.
/// </summary>
internal static class NetworkingDemo
{
    public static void Run(GameRuntime runtime)
    {
        Log.Write("");
        Log.Write("== Networking ==");

        ulong receiveBuffer = runtime.Networking.QueryConfigurationSetting(
            NetworkingConfigurationSetting.MaxTitleTcpQueuedReceiveBufferSize);

        Log.Write($"  max queued TCP receive buffer: {(receiveBuffer == ulong.MaxValue ? "unlimited" : $"{receiveBuffer} bytes")}");

        // SetConfigurationSetting is the matching writer. Not called here, because it mutates
        // networking state for the whole process.

        NetworkingConnectivityHint hint = runtime.Networking.GetConnectivityHint();
        Log.Write($"  connectivity level: {hint.ConnectivityLevel}");
        Log.Write($"  connectivity cost:  {hint.ConnectivityCost}");
        Log.Write($"  network initialized: {hint.NetworkInitialized}, roaming: {hint.Roaming}");
    }
}
