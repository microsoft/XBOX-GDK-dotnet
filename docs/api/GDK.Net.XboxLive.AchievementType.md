# <a id="GDK_Net_XboxLive_AchievementType"></a> Enum AchievementType

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Kind of achievement. Mirrors <code>XblAchievementType</code>.

```csharp
public enum AchievementType : uint
```

## Fields

`All = 1` 

Matches every type. Only meaningful as a query filter.



`Challenge = 3` 

Unlockable only within a time window; never awards gamerscore.



`Persistent = 2` 

Unlockable at any time; may award gamerscore.



`Unknown = 0` 

The type is unknown.



