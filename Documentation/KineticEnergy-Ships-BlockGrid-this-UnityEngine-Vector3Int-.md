#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## this[UnityEngine.Vector3Int] `index`
A simple "get" function for an array-space point. To get the grid space, add [offset](./KineticEnergy-Ships-BlockGrid-offset.md 'KineticEnergy.Ships.BlockGrid.offset') to the input.  




`return [index.x, index.y, index.z];`
### Remarks
Remember that this is array space.
### Example
Remember that this is array space.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-Item(UnityEngine-Vector3Int)-index'></a>
`index`

The index to look at.
### Returns
Returns the [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block') that overlaps at the given point of the grid.
