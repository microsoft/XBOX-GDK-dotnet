# <a id="GDK_Net_GameUI_WebAuthenticationResult"></a> Class WebAuthenticationResult

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

Result of a <xref href="GDK.Net.GameUI.GameUiManager.ShowWebAuthenticationAsync(GDK.Net.Users.User%2cSystem.String%2cSystem.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.GameUI.GameUiManager.ShowWebAuthenticationWithOptionsAsync(GDK.Net.Users.User%2cSystem.String%2cSystem.String%2cGDK.Net.GameUI.WebAuthenticationOptions%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> call
(<code>XGameUiShowWebAuthenticationResult</code>).

```csharp
public sealed class WebAuthenticationResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthenticationResult](GDK.Net.GameUI.WebAuthenticationResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_WebAuthenticationResult_CompletionUri"></a> CompletionUri

The URI to which the broker navigated at completion, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the
authentication did not complete successfully.

```csharp
public string? CompletionUri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_WebAuthenticationResult_ResponseStatus"></a> ResponseStatus

The HRESULT status returned by the web authentication broker.
A negative value indicates failure (see <xref href="GDK.Net.HResult.Failed(System.Int32)" data-throw-if-not-resolved="false"></xref>).

```csharp
public int ResponseStatus { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_GameUI_WebAuthenticationResult_Succeeded"></a> Succeeded

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when <xref href="GDK.Net.GameUI.WebAuthenticationResult.ResponseStatus" data-throw-if-not-resolved="false"></xref> is a success code.

```csharp
public bool Succeeded { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

