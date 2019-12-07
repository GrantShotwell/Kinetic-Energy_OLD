using KineticEnergy.Structs;
using UnityEditor;
using UnityEngine;

namespace KineticEnergy.Grids.Blocks {

    /// <summary>All sides are opaque.</summary>
    public abstract class OpaqueBlock : Block {

        #region Unity Inspector
#if UNITY_EDITOR
        /// <summary>Implementation of <see cref="Block.OnInspectorGUI(Object[], SerializedObject)"/>.</summary>
        public override void OnInspectorGUI(Object[] targets, SerializedObject serializedObject) {

            GUI.enabled = true;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("faces"), new GUIContent("Faces"));

        }
#endif
        #endregion

        private new Collider collider;
        [SerializeField] private FaceRefs faces;

        /// <summary></summary>
        public Collider Collider {
            get => collider ? collider : collider = GetComponent<Collider>();
            set => collider = value;
        }

        /// <summary>The <see cref="Block.FaceRefs"/> of this <see cref="OpaqueBlock"/>.</summary>
        public FaceRefs Faces { get => faces; set => faces = value; }

        /// <summary>Implementation of <see cref="Block.SideIsOpaque(Vector3Int, Face)"/></summary>
        /// <param name="localPosition">Position local to this <see cref="Block"/> (not rotated).</param>
        /// <param name="face"></param>
        /// <returns></returns>
        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of an opaque block: every side is opaque.
            return true;
        }

        /// <summary>Implementation of <see cref="Block.VerifyLocation"/>.</summary>
        protected override void VerifyLocation(GridLocation oldLocation, GridLocation newLocation) => UpdateFaces();

        /// <summary>Implemenation of <see cref="Block.OnNearbyPieceAdded(Vector3Int)"/>.</summary>
        public override void OnNearbyPieceAdded(Vector3Int relativePosition) => UpdateFaces();
        /// <summary>Implemenation of <see cref="Block.OnNearbyPieceRemoved(Vector3Int)"/>.</summary>
        public override void OnNearbyPieceRemoved(Vector3Int relativePosition) => UpdateFaces();

        /// <summary>Updates <see cref="Faces"/>.</summary>
        public void UpdateFaces() {
            FaceMask enabledFaces = GetFacesShown();
            Faces.ToggleFaces(enabledFaces);
            Collider.enabled = (enabledFaces & 0b111111) != 0;
        }

    }

}
