using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.Structs;
using KineticEnergy.CodeTools.Enumerators;
using KineticEnergy.Intangibles;
using KineticEnergy.Intangibles.Behaviours;
using static KineticEnergy.Intangibles.Master.LevelsOfDetail;

namespace KineticEnergy.Ships {

    /// <summary>Essentially not much more than an interface with <see cref="Array"/>.</summary>
    /// <remarks>Has <see cref="IEnumerable"/> for both type <see cref="Block"/> and <see cref="Vector3Int"/> to iterate through <see cref="Array"/>.</remarks>
    public class BlockGrid : MonoBehaviour, IEnumerable {

        #region Properties

        public Master Master { get; internal set; }
        public GlobalBehavioursManager Global => Master.Global;

        /// <summary>A 3-dimentional array that contains all <see cref="Block"/> objects on the ship.</summary>
        public Block[,,] Array { get; private set; } = new Block[1, 1, 1];

        /// <summary>A count of all of the <see cref="Block"/> that are on this <see cref="BlockGrid"/>. Can be manually updated with <see cref="Recount"/>.</summary>
        public int Count { get; private set; }
        /// <summary>Recounts all <see cref="Block"/>s that are within this grid and sets it to <see cref="Count"/>.</summary>
        /// <remarks>O(n) function, where n is volume.</remarks>
        public void Recount() {
            Count = 0;
            foreach(Block block in Array)
                if(block) Count++;
        }

        /// <summary>The sum of every <see cref="Block.Mass"/>.
        /// Updates <see cref="Rigidbody.mass"/> when set (internal). Nothing becides "return" is done on get.</summary>
        /// <remarks>This value is never changed by the <see cref="BlockGrid"/> script.
        /// Instead, the <see cref="Block"/> script updates it on <see cref="Block.UpdateGrid"/> and when 'set'-ing <see cref="Block.Mass"/>.</remarks>
        public Mass Mass {
            get => mass;
            internal set {
                mass = value;
                Rigidbody.mass = Mass.magnitude;
                Rigidbody.centerOfMass = Mass.position;
            }
        }
        private Mass mass = Mass.zero;

        /// <summary>Vector from array space to grid space.</summary>
        /// <remarks>Grid-space is essentially the local position to the parent <see cref="BlockGrid"/>'s <see cref="GameObject"/>.
        /// Meanwhile, array-space, since its index cannot be negative, needs to have its origin at the "most-negative corner" of grid-space.
        /// <para/>Any array position minus <see cref="Offset"/> is the position in the grid.
        /// Any grid position plus <see cref="Offset"/> is the position in the array.</remarks>
        public Vector3Int Offset { get; private set; } = Vector3Int.zero;

        /// <summary>The size/dimensions of the 3-dimensional array, <see cref="Array"/>.
        /// Calls <see cref="Array.GetLength(int)"/> three times.</summary>
        public Vector3Int Size => new Vector3Int(Array.GetLength(0), Array.GetLength(1), Array.GetLength(2));

        /// <summary>The volume of <see cref="Array"/>.</summary>
        public int Volume {
            get {
                var size = Size;
                return size.x * size.y * size.z;
            }
        }

        /// <summary>A simple "get" function for an array-space point.
        /// To get the grid space, add <see cref="Offset"/> to the input.</summary>
        /// <param name="index">The index to look at.</param>
        /// <returns>Returns the <see cref="Block"/> that overlaps at the given point of the array.</returns>
        /// <seealso cref="this[int, int, int]"/>
        /// <seealso cref="Offset"/>
        public Block this[Vector3Int index] {
            get => Array[index.x, index.y, index.z];
            private set => Array[index.x, index.y, index.z] = value;
        }

        /// <summary>A simple "get" function for an array-space point. To get the grid space, add <see cref="Offset"/> to the input.</summary>
        /// <param name="x">The X index to look at.</param>
        /// <param name="y">The Y index to look at.</param>
        /// <param name="z">The Z index to look at.</param>
        /// <returns>Returns the <see cref="Block"/> that overlaps at the given point of the array.</returns>
        /// <seealso cref="this[Vector3Int]"/>
        /// <seealso cref="Offset"/>
        public Block this[int x, int y, int z] {
            get => Array[x, y, z];
            private set => Array[x, y, z] = value;
        }

        
        public class EventActions {
            public List<Action<Block>> OnAddBlock { get; set; } = new List<Action<Block>>();
            public List<Action<Block>> OnSubBlock { get; set; } = new List<Action<Block>>();
        }
        public EventActions Events { get; } = new EventActions();

        #endregion

        #region Startup



        /// <summary>The <see cref="UnityEngine.Rigidbody"/> of this <see cref="BlockGrid"/>.</summary>
        new public Rigidbody rigidbody;
        /// <summary><see cref="rigidbody"/> but with a Unity.Object null-check.</summary>
        public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;
        public void OnEnable() => rigidbody = GetComponent<Rigidbody>();


        #endregion

        #region Modify/Check/Get Data

        #region Checks
        #region Check position is valid.

        /// <summary>Finds if the given point in grid space is within the bounries of the array.</summary>
        /// <param name="gridPosition">Some point in grid space.</param>
        /// <returns>Adds <see cref="Offset"/>, then returns true if 'x', 'y', and 'z' are greater than -1 and less than the <see cref="Array.GetLength(int)"/> [0 through 2].</returns>
        public bool GridPointIsInsideArray(Vector3Int gridPosition) {
            var arrayPosition = gridPosition + Offset;
            return -1 < arrayPosition.x && arrayPosition.x < Array.GetLength(0)
                && -1 < arrayPosition.y && arrayPosition.y < Array.GetLength(1)
                && -1 < arrayPosition.z && arrayPosition.z < Array.GetLength(2);
        }

