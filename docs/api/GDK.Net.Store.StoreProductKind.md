# <a id="GDK_Net_Store_StoreProductKind"></a> Enum StoreProductKind

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

The kind of a Store product. Mirrors <code>XStoreProductKind</code>.

```csharp
[Flags]
public enum StoreProductKind : uint
```

## Fields

`Consumable = 1` 

A consumable product (balance decrements on purchase).



`Durable = 2` 

A durable product (permanent entitlement).



`Game = 4` 

The base game.



`None = 0` 

No product kind.



`Pass = 8` 

A pass (subscription or season pass).



`UnmanagedConsumable = 16` 

An unmanaged consumable (the title tracks balance itself).



