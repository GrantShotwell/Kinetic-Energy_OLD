using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.CodeTools;
using KineticEnergy.Ships.Blocks;

namespace KineticEnergy.Ships {

    /// <summary>Essentially not much more than an interface with <see cref="array"/>.</summary>
    /// <remarks>Has <see cref="IEnumerable"/> for both type <see cref="Block"/> and <see cref="Vector3Int"/> to iterate through <see cref="array"/>.</remarks>
    public class BlockGrid : MonoBehaviour, IEnumerable {

        #region Properties

        /// <summary>A 3-dimentional array that contains all <see cref="Block"/> objects on the ship.</summary>
        private Block[,,] array = new Block[1, 1, 1];

        /// <summary>The sum of every <see cref="Block.Mass"/>.
        /// Updates <see cref="Rigidbody.mass"/> when set (internal). Nothing becides "return" is done on get.</summary>
        /// <remarks>This value is never changed by the <see cref="BlockGrid"/> script.
        /// Instead, the <see cref="Block"/> script updates it on <see cref="Block.SetGrid"/> and when 'set'-ing <see cref="Block.Mass"/>.</remarks>
        public Mass Mass {
            get { return m_mass; }
            internal set {
                m_mass = value;
                Rigidbody.mass = Mass.magnitude;
                Rigidbody.centerOfMass = Mass.position;
            }
        } private Mass m_mass = Mass.zero;

        /// <summary>Vector from array space to grid space.</summary>
        /// <remarks>Grid-space is essentially the local position to the parent <see cref="BlockGrid"/>'s <see cref="GameObject"/>.
        /// Meanwhile, array-space, since its index cannot be negative, needs to have its origin at the "most-negative corner" of grid-space.
        /// <para/>Any array position minus <see cref="offset"/> is the position in the grid.
        /// Any grid position plus <see cref="offset"/> is the position in the array.</remarks>
        public Vector3Int offset = Vector3Int.zero;

        /// <summary>The size/dimensions of the 3-dimensional array, <see cref="array"/>.
        /// Calls <see cref="Array.GetLength(int)"/> three times.</summary>
        public Vector3Int Size => new Vector3Int(array.GetLength(0), array.GetLength(1), array.GetLength(2));

        /// <summary>A simple "get" function for an array-space point.
        /// To get the grid space, add <see cref="offset"/> to the input.</summary>
        /// <param name="index">The index to look at.</param>
        /// <returns>Returns the <see cref="Block"/> that overlaps at the given point of the array.</returns>
        /// <seealso cref="this[int, int, int]"/>
        /// <seealso cref="offset"/>
        public Block this[Vector3Int index] {
            get { return array[index.x, index.y, index.z]; }
            private set { array[index.x, index.y, index.z] = value; }
        }

        /// <summary>A simple "get" function for an array-space point. To get the grid space, add <see cref="offset"/> to the input.</summary>
        /// <param name="x">The X index to look at.</param>
        /// <param name="y">The Y index to look at.</param>
        /// <param name="z">The Z index to look at.</param>
        /// <returns>Returns the <see cref="Block"/> that overlaps at the given point of the array.</returns>
        /// <seealso cref="this[Vector3Int]"/>
        /// <seealso cref="offset"/>
        public Block this[int x, int y, int z] {
            get { return array[x, y, z]; }
            private set { array[x, y, z] = value; }
        }

        #endregion

        #region Startup

        /// <summary>The <see cref="UnityEngine.Rigidbody"/> of this <see cref="BlockGrid"/>.</summary>
        new public Rigidbody rigidbody { get; private set; }
        /// <summary><see cref="rigidbody"/> but with a Unity.Object null-check.</summary>
        public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;
        public void OnEnable() { rigidbody = GetComponent<Rigidbody>(); }

        #endregion

        #region Modify/Check/Get Data

        #region Checks
        #region Check position is valid.

