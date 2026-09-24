# <a id="GDK_Net_PlayFab_PlayFabException"></a> Class PlayFabException

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised when a PlayFab call fails, so a title can catch PlayFab failures without inspecting
HRESULT values.

```csharp
public class PlayFabException : GameRuntimeException, ISerializable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[GameRuntimeException](../Core/GDK.Net.GameRuntimeException.md) ← 
[PlayFabException](GDK.Net.PlayFab.PlayFabException.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)

#### Inherited Members

[GameRuntimeException.HResultCode](../Core/GDK.Net.GameRuntimeException.md\#GDK\_Net\_GameRuntimeException\_HResultCode), 
[Exception.GetBaseException\(\)](https://learn.microsoft.com/dotnet/api/system.exception.getbaseexception), 
[Exception.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.exception.gettype), 
[Exception.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.exception.tostring), 
[Exception.Data](https://learn.microsoft.com/dotnet/api/system.exception.data), 
[Exception.HelpLink](https://learn.microsoft.com/dotnet/api/system.exception.helplink), 
[Exception.HResult](https://learn.microsoft.com/dotnet/api/system.exception.hresult), 
[Exception.InnerException](https://learn.microsoft.com/dotnet/api/system.exception.innerexception), 
[Exception.Message](https://learn.microsoft.com/dotnet/api/system.exception.message), 
[Exception.Source](https://learn.microsoft.com/dotnet/api/system.exception.source), 
[Exception.StackTrace](https://learn.microsoft.com/dotnet/api/system.exception.stacktrace), 
[Exception.TargetSite](https://learn.microsoft.com/dotnet/api/system.exception.targetsite), 
[Exception.SerializeObjectState](https://learn.microsoft.com/dotnet/api/system.exception.serializeobjectstate), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

#### Extension Methods

[XboxLiveErrors.GetXboxLiveErrorCondition\(GameRuntimeException\)](../XboxLive/GDK.Net.XboxLive.XboxLiveErrors.md\#GDK\_Net\_XboxLive\_XboxLiveErrors\_GetXboxLiveErrorCondition\_GDK\_Net\_GameRuntimeException\_)

## Remarks

The message names the <code>E_PF_*</code> symbol when this GDK edition defines one; unknown codes still
carry the raw HRESULT, because the service can return codes newer than the installed headers.

## Constructors

### <a id="GDK_Net_PlayFab_PlayFabException__ctor_System_Int32_"></a> PlayFabException\(int\)

Initialises a new PlayFab exception for <code class="paramref">hresult</code>.

```csharp
public PlayFabException(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing PlayFab HRESULT.

### <a id="GDK_Net_PlayFab_PlayFabException__ctor_System_Int32_System_String_"></a> PlayFabException\(int, string\)

Initialises a new PlayFab exception for <code class="paramref">hresult</code> with a custom message.

```csharp
public PlayFabException(int hresult, string message)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing PlayFab HRESULT.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exception message.

### <a id="GDK_Net_PlayFab_PlayFabException__ctor_System_Int32_System_String_System_Exception_"></a> PlayFabException\(int, string, Exception\)

Initialises a new PlayFab exception for <code class="paramref">hresult</code> with a custom message and inner exception.

```csharp
public PlayFabException(int hresult, string message, Exception innerException)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing PlayFab HRESULT.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exception message.

`innerException` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception that caused this failure.

## Properties

### <a id="GDK_Net_PlayFab_PlayFabException_ErrorName"></a> ErrorName

The <code>E_PF_*</code> symbol for this failure, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when unknown.

```csharp
public string? ErrorName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

