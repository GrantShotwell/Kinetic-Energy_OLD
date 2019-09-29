#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[Mass](./KineticEnergy-Ships-Mass.md 'KineticEnergy.Ships.Mass')
## Addition(KineticEnergy.Ships.Mass, KineticEnergy.Ships.Mass) `operator`
An addition operator that avoids overflow by checking if the result is less than either of the original values.  
            If there is an overflow, the result will be [System.UInt64.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/System.UInt64.MaxValue 'System.UInt64.MaxValue').  
            



Also gives the appropriate [position](./KineticEnergy-Ships-Mass-position.md 'KineticEnergy.Ships.Mass.position') to the return value.
