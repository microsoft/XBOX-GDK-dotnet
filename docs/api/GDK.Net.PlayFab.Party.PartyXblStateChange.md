# <a id="GDK_Net_PlayFab_Party_PartyXblStateChange"></a> Class PartyXblStateChange

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

One entry from a <xref href="GDK.Net.PlayFab.Party.PartyXblManager.ProcessStateChanges" data-throw-if-not-resolved="false"></xref> batch.

```csharp
public abstract record PartyXblStateChange : IEquatable<PartyXblStateChange>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md)

#### Derived

[PartyXblLocalChatUserDestroyed](GDK.Net.PlayFab.Party.PartyXblLocalChatUserDestroyed.md), 
[PartyXblOperationCompleted](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md), 
[PartyXblRequiredChatPermissionInfoChanged](GDK.Net.PlayFab.Party.PartyXblRequiredChatPermissionInfoChanged.md), 
[PartyXblTokenAndSignatureRequested](GDK.Net.PlayFab.Party.PartyXblTokenAndSignatureRequested.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Every record snapshots its values, so it stays valid after the batch is returned to the
extension; the chat users it references are invalidated when their destruction change is
processed.

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblStateChange__ctor_GDK_Net_PlayFab_Party_PartyXblStateChangeType_"></a> PartyXblStateChange\(PartyXblStateChangeType\)

One entry from a <xref href="GDK.Net.PlayFab.Party.PartyXblManager.ProcessStateChanges" data-throw-if-not-resolved="false"></xref> batch.

```csharp
protected PartyXblStateChange(PartyXblStateChangeType Kind)
```

#### Parameters

`Kind` [PartyXblStateChangeType](GDK.Net.PlayFab.Party.PartyXblStateChangeType.md)

The discriminator matching the native <code>PartyXblStateChangeType</code>.

#### Remarks

Every record snapshots its values, so it stays valid after the batch is returned to the
extension; the chat users it references are invalidated when their destruction change is
processed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblStateChange_Kind"></a> Kind

The discriminator matching the native <code>PartyXblStateChangeType</code>.

```csharp
public PartyXblStateChangeType Kind { get; init; }
```

#### Property Value

 [PartyXblStateChangeType](GDK.Net.PlayFab.Party.PartyXblStateChangeType.md)

