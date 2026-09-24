# <a id="GDK_Net_Store_StoreSubscriptionInfo"></a> Class StoreSubscriptionInfo

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

Subscription billing details for a SKU. Mirrors <code>XStoreSubscriptionInfo</code>.

```csharp
public sealed class StoreSubscriptionInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreSubscriptionInfo](GDK.Net.Store.StoreSubscriptionInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreSubscriptionInfo_BillingPeriod"></a> BillingPeriod

Billing frequency in <xref href="GDK.Net.Store.StoreSubscriptionInfo.BillingPeriodUnit" data-throw-if-not-resolved="false"></xref> units.

```csharp
public uint BillingPeriod { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Store_StoreSubscriptionInfo_BillingPeriodUnit"></a> BillingPeriodUnit

Unit for <xref href="GDK.Net.Store.StoreSubscriptionInfo.BillingPeriod" data-throw-if-not-resolved="false"></xref>.

```csharp
public StoreDurationUnit BillingPeriodUnit { get; }
```

#### Property Value

 [StoreDurationUnit](GDK.Net.Store.StoreDurationUnit.md)

### <a id="GDK_Net_Store_StoreSubscriptionInfo_HasTrialPeriod"></a> HasTrialPeriod

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the subscription has a trial period.

```csharp
public bool HasTrialPeriod { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Store_StoreSubscriptionInfo_TrialPeriod"></a> TrialPeriod

Length of the trial period in <xref href="GDK.Net.Store.StoreSubscriptionInfo.TrialPeriodUnit" data-throw-if-not-resolved="false"></xref> units.

```csharp
public uint TrialPeriod { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Store_StoreSubscriptionInfo_TrialPeriodUnit"></a> TrialPeriodUnit

Unit for <xref href="GDK.Net.Store.StoreSubscriptionInfo.TrialPeriod" data-throw-if-not-resolved="false"></xref>.

```csharp
public StoreDurationUnit TrialPeriodUnit { get; }
```

#### Property Value

 [StoreDurationUnit](GDK.Net.Store.StoreDurationUnit.md)

