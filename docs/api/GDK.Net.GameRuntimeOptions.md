# <a id="GDK_Net_GameRuntimeOptions"></a> Class GameRuntimeOptions

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

Options for <xref href="GDK.Net.GameRuntime.Initialize(GDK.Net.GameRuntimeOptions)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>struct XGameRuntimeOptions</code> from XGameRuntimeInit.h.

```csharp
public sealed class GameRuntimeOptions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameRuntimeOptions](GDK.Net.GameRuntimeOptions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameRuntimeOptions_GameConfig"></a> GameConfig

Inline XML game-config content (when <xref href="GDK.Net.GameRuntimeOptions.GameConfigSource" data-throw-if-not-resolved="false"></xref> is
<xref href="GDK.Net.GameRuntimeGameConfigSource.Inline" data-throw-if-not-resolved="false"></xref>) or a file path (when
<xref href="GDK.Net.GameRuntimeGameConfigSource.File" data-throw-if-not-resolved="false"></xref>). Ignored for
<xref href="GDK.Net.GameRuntimeGameConfigSource.Default" data-throw-if-not-resolved="false"></xref>.

```csharp
public string? GameConfig { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameRuntimeOptions_GameConfigSource"></a> GameConfigSource

Where to load the game configuration from. Defaults to
<xref href="GDK.Net.GameRuntimeGameConfigSource.Default" data-throw-if-not-resolved="false"></xref>.

```csharp
public GameRuntimeGameConfigSource GameConfigSource { get; set; }
```

#### Property Value

 [GameRuntimeGameConfigSource](GDK.Net.GameRuntimeGameConfigSource.md)

