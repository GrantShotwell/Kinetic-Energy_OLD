using KineticEnergy.Unity;
using UnityEditor;
using UnityEngine;

namespace KineticEnergy.Ships.Blocks {

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(OpaqueBlock), true)]
    [CanEditMultipleObjects]
    public class OpaqueBlock_UnityEditor : Editor {
        OpaqueBlock script;
        SerializedProperty
            m_collider,
            m_faces;

        public void OnEnable() {
            script = (OpaqueBlock)target;
            m_collider = serializedObject.FindProperty("collider");
            m_faces = serializedObject.FindProperty("faces");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            GUI.enabled = true;
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Dimensions != b.Dimensions);
            var _dimentions = EditorGUILayout.Vector3IntField("Dimentions", script.Dimensions);
            if(_dimentions != script.Dimensions) foreach(Block s in targets) s.Dimensions = _dimentions;

            GUI.enabled = false;
            EditorGUILayout.TextField(script.Grid.gameObject.name);
            EditorGUILayout.Vector3IntField("Position in Grid", script.GridPosition);

            GUI.enabled = true;
            EditorGUILayout.PropertyField(m_collider, new GUIContent("Collider"));

            GUI.enabled = true;
            var _mass = Mass_UnityPropertyDrawer.LayoutFromValue(script.Mass, new GUIContent("Mass"),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.magnitude != b.Mass.magnitude),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.position != b.Mass.position));
            if(script.Mass != _mass) foreach(Block s in targets) s.SetMass_Inspector(_mass);

            GUI.enabled = true;
            EditorGUILayout.PropertyField(m_faces, new GUIContent("Faces"));

            GUI.enabled = true;
            script.OnInspectorGUI(targets);

            serializedObject.ApplyModifiedProperties();
        }

    }

#endif
    #endregion

    /// <summary>All sides are opaque.</summary>
    public abstract class OpaqueBlock : Block {

        new public Collider collider;
        public FaceRefs faces;

        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of an opaque block: every side is opaque.
            return true;
        }

        public override void OnGridUpdated() {
            UpdateFaces();
        }

        public override void OnNearbyPieceAdded(Vector3Int relativePosition) {
            UpdateFaces();
        }

        public override void OnNearbyPieceRemoved(Vector3Int relativePosition) {
            UpdateFaces();
        }

        public void UpdateFaces() {
            FaceMask enabledFaces = GetFacesShown();
            faces.ToggleFaces(enabledFaces);
            collider.enabled = (enabledFaces & 0b111111) != 0;
        }

    }

}
