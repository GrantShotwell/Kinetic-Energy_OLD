#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## GetBlockAt(UnityEngine.Vector3Int) `method`
Finds the block at the given position in the grid.
### Remarks
Subject to [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors when [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') is false.
### Example
Subject to [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors when [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') is false.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-GetBlockAt(UnityEngine-Vector3Int)-gridPosition'></a>
`gridPosition`

Position in the grid to look at.
### Returns
Returns the appropirate block if there is one, otherwise returns null.
