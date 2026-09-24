# <a id="GDK_Net_Store_StoreGameLicense"></a> Class StoreGameLicense

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

The game's licence. Mirrors <code>XStoreGameLicense</code>.

```csharp
public sealed class StoreGameLicense
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreGameLicense](GDK.Net.Store.StoreGameLicense.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreGameLicense_ExpirationDate"></a> ExpirationDate

Expiry date of the licence; <xref href="System.DateTimeOffset.MinValue" data-throw-if-not-resolved="false"></xref> for permanent licences.

```csharp
public DateTimeOffset ExpirationDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Store_StoreGameLicense_IsActive"></a> IsActive

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the licence is currently active.

```csharp
public bool IsActive { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreGameLicense_IsDiscLicense"></a> IsDiscLicense

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this is a disc (offline) licence.

```csharp
public bool IsDiscLicense { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreGameLicense_IsTrial"></a> IsTrial

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this is a trial licence.

```csharp
public bool IsTrial { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreGameLicense_IsTrialOwnedByThisUser"></a> IsTrialOwnedByThisUser

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the trial was purchased by the current user.

```csharp
public bool IsTrialOwnedByThisUser { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreGameLicense_SkuStoreId"></a> SkuStoreId

The Store SKU identifier the licence is for.

```csharp
public string SkuStoreId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreGameLicense_TrialTimeRemainingInSeconds"></a> TrialTimeRemainingInSeconds

Seconds remaining in the trial; 0 if not a trial.

```csharp
public uint TrialTimeRemainingInSeconds { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Store_StoreGameLicense_TrialUniqueId"></a> TrialUniqueId

Unique identifier for the trial.

```csharp
public string TrialUniqueId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

