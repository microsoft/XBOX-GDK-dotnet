# <a id="GDK_Net_Activation_GameActivationEventArgs"></a> Class GameActivationEventArgs

Namespace: [GDK.Net.Activation](GDK.Net.Activation.md)  
Assembly: GDK.Net.dll  

Describes a single activation of the title.

```csharp
public sealed class GameActivationEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameActivationEventArgs](GDK.Net.Activation.GameActivationEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Activation_GameActivationEventArgs_Kind"></a> Kind

What kind of activation this is.

```csharp
public GameActivationType Kind { get; }
```

#### Property Value

 [GameActivationType](GDK.Net.Activation.GameActivationType.md)

### <a id="GDK_Net_Activation_GameActivationEventArgs_Uri"></a> Uri

The activation URI, as delivered by the Gaming Runtime.

```csharp
public string Uri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

