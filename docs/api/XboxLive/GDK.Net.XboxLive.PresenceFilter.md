# <a id="GDK_Net_XboxLive_PresenceFilter"></a> Enum PresenceFilter

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Presence filter for a social-manager filter group. Mirrors <code>XblPresenceFilter</code>.

```csharp
public enum PresenceFilter : uint
```

## Fields

`All = 7` 

All users.



`AllOffline = 5` 

All offline users.



`AllOnline = 4` 

All online users.



`AllTitle = 6` 

All users who have played or are playing this title.



`TitleOffline = 2` 

Users offline who have played this title.



`TitleOnline = 1` 

Users currently online and playing this title.



`TitleOnlineOutsideTitle = 3` 

Users online outside this title who have played it.



`Unknown = 0` 

The filter is unknown.



