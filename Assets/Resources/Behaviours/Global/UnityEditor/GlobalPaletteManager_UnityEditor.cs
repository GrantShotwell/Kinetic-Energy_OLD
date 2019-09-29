#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KineticEnergy.Intangibles.Global;
using UnityEditor.AnimatedValues;

namespace KineticEnergy.Unity {

    [CustomEditor(typeof(GlobalPaletteManager))]
    public sealed class GlobalPaletteManager_UnityEditor : Editor {
        SerializedProperty
            _blocks,
            _colors;

        public void OnEnable() {
            _blocks = serializedObject.FindProperty("blocks");
            _colors = serializedObject.FindProperty("colors");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            /**/
            GUI.enabled = Application.isPlaying;
            EditorGUILayout.PropertyField(_blocks, new GUIContent("Block Palette"), true);

            /**/
            GUI.enabled = true;
            EditorGUILayout.PropertyField(_colors, new GUIContent("Color Palette"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomPropertyDrawer(typeof(BlockPalette))]
    public class BlockPalette_UnityPropertyDrawer : PropertyDrawer {
        const float SPACING = 3;
        readonly AnimBool shown = new AnimBool(false);
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var list = property.FindPropertyRelative("samples");
            var height = base.GetPropertyHeight(list, label);
            if(shown.faded != 0) {
                float h = 0;
                for(int i = 0; i < list.arraySize; i++) {
                    var element = list.GetArrayElementAtIndex(i);
                    h += base.GetPropertyHeight(element, GUIContent.none) + SPACING;
                }
                height += h * shown.faded;
            }
            return height;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            EditorGUI.BeginProperty(position, label, property);
            position.height = base.GetPropertyHeight(property, label);
            shown.target = EditorGUI.BeginFoldoutHeaderGroup(position, shown.target, label);
            if(shown.value) {
                EditorGUI.indentLevel++;
                position.height *= shown.faded;
                var list = property.FindPropertyRelative("samples");
                for(int i = 0; i < list.arraySize; i++) {
                    //Move the position downwards.
                    position.y += position.height + SPACING;
                    //Get the array element.
                    var element = list.GetArrayElementAtIndex(i);
                    //Show the element.
                    EditorGUI.PropertyField(position, element, GUIContent.none);
                }
                EditorGUI.indentLevel--;
            }
            if(shown.faded != 0 && shown.faded != 1) EditorUtility.SetDirty(property.serializedObject.targetObject);
            EditorGUI.EndFoldoutHeaderGroup();
            EditorGUI.EndProperty();

        }
    }

    [CustomPropertyDrawer(typeof(BlockPalette.Sample))]
    public class BlockSample_UnityPropertyDrawer : PropertyDrawer {
        const float SPACING = 5;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            label.text = GUIContent.none.text;
            EditorGUI.BeginProperty(position, label, property);
            var left = new Rect(position.x, position.y, position.width / 2, position.height);
            var right = new Rect(position.x + left.width + SPACING, position.y, position.width - left.width - SPACING, position.height);

            EditorGUI.PropertyField(left, property.FindPropertyRelative("prefabBlock"), GUIContent.none);
            var indent = EditorGUI.indentLevel; EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(right, property.FindPropertyRelative("prefabBlock_preview"), GUIContent.none);
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();

        }

    }

    [CustomPropertyDrawer(typeof(ColorPalette))]
    public class ColorPalette_UnityPropertyDrawer : PropertyDrawer {
        const float SPACING = 3;
        AnimBool shown = new AnimBool(false);
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var list = property.FindPropertyRelative("samples");
            var height = base.GetPropertyHeight(list, label);
            if(shown.faded != 0) {
                float h = 0;
                for(int i = 0; i < list.arraySize; i++) {
                    var element = list.GetArrayElementAtIndex(i);
                    h += base.GetPropertyHeight(element, GUIContent.none) + SPACING;
                }
                height += h * shown.faded;
            }
            return height;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            EditorGUI.BeginProperty(position, label, property);
            position.height = base.GetPropertyHeight(property, label);
            shown.target = EditorGUI.BeginFoldoutHeaderGroup(position, shown.target, label);
            if(shown.value) {
                EditorGUI.indentLevel++;
                position.height *= shown.faded;
                var list = property.FindPropertyRelative("samples");
                for(int i = 0; i < list.arraySize; i++) {
                    //Move the position downwards.
                    position.y += position.height + SPACING;
                    //Get the array element.
                    var element = list.GetArrayElementAtIndex(i);
                    //Show the element.
                    EditorGUI.PropertyField(position, element, GUIContent.none);
                }
                EditorGUI.indentLevel--;
            }
            if(shown.faded != 0 && shown.faded != 1) EditorUtility.SetDirty(property.serializedObject.targetObject);
            EditorGUI.EndFoldoutHeaderGroup();
            EditorGUI.EndProperty();

        }
    }

    [CustomPropertyDrawer(typeof(ColorPalette.Sample))]
    public class ColorSample_UnityPropertyDrawer : PropertyDrawer {
        const float SPACING = 5;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            label.text = GUIContent.none.text;
            EditorGUI.BeginProperty(position, label, property);
            var left = new Rect(position.x, position.y, position.width / 2, position.height);
            var right = new Rect(position.x + left.width + SPACING, position.y, position.width - left.width - SPACING, position.height);

            EditorGUI.PropertyField(left, property.FindPropertyRelative("name"), GUIContent.none);
            var indent = EditorGUI.indentLevel; EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(right, property.FindPropertyRelative("color"), GUIContent.none);
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();

        }

    }

}

#endif