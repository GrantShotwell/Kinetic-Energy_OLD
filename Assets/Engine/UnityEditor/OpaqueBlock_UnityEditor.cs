#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.CodeTools;
using KineticEnergy.Ships;

namespace KineticEnergy.Unity {

    [CustomEditor(typeof(OpaqueBlock), true)]
    [CanEditMultipleObjects]
    public class OpaqueBlock_UnityEditor : Editor {
        OpaqueBlock script;
        SerializedProperty
            m_grid,
            m_collider,
            m_faces;

        public void OnEnable() {
            script = (OpaqueBlock)target;
            m_grid = serializedObject.FindProperty("grid");
            m_collider = serializedObject.FindProperty("collider");
            m_faces = serializedObject.FindProperty("faces");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            /**/ GUI.enabled = true;
            EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Dimensions != b.Dimensions);
            var _dimentions = EditorGUILayout.Vector3IntField("Dimentions", script.Dimensions);
            if(_dimentions != script.Dimensions) foreach(Block s in targets) s.Dimensions = _dimentions;

            /**/ GUI.enabled = false;
            EditorGUILayout.PropertyField(m_grid, new GUIContent("Grid Reference"));
            EditorGUILayout.Vector3IntField("Position in Grid", script.gridPosition);

            /**/ GUI.enabled = true;
            EditorGUILayout.PropertyField(m_collider, new GUIContent("Collider"));

            /**/ GUI.enabled = true;
            var _mass = Mass_UnityPropertyDrawer.LayoutFromValue(script.Mass, new GUIContent("Mass"),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.magnitude != b.Mass.magnitude),
                InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.position != b.Mass.position));
            if(script.Mass != _mass) foreach(Block s in targets) s.SetMassFromInspector(_mass);

            /**/ GUI.enabled = true;
            EditorGUILayout.PropertyField(m_faces, new GUIContent("Faces"));

            serializedObject.ApplyModifiedProperties();
        }

    }

}

#endif