        /// <summary>Checks if the given point in array space is within the boundries of the array.</summary>
        /// <param name="arrayPosition">Some point in array space.</param>
        /// <returns>Returns true if 'x', 'y', and 'z' are greater than -1 and less than the <see cref="Array.GetLength(int)"/> [0 through 2].</returns>
        public bool ArrayPointIsInsideArray(Vector3Int arrayPosition) {
            return -1 < arrayPosition.x && arrayPosition.x < Array.GetLength(0)
                && -1 < arrayPosition.y && arrayPosition.y < Array.GetLength(1)
                && -1 < arrayPosition.z && arrayPosition.z < Array.GetLength(2);
        }

        /// <summary>Tests if a <see cref="Block"/> can fit at the given position.</summary>
        /// <param name="block">The would-be <see cref="Block"/>.</param>
        /// <param name="gridPosition">Position, in grid space, of the would-be <see cref="Block"/>.</param>
        /// <returns>Returns true if <see cref="HasBlockAt(Vector3Int)"/> is true for every grid position inside the block.</returns>
        public bool CanPlaceBlock(Block block, Vector3Int gridPosition) {
            var corner = gridPosition + block.Dimensions;
            var point = Vector3Int.zero;
            for(int x = gridPosition.x; x < corner.x; x++) {
                point.x = x;
                for(int y = gridPosition.y; y < corner.y; y++) {
                    point.y = y;
                    for(int z = gridPosition.z; z < corner.z; z++) {
                        point.z = z;

                        if(TryHasBlockAt(point))
                            return false;

                    }
                }
            }
            return true;
        }

        #endregion
        #region Check if block at position.

        /// <summary>Checks if there is any block at the given position in the grid.</summary>
        /// <param name="gridPosition">Position in the grid to check.</param>
        /// <returns>Returns true if there is a block at the given position.</returns>
        /// <remarks>Subject to <see cref="IndexOutOfRangeException"/> errors when <see cref="GridPointIsInsideArray(Vector3Int)"/> is false.</remarks>
        public bool HasBlockAt(Vector3Int gridPosition) {
            return this[gridPosition + Offset] != null;
        }

        /// <summary>Same functionality as <see cref="HasBlockAt(Vector3Int)"/> but checks array bounds first.</summary>
        /// <param name="gridPosition">Position in the grid to check.</param>
        /// <returns>Returns false if the given gridPosition is outside of the array, otherwise returns the <see cref="Block"/> at the given grid position.</returns>
        public bool TryHasBlockAt(Vector3Int gridPosition) {
            var arrayPosition = gridPosition + Offset;
            return ArrayPointIsInsideArray(arrayPosition) ? this[arrayPosition] != null : false;
        }

        #endregion
        #region Check if side is covered.

        /// <summary>Checks if a side of the given grid position is covered.</summary>
        /// <param name="gridPosition">Position of the side to check.</param>
        /// <param name="relativeFace">The face of the given position to check.</param>
        /// <remarks>While <see cref="SideIsOpaque"/> checks the block at the given position,
        /// this function checks the block touching the given position.</remarks>
        /// <seealso cref="SideIsOpaque(Vector3Int, Face)"/>
        public bool SideIsCovered(Vector3Int gridPosition, Block.Face relativeFace) {
            gridPosition += Block.DirectionFromFace(relativeFace);
            Block focus = TryGetBlockAt(gridPosition);
            return focus ? focus.SideIsOpaque(gridPosition, (Block.Face)(-(int)relativeFace))
                         : false;
        }

        public bool SideIsCovered(Vector3 localPosition, Block.Face face) => SideIsCovered(
            LocalPoint_to_LocalGrid(localPosition), face);

        /// <summary>Checks if a side of the given grid position is opaque.</summary>
        /// <param name="gridPosition">Position of the side to check.</param>
        /// <param name="relativeFace">The face of the given position to check.</param>
        /// <remarks>While <see cref="SideIsCovered"/> checks the block touching the given position,
        /// this function checks the block at the given position.</remarks>
        /// <seealso cref="SideIsOpaque(Vector3Int, Face)"/>
        public bool SideIsOpaque(Vector3Int gridPosition, Block.Face relativeFace) {
            Block focus = TryGetBlockAt(gridPosition);
            return focus ? focus.SideIsOpaque(gridPosition, relativeFace)
                         : false;
        }

        public bool SideIsOpaque(Vector3 localPoint, Block.Face face)
            => SideIsCovered(LocalPoint_to_LocalGrid(localPoint), face);

        #endregion
        #endregion

        #region Gets
        #region Get block at position.

        /// <summary>Finds the block at the given position in the grid.</summary>
        /// <param name="gridPosition">Position in the grid to look at.</param>
        /// <returns>Returns the appropirate block if there is one, otherwise returns null.</returns>
        /// <remarks>Subject to <see cref="IndexOutOfRangeException"/> errors when <see cref="GridPointIsInsideArray(Vector3Int)"/> is false.</remarks>
        public Block GetBlockAt(Vector3Int gridPosition) {
            return this[gridPosition + Offset];
        }

        /// <summary>Same functionality as <see cref="GetBlockAt(Vector3Int)"/>, but is not subject to <see cref="IndexOutOfRangeException"/> errors.</summary>
        /// <param name="gridPosition">The position in the grid to check.</param>
        /// <returns>Returns null if <see cref="GridPointIsInsideArray(Vector3Int)"/> fails, otherwise returns <see cref="GetBlockAt(Vector3Int)"/>.</returns>
        public Block TryGetBlockAt(Vector3Int gridPosition) {
            var arrayPosition = gridPosition + Offset;
            return ArrayPointIsInsideArray(arrayPosition) ? this[arrayPosition] : null;
        }

