# <a id="GDK_Net_SystemInfo_GameThread"></a> Class GameThread

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Thread time-sensitivity markers. APIs that perform substantial work call
<xref href="GDK.Net.SystemInfo.GameThread.AssertNotTimeSensitive" data-throw-if-not-resolved="false"></xref> to detect accidental invocations from audio or
render threads.

```csharp
public static class GameThread
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameThread](GDK.Net.SystemInfo.GameThread.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Requires the Gaming Runtime to be initialized (<xref href="GDK.Net.GameRuntime.Initialize" data-throw-if-not-resolved="false"></xref>).

## Properties

### <a id="GDK_Net_SystemInfo_GameThread_IsTimeSensitive"></a> IsTimeSensitive

Gets or sets whether the calling thread is marked as time-sensitive
(<code>XThreadIsTimeSensitive</code> / <code>XThreadSetTimeSensitive</code>).

```csharp
public static bool IsTimeSensitive { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Setting this to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> causes any GDK API that performs a potentially
long operation to assert in debug builds via <xref href="GDK.Net.SystemInfo.GameThread.AssertNotTimeSensitive" data-throw-if-not-resolved="false"></xref>.

## Methods

### <a id="GDK_Net_SystemInfo_GameThread_AssertNotTimeSensitive"></a> AssertNotTimeSensitive\(\)

Asserts in debug builds that the calling thread is not marked as time-sensitive
(<code>XThreadAssertNotTimeSensitive</code>).

```csharp
public static void AssertNotTimeSensitive()
```

#### Remarks

APIs that should not be called from audio/render loops call this automatically.
Call it from your own code to document the same requirement.

