# <a id="GDK_Net_Hr"></a> Class Hr

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

HRESULT checking helpers. Every HRESULT-returning P/Invoke in this projection is funnelled
through <xref href="GDK.Net.Hr.ThrowIfFailed(System.Int32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

```csharp
public static class Hr
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Hr](GDK.Net.Hr.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_Hr_ThrowIfFailed_System_Int32_System_Threading_CancellationToken_"></a> ThrowIfFailed\(int, CancellationToken\)

Throws the mapped exception when <code class="paramref">hresult</code> denotes failure.
<code>E_ABORT</code> always surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref> so a canceled
operation never reaches a caller as a runtime fault.

```csharp
public static void ThrowIfFailed(int hresult, CancellationToken cancellationToken = default)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

### <a id="GDK_Net_Hr_ToException_System_Int32_System_Threading_CancellationToken_"></a> ToException\(int, CancellationToken\)

Maps a failing HRESULT onto the exception this projection would throw for it.

```csharp
public static Exception ToException(int hresult, CancellationToken cancellationToken = default)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

