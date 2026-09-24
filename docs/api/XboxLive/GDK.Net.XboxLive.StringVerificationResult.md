# <a id="GDK_Net_XboxLive_StringVerificationResult"></a> Class StringVerificationResult

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Managed result of verifying one user-generated string with Xbox Live.

```csharp
public sealed class StringVerificationResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringVerificationResult](GDK.Net.XboxLive.StringVerificationResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

String verification is certification-sensitive: user-generated text such as gamertags in chat
or custom object names must be verified before display. This type is fail-closed by
construction: <xref href="GDK.Net.XboxLive.StringVerificationResult.IsAcceptable" data-throw-if-not-resolved="false"></xref> can only be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when
<xref href="GDK.Net.XboxLive.StringVerificationResult.WasVerified" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and Xbox Live explicitly returned
<xref href="GDK.Net.XboxLive.VerifyStringResultCode.Success" data-throw-if-not-resolved="false"></xref>. Failed, canceled or incomplete checks always
produce an unacceptable result.

## Properties

### <a id="GDK_Net_XboxLive_StringVerificationResult_FirstOffendingSubstring"></a> FirstOffendingSubstring

The first offending substring when <xref href="GDK.Net.XboxLive.StringVerificationResult.ResultCode" data-throw-if-not-resolved="false"></xref> is
<xref href="GDK.Net.XboxLive.VerifyStringResultCode.Offensive" data-throw-if-not-resolved="false"></xref>; otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public string? FirstOffendingSubstring { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_StringVerificationResult_IsAcceptable"></a> IsAcceptable

Whether the string is acceptable for display. This is fail-closed and is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only for a completed verification whose <xref href="GDK.Net.XboxLive.StringVerificationResult.ResultCode" data-throw-if-not-resolved="false"></xref> is
<xref href="GDK.Net.XboxLive.VerifyStringResultCode.Success" data-throw-if-not-resolved="false"></xref>.

```csharp
public bool IsAcceptable { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_StringVerificationResult_ResultCode"></a> ResultCode

The Xbox Live result code for the verification.

```csharp
public VerifyStringResultCode ResultCode { get; }
```

#### Property Value

 [VerifyStringResultCode](GDK.Net.XboxLive.VerifyStringResultCode.md)

### <a id="GDK_Net_XboxLive_StringVerificationResult_VerifiedString"></a> VerifiedString

The input string this result corresponds to.

```csharp
public string VerifiedString { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_StringVerificationResult_WasVerified"></a> WasVerified

Whether Xbox Live completed the verification. If this is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>,
<xref href="GDK.Net.XboxLive.StringVerificationResult.IsAcceptable" data-throw-if-not-resolved="false"></xref> is guaranteed to be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

```csharp
public bool WasVerified { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_XboxLive_StringVerificationResult_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

