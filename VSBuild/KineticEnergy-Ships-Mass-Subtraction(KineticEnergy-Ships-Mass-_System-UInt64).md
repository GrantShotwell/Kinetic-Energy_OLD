#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[Mass](./KineticEnergy-Ships-Mass.md 'KineticEnergy.Ships.Mass')
## Subtraction(KineticEnergy.Ships.Mass, System.UInt64) `operator`
A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.  
            If there is an underflow, the result will be [System.UInt64.MinValue](https://docs.microsoft.com/en-us/dotnet/api/System.UInt64.MinValue 'System.UInt64.MinValue').  
            



The [position](./KineticEnergy-Ships-Mass-position.md 'KineticEnergy.Ships.Mass.position') does not change.
### Remarks
There is no mirroring "`-(ulong value, Mass mass)`" because it makes sense to subtract a number from a weighted position, but not to subtract a weighted position from a number.
### Example
There is no mirroring "`-(ulong value, Mass mass)`" because it makes sense to subtract a number from a weighted position, but not to subtract a weighted position from a number.
