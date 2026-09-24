# <a id="GDK_Net_PlayFab_Party_PartyException"></a> Class PartyException

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The failure reported by a PlayFab Party entry point or by a failed Party state change.

```csharp
public sealed class PartyException : GameRuntimeException, ISerializable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[GameRuntimeException](../../Core/GDK.Net.GameRuntimeException.md) ← 
[PartyException](GDK.Net.PlayFab.Party.PartyException.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)

#### Inherited Members

[GameRuntimeException.HResultCode](../../Core/GDK.Net.GameRuntimeException.md\#GDK\_Net\_GameRuntimeException\_HResultCode), 
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
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

#### Extension Methods

[XboxLiveErrors.GetXboxLiveErrorCondition\(GameRuntimeException\)](../../XboxLive/GDK.Net.XboxLive.XboxLiveErrors.md\#GDK\_Net\_XboxLive\_XboxLiveErrors\_GetXboxLiveErrorCondition\_GDK\_Net\_GameRuntimeException\_)

## Remarks

Party reports failures as a <code>PartyError</code> rather than an <code>HRESULT</code>, so it is surfaced
as its own exception type carrying the raw code and the message Party supplies for it.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyException_ErrorCode"></a> ErrorCode

The raw <code>PartyError</code> value.

```csharp
public uint ErrorCode { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

