# <a id="GDK_Net_Package_PackageWriteStats"></a> Struct PackageWriteStats

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

Write-budget statistics for the current packaged process.
Mirrors <code>XPackageWriteStats</code>.

```csharp
public readonly struct PackageWriteStats
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

GDK limits how many bytes a title can write per interval to protect storage performance.
Monitor this to avoid exceeding the budget.

## Properties

### <a id="GDK_Net_Package_PackageWriteStats_Budget"></a> Budget

Byte budget for the current interval.

```csharp
public ulong Budget { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Package_PackageWriteStats_BytesWritten"></a> BytesWritten

Bytes written so far in the current interval.

```csharp
public ulong BytesWritten { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Package_PackageWriteStats_Elapsed"></a> Elapsed

Milliseconds elapsed in the current interval.

```csharp
public ulong Elapsed { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Package_PackageWriteStats_Interval"></a> Interval

Budget interval in milliseconds.

```csharp
public ulong Interval { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

