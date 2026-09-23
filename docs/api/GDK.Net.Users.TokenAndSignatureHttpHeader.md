# <a id="GDK_Net_Users_TokenAndSignatureHttpHeader"></a> Struct TokenAndSignatureHttpHeader

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

A single HTTP header for a token-and-signature request.

```csharp
public readonly struct TokenAndSignatureHttpHeader
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_Users_TokenAndSignatureHttpHeader__ctor_System_String_System_String_"></a> TokenAndSignatureHttpHeader\(string, string\)

Initialises the header with a name and value.

```csharp
public TokenAndSignatureHttpHeader(string name, string value)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

## Properties

### <a id="GDK_Net_Users_TokenAndSignatureHttpHeader_Name"></a> Name

The header field name (e.g. <code>Content-Type</code>).

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Users_TokenAndSignatureHttpHeader_Value"></a> Value

The header field value.

```csharp
public string Value { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

