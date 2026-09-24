# <a id="GDK_Net_XboxLive_PresenceDetailLevel"></a> Enum PresenceDetailLevel

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

How much presence detail a query should request. Mirrors <code>XblPresenceDetailLevel</code>.

```csharp
public enum PresenceDetailLevel : uint
```

## Fields

`All = 4` 

All available user, device, title and rich presence information.



`Default = 0` 

The service default.



`Device = 2` 

User and device presence, with no title records.



`Title = 3` 

User, device and title presence, with no rich presence strings.



`User = 1` 

User presence only, with no device or title records.



