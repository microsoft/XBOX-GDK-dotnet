namespace GDK.Net.PlayFab.Party;

/// <summary>Identifies one of the Party library's internal threads.</summary>
public enum PartyThreadId : uint
{
    /// <summary>The thread that performs audio processing.</summary>
    Audio = 0,

    /// <summary>The thread that performs networking.</summary>
    Networking = 1,
}

/// <summary>
/// Whether the Party library drives one of its threads itself or the title pumps it.
/// </summary>
public enum PartyWorkMode : uint
{
    /// <summary>Party creates and drives its own thread.</summary>
    Automatic = 0,

    /// <summary>The title must call <see cref="PartyManager.DoWork"/> for the thread.</summary>
    Manual = 1,
}
