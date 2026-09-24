using System;
using System.Collections.Generic;
using System.Threading;
using GDK.Net.PlayFab;
using GDK.Net.PlayFab.Party;

namespace GDK.Net.PlayFabSample.Demos;

/// <summary>
/// PlayFab Party: initializing the library, measuring region latency, and creating a local user
/// from the authenticated PlayFab entity.
/// </summary>
/// <remarks>
/// <para>
/// Party is the one family that cannot be awaited. It does not use <c>XAsyncBlock</c>: work is
/// started with a call that returns immediately, and completions arrive on a queue the title drains
/// once per frame. The projection keeps that model rather than hiding it behind a Task, because a
/// game's frame loop is where the pump belongs and because a state change can arrive with no
/// operation to match it at all, a remote player leaving, say.
/// </para>
/// <para>
/// So the shape is: a start call returns a <see cref="PartyOperationId"/>, and the matching
/// completion state change carries that id back. What the projection does remove is the raw
/// <c>void* asyncIdentifier</c>, the union of change structs, and the paired
/// <c>StartProcessingStateChanges</c>/<c>FinishProcessingStateChanges</c> bracket: the returned
/// collection releases the batch when enumeration ends.
/// </para>
/// </remarks>
internal static class PartyDemo
{
    public static void Run(string titleId, PlayFabEntity? entity)
    {
        Log.Write("");
        Log.Write("== Party ==");

        // PartyInitialize. Disposing the manager calls PartyCleanup.
        using PartyManager party = PartyManager.Initialize(titleId);
        Log.Write($"  initialized for title {titleId}");
        Log.Write($"  audio work mode: {PartyManager.GetWorkMode(PartyThreadId.Audio)}");

        ShowRegions(party);

        if (entity is null)
        {
            Log.Write("  no authenticated entity; skipping the local user");
            return;
        }

        LocalUserRoundTrip(party, entity);
    }

    /// <summary>
    /// Party measures latency to each Azure region in the background and reports the result as a
    /// state change, so the list stays empty until the pump has run for a moment.
    /// </summary>
    private static void ShowRegions(PartyManager party)
    {
        IReadOnlyList<PartyRegion> regions = [];

        for (int i = 0; i < 100 && regions.Count == 0; i++)
        {
            Pump(party);
            regions = party.GetRegions();

            if (regions.Count == 0)
            {
                Thread.Sleep(100);
            }
        }

        if (regions.Count == 0)
        {
            Log.Write("  no regions: this device has no route to the Party quality-of-service endpoints");
            return;
        }

        // Already ordered by measured latency, so the first is the one to host in.
        Log.Write($"  {regions.Count} region(s), closest first:");
        for (int i = 0; i < Math.Min(5, regions.Count); i++)
        {
            Log.Write($"    {regions[i].RegionName} at {regions[i].RoundTripLatency.TotalMilliseconds:F0}ms");
        }
    }

    /// <summary>
    /// The bridge from PlayFab authentication into Party. A local user is what Party authenticates
    /// a chat or network participant with, so this is where the two libraries meet.
    /// </summary>
    private static void LocalUserRoundTrip(PartyManager party, PlayFabEntity entity)
    {
        PartyLocalUser localUser = party.CreateLocalUser(entity);
        Log.Write($"  created a local user; the manager now tracks {party.LocalUsers.Count}");

        // Destroying is asynchronous, so it returns the id to match the completion against.
        PartyOperationId operation = party.DestroyLocalUser(localUser);

        for (int i = 0; i < 40; i++)
        {
            foreach (PartyStateChange change in party.ProcessStateChanges())
            {
                // State changes are a type hierarchy, so a title matches on the change it cares
                // about instead of switching on a tag and reading the right union arm.
                if (change is PartyDestroyLocalUserCompleted completed && completed.Operation == operation)
                {
                    Log.Write($"  destroyed it; operation {operation} completed with {completed.Result}");
                    return;
                }
            }

            Thread.Sleep(50);
        }

        Log.Write($"  operation {operation} did not complete within two seconds of pumping");
    }

    /// <summary>
    /// What a game calls once per frame. Nothing is expected here, because the sample never joins
    /// a network; a real title would dispatch each change to its networking and chat code.
    /// </summary>
    private static void Pump(PartyManager party)
    {
        foreach (PartyStateChange change in party.ProcessStateChanges())
        {
            _ = change;
        }
    }
}
