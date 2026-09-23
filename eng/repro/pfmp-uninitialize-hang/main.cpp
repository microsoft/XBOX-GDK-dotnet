// Minimal native repro for PlayFab/GDK shutdown defects.
//
// Runs an arbitrary sequence of lifecycle steps so the trigger can be bisected:
//
//   pfmp-repro --steps gr-init,net-wait,pfmp-init,party-init,pfmp-uninit,party-cleanup,gr-uninit
//
// Every step is timed and watched, so a step that never returns is reported as a hang
// rather than just stalling the run. Exit code 2 means a step hung.
//
// No PlayFab account, authentication, network traffic or lobby is required.

#include <windows.h>

#include <XGameRuntime.h>
#include <XNetworking.h>
#include <playfab/multiplayer/PFMultiplayer.h>
#include <playfab/party/Party_c.h>

#include <cstdio>
#include <cstdarg>
#include <cstring>
#include <cstdlib>

namespace
{
    const char* g_titleId = "10D176";
    unsigned g_timeoutSeconds = 15;

    ULONGLONG g_start = 0;
    CRITICAL_SECTION g_lock;
    char g_currentStep[64] = {};
    ULONGLONG g_stepStart = 0;
    volatile LONG g_hangReported = 0;

    PFMultiplayerHandle g_pfmp = nullptr;
    PARTY_HANDLE g_party = nullptr;

    void Log(const char* fmt, ...)
    {
        double t = (GetTickCount64() - g_start) / 1000.0;
        char line[1024];
        va_list args;
        va_start(args, fmt);
        vsnprintf(line, sizeof(line), fmt, args);
        va_end(args);
        printf("[%7.3fs] %s\n", t, line);
        fflush(stdout);
    }

    void BeginStep(const char* name)
    {
        EnterCriticalSection(&g_lock);
        strncpy_s(g_currentStep, name, _TRUNCATE);
        g_stepStart = GetTickCount64();
        LeaveCriticalSection(&g_lock);
    }

    void EndStep()
    {
        EnterCriticalSection(&g_lock);
        g_currentStep[0] = '\0';
        LeaveCriticalSection(&g_lock);
    }

    // A step that outlives the timeout is a hang. Reported from a separate thread,
    // because the hung call never comes back to check for itself.
    DWORD WINAPI Watchdog(LPVOID)
    {
        for (;;)
        {
            Sleep(250);

            char step[64] = {};
            ULONGLONG elapsed = 0;

            EnterCriticalSection(&g_lock);
            if (g_currentStep[0])
            {
                strncpy_s(step, g_currentStep, _TRUNCATE);
                elapsed = GetTickCount64() - g_stepStart;
            }
            LeaveCriticalSection(&g_lock);

            if (step[0] && elapsed > g_timeoutSeconds * 1000ULL)
            {
                if (InterlockedExchange(&g_hangReported, 1) == 0)
                {
                    Log("*** HANG: step '%s' has not returned after %us.", step, g_timeoutSeconds);
                    Log("*** pid %lu still alive -- attach a debugger now.", GetCurrentProcessId());
                    Sleep(g_timeoutSeconds * 1000ULL);
                    Log("*** terminating with exit code 2.");
                    fflush(stdout);
                    TerminateProcess(GetCurrentProcess(), 2);
                }
            }
        }
    }

