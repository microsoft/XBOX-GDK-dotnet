# <a id="GDK_Net_PlayFab_Party_PartyLocalUser"></a> Class PartyLocalUser

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_LOCAL_USER_HANDLE</code>: a PlayFab entity signed in to the Party library on
this device.

```csharp
public sealed class PartyLocalUser : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyLocalUser_EntityId"></a> EntityId

The PlayFab entity id.

```csharp
public string EntityId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyLocalUser_EntityType"></a> EntityType

The PlayFab entity type, for example <code>title_player_account</code>.

```csharp
public string EntityType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyLocalUser_UpdateEntityToken_System_String_"></a> UpdateEntityToken\(string\)

Supplies a refreshed PlayFab entity token before the previous one expires.

```csharp
public void UpdateEntityToken(string entityToken)
```

#### Parameters

`entityToken` [string](https://learn.microsoft.com/dotnet/api/system.string)

The new entity token.

