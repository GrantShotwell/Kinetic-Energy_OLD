#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.CodeTools.Math](./Assembly-CSharp.md#KineticEnergy-CodeTools-Math 'KineticEnergy.CodeTools.Math').[Geometry](./KineticEnergy-CodeTools-Math-Geometry.md 'KineticEnergy.CodeTools.Math.Geometry')
## Vector2FromAngle(KineticEnergy.CodeTools.Math.Geometry.Angle, System.Single) `method`
Creates a new Vector2 with the given angle and magnitude.
### Parameters

<a name='KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle(KineticEnergy-CodeTools-Math-Geometry-Angle-_System-Single)-angle'></a>
`angle`

The angle of the vector. Specify degrees or radians with 'units'.

<a name='KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle(KineticEnergy-CodeTools-Math-Geometry-Angle-_System-Single)-magnitude'></a>
`magnitude`

The magnitude of the vector.

<a name='KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle(KineticEnergy-CodeTools-Math-Geometry-Angle-_System-Single)-units'></a>
`units`

Is the angle in degrees or radians?
### Returns
Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'
