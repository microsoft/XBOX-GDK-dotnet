# <a id="GDK_Net_UserException"></a> Class UserException

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

Raised for the <code>E_GAMEUSER_*</code> family so callers can catch user-identity failures
(wrong sandbox, signed out, no package identity, …) without inspecting HRESULT values.

```csharp
public class UserException : GameRuntimeException, ISerializable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[GameRuntimeException](GDK.Net.GameRuntimeException.md) ← 
[UserException](GDK.Net.UserException.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)

#### Inherited Members

[GameRuntimeException.HResultCode](GDK.Net.GameRuntimeException.md\#GDK\_Net\_GameRuntimeException\_HResultCode), 
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

[XboxLiveErrors.GetXboxLiveErrorCondition\(GameRuntimeException\)](GDK.Net.XboxLive.XboxLiveErrors.md\#GDK\_Net\_XboxLive\_XboxLiveErrors\_GetXboxLiveErrorCondition\_GDK\_Net\_GameRuntimeException\_)

## Constructors

### <a id="GDK_Net_UserException__ctor_System_Int32_"></a> UserException\(int\)

Initialises a new user exception for <code class="paramref">hresult</code>.

```csharp
public UserException(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT.

### <a id="GDK_Net_UserException__ctor_System_Int32_System_String_"></a> UserException\(int, string\)

Initialises a new user exception for <code class="paramref">hresult</code> with a custom message.

```csharp
public UserException(int hresult, string message)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exception message.

### <a id="GDK_Net_UserException__ctor_System_Int32_System_String_System_Exception_"></a> UserException\(int, string, Exception\)

Initialises a new user exception for <code class="paramref">hresult</code> with a custom message and inner exception.

```csharp
public UserException(int hresult, string message, Exception innerException)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exception message.

`innerException` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception that caused this failure.

