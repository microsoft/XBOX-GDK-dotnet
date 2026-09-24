# <a id="GDK_Net_Users_SpopPromptEventArgs"></a> Class SpopPromptEventArgs

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

The Gaming Runtime is asking the title to display a "signed in on another device" (SPOP) prompt
and report back what the user chose.

```csharp
public sealed class SpopPromptEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[SpopPromptEventArgs](GDK.Net.Users.SpopPromptEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The runtime blocks the sign-in until the title calls <xref href="GDK.Net.Users.SpopPromptEventArgs.Complete(GDK.Net.Users.SpopOperationResult)" data-throw-if-not-resolved="false"></xref>. Always call it,
including on the failure paths, or the sign-in never resolves.

## Properties

### <a id="GDK_Net_Users_SpopPromptEventArgs_ModernGamertag"></a> ModernGamertag

The modern gamertag of the account already signed in elsewhere.

```csharp
public string ModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Users_SpopPromptEventArgs_ModernGamertagSuffix"></a> ModernGamertagSuffix

The numeric suffix that disambiguates <xref href="GDK.Net.Users.SpopPromptEventArgs.ModernGamertag" data-throw-if-not-resolved="false"></xref>, or
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the gamertag is unique on its own.

```csharp
public string? ModernGamertagSuffix { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_Users_SpopPromptEventArgs_UserIdentifier"></a> UserIdentifier

The runtime's identifier for the user this prompt belongs to.

```csharp
public uint UserIdentifier { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_Users_SpopPromptEventArgs_Complete_GDK_Net_Users_SpopOperationResult_"></a> Complete\(SpopOperationResult\)

Reports the user's choice back to the Gaming Runtime
(<code>XUserPlatformSpopPromptComplete</code>).

```csharp
public void Complete(SpopOperationResult result)
```

#### Parameters

`result` [SpopOperationResult](GDK.Net.Users.SpopOperationResult.md)

What the user chose.

