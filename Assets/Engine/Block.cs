using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using KineticEnergy.Content;
using KineticEnergy.Structs;
using KineticEnergy.CodeTools.Math;
using KineticEnergy.Interfaces;
using KineticEnergy.Intangibles;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Ships.Blocks {

    #region Editor
#if UNITY_EDITOR
    using KineticEnergy.Unity;

    [CustomEditor(typeof(Block), true)]
    [CanEditMultipleObjects]
    public class Block_UnityEditor : Editor {
        Block script;
        SerializedProperty
            m_grid;

        public void OnEnable() {
            script = (Block)target;
            m_grid = serializedObject.FindProperty("grid");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            GUI.enabled = true;
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Dimensions != b.Dimensions);
            var _dimentions = EditorGUILayout.Vector3IntField("Dimentions", script.Dimensions);
            if(_dimentions != script.Dimensions) foreach(Block s in targets) s.Dimensions = _dimentions;

            GUI.enabled = false;
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Grid != b.Grid);
            EditorGUILayout.PropertyField(m_grid, new GUIContent("Grid Reference"));
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.GridPosition != b.GridPosition);
            EditorGUILayout.Vector3IntField("Position in Grid", script.GridPosition);

            GUI.enabled = Application.isEditor;
            var _mass = Mass_UnityPropertyDrawer.LayoutFromValue(script.Mass, new GUIContent("Mass"),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.magnitude != b.Mass.magnitude),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.position != b.Mass.position));
            if(script.Mass != _mass) foreach(Block s in targets) s.SetMass_Inspector(_mass);

            GUI.enabled = true;
            script.OnInspectorGUI(targets);

            serializedObject.ApplyModifiedProperties();
        }

    }

    [CustomPropertyDrawer(typeof(Block.FaceRefs))]
    public class Faces_UnityPropertyDrawer : PropertyDrawer {

        #region Configuration

        const float HEIGHT = 200.0f;
        static Rect fieldRect = new Rect(-75, -10, 150, 20);
        const float H = 100f, V = 75f;
        static readonly Vector2[] directions = new Vector2[] {
            Vector2.right.Rotate(-10) * H, //right
            Vector2.right.Rotate(170) * H, //left
            Vector2.right.Rotate(90)  * V, //top
            Vector2.right.Rotate(-90) * V, //bottom
            Vector2.right.Rotate(210) * H, //front
            Vector2.right.Rotate(30)  * H  //back
        };
        static readonly string[] propertyNames = new string[] { "right", "left", "top", "bottom", "front", "back" };
        static readonly string[] displayNames = new string[] { "Right", "Left", "Top", "Bottom", "Front", "Back" };

        private Vector2 GetDirection(int index) {
            var direction = directions[index];
            return new Vector2(direction.x, -direction.y);
        }

        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => HEIGHT;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var original_indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            if(
                GUI.RepeatButton(new Rect(position.center.x - (25f / 2f), position.center.y - (26f / 2f), 25, 26), GUIContent.none)
            ) {

                var style = new GUIStyle { alignment = TextAnchor.MiddleCenter };
                for(int n = 0; n < 6; n++) {
                    GUI.Label(
                        new Rect(position.center + GetDirection(n) + fieldRect.position, fieldRect.size),
                        new GUIContent(displayNames[n]), style
                    );
                }

            } else {

                for(int n = 0; n < 6; n++) {
                    EditorGUI.PropertyField(
                        new Rect(position.center + GetDirection(n) + fieldRect.position, fieldRect.size),
                        property.FindPropertyRelative(propertyNames[n]), GUIContent.none
                    );
                }

            }

            EditorGUI.indentLevel = original_indent;
            EditorGUI.EndProperty();
        }

    }

