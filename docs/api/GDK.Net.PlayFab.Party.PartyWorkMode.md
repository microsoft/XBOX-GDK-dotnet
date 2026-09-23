# <a id="GDK_Net_PlayFab_Party_PartyWorkMode"></a> Enum PartyWorkMode

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Whether the Party library drives one of its threads itself or the title pumps it.

```csharp
public enum PartyWorkMode : uint
```

## Fields

`Automatic = 0` 

Party creates and drives its own thread.



`Manual = 1` 

The title must call <xref href="GDK.Net.PlayFab.Party.PartyManager.DoWork(GDK.Net.PlayFab.Party.PartyThreadId)" data-throw-if-not-resolved="false"></xref> for the thread.



