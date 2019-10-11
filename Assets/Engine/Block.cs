using System;
using System.Collections;
using UnityEngine;
using KineticEnergy.CodeTools;
using KineticEnergy.Content;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>Base class for all blocks.</summary>
    public abstract class Block : MonoBehaviour {

        public virtual void OnGridSet() { }
        public virtual void OnNearbyPieceAdded(Vector3Int relativeGridPosition) { }
        public virtual void OnNearbyPieceRemoved(Vector3Int relativeGridPosition) { }
        public abstract bool SideIsOpaque(Vector3Int localPosition, Face face);

        public void OnEnable() {
            if(grid != null) grid.PlaceEnablingBlock(this, transform.localPosition);
            StasisAwake();
        }
        public virtual void StasisAwake() { }

        public void OnDisable() {
            if(grid != null) grid.RemoveBlock(gridPosition, false);
            StasisAsleep();
        }
        public virtual void StasisAsleep() { }

        /// <summary>Method called when prefab is loaded. Use this if you wanna do anything fancy with the prefab.</summary>
        public virtual void PrefabSetup() { }

        #region Variables, Constants, and Setup Methods

        /// <summary>Shortcut for "<c>gameObject.name</c>".</summary>
        public string Name {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        /// <summary>The dimentions of this block. Runs <see cref="GetDimensionInformation()"/> when set. Nothing becides "return" is done on get.</summary>
        public Vector3Int Dimensions {
            get { return m_dimentions; }
            set {
                m_dimentions = value;
                GetDimensionInformation();
            }
        }
        private Vector3Int m_dimentions = Vector3Int.one;

        /// <summary>The <see cref="Ships.Mass"/> of this block.</summary>
        /// <remarks>Updates the <see cref="grid"/>'s <see cref="Mass"/> when set.
        /// <para/>Nothing becides "return" is done on get.</remarks>
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

        /// <summary>Method for changing <see cref="Mass"/> from a Unity Inspector.</summary>
        /// <param name="newMass">The new <see cref="Ships.Mass"/> of the <see cref="Block"/>.</param>
        public void SetMassFromInspector(Mass newMass) {
            if(grid != null && grid.rigidbody != null) Mass = newMass;
            else m_mass = newMass;
        }

        /// <summary>Checks if <see cref="RemovedFromGrid"/> need to be called,
        /// then sets transform position and parent, amung some other value inside <see cref="Block"/>.</summary>
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
            OnGridSet();
        }

        /// <summary>Should be called after something like <see cref="BlockGrid.RemoveBlock"/> is called,
        /// but is redundant if <see cref="SetGrid"/> is about to be called.</summary>
        /// <remarks>Does not check if <see cref="grid"/> is null
        /// so could be subject to <see cref="NullReferenceException"/>s if <c>grid != null</c> isn't checked first.</remarks>
        public void RemovedFromGrid() {
            grid.Mass -= Mass;
            grid = null;
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

        /// <summary>Shortcut for <c>"Dimensions - Vector3Int.one"</c>.</summary>
        /// <seealso cref="Dimensions"/>
        /// <seealso cref="Vector3Int.one"/>
        public Vector3Int GridCorner => Dimensions - Vector3Int.one;

        public int xArea { get; private set; }
        public int yArea { get; private set; }
        public int zArea { get; private set; }
        /// <summary>Calculates the surface area.</summary>
        public int SurfaceArea => (xArea + yArea + zArea) * 2;

        /// <summary>The <see cref="BlockGrid"/> associated with this block.</summary>
        public BlockGrid grid;

        /// <summary>The position of this block in its <see cref="BlockGrid"/> grid space.</summary>
        /// <seealso cref="grid"/>
        /// <seealso cref="BlockGrid.offset"/>
        public Vector3Int gridPosition { get; internal set; }

        /// <summary>The position of this block in its <see cref="BlockGrid"/> array space.</summary>
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

                /**/
                if(i < npX.Length) // x
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
        public enum Face {
            PosX = +1, Right = +1,
            NegX = -1, Left = -1,
            PosY = +2, Up = +2,
            NegY = -2, Down = -2,
            PosZ = +3, Front = +3,
            NegZ = -3, Back = -3
        }

        /// <summary>Checks all faces and returns an <see cref="int"/> that represents a set of booleans of which faces are covered.</summary>
        /// <returns>Returns a byte that represents a set of booleans for which faces are shown. Starting from the right-most binary digit: 1=right, 2=left, 3=top, 4=bottom, 5=front, 6=back.</returns>
        public byte WhichFacesShown() {
            byte result = 0b000000;
            ////////////////BFBTLR/

            //maybe make this stored?
            var invRotation = Quaternion.Inverse(transform.localRotation);

            m_WhichFacesShown(ref result, this, invRotation, grid,
                neighboringPointsX, xArea,
                Face.PosX, 0b000001,
                Face.NegX, 0b000010);

            m_WhichFacesShown(ref result, this, invRotation, grid,
                neighboringPointsY, yArea,
                Face.PosY, 0b000100,
                Face.NegY, 0b001000);

            m_WhichFacesShown(ref result, this, invRotation, grid,
                neighboringPointsZ, zArea,
                Face.PosZ, 0b010000,
                Face.NegZ, 0b100000);

            return result;
        }

        private static void m_WhichFacesShown(ref byte result, Block block, Quaternion invRotation, BlockGrid grid,
            Vector3Int[] neighboringPoints, int area,
            Face facePos, byte posTrue,
            Face faceNeg, byte negTrue) {

            bool posShown = false, negShown = false;
            for(int i = 0; i < area; i++) {

                if((posShown == false) &&
                   !grid.SideIsOpaque(
                    grid.LocalPointToGrid(
                    block.LocalBlockToLocalGrid(neighboringPoints[i], invRotation)),
                    facePos)) posShown = true;

                if((negShown == false) &&
                   !grid.SideIsOpaque(
                    grid.LocalPointToGrid(
                    block.LocalBlockToLocalGrid(neighboringPoints[i + area], invRotation)),
                    faceNeg)) negShown = true;

                if(posShown && negShown) break;
            }
            if(posShown) result += posTrue;
            if(negShown) result += negTrue;

        }

        #endregion

        /// <summary>A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.</summary>
        /// <param name="relativeGridPosition">The relative grid position to translate.</param>
        /// <returns>Returns a grid position.</returns>
        public Vector3 LocalPointToLocalGrid(Vector3 relativeGridPosition)
            => (Quaternion.Inverse(transform.localRotation) * relativeGridPosition) + gridPosition;
        /// <summary>A relative grid position's origin is at the transform of the block, but does not account for rotation of the transform.</summary>
        /// <param name="relativeGridPosition">The relative grid position to translate.</param>
        /// <param name="inverseLocalRotation">The <see cref="Quaternion.Inverse(Quaternion)"/> of the <see cref="Transform.localRotation"/>.</param>
        /// <returns>Returns a grid position.</returns>
        public Vector3 LocalBlockToLocalGrid(Vector3 relativeGridPosition, Quaternion inverseLocalRotation)
            => (inverseLocalRotation * relativeGridPosition) + gridPosition;

        public static Vector3Int DirectionFromFace(Face face) {
            switch(face) {
                case Face.PosX: return new Vector3Int(1, 0, 0);
                case Face.NegX: return new Vector3Int(-1, 0, 0);
                case Face.PosY: return new Vector3Int(0, 1, 0);
                case Face.NegY: return new Vector3Int(0, -1, 0);
                case Face.PosZ: return new Vector3Int(0, 0, 1);
                case Face.NegZ: return new Vector3Int(0, 0, -1);
                default: return Vector3Int.zero;
            }
        }

    }

    /// <summary>Class used to store and show/hide the six faces of an <see cref="OpaqueBlock"/>.</summary>
    [Serializable]
    public class Faces {

        public GameObject right, left, top, bottom, front, back;

        /// <summary>Uses the given enabled faces to use <see cref="GameObject.SetActive(bool)"/>.</summary>
        /// <param name="enabledFlaces">Represents a line of booleans (starting on the right side) that correspond to Right, Left, Top, Bottom, Front, Back.</param>
        public void ToggleFaces(byte enabledFlaces) {
            if(right) right.SetActive((enabledFlaces & 0b000001) != 0);
            if(left) left.SetActive((enabledFlaces & 0b000010) != 0);
            if(top) top.SetActive((enabledFlaces & 0b000100) != 0);
            if(bottom) bottom.SetActive((enabledFlaces & 0b001000) != 0);
            if(front) front.SetActive((enabledFlaces & 0b010000) != 0);
            if(back) back.SetActive((enabledFlaces & 0b100000) != 0);
        }

    }

    public static class BlockAttributes {

        /// <summary>What does this attribute apply to?</summary>
        public enum AppliesTo {
            /// <summary>Signifies that this attribute is to only be applied to the original block, not the preview.</summary>
            Original,
            /// <summary>Signifies that this attribute is to only be applied to the preview block, not the original.</summary>
            Preview,
            /// <summary>Signifies that this attribute is to be applied to both the preview and the original.</summary>
            Both
        }

        /// <summary>What order should this attribute be applied?</summary>
        public enum Order {
            /// <summary>Signifies that this attribute must be amung the first to be applied.</summary>
            First = 1,
            /// <summary>Signifies that this attribute has normal priority.</summary>
            Default = 0,
            /// <summary>Signifies that this attribute must be amung the last to be applied.</summary>
            Last = -1
        }

        /// <summary>Abstract base class for <see cref="Block"/> and <see cref="BlockPreview"/> attributes.</summary>
        /// <remarks>For use with <see cref="Block"/> types only.
        /// Multiple attributes are allowed if the associated component can have duplicated.
        /// Attributes are not inherited.
        /// If an attribute gives you the option to set <see cref="targets"/>, then the attribute is useful for both.
        /// <para/>If you are implementing this class, do not check the given "isPreview" boolean inside <see cref="ApplyTo(GameObject, bool)"/>.
        /// This is done already by the <see cref="BlockAttribute"/> class, so if <see cref="ApplyTo"/> is being called then the attribute needs to be applied.
        /// Instead, use that given value to change values such as <see cref="Collider.isTrigger"/>.</remarks>
        public abstract class BlockAttribute : Attribute {

            /// <summary>The <see cref="BlockAttributes.Order"/> of this <see cref="BlockAttribute"/>.</summary>
            public abstract Order Order { get; }

            /// <summary>The types of </summary>
            public AppliesTo targets { get; protected set; } = AppliesTo.Both;
            /// <summary>Shorthand for "<c>targets == AppliesTo.Both || targets == AppliesTo.Original</c>".</summary>
            public bool AppliesToOriginal => targets == AppliesTo.Both || targets == AppliesTo.Original;
            /// <summary>Shorthand for "<c>targets == AppliesTo.Both || targets == AppliesTo.Preview</c>".</summary>
            public bool AppliesToPreview => targets == AppliesTo.Both || targets == AppliesTo.Preview;

            /// <summary>Applies this <see cref="BlockAttribute"/> to the given <see cref="GameObject"/>, regardless of <see cref="targets"/.></summary>
            /// <param name="block">The <see cref="Block"/> or <see cref="BlockPreview"/>'s <see cref="GameObject"/>.</param>
            /// <param name="asPreview">Is the given <see cref="GameObject"/> a <see cref="BlockPreview"/> or a <see cref="Block"/>?</param>
            public abstract void ApplyTo(GameObject block, bool asPreview);

            /// <summary>Applies this <see cref="BlockAttribute"/> if <see cref="AppliesToOriginal"/> is true.</summary>
            /// <param name="original">The <see cref="GameObject"/> to apply this <see cref="BlockAttribute"/> to.</param>
            public void ApplyToOriginal(GameObject original) { if(AppliesToOriginal) ApplyTo(original, false); }

            /// <summary>Applies this <see cref="BlockAttribute"/> if <see cref="AppliesToPreview"/> is true.</summary>
            /// <param name="preview">The <see cref="GameObject"/> to apply this <see cref="BlockAttribute"/> to.</param>
            public void ApplyToPreview(GameObject preview) { if(AppliesToPreview) ApplyTo(preview, true); }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class BasicInfo : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The name of the block.</summary>
            public readonly string name;
            /// <summary>The name of the preview.</summary>
            public readonly string name_preview;
            /// <summary>The value that <see cref="Block.Mass"/> will be set to.</summary>
            public readonly Mass mass;
            /// <summary>The value that <see cref="Block.Dimensions"/> will be set to.</summary>
            public readonly Vector3Int dimensions;

            /// <summary>Gives this block a display name, mass, and dimensions. Every block should have this.</summary>
            /// <param name="name">The name of the block.</param>
            /// <remarks>The name of the preview is <c>name + "_preview"</c>.</remarks>
            public BasicInfo(string name, int sizeX, int sizeY, int sizeZ, ulong mass, float comX, float comY, float comZ, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                this.name = name;
                name_preview = name + "_preview";
                dimensions = new Vector3Int(sizeX, sizeY, sizeZ);
                this.mass = new Mass(mass, new Vector3(comX, comY, comZ));
            }

            public override void ApplyTo(GameObject block, bool asPreview) {
                block.name = asPreview ? name_preview : name;
                if(!asPreview) {
                    var component = block.GetComponent<Block>();
                    if(component == null) throw new BlockAttributeException("'BasicInfo' attribute was being applied as \"original\", but a Block component could not be found.");
                    component.Dimensions = dimensions;
                    component.Mass = mass;
                }
            }
        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.BoxCollider"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class BoxCollider : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The <see cref="UnityEngine.BoxCollider.center"/>.</summary>
            public readonly Vector3 center;
            /// <summary>The <see cref="UnityEngine.BoxCollider.size"/>.</summary>
            public readonly Vector3 size;

            /// <summary>Gives this <see cref="Block"/> a <see cref="UnityEngine.BoxCollider"/>.</summary>
            /// <param name="centerX">The 'x' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="centerY">The 'y' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="centerZ">The 'z' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="sizeX">The 'x' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="sizeY">The 'y' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="sizeZ">The 'z' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            /// <remarks>Originals use this for collision. Previews use this for detecting if the block placement is valid.</remarks>
            public BoxCollider(float centerX, float centerY, float centerZ, float sizeX, float sizeY, float sizeZ, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                center = new Vector3(centerX, centerY, centerZ);
                size = new Vector3(sizeX, sizeY, sizeZ);
            }

            public override void ApplyTo(GameObject block, bool asPreview) {
                var collider = block.AddComponent<UnityEngine.BoxCollider>();
                collider.center = center;
                collider.size = size;
                collider.isTrigger = asPreview;

                var component = block.GetComponent<Block>();
                if(component is OpaqueBlock opaque) {
                    opaque.collider = collider;
                }

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a uniform <see cref="UnityEngine.Mesh"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class Mesh : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The file path to a '.obj' file for a mesh.</summary>
            public readonly string objPath;

            /// <summary>Gives this <see cref="Block"/> a <see cref="MeshFilter"/> with the given <see cref="UnityEngine.Mesh"/>.</summary>
            /// <param name="objPath"></param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Mesh(string objPath, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                this.objPath = objPath;
            }

            public override void ApplyTo(GameObject block, bool asPreview) {
                block.AddComponent<MeshFilter>().mesh = ContentLoader.GetObjFile(objPath);
                block.AddComponent<MeshRenderer>();
            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class Face : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The file path to a '.obj' file for a mesh.</summary>
            public readonly string objPath;

            /// <summary>The file path to a '.png' file for a diffuse texture.</summary>
            public readonly string diffusePath = null;
            /// <summary>The file path to a '.png' file for a normal texture.</summary>
            public readonly string normalPath = null;

            /// <summary>The <see cref="Block.Face"/> that this face is.</summary>
            public readonly Block.Face face;

            /// <summary>The local position of the face.</summary>
            public Vector3 position;
            /// <summary>The local rotation of the face.</summary>
            public Quaternion rotation;

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="Face"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="objPath">File path the the '.obj' file for the mesh of this block face.</param>
            /// <param name="diffusePath">File path to the '.png' file for the diffuse texture of this block face.</param>
            /// <param name="normalPath">File path the the '.png' file for the normal texture of this block face.</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Face(Block.Face face, string objPath, string diffusePath, string normalPath = null, AppliesTo targets = AppliesTo.Both)
                : this(face,

                      posX: (int)face == +1 ? +0.5f : (int)face == -1 ? -0.5f : 0,
                      posY: (int)face == +2 ? +0.5f : (int)face == -2 ? -0.5f : 0,
                      posZ: (int)face == +3 ? +0.5f : (int)face == -3 ? -0.5f : 0,

                      rotX: (int)face == +3 ? +90f : (int)face == -3 ? -90f
                          : (int)face == +2 ? 000f : (int)face == -2 ? 180f : 0,
                      rotY: 000000000000000000000000000000000000000000000000000,
                      rotZ: (int)face == +1 ? -90f : (int)face == -1 ? +90f : 0,

                      objPath, diffusePath, normalPath, targets) { }

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="Face"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="posX">The 'x' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posY">The 'y' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posZ">The 'z' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="rotX">The 'x' component of the relative rotation of this face. (ie. the spin around the x-axis)</param>
            /// <param name="rotY">The 'y' component of the relative rotation of this face. (ie. the spin around the y-axis)</param>
            /// <param name="rotZ">The 'z' component of the relative rotation of this face. (ie. the spin around the z-axis)</param>
            /// <param name="objPath">File path the the '.obj' file for the mesh of this block face.</param>
            /// <param name="diffusePath">File path to the '.png' file for the diffuse texture of this block face.</param>
            /// <param name="normalPath">File path the the '.png' file for the normal texture of this block face.</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Face(Block.Face face, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, string objPath, string diffusePath, string normalPath = null, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                this.face = face;
                position = new Vector3(posX, posY, posZ);
                rotation = Quaternion.Euler(rotX, rotY, rotZ);
                this.objPath = objPath;
                this.diffusePath = diffusePath;
                this.normalPath = normalPath;
            }

            public override void ApplyTo(GameObject block, bool asPreview) {

                //Create the face.
                var face = new GameObject("face");
                face.transform.parent = block.transform;
                face.transform.localPosition = position;
                face.transform.localRotation = rotation;

                //Add the refrence to the block.
                if(!asPreview) {
                    var opaque = block.GetComponent<OpaqueBlock>();
                    if(opaque) {
                        switch(this.face) {
                            case Block.Face.PosX: opaque.faces.right = face; break;
                            case Block.Face.NegX: opaque.faces.left = face; break;
                            case Block.Face.PosY: opaque.faces.top = face; break;
                            case Block.Face.NegY: opaque.faces.bottom = face; break;
                            case Block.Face.PosZ: opaque.faces.front = face; break;
                            case Block.Face.NegZ: opaque.faces.back = face; break;
                            default: throw new BlockAttributeException("Invalid 'face' paramater for the 'Face' attribute.");
                        }
                    } else throw new BlockAttributeException("The 'Face' attribute can only be applied on opaque blocks.");
                }

            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class FlatFace : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The <see cref="Block.Face"/> that this face is.</summary>
            public readonly Block.Face face;

            /// <summary>The local position of the face.</summary>
            public Vector3 position;
            /// <summary>The local rotation of the face.</summary>
            public Quaternion rotation;

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="FlatFace"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public FlatFace(Block.Face face, AppliesTo targets = AppliesTo.Both)
                : this(face,

                      posX: (int)face == +1 ? +0.5f : (int)face == -1 ? -0.5f : 0,
                      posY: (int)face == +2 ? +0.5f : (int)face == -2 ? -0.5f : 0,
                      posZ: (int)face == +3 ? +0.5f : (int)face == -3 ? -0.5f : 0,

                      rotX: (int)face == +3 ? +90f : (int)face == -3 ? -90f
                          : (int)face == +2 ? 000f : (int)face == -2 ? 180f : 0,
                      rotY: 000000000000000000000000000000000000000000000000000,
                      rotZ: (int)face == +1 ? -90f : (int)face == -1 ? +90f : 0,

                      targets) { }

            //todo: make it actually do what it says in that summary
            /// <summary>Gives this <see cref="OpaqueBlock"/> a mesh that can be combined and hidden with other blocks of the same type.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="posX">The 'x' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posY">The 'y' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posZ">The 'z' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="rotX">The 'x' component of the relative rotation of this face in degrees. (ie. the spin around the x-axis)</param>
            /// <param name="rotY">The 'y' component of the relative rotation of this face in degrees. (ie. the spin around the y-axis)</param>
            /// <param name="rotZ">The 'z' component of the relative rotation of this face in degrees. (ie. the spin around the z-axis)</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public FlatFace(Block.Face face, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                this.face = face;
                position = new Vector3(posX, posY, posZ);
                rotation = Quaternion.Euler(rotX, rotY, rotZ);
            }

            public override void ApplyTo(GameObject block, bool asPreview) {

                //Create the face.
                GameObject face = UnityEngine.Object.Instantiate(ContentLoader.GetResource<GameObject>("Meshes\\Plane"));
                face.transform.parent = block.transform;
                face.transform.localPosition = position;
                face.transform.localRotation = rotation;

                //Add the refrence to the block.
                if(!asPreview) {
                    var opaque = block.GetComponent<OpaqueBlock>();
                    if(opaque) {
                        if(opaque.faces == null) opaque.faces = new Faces();
                        switch(this.face) {
                            case Block.Face.PosX: opaque.faces.right = face; break;
                            case Block.Face.NegX: opaque.faces.left = face; break;
                            case Block.Face.PosY: opaque.faces.top = face; break;
                            case Block.Face.NegY: opaque.faces.bottom = face; break;
                            case Block.Face.PosZ: opaque.faces.front = face; break;
                            case Block.Face.NegZ: opaque.faces.back = face; break;
                            default: throw new BlockAttributeException("Invalid 'face' paramater for the 'Face' attribute.");
                        }
                    } else throw new BlockAttributeException("The 'FlatFace' attribute can only be applied on opaque blocks.");
                }

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.Material"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class Material : BlockAttribute {
            public override Order Order => Order.Last;

            /// <summary>The file path to a '.png' file for a diffuse texture.</summary>
            public readonly string diffusePath = null;
            /// <summary>The file path to a '.png' file for a normal texture.</summary>
            public readonly string normalPath = null;

            /// <summary>Apply this material to the parent?</summary>
            public readonly bool parent = false;
            /// <summary>Apply this material to the children?</summary>
            public readonly bool children = false;

            /// <summary>Gives this <see cref="Block"/>'s <see cref="Renderer"/> a <see cref="Material"/>.</summary>
            /// <param name="diffusePath"><c>ex. "Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png"</c></param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            public Material(string diffusePath, byte children = 2, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                this.diffusePath = diffusePath;
                if(children > 0) this.children = true;
                if(children != 1) parent = true;
            }

            /// <summary>Gives this <see cref="Block"/>'s <see cref="Renderer"/> a <see cref="Material"/>.</summary>
            /// <param name="diffusePath">ex. <c>"Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png"</c></param>
            /// <param name="normalPath">ex. <c>"Content\\Vanilla\\Blocks\\ArmorBlock\\normal.png"</c></param>
            /// <param name="children">0: only parent, 1: only children, 2: both</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            public Material(string diffusePath, string normalPath, byte children = 2, AppliesTo targets = AppliesTo.Both) {
                this.targets = targets;
                this.diffusePath = diffusePath;
                this.normalPath = normalPath;
                if(children > 0) this.children = true;
                if(children != 1) parent = true;
            }

            public override void ApplyTo(GameObject block, bool asPreview) {

                var material = new UnityEngine.Material(asPreview ?
                    ContentLoader.GetResource<UnityEngine.Material>("Materials\\BlockPreview")
                  : ContentLoader.GetResource<UnityEngine.Material>("Materials\\Block"));

                if(diffusePath != null && !asPreview)
                    material.SetTexture("BlockDiffuse",
                        ContentLoader.GetPngFile(diffusePath));
                if(normalPath != null && !asPreview)
                    material.SetTexture("BlockNormal",
                        ContentLoader.GetPngFile(normalPath));

                if(parent)
                    block.GetComponent<Renderer>().material = material;
                if(children)
                    foreach(Renderer renderer in block.GetComponentsInChildren<Renderer>())
                        renderer.material = material;

            }

        }

        public class ParticleSystem : BlockAttribute {
            public override Order Order => Order.Default;

            public ParticleSystem(AppliesTo targets = AppliesTo.Original) {
                this.targets = targets;
            }

            public override void ApplyTo(GameObject block, bool asPreview) {
                var particleSystem = block.AddComponent<UnityEngine.ParticleSystem>();
                foreach(Component component in block.GetComponents<Component>())
                    if(component is KineticEnergy.Interfaces.IPrefabComponentEditor<UnityEngine.ParticleSystem> prefabEditor)
                        prefabEditor.OnPrefab(particleSystem);
            }

        }

        public class BlockAttributeException : Exception {
            public BlockAttributeException(string message) : base(message) { }
        }

    }


}