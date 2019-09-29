#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## HasBlockAt(UnityEngine.Vector3Int) `method`
Checks if there is any block at the given position in the grid.
### Remarks
Subject to [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors when [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') is false.
### Example
Subject to [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors when [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') is false.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-HasBlockAt(UnityEngine-Vector3Int)-gridPosition'></a>
`gridPosition`

Position in the grid to check.
### Returns
Returns true if there is a block at the given position.
