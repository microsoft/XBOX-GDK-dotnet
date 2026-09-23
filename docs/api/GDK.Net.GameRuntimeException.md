# <a id="GDK_Net_GameRuntimeException"></a> Class GameRuntimeException

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

Raised when a GDK call returns a failing HRESULT.

```csharp
public class GameRuntimeException : Exception, ISerializable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[GameRuntimeException](GDK.Net.GameRuntimeException.md)

#### Derived

[PartyException](GDK.Net.PlayFab.Party.PartyException.md), 
[PlayFabException](GDK.Net.PlayFab.PlayFabException.md), 
[UserException](GDK.Net.UserException.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)

#### Inherited Members

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

## Remarks

Binary serialization is deliberately not supported: it is obsolete on modern .NET and the
exception carries only a numeric HRESULT, which round-trips through <xref href="GDK.Net.GameRuntimeException.HResultCode" data-throw-if-not-resolved="false"></xref>.

## Constructors

### <a id="GDK_Net_GameRuntimeException__ctor_System_Int32_"></a> GameRuntimeException\(int\)

Initialises a new exception for <code class="paramref">hresult</code>.

```csharp
public GameRuntimeException(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT.

### <a id="GDK_Net_GameRuntimeException__ctor_System_Int32_System_String_"></a> GameRuntimeException\(int, string\)

Initialises a new exception for <code class="paramref">hresult</code> with a custom message.

```csharp
public GameRuntimeException(int hresult, string message)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exception message.

### <a id="GDK_Net_GameRuntimeException__ctor_System_Int32_System_String_System_Exception_"></a> GameRuntimeException\(int, string, Exception\)

Initialises a new exception for <code class="paramref">hresult</code> with a custom message and inner exception.

```csharp
public GameRuntimeException(int hresult, string message, Exception innerException)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The failing HRESULT.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exception message.

`innerException` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception that caused this failure.

## Properties

### <a id="GDK_Net_GameRuntimeException_HResultCode"></a> HResultCode

The raw HRESULT returned by the GDK.

```csharp
public int HResultCode { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

