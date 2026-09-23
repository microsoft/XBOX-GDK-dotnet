# <a id="GDK_Net_Accessibility_ClosedCaptionProperties"></a> Struct ClosedCaptionProperties

Namespace: [GDK.Net.Accessibility](GDK.Net.Accessibility.md)  
Assembly: GDK.Net.dll  

Closed-caption display settings from the system (<code>XClosedCaptionGetProperties</code>).
Mirrors <code>XClosedCaptionProperties</code>.

```csharp
public readonly struct ClosedCaptionProperties
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_BackgroundColor"></a> BackgroundColor

Caption background colour.

```csharp
public GameColor BackgroundColor { get; }
```

#### Property Value

 [GameColor](GDK.Net.Accessibility.GameColor.md)

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_FontColor"></a> FontColor

Caption font colour.

```csharp
public GameColor FontColor { get; }
```

#### Property Value

 [GameColor](GDK.Net.Accessibility.GameColor.md)

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_FontEdgeAttribute"></a> FontEdgeAttribute

Font edge rendering style.

```csharp
public ClosedCaptionFontEdgeAttribute FontEdgeAttribute { get; }
```

#### Property Value

 [ClosedCaptionFontEdgeAttribute](GDK.Net.Accessibility.ClosedCaptionFontEdgeAttribute.md)

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_FontScale"></a> FontScale

Font scale factor (1.0 = 100%).

```csharp
public float FontScale { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_FontStyle"></a> FontStyle

Font style.

```csharp
public ClosedCaptionFontStyle FontStyle { get; }
```

#### Property Value

 [ClosedCaptionFontStyle](GDK.Net.Accessibility.ClosedCaptionFontStyle.md)

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_IsEnabled"></a> IsEnabled

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when closed captions are enabled by the user.

```csharp
public bool IsEnabled { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Accessibility_ClosedCaptionProperties_WindowColor"></a> WindowColor

Caption window colour.

```csharp
public GameColor WindowColor { get; }
```

#### Property Value

 [GameColor](GDK.Net.Accessibility.GameColor.md)

