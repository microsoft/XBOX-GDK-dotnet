# <a id="GDK_Net_XboxLive_StringVerificationService"></a> Class StringVerificationService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live user-generated text verification. Mirrors <code>string_verify_c.h</code>.

```csharp
public sealed class StringVerificationService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringVerificationService](GDK.Net.XboxLive.StringVerificationService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Titles must verify user-generated text before display, including chat gamertags, custom names
and similar content. This projection makes that obligation fail-closed: if Xbox Live cannot
complete a check for any reason, including cancellation, missing native exports or result-buffer
failure, the returned <xref href="GDK.Net.XboxLive.StringVerificationResult" data-throw-if-not-resolved="false"></xref> objects have
<xref href="GDK.Net.XboxLive.StringVerificationResult.IsAcceptable" data-throw-if-not-resolved="false"></xref> <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
</p>
<p>
The plural API preserves Xbox Live's result ordering exactly: result index <code>i</code> corresponds
to input string index <code>i</code>, and each result also carries its
<xref href="GDK.Net.XboxLive.StringVerificationResult.VerifiedString" data-throw-if-not-resolved="false"></xref>.
</p>

## Methods

### <a id="GDK_Net_XboxLive_StringVerificationService_VerifyStringAsync_System_String_System_Threading_CancellationToken_"></a> VerifyStringAsync\(string, CancellationToken\)

Verifies whether one user-generated string is acceptable for display
(<code>XblStringVerifyStringAsync</code>).

```csharp
public Task<StringVerificationResult> VerifyStringAsync(string text, CancellationToken cancellationToken = default)
```

#### Parameters

`text` [string](https://learn.microsoft.com/dotnet/api/system.string)

The string to verify. Empty strings are allowed.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the native call; the returned result is unacceptable.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[StringVerificationResult](GDK.Net.XboxLive.StringVerificationResult.md)\>

A fail-closed verification result. <xref href="GDK.Net.XboxLive.StringVerificationResult.IsAcceptable" data-throw-if-not-resolved="false"></xref> is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only when Xbox Live completed the check and explicitly returned
<xref href="GDK.Net.XboxLive.VerifyStringResultCode.Success" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_StringVerificationService_VerifyStringsAsync_System_Collections_Generic_IEnumerable_System_String__System_Threading_CancellationToken_"></a> VerifyStringsAsync\(IEnumerable<string\>, CancellationToken\)

Verifies whether several user-generated strings are acceptable for display
(<code>XblStringVerifyStringsAsync</code>).

```csharp
public Task<IReadOnlyList<StringVerificationResult>> VerifyStringsAsync(IEnumerable<string> texts, CancellationToken cancellationToken = default)
```

#### Parameters

`texts` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The strings to verify. Empty strings are allowed.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the native call; all returned results are unacceptable.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StringVerificationResult](GDK.Net.XboxLive.StringVerificationResult.md)\>\>

A fail-closed result for each input string, in the same order as <code class="paramref">texts</code>.
<xref href="GDK.Net.XboxLive.StringVerificationResult.IsAcceptable" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only for
entries Xbox Live explicitly verified as
<xref href="GDK.Net.XboxLive.VerifyStringResultCode.Success" data-throw-if-not-resolved="false"></xref>.

