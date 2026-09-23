# <a id="GDK_Net_GameUI_AchievementsUiRequest"></a> Class AchievementsUiRequest

Namespace: [GDK.Net.GameUI](GDK.Net.GameUI.md)  
Assembly: GDK.Net.dll  

A request to show the achievements UI (<code>XGameUiShowAchievementsUiCallback</code>).

```csharp
public sealed class AchievementsUiRequest : GameUiRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameUiRequest](GDK.Net.GameUI.GameUiRequest.md) ← 
[AchievementsUiRequest](GDK.Net.GameUI.AchievementsUiRequest.md)

#### Inherited Members

[GameUiRequest.HasResponded](GDK.Net.GameUI.GameUiRequest.md\#GDK\_Net\_GameUI\_GameUiRequest\_HasResponded), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameUI_AchievementsUiRequest_RequestingUserHandle"></a> RequestingUserHandle

The <code>XUserHandle</code> whose achievements were requested, as supplied by the runtime.

```csharp
public nint RequestingUserHandle { get; }
```

#### Property Value

 [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

### <a id="GDK_Net_GameUI_AchievementsUiRequest_TitleId"></a> TitleId

The title id whose achievements should be shown.

```csharp
public uint TitleId { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_GameUI_AchievementsUiRequest_Respond"></a> Respond\(\)

Reports that the title has finished showing achievements
(<code>XGameUiSetAchievementsUiResponse</code>).

```csharp
public void Respond()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

A response was already posted.