        /// <summary>Finds if the given point in grid space is within the bounries of the array.</summary>
        /// <param name="gridPosition">Some point in grid space.</param>
        /// <returns>Adds <see cref="offset"/>, then returns true if 'x', 'y', and 'z' are greater than -1 and less than the <see cref="Array.GetLength(int)"/> [0 through 2].</returns>
        public bool GridPointIsInsideArray(Vector3Int gridPosition) {
            var arrayPosition = gridPosition + offset;
            return -1 < arrayPosition.x && arrayPosition.x < array.GetLength(0)
                && -1 < arrayPosition.y && arrayPosition.y < array.GetLength(1)
                && -1 < arrayPosition.z && arrayPosition.z < array.GetLength(2);
        }

        /// <summary>Checks if the given point in array space is within the boundries of the array.</summary>
        /// <param name="arrayPosition">Some point in array space.</param>
        /// <returns>Returns true if 'x', 'y', and 'z' are greater than -1 and less than the <see cref="Array.GetLength(int)"/> [0 through 2].</returns>
        public bool ArrayPointIsInsideArray(Vector3Int arrayPosition) {
            return -1 < arrayPosition.x && arrayPosition.x < array.GetLength(0)
                && -1 < arrayPosition.y && arrayPosition.y < array.GetLength(1)
                && -1 < arrayPosition.z && arrayPosition.z < array.GetLength(2);
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
            return this[gridPosition + offset] != null;
        }

        /// <summary>Same functionality as <see cref="HasBlockAt(Vector3Int)"/> but checks array bounds first.</summary>
        /// <param name="gridPosition">Position in the grid to check.</param>
        /// <returns>Returns false if the given gridPosition is outside of the array, otherwise returns the <see cref="Block"/> at the given grid position.</returns>
        public bool TryHasBlockAt(Vector3Int gridPosition) {
            var arrayPosition = gridPosition + offset;
            return ArrayPointIsInsideArray(arrayPosition) ? this[arrayPosition] != null : false;
        }

        #endregion
        #region Check if side is covered.

        /// <summary>Checks if a side of the given grid position is covered.</summary>
        /// <param name="gridPosition">Position of the side to check.</param>
        /// <param name="face">The face of the given position to check.</param>
        /// <remarks>While <see cref="SideIsOpaque"/> checks the block at the given position,
        /// this function checks the block touching the given position.</remarks>
        /// <seealso cref="SideIsOpaque(Vector3Int, Face)"/>
        public bool SideIsCovered(Vector3Int gridPosition, Block.Face face) {

            var pos = gridPosition + Block.DirectionFromFace(face);
            var focus = TryGetBlockAt(pos);

            return focus == null ? false : focus.SideIsOpaque(pos, (Block.Face)(-(int)face));

        }

        public bool SideIsCovered(Vector3 localPosition, Block.Face face) => SideIsCovered(
            LocalPointToGrid(localPosition), face);

        /// <summary>Checks if a side of the given grid position is opaque.</summary>
        /// <param name="gridPosition"></param>
        /// <param name="face"></param>
        /// <remarks>While <see cref="SideIsCovered"/> checks the block touching the given position,
        /// this function checks the block at the given position.</remarks>
        /// <seealso cref="SideIsOpaque(Vector3Int, Face)"/>
        public bool SideIsOpaque(Vector3Int gridPosition, Block.Face face) {

            var pos = gridPosition;
            var focus = TryGetBlockAt(pos);

            return focus == null ? false : focus.SideIsOpaque(pos, face);

        }

        public bool SideIsOpaque(Vector3 localPosition, Block.Face face) => SideIsCovered(
            LocalPointToGrid(localPosition), face);

        #endregion
        #endregion

        #region Gets
        #region Get block at position.

