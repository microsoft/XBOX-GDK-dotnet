# <a id="GDK_Net_XboxLive_PresenceQueryFilters"></a> Class PresenceQueryFilters

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Filters for batch and social-group presence queries. Mirrors <code>XblPresenceQueryFilters</code>.

```csharp
public sealed class PresenceQueryFilters
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceQueryFilters](GDK.Net.XboxLive.PresenceQueryFilters.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_PresenceQueryFilters__ctor_System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_PresenceDeviceType__System_Collections_Generic_IEnumerable_System_UInt32__GDK_Net_XboxLive_PresenceDetailLevel_System_Boolean_System_Boolean_"></a> PresenceQueryFilters\(IEnumerable<PresenceDeviceType\>?, IEnumerable<uint\>?, PresenceDetailLevel, bool, bool\)

Creates filters for a presence query.

```csharp
public PresenceQueryFilters(IEnumerable<PresenceDeviceType>? deviceTypes = null, IEnumerable<uint>? titleIds = null, PresenceDetailLevel detailLevel = PresenceDetailLevel.Title, bool onlineOnly = false, bool broadcastingOnly = false)
```

#### Parameters

`deviceTypes` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[PresenceDeviceType](GDK.Net.XboxLive.PresenceDeviceType.md)\>?

Device types to include, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for all devices.

`titleIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)\>?

Title ids to include, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for all titles.

`detailLevel` [PresenceDetailLevel](GDK.Net.XboxLive.PresenceDetailLevel.md)

How much presence detail to request.

`onlineOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether offline users should be filtered out.

`broadcastingOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether users that are not broadcasting should be filtered out.

## Properties

### <a id="GDK_Net_XboxLive_PresenceQueryFilters_BroadcastingOnly"></a> BroadcastingOnly

Whether users that are not broadcasting should be filtered out.

```csharp
public bool BroadcastingOnly { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_PresenceQueryFilters_DetailLevel"></a> DetailLevel

How much presence detail to request.

```csharp
public PresenceDetailLevel DetailLevel { get; }
```

#### Property Value

 [PresenceDetailLevel](GDK.Net.XboxLive.PresenceDetailLevel.md)

### <a id="GDK_Net_XboxLive_PresenceQueryFilters_DeviceTypes"></a> DeviceTypes

Device types to include. Empty means the service default of all devices.

```csharp
public IReadOnlyList<PresenceDeviceType> DeviceTypes { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PresenceDeviceType](GDK.Net.XboxLive.PresenceDeviceType.md)\>

### <a id="GDK_Net_XboxLive_PresenceQueryFilters_OnlineOnly"></a> OnlineOnly

Whether offline users should be filtered out.

```csharp
public bool OnlineOnly { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_PresenceQueryFilters_TitleIds"></a> TitleIds

Title ids to include. Empty means the service default of all titles.

```csharp
public IReadOnlyList<uint> TitleIds { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)\>

