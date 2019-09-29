using System;
using System.Collections;
using UnityEngine;
using KineticEnergy.CodeTools;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>Base class for all blocks.</summary>
    public abstract class Block : MonoBehaviour {

        public virtual void OnSetGrid() { }
        public virtual void OnNearbyPieceAdded(Vector3Int relativeGridPosition) { }
        public virtual void OnNearbyPieceRemoved(Vector3Int relativeGridPosition) { }
        public abstract bool SideIsOpaque(Vector3Int localPosition, Face face);

        public void OnEnable() {
            if(grid != null) grid.PlaceEnablingBlock(this, transform.localPosition);
            StasisAwake();
        }
        public virtual void StasisAwake() { }

        public void OnDisable() {
            if(grid != null) grid.RemoveBlock(gridPosition);
            StasisAsleep();
        }
        public virtual void StasisAsleep() { }

        #region Variables, Constants, and Setup Methods

        /// <summary>
        /// Shortcut for "<c>gameObject.name</c>".
        /// </summary>
        public string Name {
            get { return gameObject.name;  }
            set { gameObject.name = value; }
        }

        /// <summary>
        /// The dimentions of this block. Runs <see cref="GetDimensionInformation()"/> when set. Nothing becides "return" is done on get.
        /// </summary>
        public Vector3Int Dimensions {
            get { return m_dimentions; }
            set {
                m_dimentions = value;
                GetDimensionInformation();
            }
        } private Vector3Int m_dimentions = Vector3Int.one;

        /// <summary>
        /// The <see cref="Ships.Mass"/> of this block. Updates the <see cref="grid"/>'s <see cref="Mass"/> when set. Nothing becides "return" is done on get.
        /// </summary>
        public Mass Mass {
            get { return m_mass; }
            set {
                if(grid != null) {
                    grid.Mass -= m_mass + gridPosition;
                    grid.Mass += value + gridPosition;
                    Debug.LogFormat("Old: {0}, New: {1}", m_mass, value);
                }
                m_mass = value;
            }
        }
        [SerializeField]
        private Mass m_mass = new Mass(1000, Vector3.zero);

        public void SetMassFromInspector(Mass newMass) {
            if(grid != null && grid.rigidbody != null) Mass = newMass;
            else m_mass = newMass;
        }

        /// <summary>
        /// Sets transform position and parent, amung some references inside Block.
        /// </summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to set this object's <see cref="grid"/> as.</param>
        /// <param name="gridPosition">The position in the <see cref="BlockGrid"/> this <see cref="Block"/> will be moved to.</param>
        public void SetGrid(BlockGrid grid, Vector3Int gridPosition) {
            if(this.grid != null && this.grid != grid) this.grid.Mass -= Mass + this.gridPosition;
            grid.Mass += Mass + gridPosition;
            this.grid = grid;
            this.gridPosition = gridPosition;
            arrayPosition = gridPosition + grid.offset;
            transform.parent = grid.gameObject.transform;
            transform.localPosition = gridPosition;
            OnSetGrid();
        }

        /// <summary>
        /// Sets important variables inside <see cref="Block"/>, which would otherwise be innacurate or null.
        /// </summary>
        /// <remarks>
        /// Should be called when this object is created or when <see cref="m_dimentions"/> (not <see cref="Dimensions"/>) is changed.
        /// </remarks>
        public void GetDimensionInformation() {
            xArea = Dimensions.x * Dimensions.x;
            yArea = Dimensions.y * Dimensions.y;
            zArea = Dimensions.z * Dimensions.z;
            UpdateInsidePoints();
            UpdateNeighboringPoints();
        }

        /// <summary>Shortcut for "<c><see cref="Dimensions"/> - <see cref="Vector3Int.one"/></c>".</summary>
        public Vector3Int GridCorner => Dimensions - Vector3Int.one;

        public int xArea { get; private set; }
        public int yArea { get; private set; }
        public int zArea { get; private set; }
        /// <summary>Calculates the surface area.</summary>
        public int SurfaceArea => (xArea + yArea + zArea) * 2;

        /// <summary>The <see cref="BlockGrid"/> associated with this block.</summary>
        public BlockGrid grid;

        /// <summary>
        /// The position of this block in its <see cref="BlockGrid"/> grid space.
        /// </summary>
        /// <seealso cref="grid"/>
        /// <seealso cref="BlockGrid.offset"/>
        public Vector3Int gridPosition { get; internal set; }

        /// <summary>
        /// The position of this block in its <see cref="BlockGrid"/> array space.
        /// </summary>
        /// <seealso cref="grid"/>
        /// <seealso cref="BlockGrid.offset"/>
        public Vector3Int arrayPosition { get; internal set; }

        #endregion

        #region "Points" Arrays

        #region Neighboring Points XYZ
        public Vector3Int[] neighboringPointsX;
        private void UpdateNeighboringPointsX() {
            Vector3Int[] npX = new Vector3Int[xArea * 2];
            for(int y = 0; y < Dimensions.y; y++) {
                for(int z = 0; z < Dimensions.z; z++) {
                    npX[y + z + xArea] = new Vector3Int(-1, y, z);
                    npX[y + z] = new Vector3Int(Dimensions.x, y, z);
                }
            }
            neighboringPointsX = npX;
        }
        public Vector3Int[] neighboringPointsY;
        private void UpdateNeighboringPointsY() {
            Vector3Int[] npY = new Vector3Int[yArea * 2];
            for(int x = 0; x < Dimensions.x; x++) {
                for(int z = 0; z < Dimensions.z; z++) {
                    npY[x + z + yArea] = new Vector3Int(x, -1, z);
                    npY[x + z] = new Vector3Int(x, Dimensions.y, z);
                }
            }
            neighboringPointsY = npY;
        }
        public Vector3Int[] neighboringPointsZ;
        private void UpdateNeighboringPointsZ() {
            Vector3Int[] npZ = new Vector3Int[zArea * 2];
            for(int x = 0; x < Dimensions.x; x++) {
                for(int y = 0; y < Dimensions.y; y++) {
                    npZ[y + x + zArea] = new Vector3Int(x, y, -1);
                    npZ[y + x] = new Vector3Int(x, y, Dimensions.z);
                }
            }
            neighboringPointsZ = npZ;
        }
        #endregion

        #region Neighboring Points
        /// <summary>
        /// All of the grid cell locations (from a local reference) that touch this block.
        /// Null until <see cref="GetDimensionInformation()"/> or <see cref="UpdateNeighboringPoints()"/> is called.
        /// </summary>
        public Vector3Int[] neighboringPoints = new Vector3Int[0];

        /// <summary>Assigns to <see cref="neighboringPoints"/>.</summary>
        private void UpdateNeighboringPoints() {
            UpdateNeighboringPointsX(); UpdateNeighboringPointsY(); UpdateNeighboringPointsZ();
            Vector3Int[] npX = neighboringPointsX, npY = neighboringPointsY, npZ = neighboringPointsZ;
            neighboringPoints = new Vector3Int[npX.Length + npY.Length + npZ.Length];
            for(int i = 0; i < neighboringPoints.Length; i++) {

                /**/ if(i < npX.Length) // x
                    neighboringPoints[i] = npX[i];

                else if(i < npX.Length + npY.Length) // y
                    neighboringPoints[i] = npY[i - npX.Length];

                else if(i < npX.Length + npY.Length + npZ.Length) // z
                    neighboringPoints[i] = npZ[i - npZ.Length - npY.Length];

            }
        }

        #endregion

        #region Inside Points
        /// <summary>All of the cells in the grid that make up this block (from a local point of view). Null until 'GetLocalInformation()' or 'UpdateInsidePieces()' is called.</summary>
        public Vector3Int[,,] insidePoints;
        /// <summary>Assigns to 'insidePieces'.</summary>
        private void UpdateInsidePoints() {
            var points = new Vector3Int[Dimensions.x, Dimensions.y, Dimensions.z];
            for(int x = 0; x < Dimensions.x; x++)
                for(int y = 0; y < Dimensions.y; y++)
                    for(int z = 0; z < Dimensions.z; z++)
                        points[x, y, z] = new Vector3Int(x, y, z);
            insidePoints = points;
        }
        #endregion

        #endregion

        #region Face Checks
        /// <summary>
        /// Defines all of the faces that a <see cref="Block"/> can have.
        /// </summary>
        public enum Face { PosX, NegX, PosY, NegY, PosZ, NegZ }

        /// <summary>
        /// Checks all faces and returns an <see cref="int"/> that represents a set of booleans of which faces are covered.
        /// </summary>
        /// <returns>
        /// Returns a byte that represents a set of booleans for which faces are shown. Starting from the right-most binary digit: 1-right, 2-left, 3-top, 4-bottom, 5-front, 6-back
        /// </returns>
        public byte WhichFacesShown() {
            byte result = 0b000000;
                          //BFBTLR

            var invRotation = Quaternion.Inverse(transform.localRotation);

            { // Right, Left
                bool posShown = false; bool negShown = false;
                for(int i = 0; i < xArea; i++) {
                    if(grid.TryGetBlockAt(RelativeGridToGrid(gridPosition + neighboringPointsX[i], invRotation)) == null)
                        posShown = true;
                    if(grid.TryGetBlockAt(RelativeGridToGrid(gridPosition + neighboringPointsX[i + xArea], invRotation)) == null)
                        negShown = true;
                }
                if(posShown) result += 0b000001;
                if(negShown) result += 0b000010;
            }
            { // Top, Bottom
                bool posShown = false; bool negShown = false;
                for(int i = 0; i < yArea; i++) {
                    if(grid.TryGetBlockAt(RelativeGridToGrid(gridPosition + neighboringPointsY[i], invRotation)) == null)
                        posShown = true;
                    if(grid.TryGetBlockAt(RelativeGridToGrid(gridPosition + neighboringPointsY[i + yArea], invRotation)) == null)
                        negShown = true;
                }
                if(posShown) result += 0b000100;
                if(negShown) result += 0b001000;
            }
            { // Front, Back
                bool posShown = false; bool negShown = false;
                for(int i = 0; i < zArea; i++) {
                    if(grid.TryGetBlockAt(RelativeGridToGrid(gridPosition + neighboringPointsZ[i], invRotation)) == null)
                        posShown = true;
                    if(grid.TryGetBlockAt(RelativeGridToGrid(gridPosition + neighboringPointsZ[i + zArea], invRotation)) == null)
                        negShown = true;
                }
                if(posShown) result += 0b010000;
                if(negShown) result += 0b100000;
            }

            return result;
        }

        #endregion

        /// <summary>A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.</summary>
        /// <param name="relativeGridPosition">The relative grid position to translate.</param>
        /// <returns>Returns a grid position.</returns>
        public Vector3 RelativeGridToGrid(Vector3 relativeGridPosition) { return (Quaternion.Inverse(transform.localRotation) * relativeGridPosition) + transform.localPosition; }
        /// <summary>A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.</summary>
        /// <param name="relativeGridPosition">The relative grid position to translate.</param>
        /// <param name="inverseLocalRotation">The <see cref="Quaternion.Inverse(Quaternion)"/> of the <see cref="Transform.localRotation"/>.</param>
        /// <returns>Returns a grid position.</returns>
        public Vector3 RelativeGridToGrid(Vector3 relativeGridPosition, Quaternion inverseLocalRotation) { return (inverseLocalRotation * relativeGridPosition) + transform.localPosition; }

    }

    /// <summary>Class used to store and show/hide the six faces of an <see cref="OpaqueBlock"/>.</summary>
    [Serializable]
    public class Faces {

        public GameObject right, left, top, bottom, front, back;

        /// <summary>
        /// Uses the given enabled faces to use <see cref="GameObject.SetActive(bool)"/>.
        /// </summary>
        /// <param name="enabledFlaces">Represents a line of booleans (starting on the right side) that correspond to Right, Left, Top, Bottom, Front, Back.</param>
        public void ToggleFaces(byte enabledFlaces) {
            right.SetActive((enabledFlaces & 0b000001) != 0);
            left.SetActive((enabledFlaces & 0b000010) != 0);
            top.SetActive((enabledFlaces & 0b000100) != 0);
            bottom.SetActive((enabledFlaces & 0b001000) != 0);
            front.SetActive((enabledFlaces & 0b010000) != 0);
            back.SetActive((enabledFlaces & 0b100000) != 0);
        }

    }

}
