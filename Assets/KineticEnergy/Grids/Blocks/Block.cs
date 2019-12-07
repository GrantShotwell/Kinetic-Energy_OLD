using System;
using System.Collections;
using System.Collections.Generic;
using KineticEnergy.Enumeration;
using KineticEnergy.Intangibles;
using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Maths;
using KineticEnergy.Structs;
using UnityEditor;
using UnityEngine;

namespace KineticEnergy.Grids.Blocks {

    #region Editor
#if UNITY_EDITOR
    using KineticEnergy.Unity;

    [CustomEditor(typeof(Block), true)]
    [CanEditMultipleObjects]
    public class Block_UnityEditor : Editor {
        Block script;
        SerializedProperty
            m_location;

        public void OnEnable() {
            script = target as Block;
            m_location = serializedObject.FindProperty("location");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            GUI.enabled = true;
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Dimensions != b.Dimensions);
            var dimensions = EditorGUILayout.Vector3IntField("Dimentions", script.Dimensions);
            if(dimensions != script.Dimensions) foreach(Block s in targets) s.Dimensions = dimensions;

            GUI.enabled = false;
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Location.Grid != b.Location.Grid);
            EditorGUILayout.PropertyField(m_location.FindPropertyRelative("grid"), new GUIContent("Grid Reference"));
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Location.GridVector != b.Location.GridVector);
            EditorGUILayout.PropertyField(m_location.FindPropertyRelative("vector"), new GUIContent("Grid Position"));

            GUI.enabled = Application.isEditor;
            var mass = Mass_UnityPropertyDrawer.LayoutFromValue(script.Mass, new GUIContent("Mass"),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.magnitude != b.Mass.magnitude),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.position != b.Mass.position));
            if(script.Mass != mass) foreach(Block s in targets) InspectorHelper.SetBlockMass(s, mass);

            GUI.enabled = true;
            script.OnInspectorGUI(targets, serializedObject);

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

        /// <summary>Method called when a nearby <see cref="Block"/> is added to the <see cref="Grid"/>.</summary>
        /// <param name="relativePosition"></param>
        public virtual void OnNearbyPieceAdded(Vector3Int relativePosition) { }

        /// <summary>Method called when a nearby <see cref="Block"/> is removed from the <see cref="Grid"/>.</summary>
        /// <param name="relativePosition"></param>
        public virtual void OnNearbyPieceRemoved(Vector3Int relativePosition) { }

        /// <summary></summary>
        /// <param name="relativePosition"></param>
        /// <param name="relativeFace"></param>
        public abstract bool SideIsOpaque(Vector3Int relativePosition, Face relativeFace);

        bool m_disabled = false;
        float m_disabledTime = 0f;
        /// <summary>Implementation of <see cref="MonoBehaviour"/> message.</summary>
        public void OnEnable() {
            BlockGrid grid = Location.Grid;
            if(grid != null) grid.PlaceEnablingBlock(this, transform.localPosition);
            StasisAwake(m_disabled ? 0f : m_disabledTime - Time.time);
        }
        /// <summary>[Base Call Not Needed]</summary>
        /// <param name="time"></param>
        public virtual void StasisAwake(float time) { }

        /// <summary>Implementation of <see cref="MonoBehaviour"/> message.</summary>
        public void OnDisable() {
            BlockGrid grid = Location.Grid;
            if(grid) grid.RemoveBlock(Location.GridVector);
            StasisAsleep();
            m_disabled = true;
            m_disabledTime = Time.time;
        }
        /// <summary>[Base Call Not Needed]</summary>
        public virtual void StasisAsleep() { }

        /// <summary>[Base Call Not Needed] Method called when prefab is loaded. Use this if you wanna do anything fancy with the prefab.</summary>
        public virtual void PrefabSetup() { }

#if UNITY_EDITOR
        /// <summary>Method called inside inspector after all basic properties of a block (mass, dimensions, ect.) are shown in the inspector.</summary>
        public abstract void OnInspectorGUI(UnityEngine.Object[] targets, SerializedObject serializedObject);
