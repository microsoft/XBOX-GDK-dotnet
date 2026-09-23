# <a id="GDK_Net_XboxLive_SocialManagerExtraDetailLevel"></a> Enum SocialManagerExtraDetailLevel

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Extra social graph detail to load for a local user. Mirrors <code>XblSocialManagerExtraDetailLevel</code>.

```csharp
[Flags]
public enum SocialManagerExtraDetailLevel : uint
```

## Fields

`All = 3` 

Include every supported extra-detail field.



`NoExtraDetail = 0` 

Only default People Hub information, such as profile and presence.



`PreferredColor = 2` 

Include preferred shell color data for users in the graph.



`TitleHistory = 1` 

Include title-history data for users in the graph.



