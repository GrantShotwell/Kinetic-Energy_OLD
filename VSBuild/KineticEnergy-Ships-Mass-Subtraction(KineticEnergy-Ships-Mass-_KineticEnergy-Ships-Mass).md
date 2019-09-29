#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[Mass](./KineticEnergy-Ships-Mass.md 'KineticEnergy.Ships.Mass')
## Subtraction(KineticEnergy.Ships.Mass, KineticEnergy.Ships.Mass) `operator`
A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.  
            If there is an underflow, the result will be [System.UInt64.MinValue](https://docs.microsoft.com/en-us/dotnet/api/System.UInt64.MinValue 'System.UInt64.MinValue').  
            



Also gives the appropriate [position](./KineticEnergy-Ships-Mass-position.md 'KineticEnergy.Ships.Mass.position') to the return value.
