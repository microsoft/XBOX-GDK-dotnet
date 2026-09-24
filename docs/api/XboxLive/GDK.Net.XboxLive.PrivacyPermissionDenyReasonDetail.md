# <a id="GDK_Net_XboxLive_PrivacyPermissionDenyReasonDetail"></a> Class PrivacyPermissionDenyReasonDetail

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Detailed policy reason for a denied Xbox Live privacy permission check.

```csharp
public sealed class PrivacyPermissionDenyReasonDetail
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PrivacyPermissionDenyReasonDetail](GDK.Net.XboxLive.PrivacyPermissionDenyReasonDetail.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_PrivacyPermissionDenyReasonDetail_Reason"></a> Reason

The broad reason the permission was denied.

```csharp
public PermissionDenyReason Reason { get; }
```

#### Property Value

 [PermissionDenyReason](GDK.Net.XboxLive.PermissionDenyReason.md)

### <a id="GDK_Net_XboxLive_PrivacyPermissionDenyReasonDetail_RestrictedPrivacySetting"></a> RestrictedPrivacySetting

The privacy setting involved when <xref href="GDK.Net.XboxLive.PrivacyPermissionDenyReasonDetail.Reason" data-throw-if-not-resolved="false"></xref> is
<xref href="GDK.Net.XboxLive.PermissionDenyReason.PrivacySettingRestrictsTarget" data-throw-if-not-resolved="false"></xref>; otherwise
<xref href="GDK.Net.XboxLive.PrivacySetting.Unknown" data-throw-if-not-resolved="false"></xref>.

```csharp
public PrivacySetting RestrictedPrivacySetting { get; }
```

#### Property Value

 [PrivacySetting](GDK.Net.XboxLive.PrivacySetting.md)

### <a id="GDK_Net_XboxLive_PrivacyPermissionDenyReasonDetail_RestrictedPrivilege"></a> RestrictedPrivilege

The privilege involved when <xref href="GDK.Net.XboxLive.PrivacyPermissionDenyReasonDetail.Reason" data-throw-if-not-resolved="false"></xref> is
<xref href="GDK.Net.XboxLive.PermissionDenyReason.MissingPrivilege" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.XboxLive.PermissionDenyReason.PrivilegeRestrictsTarget" data-throw-if-not-resolved="false"></xref>; otherwise
<xref href="GDK.Net.XboxLive.Privilege.Unknown" data-throw-if-not-resolved="false"></xref>.

```csharp
public Privilege RestrictedPrivilege { get; }
```

#### Property Value

 [Privilege](GDK.Net.XboxLive.Privilege.md)

## Methods

### <a id="GDK_Net_XboxLive_PrivacyPermissionDenyReasonDetail_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