#endif
    #endregion

    /// <summary>Base class for all blocks.</summary>
    public abstract class Block : MonoBehaviour {

        #region Abstracts / Virtuals

        public virtual void OnGridUpdated() { }
        public virtual void OnNearbyPieceAdded(Vector3Int relativePosition) { }
        public virtual void OnNearbyPieceRemoved(Vector3Int relativePosition) { }
        public abstract bool SideIsOpaque(Vector3Int relativePosition, Face relativeFace);

        bool m_disabled = false;
        float m_disabledTime = 0f;
        public void OnEnable() {
            if(Grid != null) Grid.PlaceEnablingBlock(this, transform.localPosition);
            StasisAwake(m_disabled ? 0f : m_disabledTime - Time.time);
        }
        /// <summary>[Base Call Not Needed]</summary>
        /// <param name="time"></param>
        public virtual void StasisAwake(float time) { }

        public void OnDisable() {
            if(Grid != null) Grid.RemoveBlock(GridPosition, false);
            StasisAsleep();
            m_disabled = true;
            m_disabledTime = Time.time;
        }
        /// <summary>[Base Call Not Needed]</summary>
        public virtual void StasisAsleep() { }

        /// <summary>[Base Call Not Needed] Method called when prefab is loaded. Use this if you wanna do anything fancy with the prefab.</summary>
        public virtual void PrefabSetup() { }

#if UNITY_EDITOR
        /// <summary>[Base Call Not Needed] Override this method to show custom information in the unity editor.</summary>
        public virtual void OnInspectorGUI(UnityEngine.Object[] targets) { }
#endif

        #endregion

        #region Properties, Constants, and Setup Methods

        /// <summary>Every block cintains a <see cref="BlockGrid.Traversal.Node"/> so a new array does not have to be created for every <see cref="BlockGrid.Traversal"/>.</summary>
        public BlockGrid.Traversal.Node Node { get; set; } = null;

        /// <summary>This <see cref="Block"/>'s reference to the <see cref="Master"/>.</summary>
        public Master Master {
            get => master ?? throw new NullReferenceException(string.Format("'{0}' is null. Was '{1}' called on this {2}?",
                nameof(Master), nameof(GlobalBehavioursManager.PolishBehaviour), nameof(Block)));
            internal set => master = value;
        }
        private Master master;

        /// <summary>This <see cref="Block"/>'s reference to the <see cref="GlobalBehavioursManager"/>.</summary>
        public GlobalBehavioursManager Global => Master.Global;

        /// <summary>The name of this block.</summary>
        /// <remarks>Currently just a property for the <c>gameObject.name</c>. No plan to change that yet.</remarks>
        public string Name {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        /// <summary>The dimentions of this block in grid space. Runs <see cref="UpdateDimensionInformation()"/> when set. Nothing becides "return" is done on get.</summary>
        public Vector3Int Dimensions {
            get { return dimensions; }
            set {
                dimensions = value;
                UpdateDimensionInformation();
            }
        }
        private Vector3Int dimensions = Vector3Int.one;

        /// <summary>The <see cref="Ships.Mass"/> of this block.</summary>
        /// <remarks>Updates the <see cref="Grid"/>'s <see cref="Mass"/> when set.
        /// <para/>Nothing becides "return" is done on get.</remarks>
        public Mass Mass {
            get { return mass; }
            set {
                if(Grid != null) {
                    Mass delta = mass - value;
                    Grid.Mass += delta;
                }
                mass = value;
            }
        }
        private Mass mass = new Mass(1000, Vector3.zero);

        /// <summary>Method for changing <see cref="Mass"/> from a Unity Inspector.</summary>
        /// <param name="newMass">The new <see cref="Ships.Mass"/> of the <see cref="Block"/>.</param>
        public void SetMass_Inspector(Mass newMass) {
            if(Grid != null && Grid.rigidbody != null) Mass = newMass;
            else mass = newMass;
        }

        /// <summary>The <see cref="BlockGrid"/> associated with this block.</summary>
        public BlockGrid Grid { get; set; }

        /// <summary>Checks if <see cref="OnRemovedFromGrid"/> need to be called,
        /// then sets transform position and parent, amung some other value inside <see cref="Block"/>.</summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to set this object's <see cref="Grid"/> as.</param>
        /// <param name="gridPosition">The position in the <see cref="BlockGrid"/> this <see cref="Block"/> will be moved to.</param>
        public void UpdateGrid(BlockGrid grid, Vector3Int gridPosition) {
            if(Grid != null && Grid != grid) Grid.Mass -= Mass + GridPosition;
            grid.Mass += Mass + gridPosition;
            Grid = grid;
            GridPosition = gridPosition;
            ArrayPosition = gridPosition + grid.Offset;
            transform.SetParent(grid.gameObject.transform, false);
            transform.localPosition = gridPosition;
            OnGridUpdated();
        }

        /// <summary>Should be called after something like <see cref="BlockGrid.RemoveBlock"/> is called,
        /// but is redundant if <see cref="UpdateGrid"/> is about to be called.</summary>
        /// <remarks>Does not check if <see cref="Grid"/> is null
        /// so could be subject to <see cref="NullReferenceException"/>s if <c>grid != null</c> isn't checked first.</remarks>
        public void OnRemovedFromGrid() {
            Grid.Mass -= Mass;
            Grid = null;
        }

        /// <summary>Sets important variables inside <see cref="Block"/>, which would otherwise be innacurate or null.</summary>
        /// <remarks>Should be called when this object is created or when <see cref="dimensions"/> (not <see cref="Dimensions"/>) is changed.</remarks>
        public void UpdateDimensionInformation() {
            UpdateAreaValues();
            UpdateInsidePoints();
            UpdateNeighboringPoints();
        }

        /// <summary>Shortcut for <c>"Dimensions - Vector3Int.one"</c>.</summary>
        /// <seealso cref="Dimensions"/>
        /// <seealso cref="Vector3Int.one"/>
        public Vector3Int GridCorner => Dimensions - Vector3Int.one;

        /// <summary>The surface area on the x side.</summary>
        /// <seealso cref="UpdateAreaValues"/>
        public int XArea { get; private set; }
        /// <summary>The surface area on the y side.</summary>
        /// <seealso cref="UpdateAreaValues"/>
        public int YArea { get; private set; }
        /// <summary>The surface area on the z side.</summary>
        /// <seealso cref="UpdateAreaValues"/>
        public int ZArea { get; private set; }

        /// <summary>The surface area defined by the <see cref="Dimensions"/>.</summary>
        public int SurfaceArea { get; private set; }

        /// <summary>Uses the current value of <see cref="Dimensions"/> to set the values of <see cref="XArea"/>, <see cref="YArea"/>, and <see cref="ZArea"/>.</summary>
        private void UpdateAreaValues() {
            Vector3Int dimensions = Dimensions;
            XArea = dimensions.y * dimensions.z;
            YArea = dimensions.z * dimensions.x;
            ZArea = dimensions.x * dimensions.y;
            SurfaceArea = (XArea + YArea + ZArea) * 2;
        }

        /// <summary>The position of this block in its <see cref="BlockGrid"/> grid space.</summary>
        /// <seealso cref="Grid"/>
        /// <seealso cref="BlockGrid.Offset"/>
        public Vector3Int GridPosition { get; internal set; }

        /// <summary>The position of this block in its <see cref="BlockGrid"/> array space.</summary>
        /// <seealso cref="Grid"/>
        /// <seealso cref="BlockGrid.Offset"/>
        public Vector3Int ArrayPosition { get; internal set; }

        #endregion

        #region "Points" Arrays

        #region Neighboring Points XYZ

        public Vector3Int[] NeighboringPointsX { get; set; }
        private void UpdateNeighboringPointsX() {
            Vector3Int[] npX = new Vector3Int[XArea * 2];
            for(int y = 0; y < Dimensions.y; y++) {
                for(int z = 0; z < Dimensions.z; z++) {
                    npX[y + z + XArea] = new Vector3Int(-1, y, z);
                    npX[y + z] = new Vector3Int(Dimensions.x, y, z);
                }
            }
            NeighboringPointsX = npX;
        }

        public Vector3Int[] NeighboringPointsY { get; set; }
        private void UpdateNeighboringPointsY() {
            Vector3Int[] npY = new Vector3Int[YArea * 2];
            for(int x = 0; x < Dimensions.x; x++) {
                for(int z = 0; z < Dimensions.z; z++) {
                    npY[x + z + YArea] = new Vector3Int(x, -1, z);
                    npY[x + z] = new Vector3Int(x, Dimensions.y, z);
                }
            }
            NeighboringPointsY = npY;
        }

        public Vector3Int[] NeighboringPointsZ { get; set; }
        private void UpdateNeighboringPointsZ() {
            Vector3Int[] npZ = new Vector3Int[ZArea * 2];
            for(int x = 0; x < Dimensions.x; x++) {
                for(int y = 0; y < Dimensions.y; y++) {
                    npZ[y + x + ZArea] = new Vector3Int(x, y, -1);
                    npZ[y + x] = new Vector3Int(x, y, Dimensions.z);
                }
            }
            NeighboringPointsZ = npZ;
        }

        #endregion

        #region Neighboring Points
        /// <summary>
        /// All of the grid cell locations (from a local reference) that touch this block.
        /// Null until <see cref="UpdateDimensionInformation()"/> or <see cref="UpdateNeighboringPoints()"/> is called.
        /// </summary>
        public Vector3Int[] neighboringPoints = new Vector3Int[0];

        /// <summary>Assigns to <see cref="neighboringPoints"/>.</summary>
        private void UpdateNeighboringPoints() {
            UpdateNeighboringPointsX(); UpdateNeighboringPointsY(); UpdateNeighboringPointsZ();
            Vector3Int[] npX = NeighboringPointsX, npY = NeighboringPointsY, npZ = NeighboringPointsZ;
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

        #region Face
        /// <summary>Defines all of the faces that a <see cref="Block"/> can have.</summary>
        public enum Face {
            PosX = +1, Right = +1,
            NegX = -1, Left = -1,
            PosY = +2, Up = +2,
            NegY = -2, Down = -2,
            PosZ = +3, Front = +3,
            NegZ = -3, Back = -3
        }

        public static Vector3Int DirectionFromFace(Face face) {
            switch(face) {
                case Face.PosX: return new Vector3Int(+1, 0, 0);
                case Face.NegX: return new Vector3Int(-1, 0, 0);
                case Face.PosY: return new Vector3Int(0, +1, 0);
                case Face.NegY: return new Vector3Int(0, -1, 0);
                case Face.PosZ: return new Vector3Int(0, 0, +1);
                case Face.NegZ: return new Vector3Int(0, 0, -1);
                default: return Vector3Int.zero;
            }
        }

        /// <summary>Class used to store and show/hide the six faces of an <see cref="OpaqueBlock"/>.</summary>
        [Serializable]
        public class FaceRefs {

            public GameObject right, left, top, bottom, front, back;

            /// <summary>Uses the given enabled faces to use <see cref="GameObject.SetActive(bool)"/>.</summary>
            /// <param name="enabled">A <see cref="FaceMask"/> that represents the only faces that should be enabled.</param>
            public void ToggleFaces(FaceMask enabled) {
                right.SetActive(enabled.PosX);
                left.SetActive(enabled.NegX);
                top.SetActive(enabled.PosY);
                bottom.SetActive(enabled.NegY);
                front.SetActive(enabled.PosZ);
                back.SetActive(enabled.NegZ);
            }

        }

        /// <summary>A fancy coat for a binary number which represents 6 <see cref="bool"/>s.</summary>
        /// <remarks>Digits 1-6 represent the faces.
        /// Digit 7 is saved for when this <see cref="FaceMask"/> is being used as a normal, and tells if it is a corner.</remarks>
        [Serializable]
        public struct FaceMask : IEnumerable<bool>, IEnumerable<Vector3Int> {

            /// <summary>The <see cref="byte"/> value of this <see cref="FaceMask"/>.</summary>
            public int value;
            /// <summary>The <see cref="value"/>, but only digits one through six.</summary>
            public int Culled => value | 0b111111;
            /// <summary>Sets <see cref="value"/> to <see cref="Culled"/>.</summary>
            public void Cull() => value = Culled;

            #region Enumeration

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<bool>)this).GetEnumerator();

            IEnumerator<bool> IEnumerable<bool>.GetEnumerator() => new BoolEnumerator(this);
            public class BoolEnumerator : IEnumerator<bool> {

                int index = 0;
                FaceMask mask;

                public BoolEnumerator(FaceMask mask) => this.mask = mask;

                object IEnumerator.Current => Current;
                public bool Current => (mask.value >> index) != 0;

                public bool MoveNext() => ++index < 6;
                public void Reset() => index = 0;
                public void Dispose() { }

            }

            IEnumerator<Vector3Int> IEnumerable<Vector3Int>.GetEnumerator() => new DirectionEnumerator(this);
            public class DirectionEnumerator : IEnumerator<Vector3Int> {

                int index = -1;
                FaceMask mask;

                public DirectionEnumerator(FaceMask mask) => this.mask = mask;

                object IEnumerator.Current => Current;
                public Vector3Int Current {
                    get {
                        switch(index) {
                            case 0: return new Vector3Int(+1, 0, 0);
                            case 1: return new Vector3Int(-1, 0, 0);
                            case 2: return new Vector3Int(0, +1, 0);
                            case 3: return new Vector3Int(0, -1, 0);
                            case 4: return new Vector3Int(0, 0, +1);
                            case 5: return new Vector3Int(0, 0, -1);
                            default: throw new InvalidOperationException();
                        }
                    }
                }

                //Moves to next true bit by taking advantage of recursion and boolean operators skipping the right-hand value.
                public bool MoveNext() => ++index < 6 && (((mask.value >> index) & 1) != 0 || MoveNext());
                public void Reset() => index = -1;
                public void Dispose() { }

            }

            #endregion

            /// <summary>Shorthand for "<c>bools.value</c>".</summary>
            public static implicit operator int(FaceMask bools) => bools.value;
            /// <summary>Shorthand for "<c>new FaceBools { value = value }</c>".</summary>
            public static implicit operator FaceMask(int value) => new FaceMask { value = value };

            /// <summary>Creates a new <see cref="FaceMask"/> dependent on the signs of components of the given <see cref="Vector3Int"/>.</summary>
            public FaceMask(Vector3Int vector) {
                value = 0;
                if(vector.x == 1) value += POSX;
                else if(vector.x == -1) value += NEGX;
                if(vector.y == 1) value += POSY;
                else if(vector.y == -1) value += NEGY;
                if(vector.z == 1) value += POSZ;
                else if(vector.z == -1) value += NEGZ;
            }

            /// <summary>Creates a new <see cref="FaceMask"/> dependent on if the respective components of
            /// the given <see cref="Vector3"/> are positive, negative, or zero.</summary>
            public static explicit operator FaceMask(Vector3 values) {
                FaceMask faces = 0;

                if(values.x > 0) faces.value += POSX;
                else if(values.x < 0) faces.value += NEGX;

                if(values.y > 0) faces.value += POSY;
                else if(values.y < 0) faces.value += NEGY;

                if(values.z > 0) faces.value += POSZ;
                else if(values.z < 0) faces.value += NEGZ;

                return faces;
            }

            /// <summary>Creates a new <see cref="FaceMask"/> dependent on if the respective components of
            /// the given <see cref="Vector3Int"/> are positive, negative, or zero.</summary>
            public static explicit operator FaceMask(Vector3Int values) {
                FaceMask faces = 0;

                if(values.x > 0) faces.value += POSX;
                else if(values.x < 0) faces.value += NEGX;

                if(values.y > 0) faces.value += POSY;
                else if(values.y < 0) faces.value += NEGY;

                if(values.z > 0) faces.value += POSZ;
                else if(values.z < 0) faces.value += NEGZ;

                return faces;
            }

            /// <summary>[Culled] Combines the true values of both <see cref="FaceMask"/>s.</summary>
            /// <param name="left">The first <see cref="FaceMask"/> to combine with the second.</param>
            /// <param name="right">The second <see cref="FaceMask"/> to combine with the first.</param>
            public static FaceMask operator +(FaceMask left, FaceMask right) {
                FaceMask faces = 0;

                if(left.PosX || right.PosX) faces.value += POSX;
                if(left.NegX || right.NegX) faces.value += NEGX;

                if(left.PosY || right.PosY) faces.value += POSY;
                if(left.NegY || right.NegY) faces.value += NEGY;

                if(left.PosZ || right.PosZ) faces.value += POSZ;
                if(left.NegZ || right.NegZ) faces.value += NEGZ;

                return faces;
            }

            /// <summary>[Culled] Removes the right <see cref="FaceMask"/>'s true values from the true values in the left <see cref="FaceMask"/>.</summary>
            /// <param name="left">The left <see cref="FaceMask"/>.</param>
            /// <param name="right">The right <see cref="FaceMask"/>.</param>
            public static FaceMask operator -(FaceMask left, FaceMask right) {
                FaceMask faces = 0;

                if(right.PosX) left.PosX = false;
                if(right.NegX) left.NegX = false;

                if(right.PosY) left.PosY = false;
                if(right.NegY) left.NegY = false;

                if(right.PosZ) left.PosZ = false;
                if(right.NegZ) left.NegZ = false;

                return faces;
            }

            /// <summary>A binary value with only the first digit true.</summary>
            public const int POSX = 0b000001;
            public const int NOT_POSX = 0b111110;
            /// <summary>Property that represents the first digit of <see cref="value"/>.</summary>
            public bool PosX {
                get => (value & POSX) != 0;
                set {
                    bool posX = PosX;
                    if(value == posX) return;
                    if(posX) this.value -= POSX;
                    else this.value += POSX;
                }
            }

            /// <summary>A binary value with only the second digit true.</summary>
            public const int NEGX = 0b000010;
            public const int NOT_NEGX = 0b111101;
            /// <summary>Property that represents the second digit of <see cref="value"/>.</summary>
            public bool NegX {
                get => (value & NEGX) != 0;
                set {
                    bool negX = NegX;
                    if(value == negX) return;
                    if(negX) this.value -= NEGX;
                    else this.value += NEGX;
                }
            }

            /// <summary>Shorthand for "<c>PosX || NegX</c>".</summary>
            public bool AnyX => PosX || NegX;
            /// <summary>Shorthand for "<c>PosX &amp;&amp; NegX</c>".</summary>
            public bool AllX => PosX && NegX;

            /// <summary>A binary value with only the third digit true.</summary>
            public const int POSY = 0b000100;
            public const int NOT_POSY = 0b111011;
            /// <summary>Property that represents the third digit of <see cref="value"/>.</summary>
            public bool PosY {
                get => (value & POSY) != 0;
                set {
                    bool posY = PosY;
                    if(value == posY) return;
                    if(posY) this.value -= POSY;
                    else this.value += POSY;
                }
            }

            /// <summary>A binary value with only the fourth digit true.</summary>
            public const int NEGY = 0b001000;
            public const int NOT_NEGY = 0b110111;
            /// <summary>Property that represents the fourth digit of <see cref="value"/>.</summary>
            public bool NegY {
                get => (value & NEGY) != 0;
                set {
                    bool negY = NegY;
                    if(value == negY) return;
                    if(negY) this.value -= NEGY;
                    else this.value += NEGY;
                }
            }

            /// <summary>Shorthand for "<c>PosY || NegY</c>".</summary>
            public bool AnyY => PosY || NegY;
            /// <summary>Shorthand for "<c>PosY &amp;&amp; NegY</c>".</summary>
            public bool AllY => PosY && NegY;

            /// <summary>A binary value with only the fifth digit true.</summary>
            public const int POSZ = 0b010000;
            public const int NOT_POSZ = 0b101111;
            /// <summary>Property that represents the fifth digit of <see cref="value"/>.</summary>
            public bool PosZ {
                get => (value & POSZ) != 0;
                set {
                    bool posZ = PosZ;
                    if(value == posZ) return;
                    if(posZ) this.value -= POSZ;
                    else this.value += POSZ;
                }
            }

            /// <summary>A binary value with only the sixth digit true.</summary>
            public const int NEGZ = 0b100000;
            public const int NOT_NEGZ = 0b0111111;
            /// <summary>Property that represents the sixth digit of <see cref="value"/>.</summary>
            public bool NegZ {
                get => (value & NEGZ) != 0;
                set {
                    bool negZ = NegZ;
                    if(value == negZ) return;
                    if(negZ) this.value -= NEGZ;
                    else this.value += NEGZ;
                }
            }

            /// <summary>Shorthand for "<c>PosZ || NegZ</c>".</summary>
            public bool AnyZ => PosZ || NegZ;
            /// <summary>Shorthand for "<c>PosZ &amp;&amp; NegZ</c>".</summary>
            public bool AllZ => PosZ && NegZ;

            /// <summary>A binary value with only the first six digits true.</summary>
            public const int ALL = 0b111111;

            public static FaceMask operator +(FaceMask bools, Face face) {
                FaceMask _bools = bools.value;
                switch(face) {
                    case Face.PosX:
                        _bools.PosX = true;
                        return _bools;
                    case Face.NegX:
                        _bools.NegX = true;
                        return _bools;
                    case Face.PosY:
                        _bools.PosY = true;
                        return _bools;
                    case Face.NegY:
                        _bools.NegY = true;
                        return _bools;
                    case Face.PosZ:
                        _bools.PosZ = true;
                        return _bools;
                    case Face.NegZ:
                        _bools.NegZ = true;
                        return _bools;
                    default: throw new ArgumentException("Invalid argument for FaceBools() addition operator.", face.GetType().Name);
                }
            }
            public static FaceMask operator -(FaceMask bools, Face face) {
                switch(face) {
                    case Face.PosX:
                        bools.PosX = false;
                        return bools;
                    case Face.NegX:
                        bools.NegX = false;
                        return bools;
                    case Face.PosY:
                        bools.PosY = false;
                        return bools;
                    case Face.NegY:
                        bools.NegY = false;
                        return bools;
                    case Face.PosZ:
                        bools.PosZ = false;
                        return bools;
                    case Face.NegZ:
                        bools.NegZ = false;
                        return bools;
                    default: throw new ArgumentException("Invalid argument for FaceBools() addition operator.", face.GetType().Name);
                }
            }

        }

        /// <summary>Checks all faces and returns a <see cref="FaceMask"/> that represents which faces are covered.</summary>
        public FaceMask GetFacesShown() {
            FaceMask result = 0;

            //maybe make this stored?
            var invRotation = Quaternion.Inverse(transform.localRotation);

            GetFacesShown(ref result, this, invRotation, Grid,
                NeighboringPointsX, XArea, Face.PosX, Face.NegX);

            GetFacesShown(ref result, this, invRotation, Grid,
                NeighboringPointsY, YArea, Face.PosY, Face.NegY);

            GetFacesShown(ref result, this, invRotation, Grid,
                NeighboringPointsZ, ZArea, Face.PosZ, Face.NegZ);

            return result;
        }

        private static void GetFacesShown(ref FaceMask result, Block block, Quaternion invRotation, BlockGrid grid,
            Vector3Int[] neighboringPoints, int area, Face facePos, Face faceNeg) {

            bool posShown = false, negShown = false;
            for(int i = 0; i < area; i++) {

                if((posShown == false) &&
                    !grid.SideIsOpaque(
                        grid.LocalPoint_to_LocalGrid(block.LocalPoint_to_RelativePoint(neighboringPoints[i], invRotation))
                        + block.GridPosition, facePos))
                    posShown = true;

                if((negShown == false) &&
                    !grid.SideIsOpaque(
                        grid.LocalPoint_to_LocalGrid(block.LocalPoint_to_RelativePoint(neighboringPoints[i + area], invRotation))
                        + block.GridPosition, faceNeg))
                    negShown = true;

                if(posShown && negShown) break;
            }
            if(posShown) result += facePos;
            if(negShown) result += faceNeg;

        }

        #endregion

        #region Space Conversion

        /// <summary>Converts a point local to this <see cref="Block"/>'s transform to a point relative to this <see cref="Block"/>'s transform.</summary>
        /// <param name="localPoint">The relative grid position to translate.</param>
        /// <returns>Returns a grid position.</returns>
        public Vector3 LocalPoint_to_RelativePoint(Vector3 localPoint)
            => Quaternion.Inverse(transform.localRotation) * localPoint;
        /// <summary>Converts a point local to this <see cref="Block"/>'s transform to a point relative to this <see cref="Block"/>'s transform.</summary>
        /// <param name="localPoint">The relative grid position to translate.</param>
        /// <param name="invRotation">The <see cref="Quaternion.Inverse(Quaternion)"/> of the <see cref="Transform.localRotation"/>.</param>
        /// <returns>Returns a grid position.</returns>
        public Vector3 LocalPoint_to_RelativePoint(Vector3 localPoint, Quaternion invRotation)
            => invRotation * localPoint;

        #endregion

    }

    #region Block Attributes
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
            /// <summary>A special case for the <see cref="BlockAttributes.BasicInfo"/> attribute.</summary>
            BasicInfo = 99,
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
        /// If an attribute gives you the option to set <see cref="Targets"/>, then the attribute is useful for both.
        /// <para/>If you are implementing this class, do not check the given "isPreview" boolean inside <see cref="ApplyTo(GameObject, bool)"/>.
        /// This is done already by the <see cref="BlockAttribute"/> class, so if <see cref="ApplyTo"/> is being called then the attribute needs to be applied.
        /// Instead, use that given value to change values such as <see cref="Collider.isTrigger"/>.</remarks>
        public abstract class BlockAttribute : Attribute {

            /// <summary>The <see cref="BlockAttributes.Order"/> of this <see cref="BlockAttribute"/>.</summary>
            public abstract Order Order { get; }

            /// <summary>Does this <see cref="BlockAttribute"/> apply to originals, previews, or both?</summary>
            public AppliesTo Targets { get; protected set; } = AppliesTo.Both;
            /// <summary>Shorthand for "<c>this.targets == AppliesTo.Both || this.targets == AppliesTo.Original</c>".</summary>
            public bool AppliesToOriginal => Targets == AppliesTo.Both || Targets == AppliesTo.Original;
            /// <summary>Shorthand for "<c>this.targets == AppliesTo.Both || this.targets == AppliesTo.Preview</c>".</summary>
            public bool AppliesToPreview => Targets == AppliesTo.Both || Targets == AppliesTo.Preview;

            /// <summary>Applies this <see cref="BlockAttribute"/> to the given <see cref="GameObject"/>, regardless of <see cref="Targets"/.></summary>
            /// <param name="block">The <see cref="Block"/> or <see cref="BlockPreview"/>'s <see cref="GameObject"/>.</param>
            /// <param name="asPreview">Is the given <see cref="GameObject"/> a <see cref="BlockPreview"/> or a <see cref="Block"/>?</param>
            public abstract void ApplyTo(GameObject block, Master master, bool asPreview);

            /// <summary>Applies this <see cref="BlockAttribute"/> if <see cref="AppliesToOriginal"/> is true.</summary>
            /// <param name="original">The <see cref="GameObject"/> to apply this <see cref="BlockAttribute"/> to.</param>
            public void ApplyToOriginal(GameObject original, Master master) { if(AppliesToOriginal) ApplyTo(original, master, false); }

            /// <summary>Applies this <see cref="BlockAttribute"/> if <see cref="AppliesToPreview"/> is true.</summary>
            /// <param name="preview">The <see cref="GameObject"/> to apply this <see cref="BlockAttribute"/> to.</param>
            public void ApplyToPreview(GameObject preview, Master master) { if(AppliesToPreview) ApplyTo(preview, master, true); }

            protected static void SendToPrefabEditors<T>(Master master, GameObject block, T prefabComponent, bool asPreview, BlockAttribute sender) {
                foreach(Component component in block.GetComponents<Component>())
                    if(component is IPrefabEditor<T> prefabEditor)
                        prefabEditor.OnPrefab(master, prefabComponent, asPreview, sender);
            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class BasicInfo : BlockAttribute {
            public override Order Order => Order.BasicInfo;

            /// <summary>The name of the block.</summary>
            public readonly string name;
            /// <summary>The name of the preview.</summary>
            public readonly string name_preview;
            /// <summary>The value that <see cref="Block.Mass"/> will be set to.</summary>
            public readonly Mass mass;
            /// <summary>The value that <see cref="Block.Dimensions"/> will be set to.</summary>
            public readonly Vector3Int dimensions;
            /// <summary></summary>
            public readonly Vector3 offset;

            /// <summary>Gives this block a display name, mass, and dimensions. Every block should have this.</summary>
            /// <param name="name">The name of the block.</param>
            /// <remarks>The name of the preview is <c>name + "_preview"</c>.</remarks>
            public BasicInfo(string name, int sizeX, int sizeY, int sizeZ, long mass, float comX, float comY, float comZ, AppliesTo targets = AppliesTo.Both) {
                this.Targets = targets;
                this.name = name;
                name_preview = name + "_preview";
                dimensions = new Vector3Int(sizeX, sizeY, sizeZ);
                this.mass = new Mass(mass, new Vector3(comX, comY, comZ));
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
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
            public readonly int identifier = 0;

            /// <summary>Gives this <see cref="Block"/> a <see cref="UnityEngine.BoxCollider"/>.</summary>
            /// <param name="centerX">The 'x' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="centerY">The 'y' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="centerZ">The 'z' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="sizeX">The 'x' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="sizeY">The 'y' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="sizeZ">The 'z' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            /// <remarks>Originals use this for collision. Previews use this for detecting if the block placement is valid.</remarks>
            public BoxCollider(float centerX, float centerY, float centerZ, float sizeX, float sizeY, float sizeZ, int identifier = 0, AppliesTo targets = AppliesTo.Both) {
                this.Targets = targets;
                this.identifier = identifier;
                center = new Vector3(centerX, centerY, centerZ);
                size = new Vector3(sizeX, sizeY, sizeZ);
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var collider = block.AddComponent<UnityEngine.BoxCollider>();
                collider.center = center;
                collider.size = size;
                collider.isTrigger = asPreview;

                var component = block.GetComponent<Block>();
                if(component is OpaqueBlock opaque) {
                    opaque.collider = collider;
                }

                SendToPrefabEditors(master, block, collider, asPreview, this);

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
                this.Targets = targets;
                this.objPath = objPath;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var filter = block.AddComponent<MeshFilter>();
                filter.mesh = ContentLoader.GetObjFile(objPath);
                var render = block.AddComponent<MeshRenderer>();
                SendToPrefabEditors(master, block, filter, asPreview, this);
                SendToPrefabEditors(master, block, render, asPreview, this);
            }

        }

        /// <summary>Attribute for an <see cref="OpaqueBlock"/> to have a face with a custom mesh.</summary>
        /// <seealso cref="FlatFace"/>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
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

            public readonly int identifier = 0;

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="Face"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="objPath">File path the the '.obj' file for the mesh of this block face.</param>
            /// <param name="diffusePath">File path to the '.png' file for the diffuse texture of this block face.</param>
            /// <param name="normalPath">File path the the '.png' file for the normal texture of this block face.</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Face(Block.Face face, string objPath, string diffusePath, string normalPath = null, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : this(face,

                      posX: (int)face == +1 ? +0.5f : (int)face == -1 ? -0.5f : 0,
                      posY: (int)face == +2 ? +0.5f : (int)face == -2 ? -0.5f : 0,
                      posZ: (int)face == +3 ? +0.5f : (int)face == -3 ? -0.5f : 0,

                      rotX: (int)face == +3 ? +90f : (int)face == -3 ? -90f
                          : (int)face == +2 ? 000f : (int)face == -2 ? 180f : 0,
                      rotY: 0,
                      rotZ: (int)face == +1 ? -90f : (int)face == -1 ? +90f : 0,

                      objPath, diffusePath, normalPath, identifier, targets) { }

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
            public Face(Block.Face face, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, string objPath, string diffusePath, string normalPath = null, int identifier = 0, AppliesTo targets = AppliesTo.Both) {
                this.Targets = targets;
                this.identifier = identifier;
                this.face = face;
                position = new Vector3(posX, posY, posZ);
                rotation = Quaternion.Euler(rotX, rotY, rotZ);
                this.objPath = objPath;
                this.diffusePath = diffusePath;
                this.normalPath = normalPath;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

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

                SendToPrefabEditors(master, block, face, asPreview, this);

            }

        }

        /// <summary>Attribute for an <see cref="OpaqueBlock"/> to have a face with a flat mesh.</summary>
        /// <seealso cref="Face"/>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class FlatFace : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The <see cref="Block.Face"/> that this face is.</summary>
            public readonly Block.Face face;

            /// <summary>The local position of the face.</summary>
            public Vector3 position;
            /// <summary>The local rotation of the face.</summary>
            public Quaternion rotation;

            public readonly int identifier = 0;

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="FlatFace"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public FlatFace(Block.Face face, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : this(face,

                      posX: (int)face == +1 ? +0.5f : (int)face == -1 ? -0.5f : 0,
                      posY: (int)face == +2 ? +0.5f : (int)face == -2 ? -0.5f : 0,
                      posZ: (int)face == +3 ? +0.5f : (int)face == -3 ? -0.5f : 0,

                      rotX: (int)face == +3 ? +90f : (int)face == -3 ? -90f
                          : (int)face == +2 ? 000f : (int)face == -2 ? 180f : 0,
                      rotY: 0,
                      rotZ: (int)face == +1 ? -90f : (int)face == -1 ? +90f : 0,

                      identifier, targets) { }

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
            public FlatFace(Block.Face face, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, int identifier = 0, AppliesTo targets = AppliesTo.Both) {
                this.Targets = targets;
                this.identifier = identifier;
                this.face = face;
                position = new Vector3(posX, posY, posZ);
                rotation = Quaternion.Euler(rotX, rotY, rotZ);
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                //Create the face.
                GameObject face = master.meshes.plane.Instantiate().gameObject;
                face.transform.parent = block.transform;
                face.transform.localPosition = position;
                face.transform.localRotation = rotation;

                //Add the refrence to the block.
                if(!asPreview) {
                    var opaque = block.GetComponent<OpaqueBlock>();
                    if(opaque) {
                        if(opaque.faces == null) opaque.faces = new Block.FaceRefs();
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

                SendToPrefabEditors(master, block, face, asPreview, this);

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.Material"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class Material : BlockAttribute {
            public override Order Order => Order.Last;

            /// <summary>The file path to a '.png' file for a diffuse texture.</summary>
            public readonly string diffusePath = null;
            public readonly bool diffusePoint = false;
            /// <summary>The file path to a '.png' file for a normal texture.</summary>
            public readonly string normalPath = null;
            public readonly bool normalPoint = false;

            /// <summary>Apply this material to the parent?</summary>
            public readonly bool parent = false;
            /// <summary>Apply this material to the children?</summary>
            public readonly bool children = false;

            /// <summary>Gives this <see cref="Block"/>'s <see cref="Renderer"/> a <see cref="Material"/>.</summary>
            /// <param name="diffusePath"><c>ex. "Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png"</c></param>
            /// <param name="children">0: only parent, 1: only children, 2: both</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            public Material(string diffusePath, bool diffusePoint, byte children = 2, AppliesTo targets = AppliesTo.Both) {
                this.Targets = targets;
                this.diffusePath = diffusePath;
                this.diffusePoint = diffusePoint;
                if(children > 0) this.children = true;
                if(children != 1) parent = true;
            }

            /// <summary>Gives this <see cref="Block"/>'s <see cref="Renderer"/> a <see cref="Material"/>.</summary>
            /// <param name="diffusePath">ex. <c>"Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png"</c></param>
            /// <param name="normalPath">ex. <c>"Content\\Vanilla\\Blocks\\ArmorBlock\\normal.png"</c></param>
            /// <param name="children">0: only parent, 1: only children, 2: both</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            public Material(string diffusePath, bool diffusePoint, string normalPath, bool normalPoint, byte children = 2, AppliesTo targets = AppliesTo.Both) {
                Targets = targets;
                this.diffusePath = diffusePath;
                this.diffusePoint = diffusePoint;
                this.normalPath = normalPath;
                this.normalPoint = normalPoint;
                if(children > 0) this.children = true;
                if(children != 1) parent = true;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                var material = new UnityEngine.Material(asPreview ?
                    master.materials.preview
                  : master.materials.block);

                if(diffusePath != null && !asPreview) {
                    Texture2D diffuse = ContentLoader.GetPngFile(diffusePath, TextureFormat.RGBA32, false);
                    diffuse.filterMode = diffusePoint ? FilterMode.Point : FilterMode.Bilinear;
                    diffuse.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("BlockDiffuse", diffuse);
                }

                if(normalPath != null && !asPreview) {
                    Texture2D normal = ContentLoader.GetPngFile(normalPath, TextureFormat.RGBA32, false);
                    normal.filterMode = diffusePoint ? FilterMode.Point : FilterMode.Bilinear;
                    normal.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("BlockNormal", normal);
                }

                if(parent) {
                    var renderer = block.GetComponent<Renderer>();
                    if(renderer) renderer.material = material;
                }
                if(children) {
                    foreach(Renderer renderer in block.GetComponentsInChildren<Renderer>())
                        renderer.material = material;
                }

                SendToPrefabEditors(master, block, material, asPreview, this);

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.ParticleSystem"/>.</summary>
        /// <remarks><see cref="UnityEngine.ParticleSystem"/> can only be edited through <see cref="IPrefabEditor{T}"/> of type <see cref="UnityEngine.ParticleSystem"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class ParticleSystem : BlockAttribute {
            public override Order Order => Order.Default;

            public readonly int identifier = 0;
            public ParticleSystem(int identifier = 0, AppliesTo targets = AppliesTo.Original) {
                this.identifier = identifier;
                Targets = targets;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var particles = block.AddComponent<UnityEngine.ParticleSystem>();
                SendToPrefabEditors(master, block, particles, asPreview, this);
            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class ThrusterVFX : BlockAttribute {
            public override Order Order => Order.Default;

            public readonly int identifier = 0;
            public ThrusterVFX(int identifier = 0, AppliesTo targets = AppliesTo.Original) {
                this.identifier = identifier;
                Targets = targets;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var vfx = master.prefabs.thrustVFX.Instantiate(block.transform);
                SendToPrefabEditors(master, block, vfx, asPreview, this);
            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class SelectableBox : BlockAttribute {
            public override Order Order => Order.Default;

            public readonly Vector3 center;
            public readonly Vector3 size;

            public readonly int identifier = 0;
            public SelectableBox(float centerX, float centerY, float centerZ, float sizeX, float sizeY, float sizeZ, int identifier = 0, AppliesTo targets = AppliesTo.Original) {
                this.identifier = identifier;
                Targets = targets;
                center = new Vector3(centerX, centerY, centerZ);
                size = new Vector3(sizeX, sizeY, sizeZ);
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var selectable = master.prefabs.selectableBox.Instantiate();
                selectable.gameObject.transform.SetParent(block.transform);
                selectable.gameObject.transform.localPosition = center;
                selectable.gameObject.transform.localScale = size;
                selectable.component2.material = master.materials.selectable;
                SendToPrefabEditors(master, block, selectable, asPreview, this);
            }

        }

        public class BlockAttributeException : Exception {
            public BlockAttributeException(string message) : base(message) { }
        }

    }
    #endregion

}