#endif

        #endregion

        #region Properties, Constants, and Setup Methods

        /// <summary>Every block contains a <see cref="BlockGrid.Traversal.Node"/> so that a new array does not have to be created for every <see cref="BlockGrid.Traversal"/>.</summary>
        public BlockGrid.Traversal.Node Node { get; set; } = null;

        [SerializeField] private Master master;

        [SerializeField] private Vector3Int dimensions;
        [SerializeField] private GridLocation location;
        [SerializeField] internal Mass mass;

        /// <summary>This <see cref="Block"/>'s reference to the <see cref="Master"/>.</summary>
        /// <exception cref="NullReferenceException">Thrown with a custom message when <see cref="master"/> has not been set.
        /// When this happens, <see cref="GlobalBehavioursManager.PolishBehaviour(MonoBehaviour)"/> has probably not been called yet.</exception>
        public Master Master {
            get => master ?? throw new NullReferenceException(string.Format("'{0}' is null. Was '{1}' called on this {2}?",
                nameof(Master), nameof(GlobalBehavioursManager.PolishBehaviour), nameof(Block)));
            internal set => master = value;
        }

        /// <summary>A user-friendly name.</summary>
        public string Name {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        /// <summary>The dimentions in grid space.</summary>
        public Vector3Int Dimensions {
            get => dimensions;
            set {
                dimensions = value;
                XArea = dimensions.y * dimensions.z;
                YArea = dimensions.z * dimensions.x;
                ZArea = dimensions.x * dimensions.y;
                SurfaceArea = (XArea + YArea + ZArea) * 2;
            }
        }

        /// <summary>The <see cref="GridLocation"/> that defines where this <see cref="Block"/> is.</summary>
        public GridLocation Location {
            get => location;
            set {

                BlockGrid oldGrid = location.Grid, newGrid = value.Grid;
                Vector3Int oldPos = location.GridVector, newPos = value.GridVector;
                GridLocation oldLocation = location, newLocation = value;
                location = value;

                if(oldGrid != newGrid) {
                    if(oldGrid) {
                        oldGrid.Mass -= Mass + oldPos;
                    }
                    if(newGrid) {
                        newGrid.Mass += Mass + newPos;
                        transform.SetParent(newGrid.gameObject.transform, false);
                        transform.localPosition = newPos + TransformOffset;
                    }
                } else if(oldPos != newPos) {
                    transform.localPosition = newPos;
                }

                VerifyLocation(oldLocation, newLocation);

            }
        }

        /// <summary>The difference between <see cref="Location"/> and <see cref="Transform.localPosition"/>.</summary>
        public virtual Vector3 TransformOffset => new Vector3(dimensions.x / 2f - 0.5f, dimensions.y / 2f - 0.5f, dimensions.z / 2f - 0.5f);

        /// <summary>Empty virtual method that is called inside <see cref="Location"/>'s set.</summary>
        /// <param name="oldLocation">The value of <see cref="Location"/> before set.</param>
        /// <param name="newLocation">The value of <see cref="Location"/> after set.</param>
        protected virtual void VerifyLocation(GridLocation oldLocation, GridLocation newLocation) { }

        /// <summary>The relative <see cref="Structs.Mass"/>.</summary>
        public Mass Mass {
            get => mass;
            set {
                BlockGrid grid = Location.Grid;
                if(grid) {
                    if(mass.magnitude == value.magnitude)
                        grid.Mass = grid.Mass.PartShifted(mass + Location.GridVector, value.position - mass.position);
                    else if(mass.position == value.position)
                        grid.Mass += value - mass;
                }
                mass = value;
            }
        }

        /// <summary>The top-right-front corner.</summary>
        public Vector3Int GridCorner => Dimensions - Vector3Int.one;

        /// <summary>The surface area on the x-axis sides defined by <see cref="Dimensions"/>.</summary>
        public int XArea { get; private set; }
        /// <summary>The surface area on the y-axis sides defined by <see cref="Dimensions"/>.</summary>
        public int YArea { get; private set; }
        /// <summary>The surface area on the z-axis sides defined by <see cref="Dimensions"/>.</summary>
        public int ZArea { get; private set; }
        /// <summary>The surface area defined by <see cref="Dimensions"/>.</summary>
        public int SurfaceArea { get; private set; }

        #region Neighbor Points

        public IEnumerable<Vector3Int> NeighborPoints => new EnumerationQueue<Vector3Int>(
            PosXNeighborPoints, NegXNeighborPoints,
            PosYNeighborPoints, NegYNeighborPoints,
            PosZNeighborPoints, NegZNeighborPoints);

        public IEnumerable<Vector3Int> PosXNeighborPoints {
            get {
                Vector3Int corner = GridCorner, dimensions = Dimensions;
                return new BoxOfPoints(new Vector3Int(dimensions.x, 0, 0), new Vector3Int(dimensions.x, corner.y, corner.z));
            }
        }

        public IEnumerable<Vector3Int> NegXNeighborPoints {
            get {
                Vector3Int corner = GridCorner;
                return new BoxOfPoints(new Vector3Int(-1, 0, 0), new Vector3Int(-1, corner.y, corner.z));
            }
        }

        public IEnumerable<Vector3Int> PosYNeighborPoints {
            get {
                Vector3Int corner = GridCorner, dimensions = Dimensions;
                return new BoxOfPoints(new Vector3Int(0, dimensions.y, 0), new Vector3Int(corner.x, dimensions.y, corner.z));
            }
        }

        public IEnumerable<Vector3Int> NegYNeighborPoints {
            get {
                Vector3Int corner = GridCorner;
                return new BoxOfPoints(new Vector3Int(0, -1, 0), new Vector3Int(corner.x, -1, corner.z));
            }
        }

        public IEnumerable<Vector3Int> PosZNeighborPoints {
            get {
                Vector3Int corner = GridCorner, dimensions = Dimensions;
                return new BoxOfPoints(new Vector3Int(0, 0, dimensions.z), new Vector3Int(corner.x, corner.y, dimensions.z));
            }
        }

        public IEnumerable<Vector3Int> NegZNeighborPoints {
            get {
                Vector3Int corner = GridCorner;
                return new BoxOfPoints(new Vector3Int(0, 0, -1), new Vector3Int(corner.x, corner.y, -1));
            }
        }

        #endregion
        #region Inside Points

        /// <summary>Every point held within; defined by <see cref="Dimensions"/>.</summary>
        public IEnumerable<Vector3Int> InsidePoints => new BoxOfPoints(Dimensions);

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

            public override string ToString() {
                string s = ((Mask32)value).ToString();
                return s.Substring(s.Length - 6, 6);
            }
        }

        /// <summary>Checks all faces and returns a <see cref="FaceMask"/> that represents which faces are covered.</summary>
        public FaceMask GetFacesShown() {

            FaceMask result = 0;
            BlockGrid grid = Location.Grid;
            if(!grid) return FaceMask.ALL;

            GetFacesShown(ref result, this, grid,
                PosXNeighborPoints.GetEnumerator(),
                NegXNeighborPoints.GetEnumerator(),
               Face.PosX, Face.NegX);

            GetFacesShown(ref result, this, grid,
                PosYNeighborPoints.GetEnumerator(),
                NegYNeighborPoints.GetEnumerator(),
                Face.PosY, Face.NegY);

            GetFacesShown(ref result, this, grid,
                PosZNeighborPoints.GetEnumerator(),
                NegZNeighborPoints.GetEnumerator(),
                Face.PosZ, Face.NegZ);

            return result;
        }

        private static void GetFacesShown(ref FaceMask result, Block block, BlockGrid grid,
            IEnumerator<Vector3Int> posNeighbors, IEnumerator<Vector3Int> negNeighbors, Face facePos, Face faceNeg) {
            Vector3Int gridPosition = block.Location.GridVector;

            bool posShown = false, negShown = false;
            while(posNeighbors.MoveNext() && negNeighbors.MoveNext()) {

                if(posShown == false &&
                    !grid.SideIsOpaque(
                        grid.LocalPoint_to_LocalGrid(block.LocalPoint_to_RelativePoint(posNeighbors.Current))
                        + gridPosition, facePos)
                ) {
                    posShown = true;
                }

                if(negShown == false &&
                    !grid.SideIsOpaque(
                        grid.LocalPoint_to_LocalGrid(block.LocalPoint_to_RelativePoint(negNeighbors.Current))
                        + gridPosition, faceNeg)
                ) {
                    negShown = true;
                }

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
        public Vector3 LocalPoint_to_RelativePoint(Vector3 localPoint) => transform.localRotation * localPoint;

        #endregion

    }

}
