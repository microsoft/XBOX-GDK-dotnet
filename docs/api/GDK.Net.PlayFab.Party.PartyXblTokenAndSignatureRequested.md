# <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested"></a> Class PartyXblTokenAndSignatureRequested

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The extension needs an Xbox Live token and signature for an HTTP request. The title must answer
with <xref href="GDK.Net.PlayFab.Party.PartyXblManager.CompleteGetTokenAndSignatureRequest(System.UInt32%2cSystem.String%2cSystem.String)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed record PartyXblTokenAndSignatureRequested : PartyXblStateChange, IEquatable<PartyXblStateChange>, IEquatable<PartyXblTokenAndSignatureRequested>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblTokenAndSignatureRequested](GDK.Net.PlayFab.Party.PartyXblTokenAndSignatureRequested.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblTokenAndSignatureRequested\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyXblStateChange.Kind](GDK.Net.PlayFab.Party.PartyXblStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyXblStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested__ctor_System_UInt32_System_String_System_String_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyXblHttpHeader__System_Byte___System_Boolean_System_Boolean_GDK_Net_PlayFab_Party_PartyXblChatUser_"></a> PartyXblTokenAndSignatureRequested\(uint, string?, string?, IReadOnlyList<PartyXblHttpHeader\>, byte\[\], bool, bool, PartyXblChatUser?\)

The extension needs an Xbox Live token and signature for an HTTP request. The title must answer
with <xref href="GDK.Net.PlayFab.Party.PartyXblManager.CompleteGetTokenAndSignatureRequest(System.UInt32%2cSystem.String%2cSystem.String)" data-throw-if-not-resolved="false"></xref>.

```csharp
public PartyXblTokenAndSignatureRequested(uint CorrelationId, string? Method, string? Url, IReadOnlyList<PartyXblHttpHeader> Headers, byte[] Body, bool ForceRefresh, bool AllUsers, PartyXblChatUser? LocalChatUser)
```

#### Parameters

`CorrelationId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The id to echo back when answering.

`Method` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The HTTP method of the request to sign.

`Url` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The URL of the request to sign.

`Headers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyXblHttpHeader](GDK.Net.PlayFab.Party.PartyXblHttpHeader.md)\>

The headers of the request to sign.

`Body` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The body of the request to sign.

`ForceRefresh` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether a cached token must not be used.

`AllUsers` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the token must cover every signed-in user.

`LocalChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The user the token is for, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">AllUsers</code> is set.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_AllUsers"></a> AllUsers

Whether the token must cover every signed-in user.

```csharp
public bool AllUsers { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_Body"></a> Body

The body of the request to sign.

```csharp
public byte[] Body { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_CorrelationId"></a> CorrelationId

The id to echo back when answering.

```csharp
public uint CorrelationId { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_ForceRefresh"></a> ForceRefresh

Whether a cached token must not be used.

```csharp
public bool ForceRefresh { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_Headers"></a> Headers

The headers of the request to sign.

```csharp
public IReadOnlyList<PartyXblHttpHeader> Headers { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyXblHttpHeader](GDK.Net.PlayFab.Party.PartyXblHttpHeader.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_LocalChatUser"></a> LocalChatUser

The user the token is for, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">AllUsers</code> is set.

```csharp
public PartyXblChatUser? LocalChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_Method"></a> Method

The HTTP method of the request to sign.

```csharp
public string? Method { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyXblTokenAndSignatureRequested_Url"></a> Url

The URL of the request to sign.

```csharp
public string? Url { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

