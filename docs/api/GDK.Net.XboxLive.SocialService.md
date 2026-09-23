# <a id="GDK_Net_XboxLive_SocialService"></a> Class SocialService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Social graph queries, reputation feedback and relationship-change notifications. Mirrors
<code>social_c.h</code>.

```csharp
public sealed class SocialService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialService](GDK.Net.XboxLive.SocialService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Relationship-change events are backed by real-time activity and are delivered on an
XSAPI-internal thread, not on the projection's <xref href="GDK.Net.GameTaskQueue" data-throw-if-not-resolved="false"></xref>.

## Methods

### <a id="GDK_Net_XboxLive_SocialService_GetRelationshipsAsync_System_UInt64_GDK_Net_XboxLive_SocialRelationshipFilter_System_UInt64_System_UInt64_System_Threading_CancellationToken_"></a> GetRelationshipsAsync\(ulong, SocialRelationshipFilter, ulong, ulong, CancellationToken\)

Gets a page of people socially connected to a user
(<code>XblSocialGetSocialRelationshipsAsync</code>).

```csharp
public Task<SocialRelationshipsPage> GetRelationshipsAsync(ulong xboxUserId, SocialRelationshipFilter filter = SocialRelationshipFilter.All, ulong startIndex = 0, ulong maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id whose relationships to read.

`filter` [SocialRelationshipFilter](GDK.Net.XboxLive.SocialRelationshipFilter.md)

Which relationships to include.

`startIndex` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The zero-based starting index.

`maxItems` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Maximum relationships to return. 0 lets the service choose.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[SocialRelationshipsPage](GDK.Net.XboxLive.SocialRelationshipsPage.md)\>

### <a id="GDK_Net_XboxLive_SocialService_SubmitBatchReputationFeedbackAsync_System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_ReputationFeedbackItem__System_Threading_CancellationToken_"></a> SubmitBatchReputationFeedbackAsync\(IEnumerable<ReputationFeedbackItem\>, CancellationToken\)

Submits reputation feedback for several users
(<code>XblSocialSubmitBatchReputationFeedbackAsync</code>).

```csharp
public Task SubmitBatchReputationFeedbackAsync(IEnumerable<ReputationFeedbackItem> feedbackItems, CancellationToken cancellationToken = default)
```

#### Parameters

`feedbackItems` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ReputationFeedbackItem](GDK.Net.XboxLive.ReputationFeedbackItem.md)\>

The feedback items to submit.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">feedbackItems</code> is empty.

### <a id="GDK_Net_XboxLive_SocialService_SubmitReputationFeedbackAsync_System_UInt64_GDK_Net_XboxLive_ReputationFeedbackType_GDK_Net_XboxLive_SocialMultiplayerSessionReference_System_String_System_String_System_Threading_CancellationToken_"></a> SubmitReputationFeedbackAsync\(ulong, ReputationFeedbackType, SocialMultiplayerSessionReference?, string?, string?, CancellationToken\)

Submits reputation feedback for one user (<code>XblSocialSubmitReputationFeedbackAsync</code>).

```csharp
public Task SubmitReputationFeedbackAsync(ulong xboxUserId, ReputationFeedbackType feedbackType, SocialMultiplayerSessionReference? sessionReference = null, string? reasonMessage = null, string? evidenceResourceId = null, CancellationToken cancellationToken = default)
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

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_XboxLive_SocialService_RelationshipChanged"></a> RelationshipChanged

Raised when XSAPI reports a social relationship change
(<code>XblSocialAddSocialRelationshipChangedHandler</code>).

```csharp
public event EventHandler<SocialRelationshipChangedEventArgs>? RelationshipChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[SocialRelationshipChangedEventArgs](GDK.Net.XboxLive.SocialRelationshipChangedEventArgs.md)\>?

#### Remarks

The native registration is created on the first subscription and released on the last.
Callbacks arrive on an XSAPI-internal thread.

