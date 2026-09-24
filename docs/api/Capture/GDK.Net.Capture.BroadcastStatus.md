# <a id="GDK_Net_Capture_BroadcastStatus"></a> Class BroadcastStatus

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Current broadcast capability and state of the user. Returned by
<xref href="GDK.Net.Capture.AppCaptureManager.GetBroadcastStatus(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XAppBroadcastStatus</code>.

```csharp
public sealed class BroadcastStatus
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BroadcastStatus](GDK.Net.Capture.BroadcastStatus.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Capture_BroadcastStatus_CanStartBroadcast"></a> CanStartBroadcast

The user can start a broadcast (<code>canStartBroadcast</code>).

```csharp
public bool CanStartBroadcast { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsAnyAppBroadcasting"></a> IsAnyAppBroadcasting

At least one app is currently broadcasting (<code>isAnyAppBroadcasting</code>).

```csharp
public bool IsAnyAppBroadcasting { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsAppInactive"></a> IsAppInactive

The app is not in the foreground (<code>isAppInactive</code>).

```csharp
public bool IsAppInactive { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsBlockedForApp"></a> IsBlockedForApp

Broadcasting is blocked for this specific application (<code>isBlockedForApp</code>).

```csharp
public bool IsBlockedForApp { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsCaptureResourceUnavailable"></a> IsCaptureResourceUnavailable

A capture resource needed for broadcasting is in use (<code>isCaptureResourceUnavailable</code>).

```csharp
public bool IsCaptureResourceUnavailable { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsDisabledBySystem"></a> IsDisabledBySystem

The system has disabled broadcasting (<code>isDisabledBySystem</code>).

```csharp
public bool IsDisabledBySystem { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsDisabledByUser"></a> IsDisabledByUser

The user has disabled broadcasting in system settings (<code>isDisabledByUser</code>).

```csharp
public bool IsDisabledByUser { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsGameStreamInProgress"></a> IsGameStreamInProgress

A game-streaming session is in progress (<code>isGameStreamInProgress</code>).

```csharp
public bool IsGameStreamInProgress { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_BroadcastStatus_IsGpuConstrained"></a> IsGpuConstrained

The GPU is constrained and cannot support broadcasting (<code>isGpuConstrained</code>).

```csharp
public bool IsGpuConstrained { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

