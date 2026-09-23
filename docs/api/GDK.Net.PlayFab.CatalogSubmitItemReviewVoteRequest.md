# <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest"></a> Class CatalogSubmitItemReviewVoteRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFCatalogSubmitItemReviewVoteRequest</code>.

```csharp
public sealed class CatalogSubmitItemReviewVoteRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CatalogSubmitItemReviewVoteRequest](GDK.Net.PlayFab.CatalogSubmitItemReviewVoteRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_AlternateId"></a> AlternateId

<code>AlternateId</code>.

```csharp
public CatalogCatalogAlternateId? AlternateId { get; set; }
```

#### Property Value

 [CatalogCatalogAlternateId](GDK.Net.PlayFab.CatalogCatalogAlternateId.md)?

### <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_ItemId"></a> ItemId

<code>ItemId</code>.

```csharp
public string? ItemId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_ReviewId"></a> ReviewId

<code>ReviewId</code>.

```csharp
public string? ReviewId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_CatalogSubmitItemReviewVoteRequest_Vote"></a> Vote

<code>Vote</code>.

```csharp
public CatalogHelpfulnessVote? Vote { get; set; }
```

#### Property Value

 [CatalogHelpfulnessVote](GDK.Net.PlayFab.CatalogHelpfulnessVote.md)?