        /// <summary>Uses <see cref="LocalPoint_to_LocalGrid(Vector3)"/> to call <see cref="TryGetBlockAt(Vector3)"/>.</summary>
        /// <param name="localPosition">The position in the grid's transform to check.</param>
        /// <returns>Returns null if <see cref="GridPointIsInsideArray(Vector3Int)"/> fails, otherwise returns <see cref="GetBlockAt(Vector3Int)"/>.</returns>
        public Block TryGetBlockAt(Vector3 localPosition) {
            return TryGetBlockAt(LocalPoint_to_LocalGrid(localPosition));
        }

        #endregion
        #region Get neighbors at position.

        /// <summary>A set of vectors that define all neighbors that are next to, and relative to, a 1x1x1 block.</summary>
        public static readonly Vector3Int[] neighborVectors = {
            new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0),
            new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1)
        };

        /// <summary>Finds the neighboring blocks that are next to the given grid position.</summary>
        /// <param name="gridPosition">Position in the grid to look at.</param>
        /// <returns>Returns a <see cref="Block"/> array that may contain null values and reapeats.</returns>
        public Block[] GetNeighborsAt(Vector3Int gridPosition) {
            var neighbors = new Block[6];
            for(int i = 0; i < 6; i++) {
                var neighborPosition = neighborVectors[i] + gridPosition;
                neighbors[i] = GridPointIsInsideArray(neighborPosition) ? GetBlockAt(neighborPosition) : null;
            }
            return neighbors;
        }

        /// <summary>Has the same functionallity as <see cref="GetNeighborsAt(Vector3Int)"/>, but does not contain repeats or null values at the cost of some efficiency.</summary>
        /// <param name="position">Position in the grid to look at.</param>
        /// <returns>Returns a <see cref="Block"/> array.</returns>
        public Block[] GetUniqueNeighborsAt(Vector3Int position) {
            var neighbors = new List<Block>(6);
            for(int i = 0; i < 6; i++) {
                var neighborPosition = neighborVectors[i] + position;
                if(GridPointIsInsideArray(neighborPosition)) {
                    var neighbor = GetBlockAt(neighborPosition);
                    if(neighbor != null && !neighbors.Contains(neighbor))
                        neighbors.Add(neighbor);
                }
            }
            return neighbors.ToArray();
        }

        #endregion
        #region Get grid point from world.

        /// <summary>Converts a local point into grid space for this <see cref="BlockGrid"/>.</summary>
        /// <param name="localPoint">Some point local to this object's transform.</param>
        /// <returns>Returns a <see cref="Vector3Int"/> that represents a point in grid space.</returns>
        public Vector3Int LocalPoint_to_LocalGrid(Vector3 localPoint)
            => new Vector3Int(Mathf.RoundToInt(localPoint.x), Mathf.RoundToInt(localPoint.y), Mathf.RoundToInt(localPoint.z));

        /// <summary>Converts a world point into grid space for this <see cref="BlockGrid"/>.</summary>
        /// <param name="worldPoint">Some point in the world.</param>
        /// <returns>Returns a <see cref="Vector3Int"/> that represents a point in grid space.</returns>
        public Vector3Int WorldPoint_to_LocalWorld(Vector3 worldPoint)
            => LocalPoint_to_LocalGrid(transform.InverseTransformPoint(worldPoint));

        /// <summary>Aligns the given world point to the grid.</summary>
        /// <param name="worldPoint">Some point in the world.</param>
        /// <returns>Returns a <see cref="Vector3"/> that represents a grid point in world space.</returns>
        public Vector3 WorldPoint_to_WorldGrid(Vector3 worldPoint)
            => LocalGrid_to_WorldPoint(WorldPoint_to_LocalWorld(worldPoint));

        #endregion
        #region Get world point from grid.

        /// <summary>Converts a point on this <see cref="BlockGrid"/>'s grid space into local space.</summary>
        /// <param name="gridPoint">Some coordinate on the grid.</param>
        /// <returns>Returns a <see cref="Vector3"/> that represents a point in local space.</returns>
        public Vector3 LocalGrid_to_LocalPoint(Vector3Int gridPoint) => gridPoint;

        /// <summary>Converts a point on this <see cref="BlockGrid"/>'s grid space into world space.</summary>
        /// <param name="gridPoint">Some coordinate on the grid.</param>
        /// <returns>Returns a <see cref="Vector3"/> that represents a point in world space.</returns>
        public Vector3 LocalGrid_to_WorldPoint(Vector3Int gridPoint) => transform.TransformPoint(LocalGrid_to_LocalPoint(gridPoint));

        #endregion
        #endregion

        #region Modify
        #region Place block at position.

        /// <summary>Places a block at the given position in the grid.</summary>
        /// <param name="block"><see cref="Block"/> to place.</param>
        /// <param name="gridPosition">Grid position to place the block at.</param>
        /// <remarks>Subject to clipping if <see cref="CanPlaceBlock(Block, Vector3Int)"/> is false.</remarks>
        public void PlaceBlock(Block block, Vector3Int gridPosition) {

            //Since we're editing the array, we'll be using the position in array-space.
            var arrayPosition = gridPosition + Offset;
            var boundsInArray = arrayPosition + block.Dimensions;

            //Check if the lower corner of the block breaks the array limits.
            Vector3Int corner1Break = arrayPosition;
            if(corner1Break.x > 0) corner1Break.x = 0;
            if(corner1Break.y > 0) corner1Break.y = 0;
            if(corner1Break.z > 0) corner1Break.z = 0;
            //Check if the upper corner of the block breaks the array limits.
            Vector3Int corner2Break = boundsInArray - Size;
            if(corner2Break.x < 0) corner2Break.x = 0;
            if(corner2Break.y < 0) corner2Break.y = 0;
            if(corner2Break.z < 0) corner2Break.z = 0;
            //Expand the array if we need to.
            bool corner1Broke = corner1Break.x != 0 || corner1Break.y != 0 || corner1Break.z != 0;
            bool corner2Broke = corner2Break.x != 0 || corner2Break.y != 0 || corner2Break.z != 0;
            if(corner1Broke || corner2Broke) ChangeArrayDimensions(corner2Break, corner1Break);

            //Update arrayPosition and boundsInArray with the new array.
            arrayPosition = gridPosition + Offset;
            boundsInArray = arrayPosition + block.Dimensions;

            //Now that we know the block will fit, set the refrences in the array.
            for(int x = arrayPosition.x; x < boundsInArray.x; x++) {
                for(int y = arrayPosition.y; y < boundsInArray.y; y++) {
                    for(int z = arrayPosition.z; z < boundsInArray.z; z++) {

                        //Check for BlockOverlapException.
                        if(this[x, y, z] != null) throw new BlockOverlapException(
                            this, new Vector3Int(x, y, z), this[x, y, z], block, true, z == arrayPosition.z, true);

                        //Set the refrence.
                        Array[x, y, z] = block;

                    }
                }
            }

            //Say hello to the neighbors!
            //We assume GetDimensionInfromation() has been called.
            foreach(Vector3Int relativePoint in block.neighboringPoints) {
                var neighboringGridPoint = relativePoint + gridPosition;
                var neighbor = TryGetBlockAt(neighboringGridPoint);
                if(neighbor != null && neighbor != block) //As long as somebody's home... && who isn't you.
                    neighbor.OnNearbyPieceAdded(gridPosition - neighbor.GridPosition);
            }

            //Tell the block that we're done and that it's <<here>>.
            block.UpdateGrid(this, gridPosition);

            //Tell the listeners.
            foreach(var listener in Events.OnAddBlock)
                listener.Invoke(block);

        }

        /// <summary>Instantiates a prefab, defines important values such as transform data and name then calls <see cref="Block.UpdateDimensionInformation()"/>,
        /// then places it on the grid at the given position with <see cref="PlaceBlock(Block, Vector3Int)"/>.</summary>
        /// <param name="prefab">Prefab of the "blank" GameObject.</param>
        /// <param name="gridPosition">Grid position to place the block at.</param>
        /// <remarks>Subject to clipping if <see cref="CanPlaceBlock(Block, Vector3Int)"/> is false.</remarks>
        public void PlaceNewBlock(GameObject prefab, Vector3Int gridPosition, Quaternion localRotation) {
            GameObject blockGameObject = Instantiate(prefab);
            blockGameObject.transform.localRotation = localRotation;
            blockGameObject.name = prefab.name;
            Block block = blockGameObject.GetComponent<Block>();
            Global.PolishBehaviour(block);
            block.UpdateDimensionInformation();
            blockGameObject.SetActive(true);
            PlaceBlock(block, gridPosition);
        }

        /// <summary>When a <see cref="Block"/> is disabled, it is removed from the grid. When it is enabled it is re-added through this function.</summary>
        /// <param name="block">The block that is being enabled.</param>
        /// <param name="gridPosition">The position in the grid that the block is in.</param>
        public void PlaceEnablingBlock(Block block, Vector3Int gridPosition) {
            block.UpdateDimensionInformation();
            PlaceBlock(block, gridPosition);
        }

        /// <summary>Overflow of <see cref="PlaceEnablingBlock(Block, Vector3Int)"/>
        /// where the given vector is first <see cref="Mathf.RoundToInt(float)"/>-ed into a <see cref="Vector3Int"/>.</summary>
        /// <param name="block">The block that is being enabled.</param>
        /// <param name="localPosition">The position in the grid that the block is in.</param>
        internal void PlaceEnablingBlock(Block block, Vector3 localPosition) {
            PlaceEnablingBlock(block, new Vector3Int(
                Mathf.RoundToInt(localPosition.x),
                Mathf.RoundToInt(localPosition.y),
                Mathf.RoundToInt(localPosition.z)));
        }

        #endregion
        #region Remove block at position.

        /// <summary>Finds the <see cref="Block"/> at the given position then locates and removes all refrences in <see cref="Array"/></summary>
        /// <param name="gridPosition">The position in the grid to look at.</param>
        /// <param name="callRemoveFromGrid">Call <see cref="Block.OnRemovedFromGrid()"/>? Should be true if block is being moved to a new grid.</param>
        /// <returns>Returns the block that was removed from the grid.</returns>
        public Block RemoveBlock(Vector3Int gridPosition, bool callRemoveFromGrid) {

            //Position of given point.
            var arrayPosition = gridPosition + Offset;
            Block block = this[arrayPosition];
            if(block == null) return null;

            RemoveBlockReferences(callRemoveFromGrid, block);

            //Test for a separation.
            if(Separations(gridPosition, out IEnumerable<Traversal> traversals) > 1) {

                //Separate the grid.
                var group = new TraversalGroup(traversals, DateTime.Now.Ticks);
                foreach(Traversal traversal in traversals) {
                    if(traversal.Root.Element && !traversal.IsActive) {

                        #region Log Traversal (Basic)
                        if(CanShowLog(LevelOfDetail.Basic, Master.LogSettings.traversal))
                            Debug.Log("<< Separating Grid >>");
                        #endregion

                        //List of blocks to be moved to the new grid.
                        var removal = new Queue<Block>();

                        //Find which blocks should be moved to the new grid.
                        using(IEnumerator<Traversal.Node> enumerator = traversal.StartEnumeration(group)) {
                            foreach(Traversal.Node node in traversal) {
                                Block element = node.Element;
                                if(element) removal.Enqueue(element);
                            }
                        }

                        //Prevent empty grids from being created.
                        if(removal.Count == 0) continue;

                        //Create the new grid.
                        Prefab<BlockGrid> prefab = FindObjectOfType<Master>().prefabs.grid;
                        Instantiated<BlockGrid> grid = prefab.Instantiate(active: false);
                        grid.gameObject.transform.position = transform.position; //set position
                        grid.gameObject.transform.rotation = transform.rotation; //set rotation
                        Global.PolishBehaviour(grid);       //announce that the new grid exists

                        //Set 'active' to the 'active' of the grid it came from (this one).
                        grid.gameObject.SetActive(gameObject.activeSelf);

                        //Move the blocks into the new grid.
                        foreach(Block element in removal) {
                            RemoveBlockReferences(true, element);
                            grid.component.PlaceBlock(element, element.GridPosition);
                        }


                    }
                }

            }

            return block;
        }

        private void RemoveBlockReferences(bool callRemoveFromGrid, Block block) {

            //Remove all refrences to this block from the array.
            foreach(Vector3Int relativePosition in block.insidePoints) {
                var insideArrayPosition = relativePosition + block.GridPosition + Offset;
                this[insideArrayPosition] = null;
            }

            //call neighbor.OnNerbyPieceRemoved()
            foreach(Vector3Int relativePosition in block.insidePoints) {
                var neighboringGridPosition = relativePosition + block.GridPosition;
                foreach(Block neighbor in GetNeighborsAt(neighboringGridPosition)) {
                    if(neighbor != null && neighbor != block) //As long as somebody's home... && who isn't you.
                        neighbor.OnNearbyPieceRemoved(neighboringGridPosition - neighbor.GridPosition);
                }
            }

            //call Block.RemoveFromGrid()
            if(callRemoveFromGrid)
                block.OnRemovedFromGrid();

            //Tell the listeners.
            foreach(var listener in Events.OnSubBlock)
                listener.Invoke(block);

        }

        #endregion
        #region Expand array size.

        /// <summary>Expands the <see cref="Array"/> by the given amount. The sign of the inputs are the direction to expand the grid.</summary>
        /// <param name="amount">Amount to increase the size of <see cref="Array"/> in each axis direction.</param>
        /// <remarks>Since an <see cref="int"/> can only have one sign (maybe when we're using quantum computers), this function
        /// cannot expand the grid in two complete opposite directions at the same time.</remarks>
        /// <seealso cref="ChangeArrayDimensions(Vector3Int, Vector3Int)"/>
        public void ExpandArrayDimensions(Vector3Int amount) {
            var arrayOld = Array;
            Vector3Int sizeOld = Size;

            //Create a new Block[,,] with the new size.
            Block[,,] arrayNew = new Block[
                sizeOld.x + Math.Abs(amount.x),
                sizeOld.y + Math.Abs(amount.y),
                sizeOld.z + Math.Abs(amount.z)
            ];

            //Move arrayOld to gridNew. Take into account the offset between the two arrays.
            //Offset must take into account that (0, 0, 0) of array-space must be in the "most-negative corner" of grid-space.
            //(x, y, z) of these loops are relative to arrayOld.
            var negOffset = amount;
            if(amount.x > 0) negOffset.x = 0;
            if(amount.y > 0) negOffset.y = 0;
            if(amount.z > 0) negOffset.z = 0;
            for(int x = 0; x < sizeOld.x; x++) {
                for(int y = 0; y < sizeOld.y; y++) {
                    for(int z = 0; z < sizeOld.z; z++) {
                        arrayNew[x - negOffset.x, y - negOffset.y, z - negOffset.z] = arrayOld[x, y, z];
                    }
                }
            }

            //Apply the changes.
            Offset -= negOffset;
            Array = arrayNew;

        }

        /// <summary>Expands the <see cref="Array"/> by the given amounts.
        /// All values of "amountPos" should be zero or positive and all values of amountNeg should be zero or negative.</summary>
        /// <param name="amountPos">Amount to increase the size of <see cref="Array"/> in each positive axis direction.</param>
        /// <param name="amountNeg">Amount to increase the size of <see cref="Array"/> in each negative axis direction.</param>
        public void ChangeArrayDimensions(Vector3Int amountPos, Vector3Int amountNeg) {
            var arrayOld = Array;
            Vector3Int sizeOld = Size;

            //Create a new Blocks[,,] with the new size.
            Block[,,] arrayNew = new Block[
                sizeOld.x + amountPos.x - amountNeg.x,
                sizeOld.y + amountPos.y - amountNeg.y,
                sizeOld.z + amountPos.z - amountNeg.z
            ];

            //Move arrayOld to gridNew. Take into account the offset between the two arrays.
            //Offset must take into account that (0, 0, 0) of array-space must be in the "most-negative corner" of grid-space.
            //(x, y, z) of these loops are relative to arrayOld.
            var negOffset = amountNeg;
            for(int x = 0; x < sizeOld.x; x++) {
                for(int y = 0; y < sizeOld.y; y++) {
                    for(int z = 0; z < sizeOld.z; z++) {
                        arrayNew[x - negOffset.x, y - negOffset.y, z - negOffset.z] = arrayOld[x, y, z];
                    }
                }
            }

            //Apply the changes.
            Offset -= negOffset;
            Array = arrayNew;

        }


        #endregion
        #endregion

        #endregion

        #region Separation

        /// <summary>Determines if a <see cref="BlockGrid"/> was separated after a <see cref="Block"/> was removed from the given grid point.</summary>
        /// <param name="position">Grid position of where the grid is most likely to be separated (ex. where a block was just removed).</param>
        /// <returns>Returns the number of grids after a separation at the given grid position.</returns>
        public int Separations(Vector3Int position, out IEnumerable<Traversal> final) {
            long seed = DateTime.Now.Ticks;

            #region Log Traversal (Basic)
            if(CanShowLog(LevelOfDetail.Basic, Master?.LogSettings.traversal ?? LevelOfDetail.None))
                Debug.LogFormat("<< Separations Test >> ({0})", seed);
            #endregion

            //Create six roots for new traversals.
            var traversals = new LinkedList<Traversal>();
            for(var i = 0; i < 6; i++) {
                Vector3Int pos = position;
                GetFloodInfo(i, ref pos, out Block.FaceMask next);
                var traversal = new Traversal(this, pos);
                Traversal.Node root = traversal.Root;
                if(root.Element) {
                    traversals.AddLast(traversal);
                    root.Next = next;
                }
            }

            //Create a traversal group.
            var group = new TraversalGroup(traversals, seed);

            //Start traversing.
            Queue<IEnumerator<Traversal.Node>> enumerators = group.StartEnumerationQueue();
            while(group.ActiveTraversalCount > 1 && group.UniqueRoots.Count > 1) {
                IEnumerator<Traversal.Node> enumerator = enumerators.Dequeue();
                if(enumerator.MoveNext()) enumerators.Enqueue(enumerator);
                else enumerator.Dispose();
            }

            //Return the number of active traversals.
            final = group.MakeFreshTraversals();

            #region Log Traversal (Basic)
            if(CanShowLog(LevelOfDetail.Basic, Master?.LogSettings.traversal ?? LevelOfDetail.None))
                Debug.LogFormat("<1> Traversal Count: {0}", traversals.Count);
            if(CanShowLog(LevelOfDetail.Basic, Master?.LogSettings.traversal ?? LevelOfDetail.None))
                Debug.LogFormat("<2> Returned Group Size: {0}", group.MakeFreshTraversals().Count);
            #endregion

            return traversals.Count;

        }

        public class TraversalGroup {

            public IEnumerable<Traversal> Traversals { get; }
            public long Seed { get; }

            public TraversalGroup(IEnumerable<Traversal> traversals, long seed) {
                Traversals = traversals;
                Seed = seed;
            }

            public Queue<IEnumerator<Traversal.Node>> StartEnumerationQueue() {
                var queue = new Queue<IEnumerator<Traversal.Node>>();
                foreach(Traversal traversal in Traversals) queue.Enqueue(traversal.StartEnumeration(this));
                return queue;
            }

            public int ActiveTraversalCount {
                get {
                    var count = 0;
                    foreach(Traversal traversal in Traversals)
                        if(traversal.IsActive) count++;
                    return count;
                }
            }

            public LinkedList<Traversal.Node> UniqueRoots {
                get {
                    var uniqueRoots = new LinkedList<Traversal.Node>();
                    foreach(Traversal traversal in Traversals) {

                        foreach(Traversal.Node item in uniqueRoots)
                            if(traversal.Root == item) goto Next;

                        uniqueRoots.AddFirst(traversal.Root);
                        Next: continue;

                    }
                    return uniqueRoots;
                }
            }

            public void CombineTraversals(Traversal traversal1, Traversal traversal2) {
                Traversal.Node newRoot = traversal1.Root;
                foreach(Traversal traversal in Traversals)
                    if(traversal.Root == traversal1.Root || traversal.Root == traversal2.Root)
                        traversal.Root = newRoot;
            }

            public LinkedList<Traversal> MakeFreshTraversals() {
                var traversals = new LinkedList<Traversal>();
                foreach(Traversal.Node root in UniqueRoots)
                    traversals.AddLast(root.Traversal);
                return traversals;
            }

        }

        /// <summary>Represents every block that connects to the <see cref="Root"/>.</summary>
        public class Traversal : IEnumerable<Traversal.Node> {

            private long seed;

            #region Properties

            /// <summary>The starting <see cref="Node"/> of this <see cref="Traversal"/>.</summary>
            public Node Root { get; set; }

            /// <summary>Identifier used to check if a <see cref="Node"/> has already been iterated over.</summary>
            public long Seed {
                get => seed;
                set {
                    seed = value;
                    Root.Traversal = this;
                }
            }

            /// <summary>The <see cref="BlockGrid"/> that this <see cref="Traversal"/> is in.</summary>
            public BlockGrid Grid { get; set; }

            #endregion

            #region Constructors

            /// <summary>Creates a new <see cref="Traversal"/> by copying the given <see cref="Traversal"/>.</summary>
            /// <param name="traversal">The <see cref="Traversal"/> to copy.</param>
            public Traversal(Traversal traversal) {
                Grid = traversal.Grid;
                Root = traversal.Root;
            }

            /// <summary>Creates a new <see cref="Traversal"/> with the given root <see cref="Node"/>.</summary>
            /// <param name="grid">The <see cref="BlockGrid"/> this <see cref="Traversal"/> is in.</param>
            /// <param name="root">The root <see cref="Node"/> for this <see cref="Traversal"/>.</param>
            public Traversal(BlockGrid grid, Node root) {
                Grid = grid;
                Root = root;
            }

            /// <summary>Creates a new <see cref="Traversal"/> by finding a root at the given grid position.</summary>
            /// <param name="grid">The <see cref="BlockGrid"/> this <see cref="Traversal"/> is in.</param>
            /// <param name="gridPosition">The position of the root <see cref="Block"/> inside the <see cref="Grid"/>.</param>
            public Traversal(BlockGrid grid, Vector3Int gridPosition) {
                Grid = grid;
                Root = new Node(this, gridPosition);
            }

            #endregion

            /// <summary>Represents a <see cref="Block"/> node of a <see cref="Traversal"/>.</summary>
            public class Node {

                /// <summary>The <see cref="Traversal"/> that this <see cref="Node"/> is part of.</summary>
                public Traversal Traversal {
                    get => traversal;
                    set {
                        traversal = value;
                        Seed = traversal.Seed;
                    }
                }
                private Traversal traversal;

                /// <summary>Tests for <see cref="Root"/> and <see cref="Seed"/> equality.</summary>
                /// <param name="traversal">The <see cref="BlockGrid.Traversal"/> to test with.</param>
                /// <returns>Returns true if the equality returns true.</returns>
                public bool IsTraversal(Traversal traversal) => Traversal.Root == traversal.Root && Traversal.Seed == traversal.Seed;

                /// <summary>Identifier used to check if a <see cref="Node"/> has already been iterated over.</summary>
                public long Seed { get; private set; }

                /// <summary>The root <see cref="Node"/> of this <see cref="Traversal"/>.</summary>
                public Node Root => Traversal.Root;

                /// <summary>Is this the <see cref="Traversal.Root"/> of this <see cref="Traversal"/>?</summary>
                public bool IsRoot => Traversal.Root == this;

                /// <summary>The <see cref="BlockGrid"/> that this <see cref="Node"/> is inside.</summary>
                public BlockGrid Grid => Traversal.Grid;

                /// <summary>The location in the <see cref="BlockGrid"/> that this <see cref="Node"/> is at.</summary>
                public Vector3Int GridPosition { get; set; }

                /// <summary>The <see cref="Block"/> this <see cref="Node"/> refers to.</summary>
                public Block Element => Grid.TryGetBlockAt(GridPosition);

                /// <summary>Which faces is this <see cref="Node"/> set to expand to next, and which are ignored?</summary>
                public Block.FaceMask Next { get; set; }

                /// <summary>Creates a new <see cref="Node"/> from the given parameters.</summary>
                /// <param name="traversal">The value of <see cref="Traversal"/>.</param>
                /// <param name="gridPosition">The value of <see cref="GridPosition"/>.</param>
                public Node(Traversal traversal, Vector3Int gridPosition) => Set(traversal, gridPosition);

                /// <summary>A method for setting the properties of this <see cref="Node"/> with the convienience of a constructor.</summary>
                /// <param name="traversal">The value of <see cref="Traversal"/>.</param>
                /// <param name="gridPosition">The value of <see cref="GridPosition"/>.</param>
                public void Set(Traversal traversal, Vector3Int gridPosition) {
                    Traversal = traversal;
                    GridPosition = gridPosition;
                    Block element = Element;
                    if(element) element.Node = this;
                }

            }

            #region Enumeration

            private ConnectionEnumerator Enumerator { get; set; } = null;

            /// <summary>The amount of <see cref="Node"/>s that still need to be expanded.</summary>
            /// <seealso cref="StartEnumeration"/>
            public int ActiveCount => Enumerator?.ActiveCount ?? 0;
            /// <summary>Are there any <see cref="Node"/>s that still need to be expanded?</summary>
            /// <seealso cref="StartEnumeration"/>
            public bool IsActive => Enumerator?.IsActive ?? false;

            /// <summary>Creates a new <see cref="Enumerator"/> and sets the <see cref="Seed"/>.</summary>
            /// <returns>Returns the new <see cref="Enumerator"/>.</returns>
            public IEnumerator<Node> StartEnumeration(TraversalGroup group) {
                Seed = group.Seed;
                return new ConnectionEnumerator(this, group, Grid.Master.LogSettings);
            }
            public IEnumerator<Node> GetEnumerator() => Enumerator
                ?? throw new InvalidOperationException(string.Format("'{0}' is an invalid operation because '{1}' is null. Has '{2}' been called?",
                                                       nameof(GetEnumerator), nameof(Enumerator), nameof(StartEnumeration)));
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary><see cref="IEnumerator{T}"/> for a <see cref="BlockGrid.Traversal"/>.</summary>
            public class ConnectionEnumerator : IEnumerator<Node> {

                /// <summary>The <see cref="Traversal"/> that this <see cref="ConnectionEnumerator"/> is enumerating through.</summary>
                Traversal Traversal { get; }
                /// <summary>The <see cref="Queue{T}"/> of active <see cref="Node"/>s.</summary>
                Queue<Node> Queue { get; }
                /// <summary>The current <see cref="Node"/> in the enumeration.</summary>
                public Node Current { get; set; }
                /// <summary>The <see cref="TraversalGroup"/> to consider and modify when enumerating.</summary>
                public TraversalGroup Group { get; }

                public void SetLogSettings(Master.LevelsOfDetail value) {
                    if(value == null) {
                        log_current = false;
                        log_next = false;
                        log_count = false;
                    } else {
                        log_current = CanShowLog(LevelOfDetail.Basic, value.traversal);
                        log_next = CanShowLog(LevelOfDetail.High, value.traversal);
                        log_count = CanShowLog(LevelOfDetail.Basic, value.traversal);
                    }
                }
                private bool log_current, log_next, log_count;

                /// <summary>The number of active <see cref="Node"/>s currently in the enumeration.</summary>
                public int ActiveCount => Queue.Count;
                /// <summary>Is there at least one active <see cref="Node"/> in this enumeration?</summary>
                public bool IsActive => ActiveCount > 0;

                /// <summary>Creates a new <see cref="ConnectionEnumerator"/> with the given properties.</summary>
                public ConnectionEnumerator(Traversal traversal, TraversalGroup group, Master.LevelsOfDetail logSettings = null) {
                    SetLogSettings(logSettings);
                    traversal.Enumerator = this;
                    Queue = new Queue<Node>();
                    Queue.Enqueue(traversal.Root);
                    Traversal = traversal;
                    Group = group;
                }

                object IEnumerator.Current => Current;

                public bool MoveNext() {

                    if(IsActive) {

                        //Move next:
                        Current = Queue.Dequeue();
                        Node current = Current;

                        if(log_current) Debug.LogFormat(" * Node at {0} with seed {1}.", current.GridPosition, current.Seed);

                        //For each next direction...
                        foreach(Vector3Int direction in (IEnumerable<Vector3Int>)Current.Next) {
                            Vector3Int pos = Current.GridPosition + direction;
                            Block block = Traversal.Grid.TryGetBlockAt(pos);

                            if(block) {

                                //Check if expansion would be valid.
                                Node next = block.Node;
                                if(next == null) {
                                    //Traversal ran into something that has never had a node before. Valid.
                                    if(log_next) Debug.LogFormat(" ** {0} : Traversal ran into a new node (== null).", direction);
                                    next = new Node(current.Traversal, pos);

                                } else if(next.Seed != current.Seed) {
                                    //Traversal ran into something that has had a node once, but not with this seed. Valid.
                                    if(log_next) Debug.LogFormat(" ** {0} : Traversal ran into a new node (!= seed).", direction);
                                    next.Set(current.Traversal, pos);

                                } else if(next.Root == current.Root) {
                                    //Traversal ran into itself. Not valid.
                                    if(log_next) Debug.LogFormat(" ** {0} : Traversal ran into itself.", direction);
                                    continue;

                                }

                                //Check if we combine instead of expand.
                                if(next.Seed == current.Seed && next.Root != current.Root) {
                                    //Traversal ran into another traversal. Combine the traversals.
                                    if(log_next) Debug.LogFormat(" *** {0} : Traversal combined with another traversal.", direction);
                                    Group.CombineTraversals(current.Traversal, next.Traversal);

                                } else {
                                    //Expand in the direction.
                                    if(log_next) Debug.LogFormat(" *** {0} : Traversal expanded.", direction);
                                    next.Next = ~new Block.FaceMask(-direction);
                                    Queue.Enqueue(next);

                                }

                            }

                        }

                        if(log_count) Debug.LogFormat("There are {0} unique root(s).", Group.UniqueRoots.Count);

                        return true;

                    }

                    return false;

                }

                public void Reset() => Current = Traversal.Root;
                public void Dispose() => Traversal.Enumerator = null;

            }

            #endregion

        }

        /// <summary>im not writing a summary for an extracted method</summary>
        private static void GetFloodInfo(int i, ref Vector3Int pos, out Block.FaceMask next) {
            switch(i) {
                case 0:
                    pos += new Vector3Int(+1, 0, 0);
                    next = Block.FaceMask.NOT_NEGX; break;
                case 1:
                    pos += new Vector3Int(-1, 0, 0);
                    next = Block.FaceMask.NOT_POSX; break;
                case 2:
                    pos += new Vector3Int(0, +1, 0);
                    next = Block.FaceMask.NOT_NEGY; break;
                case 3:
                    pos += new Vector3Int(0, -1, 0);
                    next = Block.FaceMask.NOT_POSY; break;
                case 4:
                    pos += new Vector3Int(0, 0, +1);
                    next = Block.FaceMask.NOT_NEGZ; break;
                default:
                    pos += new Vector3Int(0, 0, -1);
                    next = Block.FaceMask.NOT_POSZ; break;
            }
        }

        #endregion

        #region Enumerable
        //Basically makes this object compatible with a foreach loop.

        /// <summary>Gets the universal iterable of this <see cref="BlockGrid"/>.
        /// Basically, since this is here you can use this with a "foreach" loop.</summary>
        /// <returns>Returns the <see cref="IEnumerator"/> of <see cref="Array"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => Array.GetEnumerator();

        /// <summary>Gets the universal <see cref="Vector3Int"/> (array space) iterable of this <see cref="BlockGrid"/>.
        /// Basically, use this with a "foreach" loop.</summary>
        /// <returns>Returns the <see cref="IEnumerator"/> of <see cref="Array"/>.</returns>
        public ArrayPointsEnumerator ArrayPoints => new CodeTools.Enumerators.ArrayPointsEnumerator(Size);

        #endregion

        #region Exceptions
        /// <summary>Exception for when a <see cref="Block"/> was about to be / was overlapped by another <see cref="Block"/> in <see cref="Array"/>.</summary>
        public class BlockOverlapException : Exception {

            /// <summary>
            /// Creates an error message:
            /// <para/>
            /// "The Block "[native.gameObject.name]" in the "[grid.gameObject.name]" BlockGrid was trying to overlap the Block "[native.gameObject.name]"
            /// at the grid point [arrayPosition + grid.offset] / array point [arrayPosition]. Check for 'ghost' blocks."
            /// <para/>
            /// stoppedCompletely ? "This was interrupted before any data in the BlockGrid.array was changed."
            /// <para/>
            ///  : stoppedOverwrite ? "Although existing data was not changed, data on empty points of the grid may have been."
            /// <para/>
            /// tryToFix ? "An attempt at a fix has been made."
            /// </summary>
            /// <param name="grid">The grid where this overlap happened.</param>
            /// <param name="arrayPosition">The position in the array where this overlap happened.</param>
            /// <param name="native">The block that was/would've been overlapped.</param>
            /// <param name="intruder">The block that was/would've been the one overlapping.</param>
            /// <param name="stoppedOverwrite">Was this stopped before data in <see cref="Array"/> was changed?</param>
            /// <param name="stoppedCompletely">Was this stopped before ANY data in <see cref="Array"/> was changed?</param>
            /// <param name="tryToFix">O(n) operation, where n = volume. Removes all refrences to the block on the affected grid, then Destroys the block.</param>
            public BlockOverlapException(BlockGrid grid, Vector3Int arrayPosition, Block native, Block intruder, bool stoppedOverwrite, bool stoppedCompletely, bool tryToFix) : base(
                "The Block \"" + intruder.gameObject.name + "\" in the \"" + grid.gameObject.name + "\" BlockGrid was trying to overlap the Block \"" + native.gameObject.name +
                "\" at the grid point " + (arrayPosition + grid.Offset) + " / array point " + arrayPosition + ". If this was saved, check for 'ghost' blocks." +
                (stoppedCompletely ? "\nThis was interrupted before any data in the BlockGrid.array was changed."
                : stoppedOverwrite ? "\nData on empty points may have been changed, although existing data was not changed." : "") +
                (tryToFix ? "\nAn attempt at a fix has been made." : "")
            ) {

                if(tryToFix) {
                    Vector3Int size = grid.Size;
                    for(int x = 0; x < size.x; x++)
                        for(int y = 0; y < size.y; y++)
                            for(int z = 0; z < size.z; z++)
                                if(grid.Array[x, y, z] == intruder)
                                    grid.Array[x, y, z] = null;
                }

            }

        }
        #endregion

    }

}
