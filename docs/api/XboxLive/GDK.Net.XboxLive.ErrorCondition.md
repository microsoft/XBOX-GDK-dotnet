# <a id="GDK_Net_XboxLive_ErrorCondition"></a> Enum ErrorCondition

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Actionable Xbox Live error condition buckets. Mirrors <code>XblErrorCondition</code>.

```csharp
public enum ErrorCondition : uint
```

## Fields

`Auth = 3` 

Authentication failed or must be refreshed.



`GenericError = 1` 

A generic error condition.



`GenericOutOfRange = 2` 

An object or value was out of range.



`Http304NotModified = 6` 

The resource was not modified.



`Http404NotFound = 7` 

The resource was not found.



`Http412PreconditionFailed = 8` 

An HTTP precondition failed.



`Http429TooManyRequests = 9` 

The caller is being rate limited.



`HttpGeneric = 5` 

A generic HTTP failure occurred.



`HttpServiceTimeout = 10` 

The service timed out while processing the request.



`Network = 4` 

Network connectivity failed.



`NoError = 0` 

No error.



`Rta = 11` 

A real-time activity error occurred.



