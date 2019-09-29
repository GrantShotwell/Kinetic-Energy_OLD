#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships.Blocks](./Assembly-CSharp.md#KineticEnergy-Ships-Blocks 'KineticEnergy.Ships.Blocks').[Block](./KineticEnergy-Ships-Blocks-Block.md 'KineticEnergy.Ships.Blocks.Block')
## RelativeGridToGrid(UnityEngine.Vector3, UnityEngine.Quaternion) `method`
A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.
### Parameters

<a name='KineticEnergy-Ships-Blocks-Block-RelativeGridToGrid(UnityEngine-Vector3-_UnityEngine-Quaternion)-relativeGridPosition'></a>
`relativeGridPosition`

The relative grid position to translate.

<a name='KineticEnergy-Ships-Blocks-Block-RelativeGridToGrid(UnityEngine-Vector3-_UnityEngine-Quaternion)-inverseLocalRotation'></a>
`inverseLocalRotation`

The [UnityEngine.Quaternion.Inverse(UnityEngine.Quaternion)](https://docs.microsoft.com/en-us/dotnet/api/UnityEngine.Quaternion.Inverse(UnityEngine.Quaternion) 'UnityEngine.Quaternion.Inverse(UnityEngine.Quaternion)') of the [UnityEngine.Transform.localRotation](https://docs.microsoft.com/en-us/dotnet/api/UnityEngine.Transform.localRotation 'UnityEngine.Transform.localRotation').
### Returns
Returns a grid position.
