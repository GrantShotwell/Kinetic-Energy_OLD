#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.CodeTools.Math;

namespace KineticEnergy.Unity {

    [CustomEditor(typeof(Block), true)]
    [CanEditMultipleObjects]
    public class Block_UnityEditor : Editor {
        SerializedProperty
            m_grid;

        public void OnEnable() {
            m_grid = serializedObject.FindProperty("grid");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            foreach(Block script in targets) {

                /**/ GUI.enabled = true;
                EditorGUI.showMixedValue = InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Dimensions != b.Dimensions);
                var _dimentions = EditorGUILayout.Vector3IntField("Dimentions", script.Dimensions);
                if(_dimentions != script.Dimensions) foreach(Block s in targets) s.Dimensions = _dimentions;

                /**/ GUI.enabled = false;
                EditorGUILayout.PropertyField(m_grid, new GUIContent("Grid Reference"));
                EditorGUILayout.Vector3IntField("Position in Grid", script.gridPosition);

                /**/ GUI.enabled = Application.isEditor;
                var _mass = Mass_UnityPropertyDrawer.LayoutFromValue(script.Mass, new GUIContent("Mass"),
                    InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.magnitude != b.Mass.magnitude),
                    InspectorHelper.TargetsAreMixed<Block>(targets, (a, b) => a.Mass.position != b.Mass.position));
                if(script.Mass != _mass) foreach(Block s in targets) s.SetMassFromInspector(_mass);

            }

            serializedObject.ApplyModifiedProperties();
        }

    }

    [CustomPropertyDrawer(typeof(Faces))]
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

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return HEIGHT;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var original_indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            if(
                GUI.RepeatButton(new Rect(position.center.x - (25f / 2f), position.center.y - (26f / 2f), 25, 26), GUIContent.none)
            ) {

                var style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
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

}

#endif