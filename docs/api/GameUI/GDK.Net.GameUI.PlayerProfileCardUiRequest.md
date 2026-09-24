# <a id="GDK_Net_GameUI_PlayerProfileCardUiRequest"></a> Class PlayerProfileCardUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to show the profile card for <xref href="GDK.Net.GameUI.PlayerProfileCardUiRequest.TargetPlayerId" data-throw-if-not-resolved="false"></xref>
(<code>XGameUiShowPlayerProfileCardUiCallback</code>).

```csharp
public sealed class PlayerProfileCardUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[PlayerProfileCardUiRequest](GDK.Net.GameUI.PlayerProfileCardUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_PlayerProfileCardUiRequest_RequestingUserHandle"></a> RequestingUserHandle

The <code>XUserHandle</code> of the user who asked for the card, as supplied by the runtime.
Duplicate it with <xref href="GDK.Net.GameUI.CustomGameUi.DuplicateUser(System.IntPtr)" data-throw-if-not-resolved="false"></xref> before using it beyond the
lifetime of the originating operation.

```csharp
public nint RequestingUserHandle { get; }
```

#### Property Value

 [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

### <a id="GDK_Net_GameUI_PlayerProfileCardUiRequest_TargetPlayerId"></a> TargetPlayerId

The Xbox user id whose profile card was requested.

```csharp
public ulong TargetPlayerId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_GameUI_PlayerProfileCardUiRequest_Respond"></a> Respond\(\)

Reports that the title has finished showing the profile card
(<code>XGameUiSetPlayerProfileCardUiResponse</code>).

```csharp
public void Respond()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

