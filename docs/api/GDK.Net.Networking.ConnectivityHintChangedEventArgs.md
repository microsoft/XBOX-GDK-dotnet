# <a id="GDK_Net_Networking_ConnectivityHintChangedEventArgs"></a> Class ConnectivityHintChangedEventArgs

Namespace: [GDK.Net.Networking](GDK.Net.Networking.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Networking.NetworkingManager.ConnectivityHintChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class ConnectivityHintChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[ConnectivityHintChangedEventArgs](GDK.Net.Networking.ConnectivityHintChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Networking_ConnectivityHintChangedEventArgs_Hint"></a> Hint

The updated connectivity hint.

```csharp
public NetworkingConnectivityHint Hint { get; }
```

#### Property Value

 [NetworkingConnectivityHint](GDK.Net.Networking.NetworkingConnectivityHint.md)

