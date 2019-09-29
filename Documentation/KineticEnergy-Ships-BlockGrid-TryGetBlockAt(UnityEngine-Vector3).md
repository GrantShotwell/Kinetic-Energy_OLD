#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## TryGetBlockAt(UnityEngine.Vector3) `method`
Uses [LocalPointToGrid(UnityEngine.Vector3)](./KineticEnergy-Ships-BlockGrid-LocalPointToGrid(UnityEngine-Vector3).md 'KineticEnergy.Ships.BlockGrid.LocalPointToGrid(UnityEngine.Vector3)') to call [TryGetBlockAt(UnityEngine.Vector3)](./KineticEnergy-Ships-BlockGrid-TryGetBlockAt(UnityEngine-Vector3).md 'KineticEnergy.Ships.BlockGrid.TryGetBlockAt(UnityEngine.Vector3)').
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-TryGetBlockAt(UnityEngine-Vector3)-localPosition'></a>
`localPosition`

The position in the grid's transform to check.
### Returns
Returns null if [GridPointIsInsideArray(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') fails, otherwise returns [GetBlockAt(UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-GetBlockAt(UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)').