        /// <summary>Finds the block at the given position in the grid.</summary>
        /// <param name="gridPosition">Position in the grid to look at.</param>
        /// <returns>Returns the appropirate block if there is one, otherwise returns null.</returns>
        /// <remarks>Subject to <see cref="IndexOutOfRangeException"/> errors when <see cref="GridPointIsInsideArray(Vector3Int)"/> is false.</remarks>
        public Block GetBlockAt(Vector3Int gridPosition) {
            return this[gridPosition + offset];
        }

        /// <summary>Same functionality as <see cref="GetBlockAt(Vector3Int)"/>, but is not subject to <see cref="IndexOutOfRangeException"/> errors.</summary>
        /// <param name="gridPosition">The position in the grid to check.</param>
        /// <returns>Returns null if <see cref="GridPointIsInsideArray(Vector3Int)"/> fails, otherwise returns <see cref="GetBlockAt(Vector3Int)"/>.</returns>
        public Block TryGetBlockAt(Vector3Int gridPosition) {
            var arrayPosition = gridPosition + offset;
            return ArrayPointIsInsideArray(arrayPosition) ? this[arrayPosition] : null;
        }

        /// <summary>Uses <see cref="LocalPointToGrid(Vector3)"/> to call <see cref="TryGetBlockAt(Vector3)"/>.</summary>
        /// <param name="localPosition">The position in the grid's transform to check.</param>
        /// <returns>Returns null if <see cref="GridPointIsInsideArray(Vector3Int)"/> fails, otherwise returns <see cref="GetBlockAt(Vector3Int)"/>.</returns>
        public Block TryGetBlockAt(Vector3 localPosition) {
            return TryGetBlockAt(LocalPointToGrid(localPosition));
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
                if(GridPointIsInsideArray(neighborPosition)) {
                    neighbors[i] = GetBlockAt(neighborPosition);
                } else {
                    neighbors[i] = null;
                }
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
        public Vector3Int LocalPointToGrid(Vector3 localPoint) {
            return new Vector3Int(Mathf.RoundToInt(localPoint.x), Mathf.RoundToInt(localPoint.y), Mathf.RoundToInt(localPoint.z));
        }

        /// <summary>Converts a world point into grid space for this <see cref="BlockGrid"/>.</summary>
        /// <param name="worldPoint">Some point in the world.</param>
        /// <returns>Returns a <see cref="Vector3Int"/> that represents a point in grid space.</returns>
        public Vector3Int WorldPointToGrid(Vector3 worldPoint) {
            return LocalPointToGrid(transform.InverseTransformPoint(worldPoint));
        }

        /// <summary>Aligns the given world point to the grid.</summary>
        /// <param name="worldPoint">Some point in the world.</param>
        /// <returns>Returns a <see cref="Vector3"/> that represents a grid point in world space.</returns>
        public Vector3 AlignWorldPoint(Vector3 worldPoint) {
            return GridPointToWorld(WorldPointToGrid(worldPoint));
        }

        #endregion
        #region Get world point from grid.

        /// <summary>Converts a point on this <see cref="BlockGrid"/>'s grid space into local space.</summary>
        /// <param name="gridPoint">Some coordinate on the grid.</param>
        /// <returns>Returns a <see cref="Vector3"/> that represents a point in local space.</returns>
        public Vector3 GridPointToLocal(Vector3Int gridPoint) {
            return gridPoint;
        }

        /// <summary>Converts a point on this <see cref="BlockGrid"/>'s grid space into world space.</summary>
        /// <param name="gridPoint">Some coordinate on the grid.</param>
        /// <returns>Returns a <see cref="Vector3"/> that represents a point in world space.</returns>
        public Vector3 GridPointToWorld(Vector3Int gridPoint) {
            return transform.TransformPoint(GridPointToLocal(gridPoint));
        }

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
            var arrayPosition = gridPosition + offset;
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
            arrayPosition = gridPosition + offset;
            boundsInArray = arrayPosition + block.Dimensions;

            //Now that we know the block will fit, set the refrences in the array.
            for(int x = arrayPosition.x; x < boundsInArray.x; x++) {
                for(int y = arrayPosition.y; y < boundsInArray.y; y++) {
                    for(int z = arrayPosition.z; z < boundsInArray.z; z++) {

                        //Check for BlockOverlapException.
                        if(this[x, y, z] != null) throw new BlockOverlapException(
                            this, new Vector3Int(x, y, z), this[x, y, z], block, true, z == arrayPosition.z, true);

                        //Set the refrence.
                        array[x, y, z] = block;

                    }
                }
            }

            //Say hello to the neighbors!
            //We assume GetDimensionInfromation() has been called.
            foreach(Vector3Int relativePoint in block.neighboringPoints) {
                var neighboringGridPoint = relativePoint + gridPosition;
                var neighbor = TryGetBlockAt(neighboringGridPoint);
                if(neighbor != null && neighbor != block) //As long as somebody's home... && who isn't you.
                    neighbor.OnNearbyPieceAdded(gridPosition - neighbor.gridPosition);
            }

            //Tell the block that we're done and that it's <<here>>.
            block.SetGrid(this, gridPosition);

        }

