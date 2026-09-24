# <a id="GDK_Net_Users_TokenAndSignature"></a> Class TokenAndSignature

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

The Xbox Live token and optional body-signature returned by
<code>XUserGetTokenAndSignature[Utf16]Async</code>.

```csharp
public sealed class TokenAndSignature
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TokenAndSignature](GDK.Net.Users.TokenAndSignature.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Users_TokenAndSignature_Signature"></a> Signature

The HMAC-SHA256 signature of the request body, or <xref href="System.String.Empty" data-throw-if-not-resolved="false"></xref> when no body
was provided or signing was not requested.

```csharp
public string Signature { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Users_TokenAndSignature_Token"></a> Token

The Xbox Live authentication token.

```csharp
public string Token { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

