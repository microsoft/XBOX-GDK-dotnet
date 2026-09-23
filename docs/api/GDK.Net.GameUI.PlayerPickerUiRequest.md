# <a id="GDK_Net_GameUI_PlayerPickerUiRequest"></a> Class PlayerPickerUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to let the player choose from a list of players
(<code>XGameUiShowPlayerPickerUiCallback</code>).

```csharp
public sealed class PlayerPickerUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[PlayerPickerUiRequest](GDK.Net.GameUI.PlayerPickerUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_MaxSelectionCount"></a> MaxSelectionCount

The most players the title should let the player select.

```csharp
public uint MaxSelectionCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_MinSelectionCount"></a> MinSelectionCount

The fewest players the title should let the player select.

```csharp
public uint MinSelectionCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_PreSelectedPlayers"></a> PreSelectedPlayers

The Xbox user ids that should start out selected.

```csharp
public IReadOnlyList<ulong> PreSelectedPlayers { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_PromptText"></a> PromptText

The prompt to display above the list.

```csharp
public string? PromptText { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_RequestingUserHandle"></a> RequestingUserHandle

The <code>XUserHandle</code> of the requesting user, as supplied by the runtime.

```csharp
public nint RequestingUserHandle { get; }
```

#### Property Value

 [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_SelectFromPlayers"></a> SelectFromPlayers

The Xbox user ids the player may choose from.

```csharp
public IReadOnlyList<ulong> SelectFromPlayers { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

## Methods

### <a id="GDK_Net_GameUI_PlayerPickerUiRequest_Respond_System_Collections_Generic_IReadOnlyList_System_UInt64__"></a> Respond\(IReadOnlyList<ulong\>\)

Reports the players the user selected (<code>XGameUiSetPlayerPickerUiResponse</code>). Pass an
empty list when the picker was dismissed without a selection.

```csharp
public void Respond(IReadOnlyList<ulong> selectedPlayers)
```

#### Parameters

`selectedPlayers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

The chosen Xbox user ids.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">selectedPlayers</code> is null.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

