#if UNITY_EDITOR
using KineticEnergy.Inventory;
using UnityEditor;
using UnityEngine;

namespace KineticEnergy.Unity {

    [CustomPropertyDrawer(typeof(Size))]
    public class Size_UnityPropertyDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), label);
            EditorGUI.EndProperty();
        }

    }

}

#endif