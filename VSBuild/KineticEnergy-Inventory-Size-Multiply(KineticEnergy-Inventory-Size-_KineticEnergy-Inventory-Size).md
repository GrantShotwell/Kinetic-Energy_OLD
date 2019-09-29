#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Inventory](./Assembly-CSharp.md#KineticEnergy-Inventory 'KineticEnergy.Inventory').[Size](./KineticEnergy-Inventory-Size.md 'KineticEnergy.Inventory.Size')
## Multiply(KineticEnergy.Inventory.Size, KineticEnergy.Inventory.Size) `operator`
A multiplication operator that avoids overflow by checking if the result is less than either of the original values.  
            If there is an overflow, the result will be [System.UInt64.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/System.UInt64.MaxValue 'System.UInt64.MaxValue').
