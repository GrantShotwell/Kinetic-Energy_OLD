#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## GridPointIsInsideArray(UnityEngine.Vector3Int) `method`
Finds if the given point in grid space is within the bounries of the array.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int)-gridPosition'></a>
`gridPosition`

Some point in grid space.
### Returns
Adds [offset](./KineticEnergy-Ships-BlockGrid-offset.md 'KineticEnergy.Ships.BlockGrid.offset'), then returns true if 'x', 'y', and 'z' are greater than -1 and less than the [System.Array.GetLength(System.Int32)](https://docs.microsoft.com/en-us/dotnet/api/System.Array.GetLength(System.Int32) 'System.Array.GetLength(System.Int32)') [0 through 2].
