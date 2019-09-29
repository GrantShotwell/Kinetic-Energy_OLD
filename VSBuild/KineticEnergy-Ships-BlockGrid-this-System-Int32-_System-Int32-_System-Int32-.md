#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## this[System.Int32, System.Int32, System.Int32] `index`
A simple "get" function for an array-space point. To get the grid space, add [offset](./KineticEnergy-Ships-BlockGrid-offset.md 'KineticEnergy.Ships.BlockGrid.offset') to the input.  




`return [x, y, z];`
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-Item(System-Int32-_System-Int32-_System-Int32)-x'></a>
`x`

The X index to look at.

<a name='KineticEnergy-Ships-BlockGrid-Item(System-Int32-_System-Int32-_System-Int32)-y'></a>
`y`

The Y index to look at.

<a name='KineticEnergy-Ships-BlockGrid-Item(System-Int32-_System-Int32-_System-Int32)-z'></a>
`z`

The Z index to look at.
### Returns
Returns the [Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block') that overlaps at the given point of the grid.
