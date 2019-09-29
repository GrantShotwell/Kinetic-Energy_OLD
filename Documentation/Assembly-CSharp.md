<a name='assembly'></a>
# Assembly-CSharp

## Contents

- [Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle')
  - [#ctor(value,type)](#M-KineticEnergy-CodeTools-Math-Geometry-Angle-#ctor-System-Single,KineticEnergy-CodeTools-Math-Geometry-AngleType- 'KineticEnergy.CodeTools.Math.Geometry.Angle.#ctor(System.Single,KineticEnergy.CodeTools.Math.Geometry.AngleType)')
  - [radDown](#F-KineticEnergy-CodeTools-Math-Geometry-Angle-radDown 'KineticEnergy.CodeTools.Math.Geometry.Angle.radDown')
  - [radLeft](#F-KineticEnergy-CodeTools-Math-Geometry-Angle-radLeft 'KineticEnergy.CodeTools.Math.Geometry.Angle.radLeft')
  - [radRight](#F-KineticEnergy-CodeTools-Math-Geometry-Angle-radRight 'KineticEnergy.CodeTools.Math.Geometry.Angle.radRight')
  - [radUp](#F-KineticEnergy-CodeTools-Math-Geometry-Angle-radUp 'KineticEnergy.CodeTools.Math.Geometry.Angle.radUp')
  - [type](#F-KineticEnergy-CodeTools-Math-Geometry-Angle-type 'KineticEnergy.CodeTools.Math.Geometry.Angle.type')
  - [value](#F-KineticEnergy-CodeTools-Math-Geometry-Angle-value 'KineticEnergy.CodeTools.Math.Geometry.Angle.value')
  - [Degrees](#P-KineticEnergy-CodeTools-Math-Geometry-Angle-Degrees 'KineticEnergy.CodeTools.Math.Geometry.Angle.Degrees')
  - [Radians](#P-KineticEnergy-CodeTools-Math-Geometry-Angle-Radians 'KineticEnergy.CodeTools.Math.Geometry.Angle.Radians')
  - [ConvertType(type)](#M-KineticEnergy-CodeTools-Math-Geometry-Angle-ConvertType-KineticEnergy-CodeTools-Math-Geometry-AngleType- 'KineticEnergy.CodeTools.Math.Geometry.Angle.ConvertType(KineticEnergy.CodeTools.Math.Geometry.AngleType)')
  - [op_Explicit()](#M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Explicit-KineticEnergy-CodeTools-Math-Geometry-Angle-~System-Int32 'KineticEnergy.CodeTools.Math.Geometry.Angle.op_Explicit(KineticEnergy.CodeTools.Math.Geometry.Angle)~System.Int32')
  - [op_Implicit()](#M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Implicit-System-Single-~KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle.op_Implicit(System.Single)~KineticEnergy.CodeTools.Math.Geometry.Angle')
  - [op_Implicit()](#M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Implicit-KineticEnergy-CodeTools-Math-Geometry-Angle-~System-Single 'KineticEnergy.CodeTools.Math.Geometry.Angle.op_Implicit(KineticEnergy.CodeTools.Math.Geometry.Angle)~System.Single')
  - [op_Implicit()](#M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Implicit-System-Int32-~KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle.op_Implicit(System.Int32)~KineticEnergy.CodeTools.Math.Geometry.Angle')
- [AngleType](#T-KineticEnergy-CodeTools-Math-Geometry-AngleType 'KineticEnergy.CodeTools.Math.Geometry.AngleType')
- [Area](#T-KineticEnergy-CodeTools-Math-Geometry-Area 'KineticEnergy.CodeTools.Math.Geometry.Area')
- [Axis](#T-KineticEnergy-CodeTools-Math-Geometry-Axis 'KineticEnergy.CodeTools.Math.Geometry.Axis')
- [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block')
  - [grid](#F-KineticEnergy-Ships-Blocks-Block-grid 'KineticEnergy.Ships.Blocks.Block.grid')
  - [insidePoints](#F-KineticEnergy-Ships-Blocks-Block-insidePoints 'KineticEnergy.Ships.Blocks.Block.insidePoints')
  - [neighboringPoints](#F-KineticEnergy-Ships-Blocks-Block-neighboringPoints 'KineticEnergy.Ships.Blocks.Block.neighboringPoints')
  - [Dimensions](#P-KineticEnergy-Ships-Blocks-Block-Dimensions 'KineticEnergy.Ships.Blocks.Block.Dimensions')
  - [GridCorner](#P-KineticEnergy-Ships-Blocks-Block-GridCorner 'KineticEnergy.Ships.Blocks.Block.GridCorner')
  - [Mass](#P-KineticEnergy-Ships-Blocks-Block-Mass 'KineticEnergy.Ships.Blocks.Block.Mass')
  - [Name](#P-KineticEnergy-Ships-Blocks-Block-Name 'KineticEnergy.Ships.Blocks.Block.Name')
  - [SurfaceArea](#P-KineticEnergy-Ships-Blocks-Block-SurfaceArea 'KineticEnergy.Ships.Blocks.Block.SurfaceArea')
  - [arrayPosition](#P-KineticEnergy-Ships-Blocks-Block-arrayPosition 'KineticEnergy.Ships.Blocks.Block.arrayPosition')
  - [gridPosition](#P-KineticEnergy-Ships-Blocks-Block-gridPosition 'KineticEnergy.Ships.Blocks.Block.gridPosition')
  - [GetDimensionInformation()](#M-KineticEnergy-Ships-Blocks-Block-GetDimensionInformation 'KineticEnergy.Ships.Blocks.Block.GetDimensionInformation')
  - [RelativeGridToGrid(relativeGridPosition)](#M-KineticEnergy-Ships-Blocks-Block-RelativeGridToGrid-UnityEngine-Vector3- 'KineticEnergy.Ships.Blocks.Block.RelativeGridToGrid(UnityEngine.Vector3)')
  - [RelativeGridToGrid(relativeGridPosition,inverseLocalRotation)](#M-KineticEnergy-Ships-Blocks-Block-RelativeGridToGrid-UnityEngine-Vector3,UnityEngine-Quaternion- 'KineticEnergy.Ships.Blocks.Block.RelativeGridToGrid(UnityEngine.Vector3,UnityEngine.Quaternion)')
  - [SetGrid(grid,gridPosition)](#M-KineticEnergy-Ships-Blocks-Block-SetGrid-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int- 'KineticEnergy.Ships.Blocks.Block.SetGrid(KineticEnergy.Ships.BlockGrid,UnityEngine.Vector3Int)')
  - [UpdateInsidePoints()](#M-KineticEnergy-Ships-Blocks-Block-UpdateInsidePoints 'KineticEnergy.Ships.Blocks.Block.UpdateInsidePoints')
  - [UpdateNeighboringPoints()](#M-KineticEnergy-Ships-Blocks-Block-UpdateNeighboringPoints 'KineticEnergy.Ships.Blocks.Block.UpdateNeighboringPoints')
  - [WhichFacesShown()](#M-KineticEnergy-Ships-Blocks-Block-WhichFacesShown 'KineticEnergy.Ships.Blocks.Block.WhichFacesShown')
- [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid')
  - [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array')
  - [neighborVectors](#F-KineticEnergy-Ships-BlockGrid-neighborVectors 'KineticEnergy.Ships.BlockGrid.neighborVectors')
  - [offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset')
  - [Item](#P-KineticEnergy-Ships-BlockGrid-Item-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.Item(UnityEngine.Vector3Int)')
  - [Item](#P-KineticEnergy-Ships-BlockGrid-Item-System-Int32,System-Int32,System-Int32- 'KineticEnergy.Ships.BlockGrid.Item(System.Int32,System.Int32,System.Int32)')
  - [Mass](#P-KineticEnergy-Ships-BlockGrid-Mass 'KineticEnergy.Ships.BlockGrid.Mass')
  - [Rigidbody](#P-KineticEnergy-Ships-BlockGrid-Rigidbody 'KineticEnergy.Ships.BlockGrid.Rigidbody')
  - [Size](#P-KineticEnergy-Ships-BlockGrid-Size 'KineticEnergy.Ships.BlockGrid.Size')
  - [rigidbody](#P-KineticEnergy-Ships-BlockGrid-rigidbody 'KineticEnergy.Ships.BlockGrid.rigidbody')
  - [AlignWorldPoint(worldPoint)](#M-KineticEnergy-Ships-BlockGrid-AlignWorldPoint-UnityEngine-Vector3- 'KineticEnergy.Ships.BlockGrid.AlignWorldPoint(UnityEngine.Vector3)')
  - [ArrayPointIsInsideArray(arrayPosition)](#M-KineticEnergy-Ships-BlockGrid-ArrayPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.ArrayPointIsInsideArray(UnityEngine.Vector3Int)')
  - [CanPlaceBlock(block,gridPosition)](#M-KineticEnergy-Ships-BlockGrid-CanPlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.CanPlaceBlock(KineticEnergy.Ships.Blocks.Block,UnityEngine.Vector3Int)')
  - [ChangeArrayDimensions(amountPos,amountNeg)](#M-KineticEnergy-Ships-BlockGrid-ChangeArrayDimensions-UnityEngine-Vector3Int,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.ChangeArrayDimensions(UnityEngine.Vector3Int,UnityEngine.Vector3Int)')
  - [ExpandArrayDimensions(amount)](#M-KineticEnergy-Ships-BlockGrid-ExpandArrayDimensions-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.ExpandArrayDimensions(UnityEngine.Vector3Int)')
  - [GetBlockAt(gridPosition)](#M-KineticEnergy-Ships-BlockGrid-GetBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)')
  - [GetNeighborsAt(gridPosition)](#M-KineticEnergy-Ships-BlockGrid-GetNeighborsAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetNeighborsAt(UnityEngine.Vector3Int)')
  - [GetUniqueNeighborsAt(position)](#M-KineticEnergy-Ships-BlockGrid-GetUniqueNeighborsAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetUniqueNeighborsAt(UnityEngine.Vector3Int)')
  - [GridPointIsInsideArray(gridPosition)](#M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)')
  - [GridPointToLocal(gridPoint)](#M-KineticEnergy-Ships-BlockGrid-GridPointToLocal-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointToLocal(UnityEngine.Vector3Int)')
  - [GridPointToWorld(gridPoint)](#M-KineticEnergy-Ships-BlockGrid-GridPointToWorld-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointToWorld(UnityEngine.Vector3Int)')
  - [HasBlockAt(gridPosition)](#M-KineticEnergy-Ships-BlockGrid-HasBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.HasBlockAt(UnityEngine.Vector3Int)')
  - [LocalPointToGrid(localPoint)](#M-KineticEnergy-Ships-BlockGrid-LocalPointToGrid-UnityEngine-Vector3- 'KineticEnergy.Ships.BlockGrid.LocalPointToGrid(UnityEngine.Vector3)')
  - [PlaceBlock(block,gridPosition)](#M-KineticEnergy-Ships-BlockGrid-PlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.PlaceBlock(KineticEnergy.Ships.Blocks.Block,UnityEngine.Vector3Int)')
  - [PlaceEnablingBlock(block,gridPosition)](#M-KineticEnergy-Ships-BlockGrid-PlaceEnablingBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.PlaceEnablingBlock(KineticEnergy.Ships.Blocks.Block,UnityEngine.Vector3Int)')
  - [PlaceNewBlock(prefab,gridPosition)](#M-KineticEnergy-Ships-BlockGrid-PlaceNewBlock-UnityEngine-GameObject,UnityEngine-Vector3Int,UnityEngine-Quaternion- 'KineticEnergy.Ships.BlockGrid.PlaceNewBlock(UnityEngine.GameObject,UnityEngine.Vector3Int,UnityEngine.Quaternion)')
  - [System#Collections#IEnumerable#GetEnumerator()](#M-KineticEnergy-Ships-BlockGrid-System#Collections#IEnumerable#GetEnumerator 'KineticEnergy.Ships.BlockGrid.System#Collections#IEnumerable#GetEnumerator')
  - [TryGetBlockAt(gridPosition)](#M-KineticEnergy-Ships-BlockGrid-TryGetBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.TryGetBlockAt(UnityEngine.Vector3Int)')
  - [TryGetBlockAt(localPosition)](#M-KineticEnergy-Ships-BlockGrid-TryGetBlockAt-UnityEngine-Vector3- 'KineticEnergy.Ships.BlockGrid.TryGetBlockAt(UnityEngine.Vector3)')
  - [TryHasBlockAt(gridPosition)](#M-KineticEnergy-Ships-BlockGrid-TryHasBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.TryHasBlockAt(UnityEngine.Vector3Int)')
  - [WorldPointToGrid(worldPoint)](#M-KineticEnergy-Ships-BlockGrid-WorldPointToGrid-UnityEngine-Vector3- 'KineticEnergy.Ships.BlockGrid.WorldPointToGrid(UnityEngine.Vector3)')
- [BlockGridEditor](#T-KineticEnergy-Ships-BlockGridEditor 'KineticEnergy.Ships.BlockGridEditor')
  - [distance](#F-KineticEnergy-Ships-BlockGridEditor-distance 'KineticEnergy.Ships.BlockGridEditor.distance')
  - [hitError](#F-KineticEnergy-Ships-BlockGridEditor-hitError 'KineticEnergy.Ships.BlockGridEditor.hitError')
  - [preview](#F-KineticEnergy-Ships-BlockGridEditor-preview 'KineticEnergy.Ships.BlockGridEditor.preview')
  - [rotation](#F-KineticEnergy-Ships-BlockGridEditor-rotation 'KineticEnergy.Ships.BlockGridEditor.rotation')
- [BlockGridSceneEditor](#T-KineticEnergy-Ships-BlockGridSceneEditor 'KineticEnergy.Ships.BlockGridSceneEditor')
  - [distance](#F-KineticEnergy-Ships-BlockGridSceneEditor-distance 'KineticEnergy.Ships.BlockGridSceneEditor.distance')
  - [hitError](#F-KineticEnergy-Ships-BlockGridSceneEditor-hitError 'KineticEnergy.Ships.BlockGridSceneEditor.hitError')
  - [selectedBlock](#F-KineticEnergy-Ships-BlockGridSceneEditor-selectedBlock 'KineticEnergy.Ships.BlockGridSceneEditor.selectedBlock')
- [BlockOverlapException](#T-KineticEnergy-Ships-BlockGrid-BlockOverlapException 'KineticEnergy.Ships.BlockGrid.BlockOverlapException')
  - [#ctor(grid,arrayPosition,native,intruder,stoppedOverwrite,stoppedCompletely,tryToFix)](#M-KineticEnergy-Ships-BlockGrid-BlockOverlapException-#ctor-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int,KineticEnergy-Ships-Blocks-Block,KineticEnergy-Ships-Blocks-Block,System-Boolean,System-Boolean,System-Boolean- 'KineticEnergy.Ships.BlockGrid.BlockOverlapException.#ctor(KineticEnergy.Ships.BlockGrid,UnityEngine.Vector3Int,KineticEnergy.Ships.Blocks.Block,KineticEnergy.Ships.Blocks.Block,System.Boolean,System.Boolean,System.Boolean)')
- [BlockPalette](#T-KineticEnergy-Intangibles-Global-BlockPalette 'KineticEnergy.Intangibles.Global.BlockPalette')
  - [samples](#F-KineticEnergy-Intangibles-Global-BlockPalette-samples 'KineticEnergy.Intangibles.Global.BlockPalette.samples')
  - [Count](#P-KineticEnergy-Intangibles-Global-BlockPalette-Count 'KineticEnergy.Intangibles.Global.BlockPalette.Count')
  - [Item](#P-KineticEnergy-Intangibles-Global-BlockPalette-Item-System-Int32- 'KineticEnergy.Intangibles.Global.BlockPalette.Item(System.Int32)')
  - [Add(blockSample)](#M-KineticEnergy-Intangibles-Global-BlockPalette-Add-KineticEnergy-Intangibles-Global-BlockPalette-Sample- 'KineticEnergy.Intangibles.Global.BlockPalette.Add(KineticEnergy.Intangibles.Global.BlockPalette.Sample)')
- [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview')
  - [blockGridPrefab](#F-KineticEnergy-Ships-Blocks-BlockPreview-blockGridPrefab 'KineticEnergy.Ships.Blocks.BlockPreview.blockGridPrefab')
  - [realBlockPrefab](#F-KineticEnergy-Ships-Blocks-BlockPreview-realBlockPrefab 'KineticEnergy.Ships.Blocks.BlockPreview.realBlockPrefab')
  - [Place(grid,position)](#M-KineticEnergy-Ships-Blocks-BlockPreview-Place-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int- 'KineticEnergy.Ships.Blocks.BlockPreview.Place(KineticEnergy.Ships.BlockGrid,UnityEngine.Vector3Int)')
  - [PlaceNewGrid()](#M-KineticEnergy-Ships-Blocks-BlockPreview-PlaceNewGrid 'KineticEnergy.Ships.Blocks.BlockPreview.PlaceNewGrid')
- [Box](#T-KineticEnergy-CodeTools-Math-Geometry-Box 'KineticEnergy.CodeTools.Math.Geometry.Box')
  - [#ctor(corner1,corner2)](#M-KineticEnergy-CodeTools-Math-Geometry-Box-#ctor-UnityEngine-Vector2,UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.Box.#ctor(UnityEngine.Vector2,UnityEngine.Vector2)')
  - [Points](#P-KineticEnergy-CodeTools-Math-Geometry-Box-Points 'KineticEnergy.CodeTools.Math.Geometry.Box.Points')
  - [Contains(point)](#M-KineticEnergy-CodeTools-Math-Geometry-Box-Contains-UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.Box.Contains(UnityEngine.Vector2)')
  - [Place(box)](#M-KineticEnergy-CodeTools-Math-Geometry-Box-Place-KineticEnergy-CodeTools-Math-Geometry-Box- 'KineticEnergy.CodeTools.Math.Geometry.Box.Place(KineticEnergy.CodeTools.Math.Geometry.Box)')
  - [SplitAtX(value)](#M-KineticEnergy-CodeTools-Math-Geometry-Box-SplitAtX-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Box.SplitAtX(System.Single)')
  - [SplitAtY(value)](#M-KineticEnergy-CodeTools-Math-Geometry-Box-SplitAtY-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Box.SplitAtY(System.Single)')
- [Circle](#T-KineticEnergy-CodeTools-Math-Geometry-Circle 'KineticEnergy.CodeTools.Math.Geometry.Circle')
- [ClientBehavioursManager](#T-KineticEnergy-Intangibles-Client-ClientBehavioursManager 'KineticEnergy.Intangibles.Client.ClientBehavioursManager')
  - [parents](#F-KineticEnergy-Intangibles-Client-ClientBehavioursManager-parents 'KineticEnergy.Intangibles.Client.ClientBehavioursManager.parents')
  - [GlobalBehaviours](#P-KineticEnergy-Intangibles-Client-ClientBehavioursManager-GlobalBehaviours 'KineticEnergy.Intangibles.Client.ClientBehavioursManager.GlobalBehaviours')
- [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData')
  - [#ctor(id,name)](#M-KineticEnergy-Intangibles-Client-ClientData-#ctor-System-Int32,System-String- 'KineticEnergy.Intangibles.Client.ClientData.#ctor(System.Int32,System.String)')
  - [id](#F-KineticEnergy-Intangibles-Client-ClientData-id 'KineticEnergy.Intangibles.Client.ClientData.id')
  - [inputs](#F-KineticEnergy-Intangibles-Client-ClientData-inputs 'KineticEnergy.Intangibles.Client.ClientData.inputs')
  - [name](#F-KineticEnergy-Intangibles-Client-ClientData-name 'KineticEnergy.Intangibles.Client.ClientData.name')
- [ClientManager](#T-KineticEnergy-Intangibles-Client-ClientManager 'KineticEnergy.Intangibles.Client.ClientManager')
- [ColorPalette](#T-KineticEnergy-Intangibles-Global-ColorPalette 'KineticEnergy.Intangibles.Global.ColorPalette')
  - [#ctor(capacity)](#M-KineticEnergy-Intangibles-Global-ColorPalette-#ctor-System-Int32- 'KineticEnergy.Intangibles.Global.ColorPalette.#ctor(System.Int32)')
  - [samples](#F-KineticEnergy-Intangibles-Global-ColorPalette-samples 'KineticEnergy.Intangibles.Global.ColorPalette.samples')
  - [Item](#P-KineticEnergy-Intangibles-Global-ColorPalette-Item-System-Int32- 'KineticEnergy.Intangibles.Global.ColorPalette.Item(System.Int32)')
  - [Get(name,color)](#M-KineticEnergy-Intangibles-Global-ColorPalette-Get-System-String,UnityEngine-Color@- 'KineticEnergy.Intangibles.Global.ColorPalette.Get(System.String,UnityEngine.Color@)')
- [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container')
  - [Capacity](#P-KineticEnergy-Inventory-Container-Capacity 'KineticEnergy.Inventory.Container.Capacity')
  - [Contents](#P-KineticEnergy-Inventory-Container-Contents 'KineticEnergy.Inventory.Container.Contents')
  - [UnusedCapacity](#P-KineticEnergy-Inventory-Container-UnusedCapacity 'KineticEnergy.Inventory.Container.UnusedCapacity')
  - [UsedCapacity](#P-KineticEnergy-Inventory-Container-UsedCapacity 'KineticEnergy.Inventory.Container.UsedCapacity')
  - [Add(inputItem)](#M-KineticEnergy-Inventory-Container-Add-KineticEnergy-Inventory-Item- 'KineticEnergy.Inventory.Container.Add(KineticEnergy.Inventory.Item)')
  - [Take(outputItem)](#M-KineticEnergy-Inventory-Container-Take-KineticEnergy-Inventory-Item- 'KineticEnergy.Inventory.Container.Take(KineticEnergy.Inventory.Item)')
- [Direction](#T-KineticEnergy-CodeTools-Math-Geometry-Direction 'KineticEnergy.CodeTools.Math.Geometry.Direction')
- [Extentions](#T-KineticEnergy-CodeTools-Math-Extentions 'KineticEnergy.CodeTools.Math.Extentions')
  - [FromRotation(v1,v2,angle)](#M-KineticEnergy-CodeTools-Math-Extentions-FromRotation-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Extentions.FromRotation(UnityEngine.Vector2,UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [Heading()](#M-KineticEnergy-CodeTools-Math-Extentions-Heading-UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Extentions.Heading(UnityEngine.Vector2)')
  - [Rotate(vector,angle)](#M-KineticEnergy-CodeTools-Math-Extentions-Rotate-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Extentions.Rotate(UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [RotateFrom(v1,v2,theta)](#M-KineticEnergy-CodeTools-Math-Extentions-RotateFrom-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Extentions.RotateFrom(UnityEngine.Vector2,UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [RotateTo(v1,v2,angle)](#M-KineticEnergy-CodeTools-Math-Extentions-RotateTo-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Extentions.RotateTo(UnityEngine.Vector2,UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [SetHeading(v,angle)](#M-KineticEnergy-CodeTools-Math-Extentions-SetHeading-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Extentions.SetHeading(UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [SetMagnitude(v,magnitude)](#M-KineticEnergy-CodeTools-Math-Extentions-SetMagnitude-UnityEngine-Vector2,System-Single- 'KineticEnergy.CodeTools.Math.Extentions.SetMagnitude(UnityEngine.Vector2,System.Single)')
- [Face](#T-KineticEnergy-Ships-Blocks-Block-Face 'KineticEnergy.Ships.Blocks.Block.Face')
- [Faces](#T-KineticEnergy-Ships-Blocks-Faces 'KineticEnergy.Ships.Blocks.Faces')
  - [ToggleFaces(enabledFlaces)](#M-KineticEnergy-Ships-Blocks-Faces-ToggleFaces-System-Byte- 'KineticEnergy.Ships.Blocks.Faces.ToggleFaces(System.Byte)')
- [Gamemode](#T-KineticEnergy-Entities-PlayerController-Gamemode 'KineticEnergy.Entities.PlayerController.Gamemode')
- [Geometry](#T-KineticEnergy-CodeTools-Math-Geometry 'KineticEnergy.CodeTools.Math.Geometry')
  - [AngleDirection(angle,axis)](#M-KineticEnergy-CodeTools-Math-Geometry-AngleDirection-KineticEnergy-CodeTools-Math-Geometry-Angle,KineticEnergy-CodeTools-Math-Geometry-Axis- 'KineticEnergy.CodeTools.Math.Geometry.AngleDirection(KineticEnergy.CodeTools.Math.Geometry.Angle,KineticEnergy.CodeTools.Math.Geometry.Axis)')
  - [AreParallel()](#M-KineticEnergy-CodeTools-Math-Geometry-AreParallel-KineticEnergy-CodeTools-Math-Geometry-Line,KineticEnergy-CodeTools-Math-Geometry-Line- 'KineticEnergy.CodeTools.Math.Geometry.AreParallel(KineticEnergy.CodeTools.Math.Geometry.Line,KineticEnergy.CodeTools.Math.Geometry.Line)')
  - [Exists()](#M-KineticEnergy-CodeTools-Math-Geometry-Exists-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Exists(System.Single)')
  - [Exists()](#M-KineticEnergy-CodeTools-Math-Geometry-Exists-UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.Exists(UnityEngine.Vector2)')
  - [GetAngle(v1,v2)](#M-KineticEnergy-CodeTools-Math-Geometry-GetAngle-UnityEngine-Vector2,UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.GetAngle(UnityEngine.Vector2,UnityEngine.Vector2)')
  - [HeadToTailAngle(v1,v2)](#M-KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle-UnityEngine-Vector2,UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.HeadToTailAngle(UnityEngine.Vector2,UnityEngine.Vector2)')
  - [HeadToTailAngle(v1,v2,degrees)](#M-KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Geometry.HeadToTailAngle(UnityEngine.Vector2,UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [Intersection(l1,l2)](#M-KineticEnergy-CodeTools-Math-Geometry-Intersection-KineticEnergy-CodeTools-Math-Geometry-Line,KineticEnergy-CodeTools-Math-Geometry-Line- 'KineticEnergy.CodeTools.Math.Geometry.Intersection(KineticEnergy.CodeTools.Math.Geometry.Line,KineticEnergy.CodeTools.Math.Geometry.Line)')
  - [Intersection(c1,c2)](#M-KineticEnergy-CodeTools-Math-Geometry-Intersection-KineticEnergy-CodeTools-Math-Geometry-Circle,KineticEnergy-CodeTools-Math-Geometry-Circle- 'KineticEnergy.CodeTools.Math.Geometry.Intersection(KineticEnergy.CodeTools.Math.Geometry.Circle,KineticEnergy.CodeTools.Math.Geometry.Circle)')
  - [Intersection(c1,c2,c3)](#M-KineticEnergy-CodeTools-Math-Geometry-Intersection-KineticEnergy-CodeTools-Math-Geometry-Circle,KineticEnergy-CodeTools-Math-Geometry-Circle,KineticEnergy-CodeTools-Math-Geometry-Circle- 'KineticEnergy.CodeTools.Math.Geometry.Intersection(KineticEnergy.CodeTools.Math.Geometry.Circle,KineticEnergy.CodeTools.Math.Geometry.Circle,KineticEnergy.CodeTools.Math.Geometry.Circle)')
  - [IsBetweenRange(range)](#M-KineticEnergy-CodeTools-Math-Geometry-IsBetweenRange-System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.IsBetweenRange(System.Single,System.Single)')
  - [LawOfCosForAngleA(a,b,c)](#M-KineticEnergy-CodeTools-Math-Geometry-LawOfCosForAngleA-System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.LawOfCosForAngleA(System.Single,System.Single,System.Single)')
  - [LawOfCosForAngleB(a,b,c)](#M-KineticEnergy-CodeTools-Math-Geometry-LawOfCosForAngleB-System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.LawOfCosForAngleB(System.Single,System.Single,System.Single)')
  - [LawOfCosForAngleC(a,b,c)](#M-KineticEnergy-CodeTools-Math-Geometry-LawOfCosForAngleC-System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.LawOfCosForAngleC(System.Single,System.Single,System.Single)')
  - [LimitTo(input,limit)](#M-KineticEnergy-CodeTools-Math-Geometry-LimitTo-System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.LimitTo(System.Single,System.Single)')
  - [LineFromAngle()](#M-KineticEnergy-CodeTools-Math-Geometry-LineFromAngle-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Geometry.LineFromAngle(UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Angle)')
  - [LineFromShift(distance,line)](#M-KineticEnergy-CodeTools-Math-Geometry-LineFromShift-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Line- 'KineticEnergy.CodeTools.Math.Geometry.LineFromShift(UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Line)')
  - [LineFromTwoPoints(p1,p2)](#M-KineticEnergy-CodeTools-Math-Geometry-LineFromTwoPoints-UnityEngine-Vector2,UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.LineFromTwoPoints(UnityEngine.Vector2,UnityEngine.Vector2)')
  - [LinearDirection(input)](#M-KineticEnergy-CodeTools-Math-Geometry-LinearDirection-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.LinearDirection(System.Single)')
  - [LinearDirection(input)](#M-KineticEnergy-CodeTools-Math-Geometry-LinearDirection-System-Single,KineticEnergy-CodeTools-Math-Geometry-Axis- 'KineticEnergy.CodeTools.Math.Geometry.LinearDirection(System.Single,KineticEnergy.CodeTools.Math.Geometry.Axis)')
  - [LinearDirection(vector,axis)](#M-KineticEnergy-CodeTools-Math-Geometry-LinearDirection-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Axis- 'KineticEnergy.CodeTools.Math.Geometry.LinearDirection(UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Axis)')
  - [NormalizeDegree1(degr)](#M-KineticEnergy-CodeTools-Math-Geometry-NormalizeDegree1-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.NormalizeDegree1(System.Single)')
  - [NormalizeDegree2(degr)](#M-KineticEnergy-CodeTools-Math-Geometry-NormalizeDegree2-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.NormalizeDegree2(System.Single)')
  - [NormalizeDegree3(degree)](#M-KineticEnergy-CodeTools-Math-Geometry-NormalizeDegree3-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.NormalizeDegree3(System.Single)')
  - [PathToLine(point,line)](#M-KineticEnergy-CodeTools-Math-Geometry-PathToLine-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Line- 'KineticEnergy.CodeTools.Math.Geometry.PathToLine(UnityEngine.Vector2,KineticEnergy.CodeTools.Math.Geometry.Line)')
  - [RoundToMultiple(number,multiple)](#M-KineticEnergy-CodeTools-Math-Geometry-RoundToMultiple-System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.RoundToMultiple(System.Single,System.Single)')
  - [Vector2FromAngle(angle,magnitude,units)](#M-KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle-KineticEnergy-CodeTools-Math-Geometry-Angle,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Vector2FromAngle(KineticEnergy.CodeTools.Math.Geometry.Angle,System.Single)')
  - [Vector2FromAngle(angle,units)](#M-KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle-KineticEnergy-CodeTools-Math-Geometry-Angle- 'KineticEnergy.CodeTools.Math.Geometry.Vector2FromAngle(KineticEnergy.CodeTools.Math.Geometry.Angle)')
- [GlobalBehavioursManager](#T-KineticEnergy-Intangibles-Global-GlobalBehavioursManager 'KineticEnergy.Intangibles.Global.GlobalBehavioursManager')
  - [parents](#F-KineticEnergy-Intangibles-Global-GlobalBehavioursManager-parents 'KineticEnergy.Intangibles.Global.GlobalBehavioursManager.parents')
  - [GlobalBehaviours](#P-KineticEnergy-Intangibles-Global-GlobalBehavioursManager-GlobalBehaviours 'KineticEnergy.Intangibles.Global.GlobalBehavioursManager.GlobalBehaviours')
- [GlobalPaletteManager](#T-KineticEnergy-Intangibles-Global-GlobalPaletteManager 'KineticEnergy.Intangibles.Global.GlobalPaletteManager')
- [Inputs](#T-KineticEnergy-Intangibles-Client-ClientData-Inputs 'KineticEnergy.Intangibles.Client.ClientData.Inputs')
  - [hotbar](#F-KineticEnergy-Intangibles-Client-ClientData-Inputs-hotbar 'KineticEnergy.Intangibles.Client.ClientData.Inputs.hotbar')
  - [look](#F-KineticEnergy-Intangibles-Client-ClientData-Inputs-look 'KineticEnergy.Intangibles.Client.ClientData.Inputs.look')
  - [move](#F-KineticEnergy-Intangibles-Client-ClientData-Inputs-move 'KineticEnergy.Intangibles.Client.ClientData.Inputs.move')
  - [primary](#F-KineticEnergy-Intangibles-Client-ClientData-Inputs-primary 'KineticEnergy.Intangibles.Client.ClientData.Inputs.primary')
  - [secondary](#F-KineticEnergy-Intangibles-Client-ClientData-Inputs-secondary 'KineticEnergy.Intangibles.Client.ClientData.Inputs.secondary')
  - [spin](#F-KineticEnergy-Intangibles-Client-ClientData-Inputs-spin 'KineticEnergy.Intangibles.Client.ClientData.Inputs.spin')
- [InspectorHelper](#T-KineticEnergy-Unity-InspectorHelper 'KineticEnergy.Unity.InspectorHelper')
  - [TargetsAreMixed\`\`1(targets,mixedTest)](#M-KineticEnergy-Unity-InspectorHelper-TargetsAreMixed``1-UnityEngine-Object[],System-Func{``0,``0,System-Boolean}- 'KineticEnergy.Unity.InspectorHelper.TargetsAreMixed``1(UnityEngine.Object[],System.Func{``0,``0,System.Boolean})')
- [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item')
  - [count](#F-KineticEnergy-Inventory-Item-count 'KineticEnergy.Inventory.Item.count')
  - [StackSize](#P-KineticEnergy-Inventory-Item-StackSize 'KineticEnergy.Inventory.Item.StackSize')
  - [MaxToAdd(maximumSize)](#M-KineticEnergy-Inventory-Item-MaxToAdd-KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.Item.MaxToAdd(KineticEnergy.Inventory.Size)')
  - [MaxToSub(maximumSize)](#M-KineticEnergy-Inventory-Item-MaxToSub-KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.Item.MaxToSub(KineticEnergy.Inventory.Size)')
- [Line](#T-KineticEnergy-CodeTools-Math-Geometry-Line 'KineticEnergy.CodeTools.Math.Geometry.Line')
  - [#ctor(A,B,C,x)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-#ctor-System-Single,System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Line.#ctor(System.Single,System.Single,System.Single,System.Single)')
  - [#ctor(A,B,C)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-#ctor-System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Line.#ctor(System.Single,System.Single,System.Single)')
  - [X](#F-KineticEnergy-CodeTools-Math-Geometry-Line-X 'KineticEnergy.CodeTools.Math.Geometry.Line.X')
  - [a](#F-KineticEnergy-CodeTools-Math-Geometry-Line-a 'KineticEnergy.CodeTools.Math.Geometry.Line.a')
  - [b](#F-KineticEnergy-CodeTools-Math-Geometry-Line-b 'KineticEnergy.CodeTools.Math.Geometry.Line.b')
  - [c](#F-KineticEnergy-CodeTools-Math-Geometry-Line-c 'KineticEnergy.CodeTools.Math.Geometry.Line.c')
  - [isVertical](#F-KineticEnergy-CodeTools-Math-Geometry-Line-isVertical 'KineticEnergy.CodeTools.Math.Geometry.Line.isVertical')
  - [slope](#P-KineticEnergy-CodeTools-Math-Geometry-Line-slope 'KineticEnergy.CodeTools.Math.Geometry.Line.slope')
  - [Angle()](#M-KineticEnergy-CodeTools-Math-Geometry-Line-Angle-KineticEnergy-CodeTools-Math-Geometry-AngleType- 'KineticEnergy.CodeTools.Math.Geometry.Line.Angle(KineticEnergy.CodeTools.Math.Geometry.AngleType)')
  - [Intersection()](#M-KineticEnergy-CodeTools-Math-Geometry-Line-Intersection-KineticEnergy-CodeTools-Math-Geometry-Line- 'KineticEnergy.CodeTools.Math.Geometry.Line.Intersection(KineticEnergy.CodeTools.Math.Geometry.Line)')
  - [PointFromDistance(point,distance,direction)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromDistance-UnityEngine-Vector2,System-Single,UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Geometry.Line.PointFromDistance(UnityEngine.Vector2,System.Single,UnityEngine.Vector2)')
  - [PointFromDistance(point,distance,direction)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromDistance-UnityEngine-Vector2,System-Single,System-Int32- 'KineticEnergy.CodeTools.Math.Geometry.Line.PointFromDistance(UnityEngine.Vector2,System.Single,System.Int32)')
  - [PointFromX(x)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromX-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Line.PointFromX(System.Single)')
  - [PointFromY(y)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromY-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Line.PointFromY(System.Single)')
  - [ShiftX(d)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-ShiftX-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Line.ShiftX(System.Single)')
  - [ShiftY(d)](#M-KineticEnergy-CodeTools-Math-Geometry-Line-ShiftY-System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Line.ShiftY(System.Single)')
- [Mass](#T-KineticEnergy-Ships-Mass 'KineticEnergy.Ships.Mass')
  - [magnitude](#F-KineticEnergy-Ships-Mass-magnitude 'KineticEnergy.Ships.Mass.magnitude')
  - [one](#F-KineticEnergy-Ships-Mass-one 'KineticEnergy.Ships.Mass.one')
  - [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position')
  - [zero](#F-KineticEnergy-Ships-Mass-zero 'KineticEnergy.Ships.Mass.zero')
  - [Equals()](#M-KineticEnergy-Ships-Mass-Equals-System-Object- 'KineticEnergy.Ships.Mass.Equals(System.Object)')
  - [GetHashCode()](#M-KineticEnergy-Ships-Mass-GetHashCode 'KineticEnergy.Ships.Mass.GetHashCode')
  - [op_Addition()](#M-KineticEnergy-Ships-Mass-op_Addition-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass- 'KineticEnergy.Ships.Mass.op_Addition(KineticEnergy.Ships.Mass,KineticEnergy.Ships.Mass)')
  - [op_Addition()](#M-KineticEnergy-Ships-Mass-op_Addition-KineticEnergy-Ships-Mass,System-UInt64- 'KineticEnergy.Ships.Mass.op_Addition(KineticEnergy.Ships.Mass,System.UInt64)')
  - [op_Addition()](#M-KineticEnergy-Ships-Mass-op_Addition-KineticEnergy-Ships-Mass,UnityEngine-Vector3- 'KineticEnergy.Ships.Mass.op_Addition(KineticEnergy.Ships.Mass,UnityEngine.Vector3)')
  - [op_Division()](#M-KineticEnergy-Ships-Mass-op_Division-KineticEnergy-Ships-Mass,System-UInt64- 'KineticEnergy.Ships.Mass.op_Division(KineticEnergy.Ships.Mass,System.UInt64)')
  - [op_Equality()](#M-KineticEnergy-Ships-Mass-op_Equality-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass- 'KineticEnergy.Ships.Mass.op_Equality(KineticEnergy.Ships.Mass,KineticEnergy.Ships.Mass)')
  - [op_GreaterThan()](#M-KineticEnergy-Ships-Mass-op_GreaterThan-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass- 'KineticEnergy.Ships.Mass.op_GreaterThan(KineticEnergy.Ships.Mass,KineticEnergy.Ships.Mass)')
  - [op_Inequality()](#M-KineticEnergy-Ships-Mass-op_Inequality-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass- 'KineticEnergy.Ships.Mass.op_Inequality(KineticEnergy.Ships.Mass,KineticEnergy.Ships.Mass)')
  - [op_LessThan()](#M-KineticEnergy-Ships-Mass-op_LessThan-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass- 'KineticEnergy.Ships.Mass.op_LessThan(KineticEnergy.Ships.Mass,KineticEnergy.Ships.Mass)')
  - [op_Multiply()](#M-KineticEnergy-Ships-Mass-op_Multiply-KineticEnergy-Ships-Mass,System-UInt64- 'KineticEnergy.Ships.Mass.op_Multiply(KineticEnergy.Ships.Mass,System.UInt64)')
  - [op_Subtraction()](#M-KineticEnergy-Ships-Mass-op_Subtraction-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass- 'KineticEnergy.Ships.Mass.op_Subtraction(KineticEnergy.Ships.Mass,KineticEnergy.Ships.Mass)')
  - [op_Subtraction()](#M-KineticEnergy-Ships-Mass-op_Subtraction-KineticEnergy-Ships-Mass,System-UInt64- 'KineticEnergy.Ships.Mass.op_Subtraction(KineticEnergy.Ships.Mass,System.UInt64)')
  - [op_Subtraction()](#M-KineticEnergy-Ships-Mass-op_Subtraction-KineticEnergy-Ships-Mass,UnityEngine-Vector3- 'KineticEnergy.Ships.Mass.op_Subtraction(KineticEnergy.Ships.Mass,UnityEngine.Vector3)')
- [OpaqueBlock](#T-KineticEnergy-Ships-Blocks-OpaqueBlock 'KineticEnergy.Ships.Blocks.OpaqueBlock')
- [PlayerController](#T-KineticEnergy-Entities-PlayerController 'KineticEnergy.Entities.PlayerController')
  - [gamemode](#F-KineticEnergy-Entities-PlayerController-gamemode 'KineticEnergy.Entities.PlayerController.gamemode')
  - [hotbar](#P-KineticEnergy-Entities-PlayerController-hotbar 'KineticEnergy.Entities.PlayerController.hotbar')
- [Range](#T-KineticEnergy-CodeTools-Math-Range 'KineticEnergy.CodeTools.Math.Range')
  - [half](#F-KineticEnergy-CodeTools-Math-Range-half 'KineticEnergy.CodeTools.Math.Range.half')
  - [infinite](#F-KineticEnergy-CodeTools-Math-Range-infinite 'KineticEnergy.CodeTools.Math.Range.infinite')
  - [ChangeByAmount(value)](#M-KineticEnergy-CodeTools-Math-Range-ChangeByAmount-System-Single- 'KineticEnergy.CodeTools.Math.Range.ChangeByAmount(System.Single)')
  - [ChangeByFactor(value)](#M-KineticEnergy-CodeTools-Math-Range-ChangeByFactor-System-Single- 'KineticEnergy.CodeTools.Math.Range.ChangeByFactor(System.Single)')
  - [Contains()](#M-KineticEnergy-CodeTools-Math-Range-Contains-System-Single- 'KineticEnergy.CodeTools.Math.Range.Contains(System.Single)')
  - [Contains(range)](#M-KineticEnergy-CodeTools-Math-Range-Contains-KineticEnergy-CodeTools-Math-Range- 'KineticEnergy.CodeTools.Math.Range.Contains(KineticEnergy.CodeTools.Math.Range)')
  - [EdgeDistance(value)](#M-KineticEnergy-CodeTools-Math-Range-EdgeDistance-System-Single- 'KineticEnergy.CodeTools.Math.Range.EdgeDistance(System.Single)')
  - [Place()](#M-KineticEnergy-CodeTools-Math-Range-Place-System-Single- 'KineticEnergy.CodeTools.Math.Range.Place(System.Single)')
  - [PlaceOutside()](#M-KineticEnergy-CodeTools-Math-Range-PlaceOutside-System-Single- 'KineticEnergy.CodeTools.Math.Range.PlaceOutside(System.Single)')
- [Range2D](#T-KineticEnergy-CodeTools-Math-Range2D 'KineticEnergy.CodeTools.Math.Range2D')
  - [#ctor(x,y)](#M-KineticEnergy-CodeTools-Math-Range2D-#ctor-KineticEnergy-CodeTools-Math-Range,KineticEnergy-CodeTools-Math-Range- 'KineticEnergy.CodeTools.Math.Range2D.#ctor(KineticEnergy.CodeTools.Math.Range,KineticEnergy.CodeTools.Math.Range)')
  - [#ctor()](#M-KineticEnergy-CodeTools-Math-Range2D-#ctor-System-Single,System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Range2D.#ctor(System.Single,System.Single,System.Single,System.Single)')
  - [half](#F-KineticEnergy-CodeTools-Math-Range2D-half 'KineticEnergy.CodeTools.Math.Range2D.half')
  - [infinite](#F-KineticEnergy-CodeTools-Math-Range2D-infinite 'KineticEnergy.CodeTools.Math.Range2D.infinite')
  - [x](#F-KineticEnergy-CodeTools-Math-Range2D-x 'KineticEnergy.CodeTools.Math.Range2D.x')
  - [y](#F-KineticEnergy-CodeTools-Math-Range2D-y 'KineticEnergy.CodeTools.Math.Range2D.y')
  - [Contains()](#M-KineticEnergy-CodeTools-Math-Range2D-Contains-UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Range2D.Contains(UnityEngine.Vector2)')
  - [Place()](#M-KineticEnergy-CodeTools-Math-Range2D-Place-UnityEngine-Vector2- 'KineticEnergy.CodeTools.Math.Range2D.Place(UnityEngine.Vector2)')
- [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample')
- [Sample](#T-KineticEnergy-Intangibles-Global-ColorPalette-Sample 'KineticEnergy.Intangibles.Global.ColorPalette.Sample')
  - [#ctor(prefabBlock,b\lockPrefab_preview\lockPrefab_preview)](#M-KineticEnergy-Intangibles-Global-BlockPalette-Sample-#ctor-UnityEngine-GameObject,UnityEngine-GameObject- 'KineticEnergy.Intangibles.Global.BlockPalette.Sample.#ctor(UnityEngine.GameObject,UnityEngine.GameObject)')
  - [prefabBlock](#F-KineticEnergy-Intangibles-Global-BlockPalette-Sample-prefabBlock 'KineticEnergy.Intangibles.Global.BlockPalette.Sample.prefabBlock')
  - [prefabBlock_preview](#F-KineticEnergy-Intangibles-Global-BlockPalette-Sample-prefabBlock_preview 'KineticEnergy.Intangibles.Global.BlockPalette.Sample.prefabBlock_preview')
- [ServerBehavioursManager](#T-KineticEnergy-Intangibles-Server-ServerBehavioursManager 'KineticEnergy.Intangibles.Server.ServerBehavioursManager')
  - [parents](#F-KineticEnergy-Intangibles-Server-ServerBehavioursManager-parents 'KineticEnergy.Intangibles.Server.ServerBehavioursManager.parents')
  - [GlobalBehaviours](#P-KineticEnergy-Intangibles-Server-ServerBehavioursManager-GlobalBehaviours 'KineticEnergy.Intangibles.Server.ServerBehavioursManager.GlobalBehaviours')
- [ServerManager](#T-KineticEnergy-Intangibles-Server-ServerManager 'KineticEnergy.Intangibles.Server.ServerManager')
- [Sign](#T-KineticEnergy-CodeTools-Math-Geometry-Sign 'KineticEnergy.CodeTools.Math.Geometry.Sign')
- [SimpleContainer](#T-KineticEnergy-Inventory-SimpleContainer 'KineticEnergy.Inventory.SimpleContainer')
  - [Contents](#P-KineticEnergy-Inventory-SimpleContainer-Contents 'KineticEnergy.Inventory.SimpleContainer.Contents')
  - [UsedCapacity](#P-KineticEnergy-Inventory-SimpleContainer-UsedCapacity 'KineticEnergy.Inventory.SimpleContainer.UsedCapacity')
  - [Add(inputItem)](#M-KineticEnergy-Inventory-SimpleContainer-Add-KineticEnergy-Inventory-Item- 'KineticEnergy.Inventory.SimpleContainer.Add(KineticEnergy.Inventory.Item)')
  - [SimpleAdd(inputItem)](#M-KineticEnergy-Inventory-SimpleContainer-SimpleAdd-System-Collections-Generic-List{KineticEnergy-Inventory-Item}@,KineticEnergy-Inventory-Item@,KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.SimpleContainer.SimpleAdd(System.Collections.Generic.List{KineticEnergy.Inventory.Item}@,KineticEnergy.Inventory.Item@,KineticEnergy.Inventory.Size)')
  - [SimpleTake(outputItem)](#M-KineticEnergy-Inventory-SimpleContainer-SimpleTake-System-Collections-Generic-List{KineticEnergy-Inventory-Item}@,KineticEnergy-Inventory-Item@,KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.SimpleContainer.SimpleTake(System.Collections.Generic.List{KineticEnergy.Inventory.Item}@,KineticEnergy.Inventory.Item@,KineticEnergy.Inventory.Size)')
  - [Take(outputItem)](#M-KineticEnergy-Inventory-SimpleContainer-Take-KineticEnergy-Inventory-Item- 'KineticEnergy.Inventory.SimpleContainer.Take(KineticEnergy.Inventory.Item)')
- [SimpleItem](#T-KineticEnergy-Inventory-SimpleItem 'KineticEnergy.Inventory.SimpleItem')
  - [ItemSize](#P-KineticEnergy-Inventory-SimpleItem-ItemSize 'KineticEnergy.Inventory.SimpleItem.ItemSize')
  - [StackSize](#P-KineticEnergy-Inventory-SimpleItem-StackSize 'KineticEnergy.Inventory.SimpleItem.StackSize')
  - [MaxToAdd(maximumSize)](#M-KineticEnergy-Inventory-SimpleItem-MaxToAdd-KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.SimpleItem.MaxToAdd(KineticEnergy.Inventory.Size)')
  - [MaxToSub(maximumSize)](#M-KineticEnergy-Inventory-SimpleItem-MaxToSub-KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.SimpleItem.MaxToSub(KineticEnergy.Inventory.Size)')
- [Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size')
  - [value](#F-KineticEnergy-Inventory-Size-value 'KineticEnergy.Inventory.Size.value')
  - [op_Addition()](#M-KineticEnergy-Inventory-Size-op_Addition-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.Size.op_Addition(KineticEnergy.Inventory.Size,KineticEnergy.Inventory.Size)')
  - [op_Division()](#M-KineticEnergy-Inventory-Size-op_Division-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.Size.op_Division(KineticEnergy.Inventory.Size,KineticEnergy.Inventory.Size)')
  - [op_Division()](#M-KineticEnergy-Inventory-Size-op_Division-KineticEnergy-Inventory-Size,System-UInt64- 'KineticEnergy.Inventory.Size.op_Division(KineticEnergy.Inventory.Size,System.UInt64)')
  - [op_Multiply()](#M-KineticEnergy-Inventory-Size-op_Multiply-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.Size.op_Multiply(KineticEnergy.Inventory.Size,KineticEnergy.Inventory.Size)')
  - [op_Multiply()](#M-KineticEnergy-Inventory-Size-op_Multiply-KineticEnergy-Inventory-Size,System-UInt64- 'KineticEnergy.Inventory.Size.op_Multiply(KineticEnergy.Inventory.Size,System.UInt64)')
  - [op_Subtraction()](#M-KineticEnergy-Inventory-Size-op_Subtraction-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size- 'KineticEnergy.Inventory.Size.op_Subtraction(KineticEnergy.Inventory.Size,KineticEnergy.Inventory.Size)')
- [TranslucentBlock](#T-KineticEnergy-Ships-Blocks-TranslucentBlock 'KineticEnergy.Ships.Blocks.TranslucentBlock')
- [TransparentBlock](#T-KineticEnergy-Ships-Blocks-TransparentBlock 'KineticEnergy.Ships.Blocks.TransparentBlock')
- [Triangle](#T-KineticEnergy-CodeTools-Math-Geometry-Triangle 'KineticEnergy.CodeTools.Math.Geometry.Triangle')
  - [#ctor(_a,_b,_c)](#M-KineticEnergy-CodeTools-Math-Geometry-Triangle-#ctor-System-Single,System-Single,System-Single- 'KineticEnergy.CodeTools.Math.Geometry.Triangle.#ctor(System.Single,System.Single,System.Single)')

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Angle'></a>
## Angle `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Ties together a value and a unit for an angle.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Angle-#ctor-System-Single,KineticEnergy-CodeTools-Math-Geometry-AngleType-'></a>
### #ctor(value,type) `constructor`

##### Summary

Creates a new angle

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The value of the angle in the units given. |
| type | [KineticEnergy.CodeTools.Math.Geometry.AngleType](#T-KineticEnergy-CodeTools-Math-Geometry-AngleType 'KineticEnergy.CodeTools.Math.Geometry.AngleType') | The units of the angle. |

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Angle-radDown'></a>
### radDown `constants`

##### Summary

2/3 PI

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Angle-radLeft'></a>
### radLeft `constants`

##### Summary

1 PI

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Angle-radRight'></a>
### radRight `constants`

##### Summary

2 PI

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Angle-radUp'></a>
### radUp `constants`

##### Summary

1/2 PI

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Angle-type'></a>
### type `constants`

##### Summary

The units of the angle.

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Angle-value'></a>
### value `constants`

##### Summary

The value of the angle with unknown units.

<a name='P-KineticEnergy-CodeTools-Math-Geometry-Angle-Degrees'></a>
### Degrees `property`

##### Summary

Use this only if you don't know the units for sure. If you do, then use 'Angle.value'.

<a name='P-KineticEnergy-CodeTools-Math-Geometry-Angle-Radians'></a>
### Radians `property`

##### Summary

Use this only if you don't know the units for sure. If you do, then use 'Angle.value'.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Angle-ConvertType-KineticEnergy-CodeTools-Math-Geometry-AngleType-'></a>
### ConvertType(type) `method`

##### Summary

Changes the units and applies the conversion.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [KineticEnergy.CodeTools.Math.Geometry.AngleType](#T-KineticEnergy-CodeTools-Math-Geometry-AngleType 'KineticEnergy.CodeTools.Math.Geometry.AngleType') | The new units of the angle. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Explicit-KineticEnergy-CodeTools-Math-Geometry-Angle-~System-Int32'></a>
### op_Explicit() `method`

##### Summary

Casts an angle into an int with 'Angle.degrees'.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Implicit-System-Single-~KineticEnergy-CodeTools-Math-Geometry-Angle'></a>
### op_Implicit() `method`

##### Summary

Casts a float into a new Angle with units of radians.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Implicit-KineticEnergy-CodeTools-Math-Geometry-Angle-~System-Single'></a>
### op_Implicit() `method`

##### Summary

Casts an angle into a float with 'Angle.radians'.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Angle-op_Implicit-System-Int32-~KineticEnergy-CodeTools-Math-Geometry-Angle'></a>
### op_Implicit() `method`

##### Summary

Casts an int into a new Angle with units of degrees.

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-CodeTools-Math-Geometry-AngleType'></a>
## AngleType `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Enumerator for radians or degrees.

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Area'></a>
## Area `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Defines an area from boxes.

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Axis'></a>
## Axis `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

x : y : : horizontal : vertical

<a name='T-KineticEnergy-Ships-Blocks-Block'></a>
## Block `type`

##### Namespace

KineticEnergy.Ships.Blocks

##### Summary

Base class for all blocks.

<a name='F-KineticEnergy-Ships-Blocks-Block-grid'></a>
### grid `constants`

##### Summary

The [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') associated with this block.

<a name='F-KineticEnergy-Ships-Blocks-Block-insidePoints'></a>
### insidePoints `constants`

##### Summary

All of the cells in the grid that make up this block (from a local point of view). Null until 'GetLocalInformation()' or 'UpdateInsidePieces()' is called.

<a name='F-KineticEnergy-Ships-Blocks-Block-neighboringPoints'></a>
### neighboringPoints `constants`

##### Summary

All of the grid cell locations (from a local reference) that touch this block.
Null until [GetDimensionInformation](#M-KineticEnergy-Ships-Blocks-Block-GetDimensionInformation 'KineticEnergy.Ships.Blocks.Block.GetDimensionInformation') or [UpdateNeighboringPoints](#M-KineticEnergy-Ships-Blocks-Block-UpdateNeighboringPoints 'KineticEnergy.Ships.Blocks.Block.UpdateNeighboringPoints') is called.

<a name='P-KineticEnergy-Ships-Blocks-Block-Dimensions'></a>
### Dimensions `property`

##### Summary

The dimentions of this block. Runs [GetDimensionInformation](#M-KineticEnergy-Ships-Blocks-Block-GetDimensionInformation 'KineticEnergy.Ships.Blocks.Block.GetDimensionInformation') when set. Nothing becides "return" is done on get.

<a name='P-KineticEnergy-Ships-Blocks-Block-GridCorner'></a>
### GridCorner `property`

##### Summary

Shortcut for "` - `".

<a name='P-KineticEnergy-Ships-Blocks-Block-Mass'></a>
### Mass `property`

##### Summary

The [Mass](#T-KineticEnergy-Ships-Mass 'KineticEnergy.Ships.Mass') of this block. Updates the [grid](#F-KineticEnergy-Ships-Blocks-Block-grid 'KineticEnergy.Ships.Blocks.Block.grid')'s [Mass](#P-KineticEnergy-Ships-Blocks-Block-Mass 'KineticEnergy.Ships.Blocks.Block.Mass') when set. Nothing becides "return" is done on get.

<a name='P-KineticEnergy-Ships-Blocks-Block-Name'></a>
### Name `property`

##### Summary

Shortcut for "`gameObject.name`".

<a name='P-KineticEnergy-Ships-Blocks-Block-SurfaceArea'></a>
### SurfaceArea `property`

##### Summary

Calculates the surface area.

<a name='P-KineticEnergy-Ships-Blocks-Block-arrayPosition'></a>
### arrayPosition `property`

##### Summary

The position of this block in its [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') array space.

##### See Also

- [KineticEnergy.Ships.Blocks.Block.grid](#F-KineticEnergy-Ships-Blocks-Block-grid 'KineticEnergy.Ships.Blocks.Block.grid')
- [KineticEnergy.Ships.BlockGrid.offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset')

<a name='P-KineticEnergy-Ships-Blocks-Block-gridPosition'></a>
### gridPosition `property`

##### Summary

The position of this block in its [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') grid space.

##### See Also

- [KineticEnergy.Ships.Blocks.Block.grid](#F-KineticEnergy-Ships-Blocks-Block-grid 'KineticEnergy.Ships.Blocks.Block.grid')
- [KineticEnergy.Ships.BlockGrid.offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset')

<a name='M-KineticEnergy-Ships-Blocks-Block-GetDimensionInformation'></a>
### GetDimensionInformation() `method`

##### Summary

Sets important variables inside [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block'), which would otherwise be innacurate or null.

##### Parameters

This method has no parameters.

##### Remarks

Should be called when this object is created or when [m_dimentions](#F-KineticEnergy-Ships-Blocks-Block-m_dimentions 'KineticEnergy.Ships.Blocks.Block.m_dimentions') (not [Dimensions](#P-KineticEnergy-Ships-Blocks-Block-Dimensions 'KineticEnergy.Ships.Blocks.Block.Dimensions')) is changed.

<a name='M-KineticEnergy-Ships-Blocks-Block-RelativeGridToGrid-UnityEngine-Vector3-'></a>
### RelativeGridToGrid(relativeGridPosition) `method`

##### Summary

A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.

##### Returns

Returns a grid position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| relativeGridPosition | [UnityEngine.Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') | The relative grid position to translate. |

<a name='M-KineticEnergy-Ships-Blocks-Block-RelativeGridToGrid-UnityEngine-Vector3,UnityEngine-Quaternion-'></a>
### RelativeGridToGrid(relativeGridPosition,inverseLocalRotation) `method`

##### Summary

A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.

##### Returns

Returns a grid position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| relativeGridPosition | [UnityEngine.Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') | The relative grid position to translate. |
| inverseLocalRotation | [UnityEngine.Quaternion](#T-UnityEngine-Quaternion 'UnityEngine.Quaternion') | The [Inverse](#M-UnityEngine-Quaternion-Inverse-UnityEngine-Quaternion- 'UnityEngine.Quaternion.Inverse(UnityEngine.Quaternion)') of the [localRotation](#P-UnityEngine-Transform-localRotation 'UnityEngine.Transform.localRotation'). |

<a name='M-KineticEnergy-Ships-Blocks-Block-SetGrid-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int-'></a>
### SetGrid(grid,gridPosition) `method`

##### Summary

Sets transform position and parent, amung some references inside Block.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| grid | [KineticEnergy.Ships.BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') | The [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') to set this object's [grid](#F-KineticEnergy-Ships-Blocks-Block-grid 'KineticEnergy.Ships.Blocks.Block.grid') as. |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | The position in the [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') this [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') will be moved to. |

<a name='M-KineticEnergy-Ships-Blocks-Block-UpdateInsidePoints'></a>
### UpdateInsidePoints() `method`

##### Summary

Assigns to 'insidePieces'.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Blocks-Block-UpdateNeighboringPoints'></a>
### UpdateNeighboringPoints() `method`

##### Summary

Assigns to [neighboringPoints](#F-KineticEnergy-Ships-Blocks-Block-neighboringPoints 'KineticEnergy.Ships.Blocks.Block.neighboringPoints').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Blocks-Block-WhichFacesShown'></a>
### WhichFacesShown() `method`

##### Summary

Checks all faces and returns an [Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') that represents a set of booleans of which faces are covered.

##### Returns

Returns a byte that represents a set of booleans for which faces are shown. Starting from the right-most binary digit: 1-right, 2-left, 3-top, 4-bottom, 5-front, 6-back

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-Ships-BlockGrid'></a>
## BlockGrid `type`

##### Namespace

KineticEnergy.Ships

##### Summary

Represents everything about a ship-thingy.

##### Remarks

Extends [IEnumerable](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.IEnumerable 'System.Collections.IEnumerable') to iterate through [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array').

<a name='F-KineticEnergy-Ships-BlockGrid-array'></a>
### array `constants`

##### Summary

A 3-dimentional array that contains all [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') objects on the ship.

<a name='F-KineticEnergy-Ships-BlockGrid-neighborVectors'></a>
### neighborVectors `constants`

##### Summary

A set of vectors that define all neighbors that are next to, and relative to, a 1x1x1 block.

<a name='F-KineticEnergy-Ships-BlockGrid-offset'></a>
### offset `constants`

##### Summary

array space --> grid space



Any array position minus [offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset') is the position in the grid.
Any grid position plus [offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset') is the position in the array.

##### Remarks

Grid-space is essentially the local position to the parent [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid')'s [GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject').
Meanwhile, array-space, since its index cannot be negative, needs to have its origin at the "most-negative corner" of grid-space.

<a name='P-KineticEnergy-Ships-BlockGrid-Item-UnityEngine-Vector3Int-'></a>
### Item `property`

##### Summary

A simple "get" function for an array-space point. To get the grid space, add [offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset') to the input.



`return [index.x, index.y, index.z];`

##### Returns

Returns the [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') that overlaps at the given point of the grid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | The index to look at. |

##### Remarks

Remember that this is array space.

##### See Also

- [KineticEnergy.Ships.BlockGrid.Item](#P-KineticEnergy-Ships-BlockGrid-Item-System-Int32,System-Int32,System-Int32- 'KineticEnergy.Ships.BlockGrid.Item(System.Int32,System.Int32,System.Int32)')

<a name='P-KineticEnergy-Ships-BlockGrid-Item-System-Int32,System-Int32,System-Int32-'></a>
### Item `property`

##### Summary

A simple "get" function for an array-space point. To get the grid space, add [offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset') to the input.



`return [x, y, z];`

##### Returns

Returns the [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') that overlaps at the given point of the grid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The X index to look at. |
| y | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The Y index to look at. |
| z | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The Z index to look at. |

##### See Also

- [KineticEnergy.Ships.BlockGrid.Item](#P-KineticEnergy-Ships-BlockGrid-Item-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.Item(UnityEngine.Vector3Int)')

<a name='P-KineticEnergy-Ships-BlockGrid-Mass'></a>
### Mass `property`

##### Summary

The sum of every [Mass](#P-KineticEnergy-Ships-Blocks-Block-Mass 'KineticEnergy.Ships.Blocks.Block.Mass'). Updates [mass](#P-UnityEngine-Rigidbody-mass 'UnityEngine.Rigidbody.mass') when set (internal). Nothing becides "return" is done on get.

##### Remarks

This value is never changed by the [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') script.
Instead, the [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') script updates it on [SetGrid](#M-KineticEnergy-Ships-Blocks-Block-SetGrid-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int- 'KineticEnergy.Ships.Blocks.Block.SetGrid(KineticEnergy.Ships.BlockGrid,UnityEngine.Vector3Int)') and when set-ing [Mass](#P-KineticEnergy-Ships-Blocks-Block-Mass 'KineticEnergy.Ships.Blocks.Block.Mass').

<a name='P-KineticEnergy-Ships-BlockGrid-Rigidbody'></a>
### Rigidbody `property`

##### Summary

[rigidbody](#P-KineticEnergy-Ships-BlockGrid-rigidbody 'KineticEnergy.Ships.BlockGrid.rigidbody') but with a Unity.Object null-check.

<a name='P-KineticEnergy-Ships-BlockGrid-Size'></a>
### Size `property`

##### Summary

The size/dimensions of the 3-dimensional array, [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array').
Calls [GetLength](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Array.GetLength 'System.Array.GetLength(System.Int32)') three times.

<a name='P-KineticEnergy-Ships-BlockGrid-rigidbody'></a>
### rigidbody `property`

##### Summary

The [Rigidbody](#T-UnityEngine-Rigidbody 'UnityEngine.Rigidbody') of this [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid').

<a name='M-KineticEnergy-Ships-BlockGrid-AlignWorldPoint-UnityEngine-Vector3-'></a>
### AlignWorldPoint(worldPoint) `method`

##### Summary

Aligns the given world point to the grid.

##### Returns

Returns a [Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') that represents a grid point in world space.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| worldPoint | [UnityEngine.Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') | Some point in the world. |

<a name='M-KineticEnergy-Ships-BlockGrid-ArrayPointIsInsideArray-UnityEngine-Vector3Int-'></a>
### ArrayPointIsInsideArray(arrayPosition) `method`

##### Summary

Checks if the given point in array space is within the boundries of the array.

##### Returns

Returns true if 'x', 'y', and 'z' are greater than -1 and less than the [GetLength](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Array.GetLength 'System.Array.GetLength(System.Int32)') [0 through 2].

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| arrayPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Some point in array space. |

<a name='M-KineticEnergy-Ships-BlockGrid-CanPlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int-'></a>
### CanPlaceBlock(block,gridPosition) `method`

##### Summary

Tests if a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') can fit at the given position.

##### Returns

Returns true if [HasBlockAt](#M-KineticEnergy-Ships-BlockGrid-HasBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.HasBlockAt(UnityEngine.Vector3Int)') is true for every grid position inside the block.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| block | [KineticEnergy.Ships.Blocks.Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') | The would-be [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block'). |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Position, in grid space, of the would-be [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block'). |

<a name='M-KineticEnergy-Ships-BlockGrid-ChangeArrayDimensions-UnityEngine-Vector3Int,UnityEngine-Vector3Int-'></a>
### ChangeArrayDimensions(amountPos,amountNeg) `method`

##### Summary

Expands the [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') by the given amounts.
All values of "amountPos" should be zero or positive and all values of amountNeg should be zero or negative.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| amountPos | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Amount to increase the size of [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') in each positive axis direction. |
| amountNeg | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Amount to increase the size of [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') in each negative axis direction. |

<a name='M-KineticEnergy-Ships-BlockGrid-ExpandArrayDimensions-UnityEngine-Vector3Int-'></a>
### ExpandArrayDimensions(amount) `method`

##### Summary

Expands the [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') by the given amount. The sign of the inputs are the direction to expand the grid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| amount | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Amount to increase the size of [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') in each axis direction. |

##### Remarks

Since an [Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') can only have one sign, [ExpandArrayDimensions](#M-KineticEnergy-Ships-BlockGrid-ExpandArrayDimensions-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.ExpandArrayDimensions(UnityEngine.Vector3Int)') cannot expand the grid in, for example, +x and -x at the same time.

##### See Also

- [KineticEnergy.Ships.BlockGrid.ChangeArrayDimensions](#M-KineticEnergy-Ships-BlockGrid-ChangeArrayDimensions-UnityEngine-Vector3Int,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.ChangeArrayDimensions(UnityEngine.Vector3Int,UnityEngine.Vector3Int)')

<a name='M-KineticEnergy-Ships-BlockGrid-GetBlockAt-UnityEngine-Vector3Int-'></a>
### GetBlockAt(gridPosition) `method`

##### Summary

Finds the block at the given position in the grid.

##### Returns

Returns the appropirate block if there is one, otherwise returns null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Position in the grid to look at. |

##### Remarks

Subject to [IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors when [GridPointIsInsideArray](#M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') is false.

<a name='M-KineticEnergy-Ships-BlockGrid-GetNeighborsAt-UnityEngine-Vector3Int-'></a>
### GetNeighborsAt(gridPosition) `method`

##### Summary

Finds the neighboring blocks that are next to the given grid position.

##### Returns

Returns a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') array that may contain null values and reapeats.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Position in the grid to look at. |

<a name='M-KineticEnergy-Ships-BlockGrid-GetUniqueNeighborsAt-UnityEngine-Vector3Int-'></a>
### GetUniqueNeighborsAt(position) `method`

##### Summary

Has the same functionallity as [GetNeighborsAt](#M-KineticEnergy-Ships-BlockGrid-GetNeighborsAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetNeighborsAt(UnityEngine.Vector3Int)'), but does not contain repeats or null values.

##### Returns

Returns a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') array.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| position | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Position in the grid to look at. |

<a name='M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int-'></a>
### GridPointIsInsideArray(gridPosition) `method`

##### Summary

Finds if the given point in grid space is within the bounries of the array.

##### Returns

Adds [offset](#F-KineticEnergy-Ships-BlockGrid-offset 'KineticEnergy.Ships.BlockGrid.offset'), then returns true if 'x', 'y', and 'z' are greater than -1 and less than the [GetLength](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Array.GetLength 'System.Array.GetLength(System.Int32)') [0 through 2].

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Some point in grid space. |

<a name='M-KineticEnergy-Ships-BlockGrid-GridPointToLocal-UnityEngine-Vector3Int-'></a>
### GridPointToLocal(gridPoint) `method`

##### Summary

Converts a point on this [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid')'s grid space into local space.

##### Returns

Returns a [Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') that represents a point in local space.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPoint | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Some coordinate on the grid. |

<a name='M-KineticEnergy-Ships-BlockGrid-GridPointToWorld-UnityEngine-Vector3Int-'></a>
### GridPointToWorld(gridPoint) `method`

##### Summary

Converts a point on this [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid')'s grid space into world space.

##### Returns

Returns a [Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') that represents a point in world space.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPoint | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Some coordinate on the grid. |

<a name='M-KineticEnergy-Ships-BlockGrid-HasBlockAt-UnityEngine-Vector3Int-'></a>
### HasBlockAt(gridPosition) `method`

##### Summary

Checks if there is any block at the given position in the grid.

##### Returns

Returns true if there is a block at the given position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Position in the grid to check. |

##### Remarks

Subject to [IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors when [GridPointIsInsideArray](#M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') is false.

<a name='M-KineticEnergy-Ships-BlockGrid-LocalPointToGrid-UnityEngine-Vector3-'></a>
### LocalPointToGrid(localPoint) `method`

##### Summary

Converts a local point into grid space for this [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid').

##### Returns

Returns a [Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') that represents a point in grid space.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| localPoint | [UnityEngine.Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') | Some point local to this object's transform. |

<a name='M-KineticEnergy-Ships-BlockGrid-PlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int-'></a>
### PlaceBlock(block,gridPosition) `method`

##### Summary

Places a block at the given position in the grid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| block | [KineticEnergy.Ships.Blocks.Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') | [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') to place. |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Grid position to place the block at. |

##### Remarks

Subject to clipping if [CanPlaceBlock](#M-KineticEnergy-Ships-BlockGrid-CanPlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.CanPlaceBlock(KineticEnergy.Ships.Blocks.Block,UnityEngine.Vector3Int)') is false.

<a name='M-KineticEnergy-Ships-BlockGrid-PlaceEnablingBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int-'></a>
### PlaceEnablingBlock(block,gridPosition) `method`

##### Summary

When a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') is disabled, it is removed from the grid. When it is enabled it is re-added through this function.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| block | [KineticEnergy.Ships.Blocks.Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') | The block that is being enabled. |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | The position in the grid that the block is in. |

<a name='M-KineticEnergy-Ships-BlockGrid-PlaceNewBlock-UnityEngine-GameObject,UnityEngine-Vector3Int,UnityEngine-Quaternion-'></a>
### PlaceNewBlock(prefab,gridPosition) `method`

##### Summary

Instantiates a prefab, defines important values such as transform data and name then calls [GetDimensionInformation](#M-KineticEnergy-Ships-Blocks-Block-GetDimensionInformation 'KineticEnergy.Ships.Blocks.Block.GetDimensionInformation'),
then places it on the grid at the given position with [PlaceBlock](#M-KineticEnergy-Ships-BlockGrid-PlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.PlaceBlock(KineticEnergy.Ships.Blocks.Block,UnityEngine.Vector3Int)').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| prefab | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') | Prefab of the "blank" GameObject. |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Grid position to place the block at. |

##### Remarks

Subject to clipping if [CanPlaceBlock](#M-KineticEnergy-Ships-BlockGrid-CanPlaceBlock-KineticEnergy-Ships-Blocks-Block,UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.CanPlaceBlock(KineticEnergy.Ships.Blocks.Block,UnityEngine.Vector3Int)') is false.

<a name='M-KineticEnergy-Ships-BlockGrid-System#Collections#IEnumerable#GetEnumerator'></a>
### System#Collections#IEnumerable#GetEnumerator() `method`

##### Summary

Returns an object to be iterated through.

##### Returns

Returns an [IEnumerator](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.IEnumerator 'System.Collections.IEnumerator').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-BlockGrid-TryGetBlockAt-UnityEngine-Vector3Int-'></a>
### TryGetBlockAt(gridPosition) `method`

##### Summary

Same functionality as [GetBlockAt](#M-KineticEnergy-Ships-BlockGrid-GetBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)'), but is not subject to [IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors.

##### Returns

Returns null if [GridPointIsInsideArray](#M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') fails, otherwise returns [GetBlockAt](#M-KineticEnergy-Ships-BlockGrid-GetBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | The position in the grid to check. |

<a name='M-KineticEnergy-Ships-BlockGrid-TryGetBlockAt-UnityEngine-Vector3-'></a>
### TryGetBlockAt(localPosition) `method`

##### Summary

Uses [LocalPointToGrid](#M-KineticEnergy-Ships-BlockGrid-LocalPointToGrid-UnityEngine-Vector3- 'KineticEnergy.Ships.BlockGrid.LocalPointToGrid(UnityEngine.Vector3)') to call [TryGetBlockAt](#M-KineticEnergy-Ships-BlockGrid-TryGetBlockAt-UnityEngine-Vector3- 'KineticEnergy.Ships.BlockGrid.TryGetBlockAt(UnityEngine.Vector3)').

##### Returns

Returns null if [GridPointIsInsideArray](#M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') fails, otherwise returns [GetBlockAt](#M-KineticEnergy-Ships-BlockGrid-GetBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GetBlockAt(UnityEngine.Vector3Int)').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| localPosition | [UnityEngine.Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') | The position in the grid's transform to check. |

<a name='M-KineticEnergy-Ships-BlockGrid-TryHasBlockAt-UnityEngine-Vector3Int-'></a>
### TryHasBlockAt(gridPosition) `method`

##### Summary

Same functionality as [HasBlockAt](#M-KineticEnergy-Ships-BlockGrid-HasBlockAt-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.HasBlockAt(UnityEngine.Vector3Int)'), but runs [GridPointIsInsideArray](#M-KineticEnergy-Ships-BlockGrid-GridPointIsInsideArray-UnityEngine-Vector3Int- 'KineticEnergy.Ships.BlockGrid.GridPointIsInsideArray(UnityEngine.Vector3Int)') first so it is not subject to [IndexOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IndexOutOfRangeException 'System.IndexOutOfRangeException') errors.

##### Returns

Returns false if 'InsideGrid(position)' fails, otherwise returns 'HasBlock(position)'.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gridPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | Position in the grid to check. |

<a name='M-KineticEnergy-Ships-BlockGrid-WorldPointToGrid-UnityEngine-Vector3-'></a>
### WorldPointToGrid(worldPoint) `method`

##### Summary

Converts a world point into grid space for this [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid').

##### Returns

Returns a [Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') that represents a point in grid space.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| worldPoint | [UnityEngine.Vector3](#T-UnityEngine-Vector3 'UnityEngine.Vector3') | Some point in the world. |

<a name='T-KineticEnergy-Ships-BlockGridEditor'></a>
## BlockGridEditor `type`

##### Namespace

KineticEnergy.Ships

<a name='F-KineticEnergy-Ships-BlockGridEditor-distance'></a>
### distance `constants`

##### Summary

The maximum distance from the camera the should be placed at.

<a name='F-KineticEnergy-Ships-BlockGridEditor-hitError'></a>
### hitError `constants`

##### Summary

Expected error of raycasting into the grid.

<a name='F-KineticEnergy-Ships-BlockGridEditor-preview'></a>
### preview `constants`

##### Summary

Current [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview') that is being moved around.

<a name='F-KineticEnergy-Ships-BlockGridEditor-rotation'></a>
### rotation `constants`

##### Summary

The rotation of this block relative to the grid.

<a name='T-KineticEnergy-Ships-BlockGridSceneEditor'></a>
## BlockGridSceneEditor `type`

##### Namespace

KineticEnergy.Ships

##### Summary

A modified [BlockGridEditor](#T-KineticEnergy-Ships-BlockGridEditor 'KineticEnergy.Ships.BlockGridEditor') for use in Unity's scene view.

##### Remarks

Currently not tested for multiple scene views.

<a name='F-KineticEnergy-Ships-BlockGridSceneEditor-distance'></a>
### distance `constants`

##### Summary

The maximum distance from the camera the should be placed at.

<a name='F-KineticEnergy-Ships-BlockGridSceneEditor-hitError'></a>
### hitError `constants`

##### Summary

Expected error of raycasting into the grid.

<a name='F-KineticEnergy-Ships-BlockGridSceneEditor-selectedBlock'></a>
### selectedBlock `constants`

##### Summary

Current [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview') that is being moved around.

<a name='T-KineticEnergy-Ships-BlockGrid-BlockOverlapException'></a>
## BlockOverlapException `type`

##### Namespace

KineticEnergy.Ships.BlockGrid

##### Summary

Exception for when a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') was about to be / was overlapped by another [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') in [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array').

<a name='M-KineticEnergy-Ships-BlockGrid-BlockOverlapException-#ctor-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int,KineticEnergy-Ships-Blocks-Block,KineticEnergy-Ships-Blocks-Block,System-Boolean,System-Boolean,System-Boolean-'></a>
### #ctor(grid,arrayPosition,native,intruder,stoppedOverwrite,stoppedCompletely,tryToFix) `constructor`

##### Summary

Creates an error message:



"The Block "[native.gameObject.name]" in the "[grid.gameObject.name]" BlockGrid was trying to overlap the Block "[native.gameObject.name]"
at the grid point [arrayPosition + grid.offset] / array point [arrayPosition]. Check for 'ghost' blocks."



stoppedCompletely ? "This was interrupted before any data in the BlockGrid.array was changed."



: stoppedOverwrite ? "Although existing data was not changed, data on empty points of the grid may have been."



tryToFix ? "An attempt at a fix has been made."

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| grid | [KineticEnergy.Ships.BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') | The grid where this overlap happened. |
| arrayPosition | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | The position in the array where this overlap happened. |
| native | [KineticEnergy.Ships.Blocks.Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') | The block that was/would've been overlapped. |
| intruder | [KineticEnergy.Ships.Blocks.Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') | The block that (would've) been the one overlapping. |
| stoppedOverwrite | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Was this stopped before data in [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') was changed? |
| stoppedCompletely | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Was this stopped before ANY data in [array](#F-KineticEnergy-Ships-BlockGrid-array 'KineticEnergy.Ships.BlockGrid.array') was changed? |
| tryToFix | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | O(n) operation, where n = volume. Removes all refrences to the block on the affected grid, then Destroys the block. |

<a name='T-KineticEnergy-Intangibles-Global-BlockPalette'></a>
## BlockPalette `type`

##### Namespace

KineticEnergy.Intangibles.Global

##### Summary

An object that contains a list of [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample')s.

##### Remarks

Extends [IEnumerable](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.IEnumerable 'System.Collections.IEnumerable').

##### See Also

- [KineticEnergy.Intangibles.Global.GlobalPaletteManager](#T-KineticEnergy-Intangibles-Global-GlobalPaletteManager 'KineticEnergy.Intangibles.Global.GlobalPaletteManager')
- [KineticEnergy.Intangibles.Global.BlockPalette.Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample')

<a name='F-KineticEnergy-Intangibles-Global-BlockPalette-samples'></a>
### samples `constants`

##### Summary

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') of [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample')s.

<a name='P-KineticEnergy-Intangibles-Global-BlockPalette-Count'></a>
### Count `property`

##### Summary

The [Count](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1.Count 'System.Collections.Generic.List`1.Count') for [samples](#F-KineticEnergy-Intangibles-Global-BlockPalette-samples 'KineticEnergy.Intangibles.Global.BlockPalette.samples').

<a name='P-KineticEnergy-Intangibles-Global-BlockPalette-Item-System-Int32-'></a>
### Item `property`

##### Summary

A simple "get" function for [samples](#F-KineticEnergy-Intangibles-Global-BlockPalette-samples 'KineticEnergy.Intangibles.Global.BlockPalette.samples').



`return [index];`

##### Returns

Returns the [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample') at the given index.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index to look at. |

<a name='M-KineticEnergy-Intangibles-Global-BlockPalette-Add-KineticEnergy-Intangibles-Global-BlockPalette-Sample-'></a>
### Add(blockSample) `method`

##### Summary

Adds a [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample') to [samples](#F-KineticEnergy-Intangibles-Global-BlockPalette-samples 'KineticEnergy.Intangibles.Global.BlockPalette.samples').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| blockSample | [KineticEnergy.Intangibles.Global.BlockPalette.Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample') | The [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample') to add to [samples](#F-KineticEnergy-Intangibles-Global-BlockPalette-samples 'KineticEnergy.Intangibles.Global.BlockPalette.samples'). |

<a name='T-KineticEnergy-Ships-Blocks-BlockPreview'></a>
## BlockPreview `type`

##### Namespace

KineticEnergy.Ships.Blocks

##### Summary

Class for selecting then placing blocks on a [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid').

##### See Also

- [KineticEnergy.Intangibles.GlobalPaletteManager](#!-KineticEnergy-Intangibles-GlobalPaletteManager 'KineticEnergy.Intangibles.GlobalPaletteManager')

<a name='F-KineticEnergy-Ships-Blocks-BlockPreview-blockGridPrefab'></a>
### blockGridPrefab `constants`

##### Summary

A refrence to a blank [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid')[GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') to instantiate when [PlaceNewGrid](#M-KineticEnergy-Ships-Blocks-BlockPreview-PlaceNewGrid 'KineticEnergy.Ships.Blocks.BlockPreview.PlaceNewGrid') is called.

<a name='F-KineticEnergy-Ships-Blocks-BlockPreview-realBlockPrefab'></a>
### realBlockPrefab `constants`

##### Summary

The "real" [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') counterpart to this [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview').

<a name='M-KineticEnergy-Ships-Blocks-BlockPreview-Place-KineticEnergy-Ships-BlockGrid,UnityEngine-Vector3Int-'></a>
### Place(grid,position) `method`

##### Summary

Places the "real" [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') of this [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview') into the specified [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') at the specified location.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| grid | [KineticEnergy.Ships.BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') | The [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') to place the [realBlockPrefab](#F-KineticEnergy-Ships-Blocks-BlockPreview-realBlockPrefab 'KineticEnergy.Ships.Blocks.BlockPreview.realBlockPrefab') at. |
| position | [UnityEngine.Vector3Int](#T-UnityEngine-Vector3Int 'UnityEngine.Vector3Int') | The position in grid to place the [realBlockPrefab](#F-KineticEnergy-Ships-Blocks-BlockPreview-realBlockPrefab 'KineticEnergy.Ships.Blocks.BlockPreview.realBlockPrefab') at. |

<a name='M-KineticEnergy-Ships-Blocks-BlockPreview-PlaceNewGrid'></a>
### PlaceNewGrid() `method`

##### Summary

Places the "real" [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') of this [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview') into a new [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') at it's current position.

##### Returns

Returns the [BlockGrid](#T-KineticEnergy-Ships-BlockGrid 'KineticEnergy.Ships.BlockGrid') that was just created and contains the newly instantiated [realBlockPrefab](#F-KineticEnergy-Ships-Blocks-BlockPreview-realBlockPrefab 'KineticEnergy.Ships.Blocks.BlockPreview.realBlockPrefab').

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Box'></a>
## Box `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Stores data and methods for a box with a position.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Box-#ctor-UnityEngine-Vector2,UnityEngine-Vector2-'></a>
### #ctor(corner1,corner2) `constructor`

##### Summary

Creates a new box given two corners (any order).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| corner1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | A corner of the box. |
| corner2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | A corner of the box. |

<a name='P-KineticEnergy-CodeTools-Math-Geometry-Box-Points'></a>
### Points `property`

##### Summary

Clockwise, starting from bottom-left.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Box-Contains-UnityEngine-Vector2-'></a>
### Contains(point) `method`

##### Summary

Checks if the given point lies within the box.

##### Returns

Returns true if the point lies within the box.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| point | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Point to test. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Box-Place-KineticEnergy-CodeTools-Math-Geometry-Box-'></a>
### Place(box) `method`

##### Summary

If nessesary, shrinks the given box to fit within this box. The center is kept the same.

##### Returns

Returns a new box that fits inside the original.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| box | [KineticEnergy.CodeTools.Math.Geometry.Box](#T-KineticEnergy-CodeTools-Math-Geometry-Box 'KineticEnergy.CodeTools.Math.Geometry.Box') | Box to place inside. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Box-SplitAtX-System-Single-'></a>
### SplitAtX(value) `method`

##### Summary

Gives two boxes that are the original box split at the given x value.

##### Returns

Returns an array of two boxes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | X value to split the box at. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Box-SplitAtY-System-Single-'></a>
### SplitAtY(value) `method`

##### Summary

Gives two boxes that are the original box split at the given y value.

##### Returns

Returns an array of two boxes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Y value to split the box at. |

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Circle'></a>
## Circle `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Stores data and methods for a circle.

<a name='T-KineticEnergy-Intangibles-Client-ClientBehavioursManager'></a>
## ClientBehavioursManager `type`

##### Namespace

KineticEnergy.Intangibles.Client

<a name='F-KineticEnergy-Intangibles-Client-ClientBehavioursManager-parents'></a>
### parents `constants`

##### Summary

An array of [GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject')s that, combined, have all [ClientBehaviour](#T-KineticEnergy-Intangibles-Client-ClientBehaviour 'KineticEnergy.Intangibles.Client.ClientBehaviour')s.

<a name='P-KineticEnergy-Intangibles-Client-ClientBehavioursManager-GlobalBehaviours'></a>
### GlobalBehaviours `property`

##### Summary

Finds all [ClientBehaviour](#T-KineticEnergy-Intangibles-Client-ClientBehaviour 'KineticEnergy.Intangibles.Client.ClientBehaviour')s from [parents](#F-KineticEnergy-Intangibles-Client-ClientBehavioursManager-parents 'KineticEnergy.Intangibles.Client.ClientBehavioursManager.parents').

<a name='T-KineticEnergy-Intangibles-Client-ClientData'></a>
## ClientData `type`

##### Namespace

KineticEnergy.Intangibles.Client

##### Summary

Represents a user that is connected to the server.

<a name='M-KineticEnergy-Intangibles-Client-ClientData-#ctor-System-Int32,System-String-'></a>
### #ctor(id,name) `constructor`

##### Summary

Creates a new [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData') with the given properties.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The [id](#F-KineticEnergy-Intangibles-Client-ClientData-id 'KineticEnergy.Intangibles.Client.ClientData.id') of the new [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData'). |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The [name](#F-KineticEnergy-Intangibles-Client-ClientData-name 'KineticEnergy.Intangibles.Client.ClientData.name') of the new [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData'). |

<a name='F-KineticEnergy-Intangibles-Client-ClientData-id'></a>
### id `constants`

##### Summary

The id of this [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData').

<a name='F-KineticEnergy-Intangibles-Client-ClientData-inputs'></a>
### inputs `constants`

##### Summary

The inputs this [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData') is sending,
directed by its associated [ClientManager](#T-KineticEnergy-Intangibles-Client-ClientManager 'KineticEnergy.Intangibles.Client.ClientManager').

<a name='F-KineticEnergy-Intangibles-Client-ClientData-name'></a>
### name `constants`

##### Summary

The display name of this [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData').

<a name='T-KineticEnergy-Intangibles-Client-ClientManager'></a>
## ClientManager `type`

##### Namespace

KineticEnergy.Intangibles.Client

##### Summary

One of these exist on each user's computer to send messages to the server simulation.
Should not exist on the server simulation.

<a name='T-KineticEnergy-Intangibles-Global-ColorPalette'></a>
## ColorPalette `type`

##### Namespace

KineticEnergy.Intangibles.Global

##### Summary

An object that contains a list of [Sample](#T-KineticEnergy-Intangibles-Global-ColorPalette-Sample 'KineticEnergy.Intangibles.Global.ColorPalette.Sample')s.

<a name='M-KineticEnergy-Intangibles-Global-ColorPalette-#ctor-System-Int32-'></a>
### #ctor(capacity) `constructor`

##### Summary

Creates a [ColorPalette](#T-KineticEnergy-Intangibles-Global-ColorPalette 'KineticEnergy.Intangibles.Global.ColorPalette') with the given capacity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| capacity | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Capacity for [samples](#F-KineticEnergy-Intangibles-Global-ColorPalette-samples 'KineticEnergy.Intangibles.Global.ColorPalette.samples'). |

<a name='F-KineticEnergy-Intangibles-Global-ColorPalette-samples'></a>
### samples `constants`

##### Summary

An array of [Sample](#T-KineticEnergy-Intangibles-Global-ColorPalette-Sample 'KineticEnergy.Intangibles.Global.ColorPalette.Sample')s.

<a name='P-KineticEnergy-Intangibles-Global-ColorPalette-Item-System-Int32-'></a>
### Item `property`

##### Summary

A simple "get" function for [samples](#F-KineticEnergy-Intangibles-Global-ColorPalette-samples 'KineticEnergy.Intangibles.Global.ColorPalette.samples').



`return [index];`

##### Returns

Returns the [Sample](#T-KineticEnergy-Intangibles-Global-ColorPalette-Sample 'KineticEnergy.Intangibles.Global.ColorPalette.Sample') at the given index.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index to look at. |

<a name='M-KineticEnergy-Intangibles-Global-ColorPalette-Get-System-String,UnityEngine-Color@-'></a>
### Get(name,color) `method`

##### Summary

Retreives a [Color](#T-UnityEngine-Color 'UnityEngine.Color') by name.

##### Returns

Returns if any color was found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the [Sample](#T-KineticEnergy-Intangibles-Global-ColorPalette-Sample 'KineticEnergy.Intangibles.Global.ColorPalette.Sample') to get. |
| color | [UnityEngine.Color@](#T-UnityEngine-Color@ 'UnityEngine.Color@') | The color that was found. Defaults to [clear](#P-UnityEngine-Color-clear 'UnityEngine.Color.clear'). |

<a name='T-KineticEnergy-Inventory-Container'></a>
## Container `type`

##### Namespace

KineticEnergy.Inventory

##### Summary

Everything that hold [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item')s extends from this class.

<a name='P-KineticEnergy-Inventory-Container-Capacity'></a>
### Capacity `property`

##### Summary

The total capacity of [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item')s that this [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container') can hold.

<a name='P-KineticEnergy-Inventory-Container-Contents'></a>
### Contents `property`

##### Summary

The contents of this [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container').

<a name='P-KineticEnergy-Inventory-Container-UnusedCapacity'></a>
### UnusedCapacity `property`

##### Summary

The total capacity of [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item')s that this [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container') has not used.

<a name='P-KineticEnergy-Inventory-Container-UsedCapacity'></a>
### UsedCapacity `property`

##### Summary

The total capacity of [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item')s that this [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container') has used.

<a name='M-KineticEnergy-Inventory-Container-Add-KineticEnergy-Inventory-Item-'></a>
### Add(inputItem) `method`

##### Summary

Attempts to add the given [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') to the [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container').
Whatever cannot be added is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputItem | [KineticEnergy.Inventory.Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') | The stack to try to add. |

<a name='M-KineticEnergy-Inventory-Container-Take-KineticEnergy-Inventory-Item-'></a>
### Take(outputItem) `method`

##### Summary

Attempts to take the given [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') from the [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container').
Whatever can be taken is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| outputItem | [KineticEnergy.Inventory.Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') | The stack to try to take. |

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Direction'></a>
## Direction `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

General direction in terms of left/right/up/down. More specifically, the normalized direction on either the X or Y axis.

<a name='T-KineticEnergy-CodeTools-Math-Extentions'></a>
## Extentions `type`

##### Namespace

KineticEnergy.CodeTools.Math

<a name='M-KineticEnergy-CodeTools-Math-Extentions-FromRotation-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### FromRotation(v1,v2,angle) `method`

##### Summary

Sets the heading of v1 equal to the heading of v2, then rotates it.

##### Returns

Returns a new vector with the heading of (v2.heading + degrees) and the magnitude of v1.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Original vector. |
| v2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Vector from. |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | Change in angle from v2. |

<a name='M-KineticEnergy-CodeTools-Math-Extentions-Heading-UnityEngine-Vector2-'></a>
### Heading() `method`

##### Summary

The heading of the Vector. Default units of the angle are radians.

##### Returns

Returns the degrees of this vector from Vector2.right.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Extentions-Rotate-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### Rotate(vector,angle) `method`

##### Summary

Rotates a vector by an angle.

##### Returns

Returns a vector of the same magnitude, but rotated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vector | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Vector to rotate. |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | The rotation angle. |

<a name='M-KineticEnergy-CodeTools-Math-Extentions-RotateFrom-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### RotateFrom(v1,v2,theta) `method`

##### Summary

Rotates v1 away from v2.

##### Returns

Returns v1.Rotate([-/+]degrees).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Original vector. |
| v2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Vector from. |
| theta | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | Amount of rotation. |

<a name='M-KineticEnergy-CodeTools-Math-Extentions-RotateTo-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### RotateTo(v1,v2,angle) `method`

##### Summary

Rotates v1 towards v2.

##### Returns

Returns v1.Rotate([+/-]degrees).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Original vector. |
| v2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Vector to. |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | Amount of rotation. |

<a name='M-KineticEnergy-CodeTools-Math-Extentions-SetHeading-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### SetHeading(v,angle) `method`

##### Summary

Sets the heading of the vector.

##### Returns

Returns a new vector is a heading of v.Heading + degrees and magnitude of the original.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Original vector. |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | Amount of rotation in degrees. |

<a name='M-KineticEnergy-CodeTools-Math-Extentions-SetMagnitude-UnityEngine-Vector2,System-Single-'></a>
### SetMagnitude(v,magnitude) `method`

##### Summary

Sets the magnitude of the vector.

##### Returns

Returns a new vector with the heading of the original but with the new magnitude.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Original vector. |
| magnitude | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | New magnitude of the vector. |

<a name='T-KineticEnergy-Ships-Blocks-Block-Face'></a>
## Face `type`

##### Namespace

KineticEnergy.Ships.Blocks.Block

##### Summary

Defines all of the faces that a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') can have.

<a name='T-KineticEnergy-Ships-Blocks-Faces'></a>
## Faces `type`

##### Namespace

KineticEnergy.Ships.Blocks

##### Summary

Class used to store and show/hide the six faces of an [OpaqueBlock](#T-KineticEnergy-Ships-Blocks-OpaqueBlock 'KineticEnergy.Ships.Blocks.OpaqueBlock').

<a name='M-KineticEnergy-Ships-Blocks-Faces-ToggleFaces-System-Byte-'></a>
### ToggleFaces(enabledFlaces) `method`

##### Summary

Uses the given enabled faces to use [SetActive](#M-UnityEngine-GameObject-SetActive-System-Boolean- 'UnityEngine.GameObject.SetActive(System.Boolean)').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| enabledFlaces | [System.Byte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte 'System.Byte') | Represents a line of booleans (starting on the right side) that correspond to Right, Left, Top, Bottom, Front, Back. |

<a name='T-KineticEnergy-Entities-PlayerController-Gamemode'></a>
## Gamemode `type`

##### Namespace

KineticEnergy.Entities.PlayerController

##### Summary



<a name='T-KineticEnergy-CodeTools-Math-Geometry'></a>
## Geometry `type`

##### Namespace

KineticEnergy.CodeTools.Math

##### Summary

A collection of math functions related to Geometry.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-AngleDirection-KineticEnergy-CodeTools-Math-Geometry-Angle,KineticEnergy-CodeTools-Math-Geometry-Axis-'></a>
### AngleDirection(angle,axis) `method`

##### Summary

Gives the Geometry.Direction of an angle relative to the given Geometry.Axis.

##### Returns

Axis.Horizontal relates to Quadrant 1 and 2 for positive. Axis.Vertical relates to Quadrant 1 and 4 for positive.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | The input angle in degrees. |
| axis | [KineticEnergy.CodeTools.Math.Geometry.Axis](#T-KineticEnergy-CodeTools-Math-Geometry-Axis 'KineticEnergy.CodeTools.Math.Geometry.Axis') | The axis to make the output direction relative to. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-AreParallel-KineticEnergy-CodeTools-Math-Geometry-Line,KineticEnergy-CodeTools-Math-Geometry-Line-'></a>
### AreParallel() `method`

##### Summary

Are these two lines parallel?

##### Returns

Returns 'true' if (l1.a * l2.b) - (l2.a * l1.b) == 0.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Exists-System-Single-'></a>
### Exists() `method`

##### Summary

Determines if the given value exists.

##### Returns

Returns 'true' if the number is not Infinity and is not NaN.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Exists-UnityEngine-Vector2-'></a>
### Exists() `method`

##### Summary

Determines if the given value exists.

##### Returns

Returns 'true' if the components are not Infinity nor NaN.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-GetAngle-UnityEngine-Vector2,UnityEngine-Vector2-'></a>
### GetAngle(v1,v2) `method`

##### Summary

Returns the angle between v1 and v2 with respect to the X/Y axies.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Vector 1 (angle from) |
| v2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Vector 2 (angle to) |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle-UnityEngine-Vector2,UnityEngine-Vector2-'></a>
### HeadToTailAngle(v1,v2) `method`

##### Summary

Finds the angle between v1 and v2 if v2's tail is placed on the head of v1.
This angle can also be described as finding the angle of a bend in a line.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | The first vector in the angle. |
| v2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | The second vector in the angle. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-HeadToTailAngle-UnityEngine-Vector2,UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### HeadToTailAngle(v1,v2,degrees) `method`

##### Summary

Changes the heading of v2 so that if v2's tail is placed on the head of v1, the angle between those two vectors is 'degrees'.
This can also be described as setting the angle of a bend in a line.

##### Returns

Returns v2.SetHeading(180.0f - v1.Heading() - degrees).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| v1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Unchanged vector. |
| v2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Changed vector. |
| degrees | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | The angle in degrees. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Intersection-KineticEnergy-CodeTools-Math-Geometry-Line,KineticEnergy-CodeTools-Math-Geometry-Line-'></a>
### Intersection(l1,l2) `method`

##### Summary

Finds the intersection between two or more Geometry objects.

##### Returns

Returns the intersection between l1 and l2.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| l1 | [KineticEnergy.CodeTools.Math.Geometry.Line](#T-KineticEnergy-CodeTools-Math-Geometry-Line 'KineticEnergy.CodeTools.Math.Geometry.Line') | Line 1 |
| l2 | [KineticEnergy.CodeTools.Math.Geometry.Line](#T-KineticEnergy-CodeTools-Math-Geometry-Line 'KineticEnergy.CodeTools.Math.Geometry.Line') | Line 2 |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Intersection-KineticEnergy-CodeTools-Math-Geometry-Circle,KineticEnergy-CodeTools-Math-Geometry-Circle-'></a>
### Intersection(c1,c2) `method`

##### Summary

Finds the intersections between two or more Geometry objects.

##### Returns

Returns [the closest] two intersections between the two circles.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| c1 | [KineticEnergy.CodeTools.Math.Geometry.Circle](#T-KineticEnergy-CodeTools-Math-Geometry-Circle 'KineticEnergy.CodeTools.Math.Geometry.Circle') | Circle 1 |
| c2 | [KineticEnergy.CodeTools.Math.Geometry.Circle](#T-KineticEnergy-CodeTools-Math-Geometry-Circle 'KineticEnergy.CodeTools.Math.Geometry.Circle') | Circle 2 |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Intersection-KineticEnergy-CodeTools-Math-Geometry-Circle,KineticEnergy-CodeTools-Math-Geometry-Circle,KineticEnergy-CodeTools-Math-Geometry-Circle-'></a>
### Intersection(c1,c2,c3) `method`

##### Summary

Finds the intersections between two or more Geometry objects.

##### Returns

Returns [the closest] intersection shared by all three circles.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| c1 | [KineticEnergy.CodeTools.Math.Geometry.Circle](#T-KineticEnergy-CodeTools-Math-Geometry-Circle 'KineticEnergy.CodeTools.Math.Geometry.Circle') | Circle 1. |
| c2 | [KineticEnergy.CodeTools.Math.Geometry.Circle](#T-KineticEnergy-CodeTools-Math-Geometry-Circle 'KineticEnergy.CodeTools.Math.Geometry.Circle') | Circle 2. |
| c3 | [KineticEnergy.CodeTools.Math.Geometry.Circle](#T-KineticEnergy-CodeTools-Math-Geometry-Circle 'KineticEnergy.CodeTools.Math.Geometry.Circle') | Circle 3. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-IsBetweenRange-System-Single,System-Single-'></a>
### IsBetweenRange(range) `method`

##### Summary

Checks if the input is on the interval [-range, +range].

##### Returns

Returns true if input is on the interval [-range, +range]

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | [-range, +range] |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LawOfCosForAngleA-System-Single,System-Single,System-Single-'></a>
### LawOfCosForAngleA(a,b,c) `method`

##### Summary

Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.

##### Returns

Returns the angle opposite of side a.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| a | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length A. |
| b | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length B. |
| c | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length C. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LawOfCosForAngleB-System-Single,System-Single,System-Single-'></a>
### LawOfCosForAngleB(a,b,c) `method`

##### Summary

Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.

##### Returns

Returns the angle opposite of side b.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| a | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length A. |
| b | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length B. |
| c | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length C. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LawOfCosForAngleC-System-Single,System-Single,System-Single-'></a>
### LawOfCosForAngleC(a,b,c) `method`

##### Summary

Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.

##### Returns

Returns the angle opposite of side c.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| a | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length A. |
| b | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length B. |
| c | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Side length C. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LimitTo-System-Single,System-Single-'></a>
### LimitTo(input,limit) `method`

##### Summary

Limits the input to [+/-] limit.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The input value. |
| limit | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The limiting value. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LineFromAngle-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### LineFromAngle() `method`

##### Summary

Generates a line given one point and an angle.

##### Returns



##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LineFromShift-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Line-'></a>
### LineFromShift(distance,line) `method`

##### Summary

Generates a line identical to the original, but shifted left/right by distance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| distance | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') |  |
| line | [KineticEnergy.CodeTools.Math.Geometry.Line](#T-KineticEnergy-CodeTools-Math-Geometry-Line 'KineticEnergy.CodeTools.Math.Geometry.Line') |  |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LineFromTwoPoints-UnityEngine-Vector2,UnityEngine-Vector2-'></a>
### LineFromTwoPoints(p1,p2) `method`

##### Summary

Generates a line given two points.

##### Returns

Returns a line that intersects the two given points.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| p1 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Point 1 |
| p2 | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Point 2 |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LinearDirection-System-Single-'></a>
### LinearDirection(input) `method`

##### Summary

On a 1-Dimentional line, the direction from X1 to X2 is equal to the sign of the difference: X2 - X1.

##### Returns

Returns the sign of the input (-1 or +1).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | X1 - X2 |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LinearDirection-System-Single,KineticEnergy-CodeTools-Math-Geometry-Axis-'></a>
### LinearDirection(input) `method`

##### Summary

On a 1-Dimentional line, the direction from X1 to X2 is equal to the sign of the input.

##### Returns

Returns the sign of the input (-1 or +1).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | X1 - X2 |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-LinearDirection-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Axis-'></a>
### LinearDirection(vector,axis) `method`

##### Summary

Gives which direction the vector is more facing on the given axis.

##### Returns

Returns the sign of the input (-1 or +1).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vector | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | The vector to compare with the given axis. |
| axis | [KineticEnergy.CodeTools.Math.Geometry.Axis](#T-KineticEnergy-CodeTools-Math-Geometry-Axis 'KineticEnergy.CodeTools.Math.Geometry.Axis') | The axis to compare the given vector to. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-NormalizeDegree1-System-Single-'></a>
### NormalizeDegree1(degr) `method`

##### Summary

Normalizes an angle on the interval [0, 360].

##### Returns

Returns an equivalent angle on the interval [0, 360].

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| degr | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Input angle in degrees. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-NormalizeDegree2-System-Single-'></a>
### NormalizeDegree2(degr) `method`

##### Summary

Normalizes an angle on the interval [-360, +360].

##### Returns

Returns an equivalent angle on the interval [-360, +360].

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| degr | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Input angle in degrees. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-NormalizeDegree3-System-Single-'></a>
### NormalizeDegree3(degree) `method`

##### Summary

Normalizes an angle on the interval [-180, +180]

##### Returns

Returns an equivalent angle on the interval [-180, +180].

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| degree | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Input angle in degrees. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-PathToLine-UnityEngine-Vector2,KineticEnergy-CodeTools-Math-Geometry-Line-'></a>
### PathToLine(point,line) `method`

##### Summary

Finds the shortest vector that goes from the given point to the given line.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| point | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | The point where to find the path from. |
| line | [KineticEnergy.CodeTools.Math.Geometry.Line](#T-KineticEnergy-CodeTools-Math-Geometry-Line 'KineticEnergy.CodeTools.Math.Geometry.Line') | The line to find the path to. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-RoundToMultiple-System-Single,System-Single-'></a>
### RoundToMultiple(number,multiple) `method`

##### Summary

Rounds the given number to the nearest multiple of another number.

##### Returns

Returns the multiple closest to number.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| number | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Input number. |
| multiple | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Input number will be rounded to some (multiple * n). |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle-KineticEnergy-CodeTools-Math-Geometry-Angle,System-Single-'></a>
### Vector2FromAngle(angle,magnitude,units) `method`

##### Summary

Creates a new Vector2 with the given angle and magnitude.

##### Returns

Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | The angle of the vector. Specify degrees or radians with 'units'. |
| magnitude | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The magnitude of the vector. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Vector2FromAngle-KineticEnergy-CodeTools-Math-Geometry-Angle-'></a>
### Vector2FromAngle(angle,units) `method`

##### Summary

Creates a new Vector2 with the given angle and a magnitude of 1.

##### Returns

Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| angle | [KineticEnergy.CodeTools.Math.Geometry.Angle](#T-KineticEnergy-CodeTools-Math-Geometry-Angle 'KineticEnergy.CodeTools.Math.Geometry.Angle') | The angle of the vector. Specify degrees or radians with 'units'. |

<a name='T-KineticEnergy-Intangibles-Global-GlobalBehavioursManager'></a>
## GlobalBehavioursManager `type`

##### Namespace

KineticEnergy.Intangibles.Global

<a name='F-KineticEnergy-Intangibles-Global-GlobalBehavioursManager-parents'></a>
### parents `constants`

##### Summary

An array of [GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject')s that, combined, have all [GlobalBehaviour](#T-KineticEnergy-Intangibles-Global-GlobalBehaviour 'KineticEnergy.Intangibles.Global.GlobalBehaviour')s.

<a name='P-KineticEnergy-Intangibles-Global-GlobalBehavioursManager-GlobalBehaviours'></a>
### GlobalBehaviours `property`

##### Summary

Finds all [GlobalBehaviour](#T-KineticEnergy-Intangibles-Global-GlobalBehaviour 'KineticEnergy.Intangibles.Global.GlobalBehaviour')s from [parents](#F-KineticEnergy-Intangibles-Global-GlobalBehavioursManager-parents 'KineticEnergy.Intangibles.Global.GlobalBehavioursManager.parents').

<a name='T-KineticEnergy-Intangibles-Global-GlobalPaletteManager'></a>
## GlobalPaletteManager `type`

##### Namespace

KineticEnergy.Intangibles.Global

##### Summary

Stores and manages the one and only [BlockPalette](#T-KineticEnergy-Intangibles-Global-BlockPalette 'KineticEnergy.Intangibles.Global.BlockPalette').

##### Remarks

Comtains implicit cast operator from [GlobalPaletteManager](#T-KineticEnergy-Intangibles-Global-GlobalPaletteManager 'KineticEnergy.Intangibles.Global.GlobalPaletteManager') and [BlockPalette](#T-KineticEnergy-Intangibles-Global-BlockPalette 'KineticEnergy.Intangibles.Global.BlockPalette').

##### See Also

- [KineticEnergy.Intangibles.Global.BlockPalette](#T-KineticEnergy-Intangibles-Global-BlockPalette 'KineticEnergy.Intangibles.Global.BlockPalette')
- [KineticEnergy.Intangibles.Global.ColorPalette](#T-KineticEnergy-Intangibles-Global-ColorPalette 'KineticEnergy.Intangibles.Global.ColorPalette')

<a name='T-KineticEnergy-Intangibles-Client-ClientData-Inputs'></a>
## Inputs `type`

##### Namespace

KineticEnergy.Intangibles.Client.ClientData

##### Summary

Contains the inputs that a [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData') is sending.

<a name='F-KineticEnergy-Intangibles-Client-ClientData-Inputs-hotbar'></a>
### hotbar `constants`

##### Summary

Index of hotbar selection.

<a name='F-KineticEnergy-Intangibles-Client-ClientData-Inputs-look'></a>
### look `constants`

##### Summary

Look camera or ship.

<a name='F-KineticEnergy-Intangibles-Client-ClientData-Inputs-move'></a>
### move `constants`

##### Summary

Move character or ship.

<a name='F-KineticEnergy-Intangibles-Client-ClientData-Inputs-primary'></a>
### primary `constants`

##### Summary

Primary button pressed.

<a name='F-KineticEnergy-Intangibles-Client-ClientData-Inputs-secondary'></a>
### secondary `constants`

##### Summary

Secondary button pressed.

<a name='F-KineticEnergy-Intangibles-Client-ClientData-Inputs-spin'></a>
### spin `constants`

##### Summary

Spin item in hand, such as block.

<a name='T-KineticEnergy-Unity-InspectorHelper'></a>
## InspectorHelper `type`

##### Namespace

KineticEnergy.Unity

<a name='M-KineticEnergy-Unity-InspectorHelper-TargetsAreMixed``1-UnityEngine-Object[],System-Func{``0,``0,System-Boolean}-'></a>
### TargetsAreMixed\`\`1(targets,mixedTest) `method`

##### Summary

Tests if any unordered pair of a property in [](#!-Editor-targets 'Editor.targets') are unequal.

##### Returns

Returns true if any unordered pair of the given array passes the given [](#!-Func<ScriptType, ScriptType, bool> 'Func<ScriptType, ScriptType, bool>').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| targets | [UnityEngine.Object[]](#T-UnityEngine-Object[] 'UnityEngine.Object[]') | [](#!-Editor-targets 'Editor.targets') |
| mixedTest | [System.Func{\`\`0,\`\`0,System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,``0,System.Boolean}') | Checks if the two given [](#!-ScriptType 'ScriptType')s are mixed. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| ScriptType | The [Object](#T-UnityEngine-Object 'UnityEngine.Object') type that the editor is assigned to. |

<a name='T-KineticEnergy-Inventory-Item'></a>
## Item `type`

##### Namespace

KineticEnergy.Inventory

##### Summary

Everything that is held by a [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container') extends from this class.



One instance of this class represents a "stack" of the item.

<a name='F-KineticEnergy-Inventory-Item-count'></a>
### count `constants`

##### Summary

The amount of items that are in this stack.

<a name='P-KineticEnergy-Inventory-Item-StackSize'></a>
### StackSize `property`

##### Summary

The total size of this stack.

<a name='M-KineticEnergy-Inventory-Item-MaxToAdd-KineticEnergy-Inventory-Size-'></a>
### MaxToAdd(maximumSize) `method`

##### Summary

Finds how many items can be added to this stack so it as close as possible to the given maximumSize.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maximumSize | [KineticEnergy.Inventory.Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size') | The preferred [Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size'). |

<a name='M-KineticEnergy-Inventory-Item-MaxToSub-KineticEnergy-Inventory-Size-'></a>
### MaxToSub(maximumSize) `method`

##### Summary

Finds how many items can be removed from this stack so it as close as possible to the given minimumSize.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maximumSize | [KineticEnergy.Inventory.Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size') | The preferred [Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size'). |

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Line'></a>
## Line `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Stores data and methods for a line.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-#ctor-System-Single,System-Single,System-Single,System-Single-'></a>
### #ctor(A,B,C,x) `constructor`

##### Summary

Creates a vertical line given A, B, C, and it's x-coordinate.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| A | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Ax + by = c |
| B | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | ax + By = c |
| C | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | ax + by = C |
| x | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | X = this (horizontal line) |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-#ctor-System-Single,System-Single,System-Single-'></a>
### #ctor(A,B,C) `constructor`

##### Summary

Creates a line given A, B, and C in Ax + By = C

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| A | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Ax + by = c |
| B | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | ax + By = c |
| C | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | ax + by = C |

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Line-X'></a>
### X `constants`

##### Summary

If the line is horizontal, the equation of the line is X = [a constant].

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Line-a'></a>
### a `constants`

##### Summary

ax + by = c;

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Line-b'></a>
### b `constants`

##### Summary

ax + by = c;

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Line-c'></a>
### c `constants`

##### Summary

ax + by = c;

<a name='F-KineticEnergy-CodeTools-Math-Geometry-Line-isVertical'></a>
### isVertical `constants`

##### Summary

Is the line horizontal?

<a name='P-KineticEnergy-CodeTools-Math-Geometry-Line-slope'></a>
### slope `property`

##### Summary

The slope of the line.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-Angle-KineticEnergy-CodeTools-Math-Geometry-AngleType-'></a>
### Angle() `method`

##### Summary

Angle of the slope.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-Intersection-KineticEnergy-CodeTools-Math-Geometry-Line-'></a>
### Intersection() `method`

##### Summary

Finds the intersection of this and l2.

##### Returns

Returns the point of intersection.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromDistance-UnityEngine-Vector2,System-Single,UnityEngine-Vector2-'></a>
### PointFromDistance(point,distance,direction) `method`

##### Summary

Finds a point on this line from a start point, distance, and direction.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| point | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | distance from this point |
| distance | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | distance from point 'point' |
| direction | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | direction on the line (does not need to align with the lign) |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromDistance-UnityEngine-Vector2,System-Single,System-Int32-'></a>
### PointFromDistance(point,distance,direction) `method`

##### Summary

Finds a point on this line from a start point, distance, and direction.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| point | [UnityEngine.Vector2](#T-UnityEngine-Vector2 'UnityEngine.Vector2') | Distance from this point. |
| distance | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | Distance from point 'point'. |
| direction | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Left = -1, Right = +1. |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromX-System-Single-'></a>
### PointFromX(x) `method`

##### Summary

Returns (x, y) given x.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | the x-coordinate of the point |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-PointFromY-System-Single-'></a>
### PointFromY(y) `method`

##### Summary

Returns (x, y) given y.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| y | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | the y-coordinate of the point |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-ShiftX-System-Single-'></a>
### ShiftX(d) `method`

##### Summary

Shifts the line left/right by a distance of 'd'.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | distance to shift the line |

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Line-ShiftY-System-Single-'></a>
### ShiftY(d) `method`

##### Summary

Shifts the line upwards/downwards by a distance of 'd'.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | distance to shift the line. |

<a name='T-KineticEnergy-Ships-Mass'></a>
## Mass `type`

##### Namespace

KineticEnergy.Ships

##### Summary

Represents a center and magnitude of mass.

<a name='F-KineticEnergy-Ships-Mass-magnitude'></a>
### magnitude `constants`

##### Summary

The magnitude of this mass.

<a name='F-KineticEnergy-Ships-Mass-one'></a>
### one `constants`

##### Summary

Shorthand for "`new Mass(1, Vector3.zero)`".

<a name='F-KineticEnergy-Ships-Mass-position'></a>
### position `constants`

##### Summary

The local position of this mass.

<a name='F-KineticEnergy-Ships-Mass-zero'></a>
### zero `constants`

##### Summary

Shorthand for "`new Mass(0, Vector3.zero)`".

<a name='M-KineticEnergy-Ships-Mass-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

Auto-generated by Visual Studio.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

Auto-generated by Visual Studio.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Addition-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass-'></a>
### op_Addition() `method`

##### Summary

An addition operator that avoids overflow by checking if the result is less than either of the original values.
If there is an overflow, the result will be [MaxValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MaxValue 'System.UInt64.MaxValue').



Also gives the appropriate [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position') to the return value.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Addition-KineticEnergy-Ships-Mass,System-UInt64-'></a>
### op_Addition() `method`

##### Summary

An addition operator that avoids overflow by checking if the result is less than either of the original values.
If there is an overflow, the result will be [MaxValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MaxValue 'System.UInt64.MaxValue').



The [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position') does not change.

##### Parameters

This method has no parameters.

##### Remarks

There is no mirroring "`+(ulong value, Mass mass)`" because it makes sense to add a number to a weighted position, but not to add a weighted position to a number.

<a name='M-KineticEnergy-Ships-Mass-op_Addition-KineticEnergy-Ships-Mass,UnityEngine-Vector3-'></a>
### op_Addition() `method`

##### Summary

Shifts the center of mass by the given vector through addition.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Division-KineticEnergy-Ships-Mass,System-UInt64-'></a>
### op_Division() `method`

##### Summary

A division operator that avoids underflow by checking if the result is greater than either of the original values.
If there is an underflow, the result will be [MinValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MinValue 'System.UInt64.MinValue').



The [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position') does not change.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Equality-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass-'></a>
### op_Equality() `method`

##### Summary

Checks if both given [Mass](#T-KineticEnergy-Ships-Mass 'KineticEnergy.Ships.Mass') objects have equal [magnitude](#F-KineticEnergy-Ships-Mass-magnitude 'KineticEnergy.Ships.Mass.magnitude')s and equal [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position')s.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_GreaterThan-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass-'></a>
### op_GreaterThan() `method`

##### Summary

Compares the two [magnitude](#F-KineticEnergy-Ships-Mass-magnitude 'KineticEnergy.Ships.Mass.magnitude')s.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Inequality-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass-'></a>
### op_Inequality() `method`

##### Summary

Checks if both given [Mass](#T-KineticEnergy-Ships-Mass 'KineticEnergy.Ships.Mass') objects have unequal [magnitude](#F-KineticEnergy-Ships-Mass-magnitude 'KineticEnergy.Ships.Mass.magnitude')s or unequal [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position')s.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_LessThan-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass-'></a>
### op_LessThan() `method`

##### Summary

Compares the two [magnitude](#F-KineticEnergy-Ships-Mass-magnitude 'KineticEnergy.Ships.Mass.magnitude')s.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Multiply-KineticEnergy-Ships-Mass,System-UInt64-'></a>
### op_Multiply() `method`

##### Summary

A multiplication operator that avoids overflow by checking if the result is less than either of the original values.
If there is an overflow, the result will be [MaxValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MaxValue 'System.UInt64.MaxValue').



The [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position') does not change.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Subtraction-KineticEnergy-Ships-Mass,KineticEnergy-Ships-Mass-'></a>
### op_Subtraction() `method`

##### Summary

A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.
If there is an underflow, the result will be [MinValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MinValue 'System.UInt64.MinValue').



Also gives the appropriate [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position') to the return value.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Ships-Mass-op_Subtraction-KineticEnergy-Ships-Mass,System-UInt64-'></a>
### op_Subtraction() `method`

##### Summary

A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.
If there is an underflow, the result will be [MinValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MinValue 'System.UInt64.MinValue').



The [position](#F-KineticEnergy-Ships-Mass-position 'KineticEnergy.Ships.Mass.position') does not change.

##### Parameters

This method has no parameters.

##### Remarks

There is no mirroring "`-(ulong value, Mass mass)`" because it makes sense to subtract a number from a weighted position, but not to subtract a weighted position from a number.

<a name='M-KineticEnergy-Ships-Mass-op_Subtraction-KineticEnergy-Ships-Mass,UnityEngine-Vector3-'></a>
### op_Subtraction() `method`

##### Summary

Shifts the center of mass by the given vector through subtraction.

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-Ships-Blocks-OpaqueBlock'></a>
## OpaqueBlock `type`

##### Namespace

KineticEnergy.Ships.Blocks

##### Summary

All sides are opaque.

<a name='T-KineticEnergy-Entities-PlayerController'></a>
## PlayerController `type`

##### Namespace

KineticEnergy.Entities

<a name='F-KineticEnergy-Entities-PlayerController-gamemode'></a>
### gamemode `constants`

##### Summary

The current [Gamemode](#T-KineticEnergy-Entities-PlayerController-Gamemode 'KineticEnergy.Entities.PlayerController.Gamemode') of this player.

<a name='P-KineticEnergy-Entities-PlayerController-hotbar'></a>
### hotbar `property`

##### Summary

This player's [Hotbar](#T-KineticEnergy-Entities-PlayerController-Hotbar 'KineticEnergy.Entities.PlayerController.Hotbar').

<a name='T-KineticEnergy-CodeTools-Math-Range'></a>
## Range `type`

##### Namespace

KineticEnergy.CodeTools.Math

##### Summary

Class for an inclusive range.

<a name='F-KineticEnergy-CodeTools-Math-Range-half'></a>
### half `constants`

##### Summary

Size of 1: (-0.5, +0.5)

<a name='F-KineticEnergy-CodeTools-Math-Range-infinite'></a>
### infinite `constants`

##### Summary

Unlimited range.

<a name='M-KineticEnergy-CodeTools-Math-Range-ChangeByAmount-System-Single-'></a>
### ChangeByAmount(value) `method`

##### Summary

Adds twice the given value to the size.

##### Returns

Returns a new range with the new size and the same center.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The value to add to the size. |

<a name='M-KineticEnergy-CodeTools-Math-Range-ChangeByFactor-System-Single-'></a>
### ChangeByFactor(value) `method`

##### Summary

Multiplies the size by the given value.

##### Returns

Returns a new range with the new size and same center.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The value to multiply the size by. |

<a name='M-KineticEnergy-CodeTools-Math-Range-Contains-System-Single-'></a>
### Contains() `method`

##### Summary

Checks if the given value is within the range.

##### Returns

Returns 'true' if the given value is on the interval [min, max].

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Range-Contains-KineticEnergy-CodeTools-Math-Range-'></a>
### Contains(range) `method`

##### Summary

Checks if the given range is completely encompassed by this range.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [KineticEnergy.CodeTools.Math.Range](#T-KineticEnergy-CodeTools-Math-Range 'KineticEnergy.CodeTools.Math.Range') |  |

<a name='M-KineticEnergy-CodeTools-Math-Range-EdgeDistance-System-Single-'></a>
### EdgeDistance(value) `method`

##### Summary

Finds the value's distance to the closest edge of the range.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') |  |

<a name='M-KineticEnergy-CodeTools-Math-Range-Place-System-Single-'></a>
### Place() `method`

##### Summary

Places the given value to the nearest value on the interval [min, max].

##### Returns

If the given value is lesser/greater than min/max then it returns min/max. Otherwise, the value is unchanged.

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Range-PlaceOutside-System-Single-'></a>
### PlaceOutside() `method`

##### Summary

Places the given value to either the minimum or maximum: whichever is closer.

##### Returns

Returns the closest min/max to the given value.

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-CodeTools-Math-Range2D'></a>
## Range2D `type`

##### Namespace

KineticEnergy.CodeTools.Math

##### Summary

Class for an inclusive Vector2 range for x and y components.

<a name='M-KineticEnergy-CodeTools-Math-Range2D-#ctor-KineticEnergy-CodeTools-Math-Range,KineticEnergy-CodeTools-Math-Range-'></a>
### #ctor(x,y) `constructor`

##### Summary

Creates a new Range2D.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [KineticEnergy.CodeTools.Math.Range](#T-KineticEnergy-CodeTools-Math-Range 'KineticEnergy.CodeTools.Math.Range') | Range for the x component. |
| y | [KineticEnergy.CodeTools.Math.Range](#T-KineticEnergy-CodeTools-Math-Range 'KineticEnergy.CodeTools.Math.Range') | Range for the y component. |

<a name='M-KineticEnergy-CodeTools-Math-Range2D-#ctor-System-Single,System-Single,System-Single,System-Single-'></a>
### #ctor() `constructor`

##### Summary

Creates a new Range2D.

##### Parameters

This constructor has no parameters.

<a name='F-KineticEnergy-CodeTools-Math-Range2D-half'></a>
### half `constants`

##### Summary

Area of 1.

<a name='F-KineticEnergy-CodeTools-Math-Range2D-infinite'></a>
### infinite `constants`

##### Summary

Unlimited range.

<a name='F-KineticEnergy-CodeTools-Math-Range2D-x'></a>
### x `constants`

##### Summary

The range of this vector component.

<a name='F-KineticEnergy-CodeTools-Math-Range2D-y'></a>
### y `constants`

##### Summary

The range of this vector component.

<a name='M-KineticEnergy-CodeTools-Math-Range2D-Contains-UnityEngine-Vector2-'></a>
### Contains() `method`

##### Summary

Checks if the given value is within the range.

##### Returns

Returns 'true' if the given value is on the interval [min, max].

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-CodeTools-Math-Range2D-Place-UnityEngine-Vector2-'></a>
### Place() `method`

##### Summary

Applies 'Range.Place(float)' to both components of the given vector to their respective range.

##### Returns

Returns a new Vector2(x.Place(value.x), y.Place(value.y))

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-Intangibles-Global-BlockPalette-Sample'></a>
## Sample `type`

##### Namespace

KineticEnergy.Intangibles.Global.BlockPalette

##### Summary

A class for containing a [Block](#T-KineticEnergy-Ships-Blocks-Block 'KineticEnergy.Ships.Blocks.Block') prefab and it's respective [BlockPreview](#T-KineticEnergy-Ships-Blocks-BlockPreview 'KineticEnergy.Ships.Blocks.BlockPreview') prefab.

##### See Also

- [KineticEnergy.Intangibles.Global.GlobalPaletteManager](#T-KineticEnergy-Intangibles-Global-GlobalPaletteManager 'KineticEnergy.Intangibles.Global.GlobalPaletteManager')
- [KineticEnergy.Intangibles.Global.BlockPalette](#T-KineticEnergy-Intangibles-Global-BlockPalette 'KineticEnergy.Intangibles.Global.BlockPalette')

<a name='T-KineticEnergy-Intangibles-Global-ColorPalette-Sample'></a>
## Sample `type`

##### Namespace

KineticEnergy.Intangibles.Global.ColorPalette

##### Summary

A named color.

##### See Also

- [KineticEnergy.Intangibles.Global.GlobalPaletteManager](#T-KineticEnergy-Intangibles-Global-GlobalPaletteManager 'KineticEnergy.Intangibles.Global.GlobalPaletteManager')
- [KineticEnergy.Intangibles.Global.ColorPalette](#T-KineticEnergy-Intangibles-Global-ColorPalette 'KineticEnergy.Intangibles.Global.ColorPalette')

<a name='M-KineticEnergy-Intangibles-Global-BlockPalette-Sample-#ctor-UnityEngine-GameObject,UnityEngine-GameObject-'></a>
### #ctor(prefabBlock,b\lockPrefab_preview\lockPrefab_preview) `constructor`

##### Summary

Creates a new [Sample](#T-KineticEnergy-Intangibles-Global-BlockPalette-Sample 'KineticEnergy.Intangibles.Global.BlockPalette.Sample') from the given arguments.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| prefabBlock | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') | The prefab variant of the [prefabBlock](#F-KineticEnergy-Intangibles-Global-BlockPalette-Sample-prefabBlock 'KineticEnergy.Intangibles.Global.BlockPalette.Sample.prefabBlock'). |
| b\lockPrefab_preview\lockPrefab_preview | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') | The prefab that will be cloned with [](#!-Object-Instantiate-- 'Object.Instantiate()') |

<a name='F-KineticEnergy-Intangibles-Global-BlockPalette-Sample-prefabBlock'></a>
### prefabBlock `constants`

##### Summary

The prefab that will be cloned with [](#!-Object-Instantiate-- 'Object.Instantiate()').

<a name='F-KineticEnergy-Intangibles-Global-BlockPalette-Sample-prefabBlock_preview'></a>
### prefabBlock_preview `constants`

##### Summary

The prefab variant of the [prefabBlock](#F-KineticEnergy-Intangibles-Global-BlockPalette-Sample-prefabBlock 'KineticEnergy.Intangibles.Global.BlockPalette.Sample.prefabBlock').

<a name='T-KineticEnergy-Intangibles-Server-ServerBehavioursManager'></a>
## ServerBehavioursManager `type`

##### Namespace

KineticEnergy.Intangibles.Server

<a name='F-KineticEnergy-Intangibles-Server-ServerBehavioursManager-parents'></a>
### parents `constants`

##### Summary

An array of [GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject')s that, combined, have all [ServerBehaviour](#T-KineticEnergy-Intangibles-Server-ServerBehaviour 'KineticEnergy.Intangibles.Server.ServerBehaviour')s.

<a name='P-KineticEnergy-Intangibles-Server-ServerBehavioursManager-GlobalBehaviours'></a>
### GlobalBehaviours `property`

##### Summary

Finds all [ServerBehaviour](#T-KineticEnergy-Intangibles-Server-ServerBehaviour 'KineticEnergy.Intangibles.Server.ServerBehaviour')s from [parents](#F-KineticEnergy-Intangibles-Server-ServerBehavioursManager-parents 'KineticEnergy.Intangibles.Server.ServerBehavioursManager.parents').

<a name='T-KineticEnergy-Intangibles-Server-ServerManager'></a>
## ServerManager `type`

##### Namespace

KineticEnergy.Intangibles.Server

##### Summary

Manages connection between the simulation and the [ClientData](#T-KineticEnergy-Intangibles-Client-ClientData 'KineticEnergy.Intangibles.Client.ClientData')s.

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Sign'></a>
## Sign `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Positive = 1, Negative = -1.

<a name='T-KineticEnergy-Inventory-SimpleContainer'></a>
## SimpleContainer `type`

##### Namespace

KineticEnergy.Inventory

##### Summary

[Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container') without the BS.

<a name='P-KineticEnergy-Inventory-SimpleContainer-Contents'></a>
### Contents `property`

##### Summary

The contents of this [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container').

<a name='P-KineticEnergy-Inventory-SimpleContainer-UsedCapacity'></a>
### UsedCapacity `property`

##### Summary

The total capacity of [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item')s that this [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container') has not used.

<a name='M-KineticEnergy-Inventory-SimpleContainer-Add-KineticEnergy-Inventory-Item-'></a>
### Add(inputItem) `method`

##### Summary

Attempts to add the given [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') to the [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container').
Whatever cannot be added is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputItem | [KineticEnergy.Inventory.Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') | The stack to try to add. |

<a name='M-KineticEnergy-Inventory-SimpleContainer-SimpleAdd-System-Collections-Generic-List{KineticEnergy-Inventory-Item}@,KineticEnergy-Inventory-Item@,KineticEnergy-Inventory-Size-'></a>
### SimpleAdd(inputItem) `method`

##### Summary

Attempts to add the given [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') to the given [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1').
Whatever cannot be added is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputItem | [System.Collections.Generic.List{KineticEnergy.Inventory.Item}@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{KineticEnergy.Inventory.Item}@') | The stack to try to add. |

<a name='M-KineticEnergy-Inventory-SimpleContainer-SimpleTake-System-Collections-Generic-List{KineticEnergy-Inventory-Item}@,KineticEnergy-Inventory-Item@,KineticEnergy-Inventory-Size-'></a>
### SimpleTake(outputItem) `method`

##### Summary

Attempts to take the given [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') from the given [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1').
Whatever can be taken is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| outputItem | [System.Collections.Generic.List{KineticEnergy.Inventory.Item}@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{KineticEnergy.Inventory.Item}@') | The stack to try to take. |

<a name='M-KineticEnergy-Inventory-SimpleContainer-Take-KineticEnergy-Inventory-Item-'></a>
### Take(outputItem) `method`

##### Summary

Attempts to take the given [Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') from the [Container](#T-KineticEnergy-Inventory-Container 'KineticEnergy.Inventory.Container').
Whatever can be taken is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| outputItem | [KineticEnergy.Inventory.Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') | The stack to try to take. |

<a name='T-KineticEnergy-Inventory-SimpleItem'></a>
## SimpleItem `type`

##### Namespace

KineticEnergy.Inventory

##### Summary

[Item](#T-KineticEnergy-Inventory-Item 'KineticEnergy.Inventory.Item') without the BS.

<a name='P-KineticEnergy-Inventory-SimpleItem-ItemSize'></a>
### ItemSize `property`

##### Summary

Gets the size of a single item.

<a name='P-KineticEnergy-Inventory-SimpleItem-StackSize'></a>
### StackSize `property`

##### Summary

The total size of this stack.

<a name='M-KineticEnergy-Inventory-SimpleItem-MaxToAdd-KineticEnergy-Inventory-Size-'></a>
### MaxToAdd(maximumSize) `method`

##### Summary

Finds how many items can be added to this stack so it as close as possible to the given maximumSize.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maximumSize | [KineticEnergy.Inventory.Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size') | The preferred [Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size'). |

<a name='M-KineticEnergy-Inventory-SimpleItem-MaxToSub-KineticEnergy-Inventory-Size-'></a>
### MaxToSub(maximumSize) `method`

##### Summary

Finds how many items can be removed from this stack so it as close as possible to the given minimumSize.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maximumSize | [KineticEnergy.Inventory.Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size') | The preferred [Size](#T-KineticEnergy-Inventory-Size 'KineticEnergy.Inventory.Size'). |

<a name='T-KineticEnergy-Inventory-Size'></a>
## Size `type`

##### Namespace

KineticEnergy.Inventory

##### Summary

Essentially just a number with operators that check for overflow/underflow when using operators.

<a name='F-KineticEnergy-Inventory-Size-value'></a>
### value `constants`

##### Summary

20 units is twice as big as 10 units. Don't overthink it.

<a name='M-KineticEnergy-Inventory-Size-op_Addition-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size-'></a>
### op_Addition() `method`

##### Summary

An addition operator that avoids overflow by checking if the result is less than either of the original values.
If there is an overflow, the result will be [MaxValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MaxValue 'System.UInt64.MaxValue').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Inventory-Size-op_Division-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size-'></a>
### op_Division() `method`

##### Summary

A division operator that avoids underflow by checking if the result is greater than either of the original values.
If there is an underflow, the result will be [MinValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MinValue 'System.UInt64.MinValue').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Inventory-Size-op_Division-KineticEnergy-Inventory-Size,System-UInt64-'></a>
### op_Division() `method`

##### Summary

A division operator that avoids underflow by checking if the result is greater than either of the original values.
If there is an underflow, the result will be [MinValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MinValue 'System.UInt64.MinValue').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Inventory-Size-op_Multiply-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size-'></a>
### op_Multiply() `method`

##### Summary

A multiplication operator that avoids overflow by checking if the result is less than either of the original values.
If there is an overflow, the result will be [MaxValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MaxValue 'System.UInt64.MaxValue').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Inventory-Size-op_Multiply-KineticEnergy-Inventory-Size,System-UInt64-'></a>
### op_Multiply() `method`

##### Summary

A multiplication operator that avoids overflow by checking if the result is less than either of the original values.
If there is an overflow, the result will be [MaxValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MaxValue 'System.UInt64.MaxValue').

##### Parameters

This method has no parameters.

<a name='M-KineticEnergy-Inventory-Size-op_Subtraction-KineticEnergy-Inventory-Size,KineticEnergy-Inventory-Size-'></a>
### op_Subtraction() `method`

##### Summary

A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.
If there is an underflow, the result will be [MinValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64.MinValue 'System.UInt64.MinValue').

##### Parameters

This method has no parameters.

<a name='T-KineticEnergy-Ships-Blocks-TranslucentBlock'></a>
## TranslucentBlock `type`

##### Namespace

KineticEnergy.Ships.Blocks

##### Summary

Some sides are opaque.

<a name='T-KineticEnergy-Ships-Blocks-TransparentBlock'></a>
## TransparentBlock `type`

##### Namespace

KineticEnergy.Ships.Blocks

##### Summary

No sides are opaque.

<a name='T-KineticEnergy-CodeTools-Math-Geometry-Triangle'></a>
## Triangle `type`

##### Namespace

KineticEnergy.CodeTools.Math.Geometry

##### Summary

Stores data and methods for a triangle.

<a name='M-KineticEnergy-CodeTools-Math-Geometry-Triangle-#ctor-System-Single,System-Single,System-Single-'></a>
### #ctor(_a,_b,_c) `constructor`

##### Summary

Returns a complete triangle given the side lengths.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| _a | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | side length 'a' |
| _b | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | side length 'b' |
| _c | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | side length 'c' |
