# <a id="GDK_Net_XboxLive_SocialMultiplayerSessionReference"></a> Class SocialMultiplayerSessionReference

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Identifies an MPSD session related to reputation feedback. Mirrors
<code>XblMultiplayerSessionReference</code>.

```csharp
public sealed class SocialMultiplayerSessionReference
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialMultiplayerSessionReference](GDK.Net.XboxLive.SocialMultiplayerSessionReference.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_SocialMultiplayerSessionReference__ctor_System_String_System_String_System_String_"></a> SocialMultiplayerSessionReference\(string, string, string\)

Creates a session reference from its service configuration, template and session names.

```csharp
public SocialMultiplayerSessionReference(string serviceConfigurationId, string sessionTemplateName, string sessionName)
```

#### Parameters

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The case-sensitive service configuration id.

`sessionTemplateName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The multiplayer session template name.

`sessionName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The multiplayer session name.

## Properties

### <a id="GDK_Net_XboxLive_SocialMultiplayerSessionReference_ServiceConfigurationId"></a> ServiceConfigurationId

The case-sensitive service configuration id.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialMultiplayerSessionReference_SessionName"></a> SessionName

The multiplayer session name.

```csharp
public string SessionName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialMultiplayerSessionReference_SessionTemplateName"></a> SessionTemplateName

The multiplayer session template name.

```csharp
public string SessionTemplateName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

