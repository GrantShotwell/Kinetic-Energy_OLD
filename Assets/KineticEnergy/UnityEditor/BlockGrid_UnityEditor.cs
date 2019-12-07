#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Structs;

namespace KineticEnergy.Unity {

    [CustomEditor(typeof(BlockGrid))]
    [CanEditMultipleObjects]
    public sealed class BlockGrid_UnityEditor : Editor {
        BlockGrid script;

        public void OnEnable() {
            script = (BlockGrid)target;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            var size = script.Size;

            GUI.enabled = false;
            EditorGUILayout.Vector3IntField("Current Array Size", size);

            GUI.enabled = false;
            int count = 0, volume = size.x * size.y * size.z; foreach(Block block in script) if(block != null) count++;
            EditorGUILayout.IntSlider("Total Blocks (of " + volume + ")", count, 0, volume);

            GUI.enabled = false;
            var _mass = Mass_UnityPropertyDrawer.LayoutFromValue(script.Mass, new GUIContent("Mass"),
                InspectorHelper.TargetsAreMixed<BlockGrid>(targets, (a, b) => a.Mass.magnitude != b.Mass.magnitude),
                InspectorHelper.TargetsAreMixed<BlockGrid>(targets, (a, b) => a.Mass.position != b.Mass.position));

            if(!Application.isPlaying) {
                GUI.enabled = true;
                EditorGUILayout.HelpBox("Unity often resets the value of a grid's array to empty.\n" +
                    "This is fixed from Block.OnEnable(), where they add themselfs to the grid.", MessageType.Info);
            }


            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomPropertyDrawer(typeof(Mass))]
    public class Mass_UnityPropertyDrawer : PropertyDrawer {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => base.GetPropertyHeight(property, label) * 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position.height = base.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property.FindPropertyRelative("magnitude"), label);

            position.y += position.height;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("position"), GUIContent.none);

            EditorGUI.EndProperty();
        }

        public static Mass LayoutFromValue(Mass mass, GUIContent label, bool amountMixed = false, bool positionMixed = false) {
            EditorGUI.showMixedValue = amountMixed;
            if(!long.TryParse(
                EditorGUILayout.TextField(label, mass.magnitude.ToString()),
                out long amount)) amount = 0;
            EditorGUI.showMixedValue = positionMixed;
            Vector3 position = EditorGUILayout.Vector3Field(GUIContent.none, mass.position);
            return new Mass(amount, position);
        }
    }

}

#endif