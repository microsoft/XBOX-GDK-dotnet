# <a id="GDK_Net_XboxLive_VerifyStringResultCode"></a> Enum VerifyStringResultCode

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Result code returned by Xbox Live string verification.

```csharp
public enum VerifyStringResultCode : uint
```

## Fields

`Offensive = 1` 

The string contains offensive content and must not be displayed.



`Success = 0` 

Xbox Live found no issues with the string.



`TooLong = 2` 

The string is too long for Xbox Live to verify and must not be displayed.



`UnknownError = 3` 

The verification could not be completed; the string must not be displayed.



