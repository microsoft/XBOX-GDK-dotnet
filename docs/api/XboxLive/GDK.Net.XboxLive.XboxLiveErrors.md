# <a id="GDK_Net_XboxLive_XboxLiveErrors"></a> Class XboxLiveErrors

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Helpers for mapping Xbox Live HRESULTs to actionable error conditions.

```csharp
public static class XboxLiveErrors
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[XboxLiveErrors](GDK.Net.XboxLive.XboxLiveErrors.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_XboxLive_XboxLiveErrors_GetCondition_System_Int32_"></a> GetCondition\(int\)

Maps an XSAPI HRESULT to an actionable condition (<code>XblGetErrorCondition</code>).

```csharp
public static ErrorCondition GetCondition(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The HRESULT returned by Xbox Live.

#### Returns

 [ErrorCondition](GDK.Net.XboxLive.ErrorCondition.md)

### <a id="GDK_Net_XboxLive_XboxLiveErrors_GetCondition_GDK_Net_GameRuntimeException_"></a> GetCondition\(GameRuntimeException\)

Maps the HRESULT carried by a thrown <xref href="GDK.Net.GameRuntimeException" data-throw-if-not-resolved="false"></xref> to an actionable
condition (<code>XblGetErrorCondition</code>).

```csharp
public static ErrorCondition GetCondition(GameRuntimeException exception)
```

#### Parameters

`exception` [GameRuntimeException](../Core/GDK.Net.GameRuntimeException.md)

The exception thrown by <xref href="GDK.Net.Hr.ThrowIfFailed(System.Int32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [ErrorCondition](GDK.Net.XboxLive.ErrorCondition.md)

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">exception</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="GDK_Net_XboxLive_XboxLiveErrors_GetXboxLiveErrorCondition_GDK_Net_GameRuntimeException_"></a> GetXboxLiveErrorCondition\(GameRuntimeException\)

Maps the HRESULT carried by a thrown <xref href="GDK.Net.GameRuntimeException" data-throw-if-not-resolved="false"></xref> to an actionable
condition (<code>XblGetErrorCondition</code>).

```csharp
public static ErrorCondition GetXboxLiveErrorCondition(this GameRuntimeException exception)
```

#### Parameters

`exception` [GameRuntimeException](../Core/GDK.Net.GameRuntimeException.md)

The Xbox Live exception to classify.

#### Returns

 [ErrorCondition](GDK.Net.XboxLive.ErrorCondition.md)

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">exception</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

