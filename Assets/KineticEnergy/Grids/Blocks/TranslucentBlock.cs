using System;
using UnityEngine;
using UnityEditor;

namespace KineticEnergy.Grids.Blocks {

    /// <summary>Some sides are opaque.</summary>
    public abstract class TranslucentBlock : Block {

        #region Unity Inspector
#if UNITY_EDITOR
        public override void OnInspectorGUI(UnityEngine.Object[] targets, SerializedObject serializedObject) {

        }
#endif
        #endregion

        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of a translucent block (in this context): some sides are opaque.
            throw new NotImplementedException();
        }

    }

}
