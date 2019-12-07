using UnityEngine;
using UnityEditor;

namespace KineticEnergy.Grids.Blocks {

    /// <summary>No sides are opaque.</summary>
    public abstract class TransparentBlock : Block {

        #region Unity Inspector
#if UNITY_EDITOR
        public override void OnInspectorGUI(Object[] targets, SerializedObject serializedObject) {

        }
#endif
        #endregion

        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of a transparent block: every side is non-opaque.
            return false;
        }

    }

}
