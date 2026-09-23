# <a id="GDK_Net_Store_StoreCanLicenseStatus"></a> Enum StoreCanLicenseStatus

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

Whether a licence can be acquired for a given product. Mirrors <code>XStoreCanLicenseStatus</code>.

```csharp
public enum StoreCanLicenseStatus : uint
```

## Fields

`Licensable = 1` 

The user can be licensed for this product.



`LicenseActionNotApplicableToProduct = 2` 

The licence action is not applicable to this product type.



`NotLicensableToUser = 0` 

The user cannot be licensed for this product.



