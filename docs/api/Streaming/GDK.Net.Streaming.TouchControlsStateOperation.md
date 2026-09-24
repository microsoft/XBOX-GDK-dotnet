# <a id="GDK_Net_Streaming_TouchControlsStateOperation"></a> Class TouchControlsStateOperation

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

A single touch-controls state update operation passed to
<xref href="GDK.Net.Streaming.StreamingManager.UpdateTouchControlsState(System.Collections.Generic.IReadOnlyList%7bGDK.Net.Streaming.TouchControlsStateOperation%7d)" data-throw-if-not-resolved="false"></xref> and related methods.
Mirrors <code>XGameStreamingTouchControlsStateOperation</code>.

```csharp
public sealed class TouchControlsStateOperation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TouchControlsStateOperation](GDK.Net.Streaming.TouchControlsStateOperation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_Streaming_TouchControlsStateOperation__ctor_System_String_GDK_Net_Streaming_TouchControlsStateValue_GDK_Net_Streaming_TouchControlsStateOperationKind_"></a> TouchControlsStateOperation\(string, TouchControlsStateValue, TouchControlsStateOperationKind\)

Initializes a new operation.

```csharp
public TouchControlsStateOperation(string path, TouchControlsStateValue value, TouchControlsStateOperationKind kind = TouchControlsStateOperationKind.Replace)
```

#### Parameters

`path` [string](https://learn.microsoft.com/dotnet/api/system.string)

The JSON Pointer path of the state variable to update.

`value` [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

The new value for the state variable.

`kind` [TouchControlsStateOperationKind](GDK.Net.Streaming.TouchControlsStateOperationKind.md)

The operation kind; defaults to <xref href="GDK.Net.Streaming.TouchControlsStateOperationKind.Replace" data-throw-if-not-resolved="false"></xref>.

## Properties

### <a id="GDK_Net_Streaming_TouchControlsStateOperation_Kind"></a> Kind

The operation kind (<code>XGameStreamingTouchControlsStateOperationKind</code>).

```csharp
public TouchControlsStateOperationKind Kind { get; }
```

#### Property Value

 [TouchControlsStateOperationKind](GDK.Net.Streaming.TouchControlsStateOperationKind.md)

### <a id="GDK_Net_Streaming_TouchControlsStateOperation_Path"></a> Path

JSON Pointer path of the state variable.

```csharp
public string Path { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Streaming_TouchControlsStateOperation_Value"></a> Value

The value to write.

```csharp
public TouchControlsStateValue Value { get; }
```

#### Property Value

 [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

