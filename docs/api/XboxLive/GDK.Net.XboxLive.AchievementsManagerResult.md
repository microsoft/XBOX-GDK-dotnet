# <a id="GDK_Net_XboxLive_AchievementsManagerResult"></a> Class AchievementsManagerResult

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

The result of a synchronous achievements-manager cache query.

```csharp
public sealed class AchievementsManagerResult : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsManagerResult](GDK.Net.XboxLive.AchievementsManagerResult.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Native <code>XblAchievementsManagerResultHandle</code> instances are reference-counted and must be
closed. This wrapper owns one such handle and closes it from <xref href="GDK.Net.XboxLive.AchievementsManagerResult.Dispose" data-throw-if-not-resolved="false"></xref>.
</p>
<p>
The handle owns a native <code>XblAchievement</code> array. <xref href="GDK.Net.XboxLive.AchievementsManagerResult.Achievements" data-throw-if-not-resolved="false"></xref> is fully
snapshotted during construction, reusing the same <xref href="GDK.Net.XboxLive.Achievement" data-throw-if-not-resolved="false"></xref> type returned by
<xref href="GDK.Net.XboxLive.AchievementsService" data-throw-if-not-resolved="false"></xref>, so achievements remain readable after this result is disposed.
</p>

## Properties

### <a id="GDK_Net_XboxLive_AchievementsManagerResult_Achievements"></a> Achievements

The achievements in this cached-query result.

```csharp
public IReadOnlyList<Achievement> Achievements { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[Achievement](GDK.Net.XboxLive.Achievement.md)\>

## Methods

### <a id="GDK_Net_XboxLive_AchievementsManagerResult_Dispose"></a> Dispose\(\)

Closes the native result handle.

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_AchievementsManagerResult_Duplicate"></a> Duplicate\(\)

Creates another managed owner for the same native result handle
(<code>XblAchievementsManagerResultDuplicateHandle</code>).

```csharp
public AchievementsManagerResult Duplicate()
```

#### Returns

 [AchievementsManagerResult](GDK.Net.XboxLive.AchievementsManagerResult.md)

#### Remarks

The duplicate snapshots its own <xref href="GDK.Net.XboxLive.AchievementsManagerResult.Achievements" data-throw-if-not-resolved="false"></xref> list during construction. Use this
only when two components need independent disposable result lifetimes; ordinary callers can
keep the <xref href="GDK.Net.XboxLive.Achievement" data-throw-if-not-resolved="false"></xref> objects instead.

#### Exceptions

 [ObjectDisposedException](https://learn.microsoft.com/dotnet/api/system.objectdisposedexception)

This result has been disposed.

