# <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload"></a> Class GetPlayerCombinedInfoResultPayload

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFGetPlayerCombinedInfoResultPayload</code>.

```csharp
public sealed class GetPlayerCombinedInfoResultPayload
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GetPlayerCombinedInfoResultPayload](GDK.Net.PlayFab.GetPlayerCombinedInfoResultPayload.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_AccountInfo"></a> AccountInfo

<code>AccountInfo</code>.

```csharp
public UserAccountInfo? AccountInfo { get; set; }
```

#### Property Value

 [UserAccountInfo](GDK.Net.PlayFab.UserAccountInfo.md)?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_CharacterInventories"></a> CharacterInventories

<code>CharacterInventories</code>.

```csharp
public IReadOnlyList<CharacterInventory>? CharacterInventories { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[CharacterInventory](GDK.Net.PlayFab.CharacterInventory.md)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_CharacterList"></a> CharacterList

<code>CharacterList</code>.

```csharp
public IReadOnlyList<CharacterResult>? CharacterList { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[CharacterResult](GDK.Net.PlayFab.CharacterResult.md)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_PlayerProfile"></a> PlayerProfile

<code>PlayerProfile</code>.

```csharp
public PlayerProfileModel? PlayerProfile { get; set; }
```

#### Property Value

 [PlayerProfileModel](GDK.Net.PlayFab.PlayerProfileModel.md)?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_PlayerStatistics"></a> PlayerStatistics

<code>PlayerStatistics</code>.

```csharp
public IReadOnlyList<StatisticValue>? PlayerStatistics { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StatisticValue](GDK.Net.PlayFab.StatisticValue.md)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_TitleData"></a> TitleData

<code>TitleData</code>.

```csharp
public IReadOnlyDictionary<string, string>? TitleData { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserData"></a> UserData

<code>UserData</code>.

```csharp
public IReadOnlyDictionary<string, UserDataRecord>? UserData { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [UserDataRecord](GDK.Net.PlayFab.UserDataRecord.md)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserDataVersion"></a> UserDataVersion

<code>UserDataVersion</code>.

```csharp
public uint UserDataVersion { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserInventory"></a> UserInventory

<code>UserInventory</code>.

```csharp
public IReadOnlyList<ItemInstance>? UserInventory { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ItemInstance](GDK.Net.PlayFab.ItemInstance.md)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserReadOnlyData"></a> UserReadOnlyData

<code>UserReadOnlyData</code>.

```csharp
public IReadOnlyDictionary<string, UserDataRecord>? UserReadOnlyData { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [UserDataRecord](GDK.Net.PlayFab.UserDataRecord.md)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserReadOnlyDataVersion"></a> UserReadOnlyDataVersion

<code>UserReadOnlyDataVersion</code>.

```csharp
public uint UserReadOnlyDataVersion { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserVirtualCurrency"></a> UserVirtualCurrency

<code>UserVirtualCurrency</code>.

```csharp
public IReadOnlyDictionary<string, int>? UserVirtualCurrency { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

### <a id="GDK_Net_PlayFab_GetPlayerCombinedInfoResultPayload_UserVirtualCurrencyRechargeTimes"></a> UserVirtualCurrencyRechargeTimes

<code>UserVirtualCurrencyRechargeTimes</code>.

```csharp
public IReadOnlyDictionary<string, VirtualCurrencyRechargeTime>? UserVirtualCurrencyRechargeTimes { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [VirtualCurrencyRechargeTime](GDK.Net.PlayFab.VirtualCurrencyRechargeTime.md)\>?

