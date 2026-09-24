# <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult"></a> Class PrivacyPermissionCheckResult

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Managed result of an Xbox Live privacy permission check.

```csharp
public sealed class PrivacyPermissionCheckResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PrivacyPermissionCheckResult](GDK.Net.XboxLive.PrivacyPermissionCheckResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Privacy gates are a certification-sensitive surface: communications, multiplayer and
user-generated content must fail closed. Therefore <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.IsAllowed" data-throw-if-not-resolved="false"></xref> is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only when Xbox Live completed the check and explicitly granted the
permission. If the native call cannot start, is canceled, or any result-shaping step fails, the
projection returns a result with <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.WasChecked" data-throw-if-not-resolved="false"></xref> <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>,
<xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.IsAllowed" data-throw-if-not-resolved="false"></xref> <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> and an
<xref href="GDK.Net.XboxLive.PermissionDenyReason.Unknown" data-throw-if-not-resolved="false"></xref> reason instead of surfacing an exception that
a title could accidentally convert into access.

## Properties

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_DenyReasons"></a> DenyReasons

Reasons reported by Xbox Live when the permission is denied. A failed or canceled check
yields one <xref href="GDK.Net.XboxLive.PermissionDenyReason.Unknown" data-throw-if-not-resolved="false"></xref> reason.

```csharp
public IReadOnlyList<PrivacyPermissionDenyReasonDetail> DenyReasons { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PrivacyPermissionDenyReasonDetail](GDK.Net.XboxLive.PrivacyPermissionDenyReasonDetail.md)\>

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_IsAllowed"></a> IsAllowed

Whether the action is allowed. This is fail-closed: it can only be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>
when <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.WasChecked" data-throw-if-not-resolved="false"></xref> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and Xbox Live explicitly granted the
permission.

```csharp
public bool IsAllowed { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_Permission"></a> Permission

The permission that was checked.

```csharp
public Permission Permission { get; }
```

#### Property Value

 [Permission](GDK.Net.XboxLive.Permission.md)

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_TargetAnonymousUserType"></a> TargetAnonymousUserType

The anonymous target user class, or <xref href="GDK.Net.XboxLive.AnonymousUserType.Unknown" data-throw-if-not-resolved="false"></xref> when the
target was an Xbox Live user.

```csharp
public AnonymousUserType TargetAnonymousUserType { get; }
```

#### Property Value

 [AnonymousUserType](GDK.Net.XboxLive.AnonymousUserType.md)

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_TargetXboxUserId"></a> TargetXboxUserId

The target Xbox user id, or 0 when the target was an anonymous user class.

```csharp
public ulong TargetXboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_WasChecked"></a> WasChecked

Whether Xbox Live completed the permission check. If this is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>,
<xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.IsAllowed" data-throw-if-not-resolved="false"></xref> is guaranteed to be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

```csharp
public bool WasChecked { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_XboxLive_PrivacyPermissionCheckResult_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

