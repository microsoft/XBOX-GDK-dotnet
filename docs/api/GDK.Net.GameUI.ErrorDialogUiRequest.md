# <a id="GDK_Net_GameUI_ErrorDialogUiRequest"></a> Class ErrorDialogUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to show an error dialog (<code>XGameUiShowErrorDialogUiCallback</code>).

```csharp
public sealed class ErrorDialogUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[ErrorDialogUiRequest](GDK.Net.GameUI.ErrorDialogUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_ErrorDialogUiRequest_ErrorCode"></a> ErrorCode

The HRESULT the runtime wants reported to the player.

```csharp
public int ErrorCode { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_GameUI_ErrorDialogUiRequest_ServiceContext"></a> ServiceContext

Optional service context string describing where the error came from.

```csharp
public string? ServiceContext { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="GDK_Net_GameUI_ErrorDialogUiRequest_Respond"></a> Respond\(\)

Reports that the title has finished showing the error
(<code>XGameUiSetErrorDialogUiResponse</code>).

```csharp
public void Respond()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

