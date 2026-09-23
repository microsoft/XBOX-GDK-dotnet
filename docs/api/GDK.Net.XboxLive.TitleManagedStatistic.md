# <a id="GDK_Net_XboxLive_TitleManagedStatistic"></a> Class TitleManagedStatistic

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A title-managed statistic to write, update or delete.

```csharp
public sealed class TitleManagedStatistic
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TitleManagedStatistic](GDK.Net.XboxLive.TitleManagedStatistic.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_TitleManagedStatistic__ctor_System_String_System_Double_"></a> TitleManagedStatistic\(string, double\)

Creates a numeric title-managed statistic.

```csharp
public TitleManagedStatistic(string statisticName, double numberValue)
```

#### Parameters

`statisticName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The case-insensitive statistic name.

`numberValue` [double](https://learn.microsoft.com/dotnet/api/system.double)

The numeric value to write.

### <a id="GDK_Net_XboxLive_TitleManagedStatistic__ctor_System_String_System_String_"></a> TitleManagedStatistic\(string, string\)

Creates a string title-managed statistic.

```csharp
public TitleManagedStatistic(string statisticName, string stringValue)
```

#### Parameters

`statisticName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The case-insensitive statistic name.

`stringValue` [string](https://learn.microsoft.com/dotnet/api/system.string)

The string value to write.

### <a id="GDK_Net_XboxLive_TitleManagedStatistic__ctor_System_String_GDK_Net_XboxLive_TitleManagedStatisticValue_"></a> TitleManagedStatistic\(string, TitleManagedStatisticValue\)

Creates a title-managed statistic from a discriminated value.

```csharp
public TitleManagedStatistic(string statisticName, TitleManagedStatisticValue value)
```

#### Parameters

`statisticName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The case-insensitive statistic name.

`value` [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

The value to write.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">statisticName</code> is empty.

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">statisticName</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### <a id="GDK_Net_XboxLive_TitleManagedStatistic_StatisticName"></a> StatisticName

The case-insensitive statistic name.

```csharp
public string StatisticName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_TitleManagedStatistic_Value"></a> Value

The statistic value.

```csharp
public TitleManagedStatisticValue Value { get; }
```

#### Property Value

 [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

