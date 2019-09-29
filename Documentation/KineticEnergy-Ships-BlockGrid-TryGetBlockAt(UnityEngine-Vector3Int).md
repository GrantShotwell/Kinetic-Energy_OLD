#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## TryGetBlockAt(UnityEngine.Vector3Int) `method`
Same functionality as [GetBlockAt(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GetBlockAt(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)'), but is not subject to [System.IndexOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-TryGetBlockAt(UnityEngine-Vector3Int)-gridPosition'></a>
`gridPosition`

The position in the grid to check.
### Returns
Returns null if [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') fails, otherwise returns [GetBlockAt(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GetBlockAt(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)').
