#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## TryHasBlockAt(UnityEngine.Vector3Int) `method`
Same functionality as [HasBlockAt(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-HasBlockAt(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.HasBlockAt(UnityEngine.Vector3Int)'), but runs [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') first so it is not subject to [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-TryHasBlockAt(UnityEngine-Vector3Int)-gridPosition'></a>
`gridPosition`

Position in the grid to check.
### Returns
Returns false if 'InsideGrid(position)' fails, otherwise returns 'HasBlock(position)'.
