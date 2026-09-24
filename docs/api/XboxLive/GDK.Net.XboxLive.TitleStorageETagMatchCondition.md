# <a id="GDK_Net_XboxLive_TitleStorageETagMatchCondition"></a> Enum TitleStorageETagMatchCondition

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

ETag condition used when reading or writing title storage. Mirrors
<code>XblTitleStorageETagMatchCondition</code>.

```csharp
public enum TitleStorageETagMatchCondition : uint
```

## Fields

`IfMatch = 1` 

Perform the request only when the supplied ETag matches the service value.



`IfNotMatch = 2` 

Perform the request only when the supplied ETag does not match the service value.



`NotUsed = 0` 

No ETag condition is applied.



## Remarks

ETags implement optimistic concurrency. Use <xref href="GDK.Net.XboxLive.TitleStorageETagMatchCondition.IfMatch" data-throw-if-not-resolved="false"></xref> to update or delete only the
version you previously read, and <xref href="GDK.Net.XboxLive.TitleStorageETagMatchCondition.IfNotMatch" data-throw-if-not-resolved="false"></xref> to skip a transfer when the service
already has the supplied version. <xref href="GDK.Net.XboxLive.TitleStorageETagMatchCondition.NotUsed" data-throw-if-not-resolved="false"></xref> ignores the ETag.

