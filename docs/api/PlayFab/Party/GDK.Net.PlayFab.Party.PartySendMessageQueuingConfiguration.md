# <a id="GDK_Net_PlayFab_Party_PartySendMessageQueuingConfiguration"></a> Class PartySendMessageQueuingConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_SEND_MESSAGE_QUEUING_CONFIGURATION</code>.

```csharp
public sealed class PartySendMessageQueuingConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartySendMessageQueuingConfiguration](GDK.Net.PlayFab.Party.PartySendMessageQueuingConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartySendMessageQueuingConfiguration_IdentityForCancelFilters"></a> IdentityForCancelFilters

An arbitrary tag matched by the cancel filters.

```csharp
public uint IdentityForCancelFilters { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartySendMessageQueuingConfiguration_Priority"></a> Priority

Relative send priority; higher values are sent first.

```csharp
public sbyte Priority { get; set; }
```

#### Property Value

 [sbyte](https://learn.microsoft.com/dotnet/api/system.sbyte)

### <a id="GDK_Net_PlayFab_Party_PartySendMessageQueuingConfiguration_Timeout"></a> Timeout

How long the message may sit queued before it is dropped.

```csharp
public TimeSpan Timeout { get; set; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