    bool RunStep(const char* step)
    {
        if (!strcmp(step, "gr-init"))
        {
            BeginStep(step);
            HRESULT hr = XGameRuntimeInitialize();
            EndStep();
            Log("XGameRuntimeInitialize -> 0x%08X", hr);
            return true;
        }

        if (!strcmp(step, "gr-uninit"))
        {
            BeginStep(step);
            XGameRuntimeUninitialize();
            EndStep();
            Log("XGameRuntimeUninitialize -> (void)");
            return true;
        }

        if (!strcmp(step, "net-wait"))
        {
            BeginStep(step);
            for (int i = 0; i < 100; ++i)
            {
                XNetworkingConnectivityHint hint = {};
                if (SUCCEEDED(XNetworkingGetConnectivityHint(&hint)) && hint.networkInitialized)
                {
                    EndStep();
                    Log("net-wait -> ready after %dms (connectivityLevel=%d)",
                        i * 100, (int)hint.connectivityLevel);
                    return true;
                }
                Sleep(100);
            }
            EndStep();
            Log("net-wait -> WARNING: not initialized after 10s");
            return true;
        }

        if (!strcmp(step, "pfmp-init"))
        {
            MultiplayerInitializationConfiguration config = {};
            config.titleId = g_titleId;
            BeginStep(step);
            HRESULT hr = PFMultiplayerInitialize(&config, &g_pfmp);
            EndStep();
            Log("PFMultiplayerInitialize -> 0x%08X", hr);
            return SUCCEEDED(hr);
        }

        if (!strcmp(step, "pfmp-uninit"))
        {
            BeginStep(step);
            HRESULT hr = PFMultiplayerUninitialize(g_pfmp);
            EndStep();
            g_pfmp = nullptr;
            Log("PFMultiplayerUninitialize -> 0x%08X", hr);
            return true;
        }

        if (!strcmp(step, "party-init"))
        {
            PARTY_INITIALIZATION_CONFIGURATION config = {};
            config.titleId = g_titleId;
            BeginStep(step);
            PartyError err = PartyInitialize(&config, &g_party);
            EndStep();
            Log("PartyInitialize -> %u", err);
            return err == 0;
        }

        if (!strcmp(step, "party-cleanup"))
        {
            BeginStep(step);
            PartyError err = PartyCleanup(g_party);
            EndStep();
            g_party = nullptr;
            Log("PartyCleanup -> %u", err);
            return true;
        }

        if (!strcmp(step, "probe"))
        {
            // Is the Game Core runtime's state observable before XGameRuntimeInitialize? This
            // decides whether a wrapper can enforce initialization order or only assume it.
            bool async = XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature::XAsync);
            bool queue = XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature::XTaskQueue);
            XNetworkingConnectivityHint hint = {};
            HRESULT hr = XNetworkingGetConnectivityHint(&hint);
            Log("probe -> feature(Async)=%d feature(TaskQueue)=%d "
                "XNetworkingGetConnectivityHint=0x%08X networkInitialized=%d",
                async ? 1 : 0, queue ? 1 : 0, hr, hr == S_OK ? (int)hint.networkInitialized : -1);
            return true;
        }

        if (!strncmp(step, "sleep:", 6))
        {
            unsigned ms = (unsigned)atoi(step + 6);
            Log("sleep %ums ...", ms);
            Sleep(ms);
            return true;
        }

        Log("unknown step '%s'", step);
        return false;
    }
}

int main(int argc, char** argv)
{
    g_start = GetTickCount64();
    InitializeCriticalSection(&g_lock);
    setvbuf(stdout, nullptr, _IONBF, 0);

    const char* steps = nullptr;
    for (int i = 1; i < argc; ++i)
    {
        if (!strcmp(argv[i], "--steps") && i + 1 < argc) { steps = argv[++i]; }
        else if (!strcmp(argv[i], "--title") && i + 1 < argc) { g_titleId = argv[++i]; }
        else if (!strcmp(argv[i], "--timeout") && i + 1 < argc) { g_timeoutSeconds = atoi(argv[++i]); }
    }

    if (!steps)
    {
        printf("Usage: pfmp-repro --steps <a,b,c> [--title <id>] [--timeout <sec>]\n"
               "Steps: gr-init gr-uninit net-wait pfmp-init pfmp-uninit party-init "
               "party-cleanup probe sleep:<ms>\n");
        return 1;
    }

    Log("pid %lu, steps '%s'", GetCurrentProcessId(), steps);
    CreateThread(nullptr, 0, Watchdog, nullptr, 0, nullptr);

    char buffer[512];
    strncpy_s(buffer, steps, _TRUNCATE);

    char* context = nullptr;
    for (char* tok = strtok_s(buffer, ",", &context); tok; tok = strtok_s(nullptr, ",", &context))
    {
        if (!RunStep(tok))
        {
            Log("step '%s' failed; stopping.", tok);
            return 1;
        }
    }

    Log("all steps completed; returning from main.");
    return 0;
}
