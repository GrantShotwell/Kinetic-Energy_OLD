#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## CanPlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int) `method`
Tests if a [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block') can fit at the given position.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-CanPlaceBlock(KineticEnergy-Ships-Blocks-Block-_UnityEngine-Vector3Int)-block'></a>
`block`

The would-be [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block').

<a name='KineticEnergy-Ships-BlockGrid-CanPlaceBlock(KineticEnergy-Ships-Blocks-Block-_UnityEngine-Vector3Int)-gridPosition'></a>
`gridPosition`

Position, in grid space, of the would-be [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block').
### Returns
Returns true if [HasBlockAt(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-HasBlockAt(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.HasBlockAt(UnityEngine.Vector3Int)') is true for every grid position inside the block.
