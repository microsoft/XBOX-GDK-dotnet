# <a id="GDK_Net_PlayFab_Party_PartyXblHttpHeader"></a> Struct PartyXblHttpHeader

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_XBL_HTTP_HEADER</code>: one header of a token-and-signature request.

```csharp
public readonly record struct PartyXblHttpHeader : IEquatable<PartyXblHttpHeader>
```

#### Implements

[IEquatable<PartyXblHttpHeader\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblHttpHeader__ctor_System_String_System_String_"></a> PartyXblHttpHeader\(string, string\)

Projects <code>PARTY_XBL_HTTP_HEADER</code>: one header of a token-and-signature request.

```csharp
public PartyXblHttpHeader(string Name, string Value)
```

#### Parameters

`Name` [string](https://learn.microsoft.com/dotnet/api/system.string)

The header name.

`Value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The header value.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblHttpHeader_Name"></a> Name

The header name.

```csharp
public string Name { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyXblHttpHeader_Value"></a> Value

The header value.

```csharp
public string Value { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

