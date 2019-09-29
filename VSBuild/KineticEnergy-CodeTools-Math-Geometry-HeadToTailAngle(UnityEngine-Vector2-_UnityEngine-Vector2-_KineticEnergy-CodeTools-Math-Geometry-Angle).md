#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.CodeTools.Math](./Assembly-CSharp.md#KineticEnergy-CodeTools-Math 'KineticEnergy.CodeTools.Math').[Geometry](./KineticEnergy-CodeTools-Math-Geometry.md 'KineticEnergy.CodeTools.Math.Geometry')
## HeadToTailAngle(UnityEngine.Vector2, UnityEngine.Vector2, KineticEnergy.CodeTools.Math.Geometry.Angle) `method`
Changes the heading of v2 so that if v2's tail is placed on the head of v1, the angle between those two vectors is 'degrees'.  
This can also be described as setting the angle of a bend in a line.
### Parameters

<a name='KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle(UnityEngine-Vector2-_UnityEngine-Vector2-_KineticEnergy-CodeTools-Math-Geometry-Angle)-v1'></a>
`v1`

Unchanged vector.

<a name='KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle(UnityEngine-Vector2-_UnityEngine-Vector2-_KineticEnergy-CodeTools-Math-Geometry-Angle)-v2'></a>
`v2`

Changed vector.

<a name='KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle(UnityEngine-Vector2-_UnityEngine-Vector2-_KineticEnergy-CodeTools-Math-Geometry-Angle)-degrees'></a>
`degrees`

The angle in degrees.
### Returns
Returns v2.SetHeading(180.0f - v1.Heading() - degrees).
