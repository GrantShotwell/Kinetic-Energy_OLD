#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## Mass `property`
The sum of every [Mass](./KineticEnergy-Ships-Blocks-Block-Mass.md 'KineticEnergy.Ships.Blocks.Block.Mass'). Updates [UnityEngine.Rigidbody.mass](https://docs.microsoft.com/en-us/dotnet/api/UnityEngine.Rigidbody.mass 'UnityEngine.Rigidbody.mass') when set (internal). Nothing becides "return" is done on get.
### Remarks
This value is never changed by the [BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid') script.  
Instead, the [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block') script updates it on [SetGrid(KineticEnergy.Ships.BlockGrid, UnityEngine.Vector3Int)](./KineticEnergy-Ships-Blocks-Block-SetGrid(KineticEnergy-Ships-BlockGrid-_UnityEngine-Vector3Int).md 'KineticEnergy.Ships.Blocks.Block.SetGrid(KineticEnergy.Ships.BlockGrid, UnityEngine.Vector3Int)') and when set-ing [Mass](./KineticEnergy-Ships-Blocks-Block-Mass.md 'KineticEnergy.Ships.Blocks.Block.Mass').
### Example
This value is never changed by the [BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid') script.  
Instead, the [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block') script updates it on [SetGrid(KineticEnergy.Ships.BlockGrid, UnityEngine.Vector3Int)](./KineticEnergy-Ships-Blocks-Block-SetGrid(KineticEnergy-Ships-BlockGrid-_UnityEngine-Vector3Int).md 'KineticEnergy.Ships.Blocks.Block.SetGrid(KineticEnergy.Ships.BlockGrid, UnityEngine.Vector3Int)') and when set-ing [Mass](./KineticEnergy-Ships-Blocks-Block-Mass.md 'KineticEnergy.Ships.Blocks.Block.Mass').
