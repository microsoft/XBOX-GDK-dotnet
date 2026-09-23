# <a id="GDK_Net_XboxLive_ReputationFeedbackItem"></a> Class ReputationFeedbackItem

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One reputation feedback item for batch submission. Mirrors <code>XblReputationFeedbackItem</code>.

```csharp
public sealed class ReputationFeedbackItem
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ReputationFeedbackItem](GDK.Net.XboxLive.ReputationFeedbackItem.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_ReputationFeedbackItem__ctor_System_UInt64_GDK_Net_XboxLive_ReputationFeedbackType_GDK_Net_XboxLive_SocialMultiplayerSessionReference_System_String_System_String_"></a> ReputationFeedbackItem\(ulong, ReputationFeedbackType, SocialMultiplayerSessionReference?, string?, string?\)

Creates one feedback item.

```csharp
public ReputationFeedbackItem(ulong xboxUserId, ReputationFeedbackType feedbackType, SocialMultiplayerSessionReference? sessionReference = null, string? reasonMessage = null, string? evidenceResourceId = null)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id to submit feedback about.

`feedbackType` [ReputationFeedbackType](GDK.Net.XboxLive.ReputationFeedbackType.md)

The kind of feedback to submit.

`sessionReference` [SocialMultiplayerSessionReference](GDK.Net.XboxLive.SocialMultiplayerSessionReference.md)?

Optional MPSD session the feedback relates to.

`reasonMessage` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional user-supplied explanation.

`evidenceResourceId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional resource id for supporting evidence.

## Properties

### <a id="GDK_Net_XboxLive_ReputationFeedbackItem_EvidenceResourceId"></a> EvidenceResourceId

Optional resource id for supporting evidence.

```csharp
public string? EvidenceResourceId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_XboxLive_ReputationFeedbackItem_FeedbackType"></a> FeedbackType

The kind of feedback to submit.

```csharp
public ReputationFeedbackType FeedbackType { get; }
```

#### Property Value

 [ReputationFeedbackType](GDK.Net.XboxLive.ReputationFeedbackType.md)

### <a id="GDK_Net_XboxLive_ReputationFeedbackItem_ReasonMessage"></a> ReasonMessage

User-supplied explanation text. Empty when no reason was supplied.

```csharp
public string ReasonMessage { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_ReputationFeedbackItem_SessionReference"></a> SessionReference

The optional MPSD session the feedback relates to.

```csharp
public SocialMultiplayerSessionReference? SessionReference { get; }
```

#### Property Value

 [SocialMultiplayerSessionReference](GDK.Net.XboxLive.SocialMultiplayerSessionReference.md)?

### <a id="GDK_Net_XboxLive_ReputationFeedbackItem_XboxUserId"></a> XboxUserId

The Xbox user id to submit feedback about.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

