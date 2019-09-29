#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[BlockGrid](./KineticEnergy-Ships-BlockGrid.md 'KineticEnergy.Ships.BlockGrid')
## PlaceNewBlock(UnityEngine.GameObject, UnityEngine.Vector3Int, UnityEngine.Quaternion) `method`
Instantiates a prefab, defines important values such as transform data and name then calls [GetDimensionInformation()](./KineticEnergy-Ships-Blocks-Block-GetDimensionInformation().md 'KineticEnergy.Ships.Blocks.Block.GetDimensionInformation()'),  
then places it on the grid at the given position with [PlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-PlaceBlock(KineticEnergy-Ships-Blocks-Block-_UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.PlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int)').
### Remarks
Subject to clipping if [CanPlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-CanPlaceBlock(KineticEnergy-Ships-Blocks-Block-_UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.CanPlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int)') is false.
### Example
Subject to clipping if [CanPlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int)](./KineticEnergy-Ships-BlockGrid-CanPlaceBlock(KineticEnergy-Ships-Blocks-Block-_UnityEngine-Vector3Int).md 'KineticEnergy.Ships.BlockGrid.CanPlaceBlock(KineticEnergy.Ships.Blocks.Block, UnityEngine.Vector3Int)') is false.
### Parameters

<a name='KineticEnergy-Ships-BlockGrid-PlaceNewBlock(UnityEngine-GameObject-_UnityEngine-Vector3Int-_UnityEngine-Quaternion)-prefab'></a>
`prefab`

Prefab of the "blank" GameObject.

<a name='KineticEnergy-Ships-BlockGrid-PlaceNewBlock(UnityEngine-GameObject-_UnityEngine-Vector3Int-_UnityEngine-Quaternion)-gridPosition'></a>
`gridPosition`

Grid position to place the block at.