        /// <summary>Instantiates a prefab, defines important values such as transform data and name then calls <see cref="Block.GetDimensionInformation()"/>,
        /// then places it on the grid at the given position with <see cref="PlaceBlock(Block, Vector3Int)"/>.</summary>
        /// <param name="prefab">Prefab of the "blank" GameObject.</param>
        /// <param name="gridPosition">Grid position to place the block at.</param>
        /// <remarks>Subject to clipping if <see cref="CanPlaceBlock(Block, Vector3Int)"/> is false.</remarks>
        public void PlaceNewBlock(GameObject prefab, Vector3Int gridPosition, Quaternion worldRotation) {
            GameObject blockGameObject = Instantiate(prefab);
            blockGameObject.transform.rotation = worldRotation;
            blockGameObject.name = prefab.name;
            Block block = blockGameObject.GetComponent<Block>();
            block.GetDimensionInformation();
            blockGameObject.SetActive(true);
            PlaceBlock(block, gridPosition);
        }

        /// <summary>When a <see cref="Block"/> is disabled, it is removed from the grid. When it is enabled it is re-added through this function.</summary>
        /// <param name="block">The block that is being enabled.</param>
        /// <param name="gridPosition">The position in the grid that the block is in.</param>
        public void PlaceEnablingBlock(Block block, Vector3Int gridPosition) {
            block.GetDimensionInformation();
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
                Mathf.RoundToInt(localPosition.z)
            ));
        }

        #endregion
        #region Remove block at position.

        /// <summary>Finds the <see cref="Block"/> at the given position then locates and removes all refrences in <see cref="array"/></summary>
        /// <param name="gridPosition">The position in the grid to look at.</param>
        /// <param name="callRemoveFromGrid">Call <see cref="Block."/></param>
        /// <returns>Returns the block that was removed from the grid.</returns>
        public Block RemoveBlock(Vector3Int gridPosition, bool callRemoveFromGrid) {

            //Position of given point.
            var arrayPosition = gridPosition + offset;
            Block selectedBlock = this[arrayPosition];
            if(selectedBlock == null) return null;

            //Take your shit outta here.
            // - remove all refrences to this block from the array.
            foreach(Vector3Int relativePosition in selectedBlock.insidePoints) {
                var insideArrayPosition = relativePosition + selectedBlock.gridPosition + offset;
                this[insideArrayPosition] = null;
            }

            //Say goodbye to the neighbors!
            // - neighbor.OnNerbyPieceRemoved()
            foreach(Vector3Int relativePosition in selectedBlock.insidePoints) {
                var neighboringGridPosition = relativePosition + selectedBlock.gridPosition;
                foreach(Block neighbor in GetNeighborsAt(neighboringGridPosition)) {
                    if(neighbor != null && neighbor != selectedBlock) //As long as somebody's home... && who isn't you.
                        neighbor.OnNearbyPieceRemoved(neighboringGridPosition - neighbor.gridPosition);
                }
            }

            //Forget you were ever here :(
            // - call Block.RemoveFromGrid()
            if(callRemoveFromGrid)
                selectedBlock.RemovedFromGrid();

            return selectedBlock;
        }

        public Block FindAndRemoveBlock(ref Block block) {

            foreach(Vector3Int arrayPoint in ArrayPoints) {

            }

            return null;
        }

        #endregion
        #region Expand array size.

        /// <summary>Expands the <see cref="array"/> by the given amount. The sign of the inputs are the direction to expand the grid.</summary>
        /// <param name="amount">Amount to increase the size of <see cref="array"/> in each axis direction.</param>
        /// <remarks>Since an <see cref="int"/> can only have one sign (maybe when we're using quantum computers), this function
        /// cannot expand the grid in two complete opposite directions at the same time.</remarks>
        /// <seealso cref="ChangeArrayDimensions(Vector3Int, Vector3Int)"/>
        public void ExpandArrayDimensions(Vector3Int amount) {
            var arrayOld = array;
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
            offset -= negOffset;
            array = arrayNew;

        }

        /// <summary>Expands the <see cref="array"/> by the given amounts.
        /// All values of "amountPos" should be zero or positive and all values of amountNeg should be zero or negative.</summary>
        /// <param name="amountPos">Amount to increase the size of <see cref="array"/> in each positive axis direction.</param>
        /// <param name="amountNeg">Amount to increase the size of <see cref="array"/> in each negative axis direction.</param>
        public void ChangeArrayDimensions(Vector3Int amountPos, Vector3Int amountNeg) {
            var arrayOld = array;
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
            offset -= negOffset;
            array = arrayNew;

        }


        #endregion
        #endregion

        #endregion

        #region Enumerable
        //Basically makes this object compatible with a foreach loop.

        /// <summary>Gets the universal iterable of this <see cref="BlockGrid"/>.
        /// Basically, since this is here you can use this with a "foreach" loop.</summary>
        /// <returns>Returns the <see cref="IEnumerator"/> of <see cref="array"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();

        /// <summary>Gets the universal <see cref="Vector3Int"/> (array space) iterable of this <see cref="BlockGrid"/>.
        /// Basically, use this with a "foreach" loop.</summary>
        /// <returns>Returns the <see cref="IEnumerator"/> of <see cref="array"/>.</returns>
        public CodeTools.Enumerators.ArrayPointsEnumerator ArrayPoints => new CodeTools.Enumerators.ArrayPointsEnumerator(Size);

        #endregion

        #region Exceptions
        /// <summary>Exception for when a <see cref="Block"/> was about to be / was overlapped by another <see cref="Block"/> in <see cref="array"/>.</summary>
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
            /// <param name="stoppedOverwrite">Was this stopped before data in <see cref="array"/> was changed?</param>
            /// <param name="stoppedCompletely">Was this stopped before ANY data in <see cref="array"/> was changed?</param>
            /// <param name="tryToFix">O(n) operation, where n = volume. Removes all refrences to the block on the affected grid, then Destroys the block.</param>
            public BlockOverlapException(BlockGrid grid, Vector3Int arrayPosition, Block native, Block intruder, bool stoppedOverwrite, bool stoppedCompletely, bool tryToFix) : base(
                "The Block \"" + intruder.gameObject.name + "\" in the \"" + grid.gameObject.name + "\" BlockGrid was trying to overlap the Block \"" + native.gameObject.name +
                "\" at the grid point " + (arrayPosition + grid.offset) + " / array point " + arrayPosition + ". If this was saved, check for 'ghost' blocks." + 
                (stoppedCompletely ? "\nThis was interrupted before any data in the BlockGrid.array was changed."
                : stoppedOverwrite ? "\nData on empty points may have been changed, although existing data was not changed." : "") +
                (tryToFix ? "\nAn attempt at a fix has been made." : "")
            ) {

                if(tryToFix) {
                    Vector3Int size = grid.Size;
                    for(int x = 0; x < size.x; x++)
                        for(int y = 0; y < size.y; y++)
                            for(int z = 0; z < size.z; z++)
                                if(grid.array[x, y, z] == intruder)
                                    grid.array[x, y, z] = null;
                }

            }

        }
        #endregion

    }

    /// <summary>Represents a center and magnitude of mass.</summary>
    [Serializable] public struct Mass {

        /// <summary>The magnitude of this mass in the arbitrary units of grams.</summary>
        public ulong magnitude; //magnitude of myass lol
        /// <summary>The local position of this mass.</summary>
        public Vector3 position;
        /// <summary>Creates a new <see cref="Mass"/> with the given properties.</summary>
        /// <param name="magnitude">(<see cref="magnitude"/>) The magnitude of this mass in the arbitrary units of grams.</param>
        /// <param name="position">(<see cref="position"/>) The local position of this mass.</param>
        public Mass(ulong magnitude, Vector3 position) {
            this.magnitude = magnitude;
            this.position = position;
        }

        #region Shorthands

        /// <summary>Shorthand for "<c>new Mass(0, Vector3.zero)</c>".</summary>
        /// <remarks>Reccomended usecase is for setting a default value in like a constructor or something.</remarks>
        public static Mass zero = new Mass(0, Vector3.zero);
        /// <summary>Shorthand for "<c>new Mass(1, Vector3.zero)</c>".</summary>
        /// <remarks>Although not reccomended, this is useful in combination with <see cref="operator *(Mass, ulong)"/>.</remarks>
        public static Mass one = new Mass(1, Vector3.zero);

        #endregion

        #region Operators

        #region Addition

        // - Add Mass - //
        /// <summary>An addition operator that avoids overflow by checking if the result is less than either of the original values.
        /// If there is an overflow, the result will be <see cref="ulong.MaxValue"/>.
        /// <para/>Also gives the appropriate <see cref="position"/> to the return value.</summary>
        public static Mass operator +(Mass left, Mass right) {
            ulong resultingMagnitude = left.magnitude + right.magnitude;
            if(resultingMagnitude < left.magnitude || resultingMagnitude < right.magnitude)
                resultingMagnitude = ulong.MaxValue;
            var resultingPosition = Vector3.Lerp(left.position, right.position,
                resultingMagnitude == 0 ? .50f : (float)(right.magnitude /(double) resultingMagnitude));
            return new Mass(resultingMagnitude, resultingPosition);
        }

        // - Add Value - //
        /// <summary>An addition operator that avoids overflow by checking if the result is less than either of the original values.
        /// If there is an overflow, the result will be <see cref="ulong.MaxValue"/>.
        /// <para/>The <see cref="position"/> does not change.</summary>
        /// <remarks>There is no mirroring "<c>+(ulong value, Mass mass)</c>" because it makes sense to add a number to a weighted position, but not to add a weighted position to a number.</remarks>
        public static Mass operator +(Mass mass, ulong value) {
            ulong result = mass.magnitude + value;
            if(result < mass.magnitude || result < value)
                result = ulong.MaxValue;
            return new Mass(result, mass.position);
        }

        // - Add Vector - //
        /// <summary>Shifts the center of mass by the given vector through addition.</summary>
        public static Mass operator +(Mass mass, Vector3 vector) { return new Mass(mass.magnitude, mass.position + vector); }

        #endregion

        #region Subtraction

        // - Subtract Mass - //
        /// <summary>A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.
        /// If there is an underflow, the result will be <see cref="ulong.MinValue"/>.
        /// <para/>Also gives the appropriate <see cref="position"/> to the return value.</summary>
        public static Mass operator -(Mass left, Mass right) {
            ulong resultingMagnitude = left.magnitude - right.magnitude;
            if(resultingMagnitude > left.magnitude || resultingMagnitude < right.magnitude)
                resultingMagnitude = ulong.MinValue;
            var resultingPosition = Vector3.Lerp(left.position, right.position,
                resultingMagnitude == 0 ? .50f : (float)(left.magnitude /(double) resultingMagnitude));
            return new Mass(resultingMagnitude, resultingPosition);
        }

        // - Subtract Value - //
        /// <summary>A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.
        /// If there is an underflow, the result will be <see cref="ulong.MinValue"/>.
        /// <para/>The <see cref="position"/> does not change.</summary>
        /// <remarks>There is no mirroring "<c>-(ulong value, Mass mass)</c>" because it makes sense to subtract a number from a weighted position, but not to subtract a weighted position from a number.</remarks>
        public static Mass operator -(Mass mass, ulong value) {
            ulong result = mass.magnitude - value;
            if(result < mass.magnitude || result < value)
                result = ulong.MaxValue;
            return new Mass(result, mass.position);
        }

        // - Subtract Vector - //
        /// <summary>Shifts the center of mass by the given vector through subtraction.</summary>
        public static Mass operator -(Mass mass, Vector3 vector) { return new Mass(mass.magnitude, mass.position - vector); }

        #endregion

        #region Multiplication & Division

        // - Multiply by Value - //
        /// <summary>A multiplication operator that avoids overflow by checking if the result is less than either of the original values.
        /// If there is an overflow, the result will be <see cref="ulong.MaxValue"/>.
        /// <para/>The <see cref="position"/> does not change.</summary>
        public static Mass operator *(Mass mass, ulong value) {
            ulong result = mass.magnitude * value;
            if(result < mass.magnitude || result < value)
                result = ulong.MaxValue;
            return new Mass(result, mass.position);
        }



        // - Divide by Value - //
        /// <summary>A division operator that avoids underflow by checking if the result is greater than either of the original values.
        /// If there is an underflow, the result will be <see cref="ulong.MinValue"/>.
        /// <para/>The <see cref="position"/> does not change.</summary>
        public static Mass operator /(Mass mass, ulong value) {
            ulong result = mass.magnitude / value;
            if(result > mass.magnitude || result > value)
                result = ulong.MaxValue;
            return new Mass(result, mass.position);
        }

        #endregion

        #region Comparisions

        /// <summary>Compares the two <see cref="magnitude"/>s.</summary>
        public static bool operator <(Mass left, Mass right) {
            return left.magnitude < right.magnitude;
        }

        /// <summary>Compares the two <see cref="magnitude"/>s.</summary>
        public static bool operator >(Mass left, Mass right) {
            return left.magnitude > right.magnitude;
        }



        /// <summary>Checks if both given <see cref="Mass"/> objects have equal <see cref="magnitude"/>s and equal <see cref="position"/>s.</summary>
        public static bool operator ==(Mass mass1, Mass mass2) {
            return mass1.magnitude == mass2.magnitude && mass1.position == mass2.position;
        }

        /// <summary>Checks if both given <see cref="Mass"/> objects have unequal <see cref="magnitude"/>s or unequal <see cref="position"/>s.</summary>
        public static bool operator !=(Mass mass1, Mass mass2) {
            return mass1.magnitude != mass2.magnitude || mass1.position != mass2.position;
        }

        /// <summary>Auto-generated by Visual Studio.</summary>
        public override bool Equals(object obj) {
            return obj is Mass mass &&
                   magnitude == mass.magnitude &&
                   position.Equals(mass.position);
        }

        /// <summary>Auto-generated by Visual Studio.</summary>
        public override int GetHashCode() {
            var hashCode = 813208763;
            hashCode = hashCode * -1521134295 + magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(position);
            return hashCode;
        }

        #endregion

        /// <summary>"[<see cref="magnitude"/>]g at [<see cref="position"/>]"</summary>
        public override string ToString() {
            return string.Format("{0}g at {1}", magnitude, position);
        }

        #endregion

    }

}
