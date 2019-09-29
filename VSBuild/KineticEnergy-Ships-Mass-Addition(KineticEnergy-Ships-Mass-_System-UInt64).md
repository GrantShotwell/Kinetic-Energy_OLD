#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Ships](./Assembly-CSharp.md#KineticEnergy-Ships 'KineticEnergy.Ships').[Mass](./KineticEnergy-Ships-Mass.md 'KineticEnergy.Ships.Mass')
## Addition(KineticEnergy.Ships.Mass, System.UInt64) `operator`
An addition operator that avoids overflow by checking if the result is less than either of the original values.  
            If there is an overflow, the result will be [System.UInt64.MaxValue](https://docs.microsoft.com/en-us/dotnet/api/System.UInt64.MaxValue 'System.UInt64.MaxValue').  
            



The [position](./KineticEnergy-Ships-Mass-position.md 'KineticEnergy.Ships.Mass.position') does not change.
### Remarks
There is no mirroring "`+(ulong value, Mass mass)`" because it makes sense to add a number to a weighted position, but not to add a weighted position to a number.
### Example
There is no mirroring "`+(ulong value, Mass mass)`" because it makes sense to add a number to a weighted position, but not to add a weighted position to a number.
