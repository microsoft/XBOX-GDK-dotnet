# <a id="GDK_Net_XboxLive_XboxLiveOptions"></a> Class XboxLiveOptions

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Options for <xref href="GDK.Net.XboxLive.XboxLiveService.Initialize(GDK.Net.XboxLive.XboxLiveOptions)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>XblInitArgs</code> as it is declared for a GDK title.

```csharp
public sealed class XboxLiveOptions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[XboxLiveOptions](GDK.Net.XboxLive.XboxLiveOptions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_XboxLiveOptions_Scid"></a> Scid

The title's Service Configuration ID, from Partner Center's Game Setup page. Required, and
<b>case sensitive</b>: paste it verbatim rather than normalizing it.

```csharp
public string Scid { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

